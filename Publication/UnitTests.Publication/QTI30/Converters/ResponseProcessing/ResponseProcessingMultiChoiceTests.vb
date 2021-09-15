
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingMultiChoiceTests
        Inherits QTI_Base.ResponseProcessingMultiChoiceTestsBase

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleChoiceTest()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMcScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody4, _finding8, PublicationTestHelper.ResponseProcessingTemplateMatchCorrect, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseItemWithDichotousScoringWhenCreatingResponseProcessingReturnsMatchCorrectResponseProcessingTemplate()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding1, PublicationTestHelper.ResponseProcessingTemplateMatchCorrect, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseItemWithPolytomousScoringWhenCreatingResponseProcessingReturnsMapResponseResponseProcessingTemplate()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding9, PublicationTestHelper.ResponseProcessingTemplateMapResponse, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub ItemWithMultipleInteractionsWhenCreatingResponseProcessingReturnsResponseProcessingResult1()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMcAndMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody5, _finding1, _responseProcessing1, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseFactSetTest()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody2, _finding2, _responseProcessing2, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseFactSet_Polytomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody2, _finding3, _responseProcessing3, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseForCombinationOfFactSetsAndFactsOnFindingTest()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody3, _finding4, _responseProcessing4, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseForCombinationOfFactSetsAndFactsOnFinding_Polytomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody3, _finding5, _responseProcessing5, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseForCombinationOfFactSetsAndFactsOnFinding_EmptyFactsOnFinding_Polytomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody3, _finding6, _responseProcessing6, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub MultipleResponseForCombinationOfFactSetsAndFactsOnFinding_EmptyFactsOnFinding_Dichotomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody3, _finding7, _responseProcessing7, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

#Region "Items"

        Private _itemBody1 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="question">
                                <p id="c1-id-11">Wie weet waar Willem Wever woont?</p>
                            </div>
                            <div id="mc">
                                <qti-choice-interaction id="choiceInteraction1" class="" max-choices="0" shuffle="false" response-identifier="mc">
                                    <qti-simple-choice identifier="A">
                                        <p id="c1-id-11">A</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="B">
                                        <p id="c1-id-11">B</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="C">
                                        <p id="c1-id-11">C</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="D">
                                        <p id="c1-id-11">D</p>
                                    </qti-simple-choice>
                                </qti-choice-interaction>
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
                            <div id="question">
                                <p id="c1-id-11">Wat is samen 4?</p>
                            </div>
                            <div id="mc">
                                <qti-choice-interaction id="choiceInteraction1" class="" max-choices="0" shuffle="false" response-identifier="mc">
                                    <qti-simple-choice identifier="A">
                                        <p id="c1-id-11">1</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="B">
                                        <p id="c1-id-11">2</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="C">
                                        <p id="c1-id-11">2</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="D">
                                        <p id="c1-id-11">3</p>
                                    </qti-simple-choice>
                                </qti-choice-interaction>
                            </div>
                            <div id="itemgeneral">
                                <br/>
                                <p id="c1-id-11"> </p>
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
                            <div id="question">
                                <p id="c1-id-11">Welke antwoorden zijn juist ?</p>
                            </div>
                            <div id="mc">
                                <qti-choice-interaction id="choiceInteraction1" class="" max-choices="0" shuffle="false" response-identifier="mc">
                                    <qti-simple-choice identifier="A">
                                        <p id="c1-id-11">1</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="B">
                                        <p id="c1-id-11">2</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="C">
                                        <p id="c1-id-11">3</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="D">
                                        <p id="c1-id-11">4</p>
                                    </qti-simple-choice>
                                </qti-choice-interaction>
                            </div>
                            <div id="itemgeneral">
                                <br/>
                                <p id="c1-id-11"> </p>
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
                            <div id="question">
                                <p id="c1-id-11">Welk van de onderstaande stellingen is waar?</p>
                            </div>
                            <div id="mc">
                                <qti-choice-interaction id="choiceInteraction1" class="" max-choices="1" shuffle="false" response-identifier="mc">
                                    <qti-simple-choice identifier="A">
                                        <p id="c1-id-11">A</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="B">
                                        <p id="c1-id-11">B</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="C">
                                        <p id="c1-id-11">C</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="D">
                                        <p id="c1-id-11">D</p>
                                    </qti-simple-choice>
                                </qti-choice-interaction>
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
                            <div id="question">
                                <p id="c1-id-11">Wie weet waar Willem Wever woont?</p>
                            </div>
                            <div id="mc">
                                <qti-choice-interaction id="choiceInteraction1" class="" max-choices="1" shuffle="false" response-identifier="mc">
                                    <qti-simple-choice identifier="A">
                                        <p id="c1-id-11">A</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="B">
                                        <p id="c1-id-11">B</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="C">
                                        <p id="c1-id-11">C</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="D">
                                        <p id="c1-id-11">D</p>
                                    </qti-simple-choice>
                                </qti-choice-interaction>
                            </div>
                            <div id="mr">
                                <qti-choice-interaction id="choiceInteraction2" class="" max-choices="0" shuffle="false" response-identifier="mr">
                                    <qti-simple-choice identifier="A">
                                        <p id="c1-id-11">A</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="B">
                                        <p id="c1-id-11">B</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="C">
                                        <p id="c1-id-11">C</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="D">
                                        <p id="c1-id-11">D</p>
                                    </qti-simple-choice>
                                </qti-choice-interaction>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

#End Region

#Region "Expected results"

        Private _responseProcessing1 As XElement =
            <qti-response-processing>
                <qti-response-condition>
                    <qti-response-if>
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
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">C</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">D</qti-base-value>
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
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">C</qti-base-value>
                                        <qti-variable identifier="RESPONSE"/>
                                    </qti-member>
                                </qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">D</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
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
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">D</qti-base-value>
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
                                <qti-member>
                                    <qti-base-value base-type="identifier">D</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
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
                                <qti-not>
                                    <qti-member>
                                        <qti-base-value base-type="identifier">D</qti-base-value>
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

        Private _responseProcessing4 As XElement =
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
                            </qti-or>
                            <qti-member>
                                <qti-base-value base-type="identifier">D</qti-base-value>
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

        Private _responseProcessing5 As XElement =
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
                            <qti-base-value base-type="identifier">D</qti-base-value>
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

        Private _responseProcessing6 As XElement =
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

        Private _responseProcessing7 As XElement =
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

#End Region

    End Class

End Namespace