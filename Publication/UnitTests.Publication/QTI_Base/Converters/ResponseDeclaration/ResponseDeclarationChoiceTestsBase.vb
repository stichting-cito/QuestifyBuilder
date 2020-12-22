Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseDeclarationChoiceTestsBase


        Protected Function GetInlineChoiceScoringParams(nrOfParams As Integer) As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            If nrOfParams >= 1 Then scoreParams.Add(New InlineChoiceScoringParameter() With {.Label = "choice1", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I13abebba-ebb7-4ed8-81d0-46e9e1b60a53", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D"))
            If nrOfParams >= 2 Then scoreParams.Add(New InlineChoiceScoringParameter() With {.Label = "choice2", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "Ie830b508-32a0-42f4-920f-0e06f0f19313", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E"))
            If nrOfParams >= 3 Then scoreParams.Add(New InlineChoiceScoringParameter() With {.Label = "choice3", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I22f81e91-aaa4-4528-999e-61b31b8123ec", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B"))
            If nrOfParams >= 4 Then scoreParams.Add(New InlineChoiceScoringParameter() With {.Label = "choice4", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I5196c3f8-ddf3-4423-b777-d1d2c71d6efb", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E", "F"))
            Return scoreParams
        End Function



        Protected _solution1 As XElement =
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

        Protected _solution4 As XElement =
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

        Protected _solution5 As XElement =
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

        Protected _solution6 As XElement =
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

        Protected _solution7 As XElement =
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

        Protected _solution8 As XElement =
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
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        Protected _solution9 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="inlineChoiceController" scoringMethod="Polytomous">
                        <keyFact id="C-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
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


    End Class

End Namespace