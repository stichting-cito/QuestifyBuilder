Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseDeclarationInputTestsBase


        Protected Function GetSingleInputScoringParam() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I812796ae-bcc4-4fcf-803c-85fdae53013d", .MaxLength = 5}.AddSubParameters("1")
            scoreParams.Add(scoreParam1)
            Return scoreParams
        End Function

        Protected Function GetThreeInputScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I812796ae-bcc4-4fcf-803c-85fdae53013d", .MaxLength = 5}.AddSubParameters("1")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New DecimalScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I137d2333-4bdf-446a-a4b7-9c97ff566462", .IntegerPartMaxLength = 5, .FractionPartMaxLength = 3}.AddSubParameters("1")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New StringScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a", .ExpectedLength = 5}.AddSubParameters("1")
            scoreParams.Add(scoreParam3)
            Return scoreParams
        End Function



        Protected _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" occur="1">
                                <stringValue>
                                    <typedValue>tekstje</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I137d2333-4bdf-446a-a4b7-9c97ff566462" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I137d2333-4bdf-446a-a4b7-9c97ff566462" occur="1">
                                <decimalValue>
                                    <typedValue>1.1</typedValue>
                                </decimalValue>
                                <decimalValue>
                                    <typedValue>1.2</typedValue>
                                </decimalValue>
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
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" occur="1">
                                <stringValue>
                                    <typedValue>tekstje</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                    <integerValue>
                                        <typedValue>1</typedValue>
                                    </integerValue>
                                    <integerValue>
                                        <typedValue>2</typedValue>
                                    </integerValue>
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I137d2333-4bdf-446a-a4b7-9c97ff566462" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I137d2333-4bdf-446a-a4b7-9c97ff566462" occur="1">
                                    <decimalValue>
                                        <typedValue>1.1</typedValue>
                                    </decimalValue>
                                    <decimalValue>
                                        <typedValue>1.2</typedValue>
                                    </decimalValue>
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
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I53a5a19c-90a1-4d82-979a-08d3b4d6ed6a" occur="1">
                                <stringValue>
                                    <typedValue>tekstje</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                    <integerValue>
                                        <typedValue>1</typedValue>
                                    </integerValue>
                                    <integerValue>
                                        <typedValue>2</typedValue>
                                    </integerValue>
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I137d2333-4bdf-446a-a4b7-9c97ff566462" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I137d2333-4bdf-446a-a4b7-9c97ff566462" occur="1">
                                    <decimalValue>
                                        <typedValue>1.1</typedValue>
                                    </decimalValue>
                                    <decimalValue>
                                        <typedValue>1.2</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-I137d2333-4bdf-446a-a4b7-9c97ff566462" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I137d2333-4bdf-446a-a4b7-9c97ff566462" occur="1">
                                    <decimalValue>
                                        <typedValue>1.3</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                    <integerValue>
                                        <typedValue>4</typedValue>
                                    </integerValue>
                                    <integerValue>
                                        <typedValue>5</typedValue>
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

        Protected _solution5 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="IntegerInputA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="IntegerInputA" occur="1">
                                <integerRangeValue rangeEnd="999" rangeStart="100"/>
                                <integerComparisonValue>
                                    <typedComparisonValue>1234</typedComparisonValue>
                                    <comparisonType>GreaterThan</comparisonType>
                                </integerComparisonValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="TimeInputB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="TimeInputB" occur="1">
                                <stringValue>
                                    <typedValue>12:34</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>21:34</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="StringInputC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="StringInputC" occur="1">
                                <stringValue>
                                    <typedValue>tekst</typedValue>
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

        Protected _solution6 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="IntegerInputA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="IntegerInputA" occur="1">
                                <integerComparisonValue>
                                    <typedComparisonValue>100</typedComparisonValue>
                                    <comparisonType>GreaterThan</comparisonType>
                                </integerComparisonValue>
                                <integerComparisonValue>
                                    <typedComparisonValue>999</typedComparisonValue>
                                    <comparisonType>SmallerThan</comparisonType>
                                </integerComparisonValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="TimeInputB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="TimeInputB" occur="1">
                                <stringValue>
                                    <typedValue>12:34</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="StringInputC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="StringInputC" occur="1">
                                <stringValue>
                                    <typedValue>tekst</typedValue>
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
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="DateInputA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="DateInputA" occur="1">
                                <stringValue>
                                    <typedValue>03/01/2015</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>05/01/2015</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="DateInputB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="DateInputB" occur="1">
                                <stringValue>
                                    <typedValue>03/01/2015</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>05/01/2015</typedValue>
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

        Protected _solution8 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibaa838b2-b0f3-4f4c-9202-a8ab1cb9df88" occur="1">
                                <stringValue>
                                    <typedValue>10:00</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>11:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0b525f2d-804e-42a1-b8da-9abbd6d0f6ae" occur="1">
                                <stringValue>
                                    <typedValue>12:00</typedValue>
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
                  <keyFinding id="gapController" scoringMethod="Dichotomous">
                      <keyFact id="CurrencyInputA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="CurrencyInputA" occur="1">
                              <decimalValue>
                                  <typedValue>244400.00</typedValue>
                              </decimalValue>
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

        Protected _solution10 As XElement =
          <solution>
              <keyFindings>
                  <keyFinding id="gapController" scoringMethod="Dichotomous">
                      <keyFact id="CurrencyInputB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="CurrencyInputB" occur="1">
                              <decimalValue>
                                  <typedValue>244400.00</typedValue>
                              </decimalValue>
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

        Protected _solution11 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>3</typedValue>
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

        Protected _solution12 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Polytomous">
                        <keyFact id="1-I812796ae-bcc4-4fcf-803c-85fdae53013d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I812796ae-bcc4-4fcf-803c-85fdae53013d" occur="1">
                                <integerValue>
                                    <typedValue>1</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                                <integerValue>
                                    <typedValue>3</typedValue>
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