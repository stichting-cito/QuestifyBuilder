Imports System.Collections.Concurrent
Imports System.IO
Imports System.IO.Compression
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ImageGenerator
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI30.ResourceType
Imports HtmlHelper = Questify.Builder.Logic.QTI.Helpers.QTI_Base.HtmlHelper
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.CustomInteractions
Imports System.Xml.XPath
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic.QTI.Converters.XhtmlConverter.QTI30

Namespace QTI.Helpers.QTI30

    Public Class ResourceHelper

        Protected _packageCreator As PackageCreator
        Protected _stylesheetHelper As QTI30StyleSheetHelper = Nothing
        Protected _htmlHelper As HtmlHelper = Nothing
        Protected _namespaceHelper As NamespaceHelper

        Public Property FolderDirectory As ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String)
        Public Property ResourceTypes As ConcurrentDictionary(Of PackageCreator.QTIManifestResourceType, ResourceTypeType)

        Public Sub New(folderDirectory As ConcurrentDictionary(Of PackageCreatorConstants.FileDirectoryType, String), resourceTypes As ConcurrentDictionary(Of PackageCreator.QTIManifestResourceType, ResourceTypeType), packageCreator As PackageCreator)
            _ResourceTypes = resourceTypes
            _FolderDirectory = folderDirectory
            _packageCreator = packageCreator
            _namespaceHelper = New QTI30NamespaceHelper
        End Sub

        Public Overridable Function ProcessResources(ByRef content As String, ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), ByRef resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String), isPreview As Boolean, itemCode As String) As List(Of String)
            Dim depList As New List(Of String)

            Dim matchedBase64Images = ChainHandlerHelper.GetBase64Images(content)
            For Each img In matchedBase64Images
                If IsImageFromMemory(img.Value) Then
                    Dim bytes = Convert.FromBase64String(img.Value.Replace("data:image/png;base64,", ""))
                    depList.AddRange(SaveResource(img.Value, img.Key, Nothing, bytes, content, resources, resourceMimeTypeDictionary, isPreview, itemCode))
                End If
            Next

            Dim matchedResources As MatchCollection = ChainHandlerHelper.GetAllResourcesFromTemplate(content)
            depList.AddRange(ProcessResources(matchedResources, content, resources, resourceMimeTypeDictionary, isPreview, itemCode))

            FileMimeTypeHelper.ModifyMimeType(content, resourceMimeTypeDictionary)
            Return depList
        End Function

        Private Function IsImageFromMemory(imgValue As String) As Boolean
            Return imgValue.StartsWith("data:image/png;base64,")
        End Function

        Public Overridable Function ProcessAttachmentResources(attachmentResources As List(Of String), ByVal resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), ByVal resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String), ByVal isPreview As Boolean, itemCode As String) As List(Of String)
            Dim processedDependencies As New List(Of String)

            For Each resourceName As String In attachmentResources
                Dim resourceNameOrg As String = resourceName
                Dim resourceNameUsedToReadResource As String = resourceName

                Dim resourceFile As Byte() = ReadResource(resourceNameOrg, resourceName, resourceNameUsedToReadResource, Nothing)
                If resourceFile IsNot Nothing Then
                    processedDependencies.AddRange(SaveResource(resourceNameOrg, resourceName, resourceNameUsedToReadResource, resourceFile, Nothing, resources, resourceMimeTypeDictionary, isPreview, itemCode))
                Else
                    Throw New Exception($"Attachment resource {resourceNameUsedToReadResource} cannot be found in the package!")
                End If
            Next

            Return processedDependencies
        End Function

        Private Function ProcessResources(matchedResources As MatchCollection,
                                          ByRef parsedTemplate As String,
                                          ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                          ByRef resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String),
                                          isPreview As Boolean,
                                          itemCode As String) As List(Of String)

            Dim resourceDependencies As New List(Of String)
            For Each match As Match In matchedResources
                ProcessResource(match, parsedTemplate, resources, resourceMimeTypeDictionary, isPreview, itemCode, resourceDependencies)
            Next

            Return resourceDependencies
        End Function

        Private Sub ProcessResource(match As Match,
                                    ByRef parsedTemplate As String,
                                    ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                    ByRef resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String),
                                    isPreview As Boolean,
                                    itemCode As String,
                                    ByRef resourceDependencies As List(Of String))
            Dim resourceNameOrg As String = ChainHandlerHelper.GetFilenameFromPath(match.Value)
            Dim resourceName As String = Uri.UnescapeDataString(resourceNameOrg)
            Dim resourceAlreadyProcessed As Boolean = ResourceNameAlreadyProcessed(resourceName, resources)
            Dim eventArgs As ResourceNeededEventArgs = Nothing
            Dim resourceFile As Byte() = Nothing
            Dim mimeType As String = String.Empty
            Dim resourceContent As String = String.Empty
            Dim categoryDirectory As PackageCreatorConstants.FileDirectoryType = PackageCreatorConstants.FileDirectoryType.other

            If Not resourceAlreadyProcessed Then
                eventArgs = New ResourceNeededEventArgs(resourceName, AddressOf StreamConverters.ConvertStreamToByteArray)
                _packageCreator.ResourceNeeded(Nothing, eventArgs)
                resourceName = ResourceNameHelper.GetQtiCompliantResourceName(resourceName)
                categoryDirectory = ChainHandlerHelper.GetCategoryFolderFromFileName(resourceName)

                If eventArgs.BinaryResource Is Nothing OrElse eventArgs.BinaryResource.ResourceObject Is Nothing Then
                    ProcessPlaceholders(match, resourceFile, resourceName, resourceNameOrg)
                    Debug.Assert(resourceFile IsNot Nothing, "Resourcefile is nothing... not expected !")
                Else
                    resourceFile = CType(eventArgs.BinaryResource.ResourceObject, Byte())
                End If
                If resourceFile Is Nothing Then
                    Throw New Exception($"resource {match.Value} cannot be found in the package!")
                End If
            Else
                resourceName = ResourceNameHelper.GetQtiCompliantResourceName(resourceName)
                categoryDirectory = ChainHandlerHelper.GetCategoryFolderFromFileName(resourceName)
                Dim resourceNameToSearchFor As String = resourceName
                If categoryDirectory = PackageCreatorConstants.FileDirectoryType.css Then
                    resourceNameToSearchFor = GetStylesheetHelper().PrefixStylesheet(resourceNameToSearchFor)
                End If

                If resourceMimeTypeDictionary.ContainsKey(resourceNameToSearchFor) Then
                    mimeType = resourceMimeTypeDictionary(resourceNameToSearchFor)
                End If
                Debug.Assert(Not String.IsNullOrEmpty(mimeType), "Mimetype of already processed resource is empty... not expected !")
            End If

            If Not resourceAlreadyProcessed Then
                If IsSourceTextFile(resourceFile, resourceName) Then
                    ProcessSourceText(resourceFile, resourceContent, resourceName, mimeType, eventArgs, resources)
                End If
                If categoryDirectory = PackageCreatorConstants.FileDirectoryType.css Then
                    ProcessStylesheet(resourceFile, resourceContent, resourceName, mimeType, eventArgs)
                End If

                If String.IsNullOrEmpty(mimeType) Then
                    mimeType = ChainHandlerHelper.GetMimeTypeFromFile(resourceFile, resourceName)
                End If
                mimeType = ChainHandlerHelper.ConvertMimeType(mimeType, resourceName)
                If Not resourceMimeTypeDictionary.ContainsKey(resourceName) Then
                    resourceMimeTypeDictionary.TryAdd(resourceName, mimeType)
                End If
            End If
            If categoryDirectory = PackageCreatorConstants.FileDirectoryType.other Then
                categoryDirectory = ChainHandlerHelper.GetCategoryFolderFromFile(mimeType, resourceName)
            End If

            Select Case categoryDirectory
                Case PackageCreatorConstants.FileDirectoryType.css
                    resourceName = GetStylesheetHelper.PrefixStylesheet(resourceName)
                Case PackageCreatorConstants.FileDirectoryType.other
                    categoryDirectory = ChainHandlerHelper.GetCategoryFolderFromFile(mimeType, resourceName)
            End Select

            If categoryDirectory = PackageCreatorConstants.FileDirectoryType.audio Then
                AudioAdded(resourceName, itemCode)
            End If

            If mimeType = "application/x-customInteraction" Then
                ProcessCustomInteractions(categoryDirectory, resourceFile, resources, resourceDependencies, isPreview, parsedTemplate, resourceNameOrg, _packageCreator.RelativePathToResources)
            ElseIf mimeType.Equals("application/x-portableCustomInteraction", StringComparison.InvariantCultureIgnoreCase) Then
                ProcessPortableCustomInteraction(categoryDirectory, resourceFile, resources, resourceDependencies, isPreview, parsedTemplate, resourceNameOrg, _packageCreator.RelativePathToResources)
            ElseIf mimeType = "application/vnd.geogebra.file" Then
                ProcessGeogebraResources(categoryDirectory, resourceFile, resources, resourceDependencies, isPreview, parsedTemplate, resourceNameOrg, _packageCreator.RelativePathToResources)
            Else
                Dim resourceDependenciesNested As New List(Of String)
                Dim newReference As String = String.Concat(_FolderDirectory(categoryDirectory), "/", resourceName)

                If Not resourceAlreadyProcessed Then
                    If MimeTypeCanHaveDependencies(mimeType) Then
                        ProcessDependentResources(resourceFile, resourceContent, resourceName, mimeType, resources, resourceMimeTypeDictionary, isPreview, resourceDependenciesNested, eventArgs, itemCode)
                    End If

                    Dim resourcePath As String = Path.Combine(_packageCreator.TempWorkingDirectory.FullName, newReference)
                    ChainHandlerHelper.SaveFile(resourceFile, resourcePath)

                    If Not isPreview Then
                        AddResourcesToManifest(resourceName, eventArgs.ResourceName, categoryDirectory, mimeType, resources, resourceDependenciesNested)
                    End If
                End If

                ChainHandlerHelper.UpdateReferenceInTemplate(parsedTemplate, resourceNameOrg, $"{_packageCreator.RelativePathToResources}{newReference}".Replace("\", "/"))
                resourceDependencies.Add(ChainHandlerHelper.GetIdentifierFromResourceId(resourceName, PackageCreatorConstants.TypeOfResource.resource))
            End If
        End Sub

        Private Function ResourceNameAlreadyProcessed(resourceName As String, resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String)))) As Boolean
            For Each identifier As String In ChainHandlerHelper.GetPossibleIdentifiersForResourceName(resourceName, GetStylesheetHelper())
                If resources.ContainsKey(identifier) Then Return True
            Next
            Return False
        End Function

        Protected Overridable Sub AudioAdded(resourceName As String, itemCode As String)
        End Sub

        Private Sub AddResourcesToManifest(resourceName As String, resourceNameUsedToReadResource As String, categoryDirectory As PackageCreatorConstants.FileDirectoryType, mimeType As String, ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), ByRef resourceDependenciesNested As List(Of String))
            Dim resourceMetaDataCollection As MetaDataCollection = _packageCreator.ResourceMan.GetResourceMetaData(resourceNameUsedToReadResource)
            Dim resourceType = GetResourceType(resourceMetaDataCollection, resourceName)
            Dim newReference As String = String.Concat(_FolderDirectory(categoryDirectory), "/", resourceName)
            Dim manifestResourceType = GetManifestResourceType(categoryDirectory, mimeType)
            resourceType.type = _ResourceTypes(manifestResourceType)
            resourceType.href = newReference
            Dim file As New FileType
            file.href = newReference
            Dim fileArray(0) As FileType
            fileArray(0) = file
            resourceType.file = fileArray
            PackageCreator.AddResourceToManifest(resources, resourceType)

            If (resourceDependenciesNested.Count > 0) Then
                PackageCreator.AddDependentResourceToManifest(resources, resourceType.identifier, resourceDependenciesNested)
            End If
        End Sub

        Private Sub ProcessDependentResources(ByRef resourceFile As Byte(),
                                             ByRef resourceContent As String,
                                             ByRef resourceName As String,
                                             ByRef mimeType As String,
                                             ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                             ByRef resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String),
                                             isPreview As Boolean,
                                             ByRef resourceDependenciesNested As List(Of String),
                                             ByRef eventArgs As ResourceNeededEventArgs,
                                             itemCode As String)

            Dim contentShouldChange As Boolean = GetHtmlHelper.MimeTypeCheckForIncompleteHtml(mimeType)
            If contentShouldChange Then resourceContent = GetHtmlHelper.CheckIncompleteHtml(resourceContent)

            Dim matchedResourcesNested As MatchCollection = ChainHandlerHelper.GetAllResourcesFromTemplate(resourceContent)
            If (matchedResourcesNested.Count > 0) Then
                resourceDependenciesNested = ProcessResources(matchedResourcesNested, resourceContent, resources, resourceMimeTypeDictionary, isPreview, itemCode)
                If Not contentShouldChange Then contentShouldChange = resourceDependenciesNested.Count > 0
            End If

            If contentShouldChange Then
                eventArgs.BinaryResource = New BinaryResource(Encoding.UTF8.GetBytes(resourceContent))
                resourceFile = CType(eventArgs.BinaryResource.ResourceObject, Byte())
            End If
        End Sub

        Private Sub ProcessSourceText(ByRef sourceTextFile As Byte(),
                                      ByRef sourceTextContent As String,
                                      ByRef sourceTextName As String,
                                      ByRef mimeType As String,
                                      ByRef eventArgs As ResourceNeededEventArgs,
                                      ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))))
            sourceTextContent = Encoding.UTF8.GetString(sourceTextFile)
            Dim name = Path.GetFileNameWithoutExtension(sourceTextName)
            StripTablerowStylesFromSourceTexts(sourceTextContent)
            ReplaceLanguageStylesForSourceText(sourceTextContent, sourceTextFile, eventArgs)
            ProcessTextToSpeechForSourceText(sourceTextContent, sourceTextFile, eventArgs)
            ConvertSourceTextToAssessmentStimulus(name, sourceTextContent, sourceTextFile, eventArgs, resources)
            mimeType = "text/xml"
            sourceTextName = String.Concat(name, ".", "xml")
        End Sub

        Private Sub ReplaceLanguageStylesForSourceText(ByRef sourceTextContent As String,
                                                       ByRef sourceTextFile As Byte(),
                                                       ByRef eventArgs As ResourceNeededEventArgs)
            If ReplaceLanguageStyles(sourceTextContent) Then
                eventArgs.BinaryResource = New BinaryResource(Encoding.UTF8.GetBytes(sourceTextContent))
                sourceTextFile = CType(eventArgs.BinaryResource.ResourceObject, Byte())
            End If
        End Sub

        Private Sub ConvertSourceTextToAssessmentStimulus(ByRef sourceTextName As String,
                                                          ByRef sourceTextContent As String,
                                                          ByRef sourceTextFile As Byte(),
                                                          ByRef eventArgs As ResourceNeededEventArgs,
                                                          ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))))
            Dim stimulus = New AssessmentStimulusType() With {
                .identifier = ChainHandlerHelper.GetIdentifierFromResourceId($"{sourceTextName}.xml", PackageCreatorConstants.TypeOfResource.resource),
                .title = sourceTextName,
                .qtistylesheet = CreateSourceTextStylesheet(sourceTextName, sourceTextContent, resources, eventArgs),
                .qtistimulusbody = New StimulusBodyType() With {
                    .Items = GetSourceTextAsXmlNodes(sourceTextContent)
                }
            }
            sourceTextContent = ChainHandlerHelper.ObjectToString(stimulus, Nothing, True)
            sourceTextContent = UpdateSourceTextNamespaces(sourceTextContent)
            eventArgs.BinaryResource = New BinaryResource(Encoding.UTF8.GetBytes(sourceTextContent))
            sourceTextFile = CType(eventArgs.BinaryResource.ResourceObject, Byte())
        End Sub

        Private Function CreateSourceTextStylesheet(ByRef sourceTextName As String,
                                                    ByRef sourceTextContent As String,
                                                    ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                                    ByRef eventArgs As ResourceNeededEventArgs) As List(Of StyleSheetType)
            Dim result = New List(Of StyleSheetType)

            Dim dependentStylesheets = ProcessSourceTextStylesheets(resources, eventArgs)
            If dependentStylesheets.Any() Then
                dependentStylesheets.ForEach(Sub(stylesheetType)
                                                 result.Add(SetHrefPropertyOfStylesheetType(stylesheetType))
                                             End Sub)

            End If

            Dim inlineStylesAsCss = String.Empty
            sourceTextContent = ConvertSourceTextInlineStylesToCss(sourceTextContent, inlineStylesAsCss)
            If String.IsNullOrEmpty(inlineStylesAsCss.Trim()) Then
                Return result
            End If

            Dim generatedCssName = SaveGeneratedSourceTextCss(inlineStylesAsCss, sourceTextName, resources)
            If String.IsNullOrEmpty(generatedCssName) Then
                Return result
            End If

            Dim generatedStyleSheetType = GetStylesheetHelper.GetStylesheetType(generatedCssName, _packageCreator.RelativePathToResources)
            result.Add(SetHrefPropertyOfStylesheetType(generatedStyleSheetType))
            Return result
        End Function

        Private Function SetHrefPropertyOfStylesheetType(styleSheetType As StyleSheetType) As StyleSheetType
            styleSheetType.href = $"resource://package/{Path.GetFileName(styleSheetType.href)}"
            Return styleSheetType
        End Function

        Private Function ProcessSourceTextStylesheets(ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                                      ByRef eventArgs As ResourceNeededEventArgs) As List(Of StyleSheetType)
            Dim stylesheetTypes = New List(Of StyleSheetType)

            Dim dependentResourceCollection As DependentResourceCollection = _packageCreator.ResourceMan.GetDependentResourcesForResource(eventArgs.ResourceName)
            For Each dependentResource As DependentResource In dependentResourceCollection
                Dim dependentStylesheet = ProcessSourceTextStylesheet(dependentResource, resources)
                If dependentStylesheet IsNot Nothing Then
                    stylesheetTypes.Add(dependentStylesheet)
                End If
            Next

            Return stylesheetTypes
        End Function

        Private Function ProcessSourceTextStylesheet(dependentResource As DependentResource,
                                                    ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String)))) As StyleSheetType
            If ChainHandlerHelper.GetCategoryFolderFromFileName(dependentResource.Name) <> PackageCreatorConstants.FileDirectoryType.css Then
                Return Nothing
            End If

            Dim cssName As String = GetStylesheetHelper.GetSourceTextStylesheetName(dependentResource.Name)
            Dim newReference As String = String.Concat(_FolderDirectory(PackageCreatorConstants.FileDirectoryType.css), "/", cssName)
            SaveSourceTextStylesheet(newReference, dependentResource)
            AddSourceTextStylesheetToManifest(cssName, newReference, resources)
            Return GetStylesheetHelper.GetStylesheetType(cssName, _packageCreator.RelativePathToResources)
        End Function

        Private Sub SaveSourceTextStylesheet(newReference As String,
                                             dependentResource As DependentResource)
            Dim resourcePath As String = Path.Combine(_packageCreator.TempWorkingDirectory.FullName, newReference)

            Dim cssEventArgs As New ResourceNeededEventArgs(dependentResource.Name, AddressOf StreamConverters.ConvertStreamToByteArray)
            _packageCreator.ResourceNeeded(Nothing, cssEventArgs)
            If cssEventArgs.BinaryResource IsNot Nothing AndAlso cssEventArgs.BinaryResource.ResourceObject IsNot Nothing Then
                Dim cssContent As String = Encoding.UTF8.GetString(CType(cssEventArgs.BinaryResource.ResourceObject, Byte()))

                cssContent = GetStylesheetHelper.PrefixSourceTextStyles(cssContent)

                cssEventArgs.BinaryResource = New BinaryResource(Encoding.UTF8.GetBytes(cssContent))
                ChainHandlerHelper.SaveFile(CType(cssEventArgs.BinaryResource.ResourceObject, Byte()), resourcePath)
            End If
        End Sub

        Private Sub AddSourceTextStylesheetToManifest(cssName As String,
                                                      newReference As String,
                                                      ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))))
            Dim resourceType = GetResourceType(Nothing, cssName)
            resourceType.type = _ResourceTypes(PackageCreator.QTIManifestResourceType.webcontent)
            resourceType.href = newReference
            resourceType.file = {New FileType With {.href = newReference}}
            PackageCreator.AddResourceToManifest(resources, resourceType)
        End Sub

        Private Function ConvertSourceTextInlineStylesToCss(sourceTextContent As String, ByRef css As String) As String
            Dim tempDoc = New XmlDocument()
            tempDoc.LoadXml($"<wrapper>{sourceTextContent}</wrapper>")
            Using converter = New QTI30XhtmlConverter()
                converter.ConvertStylesToCss(tempDoc, css)
            End Using
            Return tempDoc.DocumentElement.InnerXml
        End Function

        Private Function SaveGeneratedSourceTextCss(ByRef css As String,
                                               ByRef sourceTextName As String,
                                               ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String)))) As String
            If String.IsNullOrEmpty(css.Trim()) Then
                Return String.Empty
            End If

            Dim helper = GetStylesheetHelper()
            GetStylesheetHelper.PrefixGeneratedStyles(css)
            Dim cssStyleFileName As String = String.Concat(Path.GetFileNameWithoutExtension(PackageCreatorConstants.GENERATED_CSS), "_", sourceTextName.Replace(" ", String.Empty), Path.GetExtension(PackageCreatorConstants.GENERATED_CSS))
            _packageCreator.SaveGeneratedCss(resources, css, cssStyleFileName, Me.FolderDirectory(PackageCreatorConstants.FileDirectoryType.css), Me.ResourceTypes(PackageCreator.QTIManifestResourceType.webcontent))

            Return cssStyleFileName
        End Function

        Private Function GetSourceTextAsXmlNodes(ByRef sourceTextContent As String) As Object()
            Dim tempDoc = New XmlDocument()
            tempDoc.LoadXml($"<wrapper>{sourceTextContent}</wrapper>")
            Dim items = New List(Of XmlNode)
            For Each node As XmlElement In tempDoc.DocumentElement.ChildNodes
                node.SetAttribute("xmlns", _namespaceHelper.GetImsQtiNamespace().NamespaceName)
                items.Add(node)
            Next
            Return items.ToArray()
        End Function

        Private Function UpdateSourceTextNamespaces(sourceTextContent As String) As String
            Dim tempDoc = New XmlDocument()
            tempDoc.LoadXml(sourceTextContent)
            _namespaceHelper.UpdateNameSpaces(tempDoc, True)
            Return tempDoc.DocumentElement.OuterXml
        End Function

        Private Sub ProcessTextToSpeechForSourceText(ByRef sourceTextContent As String,
                                                     ByRef sourceTextFile As Byte(),
                                                     ByRef eventArgs As ResourceNeededEventArgs)
            Dim xmlDoc As New XmlDocument With {.PreserveWhitespace = True}
            xmlDoc.LoadXml($"<wrapper>{sourceTextContent}</wrapper>")

            If xmlDoc.SelectNodes("//*[contains(@class, 'TTS')]").Count > 0 Then
                sourceTextContent = ProcessTextToSpeech(xmlDoc)
                eventArgs.BinaryResource = New BinaryResource(Encoding.UTF8.GetBytes(sourceTextContent))
                sourceTextFile = CType(eventArgs.BinaryResource.ResourceObject, Byte())
            End If
        End Sub

        Private Sub ProcessStylesheet(ByRef resourceFile As Byte(), ByRef resourceContent As String, ByRef resourceName As String, ByRef mimeType As String, eventArgs As ResourceNeededEventArgs)
            resourceContent = Encoding.UTF8.GetString(resourceFile)

            If Not String.IsNullOrWhiteSpace(_packageCreator.AdditionalItemCssStyles) Then
                resourceContent = String.Concat(_packageCreator.AdditionalItemCssStyles, vbNewLine, resourceContent)
            End If

            GetStylesheetHelper.StripUnwantedPrefixesFromStylesheet(resourceContent)

            eventArgs.BinaryResource = New BinaryResource(Encoding.UTF8.GetBytes(resourceContent))
            resourceFile = CType(eventArgs.BinaryResource.ResourceObject, Byte())
            resourceName = GetStylesheetHelper.PrefixStylesheet(resourceName)
        End Sub

        Private Sub ProcessPlaceholders(match As Match, ByRef resourceFile As Byte(), ByRef resourceName As String, ByRef resourceNameOrg As String)
            If ProcessGapMatchImagePlaceholder(match, resourceFile) Then
                resourceName = $"IMG_{Guid.NewGuid.ToString()}.png"
                resourceNameOrg = ResourceNameHelper.ResourcenameWithIllegalCharacters(resourceNameOrg)
                Exit Sub
            End If
            If ProcessGapMatchMathMLPlaceholder(match, resourceFile) Then Exit Sub
            If ProcessNotSupportedPlaceholder(match, resourceFile) Then Exit Sub
        End Sub

        Private Function ProcessGapMatchImagePlaceholder(match As Match, ByRef resourceFile As Byte()) As Boolean
            Dim regexResult As Match = Nothing
            If RegexpHelper.TryMatchGapMatchImage(Uri.UnescapeDataString(match.ToString), regexResult) Then
                resourceFile = CreateGapMatchResourceFile(regexResult)
                Return True
            End If
            Return False
        End Function

        Private Function CreateGapMatchResourceFile(regexResult As Match) As Byte()
            Dim resourceFile As Byte()

            Dim width As Integer = CType(regexResult.Groups(RegexpHelper.WIDTH).Value, Integer)
            Dim height As Integer = CType(regexResult.Groups(RegexpHelper.HEIGHT).Value, Integer)
            Dim perctransparency As Integer = CType(regexResult.Groups(RegexpHelper.PERCTRANSPARENCY).Value, Integer)
            Dim text As String = Uri.UnescapeDataString(regexResult.Groups(RegexpHelper.TEXT).Value)
            Dim gapMatchImageGenerator As New GapMatchImageGenerator
            resourceFile = gapMatchImageGenerator.CreateImage(width, height, text, perctransparency)
            Return resourceFile
        End Function

        Private Function ProcessGapMatchMathMLPlaceholder(match As Match, ByRef resourceFile As Byte()) As Boolean
            Dim regexResult As Match = Nothing
            If RegexpHelper.TryMatchGapMatchMathMLImage(Uri.UnescapeDataString(match.ToString), regexResult) Then
                resourceFile = CreateGapMatchMathMLResourceFile(regexResult)
                Return True
            End If
            Return False
        End Function

        Private Function CreateGapMatchMathMLResourceFile(regexResult As Match) As Byte()
            Dim resourceFile As Byte()

            Dim width As Integer = CType(regexResult.Groups(RegexpHelper.WIDTH).Value, Integer)
            Dim height As Integer = CType(regexResult.Groups(RegexpHelper.HEIGHT).Value, Integer)
            Dim perctransparency As Integer = CType(regexResult.Groups(RegexpHelper.PERCTRANSPARENCY).Value, Integer)
            Dim mathMLResource As String = regexResult.Groups(RegexpHelper.RESOURCENAME).Value
            Dim eventArgs As New ResourceNeededEventArgs(mathMLResource, AddressOf StreamConverters.ConvertStreamToByteArray)
            _packageCreator.ResourceNeeded(Nothing, eventArgs)
            If eventArgs.BinaryResource Is Nothing OrElse eventArgs.BinaryResource.ResourceObject Is Nothing Then
                Throw New Exception($"Formula resource {mathMLResource} cannot be found in the package!")
            End If
            Dim gapMatchImageGenerator As New GapMatchImageGenerator
            resourceFile = gapMatchImageGenerator.CreateTransparentImageFromMathMLImage(width, height, CType(eventArgs.BinaryResource.ResourceObject, Byte()), perctransparency)
            Return resourceFile
        End Function

        Private Function ProcessNotSupportedPlaceholder(match As Match, ByRef resourceFile As Byte()) As Boolean
            Dim regexResult As Match = Nothing
            If RegexpHelper.TryMatchNotSupportedPlaceholder(Uri.UnescapeDataString(match.ToString), regexResult) Then
                Dim width As Integer = CType(regexResult.Groups(RegexpHelper.WIDTH).Value, Integer)
                Dim height As Integer = CType(regexResult.Groups(RegexpHelper.HEIGHT).Value, Integer)
                Dim text As String = Uri.UnescapeDataString(regexResult.Groups(RegexpHelper.TEXT).Value)
                Dim notSupportedPlaceholderGenerator As New NotSupportedPlaceholderGenerator
                resourceFile = notSupportedPlaceholderGenerator.CreateImage(width, height, text)
                Return True
            End If
            Return False
        End Function

        Private Function ReadResource(ByRef resourceNameOrg As String, ByRef resourceName As String, resourceNameUsedToReadResource As String, match As Match) As Byte()
            Dim eventArgs As New ResourceNeededEventArgs(resourceNameUsedToReadResource, AddressOf StreamConverters.ConvertStreamToByteArray)
            Dim resourceFile As Byte() = Nothing

            _packageCreator.ResourceNeeded(Nothing, eventArgs)

            If eventArgs.BinaryResource Is Nothing OrElse eventArgs.BinaryResource.ResourceObject Is Nothing Then
                If match IsNot Nothing Then
                    Dim regexResult As Match = Nothing
                    Dim isGapMatchImage As Boolean = RegexpHelper.TryMatchGapMatchImage(Uri.UnescapeDataString(match.ToString()), regexResult)

                    If isGapMatchImage Then
                        resourceFile = CreateGapMatchResourceFile(regexResult)

                        resourceNameOrg = ResourceNameHelper.ResourcenameWithIllegalCharacters(resourceNameOrg)
                        resourceName = ResourceNameHelper.RemoveIllegalCharactersFromResourcename(resourceName)
                    Else
                        regexResult = Nothing
                        If RegexpHelper.TryMatchGapMatchMathMLImage(Uri.UnescapeDataString(match.ToString()), regexResult) Then
                            resourceFile = CreateGapMatchMathMLResourceFile(regexResult)

                        ElseIf RegexpHelper.TryMatchGapMatchImage(Uri.UnescapeDataString(match.ToString()), regexResult) Then
                            resourceFile = CreateGapMatchResourceFile(regexResult)
                        End If
                    End If
                End If
            Else
                resourceFile = CType(eventArgs.BinaryResource.ResourceObject, Byte())
            End If

            Return resourceFile
        End Function

        Public Function GetResourceByName(name As String) As Byte()
            Dim eventArgs As New ResourceNeededEventArgs(name, AddressOf StreamConverters.ConvertStreamToByteArray)

            _packageCreator.ResourceNeeded(Nothing, eventArgs)

            If eventArgs.BinaryResource IsNot Nothing AndAlso eventArgs.BinaryResource.ResourceObject IsNot Nothing Then
                Dim returnValue = TryCast(eventArgs.BinaryResource.ResourceObject, Byte())
                Return returnValue
            End If
            Return Nothing
        End Function

        Private Function SaveResource(
                                      resourceNameOrg As String,
                                      resourceName As String,
                                      resourceNameUsedToReadResource As String,
                                      resourceFile As Byte(),
                                      ByRef parsedTemplate As String,
                                      ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                      resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String),
                                      isPreview As Boolean,
                                      itemCode As String) As List(Of String)

            Dim resourceDependencies As New List(Of String)

            Dim rf = resourceFile

            Dim mimeType As String = String.Empty
            Dim resourceContent As String = String.Empty
            Dim resName As String = ResourceNameHelper.GetQtiCompliantResourceName(resourceName, True)

            If rf IsNot Nothing AndAlso IsSourceTextFile(rf, resName) Then
                resourceContent = Encoding.UTF8.GetString(rf)
                Dim extension As String = "xml"
                mimeType = "text/xml"
                If Not String.IsNullOrEmpty(extension) Then resName = String.Concat(Path.GetFileNameWithoutExtension(resName), ".", extension)
            End If

            Dim categoryDirectory = ChainHandlerHelper.GetCategoryFolderFromFileName(resName)
            If String.IsNullOrEmpty(mimeType) Then
                mimeType = ChainHandlerHelper.GetMimeTypeFromFile(rf, resName)
            End If
            mimeType = ChainHandlerHelper.ConvertMimeType(mimeType, resName)
            If Not resourceMimeTypeDictionary.ContainsKey(resName) Then
                resourceMimeTypeDictionary.TryAdd(resName, mimeType)
            End If

            Select Case categoryDirectory
                Case PackageCreatorConstants.FileDirectoryType.css
                    resName = GetStylesheetHelper.PrefixStylesheet(resName)
                Case PackageCreatorConstants.FileDirectoryType.other
                    categoryDirectory = ChainHandlerHelper.GetCategoryFolderFromFile(mimeType, resName)
            End Select

            If mimeType = "application/x-customInteraction" Then
                ProcessCustomInteractions(categoryDirectory, rf, resources, resourceDependencies, isPreview, parsedTemplate, resourceNameOrg, _packageCreator.RelativePathToResources)
            ElseIf mimeType.Equals("application/x-portableCustomInteraction", StringComparison.InvariantCultureIgnoreCase) Then
                ProcessPortableCustomInteraction(categoryDirectory, resourceFile, resources, resourceDependencies, isPreview, parsedTemplate, resourceNameOrg, _packageCreator.RelativePathToResources)
            ElseIf mimeType = "application/vnd.geogebra.file" Then
                ProcessGeogebraResources(categoryDirectory, rf, resources, resourceDependencies, isPreview, parsedTemplate, resourceNameOrg, _packageCreator.RelativePathToResources)
            Else
                Dim resourceDependenciesNested As New List(Of String)

                If MimeTypeCanHaveDependencies(mimeType) Then
                    Dim contentShouldChange As Boolean = GetHtmlHelper.MimeTypeCheckForIncompleteHtml(mimeType)
                    If contentShouldChange Then
                        resourceContent = GetHtmlHelper.CheckIncompleteHtml(resourceContent)
                        If Not resourceMimeTypeDictionary.ContainsKey(resName) Then
                            resourceMimeTypeDictionary.TryAdd(resName, mimeType)
                        End If
                    End If
                    Dim matchedResourcesNested As MatchCollection = ChainHandlerHelper.GetAllResourcesFromTemplate(resourceContent)
                    If (matchedResourcesNested.Count > 0) Then
                        resourceDependenciesNested = ProcessResources(matchedResourcesNested, resourceContent, resources, resourceMimeTypeDictionary, isPreview, itemCode)
                        If Not contentShouldChange Then contentShouldChange = resourceDependenciesNested.Count > 0

                    End If
                    If contentShouldChange Then
                        rf = Encoding.UTF8.GetBytes(resourceContent)
                    End If
                End If

                Dim newReference = String.Concat(_FolderDirectory(categoryDirectory), "/", resName)
                Dim resourcePath = Path.Combine(_packageCreator.TempWorkingDirectory.FullName, newReference)
                ChainHandlerHelper.SaveFile(rf, resourcePath)

                If parsedTemplate IsNot Nothing Then
                    If IsImageFromMemory(resourceNameOrg) Then
                        ChainHandlerHelper.UpdateBase64ToFileReference(parsedTemplate, resourceNameOrg, newReference)
                    Else
                        ChainHandlerHelper.UpdateReferenceInTemplate(parsedTemplate, resourceNameOrg, $"{_packageCreator.RelativePathToResources}{newReference}".Replace("\", "/"))
                    End If
                End If

                If Not isPreview Then
                    AddResourcesToManifest(resName, resourceNameUsedToReadResource, categoryDirectory, mimeType, resources, resourceDependenciesNested)
                End If

                resourceDependencies.Add(ChainHandlerHelper.GetIdentifierFromResourceId(resName, PackageCreatorConstants.TypeOfResource.resource))
            End If

            Return resourceDependencies
        End Function

        Protected Overridable Function GetResourceType(metaData As MetaDataCollection, resourceName As String) As ResourceType
            Return New ResourceType With {.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(resourceName, PackageCreatorConstants.TypeOfResource.resource)}
        End Function

        Protected Overridable Function GetManifestResourceType(categoryDirectory As PackageCreatorConstants.FileDirectoryType, Optional mimeType As String = "") As PackageCreator.QTIManifestResourceType
            Return PackageCreator.QTIManifestResourceType.webcontent
        End Function

        Protected Overridable Function GetStylesheetHelper() As QTI30StyleSheetHelper
            If _stylesheetHelper Is Nothing Then _stylesheetHelper = New QTI30StyleSheetHelper
            Return _stylesheetHelper
        End Function

        Protected Overridable Function GetHtmlHelper() As HtmlHelper
            If _htmlHelper Is Nothing Then _htmlHelper = New HtmlHelper
            Return _htmlHelper
        End Function

        Public Sub ProcessPortableCustomInteraction(categoryDirectory As PackageCreatorConstants.FileDirectoryType,
                                                    resourceFile As Byte(),
                                                    ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                                    ByRef resourceDependencies As List(Of String),
                                                    isPreview As Boolean,
                                                    ByRef parsedTemplate As String,
                                                    resourceNameOrg As String,
                                                    relativePathToResources As String)

            Dim resourceName As String = Uri.UnescapeDataString(resourceNameOrg).Replace(" "c, "_"c)
            Dim resourcePath As String = Path.Combine(_packageCreator.TempWorkingDirectory.FullName, resourceName)
            Dim tempPackageDir = Path.GetDirectoryName(resourcePath)

            DetermineTempDirectoryForCi(resourcePath, resourceName)

            If Not File.Exists(resourcePath) Then ChainHandlerHelper.SaveFile(resourceFile, resourcePath)
            Dim pciValidator = New PciPackageValidator(resourcePath)

            Dim subFolderForPciName As String = Path.GetFileNameWithoutExtension(resourceName)

            Dim pciAlreadyInPackage As Boolean = Directory.Exists(Path.Combine(tempPackageDir, _FolderDirectory(categoryDirectory), subFolderForPciName))

            Dim metadata = pciValidator.GetMetaData()
            ExtendItemWithPciMetadata(parsedTemplate, metadata, resourceNameOrg, subFolderForPciName)

            Using pciPackageReader As New StreamReader(New FileStream(resourcePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                Using pciArchive As New ZipArchive(pciPackageReader.BaseStream)

                    Dim dependenciesFromPackage As New List(Of String)

                    If Not pciAlreadyInPackage Then
                        ExtractPciToTempPackageDir(pciArchive, tempPackageDir, subFolderForPciName, dependenciesFromPackage)
                    End If

                    AddDependenciesToPackage(pciArchive, resources, dependenciesFromPackage, resourceDependencies, subFolderForPciName, pciAlreadyInPackage)
                End Using
            End Using
        End Sub

        Private Sub ExtractPciToTempPackageDir(archive As ZipArchive, tempPackageDir As String, subFolderForPciName As String, ByRef dependencies As List(Of String))
            Dim targetDir = Path.Combine(tempPackageDir, "pci", subFolderForPciName)

            For Each entry In archive.Entries.Where(Function(s) Not String.IsNullOrEmpty(s.Name))
                If (entry.Name.Equals("metadata.json")) Then Continue For

                Dim relativeTargetName As String = entry.FullName
                If relativeTargetName.StartsWith("ref/") Then
                    relativeTargetName = relativeTargetName.Substring(4)
                End If

                Dim targetFullPath As String = Path.Combine(targetDir, relativeTargetName)
                Dim pciTargetDir As String = Path.GetDirectoryName(targetFullPath)

                If Not Directory.Exists(pciTargetDir) Then
                    Directory.CreateDirectory(pciTargetDir)
                End If

                entry.ExtractToFile(targetFullPath)
                dependencies.Add($"pci/{subFolderForPciName}/{relativeTargetName}")
            Next

            Dim di = New DirectoryInfo(targetDir)

            Dim metadataFile = di.GetFiles("metadata.json", SearchOption.TopDirectoryOnly)
            metadataFile?.ToList().ForEach(Sub(mdf As FileInfo) mdf.Delete())
        End Sub

        Private Sub AddDependenciesToPackage(archive As ZipArchive, ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), dependencies As List(Of String), ByRef resourceDependencies As List(Of String), subFolderForPciName As String, pciAlreadyInPackage As Boolean)
            For Each reference In dependencies
                If Not pciAlreadyInPackage Then
                    Dim resourceType = CreateResourceType(reference, subFolderForPciName)
                    resourceType.type = ResourceTypeType.associatedcontentlearningapplicationresource
                    PackageCreator.AddResourceToManifest(resources, resourceType)
                End If

                resourceDependencies.Add(ChainHandlerHelper.GetIdentifierFromResourceId($"{subFolderForPciName}_{Path.GetFileName(reference)}", PackageCreatorConstants.TypeOfResource.resource))
            Next
        End Sub

        Private Sub ExtendItemWithPciMetadata(ByRef parsedTemplate As String, ByVal metadata As MetadataRoot, ByVal resourceName As String, ByVal resourceSubfolderName As String)
            Dim xDoc = XDocument.Parse($"<wrapper>{parsedTemplate}</wrapper>")

            Dim pciElements = xDoc.XPathSelectElements($"//qti-portable-custom-interaction[contains(@data, '/{resourceName}')]").ToList()

            For Each pciElement As XElement In pciElements
                pciElement.Add(New XAttribute("custom-interaction-type-identifier", metadata.TypeIdentifier))
                Dim modulesElement = pciElement.XPathSelectElement("./qti-interaction-modules")
                modulesElement.Add(New XAttribute("primary-configuration", $"pci/{resourceSubfolderName}/modules/module_resolution.js"))
                modulesElement.Add(New XAttribute("secondary-configuration", $"pci/{resourceSubfolderName}/modules/fallback_module_resolution.js"))

                For Each m In metadata.Modules
                    Dim primaryPath = $"pci/{resourceSubfolderName}/{m.PrimaryPath}"
                    modulesElement.Add(New XElement("qti-interaction-module", New XAttribute("id", m.Id), New XAttribute("primary-path", primaryPath)))
                Next

                pciElement.Attributes("data").Remove()

                pciElement.Add(New XElement("qti-interaction-markup"))
            Next

            parsedTemplate = xDoc.Document.Root.InnerXml()
        End Sub

        Public Sub ProcessCustomInteractions(
                                             categoryDirectory As PackageCreatorConstants.FileDirectoryType,
                                             resourceFile As Byte(),
                                             ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                             ByRef resourceDependencies As List(Of String),
                                             isPreview As Boolean,
                                             ByRef parsedTemplate As String,
                                             resourceNameOrg As String,
                                             relativePathToResources As String)

            Dim resourceName As String = Uri.UnescapeDataString(resourceNameOrg).Replace(Chr(32), "_"c)
            Dim resourcePath As String = Path.Combine(_packageCreator.TempWorkingDirectory.FullName, resourceName)
            Dim tempPackageDir = Path.GetDirectoryName(resourcePath)

            DetermineTempDirectoryForCi(resourcePath, resourceName)
            If Not File.Exists(resourcePath) Then ChainHandlerHelper.SaveFile(resourceFile, resourcePath)

            Dim subFolderForCiName As String = Path.GetFileNameWithoutExtension(resourceName)

            Dim ciAlreadyInPackage As Boolean = Directory.Exists(Path.Combine(tempPackageDir, _FolderDirectory(categoryDirectory), subFolderForCiName))

            Using ciPackageReader As New StreamReader(New FileStream(resourcePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                Dim jsonManifestPath As String = $"{_FolderDirectory(categoryDirectory)}/json"
                Dim archive = New ZipArchive(ciPackageReader.BaseStream)
                Dim jsonEntries = archive.Entries.Where(Function(e) e.FullName.StartsWith(jsonManifestPath + "/") AndAlso Not String.IsNullOrEmpty(e.Name))

                If jsonEntries.Any() Then
                    If jsonEntries.Count = 1 Then
                        Dim jsonManifestReferences As New List(Of String)
                        Dim manifestEntry = jsonEntries.Single()

                        If Not ciAlreadyInPackage Then
                            Using jsonFileReader As StreamReader = New StreamReader(manifestEntry.Open())
                                Dim jsonFileContents As String = jsonFileReader.ReadToEnd()
                                ChainHandlerHelper.SaveFile(PublicationRegExHelper.UpdateReferencesInJsonManifest(jsonFileContents, subFolderForCiName), Path.Combine(tempPackageDir, PublicationRegExHelper.AddCiNameToReference(manifestEntry.FullName, subFolderForCiName)))
                                jsonManifestReferences = PublicationRegExHelper.GetReferencesFromJsonManifest(jsonFileContents)

                                If Not isPreview Then
                                    PackageCreator.AddResourceToManifest(resources, CreateResourceType(PublicationRegExHelper.AddCiNameToReference(manifestEntry.FullName.Replace("\", "/").TrimStart("/"c), subFolderForCiName), subFolderForCiName))
                                End If
                            End Using
                        Else
                            Using jsonFileReader As StreamReader = New StreamReader(manifestEntry.Open())
                                Dim jsonFileContents As String = jsonFileReader.ReadToEnd()
                                jsonManifestReferences = PublicationRegExHelper.GetReferencesFromJsonManifest(jsonFileContents)
                            End Using
                        End If

                        resourceDependencies.Add(ChainHandlerHelper.GetIdentifierFromResourceId(String.Concat(subFolderForCiName, "_", manifestEntry.Name), PackageCreatorConstants.TypeOfResource.resource))
                        ChainHandlerHelper.UpdateReferenceInTemplate(parsedTemplate, resourceNameOrg,
                                                                     $"{relativePathToResources}{_FolderDirectory(categoryDirectory)}/{subFolderForCiName}/json/{manifestEntry.Name}".Replace("\", "/"))

                        For Each referencedFileName As String In jsonManifestReferences
                            If Not ciAlreadyInPackage Then
                                Dim referencedFile = archive.GetEntry(referencedFileName)
                                If referencedFile IsNot Nothing Then
                                    Using referencedFileStream As Stream = referencedFile.Open()
                                        ChainHandlerHelper.SaveFile(ConvertStreamToByteArray(referencedFileStream), Path.Combine(tempPackageDir, PublicationRegExHelper.AddCiNameToReference(referencedFileName.TrimStart("/"c), subFolderForCiName)))
                                    End Using
                                    If Not isPreview Then
                                        PackageCreator.AddResourceToManifest(resources, CreateResourceType(PublicationRegExHelper.AddCiNameToReference(referencedFileName, subFolderForCiName), subFolderForCiName))
                                    End If

                                    resourceDependencies.Add(ChainHandlerHelper.GetIdentifierFromResourceId(String.Concat(subFolderForCiName, "_", Path.GetFileName(referencedFileName)), PackageCreatorConstants.TypeOfResource.resource))
                                Else
                                    Throw New ArgumentException(String.Format(My.Resources.ReferencedFileFromJsonManifestNotFound, referencedFileName))
                                End If
                            Else
                                resourceDependencies.Add(ChainHandlerHelper.GetIdentifierFromResourceId(String.Concat(subFolderForCiName, "_", Path.GetFileName(referencedFileName)), PackageCreatorConstants.TypeOfResource.resource))
                            End If
                        Next
                    Else
                        Throw New ArgumentException(String.Format(My.Resources.MultipleJsonManifest, jsonManifestPath))
                    End If
                Else
                    Throw New ArgumentException(String.Format(My.Resources.MissingJsonManifest, jsonManifestPath))
                End If
            End Using
        End Sub

        Public Sub ProcessGeogebraResources(
                                            categoryDirectory As PackageCreatorConstants.FileDirectoryType,
                                            resourceFile As Byte(),
                                            ByRef resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))),
                                            ByRef resourceDependencies As List(Of String),
                                            isPreview As Boolean,
                                            ByRef parsedTemplate As String,
                                            resourceNameOrg As String,
                                            relativePathToResources As String)

            Const ggbXmlFile As String = "geogebra.xml"
            Dim resourceName As String = Uri.UnescapeDataString(resourceNameOrg).Replace(Chr(32), "_"c)
            Dim resourcePath As String = Path.Combine(_packageCreator.TempWorkingDirectory.FullName, String.Concat(_FolderDirectory(categoryDirectory), "/", resourceName))

            Dim ggbAlreadyInPackage As Boolean = File.Exists(resourcePath)

            If Not ggbAlreadyInPackage Then
                ChainHandlerHelper.SaveFile(resourceFile, resourcePath)

                Using packageWriter As New FileStream(resourcePath, FileMode.Open)
                    Using archive As New ZipArchive(packageWriter, ZipArchiveMode.Update)
                        Dim xmlEntries = archive.Entries.Where(Function(e) e.Name.Equals(ggbXmlFile) AndAlso Not String.IsNullOrEmpty(e.Name))
                        If xmlEntries.Any() Then
                            Dim ggbXDocument As XDocument
                            Using ggbFileReader As StreamReader = New StreamReader(xmlEntries(0).Open())
                                ggbXDocument = XDocument.Parse(ggbFileReader.ReadToEnd())
                            End Using
                            xmlEntries(0).Delete()

                            Dim newEntry As ZipArchiveEntry = archive.CreateEntry(ggbXmlFile)
                            Using ggbFileWriter As StreamWriter = New StreamWriter(newEntry.Open())

                                ggbXDocument.Descendants("expression").Where(Function(n) n.Attributes.Any(Function(a) a.Name.ToString.Equals("label") AndAlso a.Value.StartsWith("geo"))).Remove()

                                For Each elem In ggbXDocument.Descendants("element").Where(Function(e) e.Element("show") IsNot Nothing AndAlso e.Element("show").Attribute("object") IsNot Nothing AndAlso e.Element("show").Attribute("object").Value.Equals("false"))
                                    If elem.Attribute("label") IsNot Nothing Then
                                        ggbXDocument.Descendants("command").Where(Function(c) c.Element("input") IsNot Nothing AndAlso c.Element("input").Attributes.Any(Function(a) a.Value.Equals(elem.Attribute("label").Value))).Remove()
                                        ggbXDocument.Descendants("command").Where(Function(c) c.Element("output") IsNot Nothing AndAlso c.Element("output").Attributes.Any(Function(a) a.Value.Equals(elem.Attribute("label").Value))).Remove()
                                    End If
                                Next
                                ggbXDocument.Descendants("element").Where(Function(e) e.Element("show") IsNot Nothing AndAlso e.Element("show").Attribute("object") IsNot Nothing AndAlso e.Element("show").Attribute("object").Value.Equals("false")).Remove()

                                ggbFileWriter.Write(ggbXDocument.ToString())
                            End Using
                        Else
                            Throw New ArgumentException(String.Format(My.Resources.MissingGeogebraFile, ggbXmlFile))
                        End If
                    End Using
                End Using

                If Not isPreview Then
                    PackageCreator.AddResourceToManifest(resources, CreateResourceType(String.Concat(_FolderDirectory(categoryDirectory), "/", resourceName), String.Empty))
                End If
            End If

            resourceDependencies.Add(ChainHandlerHelper.GetIdentifierFromResourceId(resourceName, PackageCreatorConstants.TypeOfResource.resource))
            ChainHandlerHelper.UpdateReferenceInTemplate(parsedTemplate, resourceNameOrg,
                                                         $"{_packageCreator.RelativePathToResources}{String.Concat(_FolderDirectory(categoryDirectory), "/", resourceName)}".Replace("\", "/"))
        End Sub

        Private Function CreateResourceType(resourceName As String, identifierPrefix As String) As ResourceType
            Dim resourceType = GetResourceType(Nothing, resourceName)
            Dim identifier As String = Path.GetFileName(resourceName)
            If Not String.IsNullOrEmpty(identifierPrefix) Then identifier = String.Concat(identifierPrefix, "_", identifier)

            resourceType.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(identifier, PackageCreatorConstants.TypeOfResource.resource)
            resourceType.type = _ResourceTypes(PackageCreator.QTIManifestResourceType.webcontent)
            resourceType.href = resourceName

            Dim fileType As New FileType()
            fileType.href = resourceName
            Dim fileTypeArray(0) As FileType
            fileTypeArray(0) = fileType

            resourceType.file = fileTypeArray

            Return resourceType
        End Function

        Private Function ConvertStreamToByteArray(input As Stream) As Byte()
            Dim buffer As Byte() = New Byte(16 * 1024 - 1) {}

            Using ms As New MemoryStream()
                Dim read As Integer = input.Read(buffer, 0, buffer.Length)

                While read > 0
                    ms.Write(buffer, 0, read)
                    read = input.Read(buffer, 0, buffer.Length)
                End While

                Return ms.ToArray()
            End Using
        End Function

        Private Sub DetermineTempDirectoryForCi(ByRef resourcePath As String, resourceName As String)
            Dim ciDirectoryName = $"{New DirectoryInfo(resourcePath).Parent.Name}{"_CI"}"
            Dim tempCiDir = New DirectoryInfo(Path.Combine(Path.GetDirectoryName(_packageCreator.TempWorkingDirectory.FullName), ciDirectoryName))
            If Not Directory.Exists(tempCiDir.FullName) Then
                Directory.CreateDirectory(tempCiDir.FullName)
            End If
            resourcePath = Path.Combine(tempCiDir.FullName, resourceName)
        End Sub

        Private Function MimeTypeCanHaveDependencies(mimeType As String) As Boolean
            Dim canHaveDependencies As Boolean = True

            If (mimeType.Contains("image") OrElse
    mimeType.Contains("audio") OrElse
    mimeType.Contains("video") OrElse
    (mimeType.Contains("application") AndAlso Not mimeType.Contains("application/xhtml+xml")) OrElse
    mimeType.Contains("text/plain")) Then
                canHaveDependencies = False
            End If

            Return canHaveDependencies
        End Function

        Private Function ReplaceLanguageStyles(ByRef resourceContent As String) As Boolean
            Dim tempDoc As New XmlDocument
            tempDoc.PreserveWhitespace = True
            tempDoc.LoadXml($"<wrapper>{resourceContent}</wrapper>")
            If ReplaceLanguageStyles(tempDoc, True) Then
                If tempDoc.DocumentElement IsNot Nothing AndAlso tempDoc.DocumentElement.HasChildNodes Then
                    resourceContent = tempDoc.DocumentElement.InnerXml
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function ReplaceLanguageStyles(ByRef templateDocument As XmlDocument, Optional isSourceText As Boolean = False) As Boolean
            Dim resourceContentChanged As Boolean = False
            If ReplaceLanguageStyles(templateDocument, "UserSRttsEngels", "en-US", isSourceText) Then resourceContentChanged = True
            If ReplaceLanguageStyles(templateDocument, "UserSRttsNederlands", "nl", isSourceText) Then resourceContentChanged = True
            If ReplaceLanguageStyles(templateDocument, "UserSRttsFrans", "fr", isSourceText) Then resourceContentChanged = True
            If ReplaceLanguageStyles(templateDocument, "UserSRttsDuits", "de", isSourceText) Then resourceContentChanged = True
            If ReplaceLanguageStyles(templateDocument, "LangTTSEngels", "en-US", isSourceText) Then resourceContentChanged = True
            If ReplaceLanguageStyles(templateDocument, "LangTTSNederlands", "nl", isSourceText) Then resourceContentChanged = True
            If ReplaceLanguageStyles(templateDocument, "LangTTSFrans", "fr", isSourceText) Then resourceContentChanged = True
            If ReplaceLanguageStyles(templateDocument, "LangTTSDuits", "de", isSourceText) Then resourceContentChanged = True
            Return resourceContentChanged
        End Function

        Private Function ReplaceLanguageStyles(
                                               ByRef templateDocument As XmlDocument,
                                               styleToReplace As String,
                                               language As String,
                                               isSourceText As Boolean) As Boolean

            Dim result As Boolean = False

            Dim classNodesToEdit As XmlNodeList = templateDocument.SelectNodes($"//*[contains(concat(' ', @class, ' '), ' {styleToReplace} ')]")
            For Each node As XmlNode In classNodesToEdit
                node.Attributes("class").Value = node.Attributes("class").Value.Replace(styleToReplace, String.Empty).Trim

                Dim langAttribute As XmlAttribute = Nothing
                If isSourceText Then
                    langAttribute = templateDocument.CreateAttribute("lang")
                Else
                    Dim ns As String = templateDocument.GetNamespaceOfPrefix("xml")
                    langAttribute = templateDocument.CreateAttribute("xml", "lang", ns)
                End If
                langAttribute.Value = language

                If node.Attributes("lang") IsNot Nothing Then
                    node.Attributes("lang").Value = language
                Else
                    node.Attributes.Append(langAttribute)
                End If
                result = True
            Next

            Dim classNodesToRemove As XmlNodeList = templateDocument.SelectNodes(String.Format("//*[string-length(@class)=0]"))
            For Each node As XmlNode In classNodesToRemove
                node.Attributes.RemoveNamedItem("class")
                result = True
            Next

            Return result
        End Function

        Private Sub StripTablerowStylesFromSourceTexts(ByRef resourceContent As String)
            Dim tempDoc As New XmlDocument
            tempDoc.PreserveWhitespace = True
            tempDoc.LoadXml($"<wrapper>{resourceContent}</wrapper>")
            If StripTablerowStylesFromSourcetexts(tempDoc) Then
                If tempDoc.DocumentElement IsNot Nothing AndAlso tempDoc.DocumentElement.HasChildNodes Then
                    resourceContent = tempDoc.DocumentElement.InnerXml
                End If
            End If
        End Sub

        Private Function StripTablerowStylesFromSourcetexts(ByRef templateDocument As XmlDocument) As Boolean
            Dim result As Boolean = False
            Dim namespaceManager = New XmlNamespaceManager(templateDocument.NameTable)
            namespaceManager.AddNamespace("def", "http://www.w3.org/1999/xhtml")
            Dim TrNodesToEdit As XmlNodeList = templateDocument.SelectNodes("//def:tr", namespaceManager)
            For Each node As XmlNode In TrNodesToEdit
                If node.Attributes IsNot Nothing AndAlso node.Attributes("style") IsNot Nothing Then
                    node.Attributes.RemoveNamedItem("style")
                End If
                result = True
            Next
            Return result
        End Function

        Private Function ProcessTextToSpeech(ByRef xmlDoc As XmlDocument) As String
            TextToSpeechHelper.ConvertToSsml(xmlDoc, _namespaceHelper.GetSSMLNamespace().NamespaceName)
            Return xmlDoc.DocumentElement.InnerXml
        End Function

        Private Shared Function IsSourceTextFile(resourceFile() As Byte, resourceName As String) As Boolean
            Dim mimeType As String = ChainHandlerHelper.GetMimeTypeFromFile(resourceFile, resourceName)

            If mimeType.Contains("application/xhtml+xml") OrElse
                mimeType.Contains("text/plain") OrElse
                mimeType.Contains("text/html") OrElse
                mimeType.Contains("text/html") Then
                Return True
            End If

            Return False
        End Function

    End Class
End Namespace