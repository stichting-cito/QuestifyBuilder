
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class GapMatchRichTextParameterSerializationTests : Inherits SerializationTestBase

    <TestMethod(), TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InAssessmentItem_Test()
        Dim xmlData = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                          <solution>
                              <keyFindings/>
                              <aspectReferences/>
                          </solution>
                          <parameters>
                              <parameterSet id="id_1">
                                  <gapMatchRichTextScoringParameter name="scoreParam" findingOverride="gapMatchController"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>

        Dim result = Deserialize(Of AssessmentItem)(xmlData)
        Dim param = CType(result.Parameters(0).InnerParameters(0), GapMatchRichTextScoringParameter)

        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(GapMatchRichTextScoringParameter))
        Assert.AreEqual("gapMatchController", param.FindingOverride)
        Assert.AreEqual("scoreParam", param.Name)
    End Sub

    <TestMethod(), TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InRealWorldAssessmentItem_Test()

        Dim result = Deserialize(Of AssessmentItem)(_serializedGapMatchRichTextItem)

        Assert.IsInstanceOfType(result.Parameters(1).InnerParameters(10), GetType(GapMatchRichTextScoringParameter))

        Dim result2 = DoSerialize(result)
        Assert.AreEqual(_serializedGapMatchRichTextItem.ToString(), result2.ToString())
    End Sub

    <TestMethod(), TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InRealWorldAssessmentItem_Test_Alternatives()

        Dim assessmentItem = Deserialize(Of AssessmentItem)(_serializedGapMatchRichTextItem)
        Dim gapMatchRichTextScoringParameter = DirectCast(assessmentItem.Parameters(1).InnerParameters(10), GapMatchRichTextScoringParameter)

        Assert.AreEqual(2, gapMatchRichTextScoringParameter.AlternativesCount)
        Assert.IsInstanceOfType(gapMatchRichTextScoringParameter.Value.First().InnerParameters.First(), GetType(GapTextRichTextParameter))
    End Sub



    ReadOnly _serializedGapMatchRichTextItem As XElement =
        <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="GapMatchRichText1" title="GapMatchRichText1" layoutTemplateSrc="Cito.CTE.GapMatch.Inline.SC">
            <solution>
                <keyFindings/>
                <aspectReferences/>
            </solution>
            <parameters>
                <parameterSet id="styling">
                    <listedparameter name="itemStyle">Default</listedparameter>
                </parameterSet>
                <parameterSet id="entireItem">
                    <booleanparameter name="dualColumnLayout">False</booleanparameter>
                    <booleanparameter name="showCalculatorButton"/>
                    <booleanparameter name="displayVerklankingOnTheRight">True</booleanparameter>
                    <collectionparameter name="numberOfAudioContentItems">
                        <definition id="">
                            <resourceparameter name="audiocontent"/>
                            <xhtmlparameter name="description"/>
                        </definition>
                    </collectionparameter>
                    <xhtmlparameter name="leftBody"/>
                    <xhtmlresourceparameter name="leftSource"/>
                    <integerparameter name="sourceHeight">200</integerparameter>
                    <integerparameter name="sourcePositionTop">0</integerparameter>
                    <xhtmlparameter name="itemBody"/>
                    <xhtmlparameter name="itemQuestion">
                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml"> </p>
                    </xhtmlparameter>
                    <gapMatchRichTextScoringParameter name="gapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController">
                        <subparameterset id="A">
                            <gapTextRichTextParameter name="gapTextRichText" matchMax="1">
                                <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                    <cito:InlineElement id="I9583bce3-6721-453e-91e0-d806abef4b09" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                        <cito:parameters>
                                            <cito:parameterSet id="entireItem">
                                                <cito:plaintextparameter name="imageId"/>
                                                <cito:plaintextparameter name="largeImageId"/>
                                                <cito:plaintextparameter name="windowId"/>
                                                <cito:plaintextparameter name="windowContentId"/>
                                                <cito:resourceparameter name="source">MFI_20141112_13_48_53_798.png</cito:resourceparameter>
                                                <cito:booleanparameter name="useBorder">False</cito:booleanparameter>
                                                <cito:booleanparameter name="showPopup">False</cito:booleanparameter>
                                                <cito:resourceparameter name="largeImage"/>
                                                <cito:plaintextparameter name="popupDescription"/>
                                                <cito:booleanparameter name="editLargePosition">False</cito:booleanparameter>
                                                <cito:integerparameter name="largeXpos"/>
                                                <cito:integerparameter name="largeYpos"/>
                                                <cito:booleanparameter name="largeImageModal">False</cito:booleanparameter>
                                                <cito:booleanparameter name="largeImageResizable">False</cito:booleanparameter>
                                                <cito:booleanparameter name="editLargePopupSize">False</cito:booleanparameter>
                                                <cito:integerparameter name="largePopupWidth"/>
                                                <cito:integerparameter name="largePopupHeight"/>
                                            </cito:parameterSet>
                                        </cito:parameters>
                                    </cito:InlineElement> </p>
                            </gapTextRichTextParameter>
                        </subparameterset>
                        <subparameterset id="B">
                            <gapTextRichTextParameter name="gapTextRichText" matchMax="1">
                                <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                    <cito:InlineElement id="I7aea5242-b670-45a0-97c1-6e6a36e4e7d8" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                        <cito:parameters>
                                            <cito:parameterSet id="entireItem">
                                                <cito:plaintextparameter name="imageId"/>
                                                <cito:plaintextparameter name="largeImageId"/>
                                                <cito:plaintextparameter name="windowId"/>
                                                <cito:plaintextparameter name="windowContentId"/>
                                                <cito:resourceparameter name="source">MFI_20141119_10_7_33_25.png</cito:resourceparameter>
                                                <cito:booleanparameter name="useBorder">False</cito:booleanparameter>
                                                <cito:booleanparameter name="showPopup">False</cito:booleanparameter>
                                                <cito:resourceparameter name="largeImage"/>
                                                <cito:plaintextparameter name="popupDescription"/>
                                                <cito:booleanparameter name="editLargePosition">False</cito:booleanparameter>
                                                <cito:integerparameter name="largeXpos"/>
                                                <cito:integerparameter name="largeYpos"/>
                                                <cito:booleanparameter name="largeImageModal">False</cito:booleanparameter>
                                                <cito:booleanparameter name="largeImageResizable">False</cito:booleanparameter>
                                                <cito:booleanparameter name="editLargePopupSize">False</cito:booleanparameter>
                                                <cito:integerparameter name="largePopupWidth"/>
                                                <cito:integerparameter name="largePopupHeight"/>
                                            </cito:parameterSet>
                                        </cito:parameters>
                                    </cito:InlineElement> </p>
                            </gapTextRichTextParameter>
                        </subparameterset>
                        <definition id="">
                            <gapTextRichTextParameter name="gapTextRichText" matchMax="1"/>
                        </definition>
                        <xhtmlParameter name="gapMatchInlineInput">
                            <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                <cito:InlineElement id="I1052501a-4755-4797-ae7b-3d09ae90710a" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineGapMatchId">I1052501a-4755-4797-ae7b-3d09ae90710a</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineGapMatchLabel">gat1</cito:plaintextparameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement> of <cito:InlineElement id="If87ab9cd-acf4-49a8-b5eb-d003d4ec5c04" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineGapMatchId">If87ab9cd-acf4-49a8-b5eb-d003d4ec5c04</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineGapMatchLabel">gat2</cito:plaintextparameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                        </xhtmlParameter>
                    </gapMatchRichTextScoringParameter>
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


End Class
