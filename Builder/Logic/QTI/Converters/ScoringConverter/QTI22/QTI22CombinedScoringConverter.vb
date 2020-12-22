
Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.XPath
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Facade.Factories.QTI22
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Interfaces.QTI22
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final
Imports Questify.Builder.Logic.QTI.Converters.Declaration.QTI22
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.ScoringConverter.QTI22

    Public Class QTI22CombinedScoringConverter
        Implements IScoringConverterQTI22

        Private ReadOnly _ns As Serialization.XmlSerializerNamespaces = New Serialization.XmlSerializerNamespaces()
        ReadOnly _responseTypeFactoryPerFinding As New Dictionary(Of KeyFinding, ResponseTypeFactory)
        Dim _overallScoringMethod As EnumScoringMethod = EnumScoringMethod.None
        Dim _itemMaxScore As Integer = 0
        Dim _findingsInItem As List(Of KeyFinding)
        Dim _conceptFindingsInItem As List(Of ConceptFinding)
        ReadOnly _scoringParameters As HashSet(Of ScoringParameter)


        Public Sub New()
        End Sub

        Public Sub New(scoringParameters As HashSet(Of ScoringParameter))
            _scoringParameters = scoringParameters
        End Sub


        Public Sub UpdateDocumentBeforeProcessing(solution As Solution, itemDocument As XmlDocument, packageCreator As QTI22PackageCreator) Implements IScoringConverterQTI22.UpdateDocumentBeforeProcessing
            Dim addToDiv As Boolean = CombinedScoringHelper.ItemContainsGapmatchInteraction(itemDocument)
            itemDocument.BringElementOutSide("choiceInteraction", "p", addToDiv)
            itemDocument.BringElementOutSide("choiceInteraction", "hottextInteraction", addToDiv)
            itemDocument.BringElementOutSide("extendedTextInteraction", "p", addToDiv)
            itemDocument.BringElementOutSide("table[@class='tabular']", "p", addToDiv)
            AspectScoringHelper.UpdateDocumentBeforeProcessing(solution, itemDocument, packageCreator)
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
            Dim responseDeclaration As New QTI22ResponseDeclaration(responseIdentifierAttributeList, solution, _scoringParameters)
            Return responseDeclaration.GetDeclarations()
        End Function

        Public Function GetResponseDeclarations(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, ByVal type As Nullable(Of ItemTypeEnum)) As List(Of ResponseDeclarationType) Implements IScoringConverterQTI22.GetResponseDeclarations
            Return GetResponseDeclarations(solution, responseIdentifierAttributeList)
        End Function

        Public Overridable Function GetOutcomeDeclarations(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, translationTable As ItemScoreTranslationTable) As List(Of OutcomeDeclarationType) Implements IScoringConverterQTI22.GetOutcomeDeclarations
            If _findingsInItem Is Nothing Then _findingsInItem = CombinedScoringHelper.GetFindingsInItem(solution, responseIdentifierAttributeList)
            Dim list As List(Of OutcomeDeclarationType) = QTI22ScoringHelper.GetDefaultOutcomeDeclarationsList(translationTable)

            If QTI22ScoringHelper.HasAutomaticScoring(solution) AndAlso _findingsInItem IsNot Nothing AndAlso _findingsInItem.Count > 0 Then
                Dim findingIndex As Integer = 1
                Dim outcomeDeclarationType As OutcomeDeclarationType

                For Each finding As KeyFinding In _findingsInItem
                    If _findingsInItem.Count > 1 Then
                        outcomeDeclarationType = QTI22ScoringHelper.GetDefaultOutComeDeclaration(Nothing, findingIndex, Nothing, QTI22ScoringHelper.ShouldScoreBeTranslated(translationTable))
                        list.Add(outcomeDeclarationType)
                    End If

                    Dim responseTypeFactory As ResponseTypeFactory = If(_responseTypeFactoryPerFinding.ContainsKey(finding), _responseTypeFactoryPerFinding(finding), Nothing)
                    Dim responseIndexForScoreCheck As Integer = 1

                    For i As Integer = 0 To responseIdentifierAttributeList.Count - 1
                        Dim responseIdentifierAttribute As XmlAttribute = DirectCast(responseIdentifierAttributeList(i), XmlAttribute)
                        Dim controlType As CombinedScoringHelper.EnumControlType = CombinedScoringHelper.DetermineControlType(responseIdentifierAttribute, _scoringParameters)

                        If controlType = CombinedScoringHelper.EnumControlType.Input OrElse
                           controlType = CombinedScoringHelper.EnumControlType.CasEqualFormulaEditor OrElse
                           controlType = CombinedScoringHelper.EnumControlType.CasEvaluateFormulaEditor Then

                            If Not _responseTypeFactoryPerFinding.ContainsKey(finding) Then
                                If _scoringParameters Is Nothing Then
                                    responseTypeFactory = New ResponseTypeFactory(solution, finding, responseIdentifierAttributeList, Me)
                                Else
                                    responseTypeFactory = New ResponseTypeFactory(_scoringParameters, finding, responseIdentifierAttributeList, Me)
                                End If
                                _responseTypeFactoryPerFinding.Add(finding, responseTypeFactory)
                            End If

                            Dim keyvalue As KeyValue = CombinedScoringHelper.GetKeyValueByResponseIdentifier(finding, responseIdentifierAttribute.Value)
                            If keyvalue Is Nothing Then keyvalue = CombinedScoringHelper.GetKeyValueForInputByIndex(finding, responseIndexForScoreCheck)
                            If keyvalue IsNot Nothing AndAlso keyvalue.Values IsNot Nothing AndAlso QTI22ScoringHelper.KeyValueContainsDecimal(keyvalue.Values) AndAlso CombinedScoringHelper.DetermineDecimalSeparatorForGap(responseIdentifierAttribute) = CombinedScoringHelper.DecimalSeparator.Both Then
                                outcomeDeclarationType = QTI22ScoringHelper.GetDecimalOutComeDeclaration(responseTypeFactory.GetResponseIndexByIdentifier(keyvalue.Domain))
                                list.Add(outcomeDeclarationType)
                            End If
                            responseIndexForScoreCheck += 1
                        End If
                    Next

                    findingIndex += 1
                Next
            End If

            If QTI22ScoringHelper.HasManualScoring(solution) Then
                For Each aspectReference As AspectReference In solution.AspectReferenceSetCollection(0).Items
                    Dim outcomeDeclarationType As OutcomeDeclarationType = New OutcomeDeclarationType
                    outcomeDeclarationType.identifier = $"qtiAspect{aspectReference.SourceName}OutcomeDeclaration"
                    outcomeDeclarationType.baseType = OutcomeDeclarationTypeBaseType.integer
                    outcomeDeclarationType.baseTypeSpecified = True
                    outcomeDeclarationType.cardinality = OutcomeDeclarationTypeCardinality.single
                    outcomeDeclarationType.view = New List(Of ViewType)
                    outcomeDeclarationType.view.Add(ViewType.scorer)

                    outcomeDeclarationType.normalMinimum = 0
                    outcomeDeclarationType.normalMinimumSpecified = True

                    outcomeDeclarationType.normalMaximum = aspectReference.MaxScore
                    outcomeDeclarationType.normalMaximumSpecified = True

                    list.Add(outcomeDeclarationType)
                Next
            End If

            Dim maxScore = If(QTIScoringHelper.ShouldScoreBeTranslated(translationTable), solution.MaxSolutionTranslatedScore, solution.MaxSolutionRawScore)
            If maxScore.HasValue Then
                Dim maxScoreOutcomeDeclarationType = QTI22ScoringHelper.GetMaxScoreOutcomeDeclaration(maxScore.Value)
                list.Add(maxScoreOutcomeDeclarationType)
            End If

            Dim listHottext As List(Of OutcomeDeclarationType) = HottextScoringHelper.GetOutcomeDeclarationsForHottextInteractions(responseIdentifierAttributeList)
            If listHottext.Count > 0 Then list.AddRange(listHottext)

            Dim listConcepts As List(Of OutcomeDeclarationType) = GetOutcomeDeclarationsForConceptScoring(solution, responseIdentifierAttributeList)
            If listConcepts.Count > 0 Then list.AddRange(listConcepts)

            Return list
        End Function

        Private Function GetOutcomeDeclarationsForConceptScoring(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList) As List(Of OutcomeDeclarationType)
            If _conceptFindingsInItem Is Nothing Then _conceptFindingsInItem = CombinedScoringHelper.GetConceptFindingsInItem(solution, responseIdentifierAttributeList)
            Dim list As List(Of OutcomeDeclarationType) = New List(Of OutcomeDeclarationType)

            For Each finding As ConceptFinding In _conceptFindingsInItem
                If Not finding Is Nothing Then
                    Dim conceptResponseGroups = GetNumberOfConceptResponses(finding, responseIdentifierAttributeList)

                    For Each kvp In conceptResponseGroups
                        For Each conceptName In kvp.Value.OrderBy(Function(x) x)
                            Dim conceptResponseIdentifier As String = QTI22ScoringHelper.GetConceptScoringId(kvp.Key, conceptName)
                            Dim outcomeDeclarationType As New OutcomeDeclarationType With {.identifier = QTIScoringHelper.EncodeAsQTIIdentifier(conceptResponseIdentifier, "-"),
                                    .baseType = OutcomeDeclarationTypeBaseType.integer,
                                    .cardinality = OutcomeDeclarationTypeCardinality.single,
                                    .baseTypeSpecified = True}


                            Dim value As New ValueType With {.Value = "0"}
                            Dim val = New List(Of ValueType)
                            val.Add(value)
                            Dim defaultValue As New DefaultValueType() With {.value = val}
                            outcomeDeclarationType.defaultValue = defaultValue

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

        Public Function GetResponseProcessing(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList, shouldBeTranslated As Boolean) As XmlDocument Implements IScoringConverterQTI22.GetResponseProcessing

            _ns.Add(String.Empty, "http://www.imsglobal.org/xsd/imsqti_v2p2")
            Dim result As XmlDocument = New XmlDocument()
            Dim navigator As XPathNavigator = result.CreateNavigator()

            If QTI22ScoringHelper.HasAutomaticScoring(solution) Then
                If _findingsInItem Is Nothing Then _findingsInItem = CombinedScoringHelper.GetFindingsInItem(solution, responseIdentifierAttributeList)
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
                        AddOverallScoreCheck(navigator, solution, QTI22ScoringHelper.GetScoreId(shouldBeTranslated))
                    Else
                        AddOverallSumOfResponses(navigator, QTI22ScoringHelper.GetScoreId(shouldBeTranslated))
                    End If
                End If
                If shouldBeTranslated Then
                    QTI22ScoringHelper.AddLookUpOutComeValue(navigator)
                End If

                If _conceptFindingsInItem Is Nothing Then _conceptFindingsInItem = CombinedScoringHelper.GetConceptFindingsInItem(solution, responseIdentifierAttributeList)
                navigator.MoveToRoot()
                navigator.MoveToFirstChild()
                For Each finding As ConceptFinding In _conceptFindingsInItem
                    If Not finding Is Nothing Then
                        AppendResponseProcessingConceptScoring(navigator, result, responseIdentifierAttributeList, finding)
                    End If
                Next
            End If

            If result.DocumentElement Is Nothing OrElse Not result.DocumentElement.HasChildNodes Then Return Nothing
            Return result
        End Function

        Public Overridable Sub AppendResponseProcessing(ByVal navigator As XPathNavigator, result As XmlDocument, responseIdentifierAttributeList As XmlNodeList, solution As Solution, finding As KeyFinding, findingIndex As Integer, shouldBeTranslated As Boolean)
            Dim responseProcessing As QTI22ResponseProcessing = Nothing
            If _responseTypeFactoryPerFinding.ContainsKey(finding) Then
                responseProcessing = New QTI22ResponseProcessing(finding, findingIndex, _scoringParameters, _responseTypeFactoryPerFinding(finding), shouldBeTranslated)
            Else
                responseProcessing = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, _scoringParameters, Me, shouldBeTranslated)
            End If
            Dim responseProcessingScoring As XmlDocument = responseProcessing.GetProcessing().ToXmlDocument()

            For Each node As XmlNode In responseProcessingScoring.SelectNodes("//responseProcessing/responseCondition|//responseProcessing/setOutcomeValue")
                Dim importedNode As XmlNode = result.ImportNode(node, True)
                navigator.AppendChild(importedNode.OuterXml.ToString)
                navigator.MoveToRoot()
                navigator.MoveToFirstChild()
            Next
        End Sub

        Public Sub AppendResponseProcessingPartForDecimalGaps(ByVal navigator As XPathNavigator, solution As Solution, responseIdentifierAttributeList As XmlNodeList)

            If _findingsInItem Is Nothing Then _findingsInItem = CombinedScoringHelper.GetFindingsInItem(solution, responseIdentifierAttributeList)
            Dim decimalResponseCondition As XElement =
                    <responseCondition>
                        <responseIf>
                            <isNull>
                                <variable identifier="{0}"/>
                            </isNull>
                            <setOutcomeValue identifier="{1}">
                                <baseValue baseType="float">0.0</baseValue>
                            </setOutcomeValue>
                        </responseIf>
                        <responseElseIf>
                            <patternMatch pattern="{2}">
                                <variable identifier="{0}"/>
                            </patternMatch>
                            <setOutcomeValue identifier="{1}">
                                <variable identifier="{0}"/>
                            </setOutcomeValue>
                        </responseElseIf>
                        <responseElse>
                            <setOutcomeValue identifier="{1}">
                        {3}
                    </setOutcomeValue>
                        </responseElse>
                    </responseCondition>

            For Each finding As KeyFinding In _findingsInItem
                Dim responseTypeFactory As ResponseTypeFactory = If(_responseTypeFactoryPerFinding.ContainsKey(finding), _responseTypeFactoryPerFinding(finding), Nothing)
                Dim responseIndexForScoreCheck As Integer = 1

                For Each responseIdentifierAttribute As XmlAttribute In responseIdentifierAttributeList
                    If CombinedScoringHelper.DetermineControlType(responseIdentifierAttribute, _scoringParameters) = CombinedScoringHelper.EnumControlType.Input Then
                        If responseTypeFactory Is Nothing Then
                            If _scoringParameters Is Nothing Then
                                responseTypeFactory = New ResponseTypeFactory(solution, finding, responseIdentifierAttributeList, Me)
                            Else
                                responseTypeFactory = New ResponseTypeFactory(_scoringParameters, finding, responseIdentifierAttributeList, Me)
                            End If
                            _responseTypeFactoryPerFinding.Add(finding, responseTypeFactory)
                        End If
                        Dim keyvalue As KeyValue = CombinedScoringHelper.GetKeyValueByResponseIdentifier(finding, responseIdentifierAttribute.Value)
                        If keyvalue Is Nothing Then keyvalue = CombinedScoringHelper.GetKeyValueForInputByIndex(finding, responseIndexForScoreCheck)
                        If keyvalue IsNot Nothing AndAlso keyvalue.Values IsNot Nothing AndAlso QTI22ScoringHelper.KeyValueContainsDecimal(keyvalue.Values) AndAlso CombinedScoringHelper.DetermineDecimalSeparatorForGap(responseIdentifierAttribute) = CombinedScoringHelper.DecimalSeparator.Both Then
                            Dim responseIndex As Integer = responseTypeFactory.GetResponseIndexByIdentifier(keyvalue.Domain)
                            Dim patternMask As String = responseIdentifierAttribute.OwnerElement.Attributes("patternMask").Value
                            patternMask = patternMask.Replace("[\,\.]", "[\.]").Replace("[\.\,]", "[\.]")
                            navigator.AppendChild(String.Format(decimalResponseCondition.ToString, QTI22ScoringHelper.GetResponseId(responseIndex), QTI22ScoringHelper.GetDecimalResponseId(responseIndex), patternMask, GetResponseProcessingCustomOperators.GetDecimalCustomOperator(responseIndex).ToString()))
                        End If
                        responseIndexForScoreCheck += 1
                    End If
                Next
            Next
        End Sub

        Public Overridable Sub AppendResponseProcessingConceptScoring(ByVal navigator As XPathNavigator, result As XmlDocument, responseIdentifierAttributeList As XmlNodeList, finding As ConceptFinding)
            Dim conceptResponseProcessing As QTI22ConceptResponseProcessing = Nothing
            If _responseTypeFactoryPerFinding.ContainsKey(finding) Then
                conceptResponseProcessing = New QTI22ConceptResponseProcessing(finding, _responseTypeFactoryPerFinding(finding))
            Else
                conceptResponseProcessing = New QTI22ConceptResponseProcessing(responseIdentifierAttributeList, finding, _scoringParameters, Me)
            End If
            Dim responseProcessingConceptScoring As XmlDocument = conceptResponseProcessing.GetProcessing(False).ToXmlDocument()

            For Each node As XmlNode In responseProcessingConceptScoring.SelectNodes("//responseProcessing/responseCondition|//responseProcessing/setOutcomeValue")
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

            Dim outcomeVariable As XElement = <variable identifier="{0}"/>
            Dim variableProcessing As XElement = <root></root>

            Dim findingIndex As Integer = 1
            For Each finding As KeyFinding In _findingsInItem
                variableProcessing.Add(XElement.Parse(String.Format(outcomeVariable.ToString, QTI22ScoringHelper.GetScoreFindingId(findingIndex))))
                findingIndex += 1
            Next

            Dim outcome As XElement = <setOutcomeValue identifier=<%= identifier %>>
                                          <sum>
                                              <%= variableProcessing.Elements %>
                                          </sum>
                                      </setOutcomeValue>

            ResponseProcessingHelper.AddResponseProcessingIfNeeded(navigator)

            navigator.AppendChild(outcome.ToString)
            navigator.MoveToRoot()
            navigator.MoveToFirstChild()
        End Sub

        Private Sub AddOverallScoreCheck(ByVal navigator As XPathNavigator, ByVal solution As Solution, ByVal identifier As String)

            If _itemMaxScore = 0 Then DetermineOverallMaxScore()

            ResponseProcessingHelper.AddResponseProcessingIfNeeded(navigator)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "responseCondition", Nothing, True)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "responseIf", Nothing, True)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "gte", Nothing, True)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "sum", Nothing, True)

            Dim findingIndex As Integer = 1

            For Each finding As KeyFinding In _findingsInItem
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "variable", Nothing, True)
                AddAttribute(navigator, "identifier", QTI22ScoringHelper.GetScoreFindingId(findingIndex))
                navigator.MoveToParent()

                findingIndex += 1
            Next

            navigator.MoveToParent()

            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "baseValue", QTI22ScoringHelper.GetNrOfKeyFindingsWithScoringMethod(solution).ToString(), True)
            AddAttribute(navigator, "baseType", ResponseDeclarationTypeBaseType.integer.ToString())
            navigator.MoveToParent()
            navigator.MoveToParent()
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "setOutcomeValue", Nothing, True)
            AddAttribute(navigator, "identifier", identifier)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "baseValue", _itemMaxScore.ToString(), True)
            AddAttribute(navigator, "baseType", ResponseDeclarationTypeBaseType.integer.ToString())
            navigator.MoveToParent()
            navigator.MoveToParent()
            navigator.MoveToParent()

            If Not (_itemMaxScore > 1) AndAlso _overallScoringMethod <> EnumScoringMethod.Polytomous Then
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "responseElse", Nothing, True)
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "setOutcomeValue", Nothing, True)
                AddAttribute(navigator, "identifier", identifier)
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "baseValue", "0", True)
                AddAttribute(navigator, "baseType", ResponseDeclarationTypeBaseType.integer.ToString())
            End If

        End Sub

        Public Sub UpdateDocument(ByVal solution As Solution, ByVal itemDocument As XmlDocument, ByVal itemExtensionDocument As XmlDocument, packageCreator As QTI22PackageCreator) Implements IScoringConverterQTI22.UpdateDocument
            QTI22ScoringHelper.SetGapMatchGapsRequiredAttribute(solution, itemDocument)
            QTI22ScoringHelper.ConvertMCIdentifierToFixedName(itemDocument)
            QTI22ScoringHelper.ConvertResponseIdentifierToFixedName(itemDocument)
            QTI22ScoringHelper.ConvertGapMatchIdentifierToFixedName(itemDocument, itemExtensionDocument, packageCreator)
            QTI22ScoringHelper.ConvertHottextIdentifierToFixedName(itemDocument)
            QTI22ScoringHelper.ConvertResponseIdentifiersForDateInputFields(itemDocument)
            QTI22ScoringHelper.RemoveDateSubTypeAttributesForDateInputFields(itemDocument)
            QTI22ScoringHelper.RemoveTimeSubTypeAttributesForTimeInputFields(itemDocument)
            QTI22ScoringHelper.RemoveHottextIdAttributesForCorrectionFields(itemDocument)
            QTI22ScoringHelper.RemoveFormulaEditorAttributesForTextFields(itemDocument)
            QTI22ScoringHelper.RemoveCategorizeAttributesForGraphicGapMatchFields(itemDocument)
            QTI22ScoringHelper.DecodeMathMLResponses(itemDocument)
        End Sub


    End Class
End Namespace