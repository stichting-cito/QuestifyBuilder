
Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseDeclarationHotspotTestsBase

#Region "Scoring parameters"

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

#End Region

#Region "Solutions"

        Protected _solution1 As XElement =
            <solution>
                <keyFindings>
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
                            <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="C-areaInteractionController" occur="1">
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
                            <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-areaInteractionController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
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
                    <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="C-areaInteractionController" occur="1">
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
                            <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-areaInteractionController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
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
                    <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="C-areaInteractionController" occur="1">
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
                            <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="A-areaInteractionController" occur="1">
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
                    <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
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
                    <keyFinding id="areaInteractionController" scoringMethod="Polytomous">
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
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                    <ItemScoreTranslationTableEntry rawScore="2" translatedScore="2"/>
                    <ItemScoreTranslationTableEntry rawScore="3" translatedScore="3"/>
                </ItemScoreTranslationTable>
            </solution>

#End Region

    End Class

End Namespace