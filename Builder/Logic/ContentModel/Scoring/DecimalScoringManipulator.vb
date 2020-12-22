Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(CurrencyScoringParameter))>
    Friend Class DecimalScoringManipulator : Inherits BaseGapScoringManipulator(Of Decimal?)

        Public Sub New(manipulator As IFindingManipulator, param As DecimalScoringParameter)
            MyBase.New(manipulator, param)
        End Sub

        Public Sub New(manipulator As IFindingManipulator, param As CurrencyScoringParameter)
            MyBase.New(manipulator, param)
        End Sub


        Private Function BaseValueToGapValue(baseValue As BaseValue) As GapValue(Of Decimal?)
            If TypeOf baseValue Is DecimalRangeValue Then
                Dim irValue = DirectCast(baseValue, DecimalRangeValue)
                Return New GapValue(Of Decimal?)(irValue.RangeStart, irValue.RangeEnd)
            ElseIf TypeOf baseValue Is DecimalComparisonValue Then
                Dim icValue = DirectCast(baseValue, DecimalComparisonValue)
                Return New GapValue(Of Decimal?)(icValue.Value, DirectCast([Enum].Parse(GetType(GapComparisonType), icValue.TypeOfComparison), GapComparisonType))
            ElseIf TypeOf baseValue Is DecimalValue Then
                Dim iValue = DirectCast(baseValue, DecimalValue)
                Return New GapValue(Of Decimal?)(iValue.Value)
            End If
            Return GetDefaultValue()
        End Function

        Protected Overrides Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of Decimal?))
            Return lst.Select(Function(e) BaseValueToGapValue(e))
        End Function

        Protected Overrides Function GetDefaultValue() As GapValue(Of Decimal?)
            Return New GapValue(Of Decimal?)(Nothing)
        End Function

    End Class
End Namespace
