
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

<TestClass()>
Public Class QTI22ResponseProcessingChoiceTests
    Inherits QTI22ResponseProcessingTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub ChoiceTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParam_TwoControls()
        RunResponseProcessingTest(_itemBody1, _finding1, _responseProcessing1, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub ChoiceFactSetTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParam_TwoControls()
        RunResponseProcessingTest(_itemBody2, _finding2, _responseProcessing2, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))        
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub ChoiceOneFactTwoFactSetsTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParam_ThreeControls()
        RunResponseProcessingTest(_itemBody3, _finding3, _responseProcessing3, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub ChoiceTwoGroupsFourFactSetsTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetInlineChoiceScoringParam_FourControls()
        RunResponseProcessingTest(_itemBody4, _finding4, _responseProcessing4, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    Private Function GetInlineChoiceScoringParam_TwoControls() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam1 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice1", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I67632965-d307-4fb3-a4b0-1107e5af0085", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam1)
        Dim scoreParam2 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice2", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I440d2525-0615-4521-ab71-17f9473e15d5", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam2)
        Return scoreParams
    End Function

    Private Function GetInlineChoiceScoringParam_ThreeControls() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam1 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice1", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I607277c2-67be-4250-a9ae-e3206ef42c3a", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam1)
        Dim scoreParam2 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice2", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I74481228-d4ec-4159-b773-cc96a776d1b6", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam2)
        Dim scoreParam3 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice3", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam3)
        Return scoreParams
    End Function

    Private Function GetInlineChoiceScoringParam_FourControls() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam1 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice1", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I207277c2-67be-4250-a9ae-e3206ef42c3a", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam1)
        Dim scoreParam2 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice2", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I24481228-d4ec-4159-b773-cc96a776d1b6", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam2)
        Dim scoreParam3 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice3", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam3)
        Dim scoreParam4 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice4", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "Idf975b11-8ecf-4781-95d6-24677450f53a", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam4)
        Return scoreParams
    End Function

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
                                <inlineChoiceInteraction responseIdentifier="I67632965-d307-4fb3-a4b0-1107e5af0085" shuffle="false" required="true">
                                    <inlineChoice identifier="A">100</inlineChoice>
                                    <inlineChoice identifier="B">200</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I440d2525-0615-4521-ab71-17f9473e15d5" shuffle="false" required="true">
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

    Private _finding1 As XElement =
        <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
            <keyFact id="A-I67632965-d307-4fb3-a4b0-1107e5af0085" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I67632965-d307-4fb3-a4b0-1107e5af0085" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="B-I440d2525-0615-4521-ab71-17f9473e15d5" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I440d2525-0615-4521-ab71-17f9473e15d5" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
        </keyFinding>

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
                                <inlineChoiceInteraction responseIdentifier="I67632965-d307-4fb3-a4b0-1107e5af0085" shuffle="false" required="true">
                                    <inlineChoice identifier="A">100</inlineChoice>
                                    <inlineChoice identifier="B">200</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I440d2525-0615-4521-ab71-17f9473e15d5" shuffle="false" required="true">
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

    Private _finding2 As XElement =
        <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="A-I67632965-d307-4fb3-a4b0-1107e5af0085" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I67632965-d307-4fb3-a4b0-1107e5af0085" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-I440d2525-0615-4521-ab71-17f9473e15d5" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I440d2525-0615-4521-ab71-17f9473e15d5" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="B-I67632965-d307-4fb3-a4b0-1107e5af0085" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I67632965-d307-4fb3-a4b0-1107e5af0085" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-I440d2525-0615-4521-ab71-17f9473e15d5" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I440d2525-0615-4521-ab71-17f9473e15d5" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

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

    Private _itemBody3 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <inlineChoiceInteraction responseIdentifier="I607277c2-67be-4250-a9ae-e3206ef42c3a" shuffle="false" required="true">
                                    <inlineChoice identifier="A">aa</inlineChoice>
                                    <inlineChoice identifier="B">bb</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I74481228-d4ec-4159-b773-cc96a776d1b6" shuffle="false" required="true">
                                    <inlineChoice identifier="A">cc</inlineChoice>
                                    <inlineChoice identifier="B">dd</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05" shuffle="false" required="true">
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

    Private _finding3 As XElement =
        <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
            <keyFact id="A-I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="A-I607277c2-67be-4250-a9ae-e3206ef42c3a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I607277c2-67be-4250-a9ae-e3206ef42c3a" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-I74481228-d4ec-4159-b773-cc96a776d1b6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I74481228-d4ec-4159-b773-cc96a776d1b6" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="B-I607277c2-67be-4250-a9ae-e3206ef42c3a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I607277c2-67be-4250-a9ae-e3206ef42c3a" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-I74481228-d4ec-4159-b773-cc96a776d1b6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I74481228-d4ec-4159-b773-cc96a776d1b6" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

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

    Private _itemBody4 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <inlineChoiceInteraction responseIdentifier="I207277c2-67be-4250-a9ae-e3206ef42c3a" shuffle="false" required="true">
                                    <inlineChoice identifier="A">aa</inlineChoice>
                                    <inlineChoice identifier="B">bb</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I24481228-d4ec-4159-b773-cc96a776d1b6" shuffle="false" required="true">
                                    <inlineChoice identifier="A">cc</inlineChoice>
                                    <inlineChoice identifier="B">dd</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05" shuffle="false" required="true">
                                    <inlineChoice identifier="A">ee</inlineChoice>
                                    <inlineChoice identifier="B">ff</inlineChoice>
                                </inlineChoiceInteraction> <inlineChoiceInteraction responseIdentifier="Idf975b11-8ecf-4781-95d6-24677450f53a" shuffle="false" required="true">
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

    Private _finding4 As XElement =
        <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="A-I207277c2-67be-4250-a9ae-e3206ef42c3a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I207277c2-67be-4250-a9ae-e3206ef42c3a" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-I24481228-d4ec-4159-b773-cc96a776d1b6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I24481228-d4ec-4159-b773-cc96a776d1b6" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="B-I207277c2-67be-4250-a9ae-e3206ef42c3a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I207277c2-67be-4250-a9ae-e3206ef42c3a" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-I24481228-d4ec-4159-b773-cc96a776d1b6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I24481228-d4ec-4159-b773-cc96a776d1b6" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-Idf975b11-8ecf-4781-95d6-24677450f53a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Idf975b11-8ecf-4781-95d6-24677450f53a" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="B-I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4f98ff12-bcf3-4fb8-8fc1-42403ddaed05" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-Idf975b11-8ecf-4781-95d6-24677450f53a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Idf975b11-8ecf-4781-95d6-24677450f53a" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

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
