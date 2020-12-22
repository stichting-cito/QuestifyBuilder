Imports System.Drawing
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class QTI22ResponseDeclarationGraphicGapMatchCategorizeTests
    Inherits QTI22ResponseDeclarationTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
        RunResponseDeclarationTest(_itemBody1, _solution1, GetGGMScoringParams, _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForMultipleFactSetsTest()
        RunResponseDeclarationTest(_itemBody1, _solution2, GetGGMScoringParams, _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactSetsTest()
        RunResponseDeclarationTest(_itemBody1, _solution3, GetGGMScoringParams, _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactsOnFindingTest()
        RunResponseDeclarationTest(_itemBody1, _solution4, GetGGMScoringParams, _result4, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetEmptyResponseDeclarationTest()
        RunResponseDeclarationTest(_itemBody1, _solution5, GetGGMScoringParams, _result5, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactsOnFinding_MultipleAlternativesInOneGap_Test()
        RunResponseDeclarationTest(_itemBody1, _solution6, GetGGMScoringParams, _result6, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationWithNoValueTest()
        RunResponseDeclarationTest(_itemBody1, _solution7, GetGGMScoringParams, _result7, 1)
    End Sub


    Private Function GetGGMScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam As GraphGapMatchScoringParameter = New GraphGapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController", .IsCategorizationVariant = True}.AddSubParameters("A", "B", "C", "D")
        Dim area As New AreaParameter With {.Name = "itemQuestionArea"}
        scoreParam.Area = area
        scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "C", .BottomRight = New Point(694, 285), .TopLeft = New Point(474, 35)})
        scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "D", .BottomRight = New Point(101, 25), .TopLeft = New Point(76, 0)})
        scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "A", .BottomRight = New Point(191, 284), .TopLeft = New Point(0, 100)})
        scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "B", .BottomRight = New Point(472, 285), .TopLeft = New Point(192, 35)})
        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function



    Private _itemBody1 As XElement =
       <wrapper>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <graphicGapMatchInteraction responseIdentifier="gapMatchController" categorize="true">
                               <object type="image/jpeg" data="resource://package/UK.jpg" width="197" height="256"/>
                               <gapImg identifier="A" matchMax="1" class="">
                                   <object type="image/png" data="resource://package/InlineChoice.png" class="hotspot_opacity" width="68" height="21"/>
                               </gapImg>
                               <gapImg identifier="B" matchMax="1" class="">
                                   <object type="image/jpeg" data="resource://package/hotspotimage_120_30_0_bla di bla.png"/>
                               </gapImg>
                               <gapImg identifier="C" matchMax="1" class="">
                                   <object type="image/png" data="resource://package/hsmathml_120_30_0_MFI_2014814_15_3_34_924.png"/>
                               </gapImg>
                               <gapImg identifier="D" matchMax="1" class="">
                                   <object type="image/jpeg" data="resource://package/hotspotimage_120_30_0_fsfs.png"/>
                               </gapImg>
                               <associableHotspot identifier="HSA" matchMax="1" coords="45,70,159,176" shape="rect"/>
                               <associableHotspot identifier="HSB" matchMax="1" coords="198,40,278,182" shape="rect"/>
                               <associableHotspot identifier="HSC" matchMax="1" coords="291,67,431,173" shape="rect"/>
                               <associableHotspot identifier="HSD" matchMax="1" coords="452,100,548,204" shape="rect"/>
                           </graphicGapMatchInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                    <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
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
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                    <keyFactSet>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
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
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution3 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                    <keyFactSet>
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
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
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution4 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                    <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-gapMatchController" occur="1">
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
                    <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
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

    Private _solution5 As XElement =
        <solution>
            <keyFindings/>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution6 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                    <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
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

    Private _solution7 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                    <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-gapMatchController" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-gapMatchController" occur="1">
                            <noValue/>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <noValue/>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <noValue/>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="D HSA&amp;C HSB&amp;(B HSC&amp;A HSD)|(A HSC&amp;B HSD)">
                    <value>D HSA</value>
                    <value>C HSB</value>
                    <value>B HSC</value>
                    <value>A HSD</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="(D HSA&amp;C HSB)|(C HSA&amp;D HSB)&amp;(B HSC&amp;A HSD)|(A HSC&amp;B HSD)">
                    <value>D HSA</value>
                    <value>C HSB</value>
                    <value>B HSC</value>
                    <value>A HSD</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="(A HSA&amp;B HSB&amp;C HSC&amp;D HSD)|(D HSA&amp;C HSB&amp;B HSC&amp;A HSD)">
                    <value>A HSA</value>
                    <value>B HSB</value>
                    <value>C HSC</value>
                    <value>D HSD</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="D HSA&amp;C HSB&amp;B HSC&amp;A HSD">
                    <value>D HSA</value>
                    <value>C HSB</value>
                    <value>B HSC</value>
                    <value>A HSD</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="string"/>
        </responseDeclarations>

    Private _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="A HSA&amp;C HSA&amp;B HSB&amp;D HSB">
                    <value>A HSA</value>
                    <value>C HSA</value>
                    <value>B HSB</value>
                    <value>D HSB</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result7 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="D HSA&amp;(A HSD&amp;B Ø)|(B HSD&amp;A Ø)&amp;C Ø">
                    <value>D HSA</value>
                    <value>A HSD</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>


End Class
