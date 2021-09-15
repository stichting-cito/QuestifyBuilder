
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingInputDecimalTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub DecimalTest()

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
        Public Sub DecimalRangeTest()

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
        Public Sub DecimalAlternativesTest()

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
        Public Sub DecimalComparisonTest()

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
        Public Sub DecimalComparisonAlternativesTest()

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
        Public Sub DecimalFactSetTest()

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
                        <keyFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                <decimalValue>
                                    <typedValue>111.11</typedValue>
                                </decimalValue>
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
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,3})?(([\,])([0-9]{0,2}))?$" response-identifier="I9594b772-0cbc-4e67-a957-bf03ea76559f" expected-length="6"/> </p>
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
                <qti-equal tolerance-mode="exact">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="float">111.11</qti-base-value>
                </qti-equal>
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
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?(([\,])([0-9]{0,5}))?$" response-identifier="Id2ab396a-d016-40f3-9775-5ceed3c6b23e" expected-length="11"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _solution2 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-Id2ab396a-d016-40f3-9775-5ceed3c6b23e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Id2ab396a-d016-40f3-9775-5ceed3c6b23e" occur="1">
                                <decimalRangeValue rangeEnd="0.99" rangeStart="0.01"/>
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
                <qti-and>
                    <qti-gte>
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="float">0.01</qti-base-value>
                    </qti-gte>
                    <qti-lte>
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="float">0.99</qti-base-value>
                    </qti-lte>
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
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,3})?(([\,])([0-9]{0,2}))?$" response-identifier="I9594b772-0cbc-4e67-a957-bf03ea76559f" expected-length="6"/> </p>
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
                        <keyFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                <decimalValue>
                                    <typedValue>111.11</typedValue>
                                </decimalValue>
                                <decimalValue>
                                    <typedValue>222.22</typedValue>
                                </decimalValue>
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
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="float">111.11</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="float">222.22</qti-base-value>
                    </qti-equal>
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
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?(([\,])([0-9]{0,5}))?$" response-identifier="Ic73ac378-1419-4b02-bb3a-5281bc13e23f" expected-length="11"/> </p>
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
                        <keyFact id="1-Ic73ac378-1419-4b02-bb3a-5281bc13e23f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ic73ac378-1419-4b02-bb3a-5281bc13e23f" occur="1">
                                <decimalComparisonValue>
                                    <typedComparisonValue>1.1</typedComparisonValue>
                                    <comparisonType>GreaterThan</comparisonType>
                                </decimalComparisonValue>
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

        Private _responseProcessing4 As XElement =
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-gt>
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="float">1.1</qti-base-value>
                </qti-gt>
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
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?(([\,])([0-9]{0,5}))?$" response-identifier="Ic73ac378-1419-4b02-bb3a-5281bc13e23f" expected-length="11"/> </p>
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
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-Ic73ac378-1419-4b02-bb3a-5281bc13e23f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ic73ac378-1419-4b02-bb3a-5281bc13e23f" occur="1">
                                <decimalComparisonValue>
                                    <typedComparisonValue>1.1</typedComparisonValue>
                                    <comparisonType>GreaterThan</comparisonType>
                                </decimalComparisonValue>
                                <decimalComparisonValue>
                                    <typedComparisonValue>0.9</typedComparisonValue>
                                    <comparisonType>SmallerThan</comparisonType>
                                </decimalComparisonValue>
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
                <qti-or>
                    <qti-gt>
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="float">1.1</qti-base-value>
                    </qti-gt>
                    <qti-lt>
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="float">0.9</qti-base-value>
                    </qti-lt>
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

        Private _itemBody6 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,3})?(([\,])([0-9]{0,2}))?$" response-identifier="I9594b772-0cbc-4e67-a957-bf03ea76559f" expected-length="6"/> 
                                <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,3})?(([\,])([0-9]{0,2}))?$" response-identifier="I7cea3261-917d-478e-8906-7e17733b57bb" expected-length="6"/>
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
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="1-I7cea3261-917d-478e-8906-7e17733b57bb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7cea3261-917d-478e-8906-7e17733b57bb" occur="1">
                                    <decimalValue>
                                        <typedValue>2.2</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                    <decimalValue>
                                        <typedValue>1.1</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-I9594b772-0cbc-4e67-a957-bf03ea76559f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9594b772-0cbc-4e67-a957-bf03ea76559f" occur="1">
                                    <decimalValue>
                                        <typedValue>2.2</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I7cea3261-917d-478e-8906-7e17733b57bb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7cea3261-917d-478e-8906-7e17733b57bb" occur="1">
                                    <decimalValue>
                                        <typedValue>1.1</typedValue>
                                    </decimalValue>
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
                        <qti-equal tolerance-mode="exact">
                            <qti-variable identifier="RESPONSE"/>
                            <qti-base-value base-type="float">1.1</qti-base-value>
                        </qti-equal>
                        <qti-equal tolerance-mode="exact">
                            <qti-variable identifier="RESPONSE2"/>
                            <qti-base-value base-type="float">2.2</qti-base-value>
                        </qti-equal>
                    </qti-and>
                    <qti-and>
                        <qti-equal tolerance-mode="exact">
                            <qti-variable identifier="RESPONSE"/>
                            <qti-base-value base-type="float">2.2</qti-base-value>
                        </qti-equal>
                        <qti-equal tolerance-mode="exact">
                            <qti-variable identifier="RESPONSE2"/>
                            <qti-base-value base-type="float">1.1</qti-base-value>
                        </qti-equal>
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

    End Class

End Namespace
