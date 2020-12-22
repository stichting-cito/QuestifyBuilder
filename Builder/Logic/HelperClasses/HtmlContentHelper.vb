Imports System.IO
Imports System.Xml
Imports System.Text.RegularExpressions
Imports Cito.Tester.Common
Imports System.Text
Imports System.Linq
Imports System.Windows
Imports System.Xml.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions

Public Class HtmlContentHelper


    Public Const AnchorPopupInlineStyle As String = "<style type=""text/css""> a[popuppar] {{border-color: blue; border-style: dotted; border-width: 1px;}} </style>"

    Public Enum PasteFrom
        Word
        Excel
        Html
        OtherXHTMLEditor
        Other
    End Enum

    Public Function GiveResourceElementsContextNumber(ByVal bodyInnerXML As String, ByVal contextIdentifier As Integer?) As String
        Dim xDoc As XDocument
        Using strReader As New StringReader("<html>" + bodyInnerXML + "</html>")
            xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
        End Using

        RecursiveAddContextToResourceURL(xDoc, contextIdentifier)

        Return xDoc.Descendants("html").First().InnerXml()
    End Function

    Public Function RemoveResourceElementContextNumber(ByVal html As String) As String
        Dim xDoc As XDocument = XDocument.Parse(html, LoadOptions.PreserveWhitespace)

        RecursiveRemoveContextFromResourceURL(xDoc)

        Return xDoc.Root.OuterXml
    End Function

    Friend Function RemoveFontTags(html As String) As String
        Dim regEx As New Regex("<font\b[^>]*>.*?</font>", RegexOptions.IgnoreCase)
        Dim result As String = html
        Dim match As Match = regEx.Match(result)

        Do While match.Success
            Dim tempDoc As New XmlDocument()
            tempDoc.LoadXml(match.Value)
            result = result.Replace(match.Value, tempDoc.DocumentElement.InnerXml)
            match = match.NextMatch()
        Loop

        Return result
    End Function

    Public Function RemoveColGroupFromTables(ByRef node As XmlNode, ByVal namespaceManager As XmlNamespaceManager) As Boolean
        Dim aColgroupRemoved As Boolean = False
        Dim tableNodes As XmlNodeList = node.SelectNodes("ancestor-or-self::def:table", namespaceManager)

        For Each tableNode As XmlElement In tableNodes
            Dim colgroupNode As Xml.XmlNode = tableNode.SelectSingleNode("descendant::def:colgroup", namespaceManager)
            If colgroupNode IsNot Nothing Then
                tableNode.RemoveChild(colgroupNode)
                aColgroupRemoved = True
            End If
        Next

        Return aColgroupRemoved
    End Function

    Private Sub RecursiveAddContextToResourceURL(ByVal xDoc As XDocument, ByVal contextIdentifier As Integer?)
        Dim resourceTags As IEnumerable(Of XElement) = xDoc.Descendants().Where(Function(d) (d.Value IsNot Nothing AndAlso d.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase)) _
                                                                                            OrElse (d.Attributes IsNot Nothing AndAlso d.Attributes().Any(Function(a) a.Value IsNot Nothing AndAlso a.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase))))
        For Each resourceTag As XElement In resourceTags
            RecursiveAddContextToResourceURL(resourceTag, contextIdentifier)
        Next
    End Sub

    Private Sub RecursiveAddContextToResourceURL(ByVal xElem As XElement, ByVal contextIdentifier As Integer?)
        If xElem.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase) Then
            xElem.Value = AddContextToResourceURL(xElem.Value, contextIdentifier)
        End If

        If xElem.Attributes IsNot Nothing Then
            Dim resourceTags As IEnumerable(Of XAttribute) = xElem.Attributes().Where(Function(a) a.Value IsNot Nothing AndAlso a.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase))
            For Each resourceTag As XAttribute In resourceTags
                resourceTag.Value = AddContextToResourceURL(resourceTag.Value, contextIdentifier)
            Next
        End If
    End Sub

    Private Function AddContextToResourceURL(url As String, ByVal contextIdentifier As Integer?) As String
        Dim newUri As New UriBuilder(url)
        If contextIdentifier.HasValue Then newUri.Port = contextIdentifier.Value Else newUri.Port += 1
        If newUri.Port <= 0 Then newUri.Port = 1
        Return newUri.ToString()
    End Function

    Private Sub RecursiveRemoveContextFromResourceURL(ByVal xDoc As XDocument)
        Dim resourceTags As IEnumerable(Of XElement) = xDoc.Descendants().Where(Function(d) (d.Value IsNot Nothing AndAlso d.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase)) _
                                                                                            OrElse (d.Attributes IsNot Nothing AndAlso d.Attributes().Any(Function(a) a.Value IsNot Nothing AndAlso a.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase))))
        For Each resourceTag As XElement In resourceTags
            RecursiveRemoveContextFromResourceURL(resourceTag)
        Next
    End Sub

    Private Sub RecursiveRemoveContextFromResourceURL(ByVal xElem As XElement)
        If xElem.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase) Then
            xElem.Value = RemoveContextFromResourceURL(xElem.Value)
        End If

        If xElem.Attributes IsNot Nothing Then
            Dim resourceTags As IEnumerable(Of XAttribute) = xElem.Attributes().Where(Function(a) a.Value IsNot Nothing AndAlso a.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase))
            For Each resourceTag As XAttribute In resourceTags
                resourceTag.Value = RemoveContextFromResourceURL(resourceTag.Value)
            Next
        End If
    End Sub

    Private Function RemoveContextFromResourceURL(url As String) As String
        Dim newUri As New UriBuilder(url)
        newUri.Port = -1
        Return newUri.ToString()
    End Function

    Public Sub RemoveCommentOfStartAndEndOfPastedCode(ByRef element As XmlNode)
        Dim commentsToRemove As XmlNodeList = element.SelectNodes("//*/comment()")

        For Each comment As XmlComment In commentsToRemove
            If comment.InnerText.Contains("StartFragment") OrElse comment.InnerText.Contains("EndFragment") Then
                If comment.ParentNode IsNot Nothing AndAlso comment.ParentNode.ParentNode IsNot Nothing Then
                    comment.ParentNode.ParentNode.RemoveChild(comment.ParentNode)
                End If
            End If
        Next
    End Sub

    Public Function DetectPasteType(ByVal pasteCode As String, ByVal forcePlainTextPaste As Boolean) As PasteFrom
        Dim pasteType As PasteFrom = PasteFrom.Other

        If Not String.IsNullOrEmpty(pasteCode) Then
            If Not forcePlainTextPaste Then

                Dim headerRegExPattern1 As String = "Version:(?<Version>\S+)\s+" +
  "StartHTML:(?<StartHTML>\d+)\s+" +
  "EndHTML:(?<EndHTML>\d+)\s+" +
  "StartFragment:(?<StartFragment>\d+)\s+" +
  "EndFragment:(?<EndFragment>\d+)\s+" +
  "SourceURL:(?<SourceURL>\S+)"
                Dim matchesFirstTry As MatchCollection = Regex.Matches(pasteCode, headerRegExPattern1)
                Dim sourceURL As String = String.Empty
                Dim generatorString As String = String.Empty

                If matchesFirstTry.Count > 0 AndAlso matchesFirstTry(0).Groups.Count = 7 Then
                    sourceURL = matchesFirstTry(0).Groups("SourceURL").Value
                Else
                    Dim headerRegExPattern2 As String = "Version:(?<Version>\S+)\s+" +
  "StartHTML:(?<StartHTML>\d+)\s+" +
  "EndHTML:(?<EndHTML>\d+)\s+" +
  "StartFragment:(?<StartFragment>\d+)\s+" +
  "EndFragment:(?<EndFragment>\d+)\s+" +
  "StartSelection:(?<StartSelection>\d+)\s+" +
  "EndSelection:(?<EndSelection>\d+)\s+" +
  "SourceURL:(?<SourceURL>\S+)"

                    Dim matchesSecondTry As MatchCollection = Regex.Matches(pasteCode, headerRegExPattern2)
                    If matchesSecondTry.Count > 0 AndAlso matchesSecondTry(0).Groups.Count = 9 Then
                        sourceURL = matchesSecondTry(0).Groups("SourceURL").Value
                    Else
                        Dim generatorPattern As String = "<meta name=""?Generator""? content=(?<Generator>.+)>"
                        Dim matchesgeneratorPattern As MatchCollection = Regex.Matches(pasteCode, generatorPattern)
                        If matchesgeneratorPattern.Count > 0 Then
                            generatorString = matchesgeneratorPattern(0).Groups("Generator").Value
                        End If
                    End If
                End If

                If (sourceURL.EndsWith(".doc") OrElse sourceURL.EndsWith(".docx")) OrElse generatorString.Contains("Microsoft Word") Then
                    pasteType = PasteFrom.Word
                ElseIf (sourceURL.EndsWith(".xls") OrElse sourceURL.EndsWith(".xlsx")) OrElse generatorString.Contains("Microsoft Excel") Then
                    pasteType = PasteFrom.Excel
                ElseIf Not String.IsNullOrEmpty(sourceURL) AndAlso sourceURL.Contains(New Uri(Forms.Application.StartupPath).AbsolutePath) AndAlso String.IsNullOrEmpty(generatorString) Then
                    pasteType = PasteFrom.OtherXHTMLEditor
                Else
                    pasteType = PasteFrom.Other
                End If
            Else
                pasteType = PasteFrom.Other
            End If
        End If

        Return pasteType
    End Function

    Public Function CleanWordOrExcelHtml(ByVal html As String) As String
        Dim sc As New List(Of String)

        sc.Add("<!--(\w|\W)+?-->")
        sc.Add("<!(\w|\W)+?>")
        sc.Add("<title>(\w|\W)+?</title>")
        sc.Add("\s?class=\w+")
        sc.Add("\s?name=""\w+""")
        sc.Add("\s+style='[^']+'")
        sc.Add("<(meta|link|/?o:|/?style|/?div|/?st\d|/?head|/?html|/?span\[)[^>]*?>")
        sc.Add("<(h1|/?h1|h2|/?h2|h3|/?h3|h4|/?h4|h5|/?h5|h6|/?h6|a|/?a!\[)[^>]*?>")
        sc.Add("(<[^>]+>)+&nbsp;(</\w+>)+")
        sc.Add("\s+v:\w+=""[^""]+""")
        sc.Add("\s+x:\w+=""[^""]+""")
        sc.Add("\s+x:\w+")
        sc.Add("(\n\r){2,}")

        For Each s As String In sc
            html = Regex.Replace(html, s, String.Empty, RegexOptions.IgnoreCase)
        Next
        sc.Clear()
        sc.Add("<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>")
        sc.Add("<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>")
        For Each s As String In sc
            html = Regex.Replace(html, "<$1$2>", String.Empty, RegexOptions.IgnoreCase)
        Next
        html = WordPasteFixEntities(html)
        Return html
    End Function

    Private Function WordPasteFixEntities(ByVal html As String) As String
        Dim nvc As New Dictionary(Of String, String)
        nvc.Add("&nbsp;", " ")
        nvc.Add("&nbsp", " ")

        For Each key As String In nvc.Keys
            html = html.Replace(key, nvc(key))
        Next

        Return html
    End Function

    Public Function GetClipboardData() As String
        Dim pasteCode As String = String.Empty
        Dim dataObject As IDataObject = Clipboard.GetDataObject()
        If dataObject.GetDataPresent(DataFormats.Html) Then
            Using clipBoardDataStream As IO.Stream = DirectCast(dataObject.GetData("Html Format"), IO.Stream)
                If clipBoardDataStream IsNot Nothing Then
                    Dim usingEncoding As Encoding = Encoding.UTF8
                    Using reader As New IO.StreamReader(clipBoardDataStream, usingEncoding)
                        pasteCode = reader.ReadToEnd()
                    End Using
                End If
            End Using
        End If
        If String.IsNullOrEmpty(pasteCode) AndAlso dataObject.GetDataPresent(DataFormats.Text) Then
            pasteCode = dataObject.GetData(DataFormats.Text).ToString()
        End If
        Return pasteCode
    End Function

    Public Function ConvertStringIntoXmlElement(ByVal xml As String, ByVal namespaceManager As XmlNamespaceManager) As XmlElement
        Dim doc As New XmlDocument()
        Dim root As XmlElement
        If namespaceManager IsNot Nothing Then
            root = doc.CreateElement("html", namespaceManager.LookupNamespace("def"))
        Else
            root = doc.CreateElement("html")
        End If
        doc.AppendChild(root)
        If xml.Trim.StartsWith("<html") Then
            Dim xdoc = XDocument.Parse(xml, Linq.LoadOptions.PreserveWhitespace).Document
            Dim body = From d In xdoc.Descendants
                       Where d.Name.LocalName.Equals("body", StringComparison.OrdinalIgnoreCase)
            If body.Count = 1 Then
                xml = body(0).ToString(SaveOptions.DisableFormatting)
            End If
        End If
        doc.DocumentElement.InnerXml = xml
        Return doc.DocumentElement
    End Function

    Public Function CreateHtmlDoc(ByVal stylesheets As Dictionary(Of String, String), ByVal headerStyleElementContent As String, ByVal contextIdentifier As Integer?) As String
        Dim resourceProtocolPrefix As String = Constants.ResourceProtocolPrefix
        Dim styleSheetReferences As New StringBuilder()

        For Each StyleSheetName As String In stylesheets.Keys
            Dim tempUriString As String = String.Format("{0}{1}", resourceProtocolPrefix, StyleSheetName)
            Dim tempUri As New UriBuilder(tempUriString)
            If contextIdentifier.HasValue Then
                tempUri.Port = contextIdentifier.Value
            End If
            styleSheetReferences.AppendFormat("<link href=""{0}"" rel=""StyleSheet"" type=""text/css"" media=""screen""/>", tempUri.ToString())
        Next

        If headerStyleElementContent IsNot Nothing Then
            headerStyleElementContent = headerStyleElementContent.Replace("{", "{{").Replace("}", "}}")
        Else
            headerStyleElementContent = String.Empty
        End If

        Dim html As New System.Text.StringBuilder()
        html.AppendLine("<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:cito=""http://www.cito.nl/citotester"">")
        Dim metaTags As String = "<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /><meta http-equiv=""X-UA-Compatible"" content=""IE=8"" />"
        html.AppendFormat("<head>{0}{1}{2}<style type=""text/css"">{3}</style></head>{4}", metaTags, styleSheetReferences.ToString(), AnchorPopupInlineStyle, headerStyleElementContent, Environment.NewLine)
        html.AppendLine("<body style=""padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;"">{0}</body>")
        html.AppendLine("</html>")
        Return html.ToString
    End Function

    Public Sub PlaceUnderlineTagOutsideSpanTag(ByRef xmlDoc As XmlDocument, ByVal namespaceManager As XmlNamespaceManager)
        For Each elementReference As Xml.XmlNode In xmlDoc.SelectNodes(String.Format("//def:span[@cito_type='reference' and @cito_reftype='{0}']", XhtmlReferenceType.Element), namespaceManager)
            If elementReference.HasChildNodes AndAlso elementReference.FirstChild.Name = "u" Then
                Dim elementReferenceText As String = elementReference.FirstChild.InnerXml
                elementReference.RemoveChild(elementReference.FirstChild)
                Dim newElementReference As XmlElement = elementReference.OwnerDocument.CreateElement("u", "http://www.w3.org/1999/xhtml")
                Dim clonedElementReference As XmlNode = elementReference.CloneNode(True)
                clonedElementReference.InnerXml = elementReferenceText
                newElementReference.AppendChild(clonedElementReference)
                elementReference.ParentNode.ReplaceChild(newElementReference, elementReference)
            End If
        Next
    End Sub

End Class
