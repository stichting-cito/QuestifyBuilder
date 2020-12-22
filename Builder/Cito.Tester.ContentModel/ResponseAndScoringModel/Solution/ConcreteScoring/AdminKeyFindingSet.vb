Imports System.Linq
Namespace ResponseAndScoringModel.Solution.ConcreteScoring

    <DebuggerDisplay("CumulativeScore Score: [{CumulativeScore}] for Domains [{Domains}]")>
    Friend Class ScoreAdminKeyFindingSet


        Shared ReadOnly _empty As New ScoreAdminKeyFindingSet()

        Shared ReadOnly Property Empty As ScoreAdminKeyFindingSet
            Get
                Return _empty
            End Get
        End Property


        Private _score As New Dictionary(Of String, List(Of ScoreAdminKeyFinding))
        Private _highestScore As New Dictionary(Of String, ScoreAdminKeyFinding)

        Sub AddAndFindBestScoreKeyFact(scoreAdminFinding As ScoreAdminKeyFinding)
            If (Not scoreAdminFinding.HasResults) Then Return

            Dim key = scoreAdminFinding.Domains

            If (_score.ContainsKey(key)) Then

                _score(key).Add(scoreAdminFinding)

                If (_highestScore(key).CumulativeScore < scoreAdminFinding.CumulativeScore) Then
                    _highestScore(key) = scoreAdminFinding
                End If

            Else

                _score.Add(key, New List(Of ScoreAdminKeyFinding)(New ScoreAdminKeyFinding() {scoreAdminFinding}))
                _highestScore.Add(key, scoreAdminFinding)

            End If

        End Sub

        Public ReadOnly Property CumulativeScore As Integer
            Get
                Return _highestScore.Values.Sum(Function(x) x.CumulativeScore)
            End Get
        End Property

        Public ReadOnly Property NrOfNonScoredFacts As Integer
            Get
                Return _highestScore.Values.Where(Function(x) Not x.AllFactsScored).Sum(Function(x) x.NrOfNonScoredFacts)
            End Get
        End Property

        Public ReadOnly Property NrOfCorrectlyScoredFacts As Integer
            Get
                Return _highestScore.Values.Where(Function(x) Not x.AllFactsScored).Sum(Function(x) x.NrOfCorrectlyScoredFacts)
            End Get
        End Property

        Public ReadOnly Property Domains As String
            Get
                Return _highestScore.Values.Select(Function(s) s.Domains).Aggregate(Function(c, n) c & ", " & n)
            End Get
        End Property

        Public ReadOnly Property TotalNrOfFacts As Integer
            Get
                Return NrOfCorrectlyScoredFacts + NrOfNonScoredFacts
            End Get
        End Property

    End Class
End Namespace