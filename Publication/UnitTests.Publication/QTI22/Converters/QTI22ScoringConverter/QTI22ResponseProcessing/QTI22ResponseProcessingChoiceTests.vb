
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

<TestClass()>
Public Class QTI22ResponseProcessingChoiceTests
    Inherits QTI_Base.ResponseProcessingChoiceTestsBase

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub ChoiceTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(2)
        QTI22PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding1, _responseProcessing1, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub ChoiceFactSetTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(2)
        QTI22PublicationTestHelper.RunResponseProcessingTest(_itemBody2, _finding2, _responseProcessing2, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub ChoiceOneFactTwoFactSetsTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(3)
        QTI22PublicationTestHelper.RunResponseProcessingTest(_itemBody3, _finding3, _responseProcessing3, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub ChoiceTwoGroupsFourFactSetsTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParams(4)
        QTI22PublicationTestHelper.RunResponseProcessingTest(_itemBody4, _finding4, _responseProcessing4, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
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
                                <inlineChoiceInteraction responseIdentifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                    <inlineChoice identifier="A">100</inlineChoice>
                                    <inlineChoice identifier="B">200</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                    <inlineChoice identifier="A">C</inlineChoice>
                                    <inlineChoice identifier="B">CC</inlineChoice>
                                </inlineChoiceInteraction>
                            </p>
                        </div>
                        <div id="answer">

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
                                <inlineChoiceInteraction responseIdentifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                    <inlineChoice identifier="A">100</inlineChoice>
                                    <inlineChoice identifier="B">200</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                    <inlineChoice identifier="A">C</inlineChoice>
                                    <inlineChoice identifier="B">CC</inlineChoice>
                                </inlineChoiceInteraction>
                            </p>
                        </div>
                        <div id="answer">

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
                                <inlineChoiceInteraction responseIdentifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                    <inlineChoice identifier="A">aa</inlineChoice>
                                    <inlineChoice identifier="B">bb</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                    <inlineChoice identifier="A">cc</inlineChoice>
                                    <inlineChoice identifier="B">dd</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I22f81e91-aaa4-4528-999e-61b31b8123ec" shuffle="false" required="true">
                                    <inlineChoice identifier="A">ee</inlineChoice>
                                    <inlineChoice identifier="B">ff</inlineChoice>
                                </inlineChoiceInteraction>
                            </p>
                        </div>
                        <div id="answer">

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
                                <inlineChoiceInteraction responseIdentifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                    <inlineChoice identifier="A">aa</inlineChoice>
                                    <inlineChoice identifier="B">bb</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                    <inlineChoice identifier="A">cc</inlineChoice>
                                    <inlineChoice identifier="B">dd</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I22f81e91-aaa4-4528-999e-61b31b8123ec" shuffle="false" required="true">
                                    <inlineChoice identifier="A">ee</inlineChoice>
                                    <inlineChoice identifier="B">ff</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" shuffle="false" required="true">
                                    <inlineChoice identifier="A">gg</inlineChoice>
                                    <inlineChoice identifier="B">hh</inlineChoice>
                                </inlineChoiceInteraction>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _responseProcessing1 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <match>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="identifier">A</baseValue>
                        </match>
                        <match>
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="identifier">B</baseValue>
                        </match>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <match>
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="identifier">A</baseValue>
                            </match>
                            <match>
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="identifier">B</baseValue>
                            </match>
                        </and>
                        <and>
                            <match>
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="identifier">B</baseValue>
                            </match>
                            <match>
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="identifier">A</baseValue>
                            </match>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing3 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <or>
                            <and>
                                <match>
                                    <variable identifier="RESPONSE"/>
                                    <baseValue baseType="identifier">A</baseValue>
                                </match>
                                <match>
                                    <variable identifier="RESPONSE2"/>
                                    <baseValue baseType="identifier">B</baseValue>
                                </match>
                            </and>
                            <and>
                                <match>
                                    <variable identifier="RESPONSE"/>
                                    <baseValue baseType="identifier">B</baseValue>
                                </match>
                                <match>
                                    <variable identifier="RESPONSE2"/>
                                    <baseValue baseType="identifier">A</baseValue>
                                </match>
                            </and>
                        </or>
                        <match>
                            <variable identifier="RESPONSE3"/>
                            <baseValue baseType="identifier">A</baseValue>
                        </match>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing4 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <or>
                            <and>
                                <match>
                                    <variable identifier="RESPONSE"/>
                                    <baseValue baseType="identifier">A</baseValue>
                                </match>
                                <match>
                                    <variable identifier="RESPONSE2"/>
                                    <baseValue baseType="identifier">A</baseValue>
                                </match>
                            </and>
                            <and>
                                <match>
                                    <variable identifier="RESPONSE"/>
                                    <baseValue baseType="identifier">B</baseValue>
                                </match>
                                <match>
                                    <variable identifier="RESPONSE2"/>
                                    <baseValue baseType="identifier">B</baseValue>
                                </match>
                            </and>
                        </or>
                        <or>
                            <and>
                                <match>
                                    <variable identifier="RESPONSE3"/>
                                    <baseValue baseType="identifier">A</baseValue>
                                </match>
                                <match>
                                    <variable identifier="RESPONSE4"/>
                                    <baseValue baseType="identifier">A</baseValue>
                                </match>
                            </and>
                            <and>
                                <match>
                                    <variable identifier="RESPONSE3"/>
                                    <baseValue baseType="identifier">B</baseValue>
                                </match>
                                <match>
                                    <variable identifier="RESPONSE4"/>
                                    <baseValue baseType="identifier">B</baseValue>
                                </match>
                            </and>
                        </or>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="float">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="float">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

End Class
