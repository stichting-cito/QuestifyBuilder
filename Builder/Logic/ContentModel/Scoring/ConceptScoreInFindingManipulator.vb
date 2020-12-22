Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring
    Friend Class ConceptScoreInFindingManipulator
        Implements IConceptScoreManipulator

        Private ReadOnly _conceptFinding As ConceptFinding
        Private ReadOnly _keyIds As HashSet(Of String)
        Private ReadOnly _combinedScoringMapKey As CombinedScoringMapKey
        Private ReadOnly _scoringMapKey As ScoringMapKey
        Private _conceptScoreManipulator As IScoreManipulator


        Sub New(ByVal conceptFinding As ConceptFinding, ByVal combinedScoringMapKey As CombinedScoringMapKey)
            _conceptFinding = conceptFinding
            _combinedScoringMapKey = combinedScoringMapKey
            _keyIds = New HashSet(Of String)(combinedScoringMapKey.Select(Function(k) k.ScoreKey))
            _scoringMapKey = combinedScoringMapKey.First()
        End Sub



        Public Function GetConceptFactIds() As IEnumerable(Of String) Implements IConceptScoreManipulator.GetConceptIds
            Dim toReturn = New List(Of String)
            Dim possibleKeys = _combinedScoringMapKey.Select(Function(scoringMapKey) scoringMapKey.ScoreKey).ToList()
            Dim manipulator = GetConceptScoringParameter()
            If manipulator IsNot Nothing Then
                Dim reallyManipulated = manipulator.GetKeysAlreadyManipulated().ToList()
                For Each possibleKey As String In reallyManipulated
                    Dim keyToCheck = possibleKey
                    If (possibleKeys.Any(Function(rawKey) DefaultStringOperations.FactIdEquals(keyToCheck, rawKey))) Then
                        toReturn.Add(possibleKey)
                    End If
                Next
                toReturn.Sort(New ConceptIdSorter())
            End If

            Return toReturn
        End Function


        Public Iterator Function GetScoreForPart(partName As String, factIDs As IEnumerable(Of String)) _
    As IEnumerable(Of Integer?) Implements IConceptScoreManipulator.GetScoreForPart
            For Each fact In GetFactsByIDs(factIDs)
                Dim _concept As Concept = Nothing

                If (fact.Concepts Is Nothing) Then fact.Concepts = New ConceptCollection

                For Each concept As Concept In fact.Concepts
                    If (concept.Code.Equals(partName)) Then _concept = concept
                Next
                If (_concept Is Nothing) Then
                    Yield Nothing
                Else
                    Yield _concept.Value
                End If
            Next
        End Function

        Public Sub SetScore(partName As String, factID As String, Score As Integer?) _
            Implements IConceptScoreManipulator.SetScore
            Dim factsFound As Integer = 0

            Dim fact = GetFactById(factID)
            If fact.Concepts Is Nothing Then fact.Concepts = New ConceptCollection()
            Dim conceptFound = False
            factsFound += 1
            For Each c In fact.Concepts
                If (c.Code = partName) Then
                    If (Score IsNot Nothing) Then
                        c.Value = If(Score, shouldNotOccur())
                    Else
                        fact.Concepts.Remove(c)
                    End If
                    conceptFound = True
                    Exit For
                End If
            Next

            If ((Not conceptFound) AndAlso (Score IsNot Nothing)) Then
                fact.Concepts.Add(New Concept(partName, If(Score, shouldNotOccur())))
            End If


            Debug.Assert(factsFound = 1, If(factsFound = 0, "No facts found", "More than one fact updated"))
        End Sub

        Public Function GetDisplayValueForFact(factId As String) As String Implements IConceptScoreManipulator.GetDisplayValueForConceptId
            Dim manipulator = GetConceptScoringParameter()
            If manipulator IsNot Nothing Then
                Return manipulator.GetDisplayValueForKey(factId)
            End If
            Return String.Empty
        End Function

        Public Function GetValueForFact(factId As String) As String Implements IConceptScoreManipulator.GetValueForConceptId
            Dim manipulator = GetConceptScoringParameter()
            If manipulator IsNot Nothing Then
                Return manipulator.GetValueForKey(factId)
            End If
            Return String.Empty
        End Function

        Public Function IsConceptIdDeletable(id As String) As Boolean Implements IConceptScoreManipulator.IsConceptIdDeletable
            Return Not _keyIds.Contains(id)
        End Function

        Sub RemoveConcept(id As String) Implements IConceptScoreManipulator.RemoveConcept
            Dim findingManipulator = New ConceptManipulator(_conceptFinding)
            findingManipulator.RemoveFact(id)
        End Sub

        Public Sub AddCatchAllAnswerCategory() Implements IConceptScoreManipulator.AddCatchAllAnswerCategory
            If Not ContainsCatchAllAnswerCategory() Then
                Dim manipulator = GetConceptScoringParameter()
                Dim factIdForCatchAll As String = String.Empty
                Dim findingManipulator = New ConceptManipulator(_conceptFinding)
                findingManipulator.SetFactSetTarget(Nothing)
                If manipulator IsNot Nothing Then
                    findingManipulator.SetDomainOverride(Function(key) manipulator.GetDomainForKey(key))
                    factIdForCatchAll = GetConceptScoringParameter().GetFactIdForKey(GetBasicCatchAllId(_scoringMapKey))
                Else
                    findingManipulator.SetDomainOverride(Function(key) String.Empty)
                End If

                findingManipulator.SetKey(factIdForCatchAll, New CatchAllValue())
            End If
        End Sub

        Public Function ContainsCatchAllAnswerCategory() As Boolean Implements IConceptScoreManipulator.ContainsCatchAllAnswerCategory
            Dim manipulator = GetConceptScoringParameter()
            If manipulator IsNot Nothing Then
                Dim factIdForCatchAll = manipulator.GetFactIdForKey(GetBasicCatchAllId(_scoringMapKey))
                Dim result = _conceptFinding.Facts.FirstOrDefault(Function(fact) fact.Id = factIdForCatchAll)
                If (result IsNot Nothing) Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function HasPreProcessingRules(id As String) As Boolean Implements IConceptScoreManipulator.HasPreProcessingRules
            Dim manipulator = GetConceptScoringParameter()
            If manipulator IsNot Nothing Then
                Dim gapManipulator = TryCast(manipulator, IGapScoringManipulator)
                If (gapManipulator IsNot Nothing) Then
                    Return gapManipulator.GetPreProcessingMethods(id).Any()
                End If
            End If

            Return False
        End Function


        Private Function GetConceptScoringParameter() As IScoreManipulator
            If _conceptScoreManipulator Is Nothing Then
                Dim ret = ScoringParameterFactory.GetConceptScoreManipulator(_scoringMapKey.ScoringParameter, _conceptFinding)
                ret?.SetFactSetTarget(Nothing)
                _conceptScoreManipulator = ret
            End If
            Return _conceptScoreManipulator
        End Function

        Function GetBasicCatchAllId(scoreMapKey As ScoringMapKey) As String
            Return $"{scoreMapKey.ScoreKey}[*]"
        End Function


        Private Function shouldNotOccur() As Integer
            Debug.Assert(False)
            Throw New Exception
        End Function

        Private Function GetFactById(factId As String) As ConceptFact
            Return GetFactsByIDs(New String() {factId}).FirstOrDefault()
        End Function


        Private Iterator Function GetFactsByIDs(factIDs As IEnumerable(Of String)) As IEnumerable(Of ConceptFact)
            Dim manipulator = GetConceptScoringParameter()
            If manipulator IsNot Nothing Then
                Dim ids = factIDs.Select(Function(key) manipulator.GetFactIdForKey(key)).ToArray()

                For Each id As String In ids
                    For Each fact As ConceptFact In _conceptFinding.Facts
                        If id.Equals(fact.Id) Then Yield fact
                    Next
                Next
            End If
        End Function

    End Class
End Namespace