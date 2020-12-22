
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Framework

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingInputIntegerTests
        Inherits QTI_Base.ResponseProcessingInputIntegerTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub IntegerRangeTest()
            Dim solution As Solution = _solution2.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(1)

            PublicationTestHelper.RunResponseProcessingTest(_itemOneGap, solution, finding, _responseProcessing2, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub IntegerAlternativesTest()
            Dim solution As Solution = _solution3.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(1)

            PublicationTestHelper.RunResponseProcessingTest(_itemOneGap, solution, finding, PublicationTestHelper.ResponseProcessingTemplateMatchCorrect, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub IntegerRangeTwiceTest()
            Dim solution As Solution = _solution4.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(2)

            PublicationTestHelper.RunResponseProcessingTest(_itemTwoGaps, solution, finding, _responseProcessing4, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub IntegerComparisonTest()
            Dim solution As Solution = _solution5.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(1)

            PublicationTestHelper.RunResponseProcessingTest(_itemOneGap, solution, finding, _responseProcessing5, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub IntegerComparisonAlternativesTest()
            Dim solution As Solution = _solution6.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(1)

            PublicationTestHelper.RunResponseProcessingTest(_itemOneGap, solution, finding, _responseProcessing6, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub IntegerRangeAlternativesTest()
            Dim solution As Solution = _solution7.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(1)

            PublicationTestHelper.RunResponseProcessingTest(_itemOneGap, solution, finding, _responseProcessing7, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub IntegerAlternativesFactSetRangeTest()
            Dim solution As Solution = _solution8.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(3)

            PublicationTestHelper.RunResponseProcessingTest(_itemThreeGaps, solution, finding, _responseProcessing8, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub ItemWithIntegerInputWhenCreatingResponseProcessingReturnsResponseProcessingTemplateMatchCorrect()
            Dim solution As Solution = _solution1.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(1)

            PublicationTestHelper.RunResponseProcessingTest(_itemOneGap, solution, finding, PublicationTestHelper.ResponseProcessingTemplateMatchCorrect, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub ItemWithIntegerInputWhenCreatingResponseProcessingReturnsResponseProcessingTemplateMapResponse()
            Dim solution As Solution = _solution9.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim scoringPrms = GetInputScoringParams(1)

            PublicationTestHelper.RunResponseProcessingTest(_itemOneGap, solution, finding, PublicationTestHelper.ResponseProcessingTemplateMapResponse, scoringPrms, New CombinedScoringConverter(scoringPrms), 0, False)
        End Sub

        Private _itemOneGap As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I51372368-cad1-4946-9d37-f1ca446221c6" expected-length="6"/> 
                            </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _itemTwoGaps As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I51372368-cad1-4946-9d37-f1ca446221c6" expected-length="6"/> <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I8500a50f-9040-41b7-986b-1af7686d80e8" expected-length="6"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _itemThreeGaps As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="itemquestion">
                                <p id="c1-id-11">Welke twee getallen zijn opgeteld 3?</p>
                            </div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I51372368-cad1-4946-9d37-f1ca446221c6" expected-length="6"/> <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I8500a50f-9040-41b7-986b-1af7686d80e8" expected-length="6"/> <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I0dec2572-468c-49d9-bdff-c2482ec461c1" expected-length="6"/>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _responseProcessing2 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-gte>
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="integer">1</qti-base-value>
                            </qti-gte>
                            <qti-lte>
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="integer">10</qti-base-value>
                            </qti-lte>
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

        Private _responseProcessing4 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-and>
                                <qti-gte>
                                    <qti-variable identifier="RESPONSE"/>
                                    <qti-base-value base-type="integer">1</qti-base-value>
                                </qti-gte>
                                <qti-lte>
                                    <qti-variable identifier="RESPONSE"/>
                                    <qti-base-value base-type="integer">10</qti-base-value>
                                </qti-lte>
                            </qti-and>
                            <qti-and>
                                <qti-gte>
                                    <qti-variable identifier="RESPONSE2"/>
                                    <qti-base-value base-type="integer">11</qti-base-value>
                                </qti-gte>
                                <qti-lte>
                                    <qti-variable identifier="RESPONSE2"/>
                                    <qti-base-value base-type="integer">22</qti-base-value>
                                </qti-lte>
                            </qti-and>
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

        Private _responseProcessing5 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-lt>
                            <qti-variable identifier="RESPONSE"/>
                            <qti-base-value base-type="integer">2</qti-base-value>
                        </qti-lt>
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
                        <qti-or>
                            <qti-lte>
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="integer">2</qti-base-value>
                            </qti-lte>
                            <qti-gte>
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="integer">4</qti-base-value>
                            </qti-gte>
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

        Private _responseProcessing7 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-gte>
                                    <qti-variable identifier="RESPONSE"/>
                                    <qti-base-value base-type="integer">1</qti-base-value>
                                </qti-gte>
                                <qti-lte>
                                    <qti-variable identifier="RESPONSE"/>
                                    <qti-base-value base-type="integer">10</qti-base-value>
                                </qti-lte>
                            </qti-and>
                            <qti-and>
                                <qti-gte>
                                    <qti-variable identifier="RESPONSE"/>
                                    <qti-base-value base-type="integer">11</qti-base-value>
                                </qti-gte>
                                <qti-lte>
                                    <qti-variable identifier="RESPONSE"/>
                                    <qti-base-value base-type="integer">20</qti-base-value>
                                </qti-lte>
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
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-and>
                                <qti-or>
                                    <qti-equal tolerance-mode="exact">
                                        <qti-variable identifier="RESPONSE"/>
                                        <qti-base-value base-type="integer">1</qti-base-value>
                                    </qti-equal>
                                    <qti-equal tolerance-mode="exact">
                                        <qti-variable identifier="RESPONSE"/>
                                        <qti-base-value base-type="integer">2</qti-base-value>
                                    </qti-equal>
                                </qti-or>
                                <qti-equal tolerance-mode="exact">
                                    <qti-variable identifier="RESPONSE2"/>
                                    <qti-base-value base-type="integer">3</qti-base-value>
                                </qti-equal>
                            </qti-and>
                            <qti-and>
                                <qti-gte>
                                    <qti-variable identifier="RESPONSE3"/>
                                    <qti-base-value base-type="integer">4</qti-base-value>
                                </qti-gte>
                                <qti-lte>
                                    <qti-variable identifier="RESPONSE3"/>
                                    <qti-base-value base-type="integer">5</qti-base-value>
                                </qti-lte>
                            </qti-and>
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