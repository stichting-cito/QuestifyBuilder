Imports System.Drawing
Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseDeclarationGraphicGapMatchTestsBase


        Protected Function GetGGMScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As GraphGapMatchScoringParameter = New GraphGapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController", .IsCategorizationVariant = False}.AddSubParameters("A", "B", "C", "D")
            scoreParam.Value(0).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "A", .Name = GapMatchScoringParameter.GapControlName})
            scoreParam.Value(1).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "B", .Name = GapMatchScoringParameter.GapControlName})
            scoreParam.Value(2).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "C", .Name = GapMatchScoringParameter.GapControlName})
            scoreParam.Value(3).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "D", .Name = GapMatchScoringParameter.GapControlName})

            Dim area As New AreaParameter With {.Name = "itemQuestionArea"}
            scoreParam.Area = area
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "C", .BottomRight = New Point(694, 285), .TopLeft = New Point(474, 35)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "D", .BottomRight = New Point(101, 25), .TopLeft = New Point(76, 0)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "A", .BottomRight = New Point(191, 284), .TopLeft = New Point(0, 100)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "B", .BottomRight = New Point(472, 285), .TopLeft = New Point(192, 35)})

            Dim ggmScoringPrm As ScoringParameter = scoreParam.Transform
            scoreParams.Add(ggmScoringPrm)
            Return scoreParams
        End Function



        Protected _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                        <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
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

        Protected _solution2 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                        <keyFactSet>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="D-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="C-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="C-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="D-gapMatchController" occur="1">
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

        Protected _solution3 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                        <keyFactSet>
                            <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="C-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="D-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="C-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="D-gapMatchController" occur="1">
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

        Protected _solution4 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
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
                    <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                        <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <noValue/>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
                                    <noValue/>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-gapMatchController" occur="1">
                                    <noValue/>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
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

        Protected _solution7 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
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