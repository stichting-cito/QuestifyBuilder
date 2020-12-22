Imports System.Linq
Imports System.Xml

Public Class TemplateHelper

    Public Shared Function IsXHtmlParameterEmpty(elements() As XmlNode) As Boolean
        If elements IsNot Nothing AndAlso elements.Length > 0 Then
            For Each element As XmlNode In elements
                If Not IsXmlNodeEmpty(element) Then
                    Return False
                End If
            Next
        End If

        Return True
    End Function

    Public Shared Function IsXmlNodeEmpty(element As XmlNode) As Boolean
        If element IsNot Nothing Then
            If Not String.IsNullOrEmpty(element.InnerText.Replace($"{Chr(160)}{Chr(10)}", "").Trim()) Then
                Return False
            Else
                Dim tagsWithPossibleChildren As String() = {"div", "body", "p", "span", "strong", "em", "sup", "sub"}
                If tagsWithPossibleChildren.Contains(element.Name) Then
                    Dim children As XmlNodeList = element.ChildNodes

                    For Each ChildElement As XmlNode In children
                        If Not IsXmlNodeEmpty(ChildElement) Then
                            Return False
                        End If
                    Next
                Else
                    If element.Name <> "#text" AndAlso element.Name <> "#whitespace" AndAlso element.Name <> "p" Then
                        Return False
                    End If
                End If
            End If
        End If

        Return True
    End Function

    Public Shared Function XHtmlParameterContainsOnlyText(elements() As XmlNode) As Boolean
        If elements IsNot Nothing Then
            If elements.Any(Function(e) e.Name <> "#text" AndAlso e.Name <> "#whitespace") Then Return False
        End If
        Return True
    End Function

End Class
