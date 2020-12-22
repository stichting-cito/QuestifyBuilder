Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Friend Class ChoiceFactMover : Implements IFactMover



        Private ReadOnly _scoringMapKeys As IEnumerable(Of ScoringMapKey)
        Private ReadOnly _scoreManipulator As IScoreManipulator
        Private ReadOnly _findingManipulator As IFindingManipulator

        Public Sub New(scoreManipulator As IScoreManipulator, findingManipulator As IFindingManipulator, scoringMapKeys As IEnumerable(Of ScoringMapKey))
            _scoreManipulator = scoreManipulator
            _findingManipulator = findingManipulator
            _scoringMapKeys = scoringMapKeys
        End Sub

        Public Sub MoveFacts(sourceFactSetNumber As Integer?, targetFactSetNumber As Integer?) Implements IFactMover.MoveFacts

            _findingManipulator.SetFactSetTarget(sourceFactSetNumber)
            Dim factsToMove As New List(Of BaseFact)
            For Each scoringMapKey In _scoringMapKeys
                Dim factId = _scoreManipulator.GetFactIdForKey(scoringMapKey.ScoreKey)
                Dim fact = _findingManipulator.GetFacts(factId).FirstOrDefault()
                If fact IsNot Nothing Then
                    factsToMove.Add(fact)
                    _findingManipulator.RemoveFact(fact.Id)
                End If
            Next

            _findingManipulator.SetFactSetTarget(targetFactSetNumber)

            If _scoringMapKeys.Any(Function(m) _findingManipulator.GetFacts(_scoreManipulator.GetFactIdForKey(m.ScoreKey)).Any) Then
                Return
            End If

            If factsToMove.Count = 0 Then
                Dim factId = _scoreManipulator.GetFactIdForKey(_scoringMapKeys(0).ScoreKey)
                Dim newFact = _findingManipulator.FindOrCreateFact(factId)
            Else

                Dim newFact = _findingManipulator.FindOrCreateFact(factsToMove(0).Id)
                FactMover.CopyFactValues(factsToMove(0), newFact)
            End If
        End Sub
    End Class
End Namespace
