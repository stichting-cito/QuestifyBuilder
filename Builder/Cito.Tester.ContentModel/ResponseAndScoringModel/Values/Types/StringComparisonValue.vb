Public Class StringComparisonValue
    Inherits TypedComparisonValue(Of String)
    Public Sub New(value As String, comparisonType As String)
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
            Case GetType(StringValue).ToString
                Dim castvalue As StringValue = DirectCast(value, StringValue)

                Select Case GetComparisonType(Me.TypeOfComparison)
                    Case ComparisonType.None
                        If castvalue.Value.Equals(Me.Value) Then
                            matched = True
                        End If
                    Case ComparisonType.Equivalent,
                        ComparisonType.Dependency
                        matched = True
                    Case ComparisonType.NotEquals
                        If Not castvalue.Value.Equals(Me.Value) Then
                            matched = True
                        End If
                    Case ComparisonType.Evaluate
                        matched = False
                    Case Else
                        Throw New NotSupportedException
                End Select

            Case Else
                Throw New NotSupportedException($"type '{GetType(StringValue).ToString}' is not supported for matching")

        End Select

        Return matched
    End Function

End Class
