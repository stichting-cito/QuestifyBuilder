
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass>
Public Class GraphGapMatchScoringParameterTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_GraphGapMatchScoringParameterWidth_Test()

        Dim param = Deserialize(Of GraphGapMatchScoringParameter)(_serializedGraphGapMatchParameter)

        Assert.AreEqual("clickableImage.jpg", CType(param.Area.Value(0).InnerParameters(0), ResourceParameter).Value)

        Assert.AreEqual(2, param.Area.ShapeList.Count)
        Assert.AreEqual("A", param.Area.ShapeList(0).Identifier)
        Assert.AreEqual(100, CType(param.Value(0).InnerParameters(0), GapImageParameter).Height)
        Assert.AreEqual(GapImageParameter.GapImageParameterContentType.FormulaImage, CType(param.Value(0).InnerParameters(0), GapImageParameter).ContentType)
        Assert.AreEqual("A", param.Area.ShapeList(0).Label)

        Assert.AreEqual(4, param.Value.Count)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_GraphGapMatchScoringParameter_Test()

        Dim param = Deserialize(Of GraphGapMatchScoringParameter)(_serializedGraphGapMatchParameter2)

        Assert.AreNotEqual(0, CType(param.Area.Value(0).InnerParameters(0), ResourceParameter).Width)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_GraphGapMatchScoringParameter_InItem_Test()

        Dim item = Deserialize(Of AssessmentItem)(_serializedGraphicalGapMatchItem)
        Dim param = CType(item.Parameters(0).InnerParameters(9), GraphGapMatchScoringParameter)

        Assert.AreEqual("clickableImage.jpg", CType(param.Area.Value(0).InnerParameters(0), ResourceParameter).Value)

        Assert.AreEqual(2, param.Area.ShapeList.Count)
        Assert.AreEqual("A", param.Area.ShapeList(0).Identifier)
        Assert.AreEqual("A", param.Area.ShapeList(0).Label)

        Assert.AreEqual(4, param.Value.Count)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Transform_GraphGapMatchScoringParameter_Test()

        Dim item = Deserialize(Of AssessmentItem)(_serializedGraphicalGapMatchItem)
        Dim param = CType(item.Parameters(0).InnerParameters(9), GraphGapMatchScoringParameter)
        param.IsCategorizationVariant = True

        Assert.AreEqual(False, param.CanTransform)
        param.DesignerSettings.Clear()

        param.IsCategorizationVariant = False
        Assert.AreEqual(True, param.CanTransform)
    End Sub

    Private ReadOnly _serializedGraphGapMatchParameter2 as XElement = <GraphGapMatchScoringParameter name="graphicGapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController" iscategorizationvariant="false">
                                                                          <subparameterset id="A">
                                                                              <gapImageParameter name="alternative" height="29" width="50" editSize="true" matchMax="1" contentType="Image" enteredText="">L_Z3_FR_Les_A0148_00013_Bild13b.png</gapImageParameter>
                                                                          </subparameterset>
                                                                          <definition id="">
                                                                              <gapImageParameter name="alternative" height="0" width="0" matchMax="1" contentType="Image"/>
                                                                          </definition>
                                                                          <areaparameter name="itemQuestionArea">
                                                                              <subparameterset id="A">
                                                                                  <resourceparameter name="clickableImage" height="298" width="400" editSize="true">L_Z3_FR_Les_A0148_00013_Bild11.png</resourceparameter>
                                                                              </subparameterset>
                                                                              <definition id="">
                                                                                  <resourceparameter name="clickableImage"/>
                                                                              </definition>
                                                                              <Shapes>
                                                                                  <Rectangle id="A">
                                                                                      <TopLeft>
                                                                                          <X>9</X>
                                                                                          <Y>12</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>147</X>
                                                                                          <Y>90</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                                  <Rectangle id="B">
                                                                                      <TopLeft>
                                                                                          <X>314</X>
                                                                                          <Y>13</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>452</X>
                                                                                          <Y>91</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                                  <Rectangle id="C">
                                                                                      <TopLeft>
                                                                                          <X>11</X>
                                                                                          <Y>124</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>145</X>
                                                                                          <Y>202</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                                  <Rectangle id="D">
                                                                                      <TopLeft>
                                                                                          <X>314</X>
                                                                                          <Y>123</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>452</X>
                                                                                          <Y>203</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                                  <Rectangle id="E">
                                                                                      <TopLeft>
                                                                                          <X>10</X>
                                                                                          <Y>240</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>146</X>
                                                                                          <Y>316</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                                  <Rectangle id="F">
                                                                                      <TopLeft>
                                                                                          <X>315</X>
                                                                                          <Y>238</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>453</X>
                                                                                          <Y>316</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                                  <Rectangle id="G">
                                                                                      <TopLeft>
                                                                                          <X>11</X>
                                                                                          <Y>356</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>147</X>
                                                                                          <Y>432</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                              </Shapes>
                                                                          </areaparameter>
                                                                      </GraphGapMatchScoringParameter>

    Private ReadOnly _serializedGraphGapMatchParameter As XElement = <GraphGapMatchScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" ControllerId="ggm1">
                                                                         <subparameterset id="1">
                                                                             <gapImageParameter name="choice" matchMax="1" id="1" width="200" height="100" contentType="FormulaImage">1.png</gapImageParameter>
                                                                         </subparameterset>
                                                                         <subparameterset id="2">
                                                                             <gapImageParameter name="choice" matchMax="1" id="2" width="200" height="100">2.png</gapImageParameter>
                                                                         </subparameterset>
                                                                         <subparameterset id="3">
                                                                             <gapImageParameter name="choice" matchMax="1" id="3" width="200" height="100">3.png</gapImageParameter>
                                                                         </subparameterset>
                                                                         <subparameterset id="4">
                                                                             <gapImageParameter name="choice" matchMax="1" id="4" width="200" height="100">4.png</gapImageParameter>
                                                                         </subparameterset>
                                                                         <definition id="">
                                                                             <gapImageParameter name="choice" matchMax="1"/>
                                                                         </definition>
                                                                         <areaparameter>
                                                                             <subparameterset id="A">
                                                                                 <resourceparameter name="clickableImage">clickableImage.jpg</resourceparameter>
                                                                             </subparameterset>
                                                                             <definition id="">
                                                                                 <resourceparameter name="clickableImage"/>
                                                                             </definition>
                                                                             <Shapes>
                                                                                 <Rectangle id="A" label="A">
                                                                                     <TopLeft>
                                                                                         <X>33</X>
                                                                                         <Y>46</Y>
                                                                                     </TopLeft>
                                                                                     <BottomRight>
                                                                                         <X>155</X>
                                                                                         <Y>85</Y>
                                                                                     </BottomRight>
                                                                                 </Rectangle>
                                                                                 <Rectangle id="B" label="B">
                                                                                     <TopLeft>
                                                                                         <X>352</X>
                                                                                         <Y>47</Y>
                                                                                     </TopLeft>
                                                                                     <BottomRight>
                                                                                         <X>473</X>
                                                                                         <Y>85</Y>
                                                                                     </BottomRight>
                                                                                 </Rectangle>
                                                                             </Shapes>
                                                                         </areaparameter>
                                                                     </GraphGapMatchScoringParameter>

    Private ReadOnly _serializedGraphicalGapMatchItem As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TI_GapMatch" title="TI_GapMatch" layoutTemplateSrc="Cito.Generic.GraphicGapMatch.SC">
                                                                        <solution>
                                                                            <keyFindings>
                                                                                <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                                                                                    <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                        <keyValue domain="A" occur="1">
                                                                                            <stringValue>
                                                                                                <typedValue>B</typedValue>
                                                                                            </stringValue>
                                                                                        </keyValue>
                                                                                    </keyFact>
                                                                                    <keyFact id="C" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                        <keyValue domain="C" occur="1">
                                                                                            <stringValue>
                                                                                                <typedValue>D</typedValue>
                                                                                            </stringValue>
                                                                                        </keyValue>
                                                                                    </keyFact>
                                                                                    <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                        <keyValue domain="B" occur="1">
                                                                                            <stringValue>
                                                                                                <typedValue>C</typedValue>
                                                                                            </stringValue>
                                                                                        </keyValue>
                                                                                    </keyFact>
                                                                                </keyFinding>
                                                                            </keyFindings>
                                                                            <aspectReferences/>
                                                                            <ItemScoreTranslationTable>
                                                                                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                                <ItemScoreTranslationTableEntry rawScore="2" translatedScore="2"/>
                                                                                <ItemScoreTranslationTableEntry rawScore="3" translatedScore="3"/>
                                                                            </ItemScoreTranslationTable>
                                                                        </solution>
                                                                        <parameters>
                                                                            <parameterSet id="entireItem">
                                                                                <booleanparameter name="dualColumnLayout">False</booleanparameter>
                                                                                <booleanparameter name="displayVerklankingOnTheRight">True</booleanparameter>
                                                                                <booleanparameter name="showCalculatorButton">True</booleanparameter>
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
                                                                                <xhtmlparameter name="itemBody">
                                                                                    <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Koppel de juiste woorden met elkaar door de alternatieven naar het juiste vakje te slepen.</p>
                                                                                </xhtmlparameter>
                                                                                <graphGapMatchScoringParameter ControllerId="ggm1">
                                                                                    <subparameterset id="1">
                                                                                        <gapImageParameter name="choice" matchMax="1" id="1" width="200" height="100">1.png</gapImageParameter>
                                                                                    </subparameterset>
                                                                                    <subparameterset id="2">
                                                                                        <gapImageParameter name="choice" matchMax="1" id="2" width="200" height="100">2.png</gapImageParameter>
                                                                                    </subparameterset>
                                                                                    <subparameterset id="3">
                                                                                        <gapImageParameter name="choice" matchMax="1" id="3" width="200" height="100">3.png</gapImageParameter>
                                                                                    </subparameterset>
                                                                                    <subparameterset id="4">
                                                                                        <gapImageParameter name="choice" matchMax="1" id="4" width="200" height="100">4.png</gapImageParameter>
                                                                                    </subparameterset>
                                                                                    <definition id="">
                                                                                        <gapImageParameter name="choice" matchMax="1"/>
                                                                                    </definition>
                                                                                    <areaparameter>
                                                                                        <subparameterset id="A">
                                                                                            <resourceparameter name="clickableImage">clickableImage.jpg</resourceparameter>
                                                                                        </subparameterset>
                                                                                        <definition id="">
                                                                                            <resourceparameter name="clickableImage"/>
                                                                                        </definition>
                                                                                        <Shapes>
                                                                                            <Rectangle id="A" label="A">
                                                                                                <TopLeft>
                                                                                                    <X>33</X>
                                                                                                    <Y>46</Y>
                                                                                                </TopLeft>
                                                                                                <BottomRight>
                                                                                                    <X>155</X>
                                                                                                    <Y>85</Y>
                                                                                                </BottomRight>
                                                                                            </Rectangle>
                                                                                            <Rectangle id="B" label="B">
                                                                                                <TopLeft>
                                                                                                    <X>352</X>
                                                                                                    <Y>47</Y>
                                                                                                </TopLeft>
                                                                                                <BottomRight>
                                                                                                    <X>473</X>
                                                                                                    <Y>85</Y>
                                                                                                </BottomRight>
                                                                                            </Rectangle>
                                                                                        </Shapes>
                                                                                    </areaparameter>
                                                                                </graphGapMatchScoringParameter>
                                                                                <collectionparameter name="numberOfImages">
                                                                                    <subparameterset id="A">
                                                                                        <resourceparameter name="imgSource">Tegel.png</resourceparameter>
                                                                                        <plaintextparameter name="imgSourceText">Tegel</plaintextparameter>
                                                                                        <integerparameter name="nrOfConnections">1</integerparameter>
                                                                                    </subparameterset>
                                                                                    <subparameterset id="B">
                                                                                        <resourceparameter name="imgSource">Steen.png</resourceparameter>
                                                                                        <plaintextparameter name="imgSourceText"/>
                                                                                        <integerparameter name="nrOfConnections">1</integerparameter>
                                                                                    </subparameterset>
                                                                                    <subparameterset id="C">
                                                                                        <resourceparameter name="imgSource">Pan.png</resourceparameter>
                                                                                        <plaintextparameter name="imgSourceText"/>
                                                                                        <integerparameter name="nrOfConnections">1</integerparameter>
                                                                                    </subparameterset>
                                                                                    <subparameterset id="D">
                                                                                        <resourceparameter name="imgSource"/>
                                                                                        <plaintextparameter name="imgSourceText">Test</plaintextparameter>
                                                                                        <integerparameter name="nrOfConnections">1</integerparameter>
                                                                                    </subparameterset>
                                                                                    <subparameterset id="E">
                                                                                        <resourceparameter name="imgSource"/>
                                                                                        <plaintextparameter name="imgSourceText">De beste</plaintextparameter>
                                                                                        <integerparameter name="nrOfConnections">1</integerparameter>
                                                                                    </subparameterset>
                                                                                    <definition id="">
                                                                                        <resourceparameter name="imgSource"/>
                                                                                        <plaintextparameter name="imgSourceText"/>
                                                                                        <integerparameter name="nrOfConnections"/>
                                                                                    </definition>
                                                                                </collectionparameter>
                                                                                <listedparameter name="itemOrientation">vertical</listedparameter>
                                                                                <xhtmlparameter name="itemGeneral">
                                                                                    <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Polytome scoring - 3 punten</p>
                                                                                    <p id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml">Translatie<br id="c1-id-14"/>3 = 5<br id="c1-id-15"/>2 = 2<br id="c1-id-16"/>1 = 0<br id="c1-id-17"/>0 = 0</p>
                                                                                </xhtmlparameter>
                                                                            </parameterSet>
                                                                        </parameters>
                                                                    </assessmentItem>

End Class

