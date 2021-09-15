
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingInputTimeTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub TimeTest()

            'Arrange
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody1)
            Dim solution As Solution = _solution1.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim findingIndex As Integer = 0
            Dim processor = New ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New CombinedScoringConverter, False, False)

            'Act
            Dim result = processor.GetProcessing().ToXmlDocument()

            'Assert
            Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing1, result))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub TimeTwiceTest()

            'Arrange
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody2)
            Dim solution As Solution = _solution2.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim findingIndex As Integer = 0
            Dim processor = New ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New CombinedScoringConverter, False, False)

            'Act
            Dim result = processor.GetProcessing().ToXmlDocument()

            'Assert
            Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing2, result))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub TimeAlternativesTest()

            'Arrange
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody3)
            Dim solution As Solution = _solution3.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim findingIndex As Integer = 0
            Dim processor = New ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New CombinedScoringConverter, False, False)

            'Act
            Dim result = processor.GetProcessing().ToXmlDocument()

            'Assert
            Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing3, result))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub TimeFactSetAlternativesTest()

            'Arrange
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody4)
            Dim solution As Solution = _solution4.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim findingIndex As Integer = 0
            Dim processor = New ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New CombinedScoringConverter, False, False)

            'Act
            Dim result = processor.GetProcessing().ToXmlDocument()

            'Assert
            Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing4, result))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub TimeTwicePolytomousTest()

            'Arrange
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody5)
            Dim solution As Solution = _solution5.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim findingIndex As Integer = 0
            Dim processor = New ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New CombinedScoringConverter, False, False)

            'Act
            Dim result = processor.GetProcessing().ToXmlDocument()

            'Assert
            Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing5, result))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub TimeFactSetsAlternativesPolytomousTest()

            'Arrange
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody6)
            Dim solution As Solution = _solution6.Deserialize(Of Solution)()
            Dim finding As KeyFinding = solution.Findings(0)
            Dim findingIndex As Integer = 0
            Dim processor = New ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New CombinedScoringConverter, False, False)

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
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>
                                    </span> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _responseProcessing1 As XElement =
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="string">11:11</qti-base-value>
                </qti-string-match>
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
               <qti-item-body class="defaultBody">
                   <div class="content">
                       <div>
                           <div id="questionwithinlinecontrol">
                               <p id="c1-id-11">
                                   <span>
                                       <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>
                                   </span> <span>
                                       <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I45b1e6eb-d642-40b5-8727-8c398996a452" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I45b1e6eb-d642-40b5-8727-8c398996a452" expected-length="2" time-sub-type="hhmm"/>
                                   </span>
                               </p>
                           </div>
                           <div id="answer">

                           </div>
                       </div>
                   </div>
               </qti-item-body>
           </wrapper>

        Private _responseProcessing2 As XElement =
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-and>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="string">11:12</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE3"/>
                        <qti-base-value base-type="string">12:13</qti-base-value>
                    </qti-string-match>
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

        Private _itemBody3 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>
                                    </span> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-or>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="string">10:00</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="string">12:00</qti-base-value>
                    </qti-string-match>
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

        Private _itemBody4 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>
                                    </span> <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expected-length="2" time-sub-type="hhmm"/>
                                    </span>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-or>
                    <qti-and>
                        <qti-or>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="string">10:00</qti-base-value>
                            </qti-string-match>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="string">12:00</qti-base-value>
                            </qti-string-match>
                        </qti-or>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE3"/>
                            <qti-base-value base-type="string">09:00</qti-base-value>
                        </qti-string-match>
                    </qti-and>
                    <qti-and>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE"/>
                            <qti-base-value base-type="string">01:00</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE3"/>
                            <qti-base-value base-type="string">02:00</qti-base-value>
                        </qti-string-match>
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

        Private _itemBody5 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>
                                    </span> <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expected-length="2" time-sub-type="hhmm"/>
                                    </span>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="string">10:00</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE3"/>
                    <qti-base-value base-type="string">12:00</qti-base-value>
                </qti-string-match>
                <qti-set-outcome-value identifier="SCORE">
                    <qti-sum>
                        <qti-base-value base-type="float">1</qti-base-value>
                        <qti-variable identifier="SCORE"/>
                    </qti-sum>
                </qti-set-outcome-value>
            </qti-response-if>
        </qti-response-condition>
    </qti-response-processing>

        Private _itemBody6 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" expected-length="2" time-sub-type="hhmm"/>
                                    </span> <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" expected-length="2" time-sub-type="hhmm"/>
                                    </span> <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Iec08a2e8-c0e8-4462-acc0-02a7f939ac0d" expected-length="2" time-sub-type="hhmm"/>

								    :

								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Iec08a2e8-c0e8-4462-acc0-02a7f939ac0d" expected-length="2" time-sub-type="hhmm"/>
                                    </span>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-or>
                    <qti-and>
                        <qti-or>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="string">10:00</qti-base-value>
                            </qti-string-match>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE"/>
                                <qti-base-value base-type="string">11:00</qti-base-value>
                            </qti-string-match>
                        </qti-or>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE3"/>
                            <qti-base-value base-type="string">12:00</qti-base-value>
                        </qti-string-match>
                    </qti-and>
                    <qti-and>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE"/>
                            <qti-base-value base-type="string">01:00</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE3"/>
                            <qti-base-value base-type="string">02:00</qti-base-value>
                        </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE5"/>
                    <qti-base-value base-type="string">03:00</qti-base-value>
                </qti-string-match>
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
