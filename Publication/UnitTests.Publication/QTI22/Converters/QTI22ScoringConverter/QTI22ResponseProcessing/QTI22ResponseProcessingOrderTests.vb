
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

<TestClass()>
Public Class QTI22ResponseProcessingOrderTests
    Inherits QTI_Base.ResponseProcessingOrderTestsBase

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactSet_Dichotomous_Test()
        GetResponseProcessingTest(_finding1, _responseProcessing1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_Test()
        GetResponseProcessingTest(_finding2, _responseProcessing2)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_ReverseFinding_Dichotomous_Test()
        GetResponseProcessingTest(_finding5, _responseProcessing2)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactSet_Polytomous_Test()
        GetResponseProcessingTest(_finding3, _responseProcessing3)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_Test()
        GetResponseProcessingTest(_finding4, _responseProcessing4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
    Public Sub GetResponseProcessingForFactsOnFinding_ReverseFinding_Polytomous_Test()
        GetResponseProcessingTest(_finding6, _responseProcessing4)
    End Sub

    Public Sub GetResponseProcessingTest(findingElement As XElement, responseProcessingElement As XElement)
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetOrderScoringParams()
        QTI22PublicationTestHelper.RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    Private _itemBody1 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <styles xmlns="http://www.w3.org/1999/xhtml">
                        <stylecollection>
                            <style classname=".vertical .orderInteractionList" attributename="width" value="auto"/>
                        </stylecollection>
                    </styles>
                    <div xmlns="http://www.w3.org/1999/xhtml">
                        <div id="question">
                            <p id="c1-id-11">De vraag luidt...</p>
                        </div>
                        <div id="answer">
                            <orderInteraction responseIdentifier="orderController" shuffle="false" orientation="vertical" minChoices="1">
                                <simpleChoice identifier="A"><p id="c1-id-11">alt A</p></simpleChoice>
                                <simpleChoice identifier="B"><p id="c1-id-11">alt B</p></simpleChoice>
                                <simpleChoice identifier="C"><p id="c1-id-11">alt C</p></simpleChoice>
                            </orderInteraction>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

    Private _responseProcessing1 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <match>
                                <index n="1">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">A</baseValue>
                            </match>
                            <match>
                                <index n="2">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">B</baseValue>
                            </match>
                            <match>
                                <index n="3">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">C</baseValue>
                            </match>
                        </and>
                        <and>
                            <match>
                                <index n="1">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">C</baseValue>
                            </match>
                            <match>
                                <index n="2">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">B</baseValue>
                            </match>
                            <match>
                                <index n="3">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">A</baseValue>
                            </match>
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

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <match>
                            <index n="1">
                                <variable identifier="RESPONSE"/>
                            </index>
                            <baseValue baseType="identifier">C</baseValue>
                        </match>
                        <match>
                            <index n="2">
                                <variable identifier="RESPONSE"/>
                            </index>
                            <baseValue baseType="identifier">B</baseValue>
                        </match>
                        <match>
                            <index n="3">
                                <variable identifier="RESPONSE"/>
                            </index>
                            <baseValue baseType="identifier">A</baseValue>
                        </match>
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
                        <and>
                            <match>
                                <index n="1">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">A</baseValue>
                            </match>
                            <match>
                                <index n="2">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">B</baseValue>
                            </match>
                            <match>
                                <index n="3">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">C</baseValue>
                            </match>
                        </and>
                        <and>
                            <match>
                                <index n="1">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">C</baseValue>
                            </match>
                            <match>
                                <index n="2">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">B</baseValue>
                            </match>
                            <match>
                                <index n="3">
                                    <variable identifier="RESPONSE"/>
                                </index>
                                <baseValue baseType="identifier">A</baseValue>
                            </match>
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
        </responseProcessing>

    Private _responseProcessing4 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <match>
                        <index n="1">
                            <variable identifier="RESPONSE"/>
                        </index>
                        <baseValue baseType="identifier">C</baseValue>
                    </match>
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
                    <match>
                        <index n="2">
                            <variable identifier="RESPONSE"/>
                        </index>
                        <baseValue baseType="identifier">B</baseValue>
                    </match>
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
                    <match>
                        <index n="3">
                            <variable identifier="RESPONSE"/>
                        </index>
                        <baseValue baseType="identifier">A</baseValue>
                    </match>
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
