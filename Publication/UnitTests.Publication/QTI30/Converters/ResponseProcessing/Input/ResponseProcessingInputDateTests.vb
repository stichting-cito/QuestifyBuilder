
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingInputDateTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub DateTest()

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
        Public Sub DateAlternativesTest()

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
                <qti-item-body class="defaultBody">
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
                                            <qti-text-entry-interaction pattern-mask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" response-identifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expected-length="2" date-sub-type="dutch"/>

								        -


								    <qti-text-entry-interaction pattern-mask="^(?![0])(([0-9])|([1][0-2]))$" response-identifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expected-length="2" date-sub-type="dutch"/>

								        -


								    <qti-text-entry-interaction pattern-mask="^([0-9]{1,4})$" response-identifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expected-length="4" date-sub-type="dutch"/>
                                        </span> </p>
                                </div>
                                <div id="answer">

                                </div>
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
                    <qti-base-value base-type="string">07/11/2014</qti-base-value>
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

        Private _itemBody2 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
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
                                            <qti-text-entry-interaction pattern-mask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" response-identifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expected-length="2" date-sub-type="dutch"/>

								        -


								    <qti-text-entry-interaction pattern-mask="^(?![0])(([0-9])|([1][0-2]))$" response-identifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expected-length="2" date-sub-type="dutch"/>

								        -


								    <qti-text-entry-interaction pattern-mask="^([0-9]{1,4})$" response-identifier="I294446c7-6f39-4fd2-ad12-ad523209ac86" expected-length="4" date-sub-type="dutch"/>
                                        </span> </p>
                                </div>
                                <div id="answer">

                                </div>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-or>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="string">07/11/2014</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="string">07/21/2014</qti-base-value>
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

    End Class

End Namespace
