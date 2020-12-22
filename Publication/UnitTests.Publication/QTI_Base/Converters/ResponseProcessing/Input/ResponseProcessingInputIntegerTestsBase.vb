
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework

Namespace QTI_Base

    Public MustInherit Class ResponseProcessingInputIntegerTestsBase

        Protected Function GetInputScoringParams(nrOfParams As Integer) As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            If nrOfParams >= 1 Then scoreParams.Add(GetInputScoringParam("I51372368-cad1-4946-9d37-f1ca446221c6"))
            If nrOfParams >= 2 Then scoreParams.Add(GetInputScoringParam("I8500a50f-9040-41b7-986b-1af7686d80e8"))
            If nrOfParams >= 3 Then scoreParams.Add(GetInputScoringParam("I0dec2572-468c-49d9-bdff-c2482ec461c1"))
            Return scoreParams
        End Function

        Private Function GetInputScoringParam(inlineId As String) As ScoringParameter
            Return New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = inlineId, .MaxLength = 6}.AddSubParameters("1")
        End Function


        Protected _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
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

        Protected _solution2 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                                <integerRangeValue rangeEnd="10" rangeStart="1"/>
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
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>-3</typedValue>
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

        Protected _solution4 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                                <integerRangeValue rangeEnd="10" rangeStart="1"/>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I8500a50f-9040-41b7-986b-1af7686d80e8" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I8500a50f-9040-41b7-986b-1af7686d80e8" occur="1">
                                <integerRangeValue rangeEnd="22" rangeStart="11"/>
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
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                                <integerComparisonValue>
                                    <typedComparisonValue>2</typedComparisonValue>
                                    <comparisonType>SmallerThan</comparisonType>
                                </integerComparisonValue>
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

        Protected _solution6 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                                <integerComparisonValue>
                                    <typedComparisonValue>2</typedComparisonValue>
                                    <comparisonType>SmallerThanEquals</comparisonType>
                                </integerComparisonValue>
                                <integerComparisonValue>
                                    <typedComparisonValue>4</typedComparisonValue>
                                    <comparisonType>GreaterThanEquals</comparisonType>
                                </integerComparisonValue>
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
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                                <integerRangeValue rangeEnd="10" rangeStart="1"/>
                                <integerRangeValue rangeEnd="20" rangeStart="11"/>
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

        Protected _solution8 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I0dec2572-468c-49d9-bdff-c2482ec461c1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0dec2572-468c-49d9-bdff-c2482ec461c1" occur="1">
                                <integerRangeValue rangeEnd="5" rangeStart="4"/>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="1-I8500a50f-9040-41b7-986b-1af7686d80e8" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I8500a50f-9040-41b7-986b-1af7686d80e8" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
                                    <integerValue>
                                        <typedValue>1</typedValue>
                                    </integerValue>
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

        Protected _solution9 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Polytomous">
                        <keyFact id="1-I51372368-cad1-4946-9d37-f1ca446221c6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51372368-cad1-4946-9d37-f1ca446221c6" occur="1">
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

    End Class

End Namespace