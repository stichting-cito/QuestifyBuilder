Public NotInheritable Class RandomStringGenerator

    Private Shared rnd As New Random()

    Public Shared Function GetRandomString(ByVal length As Integer) As String
        Dim returnValue As String = String.Empty

        For index As Integer = 0 To length
            returnValue += Convert.ToChar(rnd.Next(65, 90))
        Next

        Return returnValue
    End Function

    Public Shared Function GetRandomString() As String
        Return GetRandomString(8)
    End Function

End Class
