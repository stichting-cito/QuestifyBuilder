
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingInputDecimalTests

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub DecimalTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody1)
        Dim solution As Solution = _solution1.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing1, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub DecimalRangeTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody2)
        Dim solution As Solution = _solution2.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing2, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub DecimalAlternativesTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody3)
        Dim solution As Solution = _solution3.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing3, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub DecimalComparisonTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody4)
        Dim solution As Solution = _solution4.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing4, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub DecimalComparisonAlternativesTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody5)
        Dim solution As Solution = _solution5.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing5, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub DecimalFactSetTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody6)
        Dim solution As Solution = _solution6.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

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
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,3})?(([\,])([0-9]{0,2}))?$" responseIdentifier="I9594b772-0cbc-4e67-a957-bf03ea76559f" expectedLength="6"/> </p>
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
                    <equal toleranceMode="exact">
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="float">111.11</baseValue>
                    </equal>
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
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?(([\,])([0-9]{0,5}))?$" responseIdentifier="Id2ab396a-d016-40f3-9775-5ceed3c6b23e" expectedLength="11"/> </p>
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
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <gte>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="float">0.01</baseValue>
                        </gte>
                        <lte>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="float">0.99</baseValue>
                        </lte>
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
                                <textEntryInteraction patternMask="^-?([0-9]{1,3})?(([\,])([0-9]{0,2}))?$" responseIdentifier="I9594b772-0cbc-4e67-a957-bf03ea76559f" expectedLength="6"/> </p>
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
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="float">111.11</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="float">222.22</baseValue>
                        </equal>
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
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?(([\,])([0-9]{0,5}))?$" responseIdentifier="Ic73ac378-1419-4b02-bb3a-5281bc13e23f" expectedLength="11"/> </p>
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
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <gt>
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="float">1.1</baseValue>
                    </gt>
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
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?(([\,])([0-9]{0,5}))?$" responseIdentifier="Ic73ac378-1419-4b02-bb3a-5281bc13e23f" expectedLength="11"/> </p>
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
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <gt>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="float">1.1</baseValue>
                        </gt>
                        <lt>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="float">0.9</baseValue>
                        </lt>
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

    Private _itemBody6 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,3})?(([\,])([0-9]{0,2}))?$" responseIdentifier="I9594b772-0cbc-4e67-a957-bf03ea76559f" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,3})?(([\,])([0-9]{0,2}))?$" responseIdentifier="I7cea3261-917d-478e-8906-7e17733b57bb" expectedLength="6"/>
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
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="float">1.1</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="float">2.2</baseValue>
                            </equal>
                        </and>
                        <and>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="float">2.2</baseValue>
                            </equal>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="float">1.1</baseValue>
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

End Class
