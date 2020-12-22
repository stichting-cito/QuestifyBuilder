Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring

Namespace ContentModel.workers.Flow

    Public Class RemoveFactsAlreadyInFactSet
        Inherits CodeActivity

        Property BaseFacts As InArgument(Of IList(Of BaseFact))
        Property ScoringParameters As InArgument(Of IEnumerable(Of ScoringParameter))
        Property Solution As InArgument(Of Solution)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)

            Dim baseFacts = context.GetValue(Me.BaseFacts)
            Dim scoringParameters = context.GetValue(Me.ScoringParameters)
            Dim solution = context.GetValue(Me.Solution)

            Dim scoringMapKeys As IEnumerable(Of CombinedScoringMapKey) = New ScoringMap(scoringParameters, solution).GetMap()

            For Each fact As BaseFact In baseFacts.ToList()
                If scoringMapKeys.Any(Function(smk) smk.IsGroup AndAlso smk.Any(Function(k) fact.Id.StartsWith(k.ScoreKey) AndAlso fact.Id.EndsWith(k.ScoringParameter.IdentifierPostFix()))) Then
                    baseFacts.Remove(fact)
                End If
            Next

        End Sub

    End Class

End Namespace