Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseProcessingMatrixTestsBase

        Protected Function GetMatrixScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim matrixColumnsDefinition As MultiChoiceScoringParameter = New MultiChoiceScoringParameter() With {.MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C")
            Dim scoreParam As ScoringParameter = New MatrixScoringParameter() With {.ControllerId = "matrix", .FindingOverride = "matrix"}.AddSubParameters("1", "2", "3", "4")
            DirectCast(scoreParam, MatrixScoringParameter).MatrixColumnsDefinition = matrixColumnsDefinition
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function

        Protected _finding1 As XElement =
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

        Protected _finding2 As XElement =
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

        Protected _finding3 As XElement =
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

        Protected _finding4 As XElement =
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

        Protected _finding5 As XElement =
            <keyFinding id="matrix" scoringMethod="Dichotomous">
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

        Protected _finding6 As XElement =
            <keyFinding id="matrix" scoringMethod="Dichotomous">
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

        Protected _finding7 As XElement =
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

    End Class

End Namespace