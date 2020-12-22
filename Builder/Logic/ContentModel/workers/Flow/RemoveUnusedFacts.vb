Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveUnusedFacts
        Inherits CodeActivity

        Property BaseFacts As InArgument(Of IList(Of BaseFact))

        Property FactIdsToScoringParameter As InArgument(Of Dictionary(Of String, ScoringParameter))

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)

            Dim baseFacts = context.GetValue(Me.BaseFacts)
            Dim factId2Sp = context.GetValue(Me.FactIdsToScoringParameter)

            Dim knowFactIds = New HashSet(Of String)(factId2Sp.Keys)

            For Each baseFact As BaseFact In baseFacts.ToList()
                If (Not knowFactIds.Contains(baseFact.Id) AndAlso NoFunctionalMatchById(baseFact, knowFactIds)) Then
                    baseFacts.Remove(baseFact)
                End If
            Next

        End Sub

        Private Function NoFunctionalMatchById(ByVal baseFact As BaseFact, ByVal knowFactIds As IEnumerable(Of String)) As Boolean
            Return Not knowFactIds.Any(Function(knownFactId) baseFact.EqualsById(knownFactId))
        End Function
    End Class

End Namespace