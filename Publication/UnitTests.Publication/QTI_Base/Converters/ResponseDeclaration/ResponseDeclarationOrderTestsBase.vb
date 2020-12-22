Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseDeclarationOrderTestsBase


        Protected Function GetOrderScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            scoreParams.Add(New OrderScoringParameter() With {.ControllerId = "orderController", .FindingOverride = "orderController"}.AddSubParameters("A", "B", "C", "D", "E", "F"))
            Return scoreParams
        End Function



        Protected _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="orderController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>4</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>5</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>6</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>1</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>2</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>4</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>5</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>6</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>1</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>2</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
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
                    <keyFinding id="orderController" scoringMethod="Dichotomous">
                        <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>4</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>5</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>6</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
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

        Protected _solution3 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="orderController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>6</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>4</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>5</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>1</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="orderController" occur="1">
                                    <integerValue>
                                        <typedValue>2</typedValue>
                                    </integerValue>
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
                    <keyFinding id="orderController" scoringMethod="Dichotomous">
                        <keyFactSet/>
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
                    <keyFinding id="orderController" scoringMethod="Polytomous">
                        <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>4</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>5</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>6</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="orderController" occur="1">
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
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
                    <ItemScoreTranslationTableEntry rawScore="4" translatedScore="4"/>
                    <ItemScoreTranslationTableEntry rawScore="5" translatedScore="5"/>
                    <ItemScoreTranslationTableEntry rawScore="6" translatedScore="6"/>
                </ItemScoreTranslationTable>
            </solution>


    End Class

End Namespace