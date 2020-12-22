Public Class StringValue
    Inherits TypedValue(Of String)

    Public Sub New(value As String)
        Me.Value = value
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function IsMatch(value As BaseValue) As Boolean
        If value Is Nothing Then
            Throw New ArgumentNullException("value")
        End If

        Dim matched As Boolean = False
        Select Case value.GetType.ToString
            Case GetType(StringValue).ToString
                Dim castvalue As StringValue = DirectCast(value, StringValue)

                If castvalue.Value.Equals(Me.Value) Then
                    matched = True
                End If
            Case Else
                Throw New NotSupportedException($"type '{GetType(StringValue).ToString}' is not supported for matching")
        End Select

        Return matched
    End Function

End Class
