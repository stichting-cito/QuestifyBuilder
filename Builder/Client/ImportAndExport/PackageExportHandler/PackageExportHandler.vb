Imports System.ComponentModel
Imports System.IO
Imports Cito.Tester.Common
Imports System.Linq
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Packaging

Public Class PackageExportHandler
    Implements IDisposable
    Implements IExportHandlerPackage

    Private _optionsControl As ExportOptionControlBase
    Private _resourceIds As List(Of Guid)
    Private WithEvents _exportMan As ExportManager
    Private ReadOnly _safeBankNames As New Dictionary(Of String, Integer)(StringComparer.CurrentCultureIgnoreCase)
    Public Property HasError As Boolean

    Public ReadOnly Property PackageExportOptions() As PackageExportOptionsDataEntity
        Get
            Return DirectCast(_optionsControl, PackageExportOptionsControl).Options
        End Get
    End Property

    Public Event Progress(ByVal sender As Object, ByVal e As ProgressEventArgs) Implements IExportHandlerPackage.Progress
    Public Event StartProgress(ByVal sender As Object, ByVal e As StartEventArgs) Implements IExportHandlerPackage.StartProgress

    Private Sub _exportMan_PublicationProgress(ByVal sender As Object, ByVal e As ProgressEventArgs) Handles _exportMan.ExportProgress
        RaiseEvent Progress(Me, e)
    End Sub

    Private Sub _exportMan_StartPublication(ByVal sender As Object, ByVal e As StartEventArgs) Handles _exportMan.StartExport
        RaiseEvent StartProgress(Me, e)
    End Sub

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



    Public ReadOnly Property GetOptionsUserControl() As ExportOptionControlBase Implements IExportHandlerPackage.GetOptionsUserControl
        Get
            If _optionsControl Is Nothing Then _optionsControl = New PackageExportOptionsControl
            Return _optionsControl
        End Get
    End Property

    Public ReadOnly Property ProgressMessage() As String Implements IExportHandlerPackage.ProgressMessage
        Get
            Return My.Resources.ExportingResourcesToSpecifiedLocation
        End Get
    End Property

    Public ReadOnly Property UserFriendlyName() As String Implements IExportHandlerPackage.UserFriendlyName
        Get
            Return My.Resources.ExportToAPackageFile
        End Get
    End Property

    Public Property ResourceIds As List(Of Guid) Implements IExportHandlerPackage.ResourceIds
        Get
            If _resourceIds Is Nothing Then _resourceIds = New List(Of Guid)
            Return _resourceIds
        End Get
        Set(ByVal value As List(Of Guid))
            _resourceIds = value
        End Set
    End Property


    Public Function Export(ByVal bgWorker As BackgroundWorker, ByVal sourceResourceManager As ResourceManagerBase, ByVal resources As ResourceEntryCollection) As Boolean Implements IExportHandlerPackage.Export

        _exportMan_PublicationProgress(Me, New ProgressEventArgs(My.Resources.PackageExportHandler_CreateExportFile))

        Dim packageRoot As String

        If Path.GetDirectoryName(Me.PackageExportOptions.PackageUrl).Length = 0 Then
            packageRoot = New FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Me.PackageExportOptions.PackageUrl)).FullName
        Else
            packageRoot = New FileInfo(Me.PackageExportOptions.PackageUrl).FullName
        End If

        If Me.PackageExportOptions.PackageUrl.EndsWith(".export", StringComparison.CurrentCultureIgnoreCase) Then
            packageRoot = $"{packageRoot}/"
        End If

        If File.Exists(Me.PackageExportOptions.PackageUrl) AndAlso Me.PackageExportOptions.OverwriteExisting Then
            File.Delete(Me.PackageExportOptions.PackageUrl)
        End If

        Dim exportResult As Boolean
        Me._exportMan = New ExportManager(sourceResourceManager, packageRoot)

        Dim metaDataCollection As New MetaDataCollection
        If TypeOf sourceResourceManager Is DataBaseResourceManager Then
            Dim customBankProperties = DirectCast(sourceResourceManager, DataBaseResourceManager).BankCustomProperties
            If customBankProperties IsNot Nothing AndAlso customBankProperties.Count > 0 Then
                Dim index As Integer = 1
                _exportMan_StartPublication(Me, New StartEventArgs(customBankProperties.Count))

                Dim referencedCustomProperties As EntityCollection = BankFactory.Instance.GetReferencedCustomBankPropertiesForListOfResources(ResourceIds)
                If referencedCustomProperties IsNot Nothing AndAlso referencedCustomProperties.Count > 0 Then
                    For Each cbp In customBankProperties
                        Dim cbpe As CustomBankPropertyEntity = DirectCast(cbp, CustomBankPropertyEntity)
                        _exportMan_PublicationProgress(Me, New ProgressEventArgs(String.Format(My.Resources.PackageExportHandler_ProcessingCustomBankProperties, cbpe.Name), index))
                        If referencedCustomProperties.Any(Function(cp) CType(cp, CustomBankPropertyEntity).CustomBankPropertyId = cbpe.CustomBankPropertyId) Then
                            metaDataCollection.Add(cbpe.ToResourceManagerSharedMetaData())
                        End If
                        index += 1
                    Next
                End If
            End If
        End If
        exportResult = Me._exportMan.ExportResources(bgWorker, metaDataCollection, resources)
        _exportMan.Dispose()

        If TypeOf _optionsControl Is PackageExportOptionsControl Then
            TestBuilderClientSettings.ExportLocation = Path.GetDirectoryName(DirectCast(_optionsControl, PackageExportOptionsControl).Options.PackageUrl)
        End If
        Return exportResult
    End Function

    Public Function Export(ByVal bgWorker As BackgroundWorker, ByVal sourceResourceManager As ResourceManagerBase, ByVal bankId As Integer) As Boolean Implements IExportHandlerPackage.Export
        Dim packageSet As PackageSet = CreatePackageSet(bankId)
        _safeBankNames.Clear()
        If ExportBank(bgWorker, bankId, Me.PackageExportOptions.ExportSubBanks, 0, packageSet.PackageSetEntryCollection(0)) Then
            If packageSet.PackageSetEntryCollection(0).PackageSetEntrySubCollection.Count >= 1 Then
                PackageSet.SaveToFile(Me.PackageExportOptions.PackageUrl + "set", packageSet)
            End If

            If TypeOf _optionsControl Is PackageExportOptionsControl Then
                TestBuilderClientSettings.ExportLocation = Path.GetDirectoryName(DirectCast(_optionsControl, PackageExportOptionsControl).Options.PackageUrl)
            End If

            Return True
        Else
            Return False
        End If
    End Function


    Private Function ExportBank(ByVal bgWorker As BackgroundWorker, ByVal bankId As Integer, ByVal recurse As Boolean, ByVal bankLevelInExport As Integer, ByVal packageSetEntry As PackageSetEntry) As Boolean
        Dim bank = DtoFactory.Bank.Get(bankId)

        Return ExportBank(bgWorker, bank, recurse, bankLevelInExport, packageSetEntry)
    End Function

    Private Function ExportBank(ByVal bgWorker As BackgroundWorker, ByVal bank As BankDto, ByVal recurse As Boolean, ByVal bankLevelInExport As Integer, ByVal packageSetEntry As PackageSetEntry) As Boolean
        Dim exportResult As Boolean = True
        Dim packageRootPath As String
        Dim packagePathFormat As String

        packageRootPath = Path.GetDirectoryName(Me.PackageExportOptions.PackageUrl)
        packagePathFormat = Path.Combine(packageRootPath, "{0}.export\")

        Using resourceManager As New DataBaseResourceManager(bank.id, True)
            Dim resources As ResourceEntryCollection = GetResourceEntryCollectionForBank(bank.id)
            Dim name As String

            If bankLevelInExport = 0 Then
                name = Path.GetFileNameWithoutExtension(Me.PackageExportOptions.PackageUrl)
            Else
                name = GetSafeBankName(bank.name)
            End If

            resourceManager.IncludeMetaData = MetaDataType.All
            packageSetEntry.PackageUri = $"{name}.export"

            Dim bankExportPackageUrl As String = Path.Combine(Path.GetDirectoryName(Me.PackageExportOptions.PackageUrl), packageSetEntry.PackageUri)
            If File.Exists(bankExportPackageUrl) Then
                File.Delete(bankExportPackageUrl)
            End If

            Debug.WriteLine($"exporting bank [{bank.name}] with {resources.Count} resources.")

            Dim metaDataCollection As New MetaDataCollection
            For Each cbp In resourceManager.BankCustomProperties
                metaDataCollection.Add(DirectCast(cbp, CustomBankPropertyEntity).ToResourceManagerSharedMetaData())
            Next

            Me._exportMan = New ExportManager(resourceManager, String.Format(packagePathFormat, name))
            exportResult = Me._exportMan.ExportResources(bgWorker, metaDataCollection, resources)
            _exportMan.Dispose()
        End Using

        If exportResult = True AndAlso recurse = True Then
            For Each subBank As BankDto In bank.BankCollection
                Dim subPackageSetEntry As PackageSetEntry = CreatePackageSetEntry(subBank, packageSetEntry)
                exportResult = ExportBank(bgWorker, subBank, recurse, bankLevelInExport + 1, subPackageSetEntry)

                If Not exportResult Then
                    Exit For
                End If
            Next
        End If

        Return exportResult
    End Function

    Private Function GetSafeBankName(ByVal bankName As String) As String
        bankName = Path.GetInvalidFileNameChars.Aggregate(bankName, Function(current, ch) current.Replace(ch, "_"))
        If _safeBankNames.ContainsKey(bankName) Then
            _safeBankNames(bankName) += 1
            Return $"{bankName}_{_safeBankNames(bankName)}"
        Else
            _safeBankNames.Add(bankName, 0)
            Return bankName
        End If
    End Function

    Private Function GetResourceEntryCollectionForBank(ByVal bankId As Integer) As ResourceEntryCollection
        Dim resourcesForBank As EntityCollection = ResourceFactory.Instance.GetResourcesForBank(bankId, False)
        Dim resourcesForBankList As New ResourceEntryCollection

        For Each ent As ResourceEntity In resourcesForBank
            resourcesForBankList.Add(New ResourceEntry(ent.Name))
            ResourceIds.Add(ent.ResourceId)
        Next
        Return resourcesForBankList
    End Function

    Private Function CreatePackageSet(ByVal rootBankId As Integer) As PackageSet
        Dim packageSetToReturn As New PackageSet
        Dim rootPackageEntry As PackageSetEntry = CreatePackageSetEntry(DtoFactory.Bank.Get(rootBankId), Nothing)

        packageSetToReturn.PackageSetEntryCollection.Add(rootPackageEntry)
        Return packageSetToReturn
    End Function

    Private Function CreatePackageSetEntry(ByVal bank As BankDto, ByVal parentEntry As PackageSetEntry) As PackageSetEntry
        Dim packageSetEntryToReturn As New PackageSetEntry

        With packageSetEntryToReturn
            .Name = bank.name
        End With

        If parentEntry IsNot Nothing Then
            parentEntry.PackageSetEntrySubCollection.Add(packageSetEntryToReturn)
        End If

        Return packageSetEntryToReturn
    End Function


End Class
