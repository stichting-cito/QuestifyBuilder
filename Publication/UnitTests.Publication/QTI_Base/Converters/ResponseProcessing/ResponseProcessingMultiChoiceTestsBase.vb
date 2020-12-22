
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework

Namespace QTI_Base

    Public MustInherit Class ResponseProcessingMultiChoiceTestsBase

        Protected Function GetMcScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Radio}.AddSubParameters("A", "B", "C", "D")
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function

        Protected Function GetMrScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 2, .MaxChoices = 2, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C", "D")
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function

        Protected Function GetMcAndMrScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim MrScoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mr", .FindingOverride = "mr", .MinChoices = 2, .MaxChoices = 2, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C", "D")
            scoreParams.Add(MrScoreParam)
            Dim McScoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Radio}.AddSubParameters("A", "B", "C", "D")
            scoreParams.Add(McScoreParam)
            Return scoreParams
        End Function


        Protected _finding1 As XElement =
            <keyFinding id="mc" scoringMethod="Dichotomous">
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
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
            </keyFinding>

        Protected _finding2 As XElement =
            <keyFinding id="mc" scoringMethod="Dichotomous">
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
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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
                    <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding3 As XElement =
            <keyFinding id="mc" scoringMethod="Polytomous">
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
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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
                    <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding4 As XElement =
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
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
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>

        Protected _finding5 As XElement =
                <keyFinding id="mc" scoringMethod="Polytomous">
                    <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-mc" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
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
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>

        Protected _finding6 As XElement =
                <keyFinding id="mc" scoringMethod="Polytomous">
                    <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
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
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>

        Protected _finding7 As XElement =
                <keyFinding id="mc" scoringMethod="Dichotomous">
                    <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-mc" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-mc" occur="1">
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
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-mc" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>

        Protected _finding8 As XElement =
            <keyFinding id="mc" scoringMethod="Dichotomous">
                <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="mc" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding9 As XElement =
            <keyFinding id="mc" scoringMethod="Polytomous">
                <keyFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
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
            </keyFinding>

        Protected _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="mc" scoringMethod="Dichotomous">
                        <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
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

        Protected _solution2 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="mc" scoringMethod="Dichotomous">
                        <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
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


    End Class

End Namespace