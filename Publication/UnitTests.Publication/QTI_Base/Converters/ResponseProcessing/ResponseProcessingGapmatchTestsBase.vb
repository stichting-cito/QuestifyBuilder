
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.UnitTests.Framework

Namespace QTI_Base

    Public MustInherit Class ResponseProcessingGapmatchTestsBase

        Protected Function GetGapMatchScoringParams_1() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim gapMatchScoringParameter = New GapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C", "D")

            Dim xhtmlValue As XElement = <xhtmlParameter name="gapMatchInlineInput">
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Tekst met <cito:InlineElement id="G1" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">G1</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 1</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> een aantal <cito:InlineElement id="G2" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">G2</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 2</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> gaten waarin teksten <cito:InlineElement id="G3" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">G3</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 3</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> kunnen worden gesleept <cito:InlineElement id="G4" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">G4</cito:plaintextparameter>
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

        Protected Function GetGapMatchScoringParams_2() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim gapMatchScoringParameter = New GapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C")

            Dim xhtmlValue As XElement = <xhtmlParameter name="gapMatchInlineInput">
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Tekst met <cito:InlineElement id="I77e2abe1-4fe4-4c34-a9af-6f6900868ecf" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I77e2abe1-4fe4-4c34-a9af-6f6900868ecf</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 1</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> een aantal <cito:InlineElement id="I9aeb153d-aaca-4390-a35a-63692cb03035" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I9aeb153d-aaca-4390-a35a-63692cb03035</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 2</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> gaten waarin teksten <cito:InlineElement id="I4174cc03-b6e0-4eab-8870-9973f6cca0d4" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I4174cc03-b6e0-4eab-8870-9973f6cca0d4</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 3</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> kunnen worden gesleept door de kandidaat.</p>
                                         </xhtmlParameter>

            Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xhtmlValue.ToString}

            gapMatchScoringParameter.GapXhtmlParameter = xhtmlPrm
            Dim scoreParam As ScoringParameter = gapMatchScoringParameter.Transform()
            scoreParams.Add(scoreParam)

            Return scoreParams
        End Function

        Protected Function GetGapMatchScoringParams_3() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)

            Dim gapMatchScoringParameter = <GapMatchRichTextScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="gapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController">
                                               <subparameterset id="A">
                                                   <gapTextRichTextParameter name="gapTextRichText" matchMax="1">
                                                       <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                                           <img isinlineelement="true" id="I40ff6b00-ea7a-49f3-80d3-d1a777526c7e" width="20" height="20" alt="Dot.jpg" src="resource://package/Dot.jpg"/> </p>
                                                   </gapTextRichTextParameter>
                                               </subparameterset>
                                               <subparameterset id="B">
                                                   <gapTextRichTextParameter name="gapTextRichText" matchMax="1">
                                                       <p id="c1-id-12">
                                                           <img isinlineelement="true" id="dfff9d57-96db-4383-b6a2-91702d5185f1" width="20" height="20" alt="Dot.jpg" src="resource://package/Dot.jpg"/>
                                                       </p>
                                                       <p id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml"> </p>
                                                   </gapTextRichTextParameter>
                                               </subparameterset>
                                               <definition id="">
                                                   <gapTextRichTextParameter name="gapTextRichText" matchMax="1"/>
                                               </definition>

                                           </GapMatchRichTextScoringParameter>

            Dim xHtml = <xhtmlParameter name="gapMatchInlineInput">
                            <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Op een dag was De Bende Van Madrid weer ober zee aan het varen. Het was toen 18 maart 1965.<br id="c1-id-12"/>Plots zag Dirk een Matroospiraat, een fles <strong id="c1-id-13">(A >) <cito:InlineElement id="I1b86db4d-9de2-4b81-96a2-b20766506894" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                        <cito:parameters>
                                            <cito:parameterSet id="e = ntireItem">
                                                <cito:plaintextparameter name="inlineGapMatchId">I1b86db4d-9de2-4b81-96a2-b20766506894</cito:plaintextparameter>
                                                <cito:plaintextparameter name="inlineGapMatchLabel">A</cito:plaintextparameter>
                                                <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                <cito:integerparameter name="width"/>
                                                <cito:integerparameter name="height"/>
                                            </cito:parameterSet>
                                        </cito:parameters>
                                    </cito:InlineElement></strong> drijven<strong id="c1-id-14"> (B >)</strong><cito:InlineElement id="I48433d84-039d-4b26-9bf8-226cdef1ffa4" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineGapMatchId">I48433d84-039d-4b26-9bf8-226cdef1ffa4</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineGapMatchLabel">B</cito:plaintextparameter>
                                            <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                            <cito:integerparameter name="width"/>
                                            <cito:integerparameter name="height"/>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement>in het water <strong id="c1-id-15">(C >)</strong><cito:InlineElement id="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineGapMatchId">I6fa9cbf3-9b01-442a-ab81-b1bfffea6549</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineGapMatchLabel">c</cito:plaintextparameter>
                                            <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                            <cito:integerparameter name="width"/>
                                            <cito:integerparameter name="height"/>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement> met behulp van nog enkele Matroospiraten, konden ze de fles aan boord krijgen. Er zat duidelijk een blad papier in, dat leek o een schatkaart.</p>
                            <p id="c1-id-16" xmlns="http://www.w3.org/1999/xhtml"> </p>
                            <p id="c1-id-17" xmlns="http://www.w3.org/1999/xhtml">
                                <strong id="c1-id-18">Vraag: Sleep de punt naar de juiste plek in de tekst.</strong>
                            </p>
                        </xhtmlParameter>
            Dim scoreParam = gapMatchScoringParameter.Deserialize(Of GapMatchRichTextScoringParameter)
            Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xHtml.ToString}

            scoreParam.GapXhtmlParameter = xhtmlPrm
            scoreParam = CType(scoreParam.Transform, GapMatchRichTextScoringParameter)
            scoreParams.Add(scoreParam)

            Return scoreParams
        End Function

        Protected _finding1 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding2 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                <keyFactSet>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G3" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G4" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G3" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G4" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding3 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                <keyFactSet>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G3" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G4" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G3" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G4" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding4 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding5 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding6 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                <keyFactSet>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G1" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G2" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G3" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G4" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G3" occur="1">
                            <stringValue>
                                <typedValue>D</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="G4" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding7 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                <keyFact id="G1-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G2-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="G4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="G4" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding8 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                <keyFactSet>
                    <keyFact id="I9aeb153d-aaca-4390-a35a-63692cb03035-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I9aeb153d-aaca-4390-a35a-63692cb03035" occur="1">
                            <stringValue>
                                <typedValue>C</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="I77e2abe1-4fe4-4c34-a9af-6f6900868ecf-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I77e2abe1-4fe4-4c34-a9af-6f6900868ecf" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="I4174cc03-b6e0-4eab-8870-9973f6cca0d4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I4174cc03-b6e0-4eab-8870-9973f6cca0d4" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding9 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                <keyFact id="I1b86db4d-9de2-4b81-96a2-b20766506894-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1b86db4d-9de2-4b81-96a2-b20766506894" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I48433d84-039d-4b26-9bf8-226cdef1ffa4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I48433d84-039d-4b26-9bf8-226cdef1ffa4" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549" occur="1">
                        <noValue/>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding10 As XElement =
            <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                <keyFact id="I1b86db4d-9de2-4b81-96a2-b20766506894-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1b86db4d-9de2-4b81-96a2-b20766506894" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I48433d84-039d-4b26-9bf8-226cdef1ffa4-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I48433d84-039d-4b26-9bf8-226cdef1ffa4" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I6fa9cbf3-9b01-442a-ab81-b1bfffea6549" occur="1">
                        <noValue/>
                    </keyValue>
                </keyFact>
            </keyFinding>

    End Class

End Namespace