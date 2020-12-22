Imports System.Globalization

Public Class IntegerComparisonValue
    Inherits TypedComparisonValue(Of Integer)

    Public Sub New(value As Integer, comparisonType As String)
        Me.Value = value
        Me.TypeOfComparison = comparisonType
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
                matched = DetermineMatch(castvalue)
            Case Else
                Throw New NotSupportedException($"type '{GetType(IntegerValue).ToString}' is not supported for matching")
        End Select

        Return matched
    End Function

    Public Overrides Function ToString() As String
        Return Me.ToString(CultureInfo.InvariantCulture)
    End Function

    Public Overloads Function ToString(culture As CultureInfo) As String
        Return String.Concat(comparisonPrefix(Me.TypeOfComparison), Me.Value.ToString(culture))
    End Function

    Private Function DetermineMatch(castvalue As IntegerValue) As Boolean
        Dim matched as Boolean = False
        Select Case GetComparisonType(Me.TypeOfComparison)
            Case ComparisonType.None
                If castvalue.Value.Equals(Me.Value) Then
                    matched = True
                End If
            Case ComparisonType.SmallerThan
                If castvalue.Value < Me.Value Then
                    matched = True
                End If
            Case ComparisonType.SmallerThanEquals
                If castvalue.Value <= Me.Value Then
                    matched = True
                End If
            Case ComparisonType.GreaterThan
                If castvalue.Value > Me.Value Then
                    matched = True
                End If
            Case ComparisonType.GreaterThanEquals
                If castvalue.Value >= Me.Value Then
                    matched = True
                End If
            Case Else
                Throw New NotSupportedException
        End Select
        Return matched
    End Function

End Class