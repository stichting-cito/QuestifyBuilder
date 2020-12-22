Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring.Validator

    Friend Class NoAndOnSameInteraction
        Inherits ValidationRuleProcessor

        Protected Overrides Sub Validate(item As AssessmentItem)
            Dim solution As Solution = item.Solution

            For Each finding As KeyFinding In solution.Findings
                Dim values As New ValueCollection
                ValidateFactList(finding.Facts, values)
            Next
        End Sub


        Private Sub ValidateFactList(facts As IEnumerable(Of BaseFact), values As ValueCollection)

            For Each fact As KeyFact In facts
                For Each value As KeyValue In fact.Values
                    If DomainMatch(values, value) Then
                        Throw New Exception()
                    Else
                        values.Add(value)
                    End If
                Next
            Next

        End Sub

        Private Function DomainMatch(values As ValueCollection, value As KeyValue) As Boolean
            Return values.Any(Function(v) v.Domain = value.Domain)
        End Function

    End Class
End Namespace
