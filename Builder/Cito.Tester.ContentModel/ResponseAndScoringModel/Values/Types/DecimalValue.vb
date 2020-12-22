Imports System.Globalization

Public Class DecimalValue
    Inherits TypedValue(Of Decimal)

    Public Sub New(value As Decimal)
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
            Case GetType(DecimalValue).ToString
                Dim castvalue As DecimalValue = DirectCast(value, DecimalValue)

                If castvalue.Value.Equals(Me.Value) Then
                    matched = True
                End If
            Case Else

                Throw New NotSupportedException($"type '{GetType(DecimalValue).ToString}' is not supported for matching")
        End Select

        Return matched
    End Function

    Public Overrides Function ToString() As String
        Return Me.ToString(CultureInfo.InvariantCulture)
    End Function

    Public Overloads Function ToString(culture As CultureInfo) As String
        Return Me.Value.ToString(culture)
    End Function

End Class
