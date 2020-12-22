
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingMultiChoiceTests
    Inherits QTI22ResponseProcessingTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleChoiceTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMcScoringParams()
        RunResponseProcessingTest(_itemBody4, _finding8, _responseProcessing8, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleChoice_NoScoringParams_Test()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody4)
        Dim solution As Solution = _solution1.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing8, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleResponseTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
        RunResponseProcessingTest(_itemBody1, _finding1, _responseProcessing1, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleResponse_NoScoringParams_Test()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody1)
        Dim solution As Solution = _solution2.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing9, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleResponseFactSetTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
        RunResponseProcessingTest(_itemBody2, _finding2, _responseProcessing2, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleResponseFactSet_Polytomous_Test()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
        RunResponseProcessingTest(_itemBody2, _finding3, _responseProcessing3, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleResponseForCombinationOfFactSetsAndFactsOnFindingTest()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
        RunResponseProcessingTest(_itemBody3, _finding4, _responseProcessing4, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleResponseForCombinationOfFactSetsAndFactsOnFinding_Polytomous_Test()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
        RunResponseProcessingTest(_itemBody3, _finding5, _responseProcessing5, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleResponseForCombinationOfFactSetsAndFactsOnFinding_EmptyFactsOnFinding_Polytomous_Test()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
        RunResponseProcessingTest(_itemBody3, _finding6, _responseProcessing6, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub MultipleResponseForCombinationOfFactSetsAndFactsOnFinding_EmptyFactsOnFinding_Dichotomous_Test()
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetMrScoringParams()
        RunResponseProcessingTest(_itemBody3, _finding7, _responseProcessing7, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    Private Function GetMcScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Radio}.AddSubParameters("A", "B", "C", "D")
        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function

    Private Function GetMrScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 2, .MaxChoices = 2, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C", "D")
        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function

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
                            <choiceInteraction id="choiceInteraction1" class="" maxChoices="0" shuffle="false" responseIdentifier="mc">
                                <simpleChoice identifier="A">
                                    <p id="c1-id-11">A</p>
                                </simpleChoice>
                                <simpleChoice identifier="B">
                                    <p id="c1-id-11">B</p>
                                </simpleChoice>
                                <simpleChoice identifier="C">
                                    <p id="c1-id-11">C</p>
                                </simpleChoice>
                                <simpleChoice identifier="D">
                                    <p id="c1-id-11">D</p>
                                </simpleChoice>
                            </choiceInteraction>
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
                            <choiceInteraction id="choiceInteraction1" class="" maxChoices="0" shuffle="false" responseIdentifier="mc">
                                <simpleChoice identifier="A">
                                    <p id="c1-id-11">1</p>
                                </simpleChoice>
                                <simpleChoice identifier="B">
                                    <p id="c1-id-11">2</p>
                                </simpleChoice>
                                <simpleChoice identifier="C">
                                    <p id="c1-id-11">2</p>
                                </simpleChoice>
                                <simpleChoice identifier="D">
                                    <p id="c1-id-11">3</p>
                                </simpleChoice>
                            </choiceInteraction>
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
                            <choiceInteraction id="choiceInteraction1" class="" maxChoices="0" shuffle="false" responseIdentifier="mc">
                                <simpleChoice identifier="A">
                                    <p id="c1-id-11">1</p>
                                </simpleChoice>
                                <simpleChoice identifier="B">
                                    <p id="c1-id-11">2</p>
                                </simpleChoice>
                                <simpleChoice identifier="C">
                                    <p id="c1-id-11">3</p>
                                </simpleChoice>
                                <simpleChoice identifier="D">
                                    <p id="c1-id-11">4</p>
                                </simpleChoice>
                            </choiceInteraction>
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
                            <choiceInteraction id="choiceInteraction1" class="" maxChoices="1" shuffle="false" responseIdentifier="mc">
                                <simpleChoice identifier="A">
                                    <p id="c1-id-11">A</p>
                                </simpleChoice>
                                <simpleChoice identifier="B">
                                    <p id="c1-id-11">B</p>
                                </simpleChoice>
                                <simpleChoice identifier="C">
                                    <p id="c1-id-11">C</p>
                                </simpleChoice>
                                <simpleChoice identifier="D">
                                    <p id="c1-id-11">D</p>
                                </simpleChoice>
                            </choiceInteraction>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

#End Region

#Region "Solutions/findings"

    Private _finding1 As XElement =
        <keyFinding id="mc" scoringMethod="Dichotomous">
            <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="D-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="A-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="B-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="C-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="C-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _finding2 As XElement =
        <keyFinding id="mc" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding3 As XElement =
        <keyFinding id="mc" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding4 As XElement =
            <keyFinding id="mc" scoringMethod="Dichotomous">
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

    Private _finding5 As XElement =
            <keyFinding id="mc" scoringMethod="Polytomous">
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

    Private _finding6 As XElement =
            <keyFinding id="mc" scoringMethod="Polytomous">
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

    Private _finding7 As XElement =
            <keyFinding id="mc" scoringMethod="Dichotomous">
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

    Private _finding8 As XElement =
        <keyFinding id="mc" scoringMethod="Dichotomous">
            <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="mc" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="mc" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="mc" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="mc" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

#End Region

#Region "Expected results"

    Private _responseProcessing1 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
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
                        <not>
                            <member>
                                <baseValue baseType="identifier">C</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">D</baseValue>
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
                            <not>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">D</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
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
                            <not>
                                <member>
                                    <baseValue baseType="identifier">D</baseValue>
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
                            <not>
                                <member>
                                    <baseValue baseType="identifier">C</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">D</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
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
                            <not>
                                <member>
                                    <baseValue baseType="identifier">D</baseValue>
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

    Private _responseProcessing4 As XElement =
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
                        </or>
                        <member>
                            <baseValue baseType="identifier">D</baseValue>
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

    Private _responseProcessing5 As XElement =
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
                        <baseValue baseType="identifier">D</baseValue>
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

    Private _responseProcessing6 As XElement =
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

    Private _responseProcessing7 As XElement =
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
                            <member>
                                <baseValue baseType="identifier">B</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">D</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
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

#End Region

End Class
