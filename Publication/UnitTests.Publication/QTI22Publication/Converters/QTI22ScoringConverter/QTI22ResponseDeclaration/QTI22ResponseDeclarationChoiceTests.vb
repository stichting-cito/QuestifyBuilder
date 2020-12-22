Imports Cito.Tester.ContentModel

<TestClass()>
Public Class QTI22ResponseDeclarationChoiceTests
    Inherits QTI22ResponseDeclarationTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_Test()
        RunResponseDeclarationTest(_itemBody1, _solution1, GetInlineChoiceScoringParams(), _result1, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_NoFinding_Test()
        RunResponseDeclarationTest(_itemBody1, _solution2, GetInlineChoiceScoringParams(), _result2, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_Factset_Test()
        RunResponseDeclarationTest(_itemBody1, _solution3, GetInlineChoiceScoringParams(), _result3, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_FindingFactsAndFactset_Test()
        RunResponseDeclarationTest(_itemBody1, _solution4, GetInlineChoiceScoringParams(), _result4, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_Factsets_Test()
        RunResponseDeclarationTest(_itemBody1, _solution5, GetInlineChoiceScoringParams(), _result5, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_InCompleteFinding_Test()
        RunResponseDeclarationTest(_itemBody1, _solution6, GetInlineChoiceScoringParams(), _result6, 4)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub IC_Factset_InCompleteFinding_Test()
        RunResponseDeclarationTest(_itemBody1, _solution7, GetInlineChoiceScoringParams(), _result7, 4)
    End Sub


    Private Function GetInlineChoiceScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)
        Dim scoreParam1 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice1", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I13abebba-ebb7-4ed8-81d0-46e9e1b60a53", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        scoreParams.Add(scoreParam1)
        Dim scoreParam2 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice2", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "Ie830b508-32a0-42f4-920f-0e06f0f19313", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E")
        scoreParams.Add(scoreParam2)
        Dim scoreParam3 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice3", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I22f81e91-aaa4-4528-999e-61b31b8123ec", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
        scoreParams.Add(scoreParam3)
        Dim scoreParam4 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice4", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I5196c3f8-ddf3-4423-b777-d1d2c71d6efb", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E", "F")
        scoreParams.Add(scoreParam4)

        Return scoreParams
    End Function



    Private _itemBody1 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <itemBody class="defaultBody">
               <div class="content">
                   <div xmlns="http://www.w3.org/1999/xhtml">
                       <div id="questionwithinlinecontrol">
                           <p id="c1-id-11">Drop down 1 <inlineChoiceInteraction responseIdentifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                   <inlineChoice identifier="A">alt.A</inlineChoice>
                                   <inlineChoice identifier="B">alt.B</inlineChoice>
                                   <inlineChoice identifier="C">alt.C</inlineChoice>
                                   <inlineChoice identifier="D">alt.D</inlineChoice>
                               </inlineChoiceInteraction>
                           </p>
                           <p id="c1-id-12">Drop down 2 <inlineChoiceInteraction responseIdentifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                   <inlineChoice identifier="A">opt.1</inlineChoice>
                                   <inlineChoice identifier="B">opt.2</inlineChoice>
                                   <inlineChoice identifier="C">opt.3</inlineChoice>
                                   <inlineChoice identifier="D">opt.4</inlineChoice>
                                   <inlineChoice identifier="E">opt.5</inlineChoice>
                               </inlineChoiceInteraction>
                           </p>
                           <p id="c1-id-13">Drop down 3 <inlineChoiceInteraction responseIdentifier="I22f81e91-aaa4-4528-999e-61b31b8123ec" shuffle="false" required="true">
                                   <inlineChoice identifier="A">keuze 1</inlineChoice>
                                   <inlineChoice identifier="B">keuze 2</inlineChoice>
                               </inlineChoiceInteraction>
                           </p>
                           <p id="c1-id-14">Drop down 4 <inlineChoiceInteraction responseIdentifier="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" shuffle="false" required="true">
                                   <inlineChoice identifier="A">A</inlineChoice>
                                   <inlineChoice identifier="B">B</inlineChoice>
                                   <inlineChoice identifier="C">C</inlineChoice>
                                   <inlineChoice identifier="D">D</inlineChoice>
                                   <inlineChoice identifier="E">E</inlineChoice>
                                   <inlineChoice identifier="F">F</inlineChoice>
                               </inlineChoiceInteraction>
                           </p>
                       </div>
                       <div id="answer">

                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                    <keyFact id="C-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                            <stringValue>
                                <typedValue>E</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="F-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                            <stringValue>
                                <typedValue>F</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
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
                <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                                <stringValue>
                                    <typedValue>F</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="B-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                                <stringValue>
                                    <typedValue>F</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="D-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>E</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                                <stringValue>
                                    <typedValue>F</typedValue>
                                </stringValue>
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
                <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                    <keyFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="F-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                            <stringValue>
                                <typedValue>F</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="B-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>E</typedValue>
                                </stringValue>
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
                <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="C-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="E-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>E</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                                <stringValue>
                                    <typedValue>F</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="E-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>E</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
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
                <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                    <keyFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="F-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                            <stringValue>
                                <typedValue>F</typedValue>
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

    Private _solution7 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="F-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                                <stringValue>
                                    <typedValue>F</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
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
                <correctResponse interpretation="C">
                    <value>C</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="E">
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse interpretation="F">
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="string"/>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(A&amp;A&amp;A&amp;F)|(B&amp;B&amp;B&amp;F)|(D&amp;E&amp;B&amp;F)">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="A">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(A&amp;A)|(B&amp;B)|(E&amp;A)">
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse interpretation="F">
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(C&amp;A)">
                    <value>C</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(E&amp;F)|(E&amp;D)">
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>A</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="B">
                    <value>B</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse interpretation="F">
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result7 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="identifier">
                <correctResponse interpretation="(B&amp;F)">
                    <value>B</value>
                </correctResponse>
            </responseDeclaration>
            <responseDeclaration identifier="RESPONSE3" cardinality="single" baseType="string"/>
            <responseDeclaration identifier="RESPONSE4" cardinality="single" baseType="identifier">
                <correctResponse>
                    <value>F</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>


End Class
