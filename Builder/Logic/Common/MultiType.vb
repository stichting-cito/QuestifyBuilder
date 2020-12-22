Public Class MultiType

    Private _integerValue As Integer?
    Private _decimalValue As Decimal?
    Private _stringValue As String = String.Empty

    Public Sub New()
    End Sub

    Public Sub New(decimalValue As Decimal?)
        _decimalValue = decimalValue
    End Sub

    Public Sub New(integerValue As Integer?)
        _integerValue = integerValue
    End Sub

    Public Sub New(stringValue As String)
        _stringValue = stringValue
    End Sub

    Public Property DecimalValue As Decimal?
        Get
            Return _decimalValue
        End Get
        Set(value As Decimal?)
            _integerValue = Nothing
            _stringValue = String.Empty
            _decimalValue = value
        End Set
    End Property

    Public Property IntegerValue As Integer?
        Get
            Return _integerValue
        End Get
        Set(value As Integer?)
            _integerValue = value
            _stringValue = String.Empty
            _decimalValue = Nothing
        End Set
    End Property

    Public Property StringValue As String
        Get
            Return _stringValue
        End Get
        Set(value As String)
            _stringValue = value
            _integerValue = Nothing
            _decimalValue = Nothing
        End Set
    End Property

    Public Shared Widening Operator CType(m As MultiType) As Decimal?
        Return m._decimalValue.Value
    End Operator

    Public Shared Widening Operator CType(i As Decimal?) As MultiType
        Return New MultiType(i)
    End Operator

    Public Shared Widening Operator CType(m As MultiType) As Integer?
        Return m._integerValue.Value
    End Operator

    Public Shared Widening Operator CType(i As Integer?) As MultiType
        Return New MultiType(i)
    End Operator

    Public Shared Widening Operator CType(m As MultiType) As String
        Return m._stringValue
    End Operator

    Public Shared Widening Operator CType(s As String) As MultiType
        Return New MultiType(s)
    End Operator

    Public Overrides Function ToString() As String
        If _integerValue.HasValue Then
            If (_integerValue.Value = Integer.MinValue) Then
                Return "-"
            Else
                Return _integerValue.Value.ToString
            End If
        ElseIf _decimalValue.HasValue Then
            If (_decimalValue.Value = Decimal.MinValue) Then
                Return "-"
            Else
                Return _decimalValue.Value.ToString
            End If
        Else
            Return _stringValue
        End If
    End Function

End Class
