﻿
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingInputCurrencyTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
        Public Sub CurrencyTest()

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

        Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="gapController" scoringMethod="Dichotomous">
                    <keyFact id="1-I34274ebd-1a48-4b7b-9954-b29b00d79ade" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I34274ebd-1a48-4b7b-9954-b29b00d79ade" occur="1">
                            <decimalValue>
                                <typedValue>1.99</typedValue>
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
                               <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?(([\,])([0-9]{0,2}))?$" response-identifier="I34274ebd-1a48-4b7b-9954-b29b00d79ade" expected-length="8"/> </p>
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
                <qti-base-value base-type="float">1.99</qti-base-value>
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

    End Class

End Namespace
