Imports System.IO
Imports System.Xml
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Public Class InlineElementConverter

    Public Sub ConvertInlineElementAnchorsToHtml(xml As XmlNode, target As String, resourceNeeded As EventHandler(Of ResourceNeededEventArgs))
        Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(xml.OwnerDocument.NameTable)
        nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")
        Dim controllerNodes As XmlNodeList = xml.SelectNodes("//cito:InlineElement", nsmgr)

        For Each node As XmlNode In controllerNodes
            Dim controllerId As String = String.Empty

            If node.Attributes("id") IsNot Nothing Then controllerId = node.Attributes("id").Value

            Using reader As New StringReader(node.OuterXml)
                Dim inlineElement As InlineElement = DirectCast(SerializeHelper.XmlDeserializeFromReader(reader, GetType(InlineElement)), InlineElement)
                Dim adapter As ItemLayoutAdapter = New ItemLayoutAdapter(inlineElement.LayoutTemplateSourceName, Nothing, resourceNeeded)
                If adapter.Template.Targets.Where(Function(t) t.Name.Equals(target.ToString, StringComparison.OrdinalIgnoreCase)).Count = 0 Then
                    target = adapter.Template.Targets.FirstOrDefault.Name
                End If
                AddHandler TestSessionContext.ResourceNeeded, resourceNeeded
                Dim xHtmlDocument As XHtmlDocument = Nothing
                Try
                    xHtmlDocument = adapter.ParseTemplate(target, inlineElement.Parameters, False)
                Finally
                    RemoveHandler TestSessionContext.ResourceNeeded, resourceNeeded
                End Try
                Dim newNodeList As XmlNodeList = xHtmlDocument.SelectNodes("html/*")

                If newNodeList IsNot Nothing AndAlso Not newNodeList.Count = 0 Then
                    For i As Integer = 0 To newNodeList.Count - 1
                        Dim nodeIndex As Integer = (newNodeList.Count - 1) - i
                        Dim importedNode As XmlNode = xml.OwnerDocument.ImportNode(newNodeList(nodeIndex), True)
                        node.ParentNode.InsertAfter(importedNode, node)
                    Next
                    node.ParentNode.RemoveChild(node)
                End If
            End Using
        Next
    End Sub

    Public Function ConvertInlineElementAnchorsToHtml(xmlstring As String, defaultTarget As String, resourceNeeded As EventHandler(Of ResourceNeededEventArgs)) As String
        Dim doc As New XmlDocument
        doc.LoadXml(String.Format("<dummyBody>{0}</dummyBody>", xmlstring))
        ConvertInlineElementAnchorsToHtml(doc.DocumentElement, defaultTarget, resourceNeeded)
        Return doc.DocumentElement.InnerXml
    End Function

End Class
