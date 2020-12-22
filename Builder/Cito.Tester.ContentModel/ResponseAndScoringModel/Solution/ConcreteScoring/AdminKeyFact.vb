Namespace ResponseAndScoringModel.Solution.ConcreteScoring

    <DebuggerDisplay("Score: [{Score}] for [{ScoredDomains}]")>
    Friend Class ScoreAdminKeyFact

        Private _score As New Dictionary(Of String, Boolean)
        Private _totalCorrectlyScored As Integer = 0
        Private ReadOnly _maxScore As Integer
        Private ReadOnly _nrOfValuesToMatch As Integer

        Public Sub New(maxScore As Integer, nrOfValuesToMatch As Integer)
            _maxScore = maxScore
            _nrOfValuesToMatch = nrOfValuesToMatch
        End Sub

        Sub AddScore(testDomain As String, matched As Boolean)
            Dim scoreToAdd = If(matched, 1, 0)

            If (_score.ContainsKey(testDomain)) Then
                If (Not _score(testDomain)) Then _score(testDomain) = matched
            Else
                _score.Add(testDomain, matched)
            End If

            _totalCorrectlyScored += scoreToAdd
        End Sub

        Public ReadOnly Property ScoredDomains As String
            Get
                Dim ret = New List(Of String)(_score.Keys)
                ret.Sort()
                Return String.Join("&", ret.ToArray())
            End Get
        End Property

        Public ReadOnly Property ScoredPerDomain As IEnumerable(Of KeyValuePair(Of String, Boolean))
            Get
                Return _score
            End Get
        End Property

        Public ReadOnly Property Score As Integer
            Get
                Return If(_totalCorrectlyScored = _nrOfValuesToMatch, _maxScore, 0)
            End Get
        End Property

        Public ReadOnly Property FactIsScoredCorrectly As Boolean
            Get
                Return If(_nrOfValuesToMatch = _totalCorrectlyScored, True, False)
            End Get
        End Property

        Public ReadOnly Property MaxScore As Integer
            Get
                Return _maxScore
            End Get
        End Property

        Public ReadOnly Property NrOfValuesToMatch As Integer
            Get
                Return _nrOfValuesToMatch
            End Get
        End Property

    End Class
End Namespace