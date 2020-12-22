Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Runtime.CompilerServices

Namespace ContentModel

    Public Module CombinedScoringMapKeyExtensions

        <Extension>
        Public Function GetIdForAnswerCategory(scoringMapKey As CombinedScoringMapKey, solution As Solution) As Integer
            Dim worker = New GetIdForAnswerCategory()
            Return worker.GetIdForAnswerCategory(scoringMapKey, solution)
        End Function

        <Extension>
        Public Function GetIdForAnswerCategory(scoringMapKey As CombinedScoringMapKey, conceptFinding As ConceptFinding) As Integer
            Dim worker = New GetIdForAnswerCategory()
            Return worker.GetIdForAnswerCategory(scoringMapKey, conceptFinding)
        End Function

        <Extension>
        Public Function GetFactId(scoringMapKey As ScoringMapKey) As String
            Return ScoringParameterLogic.GetFactIdFor(scoringMapKey)
        End Function

        <Extension>
        Public Function GetFactId(scoringMapKey As ScoringMapKey, answerCategoryId As String) As String
            Return ScoringParameterLogic.GetFactIdFor(scoringMapKey, answerCategoryId)
        End Function

        <Extension>
        Public Function GetConceptSetIdsWithoutKeySet(combinedScoringMapKey As CombinedScoringMapKey, solution As Solution) As IEnumerable(Of Integer)
            Return New ConceptFactsSetLogic().GetUnusedConceptFactSetIds(combinedScoringMapKey, solution)
        End Function


        <Extension>
        Public Sub ConceptSetToAnswerCategorySet(combinedScoringMapKey As CombinedScoringMapKey, setNumber As Integer, solution As Solution)
            Dim worker = New ConceptRenameLogic()
            worker.ConceptSetToAnswerCategorySet(combinedScoringMapKey, setNumber, solution)
        End Sub

    End Module

End Namespace