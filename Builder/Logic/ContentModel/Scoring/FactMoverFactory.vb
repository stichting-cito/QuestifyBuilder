Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Friend Class FactMoverFactory


        Public Shared Function Create(scoringMapKey As ScoringMapKey, solution As Solution) As IFactMover

            Dim scoringParameter = scoringMapKey.ScoringParameter

            If (scoringParameter.IsSingleChoice()) Then Return SingleChoiceSpecialCase(scoringParameter, solution)

            Return DoCreate(scoringMapKey, solution)

        End Function

        Private Shared Function DoCreate(scoringMapKey As ScoringMapKey, solution As Solution) As IFactMover
            Dim scoringParameter = scoringMapKey.ScoringParameter
            Dim findingManipulator = ScoringParameterFactory.GetKeyManipulator(solution, scoringParameter)
            Dim scoringManipulator = ScoringParameterFactory.GetScoreBaseManipulator(scoringParameter, findingManipulator)

            Return New FactMover(scoringMapKey, scoringManipulator, findingManipulator)
        End Function


        Private Shared Function SingleChoiceSpecialCase(scoringParameter As ScoringParameter, solution As Solution) As IFactMover
            Debug.Assert(scoringParameter.Value IsNot Nothing, "Should Not occur")
            Dim scoringMapKeys = From prm In scoringParameter.Value Select (New ScoringMapKey(scoringParameter, prm.Id))
            Dim findingManipulator = ScoringParameterFactory.GetKeyManipulator(solution, scoringParameter)
            Dim scoringManipulator = ScoringParameterFactory.GetScoreBaseManipulator(scoringParameter, findingManipulator)

            Return New ChoiceFactMover(scoringManipulator, findingManipulator, scoringMapKeys)
        End Function


    End Class

End Namespace