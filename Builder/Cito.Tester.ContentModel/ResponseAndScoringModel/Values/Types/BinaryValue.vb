Public Class BinaryValue
    Inherits TypedValue(Of Byte())

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(value As Byte())
        Me.New()
        Me.Value = value
    End Sub


    Public Overrides Function IsMatch(value As BaseValue) As Boolean
        If value Is Nothing Then
            Throw New ArgumentNullException("value")
        End If

        Dim matched As Boolean = False
        Select Case value.GetType.ToString
            Case GetType(BinaryValue).ToString
                Dim castvalue As BinaryValue = DirectCast(value, BinaryValue)

                If castvalue.Value.Equals(Me.Value) Then
                    matched = True
                End If
            Case Else
                Throw New NotSupportedException($"type '{GetType(BinaryValue).ToString}' is not supported for matching")
        End Select

        Return matched
    End Function


End Class