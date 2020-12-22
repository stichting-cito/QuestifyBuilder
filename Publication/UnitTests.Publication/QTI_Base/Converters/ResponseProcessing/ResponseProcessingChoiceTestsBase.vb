
Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseProcessingChoiceTestsBase

        Protected Function GetInlineChoiceScoringParams(nrOfParams As Integer) As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            If nrOfParams >= 1 Then scoreParams.Add(New InlineChoiceScoringParameter() With {.Label = "choice1", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I13abebba-ebb7-4ed8-81d0-46e9e1b60a53", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B"))
            If nrOfParams >= 2 Then scoreParams.Add(New InlineChoiceScoringParameter() With {.Label = "choice2", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "Ie830b508-32a0-42f4-920f-0e06f0f19313", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B"))
            If nrOfParams >= 3 Then scoreParams.Add(New InlineChoiceScoringParameter() With {.Label = "choice3", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I22f81e91-aaa4-4528-999e-61b31b8123ec", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B"))
            If nrOfParams >= 4 Then scoreParams.Add(New InlineChoiceScoringParameter() With {.Label = "choice4", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I5196c3f8-ddf3-4423-b777-d1d2c71d6efb", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B"))
            Return scoreParams
        End Function

        Protected _finding1 As XElement =
            <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                <keyFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding2 As XElement =
            <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                <keyFactSet>
                    <keyFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="B-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding3 As XElement =
            <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                <keyFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFactSet>
                    <keyFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="B-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding4 As XElement =
            <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                <keyFactSet>
                    <keyFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="B-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="A-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="B-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="I5196c3f8-ddf3-4423-b777-d1d2c71d6efb" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding5 As XElement =
            <keyFinding id="inlineChoiceController" scoringMethod="Dichotomous">
                <keyFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding6 As XElement =
            <keyFinding id="inlineChoiceController" scoringMethod="Polytomous">
                <keyFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

    End Class

End Namespace