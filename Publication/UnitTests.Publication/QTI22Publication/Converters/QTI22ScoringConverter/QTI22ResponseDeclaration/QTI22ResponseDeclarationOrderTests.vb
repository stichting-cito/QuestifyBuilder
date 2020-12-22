Imports Cito.Tester.ContentModel

<TestClass()>
Public Class QTI22ResponseDeclarationOrderTests
    Inherits QTI22ResponseDeclarationTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactSetsTest()
        RunResponseDeclarationTest(_itemBody1, _solution1, GetOrderScoringParams(), _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactsOnFindingTest()
        RunResponseDeclarationTest(_itemBody1, _solution2, GetOrderScoringParams(), _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForMixedUpFactsTest()
        RunResponseDeclarationTest(_itemBody1, _solution3, GetOrderScoringParams(), _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetEmptyResponseDeclarationTest()
        RunResponseDeclarationTest(_itemBody1, _solution4, GetOrderScoringParams(), _result4, 1)
    End Sub


    Private Function GetOrderScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        scoreParams.Add(New OrderScoringParameter() With {.ControllerId = "orderController", .FindingOverride = "orderController"}.AddSubParameters("A", "B", "C", "D", "E", "F"))
        Return scoreParams
    End Function



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
                           <p id="c1-id-11">Zet de alternatieven in de juiste volgorde</p>
                       </div>
                       <div id="answer">
                           <orderInteraction responseIdentifier="orderController" shuffle="false" orientation="vertical" minChoices="1">
                               <simpleChoice identifier="A">
                                   <p id="c1-id-11">A</p>
                               </simpleChoice>
                               <simpleChoice identifier="B">
                                   <p id="c1-id-11">B</p>
                               </simpleChoice>
                               <simpleChoice identifier="C">
                                   <p id="c1-id-11">C</p>
                               </simpleChoice>
                               <simpleChoice identifier="D">
                                   <p id="c1-id-11">D</p>
                               </simpleChoice>
                               <simpleChoice identifier="E">
                                   <p id="c1-id-11">E</p>
                               </simpleChoice>
                               <simpleChoice identifier="F">
                                   <p id="c1-id-11">F</p>
                               </simpleChoice>
                           </orderInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="orderController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>4</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>5</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>6</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>4</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>5</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>6</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
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

    Private _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="orderController" scoringMethod="Dichotomous">
                    <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>4</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>5</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>6</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>1</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
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

    Private _solution3 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="orderController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>6</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>4</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>5</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
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

    Private _solution4 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="orderController" scoringMethod="Dichotomous">
                        <keyFactSet/>
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="ordered" baseType="identifier">
                <correctResponse interpretation="(C&amp;D&amp;E&amp;F&amp;A&amp;B)|(D&amp;E&amp;F&amp;A&amp;B&amp;C)">
                    <value>C</value>
                    <value>D</value>
                    <value>E</value>
                    <value>F</value>
                    <value>A</value>
                    <value>B</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="ordered" baseType="identifier">
                <correctResponse interpretation="C&amp;D&amp;E&amp;F&amp;A&amp;B">
                    <value>C</value>
                    <value>D</value>
                    <value>E</value>
                    <value>F</value>
                    <value>A</value>
                    <value>B</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="ordered" baseType="identifier">
                <correctResponse interpretation="(C&amp;D&amp;E&amp;F&amp;A&amp;B)">
                    <value>C</value>
                    <value>D</value>
                    <value>E</value>
                    <value>F</value>
                    <value>A</value>
                    <value>B</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="ordered" baseType="string"/>
        </responseDeclarations>


End Class
