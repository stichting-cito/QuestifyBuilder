Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring

Namespace QTI.Helpers.QTI_Base

    Public Class CombinedScoringHelper


        Enum EnumControlType
            Unknown
            Input
            Choice
            Gapmatch
            MultipleChoice
            Matrix
            Essay
            Hottext
            Order
            GraphicGapMatch
            Hotspot
            CasEqualFormulaEditor
            CasEvaluateFormulaEditor
        End Enum

        Enum DecimalSeparator
            None
            Dot
            Comma
            Both
        End Enum

        Enum EnumGapType
            Unknown
            StringGap
            IntegerGap
            DecimalGap
            CurrencyGap
            TimeGap
            DateGap
            FormulaGap
            EqualStepsGap
        End Enum

        Enum FormulaItemType
            Unknown
            Equivalence
            EvaluateDependency
            EvaluateSteps
            EvaluateSubstitute
        End Enum

        Enum DomainOrderRetrievalType
            All
            CollectionId
            Domain
            FactId
        End Enum


        Public Shared Function DetermineControlType(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, ByVal finding As KeyFinding) As EnumControlType
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

        Public Shared Function DetermineControlType(ByVal responseIdentifierAttribute As XmlAttribute, scoringParameters As HashSet(Of ScoringParameter)) As EnumControlType

            If IsCasEqualControl(responseIdentifierAttribute, scoringParameters) Then
                Return EnumControlType.CasEqualFormulaEditor
            ElseIf (IsCasEvaluateControl(responseIdentifierAttribute, scoringParameters)) Then
                Return EnumControlType.CasEvaluateFormulaEditor
            Else
                Return DetermineControlType(responseIdentifierAttribute)
            End If
        End Function

        Protected Shared Function IsCasEqualControl(responseIdentifierAttribute As XmlAttribute, scoringParameters As HashSet(Of ScoringParameter)) As Boolean
            If scoringParameters Is Nothing OrElse Not scoringParameters.Any() Then Return False
            Return scoringParameters.OfType(Of MathCasEqualScoringParameter).Any(Function(s) s.InlineId = responseIdentifierAttribute.Value)
        End Function

        Protected Shared Function IsCasEvaluateControl(responseIdentifierAttribute As XmlAttribute, scoringParameters As HashSet(Of ScoringParameter)) As Boolean
            If scoringParameters Is Nothing OrElse Not scoringParameters.Any() Then Return False
            Return scoringParameters.OfType(Of MathCasEvaluateScoringParameter).Any(Function(s) s.InlineId = responseIdentifierAttribute.Value)
        End Function

        Public Shared Function IsMultiLineFormulaGap(ByVal responseIdentifierAttribute As XmlAttribute) As Boolean
            If responseIdentifierAttribute.OwnerElement.LocalName.ToLower <> "extendedtextinteraction" Then
                Return False
            End If
            If (Not responseIdentifierAttribute.OwnerElement.Attributes Is Nothing AndAlso Not responseIdentifierAttribute.OwnerElement.Attributes("isFormulaEditor") Is Nothing) Then
                Return True
            End If
            Return False
        End Function

        Protected Shared Function DetermineControlType(ByVal responseIdentifierAttribute As XmlAttribute) As EnumControlType

            If responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "gapmatchinteraction" Then
                Return EnumControlType.Gapmatch
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "graphicgapmatchinteraction" Then
                Return EnumControlType.GraphicGapMatch
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "textentryinteraction" Then
                Return EnumControlType.Input
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "inlinechoiceinteraction" Then
                Return EnumControlType.Choice
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "choiceinteraction" Then
                Return EnumControlType.MultipleChoice
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "matchinteraction" Then
                Return EnumControlType.Matrix
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "extendedtextinteraction" Then
                If (Not responseIdentifierAttribute.OwnerElement.Attributes Is Nothing AndAlso Not responseIdentifierAttribute.OwnerElement.Attributes("hottextId") Is Nothing) Then
                    Return EnumControlType.Input
                ElseIf (Not responseIdentifierAttribute.OwnerElement.Attributes Is Nothing AndAlso Not responseIdentifierAttribute.OwnerElement.Attributes("isFormulaEditor") Is Nothing) Then
                    Return EnumControlType.Input
                Else
                    Return EnumControlType.Essay
                End If
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "hottextinteraction" Then
                Return EnumControlType.Hottext
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "hotspotinteraction" Then
                Return EnumControlType.Hotspot
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "orderinteraction" Then
                Return EnumControlType.Order
            Else
                Return EnumControlType.Unknown
            End If
        End Function

        Public Shared Function DetermineDecimalSeparatorForGap(ByVal responseIdentifierAttribute As XmlAttribute) As DecimalSeparator
            If responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "textentryinteraction" Then
                If (Not responseIdentifierAttribute.OwnerElement.Attributes Is Nothing AndAlso Not responseIdentifierAttribute.OwnerElement.Attributes("patternMask") Is Nothing) Then
                    Dim patternMask As String = responseIdentifierAttribute.OwnerElement.Attributes("patternMask").Value
                    If patternMask.IndexOf("?(([\,])") <> -1 OrElse patternMask.IndexOf("?((\,)") <> -1 Then Return DecimalSeparator.Comma
                    If patternMask.IndexOf("?(([\.])") <> -1 OrElse patternMask.IndexOf("?((\.)") <> -1 Then Return DecimalSeparator.Dot
                    If patternMask.IndexOf("?(([\,\.])") <> -1 Then Return DecimalSeparator.Both
                    If patternMask.IndexOf("?(([\.\,])") <> -1 Then Return DecimalSeparator.Both
                End If
            ElseIf responseIdentifierAttribute.OwnerElement.LocalName.ToLower = "custominteraction" Then
                Return DecimalSeparator.Dot
            End If
            Return DecimalSeparator.None
        End Function

        Public Shared Function ItemContainsGapmatchInteraction(ByVal responseIdentifierAttributeList As XmlNodeList) As Boolean
            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                If Not (TypeOf responseIdentifierAttribute Is XmlAttribute) Then
                    Continue For
                End If
                If DetermineControlType(CType(responseIdentifierAttribute, XmlAttribute)) = EnumControlType.Gapmatch Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Shared Function ItemContainsGapmatchInteraction(itemDocument As XmlDocument) As Boolean
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)
            xmlNamespaceManager.AddNamespace("html", "http://www.w3.org/1999/xhtml")
            xmlNamespaceManager.AddNamespace("pci", "http://www.imsglobal.org/xsd/portableCustomInteraction_v1")
            Dim responseIdentifierAttributeList As XmlNodeList = ResponseIdentifierHelper.GetResponseIdentifiers(itemDocument, xmlNamespaceManager)
            Return ItemContainsGapmatchInteraction(responseIdentifierAttributeList)
        End Function

        Public Shared Function GetFindingsInItem(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList) As List(Of KeyFinding)
            Dim result As List(Of KeyFinding) = New List(Of KeyFinding)
            Dim finding As KeyFinding = Nothing
            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                If Not (TypeOf responseIdentifierAttribute Is XmlAttribute) Then
                    Continue For
                End If
                finding = GetFindingByResponseIdentifier(solution, responseIdentifierAttribute.Value)
                If Not finding Is Nothing AndAlso Not result.Contains(finding) Then
                    result.Add(finding)
                End If
            Next
            Return result
        End Function

        Public Shared Function GetConceptFindingsInItem(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList) As List(Of ConceptFinding)
            Dim result As List(Of ConceptFinding) = New List(Of ConceptFinding)
            Dim finding As ConceptFinding = Nothing
            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                If Not (TypeOf responseIdentifierAttribute Is XmlAttribute) Then
                    Continue For
                End If
                finding = GetConceptFindingByResponseIdentifier(solution, responseIdentifierAttribute.Value)
                If Not finding Is Nothing AndAlso Not result.Contains(finding) Then
                    result.Add(finding)
                End If
            Next
            Return result
        End Function

        Public Shared Function GetConceptFindingByResponseIdentifierList(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList) As ConceptFinding
            Dim finding As ConceptFinding
            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                finding = GetConceptFindingByResponseIdentifier(solution, responseIdentifierAttribute.Value)
                If Not finding Is Nothing Then
                    Return finding
                End If
            Next
            Return Nothing
        End Function

        Public Shared Function GetFindingByResponseIdentifier(ByVal solution As Solution, ByVal responseidentifier As String) As KeyFinding

            Dim findings = (From f In solution.Findings Where f IsNot Nothing Select f).ToList()
            Dim matchOnIdentifier = (From f In findings
                                     Where f.Id.Equals(responseidentifier, StringComparison.InvariantCultureIgnoreCase)
                                     Select f).FirstOrDefault()

            If (matchOnIdentifier IsNot Nothing) Then
                Return matchOnIdentifier
            End If

            For Each finding As KeyFinding In findings
                If finding.Facts IsNot Nothing AndAlso
                   finding.Facts.Any(Function(f) f.Values.Any(Function(v) v.Domain.Equals(responseidentifier, StringComparison.InvariantCultureIgnoreCase))) Then
                    Return finding
                End If
                If finding.KeyFactsets IsNot Nothing AndAlso
                   finding.KeyFactsets.Any(Function(kfs) kfs.Facts.ToList.Any(Function(f) f.Values.Any(Function(v) v.Domain.Equals(responseidentifier, StringComparison.InvariantCultureIgnoreCase)))) Then
                    Return finding
                End If
            Next

            If Not CheckIdentifierIsGuid(responseidentifier) Then Return Nothing

            For Each finding As KeyFinding In findings
                If finding.Facts IsNot Nothing AndAlso finding.Facts.Any(Function(f) f.Values.Any(Function(v) GetGuidPartOfIdentifier(v.Domain) = responseidentifier)) Then
                    Return finding
                End If
                If finding.KeyFactsets IsNot Nothing AndAlso finding.KeyFactsets.Any(Function(kfs) kfs.Facts.ToList.Any(Function(f) f.Values.Any(Function(v) GetGuidPartOfIdentifier(v.Domain) = responseidentifier))) Then
                    Return finding
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function GetConceptFindingByResponseIdentifier(ByVal solution As Solution, ByVal responseidentifier As String) As ConceptFinding
            Dim conceptFindings = (From f As ConceptFinding In solution.ConceptFindings Where f IsNot Nothing Select f).ToList()

            For Each finding In conceptFindings
                If finding.Id.Equals(responseidentifier, StringComparison.InvariantCultureIgnoreCase) Then
                    Return finding
                End If
            Next

            For Each finding In conceptFindings
                For Each conceptFact As ConceptFact In finding.Facts
                    For Each conceptValue As ConceptValue In conceptFact.Values
                        If conceptValue.GetControlId(responseidentifier).Equals(responseidentifier, StringComparison.InvariantCultureIgnoreCase) Then
                            Return finding
                        End If
                    Next
                Next
            Next
            For Each finding In conceptFindings
                For Each conceptFactSet As ConceptFactsSet In finding.KeyFactsets
                    For Each conceptFact As ConceptFact In conceptFactSet.Facts
                        For Each conceptValue As ConceptValue In conceptFact.Values
                            If conceptValue.GetControlId(responseidentifier).Equals(responseidentifier, StringComparison.InvariantCultureIgnoreCase) Then
                                Return finding
                            End If
                        Next
                    Next
                Next
            Next

            Return Nothing
        End Function

        Public Shared Function GetKeyValueByResponseIdentifier(ByVal finding As KeyFinding, ByVal responseidentifier As String) As KeyValue
            Dim keyValue As KeyValue = Nothing
            If finding Is Nothing Then
                Return Nothing
            End If

            If finding.KeyFactsets.Count > 0 Then
                For Each factset As KeyFactSet In finding.KeyFactsets
                    keyValue = GetKeyValueByResponseIdentifier(factset.Facts, responseidentifier)
                    If keyValue IsNot Nothing Then
                        Exit For
                    End If
                Next
            End If
            If keyValue Is Nothing AndAlso finding.Facts.Count > 0 Then
                keyValue = GetKeyValueByResponseIdentifier(finding.Facts, responseidentifier)
            End If
            Return keyValue
        End Function

        Private Shared Function GetKeyValueByResponseIdentifier(facts As List(Of BaseFact), ByVal responseidentifier As String) As KeyValue
            For Each keyFact As KeyFact In facts
                For Each keyValue As KeyValue In keyFact.Values
                    If keyValue.Domain.ToLower = responseidentifier.ToLower Then
                        Return keyValue
                    End If
                Next
            Next
            Return Nothing
        End Function

        Public Shared Function GetConceptFactListByResponseIdentifier(ByVal finding As ConceptFinding, ByVal responseidentifier As String) As List(Of ConceptFact)
            Dim result As List(Of ConceptFact) = New List(Of ConceptFact)
            If finding Is Nothing Then
                Return result
            End If

            For Each fact As ConceptFact In finding.Facts
                For Each value As ConceptValue In fact.Values
                    If responseidentifier.ToLower.Equals("mc") AndAlso finding.Id.ToLower.Equals("mc") Then
                        result.Add(fact)
                        Exit For
                    ElseIf value.GetControlId(responseidentifier).ToLower = responseidentifier.ToLower Then
                        result.Add(fact)
                        Exit For
                    End If
                Next
            Next

            Return result
        End Function

        Public Shared Function GetKeyValueForInputByIndex(ByVal finding As KeyFinding, ByVal index As Integer) As KeyValue
            If (finding Is Nothing) OrElse Not finding.Facts.Any() Then
                Return Nothing
            End If
            Return GetKeyValueForInputByIndex(finding.Facts, index)
        End Function

        Private Shared Function GetKeyValueForInputByIndex(facts As List(Of BaseFact), index As Integer) As KeyValue
            Dim result As KeyValue = Nothing
            For Each keyFact As KeyFact In facts
                result = GetKeyValueForInputByIndex(keyFact, index)
                If result IsNot Nothing Then Return result
            Next
            Return Nothing
        End Function

        Private Shared Function GetKeyValueForInputByIndex(fact As BaseFact, index As Integer) As KeyValue
            For Each keyValue As KeyValue In fact.Values
                If InStr(keyValue.Domain.ToLower, $"input{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(index)}".ToLower) > 0 Then
                    Return keyValue
                ElseIf keyValue.Domain.Equals(index.ToString()) Then
                    Return keyValue
                End If
            Next
            Return Nothing
        End Function

        Public Shared Function GetFactsForInputByIndex(ByVal finding As BaseFinding, ByVal index As Integer) As List(Of BaseFact)
            Dim result As New List(Of BaseFact)
            For Each keyFact As KeyFact In finding.Facts
                If GetKeyValueForInputByIndex(keyFact, index) IsNot Nothing Then result.Add(keyFact)
            Next
            Return result
        End Function

        Public Shared Function GetFactIdsFromScoringParameters(scoringParameters As HashSet(Of ScoringParameter)) As Dictionary(Of String, ScoringParameter)
            Dim ret As New Dictionary(Of String, ScoringParameter)
            For Each scoringParameter As ScoringParameter In scoringParameters
                Dim manipulator = scoringParameter.GetScoreManipulator(New Solution())
                If manipulator IsNot Nothing Then
                    If TypeOf scoringParameter Is GraphGapMatchScoringParameter Then
                        For Each parameterCollection As ParameterCollection In scoringParameter.Value.OrderBy(Function(v) v.Id)
                            Dim factId = manipulator.GetFactIdForKey(parameterCollection.Id)
                            If Not ret.ContainsKey(factId) Then ret.Add(factId, scoringParameter)
                        Next
                    Else
                        For Each parameterCollection As ParameterCollection In scoringParameter.Value
                            Dim factId = manipulator.GetFactIdForKey(parameterCollection.Id)
                            If Not ret.ContainsKey(factId) Then ret.Add(factId, scoringParameter)
                        Next
                    End If
                End If
            Next
            Return ret
        End Function

        Public Shared Function GetDomainOrderFromScoringParameter(scoringParam As ScoringParameter, domainOrderRetrievalType As DomainOrderRetrievalType) As Dictionary(Of String, Integer)
            Dim ret As New Dictionary(Of String, Integer)
            Dim idx As Integer = 1
            Dim manipulator = scoringParam.GetScoreManipulator(New Solution())
            If TypeOf scoringParam Is GraphGapMatchScoringParameter Then
                GetOrderFromGraphGapMatch(scoringParam, domainOrderRetrievalType, ret, idx, manipulator)
            Else
                GetOrderFromOtherParam(scoringParam, domainOrderRetrievalType, ret, idx, manipulator)
            End If

            Return ret
        End Function

        Private Shared Sub GetOrderFromGraphGapMatch(
                                                     scoringParam As ScoringParameter,
                                                     domainOrderRetrievalType As DomainOrderRetrievalType,
                                                     ByRef ret As Dictionary(Of String, Integer),
                                                     ByRef idx As Integer,
                                                     manipulator As IScoreManipulator)

            For Each parameterCollection As ParameterCollection In scoringParam.Value.OrderBy(Function(v) v.Id)
                GetOrder(domainOrderRetrievalType, ret, parameterCollection, idx, manipulator)
                idx += 1
            Next
        End Sub
        Private Shared Sub GetOrderFromOtherParam(
                                                  scoringParam As ScoringParameter,
                                                  domainOrderRetrievalType As DomainOrderRetrievalType,
                                                  ByRef ret As Dictionary(Of String, Integer),
                                                  ByRef idx As Integer,
                                                  manipulator As IScoreManipulator)

            For Each parameterCollection As ParameterCollection In scoringParam.Value
                GetOrder(domainOrderRetrievalType, ret, parameterCollection, idx, manipulator)
                idx += 1
            Next
        End Sub

        Private Shared Sub GetOrder(
                                    domainOrderRetrievalType As DomainOrderRetrievalType,
                                    ret As Dictionary(Of String, Integer),
                                    parameterCollection As ParameterCollection,
                                    idx As Integer,
                                    manipulator As IScoreManipulator)

            If domainOrderRetrievalType = CombinedScoringHelper.DomainOrderRetrievalType.All OrElse domainOrderRetrievalType = CombinedScoringHelper.DomainOrderRetrievalType.CollectionId Then
                If Not ret.ContainsKey(parameterCollection.Id) Then ret.Add(parameterCollection.Id, idx)
            End If
            If domainOrderRetrievalType = CombinedScoringHelper.DomainOrderRetrievalType.All OrElse domainOrderRetrievalType = CombinedScoringHelper.DomainOrderRetrievalType.Domain Then
                Dim domain = manipulator.GetDomainForKey(parameterCollection.Id)
                If domain IsNot Nothing AndAlso Not String.IsNullOrEmpty(domain) AndAlso Not ret.ContainsKey(domain) Then
                    ret.Add(domain, idx)
                End If
            End If
            If domainOrderRetrievalType = CombinedScoringHelper.DomainOrderRetrievalType.All OrElse domainOrderRetrievalType = CombinedScoringHelper.DomainOrderRetrievalType.FactId Then
                Dim factId = manipulator.GetFactIdForKey(parameterCollection.Id)
                If factId IsNot Nothing AndAlso Not String.IsNullOrEmpty(factId) AndAlso Not ret.ContainsKey(factId) Then
                    ret.Add(factId, idx)
                End If
            End If

        End Sub


        Public Shared Function DetermineResponseIndexPerIdentifier(responseIdentifierAttributeList As XmlNodeList) As Dictionary(Of String, Double)
            Dim result As New Dictionary(Of String, Double)
            Dim identifier As String = String.Empty
            Dim responseIndex As Integer = 1
            Dim index As Integer = 1
            Dim skipNrOfInteractions As Integer = 0
            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                identifier = responseIdentifierAttribute.Value
                If Not CheckIdentifierIsGuid(identifier) Then
                    If skipNrOfInteractions <= 0 Then
                        If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "textentryinteraction" Then
                            identifier = String.Concat("Input", Microsoft.VisualBasic.Strings.ChrW(64 + index))

                            If IsTimeValueInteraction(CType(responseIdentifierAttribute, XmlAttribute), skipNrOfInteractions) OrElse IsDateValueInteraction(CType(responseIdentifierAttribute, XmlAttribute), skipNrOfInteractions) Then
                            End If
                        ElseIf DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "gapmatchinteraction" Then
                            Dim gapMatchIndex As Integer = 1
                            For Each node As XmlNode In DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.SelectNodes("//gap")
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

        Public Shared Function GetGapTypeByScoringParameter(scoringPrm As ScoringParameter) As EnumGapType
            If TypeOf scoringPrm Is StringScoringParameter Then
                Return EnumGapType.StringGap
            ElseIf TypeOf scoringPrm Is IntegerScoringParameter Then
                Return EnumGapType.IntegerGap
            ElseIf TypeOf scoringPrm Is DecimalScoringParameter Then
                Return EnumGapType.DecimalGap
            ElseIf TypeOf scoringPrm Is CurrencyScoringParameter Then
                Return EnumGapType.CurrencyGap
            ElseIf TypeOf scoringPrm Is TimeScoringParameter Then
                Return EnumGapType.TimeGap
            ElseIf TypeOf scoringPrm Is DateScoringParameter Then
                Return EnumGapType.DateGap
            ElseIf TypeOf scoringPrm Is MathScoringParameter Then
                Return EnumGapType.FormulaGap
            ElseIf TypeOf scoringPrm Is CasEqualStepsScoringParameter Then
                Return EnumGapType.EqualStepsGap
            Else
                Return EnumGapType.Unknown
            End If
        End Function

        Public Shared Function GetGapTypeByKeyValue(responseIdentifierAttribute As XmlNode, value As KeyValue) As EnumGapType
            If QTIScoringHelper.KeyValueContainsInteger(value.Values) Then
                Return EnumGapType.IntegerGap
            ElseIf QTIScoringHelper.KeyValueContainsDecimal(value.Values) Then
                Return EnumGapType.DecimalGap
            ElseIf QTIScoringHelper.IsTimeValue(responseIdentifierAttribute, value) Then
                Return EnumGapType.TimeGap
            ElseIf QTIScoringHelper.IsDateValue(responseIdentifierAttribute, value) Then
                Return EnumGapType.DateGap
            ElseIf QTIScoringHelper.KeyValueContainsMathML(value.Values) Then
                Return EnumGapType.FormulaGap
            ElseIf QTIScoringHelper.KeyValueContainsString(value.Values) Then
                Return EnumGapType.StringGap
            Else
                Return CombinedScoringHelper.EnumGapType.Unknown
            End If
        End Function

        Public Shared Function GetFormulaItemTypeByFinding(finding As KeyFinding) As FormulaItemType
            Return GetFormulaItemType(finding, Nothing)
        End Function

        Public Shared Function GetFormulaItemType(finding As KeyFinding, scoringParam As ScoringParameter) As FormulaItemType
            If TypeOf scoringParam Is MathCasEqualScoringParameter Then
                Return FormulaItemType.EvaluateSteps
            ElseIf TypeOf scoringParam Is MathCasEvaluateScoringParameter Then
                Return FormulaItemType.EvaluateSubstitute
            ElseIf TypeOf scoringParam Is MathCasDependencyScoringParameter Then
                Return FormulaItemType.EvaluateDependency
            ElseIf (TypeOf scoringParam Is DecimalScoringParameter OrElse TypeOf scoringParam Is IntegerScoringParameter) AndAlso IsDependencyGap(scoringParam, finding, GetFactIdsFromScoringParameters(New HashSet(Of ScoringParameter)({scoringParam}))) Then
                Return FormulaItemType.EvaluateDependency
            ElseIf finding.Facts.Any(Function(f) f.Values.Any(Function(kv) DirectCast(kv, KeyValue).Values.Any(Function(bv) TypeOf bv Is StringComparisonValue AndAlso DirectCast(bv, StringComparisonValue).GetComparisonType(DirectCast(bv, StringComparisonValue).TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.NotEquals))) OrElse
                   finding.KeyFactsets.Any(Function(fs) fs.Facts.Any(Function(f) f.Values.Any(Function(kv) DirectCast(kv, KeyValue).Values.Any(Function(bv) TypeOf bv Is StringComparisonValue AndAlso DirectCast(bv, StringComparisonValue).GetComparisonType(DirectCast(bv, StringComparisonValue).TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.NotEquals)))) Then
                Return FormulaItemType.EvaluateDependency
            End If
            Return FormulaItemType.Unknown
        End Function

        Public Shared Function IsDependencyGap(scorePrm As ScoringParameter, finding As KeyFinding, factIdsPerScoringParameter As Dictionary(Of String, ScoringParameter)) As Boolean
            If Not factIdsPerScoringParameter.Any(Function(kvp) Object.ReferenceEquals(kvp.Value, scorePrm)) Then
                Return False
            End If

            Dim factId = factIdsPerScoringParameter.First(Function(kvp) Object.ReferenceEquals(kvp.Value, scorePrm)).Key

            Dim facts = finding.Facts.Where(Function(f) f.Id = factId)
            facts = facts.Union(finding.KeyFactsets.SelectMany(Function(factSet) factSet.Facts).Where(Function(f) f.Id = factId))

            If (facts Is Nothing OrElse facts.Count = 0) Then
                Debug.Assert(facts IsNot Nothing OrElse facts.Count = 0, "No fact with specified id found, this should not occur!")
                Return False
            End If

            For Each keyValue As KeyValue In facts.SelectMany(Function(f) f.Values)
                For Each value In keyValue.Values.OfType(Of BaseValue)()
                    If TypeOf value Is StringComparisonValue Then
                        Dim typedValue = CType(value, StringComparisonValue)
                        If typedValue.GetComparisonType(typedValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.Dependency Then
                            Return True
                        End If
                    End If
                Next
            Next


            Return False
        End Function

        Public Shared Function IsMultiChoiceScoring(responseIdentifierAttribute As Xml.XmlNode, scoringParameters As HashSet(Of ScoringParameter)) As Boolean
            If scoringParameters IsNot Nothing AndAlso scoringParameters.Count > 0 Then
                Dim scoringPrm = scoringParameters.OfType(Of MultiChoiceScoringParameter).FirstOrDefault(Function(s) s.ControllerId = responseIdentifierAttribute.Value)
                If scoringPrm IsNot Nothing AndAlso scoringPrm.IsSingleChoice AndAlso Not scoringPrm.IsSingleValue Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Shared Function IsTimeValueInteraction(ByVal responseIdentifierAttribute As XmlAttribute, ByRef nrOfInteractionsToSkip As Integer) As Boolean
            If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "textentryinteraction" AndAlso QTIScoringHelper.HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute, "timeSubType") Then
                nrOfInteractionsToSkip = CInt((DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("timeSubType").Value.Length / 2)) - 1
                Return True
            End If
            Return False
        End Function

        Public Shared Function IsDateValueInteraction(ByVal responseIdentifierAttribute As XmlAttribute, ByRef nrOfInteractionsToSkip As Integer) As Boolean
            If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower = "textentryinteraction" AndAlso QTIScoringHelper.HasResponseIdentifiersWithDateTimeSubType(responseIdentifierAttribute, "dateSubType") Then
                nrOfInteractionsToSkip = 2
                Return True
            End If
            Return False
        End Function

        Public Shared Function CheckIdentifierIsGuid(identifier As String) As Boolean
            Dim result As Boolean = False
            Dim newGuid As Guid

            If Guid.TryParse(identifier, newGuid) Then
                result = True
            ElseIf Guid.TryParse(identifier.Substring(1), newGuid) Then
                result = True
            Else
                Dim underScoreIndex As Integer = identifier.IndexOf("_")
                If underScoreIndex > -1 AndAlso identifier.Length > underScoreIndex + 1 Then
                    Dim guidIndex As Integer = underScoreIndex + 1

                    If Char.IsUpper(identifier.Substring(guidIndex, 1).ToCharArray()(0)) Then
                        guidIndex += 1
                    End If

                    If guidIndex < identifier.Length Then
                        result = Guid.TryParse(identifier.Substring(guidIndex), newGuid)
                    End If
                End If
            End If

            Return result
        End Function

        Public Shared Function GetGuidPartOfIdentifier(identifier As String) As String
            If identifier.IndexOf("-") <= 0 Then Return String.Empty

            If CheckIdentifierIsGuid(identifier) Then Return identifier
            Dim pos As Integer = identifier.LastIndexOf("-")
            If CheckIdentifierIsGuid(identifier.Substring(0, pos)) Then
                Return identifier.Substring(0, pos)
            End If
            pos = identifier.IndexOf("-")
            If CheckIdentifierIsGuid(identifier.Substring(pos + 1, Len(identifier) - (pos + 1))) Then
                Return identifier.Substring(pos + 1, Len(identifier) - (pos + 1))
            End If
            Dim inlineICidentifier As String = InlineCustomInteractionIdentifier(identifier)
            If Not String.IsNullOrEmpty(inlineICidentifier) Then Return inlineICidentifier

            Return String.Empty
        End Function


        Private Shared Function InlineCustomInteractionIdentifier(identifier As String) As String
            Dim regex As New Regex("CI_SP_(?<guidPart>.+?)_[0-9]")
            Dim result = regex.Match(identifier)
            If result.Success Then
                If Not String.IsNullOrEmpty(result.Groups("guidPart").Value) AndAlso CheckIdentifierIsGuid(result.Groups("guidPart").Value) Then Return result.Groups("guidPart").Value
            End If
            Return String.Empty
        End Function
    End Class
End Namespace