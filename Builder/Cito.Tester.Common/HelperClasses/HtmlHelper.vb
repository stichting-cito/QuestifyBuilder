Imports System.Xml

Public Class HtmlHelper

    Public Shared Function IsEmptyHtml(htmlString As String) As Boolean
        Dim doc As New XHtmlDocument

        Dim rootElement As XmlElement = doc.CreateElement("root")
        rootElement.InnerXml = htmlString
        doc.AppendChild(rootElement)

        Dim nodes As New ArrayList
        For Each node As XmlNode In doc.SelectSingleNode("/root").ChildNodes
            nodes.Add(node.CloneNode(True))
        Next
        Return TemplateHelper.IsXHtmlParameterEmpty(DirectCast(nodes.ToArray(GetType(XmlNode)), XmlNode()))
    End Function

End Class
