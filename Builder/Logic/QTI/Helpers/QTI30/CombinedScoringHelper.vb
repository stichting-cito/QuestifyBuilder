Imports System.Linq
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Helpers.QTI30

    Public Class QTI30CombinedScoringHelper
        Inherits CombinedScoringHelper

        Public Overloads Shared Function IsMultiLineFormulaGap(ByVal responseIdentifierAttribute As XmlAttribute) As Boolean
            If responseIdentifierAttribute.OwnerElement.LocalName.ToLower <> "qti-extended-text-interaction" Then
                Return False
            End If
            If (Not responseIdentifierAttribute.OwnerElement.Attributes Is Nothing AndAlso Not responseIdentifierAttribute.OwnerElement.Attributes("isFormulaEditor") Is Nothing) Then
                Return True
            End If
            Return False
        End Function

        Public Overloads Shared Function DetermineControlType(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, ByVal finding As KeyFinding) As EnumControlType
            For Each responseIdentifierAttribute As XmlAttribute In responseIdentifierAttributeList
                If Not (TypeOf responseIdentifierAttribute Is XmlAttribute) Then
                    Continue For
                End If

                Dim checkFinding As KeyFinding = GetFindingByResponseIdentifier(solution, responseIdentifierAttribute.Value)
                If (checkFinding Is Nothing) OrElse (finding Is Nothing) Then
                    Continue For
                End If

                If checkFinding.Id = finding.Id Then
                    Return DetermineControlType(responseIdentifierAttribute)
                End If
            Next
            Return EnumControlType.Unknown
        End Function

        Public Overloads Shared Function DetermineControlType(ByVal responseIdentifierAttribute As XmlAttribute, scoringParameters As HashSet(Of ScoringParameter)) As EnumControlType
            If IsCasEqualControl(responseIdentifierAttribute, scoringParameters) Then
                Return EnumControlType.CasEqualFormulaEditor
            ElseIf (IsCasEvaluateControl(responseIdentifierAttribute, scoringParameters)) Then
                Return EnumControlType.CasEvaluateFormulaEditor
            Else
                Return DetermineControlType(responseIdentifierAttribute)
            End If
        End Function

        Protected Overloads Shared Function DetermineControlType(ByVal responseIdentifierAttribute As XmlAttribute) As EnumControlType

            If responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-gap-match-interaction" Then
                Return EnumControlType.Gapmatch
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-graphic-gap-match-interaction" Then
                Return EnumControlType.GraphicGapMatch
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-text-entry-interaction" Then
                Return EnumControlType.Input
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-inline-choice-interaction" Then
                Return EnumControlType.Choice
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-choice-interaction" Then
                Return EnumControlType.MultipleChoice
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-match-interaction" Then
                Return EnumControlType.Matrix
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-extended-text-interaction" Then
                If (Not responseIdentifierAttribute.OwnerElement.Attributes Is Nothing AndAlso Not responseIdentifierAttribute.OwnerElement.Attributes("hottextId") Is Nothing) Then
                    Return EnumControlType.Input
                ElseIf (Not responseIdentifierAttribute.OwnerElement.Attributes Is Nothing AndAlso Not responseIdentifierAttribute.OwnerElement.Attributes("isFormulaEditor") Is Nothing) Then
                    Return EnumControlType.Input
                Else
                    Return EnumControlType.Essay
                End If
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-hottext-interaction" Then
                Return EnumControlType.Hottext
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-hotspot-interaction" Then
                Return EnumControlType.Hotspot
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-order-interaction" Then
                Return EnumControlType.Order
            Else
                Return EnumControlType.Unknown
            End If
        End Function

        Public Overloads Shared Function DetermineDecimalSeparatorForGap(ByVal responseIdentifierAttribute As XmlAttribute) As DecimalSeparator
            If responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-text-entry-interaction" Then
                If (Not responseIdentifierAttribute.OwnerElement.Attributes Is Nothing AndAlso Not responseIdentifierAttribute.OwnerElement.Attributes("pattern-mask") Is Nothing) Then
                    Dim patternMask As String = responseIdentifierAttribute.OwnerElement.Attributes("pattern-mask").Value
                    If patternMask.IndexOf("?(([\,])") <> -1 OrElse patternMask.IndexOf("?((\,)") <> -1 Then Return DecimalSeparator.Comma
                    If patternMask.IndexOf("?(([\.])") <> -1 OrElse patternMask.IndexOf("?((\.)") <> -1 Then Return DecimalSeparator.Dot
                    If patternMask.IndexOf("?(([\,\.])") <> -1 Then Return DecimalSeparator.Both
                    If patternMask.IndexOf("?(([\.\,])") <> -1 Then Return DecimalSeparator.Both
                End If
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "qti-custom-interaction" Then
                Return DecimalSeparator.Dot
            End If
            Return DecimalSeparator.None
        End Function

        Public Overloads Shared Function DetermineResponseIndexPerIdentifier(responseIdentifierAttributeList As XmlNodeList) As Dictionary(Of String, Double)
            Dim result As New Dictionary(Of String, Double)
            Dim identifier As String = String.Empty
            Dim responseIndex As Integer = 1
            Dim index As Integer = 1
            Dim skipNrOfInteractions As Integer = 0
            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                identifier = responseIdentifierAttribute.Value
                If Not CheckIdentifierIsGuid(identifier) Then
                    If skipNrOfInteractions <= 0 Then
                        If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "qti-text-entry-interaction" Then
                            identifier = String.Concat("Input", Microsoft.VisualBasic.Strings.ChrW(64 + index))

                            If IsTimeValueInteraction(CType(responseIdentifierAttribute, XmlAttribute), skipNrOfInteractions) OrElse IsDateValueInteraction(CType(responseIdentifierAttribute, XmlAttribute), skipNrOfInteractions) Then
                            End If
                        ElseIf DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "qti-gap-match-interaction" Then
                            Dim gapMatchIndex As Integer = 1
                            For Each node As XmlNode In DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.SelectNodes("//qti-gap")
                                If node.Attributes.ItemOf("identifier") IsNot Nothing Then
                                    If Not result.ContainsKey(node.Attributes.ItemOf("identifier").Value) Then
                                        result.Add(node.Attributes.ItemOf("identifier").Value, CDbl(responseIndex + (gapMatchIndex / 1000)))
                                        gapMatchIndex += 1
                                    End If
                                End If
                            Next
                        End If
                        If Not result.ContainsKey(identifier) Then
                            result.Add(identifier, responseIndex)
                        End If
                        index += 1
                    Else
                        skipNrOfInteractions -= 1
                    End If
                Else
                    If Not result.ContainsKey(identifier) Then
                        result.Add(identifier, responseIndex)
                    End If
                End If
                responseIndex += 1
            Next
            Return result
        End Function

        Public Overloads Shared Function IsTimeValueInteraction(ByVal responseIdentifierAttribute As XmlAttribute, ByRef nrOfInteractionsToSkip As Integer) As Boolean
            If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "qti-text-entry-interaction" AndAlso QTIScoringHelper.HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute, "timeSubType") Then
                nrOfInteractionsToSkip = CInt((DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("timeSubType").Value.Length / 2)) - 1
                Return True
            End If
            Return False
        End Function

        Public Overloads Shared Function IsDateValueInteraction(ByVal responseIdentifierAttribute As XmlAttribute, ByRef nrOfInteractionsToSkip As Integer) As Boolean
            If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "qti-text-entry-interaction" AndAlso QTIScoringHelper.HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute, "dateSubType") Then
                nrOfInteractionsToSkip = 2
                Return True
            End If
            Return False
        End Function

        Public Shared Function ShouldUseResponseProcessingTemplate(solution As Solution, scoringParams As HashSet(Of ScoringParameter), responseIdentifierAttributeList As XmlNodeList) As Boolean
            If solution.AutoScoring AndAlso
                    solution.Findings.Any() AndAlso solution.Findings.Count = 1 AndAlso
                    ItemHasSingleScoringParameter(scoringParams) AndAlso
                    Not ItemHasCustomInteractions(responseIdentifierAttributeList) AndAlso
                    Not ItemHasDateOrTimeScoringParameter(scoringParams) AndAlso
                    Not SolutionContainsFactSets(solution) AndAlso
                    Not SolutionContainsAlternativeKeyValues(solution) AndAlso
                    Not SolutionContainsExoticScoringValues(solution) AndAlso
                    Not SolutionContainsKeyValuesWithPreprocessingRules(solution) AndAlso
                    Not QTIScoringHelper.ShouldScoreBeTranslated(solution.ItemScoreTranslationTable) Then
                Return True
            End If

            Return False
        End Function

        Private Shared Function ItemHasCustomInteractions(responseIdentifierAttributeList As XmlNodeList) As Boolean
            Return responseIdentifierAttributeList.Cast(Of XmlNode).Any(Function(r) IsCustomInteraction(r))
        End Function

        Private Shared Function IsCustomInteraction(ByVal responseIdentifierAttribute As XmlNode) As Boolean
            Return DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "qti-custom-interaction"
        End Function

        Public Shared Function GetResponseProcessingTemplate(finding As KeyFinding, scoringParams As HashSet(Of ScoringParameter)) As String
            If ItemHasSingleScoringParameter(scoringParams) AndAlso TypeOf scoringParams.First() Is SelectPointScoringParameter Then
                Return "https://purl.imsglobal.org/spec/qti/v3p0/rptemplates/map_response_point.xml"
            End If
            If finding.Method = EnumScoringMethod.Dichotomous Then
                Return "https://purl.imsglobal.org/spec/qti/v3p0/rptemplates/match_correct.xml"
            End If
            If finding.Method = EnumScoringMethod.Polytomous Then
                Return "https://purl.imsglobal.org/spec/qti/v3p0/rptemplates/map_response.xml"
            End If

            Return String.Empty
        End Function

        Public Shared Function ShouldAddResponseDeclarationMappingsForResponseProcessingTemplateUsage(finding As KeyFinding, scoringParams As HashSet(Of ScoringParameter)) As Boolean
            If scoringParams.Any(Function(sp) TypeOf sp Is SelectPointScoringParameter) Then
                Return False
            End If
            If finding.Method = EnumScoringMethod.Polytomous Then
                Return True
            End If

            Return False
        End Function

        Private Shared Function ItemHasSingleScoringParameter(scoringParams As HashSet(Of ScoringParameter)) As Boolean
            Return scoringParams IsNot Nothing AndAlso scoringParams.Any() AndAlso scoringParams.Count = 1
        End Function

        Private Shared Function ItemHasDateOrTimeScoringParameter(scoringParams As HashSet(Of ScoringParameter)) As Boolean
            Return scoringParams.Any(Function(sp) TypeOf sp Is TimeScoringParameter OrElse TypeOf sp Is DateScoringParameter)
        End Function

        Private Shared Function SolutionContainsFactSets(solution As Solution) As Boolean
            Return solution.Findings.Any(Function(f) f.KeyFactsets.Any())
        End Function

        Private Shared Function SolutionContainsAlternativeKeyValues(solution As Solution) As Boolean
            Dim result = solution.Findings.Any(Function(f) f.Facts.Any(Function(fa) fa.Values.OfType(Of KeyValue).Any(Function(kv) kv.Values.Count() > 1)))
            result = result OrElse SolutionHasAlternativeKeyValuesInSeperateKeyFacts(solution)
            Return result
        End Function

        Private Shared Function SolutionHasAlternativeKeyValuesInSeperateKeyFacts(solution As Solution) As Boolean
            Dim domainsInKeyValues = solution.Findings.SelectMany(Function(f) f.Facts.SelectMany(Function(fa) fa.Values.OfType(Of KeyValue).Select(Function(kv) kv.Domain)))
            Dim allDomainsAreUnique = domainsInKeyValues.GroupBy(Function(d) d).All(Function(x) x.Count() = 1)
            Return Not allDomainsAreUnique
        End Function

        Private Shared Function SolutionContainsExoticScoringValues(solution As Solution) As Boolean
            Return solution.Findings.Any(Function(f) f.Facts.Any(Function(fa) fa.Values.OfType(Of KeyValue).Any(Function(kv) kv.Values.Any(Function(v) SolutionValueIsOfExoticType(v)))))
        End Function

        Private Shared Function SolutionContainsKeyValuesWithPreprocessingRules(solution As Solution) As Boolean
            Return solution.Findings.Any(Function(f) f.Facts.Any(Function(fa) fa.Values.OfType(Of KeyValue).Any(Function(kv) kv.PreProcessingRules.Any())))
        End Function

        Private Shared Function SolutionValueIsOfExoticType(value As BaseValue) As Boolean
            If TypeOf value Is BooleanValue OrElse
                    TypeOf value Is StringValue OrElse
                    TypeOf value Is IntegerValue OrElse
                    TypeOf value Is DecimalValue Then
                Return False
            Else
                Return True
            End If
        End Function

    End Class
End Namespace