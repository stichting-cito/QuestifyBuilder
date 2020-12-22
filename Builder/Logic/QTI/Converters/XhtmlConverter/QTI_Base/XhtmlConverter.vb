Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.XhtmlConverter.QTI_Base

    Public Class QTIXhtmlConverter
        Inherits XHtmlConverterBase
        Implements IXhtmlConverter, IDisposable

        Private ReadOnly _convertionAttributeDictionary As New Dictionary(Of String, String)
        Private ReadOnly _convertionElementsDictionary As New Dictionary(Of String, String)

        Private _uniqueId As String
        Const XPATH_STRIP As String = "node(){0}"
        Const EXCLUDE_TAGS As String = "[name()!=""{0}""]"
        Const GET_ALLOWED_TAG As String = "[name()=""{0}""]"
        Const XPATH_STYLES As String = "//*[@style]"
        Const XPATH_STYLES_POPUP As String = "//div[@id[starts-with(.,'POPCT_')]]//*[@style]"
        Const GENERATED_CLASSNAME As String = "cito_genclass"
        Const GENERATED_CLASSNAME_NO_CANVAS As String = "cito_genclass_not_in_canvas"

        Private Property DeletedElements As New List(Of String)
        Private Property DeletedAttributes As New List(Of String)

        Public Sub New()
        End Sub

        Public Sub Initialise(uniqueId As String) Implements IXhtmlConverter.Initialise
            SetUniqueId(uniqueId)

            InitialiseAttributesToStyle()
            InitialiseElementsToStyle()
        End Sub

        Private Sub SetUniqueId(uniqueId As String)
            _uniqueId = uniqueId.Replace(" ", "_").Replace(".", "_")
        End Sub

        Public Function ConvertXhtmlToQti(xHtml As String, checkRoot As Boolean) As String Implements IXhtmlConverter.ConvertXhtmlToQti
            Dim htm = xHtml
            htm = ExecuteRegex(htm)

            htm = FixMissingParagraph(htm)

            htm = $"<dummytag>{htm}</dummytag>"
            Dim doc As New XHtmlDocument
            doc.PreserveWhitespace = True
            doc.LoadXml(htm)

            Dim nsmgr As System.Xml.XmlNamespaceManager = New System.Xml.XmlNamespaceManager(doc.NameTable)
            nsmgr.AddNamespace("htm", "http://www.w3.org/1999/htm")
            nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")
            nsmgr.AddNamespace("m", "http://www.w3.org/1998/Math/MathML")

            FixImageTag(doc)

            FixTableTag(doc)

            FixParagraphsInParagraph(doc)

            FixC1ParagrapId(doc)

            ConvertHtmlStyleToInlineCss(doc, doc.DocumentElement)

            StripAllUnknownTags(doc.DocumentElement, checkRoot)
            RemoveIllegalParentNodesFromMediaInteraction(doc)

            Return doc.DocumentElement.InnerXml
        End Function

        Protected Overridable Sub RemoveIllegalParentNodesFromMediaInteraction(ByRef doc As XHtmlDocument)
            For Each mediaInteractionNode As XmlNode In doc.SelectNodes("//mediaInteraction")
                RemoveIllegalParentNodesOfMediaInteractionControl(mediaInteractionNode.ParentNode)
            Next
        End Sub

        Protected Sub RemoveIllegalParentNodesOfMediaInteractionControl(ByRef node As XmlNode)
            Dim parentNode As XmlNode = node.ParentNode

            If IsIllegalParentNodeOfMediaInteractionControl(node) Then
                RemoveNode(node)
            End If

            If parentNode IsNot Nothing Then
                RemoveIllegalParentNodesOfMediaInteractionControl(parentNode)
            End If
        End Sub

        Private Sub RemoveNode(ByRef node As XmlNode)
            If node.ParentNode IsNot Nothing Then
                Dim xmlBelowNodeToDelete As String = node.InnerXml

                node.ParentNode.InnerXml = xmlBelowNodeToDelete
            End If
        End Sub

        Private Function IsIllegalParentNodeOfMediaInteractionControl(ByVal node As XmlNode) As Boolean
            Dim stylingElements As New List(Of String)({"strong", "b", "i", "em", "u", "span"})

            Return stylingElements.Contains(node.Name.ToLower())
        End Function

        Public Function ConvertStylesToCss(xmlDoc As XmlDocument, ByRef css As String) As Boolean Implements IXhtmlConverter.ConvertStylesToCss
            Dim cssStylesAdded As Boolean = False
            Dim cssStringBuilder As New StringBuilder(css)

            ExtractAllStylesFromPopUpControls(xmlDoc, cssStringBuilder, cssStylesAdded)
            ExtractAllStyles(xmlDoc, cssStringBuilder, cssStylesAdded)

            css = cssStringBuilder.ToString

            Return cssStylesAdded
        End Function

        Private Sub InitialiseAttributesToStyle()
            _convertionAttributeDictionary.Add("width", "width: {0}px;")
            _convertionAttributeDictionary.Add("heigth", "heigth: {0}px;")
            _convertionAttributeDictionary.Add("border", "border: {0}px solid black;")
            _convertionAttributeDictionary.Add("bgcolor", "background-color: {0};")
            _convertionAttributeDictionary.Add("align", "text-align: {0};")
            _convertionAttributeDictionary.Add("valign", "vertical-align: {0};")
            _convertionAttributeDictionary.Add("cellpadding", "padding: {0};")
            _convertionAttributeDictionary.Add("dir", "direction: {0};")
            _convertionAttributeDictionary.Add("cellspacing", "border-spacing: {0}px;")
        End Sub

        Private Sub InitialiseElementsToStyle()
            _convertionElementsDictionary.Add("i", "font-style:italic;")
            _convertionElementsDictionary.Add("u", "text-decoration:underline;")
            _convertionElementsDictionary.Add("b", "font-weight: bold;")
        End Sub

        Private Function ExecuteRegex(xHtml As String) As String
            Dim result As String = xHtml
            result = ChainHandlerHelper.RemoveNamespaces(result, Nothing, True)

            Regex.Replace(result, "<([^>]*)(?:lang|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase)
            Regex.Replace(result, "<([^>]*)(?:lang|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase)

            Return result
        End Function

        Private Sub ConvertHtmlStyleToInlineCss(doc As XHtmlDocument, node As XmlNode)
            For Each childNode As XmlNode In node.ChildNodes
                If ShouldConvertToInlineCss(childNode.Name) AndAlso Not IsInlineElement(node) Then
                    ConvertTagAndAttributesToStyle(doc, childNode)
                End If

                If childNode.ChildNodes IsNot Nothing Then
                    ConvertHtmlStyleToInlineCss(doc, childNode)
                End If
            Next
        End Sub

        Private Sub ConvertTagAndAttributesToStyle(doc As XHtmlDocument, htmlNode As XmlNode)
            Dim listOfAttibutesToDelete As New List(Of String)
            Dim listOfElementsToDelete As New List(Of XmlNode)
            Dim attributesToAdd As New List(Of String)

            For Each attr As XmlAttribute In htmlNode.Attributes
                If _convertionAttributeDictionary.ContainsKey(attr.Name) Then
                    Dim value As String = String.Format(_convertionAttributeDictionary(attr.Name), attr.Value)

                    If attr.Value.Contains("%"c) Then
                        value = value.Replace("px", String.Empty)
                    End If

                    attributesToAdd.Add(value)
                    listOfAttibutesToDelete.Add(attr.Name)
                End If

                If (htmlNode.Name = "ol" AndAlso attr.Name = "type" AndAlso attr.Value = "I") Then
                    attributesToAdd.Add("list-style-type: upper-roman;")
                End If
            Next

            For Each styleValueToAdd As String In attributesToAdd
                AddInlineCssToNode(htmlNode, styleValueToAdd, doc)
            Next

            For Each attributeToDelete As String In listOfAttibutesToDelete
                htmlNode.Attributes.RemoveNamedItem(attributeToDelete)
            Next

            For Each tagtoConvert As String In _convertionElementsDictionary.Keys
                Dim nodeCollectionToConvert As XmlNodeList = htmlNode.SelectNodes(String.Format(XPATH_STRIP, String.Format(GET_ALLOWED_TAG, tagtoConvert)))

                If nodeCollectionToConvert IsNot Nothing AndAlso Not nodeCollectionToConvert.Count = 0 Then
                    For Each nodeToConvert As XmlNode In nodeCollectionToConvert
                        listOfElementsToDelete.Add(nodeToConvert)
                    Next

                    For Each nodeToDelete As XmlNode In listOfElementsToDelete
                        If nodeToDelete.ParentNode IsNot Nothing Then
                            Dim newElement As XmlElement

                            If nodeToDelete.ChildNodes.Cast(Of XmlNode)().ToList.Where(Function(node) node.LocalName = "div").Count = 0 Then
                                newElement = doc.CreateElement("span")
                            Else
                                newElement = doc.CreateElement("div")
                            End If

                            For x As Integer = 0 To nodeToDelete.ChildNodes.Count - 1
                                If nodeToDelete.ChildNodes(x) IsNot Nothing AndAlso Not TypeOf nodeToDelete.ChildNodes(x) Is System.Xml.XmlWhitespace Then
                                    Dim clonedNode As XmlNode = nodeToDelete.ChildNodes(x).CloneNode(True)
                                    newElement.AppendChild(clonedNode)
                                End If
                            Next

                            Dim value As String = _convertionElementsDictionary(tagtoConvert)
                            AddInlineCssToNode(newElement, value, doc)
                            nodeToDelete.ParentNode.ReplaceChild(newElement, nodeToDelete)
#If DEBUG Then
                            DeletedElements.Add($" converted: {nodeToDelete.Name} innerhtml: {nodeToDelete.InnerXml}")
#End If
                        End If
                    Next
                End If
            Next

        End Sub

        Protected Overridable Sub StripAllUnknownTags(dummyElement As XmlNode, checkRoot As Boolean)
            Dim listOfAllowedRootTags As String = "responseDeclaration|outcomeDeclaration|stylesheet|itemBody|responseProcessing"
            Dim listOfAllowedTags As String = "positionObjectStage|gapMatchInteraction|matchInteraction|graphicGapMatchInteraction|hotspotInteraction|graphicOrderInteraction|hottextInteraction|" &
                                              "selectPointInteraction|graphicAssociateInteraction|sliderInteraction|choiceInteraction|customInteraction|mediaInteraction|orderInteraction|extendedTextInteraction|" &
                                              "associateInteraction|uploadInteraction|pre|h2|h3|h1|h6|h4|h5|p|address|dl|ol|hr|rubricBlock|blockquote|feedbackBlock|ul|table|div|xi:include|m:math|&nbsp;"


            If checkRoot Then
                RemoveUnsuppportedElements(dummyElement, listOfAllowedRootTags)

                For Each childNode As XmlNode In dummyElement.ChildNodes
                    If childNode.LocalName.ToString.ToLower = "itembody" Then
                        RemoveUnsuppportedElements(childNode, listOfAllowedTags)

                        For Each node As XmlNode In childNode.ChildNodes
                            CleanupChildNodes(node)
                        Next
                    End If
                Next
            Else
                RemoveUnsuppportedElements(dummyElement, listOfAllowedTags)

                For Each childNode As XmlNode In dummyElement.ChildNodes
                    CleanupChildNodes(childNode)
                Next
            End If
        End Sub

        Protected Sub CleanupChildNodes(node As XmlNode)
            If Not IsInlineElement(node) Then
                Dim supportedAttributes As String = GetSupportedAttributes(node.Name)
                Dim supportedTags As String = GetSupportedElements(node.Name)

                If Not String.IsNullOrEmpty(supportedTags) OrElse Not String.IsNullOrEmpty(supportedAttributes) Then
                    RemoveUnsupportedAttributes(node, supportedAttributes)
                    RemoveUnsuppportedElements(node, supportedTags)
                End If

                If node.ChildNodes IsNot Nothing Then
                    For Each childNode As XmlNode In node.ChildNodes
                        CleanupChildNodes(childNode)
                    Next
                End If
            End If
        End Sub

        Protected Sub RemoveUnsuppportedElements(node As XmlNode, supportedElements As String)
            Dim xpathBuilderForChildNodes As New StringBuilder(String.Empty)

            For Each allowedChildTag As String In supportedElements.Split("|"c)
                xpathBuilderForChildNodes.AppendFormat(EXCLUDE_TAGS, allowedChildTag)
            Next

            If Not supportedElements.Contains("*") Then
                Dim notSupportedChildTagCollection As XmlNodeList = node.SelectNodes(String.Format(XPATH_STRIP, xpathBuilderForChildNodes.ToString))
                Dim listOfNotSupportedElements As New List(Of XmlNode)

                If notSupportedChildTagCollection IsNot Nothing Then
                    For Each unSupportedTag As XmlNode In notSupportedChildTagCollection
                        If Not IsInlineElement(unSupportedTag) _
                           AndAlso Not TypeOf unSupportedTag Is XmlText _
                           AndAlso Not TypeOf unSupportedTag Is XmlComment _
                           AndAlso Not TypeOf unSupportedTag Is System.Xml.XmlWhitespace OrElse IsEmptyText(unSupportedTag) _
                            Then
                            listOfNotSupportedElements.Add(unSupportedTag)
#If DEBUG Then
                            DeletedElements.Add($"{unSupportedTag.Name} innerhtml: {unSupportedTag.InnerXml}")
#End If
                        End If
                    Next
                End If

                For Each nodeToDelete As XmlNode In listOfNotSupportedElements
                    If TypeOf nodeToDelete Is System.Xml.XmlWhitespace AndAlso nodeToDelete.ParentNode.Name.ToLower.Equals("p") Then
                    Else
                        nodeToDelete.ParentNode.RemoveChild(nodeToDelete)
                    End If
                Next
            End If
        End Sub

        Private Function IsEmptyText(node As XmlNode) As Boolean
            Dim returnValue As Boolean = False

            If TypeOf (node) Is XmlText AndAlso node.InnerText.ToString.Trim = String.Empty AndAlso
               node.ParentNode IsNot Nothing AndAlso node.ParentNode.Name.ToLower = "dummytag" Then
                returnValue = True
            End If

            Return returnValue
        End Function

        Private Sub RemoveUnsupportedAttributes(node As XmlNode, supportedAttributes As String)
            Dim listOfAttributes As New List(Of String)
            listOfAttributes.AddRange(supportedAttributes.Split("|"c))

            Dim listOfAttributesToRemove As New List(Of String)

            If Not listOfAttributes.Contains("*") Then
                Dim attributesStartingWith = listOfAttributes.Where(Function(a) a.EndsWith("~")).Select(Function(a) a.Substring(0, Len(a) - 1))
                For Each attr As XmlAttribute In node.Attributes
                    If Not listOfAttributes.Contains(attr.Name, StringComparer.OrdinalIgnoreCase) AndAlso Not String.Compare(attr.Name, "style", StringComparison.OrdinalIgnoreCase) = 0 AndAlso Not attributesStartingWith.Any(Function(a) attr.Name.StartsWith(a)) Then
                        listOfAttributesToRemove.Add(attr.Name)
#If DEBUG Then
                        DeletedAttributes.Add($"{attr.Name} text: {attr.Value}")
#End If
                    End If
                Next

                For Each attributeToRemove As String In listOfAttributesToRemove
                    node.Attributes.RemoveNamedItem(attributeToRemove)
                Next
            End If
        End Sub

        Protected Overridable Function GetSupportedElements(elementName As String) As String
            Dim returnValue As String = "*"

            Select Case elementName.ToLower
                Case "h1", "h2", "h3", "h4", "h5", "h6", "p", "span"
                    returnValue = "textEntryInteraction|inlineChoiceInteraction|endAttemptInteraction|hottext|img|br|printedVariable|object|gap|em|a|code|span|sub|acronym|big" &
                                  "|tt|kbd|q|i|dfn|feedbackInline|abbr|strong|sup|var|small|samp|b|cite|xi:include|m:math|hottext"
                Case "ol"
                    returnValue = "li"
                Case "td", "div", "li"
                    returnValue = "pre|h2|h3|h1|h6|h4|h5|p|address|dl|ol|img|br|ul|hr|printedVariable|object|rubricBlock|blockquote|feedbackBlock" &
                                  "|hottext|em|a|code|span|sub|acronym|big|tt|kbd|q|i|dfn|feedbackInline|abbr|strong|sup|var|small|samp|b|cite|table" &
                                  "|div|xi:include|m:math|textEntryInteraction|inlineChoiceInteraction|endAttemptInteraction|customInteraction|gapMatchInteraction" &
                                  "|matchInteraction|graphicGapMatchInteraction|hotspotInteraction|graphicOrderInteraction|selectPointInteraction|graphicAssociateInteraction" &
                                  "|sliderInteraction|choiceInteraction|mediaInteraction|orderInteraction|extendedTextInteraction|associateInteraction|hottextInteraction|positionObjectStage" &
                                  "|uploadInteraction"
                Case "table"
                    returnValue = "caption|col|colgroup|thead|tfoot|tbody"
                Case "tbody"
                    returnValue = "tr"
                Case "colgroup"
                    returnValue = "col"
                Case "tr"
                    returnValue = "th|td"
            End Select

            Return returnValue
        End Function

        Protected Overridable Function GetSupportedAttributes(elementName As String) As String
            Dim returnValue As String = "*"

            Select Case elementName.ToLower
                Case "h1", "h2", "h3", "h4", "h5", "h6", "p", "span", "div", "pre"
                    returnValue = "class|id|label|xml:base|xml:lang|xsi:type|data-alias|data-dep-dialog~|data-stimulus~"
                Case "ol", "sup", "sub", "tbody", "tr", "ul"
                    returnValue = "class|id|label|xml:base|xml:lang|xsi:type"
                Case "li"
                    returnValue = "class|id|label|xml:base|xml:lang|xsi:type|data-value"
                Case "img"
                    returnValue = "alt|src|height|width|longdesc|class|id|label|xml:base|xml:lang|xsi:type"
                Case "td"
                    returnValue = "abbr|axis|colspan|rowspan|scope|headers|class|id|label|header|xml:lang|xsi:type"
                Case "table"
                    returnValue = "summary|class|id|label|xml:base|xml:lang|xsi:type"
                Case "col", "colgroup"
                    returnValue = "class|id|label|span|xml:lang|xsi:type"
                Case "mediainteraction"
                    returnValue = "class|id|label|span|responseidentifier|autostart|maxplays|xml:lang|xsi:type"
            End Select

            Return returnValue
        End Function

        Private Sub ExtractAllStylesFromPopUpControls(ByRef doc As XmlDocument, cssStringBuilder As StringBuilder, ByRef cssStylesAdded As Boolean)
            ExtractAllStyles(doc, cssStringBuilder, cssStylesAdded, XPATH_STYLES_POPUP, GENERATED_CLASSNAME_NO_CANVAS)
        End Sub

        Private Sub ExtractAllStyles(ByRef doc As XmlDocument, cssStringBuilder As StringBuilder, ByRef cssStylesAdded As Boolean, Optional searchPattern As String = XPATH_STYLES, Optional generatedClassName As String = GENERATED_CLASSNAME)
            If doc Is Nothing Then Return

            Dim nodeCollection As XmlNodeList = doc.DocumentElement.SelectNodes(searchPattern)
            Dim index As Integer = 0
            Dim trClassName As String = Nothing

            If nodeCollection Is Nothing Then Return

            For Each htmlNode As XmlNode In nodeCollection
                index += 1
                Dim style As String = Nothing

                If htmlNode.Attributes("style") IsNot Nothing Then
                    style = htmlNode.Attributes("style").Value.Trim()
                End If

                If Not String.IsNullOrEmpty(style) Then
                    Dim id As String = Nothing

                    If htmlNode.Attributes("id") IsNot Nothing Then
                        id = htmlNode.Attributes("id").Value
                    End If

                    If Not String.IsNullOrEmpty(id) Then
                        style = AddImportantRules(style)
                        cssStringBuilder.AppendLine($"{htmlNode.Name}[id='{id}'] {{ {style}; }}")
                        cssStylesAdded = True
                    Else
                        Dim className = $"{generatedClassName}_{_uniqueId.Replace(" ", "_")}_{index}"

                        If htmlNode.Name = "tr" Then
                            trClassName = className
                        ElseIf htmlNode.Name = "td" Then
                            If htmlNode.Attributes("class") Is Nothing Then
                                Dim attr As XmlAttribute = doc.CreateAttribute("class")
                                htmlNode.Attributes.Append(attr)
                            End If

                            style = AddNoLineBorderAttributes(style)
                            htmlNode.Attributes("class").Value = String.Concat(htmlNode.Attributes("class").Value, " ", trClassName, " ", className).Trim
                            cssStylesAdded = True
                        Else
                            If htmlNode.Attributes("class") Is Nothing Then
                                Dim attr As XmlAttribute = doc.CreateAttribute("class")
                                htmlNode.Attributes.Append(attr)
                            End If

                            htmlNode.Attributes("class").Value = String.Concat(htmlNode.Attributes("class").Value, " ", className).Trim
                        End If

                        style = AddImportantRules(style)
                        AddStyleToCss(cssStringBuilder, className, style)
                        cssStylesAdded = True
                    End If

                    htmlNode.Attributes.RemoveNamedItem("style")
                End If
            Next
        End Sub

        Private Function AddNoLineBorderAttributes(ByVal styleString As String) As String
            Dim newStyle As New StringBuilder(styleString)
            Dim styles As String() = styleString.Split(";".ToCharArray())
            Dim borderLeftWidthExists As Boolean = False
            Dim borderTopWidthExists As Boolean = False
            Dim borderRightWidthExists As Boolean = False
            Dim borderBottomWidthExists As Boolean = False

            For Each style As String In styles
                If style.Trim().StartsWith("border-left-width", StringComparison.CurrentCultureIgnoreCase) OrElse style.Trim().StartsWith("border-left", StringComparison.CurrentCultureIgnoreCase) Then borderLeftWidthExists = True
                If style.Trim().StartsWith("border-top-width", StringComparison.CurrentCultureIgnoreCase) OrElse style.Trim().StartsWith("border-top", StringComparison.CurrentCultureIgnoreCase) Then borderTopWidthExists = True
                If style.Trim().StartsWith("border-right-width", StringComparison.CurrentCultureIgnoreCase) OrElse style.Trim().StartsWith("border-right", StringComparison.CurrentCultureIgnoreCase) Then borderRightWidthExists = True
                If style.Trim().StartsWith("border-bottom-width", StringComparison.CurrentCultureIgnoreCase) OrElse style.Trim().StartsWith("border-bottom", StringComparison.CurrentCultureIgnoreCase) Then borderBottomWidthExists = True
            Next

            If Not styleString.EndsWith(";") Then newStyle.Append(";")
            If Not borderLeftWidthExists Then newStyle.Append(" border-left-width: 0px;")
            If Not borderTopWidthExists Then newStyle.Append(" border-top-width: 0px;")
            If Not borderRightWidthExists Then newStyle.Append(" border-right-width: 0px;")
            If Not borderBottomWidthExists Then newStyle.Append(" border-bottom-width: 0px;")

            Return newStyle.ToString()
        End Function

        Private Function AddImportantRules(style As String) As String
            Dim result As String = style
            If result.Contains(":") AndAlso Not result.EndsWith(";") Then result = $"{result};"
            If result.Contains(";") Then
                result = result.Replace(";", "!important;")

                result = result.Replace("!important!important;", "!important;")
                result = result.Replace("!important; !important;", "!important;")
            End If
            Return result
        End Function

        Private Sub FixImageTag(ByRef doc As XHtmlDocument)
            If doc Is Nothing Then Return

            Dim nodeCollection As XmlNodeList = doc.DocumentElement.SelectNodes("//*[name()=""img""]")
            If nodeCollection Is Nothing Then Return

            For Each imageNode As XmlNode In nodeCollection

                If imageNode.Attributes Is Nothing OrElse imageNode.Attributes("alt") Is Nothing Then
                    Dim attr As XmlAttribute = doc.CreateAttribute("alt")
                    imageNode.Attributes.Append(attr)
                End If

                If imageNode.Attributes Is Nothing OrElse imageNode.Attributes("id") Is Nothing Then
                    Dim attr As XmlAttribute = doc.CreateAttribute("id")
                    attr.Value = $"IMG-{Guid.NewGuid.ToString()}"
                    imageNode.Attributes.Append(attr)
                End If

                imageNode.Attributes("id").Value = $"Id-{imageNode.Attributes("id").Value}"
            Next
        End Sub

        Private Sub FixTableTag(ByRef doc As XHtmlDocument)
            If doc Is Nothing Then Return

            Dim nodeCollection As XmlNodeList = doc.DocumentElement.SelectNodes("//table")
            If nodeCollection Is Nothing Then Return

            For Each tableNode As XmlNode In nodeCollection
                If tableNode.SelectNodes("//tbody") Is Nothing OrElse tableNode.SelectNodes("//tbody").Count = 0 Then
                    Dim clonedTableNode As XmlNode = tableNode.CloneNode(True)
                    RemoveAllchildNodesFromNode(tableNode)

                    Dim tbodyNode As XmlNode = doc.CreateElement("tbody")

                    For Each tableNodeToAdd As XmlNode In clonedTableNode.ChildNodes.Cast(Of XmlNode).ToList()
                        tbodyNode.AppendChild(tableNodeToAdd)
                    Next

                    tableNode.AppendChild(tbodyNode)
                End If
            Next
        End Sub

        Private Sub RemoveAllchildNodesFromNode(xmlNode As XmlNode)
            If xmlNode.ChildNodes IsNot Nothing Then
                For i As Integer = 1 To xmlNode.ChildNodes.Count
                    xmlNode.RemoveChild(xmlNode.ChildNodes(0))
                Next
            End If
        End Sub

        Private Function FixMissingParagraph(ByVal xHtml As String) As String
            Dim result As String = xHtml
            If result.Trim.Length = 0 OrElse (Not result.Trim.Length = 0 AndAlso (Not (result.Trim.Substring(0, 1) = "<") OrElse (result.Trim.Length > 5 AndAlso result.Trim.Substring(0, 5) = "<span") OrElse (result.Trim.Length > 4 AndAlso result.Trim.Substring(0, 4) = "<img"))) Then
                result = $"<p>{result}</p>"
            End If

            Return result
        End Function

        Private Sub AddStyleToCss(cssStringBuilder As StringBuilder, className As String, styles As String)
            Dim formatCss As String = ".{0}{1}{{{1}{2}{1}}}"

            Dim s As String = styles.Replace(";", $";{vbNewLine}")
            s = String.Format(formatCss, className, vbNewLine, s)
            cssStringBuilder.AppendLine(s)
        End Sub

        Private Function IsInlineElement(ByVal node As XmlNode) As Boolean
            Dim isInlineNode As Boolean = False
            NodeIsInlineElement(node, isInlineNode)

            If isInlineNode Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub NodeIsInlineElement(node As XmlNode, ByRef isInline As Boolean)
            If Not isInline Then

                If node.Name.ToLower = "cito:inlineelement" Then
                    isInline = True
                    Exit Sub
                End If
                If node.ParentNode IsNot Nothing Then
                    NodeIsInlineElement(node.ParentNode, isInline)
                End If
            End If
        End Sub

        Private Function ShouldConvertToInlineCss(tagname As String) As Boolean
            Dim returnValue As Boolean = False

            Select Case tagname.ToLower
                Case "h1", "h2", "h3", "h4", "h5", "h6", "p", "span", "ol", "li", "td", "table", "tbody", "tr", "colgroup", "col", "tr", "div"
                    returnValue = True
            End Select

            Return returnValue
        End Function

        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                End If

            End If
            Me.disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class
End Namespace