
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

<TestClass()>
Public Class QTI22ResponseProcessingHotspotTests
    Inherits QTI22ResponseProcessingTests_Base

    'Hotspot items are (scoring-wise) MR-items, so the response processing is build using CesResponseProcessingMC (for mc and mr items).
    'This functionality may already be tested in other unittests, but still it's useful to check if it also correctly works for hotspot items.

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
    Public Sub GetResponseProcessingForFactSetTest()
        GetResponseProcessingTest(_finding3, _responseProcessing3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_Test()
        GetResponseProcessingTest(_finding4, _responseProcessing4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_Test()
        GetResponseProcessingTest(_finding7, _responseProcessing7)
    End Sub

    Public Sub GetResponseProcessingTest(findingElement As XElement, responseProcessingElement As XElement)
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetHotspotScoringParams()
        RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    Private Function GetHotspotScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)

        Dim scoreParam As HotspotScoringParameter = New HotspotScoringParameter() With {.ControllerId = "areaInteractionController", .FindingOverride = "areaInteractionController", .MinChoices = 1, .MaxChoices = 5}.AddSubParameters("A", "B", "C", "D", "E")
        Dim area As New AreaParameter With {.Name = "clickableArea"}
        scoreParam.Area = area
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "A", .Radius = 35})
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "B", .Radius = 35})
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "C", .Radius = 35})
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "D", .Radius = 35})
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "E", .Radius = 35})

        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function

    Private _itemBody1 As XElement =
       <wrapper>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <hotspotInteraction responseIdentifier="areaInteractionController" minChoices="1" maxChoices="5">
                               <object type="image/jpeg" data="resource://package/UK.jpg" width="197" height="256"/>
                               <hotspotChoice identifier="A" coords="42,42,35" shape="circle"/>
                               <hotspotChoice identifier="B" coords="153,41,35" shape="circle"/>
                               <hotspotChoice identifier="C" coords="96,122,35" shape="circle"/>
                               <hotspotChoice identifier="D" coords="46,204,35" shape="circle"/>
                               <hotspotChoice identifier="E" coords="154,206,35" shape="circle"/>
                           </hotspotInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

    Private _finding1 As XElement =
        <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
            <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="D-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="E-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding2 As XElement =
        <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding3 As XElement =
        <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding4 As XElement =
        <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
            <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="A-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="B-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="C-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="D-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="E-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _finding5 As XElement =
        <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
            <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="D-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="E-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding6 As XElement =
        <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding7 As XElement =
        <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
            <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="A-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="B-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="C-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="D-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="E-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
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
                                <baseValue baseType="identifier">A</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">B</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">C</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">A</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">B</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                        </and>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">A</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">B</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">C</baseValue>
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
                        <baseValue baseType="identifier">E</baseValue>
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
                                <baseValue baseType="identifier">A</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">B</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">C</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">A</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">B</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">C</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">A</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">B</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
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
                            <not>
                                <member>
                                    <baseValue baseType="identifier">D</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">E</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">D</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">E</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
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
                                <baseValue baseType="identifier">A</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">B</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">C</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">D</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">E</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">A</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">B</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">C</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">D</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">E</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                        </and>
                        <and>
                            <member>
                                <baseValue baseType="identifier">A</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">B</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">D</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not
                            ><member>
                                <baseValue baseType="identifier">E</baseValue>
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
                        <baseValue baseType="identifier">A</baseValue>
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
                        <baseValue baseType="identifier">C</baseValue>
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
                        <baseValue baseType="identifier">E</baseValue>
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
                                    <baseValue baseType="identifier">A</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">B</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">A</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">B</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">C</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                            </and>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">A</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">B</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                        </or>
                        <not>
                            <member>
                                <baseValue baseType="identifier">D</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">E</baseValue>
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
                                    <baseValue baseType="identifier">A</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">B</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">A</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">B</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">A</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">B</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">C</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                            </and>
                        </or>
                        <or>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">D</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">E</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <member>
                                    <baseValue baseType="identifier">D</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">E</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
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
                            <baseValue baseType="identifier">A</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">B</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">C</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">D</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">E</baseValue>
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

End Class
