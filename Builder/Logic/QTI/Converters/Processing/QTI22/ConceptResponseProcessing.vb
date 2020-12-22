Imports System.Linq
Imports System.Xml
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.Logic.QTI.Facade.Factories.QTI22
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI22

Namespace QTI.Converters.Processing.QTI22

    Public Class QTI22ConceptResponseProcessing

        Private ReadOnly _responseTypeFactory As ResponseTypeFactory
        Private ReadOnly _finding As ConceptFinding
        Private _conceptScoringIndex As Integer = 1
        Private _conceptIndexForFindingFacts As Dictionary(Of String, Integer)
        Private _groups As List(Of IEnumerable(Of KeyFactSet))

        Public Sub New(responseIdentifierAttributeList As XmlNodeList, finding As ConceptFinding, scoringParams As HashSet(Of ScoringParameter), owner As QTI22CombinedScoringConverter)
            _finding = finding
            _responseTypeFactory = New ResponseTypeFactory(scoringParams, _finding, responseIdentifierAttributeList, owner)
        End Sub

        Public Sub New(finding As ConceptFinding, responseTypeFactory As ResponseTypeFactory)
            _finding = finding
            _responseTypeFactory = responseTypeFactory
        End Sub

        Public Function GetProcessing(invokedForReportingPurposes As Boolean) As XElement

            Dim responseGroupingHelper As New ResponseGroupingHelper(_responseTypeFactory.IdHelper)
            Dim groups As IEnumerable(Of IEnumerable(Of KeyFactSet)) = responseGroupingHelper.GetGroups(_finding)
            _responseTypeFactory.IdHelper = responseGroupingHelper.IdentifierHelper
            _groups = ResponseProcessingHelper.GetOrderedConceptFactSetGroups(groups, _responseTypeFactory)

            Dim responseProcessing As XElement = <responseProcessing></responseProcessing>

            If _groups.Any() Then
                responseProcessing = GetProcessingForGroups(_groups, responseProcessing, invokedForReportingPurposes)
            End If

            If _finding.Facts.Any() Then
                responseProcessing = GetProcessingForFindingFacts(responseProcessing, invokedForReportingPurposes)
            End If

            Return responseProcessing
        End Function

        Private Function GetProcessingForGroups(groups As IEnumerable(Of IEnumerable(Of KeyFactSet)), responseProcessing As XElement, invokedForReportingPurposes As Boolean) As XElement

            For Each group In groups
                Dim processingForGroup = GetProcessingForGroup(group, invokedForReportingPurposes)
                responseProcessing.Add(processingForGroup)
                _conceptScoringIndex += 1
            Next

            Return responseProcessing
        End Function

        Private Function GetProcessingForFindingFacts(responseProcessing As XElement, invokedForReportingPurposes As Boolean) As XElement
            Dim uniqueDomains As Dictionary(Of String, Integer) = ResponseProcessingHelper.GetListOfUniqueDomainsFromConceptFindingFacts(_finding, _responseTypeFactory)

            For Each domain As String In uniqueDomains.Keys
                Dim processing As XElement = Nothing
                Dim idx As Integer = 1
                Dim catchAllFactWithNonZeroValuesExists As Boolean = CheckCatchAllFactWithNonZeroValuesExists(_finding.Facts.Where(Function(f) _responseTypeFactory.GetFactIdentifier(f).Equals(domain)).ToList)

                For Each fact As KeyFact In _finding.Facts.Where(Function(f) _responseTypeFactory.GetFactIdentifier(f).Equals(domain)).OrderByDescending(Function(x) x.Id).ToList
                    _conceptScoringIndex = GetConceptIndexForFindingFactsByDomain(_responseTypeFactory.GetFactIdentifier(fact))

                    Dim addProcessingPart As Boolean = DirectCast(fact, ConceptFact).Concepts.Count > 0

                    If Not invokedForReportingPurposes Then
                        If addProcessingPart AndAlso Not catchAllFactWithNonZeroValuesExists Then addProcessingPart = DirectCast(fact, ConceptFact).Concepts.Any(Function(c) Not c.Value.ToString().Equals("0"))
                    End If

                    If Not addProcessingPart Then Continue For

                    Dim processingIf = GetIfElement(idx, uniqueDomains(domain), fact.Id)
                    Dim processingForFact = _responseTypeFactory.CreateConceptResponseProcessingTypeByFact(fact).GetProcessingForFact(fact)
                    processingIf.Add(processingForFact)

                    Dim concepts = DirectCast(fact, ConceptFact).Concepts.OrderBy(Function(x) x.Code)
                    If Not invokedForReportingPurposes Then
                        If concepts.Any(Function(c) Not c.Value.ToString().Equals("0")) Then concepts = concepts.Where(Function(c) Not c.Value.ToString().Equals("0")).OrderBy(Function(x) x.Code)
                    End If

                    For Each concept In concepts
                        Dim processingForConcept = GetProcessingForConcept(concept)

                        If invokedForReportingPurposes Then
                            processingForConcept.SetAttributeValue("idfirstfact", fact.Id)
                            processingForConcept.SetAttributeValue("conceptcode", concept.Code)
                        End If

                        If processing IsNot Nothing OrElse processingForFact IsNot Nothing Then
                            processingIf.Add(processingForConcept)
                        Else
                            processingIf = processingForConcept
                        End If
                    Next

                    If processing Is Nothing AndAlso processingForFact IsNot Nothing Then processing = <responseCondition></responseCondition>
                    If processing IsNot Nothing Then
                        processing.Add(processingIf)
                    Else
                        processing = processingIf
                    End If

                    idx += 1

                Next
                If processing IsNot Nothing Then responseProcessing.Add(processing)
            Next

            Return responseProcessing
        End Function

        Private Function GetProcessingForGroup(group As IEnumerable(Of KeyFactSet), invokedForReportingPurposes As Boolean) As XElement
            Dim processingForGroup As XElement = Nothing
            Dim idx As Integer = 1

            Dim factSetsWithoutCatchAll As IEnumerable(Of KeyFactSet) = group.Where(Function(kfs) kfs.Facts.All(Function(x) DirectCast(x.Values.First(), ConceptValue).Values.All(Function(bv) Not TypeOf bv Is CatchAllValue)))
            Dim factSetsWithCatchAll As IEnumerable(Of KeyFactSet) = group.Where(Function(kfs) kfs.Facts.All(Function(x) DirectCast(x.Values.First(), ConceptValue).Values.All(Function(bv) TypeOf bv Is CatchAllValue)))
            Dim catchAllFactSetWithNonZeroValuesExists As Boolean = factSetsWithCatchAll.Any(Function(fs) CheckCatchAllFactSetWithNonZeroValuesExists(fs))

            For Each factSet As KeyFactSet In factSetsWithoutCatchAll.OrderByDescending(Function(x) x.Facts(0).Id)
                Dim processingForFactSet As XElement = GetProcessingForFactSet(factSet, group.Count(), idx, catchAllFactSetWithNonZeroValuesExists, invokedForReportingPurposes)
                If Not processingForFactSet Is Nothing Then
                    If processingForGroup Is Nothing Then processingForGroup = <responseCondition></responseCondition>
                    processingForGroup.Add(processingForFactSet)
                    idx += 1
                End If
            Next

            If factSetsWithCatchAll.Any() Then
                Dim catchAllFactSet As KeyFactSet = factSetsWithCatchAll.First()
                Dim processingForCatchAllFactSet As XElement = GetProcessingForFactSet(catchAllFactSet, group.Count(), group.Count(), False, invokedForReportingPurposes)
                If Not processingForCatchAllFactSet Is Nothing Then
                    If processingForGroup IsNot Nothing Then
                        processingForGroup.Add(processingForCatchAllFactSet)
                    Else
                        processingForGroup = XElement.Parse(processingForCatchAllFactSet.Elements("setOutcomeValue").First.ToXmlDocument.OuterXml)
                    End If
                End If
            End If

            Return processingForGroup
        End Function

        Private Function GetProcessingForFactSet(factSet As KeyFactSet, factSetsCount As Integer, idx As Integer, addWithZeroValues As Boolean, invokedForReportingPurposes As Boolean) As XElement

            Dim processingIf As XElement = Nothing
            Dim addProcessingPart As Boolean = DirectCast(factSet, ConceptFactsSet).Concepts.Count > 0

            If Not invokedForReportingPurposes Then
                If addProcessingPart AndAlso Not addWithZeroValues Then addProcessingPart = DirectCast(factSet, ConceptFactsSet).Concepts.Any(Function(c) Not c.Value.ToString().Equals("0"))
            End If

            If Not addProcessingPart Then Return processingIf

            processingIf = GetIfElement(idx, factSetsCount, factSet.Facts(0).Id)
            Dim processingForFactSet As XElement = Nothing

            Dim sortedFacts = factSet.Facts.OrderBy(Function(f) _responseTypeFactory.GetConceptIndexByIdentifier(_responseTypeFactory.GetFactIdentifier(f)))
            For Each fact As KeyFact In sortedFacts
                Dim processingForFact = _responseTypeFactory.CreateConceptResponseProcessingTypeByFact(fact).GetProcessingForFact(fact)
                If processingForFact IsNot Nothing Then
                    If processingForFactSet Is Nothing Then processingForFactSet = <and></and>
                    processingForFactSet.Add(processingForFact)
                End If
            Next

            If processingForFactSet IsNot Nothing Then processingIf.Add(processingForFactSet)

            Dim concepts = DirectCast(factSet, ConceptFactsSet).Concepts.OrderBy(Function(x) x.Code)
            If Not invokedForReportingPurposes Then
                If concepts.Any(Function(c) Not c.Value.ToString().Equals("0")) Then concepts = concepts.Where(Function(c) Not c.Value.ToString().Equals("0")).OrderBy(Function(x) x.Code)
            End If
            For Each concept In concepts
                Dim processingForConcept = GetProcessingForConcept(concept)

                If invokedForReportingPurposes Then
                    If factSet.Facts IsNot Nothing AndAlso factSet.Facts.Any() Then
                        processingForConcept.SetAttributeValue("idfirstfact", factSet.Facts.First.Id)
                        processingForConcept.SetAttributeValue("conceptcode", concept.Code)
                    End If
                End If
                processingIf.Add(processingForConcept)
            Next


            Return processingIf
        End Function

        Private Function GetProcessingForConcept(concept As Concept) As XElement

            Dim identifier As String = QTIScoringHelper.EncodeAsQTIIdentifier(QTIScoringHelper.GetConceptScoringId(_conceptScoringIndex, concept.Code), "-")
            Dim value As String = concept.Value.ToString()

            Dim processingForConcept As XElement =
                <setOutcomeValue identifier=<%= identifier %>>
                    <baseValue baseType="float"><%= value %></baseValue>
                </setOutcomeValue>

            Return processingForConcept
        End Function

        Private Function GetIfElement(index As Integer, nrOfFactIds As Integer, factId As String) As XElement
            If index = nrOfFactIds AndAlso factId.Contains("[*]") Then
                Return <responseElse></responseElse>
            ElseIf index = 1 Then
                Return <responseIf></responseIf>
            Else
                Return <responseElseIf></responseElseIf>
            End If
        End Function

        Private Function GetConceptIndexForFindingFactsByDomain(domain As String) As Integer
            Dim result As Integer = _responseTypeFactory.GetConceptIndexByIdentifier(domain)
            Debug.Assert(result > 0, "Processing conceptfacts on finding - Index for concept-encoding should not be 0")
            If _groups.Count > 0 Then
                If _conceptIndexForFindingFacts Is Nothing OrElse _conceptIndexForFindingFacts.Count = 0 Then
                    _conceptIndexForFindingFacts = _responseTypeFactory.CollectionOfConceptIndexPerIdentifier

                    Dim newConceptIndexForFindingFacts As New Dictionary(Of String, Integer)
                    Dim idx As Integer = _groups.Count + 1

                    For Each i In _conceptIndexForFindingFacts.Values.GroupBy(Function(v) v)
                        Dim addToNewConceptIndex As Boolean = True
                        For Each kvp As KeyValuePair(Of String, Integer) In _conceptIndexForFindingFacts.Where(Function(x) x.Value = i.Key)
                            If CheckIdentifierIsInFactSets(kvp.Key) Then
                                addToNewConceptIndex = False
                            End If
                        Next
                        If addToNewConceptIndex Then
                            _conceptIndexForFindingFacts.Where(Function(x) x.Value = i.Key).ToList.ForEach(Sub(ff)
                                                                                                               newConceptIndexForFindingFacts.Add(ff.Key, idx)
                                                                                                           End Sub)
                            idx += 1
                        End If
                    Next

                    _conceptIndexForFindingFacts = newConceptIndexForFindingFacts
                End If
                If _conceptIndexForFindingFacts.ContainsKey(domain) Then result = _conceptIndexForFindingFacts(domain)
            End If
            Return result
        End Function

        Private Function CheckIdentifierIsInFactSets(domain As String) As Boolean
            Return _groups.Any(Function(g) g.Any(Function(kfs) kfs.Facts.Any(Function(f) _responseTypeFactory.GetFactIdentifier(f).Equals(domain))))
        End Function

        Private Function CheckCatchAllFactWithNonZeroValuesExists(facts As List(Of BaseFact)) As Boolean
            For Each fact As KeyFact In facts
                If DirectCast(fact, ConceptFact).Values.All(Function(c) DirectCast(c, ConceptValue).Values.All(Function(cv) TypeOf cv Is CatchAllValue)) Then
                    If DirectCast(fact, ConceptFact).Concepts.Any(Function(c) Not c.Value.ToString().Equals("0")) Then Return True
                End If
            Next
            Return False
        End Function

        Private Function CheckCatchAllFactSetWithNonZeroValuesExists(factSet As KeyFactSet) As Boolean
            For Each fact As KeyFact In factSet.Facts
                If DirectCast(fact, ConceptFact).Values.All(Function(c) DirectCast(c, ConceptValue).Values.All(Function(cv) TypeOf cv Is CatchAllValue)) Then
                    If DirectCast(factSet, ConceptFactsSet).Concepts.Any(Function(c) Not c.Value.ToString().Equals("0")) Then Return True
                End If
            Next
            Return False
        End Function

    End Class

End Namespace
