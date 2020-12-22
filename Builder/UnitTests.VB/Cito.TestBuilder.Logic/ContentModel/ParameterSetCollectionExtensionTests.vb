Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass>
Public Class ParameterSetCollectionExtensionTests

    <TestMethod(), TestCategory("Logic")>
    Sub GetParametersWithResourcesInGapMatchItemWithScoringParameterTest()
        Dim item As AssessmentItem = SerializeHelper.XmlDeserializeFromString(Of AssessmentItem)(_item1.ToString)

        Dim parameterList = item.Parameters.GetParametersWithResources()

        Assert.AreEqual(7, parameterList.Count, "Item should have seven parameters with resources")
        Assert.IsTrue(CheckForXhtmlParameter(parameterList, "itemInlineInput"))
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Sub GetResourcesInGapMatchItemWithScoringParameterTest()
        Dim item As AssessmentItem = SerializeHelper.XmlDeserializeFromString(Of AssessmentItem)(_item1.ToString)

        Dim resources = item.GetResources()

        Assert.AreEqual(2, resources.Count, "Item should have two template resources")
        Assert.IsTrue(resources.Contains("InlineGapMatchLayoutTemplate"), String.Format("Item should have template {0} as a resource", "InlineGapMatchLayoutTemplate"))
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Sub GetParametersWithResourcesInGraphicGapMatchItemWithScoringParameterTest()
        Dim item As AssessmentItem = SerializeHelper.XmlDeserializeFromString(Of AssessmentItem)(_item2.ToString)

        Dim parameterList = item.Parameters.GetParametersWithResources()

        Assert.AreEqual(9, parameterList.Count, "Item should have nine parameters with resources")
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Sub GetResourcesInGraphicGapMatchItemWithScoringParameterTest()
        Dim item As AssessmentItem = SerializeHelper.XmlDeserializeFromString(Of AssessmentItem)(_item2.ToString)

        Dim resources = item.GetResources()

        Assert.AreEqual(4, resources.Count, "Item should have four resources")
        Assert.IsTrue(resources.Contains("Cito.Generic.GraphicGapMatch.SC"), String.Format("Item should have template {0} as a resource", "Cito.Generic.GraphicGapMatch.SC"))
        Assert.IsTrue(resources.Contains("basisplaatje.jpg"), String.Format("Item should have image {0} as a resource", "basisplaatje.jpg"))
        Assert.IsTrue(resources.Contains("test_plaatje.png"), String.Format("Item should have image {0} as a resource", "test_plaatje.png"))
        Assert.IsTrue(resources.Contains("test_formule.png"), String.Format("Item should have image {0} as a resource", "test_formule.png"))
    End Sub

    Private Function CheckForXhtmlParameter(parameterList As List(Of ParameterBase), parameterName As String) As Boolean
        For Each prm As ParameterBase In parameterList
            If TypeOf prm Is XHtmlParameter AndAlso prm.Name.Equals(parameterName) Then Return True
        Next
        Return False
    End Function


    ReadOnly _item1 As XElement =
        <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="dtt-en-vm-0200-SC" title="Frankrijk" layoutTemplateSrc="Cito.Generic.GapMatch.Inline.DC">
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="If8d49acf-9059-4c11-9761-14cc7e799c10" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="If8d49acf-9059-4c11-9761-14cc7e799c10" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="gapMatchController" scoringMethod="None">
                        <conceptFact id="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="EN-SC-4"/>
                                <concept value="2" code="EN-SC-4.1"/>
                                <concept value="0" code="EN-SC-2"/>
                                <concept value="1" code="EN-SC-3"/>
                                <concept value="1" code="EN-SC-3.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="EN-SC-1"/>
                                <concept value="1" code="EN-SC-4"/>
                                <concept value="1" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7[*]" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="EN-SC-1"/>
                                <concept value="0" code="EN-SC-4"/>
                                <concept value="0" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0[*]" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="EN-SC-4"/>
                                <concept value="0" code="EN-SC-4.1"/>
                                <concept value="0" code="EN-SC-2"/>
                                <concept value="0" code="EN-SC-3"/>
                                <concept value="0" code="EN-SC-3.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFactSet>
                            <conceptFact id="If8d49acf-9059-4c11-9761-14cc7e799c10" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="EN-SC-4"/>
                                <concept value="1" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="If8d49acf-9059-4c11-9761-14cc7e799c10" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="EN-SC-4"/>
                                <concept value="1" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="If8d49acf-9059-4c11-9761-14cc7e799c10[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10[*]" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9[*]" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="EN-SC-4"/>
                                <concept value="0" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>
            <parameters>
                <parameterSet id="entireItem">
                    <listedparameter name="itemStyle">Default</listedparameter>
                    <booleanparameter name="dualColumnLayout">True</booleanparameter>
                    <booleanparameter name="showCalculatorButton"/>
                    <booleanparameter name="displayVerklankingOnTheRight">True</booleanparameter>
                    <collectionparameter name="numberOfAudioContentItems">
                        <definition id="">
                            <resourceparameter name="audiocontent"/>
                            <xhtmlparameter name="description"/>
                        </definition>
                    </collectionparameter>
                    <texttospeechparameter name="verklankingLinks"/>
                    <texttospeechparameter name="verklankingRechts"/>
                    <xhtmlparameter name="leftBody"/>
                    <xhtmlresourceparameter name="leftSource"/>
                    <integerparameter name="sourceHeight">200</integerparameter>
                    <integerparameter name="sourcePositionTop">0</integerparameter>
                    <xhtmlparameter name="itemBody">
                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Drag and drop</p>
                    </xhtmlparameter>
                    <xhtmlparameter name="itemQuestion">
                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                            <strong id="c1-id-12">Zet de zinsdelen in de juiste volgorde. Het eerste woord staat er al.</strong>
                        </p>
                    </xhtmlparameter>
                    <gapMatchScoringParameter name="gapMatchScoring" findingOverride="gapMatchController">
                        <subparameterset id="A">
                            <gapTextParameter name="gapText" matchMax="1">am going</gapTextParameter>
                        </subparameterset>
                        <subparameterset id="B">
                            <gapTextParameter name="gapText" matchMax="1">in July</gapTextParameter>
                        </subparameterset>
                        <subparameterset id="C">
                            <gapTextParameter name="gapText" matchMax="1">on holiday</gapTextParameter>
                        </subparameterset>
                        <subparameterset id="D">
                            <gapTextParameter name="gapText" matchMax="1">to France</gapTextParameter>
                        </subparameterset>
                        <definition id="">
                            <gapTextParameter name="gapText" matchMax="1"/>
                        </definition>
                        <xhtmlParameter name="itemInlineInput">
                            <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">I <cito:InlineElement id="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="entireItem"><cito:plaintextparameter name="inlineGapMatchId">Ied60a714-2bc7-4c12-badb-8afc5f9d30f7</cito:plaintextparameter><cito:plaintextparameter name="inlineGapMatchLabel">1</cito:plaintextparameter></cito:parameterSet></cito:parameters></cito:InlineElement> <cito:InlineElement id="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="entireItem"><cito:plaintextparameter name="inlineGapMatchId">Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0</cito:plaintextparameter><cito:plaintextparameter name="inlineGapMatchLabel">2</cito:plaintextparameter><cito:booleanparameter name="editSize">True</cito:booleanparameter><cito:integerparameter name="width"/><cito:integerparameter name="height"/></cito:parameterSet></cito:parameters></cito:InlineElement> <cito:InlineElement id="If8d49acf-9059-4c11-9761-14cc7e799c10" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="entireItem"><cito:plaintextparameter name="inlineGapMatchId">If8d49acf-9059-4c11-9761-14cc7e799c10</cito:plaintextparameter><cito:plaintextparameter name="inlineGapMatchLabel">3</cito:plaintextparameter><cito:booleanparameter name="editSize">True</cito:booleanparameter><cito:integerparameter name="width"/><cito:integerparameter name="height"/></cito:parameterSet></cito:parameters></cito:InlineElement> <cito:InlineElement id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester"><cito:parameters><cito:parameterSet id="entireItem"><cito:plaintextparameter name="inlineGapMatchId">Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9</cito:plaintextparameter><cito:plaintextparameter name="inlineGapMatchLabel">4</cito:plaintextparameter></cito:parameterSet></cito:parameters></cito:InlineElement>. </p>
                        </xhtmlParameter>
                    </gapMatchScoringParameter>
                    <xhtmlparameter name="itemInlineInput"/>
                    <xhtmlparameter name="itemGeneral"/>
                    <collectionparameter name="choices">
                        <definition id="">
                            <plaintextparameter name="choice"/>
                            <integerparameter name="nrOfConnections"/>
                        </definition>
                    </collectionparameter>
                </parameterSet>
            </parameters>
        </assessmentItem>

    ReadOnly _item2 As XElement =
        <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TEST_Generic-GraphicGapMatch-SC" title="TEST_Generic-GraphicGapMatch-SC" layoutTemplateSrc="Cito.Generic.GraphicGapMatch.SC">
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>
            <parameters>
                <parameterSet id="entireItem">
                    <booleanparameter name="dualColumnLayout">False</booleanparameter>
                    <booleanparameter name="isCategorizationItem">False</booleanparameter>
                    <booleanparameter name="showCalculatorButton"/>
                    <xhtmlparameter name="leftBody"/>
                    <xhtmlresourceparameter name="leftSource"/>
                    <integerparameter name="sourceHeight">200</integerparameter>
                    <integerparameter name="sourcePositionTop">0</integerparameter>
                    <xhtmlparameter name="itemBody">
                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                            <strong id="c1-id-12">Sleep de woorden naar het juiste deel van het lichaam.</strong>
                        </p>
                    </xhtmlparameter>
                    <graphGapMatchScoringParameter name="graphicGapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController" iscategorizationvariant="false">
                        <subparameterset id="A">
                            <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="Image" enteredText="">test_plaatje.png</gapImageParameter>
                        </subparameterset>
                        <subparameterset id="B">
                            <gapImageParameter name="alternative" matchMax="1" width="60" height="20" contentType="Text" enteredText="chest"/>
                        </subparameterset>
                        <subparameterset id="C">
                            <gapImageParameter name="alternative" matchMax="1" width="60" height="20" contentType="Text" enteredText="thigh"/>
                        </subparameterset>
                        <subparameterset id="D">
                            <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="FormulaImage" enteredText="">test_formule.png</gapImageParameter>
                        </subparameterset>
                        <definition id="">
                            <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="Image"/>
                        </definition>
                        <areaparameter name="itemQuestionArea">
                            <subparameterset id="A">
                                <resourceparameter name="clickableImage">basisplaatje.jpg</resourceparameter>
                            </subparameterset>
                            <definition id="">
                                <resourceparameter name="clickableImage"/>
                            </definition>
                            <Shapes>
                                <Rectangle id="C">
                                    <TopLeft>
                                        <X>122</X>
                                        <Y>203</Y>
                                    </TopLeft>
                                    <BottomRight>
                                        <X>258</X>
                                        <Y>233</Y>
                                    </BottomRight>
                                </Rectangle>
                                <Rectangle id="A">
                                    <TopLeft>
                                        <X>124</X>
                                        <Y>142</Y>
                                    </TopLeft>
                                    <BottomRight>
                                        <X>260</X>
                                        <Y>172</Y>
                                    </BottomRight>
                                </Rectangle>
                                <Rectangle id="B">
                                    <TopLeft>
                                        <X>122</X>
                                        <Y>93</Y>
                                    </TopLeft>
                                    <BottomRight>
                                        <X>258</X>
                                        <Y>123</Y>
                                    </BottomRight>
                                </Rectangle>
                                <Rectangle id="D">
                                    <TopLeft>
                                        <X>123</X>
                                        <Y>301</Y>
                                    </TopLeft>
                                    <BottomRight>
                                        <X>259</X>
                                        <Y>331</Y>
                                    </BottomRight>
                                </Rectangle>
                            </Shapes>
                        </areaparameter>
                    </graphGapMatchScoringParameter>
                    <areaparameter name="itemQuestionArea">
                        <definition id="">
                            <resourceparameter name="clickableImage"/>
                        </definition>
                        <Shapes/>
                    </areaparameter>
                    <integerparameter name="nrOfAnswers">1</integerparameter>
                    <collectionparameter name="numberOfImages">
                        <definition id="">
                            <resourceparameter name="imgSource"/>
                            <plaintextparameter name="imgSourceText"/>
                            <xhtmlparameter name="imgSourceFormula"/>
                            <booleanparameter name="editSize"/>
                            <integerparameter name="width"/>
                            <integerparameter name="height"/>
                            <integerparameter name="nrOfConnections"/>
                        </definition>
                    </collectionparameter>
                    <listedparameter name="itemOrientation">vertical</listedparameter>
                    <listedparameter name="nrAlternativesPerLine">1</listedparameter>
                    <integerparameter name="percentageTransparency">0</integerparameter>
                    <xhtmlparameter name="itemGeneral"/>
                </parameterSet>
            </parameters>
        </assessmentItem>


End Class
