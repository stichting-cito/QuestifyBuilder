
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.XPath
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Converters.Declaration.QTI30
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Facade.Factories.QTI30
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Interfaces
Imports Questify.Builder.Logic.QTI.Interfaces.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Converters.ScoringConverter.QTI30

    Public Class CombinedScoringConverter
        Implements IScoringConverter

        Private ReadOnly _ns As Serialization.XmlSerializerNamespaces = New Serialization.XmlSerializerNamespaces()
        ReadOnly _responseTypeFactoryPerFinding As New Dictionary(Of KeyFinding, ResponseTypeFactory)
        Dim _overallScoringMethod As EnumScoringMethod = EnumScoringMethod.None
        Dim _itemMaxScore As Integer = 0
        Dim _findingsInItem As List(Of KeyFinding)
        Dim _conceptFindingsInItem As List(Of ConceptFinding)
        Dim _aspectScoringHelper As AspectScoringHelper
        Protected ReadOnly _scoringParameters As HashSet(Of ScoringParameter)

        Public Sub New()
        End Sub

        Public Sub New(scoringParameters As HashSet(Of ScoringParameter))
            _scoringParameters = scoringParameters
        End Sub

        Public Sub UpdateDocumentBeforeProcessing(solution As Solution, itemDocument As XmlDocument, packageCreator As PackageCreator) Implements IScoringConverter.UpdateDocumentBeforeProcessing
            Dim addToDiv As Boolean = QTI30CombinedScoringHelper.ItemContainsGapmatchInteraction(itemDocument)
            itemDocument.BringElementOutSide("qti-choice-interaction", "p", addToDiv)
            itemDocument.BringElementOutSide("qti-choice-interaction", "qti-hottext-interaction", addToDiv)
            itemDocument.BringElementOutSide("qti-extended-text-interaction", "p", addToDiv)
            itemDocument.BringElementOutSide("table[@class='tabular']", "p", addToDiv)

            Dim aspectScoringHelper = GetAspectScoringHelper(solution, packageCreator)
            aspectScoringHelper.UpdateDocumentBeforeProcessing(itemDocument)

            HottextScoringHelper.UpdateDocumentBeforeProcessing(solution, itemDocument, packageCreator)
        End Sub

        Public Sub DetermineOverallScoringMethod()
            If _findingsInItem.Count > 1 Then
                _overallScoringMethod = EnumScoringMethod.Polytomous
            Else
                For intX As Integer = 0 To _findingsInItem.Count - 1
                    Select Case _findingsInItem(intX).Method
                        Case EnumScoringMethod.Polytomous
                            _overallScoringMethod = EnumScoringMethod.Polytomous
                            Exit For
                        Case EnumScoringMethod.None
                        Case Else
                            _overallScoringMethod = _findingsInItem(intX).Method
                    End Select
                Next
            End If
        End Sub

        Public Sub DetermineOverallMaxScore()
            If _overallScoringMethod = EnumScoringMethod.Dichotomous Then
                _itemMaxScore = 1
            End If
        End Sub

        Public Function GetResponseDeclarations(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList) As List(Of ResponseDeclarationType)
            Dim responseDeclaration As New ResponseDeclaration(responseIdentifierAttributeList, solution, _scoringParameters)
            Return responseDeclaration.GetDeclarations()
        End Function

        Public Function GetResponseDeclarations(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, ByVal type As Nullable(Of ItemTypeEnum)) As List(Of ResponseDeclarationType) Implements IScoringConverter.GetResponseDeclarations
            Return GetResponseDeclarations(solution, responseIdentifierAttributeList)
        End Function

        Public Overridable Function GetOutcomeDeclarations(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, translationTable As ItemScoreTranslationTable,
                                                           ByVal packageCreator As IPackageCreator) As List(Of OutcomeDeclarationType) Implements IScoringConverter.GetOutcomeDeclarations
            If _findingsInItem Is Nothing Then _findingsInItem = QTI30CombinedScoringHelper.GetFindingsInItem(solution, responseIdentifierAttributeList)
            Dim outcomeDeclarations As List(Of OutcomeDeclarationType) = ScoringHelper.GetDefaultOutcomeDeclarationsList(translationTable)

            If ScoringHelper.HasAutomaticScoring(solution) AndAlso _findingsInItem IsNot Nothing AndAlso _findingsInItem.Count > 0 Then
                Dim automaticScoringOutcomeDeclarations = GetOutcomeDeclarationsForAutomaticallyScoredInteractions(solution, responseIdentifierAttributeList, translationTable)
                outcomeDeclarations.AddRange(automaticScoringOutcomeDeclarations)
            End If

            If ScoringHelper.HasManualScoring(solution) Then
                Dim aspectScoringHelper = GetAspectScoringHelper(solution, packageCreator)
                Dim aspectScoringOutcomeDeclarations = aspectScoringHelper.GetOutcomeDeclarations()
                outcomeDeclarations.AddRange(aspectScoringOutcomeDeclarations)
            End If

            Dim maxScore = If(QTIScoringHelper.ShouldScoreBeTranslated(translationTable), solution.MaxSolutionTranslatedScore, solution.MaxSolutionRawScore)
            If maxScore.HasValue Then
                Dim maxScoreOutcomeDeclarationType = ScoringHelper.GetMaxScoreOutcomeDeclaration(maxScore.Value)
                outcomeDeclarations.Add(maxScoreOutcomeDeclarationType)
            End If

            Dim listHottext As List(Of OutcomeDeclarationType) = HottextScoringHelper.GetOutcomeDeclarationsForHottextInteractions(responseIdentifierAttributeList)
            If listHottext.Count > 0 Then
                outcomeDeclarations.AddRange(listHottext)
            End If

            Dim listConcepts As List(Of OutcomeDeclarationType) = GetOutcomeDeclarationsForConceptScoring(solution, responseIdentifierAttributeList)
            If listConcepts.Count > 0 Then
                outcomeDeclarations.AddRange(listConcepts)
            End If

            Return outcomeDeclarations
        End Function

        Private Function GetOutcomeDeclarationsForAutomaticallyScoredInteractions(solution As Solution, responseIdentifierAttributeList As XmlNodeList, translationTable As ItemScoreTranslationTable) As List(Of OutcomeDeclarationType)
            Dim outcomeDeclarations = New List(Of OutcomeDeclarationType)
            Dim findingIndex As Integer = 1
            Dim outcomeDeclarationType As OutcomeDeclarationType

            For Each finding As KeyFinding In _findingsInItem
                If _findingsInItem.Count > 1 Then
                    outcomeDeclarationType = ScoringHelper.GetDefaultOutComeDeclaration(Nothing, findingIndex, Nothing, QTIScoringHelper.ShouldScoreBeTranslated(translationTable))
                    outcomeDeclarations.Add(outcomeDeclarationType)
                End If
                Dim extraOutcomeDeclarations = AddExtraOutcomeDeclarationForDecimalAndCurrencyGaps(finding, solution, responseIdentifierAttributeList)
                outcomeDeclarations.AddRange(extraOutcomeDeclarations)
                findingIndex += 1
            Next

            Return outcomeDeclarations
        End Function

        Private Function AddExtraOutcomeDeclarationForDecimalAndCurrencyGaps(finding As KeyFinding, solution As Solution, responseIdentifierAttributeList As XmlNodeList) As List(Of OutcomeDeclarationType)
            Dim decimalAndConcurrencyGapOutcomeDeclarations = New List(Of OutcomeDeclarationType)
            Dim responseTypeFactory As ResponseTypeFactory = If(_responseTypeFactoryPerFinding.ContainsKey(finding), _responseTypeFactoryPerFinding(finding), Nothing)
            Dim responseIndexForScoreCheck As Integer = 1

            For i As Integer = 0 To responseIdentifierAttributeList.Count - 1
                Dim responseIdentifierAttribute As XmlAttribute = DirectCast(responseIdentifierAttributeList(i), XmlAttribute)
                Dim controlType As QTI30CombinedScoringHelper.EnumControlType = QTI30CombinedScoringHelper.DetermineControlType(responseIdentifierAttribute, _scoringParameters)

                If controlType <> QTI30CombinedScoringHelper.EnumControlType.Input AndAlso
                   controlType <> QTI30CombinedScoringHelper.EnumControlType.CasEqualFormulaEditor AndAlso
                   controlType <> QTI30CombinedScoringHelper.EnumControlType.CasEvaluateFormulaEditor Then
                    Continue For
                End If

                If Not _responseTypeFactoryPerFinding.ContainsKey(finding) Then
                    If _scoringParameters Is Nothing Then
                        responseTypeFactory = New ResponseTypeFactory(solution, finding, responseIdentifierAttributeList, Me)
                    Else
                        responseTypeFactory = New ResponseTypeFactory(_scoringParameters, finding, responseIdentifierAttributeList, Me)
                    End If
                    _responseTypeFactoryPerFinding.Add(finding, responseTypeFactory)
                End If

                Dim keyvalue = GetKeyValueForFinding(finding, responseIdentifierAttribute, responseIndexForScoreCheck)
                If KeyValueContainsDecimal(keyvalue, responseIdentifierAttribute) Then
                    Dim outcomeDeclarationType = ScoringHelper.GetDecimalOutComeDeclaration(responseTypeFactory.GetResponseIndexByIdentifier(keyvalue.Domain))
                    decimalAndConcurrencyGapOutcomeDeclarations.Add(outcomeDeclarationType)
                End If
            Next
            Return decimalAndConcurrencyGapOutcomeDeclarations
        End Function

        Private Function GetKeyValueForFinding(finding As KeyFinding, responseIdentifierAttribute As XmlAttribute, responseIndexForScoreCheck As Integer) As KeyValue
            Dim keyvalue As KeyValue = QTI30CombinedScoringHelper.GetKeyValueByResponseIdentifier(finding, responseIdentifierAttribute.Value)
            If keyvalue IsNot Nothing Then
                Return keyvalue
            End If
            Return QTI30CombinedScoringHelper.GetKeyValueForInputByIndex(finding, responseIndexForScoreCheck)
        End Function

        Private Function KeyValueContainsDecimal(keyvalue As KeyValue, responseIdentifierAttribute As XmlAttribute) As Boolean
            Return keyvalue IsNot Nothing AndAlso
                keyvalue.Values IsNot Nothing AndAlso
                QTIScoringHelper.KeyValueContainsDecimal(keyvalue.Values) AndAlso
                QTI30CombinedScoringHelper.DetermineDecimalSeparatorForGap(responseIdentifierAttribute) = QTI30CombinedScoringHelper.DecimalSeparator.Both
        End Function

        Private Function GetOutcomeDeclarationsForConceptScoring(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList) As List(Of OutcomeDeclarationType)
            If _conceptFindingsInItem Is Nothing Then _conceptFindingsInItem = QTI30CombinedScoringHelper.GetConceptFindingsInItem(solution, responseIdentifierAttributeList)
            Dim list As List(Of OutcomeDeclarationType) = New List(Of OutcomeDeclarationType)

            For Each finding As ConceptFinding In _conceptFindingsInItem
                If Not finding Is Nothing Then
                    Dim conceptResponseGroups = GetNumberOfConceptResponses(finding, responseIdentifierAttributeList)

                    For Each kvp In conceptResponseGroups
                        For Each conceptName In kvp.Value.OrderBy(Function(x) x)
                            Dim conceptResponseIdentifier As String = QTIScoringHelper.GetConceptScoringId(kvp.Key, conceptName)
                            Dim outcomeDeclarationType As New OutcomeDeclarationType With {.identifier = QTIScoringHelper.EncodeAsQTIIdentifier(conceptResponseIdentifier, "-"),
                                    .basetype = OutcomeDeclarationTypeBasetype.integer,
                                    .cardinality = OutcomeDeclarationTypeCardinality.single,
                                    .basetypeSpecified = True}


                            Dim value As New ValueType With {.Value = "0"}
                            Dim val = New List(Of ValueType)
                            val.Add(value)
                            Dim defaultValue As New DefaultValueType() With {.qtivalue = val}
                            outcomeDeclarationType.qtidefaultvalue = defaultValue

                            list.Add(outcomeDeclarationType)
                        Next

                    Next
                End If
            Next

            Return list
        End Function

        Private Function GetNumberOfConceptResponses(ByVal finding As ConceptFinding, ByVal responseIdentifierAttributeList As XmlNodeList) As IDictionary(Of Integer, IEnumerable(Of String))

            Dim ret As New Dictionary(Of Integer, IEnumerable(Of String))
            Dim responseTypeFactory As ResponseTypeFactory
            If _responseTypeFactoryPerFinding.ContainsKey(finding) Then
                responseTypeFactory = _responseTypeFactoryPerFinding(finding)
            Else
                responseTypeFactory = New ResponseTypeFactory(_scoringParameters, finding, responseIdentifierAttributeList, Me)
                _responseTypeFactoryPerFinding.Add(finding, responseTypeFactory)
            End If
            Dim conceptResponseIndex As Integer = 1
            Dim conceptIndexForFindingFacts As Dictionary(Of String, Integer) = responseTypeFactory.CollectionOfConceptIndexPerIdentifier
            Dim responseGroupingHelper As New ResponseGroupingHelper(responseTypeFactory.IdHelper)
            Dim groups As IEnumerable(Of IEnumerable(Of KeyFactSet)) = responseGroupingHelper.GetGroups(finding)
            responseTypeFactory.IdHelper = responseGroupingHelper.IdentifierHelper

            Dim orderedGroups = ResponseProcessingHelper.GetOrderedConceptFactSetGroups(groups, responseTypeFactory)
            For i As Integer = 0 To orderedGroups.Count() - 1
                Dim conceptFactSet = DirectCast(orderedGroups(i).First(), ConceptFactsSet)
                Dim conceptCodes As New List(Of String)()
                For Each c In conceptFactSet.Concepts
                    conceptCodes.Add(c.Code)
                Next
                ret.Add(i + 1, conceptCodes)
            Next

            If orderedGroups.Count > 0 Then
                Dim newConceptIndexForFindingFacts As New Dictionary(Of String, Integer)
                Dim idx As Integer = orderedGroups.Count + 1

                For Each i In conceptIndexForFindingFacts.Values.GroupBy(Function(v) v)
                    Dim addToNewConceptIndex As Boolean = True
                    For Each kvp As KeyValuePair(Of String, Integer) In conceptIndexForFindingFacts.Where(Function(x) x.Value = i.Key)
                        If orderedGroups.Any(Function(g) g.Any(Function(kfs) kfs.Facts.Any(Function(f) responseTypeFactory.GetFactIdentifier(f).Equals(kvp.Key)))) Then
                            addToNewConceptIndex = False
                        End If
                    Next
                    If addToNewConceptIndex Then
                        conceptIndexForFindingFacts.Where(Function(x) x.Value = i.Key).ToList.ForEach(Sub(ff)
                                                                                                          newConceptIndexForFindingFacts.Add(ff.Key, idx)
                                                                                                      End Sub)
                        idx += 1
                    End If
                Next

                conceptIndexForFindingFacts = newConceptIndexForFindingFacts
            End If

            Dim uniqueDomains As Dictionary(Of String, Integer) = ResponseProcessingHelper.GetListOfUniqueDomainsFromConceptFindingFacts(finding, responseTypeFactory)

            For Each domain As String In uniqueDomains.Keys
                conceptResponseIndex = 0
                If conceptIndexForFindingFacts.ContainsKey(domain) Then conceptResponseIndex = conceptIndexForFindingFacts(domain)
                Debug.Assert(conceptResponseIndex > 0, "Get conceptresponses - Index for concept-encoding of findingfact should not be 0")

                Dim conceptCodes As New List(Of String)()
                For Each fact As KeyFact In finding.Facts.Where(Function(f) responseTypeFactory.GetFactIdentifier(f).Equals(domain)).OrderByDescending(Function(x) x.Id).ToList
                    For Each c In DirectCast(fact, ConceptFact).Concepts.OrderBy(Function(concept) concept.Code)
                        If Not conceptCodes.Contains(c.Code) Then conceptCodes.Add(c.Code)
                    Next
                Next
                ret.Add(conceptResponseIndex, conceptCodes)
            Next

            Return ret
        End Function

        Public Function GetResponseProcessing(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, shouldBeTranslated As Boolean, packageCreator As IPackageCreator) As XmlDocument Implements IScoringConverter.GetResponseProcessing

            _ns.Add(String.Empty, "http://www.imsglobal.org/xsd/imsqtiasi_v3p0")
            Dim result As XmlDocument = New XmlDocument()
            Dim navigator As XPathNavigator = result.CreateNavigator()

            If ScoringHelper.HasManualScoring(solution) Then
                Dim aspectScoringHelper = GetAspectScoringHelper(solution, packageCreator)
                aspectScoringHelper.AddLookUpOutComeValues(navigator)
                aspectScoringHelper.AddOverallSumOfAspectOutcomes(QTIScoringHelper.GetScoreId(shouldBeTranslated), navigator)
            End If

            If QTIScoringHelper.HasAutomaticScoring(solution) Then
                If _findingsInItem Is Nothing Then _findingsInItem = QTI30CombinedScoringHelper.GetFindingsInItem(solution, responseIdentifierAttributeList)
                If _overallScoringMethod = EnumScoringMethod.None Then DetermineOverallScoringMethod()

                Dim findingIndex As Integer = CInt(IIf(_findingsInItem IsNot Nothing AndAlso _findingsInItem.Count > 1, 1, 0))

                ResponseProcessingHelper.AddResponseProcessingIfNeeded(navigator)
                AppendResponseProcessingPartForDecimalGaps(navigator, solution, responseIdentifierAttributeList)

                For Each finding As KeyFinding In _findingsInItem
                    If Not finding.Method = EnumScoringMethod.None Then
                        AppendResponseProcessing(navigator, result, responseIdentifierAttributeList, solution, finding, findingIndex, shouldBeTranslated)
                    End If
                    findingIndex += 1
                Next

                navigator.MoveToRoot()
                navigator.MoveToFirstChild()

                If _findingsInItem IsNot Nothing AndAlso _findingsInItem.Count > 1 Then
                    If _overallScoringMethod = EnumScoringMethod.Dichotomous Then
                        AddOverallScoreCheck(navigator, solution, QTIScoringHelper.GetScoreId(shouldBeTranslated))
                    Else
                        AddOverallSumOfResponses(navigator, QTIScoringHelper.GetScoreId(shouldBeTranslated))
                    End If
                End If
            End If

            If shouldBeTranslated Then
                ScoringHelper.AddLookUpOutComeValue(navigator)
            End If

            If _conceptFindingsInItem Is Nothing Then _conceptFindingsInItem = QTI30CombinedScoringHelper.GetConceptFindingsInItem(solution, responseIdentifierAttributeList)
            navigator.MoveToRoot()
            navigator.MoveToFirstChild()
            For Each finding As ConceptFinding In _conceptFindingsInItem
                If Not finding Is Nothing Then
                    AppendResponseProcessingConceptScoring(navigator, result, responseIdentifierAttributeList, finding)
                End If
            Next

            If result.DocumentElement Is Nothing OrElse (Not result.DocumentElement.HasChildNodes AndAlso Not result.DocumentElement.HasAttribute("template")) Then Return Nothing
            Return result
        End Function

        Public Overridable Sub AppendResponseProcessing(ByVal navigator As XPathNavigator, result As XmlDocument, responseIdentifierAttributeList As XmlNodeList, solution As Solution, finding As KeyFinding, findingIndex As Integer, shouldBeTranslated As Boolean)
            Dim responseProcessing As ResponseProcessing = Nothing
            Dim useResponseProcessingTemplate As Boolean = ShouldUseResponseProcessingTemplate(solution, responseIdentifierAttributeList)
            If _responseTypeFactoryPerFinding.ContainsKey(finding) Then
                responseProcessing = New ResponseProcessing(finding, findingIndex, _scoringParameters, _responseTypeFactoryPerFinding(finding), shouldBeTranslated, useResponseProcessingTemplate)
            Else
                responseProcessing = New ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, _scoringParameters, Me, shouldBeTranslated, useResponseProcessingTemplate)
            End If
            Dim responseProcessingScoring As XmlDocument = responseProcessing.GetProcessing().ToXmlDocument()

            Dim responseProcessingTemplateNode = responseProcessingScoring.SelectSingleNode("//qti-response-processing[@template]")
            If responseProcessingTemplateNode IsNot Nothing Then
                Dim importedTemplateNode As XmlNode = result.ImportNode(responseProcessingTemplateNode, True)
                navigator.ReplaceSelf(importedTemplateNode.OuterXml.ToString)
                navigator.MoveToRoot()
                navigator.MoveToFirstChild()
            Else
                For Each node As XmlNode In responseProcessingScoring.SelectNodes("//qti-response-processing/qti-response-condition|//qti-response-processing/qti-set-outcome-value")
                    Dim importedNode As XmlNode = result.ImportNode(node, True)
                    navigator.AppendChild(importedNode.OuterXml.ToString)
                    navigator.MoveToRoot()
                    navigator.MoveToFirstChild()
                Next
            End If
        End Sub

        Protected Overridable Function ShouldUseResponseProcessingTemplate(solution As Solution, responseIdentifierAttributeList As XmlNodeList) As Boolean
            Return QTI30CombinedScoringHelper.ShouldUseResponseProcessingTemplate(solution, _scoringParameters, responseIdentifierAttributeList)
        End Function

        Public Sub AppendResponseProcessingPartForDecimalGaps(ByVal navigator As XPathNavigator, solution As Solution, responseIdentifierAttributeList As XmlNodeList)

            If _findingsInItem Is Nothing Then _findingsInItem = QTI30CombinedScoringHelper.GetFindingsInItem(solution, responseIdentifierAttributeList)
            Dim decimalResponseCondition As XElement =
                    <qti-response-condition>
                        <qti-response-if>
                            <qti-is-null>
                                <qti-variable identifier="{0}"/>
                            </qti-is-null>
                            <qti-set-outcome-value identifier="{1}">
                                <qti-base-value base-type="float">0.0</qti-base-value>
                            </qti-set-outcome-value>
                        </qti-response-if>
                        <qti-response-else-if>
                            <qti-pattern-match pattern="{2}">
                                <qti-variable identifier="{0}"/>
                            </qti-pattern-match>
                            <qti-set-outcome-value identifier="{1}">
                                <qti-variable identifier="{0}"/>
                            </qti-set-outcome-value>
                        </qti-response-else-if>
                        <qti-response-else>
                            <qti-set-outcome-value identifier="{1}">
                        {3}
                    </qti-set-outcome-value>
                        </qti-response-else>
                    </qti-response-condition>

            For Each finding As KeyFinding In _findingsInItem
                Dim responseTypeFactory As ResponseTypeFactory = If(_responseTypeFactoryPerFinding.ContainsKey(finding), _responseTypeFactoryPerFinding(finding), Nothing)
                Dim responseIndexForScoreCheck As Integer = 1

                For Each responseIdentifierAttribute As XmlAttribute In responseIdentifierAttributeList
                    If QTI30CombinedScoringHelper.DetermineControlType(responseIdentifierAttribute, _scoringParameters) = QTI30CombinedScoringHelper.EnumControlType.Input Then
                        If responseTypeFactory Is Nothing Then
                            If _scoringParameters Is Nothing Then
                                responseTypeFactory = New ResponseTypeFactory(solution, finding, responseIdentifierAttributeList, Me)
                            Else
                                responseTypeFactory = New ResponseTypeFactory(_scoringParameters, finding, responseIdentifierAttributeList, Me)
                            End If
                            _responseTypeFactoryPerFinding.Add(finding, responseTypeFactory)
                        End If
                        Dim keyvalue As KeyValue = QTI30CombinedScoringHelper.GetKeyValueByResponseIdentifier(finding, responseIdentifierAttribute.Value)
                        If keyvalue Is Nothing Then keyvalue = QTI30CombinedScoringHelper.GetKeyValueForInputByIndex(finding, responseIndexForScoreCheck)
                        If keyvalue IsNot Nothing AndAlso keyvalue.Values IsNot Nothing AndAlso QTIScoringHelper.KeyValueContainsDecimal(keyvalue.Values) AndAlso QTI30CombinedScoringHelper.DetermineDecimalSeparatorForGap(responseIdentifierAttribute) = QTI30CombinedScoringHelper.DecimalSeparator.Both Then
                            Dim responseIndex As Integer = responseTypeFactory.GetResponseIndexByIdentifier(keyvalue.Domain)
                            Dim patternMask As String = responseIdentifierAttribute.OwnerElement.Attributes("pattern-mask").Value
                            patternMask = patternMask.Replace("[\,\.]", "[\.]").Replace("[\.\,]", "[\.]")
                            navigator.AppendChild(String.Format(decimalResponseCondition.ToString, QTIScoringHelper.GetResponseId(responseIndex), QTIScoringHelper.GetDecimalResponseId(responseIndex), patternMask, GetResponseProcessingCustomOperators.GetDecimalCustomOperator(responseIndex).ToString()))
                        End If
                        responseIndexForScoreCheck += 1
                    End If
                Next
            Next
        End Sub

        Public Overridable Sub AppendResponseProcessingConceptScoring(ByVal navigator As XPathNavigator, result As XmlDocument, responseIdentifierAttributeList As XmlNodeList, finding As ConceptFinding)
            Dim conceptResponseProcessing As ConceptResponseProcessing = Nothing
            If _responseTypeFactoryPerFinding.ContainsKey(finding) Then
                conceptResponseProcessing = New ConceptResponseProcessing(finding, _responseTypeFactoryPerFinding(finding))
            Else
                conceptResponseProcessing = New ConceptResponseProcessing(responseIdentifierAttributeList, finding, _scoringParameters, Me)
            End If
            Dim responseProcessingConceptScoring As XmlDocument = conceptResponseProcessing.GetProcessing(False).ToXmlDocument()

            For Each node As XmlNode In responseProcessingConceptScoring.SelectNodes("//qti-response-processing/qti-response-condition|//qti-response-processing/qti-set-outcome-value")
                Dim importedNode As XmlNode = result.ImportNode(node, True)
                navigator.AppendChild(importedNode.OuterXml.ToString)
                navigator.MoveToRoot()
                navigator.MoveToFirstChild()
            Next
        End Sub

        Public Overridable Function GetResponseProcessingCustomOperators() As IResponseProcessingCustomOperators
            Return New ResponseProcessingCustomOperators
        End Function

        Private Sub AddAttribute(ByVal navigator As XPathNavigator, ByVal attributeName As String, ByVal attributeValue As String)
            Using attributes As XmlWriter = navigator.CreateAttributes()
                attributes.WriteAttributeString(attributeName, attributeValue)
            End Using
        End Sub

        Private Sub AddOverallSumOfResponses(ByVal navigator As XPathNavigator, ByVal identifier As String)

            Dim outcomeVariable As XElement = <qti-variable identifier="{0}"/>
            Dim variableProcessing As XElement = <root></root>

            Dim findingIndex As Integer = 1
            For Each finding As KeyFinding In _findingsInItem
                variableProcessing.Add(XElement.Parse(String.Format(outcomeVariable.ToString, QTIScoringHelper.GetScoreFindingId(findingIndex))))
                findingIndex += 1
            Next

            Dim outcome As XElement = <qti-set-outcome-value identifier=<%= identifier %>>
                                          <qti-sum>
                                              <%= variableProcessing.Elements %>
                                          </qti-sum>
                                      </qti-set-outcome-value>

            ResponseProcessingHelper.AddResponseProcessingIfNeeded(navigator)

            navigator.AppendChild(outcome.ToString)
            navigator.MoveToRoot()
            navigator.MoveToFirstChild()
        End Sub

        Private Sub AddOverallScoreCheck(ByVal navigator As XPathNavigator, ByVal solution As Solution, ByVal identifier As String)

            If _itemMaxScore = 0 Then DetermineOverallMaxScore()

            ResponseProcessingHelper.AddResponseProcessingIfNeeded(navigator)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-response-condition", Nothing, True)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-response-if", Nothing, True)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-gte", Nothing, True)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-sum", Nothing, True)

            Dim findingIndex As Integer = 1

            For Each finding As KeyFinding In _findingsInItem
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-variable", Nothing, True)
                AddAttribute(navigator, "identifier", QTIScoringHelper.GetScoreFindingId(findingIndex))
                navigator.MoveToParent()

                findingIndex += 1
            Next

            navigator.MoveToParent()

            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-base-value", QTIScoringHelper.GetNrOfKeyFindingsWithScoringMethod(solution).ToString(), True)
            AddAttribute(navigator, "base-type", ResponseDeclarationTypeBasetype.integer.ToString())
            navigator.MoveToParent()
            navigator.MoveToParent()
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-set-outcome-value", Nothing, True)
            AddAttribute(navigator, "identifier", identifier)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-base-value", _itemMaxScore.ToString(), True)
            AddAttribute(navigator, "base-type", ResponseDeclarationTypeBasetype.integer.ToString())
            navigator.MoveToParent()
            navigator.MoveToParent()
            navigator.MoveToParent()

            If Not (_itemMaxScore > 1) AndAlso _overallScoringMethod <> EnumScoringMethod.Polytomous Then
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-response-else", Nothing, True)
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-set-outcome-value", Nothing, True)
                AddAttribute(navigator, "identifier", identifier)
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "qti-base-value", "0", True)
                AddAttribute(navigator, "base-type", ResponseDeclarationTypeBasetype.integer.ToString())
            End If

        End Sub

        Public Sub UpdateDocument(ByVal solution As Solution, ByVal itemDocument As XmlDocument, ByVal itemExtensionDocument As XmlDocument, packageCreator As PackageCreator) Implements IScoringConverter.UpdateDocument
            ScoringHelper.SetGapMatchGapsRequiredAttribute(solution, itemDocument)
            ScoringHelper.ConvertMCIdentifierToFixedName(itemDocument)
            ScoringHelper.ConvertResponseIdentifierToFixedName(itemDocument)
            ScoringHelper.ConvertGapMatchIdentifierToFixedName(itemDocument, itemExtensionDocument, packageCreator)
            ScoringHelper.ConvertHottextIdentifierToFixedName(itemDocument)
            ScoringHelper.ConvertResponseIdentifiersForDateInputFields(itemDocument)
            ScoringHelper.RemoveDateSubTypeAttributesForDateInputFields(itemDocument)
            ScoringHelper.RemoveTimeSubTypeAttributesForTimeInputFields(itemDocument)
            ScoringHelper.RemoveHottextIdAttributesForCorrectionFields(itemDocument)
            ScoringHelper.RemoveFormulaEditorAttributesForTextFields(itemDocument)
            ScoringHelper.RemoveCategorizeAttributesForGraphicGapMatchFields(itemDocument)
            ScoringHelper.DecodeMathMLResponses(itemDocument)
        End Sub

        Private Function GetAspectScoringHelper(solution As Solution, packageCreator As IPackageCreator) As AspectScoringHelper
            If _aspectScoringHelper Is Nothing Then
                _aspectScoringHelper = New AspectScoringHelper(solution, packageCreator, _scoringParameters)
            End If
            Return _aspectScoringHelper
        End Function

    End Class
End Namespace