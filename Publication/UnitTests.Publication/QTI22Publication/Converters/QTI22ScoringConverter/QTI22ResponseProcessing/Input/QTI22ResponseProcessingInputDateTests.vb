
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingInputDateTests

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub DateTest()

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
    Public Sub DateAlternativesTest()

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

    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I294446c7-6f39-4fd2-ad12-ad523209ac86" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I294446c7-6f39-4fd2-ad12-ad523209ac86" occur="1">
                            <stringValue>
                                <typedValue>07/11/2014</typedValue>
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

    Private _itemBody1 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div class="div_left">
                        <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_left_inner">

                        </div>
                    </div>
                    <div class="div_right">
                        <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_right_inner">
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <textEntryInteraction patternMask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="2" dateSubType="dutch"/>

								        -


								    <textEntryInteraction patternMask="^(?![0])(([0-9])|([1][0-2]))$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="2" dateSubType="dutch"/>

								        -


								    <textEntryInteraction patternMask="^([0-9]{1,4})$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="4" dateSubType="dutch"/>
                                    </span> </p>
                            </div>
                            <div id="answer">

                            </div>
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
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">07</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">11</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE3"/>
                            <baseValue baseType="integer">2014</baseValue>
                        </equal>
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
                    <div class="div_left">
                        <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_left_inner">

                        </div>
                    </div>
                    <div class="div_right">
                        <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_right_inner">
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <textEntryInteraction patternMask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="2" dateSubType="dutch"/>

								        -


								    <textEntryInteraction patternMask="^(?![0])(([0-9])|([1][0-2]))$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="2" dateSubType="dutch"/>

								        -


								    <textEntryInteraction patternMask="^([0-9]{1,4})$" responseIdentifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expectedLength="4" dateSubType="dutch"/>
                                    </span> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I294446c7-6f39-4fd2-ad12-ad523209ac86" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I294446c7-6f39-4fd2-ad12-ad523209ac86" occur="1">
                            <stringValue>
                                <typedValue>07/11/2014</typedValue>
                            </stringValue>
                            <stringValue>
                                <typedValue>07/21/2014</typedValue>
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

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">07</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="integer">11</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE3"/>
                                <baseValue baseType="integer">2014</baseValue>
                            </equal>
                        </and>
                        <and>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">07</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="integer">21</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE3"/>
                                <baseValue baseType="integer">2014</baseValue>
                            </equal>
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

End Class
