Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.Logic.ContentModel

Public NotInheritable Class MakeConceptSetsDeletable
    Inherits CodeActivity

    Property Solution As InArgument(Of Solution)

    Property CombinedScoringMapKey As InArgument(Of CombinedScoringMapKey)

    Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
        Dim sol = context.GetValue(Me.Solution)
        Dim combScoringMapKey = context.GetValue(Me.CombinedScoringMapKey)

        Dim orphanedSetIdList = combScoringMapKey.GetConceptSetIdsWithoutKeySet(sol)


        For Each orphanSetId As Integer In orphanedSetIdList

            combScoringMapKey.GetIdForAnswerCategory(sol)
            combScoringMapKey.ConceptSetToAnswerCategorySet(orphanSetId, sol)

        Next

    End Sub



End Class
