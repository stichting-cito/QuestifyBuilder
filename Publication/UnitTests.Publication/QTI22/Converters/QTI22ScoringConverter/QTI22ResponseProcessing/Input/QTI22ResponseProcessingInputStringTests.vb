
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingInputStringTests

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub StringTest()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody1)
        Dim solution As Solution = _solution1.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing1, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub StringPreprocessingTest()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody2)
        Dim solution As Solution = _solution2.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing2, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub StringPreprocessingAlternativesTest()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody3)
        Dim solution As Solution = _solution3.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing3, result))
    End Sub


    Private _itemBody1 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^.{0,5}$" responseIdentifier="Ieae9de03-f0cb-48fd-a5ad-909a5b78e562" expectedLength="5"/>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                        <div id="itemgeneral">
                            <br/>
                            <p id="c1-id-11"> </p>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-Ieae9de03-f0cb-48fd-a5ad-909a5b78e562" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ieae9de03-f0cb-48fd-a5ad-909a5b78e562" occur="1">
                            <stringValue>
                                <typedValue>wat</typedValue>
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

    Private _responseProcessing1 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <stringMatch caseSensitive="true">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="string">wat</baseValue>
                    </stringMatch>
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

    Private _itemBody2 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^.{0,5}$" responseIdentifier="Ie75a6cb7-dc38-488b-b8d0-bfb7539fb7ac" expectedLength="5"/> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-Ie75a6cb7-dc38-488b-b8d0-bfb7539fb7ac" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie75a6cb7-dc38-488b-b8d0-bfb7539fb7ac" occur="1">
                            <stringValue>
                                <typedValue>correctewaarde</typedValue>
                            </stringValue>
                            <preprocessingRule>
                                <ruleName>HLKL</ruleName>
                            </preprocessingRule>
                            <preprocessingRule>
                                <ruleName>YIJ</ruleName>
                            </preprocessingRule>
                            <preprocessingRule>
                                <ruleName>VSBE</ruleName>
                            </preprocessingRule>
                            <preprocessingRule>
                                <ruleName>VAP</ruleName>
                            </preprocessingRule>
                            <preprocessingRule>
                                <ruleName>VDT</ruleName>
                            </preprocessingRule>
                            <preprocessingRule>
                                <ruleName>VKT</ruleName>
                            </preprocessingRule>
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

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <stringMatch caseSensitive="true">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="string">correctewaarde</baseValue>
                    </stringMatch>
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

    Private _itemBody3 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^.{0,5}$" responseIdentifier="Ie75a6cb7-dc38-488b-b8d0-bfb7539fb7ac" expectedLength="5"/> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution3 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-Ie75a6cb7-dc38-488b-b8d0-bfb7539fb7ac" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie75a6cb7-dc38-488b-b8d0-bfb7539fb7ac" occur="1">
                            <stringValue>
                                <typedValue>waarde1</typedValue>
                            </stringValue>
                            <stringValue>
                                <typedValue>waarde2</typedValue>
                            </stringValue>
                            <preprocessingRule>
                                <ruleName>HLKL</ruleName>
                            </preprocessingRule>
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

    Private _responseProcessing3 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <stringMatch caseSensitive="true">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="string">waarde1</baseValue>
                        </stringMatch>
                        <stringMatch caseSensitive="true">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="string">waarde2</baseValue>
                        </stringMatch>
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

End Class
