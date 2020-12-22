Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Text

Namespace ContentModel.Scoring
    Friend Class ConceptScoreInSetsManipulator
        Implements IConceptScoreManipulator


        Private ReadOnly _conceptFinding As ConceptFinding
        Private ReadOnly _conceptMapKey As CombinedScoringMapKey
        Private ReadOnly _conceptFactSetsNumber As List(Of Integer)
        Private ReadOnly _keyFactSetsNumber As List(Of Integer)



        Public Sub New(ByVal finding As ConceptFinding,
                       ByVal conceptMapKey As CombinedScoringMapKey,
                       ByVal keyFactSetsNumber As IEnumerable(Of Integer))
            _conceptFinding = finding
            _conceptMapKey = conceptMapKey
            _conceptFactSetsNumber = conceptMapKey.SetNumbers.ToList()
            _keyFactSetsNumber = keyFactSetsNumber.ToList()
        End Sub



        Public Function GetConceptFactIds() As IEnumerable(Of String) Implements IConceptScoreManipulator.GetConceptIds
            Dim firstFactIdAndSetNumbers = New Dictionary(Of String, List(Of String))
            For Each factSetId In _conceptFactSetsNumber.Select(Function(nr) nr.ToString())
                Dim manipulator = GetConceptScoringParamater(_conceptMapKey.First())
                manipulator.SetFactSetTarget(GetSetNumber(factSetId))
                Dim firstKey = manipulator.GetKeysAlreadyManipulated().OrderBy(Function(stringValue) stringValue).First()

                If (Not firstFactIdAndSetNumbers.ContainsKey(firstKey)) Then firstFactIdAndSetNumbers.Add(firstKey, New List(Of String)())
                firstFactIdAndSetNumbers(firstKey).Add(factSetId)
            Next

            Dim factKeys = New List(Of String)(firstFactIdAndSetNumbers.Keys)
            factKeys.Sort(New ConceptIdSorter())

            Return factKeys.SelectMany(Function(factKey) firstFactIdAndSetNumbers(factKey))
        End Function

        Public Function GetDisplayValueForConceptId(id As String) As String _
            Implements IConceptScoreManipulator.GetDisplayValueForConceptId

            Dim returnValueSb As New StringBuilder()

            For Each scoreMapKey In _conceptMapKey
                Dim manipulator = GetConceptScoringParamater(scoreMapKey)
                Dim targetSet = GetSetNumber(id)
                manipulator.SetFactSetTarget(targetSet)
                Dim oldLength = returnValueSb.Length
                returnValueSb.Append(manipulator.GetDisplayValueForKey(scoreMapKey.ScoreKey))
                If (oldLength <> returnValueSb.Length) Then returnValueSb.Append("&")
            Next
            If (returnValueSb.Length > 0) Then
                returnValueSb.Remove(returnValueSb.Length - 1, 1)
            End If

            Return returnValueSb.ToString()
        End Function

        Public Function GetValueForConceptId(id As String) As String _
            Implements IConceptScoreManipulator.GetValueForConceptId
            Return GetDisplayValueForConceptId(id)
        End Function

        Public Iterator Function GetScoreForPart(partName As String, ids As IEnumerable(Of String)) _
            As IEnumerable(Of Integer?) Implements IConceptScoreManipulator.GetScoreForPart

            For Each setNr In ids.Select(Function(strId) GetSetNumber(strId))

                Dim conceptSet = GetConceptSet(setNr)
                If (conceptSet.Concepts Is Nothing) Then conceptSet.Concepts = New ConceptCollection()

                Dim concept As Concept = conceptSet.Concepts.FirstOrDefault(Function(conceptElement) conceptElement.Code.Equals(partName))

                If (concept Is Nothing) Then
                    Yield Nothing
                Else
                    Yield concept.Value
                End If

            Next
        End Function

        Public Sub SetScore(partName As String, id As String, Score As Integer?) _
            Implements IConceptScoreManipulator.SetScore
            Dim setNr = GetSetNumber(id)

            Dim conceptSet = GetConceptSet(setNr)

            If (conceptSet.Concepts Is Nothing) Then conceptSet.Concepts = New ConceptCollection()

            conceptSet.Concepts.Remove(partName)
            If (Score.HasValue) Then
                conceptSet.Concepts.Add(partName, Score.Value)
            End If
        End Sub

        Public Function IsConceptIdDeletable(id As String) As Boolean _
            Implements IConceptScoreManipulator.IsConceptIdDeletable

            Dim cfsIndex As Integer = Integer.Parse(id)

            If _conceptFinding.KeyFactsets IsNot Nothing AndAlso _conceptFinding.KeyFactsets.Count > cfsIndex Then
                Return _conceptFinding.KeyFactsets(cfsIndex).Facts.Any(Function(x) DefaultStringOperations.IsCatchAllOrAnswerCategoryFactId(x.Id))
            End If

            Return False
        End Function

        Sub RemoveConcept(id As String) Implements IConceptScoreManipulator.RemoveConcept
            Dim findingManipulator = New ConceptManipulator(_conceptFinding)
            findingManipulator.RemoveFactSetTarget(Integer.Parse(id))
        End Sub

        Public Sub AddCatchAllAnswerCategory() Implements IConceptScoreManipulator.AddCatchAllAnswerCategory
            If (Not ContainsCatchAllAnswerCategory()) Then

                Dim setNumber = CreateAFactSet()

                Dim findingManipulator = New ConceptManipulator(_conceptFinding)
                findingManipulator.SetFactSetTarget(setNumber)

                For Each scoreKey In _conceptMapKey
                    Dim factIdForCatchAll = GetConceptScoringParamater(scoreKey).GetFactIdForKey(GetBasicCatchAllId(scoreKey))
                    findingManipulator.SetDomainOverride(Function(key) GetConceptScoringParamater(scoreKey).GetDomainForKey(key))
                    findingManipulator.SetKey(factIdForCatchAll, New CatchAllValue())
                Next

            End If
        End Sub

        Public Function ContainsCatchAllAnswerCategory() As Boolean Implements IConceptScoreManipulator.ContainsCatchAllAnswerCategory

            Dim firstScoreMapKey = _conceptMapKey.First()
            Dim manipulator = GetConceptScoringParamater(firstScoreMapKey)
            Dim factIdForCatchAll = manipulator.GetFactIdForKey(GetBasicCatchAllId(firstScoreMapKey))

            For Each factSetNumber As Integer In _conceptFactSetsNumber
                Dim result = _conceptFinding.KeyFactsets(factSetNumber).Facts.FirstOrDefault(Function(fact) fact.Id = factIdForCatchAll)
                If (result IsNot Nothing) Then Return True
            Next

            Return False
        End Function

        Public Function HasPreProcessingRules(id As String) As Boolean Implements IConceptScoreManipulator.HasPreProcessingRules
            For Each scoreMapKey In _conceptMapKey
                Dim manipulator = GetConceptScoringParamater(scoreMapKey)

                Dim gapManipulator = TryCast(manipulator, IGapScoringManipulator)
                If (gapManipulator IsNot Nothing) Then
                    Dim targetSet = GetSetNumber(id)
                    manipulator.SetFactSetTarget(targetSet)

                    If (gapManipulator.GetPreProcessingMethods(scoreMapKey.ScoreKey).Any()) Then Return True

                Else
                    Continue For
                End If
            Next
            Return False
        End Function


        Private Function GetSetNumber(id As String) As Integer
            Dim setNumber As Integer = -1

            If (Integer.TryParse(id, setNumber)) Then
                If (Not _conceptFactSetsNumber.Contains(setNumber)) Then
                    Throw New ArgumentException("id")
                End If
            Else
                Throw New ArgumentException("id")
            End If

            Return setNumber
        End Function

        Private Function GetConceptSet(setNr As Integer) As ConceptFactsSet
            Return DirectCast(_conceptFinding.KeyFactsets(setNr), ConceptFactsSet)
        End Function

        Private Function CreateAFactSet() As Integer
            Dim manipulator = GetConceptScoringParamater(_conceptMapKey.First())
            Return manipulator.CreateFactSetTarget()
        End Function

        Private Function GetConceptScoringParamater(scoringMapkey As ScoringMapKey) As IScoreManipulator
            Return ScoringParameterFactory.GetConceptScoreManipulator(scoringMapkey.ScoringParameter, _conceptFinding)
        End Function

        Function GetBasicCatchAllId(scoreMapKey As ScoringMapKey) As String
            Return $"{scoreMapKey.ScoreKey}[*]"
        End Function

    End Class
End Namespace


