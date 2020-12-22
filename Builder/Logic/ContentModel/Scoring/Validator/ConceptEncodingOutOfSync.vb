Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring.Validator

    Friend Class ConceptEncodingOutOfSync
        Inherits ValidationRuleProcessor

        Protected Overrides Sub Validate(item As AssessmentItem)

            Dim scoringPrms = item.Parameters.DeepFetchInlineScoringParameters()
            If scoringPrms IsNot Nothing Then

                Dim findingId As String = scoringPrms.First.FindingId
                Dim keyFinding = item.Solution.GetFindingOrMakeIt(findingId)
                Dim conceptFinding = item.Solution.ConceptFindings.Single(Function(cf) cf.Id = findingId)

                Dim keyManipulator = New KeyManipulator(keyFinding)
                Dim conceptManipulator = New ConceptManipulator(conceptFinding)

                Dim comparer = New FindingToFindingComparer(keyManipulator, conceptManipulator)
                comparer.Logic = New SkipAnswerCategoriesAsTarget(comparer.Logic)
                If Not comparer.AreEqual() Then
                    Throw New Exception()
                End If
            End If

        End Sub

    End Class

End Namespace