
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingChoiceTests
        Inherits QTI_Base.ResponseProcessingChoiceTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub ChoiceTest()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(2)
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding1, _responseProcessing1, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub ChoiceFactSetTest()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(2)
            PublicationTestHelper.RunResponseProcessingTest(_itemBody2, _finding2, _responseProcessing2, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub ChoiceOneFactTwoFactSetsTest()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(3)
            PublicationTestHelper.RunResponseProcessingTest(_itemBody3, _finding3, _responseProcessing3, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub ChoiceTwoGroupsFourFactSetsTest()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(4)
            PublicationTestHelper.RunResponseProcessingTest(_itemBody4, _finding4, _responseProcessing4, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub ChoiceItemWithDichotousScoringWhenCreatingResponseProcessingReturnsMatchCorrectResponseProcessingTemplate()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(1)
            PublicationTestHelper.RunResponseProcessingTest(_itemBody5, _finding5, PublicationTestHelper.ResponseProcessingTemplateMatchCorrect, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseItemWithPolytomousScoringWhenCreatingResponseProcessingReturnsMapResponseResponseProcessingTemplate()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(1)
            PublicationTestHelper.RunResponseProcessingTest(_itemBody5, _finding6, PublicationTestHelper.ResponseProcessingTemplateMapResponse, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        Private _itemBody1 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="itemquestion">
                                <p id="c1-id-11">Welke horen bij elkaar?</p>
                            </div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-inline-choice-interaction response-identifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">100</qti-inline-choice>
                                        <qti-inline-choice identifier="B">200</qti-inline-choice>
                                    </qti-inline-choice-interaction> <qti-inline-choice-interaction response-identifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">C</qti-inline-choice>
                                        <qti-inline-choice identifier="B">CC</qti-inline-choice>
                                    </qti-inline-choice-interaction>
                                </p>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _itemBody2 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="itemquestion">
                                <p id="c1-id-11">Welke horen bij elkaar?</p>
                            </div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-inline-choice-interaction response-identifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">100</qti-inline-choice>
                                        <qti-inline-choice identifier="B">200</qti-inline-choice>
                                    </qti-inline-choice-interaction> <qti-inline-choice-interaction response-identifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">C</qti-inline-choice>
                                        <qti-inline-choice identifier="B">CC</qti-inline-choice>
                                    </qti-inline-choice-interaction>
                                </p>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _itemBody3 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-inline-choice-interaction response-identifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">aa</qti-inline-choice>
                                        <qti-inline-choice identifier="B">bb</qti-inline-choice>
                                    </qti-inline-choice-interaction> <qti-inline-choice-interaction response-identifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">cc</qti-inline-choice>
                                        <qti-inline-choice identifier="B">dd</qti-inline-choice>
                                    </qti-inline-choice-interaction> <qti-inline-choice-interaction response-identifier="I22f81e91-aaa4-4528-999e-61b31b8123ec" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">ee</qti-inline-choice>
                                        <qti-inline-choice identifier="B">ff</qti-inline-choice>
                                    </qti-inline-choice-interaction>
                                </p>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _itemBody4 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-inline-choice-interaction response-identifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">aa</qti-inline-choice>
                                        <qti-inline-choice identifier="B">bb</qti-inline-choice>
                                    </qti-inline-choice-interaction> <qti-inline-choice-interaction response-identifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">cc</qti-inline-choice>
                                        <qti-inline-choice identifier="B">dd</qti-inline-choice>
                                    </qti-inline-choice-interaction> <qti-inline-choice-interaction response-identifier="I22f81e91-aaa4-4528-999e-61b31b8123ec" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">ee</qti-inline-choice>
                                        <qti-inline-choice identifier="B">ff</qti-inline-choice>
                                    </qti-inline-choice-interaction> <qti-inline-choice-interaction response-identifier="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">gg</qti-inline-choice>
                                        <qti-inline-choice identifier="B">hh</qti-inline-choice>
                                    </qti-inline-choice-interaction>
                                </p>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _itemBody5 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-inline-choice-interaction response-identifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                        <qti-inline-choice identifier="A">100</qti-inline-choice>
                                        <qti-inline-choice identifier="B">200</qti-inline-choice>
                                    </qti-inline-choice-interaction>
                                </p>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _responseProcessing1 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-match>
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="identifier">A</qti-base-value>
                            </qti-match>
                            <qti-match>
                                <qti-variable identifier="RESPONSE2"/>
                                <qti-base-value base-type="identifier">B</qti-base-value>
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

        Private _responseProcessing2 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-or>
                            <qti-and>
                                <qti-match>
                                    <qti-variable identifier="RESPONSE"/>
                                    <qti-base-value base-type="identifier">A</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-variable identifier="RESPONSE2"/>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                </qti-match>
                            </qti-and>
                            <qti-and>
                                <qti-match>
                                    <qti-variable identifier="RESPONSE"/>
                                    <qti-base-value base-type="identifier">B</qti-base-value>
                                </qti-match>
                                <qti-match>
                                    <qti-variable identifier="RESPONSE2"/>
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

        Private _responseProcessing3 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-or>
                                <qti-and>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE"/>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                    </qti-match>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE2"/>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                    </qti-match>
                                </qti-and>
                                <qti-and>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE"/>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                    </qti-match>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE2"/>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                    </qti-match>
                                </qti-and>
                            </qti-or>
                            <qti-match>
                                <qti-variable identifier="RESPONSE3"/>
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

        Private _responseProcessing4 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
                        <qti-and>
                            <qti-or>
                                <qti-and>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE"/>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                    </qti-match>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE2"/>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                    </qti-match>
                                </qti-and>
                                <qti-and>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE"/>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                    </qti-match>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE2"/>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                    </qti-match>
                                </qti-and>
                            </qti-or>
                            <qti-or>
                                <qti-and>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE3"/>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                    </qti-match>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE4"/>
                                        <qti-base-value base-type="identifier">A</qti-base-value>
                                    </qti-match>
                                </qti-and>
                                <qti-and>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE3"/>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                    </qti-match>
                                    <qti-match>
                                        <qti-variable identifier="RESPONSE4"/>
                                        <qti-base-value base-type="identifier">B</qti-base-value>
                                    </qti-match>
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