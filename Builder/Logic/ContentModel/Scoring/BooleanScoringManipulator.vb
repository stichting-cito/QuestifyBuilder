Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Friend Class BooleanScoringManipulator
        Inherits BaseGapScoringManipulator(Of Boolean)

        Public Sub New(manipulator As IFindingManipulator, param As BooleanScoringParameter)
            MyBase.New(manipulator, param)
        End Sub

        Protected Overrides Function GetDefaultValue() As GapValue(Of Boolean)
            Return New GapValue(Of Boolean)(False)
        End Function

        Protected Overrides Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of Boolean))
            Return (From val In lst Select New GapValue(Of Boolean)(DirectCast(val, BooleanValue).Value)).ToList()
        End Function

    End Class

End Namespace