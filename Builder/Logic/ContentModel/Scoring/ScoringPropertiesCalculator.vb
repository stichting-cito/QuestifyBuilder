Imports System.Linq
Imports System.Text.RegularExpressions
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Public Class ScoringPropertiesCalculator

        Private Const ScoreSeparator As String = "|"

        Private Shared ReadOnly ScoringFormatRegularExpressions As Dictionary(Of Regex, Func(Of String, String)) = New Dictionary(Of Regex, Func(Of String, String))

        Shared Sub New()
            ScoringFormatRegularExpressions.Add(New Regex("^([A-Z][#][A-Z][&]?)+$", RegexOptions.Compiled), AddressOf FormatMatrixKeyValues)
        End Sub

        Public Shared Function TryFormatKeyValuesString(ByVal unformattedKeyValues As String, ByRef formattedKeyValues As String) As Boolean
            For Each kvp As KeyValuePair(Of Regex, Func(Of String, String)) In ScoringFormatRegularExpressions
                If kvp.Key.IsMatch(unformattedKeyValues) Then
                    formattedKeyValues = kvp.Value.Invoke(unformattedKeyValues)
                    Return True
                End If
            Next

            Return False
        End Function



        Private Shared Function FormatMatrixKeyValues(unformattedKeyValues As String) As String
            Dim regEx As New Regex("(?<keyValue>[A-Z])#[A-Z][&]?", RegexOptions.Compiled)

            Return regEx.Replace(unformattedKeyValues, New MatchEvaluator(Function(m As Match)
                                                                              Return m.Groups("keyValue").Value
                                                                          End Function))
        End Function


        Public Shared Function GetResponseCount(solution As Solution) As Nullable(Of Integer)
            Dim count As Nullable(Of Integer) = solution.Findings.Sum(Function(f) f.Facts.Count)
            Return If(count = 0, Nothing, count)
        End Function

        Public Shared Function GetKeyValuesAsString(solution As Solution, bankId As Integer) As String

            Dim findings = solution.Findings.Select(
    Function(f)
        Dim findingKeyValue = f.ToString()
        If IsHottextKeyFinding(findingKeyValue.Split("&"c)(0)) Then Return "HotText"

        Dim mathMlStartPattern As String = "<math"
        Dim mathMlEndPattern As String = "/math>"

        While (findingKeyValue.Contains(mathMlStartPattern))
            Dim mathMlStart = findingKeyValue.IndexOf(mathMlStartPattern)
            Dim mathMlEnd = findingKeyValue.IndexOf(mathMlEndPattern, mathMlStart)
            Dim contentml = findingKeyValue.Substring(mathMlStart, mathMlEnd - mathMlStart + mathMlEndPattern.Length)
        End While
        Return $"{findingKeyValue}{GetResponsePreprocessorInfoAsString(f)}"
    End Function)

            Dim keyValuesString = String.Join(ScoreSeparator, findings.Where(Function(r) Not String.IsNullOrEmpty(r)).ToArray())

            If String.IsNullOrEmpty(keyValuesString) AndAlso solution.AspectReferenceSetCollection IsNot Nothing AndAlso solution.AspectReferenceSetCollection.Count = 1 Then
                keyValuesString = String.Join(",", solution.AspectReferenceSetCollection.Item(0).Items.OrderBy(Function(x) x.SourceName).Select(Function(x) x.SourceName).ToArray())
            End If

            If Not String.IsNullOrEmpty(keyValuesString) Then
                Dim formattedResult As String = Nothing
                If TryFormatKeyValuesString(keyValuesString, formattedResult) Then
                    Return formattedResult
                End If
            End If

            Return keyValuesString
        End Function

        Public Shared Function GetRawScore(solution As Solution) As Nullable(Of Integer)
            Dim findingCount As Integer = solution.Findings.Sum(Function(f) f.MaxFindingScore)
            Dim aspectCount As Integer = solution.AspectReferenceSetCollection.Sum(Function(a) a.GetMaxScore())
            Dim count As Integer = findingCount + aspectCount
            Return If(count = 0, Nothing, count)
        End Function

        Public Shared Function GetMaxScore(solution As Solution) As Nullable(Of Decimal)
            Dim maxScore As Nullable(Of Integer) = GetRawScore(solution)
            Dim maxXlatedScore As Nullable(Of Decimal)

            If solution.ItemScoreTranslationTableSpecified AndAlso solution.ItemScoreTranslationTable.Count > 0 Then
                Dim highestValue As Double = -1

                For i As Integer = 0 To solution.ItemScoreTranslationTable.Count - 1
                    If solution.ItemScoreTranslationTable(i).TranslatedScore > highestValue Then
                        highestValue = solution.ItemScoreTranslationTable(i).TranslatedScore
                    End If
                Next

                maxXlatedScore = CDec(highestValue)
            Else
                maxXlatedScore = maxScore
            End If

            Return If(maxXlatedScore > 0, maxXlatedScore, Nothing)
        End Function

        Public Shared Function GetAlternativesCount(params As IEnumerable(Of ScoringParameter)) As Nullable(Of Integer)
            Dim count As Nullable(Of Integer) = params.Sum(Function(sp) sp.AlternativesCount)
            Return If(count = 0, Nothing, count)
        End Function

        Private Shared Function IsHottextKeyFinding(firstFindingKeyValue As String) As Boolean
            Dim newGuid As Guid
            If Guid.TryParse(firstFindingKeyValue, newGuid) Then
                Return True
            ElseIf firstFindingKeyValue.Length > 1 AndAlso Guid.TryParse(firstFindingKeyValue.Substring(1), newGuid) Then
                Return True
            End If
            Return False
        End Function

        Private Shared Function GetResponsePreprocessorInfoAsString(ByVal finding As KeyFinding) As String
            Dim returnValueSb As New System.Text.StringBuilder()
            If finding.Facts IsNot Nothing Then
                Dim result As String = GetResponsePreprocessorInfoFromFacts(finding.Facts)
                If Not String.IsNullOrEmpty(result) Then
                    If returnValueSb.Length > 0 Then
                        returnValueSb.Append("|")
                    End If
                    returnValueSb.Append(result)
                End If
            End If
            If finding.KeyFactsets IsNot Nothing Then
                For Each factset As KeyFactSet In finding.KeyFactsets
                    Dim result As String = GetResponsePreprocessorInfoFromFacts(factset.Facts)
                    If Not String.IsNullOrEmpty(result) Then
                        If returnValueSb.Length > 0 Then
                            returnValueSb.Append("|")
                        End If
                        returnValueSb.Append($"({result})")
                    End If
                Next

            End If
            If returnValueSb.Length > 0 Then Return $" {returnValueSb.ToString}"
            Return returnValueSb.ToString
        End Function

        Private Shared Function GetResponsePreprocessorInfoFromFacts(ByVal facts As List(Of BaseFact)) As String
            Dim returnValueSb As New System.Text.StringBuilder()
            Dim aPreprocessingRuleFound As Boolean = False

            For Each fact As KeyFact In facts
                Dim ppRuleCounter As Integer = 0
                For Each keyValue As KeyValue In fact.Values
                    If keyValue.PreProcessingRules IsNot Nothing AndAlso keyValue.PreProcessingRules.Count > 0 Then
                        If returnValueSb.Length > 0 Then returnValueSb.Append("&")
                        keyValue.PreProcessingRules.ForEach(Sub(p)
                                                                If ppRuleCounter > 0 Then returnValueSb.Append("|")

                                                                Dim preprocessor As IResponseKeyValuePreprocessor = PreProcessingHelper.GetPreprocessorRule(p.Rule)
                                                                If preprocessor IsNot Nothing Then
                                                                    returnValueSb.Append(preprocessor.Id)
                                                                End If

                                                                aPreprocessingRuleFound = True
                                                                ppRuleCounter += 1
                                                            End Sub)
                    End If
                Next
            Next

            If aPreprocessingRuleFound Then
                Return $"{{{returnValueSb.ToString()}}}"
            End If

            Return String.Empty
        End Function

        Public Shared Function GetResponsePreprocessorRules(ByVal fact As BaseFact) As List(Of IResponseKeyValuePreprocessor)
            Dim rules = New List(Of IResponseKeyValuePreprocessor)

            For Each keyValue As KeyValue In fact.Values
                If keyValue.PreProcessingRules IsNot Nothing AndAlso keyValue.PreProcessingRules.Count > 0 Then
                    keyValue.PreProcessingRules.ForEach(Sub(p)
                                                            Dim preprocessor As IResponseKeyValuePreprocessor = PreProcessingHelper.GetPreprocessorRule(p.Rule)
                                                            If preprocessor IsNot Nothing Then
                                                                rules.Add(preprocessor)
                                                            End If
                                                        End Sub)
                End If
            Next
            Return rules
        End Function

    End Class
End Namespace