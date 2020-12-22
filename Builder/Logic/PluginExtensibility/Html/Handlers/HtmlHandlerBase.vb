Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports HtmlAgilityPack
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.Handlers
    Public Class HtmlHandlerBase

        Public Event InlineElementAdded As EventHandler(Of InlineElementEventArgs)
        Public Event ResourceAdded As EventHandler(Of ResourceNameEventArgs)


        Protected ReadOnly editor As IXHtmlEditor
        Protected ReadOnly namespaceManager As XmlNamespaceManager
        Protected ReadOnly resourceProtocolPrefix As String = Constants.ResourceProtocolPrefix
        Protected ReadOnly bankId As Integer
        Protected ReadOnly cntHlp As New HtmlContentHelper
        Protected ReadOnly resourceManager As ResourceManagerBase
        Protected ReadOnly inlineTemplates As Dictionary(Of String, String)
        Protected ReadOnly resourceEntity As ResourceEntity
        Protected ReadOnly param As XHtmlParameter


        Public Sub New(ByVal editor As IXHtmlEditor, ByVal bankId As Integer, ByVal resourceManager As ResourceManagerBase, ByVal inlineTemplates As Dictionary(Of String, String))
            Me.New(editor, bankId, resourceManager, inlineTemplates, Nothing, Nothing)
        End Sub

        Public Sub New(ByVal editor As IXHtmlEditor, ByVal bankId As Integer, ByVal resourceManager As ResourceManagerBase, ByVal inlineTemplates As Dictionary(Of String, String),
                       resourceEntity As ResourceEntity, param As XHtmlParameter)
            Me.editor = editor
            Me.bankId = bankId
            Me.resourceManager = resourceManager
            Me.namespaceManager = XHtmlParameterExtensions.GetNamespaceManager()
            Me.inlineTemplates = inlineTemplates
            Me.resourceEntity = resourceEntity
            Me.param = param
        End Sub

        Public ReadOnly Property ResManager As ResourceManagerBase
            Get
                Return resourceManager
            End Get
        End Property

        Public ReadOnly Property ResEntity As ResourceEntity
            Get
                Return resourceEntity
            End Get
        End Property



        Protected Sub OnInlineElementAdded(ByVal sender As Object, e As InlineElementEventArgs)
            RaiseEvent InlineElementAdded(sender, e)
        End Sub

        Protected Sub OnResourceAdded(ByVal sender As Object, e As ResourceNameEventArgs)
            RaiseEvent ResourceAdded(sender, e)
        End Sub

        Public Function GetEditorData() As XmlDocument
            Return editor.Document
        End Function

        Public Sub PerformDropOperation(html As String, droppedInlineElements As Dictionary(Of String, String))
            Dim formulaHandler As New HtmlFormulaHandler(editor, resourceManager, namespaceManager, bankId)
            AddHandler formulaHandler.ResourceAdded, Sub(s, e)
                                                         RaiseEvent ResourceAdded(s, e)
                                                     End Sub

            Dim inlineHandler As New HtmlInlineHandler(editor, resourceManager, bankId, inlineTemplates, resourceEntity, Me.param)
            AddHandler inlineHandler.InlineElementAdded, Sub(s, e)
                                                             RaiseEvent InlineElementAdded(s, e)
                                                         End Sub
            AddHandler inlineHandler.ResourceAdded, Sub(s, e)
                                                        RaiseEvent ResourceAdded(s, e)
                                                    End Sub

            Dim attributeNameForMarking As String = "pasted"

            Try
                Dim conversionResult As Boolean = True
                If Not String.IsNullOrEmpty(html) Then
                    Dim bodyText = GetBodyText(html)
                    If Not String.IsNullOrEmpty(bodyText) Then
                        Dim body = String.Format("<fakeBody>{0}</fakeBody", bodyText)
                        Dim doc As New HtmlAgilityPack.HtmlDocument()
                        doc.LoadHtml(body)
                        doc.OptionOutputAsXml = True
                        conversionResult = formulaHandler.ConvertPastedImagesToMathMLFormulas(doc.DocumentNode.SelectNodes("//img[@ismathmlimage]"))
                        conversionResult = conversionResult And inlineHandler.ConvertPastedImagesToInlineElements(doc.DocumentNode.SelectNodes("//img[not(@ismathmlimage)]"), HtmlContentHelper.PasteFrom.OtherXHTMLEditor, Nothing, droppedInlineElements)
                    End If
                End If
                If conversionResult Then
                    RemoveAttributeFromImageElements(attributeNameForMarking)
                    cntHlp.RemoveCommentOfStartAndEndOfPastedCode(editor.Document.SelectSingleNode("//def:body", namespaceManager))
                End If
            Catch ex As Exception
                MessageBox.Show(String.Format("Something went wrong while pasting : {0}", ex.Message))
                editor.LoadXml(editor.Document.OuterXml)
            End Try
        End Sub

        Public Sub PerformPasteOperation(ByVal forcePlainTextPaste As Boolean)
            PerformPasteOperation(forcePlainTextPaste, Nothing)
        End Sub

        Public Sub PerformPasteOperation(ByVal forcePlainTextPaste As Boolean, ByVal lastCopiedInlineElements As List(Of InlineElement))
            Dim formulaHandler As New HtmlFormulaHandler(editor, resourceManager, namespaceManager, bankId)
            AddHandler formulaHandler.ResourceAdded, Sub(s, e)
                                                         RaiseEvent ResourceAdded(s, e)
                                                     End Sub

            Dim inlineHandler As New HtmlInlineHandler(editor, resourceManager, bankId, inlineTemplates, resourceEntity, Me.param)
            AddHandler inlineHandler.InlineElementAdded, Sub(s, e)
                                                             RaiseEvent InlineElementAdded(s, e)
                                                         End Sub
            AddHandler inlineHandler.ResourceAdded, Sub(s, e)
                                                        RaiseEvent ResourceAdded(s, e)
                                                    End Sub

            Dim attributeNameForMarking As String = "pasted"
            Dim pastedAsText As Boolean = False

            Try
                Dim conversionResult As Boolean = True

                If forcePlainTextPaste Then
                    editor.BeginTransaction()
                    editor.DoPasteAsText()
                    pastedAsText = True
                Else
                    Dim pasteCode As String = cntHlp.GetClipboardData
                    Dim pasteType As HtmlContentHelper.PasteFrom = cntHlp.DetectPasteType(pasteCode, forcePlainTextPaste)
                    If Not String.IsNullOrEmpty(pasteCode) Then
                        Select Case pasteType
                            Case HtmlContentHelper.PasteFrom.Word, HtmlContentHelper.PasteFrom.Excel
                                Dim cleanCode As String = cntHlp.CleanWordOrExcelHtml(pasteCode)

                                Dim doc As New HtmlAgilityPack.HtmlDocument()
                                doc.LoadHtml(cleanCode)
                                doc.OptionOutputAsXml = True

                                If doc.DocumentNode.SelectSingleNode("//body") IsNot Nothing Then
                                    Dim pastedImageTags As HtmlNodeCollection = doc.DocumentNode.SelectNodes("//img[not(@isinlineelement) and not(@ismathmlimage)]")
                                    conversionResult = inlineHandler.ConvertPastedImagesToInlineElements(pastedImageTags, pasteType)
                                    If conversionResult Then
                                        editor.BeginTransaction()
                                        Dim range As ITextRange = editor.CreateRangeFromSelection()
                                        range.XmlText = doc.DocumentNode.SelectSingleNode("//body").InnerHtml
                                    End If
                                End If
                            Case HtmlContentHelper.PasteFrom.OtherXHTMLEditor, HtmlContentHelper.PasteFrom.Html
                                Dim bodyText = GetBodyText(pasteCode)
                                If Not String.IsNullOrEmpty(bodyText) Then
                                    Dim body = String.Format("<fakeBody>{0}</fakeBody", bodyText)
                                    Dim doc As New HtmlAgilityPack.HtmlDocument()
                                    doc.LoadHtml(body)
                                    doc.OptionOutputAsXml = True

                                    conversionResult = formulaHandler.ConvertPastedImagesToMathMLFormulas(doc.DocumentNode.SelectNodes("//img[@ismathmlimage]"))
                                    conversionResult = conversionResult And inlineHandler.ConvertPastedImagesToInlineElements(doc.DocumentNode.SelectNodes("//img[not(@ismathmlimage)]"), pasteType, lastCopiedInlineElements, Nothing)
                                    If conversionResult Then
                                        editor.BeginTransaction()
                                        Dim range As ITextRange = editor.CreateRangeFromSelection()
                                        range.XmlText = doc.DocumentNode.SelectSingleNode("//fakebody").InnerHtml
                                    End If
                                End If
                            Case HtmlContentHelper.PasteFrom.Other
                                editor.BeginTransaction()
                                editor.DoPasteAsText()
                                pastedAsText = True
                        End Select
                    End If
                End If
                If conversionResult Then
                    editor.CommitTransaction()
                    If Not pastedAsText Then
                        RemoveAttributeFromImageElements(attributeNameForMarking)
                        cntHlp.RemoveCommentOfStartAndEndOfPastedCode(editor.Document.SelectSingleNode("//def:body", namespaceManager))
                    End If
                Else
                    RollbackPasteOperation(editor)
                End If
            Catch ex As Exception
                MessageBox.Show(String.Format("Something went wrong while pasting : {0}", ex.Message))
                RollbackPasteOperation(editor)
            End Try
        End Sub

        Private Sub RollbackPasteOperation(editor As IXHtmlEditor)
            Try
                editor.RollbackTransaction()
                editor.LoadXml(editor.Document.OuterXml)
            Catch exc As Exception
            End Try
        End Sub

        Private Function GetBodyText(input As String) As String
            input = Regex.Replace(input, "<!--.*?-->", "", RegexOptions.Singleline)
            Dim regexGetBody As New Regex("<\s*body[^>]*>(.*?)<\s*/\s*body>", RegexOptions.Singleline Or RegexOptions.IgnoreCase)
            Dim m As Match = regexGetBody.Match(input)
            If m.Success Then
                Return m.Groups(m.Groups.Count - 1).ToString().Trim
            End If
            Return String.Empty
        End Function

        Private Sub MarkPastedImages(ByVal bodyBeforePaste As XmlElement, ByRef allImagesAfterPaste As XmlNodeList, ByVal attributeNameForMarking As String)
            For Each imageNodeAfterPaste As XmlElement In allImagesAfterPaste
                If bodyBeforePaste.SelectSingleNode(String.Format("//def:img[@id=""{0}""]", imageNodeAfterPaste.GetAttribute("id")), namespaceManager) Is Nothing Then
                    imageNodeAfterPaste.SetAttribute(attributeNameForMarking, "true")
                End If
            Next
        End Sub

        Private Sub RemoveAttributeFromImageElements(ByVal attributeName As String)
            For Each node As XmlElement In editor.Document.SelectNodes("//def:img[@pasted]", namespaceManager)
                node.RemoveAttribute(attributeName)
            Next
        End Sub

        Protected Function CreateCopyOfNodeAndAddDefaultNamespace(ByVal node As XmlNode) As XmlNode
            Dim newElement As XmlElement = editor.Document.CreateElement("", node.Name, namespaceManager.LookupNamespace("def"))

            For Each attribute As XmlAttribute In node.Attributes
                newElement.SetAttribute(attribute.Name, attribute.Value)
            Next

            newElement.InnerXml = node.InnerXml

            Return newElement
        End Function

        Protected Function GetFirstParentNodeOfTypeElement(ByVal node As XmlNode) As XmlNode
            If TypeOf node Is XmlElement Then
                Return node
            Else
                If node.ParentNode IsNot Nothing Then
                    Return GetFirstParentNodeOfTypeElement(node.ParentNode)
                Else
                    Throw New ArgumentException("Could not determine node of type Element.")
                End If
            End If
        End Function

        Protected Function CreateNewUniqueContextIdentifier(ByVal path As String) As Integer
            Dim newUri As Uri = Nothing

            If Uri.TryCreate(path, UriKind.RelativeOrAbsolute, newUri) Then
                If newUri.IsAbsoluteUri Then
                    If newUri.Port > 1 Then
                        Return newUri.Port - 1
                    Else
                        Return newUri.Port + 1
                    End If
                Else
                    Return 1
                End If
            Else
                Return 1
            End If
        End Function

        Protected Function LinkPastedImageAsResource(ByVal sourceImagePath As Uri, ByRef uniquenessSeed As Integer, ByVal bankId As Integer, pasteType As HtmlContentHelper.PasteFrom, ByVal filenamePrefix As String) As String
            Dim imageGenericResource As GenericResourceEntity
            Dim filename As String = sourceImagePath.LocalPath

            If sourceImagePath.Scheme = "file" Then
                uniquenessSeed = CreateNewUniqueContextIdentifier(sourceImagePath.LocalPath)
            End If

            imageGenericResource = New GenericResourceEntity()
            With imageGenericResource
                .ResourceId = Guid.NewGuid()
                .Version = "0.1"
                .BankId = bankId
                .Description = String.Empty
                .ResourceData = New ResourceDataEntity()
            End With

            If pasteType = HtmlContentHelper.PasteFrom.Other Then
                If sourceImagePath.Scheme = "file" Then
                    imageGenericResource.MediaType = FileHelper.GetMimeFromFile(filename)
                    imageGenericResource.Title = Path.GetFileName(filename)
                    Dim extension As String = FileHelper.GetFileExtensionByMimeType(imageGenericResource.MediaType)

                    If String.IsNullOrEmpty(extension) Then
                        extension = Path.GetExtension(filename).Trim(".".ToCharArray)
                    End If

                    imageGenericResource.Name = String.Format("{8}_{0:####}{1:##}{2:##}_{3:##}_{4:##}_{5:##}_{6:###}_{7}.{9}", Date.Today.Year, Date.Today.Month, Date.Today.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond, uniquenessSeed, filenamePrefix, extension)
                    imageGenericResource.ResourceData.BinData = FileHelper.MakeByteArrayFromFile(filename)
                Else
                    MessageBox.Show(String.Format(My.Resources.CannotPasteImagesOutsideBankHierarchy, Path.GetFileName(sourceImagePath.LocalPath)))
                    Return String.Empty
                End If
            ElseIf pasteType = HtmlContentHelper.PasteFrom.Word OrElse pasteType = HtmlContentHelper.PasteFrom.Excel Then
                imageGenericResource.Name = String.Format("{8}_{0:####}{1:##}{2:##}_{3:##}_{4:##}_{5:##}_{6:###}_{7}.{9}", Date.Today.Year, Date.Today.Month, Date.Today.Day, Date.Now.Hour, Date.Now.Minute, Date.Now.Second, Date.Now.Millisecond, uniquenessSeed, filenamePrefix, Path.GetExtension(filename).Trim(".".ToCharArray))
                imageGenericResource.ResourceData.BinData = FileHelper.MakeByteArrayFromFile(filename)
                imageGenericResource.MediaType = FileHelper.GetMimeFromFile(filename)
                imageGenericResource.Title = Path.GetFileNameWithoutExtension(sourceImagePath.LocalPath)
            Else
                Return String.Empty
            End If

            imageGenericResource.SetResource(imageGenericResource.ResourceData.BinData, imageGenericResource.MediaType)

            Dim result As String = ResourceFactory.Instance.UpdateGenericResource(imageGenericResource)

            If Not String.IsNullOrEmpty(result) Then
                MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            Return imageGenericResource.Name
        End Function


        Protected Sub GenericHandler_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)

            Dim _resource As BinaryResource = Nothing
            Dim request = New ResourceRequestDTO()
            If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then
                If e.TypedResourceType IsNot Nothing Then
                    _resource = resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
                Else
                    _resource = resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
                End If
                e.BinaryResource = _resource
            Else
                e.BinaryResource = New BinaryResource(New Object)
            End If

            If (e.Command And ResourceNeededCommand.MetaData) = ResourceNeededCommand.MetaData Then
                Dim fetchedMetaData As MetaDataCollection = resourceManager.GetResourceMetaData(e.ResourceName)

                e.Metadata.Clear()
                e.Metadata.AddRange(fetchedMetaData)
            End If
        End Sub

    End Class
End Namespace

