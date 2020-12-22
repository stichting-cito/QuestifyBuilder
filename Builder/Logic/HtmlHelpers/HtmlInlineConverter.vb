Imports System.Collections.Specialized
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports Questify.Builder.Logic.ItemProcessing
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Namespace HtmlHelpers

    Public Class HtmlInlineConverter

        Private ReadOnly _resourceManager As ResourceManagerBase
        Private ReadOnly _namespaceManager As XmlNamespaceManager
        Private ReadOnly _inlineTemplates As Dictionary(Of String, String)
        Private _psHelper As ParameterSetCollectionHelper
        Private ReadOnly _templateNames As IHtmlInlineTemplateNames

        Public Sub New(resourceManager As ResourceManagerBase,
                       namespaceManager As XmlNamespaceManager,
                       inlineTemplates As Dictionary(Of String, String),
                       templateNames As IHtmlInlineTemplateNames)
            _resourceManager = resourceManager
            _namespaceManager = namespaceManager
            _inlineTemplates = inlineTemplates
            _templateNames = If(templateNames, New DefaultHtmlInlineTemplateNames())
        End Sub

        Public Function ConvertHtmlBasedOnOldItemLayoutToInlineElementLayout(bodyInnerXml As String,
                                                                             htmlDocTemplate As String,
                                                                             inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)), resourceProtocolPrefix As String) As String
            Dim doc As New XHtmlDocument()
            doc.PreserveWhitespace = True
            doc.LoadXml(String.Format(htmlDocTemplate, bodyInnerXml))
            Dim popupList As XmlNodeList = doc.SelectNodes("descendant::cito:popupcontrol", _namespaceManager)

            For Each popupNode As XmlNode In popupList
                Dim innerHtmNode As XmlNode = popupNode.SelectSingleNode("cito:innerhtml", _namespaceManager)

                If innerHtmNode IsNot Nothing Then
                    Dim popupParAttributes As String = innerHtmNode.Attributes("popuppar").InnerText
                    Dim newAnchorNode As XmlNode = doc.CreateNode(XmlNodeType.Element, "a", _namespaceManager.LookupNamespace("def"))
                    Dim popupControlAttr As XmlAttribute = doc.CreateAttribute("popuppar")
                    popupControlAttr.InnerText = popupParAttributes
                    newAnchorNode.Attributes.Append(popupControlAttr)

                    If innerHtmNode.SelectSingleNode("./def:img", _namespaceManager) Is Nothing Then
                        popupNode.ParentNode.RemoveChild(popupNode)
                    Else
                        newAnchorNode.InnerXml = innerHtmNode.SelectSingleNode("./def:img", _namespaceManager).OuterXml
                        popupNode.ParentNode.ReplaceChild(newAnchorNode, popupNode)
                    End If
                End If
            Next

            Dim bodyNode As XmlNode = doc.SelectSingleNode("//def:body", _namespaceManager)

            ConvertImagesAndPopupParsBasedOnOldItemLayoutToInlineElementLayout(doc, inlineElements, resourceProtocolPrefix)

            Return bodyNode.InnerXml
        End Function

        Public Function ConvertCitoControlsToInlineHtmlElements(bodyHtml As String, inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)), inlineElementPlaceHolders As Dictionary(Of String, XmlNode)) As String
            Dim element As XmlElement = ConvertXhtmlParameterToXmlElement(bodyHtml)
            ReplaceAllInlineElementControlsWithInlineHtmlElements(element, inlineElements, inlineElementPlaceHolders)
            Return element.InnerXml
        End Function

        Friend Sub ConvertImagesAndPopupParsBasedOnOldItemLayoutToInlineElementLayout(ByRef doc As XHtmlDocument, inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)),
                                                                                      resourceProtocolPrefix As String)
            Dim imageBasedOnOldItemLayoutNodes As XmlNodeList = doc.SelectNodes("//def:img[not(@isinlineelement=""true"") and not(ancestor::def:a) and not(@ismathmlimage=""true"")]", _namespaceManager)
            Dim popupParBasedOnOldItemLayoutNodes As XmlNodeList = doc.SelectNodes("//def:a[@popuppar]", _namespaceManager)
            Dim imagesAndPopupParsBasedOnOldItemLayout = imageBasedOnOldItemLayoutNodes.Cast(Of XmlNode)().Concat(popupParBasedOnOldItemLayoutNodes.Cast(Of XmlNode)())

            If imagesAndPopupParsBasedOnOldItemLayout.Count <= 0 Then
                Return
            End If

            Dim inlineImageTemplate As String = GetInlineMediaTemplate("image")
            Dim isEmbeddedResourceTemplate = InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(inlineImageTemplate)
            If Not isEmbeddedResourceTemplate AndAlso _resourceManager.GetResource(inlineImageTemplate) Is Nothing Then
                Throw New ResourceException(String.Format(My.Resources.ItemLayoutTemplateInlineMultimediaMissing, inlineImageTemplate))
            End If

            Using placeHolderHelper As New PlaceHolderHelper
                For Each childElement As XmlElement In imagesAndPopupParsBasedOnOldItemLayout
                    Dim inlineElement As InlineElement
                    If childElement.Attributes("id") IsNot Nothing AndAlso inlineElements.ContainsKey(childElement.Attributes("id").Value) Then
                        inlineElement = inlineElements(childElement.Attributes("id").Value).Item1
                        Dim inlineImageConverter As InlineElement.InlineElementImageConverter
                        If inlineElement IsNot Nothing AndAlso inlineElement.ImageConverter IsNot Nothing Then
                            inlineImageConverter = inlineElement.ImageConverter
                        Else
                            inlineImageConverter = New InlineElement.InlineElementImageConverter(_namespaceManager, resourceProtocolPrefix, False)
                            inlineElement.ImageConverter = inlineImageConverter
                        End If
                        inlineImageConverter.ConvertHtmlBasedOnOldItemLayoutToInlineElementLayout(childElement, inlineElement)
                    Else
                        inlineElement = New InlineElement()
                        inlineElement.Identifier = InlineElementHelper.GetNewInlineElementIdentifier()
                        inlineElement.LayoutTemplateSourceName = inlineImageTemplate
                        If Not isEmbeddedResourceTemplate Then
                            If _psHelper Is Nothing Then
                                _psHelper = New ParameterSetCollectionHelper(_resourceManager, inlineImageTemplate)
                                _psHelper.CachingStrategy = New InMemoryParameterSetCacheByBank(0)
                            End If
                            inlineElement.Parameters.AddRange(_psHelper.GetExtractedParameters)
                        Else
                            inlineElement.Parameters.AddRange(InlineMediaTemplateHelper.GetParameterSetFromEmbeddedResource(inlineImageTemplate))
                        End If
                        Dim inlineImageConverter As New InlineElement.InlineElementImageConverter(_namespaceManager, resourceProtocolPrefix, False)
                        inlineImageConverter.ConvertHtmlBasedOnOldItemLayoutToInlineElementLayout(childElement, inlineElement)
                        inlineElement.ImageConverter = inlineImageConverter
                        inlineElements.Add(inlineElement.Identifier, New Tuple(Of InlineElement, Boolean)(inlineElement, True))
                    End If


                    Dim newImageElement = placeHolderHelper.InlineElementToPlaceHolderImage(inlineElement, "http://www.w3.org/1999/xhtml", IsPopupInlineElement(inlineElement.LayoutTemplateSourceName))
                    Dim importedNode As XmlNode = doc.ImportNode(newImageElement, True)
                    childElement.ParentNode.ReplaceChild(importedNode, childElement)
                Next
            End Using
        End Sub

        Public Function ConvertInlineElementLayoutToHtmlBasedOnOldItemLayout(body As XmlElement, ByRef inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)), resourceProtocolPrefix As String) As String
            Dim doc As New XmlDocument()
            doc.PreserveWhitespace = True
            doc.LoadXml(body.OuterXml)
            Dim elementsToReplace As XmlNodeList = doc.SelectNodes(String.Format("//def:img[@isinlineelement=""true""] | //img[@isinlineelement=""true""]"), _namespaceManager)

            For Each elementToReplace As XmlElement In elementsToReplace
                Dim inlineElementId As String = elementToReplace.GetAttribute("id")

                If inlineElements.Any(Function(ie) ie.Key.Equals(inlineElementId, StringComparison.InvariantCultureIgnoreCase) AndAlso ie.Value.Item2 = True) Then
                    Dim inlineElement As InlineElement = inlineElements(inlineElementId).Item1
                    CheckForInEditorResizingOfResource(elementToReplace, inlineElement)

                    Dim elementBasedOnOldItemLayout As XmlElement

                    If inlineElement.Parameters(0).GetParameterByName("showPopup", False) IsNot Nothing AndAlso DirectCast(inlineElement.Parameters(0).GetParameterByName("showPopup", False), BooleanParameter).Value Then
                        elementBasedOnOldItemLayout = doc.CreateElement("a", _namespaceManager.LookupNamespace("def"))
                        elementBasedOnOldItemLayout.AppendChild(doc.CreateElement(Nothing, "img", elementBasedOnOldItemLayout.NamespaceURI))
                    Else
                        elementBasedOnOldItemLayout = doc.CreateElement("img", _namespaceManager.LookupNamespace("def"))
                    End If

                    Dim inlineElementImageConverter As InlineElement.InlineElementImageConverter

                    If inlineElement.ImageConverter IsNot Nothing Then
                        inlineElementImageConverter = inlineElement.ImageConverter
                    Else
                        inlineElementImageConverter = New InlineElement.InlineElementImageConverter(_namespaceManager, resourceProtocolPrefix, True)
                    End If

                    inlineElementImageConverter.ConvertInlineElementLayoutToHtmlBasedOnOldItemLayout(elementBasedOnOldItemLayout, inlineElement)
                    elementToReplace.ParentNode.ReplaceChild(elementBasedOnOldItemLayout, elementToReplace)
                    ConvertPopupControlToHtmlBasedOnOldItemLayout(doc.DocumentElement)
                End If
            Next

            Return doc.DocumentElement.InnerXml
        End Function

        Public Function ConvertInlineElementToCitoControl(body As XmlElement, ByRef inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean))) As String
            ReplaceAllInlineHtmlElementsWithCitoControl(body, inlineElements)

            Return body.OuterXml
        End Function

        Private Sub ReplaceAllInlineHtmlElementsWithCitoControl(element As XmlElement, ByRef inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)))
            Dim removedInlineElements As New List(Of String)
            For Each inlineElementId As String In inlineElements.Where(Function(ie) ie.Value.Item2 = False).Select(Function(ie2) ie2.Key)
                Dim xmlElement As XmlElement = GetInlineElementByIdentifier(element, inlineElementId)
                If xmlElement IsNot Nothing Then
                    Dim inlineElement As InlineElement = inlineElements(inlineElementId).Item1
                    CheckForInEditorResizingOfResource(xmlElement, inlineElement)

                    Dim newElement As XmlElement = xmlElement.OwnerDocument.CreateElement("img", xmlElement.NamespaceURI)
                    Dim xmlSerializerNamespaces As New XmlSerializerNamespaces()
                    xmlSerializerNamespaces.Add("cito", "http://www.cito.nl/citotester")
                    newElement.InnerXml = SerializeHelper.XmlSerializeToString(inlineElement, False, xmlSerializerNamespaces, True, Encoding.UTF8).Trim()

                    xmlElement.ParentNode.ReplaceChild(newElement.GetElementsByTagName("InlineElement", "http://www.cito.nl/citotester")(0), xmlElement)
                Else
                    removedInlineElements.Add(inlineElementId)
                End If
            Next
            For Each removedInlineElement As String In removedInlineElements
                inlineElements.Remove(removedInlineElement)
            Next
        End Sub

        Private Sub CheckForInEditorResizingOfResource(ByRef xmlElement As XmlElement, inlineElement As InlineElement)
            Dim source = inlineElement.Parameters(0).Parameters.OfType(Of ResourceParameter).FirstOrDefault
            If xmlElement.Attributes("width") IsNot Nothing AndAlso source IsNot Nothing Then
                If CInt(xmlElement.Attributes("width").Value) <> source.Width Then
                    source.EditSize = True
                End If
                source.Width = CInt(xmlElement.GetAttribute("width"))
            End If
            If xmlElement.Attributes("height") IsNot Nothing AndAlso source IsNot Nothing Then
                If CInt(xmlElement.Attributes("height").Value) <> source.Height Then
                    source.EditSize = True
                End If
                source.Height = CInt(xmlElement.GetAttribute("height"))
            End If
        End Sub


        Friend Sub ConvertPopupControlToHtmlBasedOnOldItemLayout(bodyNode As XmlElement)
            Dim popupAnchorList As XmlNodeList = bodyNode.SelectNodes("//def:a[@popuppar]", _namespaceManager)
            Dim emptyAnchorParentNodes As List(Of XmlNode) = New List(Of XmlNode)

            For Each anchorNode As XmlNode In popupAnchorList
                If anchorNode.SelectSingleNode("descendant::def:img", _namespaceManager) Is Nothing Then
                    emptyAnchorParentNodes.Add(anchorNode.ParentNode)
                    Continue For
                End If

                Dim popupparAttributeValue As String = anchorNode.Attributes("popuppar").InnerText
                Dim popupAttr As NameValueCollection = ConvertPopupParAttributeToNameValueCollection(popupparAttributeValue)

                Dim sb As New StringBuilder()
                Dim pad As Char = " "c

                sb.AppendLine("<cito:template xmlns:cito=""http://www.cito.nl/citotester"">".PadLeft(3, pad))
                sb.AppendLine("<html>".PadLeft(5, pad))

                sb.AppendLine("<body>".PadLeft(9, pad))
                sb.AppendFormat("<img src='resource://package/{0}' width='{1}' height='{2}' id='1' xmlns=""http://www.w3.org/1999/xhtml""/>{3}".PadLeft(11, pad), popupAttr("resource"), popupAttr("width"), popupAttr("height"), Environment.NewLine)
                sb.AppendLine("</body>".PadLeft(9, pad))
                sb.AppendLine("</html>".PadLeft(5, pad))
                sb.AppendLine("</cito:template>".PadLeft(3, pad))
                sb.AppendFormat("<cito:innerhtml xmlns:cito=""http://www.cito.nl/citotester"" popuppar=""{0}"">{1}".PadLeft(3, pad), popupparAttributeValue, Environment.NewLine)
                sb.AppendLine("<style type=""text/css""> .popuptrigger {cursor: hand;} </style>".PadLeft(6, pad))
                sb.AppendLine(anchorNode.InnerXml.PadLeft(6, pad))
                sb.AppendLine("</cito:innerhtml>".PadLeft(3, pad))
                Dim citoPopupControlNode As XmlNode = bodyNode.OwnerDocument.CreateNode(XmlNodeType.Element, "cito", "popupcontrol", _namespaceManager.LookupNamespace("cito"))

                Dim popupControlAttr As XmlAttribute = bodyNode.OwnerDocument.CreateAttribute("id")
                popupControlAttr.InnerText = popupAttr("popupid")
                citoPopupControlNode.Attributes.Append(popupControlAttr)

                popupControlAttr = bodyNode.OwnerDocument.CreateAttribute("styleClass")
                popupControlAttr.InnerText = "popuptrigger"
                citoPopupControlNode.Attributes.Append(popupControlAttr)

                popupControlAttr = bodyNode.OwnerDocument.CreateAttribute("window-style")
                Dim resizablePopup As Boolean
                Boolean.TryParse(popupAttr("resizable"), resizablePopup)
                If resizablePopup Then
                    popupControlAttr.InnerText = "Sizable"
                Else
                    popupControlAttr.InnerText = "Fixed"
                End If
                citoPopupControlNode.Attributes.Append(popupControlAttr)

                Dim attributeNames() As String = {"left", "top", "width", "height", "modal", "title"}
                For Each attribName As String In attributeNames
                    popupControlAttr = bodyNode.OwnerDocument.CreateAttribute($"window-{attribName}")

                    If attribName.Equals("width") OrElse attribName.Equals("height") Then
                        Dim size As Integer = 0
                        Integer.TryParse(popupAttr(attribName), size)
                        If size > 0 Then
                            popupControlAttr.InnerText = CStr(size + 48)
                        Else
                            popupControlAttr.InnerText = popupAttr(attribName)
                        End If
                    Else
                        popupControlAttr.InnerText = popupAttr(attribName)
                    End If

                    citoPopupControlNode.Attributes.Append(popupControlAttr)
                Next

                citoPopupControlNode.InnerXml = sb.ToString

                anchorNode.ParentNode.ReplaceChild(citoPopupControlNode, anchorNode)
            Next

            For Each emptyAnchorParent As XmlNode In emptyAnchorParentNodes
                Dim anchorNode As XmlNode = emptyAnchorParent.SelectSingleNode("descendant::def:a[@popuppar]")
                emptyAnchorParent.RemoveChild(anchorNode)
            Next
        End Sub

        Private Function ConvertPopupParAttributeToNameValueCollection(popupparAttributeValue As String) As NameValueCollection
            Dim popupAttributes() As String = popupparAttributeValue.Split(";"c)
            Dim popupAttr As New NameValueCollection

            For Each attribPair As String In popupAttributes
                Dim AttributeValue As String = attribPair.Split("="c)(1)

                If AttributeValue.Length > 2 Then
                    AttributeValue = AttributeValue.Substring(1, AttributeValue.LastIndexOf("'"c) - 1)
                Else
                    AttributeValue = String.Empty
                End If

                popupAttr.Add(attribPair.Split("="c)(0).TrimStart(" "c), AttributeValue)
            Next

            Return popupAttr
        End Function


        Public Function GetInlineElementsOfTemplateFromXhtmlParameters(templateName As String, xHtmlParameterList As List(Of XHtmlParameter)) As Dictionary(Of XHtmlParameter, IEnumerable(Of InlineElement))
            Dim listOfInlineElements As New Dictionary(Of XHtmlParameter, IEnumerable(Of InlineElement))
            For Each xhtmlParameter As XHtmlParameter In xHtmlParameterList
                Dim element As XmlElement = ConvertXhtmlParameterToXmlElement(xhtmlParameter.Value)

                Dim listOfInlineElementsInTemplate As List(Of InlineElement) = GetInlineElementsFromXmlElement(element)

                Dim listOfElementsPerParameter As IEnumerable(Of InlineElement) = listOfInlineElementsInTemplate.Where(Function(o) o.LayoutTemplateSourceName = templateName)
                If listOfElementsPerParameter IsNot Nothing AndAlso Not listOfElementsPerParameter.Count = 0 Then
                    listOfInlineElements.Add(xhtmlParameter, listOfElementsPerParameter)
                End If
            Next
            Return listOfInlineElements
        End Function

        Private Function ConvertXhtmlParameterToXmlElement(xHtmlParameterValue As String) As XmlElement
            Dim doc As New XHtmlDocument()
            Dim root As XmlElement = doc.CreateElement("root")
            root.InnerXml = xHtmlParameterValue
            doc.AppendChild(root)
            If doc IsNot Nothing AndAlso doc.DocumentElement IsNot Nothing Then
                Return doc.DocumentElement
            Else
                Return Nothing
            End If
        End Function

        Public Sub UpdateInlineElements(inlineElement As InlineElement, xHtmlParameter As XHtmlParameter)
            If inlineElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(inlineElement.Identifier) Then
                Dim element As XmlElement = ConvertXhtmlParameterToXmlElement(xHtmlParameter.Value)
                If element IsNot Nothing Then

                    Dim childElement As XmlElement = CType(element.SelectSingleNode(
                        $"//*[@id=""{inlineElement.Identifier}""]"), XmlElement)
                    If childElement IsNot Nothing Then
                        Dim xmlSerializerNamespaces As New XmlSerializerNamespaces()
                        xmlSerializerNamespaces.Add("cito", "http://www.cito.nl/citotester")
                        Dim newElement As XmlElement = childElement.OwnerDocument.CreateElement("img", childElement.NamespaceURI)
                        newElement.InnerXml = SerializeHelper.XmlSerializeToString(inlineElement, False, xmlSerializerNamespaces, True, Encoding.UTF8).Trim()
                        Dim newInlineElement As XmlElement = CType(newElement.SelectSingleNode(
                            $"//*[@id=""{inlineElement.Identifier}""]"), XmlElement)
                        childElement.ParentNode.ReplaceChild(newInlineElement, childElement)
                    Else
                        Debug.Assert(False, $"element: '{inlineElement.Identifier}'not found in xHtmlParameter")
                    End If
                Else
                    Debug.Assert(False,
                                 $"element empty while trying to replace the inlineelement: {inlineElement.Identifier}")
                End If
                xHtmlParameter.SetValue(element.InnerXml)

            Else
                Debug.Assert(False, "Inline element Empty or no Identifier!")
            End If
        End Sub

        Friend Function GetInlineElementsFromXmlElement(element As XmlElement) As List(Of InlineElement)
            Dim ret As New List(Of InlineElement)
            If element IsNot Nothing Then

                For Each childNode As XmlNode In element.SelectNodes("//cito:InlineElement", _namespaceManager)
                    Using reader As New StringReader(childNode.OuterXml)
                        Dim inlineElement As InlineElement = DirectCast(SerializeHelper.XmlDeserializeFromReader(reader, GetType(InlineElement)), InlineElement)
                        ret.Add(inlineElement)
                    End Using
                Next
            End If
            Return ret
        End Function

        Private Sub ReplaceAllInlineElementControlsWithInlineHtmlElements(element As XmlElement, ByRef inlineElements As Dictionary(Of String, Tuple(Of InlineElement, Boolean)), ByRef inlineElementPlaceholders As Dictionary(Of String, XmlNode))
            Dim listOfInlineElements As List(Of InlineElement) = GetInlineElementsFromXmlElement(element)
            Using pc = New PlaceHolderHelper
                For Each inlineElement As InlineElement In listOfInlineElements
                    Dim childElement = CType(element.SelectSingleNode(
                        $"//*[@id=""{inlineElement.Identifier}""]"), XmlElement)
                    If childElement Is Nothing Then
                        Continue For
                    End If

                    Dim importedNode As XmlNode = Nothing
                    If inlineElementPlaceholders.ContainsKey(inlineElement.Identifier) Then
                        importedNode = childElement.OwnerDocument.ImportNode(inlineElementPlaceholders(inlineElement.Identifier), True)
                    Else
                        Dim inElement = pc.InlineElementToPlaceHolderImage(inlineElement, "http://www.w3.org/1999/xhtml", IsPopupInlineElement(inlineElement.LayoutTemplateSourceName))
                        importedNode = childElement.OwnerDocument.ImportNode(inElement, True)
                        inlineElementPlaceholders.Add(inlineElement.Identifier, importedNode)
                    End If

                    If importedNode IsNot Nothing Then
                        childElement.ParentNode.ReplaceChild(importedNode, childElement)
                        If Not (inlineElements.ContainsKey(inlineElement.Identifier)) Then
                            inlineElements.Add(inlineElement.Identifier, New Tuple(Of InlineElement, Boolean)(inlineElement, False))
                        End If
                    End If
                Next
            End Using
        End Sub

        Private Function IsPopupInlineElement(inlineElementTemplate As String) As Boolean
            If String.IsNullOrEmpty(inlineElementTemplate) Then Return False
            If _inlineTemplates IsNot Nothing AndAlso _inlineTemplates.Any(Function(t) t.Key.Equals("popup", StringComparison.InvariantCultureIgnoreCase) AndAlso t.Value.Equals(inlineElementTemplate, StringComparison.InvariantCultureIgnoreCase)) Then
                Return True
            End If
            Return False
        End Function

        Private Function GetInlineElementByIdentifier(xmlElement As XmlElement, identifier As String) As XmlElement
            Dim node As XmlNode = xmlElement.SelectSingleNode($"//*[@id=""{identifier}""]")

            If (node IsNot Nothing) Then
                Return DirectCast(node, XmlElement)
            End If
            Return Nothing
        End Function

        Public Function GetInlineMediaTemplate(mediaType As String) As String
            Dim result As String = InlineMediaTemplateHelper.GetInlineMediaTemplate(_inlineTemplates, mediaType, _templateNames)
            If Not String.IsNullOrEmpty(result) Then
                Return result
            End If

            Throw New ArgumentException($"Failed to get name of inline {mediaType} template.")
        End Function

        Public Shared Function CreateXmlNamespaceManager(nameTable As XmlNameTable) As XmlNamespaceManager
            Dim namespaceManager = New XmlNamespaceManager(nameTable)
            namespaceManager.AddNamespace("def", "http://www.w3.org/1999/xhtml")
            namespaceManager.AddNamespace("cito", "http://www.cito.nl/citotester")

            Return namespaceManager
        End Function
    End Class
End Namespace