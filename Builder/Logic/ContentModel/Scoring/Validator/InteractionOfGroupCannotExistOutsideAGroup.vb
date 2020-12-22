Imports Cito.Tester.ContentModel


Namespace ContentModel.Scoring.Validator

    Friend Class InteractionOfGroupCannotExistOutsideAGroup
        Inherits ValidationRuleProcessor

        Protected Overrides Sub Validate(item As AssessmentItem)

            Dim solution As Solution = item.Solution

            For Each finding In solution.Findings
                For Each fact In finding.Facts
                    If FactIdIsUsedInFactSet(fact.Id, finding) Then
                        Throw New Exception()
                    End If
                Next
            Next


        End Sub

        Private Function FactIdIsUsedInFactSet(factId As String, finding As KeyFinding) As Boolean

            For Each factSet In finding.KeyFactsets
                For Each fact In factSet.Facts
                    If fact.Id = factId Then
                        Return True
                    End If
                Next
            Next

            Return False
        End Function

    End Class
End Namespace