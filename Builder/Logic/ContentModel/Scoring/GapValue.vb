Namespace ContentModel.Scoring
    Public Class GapValue(Of T)

        Public Property Value As T

        Public Property Comparison As GapComparisonType

        Public Property Value2 As T

        Public Sub New(valueArg As T)
            Value = valueArg
            Comparison = GapComparisonType.Equals
        End Sub

        Public Sub New(valueArg As T, comparisonArg As GapComparisonType)
            Value = valueArg
            Comparison = comparisonArg
        End Sub

        Public Sub New(valueArg As T, valueArg2 As T)
            Value = valueArg
            Value2 = valueArg2
            Comparison = GapComparisonType.Range
        End Sub

        Public Sub New(valueArg As T, valueArg2 As T, comparisonArg As GapComparisonType)
            Value = valueArg
            Value2 = valueArg2
            Comparison = comparisonArg
        End Sub

    End Class
End Namespace
