Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring
    Public Class ConceptScoringMap
        Inherits ScoringMap


        Public Sub New(scoringParameters As IEnumerable(Of ScoringParameter), solution As Solution)
            MyBase.New(scoringParameters, solution)
        End Sub


        Public Sub New(combinedScoringMapKey As CombinedScoringMapKey, solution As Solution)
            MyBase.New(CombinedScoringMapKey.Select(Function(key) key.ScoringParameter).Distinct(), solution)
        End Sub


        Protected Overrides Function DoGetFactSetNumbers(ByVal scoreMapKey As ScoringMapKey) As IEnumerable(Of Integer)
            Dim manipulator = scoreMapKey.ScoringParameter.GetConceptScoreManipulator(Solution)
            Return manipulator.GetFactSetNumbers(scoreMapKey.ScoreKey)
        End Function

    End Class
End Namespace
