Imports System.Globalization
Imports System.Linq
Imports System.Web
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.Xml.XPath
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Helpers.QTI22

    Public Class QTI22ScoringHelper
        Inherits QTIScoringHelper

        Private Const XHTMLNAMESPACE As String = "http://www.w3.org/1999/xhtml"
        Private Const PCINAMESPACE As String = "http://www.imsglobal.org/xsd/portableCustomInteraction_v1"

        Public Shared Function GetGapMatchNoValueResponseProcessing(scoringParameter As ScoringParameter, keyValue As KeyValue, processingForVariable As XElement) As XElement
            Dim processing = <and><not></not></and>

            If TypeOf scoringParameter IsNot GapMatchScoringParameter Then
                Throw New ArgumentException("ScoreParameter not available or not of type 'GapMatchScoringParameter'.", NameOf(scoringParameter))
            End If

            Dim orConstruction = <or></or>

            For Each choice In DirectCast(scoringParameter, GapMatchScoringParameter).Gaps
                Dim subProcessing = <member></member>
                subProcessing.Add(GetDirectPairProcessingForValue(choice.Key, keyValue.Domain))
                subProcessing.Add(processingForVariable)

                orConstruction.Add(subProcessing)
            Next

            processing.Element("not").Add(orConstruction)

            Return processing
        End Function

        Public Shared Function GetGraphicGapMatchNoValueResponseProcessing(
                                                                           scoringParameter As ScoringParameter,
                                                                           keyValue As KeyValue,
                                                                           processingForVariable As XElement) As XElement

            Dim processing = <not></not>

            If TypeOf scoringParameter IsNot GraphGapMatchScoringParameter Then
                Throw New ArgumentException("ScoreParameter not available or not of type 'GraphGapMatchScoringParameter'.", NameOf(scoringParameter))
            End If

            Dim orConstruction = <or></or>
            Dim domain As String = keyValue.Domain

            If domain.IndexOf("-") > 0 Then
                domain = domain.Substring(0, domain.IndexOf("-"))
            End If

            If DirectCast(scoringParameter, GraphGapMatchScoringParameter).IsCategorizationVariant Then
                For Each shape In DirectCast(scoringParameter, GraphGapMatchScoringParameter).Area.ShapeList.OrderBy(Function(s) s.Identifier)
                    Dim subProcessing = <member></member>

                    subProcessing.Add(GetDirectPairProcessingForValue(AlphabeticIdentifierHelper.GetAlphabeticIdentifier(domain),
                                                                      $"HS{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(shape.Identifier)}"))
                    subProcessing.Add(processingForVariable)
                    orConstruction.Add(subProcessing)
                Next
            Else
                For Each gap In DirectCast(scoringParameter, GraphGapMatchScoringParameter).Gaps.OrderBy(Function(g) g.Key)
                    Dim subProcessing = <member></member>

                    subProcessing.Add(GetDirectPairProcessingForValue(AlphabeticIdentifierHelper.GetAlphabeticIdentifier(gap.Key), $"HS{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(domain)}"))
                    subProcessing.Add(processingForVariable)
                    orConstruction.Add(subProcessing)
                Next
            End If

            processing.Add(orConstruction)

            Return processing
        End Function

        Public Shared Function GetDirectPairProcessingForValue(value As String, domain As String) As XElement
            Dim correctValue As XElement = <baseValue baseType="directedPair"><%= $"{value.ToString} {domain}" %></baseValue>

            Return correctValue
        End Function

        Public Shared Function GetResponseDeclarationCardinality(responseIdentifierAttribute As XmlNode) As ResponseDeclarationTypeCardinality
            Dim returnValue = ResponseDeclarationTypeCardinality.single

            If (responseIdentifierAttribute Is Nothing) OrElse (DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement Is Nothing) Then
                Return returnValue
            End If

            Select Case DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Name.ToLower
                Case "orderinteraction"
                    returnValue = ResponseDeclarationTypeCardinality.ordered
                Case "custominteraction"
                    Dim xmlNamespaceManager As New XmlNamespaceManager(responseIdentifierAttribute.OwnerDocument.NameTable)
                    xmlNamespaceManager.AddNamespace("qti", responseIdentifierAttribute.OwnerDocument.NamespaceURI)
                    xmlNamespaceManager.AddNamespace("html", XHTMLNAMESPACE)

                    Dim responseIdXmlAttribute = DirectCast(responseIdentifierAttribute, XmlAttribute)
                    If responseIdXmlAttribute.OwnerElement.HasChildNodes AndAlso
                       responseIdXmlAttribute.OwnerElement.SelectNodes("html:object", xmlNamespaceManager).Count > 0 AndAlso
                       responseIdXmlAttribute.OwnerElement.SelectNodes("html:object", xmlNamespaceManager).Item(0).Attributes("type") IsNot Nothing AndAlso
                       responseIdXmlAttribute.OwnerElement.SelectNodes("html:object", xmlNamespaceManager).Item(0).Attributes("type").Value.Equals("application/vnd.GeoGebra.file") Then
                        Return returnValue
                    End If

                    returnValue = ResponseDeclarationTypeCardinality.ordered
                Case "gapmatchinteraction"
                    If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.SelectNodes("//gap").Count > 1 Then
                        returnValue = ResponseDeclarationTypeCardinality.multiple
                    End If
                Case "choiceinteraction", "hotspotinteraction", "selectpointinteraction", "positionobjectinteraction", "hottextinteraction"
                    If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("maxChoices") IsNot Nothing _
   AndAlso (CType(DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("maxChoices").Value, Integer) = 0 _
            OrElse CType(DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("maxChoices").Value, Integer) > 1) _
    Then
                        returnValue = ResponseDeclarationTypeCardinality.multiple
                    End If
                Case "matchinteraction"
                    If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("maxAssociations") IsNot Nothing _
   AndAlso (CType(DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("maxAssociations").Value, Integer) = 0 _
            OrElse CType(DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("maxAssociations").Value, Integer) > 1) _
    Then
                        returnValue = ResponseDeclarationTypeCardinality.multiple
                    End If
                Case "graphicgapmatchinteraction", "graphicassociateinteraction"
                    If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.SelectNodes("//associableHotspot").Count > 1 Then
                        returnValue = ResponseDeclarationTypeCardinality.multiple
                    End If
                Case "extendedtextinteraction"
                    If DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("isFormulaEditor") IsNot Nothing Then
                        returnValue = ResponseDeclarationTypeCardinality.ordered
                    End If
            End Select


            Return returnValue
        End Function

        Public Shared Function GetResponseDeclarationBaseType(responseIdentifierAttribute As XmlNode, scoringParams As List(Of ScoringParameter), finding As KeyFinding, keyValue As KeyValue) As ResponseDeclarationTypeBaseType
            Dim returnValue = ResponseDeclarationTypeBaseType.string

            If (responseIdentifierAttribute Is Nothing) OrElse (DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement Is Nothing) Then
                Return returnValue
            End If

            Select Case DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Name.ToLower
                Case "textentryinteraction"
                    returnValue = GetBaseType(keyValue.Values(0))

                    If returnValue = ResponseDeclarationTypeBaseType.string _
                       AndAlso (IsTimeValue(responseIdentifierAttribute, keyValue) OrElse IsDateValue(responseIdentifierAttribute, keyValue)) _
                        Then
                        returnValue = ResponseDeclarationTypeBaseType.integer
                    End If

                    If returnValue = ResponseDeclarationTypeBaseType.float Then
                        returnValue = ResponseDeclarationTypeBaseType.string
                    End If
                Case "custominteraction"
                    If scoringParams IsNot Nothing _
   AndAlso scoringParams.Any() _
   AndAlso scoringParams.All(Function(sp) TypeOf sp Is DecimalScoringParameter) _
    Then
                        If Not scoringParams.Any(Function(s) CombinedScoringHelper.GetFormulaItemType(finding, s) = CombinedScoringHelper.FormulaItemType.EvaluateDependency) Then Return ResponseDeclarationTypeBaseType.float
                    End If
                Case "choiceinteraction", "hottextinteraction", "orderinteraction", "inlinechoiceinteraction", "hotspotinteraction"
                    returnValue = ResponseDeclarationTypeBaseType.identifier
                Case "gapmatchinteraction", "graphicgapmatchinteraction", "matchinteraction"
                    returnValue = ResponseDeclarationTypeBaseType.directedPair
                Case "selectpointinteraction", "positionobjectinteraction"
                    returnValue = ResponseDeclarationTypeBaseType.point
                Case "graphicassociateinteraction"
                    returnValue = ResponseDeclarationTypeBaseType.pair
            End Select


            Return returnValue
        End Function

        Friend Shared Sub AddEmptyResponseDeclaration(ByRef itemDocument As XmlDocument, ByRef assessmentItemType As AssessmentItemType)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)
            xmlNamespaceManager.AddNamespace("html", XHTMLNAMESPACE)
            xmlNamespaceManager.AddNamespace("pci", PCINAMESPACE)

            Dim responseIdentifierAttributeList As XmlNodeList = ResponseIdentifierHelper.GetResponseIdentifiers(itemDocument, xmlNamespaceManager)
            Dim responseIndex As Integer = 1
            Dim responseList = New List(Of ResponseDeclarationType)

            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                Dim id As String = GetResponseId(responseIndex)
                Dim responseDeclaration = New ResponseDeclarationType With {.identifier = id,
                        .cardinality = GetResponseDeclarationCardinality(responseIdentifierAttribute),
                        .baseType = ResponseDeclarationTypeBaseType.string,
                        .baseTypeSpecified = True}

                responseList.Add(responseDeclaration)
                responseIndex += 1
            Next

            assessmentItemType.responseDeclaration = responseList
        End Sub

        Friend Shared Sub AddResponseToMediaInteraction(ByRef xmlDoc As XmlDocument)
            Dim mediaInteractionNodesList As XmlNodeList = GetMediaInteractionNodes(xmlDoc)
            Dim mediaInteractionList As List(Of ResponseDeclarationType) = GetMediaInteractionResponses(mediaInteractionNodesList)

            AddResponseToInteraction(xmlDoc, mediaInteractionList)
        End Sub

        Friend Shared Sub AddResponseToUploadInteraction(ByRef xmlDoc As XmlDocument)
            Dim uploadInteractionNodesList As XmlNodeList = GetUploadInteractionNodes(xmlDoc)
            Dim uploadInteractionList As List(Of ResponseDeclarationType) = GetUploadInteractionResponses(uploadInteractionNodesList)

            AddResponseToInteraction(xmlDoc, uploadInteractionList)
        End Sub

        Friend Shared Sub AddResponseToCustomInteraction(ByRef xmlDoc As XmlDocument)
            Dim customInteractionNodesList As XmlNodeList = GetCustomInteractionNodes(xmlDoc)
            Dim customInteractionList As List(Of ResponseDeclarationType) = GetCustomInteractionResponses(customInteractionNodesList)

            AddResponseToInteraction(xmlDoc, customInteractionList)
        End Sub

        Private Shared Sub AddResponseToInteraction(xmlDoc As XmlDocument, interactions As List(Of ResponseDeclarationType))
            Dim xmlNamespaceManager As New XmlNamespaceManager(xmlDoc.NameTable)
            xmlNamespaceManager.AddNamespace("qti", xmlDoc.DocumentElement.NamespaceURI)

            For Each mediaInteractionControl As ResponseDeclarationType In interactions
                Dim ns As New XmlSerializerNamespaces()
                ns.Add(String.Empty, "http://www.imsglobal.org/xsd/imsqti_v2p2")

                Dim nodes = xmlDoc.SelectNodes("//qti:responseDeclaration", xmlNamespaceManager)
                Dim mediaResponseDeclarationNode = ChainHandlerHelper.ObjectToXmlDocument(mediaInteractionControl, ns).DocumentElement
                Dim newChild = xmlDoc.ImportNode(mediaResponseDeclarationNode, True)

                If nodes IsNot Nothing AndAlso nodes.Count > 0 Then
                    Dim lastNode = nodes(nodes.Count - 1)

                    lastNode.ParentNode.InsertAfter(newChild, lastNode)
                Else
                    xmlDoc.DocumentElement.PrependChild(newChild)
                End If
            Next
        End Sub

        Friend Shared Function GetMediaInteractionResponses(xmlNodeList As XmlNodeList) As List(Of ResponseDeclarationType)
            Dim returnList As New List(Of ResponseDeclarationType)

            If xmlNodeList IsNot Nothing Then
                For Each xmlNode As XmlNode In xmlNodeList
                    Dim responseDeclarationType As New ResponseDeclarationType With {
                        .identifier = If(xmlNode.Attributes("responseIdentifier") IsNot Nothing, xmlNode.Attributes("responseIdentifier").InnerText, "RESPONSE"),
                        .cardinality = ResponseDeclarationTypeCardinality.single,
                        .baseType = ResponseDeclarationTypeBaseType.integer,
                        .baseTypeSpecified = True
                    }

                    returnList.Add(responseDeclarationType)
                Next
            End If

            Return returnList
        End Function

        Friend Shared Function GetUploadInteractionResponses(xmlNodeList As XmlNodeList) As List(Of ResponseDeclarationType)
            Dim returnList As New List(Of ResponseDeclarationType)

            If xmlNodeList IsNot Nothing Then
                For Each xmlNode As XmlNode In xmlNodeList
                    Dim responseDeclarationType As New ResponseDeclarationType With {
                        .identifier = xmlNode.Attributes("responseIdentifier").InnerText,
                        .cardinality = ResponseDeclarationTypeCardinality.single,
                        .baseType = ResponseDeclarationTypeBaseType.file,
                        .baseTypeSpecified = True
                    }

                    returnList.Add(responseDeclarationType)
                Next
            End If

            Return returnList
        End Function

        Friend Shared Function GetCustomInteractionResponses(xmlNodeList As XmlNodeList) As List(Of ResponseDeclarationType)
            Dim returnList As New List(Of ResponseDeclarationType)
            If xmlNodeList IsNot Nothing Then
                For Each xmlNode As XmlNode In xmlNodeList
                    Dim responseDeclarationType As New ResponseDeclarationType With {
                        .identifier = xmlNode.Attributes("responseIdentifier").InnerText,
                        .cardinality = ResponseDeclarationTypeCardinality.single,
                        .baseType = ResponseDeclarationTypeBaseType.integer,
                        .baseTypeSpecified = True
                    }

                    returnList.Add(responseDeclarationType)
                Next
            End If

            Return returnList
        End Function

        Friend Shared Function GetDecimalOutComeDeclaration(responseId As Nullable(Of Integer)) As OutcomeDeclarationType
            Dim identifier As String = GetDecimalResponseId(If(responseId Is Nothing, 0, CInt(responseId)))

            Return GetOutComeDeclaration(OutcomeDeclarationTypeBaseType.float, identifier, False)
        End Function

        Friend Shared Function GetDefaultOutComeDeclaration(responseId As Nullable(Of Integer), findingId As Nullable(Of Integer), concept As Concept, shouldBeTranslated As Boolean) As OutcomeDeclarationType
            Return GetDefaultOutComeDeclaration(OutcomeDeclarationTypeBaseType.integer, responseId, findingId, concept, shouldBeTranslated)
        End Function

        Friend Shared Function GetDefaultOutComeDeclaration(baseType As OutcomeDeclarationTypeBaseType, responseId As Nullable(Of Integer), findingId As Nullable(Of Integer), concept As Concept, shouldBeTranslated As Boolean) As OutcomeDeclarationType
            Dim scoreId As String = GetScoreId(shouldBeTranslated)

            If concept IsNot Nothing Then
                If responseId IsNot Nothing AndAlso responseId > 0 Then
                    scoreId = String.Concat(GetResponseId(responseId.Value), "_", concept.Code)
                Else
                    scoreId = concept.Code
                End If
            ElseIf responseId IsNot Nothing AndAlso responseId > 0 Then
                scoreId = GetScoreResponseId(responseId.Value)
            ElseIf findingId IsNot Nothing AndAlso findingId > 0 Then
                scoreId = GetScoreFindingId(findingId.Value)
            End If

            Dim bt = baseType
            If scoreId.Equals(PackageCreatorConstants.SCORE, StringComparison.InvariantCultureIgnoreCase) Then
                bt = OutcomeDeclarationTypeBaseType.float
            End If
            Return GetOutComeDeclaration(bt, scoreId, True)
        End Function

        Friend Shared Function GetOutComeDeclaration(baseType As OutcomeDeclarationTypeBaseType, identifier As String, addDefaultValue As Boolean) As OutcomeDeclarationType
            Dim defaultoutcomeDeclarationType As New OutcomeDeclarationType With {.identifier = identifier,
                    .baseType = baseType,
                    .baseTypeSpecified = True,
                    .cardinality = OutcomeDeclarationTypeCardinality.single}
            If addDefaultValue Then
                defaultoutcomeDeclarationType.defaultValue = New DefaultValueType

                Dim defaultValue(0) As ValueType
                defaultValue(0) = New ValueType With {
                    .Value = "0"
                }

                defaultoutcomeDeclarationType.defaultValue.value = defaultValue.ToList()
            End If

            Return defaultoutcomeDeclarationType
        End Function

        Friend Shared Function GetMaxScoreOutcomeDeclaration(maxScore As Decimal) As OutcomeDeclarationType
            Dim maxScoreOutcomeDeclaration = GetOutComeDeclaration(OutcomeDeclarationTypeBaseType.float, PackageCreatorConstants.MAXSCORE, True)
            maxScoreOutcomeDeclaration.defaultValue.value.First().Value = maxScore.ToString()
            Return maxScoreOutcomeDeclaration
        End Function

        Friend Shared Function GetDefaultOutcomeDeclarationsList(translationTable As ItemScoreTranslationTable) As List(Of OutcomeDeclarationType)
            Return GetDefaultOutcomeDeclarationsList(translationTable, OutcomeDeclarationTypeBaseType.integer, Nothing)
        End Function

        Friend Shared Function GetDefaultOutcomeDeclarationsList(translationTable As ItemScoreTranslationTable, baseType As OutcomeDeclarationTypeBaseType, responseId As Nullable(Of Integer)) As List(Of OutcomeDeclarationType)
            Dim shouldBeTranslated As Boolean = False
            If translationTable IsNot Nothing Then shouldBeTranslated = ShouldScoreBeTranslated(translationTable)

            Dim returnValue As New List(Of OutcomeDeclarationType) From {
                GetDefaultOutComeDeclaration(baseType, responseId, Nothing, Nothing, shouldBeTranslated)
            }

            If translationTable IsNot Nothing AndAlso ShouldScoreBeTranslated(translationTable) Then
                Dim outcomeDeclarationInterpolationType As OutcomeDeclarationType = GetInterpolationTable(translationTable, OutcomeDeclarationTypeBaseType.float)

                returnValue.Add(outcomeDeclarationInterpolationType)
            End If

            Return returnValue
        End Function

        Friend Shared Function GetDefaultOutcomeDeclarationsForConceptScoringList(ByVal solution As Solution, ByVal responseIdentifierAttributeList As XmlNodeList) As List(Of OutcomeDeclarationType)
            Return GetDefaultOutcomeDeclarationsForConceptScoringList(solution, responseIdentifierAttributeList, Nothing, 0)
        End Function

        Friend Shared Function GetDefaultOutcomeDeclarationsForConceptScoringList(
        ByVal solution As Solution,
        ByVal responseIdentifierAttributeList As XmlNodeList,
        ByVal finding As ConceptFinding,
        ByVal conceptFindingIndex As Integer) As List(Of OutcomeDeclarationType)

            Dim conceptFinding = finding
            Dim list As List(Of OutcomeDeclarationType) = New List(Of OutcomeDeclarationType)
            Dim listConcepts As SortedList(Of String, OutcomeDeclarationType) = New SortedList(Of String, OutcomeDeclarationType)
            Dim responseIndex As Integer = 1
            If conceptFindingIndex > 0 Then
                responseIndex = conceptFindingIndex
            End If

            If conceptFinding Is Nothing Then
                conceptFinding = CombinedScoringHelper.GetConceptFindingByResponseIdentifierList(solution, responseIdentifierAttributeList)
            End If

            If conceptFinding IsNot Nothing Then
                For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                    Dim conceptFactList As List(Of ConceptFact) = CombinedScoringHelper.GetConceptFactListByResponseIdentifier(conceptFinding, responseIdentifierAttribute.Value)
                    If conceptFactList IsNot Nothing AndAlso conceptFactList.Count > 0 Then
                        For Each conceptFact As ConceptFact In conceptFactList
                            If conceptFact IsNot Nothing AndAlso conceptFact.Concepts.Count > 0 Then
                                For Each concept As Concept In conceptFact.Concepts
                                    Dim outcomeDeclarationType As OutcomeDeclarationType = GetDefaultOutComeDeclaration(responseIndex, Nothing, concept, False)
                                    If Not listConcepts.ContainsKey(outcomeDeclarationType.identifier) Then listConcepts.Add(outcomeDeclarationType.identifier, outcomeDeclarationType)
                                Next
                            End If
                        Next
                    End If
                Next

                list.AddRange(listConcepts.Values)
            End If

            Return list
        End Function

        Private Shared Function GetInterpolationTable(translationTable As ItemScoreTranslationTable, baseType As OutcomeDeclarationTypeBaseType) As OutcomeDeclarationType
            Dim outcomeDeclarationinterpolation As New OutcomeDeclarationType With {.baseType = baseType, .baseTypeSpecified = True, .cardinality = OutcomeDeclarationTypeCardinality.single, .identifier = PackageCreatorConstants.SCORE}
            Dim interpolationTable As New InterpolationTableType
            Dim interpolationTableEntryList As New List(Of InterpolationTableEntryType)
            translationTable.OrderByDescending(Function(el) el.RawScore).ToList.ForEach(
                Sub(element) interpolationTableEntryList.Add(New InterpolationTableEntryType() With {
                                                                .includeBoundary = True, .sourceValue = element.RawScore,
                                                                .targetValue = element.TranslatedScore.ToString}))
            interpolationTable.interpolationTableEntry = interpolationTableEntryList

            outcomeDeclarationinterpolation.Item = interpolationTable

            Return outcomeDeclarationinterpolation
        End Function

        Friend Shared Function GetDefaultResponseProcessingForConceptScoring(
                  responseIdentifierAttributeList As XmlNodeList,
                  conceptFinding As ConceptFinding,
                  responseIndex As Integer) As XmlDocument

            Dim result As XmlDocument = New XmlDocument()
            Dim index As Integer = responseIndex
            If conceptFinding IsNot Nothing Then
                Dim navigator As XPathNavigator = result.CreateNavigator()
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "responseProcessing", Nothing, True)

                Dim responseId As String = String.Empty
                Dim responseProcessing As String = String.Empty

                Dim responseVariable As XElement = <variable identifier="{0}"/>
                Dim basevalueVariable As XElement = <baseValue baseType="{0}">{1}</baseValue>
                Dim outcomeValue As XElement = <setOutcomeValue identifier="{0}">
                                                   <baseValue baseType="{1}">{2}</baseValue>
                                               </setOutcomeValue>

                For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                    Dim conceptFactList As List(Of ConceptFact) = CombinedScoringHelper.GetConceptFactListByResponseIdentifier(conceptFinding, responseIdentifierAttribute.Value)

                    If conceptFactList IsNot Nothing AndAlso conceptFactList.Count > 0 Then
                        For Each conceptFact As ConceptFact In conceptFactList
                            If conceptFact IsNot Nothing AndAlso conceptFact.Concepts.Count > 0 Then
                                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "responseCondition", Nothing, True)
                                Dim conceptIndex As Integer = 1
                                responseId = GetResponseId(index)

                                For Each value As ConceptValue In conceptFact.Values
                                    If conceptIndex = 1 Then
                                        ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "responseIf", Nothing, True)
                                    Else
                                        ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "responseElseIf", Nothing, True)
                                    End If

                                    If value.Values.Count > 1 Then ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "or", Nothing, True)

                                    For Each basevalue As BaseValue In value.Values
                                        responseProcessing = String.Format(GetTypeOfComparison(basevalue).ToString, String.Format(responseVariable.ToString, responseId), String.Format(basevalueVariable.ToString, ResponseDeclarationTypeBaseType.identifier.ToString(), basevalue.ToString))
                                        navigator.AppendChild(responseProcessing)
                                    Next

                                    If value.Values.Count > 1 Then navigator.MoveToParent()

                                    For Each concept As Concept In conceptFact.Concepts
                                        responseProcessing = String.Format(outcomeValue.ToString, String.Concat(responseId, "_", concept.Code), ResponseDeclarationTypeBaseType.float.ToString(), concept.Value.ToString(CultureInfo.InvariantCulture.NumberFormat))
                                        navigator.AppendChild(responseProcessing)
                                    Next

                                    conceptIndex += 1
                                Next
                            End If

                            navigator.MoveToRoot()
                            navigator.MoveToFirstChild()
                        Next
                    End If

                    index += 1
                Next
            End If

            Return result
        End Function

        Friend Shared Sub ConvertResponseIdentifierToFixedName(ByRef itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)
            xmlNamespaceManager.AddNamespace("html", XHTMLNAMESPACE)
            xmlNamespaceManager.AddNamespace("pci", PCINAMESPACE)

            Dim responseIndex As Integer = 1

            For Each responseIdentifierAttribute As XmlNode In ResponseIdentifierHelper.GetResponseIdentifiers(itemDocument, xmlNamespaceManager)
                Dim attribute As XmlAttribute = DirectCast(responseIdentifierAttribute, XmlAttribute)
                Dim oldValue As String = attribute.Value
                Dim newValue As String = GetResponseId(responseIndex)

                attribute.Value = newValue

                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:responseDeclaration[@identifier='{oldValue}']", xmlNamespaceManager)
                    referenceNode.Attributes("identifier").Value = newValue
                Next

                responseIndex += 1
            Next
        End Sub

        Friend Shared Sub ConvertMediaResponseIdentifierToFixedName(ByRef itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)
            xmlNamespaceManager.AddNamespace("pci", PCINAMESPACE)
            xmlNamespaceManager.AddNamespace("html", XHTMLNAMESPACE)

            Dim responseIndex As Integer = 1

            For Each responseIdentifierAttribute As XmlNode In ResponseIdentifierHelper.GetMediaResponseIdentifiers(itemDocument, xmlNamespaceManager)
                Dim attribute As XmlAttribute = DirectCast(responseIdentifierAttribute, XmlAttribute)

                Dim oldValue As String = attribute.Value
                Dim newValue As String = oldValue

                If responseIndex > 1 Then
                    newValue = String.Format(String.Concat(oldValue, "{0}"), responseIndex.ToString())
                End If
                attribute.Value = newValue

                Dim referenceNode As XmlNode = itemDocument.SelectSingleNode($"//qti:responseDeclaration[@identifier='{oldValue}'][last()]", xmlNamespaceManager)
                If referenceNode IsNot Nothing Then
                    referenceNode.Attributes("identifier").Value = newValue
                End If

                responseIndex += 1
            Next
        End Sub

        Friend Shared Sub ConvertGapMatchIdentifierToFixedName(ByRef itemDocument As XmlDocument, ByRef itemExtensionDocument As XmlDocument, packageCreator As QTI22PackageCreator)
            Const IDENTIFIERREF As String = "IdentifierRef"
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)
            If itemExtensionDocument IsNot Nothing Then xmlNamespaceManager.AddNamespace("dep", itemExtensionDocument.DocumentElement.NamespaceURI)

            Dim gapmatchIdentifierList As XmlNodeList = itemDocument.SelectNodes("//qti:gap[@identifier]", xmlNamespaceManager)
            Dim gapmatchIndex As Integer = 1

            For Each gapmatchIdentifier As XmlNode In gapmatchIdentifierList
                Dim attribute As XmlAttribute = gapmatchIdentifier.Attributes("identifier")
                Dim oldValue As String = attribute.Value
                Dim newValue As String = String.Concat("G", gapmatchIndex)

                If packageCreator IsNot Nothing AndAlso packageCreator.ReplacedIds IsNot Nothing Then
                    If packageCreator.ReplacedIds.ContainsKey(oldValue) Then
                        packageCreator.ReplacedIds(oldValue) = newValue
                    Else
                        packageCreator.ReplacedIds.TryAdd(oldValue, newValue)
                    End If
                End If

                attribute.Value = newValue
                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:correctResponse/qti:value[contains(text(), '{oldValue}')]", xmlNamespaceManager)
                    referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                Next

                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:correctResponse[contains(@interpretation, '{oldValue}')]/@interpretation", xmlNamespaceManager)
                    referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                Next

                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:mapping/qti:mapEntry[contains(@mapKey, '{oldValue}')]/@mapKey", xmlNamespaceManager)
                    DirectCast(referenceNode, XmlAttribute).Value = Replace(DirectCast(referenceNode, XmlAttribute).Value, oldValue, newValue)
                Next

                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:member/qti:baseValue[contains(text(), '{oldValue}')]", xmlNamespaceManager)
                    referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                Next

                If itemExtensionDocument IsNot Nothing Then
                    For Each referenceNode As XmlNode In itemExtensionDocument.SelectNodes($"//dep:gapMatchInteractionExtensions/dep:gapMatchInteractionExtension/dep:gap[@{IDENTIFIERREF}='{oldValue}']", xmlNamespaceManager)
                        referenceNode.Attributes(IDENTIFIERREF).Value = newValue
                    Next
                End If

                gapmatchIndex += 1
            Next
        End Sub

        Friend Shared Function GetDefaultResponseProcessing(shouldBeTranslated As Boolean) As XmlDocument
            Return GetDefaultResponseProcessing(shouldBeTranslated, ScoreTemplates.TemplateType.None)
        End Function

        Friend Shared Function GetDefaultResponseProcessing(shouldBeTranslated As Boolean, scoringsTemplate As ScoreTemplates.TemplateType) As XmlDocument
            Return GetDefaultResponseProcessing(shouldBeTranslated, scoringsTemplate, PackageCreatorConstants.RESPONSE, GetScoreId(shouldBeTranslated))
        End Function

        Friend Shared Function GetDefaultResponseProcessing(shouldBeTranslated As Boolean, scoringsTemplate As ScoreTemplates.TemplateType, responseIdentifier As String, scoringIdentifier As String) As XmlDocument
            Dim result As XmlDocument = New XmlDocument()
            Dim navigator As XPathNavigator = result.CreateNavigator()

            If scoringsTemplate <> ScoreTemplates.TemplateType.None Then
                AddTemplate(navigator, scoringsTemplate, responseIdentifier, scoringIdentifier)
            End If

            If shouldBeTranslated Then
                AddLookUpOutComeValue(navigator)
            End If

            Return result
        End Function

        Private Shared Sub AddTemplate(navigator As XPathNavigator, scoringsTemplate As ScoreTemplates.TemplateType, responseIdentifier As String, scoringIdentifier As String)
            AddResponseProcessingIfNeeded(navigator)

            Dim template As String = ScoreTemplates.GetResponseTemplate(scoringsTemplate, responseIdentifier, scoringIdentifier)

            navigator.AppendChild(template)
        End Sub

        Friend Shared Function GetMediaInteractionNodes(xmlDocument As XmlDocument) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing

            If xmlDocument IsNot Nothing AndAlso xmlDocument.DocumentElement IsNot Nothing Then
                Dim xmlNamespaceManager As New XmlNamespaceManager(xmlDocument.NameTable)
                xmlNamespaceManager.AddNamespace("qti", xmlDocument.DocumentElement.NamespaceURI)

                nodeList = GetMediaInteractionNodes(xmlDocument.DocumentElement, xmlNamespaceManager)
            End If

            Return nodeList
        End Function

        Friend Shared Function GetMediaInteractionNodes(xmlNode As XmlNode, xmlNamespaceManager As XmlNamespaceManager) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing

            If xmlNode IsNot Nothing Then
                nodeList = xmlNode.SelectNodes("//qti:mediaInteraction", xmlNamespaceManager)
            End If

            Return nodeList
        End Function

        Friend Shared Function GetUploadInteractionNodes(xmlDocument As XmlDocument) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing

            If xmlDocument IsNot Nothing AndAlso xmlDocument.DocumentElement IsNot Nothing Then
                Dim xmlNamespaceManager As New XmlNamespaceManager(xmlDocument.NameTable)
                xmlNamespaceManager.AddNamespace("qti", xmlDocument.DocumentElement.NamespaceURI)

                nodeList = GetUploadInteractionNodes(xmlDocument.DocumentElement, xmlNamespaceManager)
            End If

            Return nodeList
        End Function

        Friend Shared Function GetUploadInteractionNodes(xmlNode As XmlNode, xmlNamespaceManager As XmlNamespaceManager) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing

            If xmlNode IsNot Nothing Then
                nodeList = xmlNode.SelectNodes("//qti:uploadInteraction", xmlNamespaceManager)
            End If

            Return nodeList
        End Function

        Friend Shared Function GetCustomInteractionNodes(xmlDocument As XmlDocument) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing

            If xmlDocument IsNot Nothing AndAlso xmlDocument.DocumentElement IsNot Nothing Then
                Dim xmlNamespaceManager As New XmlNamespaceManager(xmlDocument.NameTable)
                xmlNamespaceManager.AddNamespace("qti", xmlDocument.DocumentElement.NamespaceURI)
                xmlNamespaceManager.AddNamespace("html", XHTMLNAMESPACE)
                xmlNamespaceManager.AddNamespace("pci", PCINAMESPACE)

                nodeList = GetCustomInteractionNodes(xmlDocument.DocumentElement, xmlNamespaceManager)
            End If

            Return nodeList
        End Function

        Friend Shared Function GetCustomInteractionNodes(xmlNode As XmlNode, xmlNamespaceManager As XmlNamespaceManager) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing

            If xmlNode IsNot Nothing Then
                nodeList = xmlNode.SelectNodes("//*[@responseIdentifier][(name() = 'customInteraction' and not(./html:object/qti:param[contains(@name, 'responseLength')]) and not(./html:object[@type = 'application/vnd.GeoGebra.file']) and not (./pci:portableCustomInteraction))]", xmlNamespaceManager)
            End If

            Return nodeList
        End Function

        Friend Shared Sub SetGapMatchGapsRequiredAttribute(ByVal solution As Solution, ByRef itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)
            Dim gapmatchInteractionList As XmlNodeList = itemDocument.SelectNodes("//qti:gapMatchInteraction[@responseIdentifier]", xmlNamespaceManager)

            For Each gapmatchInteraction As XmlNode In gapmatchInteractionList
                Dim identifier As String = gapmatchInteraction.Attributes("responseIdentifier").Value

                If solution IsNot Nothing AndAlso
    Not String.IsNullOrEmpty(identifier) AndAlso
    solution.Findings.FirstOrDefault(Function(f) f.Id.Equals(identifier)) IsNot Nothing AndAlso
    (solution.Findings.First(Function(f) f.Id.Equals(identifier)).Facts.Any(Function(fact) fact.Values.OfType(Of KeyValue).Any(Function(kv) kv.Values.Any(Function(bv) TypeOf bv Is NoValue))) OrElse
    solution.Findings.First(Function(f) f.Id.Equals(identifier)).KeyFactsets.Any(Function(kfs) kfs.Facts.Any(Function(fact) fact.Values.OfType(Of KeyValue).Any(Function(kv) kv.Values.Any(Function(bv) TypeOf bv Is NoValue))))) Then

                    For Each gapElement As XmlNode In gapmatchInteraction.SelectNodes("//qti:gap[@required]", xmlNamespaceManager)
                        Dim attribute As XmlAttribute = gapElement.Attributes("required")
                        attribute.Value = "false"
                    Next
                End If
            Next
        End Sub

        Friend Shared Sub ConvertHottextIdentifierToFixedName(ByRef itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)

            Dim hottextIdentifierList As XmlNodeList = itemDocument.SelectNodes("//qti:hottext[@identifier]", xmlNamespaceManager)
            Dim hottextIndex As Integer = 1

            For Each hottextIdentifier As XmlNode In hottextIdentifierList
                Dim attribute As XmlAttribute = hottextIdentifier.Attributes("identifier")
                Dim oldValue As String = attribute.Value
                Dim newValue As String = GetNewHottextIdentifier(hottextIndex)
                attribute.Value = newValue

                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:correctResponse/qti:value[contains(text(), '{oldValue}')]", xmlNamespaceManager)
                    referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                Next

                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:correctResponse[contains(@interpretation, '{oldValue}')]/@interpretation", xmlNamespaceManager)
                    referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                Next

                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:mapping/qti:mapEntry[contains(@mapKey, '{oldValue}')]/@mapKey", xmlNamespaceManager)
                    DirectCast(referenceNode, XmlAttribute).Value = Replace(DirectCast(referenceNode, XmlAttribute).Value, oldValue, newValue)
                Next

                For Each referenceNode As XmlNode In itemDocument.SelectNodes(
                    $"//qti:outcomeDeclaration[contains(@identifier, '{String.Format($"{GetScoreId(False)}_{oldValue}", GetScoreId(False), oldValue)}')]/@identifier", xmlNamespaceManager)
                    referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                Next

                For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:member/qti:baseValue[contains(text(), '{oldValue}')]", xmlNamespaceManager)
                    referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                Next

                For Each referenceNode As XmlNode In itemDocument.SelectNodes(
                    $"//qti:setOutcomeValue[contains(@identifier, '{String.Format($"{GetScoreId(False)}_{oldValue}", GetScoreId(False), oldValue)}')]/@identifier", xmlNamespaceManager)
                    referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                Next

                hottextIndex += 1
            Next
        End Sub

        Friend Shared Sub ConvertMCIdentifierToFixedName(ByRef itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)

            Dim mcIdentifierList As XmlNodeList = itemDocument.SelectNodes("//qti:choiceInteraction[@responseIdentifier]", xmlNamespaceManager)
            Dim mcIndex As Integer = 1

            For Each mcIdentifier As XmlNode In mcIdentifierList
                Dim attribute As XmlAttribute = mcIdentifier.Attributes("responseIdentifier")

                Dim mcChoicesList As XmlNodeList = itemDocument.SelectNodes($"//qti:simpleChoice[contains(@identifier, '{attribute.Value}')]", xmlNamespaceManager)
                Dim mcChoiceIndex As Integer = 1
                Dim addChoiceIndex As Boolean = True

                If mcChoicesList.Count = 1 Then addChoiceIndex = False

                For Each mcChoice As XmlNode In mcChoicesList
                    Dim choiceAttribute As XmlAttribute = mcChoice.Attributes("identifier")
                    Dim oldValue As String = choiceAttribute.Value
                    Dim newValue As String = $"MC{mcIndex}_{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(mcChoiceIndex)}"
                    If Not addChoiceIndex Then newValue = $"MC{mcIndex}_"
                    choiceAttribute.Value = newValue

                    For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:correctResponse/qti:value[contains(text(), '{oldValue}')]", xmlNamespaceManager)
                        referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                    Next

                    For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:correctResponse[contains(@interpretation, '{oldValue}')]/@interpretation", xmlNamespaceManager)
                        referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                    Next

                    For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:mapping/qti:mapEntry[contains(@mapKey, '{oldValue}')]/@mapKey", xmlNamespaceManager)
                        DirectCast(referenceNode, XmlAttribute).Value = Replace(DirectCast(referenceNode, XmlAttribute).Value, oldValue, newValue)
                    Next

                    For Each referenceNode As XmlNode In itemDocument.SelectNodes($"//qti:member/qti:baseValue[contains(text(), '{oldValue}')]", xmlNamespaceManager)
                        referenceNode.InnerText = Replace(referenceNode.InnerText, oldValue, newValue)
                    Next

                    mcChoiceIndex += 1
                Next

                mcIndex += 1
            Next
        End Sub

        Friend Shared Sub ConvertResponseIdentifiersForDateInputFields(ByRef itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)

            Dim inputIdentifierList As XmlNodeList = itemDocument.SelectNodes("//qti:textEntryInteraction[@dateSubType][@expectedLength='2']", xmlNamespaceManager)
            Dim isMonthInput As Boolean

            For Each inputIdentifier As XmlNode In inputIdentifierList
                Dim dateSubTypeAttribute As XmlAttribute = inputIdentifier.Attributes("dateSubType")

                Select Case dateSubTypeAttribute.Value
                    Case GapDateScoringHelper.DateSubType.dutch.ToString, GapDateScoringHelper.DateSubType.scandinavian.ToString
                        Dim identifierAttribute As XmlAttribute = inputIdentifier.Attributes("responseIdentifier")
                        Dim responseId As Integer = GetResponseId(identifierAttribute.Value)

                        If isMonthInput Then
                            responseId -= 1
                        Else
                            responseId += 1
                        End If

                        identifierAttribute.Value = GetResponseId(responseId)
                        isMonthInput = (Not isMonthInput)
                End Select
            Next
        End Sub

        Friend Shared Sub RemoveDateSubTypeAttributesForDateInputFields(ByRef itemDocument As XmlDocument)
            RemoveAttributesFromInteractions(itemDocument, "textEntryInteraction", "dateSubType")
        End Sub

        Friend Shared Sub RemoveTimeSubTypeAttributesForTimeInputFields(ByRef itemDocument As XmlDocument)
            RemoveAttributesFromInteractions(itemDocument, "textEntryInteraction", "timeSubType")
        End Sub

        Friend Shared Sub RemoveHottextIdAttributesForCorrectionFields(ByRef itemDocument As XmlDocument)
            RemoveAttributesFromInteractions(itemDocument, "extendedTextInteraction", "hottextId")
        End Sub

        Friend Shared Sub RemoveFormulaEditorAttributesForTextFields(ByRef itemDocument As XmlDocument)
            RemoveAttributesFromInteractions(itemDocument, "extendedTextInteraction", "isFormulaEditor")
        End Sub

        Friend Shared Sub RemoveCategorizeAttributesForGraphicGapMatchFields(ByRef itemDocument As XmlDocument)
            RemoveAttributesFromInteractions(itemDocument, "graphicGapMatchInteraction", "categorize")
        End Sub

        Public Shared Sub DecodeMathMLResponses(ByRef itemDocument As XmlDocument)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)

            Dim valueList As XmlNodeList = itemDocument.SelectNodes("//qti:correctResponse/qti:value", xmlNamespaceManager)
            For Each responseValue As XmlNode In valueList
                If BaseValueContainsMathML(responseValue.InnerXml) Then
                    Dim cData As XmlCDataSection = itemDocument.CreateCDataSection(HttpUtility.HtmlDecode(responseValue.InnerXml))
                    responseValue.InnerXml = cData.OuterXml
                End If
            Next

            Dim responseList As XmlNodeList = itemDocument.SelectNodes("//qti:customOperator/qti:baseValue", xmlNamespaceManager)
            For Each responseValue As XmlNode In responseList
                If BaseValueContainsMathML(responseValue.InnerXml) Then
                    Dim cData As XmlCDataSection = itemDocument.CreateCDataSection(HttpUtility.HtmlDecode(responseValue.InnerXml))
                    responseValue.InnerXml = cData.OuterXml
                End If
            Next
        End Sub

        Private Shared Sub AddResponseProcessingIfNeeded(navigator As XPathNavigator)
            navigator.MoveToRoot()

            If Not navigator.MoveToChild("responseProcessing", String.Empty) Then
                ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "responseProcessing", Nothing, True)
            End If
        End Sub

        Friend Shared Sub AddLookUpOutComeValue(testNavigator As XPathNavigator)
            AddResponseProcessingIfNeeded(testNavigator)

            ChainHandlerHelper.AppendChild(testNavigator, "lookupOutcomeValue", True)
            ChainHandlerHelper.AddAttribute(testNavigator, "identifier", PackageCreatorConstants.SCORE)
            ChainHandlerHelper.AppendChild(testNavigator, "variable", True)
            ChainHandlerHelper.AddAttribute(testNavigator, "identifier", PackageCreatorConstants.RAW_SCORE)
        End Sub

        Friend Shared Sub AddScoreCheck(ByVal navigator As XPathNavigator, ByVal solution As Solution, identifier As String)
            Dim scoringMethod As EnumScoringMethod = GetScoringMethod(solution)
            Dim checkValue As Double = GetNumberOfResponses(solution)
            Dim outcomeValue As Double = checkValue

            If scoringMethod = EnumScoringMethod.Dichotomous Then outcomeValue = 1

            AddScoreCheck(navigator, scoringMethod, identifier, checkValue, outcomeValue)
        End Sub

        Friend Shared Sub AddScoreCheck(ByVal navigator As XPathNavigator, scoringMethod As EnumScoringMethod, identifier As String, checkValue As Double, outcomeValue As Double)
            Dim scoreCheck As String = String.Empty
            Dim checkIf As XElement = <responseCondition>
                                          <responseIf>
                                              <gte>
                                                  <variable identifier="{0}"/>
                                                  <baseValue baseType="integer">{1}</baseValue>
                                              </gte>
                                              <setOutcomeValue identifier="{2}">
                                                  <baseValue baseType="integer">{3}</baseValue>
                                              </setOutcomeValue>
                                          </responseIf>
                                      {4}
                                  </responseCondition>

            Dim checkElse As XElement = <responseElse>
                                            <setOutcomeValue identifier="{0}">
                                                <baseValue baseType="integer">0</baseValue>
                                            </setOutcomeValue>
                                        </responseElse>

            If scoringMethod = EnumScoringMethod.Dichotomous Then
                scoreCheck = String.Format(checkIf.ToString, identifier, checkValue.ToString(), identifier, outcomeValue.ToString(), String.Format(checkElse.ToString, identifier))
            Else
                scoreCheck = String.Format(checkIf.ToString, identifier, checkValue.ToString(), identifier, outcomeValue.ToString(), Nothing)
            End If

            navigator.AppendChild(scoreCheck)
        End Sub

        Friend Shared Sub AddSumOfResponses(ByVal navigator As XPathNavigator, identifier As String)
            AddSumOfResponses(navigator, False, identifier)
        End Sub

        Friend Shared Sub AddSumOfResponses(ByVal navigator As XPathNavigator, matchPoint As Boolean, identifier As String)
            Dim mapResponseString As String = If(matchPoint, "mapResponsePoint", "mapResponse")

            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "setOutcomeValue", Nothing, True)
            ChainHandlerHelper.AddAttribute(navigator, "identifier", identifier)

            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "sum", Nothing, True)

            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, mapResponseString, Nothing, True)
            ChainHandlerHelper.AddAttribute(navigator, "identifier", PackageCreatorConstants.RESPONSE)

            navigator.MoveToParent()

            navigator.MoveToParent()
            navigator.MoveToParent()
        End Sub

        Friend Shared Sub AddSumOfMapResponsePoints(ByVal navigator As XPathNavigator, ByVal responseIdentifierAttributeList As XmlNodeList, identifier As String)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "setOutcomeValue", Nothing, True)
            ChainHandlerHelper.AddAttribute(navigator, "identifier", identifier)
            ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "sum", Nothing, True)

            Dim responseIndex As Integer = 1

            For Each responseIdentifierAttribute As XmlNode In responseIdentifierAttributeList
                If TypeOf responseIdentifierAttribute Is XmlAttribute AndAlso DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.LocalName.ToLower <> "mediainteraction" Then
                    ChainHandlerHelper.AddNewChild(navigator, String.Empty, String.Empty, "mapResponsePoint", Nothing, True)
                    ChainHandlerHelper.AddAttribute(navigator, "identifier", GetResponseId(responseIndex))

                    navigator.MoveToParent()
                    responseIndex += 1
                End If
            Next

            navigator.MoveToParent()
            navigator.MoveToParent()
        End Sub

        Public Shared Function GetBaseType(baseValue As BaseValue) As ResponseDeclarationTypeBaseType
            Dim returnValue As ResponseDeclarationTypeBaseType

            If TypeOf baseValue Is DecimalValue OrElse TypeOf baseValue Is DecimalRangeValue OrElse TypeOf baseValue Is DecimalComparisonValue Then
                returnValue = ResponseDeclarationTypeBaseType.float
            ElseIf TypeOf baseValue Is IntegerValue OrElse TypeOf baseValue Is IntegerRangeValue OrElse TypeOf baseValue Is IntegerComparisonValue Then
                returnValue = ResponseDeclarationTypeBaseType.integer
            Else
                returnValue = ResponseDeclarationTypeBaseType.string
            End If

            Return returnValue
        End Function

        Public Shared Function GetTypeOfComparison(baseValue As BaseValue) As XElement
            If GetBaseType(baseValue) = ResponseDeclarationTypeBaseType.string Then
                Return <match>{0}{1}</match>
            Else
                If TypeOf baseValue Is DecimalComparisonValue Then
                    Dim value As DecimalComparisonValue = DirectCast(baseValue, DecimalComparisonValue)

                    Select Case value.GetComparisonType(value.TypeOfComparison)
                        Case TypedComparisonValue(Of Decimal).ComparisonType.SmallerThan
                            Return <lt>{0}{1}</lt>
                        Case TypedComparisonValue(Of Decimal).ComparisonType.SmallerThanEquals
                            Return <lte>{0}{1}</lte>
                        Case TypedComparisonValue(Of Decimal).ComparisonType.GreaterThan
                            Return <gt>{0}{1}</gt>
                        Case TypedComparisonValue(Of Decimal).ComparisonType.GreaterThanEquals
                            Return <gte>{0}{1}</gte>
                    End Select
                ElseIf TypeOf baseValue Is IntegerComparisonValue Then
                    Dim value As IntegerComparisonValue = DirectCast(baseValue, IntegerComparisonValue)

                    Select Case value.GetComparisonType(value.TypeOfComparison)
                        Case TypedComparisonValue(Of Integer).ComparisonType.SmallerThan
                            Return <lt>{0}{1}</lt>
                        Case TypedComparisonValue(Of Integer).ComparisonType.SmallerThanEquals
                            Return <lte>{0}{1}</lte>
                        Case TypedComparisonValue(Of Integer).ComparisonType.GreaterThan
                            Return <gt>{0}{1}</gt>
                        Case TypedComparisonValue(Of Integer).ComparisonType.GreaterThanEquals
                            Return <gte>{0}{1}</gte>
                    End Select
                End If

                Return <equal toleranceMode="exact">{0}{1}</equal>
            End If
        End Function

    End Class
End Namespace