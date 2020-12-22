Imports System.Linq

Namespace ResponseAndScoringModel.Solution.ConcreteScoring

    Friend Class ScoringFactSetsAndFacts : Inherits baseScoring

        Public Sub New(finding As KeyFinding)
            MyBase.New(finding)
        End Sub


        Public Overrides ReadOnly Property Information As String
            Get
                Return "Scoring that can score distinctive fact sets, and facts outside fact set at the same tiFinding."
            End Get
        End Property

        Public Overrides Function GetMaxScoreForFinding() As Integer
            Return GetMaxFindingScore()
        End Function

        Public Overrides Function ScoreFinding(responseFinding As ResponseFinding) As Integer
            Return DoScoreFinding(responseFinding)
        End Function



        Public Function DoScoreFinding(responseFinding As ResponseFinding) As Integer
            Dim rawScore As Integer = 0
            Dim responseFacts As List(Of BaseFact) = responseFinding.Facts


            Dim score = New ScoreAdminKeyFindingSet()

            For Each keyFactSet As KeyFactSet In Finding.KeyFactsets
                Dim scoreResultFactsSet = ScoreFacts(responseFacts, keyFactSet.Facts, True)
                score.AddAndFindBestScoreKeyFact(scoreResultFactsSet)
            Next


            Dim scoreResultFact = ScoreFacts(responseFacts, Finding.Facts, False)
            score.AddAndFindBestScoreKeyFact(scoreResultFact)

            Select Case Finding.Method
                Case EnumScoringMethod.Polytomous
                    rawScore = score.CumulativeScore

                Case EnumScoringMethod.Dichotomous
                    If score.NrOfNonScoredFacts = 0 Then
                        rawScore = If(score.CumulativeScore > 0, 1, 0)
                    ElseIf score.NrOfNonScoredFacts > 0 Then
                        Dim factDomains = (From x In responseFinding.Facts Select x.Values(0).Domain).Distinct
                        If Finding.KeyFactsets.Count = 0 AndAlso factDomains.Count = 1 AndAlso responseFinding.Facts.Count > Finding.Facts.Count Then
                            rawScore = 0
                        Else
                            rawScore = If(score.TotalNrOfFacts / responseFinding.Facts.Count = score.NrOfCorrectlyScoredFacts, 1, 0)
                        End If
                    Else
                        rawScore = 0
                    End If

                Case EnumScoringMethod.None
                    rawScore = 0

                Case Else
                    Throw New NotSupportedException(String.Empty)
            End Select

            Return rawScore
        End Function

        Private Function ScoreFacts(responseFacts As List(Of BaseFact), keyFacts As List(Of BaseFact), allFactsShouldMatch As Boolean) As ScoreAdminKeyFinding

            If (keyFacts.Count = 0) Then Return ScoreAdminKeyFinding.Empty
            Dim ret = New ScoreAdminKeyFinding(allFactsShouldMatch, keyFacts.Count)

            For Each responsefact As ResponseFact In responseFacts

                For Each keyfact As KeyFact In keyFacts
                    If ((String.IsNullOrEmpty(responsefact.Id)) OrElse
    (Not String.IsNullOrEmpty(keyfact.Id) AndAlso keyfact.Id.Equals(responsefact.Id, StringComparison.InvariantCultureIgnoreCase)) OrElse
    (keyFacts.Count >= 1)) Then

                        Dim result = ScoreFact(keyfact, responsefact)
                        ret.AddScoreKeyFact(result)
                    End If
                Next
            Next

            Return ret
        End Function

        Friend Function ScoreFact(keyfact As KeyFact, responseFact As ResponseFact) As ScoreAdminKeyFact
            Dim ret As New ScoreAdminKeyFact(keyfact.Score, keyfact.Values.Count)

            For Each keyValue As KeyValue In keyfact.Values
                For Each responseValue As ResponseValue In responseFact.Values
                    Dim testDomain As String = responseValue.Domain

                    If (keyValue.Domain.Equals(testDomain)) AndAlso (keyValue.Occur >= 1) Then
                        Dim testValue As BaseValue = getPreProcessedValue(responseValue.Value, keyValue.PreProcessingRules)
                        Dim valueMatched = False
                        valueMatched = GetValueMatched(keyValue, testValue)

                        If valueMatched Then keyValue.Occur -= 1

                        ret.AddScore(testDomain, valueMatched)
                    End If
                Next

            Next

            Return ret
        End Function

        Private Function GetValueMatched(keyValue As KeyValue, testValue As BaseValue) As Boolean

            For Each subValue As BaseValue In keyValue.Values
                If subValue.IsMatch(testValue) Then
                    Return True
                End If
            Next
            Return False
        End Function



        Protected Overrides Function GetCumulativeScore() As Integer
            Dim cumulativeScore As Integer = 0

            Dim maxScore = New Dictionary(Of String, Integer)()

            For Each keyFactSet As KeyFactSet In Finding.KeyFactsets.Where(function(fs) DirectCast(fs, KeyFactSet).Facts.All(function(f) f.Values.Any() AndAlso f.Values.All(Function(bf) DirectCast(bf, KeyValue).Values.Any() AndAlso DirectCast(bf, KeyValue).Values.All(function(v) v.HasValue()))))
                Dim scoreForSet As Integer = keyFactSet.Facts.Sum(Function(f) DirectCast(f, KeyFact).Score)
                If scoreForSet > 1 Then scoreForSet = 1
                Dim domains = New List(Of String)(keyFactSet.Facts.Select(Function(f) DirectCast(f, KeyFact).ScorableDomains()))
                domains.Sort()
                Dim key = String.Join(",", domains.ToArray())

                If (Not maxScore.ContainsKey(key)) Then maxScore.Add(key, scoreForSet)
            Next

            cumulativeScore += maxScore.Values.Sum()

            For Each keyFact As KeyFact In Finding.Facts
                cumulativeScore += keyFact.Score
            Next

            Return cumulativeScore

        End Function

    End Class

End Namespace