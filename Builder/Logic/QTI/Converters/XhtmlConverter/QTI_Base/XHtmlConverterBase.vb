Imports System.Text.RegularExpressions
Imports System.Xml
Imports Cito.Tester.Common

Namespace QTI.Converters.XhtmlConverter.QTI_Base

    Public Class XHtmlConverterBase

        Protected Sub FixParagraphsInParagraph(ByRef doc As XHtmlDocument)
            If doc IsNot Nothing Then
                Dim listOfParagraphsToDelete As New List(Of XmlNode)
                Dim nodeCollection As XmlNodeList = doc.DocumentElement.SelectNodes("//p")
                If nodeCollection IsNot Nothing Then
                    For Each paragraphNode As XmlNode In nodeCollection
                        If CanBeDeleted(paragraphNode) Then
                            listOfParagraphsToDelete.Add(paragraphNode)
                        End If
                    Next
                End If
                For i As Integer = 1 To listOfParagraphsToDelete.Count
                    Dim paragraph As XmlNode = listOfParagraphsToDelete(listOfParagraphsToDelete.Count - i)

                    If paragraph.ParentNode IsNot Nothing Then
                        If paragraph.ChildNodes IsNot Nothing AndAlso paragraph.ChildNodes.Count > 0 Then
                            For Each nodeToAdd As XmlNode In paragraph.ChildNodes
                                paragraph.ParentNode.InsertBefore(nodeToAdd.CloneNode(True), paragraph)
                            Next
                            Dim paragraphNodeToAdd As XmlNode = doc.CreateNode("element", "p", "")
                            If paragraph.Attributes IsNot Nothing Then
                                If paragraph.Attributes("style") IsNot Nothing Then
                                    Dim styleAttr As XmlAttribute = doc.CreateAttribute("style")
                                    styleAttr.InnerText = paragraph.Attributes("style").InnerText
                                    paragraphNodeToAdd.Attributes.Append(styleAttr)
                                End If
                                If paragraph.Attributes("class") IsNot Nothing Then
                                    Dim classAttr As XmlAttribute = doc.CreateAttribute("class")
                                    classAttr.InnerText = paragraph.Attributes("class").InnerText
                                    paragraphNodeToAdd.Attributes.Append(classAttr)
                                End If
                            End If

                            paragraph.ParentNode.InsertBefore(paragraphNodeToAdd.CloneNode(True), paragraph)
                        End If
                        If (paragraph.ChildNodes IsNot Nothing AndAlso paragraph.ChildNodes.Count > 0) OrElse _
                           (paragraph.ParentNode IsNot Nothing AndAlso paragraph.ParentNode.Name.Equals("p", StringComparison.InvariantCultureIgnoreCase)) Then
                            paragraph.ParentNode.RemoveChild(paragraph)
                        End If
                    End If
                Next
            End If
        End Sub

        Protected Sub FixC1ParagrapId(ByRef doc As XHtmlDocument)
            If doc IsNot Nothing Then
                Dim nodeCollection As XmlNodeList = doc.DocumentElement.SelectNodes("//*[starts-with(@id,'c1')]")
                If nodeCollection IsNot Nothing Then
                    For Each nodeWithc1Id As XmlNode In nodeCollection
                        nodeWithc1Id.Attributes.RemoveNamedItem("id")
                    Next
                End If
            End If
        End Sub

        Protected Sub AddInlineCssToNode(node As XmlNode, value As String, doc As XHtmlDocument)
            If node.Attributes("style") Is Nothing Then
                Dim attrToAdd As XmlAttribute = doc.CreateAttribute("style")
                node.Attributes.Append(attrToAdd)
            End If
            Dim newvalue As String = String.Empty
            Dim regexToCheckIfStyleAlreayExists As String = $"{Regex.Match(value, ".+?:").Value.Replace(":"c, String.Empty)}.+?;"
            If Not Regex.Match(node.Attributes("style").Value, regexToCheckIfStyleAlreayExists, RegexOptions.IgnoreCase).Success Then
                newvalue = String.Concat(node.Attributes("style").Value, If(node.Attributes("style").Value.EndsWith(";"), " ", "; "), value).Trim
            Else
                newvalue = Regex.Replace(node.Attributes("style").Value, regexToCheckIfStyleAlreayExists, value, RegexOptions.IgnoreCase)
            End If
            node.Attributes("style").Value = newvalue
        End Sub

        Private Function CanBeDeleted(paragraphNode As XmlNode) As Boolean
            Dim returnValue As Boolean = True
            If paragraphNode.ChildNodes IsNot Nothing Then
                For Each paragraphChildNode As XmlNode In paragraphNode.ChildNodes
                    If (TypeOf paragraphChildNode Is XmlText AndAlso Not String.IsNullOrEmpty(paragraphChildNode.InnerText.Trim)) OrElse
                       (Not TypeOf paragraphChildNode Is XmlText AndAlso
                        Not TypeOf paragraphChildNode Is XmlComment AndAlso
                        (paragraphChildNode.Name.ToLower <> "p" AndAlso paragraphChildNode.Name.ToLower <> "div" AndAlso paragraphChildNode.Name.ToLower <> "cito:inlineelement")) Then
                        returnValue = False
                        Exit For
                    ElseIf paragraphNode.ParentNode Is Nothing OrElse
                           Not (paragraphNode.ParentNode.Name.ToLower = "p" OrElse paragraphNode.ParentNode.Name.ToLower = "div") Then
                        returnValue = False
                        Exit For
                    End If
                Next
            End If
            Return returnValue
        End Function

    End Class
End NameSpace