Public NotInheritable Class AlphabeticIdentifierHelper



    Public Shared Function GetAlphabeticIdentifier(input As Integer) As String
        Dim result As String = String.Empty
        Dim nrOfAlphabeticRounds As Integer = Convert.ToInt32(Math.Floor((input - 1) / 26))
        If nrOfAlphabeticRounds >= 1 Then
            result = New String(ChrW(64 + 26), nrOfAlphabeticRounds)
            input = (input - (nrOfAlphabeticRounds * 26))
        End If
        result = String.Concat(result, ChrW(64 + input).ToString)
        Return result
    End Function

    Public Shared Function GetAlphabeticIdentifierForHottext(input As Integer) As String
        Dim result As String = String.Empty
        Dim nrOfAlphabeticRounds As Integer = Convert.ToInt32(Math.Floor((input - 1) / 26))
        If nrOfAlphabeticRounds >= 1 Then
            result = String.Concat(result, ChrW(64 + nrOfAlphabeticRounds).ToString)
            input = (input - (nrOfAlphabeticRounds * 26))
        End If
        result = String.Concat(result, ChrW(64 + input).ToString)
        Return result
    End Function



    Public Shared Function GetAlphabeticIdentifier(input As String) As String
        Dim index As Integer = 0

        If Not String.IsNullOrEmpty(input) Then
            If input.Length = 1 Then
                Integer.TryParse(CStr(AscW(input) - 64), index)
                If index <> 0 Then
                    Return GetAlphabeticIdentifier(index)
                End If
            End If
        End If

        Return input
    End Function

End Class
