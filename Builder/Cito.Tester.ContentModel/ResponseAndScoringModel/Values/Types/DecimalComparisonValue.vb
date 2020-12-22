Imports System.Globalization

Public Class DecimalComparisonValue
    Inherits TypedComparisonValue(Of Decimal)

    Public Sub New(value As Decimal, comparisonType As String)
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
            Case GetType(DecimalValue).ToString
                Dim castvalue As DecimalValue = DirectCast(value, DecimalValue)
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

            Case Else

                Throw New NotSupportedException($"type '{GetType(DecimalValue).ToString}' is not supported for matching")
        End Select

        Return matched
    End Function

    Public Overrides Function ToString() As String
        Return Me.ToString(CultureInfo.InvariantCulture)
    End Function

    Public Overloads Function ToString(culture As CultureInfo) As String
        Return String.Concat(comparisonPrefix(Me.TypeOfComparison), Me.Value.ToString(culture))
    End Function

End Class
