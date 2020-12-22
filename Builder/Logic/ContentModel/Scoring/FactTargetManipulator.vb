Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Public Class FactTargetManipulator

        Private ReadOnly _solution As Solution

        Public Sub New(solution As Solution)
            _solution = solution
        End Sub

        Public Function CanGroupInteractions(scoringMapKeys As IEnumerable(Of ScoringMapKey), scoringMap As IEnumerable(Of CombinedScoringMapKey)) As Boolean

            If scoringMapKeys.Count() = 1 Then
                If Not scoringMap.Any(Function(c) c.IsGroup) Then
                    Return False
                End If
            End If

            For Each scoringMapKey In scoringMapKeys
                Dim scoringParameter = scoringMapKey.ScoringParameter
                Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringParameter, _solution)
                Dim key = scoringMapKey.ScoreKey
                If InteractionPartOfAnyFactSet(scoreManipulator, key) Then Return False
                If Not scoringParameter.Groupable Then Return False
            Next

            Return True
        End Function

        Private Function InteractionPartOfAnyFactSet(scoreManipulator As IScoreManipulator, key As String) As Boolean
            If (scoreManipulator.GetFactSetNumbers(key) IsNot Nothing AndAlso scoreManipulator.GetFactSetNumbers(key).Count > 0) Then
                Return True
            End If
            Return False
        End Function

        Public Function GroupInteractions(scoringMapKeys As IEnumerable(Of ScoringMapKey)) As Integer
            Debug.Assert(scoringMapKeys.Count() >= 2, "Should be 2 or more.")

            Dim newFactSetNumber As Integer = CreateFactSet(scoringMapKeys)

            For Each scoringMapKey In scoringMapKeys
                Dim factMoverForKey = FactMoverFactory.Create(scoringMapKey, _solution)
                factMoverForKey.MoveFacts(Nothing, newFactSetNumber)
            Next
            Return newFactSetNumber
        End Function

        Private Function CreateFactSet(scoringMapKeys As IEnumerable(Of ScoringMapKey)) As Integer
            Dim scoringParameter = scoringMapKeys.First().ScoringParameter
            Dim factSetNumber = CreateFactSetTarget(scoringParameter)
            Return factSetNumber
        End Function

        Private Function CreateFactSetTarget(scoringParameter As ScoringParameter) As Integer
            Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringParameter, _solution)
            Return scoreManipulator.CreateFactSetTarget()
        End Function

        Public Sub AddToGroup(scoringMapKey As ScoringMapKey, factSetNumbers As IEnumerable(Of Integer))
            AddInteractionToGroup.Create(scoringMapKey.ScoringParameter, _solution).Execute(scoringMapKey.ScoreKey, factSetNumbers)
        End Sub

        Public Function CanRemoveFromGroup(scoringMapKey As ScoringMapKey, factSetNumber As Integer, scoringMap As IEnumerable(Of CombinedScoringMapKey)) As Boolean
            If Not scoringMap.Any(Function(smk) smk.SetNumbers.Contains(factSetNumber)) Then Return False

            Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringMapKey.ScoringParameter, _solution)
            Return scoreManipulator.CanBeRemovedFromFactSet(scoringMapKey.ScoreKey)
        End Function

        Public Sub RemoveFromGroup(scoringMapKey As ScoringMapKey, scoringMap As IEnumerable(Of CombinedScoringMapKey))
            Dim combinedScoringMapContainsMapKey = scoringMap.First(Function(csm) csm.Contains(scoringMapKey))

            Debug.Assert(combinedScoringMapContainsMapKey.IsGroup, "Should be a group")

            UnGroup(scoringMapKey, combinedScoringMapContainsMapKey.SetNumbers)

            Dim scoringMapKeys = combinedScoringMapContainsMapKey.ToList()
            If (scoringMapKey.ScoringParameter.IsSingleChoice) Then
                scoringMapKeys.RemoveAll(Function(p) p.ScoringParameter.Equals(scoringMapKey.ScoringParameter))
            Else
                scoringMapKeys.Remove(scoringMapKey)
            End If
            Dim updatedCombinedScoringMapKey = CombinedScoringMapKey.Create(scoringMapKeys)

            If (Not updatedCombinedScoringMapKey.IsGroup) Then
                UnGroup(updatedCombinedScoringMapKey.First(), combinedScoringMapContainsMapKey.SetNumbers)
            End If
        End Sub

        Private Sub UnGroup(scoringMapKey As ScoringMapKey, factSets As IEnumerable(Of Integer))
            Dim mover = FactMoverFactory.Create(scoringMapKey, _solution)
            For Each factSetNumber In factSets
                mover.MoveFacts(factSetNumber, Nothing)
            Next
        End Sub


        Public Function CanAddFactSet(interactions As IEnumerable(Of ScoringMapKey)) As Boolean
            If interactions Is Nothing OrElse interactions.Count < 1 Then Return False

            Dim firstKey = interactions.First().ScoreKey
            Dim firstScoringParameter = interactions.First().ScoringParameter
            Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(firstScoringParameter, _solution)
            Dim factSetNumbers = scoreManipulator.GetFactSetNumbers(firstKey)
            If factSetNumbers Is Nothing OrElse factSetNumbers.Count < 1 Then Return False

            Return True
        End Function

        Public Function AddFactSet(interactions As IEnumerable(Of ScoringMapKey)) As Integer
            Dim firstScoringParameter = interactions.First().ScoringParameter
            Dim newFactSetIndex = CreateFactSetTarget(firstScoringParameter)

            For Each scoringMapKey In interactions
                Dim scoringParameter = scoringMapKey.ScoringParameter
                DefaultValuesSetter.Create(scoringParameter, _solution).Execute(scoringMapKey.ScoreKey, newFactSetIndex)
            Next

            Return newFactSetIndex
        End Function

        Public Function CanRemoveFactSet(scoringMapKey As ScoringMapKey, factSetNumber As Integer?) As Boolean

            If factSetNumber Is Nothing OrElse Not factSetNumber.HasValue Then Return False
            Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringMapKey.ScoringParameter, _solution)
            Dim factSetNumbers = scoreManipulator.GetFactSetNumbers(scoringMapKey.ScoreKey)
            If factSetNumbers.Count() < 2 Then Return False

            Return True
        End Function

        Public Sub RemoveFactSet(scoringMapKey As ScoringMapKey, factSetNumber As Integer)
            Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringMapKey.ScoringParameter, _solution)
            scoreManipulator.RemoveFactSetTarget(factSetNumber)
        End Sub

        Public Function GetFactSetNumbers(key As String, scoringParameter As ScoringParameter) As IEnumerable(Of Integer)
            Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringParameter, _solution)
            Return scoreManipulator.GetFactSetNumbers(key)
        End Function

    End Class
End Namespace