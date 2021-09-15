
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingInputTimeTests

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub TimeTest()

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
    Public Sub TimeTwiceTest()

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
    Public Sub TimeAlternativesTest()

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

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub TimeFactSetAlternativesTest()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody4)
        Dim solution As Solution = _solution4.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing4, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub TimeTwicePolytomousTest()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody5)
        Dim solution As Solution = _solution5.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing5, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub TimeFactSetsAlternativesPolytomousTest()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody6)
        Dim solution As Solution = _solution6.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing6, result))
    End Sub


    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                            <stringValue>
                                <typedValue>11:11</typedValue>
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
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
                                </span> </p>
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
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">11</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">11</baseValue>
                        </equal>
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

    Private _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                            <stringValue>
                                <typedValue>11:12</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="1-I45b1e6eb-d642-40b5-8727-8c398996a452" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I45b1e6eb-d642-40b5-8727-8c398996a452" occur="1">
                            <stringValue>
                                <typedValue>12:13</typedValue>
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

    Private _itemBody2 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div id="questionwithinlinecontrol">
                           <p id="c1-id-11">
                               <span>
                                   <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
                               </span> <span>
                                   <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I45b1e6eb-d642-40b5-8727-8c398996a452" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I45b1e6eb-d642-40b5-8727-8c398996a452" expectedLength="2" timeSubType="hhmm"/>
                               </span>
                           </p>
                       </div>
                       <div id="answer">

                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <and>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">11</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="integer">12</baseValue>
                            </equal>
                        </and>
                        <and>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE3"/>
                                <baseValue baseType="integer">12</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE4"/>
                                <baseValue baseType="integer">13</baseValue>
                            </equal>
                        </and>
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

    Private _itemBody3 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
                                </span> </p>
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
                    <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                            <stringValue>
                                <typedValue>10:00</typedValue>
                            </stringValue>
                            <stringValue>
                                <typedValue>12:00</typedValue>
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

    Private _responseProcessing3 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">10</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="integer">00</baseValue>
                            </equal>
                        </and>
                        <and>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">12</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="integer">00</baseValue>
                            </equal>
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

    Private _itemBody4 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
                                </span> <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>
                                </span>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution4 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                                <stringValue>
                                    <typedValue>10:00</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>12:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                                <stringValue>
                                    <typedValue>09:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                                <stringValue>
                                    <typedValue>01:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                                <stringValue>
                                    <typedValue>02:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _responseProcessing4 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <or>
                                <and>
                                    <equal toleranceMode="exact">
                                        <variable identifier="RESPONSE"/>
                                        <baseValue baseType="integer">10</baseValue>
                                    </equal>
                                    <equal toleranceMode="exact">
                                        <variable identifier="RESPONSE2"/>
                                        <baseValue baseType="integer">00</baseValue>
                                    </equal>
                                </and>
                                <and>
                                    <equal toleranceMode="exact">
                                        <variable identifier="RESPONSE"/>
                                        <baseValue baseType="integer">12</baseValue>
                                    </equal>
                                    <equal toleranceMode="exact">
                                        <variable identifier="RESPONSE2"/>
                                        <baseValue baseType="integer">00</baseValue>
                                    </equal>
                                </and>
                            </or>
                            <and>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE3"/>
                                    <baseValue baseType="integer">09</baseValue>
                                </equal>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE4"/>
                                    <baseValue baseType="integer">00</baseValue>
                                </equal>
                            </and>
                        </and>
                        <and>
                            <and>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE"/>
                                    <baseValue baseType="integer">01</baseValue>
                                </equal>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE2"/>
                                    <baseValue baseType="integer">00</baseValue>
                                </equal>
                            </and>
                            <and>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE3"/>
                                    <baseValue baseType="integer">02</baseValue>
                                </equal>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE4"/>
                                    <baseValue baseType="integer">00</baseValue>
                                </equal>
                            </and>
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

    Private _itemBody5 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
                                </span> <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>
                                </span>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution5 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Polytomous">
                    <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                            <stringValue>
                                <typedValue>10:00</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                            <stringValue>
                                <typedValue>12:00</typedValue>
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

    Private _responseProcessing5 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">10</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE2"/>
                            <baseValue baseType="integer">00</baseValue>
                        </equal>
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
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE3"/>
                            <baseValue baseType="integer">12</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE4"/>
                            <baseValue baseType="integer">00</baseValue>
                        </equal>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _itemBody6 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expectedLength="2" timeSubType="hhmm"/>
                                </span> <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expectedLength="2" timeSubType="hhmm"/>
                                </span> <span>
                                    <textEntryInteraction patternMask="^(([0-1]?[0-9])|([2][0-3]))$" responseIdentifier="Iec08a2e8-c0e8-4462-acc0-02a7f939ac0d" expectedLength="2" timeSubType="hhmm"/>

								    :

								    <textEntryInteraction patternMask="^([0-5]?[0-9])$" responseIdentifier="Iec08a2e8-c0e8-4462-acc0-02a7f939ac0d" expectedLength="2" timeSubType="hhmm"/>
                                </span>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution6 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Polytomous">
                    <keyFact id="1-Iec08a2e8-c0e8-4462-acc0-02a7f939ac0d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Iec08a2e8-c0e8-4462-acc0-02a7f939ac0d" occur="1">
                            <stringValue>
                                <typedValue>03:00</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                                <stringValue>
                                    <typedValue>12:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                                <stringValue>
                                    <typedValue>10:00</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>11:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                                <stringValue>
                                    <typedValue>01:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                                <stringValue>
                                    <typedValue>02:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _responseProcessing6 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <or>
                                <and>
                                    <equal toleranceMode="exact">
                                        <variable identifier="RESPONSE"/>
                                        <baseValue baseType="integer">10</baseValue>
                                    </equal>
                                    <equal toleranceMode="exact">
                                        <variable identifier="RESPONSE2"/>
                                        <baseValue baseType="integer">00</baseValue>
                                    </equal>
                                </and>
                                <and>
                                    <equal toleranceMode="exact">
                                        <variable identifier="RESPONSE"/>
                                        <baseValue baseType="integer">11</baseValue>
                                    </equal>
                                    <equal toleranceMode="exact">
                                        <variable identifier="RESPONSE2"/>
                                        <baseValue baseType="integer">00</baseValue>
                                    </equal>
                                </and>
                            </or>
                            <and>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE3"/>
                                    <baseValue baseType="integer">12</baseValue>
                                </equal>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE4"/>
                                    <baseValue baseType="integer">00</baseValue>
                                </equal>
                            </and>
                        </and>
                        <and>
                            <and>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE"/>
                                    <baseValue baseType="integer">01</baseValue>
                                </equal>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE2"/>
                                    <baseValue baseType="integer">00</baseValue>
                                </equal>
                            </and>
                            <and>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE3"/>
                                    <baseValue baseType="integer">02</baseValue>
                                </equal>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE4"/>
                                    <baseValue baseType="integer">00</baseValue>
                                </equal>
                            </and>
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
                    <and>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE5"/>
                            <baseValue baseType="integer">03</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE6"/>
                            <baseValue baseType="integer">00</baseValue>
                        </equal>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="float">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

End Class
