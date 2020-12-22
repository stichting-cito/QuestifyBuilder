Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring
    Public Class ScoringMap


        Private ReadOnly _scoringParameters As List(Of ScoringParameter)
        Private ReadOnly _solution As Solution

        Private Const NotInFactSet As Integer = -9999
        Friend Const InFactSetToBeAssigned As Integer = -9998

        Private _byFactSetNumber As Dictionary(Of Integer, List(Of ScoringMapKey))
        Private _byScoreMapKey As Dictionary(Of ScoringMapKey, List(Of Integer))




        Public Sub New(scoringParameters As IEnumerable(Of ScoringParameter), solution As Solution)
            _scoringParameters = scoringParameters.ToList()
            _solution = solution
        End Sub


        Public Iterator Function GetMap() As IEnumerable(Of CombinedScoringMapKey)
            DoActualWork()
            Dim alreadyReturned As New HashSet(Of ScoringMapKey)()

            For Each mapKey In GetScoringMapKeys()
                If (Not alreadyReturned.Contains(mapKey)) Then

                    Dim result = AddCompanions(mapKey)
                    alreadyReturned.UnionWith(result)

                    Yield result

                End If
            Next
        End Function

        Private Sub DoActualWork()
            If (Not ((_byFactSetNumber Is Nothing) AndAlso (_byScoreMapKey Is Nothing))) Then Return

            For Each scoreMapKey In GetScoringMapKeys()
                Dim factSetNumbers = GetFactSetNumbers(scoreMapKey)
                RegisterByLocation(factSetNumbers, scoreMapKey)
                RegisterForScoreKey(scoreMapKey, factSetNumbers)
            Next

        End Sub


        Private Sub RegisterByLocation(factSetNumbers As IEnumerable(Of Integer), scoreMapKey As ScoringMapKey)
            If (_byFactSetNumber Is Nothing) Then _
                _byFactSetNumber = New Dictionary(Of Integer, List(Of ScoringMapKey))()

            If (Not factSetNumbers.Any()) Then
                factSetNumbers = New List(Of Integer)(New Integer() {NotInFactSet})
            End If

            For Each factSetNumber In factSetNumbers

                If (Not _byFactSetNumber.ContainsKey(factSetNumber)) Then
                    _byFactSetNumber.Add(factSetNumber, New List(Of ScoringMapKey)())
                End If

                _byFactSetNumber(factSetNumber).Add(scoreMapKey)
            Next
        End Sub

        Private Sub RegisterForScoreKey(scoreMapKey As ScoringMapKey, factSetNumbers As IEnumerable(Of Integer))
            If (_byScoreMapKey Is Nothing) Then _byScoreMapKey = New Dictionary(Of ScoringMapKey, List(Of Integer))()

            If (Not factSetNumbers.Any()) Then
                factSetNumbers = New List(Of Integer)(New Integer() {NotInFactSet})
            End If

            If (Not _byScoreMapKey.ContainsKey(scoreMapKey)) Then
                _byScoreMapKey.Add(scoreMapKey, New List(Of Integer)())
            End If

            _byScoreMapKey(scoreMapKey).AddRange(factSetNumbers)
        End Sub

        Private Iterator Function GetIds(scoringParameter As ScoringParameter) As IEnumerable(Of String)
            If (scoringParameter.Value IsNot Nothing) Then
                For Each subCollection In scoringParameter.Value
                    Yield subCollection.Id
                Next
            End If
        End Function

        Private Function GetFactSetNumbers(scoreMapKey As ScoringMapKey) As IEnumerable(Of Integer)
            Dim setNumbers As IEnumerable(Of Integer) = DoGetFactSetNumbers(scoreMapKey)

            If ((Not setNumbers.Any()) AndAlso scoreMapKey.ScoringParameter.GroupInitially) Then
                setNumbers = New Integer() {InFactSetToBeAssigned}
            End If

            Return setNumbers
        End Function

        Protected Overridable Function DoGetFactSetNumbers(ByVal scoreMapKey As ScoringMapKey) As IEnumerable(Of Integer)
            Dim manipulator = scoreMapKey.ScoringParameter.GetScoreManipulator(_solution)
            If manipulator IsNot Nothing Then
                Return manipulator.GetFactSetNumbers(scoreMapKey.ScoreKey)
            End If
            Return New List(Of Integer)
        End Function

        Private Iterator Function GetScoringMapKeys() As IEnumerable(Of ScoringMapKey)
            For Each sp In _scoringParameters
                For Each key In GetIds(sp)
                    Yield New ScoringMapKey(sp, key)
                Next
            Next
        End Function

        Private Function AddCompanions(scoringMapKey As ScoringMapKey) As CombinedScoringMapKey
            Dim ret As New HashSet(Of ScoringMapKey)
            ret.Add(scoringMapKey)

            Dim locations = _byScoreMapKey(scoringMapKey)
            For Each location In locations
                If (Not (location = NotInFactSet)) Then
                    For Each companion In _byFactSetNumber(location)
                        ret.Add(companion)
                    Next

                ElseIf (scoringMapKey.ScoringParameter.IsSingleChoice) Then
                    For Each companion In _byFactSetNumber(location)
                        If (ReferenceEquals(scoringMapKey.ScoringParameter, companion.ScoringParameter)) Then _
    ret.Add(companion)

                    Next
                End If
            Next

            If (locations.Count = 1 AndAlso locations(0) = NotInFactSet) Then
                Return CombinedScoringMapKey.Create(ret)
            Else
                Return CombinedScoringMapKey.Create(ret, locations)
            End If
        End Function

        Protected ReadOnly Property Solution As Solution
            Get
                Return _solution
            End Get
        End Property

    End Class
End Namespace

