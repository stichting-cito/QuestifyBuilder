Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.Logic.QTI.Facade.Factories.QTI22
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Processing.QTI22

    Public Class QTI22ResponseProcessing

        Private ReadOnly _responseTypeFactory As ResponseTypeFactory
        Private ReadOnly _responseIdentifierAttributeList As XmlNodeList
        Private ReadOnly _finding As KeyFinding
        Private ReadOnly _findingIndex As Integer
        Private ReadOnly _scoringParams As HashSet(Of ScoringParameter)
        Private ReadOnly _shouldBeTranslated As Boolean = False

        Public Sub New(responseIdentifierAttributeList As XmlNodeList, solution As Solution, finding As KeyFinding, findingIndex As Integer, scoringParams As HashSet(Of ScoringParameter), owner As QTI22CombinedScoringConverter, shouldBeTranslated As Boolean)
            _responseIdentifierAttributeList = responseIdentifierAttributeList
            _finding = finding
            _findingIndex = findingIndex
            _scoringParams = scoringParams
            _shouldBeTranslated = shouldBeTranslated
            If _scoringParams IsNot Nothing AndAlso _scoringParams.Count > 0 Then
                _responseTypeFactory = New ResponseTypeFactory(_scoringParams, _finding, _responseIdentifierAttributeList, owner)
            Else
                _responseTypeFactory = New ResponseTypeFactory(solution, _finding, responseIdentifierAttributeList, owner)
            End If
        End Sub

        Public Sub New(finding As KeyFinding, findingIndex As Integer, scoringParams As HashSet(Of ScoringParameter), responseTypeFactory As ResponseTypeFactory, shouldBeTranslated As Boolean)
            _finding = finding
            _findingIndex = findingIndex
            _scoringParams = scoringParams
            _shouldBeTranslated = shouldBeTranslated
            _responseTypeFactory = responseTypeFactory
        End Sub

        Public Function GetProcessing() As XElement

            Dim groups = GetGroups(_finding)

            Dim responseProcessing As XElement = <responseProcessing></responseProcessing>

            If groups.Any() Then
                responseProcessing = GetProcessingForGroups(groups, responseProcessing)
            End If

            If _finding.Facts.Any() AndAlso Not AddFindingFactsDuringProcessingOfGroups() Then
                responseProcessing = GetProcessingForFindingFacts(responseProcessing)
            End If

            If _finding.Method <> EnumScoringMethod.Polytomous Then
                Dim scoreFinding = GetScoreFinding(_findingIndex, responseProcessing)
                responseProcessing.Add(scoreFinding)
            End If

            Return responseProcessing
        End Function


        Private Function GetProcessingForGroups(groups As IEnumerable(Of IEnumerable(Of KeyFactSet)), responseProcessing As XElement) As XElement
            Dim factIndex As Integer = 1
            If _finding.Method = EnumScoringMethod.Polytomous Then
                For Each group In groups.OrderBy(Function(g) GetMinOrderIndexForGroup(g))
                    Dim processing = GetProcessingForGroup(group)
                    Dim processingMerged = GetProcessingMerged(processing)
                    responseProcessing.Add(processingMerged)
                Next
            Else
                Dim processingForGroups As XElement = Nothing
                If groups.Count > 1 OrElse (AddFindingFactsDuringProcessingOfGroups() AndAlso FactsContainsValueForProcessing(_finding.Facts)) Then
                    processingForGroups = <and></and>
                End If
                For Each group In groups.OrderBy(Function(g) GetMinOrderIndexForGroup(g))
                    If processingForGroups IsNot Nothing Then
                        processingForGroups.Add(GetProcessingForGroup(group))
                    Else
                        processingForGroups = GetProcessingForGroup(group)
                    End If
                Next
                If (AddFindingFactsDuringProcessingOfGroups() AndAlso FactsContainsValueForProcessing(_finding.Facts)) Then
                    factIndex = 1
                    For Each fact As KeyFact In GetOrderedList(_finding.Facts)
                        If processingForGroups IsNot Nothing Then
                            processingForGroups.Add(_responseTypeFactory.CreateResponseProcessingTypeByFact(fact)?.GetProcessingForFact(fact, True, GetFindingIdentifier(_findingIndex)))
                        Else
                            processingForGroups = _responseTypeFactory.CreateResponseProcessingTypeByFact(fact)?.GetProcessingForFact(fact, True, GetFindingIdentifier(_findingIndex))
                        End If
                        factIndex += 1
                    Next
                End If
                Dim processingMerged = GetProcessingMerged(processingForGroups)
                responseProcessing.Add(processingMerged)
            End If

            Return responseProcessing
        End Function

        Private Function GetProcessingForGroup(group As IEnumerable(Of KeyFactSet)) As XElement
            Dim processingForGroup As XElement = Nothing

            If group.Count() > 1 Then
                processingForGroup = <or></or>
            End If

            For Each factSet As KeyFactSet In group
                Dim processingForFactSet = GetProcessingForFacts(GetOrderedList(factSet.Facts), True)

                If processingForGroup Is Nothing Then
                    processingForGroup = processingForFactSet
                Else
                    processingForGroup.Add(processingForFactSet)
                End If
            Next

            Return processingForGroup
        End Function

        Private Function GetProcessingForFindingFacts(responseProcessing As XElement) As XElement
            If _finding.Method = EnumScoringMethod.Polytomous Then
                Dim factIndex As Integer = 1
                For Each fact As KeyFact In GetOrderedList(_finding.Facts)
                    If fact.ContainsValueForProcessing() Then
                        Dim processing = _responseTypeFactory.CreateResponseProcessingTypeByFact(fact)?.GetProcessingForFact(fact, False, GetFindingIdentifier(_findingIndex))
                        Dim processingMerged = GetProcessingMerged(processing)
                        responseProcessing.Add(processingMerged)
                    End If
                    factIndex += 1
                Next
            Else
                Dim processing = GetProcessingForFacts(GetOrderedList(_finding.Facts), True)
                Dim processingMerged = GetProcessingMerged(processing)
                responseProcessing.Add(processingMerged)
            End If
            Return responseProcessing
        End Function

        Private Function GetProcessingForFacts(facts As List(Of BaseFact), addNotMemberOfElement As Boolean) As XElement
            Dim processingForFacts As XElement = Nothing
            Dim factIndex As Integer = 1
            Dim multipleFacts As Boolean = (facts.Count > 1)

            Dim McFacts As New List(Of BaseFact)
            Dim McDomain As String = String.Empty

            For Each fact As KeyFact In facts
                If TypeOf _responseTypeFactory.CreateResponseProcessingTypeByFact(DirectCast(fact, KeyFact)) Is ResponseProcessingMultipleResponse AndAlso fact.Values.Any() AndAlso fact.Values.All(Function(fv) DirectCast(fv, KeyValue).Values.All(Function(v) TypeOf v Is StringValue)) Then
                    If String.IsNullOrEmpty(McDomain) Then McDomain = fact.Values.First.Domain
                    If fact.Values.First.Domain.Equals(McDomain) Then
                        McFacts.Add(fact)
                    Else
                        ProcessMcFacts(McFacts, multipleFacts, addNotMemberOfElement, processingForFacts)
                        McFacts.Clear()
                        McDomain = fact.Values.First.Domain
                        McFacts.Add(fact)
                    End If
                Else
                    If McFacts.Any() Then
                        ProcessMcFacts(McFacts, multipleFacts, addNotMemberOfElement, processingForFacts)
                        McFacts.Clear()
                    End If

                    Dim processingForFact As XElement = _responseTypeFactory.CreateResponseProcessingTypeByFact(fact)?.GetProcessingForFact(fact, addNotMemberOfElement, GetFindingIdentifier(_findingIndex))
                    If processingForFact IsNot Nothing Then
                        If multipleFacts AndAlso processingForFacts Is Nothing Then processingForFacts = <and></and>
                        If processingForFacts IsNot Nothing Then
                            processingForFacts.Add(processingForFact)
                        Else
                            processingForFacts = processingForFact
                        End If
                    End If
                End If
                factIndex += 1
            Next

            If McFacts.Any() Then
                ProcessMcFacts(McFacts, multipleFacts, addNotMemberOfElement, processingForFacts)
                McFacts.Clear()
            End If

            Return processingForFacts
        End Function

        Private Sub ProcessMcFacts(McFacts As List(Of BaseFact), multipleFacts As Boolean, addNotMemberOfElement As Boolean, ByRef processingForFacts As XElement)
            Dim processingForMcFacts As XElement = Nothing

            McFacts.OfType(Of KeyFact).ToList().ForEach(Sub(mcf)
                                                            Dim processingForMcFact As XElement = _responseTypeFactory.CreateResponseProcessingTypeByFact(mcf)?.GetProcessingForFact(mcf, addNotMemberOfElement, GetFindingIdentifier(_findingIndex))
                                                            If processingForMcFact IsNot Nothing Then
                                                                If McFacts.Count > 1 AndAlso processingForMcFacts Is Nothing Then processingForMcFacts = <or></or>
                                                                If processingForMcFacts IsNot Nothing Then
                                                                    processingForMcFacts.Add(processingForMcFact)
                                                                Else
                                                                    processingForMcFacts = processingForMcFact
                                                                End If
                                                            End If
                                                        End Sub)

            If McFacts.Any() AndAlso multipleFacts AndAlso processingForFacts Is Nothing Then processingForFacts = <and></and>
            If McFacts.Any() Then
                If processingForFacts IsNot Nothing Then
                    processingForFacts.Add(processingForMcFacts)
                Else
                    processingForFacts = processingForMcFacts
                End If
            End If
        End Sub

        Private Function GetGroups(finding As KeyFinding) As IEnumerable(Of IEnumerable(Of KeyFactSet))

            Dim groups = New List(Of List(Of KeyFactSet))()

            For Each factSet In finding.KeyFactsets
                Dim group As List(Of KeyFactSet) = ResponseHelper.GetGroup(groups, factSet)
                If Not group.Any() Then groups.Add(group)
                group.Add(factSet)
            Next

            Return groups
        End Function

        Private Function FactsContainsValueForProcessing(facts As List(Of BaseFact)) As Boolean
            For Each fact As KeyFact In facts
                If (fact.ContainsValueForProcessing()) Then
                    Return True
                End If
            Next
            Return False
        End Function

        Private Function AddFindingFactsDuringProcessingOfGroups() As Boolean
            Return _finding.Method = EnumScoringMethod.Dichotomous AndAlso GetGroups(_finding).Any() AndAlso _finding.Facts.Any()
        End Function


        Private Function GetOrderedList(list As List(Of BaseFact)) As List(Of BaseFact)
            Dim sortedFacts = list.OrderBy(Function(f) _responseTypeFactory.GetOrderIndexByIdentifier(f.Values(0).Domain))
            If _scoringParams IsNot Nothing AndAlso _scoringParams.Count > 0 Then
                sortedFacts = sortedFacts.OrderBy(Function(kf) _responseTypeFactory.GetOrderIndexByKeyValue(kf)).ThenBy(Function(kf) _responseTypeFactory.GetOrderIndexByIdentifier(kf.Values(0).Domain))
            End If
            Return sortedFacts.ToList
        End Function

        Private Function GetMinOrderIndexForGroup(group As IEnumerable(Of KeyFactSet)) As Double
            Dim minOrderIndex As Double = 0
            For Each factset As KeyFactSet In group
                Dim idx As Double = _responseTypeFactory.GetOrderIndexByIdentifier(GetOrderedList(factset.Facts).First.Values(0).Domain)
                If idx > 0 AndAlso idx < minOrderIndex Then minOrderIndex = idx
                If idx > 0 AndAlso minOrderIndex = 0 Then minOrderIndex = idx
            Next
            Return minOrderIndex
        End Function

        Private Function GetProcessingMerged(processing As XElement) As XElement
            If Not ShouldMergeProcessing(processing) Then
                Return processing
            End If

            Dim merged = New XElement(_template)
            Dim findingIdentifier = GetFindingIdentifier(_findingIndex)

            For Each element As XElement In merged.Descendants.Where(Function(d) d.Attribute("identifier") IsNot Nothing)
                element.Attribute("identifier").Value = findingIdentifier
            Next
            If IsScoreFinding(findingIdentifier) Then
                For Each element As XElement In merged.Descendants.Where(Function(d) d.Attribute("baseType") IsNot Nothing)
                    element.Attribute("baseType").Value = BaseValueTypeBaseType.float.ToString()
                Next
            End If

            merged.Descendants("responseIf").First().AddFirst(processing)

            Return merged
        End Function

        Private Function ShouldMergeProcessing(processing As XElement) As Boolean
            If processing IsNot Nothing AndAlso processing.HasElements AndAlso Not processing.FirstNode.Parent.Name.LocalName.ToLower().Equals("setoutcomevalue", StringComparison.InvariantCultureIgnoreCase) Then
                Return True
            End If
            Return False
        End Function

        Private _template As XElement =
            <responseCondition>
                <responseIf>
                    <setOutcomeValue identifier="">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier=""/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>

        Private Function GetScoreFinding(findingIndex As Integer, responseProcessing As XElement) As XElement
            Dim scoreFinding = New XElement(_scoreFinding)
            Dim findingIdentifier = GetFindingIdentifier(findingIndex)

            For Each element As XElement In scoreFinding.Descendants.Where(Function(d) d.Attribute("identifier") IsNot Nothing)
                element.Attribute("identifier").Value = findingIdentifier
            Next

            Dim sumOfResponses = GetSumOfResponses(responseProcessing)
            scoreFinding.Descendants("gte").First().Element("baseValue").Value = sumOfResponses.ToString()

            If IsScoreFinding(findingIdentifier) Then
                For Each element As XElement In scoreFinding.Descendants.Where(Function(d) d.Attribute("baseType") IsNot Nothing)
                    element.Attribute("baseType").Value = BaseValueTypeBaseType.float.ToString()
                Next
            End If

            Return scoreFinding
        End Function

        Private Function GetSumOfResponses(responseProcessing As XElement) As Integer
            Dim findingIdentifier = GetFindingIdentifier(_findingIndex)
            Dim baseValueToCheck = BaseValueTypeBaseType.integer.ToString()
            If IsScoreFinding(findingIdentifier) Then
                baseValueToCheck = BaseValueTypeBaseType.float.ToString()
            End If

            Dim outComeValues = responseProcessing.Descendants("setOutcomeValue").Where(
                Function(s)
                    Return s.Attribute("identifier").Value.Equals(findingIdentifier) AndAlso
                           s.Descendants("sum").Elements("baseValue").Any() AndAlso
                           s.Descendants("sum").Elements("baseValue").Attributes("baseType").FirstOrDefault.Value = baseValueToCheck AndAlso
                           s.Descendants("sum").Elements("baseValue").Value <> "0"
                End Function)

            Dim sum As Integer = 0
            For Each element As XElement In outComeValues
                Dim score As Integer = 0
                Integer.TryParse(element.Descendants("sum").Elements("baseValue").Value, score)
                sum += score
            Next

            sum += responseProcessing.Descendants("setOutcomeValue").Where(Function(s)
                                                                               Return s.Attribute("identifier").Value.Equals(findingIdentifier) AndAlso
                                                                                      s.Descendants("sum").Any() AndAlso s.Descendants("mapResponsePoint").Any()
                                                                           End Function).Sum(Function(m) m.Descendants("mapResponsePoint").Count())

            Return sum
        End Function

        Private Function GetFindingIdentifier(findingIndex As Integer) As String
            If findingIndex > 0 Then
                Return QTIScoringHelper.GetScoreFindingId(findingIndex)
            Else
                Return QTIScoringHelper.GetScoreId(_shouldBeTranslated)
            End If
        End Function

        Private Function IsScoreFinding(findingIdentifier As String) As Boolean
            Return findingIdentifier.Equals(PackageCreatorConstants.SCORE, StringComparison.InvariantCultureIgnoreCase)
        End Function

        Private _scoreFinding As XElement =
             <responseCondition>
                 <responseIf>
                     <gte>
                         <variable identifier=""/>
                         <baseValue baseType="integer">1</baseValue>
                     </gte>
                     <setOutcomeValue identifier="">
                         <baseValue baseType="integer">1</baseValue>
                     </setOutcomeValue>
                 </responseIf>
                 <responseElse>
                     <setOutcomeValue identifier="">
                         <baseValue baseType="integer">0</baseValue>
                     </setOutcomeValue>
                 </responseElse>
             </responseCondition>


    End Class

End Namespace