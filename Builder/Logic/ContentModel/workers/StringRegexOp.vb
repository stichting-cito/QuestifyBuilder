Imports System.Text.RegularExpressions
Imports System.Text

Namespace ContentModel

    Public Class StringRegexOp

        Public Shared Function EqualStringByRegex(s1 As String, s2 As String, regex As Regex) As Boolean

            Dim capturedS1 = GetCapturedString(s1, regex)
            Dim capturedS2 = GetCapturedString(s2, regex)

            Return capturedS1.Equals(capturedS2)

        End Function

        Public Shared Function GetCapturedString(s As String, regex As Regex) As String
            Dim match = regex.Match(s)

            If (match.Success) Then
                Dim sb As New StringBuilder()
                For i = 1 To match.Groups.Count
                    For Each capture As Capture In match.Groups(i).Captures
                        sb.Append(capture.Value)
                    Next
                Next
                Return sb.ToString()
            End If

            Return String.Empty
        End Function

    End Class

End Namespace
