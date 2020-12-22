Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring.Validator

    Friend Class EachInteractionHasScoringDefinition
        Inherits ValidationRuleProcessor

        Protected Overrides Sub Validate(item As AssessmentItem)
            Dim solution As Solution = item.Solution

            If Not solution.Findings.Any() Then Return

            Dim findingsToEvaluate As List(Of KeyFinding) = GetFindingsWithFacts(solution)

            Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchScoringParameters()

            If scoringParameters.Count = 1 Then
                For Each scoringParameter As ScoringParameter In scoringParameters
                    If TypeOf scoringParameter Is InlineChoiceScoringParameter Then
                        If findingsToEvaluate.Any() AndAlso Not ScoreDefinitionForInlineChoiceScoringParameterFound(findingsToEvaluate, scoringParameter) Then
                            Throw New Exception()
                        End If
                    ElseIf TypeOf scoringParameter Is ChoiceScoringParameter Then
                        If findingsToEvaluate.Any() AndAlso Not ScoreDefinitionForChoiceScoringParameterFound(findingsToEvaluate, scoringParameter) Then
                            Throw New Exception()
                        End If
                    ElseIf TypeOf scoringParameter Is IntegerScoringParameter Then
                        If findingsToEvaluate.Any() AndAlso Not ScoreDefinitionForIntegerScoringParameterFound(findingsToEvaluate, scoringParameter) Then
                            Throw New Exception()
                        End If
                    ElseIf TypeOf scoringParameter Is GapMatchScoringParameter Then
                        If findingsToEvaluate.Any() AndAlso Not ScoreDefinitionForIntegerScoringParameterFound(findingsToEvaluate, scoringParameter) Then
                            Throw New Exception()
                        End If
                    Else
                        If Not ScoreDefinitionForScoringParameterFound(solution, scoringParameter) Then
                            Throw New Exception()
                        End If
                    End If
                Next
            End If

        End Sub

        Private Function ScoreDefinitionForScoringParameterFound(solution As Solution, scoringParameter As ScoringParameter) As Boolean

            For Each finding As KeyFinding In solution.Findings
                If Not String.IsNullOrWhiteSpace(scoringParameter.FindingOverride) Then
                    If scoringParameter.FindingOverride = finding.Id Then
                        Return True
                    End If
                ElseIf scoringParameter.ControllerId = finding.Id Then
                    Return True
                End If
            Next

            Return False
        End Function

        Private Function GetFindingsWithFacts(solution As Solution) As List(Of KeyFinding)

            Dim findingsWithFacts As New List(Of KeyFinding)

            For Each finding As KeyFinding In solution.Findings
                If Not IsEmptyFinding(finding) Then
                    findingsWithFacts.Add(finding)
                End If
            Next

            Return findingsWithFacts
        End Function

        Private Function ScoreDefinitionForChoiceScoringParameterFound(findings As IEnumerable(Of KeyFinding), scoringParameter As ScoringParameter) As Boolean

            For Each finding As KeyFinding In findings
                If ShouldCheckDomainMatchesControllerId(scoringParameter) Then
                    If DomainMatchesControllerId(finding.Facts, scoringParameter) Then
                        Return True
                    End If

                    For Each factSet In finding.KeyFactsets
                        If DomainMatchesControllerId(factSet.Facts, scoringParameter) Then
                            Return True
                        End If
                    Next
                Else
                    If CheckChoiceFacts(finding.Facts, scoringParameter) Then
                        Return True
                    End If

                    For Each factSet In finding.KeyFactsets
                        If CheckChoiceFacts(factSet.Facts, scoringParameter) Then
                            Return True
                        End If
                    Next
                End If
            Next

            Return False
        End Function

        Private Function IsInlineInteraction(scoringParameter As ScoringParameter) As Boolean
            Return scoringParameter.InlineId IsNot Nothing AndAlso Not String.IsNullOrEmpty(scoringParameter.InlineId)
        End Function

        Private Function CheckChoiceFacts(facts As IEnumerable(Of BaseFact), scoringParameter As ScoringParameter) As Boolean
            If DomainMatchesCombinationOfKeyAndControllerId(facts, scoringParameter) Then
                Return True
            ElseIf IsInlineInteraction(scoringParameter) Then
                If Not scoringParameter.IsSingleChoice AndAlso DomainMatchesCombinationOfKeyAndInlineId(facts, scoringParameter) Then
                    Return True
                ElseIf DomainMatchesInlineId(facts, scoringParameter) Then
                    Return True
                End If
            End If
            Return False
        End Function


        Private Function ShouldCheckDomainMatchesControllerId(scoringParameter As ScoringParameter) As Boolean

            If IsInlineInteraction(scoringParameter) Then Return False
            If scoringParameter.IsSingleChoice Then Return True
            If TypeOf scoringParameter Is OrderScoringParameter Then Return True

            Return False
        End Function

        Private Function IsEmptyFinding(finding As KeyFinding) As Boolean

            If Not finding.Facts.Any() AndAlso Not finding.KeyFactsets.Any() Then Return True

            Return False
        End Function

        Private Function DomainMatchesControllerId(facts As IEnumerable(Of BaseFact), scoringParameter As ScoringParameter) As Boolean

            For Each fact As KeyFact In facts
                For Each value As KeyValue In fact.Values
                    If value.Domain = scoringParameter.ControllerId Then
                        Return True
                    End If
                Next
            Next

            Return False
        End Function

        Private Function DomainMatchesCombinationOfKeyAndControllerId(facts As IEnumerable(Of BaseFact), scoringParameter As ScoringParameter) As Boolean

            For Each fact As KeyFact In facts
                For Each value As KeyValue In fact.Values
                    If value.Domain.IndexOf($"-{scoringParameter.ControllerId}") >= 1 Then
                        Return True
                    End If
                Next
            Next

            Return False
        End Function

        Private Function DomainMatchesCombinationOfKeyAndInlineId(facts As IEnumerable(Of BaseFact), scoringParameter As ScoringParameter) As Boolean

            For Each fact As KeyFact In facts
                For Each value As KeyValue In fact.Values
                    If value.Domain.IndexOf($"-{scoringParameter.InlineId}") >= 1 Then
                        Return True
                    End If
                Next
            Next

            Return False
        End Function

        Private Function ScoreDefinitionForInlineChoiceScoringParameterFound(findings As IEnumerable(Of KeyFinding), scoringParameter As ScoringParameter) As Boolean

            For Each finding As KeyFinding In findings

                If DomainMatchesInlineId(finding.Facts, scoringParameter) Then
                    Return True
                End If

                For Each factSet As KeyFactSet In finding.KeyFactsets
                    If DomainMatchesInlineId(factSet.Facts, scoringParameter) Then
                        Return True
                    End If
                Next

            Next

            Return False
        End Function

        Private Function DomainMatchesInlineId(facts As IEnumerable(Of BaseFact), scoringParameter As ScoringParameter) As Boolean
            For Each fact In facts
                For Each value As KeyValue In fact.Values
                    If value.Domain = scoringParameter.InlineId Then
                        Return True
                    End If
                Next
            Next
            Return False
        End Function


        Private Function ScoreDefinitionForIntegerScoringParameterFound(findings As IEnumerable(Of KeyFinding), scoringParameter As ScoringParameter) As Boolean

            For Each finding As KeyFinding In findings
                If scoringParameter.FindingId = finding.Id OrElse scoringParameter.FindingOverride = finding.Id Then
                    Return True
                End If
            Next

            Return False
        End Function

    End Class
End Namespace
