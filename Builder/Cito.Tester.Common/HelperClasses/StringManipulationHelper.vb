Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions

Public NotInheritable Class StringManipulationHelper

    Public Shared Function ReplaceTabsAndNewLinesBySpaces(value As String) As String
        If value Is Nothing Then Return Nothing
        Return value.Replace(vbCrLf, " ").Replace(vbCr, String.Empty).Replace(vbLf, String.Empty).Replace(vbTab, " ").Trim()
    End Function

    Public Shared Function FixEncodingXmlString(xmlString As String) As String
        Return Regex.Replace(xmlString, "[^\u0000-\u007F]", "")
    End Function


    Public Shared Function ContainsUnicodeCharacter(input As String) As Boolean
        Return Regex.IsMatch(input, "[^\u0000-\u007F]")
    End Function


    Public Shared Function EscapeAmpersand(input As String) As String

        If input.IndexOf("&") = -1 Then Return input

        Dim list As List(Of String) = input.Split(CChar("&")).ToList()

        Dim stringBuilder As New StringBuilder()

        stringBuilder.Append(list(0))

        For i As Integer = 1 To list.Count - 1
            If list(i).StartsWith("amp;") Then
                stringBuilder.Append("&")
                stringBuilder.Append(list(i))
            Else
                stringBuilder.Append("&amp;")
                stringBuilder.Append(list(i))
            End If
        Next

        Return stringBuilder.ToString()
    End Function

End Class
