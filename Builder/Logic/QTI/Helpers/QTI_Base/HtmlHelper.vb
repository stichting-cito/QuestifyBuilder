Imports System.Text.RegularExpressions

Namespace QTI.Helpers.QTI_Base

    Public Class HtmlHelper

        Public Function MimeTypeCheckForIncompleteHtml(ByVal mimeType As String) As Boolean
            Dim checkForIncompleteHtml As Boolean = False

            If mimeType.Contains("application/xhtml+xml") OrElse
   mimeType.Contains("text/html") Then
                checkForIncompleteHtml = True
            End If

            Return checkForIncompleteHtml
        End Function

        Public Function CheckIncompleteHtml(ByVal resourceContent As String) As String
            Dim result As String = resourceContent
            If Not Regex.IsMatch(result.ToLower, "<html\b[^>]*>(.*?)</html>") Then
                result = $"<html xmlns=""http://www.w3.org/1999/xhtml"">{result}</html>"
            End If
            Return result
        End Function

    End Class
End NameSpace