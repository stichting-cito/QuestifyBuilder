Imports System.Drawing
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationGraphicGapMatchCategorizeTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetGGMScoringParams, _result1, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForMultipleFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetGGMScoringParams, _result2, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetGGMScoringParams, _result3, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForFactsOnFindingTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetGGMScoringParams, _result4, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetEmptyResponseDeclarationTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetGGMScoringParams, _result5, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationForFactsOnFinding_MultipleAlternativesInOneGap_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetGGMScoringParams, _result6, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub GetResponseDeclarationWithNoValueTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution7, GetGGMScoringParams, _result7, 1)
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
               <qti-item-body class="defaultBody">
                   <div class="content">
                       <div>
                           <div>
                               <qti-graphic-gap-match-interaction response-identifier="gapMatchController" categorize="true">
                                   <object type="image/jpeg" data="resource://package/UK.jpg" width="197" height="256"/>
                                   <qti-gap-img identifier="A" match-max="1" class="">
                                       <object type="image/png" data="resource://package/InlineChoice.png" class="hotspot_opacity" width="68" height="21"/>
                                   </qti-gap-img>
                                   <qti-gap-img identifier="B" match-max="1" class="">
                                       <object type="image/jpeg" data="resource://package/hotspotimage_120_30_0_bla di bla.png"/>
                                   </qti-gap-img>
                                   <qti-gap-img identifier="C" match-max="1" class="">
                                       <object type="image/png" data="resource://package/hsmathml_120_30_0_MFI_2014814_15_3_34_924.png"/>
                                   </qti-gap-img>
                                   <qti-gap-img identifier="D" match-max="1" class="">
                                       <object type="image/jpeg" data="resource://package/hotspotimage_120_30_0_fsfs.png"/>
                                   </qti-gap-img>
                                   <qti-associable-hotspot identifier="HSA" match-max="1" coords="45,70,159,176" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSB" match-max="1" coords="198,40,278,182" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSC" match-max="1" coords="291,67,431,173" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSD" match-max="1" coords="452,100,548,204" shape="rect"/>
                               </qti-graphic-gap-match-interaction>
                           </div>
                       </div>
                   </div>
               </qti-item-body>
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
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
            <qti-correct-response interpretation="D HSA&amp;C HSB&amp;(B HSC&amp;A HSD)|(A HSC&amp;B HSD)">
                <qti-value>D HSA</qti-value>
                <qti-value>C HSB</qti-value>
                <qti-value>B HSC</qti-value>
                <qti-value>A HSD</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result2 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
            <qti-correct-response interpretation="(D HSA&amp;C HSB)|(C HSA&amp;D HSB)&amp;(B HSC&amp;A HSD)|(A HSC&amp;B HSD)">
                <qti-value>D HSA</qti-value>
                <qti-value>C HSB</qti-value>
                <qti-value>B HSC</qti-value>
                <qti-value>A HSD</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result3 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
            <qti-correct-response interpretation="(A HSA&amp;B HSB&amp;C HSC&amp;D HSD)|(D HSA&amp;C HSB&amp;B HSC&amp;A HSD)">
                <qti-value>A HSA</qti-value>
                <qti-value>B HSB</qti-value>
                <qti-value>C HSC</qti-value>
                <qti-value>D HSD</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result4 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
            <qti-correct-response interpretation="D HSA&amp;C HSB&amp;B HSC&amp;A HSD">
                <qti-value>D HSA</qti-value>
                <qti-value>C HSB</qti-value>
                <qti-value>B HSC</qti-value>
                <qti-value>A HSD</qti-value>
            </qti-correct-response>
            <qti-mapping>
                <qti-map-entry map-key="D HSA" mapped-value="1"/>
                <qti-map-entry map-key="C HSB" mapped-value="1"/>
                <qti-map-entry map-key="B HSC" mapped-value="1"/>
                <qti-map-entry map-key="A HSD" mapped-value="1"/>
            </qti-mapping>
        </qti-response-declaration>
    </root>

        Private _result5 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="string"/>
    </root>

        Private _result6 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
            <qti-correct-response interpretation="A HSA&amp;C HSA&amp;B HSB&amp;D HSB">
                <qti-value>A HSA</qti-value>
                <qti-value>C HSA</qti-value>
                <qti-value>B HSB</qti-value>
                <qti-value>D HSB</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result7 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
            <qti-correct-response interpretation="D HSA&amp;(A HSD&amp;B Ø)|(B HSD&amp;A Ø)&amp;C Ø">
                <qti-value>D HSA</qti-value>
                <qti-value>A HSD</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>


    End Class

End Namespace
