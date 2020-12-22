Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseDeclarationGapMatchTestsBase


        Protected Function GetGapMatchScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim gapMatchScoringParameter = New GapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C", "D", "E", "F")

            Dim xhtmlValue As XElement = <xhtmlParameter name="gapMatchInlineInput">
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Tekst met <cito:InlineElement id="I51faf178-ca03-41eb-8276-385ef2a185b3" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I51faf178-ca03-41eb-8276-385ef2a185b3</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 1</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> een aantal <cito:InlineElement id="I989f6f3c-9d38-492f-80fe-4b71cfee574f" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I989f6f3c-9d38-492f-80fe-4b71cfee574f</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 2</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> gaten waarin teksten <cito:InlineElement id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">Ie1f57945-a74c-4f6c-948b-5133fb9778e8</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 3</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> kunnen worden gesleept <cito:InlineElement id="I723529e7-8893-455e-b785-595592528040" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I723529e7-8893-455e-b785-595592528040</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 4</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> door de kandidaat.</p>
                                         </xhtmlParameter>

            Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xhtmlValue.ToString}

            gapMatchScoringParameter.GapXhtmlParameter = xhtmlPrm
            Dim scoreParam As ScoringParameter = gapMatchScoringParameter.Transform()
            scoreParams.Add(scoreParam)

            Return scoreParams
        End Function



        Protected _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>E</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
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
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                    <stringValue>
                                        <typedValue>F</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>E</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
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

        Protected _solution3 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>C</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                    <stringValue>
                                        <typedValue>F</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>E</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
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
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
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
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                <noValue/>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <noValue/>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <noValue/>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                    <stringValue>
                                        <typedValue>E</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
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
                    <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                        <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
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
                </ItemScoreTranslationTable>
            </solution>

        Protected _solution8 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                        <keyFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                    <ItemScoreTranslationTableEntry rawScore="2" translatedScore="1"/>
                    <ItemScoreTranslationTableEntry rawScore="3" translatedScore="1"/>
                    <ItemScoreTranslationTableEntry rawScore="4" translatedScore="2"/>
                </ItemScoreTranslationTable>
            </solution>


    End Class

End Namespace