
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingInputStringTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub StringTest()

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
        Public Sub StringPreprocessingTest()

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
        Public Sub StringPreprocessingAlternativesTest()

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


        Private _itemBody1 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^.{0,5}$" response-identifier="Ieae9de03-f0cb-48fd-a5ad-909a5b78e562" expected-length="5"/>
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
                </qti-item-body>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="string">wat</qti-base-value>
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
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^.{0,5}$" response-identifier="Ie75a6cb7-dc38-488b-b8d0-bfb7539fb7ac" expected-length="5"/> </p>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="string">correctewaarde</qti-base-value>
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

        Private _itemBody3 As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^.{0,5}$" response-identifier="Ie75a6cb7-dc38-488b-b8d0-bfb7539fb7ac" expected-length="5"/> </p>
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
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-or>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="string">waarde1</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="string">waarde2</qti-base-value>
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
