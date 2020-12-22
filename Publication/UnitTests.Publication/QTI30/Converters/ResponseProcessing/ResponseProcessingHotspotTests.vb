
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingHotspotTests
        Inherits QTI_Base.ResponseProcessingHotspotTestsBase


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
        Public Sub HotspotItemWithPolytomousScoringWhenCreatingResponseProcessingReturnsMapResponseResponseProcessingTemplate()
            GetResponseProcessingTest(_finding4, PublicationTestHelper.ResponseProcessingTemplateMapResponse)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub HotspotItemWithDichotousScoringWhenCreatingResponseProcessingReturnsMatchCorrectResponseProcessingTemplate()
            GetResponseProcessingTest(_finding7, PublicationTestHelper.ResponseProcessingTemplateMatchCorrect)
        End Sub

        Public Sub GetResponseProcessingTest(findingElement As XElement, responseProcessingElement As XElement)
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetHotspotScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        Private _itemBody1 As XElement =
           <wrapper>
               <itemBody class="defaultBody">
                   <div class="content">
                       <div>
                           <div>
                               <qti-hotspot-interaction response-identifier="areaInteractionController" min-choices="1" max-choices="5">
                                   <object type="image/jpeg" data="resource://package/UK.jpg" width="197" height="256"/>
                                   <qti-hotspot-choice identifier="A" coords="42,42,35" shape="circle"/>
                                   <qti-hotspot-choice identifier="B" coords="153,41,35" shape="circle"/>
                                   <qti-hotspot-choice identifier="C" coords="96,122,35" shape="circle"/>
                                   <qti-hotspot-choice identifier="D" coords="46,204,35" shape="circle"/>
                                   <qti-hotspot-choice identifier="E" coords="154,206,35" shape="circle"/>
                               </qti-hotspot-interaction>
                           </div>
                       </div>
                   </div>
               </itemBody>
           </wrapper>

        Private _responseProcessing1 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">C</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                            </qti-and>
                            <qti-and>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
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
                            <qti-base-value base-type="identifier">E</qti-base-value>
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
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">C</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
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
                        <qti-or>
                            <qti-and>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">D</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">E</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="identifier">D</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">E</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
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
            </qti-response-processing>

        Private _responseProcessing3 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">D</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">E</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-member>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">D</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">E</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                            </qti-and>
                            <qti-and>
                                <qti-member>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">C</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">D</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">E</qti-base-value>
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
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-not>
                                        <qti-member>
                                            <qti-base-value base-type="identifier">B</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">C</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-not>
                                        <qti-member>
                                            <qti-base-value base-type="identifier">C</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-not>
                                </qti-and>
                                <qti-and>
                                    <qti-not>
                                        <qti-member>
                                            <qti-base-value base-type="identifier">A</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">C</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                            </qti-or>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">D</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">E</qti-base-value>
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
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-not>
                                        <qti-member>
                                            <qti-base-value base-type="identifier">B</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">C</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">C</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-not>
                                        <qti-member>
                                            <qti-base-value base-type="identifier">B</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-not>
                                    <qti-not>
                                        <qti-member>
                                            <qti-base-value base-type="identifier">C</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-not>
                                </qti-and>
                            </qti-or>
                            <qti-or>
                                <qti-and>
                                    <qti-not>
                                        <qti-member>
                                            <qti-base-value base-type="identifier">D</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">E</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-and>
                                <qti-and>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">D</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                    <qti-not>
                                        <qti-member>
                                            <qti-base-value base-type="identifier">E</qti-base-value>
                                            <qti-variable identifier="RESPONSE"/>
                                        </qti-member>
                                    </qti-not>
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

    End Class

End Namespace