
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingInputIntegerTests
    Inherits QTI_Base.ResponseProcessingInputIntegerTestsBase

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemOneGap)
        Dim solution As Solution = _solution1.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing1, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerRangeTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemOneGap)
        Dim solution As Solution = _solution2.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing2, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerAlternativesTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemOneGap)
        Dim solution As Solution = _solution3.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing3, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerRangeTwiceTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemTwoGaps)
        Dim solution As Solution = _solution4.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing4, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerComparisonTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemOneGap)
        Dim solution As Solution = _solution5.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing5, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerComparisonAlternativesTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemOneGap)
        Dim solution As Solution = _solution6.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing6, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerRangeAlternativesTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemOneGap)
        Dim solution As Solution = _solution7.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing7, result))
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub IntegerAlternativesFactSetRangeTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemThreeGaps)
        Dim solution As Solution = _solution8.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

        Assert.IsTrue(UnitTestHelper.AreSame(_responseProcessing8, result))
    End Sub

    Private _itemOneGap As XElement =
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

    Private _itemTwoGaps As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I51372368-cad1-4946-9d37-f1ca446221c6" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I8500a50f-9040-41b7-986b-1af7686d80e8" expectedLength="6"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

    Private _itemThreeGaps As XElement =
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
                                    <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I51372368-cad1-4946-9d37-f1ca446221c6" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I8500a50f-9040-41b7-986b-1af7686d80e8" expectedLength="6"/> <textEntryInteraction patternMask="^-?([0-9]{1,5})?$" responseIdentifier="I0dec2572-468c-49d9-bdff-c2482ec461c1" expectedLength="6"/>
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
