Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.Logic.ContentModel

Class SolutionFixer
    Public Shared Sub RemoveOrphanFactsFromSolution(solution As Solution,
                                                    scoringParameters As IEnumerable(Of ScoringParameter))

        Dim keyFindingsToRemove = New List(Of KeyFinding)()

        For Each keyFinding In solution.Findings

            If AnyReferencedKeyFinding(keyFinding, scoringParameters) Then
                RemoveOrphanKeyFacts(solution, scoringParameters, keyFinding)
            Else
                keyFindingsToRemove.Add(keyFinding)
            End If

        Next

        For Each keyFinding In keyFindingsToRemove
            solution.Findings.Remove(keyFinding)
        Next
    End Sub

    Private Shared Sub RemoveOrphanKeyFacts(solution As Solution, scoringParameters As IEnumerable(Of ScoringParameter),
                                            keyFinding As KeyFinding)

        Dim possibleInlineParams =
                scoringParameters.Where(
                    Function(p) _
                                           (keyFinding.Id = p.ControllerId OrElse keyFinding.Id = p.FindingOverride OrElse
                                            keyFinding.Id = p.InlineId))
        Dim possibleParams = GetPossibleParameters(scoringParameters, keyFinding)

        RemoveOrphanKeyFactsFromKeyFactList(solution, keyFinding.Facts, possibleParams, possibleInlineParams)

        For Each keyFactSet As KeyFactSet In keyFinding.KeyFactsets
            RemoveOrphanKeyFactsFromKeyFactList(solution, keyFactSet.Facts, possibleParams, possibleInlineParams)
        Next

        Dim factSetsWithOneFact = keyFinding.KeyFactsets.Where(Function(s) s.Facts.Count = 1)
        If factSetsWithOneFact.Count() > 1 AndAlso AllFactsHaveSameIdPostFix(factSetsWithOneFact) Then
            For Each keyFactSet As KeyFactSet In factSetsWithOneFact
                MoveFactsFromIncompleteKeyFactSet(solution, scoringParameters, keyFinding, keyFactSet)
            Next
        End If

        For Each keyFactSet As KeyFactSet In keyFinding.KeyFactsets.ToList
            If keyFactSet.Facts.Count < 2 Then
                keyFinding.KeyFactsets.Remove(keyFactSet)
            End If
        Next

        Dim map = New ScoringMap(scoringParameters, solution)
        For Each combined As CombinedScoringMapKey In map.GetMap()
            If Not combined.IsGroup Then Continue For

            For Each scoringMapKey In combined
                Dim man = scoringMapKey.ScoringParameter.GetScoreManipulator(solution)
                Dim nrs = man.GetFactSetNumbers(scoringMapKey.ScoreKey)

                If nrs.Count < combined.SetNumbers.Count Then
                    MoveFactsFromOverCompleteFactSet(keyFinding, solution, scoringParameters)
                End If
            Next
        Next
    End Sub

    Private Shared Function AllFactsHaveSameIdPostFix(factSets As IEnumerable(Of KeyFactSet)) As Boolean

        Dim factIds As New List(Of String)
        For Each factSet In factSets
            Dim fact = factSet.Facts.First()
            Dim factIdPostFix = fact.Id.Substring(fact.Id.IndexOf("-"))
            If Not factIds.Contains(factIdPostFix) Then
                factIds.Add(factIdPostFix)
            End If
        Next

        Return factIds.Count = 1
    End Function

    Private Shared Function GetPossibleParameters(scoringParameters As IEnumerable(Of ScoringParameter),
                                                  keyFinding As KeyFinding) As IEnumerable(Of ScoringParameter)
        Return _
            scoringParameters.Where(Function(p) keyFinding.Id = p.ControllerId OrElse keyFinding.Id = p.FindingOverride)
    End Function

    Private Shared Sub MoveFactsFromIncompleteKeyFactSet(solution As Solution,
                                                         scoringParameters As IEnumerable(Of ScoringParameter),
                                                         keyFinding As KeyFinding, keyFactSet As KeyFactSet)
        If keyFactSet.Facts.Any Then
            Dim keyFact As KeyFact = DirectCast(keyFactSet.Facts.First, KeyFact)
            Dim scoringParameter As ScoringParameter = GetScoreParameterForFact(solution, scoringParameters, keyFact)

            Dim existingFact As BaseFact = keyFinding.Facts.FirstOrDefault(Function(k) k.Id = keyFact.Id)
            If existingFact IsNot Nothing Then
                If Not scoringParameter.IsSingleValue Then
                    Dim keyValue As KeyValue = DirectCast(existingFact.Values(0), KeyValue)
                    keyValue.Values.AddRange(DirectCast(keyFact.Values(0), KeyValue).Values)
                End If
            Else
                keyFinding.Facts.Add(keyFact)
            End If
        End If
    End Sub

    Private Shared Sub RemoveOrphanKeyFactsFromKeyFactList(solution As Solution, keyFacts As List(Of BaseFact),
                                                           possibleParams As IEnumerable(Of ScoringParameter),
                                                           possibleInlineParams As IEnumerable(Of ScoringParameter))

        Dim keyFactsToRemove = New List(Of BaseFact)()
        For Each keyFact As KeyFact In keyFacts

            If Not FactIdInCollectionIdx(keyFact, possibleParams) AndAlso
               Not FactIdInSubParameter(solution, keyFact, possibleParams) AndAlso
               Not DomainMatchesInlineId(keyFact, possibleInlineParams) Then

                keyFactsToRemove.Add(keyFact)
            End If

        Next

        For Each keyFact In keyFactsToRemove
            keyFacts.Remove(keyFact)
        Next
    End Sub

    Private Shared Function AnyReferencedKeyFinding(keyFinding As KeyFinding,
                                                    scoringParameters As IEnumerable(Of ScoringParameter)) As Boolean
        Return _
            scoringParameters.Any(
                Function(p) _
                                     keyFinding.Id = p.ControllerId OrElse keyFinding.Id = p.FindingOverride OrElse
                                     keyFinding.Id = p.InlineId)
    End Function

    Private Shared Function FactIdInSubParameter(solution As Solution, keyFact As BaseFact,
                                                 possibleParams As IEnumerable(Of ScoringParameter)) As Boolean
        For Each possibleParameter In possibleParams
            Dim manipulator = possibleParameter.GetScoreManipulator(solution)

            For Each subParameter In possibleParameter.Value
                If manipulator.GetFactIdForKey(subParameter.Id) = keyFact.Id Then
                    Return True
                End If
            Next

        Next
        Return False
    End Function

    Private Shared Function FactIdInCollectionIdx(keyFact As BaseFact,
                                                  possibleParams As IEnumerable(Of ScoringParameter)) As Boolean
        Return possibleParams.Any(Function(p) p.CollectionIdx = keyFact.Id)
    End Function

    Private Shared Function DomainMatchesInlineId(keyFact As BaseFact,
                                                  possibleInlineParams As IEnumerable(Of ScoringParameter)) As Boolean
        For Each keyValue As KeyValue In keyFact.Values
            Dim isMatch = possibleInlineParams.Any(Function(p) p.InlineId = keyValue.Domain)
            If (isMatch) Then Return True
        Next
        Return False
    End Function

    Private Shared Function GetScoreParameterForFact(solution As Solution,
                                                     possibleParams As IEnumerable(Of ScoringParameter),
                                                     keyFact As KeyFact) As ScoringParameter
        For Each possibleParameter In possibleParams
            Dim manipulator = possibleParameter.GetScoreManipulator(solution)

            For Each subParameter In possibleParameter.Value
                If manipulator.GetFactIdForKey(subParameter.Id) = keyFact.Id Then
                    Return possibleParameter
                End If
            Next
        Next
        Return Nothing
    End Function

    Private Shared Sub MoveFactsFromOverCompleteFactSet(keyFinding As KeyFinding, solution As Solution,
                                                        parameters As IEnumerable(Of ScoringParameter))

        For Each factSet In keyFinding.KeyFactsets

            ' find largest factSetCount
            Dim factSetCount = 0
            For Each fact As KeyFact In factSet.Facts
                Dim prm = GetScoreParameterForFact(solution, parameters, fact)
                Dim key = fact.Id.Substring(0, fact.Id.Length - prm.IdentifierPostFix().Length)
                Dim nrs = prm.GetScoreManipulator(solution).GetFactSetNumbers(key)

                If nrs.Count > 0 AndAlso nrs.Count > factSetCount Then
                    factSetCount = nrs.Count
                End If

            Next

            'find facts to move
            Dim factsToRemove = New List(Of KeyFact)
            For Each fact As KeyFact In factSet.Facts
                Dim prm = GetScoreParameterForFact(solution, parameters, fact)
                Dim key = fact.Id.Substring(0, fact.Id.Length - prm.IdentifierPostFix().Length)
                Dim nrs = prm.GetScoreManipulator(solution).GetFactSetNumbers(key)

                If nrs.Count < factSetCount Then
                    factsToRemove.Add(fact)
                End If

            Next

            '(re)move facts
            For Each fact As KeyFact In factsToRemove

                factSet.Facts.Remove(fact)

                Dim existingFact As BaseFact = keyFinding.Facts.FirstOrDefault(Function(k) k.Id = fact.Id)
                If existingFact Is Nothing Then
                    keyFinding.Facts.Add(fact)
                End If
            Next

        Next
    End Sub
End Class
