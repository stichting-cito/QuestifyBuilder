Public Class NoValueType(Of T)

    Private _value As T
    Private _noValueIsCorrect As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(noValueisCorrect As Boolean)
        _noValueIsCorrect = noValueisCorrect
    End Sub

    Public Sub New(value As T)
        _value = value
    End Sub

    Public Property NoValueIsCorrect As Boolean
        Get
            Return _noValueIsCorrect
        End Get
        Set()
            _noValueIsCorrect = Value
        End Set
    End Property

    Public Property Value As T
        Get
            Return _value
        End Get
        Set()
            _value = Value
        End Set
    End Property

    Public Shared Widening Operator CType(m As NoValueType(Of T)) As T
        Return m.Value
    End Operator

    Public Shared Widening Operator CType(s As T) As NoValueType(Of T)
        Return New NoValueType(Of T)(s)
    End Operator

    Public Overrides Function ToString() As String
        If _noValueIsCorrect Then
            Return "Ø"
        Else
            Return Value.ToString
        End If
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim other = TryCast(obj, NoValueType(Of T))
        If (other IsNot Nothing) Then
            If Value Is Nothing Then
                Return other.Value Is Nothing AndAlso NoValueIsCorrect.Equals(other.NoValueIsCorrect)
            Else
                Return Value.Equals(other.Value) AndAlso NoValueIsCorrect.Equals(other.NoValueIsCorrect)
            End If
        Else
            Return MyBase.Equals(obj)
        End If
    End Function

End Class
