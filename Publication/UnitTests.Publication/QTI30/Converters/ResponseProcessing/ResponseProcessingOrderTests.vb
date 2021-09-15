Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingOrderTests
        Inherits QTI_Base.ResponseProcessingOrderTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactSet_Dichotomous_Test()
            GetResponseProcessingTest(_finding1, _responseProcessing1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_Test()
            GetResponseProcessingTest(_finding2, _responseProcessing2)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_ReverseFinding_Dichotomous_Test()
            GetResponseProcessingTest(_finding5, _responseProcessing2)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactSet_Polytomous_Test()
            GetResponseProcessingTest(_finding3, _responseProcessing3)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub OrderItemWithPolytomousScoringWhenCreatingResponseProcessingReturnsMapResponseResponseProcessingTemplate()
            GetResponseProcessingTest(_finding4, _responseProcessing4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_ReverseFinding_Polytomous_Test()
            GetResponseProcessingTest(_finding6, _responseProcessing4)
        End Sub

        Public Sub GetResponseProcessingTest(findingElement As XElement, responseProcessingElement As XElement)
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetOrderScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        Private _itemBody1 As XElement =
            <wrapper>
                <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <styles xmlns="http://www.w3.org/1999/xhtml">
                            <stylecollection>
                                <style classname=".vertical .orderInteractionList" attributename="width" value="auto"/>
                            </stylecollection>
                        </styles>
                        <div xmlns="http://www.w3.org/1999/xhtml">
                            <div id="question">
                                <p id="c1-id-11">De vraag luidt...</p>
                            </div>
                            <div id="answer">
                                <qti-order-interaction response-identifier="orderController" shuffle="false" orientation="vertical" min-choices="1">
                                    <qti-simple-choice identifier="A"><p id="c1-id-11">alt A</p></qti-simple-choice>
                                    <qti-simple-choice identifier="B"><p id="c1-id-11">alt B</p></qti-simple-choice>
                                    <qti-simple-choice identifier="C"><p id="c1-id-11">alt C</p></qti-simple-choice>
                                </qti-order-interaction>
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
                                <qti-match>
                                    <qti-index n="1">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-index n="2">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-index n="3">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                </qti-match>
                            </qti-and>
                            <qti-and>
                                <qti-match>
                                    <qti-index n="1">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-index n="2">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-index n="3">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                </qti-match>
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

        Private _responseProcessing2 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-match>
                                <qti-index n="1">
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-index>
                                <qti-base-value base-type="identifier">C</qti-base-value>
                            </qti-match>
                            <qti-match>
                                <qti-index n="2">
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-index>
                                <qti-base-value base-type="identifier">B</qti-base-value>
                            </qti-match>
                            <qti-match>
                                <qti-index n="3">
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-index>
                                <qti-base-value base-type="identifier">A</qti-base-value>
                            </qti-match>
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

        Private _responseProcessing3 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-match>
                                    <qti-index n="1">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-index n="2">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-index n="3">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                </qti-match>
                            </qti-and>
                            <qti-and>
                                <qti-match>
                                    <qti-index n="1">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-index n="2">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-index n="3">
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-index>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                </qti-match>
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
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-match>
                            <qti-index n="1">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">C</qti-base-value>
                        </qti-match>
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
                        <qti-match>
                            <qti-index n="2">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">B</qti-base-value>
                        </qti-match>
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
                        <qti-match>
                            <qti-index n="3">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">A</qti-base-value>
                        </qti-match>
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