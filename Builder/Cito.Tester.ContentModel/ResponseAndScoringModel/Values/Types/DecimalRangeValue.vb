Imports System.Globalization

Public Class DecimalRangeValue
    Inherits TypedRangeValue(Of Decimal)

    Public Overrides Function IsMatch(value As BaseValue) As Boolean
        If value Is Nothing Then
            Throw New ArgumentNullException("value")
        End If

        Dim matched As Boolean = False
        Select Case value.GetType.ToString
            Case GetType(DecimalValue).ToString

                Dim startRange As Decimal
                Dim endRange As Decimal
                If Me.RangeStart <= Me.RangeEnd Then
                    startRange = Me.RangeStart
                    endRange = Me.RangeEnd
                Else
                    startRange = Me.RangeEnd
                    endRange = Me.RangeStart
                End If

                Dim castvalue As DecimalValue = DirectCast(value, DecimalValue)
                If castvalue.Value >= startRange AndAlso castvalue.Value <= endRange Then
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
        Return $"{Me.RangeStart.ToString(culture)}-{Me.RangeEnd.ToString(culture)}"
    End Function

    Public Sub New(rangeStart As Decimal, rangeEnd As Decimal)
        Me.RangeStart = rangeStart
        Me.RangeEnd = rangeEnd
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
