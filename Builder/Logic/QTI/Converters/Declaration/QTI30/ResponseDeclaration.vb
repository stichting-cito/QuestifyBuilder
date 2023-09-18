Imports System.Linq
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Facade.Factories.QTI30
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Converters.Declaration.QTI30

    Public Class ResponseDeclaration

        Private ReadOnly _responseIdentifierAttributeList As XmlNodeList
        Private ReadOnly _solution As Solution
        Private ReadOnly _scoringParams As HashSet(Of ScoringParameter)
        Private _finding As KeyFinding
        Private _responseTypeFactory As ResponseTypeFactory = Nothing
        Private _nrOfPartsInComposedInteraction As Integer = 1
        Private _indexOfSubPartInComposedInteraction As Integer = 1
        Private _indexOfProcessedResponseInteraction As Integer = 1
        Private _indexForScoringDetermination As Integer = 1

        Private Enum ResponseValueType
            CorrectReponse
            DefaultValue
        End Enum

        Public Sub New(responseIdentifierAttributeList As XmlNodeList, solution As Solution, scoringParams As HashSet(Of ScoringParameter))
            _responseIdentifierAttributeList = responseIdentifierAttributeList
            _solution = solution
            _scoringParams = scoringParams
        End Sub

        Public Function GetDeclarations() As List(Of ResponseDeclarationType)
            Dim list As New List(Of ResponseDeclarationType)()
            Dim shouldUseResponseProcessingTemplate = QTI30CombinedScoringHelper.ShouldUseResponseProcessingTemplate(_solution, _scoringParams, _responseIdentifierAttributeList)

            For Each responseIdentifierAttribute As XmlNode In _responseIdentifierAttributeList
                Dim addEmptyDeclaration = True
                _finding = QTI30CombinedScoringHelper.GetFindingByResponseIdentifier(_solution, responseIdentifierAttribute.Value)
                InstantiateResponseTypeFactory()
                Dim keyValue = GetKeyValueForResponse(responseIdentifierAttribute)
                DetermineIfResponseIsPartOfComposedInteraction(responseIdentifierAttribute, keyValue)
                If FindingContainsFactsToProcess(_finding) Then
                    Dim groups = GetGroups(responseIdentifierAttribute, _finding).ToList
                    Dim responseIsInFactSet As Boolean = groups.Any()
                    Dim shouldAddToCorrectResponse = True
                    Dim facts As List(Of BaseFact) = GetFactsForResponse(responseIdentifierAttribute, groups, shouldAddToCorrectResponse)
                    If facts.Any OrElse responseIsInFactSet Then
                        addEmptyDeclaration = False

                        Dim interPretationValue As String = String.Empty
                        Dim responseIsFirstInteractionInFactSet As Boolean = DetermineIfResponseIsFirstInteractionInFactSet(responseIdentifierAttribute.Value, groups)
                        Dim addResponseDeclarationMappings = shouldUseResponseProcessingTemplate AndAlso QTI30CombinedScoringHelper.ShouldAddResponseDeclarationMappingsForResponseProcessingTemplateUsage(_finding, _scoringParams)

                        If (Not responseIsInFactSet AndAlso Not _indexOfSubPartInComposedInteraction > 1) OrElse (responseIsInFactSet AndAlso responseIsFirstInteractionInFactSet) Then
                            interPretationValue = GetInterpretationValue(responseIdentifierAttribute, groups)
                        End If
                        AddResponseDeclaration(responseIdentifierAttribute,
                                               GetResponseValues(responseIdentifierAttribute, ResponseValueType.CorrectReponse, facts, shouldAddToCorrectResponse),
                                               GetResponseValues(responseIdentifierAttribute, ResponseValueType.DefaultValue, facts, shouldAddToCorrectResponse),
                                               If(addResponseDeclarationMappings, GetMappings(facts), Nothing),
                                               GetAreaMappings(responseIdentifierAttribute, facts),
                                               interPretationValue, list, keyValue)
                    End If
                End If
                If addEmptyDeclaration Then
                    AddEmptyResponseDeclaration(responseIdentifierAttribute, list)
                End If
                SetIndexes()
            Next
            Return list
        End Function

        Private Sub AddEmptyResponseDeclaration(ByVal responseIdentifierAttribute As XmlNode, ByRef list As List(Of ResponseDeclarationType))
            Dim responseDeclarationType As New ResponseDeclarationType
            responseDeclarationType.identifier = ScoringHelper.GetResponseId(GetResponseIndex(responseIdentifierAttribute.Value))
            responseDeclarationType.cardinality = ScoringHelper.GetResponseDeclarationCardinality(responseIdentifierAttribute)
            responseDeclarationType.basetype = ResponseDeclarationTypeBasetype.string
            responseDeclarationType.basetypeSpecified = True
            list.Add(responseDeclarationType)
        End Sub

        Private Sub AddResponseDeclaration(ByVal responseIdentifierAttribute As XmlNode, ByVal correctResponseValues() As ValueType, ByVal defaultValues() As ValueType,
                                           ByVal mappings() As MapEntryType, ByVal areaMappings() As AreaMapEntryType, ByVal interPretationValue As String,
                                           ByRef list As List(Of ResponseDeclarationType), keyValue As KeyValue)
            Dim responseDeclarationType As New ResponseDeclarationType
            responseDeclarationType.identifier = ScoringHelper.GetResponseId(GetResponseIndex(responseIdentifierAttribute.Value))
            responseDeclarationType.cardinality = ScoringHelper.GetResponseDeclarationCardinality(responseIdentifierAttribute)

            If correctResponseValues IsNot Nothing OrElse Not String.IsNullOrEmpty(interPretationValue) Then
                responseDeclarationType.qticorrectresponse = New CorrectResponseType
                If Not String.IsNullOrEmpty(interPretationValue) Then responseDeclarationType.qticorrectresponse.interpretation = interPretationValue
                If correctResponseValues IsNot Nothing Then responseDeclarationType.qticorrectresponse.qtivalue = correctResponseValues
            End If

            If defaultValues IsNot Nothing Then
                responseDeclarationType.qtidefaultvalue = New DefaultValueType
                responseDeclarationType.qtidefaultvalue.qtivalue = defaultValues
            End If

            If areaMappings IsNot Nothing Then
                responseDeclarationType.qtiareamapping = New AreaMappingType()
                responseDeclarationType.qtiareamapping.qtiareamapentry = areaMappings
            End If

            If mappings IsNot Nothing Then
                responseDeclarationType.qtimapping = New MappingType()
                responseDeclarationType.qtimapping.defaultvalue = 0
                responseDeclarationType.qtimapping.qtimapentry = mappings
            End If

            responseDeclarationType.basetype = ScoringHelper.GetResponseDeclarationBaseType(responseIdentifierAttribute, GetResponseTypeFactory().GetScoringParamFromFactIdsPerScoringParameterById(responseIdentifierAttribute.Value), _finding, keyValue)
            responseDeclarationType.basetypeSpecified = True
            list.Add(responseDeclarationType)
        End Sub

        Private Function GetResponseValues(responseIdentifierAttribute As XmlNode, responseValueType As ResponseValueType, facts As List(Of BaseFact), shouldAddToCorrectResponse As Boolean) As ValueType()
            If facts Is Nothing OrElse facts.Count = 0 Then
                Return Nothing
            End If

            Dim processedFacts = GetFactsToProcess(responseIdentifierAttribute, facts)

            Dim responseValuesFound As Boolean = False
            Dim responseValues As New List(Of ValueType)

            For x As Integer = 0 To processedFacts.Count - 1
                If processedFacts(x).Values IsNot Nothing AndAlso DirectCast(processedFacts(x).Values(0), KeyValue).Values IsNot Nothing AndAlso Not TypeOf DirectCast(processedFacts(x).Values(0), KeyValue).Values(0) Is NoValue Then
                    Dim responseValue As ValueType() = Nothing
                    If responseValueType = ResponseDeclaration.ResponseValueType.CorrectReponse AndAlso shouldAddToCorrectResponse Then
                        responseValue = GetCorrectResponseValue(CType(processedFacts(x), KeyFact))
                    ElseIf responseValueType = ResponseDeclaration.ResponseValueType.DefaultValue AndAlso ShouldAddDefaultValue(responseIdentifierAttribute) Then
                        responseValue = New ValueType() {GetResponseDefaultValue(CType(processedFacts(x), KeyFact), responseIdentifierAttribute)}
                    End If
                    If responseValue IsNot Nothing AndAlso responseValue.Length > 0 Then
                        responseValuesFound = True
                        responseValues.AddRange(responseValue)
                    End If
                End If
            Next
            If Not responseValuesFound Then Return Nothing
            Return responseValues.ToArray()
        End Function

        Private Function GetFactsToProcess(responseIdentifierAttribute As XmlNode, facts As List(Of BaseFact)) As List(Of BaseFact)
            If QTI30CombinedScoringHelper.IsMultiChoiceScoring(responseIdentifierAttribute, _scoringParams) Then
                facts.RemoveRange(1, facts.Count() - 1)
            End If
            Return facts
        End Function

        Private Function GetMappings(facts As List(Of BaseFact)) As MapEntryType()
            Dim mappingsFound As Boolean = False
            Dim mappings As New List(Of MapEntryType)

            facts.ForEach(Sub(f)
                              Dim kf As KeyFact = CType(f, KeyFact)
                              If kf IsNot Nothing AndAlso kf.Values IsNot Nothing Then
                                  Dim kv As KeyValue = DirectCast(kf.Values(0), KeyValue)

                                  If kv IsNot Nothing AndAlso kv.Values IsNot Nothing AndAlso Not TypeOf kv.Values(0) Is NoValue Then
                                      Dim mappingsPerFact = GetResponseTypeFactory.CreateResponseDeclarationTypeByFact(kf).GetMappings(kf)
                                      If mappingsPerFact IsNot Nothing AndAlso mappingsPerFact.Any() Then
                                          mappingsFound = True
                                          mappings.AddRange(mappingsPerFact)
                                      End If
                                  End If
                              End If
                          End Sub)

            If Not mappingsFound Then Return Nothing
            Return mappings.ToArray()
        End Function

        Private Function GetAreaMappings(responseIdentifierAttribute As XmlNode, facts As List(Of BaseFact)) As AreaMapEntryType()
            Dim areaMappingsFound As Boolean = False
            Dim areaMappings As New List(Of AreaMapEntryType)

            If ShouldAddAreaMapping(responseIdentifierAttribute) Then
                For x As Integer = 0 To facts.Count - 1
                    If facts(x).Values IsNot Nothing AndAlso DirectCast(facts(x).Values(0), KeyValue).Values IsNot Nothing AndAlso Not TypeOf DirectCast(facts(x).Values(0), KeyValue).Values(0) Is NoValue Then
                        Dim areaMapping = New AreaMapEntryType() {GetSingleAreaMappingForFact(CType(facts(x), KeyFact))}
                        If areaMapping IsNot Nothing AndAlso areaMapping.Length > 0 Then
                            areaMappingsFound = True
                            areaMappings.AddRange(areaMapping)
                        End If
                    End If
                Next
            End If

            If Not areaMappingsFound Then Return Nothing
            Return areaMappings.ToArray()
        End Function

        Private Function GetCorrectResponseValue(fact As KeyFact) As ValueType()
            Dim responseDeclarationTypeByFact = GetResponseTypeFactory.CreateResponseDeclarationTypeByFact(fact)
            If TypeOf responseDeclarationTypeByFact Is ResponseDeclarationInput Then
                Dim typedResponseDeclaration = CType(responseDeclarationTypeByFact, ResponseDeclarationInput)
                If typedResponseDeclaration.IsEvaluateComparisonType(fact) Then
                    Return CType(responseDeclarationTypeByFact, ResponseDeclarationInput).GetCorrectResponseValuesForFact(fact).ToArray
                Else
                    Return New ValueType() {responseDeclarationTypeByFact.GetSingleCorrectResponseValueForFact(fact, _indexOfSubPartInComposedInteraction)}
                End If
            Else
                Return New ValueType() {responseDeclarationTypeByFact.GetSingleCorrectResponseValueForFact(fact, _indexOfSubPartInComposedInteraction)}
            End If
        End Function

        Private Function GetResponseDefaultValue(fact As KeyFact, responseIdentifierAttribute As XmlNode) As ValueType
            Return GetResponseTypeFactory.CreateResponseDeclarationTypeByFact(fact).GetResponseDefaultValue(fact, responseIdentifierAttribute)
        End Function

        Private Function GetSingleAreaMappingForFact(fact As KeyFact) As AreaMapEntryType
            Return GetResponseTypeFactory.CreateResponseDeclarationTypeByFact(fact).GetSingleAreaMappingForFact(fact)
        End Function

        Private Function GetInterpretationValue(responseIdentifierAttribute As XmlNode, groups As IEnumerable(Of IEnumerable(Of KeyFactSet))) As String
            Dim result As String = String.Empty
            Dim values As New Dictionary(Of Double, String)

            If groups.Any Then
                GetProcessingForGroups(groups, values)
            End If

            Dim factsOnFinding = GetFactsOnFinding(responseIdentifierAttribute, _finding)
            GetProcessingForFindingFacts(factsOnFinding, values)

            For Each value As KeyValuePair(Of Double, String) In values.OrderBy(Function(kvp) kvp.Key)
                If Not String.IsNullOrEmpty(value.Value) Then
                    If Not String.IsNullOrEmpty(result) Then result += InterpretationValueSeparator(responseIdentifierAttribute)
                    result += value.Value
                End If
            Next
            Return result
        End Function

        Private Sub GetProcessingForGroups(groups As IEnumerable(Of IEnumerable(Of KeyFactSet)), ByRef results As Dictionary(Of Double, String))
            For Each group In groups
                Dim valueForGroup As String = GetProcessingForGroup(group)
                If Not String.IsNullOrEmpty(valueForGroup) Then
                    Dim minIndexForGroup As Double = GetOrderIndex(CType(group, List(Of KeyFactSet)).First.Facts.OrderBy(Function(f) GetOrderIndex(f)).First)
                    results.Add(minIndexForGroup, valueForGroup)
                End If
            Next
        End Sub

        Private Function GetProcessingForGroup(group As IEnumerable(Of KeyFactSet)) As String
            Dim processingForGroup As String = String.Empty

            For Each factSet As KeyFactSet In group
                Dim processingForFactSet As String = GetProcessingForFactSetFacts(GetOrderedList(factSet.Facts))
                If Not String.IsNullOrEmpty(processingForFactSet) Then
                    If Not String.IsNullOrEmpty(processingForGroup) Then processingForGroup += "|"
                    processingForGroup = String.Concat(processingForGroup, processingForFactSet)
                End If
            Next

            Return processingForGroup
        End Function

        Private Sub GetProcessingForFindingFacts(facts As List(Of BaseFact), ByRef results As Dictionary(Of Double, String))
            For Each fact As KeyFact In facts
                Dim processingForFact As String = GetResponseTypeFactory.CreateResponseDeclarationTypeByFact(fact).GetInterpretationValueForFact(fact)
                If Not String.IsNullOrEmpty(processingForFact) Then
                    Dim indexForFact As Double = GetOrderIndex(fact)
                    Debug.Assert(Not (results.ContainsKey(indexForFact)), "Ordering index for keyfact already in dictionary. Should not occur.")
                    If Not results.ContainsKey(indexForFact) Then results.Add(indexForFact, processingForFact)
                End If
            Next
        End Sub

        Private Function GetProcessingForFactSetFacts(facts As List(Of BaseFact)) As String
            Dim processingForFacts As String = String.Empty
            Dim multipleCorrectFactsInFactSets As Boolean = False

            For Each fact As KeyFact In facts
                Dim processingForFact As String = GetResponseTypeFactory.CreateResponseDeclarationTypeByFact(fact).GetInterpretationValueForFact(fact)
                If Not String.IsNullOrEmpty(processingForFact) Then
                    If facts.Count > 1 AndAlso Not String.IsNullOrEmpty(processingForFacts) Then
                        processingForFacts += "&"c
                        multipleCorrectFactsInFactSets = True
                    End If
                    processingForFacts += processingForFact
                End If
            Next
            If multipleCorrectFactsInFactSets Then processingForFacts = $"({processingForFacts})"
            Return processingForFacts
        End Function

        Private Function GetOrderedList(list As List(Of BaseFact)) As List(Of BaseFact)
            Dim sortedFacts = list.OrderBy(Function(f) GetOrderIndex(f))
            If _scoringParams IsNot Nothing AndAlso _scoringParams.Count > 0 Then
                sortedFacts = sortedFacts.OrderBy(Function(kf) GetResponseTypeFactory.GetOrderIndexByKeyValue(kf)).ThenBy(Function(kf) GetOrderIndex(kf))
            End If
            Return sortedFacts.ToList
        End Function

        Private Function GetGroups(responseIdentifierAttribute As XmlNode, finding As KeyFinding) As IEnumerable(Of IEnumerable(Of KeyFactSet))
            Dim groups = New List(Of List(Of KeyFactSet))()
            For Each factSet In GetFactsInFactSets(responseIdentifierAttribute, finding)
                Dim group As List(Of KeyFactSet) = ResponseHelper.GetGroup(groups, factSet)
                If Not group.Any() Then groups.Add(group)
                group.Add(factSet)
            Next
            Dim checkIdx As Integer = GetScoreResponseIndex(responseIdentifierAttribute.Value)
            Return groups.Where(Function(g) g.Any(Function(kfs) kfs.Facts.Any(Function(f) f.Values IsNot Nothing AndAlso f.Values.Count > 0 AndAlso GetResponseTypeFactory().GetResponseIndexByIdentifier(f.Values(0).Domain) = checkIdx)))
        End Function

        Private Sub InstantiateResponseTypeFactory()
            If _scoringParams IsNot Nothing AndAlso _scoringParams.Count > 0 AndAlso _responseTypeFactory Is Nothing Then _responseTypeFactory = New ResponseTypeFactory(_scoringParams, _finding, _responseIdentifierAttributeList)
            If _scoringParams Is Nothing AndAlso _finding IsNot Nothing Then _responseTypeFactory = New ResponseTypeFactory(_solution, _finding, _responseIdentifierAttributeList)
            If _scoringParams Is Nothing AndAlso _finding Is Nothing AndAlso _responseTypeFactory Is Nothing Then _responseTypeFactory = New ResponseTypeFactory(_solution, _finding, _responseIdentifierAttributeList)
        End Sub

        Private Sub SetIndexes()
            If _nrOfPartsInComposedInteraction > 1 Then _indexOfSubPartInComposedInteraction += 1
            If _indexOfSubPartInComposedInteraction > _nrOfPartsInComposedInteraction Then
                _nrOfPartsInComposedInteraction = 1
                _indexOfSubPartInComposedInteraction = 1
            End If
            _indexOfProcessedResponseInteraction += 1
            _indexForScoringDetermination += (If(_nrOfPartsInComposedInteraction > 1, 0, 1))
        End Sub

        Private Function GetResponseIndex(ByVal identifier As String) As Integer
            Dim idx As Integer = GetResponseTypeFactory.GetResponseIndexByIdentifier(identifier)
            If idx > 0 AndAlso _indexOfSubPartInComposedInteraction > 1 Then idx += (_indexOfSubPartInComposedInteraction - 1)
            If idx = 0 Then idx = _indexOfProcessedResponseInteraction
            Return idx
        End Function

        Private Function GetScoreResponseIndex(ByVal identifier As String) As Integer
            Dim idx As Integer = GetResponseTypeFactory.GetResponseIndexByIdentifier(identifier)
            If idx = 0 Then idx = _indexOfProcessedResponseInteraction
            Return idx
        End Function

        Private Function GetOrderIndex(ByVal fact As BaseFact) As Double
            Dim idx As Double = GetResponseTypeFactory.GetOrderIndexByKeyValue(fact)
            If idx > 0 AndAlso fact.Id IsNot Nothing Then idx += (GetResponseTypeFactory.GetOrderIndexByIdentifier(fact.Id) / 100)
            If idx = 0 Then idx = GetOrderIndexForOlderItemsFromKeyValues(fact)
            If idx = 0 AndAlso fact.Id IsNot Nothing Then idx = GetResponseTypeFactory.GetOrderIndexByIdentifier(fact.Id)
            If idx = 0 Then idx = GetOrderIndex(fact.Values(0).Domain)
            Return idx
        End Function

        Private Function GetOrderIndexForOlderItemsFromKeyValues(fact As BaseFact) As Integer
            If _scoringParams Is Nothing AndAlso fact.Id Is Nothing AndAlso fact.Values IsNot Nothing AndAlso fact.Values.Count > 0 Then
                Dim keyValue As KeyValue = DirectCast(fact.Values.First, KeyValue)
                If keyValue.Values IsNot Nothing AndAlso keyValue.Values.Count > 0 Then
                    Dim baseValue As BaseValue = DirectCast(keyValue.Values.First, BaseValue)
                    If TypeOf baseValue Is StringValue AndAlso baseValue.ToString.Length = 1 Then
                        Dim idx As Integer = 0
                        If Integer.TryParse(CStr(AscW(baseValue.ToString)), idx) Then
                            If (idx - 64) > 0 Then Return (idx - 64)
                        End If
                    End If
                End If
            End If
            Return 0
        End Function

        Private Function GetOrderIndex(identifier As String) As Double
            Dim idx As Double = GetResponseTypeFactory.GetOrderIndexByIdentifier(identifier)
            If idx = 0 Then idx = _indexOfProcessedResponseInteraction
            Return idx
        End Function

        Private Function GetIndexForScoringDetermination() As Integer
            Return _indexForScoringDetermination
        End Function

        Private Function GetResponseTypeFactory() As ResponseTypeFactory
            If _responseTypeFactory Is Nothing Then InstantiateResponseTypeFactory()
            Return _responseTypeFactory
        End Function

        Private Function DetermineIfResponseIsFirstInteractionInFactSet(responseIdentifier As String, groups As IEnumerable(Of IEnumerable(Of KeyFactSet))) As Boolean
            If Not groups.Any() Then Return False
            Dim indexForResponse As Double = GetOrderIndex(responseIdentifier)
            If indexForResponse > 0 AndAlso _indexOfSubPartInComposedInteraction > 1 Then indexForResponse += (_indexOfSubPartInComposedInteraction - 1)
            Dim minIndexForFactSet As Double = GetOrderIndex(CType(groups.First, List(Of KeyFactSet)).First.Facts.OrderBy(Function(f) GetOrderIndex(f)).First)
            Return (indexForResponse <= minIndexForFactSet)
        End Function

        Private Function GetKeyValueForResponse(responseIdentifierAttribute As XmlNode) As KeyValue
            Dim keyvalue As KeyValue = QTI30CombinedScoringHelper.GetKeyValueByResponseIdentifier(_finding, responseIdentifierAttribute.Value)
            If keyvalue Is Nothing Then keyvalue = QTI30CombinedScoringHelper.GetKeyValueForInputByIndex(_finding, GetIndexForScoringDetermination())
            Return keyvalue
        End Function

        Private Function GetFactsForResponse(responseIdentifierAttribute As XmlNode, groups As IEnumerable(Of IEnumerable(Of KeyFactSet)), ByRef shouldAddToCorrectResponse As Boolean) As List(Of BaseFact)
            Dim result As New List(Of BaseFact)
            Dim facts As New Dictionary(Of Double, BaseFact)

            If groups.Any Then
                Dim checkIdx As Integer = GetScoreResponseIndex(responseIdentifierAttribute.Value)
                For Each group As List(Of KeyFactSet) In groups
                    Dim factSetIdx As Integer = 0
                    For Each factSet As KeyFactSet In group
                        For Each fact As KeyFact In factSet.Facts.Where(Function(f) f.Values.Any(Function(v) GetResponseTypeFactory().GetResponseIndexByIdentifier(v.Domain) = checkIdx))
                            facts.Add(GetOrderIndex(fact), fact)
                        Next

                        If facts.Count > 0 Then
                            shouldAddToCorrectResponse = (factSetIdx = 0)
                            Exit For
                        End If
                        factSetIdx += 1
                    Next
                Next
                If facts.Count = 0 Then
                    For Each group As List(Of KeyFactSet) In groups
                        For Each fact As KeyFact In group.First.Facts
                            facts.Add(GetOrderIndex(fact), fact)
                        Next
                    Next
                End If
            End If
            For Each findingFact As KeyFact In GetFactsOnFinding(responseIdentifierAttribute, _finding)
                facts.Add(GetOrderIndex(findingFact), findingFact)
            Next

            For Each responseFact As KeyValuePair(Of Double, BaseFact) In facts.OrderBy(Function(kvp) kvp.Key)
                result.Add(responseFact.Value)
            Next
            Return result
        End Function

        Private Function GetFactsOnFinding(responseIdentifierAttribute As XmlNode, finding As KeyFinding) As List(Of BaseFact)
            Dim factsOnFinding As New List(Of BaseFact)

            factsOnFinding = QTI30CombinedScoringHelper.GetFactsForInputByIndex(finding, _indexForScoringDetermination)
            If factsOnFinding IsNot Nothing AndAlso factsOnFinding.Count > 0 Then Return factsOnFinding

            Dim checkIdx As Integer = GetScoreResponseIndex(responseIdentifierAttribute.Value)
            Dim factQuery = (From fact As BaseFact In finding.Facts
                             Where finding.Id.ToLower() = responseIdentifierAttribute.Value.ToLower()
                             Select fact).Distinct().Where(Function(f) f.Values IsNot Nothing AndAlso f.Values.Count > 0 AndAlso GetResponseTypeFactory().GetResponseIndexByIdentifier(f.Values(0).Domain) = checkIdx)
            If factQuery IsNot Nothing AndAlso factQuery.ToList.Count > 0 Then Return factQuery.ToList

            factsOnFinding = finding.Facts.Where(Function(f) f.Values.Any(Function(v) GetIdentifierFromValue(v) = responseIdentifierAttribute.Value)).ToList

            Return factsOnFinding
        End Function

        Private Function GetFactsInFactSets(responseIdentifierAttribute As XmlNode, finding As KeyFinding) As List(Of KeyFactSet)
            Dim factsInFactSets As New List(Of KeyFactSet)
            If finding IsNot Nothing Then
                Dim factQuery = (From factSet As KeyFactSet In finding.KeyFactsets.Where(Function(fs) fs.Facts IsNot Nothing AndAlso fs.Facts.Any())
                                 Where finding.Id.ToLower() = responseIdentifierAttribute.Value.ToLower()
                                 Select factSet)
                If factQuery IsNot Nothing AndAlso factQuery.ToList.Count > 0 Then
                    factsInFactSets = factQuery.ToList
                Else
                    factsInFactSets = finding.KeyFactsets
                End If
            End If
            Return factsInFactSets
        End Function


        Private Function GetIdentifierFromValue(value As BaseFactValue) As String
            If value IsNot Nothing AndAlso value.Domain IsNot Nothing AndAlso Not String.IsNullOrEmpty(value.Domain) Then Return GetIdentifier(value.Domain)
            Return String.Empty
        End Function

        Private Function GetIdentifier(input As String) As String
            If input.IndexOf("-") = -1 Then Return input
            Dim guidPart As String = QTI30CombinedScoringHelper.GetGuidPartOfIdentifier(input)
            If Not String.IsNullOrEmpty(guidPart) Then Return guidPart
            Dim pos As Integer = input.LastIndexOf("-")
            Return input.Substring(pos + 1, Len(input) - (pos + 1))
        End Function

        Private Sub DetermineIfResponseIsPartOfComposedInteraction(responseIdentifierAttribute As XmlNode, keyvalue As KeyValue)
            If _nrOfPartsInComposedInteraction > 1 AndAlso _indexOfSubPartInComposedInteraction <= _nrOfPartsInComposedInteraction Then Exit Sub
            If QTI30CombinedScoringHelper.DetermineControlType(DirectCast(responseIdentifierAttribute, XmlAttribute), _scoringParams) = QTI30CombinedScoringHelper.EnumControlType.Input Then
                If Not keyvalue Is Nothing AndAlso ScoringHelper.IsTimeValue(responseIdentifierAttribute, keyvalue) Then
                    _nrOfPartsInComposedInteraction = GapTimeScoringHelper.NrOfTimeParts(keyvalue.Values)
                    Exit Sub
                ElseIf keyvalue IsNot Nothing AndAlso ScoringHelper.IsDateValue(responseIdentifierAttribute, keyvalue) Then
                    _nrOfPartsInComposedInteraction = GapDateScoringHelper.NrOfDateParts(keyvalue.Values)
                    Exit Sub
                ElseIf _scoringParams IsNot Nothing AndAlso _scoringParams.Any() Then
                    If _scoringParams.OfType(Of DateScoringParameter).Any(Function(s) s.InlineId = responseIdentifierAttribute.Value) Then
                        _nrOfPartsInComposedInteraction = 3
                        Exit Sub
                    ElseIf _scoringParams.OfType(Of TimeScoringParameter).Any(Function(s) s.InlineId = responseIdentifierAttribute.Value) Then
                        Dim scoringPrm As TimeScoringParameter = _scoringParams.OfType(Of TimeScoringParameter).First(Function(s) s.InlineId = responseIdentifierAttribute.Value)
                        _nrOfPartsInComposedInteraction = scoringPrm.TimeFormat.Split(":"c).Count()
                        Exit Sub
                    End If
                ElseIf (Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes Is Nothing AndAlso Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("dateSubType") Is Nothing) Then
                    _nrOfPartsInComposedInteraction = 3
                    Exit Sub
                ElseIf (Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes Is Nothing AndAlso Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("timeSubType") Is Nothing) Then
                    _nrOfPartsInComposedInteraction = CInt((DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("timeSubType").Value.Length / 2))
                    Exit Sub
                End If
            End If
            _nrOfPartsInComposedInteraction = 1
            _indexOfSubPartInComposedInteraction = 1
        End Sub

        Private Function ShouldAddDefaultValue(responseIdentifierAttribute As Xml.XmlNode) As Boolean
            If (Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes Is Nothing AndAlso Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("hottextId") Is Nothing) Then
                Return True
            End If
            Return False
        End Function

        Private Function ShouldAddAreaMapping(responseIdentifierAttribute As Xml.XmlNode) As Boolean
            If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Name.ToLower.Equals("qti-select-point-interaction", StringComparison.InvariantCultureIgnoreCase) Then
                Return True
            End If
            Return False
        End Function

        Private Function InterpretationValueSeparator(responseIdentifierAttribute As Xml.XmlNode) As String
            If QTI30CombinedScoringHelper.IsMultiChoiceScoring(responseIdentifierAttribute, _scoringParams) Then
                Return "#"
            End If
            Return "&"
        End Function

        Private Function FindingContainsFactsToProcess(finding As KeyFinding) As Boolean
            If finding IsNot Nothing AndAlso Not finding.Method = EnumScoringMethod.None Then
                If (finding.Facts IsNot Nothing AndAlso FactsContainsValueForProcessing(finding.Facts)) OrElse (finding.KeyFactsets IsNot Nothing AndAlso finding.KeyFactsets.Any(Function(kfs) FactsContainsValueForProcessing(kfs.Facts))) Then Return True
            End If
            Return False
        End Function

        Private Function FactsContainsValueForProcessing(facts As List(Of BaseFact)) As Boolean
            For Each fact As KeyFact In facts
                If (fact.ContainsValueForProcessing) Then
                    Return True
                End If
            Next
            Return False
        End Function

    End Class

End Namespace