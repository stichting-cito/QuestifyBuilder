
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports System.Xml.Serialization
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.Logic.QTI.Helpers.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingGapmatchTests
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
        Public Sub GapMatchItemWithPolytomousScoringWhenCreatingResponseProcessingReturnsMapResponseResponseProcessingTemplate()
            GetResponseProcessingTest(_itemBody1, _finding4, PublicationTestHelper.ResponseProcessingTemplateMapResponse, GetGapMatchScoringParams_1, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GapMatchItemWithDichotousScoringWhenCreatingResponseProcessingReturnsMatchCorrectResponseProcessingTemplate()
            GetResponseProcessingTest(_itemBody1, _finding7, PublicationTestHelper.ResponseProcessingTemplateMatchCorrect, GetGapMatchScoringParams_1, False)
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
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(itemBody)
            Dim scoringHelper = New CombinedScoringConverter(scoringPrms)

            Dim finding As KeyFinding = findingElement.Deserialize(Of KeyFinding)()
            Dim findingIndex As Integer = 0
            Dim s = New Solution
            s.Findings.Add(finding)

            Dim useResponseProcessingTemplate = QTI30CombinedScoringHelper.ShouldUseResponseProcessingTemplate(s, scoringPrms)
            Dim processor = New ResponseProcessing(responseIdentifierAttributeList, s, finding, findingIndex, scoringPrms, New CombinedScoringConverter, False, useResponseProcessingTemplate)
            Dim assessmentItemType As New AssessmentItemType() With {.qtiitembody = CType(ChainHandlerHelper.StringToObject(itemBody.ToString, GetType(ItemBodyType)), ItemBodyType)}

            'Act
            Dim result = processor.GetProcessing().ToXmlDocument
            If fixGapNames Then
                assessmentItemType.qtiresponseprocessing = CType(ChainHandlerHelper.StringToObject(result.OuterXml.Replace("<qti-response-processing>", "<qti-response-processing xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://www.imsglobal.org/xsd/imsqtiasi_v3p0"">"), GetType(ResponseProcessingType)), ResponseProcessingType)
                Dim itemDocument = ChainHandlerHelper.ObjectToXmlDocument(assessmentItemType, New XmlSerializerNamespaces())
                scoringHelper.UpdateDocument(s, itemDocument, Nothing, Nothing)
                result = XElement.Parse(XDocument.Parse(itemDocument.OuterXml).Descendants.FirstOrDefault(Function(d) d.Name.LocalName = "qti-response-processing").ToString.Replace("xmlns=""http://www.imsglobal.org/xsd/imsqtiasi_v3p0""", String.Empty)).ToXmlDocument
            End If

            'Assert
            Assert.IsTrue(UnitTestHelper.AreSame(responseProcessingElement, result))
        End Sub

        Private _itemBody1 As XElement =
            <qti-item-body xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.imsglobal.org/xsd/imsqtiasi_v3p0">
                <div class="content">
                    <div>
                        <div>
                            <qti-gap-match-interaction response-identifier="gapMatchController" shuffle="false">
                                <qti-gap-text identifier="A" match-max="1">tekst 1</qti-gap-text>
                                <qti-gap-text identifier="B" match-max="1">tekst 2</qti-gap-text>
                                <qti-gap-text identifier="C" match-max="1">tekst 3</qti-gap-text>
                                <qti-gap-text identifier="D" match-max="1">tekst 4</qti-gap-text>
                                <p id="c1-id-11">Tekst <span><qti-gap identifier="G1" required="true"/></span> met een <span><qti-gap identifier="G2" required="true"/></span> gaten <span><qti-gap identifier="G3" required="true"/></span> en nog <span><qti-gap identifier="G4" required="true"/></span> gaten</p>
                            </qti-gap-match-interaction>
                        </div>
                    </div>
                </div>
            </qti-item-body>

        Private _itemBody2 As XElement =
           <qti-item-body xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.imsglobal.org/xsd/imsqtiasi_v3p0">
               <div class="content">
                   <div>
                       <div>
                           <qti-gap-match-interaction response-identifier="gapMatchController" shuffle="false">
                               <qti-gap-text identifier="A" match-max="1">KeuzeA</qti-gap-text>
                               <qti-gap-text identifier="B" match-max="1">KeuzeB</qti-gap-text>
                               <qti-gap-text identifier="C" match-max="1">KeuzeC</qti-gap-text>
                               <p id="c1-id-11">A juist <span>
                                       <qti-gap identifier="I77e2abe1-4fe4-4c34-a9af-6f6900868ecf" required="true"/>
                                   </span>    C juist <span>
                                       <qti-gap identifier="I9aeb153d-aaca-4390-a35a-63692cb03035" required="true"/>
                                   </span>    B juist <span>
                                       <qti-gap identifier="I4174cc03-b6e0-4eab-8870-9973f6cca0d4" required="true"/>
                                   </span>
                               </p>
                           </qti-gap-match-interaction>
                       </div>
                   </div>
               </div>
           </qti-item-body>

        Private _itemBody3 As XElement =
            <qti-item-body xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.imsglobal.org/xsd/imsqtiasi_v3p0">
                <div class="content">
                    <div>
                        <div id="gapmatch">
                            <qti-gap-match-interaction id="gapMatchScoring" response-identifier="gapMatchController" shuffle="false">
                                <qti-gap-text identifier="A" match-max="1"/>
                                <qti-gap-text identifier="B" match-max="1"/>
                                <p id="c1-id-11">Op een dag was De Bende Van Madrid weer ober zee aan het varen. Het was toen 18 maart 1965.<br id="c1-id-12"/>Plots zag Dirk een Matroospiraat, een fles <strong id="c1-id-13">(A >) <span>
                                            <qti-gap identifier="I1b86db4d-9de2-4b81-96a2-b20766506894" required="true"/>
                                        </span>
                                    </strong> drijven<strong id="c1-id-14"> (B >)</strong>
                                    <span>
                                        <qti-gap identifier="I48433d84-039d-4b26-9bf8-226cdef1ffa4" required="true"/>
                                    </span>in het water <strong id="c1-id-15">(C >)</strong>
                                    <span>
                                        <qti-gap identifier="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549" required="true"/>
                                    </span> met behulp van nog enkele Matroospiraten, konden ze de fles aan boord krijgen. Er zat duidelijk een blad papier in, dat leek o een schatkaart.</p>
                                <p id="c1-id-16"> </p>
                                <p id="c1-id-17">
                                    <strong id="c1-id-18">Vraag: Sleep de punt naar de juiste plek in de tekst.</strong>
                                </p>
                            </qti-gap-match-interaction>
                        </div>
                    </div>
                </div>
            </qti-item-body>

        Private _responseProcessing1 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A G1</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B G2</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B G1</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A G2</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                        </qti-or>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-member>
                            <qti-base-value base-type="directedPair">C G3</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-member>
                            <qti-base-value base-type="directedPair">D G4</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
            </qti-response-processing>

        Private _responseProcessing2 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A G1</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B G2</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B G1</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A G2</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                        </qti-or>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">C G3</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">D G4</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">D G3</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">C G4</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                        </qti-or>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
            </qti-response-processing>

        Private _responseProcessing3 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A G1</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B G2</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">C G3</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">D G4</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">D G1</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">C G2</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B G3</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A G4</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                        </qti-or>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
            </qti-response-processing>

        Private _responseProcessing5 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-or>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">A G1</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">B G2</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">B G1</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">A G2</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                            </qti-or>
                            <qti-member>
                                <qti-base-value base-type="directedPair">C G3</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">D G4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-and>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-gte>
                            <qti-variable identifier="SCORE"/>
                            <qti-base-value base-type="float">1</qti-base-value>
                        </qti-gte>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-base-value base-type="float">1</qti-base-value>
                        </qti-set-outcome-value>
                    </qti-response-if>
                    <qti-response-else>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-base-value base-type="float">0</qti-base-value>
                        </qti-set-outcome-value>
                    </qti-response-else>
                </qti-response-condition>
            </qti-response-processing>

        Private _responseProcessing6 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-or>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">A G1</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">B G2</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">B G1</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">A G2</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                            </qti-or>
                            <qti-or>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">C G3</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">D G4</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">D G3</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">C G4</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                            </qti-or>
                        </qti-and>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-gte>
                            <qti-variable identifier="SCORE"/>
                            <qti-base-value base-type="float">1</qti-base-value>
                        </qti-gte>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-base-value base-type="float">1</qti-base-value>
                        </qti-set-outcome-value>
                    </qti-response-if>
                    <qti-response-else>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-base-value base-type="float">0</qti-base-value>
                        </qti-set-outcome-value>
                    </qti-response-else>
                </qti-response-condition>
            </qti-response-processing>

        Private _responseProcessing8 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-member>
                                <qti-base-value base-type="directedPair">A I77e2abe1-4fe4-4c34-a9af-6f6900868ecf</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">C I9aeb153d-aaca-4390-a35a-63692cb03035</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">B I4174cc03-b6e0-4eab-8870-9973f6cca0d4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-and>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-gte>
                            <qti-variable identifier="SCORE"/>
                            <qti-base-value base-type="float">1</qti-base-value>
                        </qti-gte>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-base-value base-type="float">1</qti-base-value>
                        </qti-set-outcome-value>
                    </qti-response-if>
                    <qti-response-else>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-base-value base-type="float">0</qti-base-value>
                        </qti-set-outcome-value>
                    </qti-response-else>
                </qti-response-condition>
            </qti-response-processing>

        Private _responseProcessing9 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-member>
                                <qti-base-value base-type="directedPair">A G1</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">B G2</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-and>
                                <qti-not>
                                    <qti-or>
                                        <qti-member>
                                            <qti-base-value base-type="directedPair">A G3</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                        <qti-member>
                                            <qti-base-value base-type="directedPair">B G3</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-or>
                                </qti-not>
                            </qti-and>
                        </qti-and>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-gte>
                            <qti-variable identifier="SCORE"/>
                            <qti-base-value base-type="float">1</qti-base-value>
                        </qti-gte>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-base-value base-type="float">1</qti-base-value>
                        </qti-set-outcome-value>
                    </qti-response-if>
                    <qti-response-else>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-base-value base-type="float">0</qti-base-value>
                        </qti-set-outcome-value>
                    </qti-response-else>
                </qti-response-condition>
            </qti-response-processing>

        Private _responseProcessing10 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-member>
                            <qti-base-value base-type="directedPair">A G1</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-member>
                            <qti-base-value base-type="directedPair">B G2</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-not>
                                <qti-or>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">A G3</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="directedPair">B G3</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-or>
                            </qti-not>
                        </qti-and>
                        <qti-set-outcome-value identifier="SCORE">
                            <qti-sum>
                                <qti-base-value base-type="float">1</qti-base-value>
                                <qti-variable identifier="SCORE"/>
                            </qti-sum>
                        </qti-set-outcome-value>
                    </qti-response-if>
                </qti-response-condition>
            </qti-response-processing>
    End Class

End Namespace