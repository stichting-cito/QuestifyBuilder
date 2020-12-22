Imports System.Drawing
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

<TestClass()>
Public Class QTI22ResponseProcessingGraphicGapMatchTests
    Inherits QTI22ResponseProcessingTests_Base

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
        RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    Private Function GetGGMScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam As GraphGapMatchScoringParameter = New GraphGapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController", .IsCategorizationVariant = False}.AddSubParameters("A", "B", "C", "D")
        scoreParam.Value(0).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "A", .Name = GapMatchScoringParameter.GapControlName})
        scoreParam.Value(1).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "B", .Name = GapMatchScoringParameter.GapControlName})
        scoreParam.Value(2).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "C", .Name = GapMatchScoringParameter.GapControlName})
        scoreParam.Value(3).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "D", .Name = GapMatchScoringParameter.GapControlName})

        Dim area As New AreaParameter With {.Name = "itemQuestionArea"}
        scoreParam.Area = area
        scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "C", .BottomRight = New Point(694, 285), .TopLeft = New Point(474, 35)})
        scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "D", .BottomRight = New Point(101, 25), .TopLeft = New Point(76, 0)})
        scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "A", .BottomRight = New Point(191, 284), .TopLeft = New Point(0, 100)})
        scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "B", .BottomRight = New Point(472, 285), .TopLeft = New Point(192, 35)})

        Dim ggmScoringPrm As ScoringParameter = scoreParam.Transform
        scoreParams.Add(ggmScoringPrm)
        Return scoreParams
    End Function

    Private _itemBody1 As XElement =
       <wrapper>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <graphicGapMatchInteraction responseIdentifier="gapMatchController">
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
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">D HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C HSB</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">C HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">D HSB</baseValue>
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
                        <baseValue baseType="directedPair">B HSC</baseValue>
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
                        <baseValue baseType="directedPair">A HSD</baseValue>
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
                                <baseValue baseType="directedPair">D HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C HSB</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">C HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">D HSB</baseValue>
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
                                <baseValue baseType="directedPair">B HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">A HSD</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">A HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B HSD</baseValue>
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
                                <baseValue baseType="directedPair">A HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B HSB</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">D HSD</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">D HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C HSB</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">A HSD</baseValue>
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
                        <baseValue baseType="directedPair">D HSA</baseValue>
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
                        <baseValue baseType="directedPair">C HSB</baseValue>
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
                        <baseValue baseType="directedPair">B HSC</baseValue>
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
                        <baseValue baseType="directedPair">A HSD</baseValue>
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
                                    <baseValue baseType="directedPair">D HSA</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">C HSB</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">C HSA</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">D HSB</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                        <member>
                            <baseValue baseType="directedPair">B HSC</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">A HSD</baseValue>
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
                                    <baseValue baseType="directedPair">D HSA</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">C HSB</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">C HSA</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">D HSB</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                        <or>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">B HSC</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">A HSD</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">A HSC</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">B HSD</baseValue>
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
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">A HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B HSB</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">D HSD</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">D HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C HSB</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">A HSD</baseValue>
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
                            <baseValue baseType="directedPair">D HSA</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">C HSB</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">B HSC</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <member>
                            <baseValue baseType="directedPair">A HSD</baseValue>
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
                        <or>
                            <and>
                                <member>
                                    <baseValue baseType="directedPair">D HSA</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <or>
                                        <member>
                                            <baseValue baseType="directedPair">A HSB</baseValue>
                                            <variable identifier="RESPONSE"/>
                                        </member>
                                        <member>
                                            <baseValue baseType="directedPair">B HSB</baseValue>
                                            <variable identifier="RESPONSE"/>
                                        </member>
                                        <member>
                                            <baseValue baseType="directedPair">C HSB</baseValue>
                                            <variable identifier="RESPONSE"/>
                                        </member>
                                        <member>
                                            <baseValue baseType="directedPair">D HSB</baseValue>
                                            <variable identifier="RESPONSE"/>
                                        </member>
                                    </or>
                                </not>
                            </and>
                            <and>
                                <not>
                                    <or>
                                        <member>
                                            <baseValue baseType="directedPair">A HSA</baseValue>
                                            <variable identifier="RESPONSE"/>
                                        </member>
                                        <member>
                                            <baseValue baseType="directedPair">B HSA</baseValue>
                                            <variable identifier="RESPONSE"/>
                                        </member>
                                        <member>
                                            <baseValue baseType="directedPair">C HSA</baseValue>
                                            <variable identifier="RESPONSE"/>
                                        </member>
                                        <member>
                                            <baseValue baseType="directedPair">D HSA</baseValue>
                                            <variable identifier="RESPONSE"/>
                                        </member>
                                    </or>
                                </not>
                                <member>
                                    <baseValue baseType="directedPair">D HSB</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                        <not>
                            <or>
                                <member>
                                    <baseValue baseType="directedPair">A HSC</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">B HSC</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">C HSC</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="directedPair">D HSC</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </or>
                        </not>
                        <member>
                            <baseValue baseType="directedPair">A HSD</baseValue>
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

    Private _responseProcessing10 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <member>
                                <baseValue baseType="directedPair">D HSA</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <or>
                                    <member>
                                        <baseValue baseType="directedPair">A HSB</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                    <member>
                                        <baseValue baseType="directedPair">B HSB</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                    <member>
                                        <baseValue baseType="directedPair">C HSB</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                    <member>
                                        <baseValue baseType="directedPair">D HSB</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </or>
                            </not>
                        </and>
                        <and>
                            <not>
                                <or>
                                    <member>
                                        <baseValue baseType="directedPair">A HSA</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                    <member>
                                        <baseValue baseType="directedPair">B HSA</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                    <member>
                                        <baseValue baseType="directedPair">C HSA</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                    <member>
                                        <baseValue baseType="directedPair">D HSA</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </or>
                            </not>
                            <member>
                                <baseValue baseType="directedPair">D HSB</baseValue>
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
                    <not>
                        <or>
                            <member>
                                <baseValue baseType="directedPair">A HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">B HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">C HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="directedPair">D HSC</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </or>
                    </not>
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
                        <baseValue baseType="directedPair">A HSD</baseValue>
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

End Class
