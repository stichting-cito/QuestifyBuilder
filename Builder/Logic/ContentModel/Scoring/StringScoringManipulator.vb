Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(StringScoringParameter))>
    <ScoreEditorFor(GetType(TimeScoringParameter))>
    <ScoreEditorFor(GetType(MathScoringParameter))>
    Friend Class StringScoringManipulator : Inherits BaseGapScoringManipulator(Of String)

        Public Sub New(manipulator As IFindingManipulator, param As StringScoringParameter)
            MyBase.New(manipulator, param)
        End Sub

        Public Sub New(manipulator As IFindingManipulator, param As TimeScoringParameter)
            MyBase.New(manipulator, param)
        End Sub

        Public Sub New(manipulator As IFindingManipulator, param As MathScoringParameter)
            MyBase.New(manipulator, param)
        End Sub


        Private Function BaseValueToGapValue(baseValue As BaseValue) As GapValue(Of String)
            If TypeOf baseValue Is StringComparisonValue Then
                Dim icValue = DirectCast(baseValue, StringComparisonValue)
                Return New GapValue(Of String)(icValue.Value, DirectCast([Enum].Parse(GetType(GapComparisonType), icValue.TypeOfComparison), GapComparisonType))
            ElseIf TypeOf baseValue Is StringValue Then
                Dim iValue = DirectCast(baseValue, StringValue)
                Return New GapValue(Of String)(iValue.Value)
            End If
            Return GetDefaultValue()
        End Function

        Protected Overrides Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of String))
            Return lst.Select(Function(e) BaseValueToGapValue(e))
        End Function

        Protected Overrides Function GetDefaultValue() As GapValue(Of String)
            If ScoreParameter.GetType() = GetType(MathScoringParameter) Then
                Return New GapValue(Of String)("", GapComparisonType.EqualsStrict)
            ElseIf ScoreParameter.GetType() = GetType(MathCasEqualScoringParameter) Then
                If ScoreParameter.Name = "mathCasEqualScoringLast" Then
                    Return New GapValue(Of String)("", GapComparisonType.Equals)
                Else
                    Return New GapValue(Of String)("", GapComparisonType.EqualEquation)
                End If
            Else
                Return New GapValue(Of String)("")

            End If

        End Function


    End Class
End Namespace