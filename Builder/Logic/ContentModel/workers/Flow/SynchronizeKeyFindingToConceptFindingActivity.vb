Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring

Namespace ContentModel.workers.Flow
    Public NotInheritable Class SynchronizeKeyFindingToConceptFindingActivity : Inherits CodeActivity

        Property Solution As InArgument(Of Solution)

        Property ScoreParameter As InArgument(Of ScoringParameter)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim solution As Solution = context.GetValue(Me.Solution)
            Dim scoringParameter = context.GetValue(Me.ScoreParameter)
            Dim keyFinding = solution.GetFindingOrMakeIt(scoringParameter.FindingId)
            Dim conceptFinding = GetConceptFindingOrMakeIt(scoringParameter.FindingId, solution)
            Dim keyManipulator = New KeyManipulator(keyFinding)
            Dim conceptManipulator = New ConceptManipulator(conceptFinding)

            Dim synchronizer = New FindingToFindingManipulator(keyManipulator, conceptManipulator)
            synchronizer.Logic = New SkipAnswerCategoriesAsTarget(synchronizer.Logic)

            synchronizer.Execute()
        End Sub

        Private Shared Function GetConceptFindingOrMakeIt(id As String, solution As Solution) As ConceptFinding
            For Each kf In solution.ConceptFindings
                If (kf.Id = id) Then
                    Return kf
                End If
            Next
            Dim ret = New ConceptFinding(id)
            solution.ConceptFindings.Add(ret)
            Return ret
        End Function

    End Class

End Namespace