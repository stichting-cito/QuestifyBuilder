Imports System.Activities
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring

Namespace ContentModel.workers.Flow

    Public NotInheritable Class MoveSingleFactsFromFactSetsToFinding
        Inherits CodeActivity



        Property ScoringMap As InArgument(Of IEnumerable(Of CombinedScoringMapKey))

        Property BaseFacts As InArgument(Of IList(Of BaseFact))

        Property FindingToMoveTo As InArgument(Of KeyFinding)

        Property FactIdsToScoringParameter As InArgument(Of Dictionary(Of String, ScoringParameter))


        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim scoringMap = context.GetValue(Me.ScoringMap)
            Dim facts = context.GetValue(Me.BaseFacts)
            Dim findingToMoveTo = context.GetValue(Me.FindingToMoveTo)
            Dim factIdsToScoringParameter = New Dictionary(Of String, ScoringParameter)(context.GetValue(Me.FactIdsToScoringParameter), New FactIdEqualityComparer())

            If (facts.Count = 1) Then
                Dim fact = DirectCast(facts.First(), KeyFact)
                Dim existingFact As KeyFact = DirectCast(findingToMoveTo.Facts.FirstOrDefault(Function(k) k.Id = fact.Id), KeyFact)
                Dim owningScoringParameter As ScoringParameter = Nothing

                If (factIdsToScoringParameter.TryGetValue(fact.Id, owningScoringParameter)) Then

                    Dim map As CombinedScoringMapKey = scoringMap.First(Function(key As CombinedScoringMapKey) key.Contains(GetScoringKey(owningScoringParameter, fact.Id)))
                    If Not map.IsGroup Then

                        If (existingFact IsNot Nothing) Then
                            TryAddValuesToExistingFact(fact, owningScoringParameter, existingFact)
                        Else
                            AddNewFactToFinding(findingToMoveTo, fact)
                        End If
                    End If

                    If Not owningScoringParameter.GroupInitially Then
                        facts.Remove(fact)
                    End If

                Else
                    Debug.Assert(False, "Key should be present.")
                    Throw New ArgumentException()
                End If

            End If

        End Sub

        Private Sub AddNewFactToFinding(ByVal findingToMoveTo As KeyFinding, ByVal fact As KeyFact)
            findingToMoveTo.Facts.Add(fact)
        End Sub

        Private Sub TryAddValuesToExistingFact(ByVal fact As KeyFact, ByVal owningScoringParameter As ScoringParameter, ByVal existingFact As KeyFact)
            If (owningScoringParameter.IsSingleChoice) Then
            Else
                Dim keyValueTarget = DirectCast(existingFact.Values(0), KeyValue)
                Dim keyValueSource = DirectCast(fact.Values(0), KeyValue)

                Dim valuesNotDuplicated = From newValue In keyValueSource.Values Where Not keyValueTarget.Values.Any(Function(existingValue) existingValue.IsMatch(newValue))

                keyValueTarget.Values.AddRange(valuesNotDuplicated)
            End If
        End Sub

        Private Function GetScoringKey(owningScoringParameter As ScoringParameter, factId As String) As ScoringMapKey
            Dim manipulator = owningScoringParameter.GetScoreManipulator(New Solution)

            For Each [set] In owningScoringParameter.Value
                Dim id = [set].Id
                If (DefaultStringOperations.FactIdEquals(manipulator.GetFactIdForKey(id), factId)) Then
                    Return New ScoringMapKey(owningScoringParameter, id)
                End If
            Next

            Debug.Assert(False, "factset id was not found")
            Throw New ArgumentException()
        End Function


    End Class
End Namespace