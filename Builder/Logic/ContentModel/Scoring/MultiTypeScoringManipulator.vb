Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring
    <ScoreEditorFor(GetType(IntegerScoringParameter))>
    <ScoreEditorFor(GetType(DecimalScoringParameter))>
    Friend Class MultiTypeScoringManipulator : Inherits BaseGapScoringManipulator(Of MultiType)

        Public Sub New(manipulator As IFindingManipulator, param As IntegerScoringParameter)
            MyBase.New(manipulator, param)
        End Sub

        Public Sub New(manipulator As IFindingManipulator, param As DecimalScoringParameter)
            MyBase.New(manipulator, param)
        End Sub

        Private Function BaseValueToGapValue(baseValue As BaseValue) As GapValue(Of MultiType)
            If TypeOf baseValue Is IntegerRangeValue Then
                Dim irValue = DirectCast(baseValue, IntegerRangeValue)
                Return New GapValue(Of MultiType)(New MultiType(irValue.RangeStart), New MultiType(irValue.RangeEnd))
            ElseIf TypeOf baseValue Is IntegerComparisonValue Then
                Dim icValue = DirectCast(baseValue, IntegerComparisonValue)
                Return New GapValue(Of MultiType)(New MultiType(icValue.Value), DirectCast([Enum].Parse(GetType(GapComparisonType), icValue.TypeOfComparison), GapComparisonType))
            ElseIf TypeOf baseValue Is IntegerValue Then
                Dim iValue = DirectCast(baseValue, IntegerValue)
                Return New GapValue(Of MultiType)(New MultiType(iValue.Value))
            ElseIf TypeOf baseValue Is DecimalRangeValue Then
                Dim irValue = DirectCast(baseValue, DecimalRangeValue)
                Return New GapValue(Of MultiType)(New MultiType(irValue.RangeStart), New MultiType(irValue.RangeEnd))
            ElseIf TypeOf baseValue Is DecimalComparisonValue Then
                Dim icValue = DirectCast(baseValue, DecimalComparisonValue)
                Return New GapValue(Of MultiType)(New MultiType(icValue.Value), DirectCast([Enum].Parse(GetType(GapComparisonType), icValue.TypeOfComparison), GapComparisonType))
            ElseIf TypeOf baseValue Is DecimalValue Then
                Dim iValue = DirectCast(baseValue, DecimalValue)
                Return New GapValue(Of MultiType)(New MultiType(iValue.Value))
            ElseIf TypeOf baseValue Is StringValue Then
                Dim sValue = DirectCast(baseValue, StringValue)
                Return New GapValue(Of MultiType)(New MultiType(sValue.Value))
            ElseIf TypeOf baseValue Is StringComparisonValue Then
                Dim icValue = DirectCast(baseValue, StringComparisonValue)
                Return New GapValue(Of MultiType)(New MultiType(icValue.Value), DirectCast([Enum].Parse(GetType(GapComparisonType), icValue.TypeOfComparison), GapComparisonType))
            End If
            Return GetDefaultValue()
        End Function

        Protected Overrides Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of MultiType))
            Return lst.ToList().Select(Function(e) BaseValueToGapValue(e)).ToList()
        End Function

        Protected Overrides Function GetDefaultValue() As GapValue(Of MultiType)
            Dim defaulValue As Integer?
            Return New GapValue(Of MultiType)(New MultiType(defaulValue))
        End Function
    End Class
End Namespace