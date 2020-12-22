Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

Namespace ContentModel

    Friend Class GetIdForAnswerCategory

        Function GetIdForAnswerCategory(scoringMapKey As Scoring.CombinedScoringMapKey, conceptFinding As ConceptFinding) As Integer
            Dim id = -1
            If (scoringMapKey.IsGroup) Then
                id = GetIdForGroup(scoringMapKey, conceptFinding)
            Else
                id = GetIdForFinding(scoringMapKey, conceptFinding)
            End If
            Return id
        End Function

        Function GetIdForAnswerCategory(scoringMapKey As CombinedScoringMapKey, solution As Solution) As Integer
            Dim findingId = scoringMapKey.First().ScoringParameter.FindingId
            Return GetIdForAnswerCategory(scoringMapKey, solution.ConceptFindings.First(Function(conceptFinding) conceptFinding.Id = findingId))
        End Function


        Private Function GetIdForGroup(scoringMapKeys As CombinedScoringMapKey, conceptFinding As ConceptFinding) As Integer

            Dim highest = Integer.MinValue

            For Each key As ScoringMapKey In scoringMapKeys
                Dim sum = conceptFinding.KeyFactsets.Sum(Function(factSet)
                                                             Dim facts = factSet.Facts
                                                             Return GetCountFactsAndGetHighestId(facts, key, highest)
                                                         End Function)

                highest = Math.Max(highest, sum)


            Next

            Return highest
        End Function



        Private Function GetIdForFinding(scoringMapKeys As CombinedScoringMapKey, conceptFinding As ConceptFinding) As Integer
            Dim highest = Integer.MinValue
            Dim sum = 0

            For Each scoringMapKey As ScoringMapKey In scoringMapKeys
                Dim currentSum = GetCountFactsAndGetHighestId(conceptFinding.Facts, scoringMapKey, highest)
                sum = Math.Max(currentSum, sum)
            Next


            highest = Math.Max(highest, sum)

            Return highest
        End Function

        Private Function GetCountFactsAndGetHighestId(ByVal facts As List(Of BaseFact), ByVal key As ScoringMapKey, ByRef highest As Integer) As Integer

            Dim facts2 = facts.Where(Function(fact)
                                         Return Not DefaultStringOperations.IsCatchAllFactId(fact.Id) AndAlso
                                         DefaultStringOperations.FactIdEquals(fact.Id, key.GetFactId())
                                     End Function).ToList()
            Dim cnt = facts2.Count

            If (cnt > 0) Then
                Dim max = GetHighestIdFromFacts(facts2)
                If (max.HasValue) Then
                    highest = Math.Max(highest, max.Value + 1)
                End If
            End If

            Return cnt
        End Function

        Private Function GetHighestIdFromFacts(facts As IEnumerable(Of BaseFact)) As Integer?
            Dim result As Integer? = Nothing

            For Each fact As BaseFact In facts
                Dim possibleNumber = DefaultStringOperations.GetNumberFromFactId(fact.Id)
                If (possibleNumber.HasValue) Then
                    If (result.HasValue) Then
                        result = Math.Max(result.Value, possibleNumber.Value)
                    Else
                        result = possibleNumber.Value
                    End If
                End If
            Next
            Return result
        End Function

    End Class

End Namespace