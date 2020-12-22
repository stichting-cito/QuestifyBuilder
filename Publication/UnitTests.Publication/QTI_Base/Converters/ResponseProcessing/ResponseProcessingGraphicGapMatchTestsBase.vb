Imports System.Drawing
Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseProcessingGraphicGapMatchTestsBase

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

        Protected _finding1 As XElement =
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

        Protected _finding2 As XElement =
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

        Protected _finding3 As XElement =
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

        Protected _finding4 As XElement =
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

        Protected _finding5 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Protected _finding6 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Protected _finding7 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Protected _finding8 As XElement =
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

        Protected _finding9 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
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

        Protected _finding10 As XElement =
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

    End Class

End Namespace