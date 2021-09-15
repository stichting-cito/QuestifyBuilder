
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
    Inherits QTI_Base.ResponseProcessingGapmatchTestsBase

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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
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
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>
End Class
