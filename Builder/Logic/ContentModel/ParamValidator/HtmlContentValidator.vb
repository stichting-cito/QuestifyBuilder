Imports System.Xml

Namespace ContentModel.ParamValidator
    Public Class HtmlContentValidator

        Private Shared ReadOnly contentNodes As HashSet(Of String)

        Shared Sub New()
            contentNodes = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            contentNodes.Add("img")
        End Sub

        Public Function HtmlContainsValue(xhtmlParam As String) As Boolean
            Dim ret As Boolean

            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml($"<body>{xhtmlParam}</body>")

            ret = ElementHasContent(DirectCast(xmlDoc.FirstChild, XmlElement))
            Return ret
        End Function

        Private Function ElementHasContent(ByVal element As XmlElement) As Boolean
            If element IsNot Nothing Then
                If (Not element.HasChildNodes) Then
                    Return Not String.IsNullOrEmpty(element.InnerText.Trim())
                Else
                    Dim children As XmlNodeList = element.ChildNodes()

                    For Each childNode As XmlNode In children
                        If (TypeOf childNode Is XmlElement) Then
                            Dim node As XmlElement = DirectCast(childNode, XmlElement)
                            If contentNodes.Contains(node.Name) Then Return True
                            Dim x As XmlElement = DirectCast(childNode, XmlElement)
                            If ElementHasContent(DirectCast(childNode, XmlElement)) Then
                                Return True
                            End If
                        End If
                        If (TypeOf childNode Is XmlText) Then
                            If Not String.IsNullOrEmpty(childNode.Value.Trim()) Then Return True
                        End If
                    Next

                End If
            End If
            Return False
        End Function

    End Class

End Namespace