Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection
Imports Cito.Tester.Common
Imports Cito.Tester.Common.Logging

Namespace ResponseAndScoringModel.Solution.ConcreteScoring

    Friend Class ScoringV23 : Inherits baseScoring

        Public Sub New(finding As KeyFinding)
            MyBase.New(finding)
        End Sub


        Public Overrides ReadOnly Property Information As String
            Get
                Return "Content model Version 2.3 scoring"
            End Get
        End Property

        Public Overrides Function GetMaxScoreForFinding() As Integer
            Return GetMaxFindingScore()
        End Function

        Public Overrides Function ScoreFinding(responseFinding As ResponseFinding) As Integer
            Return DoScoreFinding(responseFinding)
        End Function



        Public Function DoScoreFinding(responseFinding As ResponseFinding) As Integer

            Log.Indent()
            Log.TraceInformation(TraceCategory.ScoreModel, $"Scoring Finding id={responseFinding.Id}")

            WriteResponseTraceInfo(responseFinding)
            WriteKeyTraceInfo(Finding)

            Trace.WriteLine($"Key Finding (scoring method = {Finding.Method.ToString()})")

            Dim cummulativeScore As Integer = 0
            Dim rawScore As Integer = 0
            Dim numberOfNonScoredFacts As Integer = 0
            Dim facts As List(Of BaseFact) = responseFinding.Facts

            If Finding.KeyFactsets.Count = 0 Then
                ScoreFacts(facts, Finding.Facts, cummulativeScore, numberOfNonScoredFacts)
            Else
                Dim maxCummulativeScore As Integer = Integer.MinValue
                For Each keyFactSet As KeyFactSet In Finding.KeyFactsets
                    Dim tempCummulativeScore As Integer = 0
                    Dim tempNumberOfNonScoredFacts As Integer = 0

                    ScoreFacts(facts, keyFactSet.Facts, tempCummulativeScore, tempNumberOfNonScoredFacts)
                    If tempCummulativeScore > maxCummulativeScore Then
                        maxCummulativeScore = tempCummulativeScore
                        cummulativeScore = tempCummulativeScore
                        numberOfNonScoredFacts = tempNumberOfNonScoredFacts
                    End If
                Next
            End If

            Select Case Finding.Method
                Case EnumScoringMethod.Polytomous
                    rawScore = ScorePolytomous(cummulativeScore, numberOfNonScoredFacts)

                Case EnumScoringMethod.Dichotomous
                    rawScore = ScoreDichotomous(cummulativeScore, numberOfNonScoredFacts)

                Case EnumScoringMethod.None
                    rawScore = 0

                Case Else
                    rawScore = 0
                    Throw New NotSupportedException("")
            End Select

            Log.TraceInformation(TraceCategory.ScoreModel, $"finding score = {rawScore.ToString()}")
            Log.TraceInformation(TraceCategory.ScoreModel, "End Of Scoring Finding")
            Log.Unindent()
            Return rawScore
        End Function

        <SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")>
        Public Function ScoreFact(keyfact As KeyFact, responsefact As ResponseFact) As Integer
            Dim CummulativeScore As Integer = 0
            Dim FactMatched As Boolean

            Log.Indent()
            Log.TraceInformation(TraceCategory.ScoreModel, $"Scoring Fact id={responsefact.Id}")

            Log.Indent()
            For Each value As ResponseValue In responsefact.Values



                Trace.WriteLine($"   Fact with domain [{value.Domain}] and Value [{value.Value.ToString()}]")
                Dim factValueDomain As String = value.Domain

                For Each keyvalue As KeyValue In keyfact.Values

                    If keyvalue.Domain.Equals(factValueDomain) Then
                        If keyvalue.Occur >= 1 Then
                            FactMatched = False

                            Dim tempValue As BaseValue = value.Value

                            keyvalue.PreProcessingRules.ForEach(Sub(p)
                                                                    Dim rule = ResponseKeyValuePreProcessorFactory.Create(p.Rule)
                                                                    If rule IsNot Nothing Then
                                                                        tempValue = rule.PreprocessValue(tempValue)
                                                                    End If
                                                                End Sub)

                            For Each subvalue As BaseValue In keyvalue.Values
                                If subvalue.IsMatch(tempValue) AndAlso Not FactMatched Then
                                    CummulativeScore += keyfact.Score
                                    FactMatched = True
                                End If
                            Next
                            If FactMatched Then
                                keyvalue.Occur -= 1
                            End If

                            Log.TraceInformation(TraceCategory.ScoreModel, $"Response Value {value.Id}, KeyValue {keyvalue.Id}, matched {FactMatched}")
                        End If

                    End If
                Next
            Next
            Log.Unindent()


            Dim factScore As Integer
            If CummulativeScore = keyfact.Values.Count Then
                factScore = keyfact.Score
            Else
                factScore = 0
            End If

            Log.TraceInformation(TraceCategory.ScoreModel, $"fact score = {factScore.ToString()}")
            Log.TraceInformation(TraceCategory.ScoreModel, "End Of Scoring Fact")
            Log.Unindent()

            Return factScore
        End Function

        Private Sub ScoreFacts(facts As List(Of BaseFact), keyFacts As List(Of BaseFact), ByRef cummulativeScore As Integer, ByRef numberOfNonScoredFacts As Integer)
            For Each responsefact As ResponseFact In facts
                Dim responseFactcummulativeScore As Integer = 0

                For Each keyfact As KeyFact In keyFacts
                    responseFactcummulativeScore += ScoreFact(keyfact, responsefact)
                Next

                If responseFactcummulativeScore = 0 Then
                    numberOfNonScoredFacts += 1
                End If
                cummulativeScore += responseFactcummulativeScore
            Next
        End Sub

        Private Function ScorePolytomous(Score As Integer, numberOfNonScoredFacts As Integer) As Integer
            Log.Indent()
            Log.TraceInformation(TraceCategory.ScoreModel,
                                 $"Scoring Polytomous score={Score}, maxscore={GetMaxFindingScore()}")
            Log.Unindent()

            Return Score
        End Function

        Private Function ScoreDichotomous(score As Integer, numberOfNonScoredFacts As Integer) As Integer
            Dim dichotomousScore As Integer
            Log.Indent()

            Dim cummulativeScore As Integer = GetCumulativeScore()

            If numberOfNonScoredFacts = 0 AndAlso score = cummulativeScore AndAlso cummulativeScore > 0 Then
                dichotomousScore = 1
            Else
                dichotomousScore = 0
            End If

            Log.TraceInformation(TraceCategory.ScoreModel,
                                 $"Scoring Dichotomous score={score}, maxscore={GetCumulativeScore()}")
            Log.Unindent()
            Return dichotomousScore
        End Function

        Protected Overrides Function GetCumulativeScore() As Integer
            Dim cummulativeScore As Integer = 0

            If Finding.KeyFactsets.Count = 0 Then
                For Each keyfact As KeyFact In Finding.Facts
                    cummulativeScore += keyfact.Score
                Next
                Return cummulativeScore
            Else
                For Each keyFactset As KeyFactSet In Finding.KeyFactsets
                    Dim tempCummulativeScore As Integer = 0
                    For Each keyfact As KeyFact In keyFactset.Facts
                        tempCummulativeScore += keyfact.Score
                    Next

                    If tempCummulativeScore > cummulativeScore Then
                        cummulativeScore = tempCummulativeScore
                    End If
                Next
                Return cummulativeScore
            End If
        End Function

        Private Shared Sub WriteResponseTraceInfo(finding As ResponseFinding)
            Trace.WriteLine("#Response")
            Dim i As Integer = 0
            For Each fact As ResponseFact In finding.Facts
                i += 1
                Trace.WriteLine($" finding - fact {i}")
                For Each value As ResponseValue In fact.Values
                    Trace.WriteLine($"  Domain [{value.Domain}] value [{value.ToString}]")
                Next
            Next
            Trace.WriteLine("#End Of Response")
            Trace.WriteLine("")
        End Sub

        Private Shared Sub WriteKeyTraceInfo(key As KeyFinding)
            Trace.WriteLine("#Key")
            Dim i As Integer = 0
            For Each fact As KeyFact In key.Facts
                i += 1
                Trace.WriteLine($" Key - fact {i}")
                Dim j As Integer = 0
                For Each value As KeyValue In fact.Values
                    j += 1
                    For Each subvalue As BaseValue In value.Values
                        Trace.WriteLine($"   Domain [{value.Domain}] value [{subvalue.ToString}]")
                    Next
                Next
            Next
            Trace.WriteLine("#End Of Key")
            Trace.WriteLine("")
        End Sub


    End Class

End Namespace
