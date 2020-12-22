Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel

    Friend Class ConceptFactsSetLogic

        Private setIdSeen As HashSet(Of Integer)

        Public Function GetUnusedConceptFactSetIds(ByVal combinedKeyScoringMapKey As CombinedScoringMapKey, ByVal solution As Solution) As IEnumerable(Of Integer)

            Dim conceptCombinedScoringMapKey = GetConceptCombinedScoringMapKey(combinedKeyScoringMapKey, solution)

            If (conceptCombinedScoringMapKey IsNot Nothing) Then

                setIdSeen = New HashSet(Of Integer)()
                Dim keyFactSetsIds = combinedKeyScoringMapKey.SetNumbers.ToList()
                Dim conceptFactSets = GetConceptFactSetsBySequenceId(conceptCombinedScoringMapKey.SetNumbers, solution).ToList()

                Return (From IdFactSetCombination In conceptFactSets
                        Let isOrphan = IsSetOrphanaged(solution, keyFactSetsIds, IdFactSetCombination)
                        Where (isOrphan) Select IdFactSetCombination.Key).ToList()

            End If

            Return Enumerable.Empty(Of Integer)()
        End Function

        Private Function IsSetOrphanaged(ByVal solution As Solution, ByVal keyFactSetsIds As List(Of Integer), ByVal IdFactSetCombination As KeyValuePair(Of Integer, KeyFactSet)) As Boolean
            Dim hasMirrorSet = keyFactSetsIds.Any(Function(keyFactId)

                                                      If (Not setIdSeen.Contains(keyFactId)) Then
                                                          Dim comparer = New FactSetEqualityComparer()

                                                          Dim ret = comparer.CompareEquals(IdFactSetCombination.Value,
                                                                                        solution.Findings.First().KeyFactsets(keyFactId))

                                                          If (ret) Then setIdSeen.Add(keyFactId)

                                                          Return ret
                                                      End If
                                                  End Function)
            Return Not hasMirrorSet
        End Function

        Private Function GetConceptCombinedScoringMapKey(ByVal combinedScoringMapKey As CombinedScoringMapKey,
                                                         ByVal solution As Solution) As CombinedScoringMapKey
            Return New ConceptScoringMap(combinedScoringMapKey, solution).GetMap().SingleOrDefault(Function(combinedScoringMap) combinedScoringMap.Name = combinedScoringMapKey.Name)
        End Function

        Private Function GetConceptFactSetsBySequenceId(conceptFactSetIds As IEnumerable(Of Integer), solution As Solution) As Dictionary(Of Integer, KeyFactSet)
            Dim ret = New Dictionary(Of Integer, KeyFactSet)

            Dim conceptSets = solution.ConceptFindings.FirstOrDefault()

            If (conceptSets IsNot Nothing) Then

                For Each id In conceptFactSetIds

                    Dim conceptSet = conceptSets.KeyFactsets(id)
                    If (IsRegularKeyFactSet(conceptSet)) Then
                        ret.Add(id, conceptSet)
                    End If

                Next
            End If

            Return ret
        End Function

        Private Function IsRegularKeyFactSet(ByVal keyFactSet As KeyFactSet) As Boolean
            Return keyFactSet.Facts.All(Function(fact) IsRegularKeyFact(fact))
        End Function

        Private Function IsRegularKeyFact(ByVal baseFact As BaseFact) As Boolean
            Return Not DefaultStringOperations.IsCatchAllOrAnswerCategoryFactId(baseFact.Id)
        End Function
    End Class

End Namespace