Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseDeclarationMatrixTestsBase


        Protected Function GetMatrixScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim matrixColumnsDefinition As MultiChoiceScoringParameter = New MultiChoiceScoringParameter() With {.MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C")
            Dim scoreParam As ScoringParameter = New MatrixScoringParameter() With {.ControllerId = "matrix", .FindingOverride = "matrix"}.AddSubParameters("1", "2", "3", "4")
            DirectCast(scoreParam, MatrixScoringParameter).MatrixColumnsDefinition = matrixColumnsDefinition
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function



        Protected _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="matrix" scoringMethod="Polytomous">
                        <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix3" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix4" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix2" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix1" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix1" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix2" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
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

        Protected _solution2 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="matrix" scoringMethod="Polytomous">
                        <keyFactSet>
                            <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix2" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix1" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix1" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix2" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix3" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix4" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix3" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix4" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
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

        Protected _solution3 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="matrix" scoringMethod="Polytomous">
                        <keyFactSet>
                            <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix2" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix1" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix3" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix4" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix1" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix2" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix3" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="matrix4" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
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
                    <keyFinding id="matrix" scoringMethod="Polytomous">
                        <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix1" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix3" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix4" occur="1">
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

        Protected _solution5 As XElement =
            <solution>
                <keyFindings/>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        Protected _solution6 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="matrix" scoringMethod="Dichotomous">
                        <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix1" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix2" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix3" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix4" occur="1">
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


    End Class

End Namespace