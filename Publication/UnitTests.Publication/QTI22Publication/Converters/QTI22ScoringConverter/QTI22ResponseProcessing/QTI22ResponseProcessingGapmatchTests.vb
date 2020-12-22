
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports System.Xml.Serialization
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingGapmatchTests
    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Polytomous_Test()
        GetResponseProcessingTest(_itemBody1, _finding1, _responseProcessing1, GetGapMatchScoringParams_1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Dichotomous_Test()
        GetResponseProcessingTest(_itemBody1, _finding5, _responseProcessing5, GetGapMatchScoringParams_1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForMultipleFactSets_Polytomous_Test()
        GetResponseProcessingTest(_itemBody1, _finding2, _responseProcessing2, GetGapMatchScoringParams_1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForMultipleFactSets_Dichotomous_Test()
        GetResponseProcessingTest(_itemBody1, _finding6, _responseProcessing6, GetGapMatchScoringParams_1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactSetTest()
        GetResponseProcessingTest(_itemBody1, _finding3, _responseProcessing3, GetGapMatchScoringParams_1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_Test()
        GetResponseProcessingTest(_itemBody1, _finding4, _responseProcessing4, GetGapMatchScoringParams_1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_Test()
        GetResponseProcessingTest(_itemBody1, _finding7, _responseProcessing7, GetGapMatchScoringParams_1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingInRightOrder_Dichotomous_Test()
        'The gaps are actually guids in the itembody (but are renamed later on during publication to G1, G2, etc)
        'The response processing should be in the right order (so G1, G2, etc) and not the order of the guids
        GetResponseProcessingTest(_itemBody2, _finding8, _responseProcessing8, GetGapMatchScoringParams_2, False)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForNoValue_Dichotomous_Test()
        GetResponseProcessingTest(_itemBody3, _finding9, _responseProcessing9, GetGapMatchScoringParams_3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForNoValue_Polytomous_Test()
        GetResponseProcessingTest(_itemBody3, _finding10, _responseProcessing10, GetGapMatchScoringParams_3)
    End Sub

    Public Sub GetResponseProcessingTest(itemBody As XElement, findingElement As XElement, responseProcessingElement As XElement, scoringPrms As HashSet(Of ScoringParameter), Optional fixGapNames As Boolean = True)

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(itemBody)
        Dim scoringHelper = New QTI22CombinedScoringConverter(scoringPrms)

        Dim finding As KeyFinding = findingElement.Deserialize(Of KeyFinding)()
        Dim findingIndex As Integer = 0
        Dim s = New Solution
        s.Findings.Add(finding)

        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, s, finding, findingIndex, scoringPrms, New QTI22CombinedScoringConverter, False)
        Dim assessmentItemType As New AssessmentItemType()
        assessmentItemType.itemBody = CType(ChainHandlerHelper.StringToObject(itemBody.ToString, GetType(ItemBodyType)), ItemBodyType)
        'Act
        Dim result = processor.GetProcessing().ToXmlDocument
        If fixGapNames Then
            assessmentItemType.responseProcessing = CType(ChainHandlerHelper.StringToObject(result.OuterXml.Replace("<responseProcessing>", "<responseProcessing xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://www.imsglobal.org/xsd/imsqti_v2p2"">"), GetType(ResponseProcessingType)), ResponseProcessingType)
            Dim itemDocument = ChainHandlerHelper.ObjectToXmlDocument(assessmentItemType, New XmlSerializerNamespaces())
            scoringHelper.UpdateDocument(s, itemDocument, Nothing, Nothing)
            result = XElement.Parse(XDocument.Parse(itemDocument.OuterXml).Descendants.FirstOrDefault(Function(d) d.Name.LocalName = "responseProcessing").ToString.Replace("xmlns=""http://www.imsglobal.org/xsd/imsqti_v2p2""", String.Empty)).ToXmlDocument
        End If
        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(responseProcessingElement, result))
    End Sub

    Private Function GetGapMatchScoringParams_1() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim gapMatchScoringParameter = New GapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C", "D")

        Dim xhtmlValue As XElement = <xhtmlParameter name="gapMatchInlineInput">
                                         <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Tekst met <cito:InlineElement id="G1" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:plaintextparameter name="inlineGapMatchId">G1</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="inlineGapMatchLabel">gap 1</cito:plaintextparameter>
                                                         <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                         <cito:integerparameter name="width"/>
                                                         <cito:integerparameter name="height"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement> een aantal <cito:InlineElement id="G2" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:plaintextparameter name="inlineGapMatchId">G2</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="inlineGapMatchLabel">gap 2</cito:plaintextparameter>
                                                         <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                         <cito:integerparameter name="width"/>
                                                         <cito:integerparameter name="height"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement> gaten waarin teksten <cito:InlineElement id="G3" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:plaintextparameter name="inlineGapMatchId">G3</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="inlineGapMatchLabel">gap 3</cito:plaintextparameter>
                                                         <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                         <cito:integerparameter name="width"/>
                                                         <cito:integerparameter name="height"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement> kunnen worden gesleept <cito:InlineElement id="G4" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:plaintextparameter name="inlineGapMatchId">G4</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="inlineGapMatchLabel">gap 4</cito:plaintextparameter>
                                                         <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                         <cito:integerparameter name="width"/>
                                                         <cito:integerparameter name="height"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement> door de kandidaat.</p>
                                     </xhtmlParameter>

        Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xhtmlValue.ToString}

        gapMatchScoringParameter.GapXhtmlParameter = xhtmlPrm
        Dim scoreParam As ScoringParameter = gapMatchScoringParameter.Transform()
        scoreParams.Add(scoreParam)

        Return scoreParams
    End Function

    Private Function GetGapMatchScoringParams_2() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim gapMatchScoringParameter = New GapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C")

        Dim xhtmlValue As XElement = <xhtmlParameter name="gapMatchInlineInput">
                                         <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Tekst met <cito:InlineElement id="I77e2abe1-4fe4-4c34-a9af-6f6900868ecf" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:plaintextparameter name="inlineGapMatchId">I77e2abe1-4fe4-4c34-a9af-6f6900868ecf</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="inlineGapMatchLabel">gap 1</cito:plaintextparameter>
                                                         <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                         <cito:integerparameter name="width"/>
                                                         <cito:integerparameter name="height"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement> een aantal <cito:InlineElement id="I9aeb153d-aaca-4390-a35a-63692cb03035" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:plaintextparameter name="inlineGapMatchId">I9aeb153d-aaca-4390-a35a-63692cb03035</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="inlineGapMatchLabel">gap 2</cito:plaintextparameter>
                                                         <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                         <cito:integerparameter name="width"/>
                                                         <cito:integerparameter name="height"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement> gaten waarin teksten <cito:InlineElement id="I4174cc03-b6e0-4eab-8870-9973f6cca0d4" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:plaintextparameter name="inlineGapMatchId">I4174cc03-b6e0-4eab-8870-9973f6cca0d4</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="inlineGapMatchLabel">gap 3</cito:plaintextparameter>
                                                         <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                         <cito:integerparameter name="width"/>
                                                         <cito:integerparameter name="height"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement> kunnen worden gesleept door de kandidaat.</p>
                                     </xhtmlParameter>

        Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xhtmlValue.ToString}

        gapMatchScoringParameter.GapXhtmlParameter = xhtmlPrm
        Dim scoreParam As ScoringParameter = gapMatchScoringParameter.Transform()
        scoreParams.Add(scoreParam)

        Return scoreParams
    End Function

    Private Function GetGapMatchScoringParams_3() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)

        Dim gapMatchScoringParameter = <GapMatchRichTextScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="gapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController">
                                           <subparameterset id="A">
                                               <gapTextRichTextParameter name="gapTextRichText" matchMax="1">
                                                   <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                                       <img isinlineelement="true" id="I40ff6b00-ea7a-49f3-80d3-d1a777526c7e" width="20" height="20" alt="Dot.jpg" src="resource://package/Dot.jpg"/> </p>
                                               </gapTextRichTextParameter>
                                           </subparameterset>
                                           <subparameterset id="B">
                                               <gapTextRichTextParameter name="gapTextRichText" matchMax="1">
                                                   <p id="c1-id-12">
                                                       <img isinlineelement="true" id="dfff9d57-96db-4383-b6a2-91702d5185f1" width="20" height="20" alt="Dot.jpg" src="resource://package/Dot.jpg"/>
                                                   </p>
                                                   <p id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml"> </p>
                                               </gapTextRichTextParameter>
                                           </subparameterset>
                                           <definition id="">
                                               <gapTextRichTextParameter name="gapTextRichText" matchMax="1"/>
                                           </definition>

                                       </GapMatchRichTextScoringParameter>

        Dim xHtml = <xhtmlParameter name="gapMatchInlineInput">
                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Op een dag was De Bende Van Madrid weer ober zee aan het varen. Het was toen 18 maart 1965.<br id="c1-id-12"/>Plots zag Dirk een Matroospiraat, een fles <strong id="c1-id-13">(A >) <cito:InlineElement id="I1b86db4d-9de2-4b81-96a2-b20766506894" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="e = ntireItem">
                                            <cito:plaintextparameter name="inlineGapMatchId">I1b86db4d-9de2-4b81-96a2-b20766506894</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineGapMatchLabel">A</cito:plaintextparameter>
                                            <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                            <cito:integerparameter name="width"/>
                                            <cito:integerparameter name="height"/>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></strong> drijven<strong id="c1-id-14"> (B >)</strong><cito:InlineElement id="I48433d84-039d-4b26-9bf8-226cdef1ffa4" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                <cito:parameters>
                                    <cito:parameterSet id="entireItem">
                                        <cito:plaintextparameter name="inlineGapMatchId">I48433d84-039d-4b26-9bf8-226cdef1ffa4</cito:plaintextparameter>
                                        <cito:plaintextparameter name="inlineGapMatchLabel">B</cito:plaintextparameter>
                                        <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                        <cito:integerparameter name="width"/>
                                        <cito:integerparameter name="height"/>
                                    </cito:parameterSet>
                                </cito:parameters>
                            </cito:InlineElement>in het water <strong id="c1-id-15">(C >)</strong><cito:InlineElement id="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                <cito:parameters>
                                    <cito:parameterSet id="entireItem">
                                        <cito:plaintextparameter name="inlineGapMatchId">I6fa9cbf3-9b01-442a-ab81-b1bfffea6549</cito:plaintextparameter>
                                        <cito:plaintextparameter name="inlineGapMatchLabel">c</cito:plaintextparameter>
                                        <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                        <cito:integerparameter name="width"/>
                                        <cito:integerparameter name="height"/>
                                    </cito:parameterSet>
                                </cito:parameters>
                            </cito:InlineElement> met behulp van nog enkele Matroospiraten, konden ze de fles aan boord krijgen. Er zat duidelijk een blad papier in, dat leek o een schatkaart.</p>
                        <p id="c1-id-16" xmlns="http://www.w3.org/1999/xhtml"> </p>
                        <p id="c1-id-17" xmlns="http://www.w3.org/1999/xhtml">
                            <strong id="c1-id-18">Vraag: Sleep de punt naar de juiste plek in de tekst.</strong>
                        </p>
                    </xhtmlParameter>
        Dim scoreParam = gapMatchScoringParameter.Deserialize(Of GapMatchRichTextScoringParameter)
        Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xHtml.ToString}

        scoreParam.GapXhtmlParameter = xhtmlPrm
        scoreParam = CType(scoreParam.Transform, GapMatchRichTextScoringParameter)
        scoreParams.Add(scoreParam)

        Return scoreParams
    End Function
    Private _itemBody1 As XElement =
<itemBody xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p2">
    <div class="content">
        <div>
            <div>
                <gapMatchInteraction responseIdentifier="gapMatchController" shuffle="false">
                    <gapText identifier="A" matchMax="1">tekst 1</gapText>
                    <gapText identifier="B" matchMax="1">tekst 2</gapText>
                    <gapText identifier="C" matchMax="1">tekst 3</gapText>
                    <gapText identifier="D" matchMax="1">tekst 4</gapText>
                    <p id="c1-id-11">Tekst <span><gap identifier="G1" required="true"/></span> met een <span><gap identifier="G2" required="true"/></span> gaten <span><gap identifier="G3" required="true"/></span> en nog <span><gap identifier="G4" required="true"/></span> gaten</p>
                </gapMatchInteraction>
            </div>
        </div>
    </div>
</itemBody>


    Private _itemBody2 As XElement =
       <itemBody xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p2">
           <div class="content">
               <div>
                   <div>
                       <gapMatchInteraction responseIdentifier="gapMatchController" shuffle="false">
                           <gapText identifier="A" matchMax="1">KeuzeA</gapText>
                           <gapText identifier="B" matchMax="1">KeuzeB</gapText>
                           <gapText identifier="C" matchMax="1">KeuzeC</gapText>
                           <p id="c1-id-11">A juist <span>
                                   <gap identifier="I77e2abe1-4fe4-4c34-a9af-6f6900868ecf" required="true"/>
                               </span>    C juist <span>
                                   <gap identifier="I9aeb153d-aaca-4390-a35a-63692cb03035" required="true"/>
                               </span>    B juist <span>
                                   <gap identifier="I4174cc03-b6e0-4eab-8870-9973f6cca0d4" required="true"/>
                               </span>
                           </p>
                       </gapMatchInteraction>
                   </div>
               </div>
           </div>
       </itemBody>

    Private _itemBody3 As XElement = <itemBody xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p2">
                                         <div class="content">
                                             <div>
                                                 <div id="gapmatch">
                                                     <gapMatchInteraction id="gapMatchScoring" responseIdentifier="gapMatchController" shuffle="false">
                                                         <gapText identifier="A" matchMax="1"/>
                                                         <gapText identifier="B" matchMax="1"/>
                                                         <p id="c1-id-11">Op een dag was De Bende Van Madrid weer ober zee aan het varen. Het was toen 18 maart 1965.<br id="c1-id-12"/>Plots zag Dirk een Matroospiraat, een fles <strong id="c1-id-13">(A >) <span>
                                                                     <gap identifier="I1b86db4d-9de2-4b81-96a2-b20766506894" required="true"/>
                                                                 </span>
                                                             </strong> drijven<strong id="c1-id-14"> (B >)</strong>
                                                             <span>
                                                                 <gap identifier="I48433d84-039d-4b26-9bf8-226cdef1ffa4" required="true"/>
                                                             </span>in het water <strong id="c1-id-15">(C >)</strong>
                                                             <span>
                                                                 <gap identifier="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549" required="true"/>
                                                             </span> met behulp van nog enkele Matroospiraten, konden ze de fles aan boord krijgen. Er zat duidelijk een blad papier in, dat leek o een schatkaart.</p>
                                                         <p id="c1-id-16"> </p>
                                                         <p id="c1-id-17">
                                                             <strong id="c1-id-18">Vraag: Sleep de punt naar de juiste plek in de tekst.</strong>
                                                         </p>
                                                     </gapMatchInteraction>
                                                 </div>
                                             </div>
                                         </div>
                                     </itemBody>


    Private _finding1 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Polytomous">
            <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G4" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding2 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding3 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding4 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Polytomous">
            <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G1" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G2" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G4" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _finding5 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
            <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G4" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding6 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding7 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
            <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G1" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G2" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="G4" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _finding8 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="I9aeb153d-aaca-4390-a35a-63692cb03035-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I9aeb153d-aaca-4390-a35a-63692cb03035" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I77e2abe1-4fe4-4c34-a9af-6f6900868ecf-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I77e2abe1-4fe4-4c34-a9af-6f6900868ecf" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4174cc03-b6e0-4eab-8870-9973f6cca0d4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4174cc03-b6e0-4eab-8870-9973f6cca0d4" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding9 As XElement =
  <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
      <keyFact id="I1b86db4d-9de2-4b81-96a2-b20766506894-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
          <keyValue domain="I1b86db4d-9de2-4b81-96a2-b20766506894" occur="1">
              <stringValue>
                  <typedValue>A</typedValue>
              </stringValue>
          </keyValue>
      </keyFact>
      <keyFact id="I48433d84-039d-4b26-9bf8-226cdef1ffa4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
          <keyValue domain="I48433d84-039d-4b26-9bf8-226cdef1ffa4" occur="1">
              <stringValue>
                  <typedValue>B</typedValue>
              </stringValue>
          </keyValue>
      </keyFact>
      <keyFact id="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
          <keyValue domain="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549" occur="1">
              <noValue/>
          </keyValue>
      </keyFact>
  </keyFinding>

    Private _finding10 As XElement =
     <keyFinding id="gapMatchController" scoringMethod="Polytomous">
         <keyFact id="I1b86db4d-9de2-4b81-96a2-b20766506894-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
             <keyValue domain="I1b86db4d-9de2-4b81-96a2-b20766506894" occur="1">
                 <stringValue>
                     <typedValue>A</typedValue>
                 </stringValue>
             </keyValue>
         </keyFact>
         <keyFact id="I48433d84-039d-4b26-9bf8-226cdef1ffa4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
             <keyValue domain="I48433d84-039d-4b26-9bf8-226cdef1ffa4" occur="1">
                 <stringValue>
                     <typedValue>B</typedValue>
                 </stringValue>
             </keyValue>
         </keyFact>
         <keyFact id="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
             <keyValue domain="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549" occur="1">
                 <noValue/>
             </keyValue>
         </keyFact>
     </keyFinding>



    Private _responseProcessing1 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">A G1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B G2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">B G1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">A G2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="directedPair">C G3</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="directedPair">D G4</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">A G1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B G2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">B G1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">A G2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">C G3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">D G4</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">D G3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C G4</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing3 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">A G1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B G2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C G3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">D G4</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">D G1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C G2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B G3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">A G4</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing4 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="directedPair">A G1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="directedPair">B G2</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="directedPair">C G3</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="directedPair">D G4</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing5 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <or>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">A G1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">B G2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">B G1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">A G2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                        <member>
                            <baseValue baseType="directedPair">C G3</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">D G4</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing6 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <or>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">A G1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">B G2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">B G1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">A G2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                        <or>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">C G3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">D G4</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">D G3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">C G4</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing7 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <member>
                            <baseValue baseType="directedPair">A G1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">B G2</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">C G3</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">D G4</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing8 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <member>
                            <baseValue baseType="directedPair">A I77e2abe1-4fe4-4c34-a9af-6f6900868ecf</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">C I9aeb153d-aaca-4390-a35a-63692cb03035</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">B I4174cc03-b6e0-4eab-8870-9973f6cca0d4</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing9 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <member>
                            <baseValue baseType="directedPair">A G1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">B G2</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <and>
                            <not>
                                <or>
                                    <member>
                                        <baseValue baseType="directedPair">A G3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                    <member>
                                        <baseValue baseType="directedPair">B G3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </or>
                            </not>
                        </and>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing10 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="directedPair">A G1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="directedPair">B G2</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <and>
                        <not>
                            <or>
                                <member>
                                    <baseValue baseType="directedPair">A G3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">B G3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </or>
                        </not>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>
End Class
