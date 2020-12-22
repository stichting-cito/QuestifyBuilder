Imports Cito.Tester.ContentModel

<TestClass()>
Public Class QTI22ResponseDeclarationGapMatchTests
    Inherits QTI22ResponseDeclarationTests_Base

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
        RunResponseDeclarationTest(_itemBody1, _solution1, GetGapMatchScoringParams(), _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForMultipleFactSetsTest()
        RunResponseDeclarationTest(_itemBody1, _solution2, GetGapMatchScoringParams(), _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactSetsTest()
        RunResponseDeclarationTest(_itemBody1, _solution3, GetGapMatchScoringParams(), _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactsOnFindingTest()
        RunResponseDeclarationTest(_itemBody1, _solution4, GetGapMatchScoringParams(), _result4, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetEmptyResponseDeclarationTest()
        RunResponseDeclarationTest(_itemBody1, _solution5, GetGapMatchScoringParams(), _result5, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationWithNoValueTest()
        RunResponseDeclarationTest(_itemBody1, _solution6, GetGapMatchScoringParams(), _result6, 1)
    End Sub


    Private Function GetGapMatchScoringParams() As HashSet(Of ScoringParameter)
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



    Private _itemBody1 As XElement =
       <wrapper>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <gapMatchInteraction responseIdentifier="gapMatchController" shuffle="false">
                               <gapText identifier="A" matchMax="1">A</gapText>
                               <gapText identifier="B" matchMax="1">B</gapText>
                               <gapText identifier="C" matchMax="1">C</gapText>
                               <gapText identifier="D" matchMax="1">D</gapText>
                               <gapText identifier="E" matchMax="1">E</gapText>
                               <gapText identifier="F" matchMax="1">F</gapText>
                               <p id="c1-id-11">Tekst met <span>
                                       <gap identifier="I51faf178-ca03-41eb-8276-385ef2a185b3" required="true"/>
                                   </span> een aantal <span>
                                       <gap identifier="I989f6f3c-9d38-492f-80fe-4b71cfee574f" required="true"/>
                                   </span> gaten waarin teksten <span>
                                       <gap identifier="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" required="true"/>
                                   </span> kunnen worden gesleept <span>
                                       <gap identifier="I723529e7-8893-455e-b785-595592528040" required="true"/>
                                   </span> door de kandidaat.</p>
                           </gapMatchInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>



    Private _solution1 As XElement =
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

    Private _solution2 As XElement =
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

    Private _solution3 As XElement =
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

    Private _solution4 As XElement =
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

    Private _solution5 As XElement =
        <solution>
            <keyFindings/>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution6 As XElement =
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



    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;D I723529e7-8893-455e-b785-595592528040">
                    <value>A I51faf178-ca03-41eb-8276-385ef2a185b3</value>
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="(A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(C I51faf178-ca03-41eb-8276-385ef2a185b3&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(F I51faf178-ca03-41eb-8276-385ef2a185b3&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D I723529e7-8893-455e-b785-595592528040)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;F I723529e7-8893-455e-b785-595592528040)">
                    <value>A I51faf178-ca03-41eb-8276-385ef2a185b3</value>
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="(A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;D I723529e7-8893-455e-b785-595592528040)|(D I51faf178-ca03-41eb-8276-385ef2a185b3&amp;C I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;B Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;A I723529e7-8893-455e-b785-595592528040)|(F I51faf178-ca03-41eb-8276-385ef2a185b3&amp;E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;B Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;A I723529e7-8893-455e-b785-595592528040)">
                    <value>A I51faf178-ca03-41eb-8276-385ef2a185b3</value>
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="A I51faf178-ca03-41eb-8276-385ef2a185b3&amp;B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;C Ie1f57945-a74c-4f6c-948b-5133fb9778e8&amp;D I723529e7-8893-455e-b785-595592528040">
                    <value>A I51faf178-ca03-41eb-8276-385ef2a185b3</value>
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="string"/>
        </responseDeclarations>

    Private _result6 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="directedPair">
                <correctResponse interpretation="Ø I51faf178-ca03-41eb-8276-385ef2a185b3&amp;(B I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;Ø Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(Ø I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;A Ie1f57945-a74c-4f6c-948b-5133fb9778e8)|(E I989f6f3c-9d38-492f-80fe-4b71cfee574f&amp;D Ie1f57945-a74c-4f6c-948b-5133fb9778e8)&amp;D I723529e7-8893-455e-b785-595592528040">
                    <value>B I989f6f3c-9d38-492f-80fe-4b71cfee574f</value>
                    <value>D I723529e7-8893-455e-b785-595592528040</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>


End Class
