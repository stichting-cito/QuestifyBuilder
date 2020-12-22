Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel

    Public Class ConceptRenameLogic

        Public Sub ConceptSetToAnswerCategorySet(combinedScoringMapKey As CombinedScoringMapKey, setNumber As Integer, solution As Solution)

            Debug.Assert(combinedScoringMapKey.IsGroup)

            Dim finding = solution.ConceptFindings.First()
            Dim renameSet = finding.KeyFactsets(setNumber)

            Dim answerCategoryId = combinedScoringMapKey.GetIdForAnswerCategory(finding)

            For Each key In combinedScoringMapKey
                RenamePerFact(renameSet, key, answerCategoryId)
            Next

        End Sub

        Private Sub RenamePerFact(ByVal renameSet As KeyFactSet, ByVal key As ScoringMapKey, ByVal answerCategoryId As Integer)

            Dim renameFact = renameSet.Facts.FirstOrDefault(Function(fact) fact.Id.StartsWith(key.ScoreKey) AndAlso fact.Id.EndsWith(key.ScoringParameter.IdentifierPostFix()))

            If (renameFact Is Nothing) Then
                Debug.Assert(key.ScoringParameter.IsSingleChoice)
            Else
                Dim newId = key.GetFactId(answerCategoryId.ToString())
                renameFact.Id = newId
            End If

        End Sub
    End Class

End Namespace
