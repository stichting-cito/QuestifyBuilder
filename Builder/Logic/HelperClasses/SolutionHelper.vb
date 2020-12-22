Imports Cito.Tester.ContentModel
Imports System.Activities

Namespace HelperClasses

    Public Class SolutionHelper

        Public Shared Sub CleanConceptFindings(solution As Solution, scoringParameters As IEnumerable(Of ScoringParameter))
            Dim inputs = New Dictionary(Of String, Object) From {
                    {"Solution", solution},
                    {"ScoringParameters", scoringParameters}
                }

            WorkflowInvoker.Invoke(New ConceptCleaner(), inputs)
        End Sub

    End Class

End Namespace