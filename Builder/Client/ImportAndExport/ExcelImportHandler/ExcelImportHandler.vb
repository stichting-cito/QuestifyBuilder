
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ContentModel
Imports System.Text
Imports Enums
Imports Janus.Windows.GridEX
Imports Questify.Builder.Logic.Conversion
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Packaging
Imports Questify.Builder.UI
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class ExcelImportHandler
    Implements IDisposable
    Implements IImportHandler


    Private _optionsControl As ImportOptionControlBase
    Private ReadOnly _results As New List(Of String)
    Private ReadOnly _cachedParents As New Dictionary(Of TreeStructurePartCustomBankPropertyEntity, List(Of TreeStructurePartCustomBankPropertyEntity))
    Private _hasError As Boolean


    Public ReadOnly Property HasError As Boolean
        Get
            Return _hasError
        End Get
    End Property

    Public ReadOnly Property SupportedResourceTypes() As String Implements IImportHandler.SupportedResourceTypes
        Get
            Return "items|media|tests"
        End Get
    End Property

    Public ReadOnly Property ImportResults() As String
        Get
            Dim sb As New StringBuilder(String.Empty)
            For Each result As String In _results
                sb.AppendLine(result)
            Next
            Return sb.ToString
        End Get
    End Property

    Public Function Import(ByVal packageSet As PackageSet, ByVal parentBank As Integer?) As Boolean Implements IImportHandler.Import
        Throw New NotImplementedException("Function is used for import package files")
    End Function

    Public Function Import(ByVal sourceResourceManager As ResourceManagerBase, ByVal bank As Integer) As Boolean Implements IImportHandler.Import
        Throw New NotImplementedException("Function is used for import package files")
    End Function

    Public Function Import(ByVal parentBankId As Integer?) As Boolean Implements IImportHandler.Import
        _hasError = False
        Try
            Dim excelReader As New OpenXmlExcelReader
            Dim listOfKnownColumns As New List(Of String)
            Dim resourceCodeColumnName As String = "code"

            listOfKnownColumns.Add("itm-nr")
            listOfKnownColumns.Add("code")
            listOfKnownColumns.Add("mk-sleutel")
            listOfKnownColumns.Add("itm-naam")
            listOfKnownColumns.Add("score-max")
            listOfKnownColumns.Add("mk-alt")
            listOfKnownColumns.Add("type")

            AddColumnsFromGrid(New ItemGrid, listOfKnownColumns)
            AddColumnsFromGrid(New MediaGrid, listOfKnownColumns)
            AddColumnsFromGrid(New TestGrid, listOfKnownColumns)

            Dim excelImportErrors As List(Of String) = excelReader.ReadExcelDocument(ExcelImportOptions.Url, listOfKnownColumns, resourceCodeColumnName)
            If excelImportErrors.Count = 0 Then
                Dim customBankPropertyDefinition As List(Of String)
                Dim customBankPropertyValues As Dictionary(Of String, Dictionary(Of String, String))
                customBankPropertyDefinition = excelReader.CustomPropertyDefinitions
                customBankPropertyValues = excelReader.CustomPropertyValues

                Dim collection = GetCollection(customBankPropertyValues, parentBankId)

                Dim numberOfItemsInBankThatWillBeUpdated As Integer = 0
                If collection IsNot Nothing Then
                    numberOfItemsInBankThatWillBeUpdated = collection.Count
                End If
                Dim eStartImportEventArgs As New StartEventArgs(customBankPropertyDefinition.Count + numberOfItemsInBankThatWillBeUpdated)
                RaiseEvent StartProgress(Me, eStartImportEventArgs)
                _results.Add(String.Format(My.Resources.ItemsInExcel, customBankPropertyValues.Count.ToString))
                AddCustomBankProperties(customBankPropertyDefinition, parentBankId)
                AddCustomPropertiesToResources(customBankPropertyValues, parentBankId, collection)
                collection.Dispose()
                Return True
            Else
                Dim errorMessageBuilder = New StringBuilder()
                For Each errormessage As String In excelImportErrors
                    errorMessageBuilder.AppendLine(errormessage)
                Next
                errorMessageBuilder.AppendLine(My.Resources.ReadExcelAdditionalExceptionMessage)
                Throw New Exception(errorMessageBuilder.ToString())
            End If
        Catch ex As Exception
            _hasError = True
            _results.Add(My.Resources.AnErrorOccurred)
            _results.Add(ex.Message)
        End Try
        Return Not _hasError
    End Function

    Private Sub AddColumnsFromGrid(tempGrid As GridBase, listOfKnownColumns As List(Of String))
        Using tempGrid
            If tempGrid.GridControl IsNot Nothing AndAlso tempGrid.GridControl.Tables.Count > 0 Then
                For Each column As GridEXColumn In tempGrid.GridControl.Tables(0).Columns
                    If Not String.IsNullOrEmpty(column.Caption) AndAlso
                       Not listOfKnownColumns.Contains(column.Caption.ToLower) Then
                        listOfKnownColumns.Add(column.Caption.ToLower)
                    End If
                Next
            End If
        End Using
    End Sub

    Private Function GetCollection(ByVal customBankPropertyValues As Dictionary(Of String, Dictionary(Of String, String)), ByVal parentBankId As Integer?) As EntityCollection
        Dim newResourceList As New List(Of String)
        For Each code As String In customBankPropertyValues.Keys
            newResourceList.Add(code)
        Next
        Dim coll = New EntityCollection()
        If newResourceList.Any() AndAlso parentBankId.HasValue Then
            coll = ResourceFactory.Instance.GetResourcesByNamesWithOption(parentBankId.Value, newResourceList, New ResourceRequestDTO())
        End If
        Return coll
    End Function

    Private Function DetermineResourceTypeFactory(resourceType As String) As IEntityFactory2
        Select Case resourceType.ToLower()
            Case "item"
                Return New ItemResourceEntityFactory()
            Case "media"
                Return New GenericResourceEntityFactory()
            Case My.Resources.Test
                Return New AssessmentTestResourceEntityFactory()
            Case Else
                Return New ResourceEntityFactory()
        End Select
    End Function

    Private Sub AddCustomPropertiesToResources(ByVal customBankPropertyValues As Dictionary(Of String, Dictionary(Of String, String)),
                                           ByVal parentBankId As Integer?,
                                           ByVal collection As EntityCollection)
        Dim added = 0

        If Not parentBankId.HasValue Then
            _results.Add(String.Format(My.Resources.ItemsUpdatedWithCustomProperties, added.ToString))
            Return
        End If

        Dim existingCustomProperties As EntityCollection = BankFactory.Instance.GetCustomBankPropertiesForBranchById(parentBankId.Value, ResourceTypeEnum.AllResources)
        For Each resource As ResourceEntity In collection
            If Not ResourceFactory.Instance.ResourceExists(parentBankId.Value, resource.ResourceId, False, DetermineResourceTypeFactory(resource.ResourceType)) Then
                Continue For
            End If

            Dim e As New ProgressEventArgs(String.Format(My.Resources.AddingCustomPropertiesToItem, resource.Name))
            RaiseEvent Progress(Me, e)

            Dim customPropertyDictionary = customBankPropertyValues(resource.Name.ToString())
            Select Case resource.ResourceType.ToLower()
                Case "item"
                    Dim itemResource = DirectCast(resource, ItemResourceEntity)
                    Dim resourceFull = ResourceFactory.Instance.GetItem(itemResource, New ResourceRequestDTO() With {.WithCustomProperties = True})
                    If AddPropertiesToResource(customPropertyDictionary, resourceFull, existingCustomProperties) Then
                        added = added + 1
                        ResourceFactory.Instance.UpdateItemResource(resourceFull)
                    End If
                Case "media"
                    Dim genericResource = DirectCast(resource, GenericResourceEntity)
                    Dim resourceFull = ResourceFactory.Instance.GetGenericResource(genericResource)
                    If AddPropertiesToResource(customPropertyDictionary, resourceFull, existingCustomProperties) Then
                        added = added + 1
                        ResourceFactory.Instance.UpdateGenericResource(resourceFull)
                    End If
                Case My.Resources.Test
                    Dim assessmentTestResource = DirectCast(resource, AssessmentTestResourceEntity)
                    Dim resourceFull = ResourceFactory.Instance.GetAssessmentTest(assessmentTestResource)
                    If AddPropertiesToResource(customPropertyDictionary, resourceFull, existingCustomProperties) Then
                        added = added + 1
                        ResourceFactory.Instance.UpdateAssessmentTestResource(resourceFull)
                    End If
                Case Else
                    Throw New NotSupportedException("ResourceType is not supported.")
            End Select
        Next

        _results.Add(String.Format(My.Resources.ItemsUpdatedWithCustomProperties, added.ToString))
    End Sub

    Protected Friend Function AddPropertiesToResource(customPropertyDictionary As Dictionary(Of String, String), resource As ResourceEntity, existingCustomProperties As EntityCollection) As Boolean
        Dim isChanged As Boolean = False
        For Each customProperty As String In customPropertyDictionary.Keys
            If String.IsNullOrEmpty(customPropertyDictionary(customProperty)) Then
                Continue For
            End If

            Dim existingBankProperty As CustomBankPropertyEntity = GetExistingBankProperty(existingCustomProperties, customProperty)
            If existingBankProperty Is Nothing Then
                Continue For
            End If

            Dim customPropteryValueFilter As IPredicate = (CustomBankPropertyValueFields.CustomBankPropertyId = existingBankProperty.CustomBankPropertyId)
            customPropteryValueFilter = (CustomBankPropertyValueFields.CustomBankPropertyId = existingBankProperty.CustomBankPropertyId)
            Dim matchedPropertyList = resource.CustomBankPropertyValueCollection.FindMatches(customPropteryValueFilter)

            Select Case existingBankProperty.GetType.ToString
                Case GetType(FreeValueCustomBankPropertyEntity).ToString
                    Dim fvProperty As FreeValueCustomBankPropertyValueEntity
                    If matchedPropertyList.Count = 0 Then
                        fvProperty = New FreeValueCustomBankPropertyValueEntity(resource.ResourceId, existingBankProperty.CustomBankPropertyId)
                        fvProperty.CustomBankProperty = existingBankProperty
                        resource.CustomBankPropertyValueCollection.Add(fvProperty)
                    Else
                        fvProperty = CType(resource.CustomBankPropertyValueCollection(matchedPropertyList(0)), FreeValueCustomBankPropertyValueEntity)
                    End If
                    fvProperty.Value = customPropertyDictionary(customProperty)
                    fvProperty.SetCustomPropertyDisplayValue(existingBankProperty)
                    isChanged = True
                Case GetType(ListCustomBankPropertyEntity).ToString
                    Dim lvProperty As ListCustomBankPropertyValueEntity
                    Dim isNew As Boolean = matchedPropertyList.Count = 0
                    If isNew Then
                        lvProperty = New ListCustomBankPropertyValueEntity(resource.ResourceId, existingBankProperty.CustomBankPropertyId)
                        lvProperty.CustomBankProperty = existingBankProperty
                    Else
                        lvProperty = CType(resource.CustomBankPropertyValueCollection(matchedPropertyList(0)), ListCustomBankPropertyValueEntity)
                    End If
                    Dim lcp = DirectCast(existingBankProperty, ListCustomBankPropertyEntity)
                    isChanged = AddListValueByValueOrTitle(resource, lcp, lvProperty, customPropertyDictionary(customProperty), isNew)
                    lvProperty.SetCustomPropertyDisplayValue(existingBankProperty)
                Case GetType(ConceptStructureCustomBankPropertyEntity).ToString
                    Dim csProperty As ConceptStructureCustomBankPropertyValueEntity
                    Dim isNew As Boolean = matchedPropertyList.Count = 0
                    If isNew Then
                        csProperty = New ConceptStructureCustomBankPropertyValueEntity(resource.ResourceId, existingBankProperty.CustomBankPropertyId)
                        csProperty.CustomBankProperty = existingBankProperty
                    Else
                        csProperty = CType(resource.CustomBankPropertyValueCollection(matchedPropertyList(0)), ConceptStructureCustomBankPropertyValueEntity)
                    End If
                    Dim ccp = DirectCast(existingBankProperty, ConceptStructureCustomBankPropertyEntity)
                    isChanged = AddConceptValueByValueOrTitle(resource, ccp, csProperty, customPropertyDictionary(customProperty), isNew)
                    csProperty.SetCustomPropertyDisplayValue(existingBankProperty)
                Case GetType(TreeStructureCustomBankPropertyEntity).ToString
                    Dim tsProperty As TreeStructureCustomBankPropertyValueEntity = Nothing
                    Dim isNew As Boolean = matchedPropertyList.Count = 0
                    If Not isNew Then
                        tsProperty = CType(resource.CustomBankPropertyValueCollection(matchedPropertyList(0)), TreeStructureCustomBankPropertyValueEntity)
                    End If
                    Dim tcp = DirectCast(existingBankProperty, TreeStructureCustomBankPropertyEntity)
                    isChanged = AddTreeStructureValueByValueOrTitle(resource, tcp, tsProperty, customPropertyDictionary(customProperty), isNew)
                    If tsProperty IsNot Nothing Then
                        tsProperty.SetCustomPropertyDisplayValue(existingBankProperty)
                    End If
                Case Else
                    Throw New NotSupportedException("The type of bank property is not supported.")
            End Select
        Next

        If resource.CustomBankPropertyValueCollection.OfType(Of TreeStructureCustomBankPropertyValueEntity).Count > 1 Then
            isChanged = True
            Dim toRemove = resource.CustomBankPropertyValueCollection.OfType(Of TreeStructureCustomBankPropertyValueEntity).ToList()
            RemoveCustomProperty(Of TreeStructureCustomBankPropertyValueEntity)(resource, Nothing, toRemove)
            RaiseEvent ImportHandlerCustomBankPropertiesRemoved(Me, New ImportCustomBankPropertiesRemovedArgs(resource.Name, toRemove.Select(Function(tr) tr.CustomBankProperty.Name).ToArray()))
        End If

        Return isChanged
    End Function

    Private Function AddListValueByValueOrTitle(resource As ResourceEntity,
                                            lcp As ListCustomBankPropertyEntity,
                                            listValue As ListCustomBankPropertyValueEntity,
                                            values As String,
                                            isNew As Boolean) As Boolean
        Dim matchFound = False
        Dim list As New List(Of String)

        If lcp.MultipleSelect Then
            list = values.Split(";"c).ToList
        Else
            list = New String() {values}.ToList
        End If

        list.ForEach(Sub(customPropertyValue)
                         If lcp.ListValueCustomBankPropertyCollection Is Nothing Then
                             Return
                         End If

                         Dim codefromExcel = customPropertyValue.Split("-"c).FirstOrDefault.Trim
                         Dim matchedValue = lcp.ListValueCustomBankPropertyCollection.Where(Function(v) v.Name = customPropertyValue OrElse
                                                                                                v.Title = customPropertyValue OrElse
                                                                                                v.Name.Trim = codefromExcel).FirstOrDefault
                         If matchedValue Is Nothing Then
                             Return
                         End If

                         If Not matchFound Then
                             Dim lsvCount = listValue.ListCustomBankPropertySelectedValueCollection.Count
                             For i = 0 To lsvCount - 1
                                 Dim lvToRemove = listValue.ListCustomBankPropertySelectedValueCollection.Item(0)
                                 listValue.ListCustomBankPropertySelectedValueCollection.Remove(lvToRemove)
                                 If resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                                     resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
                                 End If
                                 resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(lvToRemove)
                             Next
                         End If
                         matchFound = True
                         Dim selectedValue = New ListCustomBankPropertySelectedValueEntity
                         selectedValue.CustomBankPropertyId = lcp.CustomBankPropertyId
                         selectedValue.ResourceId = resource.ResourceId
                         selectedValue.ListValueBankCustomPropertyId = matchedValue.ListValueBankCustomPropertyId

                         If listValue.ListCustomBankPropertySelectedValueCollection.Where(Function(c) c.ListValueBankCustomPropertyId = matchedValue.ListValueBankCustomPropertyId).Count = 0 Then
                             listValue.ListCustomBankPropertySelectedValueCollection.Add(selectedValue)
                         End If
                     End Sub)

        If matchFound AndAlso isNew Then
            resource.CustomBankPropertyValueCollection.Add(listValue)
        End If
        Return matchFound
    End Function

    Private Function AddConceptValueByValueOrTitle(resource As ResourceEntity,
                                                   ccp As ConceptStructureCustomBankPropertyEntity,
                                                   conceptValue As ConceptStructureCustomBankPropertyValueEntity,
                                                   value As String, isNew As Boolean) As Boolean
        Dim matchFound = False

        If Not resource.ResourceType.Equals("item", StringComparison.InvariantCultureIgnoreCase) Then
            Return matchFound
        End If

        Dim itemResource = DirectCast(resource, ItemResourceEntity)
        Dim assessmentItem = itemResource.GetAssessmentItem
        If assessmentItem IsNot Nothing AndAlso assessmentItem.Solution IsNot Nothing AndAlso assessmentItem.Solution.ConceptFindings.Count = 0 Then
            If ccp.ConceptStructurePartCustomBankPropertyCollection IsNot Nothing Then
                Dim codeFromExcel = value.Split("-"c).FirstOrDefault.Trim()
                If value.Equals("null", StringComparison.OrdinalIgnoreCase) Then
                    RemoveCustomProperty(itemResource, New List(Of CustomBankPropertyValueEntity) From {conceptValue})
                    matchFound = True
                Else
                    Dim matchedValue = ccp.ConceptStructurePartCustomBankPropertyCollection.Where(Function(v) (v.Name = value OrElse v.Title = value OrElse v.Name.Trim = codeFromExcel) AndAlso
                                                                                                      (v.ConceptType.ApplicableToMask And ResourceTypeEnum.ItemResource) = ResourceTypeEnum.ItemResource).FirstOrDefault
                    If matchedValue IsNot Nothing Then
                        Dim csCount = conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Count
                        If Not matchFound Then
                            For i = 0 To csCount - 1
                                Dim lvToRemove = conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Item(0)
                                conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Remove(lvToRemove)
                                If itemResource.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                                    itemResource.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
                                End If
                                itemResource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(lvToRemove)
                            Next
                        End If
                        matchFound = True
                        Dim selectedValue = New ConceptStructureCustomBankPropertySelectedPartEntity
                        selectedValue.CustomBankPropertyId = ccp.CustomBankPropertyId
                        selectedValue.ResourceId = itemResource.ResourceId
                        selectedValue.ConceptStructurePartId = matchedValue.ConceptStructurePartCustomBankPropertyId
                        conceptValue.ConceptStructureCustomBankPropertySelectedPartCollection.Add(selectedValue)
                    End If
                End If
            End If
            If matchFound AndAlso isNew Then
                RemoveCustomProperty(Of ConceptStructureCustomBankPropertyValueEntity)(itemResource)
                itemResource.CustomBankPropertyValueCollection.Add(conceptValue)
            End If
        End If

        Return matchFound
    End Function

    Private Function AddTreeStructureValueByValueOrTitle(resource As ResourceEntity,
                                                     tcp As TreeStructureCustomBankPropertyEntity,
                                                     treeStructureValue As TreeStructureCustomBankPropertyValueEntity,
                                                     value As String,
                                                     isNew As Boolean) As Boolean
        Dim tsValue = treeStructureValue
        Dim matchFound = False
        Dim isRemoved As Boolean = False
        Dim list = value.Split(";"c).ToList
        If value.Equals("null", StringComparison.OrdinalIgnoreCase) Then
            RemoveCustomProperty(Of TreeStructureCustomBankPropertyValueEntity)(resource, Nothing, New List(Of TreeStructureCustomBankPropertyValueEntity) From {tsValue})
            isRemoved = True
        Else
            If isNew AndAlso tsValue Is Nothing Then
                tsValue = New TreeStructureCustomBankPropertyValueEntity(resource.ResourceId, tcp.CustomBankPropertyId)
                tsValue.CustomBankProperty = tcp
            End If

            list.ForEach(Sub(customPropertyValue)
                             Dim codeFromExcel = customPropertyValue.Split("-"c).FirstOrDefault.Trim()
                             If tcp.TreeStructurePartCustomBankPropertyCollection Is Nothing Then
                                 Return
                             End If
                             Dim matchedValue = tcp.TreeStructurePartCustomBankPropertyCollection.Where(Function(v) (v.Name = customPropertyValue OrElse v.Title = customPropertyValue OrElse v.Name.Trim = codeFromExcel)).FirstOrDefault
                             If matchedValue Is Nothing Then
                                 Return
                             End If

                             If Not matchFound Then
                                 Dim tsCount = tsValue.TreeStructureCustomBankPropertySelectedPartCollection.Count
                                 For i = 0 To tsCount - 1
                                     Dim lvToRemove = tsValue.TreeStructureCustomBankPropertySelectedPartCollection.Item(0)
                                     tsValue.TreeStructureCustomBankPropertySelectedPartCollection.Remove(lvToRemove)
                                     If resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                                         resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
                                     End If
                                     resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(lvToRemove)
                                 Next
                             End If
                             matchFound = True
                             If tsValue.TreeStructureCustomBankPropertySelectedPartCollection.Where(Function(tv) tv.TreeStructurePartId = matchedValue.TreeStructurePartCustomBankPropertyId).Count <> 0 Then
                                 Return
                             End If

                             Dim selectedValue = New TreeStructureCustomBankPropertySelectedPartEntity
                             selectedValue.CustomBankPropertyId = tcp.CustomBankPropertyId
                             selectedValue.ResourceId = resource.ResourceId
                             selectedValue.TreeStructurePartId = matchedValue.TreeStructurePartCustomBankPropertyId
                             tsValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(selectedValue)
                             If Not _cachedParents.ContainsKey(matchedValue) Then
                                 _cachedParents.Add(matchedValue, matchedValue.GetParents(tcp))
                             End If
                             For Each parent In _cachedParents(matchedValue)
                                 If tsValue.TreeStructureCustomBankPropertySelectedPartCollection.Where(Function(tv) tv.TreeStructurePartId = parent.TreeStructurePartCustomBankPropertyId).Count <> 0 Then
                                     Return
                                 End If

                                 Dim selectedParentValue = New TreeStructureCustomBankPropertySelectedPartEntity
                                 selectedParentValue.CustomBankPropertyId = tcp.CustomBankPropertyId
                                 selectedParentValue.ResourceId = resource.ResourceId
                                 selectedParentValue.TreeStructurePartId = parent.TreeStructurePartCustomBankPropertyId
                                 tsValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(selectedParentValue)
                             Next
                         End Sub)
        End If

        If matchFound AndAlso isNew Then
            tsValue.SetCustomPropertyDisplayValue(tcp)
            resource.CustomBankPropertyValueCollection.Add(tsValue)
        End If
        Return matchFound OrElse isRemoved
    End Function

    Private Sub RemoveCustomProperty(Of TCustomPropertyType As CustomBankPropertyValueEntity)(resource As ResourceEntity)
        RemoveCustomProperty(Of TCustomPropertyType)(resource, Nothing)
    End Sub

    Private Sub RemoveCustomProperty(Of TCustomPropertyType As CustomBankPropertyValueEntity)(resource As ResourceEntity, exclusionList As IList(Of TCustomPropertyType))
        RemoveCustomProperty(Of TCustomPropertyType)(resource, exclusionList, Nothing)
    End Sub

    Private Sub RemoveCustomProperty(Of TCustomPropertyType As CustomBankPropertyValueEntity)(resource As ResourceEntity,
                                                                                          exclusionList As IList(Of TCustomPropertyType),
                                                                                          propertiesToRemove As IList(Of TCustomPropertyType))
        Dim currentTrees = resource.CustomBankPropertyValueCollection.OfType(Of TCustomPropertyType)()
        If exclusionList IsNot Nothing Then
            currentTrees = currentTrees.Where(Function(c) Not exclusionList.Contains(c))
        End If
        If propertiesToRemove IsNot Nothing Then
            currentTrees = currentTrees.Where(Function(c) propertiesToRemove.Contains(c))
        End If
        Dim currentTreesHashSet = New HashSet(Of TCustomPropertyType)(currentTrees)
        currentTreesHashSet.ToList.ForEach(Sub(cp)
                                               resource.CustomBankPropertyValueCollection.Remove(cp)
                                               If resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker Is Nothing Then
                                                   resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker = New EntityCollection
                                               End If
                                               resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Add(cp)
                                           End Sub)
    End Sub

    Private Sub RemoveCustomProperty(itemResource As ItemResourceEntity, propertiesToRemove As IList(Of CustomBankPropertyValueEntity))
        RemoveCustomProperty(itemResource, Nothing, propertiesToRemove)
    End Sub


    Protected Sub AddCustomBankProperties(ByVal customPropertyDefintion As List(Of String), ByVal parentBankId As Integer?)
        If parentBankId.HasValue Then

            Dim numberOfAddedCustomBankProperties As Integer = 0
            Dim numberOfExistingCustomBankProperties As Integer = 0
            Dim numberOfConflicts As Integer = 0

            Dim existingCustomProperties As EntityCollection = BankFactory.Instance.GetCustomBankPropertiesForBranchById(parentBankId.Value, ResourceTypeEnum.AllResources)
            Dim newCustomProperties As New EntityCollection
            Dim existingBankPropery As CustomBankPropertyEntity = Nothing

            For Each customProperty As String In customPropertyDefintion
                existingBankPropery = GetExistingBankProperty(existingCustomProperties, customProperty)
                Dim e As New ProgressEventArgs(String.Format(My.Resources.AddingCustomProperties, customProperty))
                RaiseEvent Progress(Me, e)

                If existingBankPropery Is Nothing Then
                    Dim newFreeValuePropertyDefinition As New FreeValueCustomBankPropertyEntity
                    With newFreeValuePropertyDefinition
                        .BankId = parentBankId.Value
                        .CustomBankPropertyId = Guid.NewGuid
                        .Name = customProperty
                        .Title = customProperty
                        .ApplicableToMask = 35
                        .Publishable = True
                    End With
                    newCustomProperties.Add(newFreeValuePropertyDefinition)
                    numberOfAddedCustomBankProperties += 1
                Else
                    numberOfExistingCustomBankProperties += 1
                End If
            Next
            If numberOfAddedCustomBankProperties <> 0 Then
                BankFactory.Instance.UpdateCustomProperties(newCustomProperties)
            End If
            _results.Add(String.Format(My.Resources.CustomBankPropertiesAdded, numberOfAddedCustomBankProperties.ToString))
            _results.Add(String.Format(My.Resources.CustomBankPropertiesAlreadyExisted, numberOfExistingCustomBankProperties.ToString))
            If numberOfConflicts <> 0 Then
                _results.Add(String.Format(My.Resources.CustomBankPropertiesConflicts, numberOfConflicts.ToString))
            End If

        End If
    End Sub

    Private Shared Function GetExistingBankProperty(ByVal existingCustomProperties As EntityCollection, ByVal customProperty As String) As CustomBankPropertyEntity
        Dim returnValue As CustomBankPropertyEntity = Nothing
        Dim results As List(Of Integer)

        Dim filter As New FieldCompareValuePredicate(CustomBankPropertyFields.Name, Nothing, ComparisonOperator.Equal, customProperty.Trim("["c).Trim("]"c))
        filter.CaseSensitiveCollation = True

        results = existingCustomProperties.FindMatches(filter)
        If results.Count >= 1 Then
            returnValue = DirectCast(existingCustomProperties.Items(results(0)), CustomBankPropertyEntity)
        End If
        Return returnValue
    End Function

    Public Event ImportHandlerHandleConflict(ByVal sender As Object, ByVal e As ImportHandlerHandleConflictEventArgs) Implements IImportHandler.ImportHandlerHandleConflict
    Public Event ImportHandlerHandleError(ByVal sender As Object, ByVal e As ImportExportHandlerHandleErrorEventArgs) Implements IImportHandler.ImportHandlerHandleError
    Public Event ImportHandlerHandleWarning(ByVal sender As Object, ByVal e As ImportExportHandlerHandleWarningEventArgs) Implements IImportHandler.ImportHandlerHandleWarning
    Public Event Progress(ByVal sender As Object, ByVal e As Cito.Tester.Common.ProgressEventArgs) Implements IImportHandler.Progress
    Public Event StartProgress(ByVal sender As Object, ByVal e As StartEventArgs) Implements IImportHandler.StartProgress
    Public Event ImportHandlerCustomBankPropertiesRemoved(ByVal sender As Object, ByVal e As ImportCustomBankPropertiesRemovedArgs) Implements IImportHandler.ImportHandlerCustomBankPropertiesRemoved

    Public ReadOnly Property GetOptionsUserControl() As ImportOptionControlBase Implements IImportHandler.GetOptionsUserControl
        Get
            If _optionsControl Is Nothing Then
                _optionsControl = New ExcelImportOptionsControl
            End If
            Return _optionsControl
        End Get
    End Property
    Public ReadOnly Property ExcelImportOptions() As ExcelImportOptionsDataEntity
        Get
            Return DirectCast(_optionsControl, ExcelImportOptionsControl).Options
        End Get
    End Property

    Public ReadOnly Property ImportFileIsPackage() As Boolean Implements IImportHandler.ImportFileIsPackage
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property UserFriendlyName() As String Implements IImportHandler.UserFriendlyName
        Get
            Return My.Resources.ImportFromAExcelFile
        End Get
    End Property

    Public ReadOnly Property ProgressMessage() As String Implements IImportHandler.ProgressMessage
        Get
            Return My.Resources.ImportingResourcesToSpecifiedLocation
        End Get
    End Property

    Private _disposedValue As Boolean = False

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me._disposedValue Then
            If disposing Then
            End If

        End If
        Me._disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub


End Class
