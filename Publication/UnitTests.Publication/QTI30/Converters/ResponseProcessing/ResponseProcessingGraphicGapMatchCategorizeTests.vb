Imports System.Drawing
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingGraphicGapMatchCategorizeTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Polytomous_Test()
            GetResponseProcessingTest(_finding1, _responseProcessing1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Dichotomous_Test()
            GetResponseProcessingTest(_finding5, _responseProcessing5)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForMultipleFactSets_Polytomous_Test()
            GetResponseProcessingTest(_finding2, _responseProcessing2)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForMultipleFactSets_Dichotomous_Test()
            GetResponseProcessingTest(_finding6, _responseProcessing6)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactSet_Polytomous_Test()
            GetResponseProcessingTest(_finding3, _responseProcessing3)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactSet_Dichotomous_Test()
            GetResponseProcessingTest(_finding7, _responseProcessing7)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_Test()
            GetResponseProcessingTest(_finding4, _responseProcessing4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_Test()
            GetResponseProcessingTest(_finding8, _responseProcessing8)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForNoValue_Dichotomous_Test()
            GetResponseProcessingTest(_finding9, _responseProcessing9)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForNoValue_Polytomous_Test()
            GetResponseProcessingTest(_finding10, _responseProcessing10)
        End Sub

        Public Sub GetResponseProcessingTest(findingElement As XElement, responseProcessingElement As XElement)
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetGGMScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New CombinedScoringConverter(scoringPrms))
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

        Private _finding1 As XElement =
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

        Private _finding2 As XElement =
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

        Private _finding3 As XElement =
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

        Private _finding4 As XElement =
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

        Private _finding5 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Private _finding6 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Private _finding7 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Private _finding8 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Private _finding9 As XElement =
        <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Private _finding10 As XElement =
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

        Private _responseProcessing1 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-or>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">A HSD</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-and>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">A HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">B HSD</qti-base-value>
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
                <qti-base-value base-type="directedPair">D HSA</qti-base-value>
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
                <qti-base-value base-type="directedPair">C HSB</qti-base-value>
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
                        <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">A HSD</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-and>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">A HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">B HSD</qti-base-value>
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
                        <qti-base-value base-type="directedPair">D HSA</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSB</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-and>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSA</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">D HSB</qti-base-value>
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
                        <qti-base-value base-type="directedPair">A HSA</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">B HSB</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">D HSD</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-and>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">D HSA</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSB</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">A HSD</qti-base-value>
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

        Private _responseProcessing4 As XElement =
<qti-response-processing template="https://purl.imsglobal.org/spec/qti/v3p0/rptemplates/map_response.xml"/>

        Private _responseProcessing5 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-or>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">A HSD</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="directedPair">A HSC</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">B HSD</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                </qti-or>
                <qti-member>
                    <qti-base-value base-type="directedPair">D HSA</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">C HSB</qti-base-value>
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
                            <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">A HSD</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="directedPair">A HSC</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">B HSD</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                </qti-or>
                <qti-or>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="directedPair">D HSA</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">C HSB</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="directedPair">C HSA</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">D HSB</qti-base-value>
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

        Private _responseProcessing7 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-or>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">A HSA</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">B HSB</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">D HSD</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-and>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">D HSA</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSB</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">A HSD</qti-base-value>
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
<qti-response-processing template="https://purl.imsglobal.org/spec/qti/v3p0/rptemplates/match_correct.xml"/>

        Private _responseProcessing9 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-or>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="directedPair">A HSD</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-or>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B HSA</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B HSB</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">B HSD</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-or>
                        </qti-not>
                    </qti-and>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="directedPair">B HSD</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-or>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A HSA</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A HSB</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A HSC</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="directedPair">A HSD</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-or>
                        </qti-not>
                    </qti-and>
                </qti-or>
                <qti-member>
                    <qti-base-value base-type="directedPair">D HSA</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-or>
                        <qti-member>
                            <qti-base-value base-type="directedPair">C HSA</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">C HSB</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">C HSC</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="directedPair">C HSD</qti-base-value>
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
            <qti-or>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">A HSD</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-not>
                        <qti-or>
                            <qti-member>
                                <qti-base-value base-type="directedPair">B HSA</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">B HSB</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">B HSD</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-or>
                    </qti-not>
                </qti-and>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="directedPair">B HSD</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-not>
                        <qti-or>
                            <qti-member>
                                <qti-base-value base-type="directedPair">A HSA</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">A HSB</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">A HSC</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="directedPair">A HSD</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-or>
                    </qti-not>
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
                <qti-base-value base-type="directedPair">D HSA</qti-base-value>
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
            <qti-not>
                <qti-or>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSA</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSB</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSC</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="directedPair">C HSD</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-or>
            </qti-not>
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
