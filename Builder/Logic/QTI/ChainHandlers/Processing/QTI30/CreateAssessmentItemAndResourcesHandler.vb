Imports System.Collections.Concurrent
Imports System.IO
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Helpers.QTI30.QtiModelHelpers
Imports Questify.Builder.Logic.QTI.Model.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI30.ResourceType

Namespace QTI.ChainHandlers.Processing.QTI30

    Public Class CreateAssessmentItemAndResourcesHandler
        Inherits ChainHandlerBase

        Protected _assessmentItem As AssessmentItem
        Protected ReadOnly _itemcode As String
        Private ReadOnly _testIdentifiers As List(Of String)
        Protected _resourceHelper As ResourceHelper
        Private _lock As Object
        Protected _itemHelper As ItemHelper
        Protected _folderDirectory As ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
        Protected _resourceTypes As ConcurrentDictionary(Of PackageCreator.QTIManifestResourceType, ResourceTypeType)
        Private _settings As XmlReaderSettings
        Protected _itemDocument As XmlDocument
        Private _generatedCssContent As ConcurrentBag(Of String)

        Public Sub New(itemCode As String, testIdentifiers As List(Of String), packageCreator As PackageCreator)
            MyBase.New(packageCreator)
            _itemcode = itemCode
            _testIdentifiers = testIdentifiers
            LastHandledObject = $"item {itemCode}"
            If _assessmentItem Is Nothing Then
                _assessmentItem = Me.PackageCreator.GetItemByCode(_itemcode)
            End If
        End Sub

        Public Overrides Function ProcessRequest(requestData As PublicationRequest) As ChainHandlerResult
            _generatedCssContent = requestData.GeneratedCssContent
            _folderDirectory = requestData.FileTypeDictionary
            _lock = requestData.ValidationLock
            _settings = requestData.Settings
            _resourceTypes = requestData.ResourceTypeDictionary
            _resourceHelper = GetResourceHelper()
            Dim chainHandlerRequest As ChainHandlerResult = ExecuteRequest(requestData)
            Return chainHandlerRequest
        End Function

        Protected Overridable Function GetResourceHelper() As ResourceHelper
            Return New ResourceHelper(_folderDirectory, _resourceTypes, PackageCreator)
        End Function

        Protected Overridable Function GetPublishedItem(itemHelper As ItemHelper, maxScore As Integer, itemDocument As XmlDocument) As PublishedItem
            Return New PublishedItem(itemHelper, itemDocument, maxScore)
        End Function

        Protected Overridable Sub SaveItem(itemHelper As ItemHelper, ByRef filesPerEntity As ConcurrentDictionary(Of String, List(Of String)))
            _itemDocument = itemHelper.CreateItemDocument()
            ChainHandlerHelper.SaveDocument(_itemDocument, GetFullItemPath(False), _assessmentItem.Identifier, filesPerEntity)
        End Sub

        Protected Overridable Function GetItemHelper(item As AssessmentItem, resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String)) As ItemHelper
            Return New ItemHelper(item, resources, resourceMimeTypeDictionary, _resourceHelper, PackageCreator)
        End Function

        Protected Function GetFullItemPath(isExtension As Boolean) As String
            Dim _itemResourceIdentifier = _itemHelper.GetItemResourceIdentifier
            Dim fileName As String = $"{_itemResourceIdentifier}.xml"
            If isExtension Then
                fileName = $"{_itemResourceIdentifier}_extension.xml"
            End If
            Return Path.Combine(Path.Combine(PackageCreator.TempWorkingDirectory.FullName, _folderDirectory(PackageCreatorConstants.FileDirectoryType.items)), fileName)
        End Function

        Protected Sub Validate(filesPerEntity As ConcurrentDictionary(Of String, List(Of String)))
            SyncLock _lock
                For Each fileName As String In filesPerEntity.Item(_assessmentItem.Identifier)
                    Using reader As XmlReader = XmlReader.Create(fileName, _settings)
                        While reader.Read()
                        End While

                        If PackageCreator.Errors.Count > 0 Then
                            Throw New XmlException("XSD validation failed.")
                        End If
                    End Using
                Next
            End SyncLock
        End Sub

        Protected Overridable Sub ExecuteRequestActions(requestData As PublicationRequest)
            SyncLock PackageCreator.LockCss
                If Not String.IsNullOrEmpty(PackageCreator.AdditionalItemCssStyles) Then
                    requestData.GeneratedCssContent.Add(PackageCreator.AdditionalItemCssStyles)
                End If
            End SyncLock
            Dim publishedItem As PublishedItem
            If Not requestData.ListOfItems.ContainsKey(_itemcode) Then
                publishedItem = ProcessItem(requestData.Resources, requestData.ResourceMimeTypeDictionary, requestData.FilesPerEntity)
                requestData.ListOfItems.TryAdd(_itemcode, publishedItem)
            Else
                publishedItem = requestData.ListOfItems(_itemcode)
            End If
            For Each testIdentifier In _testIdentifiers
                Dim testDocumentSet As TestDocumentSet = requestData.Tests(testIdentifier)
                _itemHelper.AddItemToManifest(publishedItem, requestData.Resources, testDocumentSet, _itemcode, testIdentifier, requestData.FileTypeDictionary(PackageCreatorConstants.FileDirectoryType.items), publishedItem.ItemMetaDataCollection, PackageCreator)
            Next
        End Sub

        Protected Overridable Function GetNamespaceHelper() As NamespaceHelper
            Return New QTI30NamespaceHelper
        End Function

        Private Function ProcessItem(resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String), ByRef filesPerEntity As ConcurrentDictionary(Of String, List(Of String))) As PublishedItem
            _itemHelper = GetItemHelper(_assessmentItem, resources, resourceMimeTypeDictionary)
            SaveItem(_itemHelper, filesPerEntity)
            If Not String.IsNullOrWhiteSpace(_itemHelper.CssContent) Then
                _generatedCssContent.Add(_itemHelper.CssContent)
            End If
            Dim maxScore As Integer = 0
            If _assessmentItem.Solution IsNot Nothing Then
                maxScore = _assessmentItem.Solution.MaxSolutionRawScore
            End If
            Validate(filesPerEntity)
            Return GetPublishedItem(_itemHelper, maxScore, _itemDocument)
        End Function

        Private Function ExecuteRequest(requestData As PublicationRequest) As ChainHandlerResult
            ExecuteRequestActions(requestData)

            _assessmentItem = Nothing
            _resourceHelper = Nothing
            _folderDirectory = Nothing
            _resourceTypes = Nothing

            Return ChainHandlerResult.RequestHandled
        End Function

    End Class
End Namespace