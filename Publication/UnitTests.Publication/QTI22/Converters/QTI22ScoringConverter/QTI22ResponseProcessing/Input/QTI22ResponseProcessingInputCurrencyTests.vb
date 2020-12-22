
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class QTI22ResponseProcessingInputCurrencyTests

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("ResponseProcessing")>
    Public Sub CurrencyTest()

        Dim responseIdentifierAttributeList As XmlNodeList = QTI22PublicationTestHelper.GetResponseIdentifiers(_itemBody1)
        Dim solution As Solution = _solution1.Deserialize(Of Solution)()
        Dim finding As KeyFinding = solution.Findings(0)
        Dim findingIndex As Integer = 0
        Dim processor = New QTI22ResponseProcessing(responseIdentifierAttributeList, solution, finding, findingIndex, Nothing, New QTI22CombinedScoringConverter, False)

        Dim result = processor.GetProcessing().ToXmlDocument()

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
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div id="questionwithinlinecontrol">
                           <p id="c1-id-11">
                               <textEntryInteraction patternMask="^-?([0-9]{1,5})?(([\,])([0-9]{0,2}))?$" responseIdentifier="I34274ebd-1a48-4b7b-9954-b29b00d79ade" expectedLength="8"/> </p>
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
                        <baseValue baseType="float">1.99</baseValue>
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

End Class
