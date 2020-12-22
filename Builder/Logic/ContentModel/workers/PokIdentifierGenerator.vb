Namespace ContentModel

    Public Class PokIdentifierGenerator

        Const ALLOWEDCHARS As String = "23456789abcdefghjklmnpqrstuvwxyz"
        Const ALLOWEDCHARSCOUNT As Integer = 32

        Public Shared Function GetPokIdentifier(value As Integer) As String

            If value < 0 OrElse value > Math.Pow(32, 6) - 1 Then
                Throw New ArgumentOutOfRangeException("value", "Value should be between 0 and 1073741823  (= (32^6)-1)")
            End If

            Dim remainder As Integer = 0
            Dim quotient As Integer = Math.DivRem(value, ALLOWEDCHARSCOUNT, remainder)
            If quotient > 0 Then
                Return GetPokIdentifier(quotient) + ValToChar(remainder).ToString
            Else
                Return ValToChar(remainder).ToString
            End If

        End Function

        Private Shared Function ValToChar(value As Integer) As Char
            Return ALLOWEDCHARS(value)
        End Function
    End Class
End Namespace

