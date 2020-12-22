Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Friend Class FactMover : Implements IFactMover

        Private ReadOnly _scoringMapKey As ScoringMapKey
        Private ReadOnly _scoreManipulator As IScoreManipulator
        Private ReadOnly _findingManipulator As IFindingManipulator

        Public Sub New(scoringMapKey As ScoringMapKey, scoreManipulator As IScoreManipulator, findingManipulator As IFindingManipulator)
            _scoringMapKey = scoringMapKey
            _scoreManipulator = scoreManipulator
            _findingManipulator = findingManipulator
        End Sub

        Public Sub MoveFacts(sourceFactSetNumber As Integer?, targetFactSetNumber As Integer?) Implements IFactMover.MoveFacts

            _findingManipulator.SetFactSetTarget(sourceFactSetNumber)

            Dim factId = _scoreManipulator.GetFactIdForKey(_scoringMapKey.ScoreKey)
            Dim factToMove = _findingManipulator.GetFacts(factId).FirstOrDefault()
            If factToMove IsNot Nothing Then
                _findingManipulator.RemoveFact(factToMove.Id)
            End If
            _findingManipulator.SetFactSetTarget(targetFactSetNumber)
            Dim newFact = _findingManipulator.FindOrCreateFact(factId)
            If factToMove IsNot Nothing AndAlso
                ((TypeOf _scoringMapKey.ScoringParameter Is MultiChoiceScoringParameter AndAlso Not _scoringMapKey.ScoringParameter.IsSingleValue AndAlso _scoringMapKey.ScoringParameter.IsSingleChoice) OrElse
                (Not TypeOf _scoringMapKey.ScoringParameter Is MultiChoiceScoringParameter AndAlso Not _scoringMapKey.ScoringParameter.IsSingleValue) OrElse
                newFact.Values.Count = 0) Then
                CopyFactValues(factToMove, newFact)
            End If

        End Sub

        Friend Shared Sub CopyFactValues(source As BaseFact, destination As BaseFact)

            Debug.Assert(source.Values.Count = 1, "Can not handle more than 1 value")
            Debug.Assert(TypeOf source Is KeyFact)
            Debug.Assert(TypeOf destination Is KeyFact)

            Dim keyfactSource = DirectCast(source, KeyFact)
            Dim keyfactDest = DirectCast(destination, KeyFact)

            For Each baseFactValue In keyfactSource.Values
                If (keyfactDest.Values.Count = 0) Then
                    keyfactDest.Values.Add(baseFactValue)
                Else
                    Dim dest = DirectCast(keyfactDest.Values(0), KeyValue)
                    Dim src = DirectCast(keyfactSource.Values(0), KeyValue)

                    For Each keyval In src.Values
                        dest.Values.Add(keyval)
                    Next

                End If
            Next

        End Sub
    End Class

End Namespace