Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseDeclarationMultiChoiceTestsBase


        Protected Function GetMcScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Radio}.AddSubParameters("A", "B", "C", "D")
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function

        Protected Function GetMrScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 3, .MaxChoices = 3, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C", "D", "E")
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function



        Protected _solution1 As XElement =
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

        Protected _solution2 As XElement =
            <solution>
                <keyFindings/>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        Protected _solution3 As XElement =
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

        Protected _solution4 As XElement =
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

        Protected _solution5 As XElement =
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

        Protected _solution6 As XElement =
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

        Protected _solution7 As XElement =
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

        Protected _solution8 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="mc" scoringMethod="Polytomous">
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
                    <ItemScoreTranslationTableEntry rawScore="2" translatedScore="2"/>
                    <ItemScoreTranslationTableEntry rawScore="3" translatedScore="3"/>
                </ItemScoreTranslationTable>
            </solution>

        Protected _solution9 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="mc" scoringMethod="Polytomous">
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


    End Class

End Namespace