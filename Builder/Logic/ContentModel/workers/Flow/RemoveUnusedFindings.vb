Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveUnusedFindings(Of TFinding As {BaseFinding})
        Inherits CodeActivity

        Property Findings As InArgument(Of ICollection(Of TFinding))

        Property ScoringParameters As InArgument(Of IEnumerable(Of ScoringParameter))

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)

            Dim findings = context.GetValue(Me.Findings)
            Dim scoringParameters = context.GetValue(Me.ScoringParameters)

            Dim whiteListOfIds = New HashSet(Of String)(scoringParameters.Select(Function(parameter) parameter.FindingId))

            For Each finding In findings.ToList()
                If (Not whiteListOfIds.Contains(finding.Id)) Then
                    findings.Remove(finding)
                End If
            Next

        End Sub
    End Class

End Namespace