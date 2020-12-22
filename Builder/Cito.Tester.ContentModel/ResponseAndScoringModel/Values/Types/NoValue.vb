Public Class NoValue
    Inherits BaseValue

    Public Overrides Function IsMatch(value As BaseValue) As Boolean
        Return TypeOf value Is NoValue
    End Function

    Public Overrides ReadOnly Property IsRange As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return "Ø"
    End Function
End Class