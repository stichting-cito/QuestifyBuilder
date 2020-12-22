Imports System.Globalization

Public Class IntegerRangeValue
    Inherits TypedRangeValue(Of Integer)

    Public Sub New(rangeStart As Integer, rangeEnd As Integer)
        Me.RangeStart = rangeStart
        Me.RangeEnd = rangeEnd
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

                Dim startRange As Integer
                Dim endRange As Integer
                If Me.RangeStart <= Me.RangeEnd Then
                    startRange = Me.RangeStart
                    endRange = Me.RangeEnd
                Else
                    startRange = Me.RangeEnd
                    endRange = Me.RangeStart
                End If

                Dim castvalue As IntegerValue = DirectCast(value, IntegerValue)
                If castvalue.Value >= startRange AndAlso castvalue.Value <= endRange Then
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
        Return $"{Me.RangeStart.ToString(culture)}-{Me.RangeEnd.ToString(culture)}"
    End Function

End Class
