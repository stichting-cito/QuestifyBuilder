Imports System.Activities
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

Namespace ContentModel.workers.Flow

    Public NotInheritable Class EnforceSingleChoiceScore
        Inherits CodeActivity

        Property Solution As InArgument(Of Solution)

        Property CombinedScoringMapKey As InArgument(Of CombinedScoringMapKey)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim sol = context.GetValue(Me.Solution)
            Dim combScoringMapKey = context.GetValue(CombinedScoringMapKey)


            Dim singleChoiceScoringParameterrs = GetSingleChoiceScoringParameters(combScoringMapKey)

            For Each singleChoice In singleChoiceScoringParameterrs

                Dim finding = sol.ConceptFindings.First(Function(f) f.Id = singleChoice.FindingId)

                If (combScoringMapKey.IsGroup) Then
                    RemoveExcessFactsInGroup(singleChoice, finding)
                Else
                    RemoveExcessFacts(singleChoice, finding)
                End If


            Next
        End Sub

        Private Sub RemoveExcessFactsInGroup(singleChoice As ChoiceScoringParameter, finding As ConceptFinding)
            For Each sets In finding.KeyFactsets
                RemoveFromFactsCollection(singleChoice, sets.Facts)
            Next
        End Sub

        Private Sub RemoveExcessFacts(singleChoice As ChoiceScoringParameter, finding As ConceptFinding)
            RemoveFromFactsCollection(singleChoice, finding.Facts)
        End Sub

        Private Sub RemoveFromFactsCollection(singleChoice As ChoiceScoringParameter, ByRef list As List(Of BaseFact))

            Dim possibleFactIds = GetFactIds(singleChoice)

            Dim cnt = 0

            Do
                cnt = Enumerable.Count(list, Function(fact) possibleFactIds.Contains(fact.Id))

                If (cnt > 1) Then
                    Dim firstToRemove = list.First(Function(fact) possibleFactIds.Contains(fact.Id))
                    list.Remove(firstToRemove)
                End If

            Loop While cnt > 1

        End Sub

        Private Function GetSingleChoiceScoringParameters(ByVal combinedScoringMapKey As CombinedScoringMapKey) As List(Of ChoiceScoringParameter)

            Dim singleChoiceParams = (From scoringMapKey In combinedScoringMapKey
                                      Where ScoringMapKey.ScoringParameter.IsSingleChoice
                                      Where ScoringMapKey.ScoringParameter.IsSingleValue
                                      Select ScoringMapKey.ScoringParameter).Distinct().Cast(Of ChoiceScoringParameter)()

            Return singleChoiceParams.ToList()
        End Function

        Private Function GetFactIds(singleChoice As ChoiceScoringParameter) As List(Of String)
            Dim conceptScoringManipulator = ScoringParameterFactory.GetConceptScoreManipulator(Of IChoiceScoringManipulator)(singleChoice, New Solution())

            Return singleChoice.Value.Select(
                Function(parameterCollection) conceptScoringManipulator.GetFactIdForKey(parameterCollection.Id)
                    ).ToList()

        End Function

    End Class

End Namespace