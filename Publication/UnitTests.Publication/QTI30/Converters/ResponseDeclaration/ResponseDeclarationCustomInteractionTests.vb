Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationCustomInteractionTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetCustomInteractionScoringParams(), _result1, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_NoFinding_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetCustomInteractionScoringParams(), _result2, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_Factset_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetCustomInteractionScoringParams(), _result3, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_FindingFactsAndFactset_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetCustomInteractionScoringParams(), _result4, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_Factsets_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetCustomInteractionScoringParams(), _result5, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_InCompleteFinding_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetCustomInteractionScoringParams(), _result6, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_Factset_InCompleteFinding_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution7, GetCustomInteractionScoringParams(), _result7, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_Type3_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody_Type3, _solution_Type3_1, GetCustomInteractionScoringParams_Type3And4, _result_Type3_1, 1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub CI_Type4_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody_Type4, _solution_Type4_1, GetCustomInteractionScoringParams_Type3And4, _result_Type4_1, 1)
        End Sub


        Private Function GetCustomInteractionScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New IntegerScoringParameter() With {.Label = "coordinate 1", .ControllerId = "CI_SP0", .FindingOverride = "CustomInteractions", .MaxLength = 5}.AddSubParameters("1")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New MultiChoiceScoringParameter() With {.Label = "coordinate 2", .ControllerId = "CI_SP1", .FindingOverride = "CustomInteractions", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "coordinate 3", .ControllerId = "CI_SP2", .FindingOverride = "CustomInteractions", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C")
            scoreParams.Add(scoreParam3)
            Dim scoreParam4 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "coordinate 4", .ControllerId = "CI_SP3", .FindingOverride = "CustomInteractions", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E", "F")
            scoreParams.Add(scoreParam4)

            Return scoreParams
        End Function

        Private Function GetCustomInteractionScoringParams_Type3And4() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 1 - x (A)", .Name = "CI_SP0", .ControllerId = "CI_SP0", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 1 - y (B)", .Name = "CI_SP1", .ControllerId = "CI_SP1", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 2 - x (C)", .Name = "CI_SP2", .ControllerId = "CI_SP2", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam3)
            Dim scoreParam4 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 2 - y (D)", .Name = "CI_SP3", .ControllerId = "CI_SP3", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam4)
            Dim scoreParam5 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 3 - x (E)", .Name = "CI_SP4", .ControllerId = "CI_SP4", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam5)
            Dim scoreParam6 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 3 - y (F)", .Name = "CI_SP5", .ControllerId = "CI_SP5", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam6)
            Dim scoreParam7 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 4 - x (G)", .Name = "CI_SP6", .ControllerId = "CI_SP6", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam7)
            Dim scoreParam8 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 4 - y (H)", .Name = "CI_SP7", .ControllerId = "CI_SP7", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam8)
            Return scoreParams
        End Function



        Private _itemBody1 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <qti-item-body class="defaultBody">
               <div class="content">
                   <qti-custom-interaction response-identifier="CustomInteractions">
                       <qti-prompt>Hieronder zou de Custom Interaction moeten staan...</qti-prompt>
                       <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" data="../ref/json/test.manifest.json" height="680" width="680">
                           <param name="responseLength" value="4" valuetype="DATA"/>
                       </object>
                   </qti-custom-interaction>
               </div>
           </qti-item-body>
       </wrapper>


        Private _itemBody_Type3 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div id="question">
                        <p id="c1-id-11">Gegeven ...</p>
                        <p id="c1-id-12">
                            <strong id="c1-id-14">Teken ...</strong>
                        </p>
                        <p id="c1-id-13"> </p>
                    </div>
                    <qti-custom-interaction response-identifier="CustomInteractions">
                        <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" height="500" width="530" data="resource://package/DWOitemtype3JH1806.ci">
                            <!-- + 1 because first entry is reserved to save state -->
                            <param name="responseLength" value="1" valuetype="DATA"/>
                        </object>
                    </qti-custom-interaction>
                </div>
            </qti-item-body>
        </wrapper>

        Private _itemBody_Type4 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div id="question">
                        <p id="c1-id-11">Gegeven 4,12...</p>
                        <p id="c1-id-12">
                            <strong id="c1-id-14">Teken...</strong>
                        </p>
                        <p id="c1-id-13"> </p>
                    </div>
                    <qti-custom-interaction response-identifier="CustomInteractions">
                        <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" height="500" width="530" data="resource://package/DWOitemtype4aJH1806.ci">
                            <!-- + 1 because first entry is reserved to save state -->
                            <param name="responseLength" value="1" valuetype="DATA"/>
                        </object>
                    </qti-custom-interaction>
                </div>
            </qti-item-body>
        </wrapper>



        Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
                    <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP0" occur="1">
                            <integerValue>
                                <typedValue>12345</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP1" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP2" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP3" occur="1">
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
                <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <integerValue>
                                    <typedValue>12345</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP1" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
                                <stringValue>
                                    <typedValue>F</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <integerValue>
                                    <typedValue>54321</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP1" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
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

        Private _solution4 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
                    <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP1" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP3" occur="1">
                            <stringValue>
                                <typedValue>F</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <integerValue>
                                    <typedValue>12345</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <integerValue>
                                    <typedValue>54321</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
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

        Private _solution5 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP1" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
                                <stringValue>
                                    <typedValue>F</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <integerValue>
                                    <typedValue>12345</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <integerValue>
                                    <typedValue>54321</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="C-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP1" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
                                <stringValue>
                                    <typedValue>E</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="B-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP1" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
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
                <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
                    <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP0" occur="1">
                            <integerValue>
                                <typedValue>12345</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP1" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="CI_SP3" occur="1">
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
                <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP1" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
                                <stringValue>
                                    <typedValue>F</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <integerValue>
                                    <typedValue>54321</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
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

        Private _solution_Type3_1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="A-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <decimalRangeValue rangeEnd="1.1" rangeStart="0.9"/>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP1" occur="1">
                                <decimalRangeValue rangeEnd="1.1" rangeStart="0.9"/>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <decimalRangeValue rangeEnd="2.1" rangeStart="1.9"/>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
                                <decimalRangeValue rangeEnd="2.1" rangeStart="1.9"/>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-CI_SP4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP4" occur="1">
                                <decimalRangeValue rangeEnd="3.1" rangeStart="2.9"/>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP5" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP5" occur="1">
                                <decimalRangeValue rangeEnd="3.1" rangeStart="2.9"/>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-CI_SP6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP6" occur="1">
                                <decimalRangeValue rangeEnd="4.1" rangeStart="3.9"/>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP7" occur="1">
                                <decimalRangeValue rangeEnd="4.1" rangeStart="3.9"/>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <conceptFindings/>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

        Private _solution_Type4_1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="A-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP0" occur="1">
                                <decimalValue>
                                    <typedValue>1</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP1" occur="1">
                                <decimalValue>
                                    <typedValue>3</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP2" occur="1">
                                <decimalValue>
                                    <typedValue>2</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP3" occur="1">
                                <decimalValue>
                                    <typedValue>6</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-CI_SP4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP4" occur="1">
                                <decimalValue>
                                    <typedValue>3</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP5" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP5" occur="1">
                                <decimalValue>
                                    <typedValue>9</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-CI_SP6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP6" occur="1">
                                <decimalValue>
                                    <typedValue>4</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP7" occur="1">
                                <decimalValue>
                                    <typedValue>12</typedValue>
                                </decimalValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <conceptFindings/>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>



        Private _result1 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="string">
        <qti-correct-response interpretation="12345&amp;D&amp;B&amp;F">
            <qti-value>12345</qti-value>
            <qti-value>D</qti-value>
            <qti-value>B</qti-value>
            <qti-value>F</qti-value>
        </qti-correct-response>
    </qti-response-declaration>
</root>

        Private _result2 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="string"/>
</root>

        Private _result3 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="string">
        <qti-correct-response interpretation="(12345&amp;D&amp;B&amp;F)|(54321&amp;D&amp;B&amp;E)">
            <qti-value>12345</qti-value>
            <qti-value>D</qti-value>
            <qti-value>B</qti-value>
            <qti-value>F</qti-value>
        </qti-correct-response>
    </qti-response-declaration>
</root>

        Private _result4 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="string">
        <qti-correct-response interpretation="(12345&amp;B)|(54321&amp;B)&amp;D&amp;F">
            <qti-value>12345</qti-value>
            <qti-value>D</qti-value>
            <qti-value>B</qti-value>
            <qti-value>F</qti-value>
        </qti-correct-response>
    </qti-response-declaration>
</root>

        Private _result5 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="string">
        <qti-correct-response interpretation="(12345&amp;B)|(54321&amp;B)&amp;(D&amp;F)|(C&amp;E)|(B&amp;D)">
            <qti-value>12345</qti-value>
            <qti-value>D</qti-value>
            <qti-value>B</qti-value>
            <qti-value>F</qti-value>
        </qti-correct-response>
    </qti-response-declaration>
</root>

        Private _result6 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="string">
        <qti-correct-response interpretation="12345&amp;D&amp;F">
            <qti-value>12345</qti-value>
            <qti-value>D</qti-value>
            <qti-value>F</qti-value>
        </qti-correct-response>
    </qti-response-declaration>
</root>

        Private _result7 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="string">
        <qti-correct-response interpretation="(D&amp;B&amp;F)|(54321&amp;B&amp;E)">
            <qti-value>D</qti-value>
            <qti-value>B</qti-value>
            <qti-value>F</qti-value>
        </qti-correct-response>
    </qti-response-declaration>
</root>

        Private _result_Type3_1 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="float">
        <qti-correct-response interpretation="(0.9-1.1&amp;0.9-1.1)&amp;(1.9-2.1&amp;1.9-2.1)&amp;(2.9-3.1&amp;2.9-3.1)&amp;(3.9-4.1&amp;3.9-4.1)">
            <qti-value>0.9</qti-value>
            <qti-value>0.9</qti-value>
            <qti-value>1.9</qti-value>
            <qti-value>1.9</qti-value>
            <qti-value>2.9</qti-value>
            <qti-value>2.9</qti-value>
            <qti-value>3.9</qti-value>
            <qti-value>3.9</qti-value>
        </qti-correct-response>
    </qti-response-declaration>
</root>

        Private _result_Type4_1 As XElement =
<root>
    <qti-response-declaration identifier="RESPONSE" cardinality="ordered" base-type="float">
        <qti-correct-response interpretation="(1&amp;3)&amp;(2&amp;6)&amp;(3&amp;9)&amp;(4&amp;12)">
            <qti-value>1</qti-value>
            <qti-value>3</qti-value>
            <qti-value>2</qti-value>
            <qti-value>6</qti-value>
            <qti-value>3</qti-value>
            <qti-value>9</qti-value>
            <qti-value>4</qti-value>
            <qti-value>12</qti-value>
        </qti-correct-response>
    </qti-response-declaration>
</root>


    End Class

End Namespace