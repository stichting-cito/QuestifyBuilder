
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingInputIntegerTests

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerTest()

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
    Public Sub IntegerRangeTest()

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
    Public Sub IntegerAlternativesTest()

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
    Public Sub IntegerRangeTwiceTest()

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
    Public Sub IntegerComparisonTest()

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
    Public Sub IntegerComparisonAlternativesTest()

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

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerRangeAlternativesTest()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody7)
        Dim solution As Solution = _solution7.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing7, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerAlternativesFactSetRangeTest()

        'Arrange
        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody8)
        Dim solution As Solution = _solution8.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        'Act
        Dim result = processor.GetProcessing().ToXmlDocument()

        'Assert
        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing8, result))
    End Sub

    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
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
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I51372368-cad1-4946-9d37-f1ca446221c6" expectedLength="6"/> 
                            </p>
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
                        <baseValue baseType="integer">2</baseValue>
                    </equal>
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

    Private _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I310cc6e0-146e-45e4-a969-d2e89845dd64" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I310cc6e0-146e-45e4-a969-d2e89845dd64" occur="1">
                            <integerRangeValue rangeEnd="10" rangeStart="1"/>
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
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I310cc6e0-146e-45e4-a969-d2e89845dd64" expectedLength="6"/> </p>
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
                        <gte>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">1</baseValue>
                        </gte>
                        <lte>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">10</baseValue>
                        </lte>
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

    Private _solution3 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
                            <integerValue>
                                <typedValue>-3</typedValue>
                            </integerValue>
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

    Private _itemBody3 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I51372368-cad1-4946-9d37-f1ca446221c6" expectedLength="6"/> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _responseProcessing3 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">2</baseValue>
                        </equal>
                        <equal toleranceMode="exact">
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">-3</baseValue>
                        </equal>
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

    Private _itemBody4 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I310cc6e0-146e-45e4-a969-d2e89845dd64" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I8500a50f-9040-41b7-986b-1af7686d80e8" expectedLength="6"/> </p>
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
                    <keyFact id="1-I310cc6e0-146e-45e4-a969-d2e89845dd64" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I310cc6e0-146e-45e4-a969-d2e89845dd64" occur="1">
                            <integerRangeValue rangeEnd="10" rangeStart="1"/>
                        </keyValue>
                    </keyFact>
                    <keyFact id="1-I8500a50f-9040-41b7-986b-1af7686d80e8" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I8500a50f-9040-41b7-986b-1af7686d80e8" occur="1">
                            <integerRangeValue rangeEnd="22" rangeStart="11"/>
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
                    <and>
                        <and>
                            <gte>
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">1</baseValue>
                            </gte>
                            <lte>
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">10</baseValue>
                            </lte>
                        </and>
                        <and>
                            <gte>
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="integer">11</baseValue>
                            </gte>
                            <lte>
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="integer">22</baseValue>
                            </lte>
                        </and>
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

    Private _itemBody5 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="If5e784e4-08e3-4973-8b39-51fe66bf8c9c" expectedLength="6"/> </p>
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
                    <keyFact id="1-If5e784e4-08e3-4973-8b39-51fe66bf8c9c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="If5e784e4-08e3-4973-8b39-51fe66bf8c9c" occur="1">
                            <integerComparisonValue>
                                <typedComparisonValue>2</typedComparisonValue>
                                <comparisonType>SmallerThan</comparisonType>
                            </integerComparisonValue>
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
                    <lt>
                        <variable identifier="RESPONSE"/>
                        <baseValue baseType="integer">2</baseValue>
                    </lt>
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

    Private _itemBody6 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I51372368-cad1-4946-9d37-f1ca446221c6" expectedLength="6"/> </p>
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
                    <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                            <integerComparisonValue>
                                <typedComparisonValue>2</typedComparisonValue>
                                <comparisonType>SmallerThanEquals</comparisonType>
                            </integerComparisonValue>
                            <integerComparisonValue>
                                <typedComparisonValue>4</typedComparisonValue>
                                <comparisonType>GreaterThanEquals</comparisonType>
                            </integerComparisonValue>
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

    Private _responseProcessing6 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <lte>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">2</baseValue>
                        </lte>
                        <gte>
                            <variable identifier="RESPONSE"/>
                            <baseValue baseType="integer">4</baseValue>
                        </gte>
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

    Private _itemBody7 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I310cc6e0-146e-45e4-a969-d2e89845dd64" expectedLength="6"/> </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution7 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I310cc6e0-146e-45e4-a969-d2e89845dd64" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I310cc6e0-146e-45e4-a969-d2e89845dd64" occur="1">
                            <integerRangeValue rangeEnd="10" rangeStart="1"/>
                            <integerRangeValue rangeEnd="20" rangeStart="11"/>
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

    Private _responseProcessing7 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <gte>
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">1</baseValue>
                            </gte>
                            <lte>
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">10</baseValue>
                            </lte>
                        </and>
                        <and>
                            <gte>
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">11</baseValue>
                            </gte>
                            <lte>
                                <variable identifier="RESPONSE"/>
                                <baseValue baseType="integer">20</baseValue>
                            </lte>
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

    Private _itemBody8 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div>
                        <div id="itemquestion">
                            <p id="c1-id-11">Welke twee getallen zijn opgeteld 3?</p>
                        </div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">
                                <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I0dec2572-468c-49d9-bdff-c2482ec461c1" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="Ia896c73f-84fa-4ead-b3fc-210882efb8b9" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" expectedLength="6"/>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _solution8 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" occur="1">
                            <integerRangeValue rangeEnd="5" rangeStart="4"/>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="1-Ia896c73f-84fa-4ead-b3fc-210882efb8b9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ia896c73f-84fa-4ead-b3fc-210882efb8b9" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I0dec2572-468c-49d9-bdff-c2482ec461c1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0dec2572-468c-49d9-bdff-c2482ec461c1" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
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

    Private _responseProcessing8 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <and>
                            <or>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE"/>
                                    <baseValue baseType="integer">1</baseValue>
                                </equal>
                                <equal toleranceMode="exact">
                                    <variable identifier="RESPONSE"/>
                                    <baseValue baseType="integer">2</baseValue>
                                </equal>
                            </or>
                            <equal toleranceMode="exact">
                                <variable identifier="RESPONSE2"/>
                                <baseValue baseType="integer">3</baseValue>
                            </equal>
                        </and>
                        <and>
                            <gte>
                                <variable identifier="RESPONSE3"/>
                                <baseValue baseType="integer">4</baseValue>
                            </gte>
                            <lte>
                                <variable identifier="RESPONSE3"/>
                                <baseValue baseType="integer">5</baseValue>
                            </lte>
                        </and>
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
