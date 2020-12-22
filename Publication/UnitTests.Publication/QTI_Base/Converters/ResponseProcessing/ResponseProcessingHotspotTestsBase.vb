
Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseProcessingHotspotTestsBase

        Protected Function GetHotspotScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)

            Dim scoreParam As HotspotScoringParameter = New HotspotScoringParameter() With {.ControllerId = "areaInteractionController", .FindingOverride = "areaInteractionController", .MinChoices = 1, .MaxChoices = 5}.AddSubParameters("A", "B", "C", "D", "E")
            Dim area As New AreaParameter With {.Name = "clickableArea"}
            scoreParam.Area = area
            scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "A", .Radius = 35})
            scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "B", .Radius = 35})
            scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "C", .Radius = 35})
            scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "D", .Radius = 35})
            scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "E", .Radius = 35})

            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function

        Protected _finding1 As XElement =
            <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding2 As XElement =
            <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding3 As XElement =
            <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding4 As XElement =
            <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding5 As XElement =
            <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding6 As XElement =
            <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding7 As XElement =
            <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

    End Class

End Namespace