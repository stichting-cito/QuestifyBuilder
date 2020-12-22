Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring.Validator

    Friend Class NoOrBetweenSingleInteractions
        Inherits ValidationRuleProcessor

        Protected Overrides Sub Validate(item As AssessmentItem)
            Dim solution As Solution = item.Solution
            For Each finding As KeyFinding In solution.Findings
                For Each factSet As KeyFactSet In finding.KeyFactsets
                    If factSet.Facts.Count = 1 Then
                        For Each fact In factSet.Facts
                            If fact.Values.Count = 1 Then
                                Throw New Exception()
                            End If
                        Next
                    End If
                Next
            Next
        End Sub

    End Class
End Namespace
