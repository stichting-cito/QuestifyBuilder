Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq

Public NotInheritable Class RemoveOrphanConceptFact
    Inherits CodeActivity

    Property Solution As InArgument(Of Solution)

    Property CombinedScoringMapKey As InArgument(Of CombinedScoringMapKey)

    Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
        Dim sol = context.GetValue(Solution)
        Dim combScoringMapKey = context.GetValue(CombinedScoringMapKey)

        Dim orphanedSetIdList = combScoringMapKey.GetConceptSetIdsWithoutKeySet(sol)

        For Each orphanSetId As Integer In orphanedSetIdList.OrderByDescending(Function(int) int)
            ScoringParameterFactory.GetConceptManipulatorBare(combScoringMapKey, sol).RemoveConcept(orphanSetId.ToString())
        Next

    End Sub



End Class
