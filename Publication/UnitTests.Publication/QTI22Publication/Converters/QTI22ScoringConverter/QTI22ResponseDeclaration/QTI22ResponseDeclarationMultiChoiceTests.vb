Imports Cito.Tester.ContentModel

<TestClass()>
Public Class QTI22ResponseDeclarationMultiChoiceTests
    Inherits QTI22ResponseDeclarationTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MC_Test()
        RunResponseDeclarationTest(_itemBody1, _solution1, GetMcScoringParams(), _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MC_NoFinding_Test()
        RunResponseDeclarationTest(_itemBody1, _solution2, GetMcScoringParams(), _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MR_Test()
        RunResponseDeclarationTest(_itemBody2, _solution3, GetMrScoringParams(), _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MR_Factset_Test()
        RunResponseDeclarationTest(_itemBody2, _solution4, GetMrScoringParams(), _result4, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MR_FindingFactsAndFactset_Test()
        RunResponseDeclarationTest(_itemBody2, _solution5, GetMrScoringParams(), _result5, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MR_Factsets_Test()
        RunResponseDeclarationTest(_itemBody2, _solution6, GetMrScoringParams(), _result6, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub MultipleResponseKeyFactsWithoutTrueValuesTest()
        RunResponseDeclarationTest(_itemBody1, _solution7, GetMrScoringParams(), _result7, 1)
    End Sub


    Private Function GetMcScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Radio}.AddSubParameters("A", "B", "C", "D")
        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function

    Private Function GetMrScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 3, .MaxChoices = 3, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C", "D", "E")
        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function



    Private _itemBody1 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div id="question">
                           <p id="c1-id-11">Welke van de onderstaande stellingen zijn waar?</p>
                       </div>
                       <div id="mc">
                           <choiceInteraction id="choiceInteraction1" class="" maxChoices="1" shuffle="false" responseIdentifier="mc">
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
                           </choiceInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

    Private _itemBody2 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div id="question">
                           <p id="c1-id-11">Welke van de onderstaande stellingen zijn waar?</p>
                       </div>
                       <div id="mc">
                           <choiceInteraction id="choiceInteraction1" class="" maxChoices="0" shuffle="false" responseIdentifier="mc">
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
                           </choiceInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="mc" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
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

    Private _solution2 As XElement =
        <solution>
            <keyFindings/>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution3 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
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

    Private _solution4 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="A-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
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

    Private _solution5 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="C-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
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

    Private _solution6 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
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

    Private _solution7 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="C-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="A-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="E-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
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



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A&amp;C&amp;E">
                    <value>A</value>
                    <value>C</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(B&amp;C&amp;E)|(A&amp;B&amp;E)">
                    <value>B</value>
                    <value>C</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A&amp;(D&amp;E)|(C&amp;D)">
                    <value>A</value>
                    <value>D</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A|B&amp;C&amp;E|D">
                    <value>A</value>
                    <value>C</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result7 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
        </responseDeclarations>


End Class
