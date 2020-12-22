Imports System.Linq
Namespace ResponseAndScoringModel.Solution.ConcreteScoring

    <DebuggerDisplay("CumulativeScore Score: [{CumulativeScore}] for Domains [{Domains}]")>
    Friend Class ScoreAdminKeyFinding

        Shared ReadOnly _empty As New ScoreAdminKeyFinding()

        Shared ReadOnly Property Empty As ScoreAdminKeyFinding
            Get
                Return _empty
            End Get
        End Property


        Private _allFactsShouldMatch As Boolean = False
        Private _nrOfKeyFacts As Integer = Integer.MinValue

        Public Sub New(Optional allFactsShouldMatch As Boolean = False, Optional nrOfKeyFacts As Integer = Integer.MinValue)
            _allFactsShouldMatch = allFactsShouldMatch
            If Not nrOfKeyFacts = Integer.MinValue Then _nrOfKeyFacts = nrOfKeyFacts
        End Sub

        Private _score As New Dictionary(Of String, List(Of ScoreAdminKeyFact))

        Sub AddScoreKeyFact(scoreAdminKeyFact As ScoreAdminKeyFact)
            Dim key = scoreAdminKeyFact.ScoredDomains

            If (_score.ContainsKey(key)) Then
                _score(key).Add(scoreAdminKeyFact)
            Else
                _score.Add(key, New List(Of ScoreAdminKeyFact)(New ScoreAdminKeyFact() {scoreAdminKeyFact}))
            End If

        End Sub

        Public ReadOnly Property CumulativeScore As Integer
            Get
                If (Not HasResults) Then Return 0
                If _allFactsShouldMatch Then Return If(AllFactsScored, 1, 0)
                Return _score.Values.Sum(Function(x) x.Sum(Function(y) y.Score))
            End Get
        End Property

        Public ReadOnly Property AllFactsScored As Boolean
            Get
                If (Not HasResults) Then Return False
                If Not _nrOfKeyFacts = Integer.MinValue AndAlso _allFactsShouldMatch Then Return (_score.Values.Sum(Function(x) x.Where(Function(y) y.FactIsScoredCorrectly).Count()) = _nrOfKeyFacts)
                Return _score.Values.All(Function(x) x.All(Function(y) y.FactIsScoredCorrectly))
            End Get
        End Property

        Public ReadOnly Property NrOfNonScoredFacts As Integer
            Get
                If (Not HasResults) Then Return 0
                If _allFactsShouldMatch AndAlso Not AllFactsScored Then Return _score.Values.Sum(Function(x) x.Count())
                Return _score.Values.Sum(Function(x) x.Where(Function(y) Not y.FactIsScoredCorrectly).Count())
            End Get
        End Property

        Public ReadOnly Property NrOfCorrectlyScoredFacts As Integer
            Get
                If (Not HasResults) Then Return 0
                If _allFactsShouldMatch AndAlso Not AllFactsScored Then Return 0
                Return _score.Values.Sum(Function(x) x.Where(Function(y) y.FactIsScoredCorrectly).Count())
            End Get
        End Property

        Public ReadOnly Property Domains As String
            Get
                If (Not HasResults) Then Return String.Empty
                Dim sortedDict = (From key In _score.Keys Order By key Select key)
                Return sortedDict.Aggregate(Function(c, n) c & ", " & n)
            End Get
        End Property

        Public ReadOnly Property HasResults As Boolean
            Get
                Return _score.Count > 0
            End Get
        End Property

        Public ReadOnly Property TotalNrOfFacts As Integer
            Get
                If (Not HasResults) Then Return 0
                Return NrOfCorrectlyScoredFacts + NrOfNonScoredFacts
            End Get
        End Property

    End Class
End Namespace
