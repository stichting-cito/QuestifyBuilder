Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI_Base.QTICompliantHelper

    Public Class FixParagraphInTag
        Implements IModifyItemDocument

        Public Sub Modify(ByRef xmlDoc As Xml.XmlDocument, docHelper As DocumentHelper) Implements IModifyDocument.Modify
            RemoveParagraphFromTag(xmlDoc, New List(Of String)(New String() {"i", "em", "b"}), docHelper)
        End Sub

        Private Sub RemoveParagraphFromTag(xmlDoc As XmlDocument, tagsToFix As List(Of String), docHelper As DocumentHelper)
            For Each tagToFix As String In tagsToFix
                Dim nodeList As XmlNodeList = xmlDoc.SelectNodes($"//{tagToFix}/p")

                For Each node As XmlNode In nodeList
                    If node.ChildNodes Is Nothing OrElse (node.ChildNodes IsNot Nothing AndAlso TypeOf node Is XmlElement) Then

                        docHelper.ReplaceElement(node, FixParagraphInTags(node.ParentNode, docHelper).ChildNodes)
                    Else
                    End If
                Next
            Next
        End Sub

        Private Function FixParagraphInTags(tagToFix As XmlNode, docHelper As DocumentHelper) As XmlElement
            Dim nsHelper = docHelper.GetNamespaceHelper()
            If nsHelper IsNot Nothing Then
                Dim imsQtiNamespace = nsHelper.GetImsQtiNamespace()
                If imsQtiNamespace IsNot Nothing Then
                    Dim result As String = tagToFix.OuterXml
                    result = result.Replace("<p>", String.Empty)
                    result = result.Replace("</p>", String.Empty)
                    Dim tempDoc As New XmlDocument
                    tempDoc.PreserveWhitespace = True
                    tempDoc.LoadXml($"<root xmlns=""{imsQtiNamespace.ToString()}"">{result}</root>")
                    Return tempDoc.DocumentElement
                End If
            End If

            Return CType(tagToFix, XmlElement)
        End Function



    End Class
End Namespace