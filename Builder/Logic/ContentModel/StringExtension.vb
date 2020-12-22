Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Runtime.CompilerServices

Namespace ContentModel
    Public Module StringExtension

        <Extension>
        Public Function EqualStringByRegex(s1 As String, s2 As String, regex As Regex) As Boolean
            Return StringRegexOp.EqualStringByRegex(s1, s2, regex)
        End Function

        <Extension>
        Public Function ExtractNumber(ByVal value As String) As Integer
            Dim returnVal As String = String.Empty
            Dim collection As MatchCollection = Regex.Matches(value, "\d+")
            returnVal = collection.Cast(Of Match)().Aggregate(returnVal, Function(current, m) current + m.ToString())
            Return Convert.ToInt32(returnVal)
        End Function

        <Extension>
        Public Function TruncateWithEllipsis(value As String, maxLength As Integer) As String
            If (String.IsNullOrEmpty(value)) Then Return value
            If (maxLength <= 3) Then Throw New ArgumentException("maxLength")

            If (value.Length > maxLength) Then
                Return value.Substring(0, maxLength - 3) + "..."
            End If

            Return value
        End Function

    End Module
End Namespace


