Imports System.Collections.Concurrent
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports Questify.Builder.Logic.QTI.Model.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI30.ResourceType
Imports Questify.Builder.Logic.QTI.Interfaces.QTI30
Imports System.Xml.Serialization

Namespace QTI.Helpers.QTI30.QtiModelHelpers


    Public Class ItemHelper

        Protected PackageCreator As PackageCreator
        Protected AssessmentItem As AssessmentItem
        Protected AssessmentItemType As AssessmentItemType
        Protected QtiTemplate As QtixHtml
        Protected ItemMetaDataCollection As MetaDataCollection
        Protected Preview As Boolean = False
        Protected ResourceMimeTypeDictionary As ConcurrentDictionary(Of String, String)
        Protected StyleSheets As New List(Of String)
        Protected ScoringMethod As IScoringConverter
        Protected ParsedTemplate As String = String.Empty
        Protected ResourceHelper As ResourceHelper
        Protected StylesheetHelper As QTI30StyleSheetHelper = Nothing
        Protected TemplateHelper As TemplateHelper = Nothing
        Protected TranslationTable As ItemScoreTranslationTable


        Public Property Resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String)))

        Public Property Dependencies As New List(Of String)

        Public Property ItemType As Nullable(Of ItemTypeEnum)

        Public Property Code As String
        Public Property Title As String

        Public Property MetaDataCollection As MetaDataCollection
            Get
                Return ItemMetaDataCollection
            End Get
            Set
                ItemMetaDataCollection = Value
            End Set
        End Property

        Public Property CssContent As String



        Public Sub New(item As AssessmentItem, resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String), resourceHelper As ResourceHelper, template As String, scoringConverter As IScoringConverter, packageCreator As PackageCreator)
            Init(item, resources, resourceMimeTypeDictionary, resourceHelper, template, scoringConverter, packageCreator)
        End Sub

        Public Sub New(item As AssessmentItem, resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String), resourceHelper As ResourceHelper, packageCreator As PackageCreator)
            Init(item, resources, resourceMimeTypeDictionary, resourceHelper, packageCreator)
        End Sub



        Public Overridable Function CreateItemDocument() As XmlDocument
            Dim itemDocument As XmlDocument = Nothing

            If Not String.IsNullOrEmpty(ParsedTemplate) Then
                CreateAssessmentItemType()
                CreateAndUpdateItemDocuments(itemDocument, Nothing)
            Else
                Throw New ArgumentException(String.Format(CultureInfo.InvariantCulture, "qti element not found in item {0}", AssessmentItem.Title))
            End If

            Return itemDocument
        End Function

        Protected Sub CreateAssessmentItemType()
            AssessmentItemType = CreateItem()
            Dim tempDoc As New XmlDocument
            tempDoc.PreserveWhitespace = True
            tempDoc.LoadXml(String.Format(CultureInfo.InvariantCulture, "<wrapper>{0}</wrapper>", GetTemplateHelper().GetQtiTemplate(ParsedTemplate)))
            CreateUniqueC1Identifiers(tempDoc)

            If ScoringMethod IsNot Nothing AndAlso HasScore() Then
                AddScore(tempDoc, AssessmentItemType)
            ElseIf Not HasScore() Then
                ScoringHelper.AddEmptyResponseDeclaration(tempDoc, AssessmentItemType)
            End If

            If ScoringMethod IsNot Nothing Then
                ScoringMethod.UpdateDocumentBeforeProcessing(AssessmentItem.Solution, tempDoc, PackageCreator)
            End If

            QtiTemplate = New QtixHtml(tempDoc.DocumentElement.InnerXml, AssessmentItem.Identifier, Preview, Resources, ResourceMimeTypeDictionary, CssContent, ResourceHelper, PackageCreator)
            tempDoc.LoadXml(QtiTemplate.ToString)

            ResourceHelper.ReplaceLanguageStyles(tempDoc)

            Dim attachmentResources As List(Of String) = GetAttachmentResourceNames(AssessmentItem)
            Dim attachmentDependencies As List(Of String) = ResourceHelper.ProcessAttachmentResources(attachmentResources, Resources, ResourceMimeTypeDictionary, Preview, ChainHandlerHelper.GetIdentifierFromResourceId(AssessmentItem.Identifier, PackageCreatorConstants.TypeOfResource.item))

            Dependencies.AddRange(QtiTemplate.Dependencies)
            Dependencies.AddRange(attachmentDependencies)

            AssessmentItemType.qticompanionmaterialsinfo = CreateCompanionMaterialsInfo(tempDoc)
            AssessmentItemType.qtiitembody = CreateItemBody(tempDoc, AssessmentItem)

            CssContent = QtiTemplate.Css
            ProcessStyleSheets(AssessmentItemType, QtiTemplate)
            ProcessItemType(AssessmentItem)
            ProcessTts(AssessmentItem)
        End Sub

        Protected Overridable Sub CreateAndUpdateItemDocuments(ByRef itemDocument As XmlDocument, ByRef itemExtensionDocument As XmlDocument)
            itemDocument = GetItemDocumentXml(AssessmentItemType)

            If ScoringMethod IsNot Nothing Then
                ScoringMethod.UpdateDocument(AssessmentItem.Solution, itemDocument, itemExtensionDocument, PackageCreator)
            ElseIf Not HasScore() Then
                ScoringHelper.ConvertResponseIdentifierToFixedName(itemDocument)
            End If

            ScoringHelper.AddResponseToMediaInteraction(itemDocument)
            ScoringHelper.AddResponseToCustomInteraction(itemDocument)
            ScoringHelper.AddResponseToUploadInteraction(itemDocument)
            ScoringHelper.ConvertMediaResponseIdentifierToFixedName(itemDocument)

            Dim namespaceHelper = GetNamespaceHelper()

            TextToSpeechHelper.ConvertToSsml(itemDocument, namespaceHelper.GetSSMLNamespace().NamespaceName)

            namespaceHelper.UpdateNameSpaces(itemDocument, True)
            namespaceHelper.UpdateNameSpaces(itemExtensionDocument, True)
        End Sub

        Public Function GetFileList() As List(Of FileType)
            Dim list As New List(Of FileType)
            list.Add(GetItemFileType(False))

            Return list
        End Function



        Public Sub Init(item As AssessmentItem,
                        resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                        resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String),
                        resourceHelper As ResourceHelper,
                        template As String,
                        scoringMethod As IScoringConverter,
                        packageCreator As PackageCreator)

            Me.PackageCreator = packageCreator
            AssessmentItem = item

            If scoringMethod Is Nothing Then
                Me.ScoringMethod = Me.PackageCreator.GetScoreConverterFromScoringParameters(AssessmentItem)
            Else
                Me.ScoringMethod = scoringMethod
            End If

            If String.IsNullOrEmpty(template) Then
                ParsedTemplate = GetTemplateHelper().GetParsedTemplate(AssessmentItem, ItemMetaDataCollection, AssessmentItem.Identifier, packageCreator, GetAssessmentTestViewType)
            Else
                ParsedTemplate = template
            End If

            ItemType = GetItemType(item)
            _Code = item.Identifier
            _Title = item.Title
            Me.Resources = resources
            Me.ResourceMimeTypeDictionary = resourceMimeTypeDictionary

            If item.Solution IsNot Nothing AndAlso item.Solution.ItemScoreTranslationTable IsNot Nothing AndAlso item.Solution.ItemScoreTranslationTable.Count > 0 Then
                TranslationTable = item.Solution.ItemScoreTranslationTable
            End If
            Me.ResourceHelper = resourceHelper
        End Sub

        Public Sub Init(
                        item As AssessmentItem,
                        resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                        resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String),
                        resourceHelper As ResourceHelper,
                        packageCreator As PackageCreator)

            Init(item, resources, resourceMimeTypeDictionary, resourceHelper, String.Empty, Nothing, packageCreator)
        End Sub

        Protected Overridable Function GetNamespaceHelper() As NamespaceHelper
            Return New QTI30NamespaceHelper
        End Function

        Protected Overridable Function GetStylesheetHelper() As QTI30StyleSheetHelper
            If StylesheetHelper Is Nothing Then StylesheetHelper = New QTI30StyleSheetHelper
            Return StylesheetHelper
        End Function

        Protected Overridable Function GetTemplateHelper() As TemplateHelper
            If TemplateHelper Is Nothing Then TemplateHelper = New TemplateHelper
            Return TemplateHelper
        End Function

        Protected Overridable Function GetAssessmentTestViewType() As String
            Return GenericTestModelPlugin.PLUGIN_NAME
        End Function

        Public Overridable Sub AddItemToManifest(publishedItem As PublishedItem, resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), testDocumentSet As TestDocumentSet, itemCode As String,
                                                 testCode As String, itemDir As String, itemMetaDataCollection As MetaDataCollection, packageCreator As PackageCreator)
            Dim item As AssessmentItem = packageCreator.GetItemByCode(itemCode)
            Dim fileNameItem As String = $"{itemCode}.xml"
            Dim hrefItem As String = $"{itemDir}/{fileNameItem}"
            Dim resourceType As ResourceType = CreateItemResourceType(item, hrefItem, publishedItem, itemMetaDataCollection)
            PackageCreator.AddResourceToManifest(resources, resourceType, publishedItem.FileList.ToArray)

            Dim testId As String = ChainHandlerHelper.GetIdentifierFromResourceId(testCode, PackageCreatorConstants.TypeOfResource.test)
            Dim itemId As String = ChainHandlerHelper.GetIdentifierFromResourceId(item.Identifier, PackageCreatorConstants.TypeOfResource.item)
            PackageCreator.AddDependentResourceToManifest(resources, testId, itemId)

            PackageCreator.AddDependentResourceToManifest(resources, itemId, publishedItem.DepencencyList)
        End Sub

        Protected Overridable Function CreateItemResourceType(item As AssessmentItem, hrefItem As String, publishedItem As PublishedItem, itemMetaDataCollection As MetaDataCollection) As ResourceType
            Dim returnValue = GetNewResourceType(itemMetaDataCollection)
            returnValue.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(item.Identifier, PackageCreatorConstants.TypeOfResource.item)
            returnValue.type = ResourceTypeType.imsqti_item_xmlv3p0
            returnValue.href = hrefItem





            Return returnValue
        End Function

        Protected Overridable Function GetMetadata(item As AssessmentItem) As ManifestMetadataType
            Return New ManifestMetadataType
        End Function

        Protected Overridable Function GetNewResourceType(itemMetaDataCollection As MetaDataCollection) As ResourceType
            Return New ResourceType
        End Function

        Protected Overridable Function GetItemFileType(isExtension As Boolean) As FileType
            Dim format As String = "{0}.xml"
            Dim fileNameItem As String = String.Format(format, AssessmentItem.Identifier)
            Dim hrefItem As String = $"{ResourceHelper.FolderDirectory(PackageCreatorConstants.FileDirectoryType.items)}/{fileNameItem}"
            Dim itemFile As New FileType

            itemFile.href = hrefItem

            Return itemFile
        End Function

        Protected Overridable Function GetAttachmentResourceNames(ByVal item As AssessmentItem) As List(Of String)
            Return New List(Of String)
        End Function

        Protected Overridable Function GetItemLanguage(item As AssessmentItem) As String
            Return String.Empty
        End Function

        Protected Overridable Sub ProcessStyleSheets(item As AssessmentItemType, qtiTemplate As QtixHtml)
            If Not String.IsNullOrEmpty(CssContent.Trim) Then
                GetStylesheetHelper.PrefixGeneratedStyles(CssContent)
                qtiTemplate.StyleSheets.Add(GetStylesheetHelper.GetStylesheetType(PackageCreatorConstants.GENERATED_CSS, PackageCreator.RelativePathToResources))
                qtiTemplate.Dependencies.Add(PackageCreatorConstants.GENERATED_CSS)
            End If

            Dim cssStyleFileName As String = String.Concat(Path.GetFileNameWithoutExtension(PackageCreatorConstants.GENERATED_CSS), "_", AssessmentItem.Identifier.Replace(" ", String.Empty), Path.GetExtension(PackageCreatorConstants.GENERATED_CSS))
            Dim styles As List(Of String) = TemplateHelper.GetStyles(ParsedTemplate)
            Dim styleOverrideFromTemplate As String = String.Empty

            If styles IsNot Nothing AndAlso Not styles.Count = 0 Then
                GetStylesheetHelper.AddStyleToCss(styleOverrideFromTemplate, styles)
                PackageCreator.SaveGeneratedCss(Resources, styleOverrideFromTemplate, cssStyleFileName, ResourceHelper.FolderDirectory(PackageCreatorConstants.FileDirectoryType.css), ResourceHelper.ResourceTypes(PackageCreator.QTIManifestResourceType.webcontent))
                qtiTemplate.StyleSheets.Add(GetStylesheetHelper.GetStylesheetType(cssStyleFileName, PackageCreator.RelativePathToResources))
            End If

            GetDependentStylesheetsFromSourceText(qtiTemplate.StyleSheets)

            qtiTemplate.StyleSheets.ForEach(Sub(styleSheet)
                                                Dim resourceName As String = Path.GetFileName(styleSheet.href).Replace("%20", " ")
                                                Dim nameInManifest = ChainHandlerHelper.GetIdentifierFromResourceId(resourceName, PackageCreatorConstants.TypeOfResource.resource)
                                                If Not Dependencies.Contains(nameInManifest) Then
                                                    Dependencies.Add(nameInManifest)
                                                End If
                                            End Sub)
            item.qtistylesheet = qtiTemplate.StyleSheets
        End Sub

        Protected Overridable Sub ProcessTts(item As AssessmentItem)
        End Sub

        Protected Overridable Sub ProcessItemType(item As AssessmentItem)
        End Sub

        Protected Overridable Sub GetDependentStylesheetsFromSourceText(ByRef styleSheetList As List(Of StyleSheetType))
        End Sub

        Private Function CreateItem() As AssessmentItemType
            Dim asssement = GetAssessmentItemType()
            asssement.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(AssessmentItem.Identifier, PackageCreatorConstants.TypeOfResource.item)
            asssement.title = AssessmentItem.Title
            asssement.adaptive = False
            asssement.timedependent = False

            Return asssement
        End Function

        Protected Overridable Function GetAssessmentItemType() As AssessmentItemType
            Return New AssessmentItemType
        End Function

        Protected Function HasScore() As Boolean
            Return HasAutomaticScoring(AssessmentItem.Solution) OrElse HasManualScoring(AssessmentItem.Solution)
        End Function

        Private Function HasAutomaticScoring(solution As Solution) As Boolean
            Dim returnValue = False

            If solution IsNot Nothing _
               AndAlso solution.Findings IsNot Nothing _
               AndAlso Not solution.Findings.Count = 0 _
               AndAlso Not (solution.Findings.Where(Function(f) KeyFactsInFindingsAreEmpty(f) = True OrElse FindingHasFacts(f)).Count = 0) _
                Then
                returnValue = True
            End If

            Return returnValue
        End Function

        Private Function KeyFactsInFindingsAreEmpty(finding As KeyFinding) As Boolean
            Return finding.KeyFactsets IsNot Nothing AndAlso Not finding.KeyFactsets.Count = 0 AndAlso
                   Not finding.KeyFactsets.Where(Function(ks) KeyFactHasFacts(ks) = True).Count = 0
        End Function

        Private Function KeyFactHasFacts(keyFactSet As KeyFactSet) As Boolean
            Return keyFactSet.Facts IsNot Nothing AndAlso Not keyFactSet.Facts.Count = 0 AndAlso keyFactSet.Facts.Any(Function(f) f.Values.Count > 0)
        End Function

        Private Function FindingHasFacts(finding As KeyFinding) As Boolean
            Return finding IsNot Nothing AndAlso finding.Facts IsNot Nothing AndAlso Not finding.Facts.Count = 0
        End Function

        Public Shared Function HasManualScoring(solution As Solution) As Boolean
            Dim returnValue = False

            If solution IsNot Nothing _
               AndAlso solution.AspectReferenceSetCollection IsNot Nothing _
               AndAlso Not solution.AspectReferenceSetCollection.Count = 0 _
               AndAlso Not solution.AspectReferenceSetCollection(0).Items.Count = 0 _
                Then
                returnValue = True
            End If

            Return returnValue
        End Function

        Protected Sub AddScore(doc As XmlDocument, item As AssessmentItemType)
            Dim xmlNamespaceManager As New XmlNamespaceManager(doc.NameTable)
            xmlNamespaceManager.AddNamespace("qti", doc.DocumentElement.NamespaceURI)
            xmlNamespaceManager.AddNamespace("html", "http://www.w3.org/1999/xhtml")

            Dim responseIdentifierAttributeList As XmlNodeList = ResponseIdentifierHelper.GetResponseIdentifiers(doc, xmlNamespaceManager)
            Dim outcomeDeclarations = New List(Of OutcomeDeclarationType)
            Dim shouldScoreBeTranslated = QTIScoringHelper.ShouldScoreBeTranslated(TranslationTable)
            Dim responseDeclarations = New List(Of ResponseDeclarationType)

            If ScoringMethod IsNot Nothing AndAlso HasScore() Then
                responseDeclarations.AddRange(GetResponseDeclarations(responseIdentifierAttributeList))
                outcomeDeclarations.AddRange(ScoringMethod.GetOutcomeDeclarations(AssessmentItem.Solution, responseIdentifierAttributeList, TranslationTable))

                Dim responseProcessing As XmlDocument = ScoringMethod.GetResponseProcessing(AssessmentItem.Solution, responseIdentifierAttributeList, shouldScoreBeTranslated)
                If responseProcessing IsNot Nothing Then item.qtiresponseprocessing = ResponseProcessingHelper.MergeResponseProcessing(item.qtiresponseprocessing, CreateResponseProcessing(responseProcessing.OuterXml))
            End If

            item.qtioutcomedeclaration = outcomeDeclarations
            item.qtiresponsedeclaration = responseDeclarations
        End Sub

        Private Function GetResponseDeclarations(responseIdentifierAttributeList As XmlNodeList) As List(Of ResponseDeclarationType)
            Dim responsDeclarationList As New List(Of ResponseDeclarationType)

            For Each responseDeclaration As ResponseDeclarationType In ScoringMethod.GetResponseDeclarations(AssessmentItem.Solution, responseIdentifierAttributeList, ItemType)
                If Not String.IsNullOrEmpty(responseDeclaration.identifier) Then
                    responsDeclarationList.Add(responseDeclaration)
                End If
            Next

            Return responsDeclarationList
        End Function

        Private Function GetItemType(item As AssessmentItem) As Nullable(Of ItemTypeEnum)
            Dim itemType As Nullable(Of ItemTypeEnum) = Nothing

            If PackageCreator.ResourceMan IsNot Nothing Then
                Dim metaDataCollectionItemLayoutTemplate As MetaDataCollection = PackageCreator.ResourceMan.GetResourceMetaData(item.LayoutTemplateSourceName)
                Dim itemTypestring As String = metaDataCollectionItemLayoutTemplate.Find(Function(metaData) String.Compare(metaData.Name, "ItemType", True) = 0).Value

                If [Enum].IsDefined(GetType(ItemTypeEnum), itemTypestring) Then
                    itemType = CType([Enum].Parse(GetType(ItemTypeEnum), itemTypestring), ItemTypeEnum)
                End If
            End If

            Return itemType
        End Function

        Protected Overridable Function CreateItemBody(doc As XmlDocument, item As AssessmentItem) As ItemBodyType
            Dim xmlnamespaceManager As New XmlNamespaceManager(doc.NameTable)
            Dim bodynode As XmlNode = doc.SelectSingleNode("//qti-item-body", xmlnamespaceManager)

            AddDefaultNamespaces(CType(bodynode, XmlElement))

            Dim itemBody As ItemBodyType = CType(ChainHandlerHelper.StringToObject(bodynode.OuterXml, GetType(ItemBodyType)), ItemBodyType)
            Dim itemLanguage As String = GetItemLanguage(item)

            If Not String.IsNullOrEmpty(itemLanguage) Then
                itemBody.lang = itemLanguage
            End If

            Return itemBody
        End Function

        Protected Overridable Function CreateCompanionMaterialsInfo(ByRef doc As XmlDocument) As CompanionMaterialsInfoType
            Dim xmlNamespaceManager As New XmlNamespaceManager(doc.NameTable)

            Dim materialsNode As XmlNode = doc.SelectSingleNode("//qti-companion-materials-info", xmlNamespaceManager)
            If materialsNode IsNot Nothing Then
                AddDefaultNamespaces(CType(materialsNode, XmlElement))
                Dim companionMaterials As CompanionMaterialsInfoType = CType(ChainHandlerHelper.StringToObject(materialsNode.OuterXml, GetType(CompanionMaterialsInfoType)), CompanionMaterialsInfoType)
                materialsNode.ParentNode.RemoveChild(materialsNode)
                Return companionMaterials
            End If
            Return Nothing
        End Function

        Private Sub AddDefaultNamespaces(xmlElement As XmlElement)
            xmlElement.SetAttribute("xmlns", "http://www.imsglobal.org/xsd/imsqtiasi_v3p0")
            xmlElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        End Sub

        Private Sub AddDefaultNamespaces(doc As XmlDocument)
            AddDefaultNamespaces(doc.DocumentElement)
        End Sub

        Private Sub AddDefaultNamespaces(ByRef content As String)
            Dim tempDoc As New XmlDocument
            tempDoc.PreserveWhitespace = True
            tempDoc.LoadXml(content)
            AddDefaultNamespaces(tempDoc)
            content = tempDoc.InnerXml
        End Sub

        Private Function CreateResponseProcessing(responseProcessing As String) As ResponseProcessingType
            If Not String.IsNullOrEmpty(responseProcessing) Then
                AddDefaultNamespaces(responseProcessing)
                Return CType(ChainHandlerHelper.StringToObject(responseProcessing, GetType(ResponseProcessingType)), ResponseProcessingType)
            Else
                Return Nothing
            End If
        End Function

        Private Function GetItemDocumentXml(item As AssessmentItemType) As XmlDocument
            Return ChainHandlerHelper.ObjectToXmlDocument(item, Nothing)
        End Function

        Private Sub CreateUniqueC1Identifiers(ByRef xmlDoc As XmlDocument)
            Dim xDoc As XDocument = XDocument.Parse(xmlDoc.DocumentElement.OuterXml, LoadOptions.PreserveWhitespace)
            Dim uniqueIdentifiers As New Dictionary(Of String, Integer)

            xDoc.Descendants().Where(Function(n) n.Attributes.Any(Function(a) a.Name.ToString.Equals("id") AndAlso a.Value.StartsWith("c1-id-"))).ToList().ForEach(Sub(d)
                                                                                                                                                                       Dim attr = d.Attributes.First(Function(at) at.Name.ToString.Equals("id") AndAlso at.Value.StartsWith("c1-id-"))
                                                                                                                                                                       If attr IsNot Nothing AndAlso attr.Value IsNot Nothing Then
                                                                                                                                                                           If uniqueIdentifiers.ContainsKey(attr.Value) Then
                                                                                                                                                                               uniqueIdentifiers(attr.Value) += 1
                                                                                                                                                                               attr.Value =
                                                                                                                                                                                                                                                                                                                             $"{attr.Value}-{ _
                                                                                                                                                                                                                                                                                                                             uniqueIdentifiers(attr.Value)}"
                                                                                                                                                                           Else
                                                                                                                                                                               uniqueIdentifiers.Add(attr.Value, 1)
                                                                                                                                                                           End If
                                                                                                                                                                       End If
                                                                                                                                                                   End Sub)
            xmlDoc = xDoc.ToXmlDocument()

        End Sub


        Function [IsNot]() As Integer
            Throw New NotImplementedException
        End Function

    End Class
End Namespace