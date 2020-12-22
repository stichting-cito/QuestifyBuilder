Imports Cito.Tester.ContentModel


Namespace ContentModel.Scoring.Validator

    Friend Class GroupConsistsOfMultipleDifferentInteractions
        Inherits ValidationRuleProcessor

        Protected Overrides Sub Validate(item As AssessmentItem)
            Dim solution As Solution = item.Solution

            Dim scoringParametersInline As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchInlineScoringParameters

            For Each ScoringParameter As ScoringParameter In scoringParametersInline
                If TypeOf ScoringParameter Is InlineChoiceScoringParameter Then
                    CheckForSameInteractions(solution)
                End If
            Next

            Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchScoringParameters()

            For Each scoringParameter As ScoringParameter In scoringParameters
                If TypeOf scoringParameter Is ChoiceScoringParameter Then
                    CheckForSameInteractions(solution)
                End If
            Next

        End Sub

        Private Sub CheckForSameInteractions(solution As Solution)

            For Each finding As KeyFinding In solution.Findings
                For Each factset As KeyFactSet In finding.KeyFactsets
                    Dim factIds As New List(Of String)()
                    For Each fact In factset.Facts
                        If Not factIds.Contains(fact.Id) Then
                            factIds.Add(fact.Id)
                        Else
                            Throw New Exception()
                        End If
                    Next
                Next
            Next
        End Sub

    End Class
End Namespace