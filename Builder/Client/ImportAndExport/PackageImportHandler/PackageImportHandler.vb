
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.IO
Imports System.ComponentModel
Imports System.Globalization
Imports System.Linq
Imports Questify.Builder.Logic.Service.InvalidateCache.Helper
Imports Questify.Builder.Logic.Service.Factories
Imports System.Text
Imports Enums
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.ValidatorClasses
Imports Questify.Builder.Client.BuilderTasksService
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Packaging
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class PackageImportHandler
    Implements IDisposable
    Implements IImportHandler, IImportHandlerQuestifyExportFiles

    Private WithEvents _importMan As ImportManager
    Private _optionsControl As ImportOptionControlBase
    Private ReadOnly _bankNameCollection As New Dictionary(Of Integer, String)
    Private ReadOnly _parentBankCollection As New Dictionary(Of Integer, Integer?)
    Private ReadOnly _leaveBankCollection As New Dictionary(Of Integer, IEnumerable(Of Integer))
    Private _harmonizeWorker As BackgroundWorker


    Public Property HasError As Boolean

    Public ReadOnly Property SupportedResourceTypes As String Implements IImportHandler.SupportedResourceTypes
        Get
            Return "all"
        End Get
    End Property

    Public ReadOnly Property PackageImportOptions As PackageImportOptionsDataEntity
        Get
            Return DirectCast(_optionsControl, PackageImportOptionsControl).Options
        End Get
    End Property



    Public ReadOnly Property GetOptionsUserControl As ImportOptionControlBase Implements IImportHandler.GetOptionsUserControl
        Get
            If _optionsControl Is Nothing Then
                _optionsControl = New PackageImportOptionsControl
            End If
            Return _optionsControl
        End Get
    End Property

    Public ReadOnly Property ProgressMessage As String Implements IImportHandler.ProgressMessage
        Get
            Return My.Resources.ImportingResourcesToSpecifiedLocation
        End Get
    End Property


    Public ReadOnly Property ImportFileIsPackage As Boolean Implements IImportHandler.ImportFileIsPackage
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property UserFriendlyName As String Implements IImportHandler.UserFriendlyName
        Get
            Return My.Resources.ImportFromATestBuilderPackageFile
        End Get
    End Property

    Public Function Import(packageSet As PackageSet, parentBankId As Integer?) As Boolean Implements IImportHandler.Import
        If packageSet IsNot Nothing Then
            Return ImportPackageSetEntryCollection(packageSet.PackageSetEntryCollection, parentBankId, packageSet.PackageSetRoot)
        Else
            Return False
        End If
    End Function


    Private Function ImportPackageSetEntryCollection(packageSetEntryCollection As PackageSetEntryCollection, parentBankId As Integer?, packageSetRoot As Uri) As Boolean
        Dim result = True
        Using New NotifyCacheAfterBatch
            For Each entry As PackageSetEntry In packageSetEntryCollection
                Dim bankId As Integer? = CreateOrGetBank(entry.Name, parentBankId)

                If bankId.HasValue AndAlso Not String.IsNullOrEmpty(entry.PackageUri) Then
                    Dim absolutePackageUri As New Uri(packageSetRoot, entry.PackageUri & "/")
                    If Not ImportPackage(absolutePackageUri, bankId.Value) Then
                        HasError = True
                        result = False
                    End If
                End If

                If Not ImportPackageSetEntryCollection(entry.PackageSetEntrySubCollection, bankId, packageSetRoot) Then
                    HasError = True
                    result = False
                End If
            Next
        End Using
        Return result
    End Function

    Private Function CreateOrGetBank(bankName As String, parentBankId As Integer?) As Integer?
        Dim bankToReturn As Integer? = Nothing

        If parentBankId.HasValue Then
            Dim bankDto As BankDto = DtoFactory.Bank.Get(parentBankId.Value)

            Dim subBank = bankDto.BankCollection.FirstOrDefault(Function(b) b.name.Equals(bankName, StringComparison.InvariantCultureIgnoreCase))
            If subBank IsNot Nothing Then
                bankToReturn = subBank.id
            End If

            If Not bankToReturn.HasValue Then
                Dim newBankToReturn = New BankEntity
                With newBankToReturn
                    .Name = bankName
                    .ParentBankId = parentBankId.Value
                End With

                BankFactory.Instance.UpdateBank(newBankToReturn)
                bankToReturn = newBankToReturn.Id
            End If
        Else
            Dim bankHierarchy = DtoFactory.Bank.All

            Dim bank = bankHierarchy.FirstOrDefault(Function(b) b.name.Equals(bankName, StringComparison.InvariantCultureIgnoreCase))
            If bank IsNot Nothing Then
                bankToReturn = bank.id
            End If

            If Not bankToReturn.HasValue Then
                Dim newBankToReturn = New BankEntity
                With newBankToReturn
                    .Name = bankName
                    .ParentBank = Nothing
                End With

                BankFactory.Instance.UpdateBank(newBankToReturn)
                bankToReturn = newBankToReturn.Id
            End If
        End If
        Return bankToReturn
    End Function


    Private Function ImportPackage(packageUri As Uri, parentBankId As Integer) As Boolean
        Dim manifest As ResourceManifest = ResourceManifest.Load(New Uri(packageUri, "Manifest.xml"))
        Dim manifestMetaData As ResourceMetaDataManifest = ResourceMetaDataManifest.Load(New Uri(packageUri, "ManifestMetaData.xml"))
        Dim result As Boolean

        Using resManager As New ManifestResourceManager(manifest, manifestMetaData, Nothing, packageUri, Guid.NewGuid.ToString)
            result = Import(resManager, parentBankId)
            PackageManager.RemovePackageFromCache(packageUri)
        End Using

        Return result
    End Function


    Public Function Import(parentBankId As Integer?) As Boolean Implements IImportHandler.Import
        Throw New NotImplementedException("Function used for importhandlers that don't import packages.")
    End Function


    Public Function Import(ByVal sourceResourceManager As ResourceManagerBase, bankId As Integer) As Boolean Implements IImportHandler.Import
        Dim importResult As Boolean

        If Not TypeOf sourceResourceManager Is ManifestResourceManager Then
            Throw New Exception("TypeOf sourceResourceManager Is Not ManifestResourceManager")
        End If

        Dim manResMan = DirectCast(sourceResourceManager, ManifestResourceManager)
        Dim destinationResourceMgrFactory = New DbResourceManagerFactory(bankId)
        Try
            Using New NotifyCacheAfterBatch
                Dim importedCustomProperties As Boolean
                Dim importRelatedCustomProperties As New EntityCollection

                Using dbResMan As New DataBaseResourceManager(bankId)
                    importedCustomProperties = ImportCustomProperties(manResMan, dbResMan, importRelatedCustomProperties)
                End Using

                If importedCustomProperties Then
                    Dim resources As ResourceEntryCollection = manResMan.Manifest.Resources

                    _importMan = New ImportManager(sourceResourceManager, destinationResourceMgrFactory)
                    importResult = _importMan.ImportResources(resources)

                    If importResult Then
                        importResult = CheckTestPartValidationRules(resources, bankId)
                        Dim resourceIds As List(Of Guid) = GetResourceIds(bankId, resources)
                        importResult = SetDisplayValuesForImportRelatedCustomProperties(resourceIds, importRelatedCustomProperties.Select(Function(cp) DirectCast(cp, CustomBankPropertyEntity)).ToList(), bankId)
                    End If
                Else
                    importResult = False
                End If
                If TypeOf _optionsControl Is PackageImportOptionsControl Then
                    TestBuilderClientSettings.ImportLocation = Path.GetDirectoryName(DirectCast(_optionsControl, PackageImportOptionsControl).Options.Url)
                End If
            End Using
        Catch ex As Exception
            Dim innerErrorMessage As String
            Dim eError As ImportExportHandlerHandleErrorEventArgs

            If ex.InnerException IsNot Nothing Then
                innerErrorMessage = ex.InnerException.Message
                eError = New ImportExportHandlerHandleErrorEventArgs(String.Format(CultureInfo.InvariantCulture, "{0} - {1}", ex.Message, innerErrorMessage))
            Else
                eError = New ImportExportHandlerHandleErrorEventArgs(String.Format(CultureInfo.InvariantCulture, "{0}", ex.Message))
            End If

            HasError = True
            RaiseEvent ImportHandlerHandleError(Me, eError)
        End Try

        If Not importResult Then
            HasError = True
        End If
        Return importResult
    End Function

    Private Function GetResourceIds(bankId As Integer, resources As ResourceEntryCollection) As List(Of Guid)
        Dim resourceIds As New List(Of Guid)
        Dim resourcesWithCustomProperties As String() = {String.Format(CultureInfo.InvariantCulture, "{0}entity", ResourceTypeEnum.GenericResource.ToString.ToLower),
                                                         String.Format(CultureInfo.InvariantCulture, "{0}entity", ResourceTypeEnum.ItemResource.ToString.ToLower),
                                                         String.Format(CultureInfo.InvariantCulture, "{0}entity", ResourceTypeEnum.AssessmentTestResource.ToString.ToLower)}

        Dim resourceNames = resources.Where(Function(r) resourcesWithCustomProperties.Any(Function(name) name.Equals(r.Type,
            StringComparison.CurrentCultureIgnoreCase))).Select(Function(r) r.Name).ToList
        Dim resourcesByNames = ResourceFactory.Instance.GetResourcesByNamesWithOption(bankId, resourceNames, New ResourceRequestDTO())
        If resourcesByNames IsNot Nothing AndAlso resourcesByNames.All(Function(x) TypeOf x Is ResourceEntity) Then
            Return resourcesByNames.Select(Function(res) CType(res, ResourceEntity).ResourceId).ToList()
        End If
        Return resourceIds
    End Function



    Public Sub Harmonize(resourceType As ResourceTypeEnum, resourceGuids As IEnumerable(Of Guid)) Implements IImportHandlerQuestifyExportFiles.Harmonize
        StartHarmonization(resourceType, resourceGuids)
    End Sub

    Public Sub HarmonizeAfterImport(bankId As Integer, templatesToHarmonize As List(Of Guid), itemCodesToHarmonize As IEnumerable(Of String)) Implements IImportHandlerQuestifyExportFiles.HarmonizeAfterImport
        StartHarmonizationAfterImport(bankId, templatesToHarmonize, itemCodesToHarmonize)
    End Sub

    Public Sub CancelHarmonization() Implements IImportHandlerQuestifyExportFiles.CancelHarmonization
        If _harmonizeWorker IsNot Nothing AndAlso _harmonizeWorker.IsBusy Then
            _harmonizeWorker.CancelAsync()
        End If
    End Sub



    Private Function SetDisplayValuesForImportRelatedCustomProperties(resourceIds As List(Of Guid), importRelatedCustomProperties As List(Of CustomBankPropertyEntity),
                                                                      bankId As Integer) As Boolean
        Dim result = True
        Dim knownCustomProperties As List(Of CustomBankPropertyDto) = Nothing
        If importRelatedCustomProperties.Count > 0 AndAlso resourceIds.Count > 0 Then
            Dim index = 1
            Dim updatedCustomPropertiesValues As New EntityCollection
            Dim entitiesToProgress = BankFactory.Instance.GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(importRelatedCustomProperties.
                                                                                                                     Select(Function(cp) cp.CustomBankPropertyId).ToList(), resourceIds, True)
            RaiseEvent StartProgress(Nothing, New StartEventArgs(entitiesToProgress.Count))

            For Each value As CustomBankPropertyValueEntity In entitiesToProgress
                If knownCustomProperties Is Nothing Then
                    knownCustomProperties = New List(Of CustomBankPropertyDto)
                    knownCustomProperties.AddRange(DtoFactory.CustomBankProperty.GetMulti(BankFactory.Instance.GetCustomBankPropertiesForBranchById(bankId, ResourceTypeEnum.AllResources).
                                                                                          OfType(Of CustomBankPropertyEntity).Select(Function(c) c.CustomBankPropertyId)))
                End If
                value.SetCustomPropertyDisplayValue(knownCustomProperties)
                updatedCustomPropertiesValues.Add(value)

                RaiseEvent Progress(Nothing, New ProgressEventArgs(My.Resources.ImportSettingCustomPropertyDisplayValue, index))
                index += 1

                If updatedCustomPropertiesValues.Count = 500 Then
                    BankFactory.Instance.UpdateCustomPropertyValues(updatedCustomPropertiesValues)
                    updatedCustomPropertiesValues.Clear()
                End If
            Next

            BankFactory.Instance.UpdateCustomPropertyValues(updatedCustomPropertiesValues)
        End If

        Return result
    End Function

    Private Function CheckTestPartValidationRules(resources As ResourceEntryCollection, bankId As Integer) As Boolean
        Dim result As Boolean
        Dim testResources = resources.Where(Function(r) r.Type = "AssessmentTestResourceEntity")
        Dim testResourceEntities = ResourceFactory.Instance.GetAssessmentTestsForBank(bankId)
        If testResources.Count > 0 Then
            Dim customBankProperties = GetCustomBankProperties(bankId)
            For Each test In testResources
                Dim testResource = testResourceEntities.OfType(Of AssessmentTestResourceEntity).FirstOrDefault(Function(tr) tr.Name.Equals(test.Name, StringComparison.CurrentCultureIgnoreCase))
                If (testResource Is Nothing) Then
                    Continue For
                End If

            Next
        End If

        Return result
    End Function

    Private Function GetCustomBankProperties(bankId As Integer) As List(Of CustomBankPropertyEntity)
        Return BankFactory.Instance.GetCustomBankPropertiesForBranchById(bankId, ResourceTypeEnum.AllResources).OfType(Of CustomBankPropertyEntity).Where(
            Function(cbp)
                Return TypeOf cbp Is ListCustomBankPropertyEntity OrElse TypeOf cbp Is TreeStructureCustomBankPropertyEntity
            End Function).ToList()
    End Function

    Private Sub StartHarmonization(resourceType As ResourceTypeEnum, resourceGuids As IEnumerable(Of Guid))
        Dim harmonizer As New HarmonizeDuringImport(resourceType, resourceGuids)
        StartHarmonization(harmonizer)
    End Sub

    Private Sub StartHarmonizationAfterImport(bankId As Integer, templatesToHarmonize As List(Of Guid), itemCodesToHarmonize As IEnumerable(Of String))
        Dim harmonizer As New HarmonizeDuringImport(bankId, templatesToHarmonize, itemCodesToHarmonize)
        StartHarmonization(harmonizer)
    End Sub

    Private Sub StartHarmonization(harmonizer As HarmonizeDuringImport)
        RaiseEvent StartProgress(Nothing, New StartEventArgs(100))
        RaiseEvent Progress(Nothing, New ProgressEventArgs(My.Resources.InitializingHarmonization, 0))
        _harmonizeWorker = New BackgroundWorker
        _harmonizeWorker.WorkerReportsProgress = True
        _harmonizeWorker.WorkerSupportsCancellation = True
        AddHandler _harmonizeWorker.ProgressChanged, AddressOf HarmonizeWorker_ProgressChanged
        AddHandler _harmonizeWorker.RunWorkerCompleted, AddressOf HarmonizeWorker_RunworkerCompleted
        AddHandler _harmonizeWorker.DoWork, AddressOf Harmonizer_DoWork

        _harmonizeWorker.RunWorkerAsync(harmonizer)
    End Sub

    Private Sub HarmonizeWorker_RunworkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        Dim result = TryCast(e.Result, BuilderTaskResult)
        RaiseEvent HarmonizeCompleted(result)
        RemoveHandler _harmonizeWorker.ProgressChanged, AddressOf HarmonizeWorker_ProgressChanged
        RemoveHandler _harmonizeWorker.RunWorkerCompleted, AddressOf HarmonizeWorker_RunworkerCompleted
        RemoveHandler _harmonizeWorker.DoWork, AddressOf Harmonizer_DoWork
        _harmonizeWorker.Dispose()
    End Sub

    Private Sub Harmonizer_DoWork(sender As Object, e As DoWorkEventArgs)
        TryCast(e.Argument, HarmonizeDuringImport)?.HarmonizeOnServer(sender, e)
    End Sub

    Private Sub HarmonizeWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        Dim message As String = String.Empty
        If e.UserState IsNot Nothing Then
            message = e.UserState.ToString
        End If
        RaiseEvent Progress(Nothing, New ProgressEventArgs(message, e.ProgressPercentage))
    End Sub


    Private Sub FillBankCacheWhileRetrievingBank(bank As BankEntity)
        Dim bankId = bank.Id
        If Not _bankNameCollection.ContainsKey(bankId) Then
            _bankNameCollection.Add(bankId, bank.Name)
        End If
        If Not _parentBankCollection.ContainsKey(bankId) Then
            _parentBankCollection.Add(bankId, bank.ParentBankId)
        End If
        If Not _leaveBankCollection.ContainsKey(bankId) AndAlso bank.BankCollection IsNot Nothing Then
            _leaveBankCollection.Add(bankId, bank.BankCollection.Select(Function(b) b.Id))
            For Each b In bank.BankCollection
                FillBankCacheWhileRetrievingBank(b)
            Next
        End If
    End Sub


    Private Function GetBankName(bankId As Integer) As String
        If Not _bankNameCollection.ContainsKey(bankId) Then
            Dim bank = BankFactory.Instance.GetBank(bankId)
            FillBankCacheWhileRetrievingBank(bank)
        End If
        Return _bankNameCollection(bankId)
    End Function


    Private Function GetParentBank(bankId As Integer) As Integer?
        If Not _parentBankCollection.ContainsKey(bankId) Then
            Dim bank = BankFactory.Instance.GetBank(bankId)
            FillBankCacheWhileRetrievingBank(bank)
        End If
        Return _parentBankCollection(bankId)
    End Function


    Private Function ImportCustomProperties(sourceManResMan As ManifestResourceManager, destinationDbResMan As DataBaseResourceManager, ByRef newCustomProperties As EntityCollection) As Boolean
        Dim bankId As Integer = destinationDbResMan.BankId
        Dim result = True

        If Not sourceManResMan.ManifestMetaData.CustomPropertyDefinitions.Any() Then
            Return result
        End If

        Dim existingCustomProperties As EntityCollection = BankFactory.Instance.GetCustomBankPropertiesForBranchById(bankId, ResourceTypeEnum.AllResources)
        Dim conflictingCustomProperties As New EntityCollection

        For i = 0 To sourceManResMan.ManifestMetaData.CustomPropertyDefinitions.Count - 1
            With sourceManResMan.ManifestMetaData.CustomPropertyDefinitions(i)
                If .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty Then
                    Dim existingBankProperty As CustomBankPropertyEntity = Nothing

                    Dim results As List(Of Integer)
                    Dim filter = New PredicateExpression()
                    Dim fieldCompare = New FieldCompareValuePredicate(CustomBankPropertyFields.Name, Nothing, ComparisonOperator.Equal, ResourceValidator.ReplaceIllegalCharacters(.Name).ToUpper())
                    fieldCompare.CaseSensitiveCollation() = True
                    filter.Add(fieldCompare)
                    results = existingCustomProperties.FindMatches(filter)
                    If results.Count = 1 Then
                        existingBankProperty = DirectCast(existingCustomProperties.Items(results(0)), CustomBankPropertyEntity)
                    End If

                    newCustomProperties.AddRange(ConvertToTestBuilderdefinition(sourceManResMan.ManifestMetaData.CustomPropertyDefinitions(i),
                             bankId, existingBankProperty, conflictingCustomProperties))
                End If
            End With
        Next

        result = HandleConflictingCustomProperties(conflictingCustomProperties)

        If result Then
            Dim updateResult = BankFactory.Instance.UpdateCustomProperties(newCustomProperties)

            If String.IsNullOrEmpty(updateResult) Then
                destinationDbResMan.BankId = bankId
            Else
                result = False
                RaiseEvent ImportHandlerHandleError(Me, New ImportExportHandlerHandleErrorEventArgs(updateResult))
            End If
        End If

        Return result
    End Function

    Private Shared Function HandleConflictingCustomProperties(conflictingCustomProperties As EntityCollection) As Boolean
        Dim result As Boolean

        If conflictingCustomProperties.Any() Then
            Dim msgText As New StringBuilder
            Dim firstProperty = True

            msgText.AppendLine()
            msgText.AppendLine()

            For Each cp As CustomBankPropertyEntity In conflictingCustomProperties
                If Not firstProperty Then
                    msgText.Append(", ")
                Else
                    firstProperty = False
                End If

                msgText.Append(cp.Name)
            Next

            msgText.AppendLine()
            msgText.AppendLine()

            Using handleConflictDialog As New HandleImportConflictDialog(String.Format(My.Resources.SkipConflictingBankPropertiesAndContinueWithImport, msgText.ToString()),
                              ImportFormWizard.ResourceConflictResolution.LeaveCurrentInstance Or ImportFormWizard.ResourceConflictResolution.ReplaceCurrentInstance,
                              My.Resources.HandleImportConflictCustomPropertyDialogWindowTitle, String.Empty)
                handleConflictDialog.ShowDialog()

                Return handleConflictDialog.ChoosenConflictResolution = ImportFormWizard.ResourceConflictResolution.ReplaceCurrentInstance
            End Using
        Else
            result = True
        End If

        Return result
    End Function

    Private Shared Function ConvertToTestBuilderdefinition(customPropertyDefinition As ResourceManifestMetadataDefinitionBase, bankId As Integer, existingBankProperty As CustomBankPropertyEntity, conflictingCustomProperties As EntityCollection) As EntityCollection
        Dim newCustomProperties = New EntityCollection()

        If (existingBankProperty IsNot Nothing AndAlso customPropertyDefinition.Name.Equals(existingBankProperty.Name)) OrElse existingBankProperty Is Nothing Then
            Select Case customPropertyDefinition.GetType().ToString()
                Case GetType(ResourceManifestMetadataMultiValueDefinition).ToString
                    ConvertResourceManifestMetadataMultiValueDefinition(customPropertyDefinition, existingBankProperty, bankId, newCustomProperties, conflictingCustomProperties)
                Case GetType(ResourceManifestMetaDataConceptStructureDefinition).ToString()
                    ConvertResourceManifestMetaDataConceptStructureDefinition(customPropertyDefinition, existingBankProperty, bankId, newCustomProperties, conflictingCustomProperties)
                Case GetType(ResourceManifestMetaDataTreeStructureDefinition).ToString()
                    ConvertResourceManifestMetaDataTreeStructureDefinition(customPropertyDefinition, existingBankProperty, bankId, newCustomProperties, conflictingCustomProperties)
                Case GetType(ResourceManifestMetadataSingleValueDefinition).ToString
                    ConvertResourceManifestMetadataSingleValueDefinition(customPropertyDefinition, existingBankProperty, bankId, newCustomProperties, conflictingCustomProperties)
            End Select
        Else
            conflictingCustomProperties.Add(existingBankProperty)
        End If

        Return newCustomProperties
    End Function

    Private Shared Sub ConvertResourceManifestMetadataMultiValueDefinition(customPropertyDefinition As ResourceManifestMetadataDefinitionBase, existingBankProperty As CustomBankPropertyEntity,
                                                                           bankId As Integer, newCustomProperties As EntityCollection, conflictingCustomProperties As EntityCollection)

        Dim mvMetaData As ResourceManifestMetadataMultiValueDefinition = DirectCast(customPropertyDefinition, ResourceManifestMetadataMultiValueDefinition)
        If existingBankProperty Is Nothing Then
            Dim newListPropertyDefinition As New ListCustomBankPropertyEntity
            With newListPropertyDefinition
                .BankId = bankId
                .CustomBankPropertyId = Guid.NewGuid
                .Name = ResourceValidator.ReplaceIllegalCharacters(mvMetaData.Name)
                .Title = mvMetaData.Title
                .MultipleSelect = mvMetaData.MultiSelect
                .ApplicableToMask = mvMetaData.ApplicableTo
                .Code = mvMetaData.Code
                .Publishable = mvMetaData.Publishable
                .Scorable = mvMetaData.Scorable
            End With

            For Each lv As ResourceManifestMetadataListValue In mvMetaData.ListValues
                Dim lvcbp As New ListValueCustomBankPropertyEntity
                With lvcbp
                    .Name = lv.Name
                    .Title = lv.Title
                    .Code = lv.Code
                End With
                newListPropertyDefinition.ListValueCustomBankPropertyCollection.Add(lvcbp)
            Next

            newCustomProperties.Add(newListPropertyDefinition)
        ElseIf Not TypeOf existingBankProperty Is ListCustomBankPropertyEntity Then
            conflictingCustomProperties.Add(existingBankProperty)
        Else
            Dim existingListDefinition As ListCustomBankPropertyEntity = DirectCast(existingBankProperty, ListCustomBankPropertyEntity)

            Dim newListValues = From lv In mvMetaData.ListValues Where existingListDefinition.ListValueCustomBankPropertyCollection.FirstOrDefault(Function(s) s.Name = lv.Name) Is Nothing Select lv

            For Each newlv In newListValues
                Dim nlv As ResourceManifestMetadataListValue = newlv
                Dim lvcbp As New ListValueCustomBankPropertyEntity
                With lvcbp
                    .Name = nlv.Name
                    .Title = nlv.Title
                    .Code = nlv.Code
                End With
                existingListDefinition.ListValueCustomBankPropertyCollection.Add(lvcbp)
            Next

            existingListDefinition.ApplicableToMask = CInt(existingListDefinition.ApplicableToMask) Or mvMetaData.ApplicableTo

            newCustomProperties.Add(existingListDefinition)
        End If
    End Sub

    Private Shared Sub ConvertResourceManifestMetaDataConceptStructureDefinition(customPropertyDefinition As ResourceManifestMetadataDefinitionBase, existingBankProperty As CustomBankPropertyEntity,
                                                                                 bankId As Integer, newCustomProperties As EntityCollection, conflictingCustomProperties As EntityCollection)

        Dim csMetaData As ResourceManifestMetaDataConceptStructureDefinition = DirectCast(customPropertyDefinition, ResourceManifestMetaDataConceptStructureDefinition)
        If existingBankProperty Is Nothing Then
            Dim newConceptStructureCustomBankPropertyEntity As New ConceptStructureCustomBankPropertyEntity With {
        .BankId = bankId,
        .CustomBankPropertyId = Guid.NewGuid,
        .Name = ResourceValidator.ReplaceIllegalCharacters(csMetaData.Name),
        .Title = csMetaData.Title,
        .ApplicableToMask = csMetaData.ApplicableTo,
        .Code = csMetaData.Code,
        .Publishable = csMetaData.Publishable,
        .Scorable = csMetaData.Scorable}

            For Each csPartMetaData As ResourceManifestMetaDataConceptStructurePartDefinition In csMetaData.ConceptStructurePartDefinitions
                Dim csPartEntity As New ConceptStructurePartCustomBankPropertyEntity With {
                        .ConceptStructurePartCustomBankPropertyId = Guid.NewGuid,
                        .Name = csPartMetaData.Name,
                        .Title = csPartMetaData.Title,
                        .Code = csPartMetaData.Code,
                        .ConceptTypeId = csPartMetaData.ConceptTypeId
                        }

                newConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.Add(csPartEntity)
            Next

            For Each csPartMetaData As ResourceManifestMetaDataConceptStructurePartDefinition In csMetaData.ConceptStructurePartDefinitions
                Dim csPartEntity As ConceptStructurePartCustomBankPropertyEntity = newConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.
                    GetFirstMatch(ConceptStructurePartCustomBankPropertyFields.Name = csPartMetaData.Name)

                For Each csRelatedPart As ResourceManifestMetaDataConceptStructurePartDefinition In csPartMetaData.ChildConceptStructureParts
                    Dim childCSPartEntity As ConceptStructurePartCustomBankPropertyEntity = newConceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection.
                        GetFirstMatch(ConceptStructurePartCustomBankPropertyFields.Name = csRelatedPart.Name)

                    Debug.Assert(childCSPartEntity IsNot Nothing, String.Format(CultureInfo.InvariantCulture, "No concept structure part named {0} found in the concept structure named {1}",
                                                                                csRelatedPart.Name, newConceptStructureCustomBankPropertyEntity.Name))

                    Dim csChildPartEntity As New ChildConceptStructurePartCustomBankPropertyEntity With {
                            .Id = Guid.NewGuid(),
                            .ConceptStructurePartCustomBankPropertyId = csPartEntity.ConceptStructurePartCustomBankPropertyId,
                            .ChildConceptStructurePartCustomBankProperty = childCSPartEntity,
                            .VisualOrder = csRelatedPart.VisualOrder
                            }

                    csPartEntity.ChildConceptStructurePartCustomBankPropertyCollection.Add(csChildPartEntity)
                Next
            Next

            newCustomProperties.Add(newConceptStructureCustomBankPropertyEntity)

        Else
            conflictingCustomProperties.Add(existingBankProperty)
        End If
    End Sub

    Private Shared Sub ConvertResourceManifestMetaDataTreeStructureDefinition(customPropertyDefinition As ResourceManifestMetadataDefinitionBase, existingBankProperty As CustomBankPropertyEntity,
                                                                              bankId As Integer, newCustomProperties As EntityCollection, conflictingCustomProperties As EntityCollection)

        Dim csMetaData = DirectCast(customPropertyDefinition, ResourceManifestMetaDataTreeStructureDefinition)
        If existingBankProperty Is Nothing Then
            Dim newTreeStructureCustomBankPropertyEntity As New TreeStructureCustomBankPropertyEntity With {
        .BankId = bankId,
        .CustomBankPropertyId = Guid.NewGuid,
        .Name = ResourceValidator.ReplaceIllegalCharacters(csMetaData.Name),
        .Title = csMetaData.Title,
        .ApplicableToMask = csMetaData.ApplicableTo,
        .Code = csMetaData.Code,
        .Publishable = csMetaData.Publishable,
        .Scorable = csMetaData.Scorable}

            For Each csPartMetaData As ResourceManifestMetaDataTreeStructurePartDefinition In csMetaData.TreeStructurePartDefinitions
                Dim csPartEntity As New TreeStructurePartCustomBankPropertyEntity With {
                        .TreeStructurePartCustomBankPropertyId = Guid.NewGuid,
                        .Name = csPartMetaData.Name,
                        .Title = csPartMetaData.Title,
                        .Code = csPartMetaData.Code
                        }

                newTreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.Add(csPartEntity)
            Next

            For Each csPartMetaData As ResourceManifestMetaDataTreeStructurePartDefinition In csMetaData.TreeStructurePartDefinitions
                Dim csPartEntity As TreeStructurePartCustomBankPropertyEntity = newTreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.GetFirstMatch(TreeStructurePartCustomBankPropertyFields.Name = csPartMetaData.Name)

                For Each csRelatedPart As ResourceManifestMetaDataTreeStructurePartDefinition In csPartMetaData.ChildTreeStructureParts
                    Dim childCSPartEntity As TreeStructurePartCustomBankPropertyEntity = newTreeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection.GetFirstMatch(TreeStructurePartCustomBankPropertyFields.Name = csRelatedPart.Name)

                    Debug.Assert(childCSPartEntity IsNot Nothing, String.Format(CultureInfo.InvariantCulture, "No tree structure part named {0} found in the tree structure named {1}", csRelatedPart.Name,
                                                                                newTreeStructureCustomBankPropertyEntity.Name))

                    Dim csChildPartEntity As New ChildTreeStructurePartCustomBankPropertyEntity With {
                            .Id = Guid.NewGuid(),
                            .TreeStructurePartCustomBankPropertyId = csPartEntity.TreeStructurePartCustomBankPropertyId,
                            .ChildTreeStructurePartCustomBankPropertyId = childCSPartEntity.TreeStructurePartCustomBankPropertyId,
                            .VisualOrder = csRelatedPart.VisualOrder
                            }

                    csPartEntity.ChildTreeStructurePartCustomBankPropertyCollection.Add(csChildPartEntity)
                Next
            Next

            newCustomProperties.Add(newTreeStructureCustomBankPropertyEntity)

        ElseIf Not TypeOf existingBankProperty Is TreeStructureCustomBankPropertyEntity Then
            conflictingCustomProperties.Add(existingBankProperty)
        Else
            conflictingCustomProperties.Add(existingBankProperty)
            newCustomProperties.Add(DirectCast(existingBankProperty, TreeStructureCustomBankPropertyEntity))
        End If
    End Sub

    Private Shared Sub ConvertResourceManifestMetadataSingleValueDefinition(customPropertyDefinition As ResourceManifestMetadataDefinitionBase, existingBankProperty As CustomBankPropertyEntity,
                                                                            bankId As Integer, newCustomProperties As EntityCollection, conflictingCustomProperties As EntityCollection)

        Dim svMetaData = DirectCast(customPropertyDefinition, ResourceManifestMetadataSingleValueDefinition)

        If existingBankProperty Is Nothing Then
            Dim newValuePropertyDefinition As CustomBankPropertyEntity
            If Not svMetaData.RichText Then
                newValuePropertyDefinition = New FreeValueCustomBankPropertyEntity
            Else
                newValuePropertyDefinition = New RichTextValueCustomBankPropertyEntity
            End If

            With newValuePropertyDefinition
                .BankId = bankId
                .CustomBankPropertyId = Guid.NewGuid
                .Name = ResourceValidator.ReplaceIllegalCharacters(svMetaData.Name)
                .Title = svMetaData.Title
                .ApplicableToMask = svMetaData.ApplicableTo
                .Publishable = svMetaData.Publishable
                .Scorable = svMetaData.Scorable
            End With

            newCustomProperties.Add(newValuePropertyDefinition)

        ElseIf Not TypeOf existingBankProperty Is FreeValueCustomBankPropertyEntity AndAlso Not TypeOf existingBankProperty Is RichTextValueCustomBankPropertyEntity Then
            conflictingCustomProperties.Add(existingBankProperty)
        Else
            Dim existingSinglValue As CustomBankPropertyEntity
            If Not svMetaData.RichText Then
                existingSinglValue = DirectCast(existingBankProperty, FreeValueCustomBankPropertyEntity)
            Else
                existingSinglValue = DirectCast(existingBankProperty, RichTextValueCustomBankPropertyEntity)
            End If
            existingSinglValue.ApplicableToMask = CInt(existingSinglValue.ApplicableToMask) Or svMetaData.ApplicableTo
            newCustomProperties.Add(existingSinglValue)
        End If
    End Sub


    Public Event Progress(ByVal sender As Object, ByVal e As ProgressEventArgs) Implements IImportHandler.Progress
    Public Event StartProgress(ByVal sender As Object, ByVal e As StartEventArgs) Implements IImportHandler.StartProgress
    Public Event ImportHandlerHandleConflict(ByVal sender As Object, ByVal e As ImportHandlerHandleConflictEventArgs) Implements IImportHandler.ImportHandlerHandleConflict
    Public Event ImportHandlerHandleError(ByVal sender As Object, ByVal e As ImportExportHandlerHandleErrorEventArgs) Implements IImportHandler.ImportHandlerHandleError
    Public Event ImportHandlerHandleWarning(ByVal sender As Object, ByVal e As ImportExportHandlerHandleWarningEventArgs) Implements IImportHandler.ImportHandlerHandleWarning
    Public Event HarmonizeCompleted(result As BuilderTaskResult) Implements IImportHandlerQuestifyExportFiles.HarmonizeCompleted

    Public Event ImportHandlerCustomBankPropertiesRemoved(ByVal sender As Object, ByVal e As ImportCustomBankPropertiesRemovedArgs) Implements IImportHandler.ImportHandlerCustomBankPropertiesRemoved

    Private Sub ImportManager_HandleConflict(ByVal sender As Object, ByVal e As GenericConflictEventArgs) Handles _importMan.HandleConflict

        Dim bankName = String.Empty
        If sender.GetType() Is GetType(ImportManager) Then
            Dim importManager = DirectCast(sender, ImportManager)
            If importManager.DestinationResourceManager.GetType() Is GetType(DataBaseResourceManager) Then
                Dim destinationResourceManager = DirectCast(importManager.DestinationResourceManager, DataBaseResourceManager)
                bankName = ConstructBankPath(destinationResourceManager.BankId)
            End If
        End If

        Dim importHandlerEventargs As New ImportHandlerHandleConflictEventArgs(e.ResourceName, e.BankId, e.BankContextId, bankName)
        RaiseEvent ImportHandlerHandleConflict(Me, importHandlerEventargs)
        e.Cancel = importHandlerEventargs.Cancel
    End Sub

    Private Sub ImportManager_CustomBankPropertiesRemoved(ByVal sender As Object, ByVal e As ImportCustomBankPropertiesRemovedArgs) Handles _importMan.CustomBankPropertiesRemoved
        RaiseEvent ImportHandlerCustomBankPropertiesRemoved(sender, e)
    End Sub

    Private Function ConstructBankPath(bankId As Integer?) As String
        Dim bankNameBuilder = New StringBuilder(GetBankName(bankId.Value))
        While GetParentBank(bankId.Value).HasValue
            bankId = GetParentBank(bankId.Value)
            If bankId.HasValue Then
                bankNameBuilder.Insert(0, String.Format(CultureInfo.InvariantCulture, "{0}\", GetBankName(bankId.Value)))
            End If
        End While
        Return bankNameBuilder.ToString()
    End Function

    Private Sub ImportManager_ImportProgress(ByVal sender As Object, ByVal e As ProgressEventArgs) Handles _importMan.ImportProgress
        RaiseEvent Progress(Me, e)
    End Sub

    Private Sub ImportManager_StartPublication(ByVal sender As Object, ByVal e As StartEventArgs) Handles _importMan.StartImport
        RaiseEvent StartProgress(Me, e)
    End Sub



    Private _disposedValue As Boolean = False

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                If _importMan IsNot Nothing Then
                    _importMan.Dispose()
                    _importMan = Nothing
                End If

            End If

        End If
        _disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub



End Class
