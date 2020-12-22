Imports System.Globalization

Public Class IntegerValue
    Inherits TypedValue(Of Integer)

    Public Sub New(value As Integer)
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
            Case GetType(IntegerValue).ToString
                Dim castvalue As IntegerValue = DirectCast(value, IntegerValue)

                If castvalue.Value.Equals(Me.Value) Then
                    matched = True
                End If
            Case Else
                Throw New NotSupportedException($"type '{GetType(IntegerValue).ToString}' is not supported for matching")
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
