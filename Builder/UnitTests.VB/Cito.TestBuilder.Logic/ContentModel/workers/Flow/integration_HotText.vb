
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class integration_HotText

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    Public Sub IntegrationTest_Step1()
        Dim solution = New Solution()
        Dim HotTxtPrms = HotTextScoringParameters()
        WriteToDebug(solution, "Arrange")

        HotTxtPrms(0).GetScoreManipulator(solution).RemoveKey("123")
        HotTxtPrms(1).GetScoreManipulator(solution).RemoveKey("456")

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Group and add a second set, just like the scoring editor.")>
    Public Sub IntegrationTest_Step2()
        Dim solution = Step1.To(Of Solution)()
        Dim HotTxtPrms = HotTextScoringParameters()
        Dim map = New ScoringMap(HotTxtPrms, solution).GetMap().ToList()
        WriteToDebug(solution, "Arrange")

        Dim factSetId = New FactTargetManipulator(solution).GroupInteractions(map.SelectMany(Function(smk) smk))
        Dim map2 = New ScoringMap(HotTxtPrms, solution).GetMap().ToList()
        Dim factSetId2 = New FactTargetManipulator(solution).AddFactSet(map2.First())

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step2.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Get Concept")>
    Public Sub IntegrationTest_Step3()
        Dim solution = Step2.To(Of Solution)()
        Dim HotTxtPrms = HotTextScoringParameters()
        Dim combinedScoringMapKey = New ScoringMap(HotTxtPrms, solution).GetMap().First()
        WriteToDebug(solution, "Arrange")

        combinedScoringMapKey.GetConceptManipulator(solution)

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step3.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Remove factset from keyFiding")>
    Public Sub IntegrationTest_Step4()
        Dim solution = Step3.To(Of Solution)()
        Dim HotTxtPrms = HotTextScoringParameters()
        Dim combinedScoringMapKey = New ScoringMap(HotTxtPrms, solution).GetMap().First()
        WriteToDebug(solution, "Arrange")

        Dim manipulator = New FactTargetManipulator(solution)
        manipulator.RemoveFactSet(combinedScoringMapKey.First, 1)

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(step4.ToString(), solution.DoSerialize().ToString()))
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Get Concept, an answerCatagory should have been formed.")>
    Public Sub IntegrationTest_Step5()
        Dim solution = step4.To(Of Solution)()
        Dim HotTxtPrms = HotTextScoringParameters()
        Dim combinedScoringMapKey = New ScoringMap(HotTxtPrms, solution).GetMap().First()
        WriteToDebug(solution, "Arrange")

        combinedScoringMapKey.GetConceptManipulator(solution)

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(step5.ToString(), solution.DoSerialize().ToString()))
    End Sub

    Private Function HotTextScoringParameters() As List(Of HotTextScoringParameter)
        Dim ret = New List(Of HotTextScoringParameter)

        ret.Add(New HotTextScoringParameter() With {.Name = "a", .FindingOverride = "RodeFinding", .ControllerId = "Test1"}.AddSubParameters("123"))
        ret.Add(New HotTextScoringParameter() With {.Name = "b", .FindingOverride = "RodeFinding", .ControllerId = "Test2"}.AddSubParameters("456"))

        Return ret
    End Function


    Private Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                    <keyFindings>
                                        <keyFinding id="RodeFinding" scoringMethod="None">
                                            <keyFact id="123-Test1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="123-Test1" occur="1">
                                                    <booleanValue>
                                                        <typedValue>false</typedValue>
                                                    </booleanValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="456-Test2" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="456-Test2" occur="1">
                                                    <booleanValue>
                                                        <typedValue>false</typedValue>
                                                    </booleanValue>
                                                </keyValue>
                                            </keyFact>
                                        </keyFinding>
                                    </keyFindings>
                                    <aspectReferences/>
                                </solution>

    Private Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                    <keyFindings>
                                        <keyFinding id="RodeFinding" scoringMethod="None">
                                            <keyFactSet>
                                                <keyFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                                <keyFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                            </keyFactSet>
                                            <keyFactSet>
                                                <keyFact id="123-Test1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                                <keyFact id="456-Test2" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                            </keyFactSet>
                                        </keyFinding>
                                    </keyFindings>
                                    <aspectReferences/>
                                </solution>

    Private Step3 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                    <keyFindings>
                                        <keyFinding id="RodeFinding" scoringMethod="None">
                                            <keyFactSet>
                                                <keyFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                                <keyFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                            </keyFactSet>
                                            <keyFactSet>
                                                <keyFact id="123-Test1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                                <keyFact id="456-Test2" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                            </keyFactSet>
                                        </keyFinding>
                                    </keyFindings>
                                    <conceptFindings>
                                        <conceptFinding id="RodeFinding" scoringMethod="None">
                                            <conceptFactSet>
                                                <conceptFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFactSet>
                                            <conceptFactSet>
                                                <conceptFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFactSet>
                                            <conceptFactSet>
                                                <conceptFact id="123[*]-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="123-Test1" occur="1">
                                                        <catchAllValue/>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="456[*]-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="456-Test2" occur="1">
                                                        <catchAllValue/>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFactSet>
                                        </conceptFinding>
                                    </conceptFindings>
                                    <aspectReferences/>
                                </solution>

    Private step4 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                    <keyFindings>
                                        <keyFinding id="RodeFinding" scoringMethod="None">
                                            <keyFactSet>
                                                <keyFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                                <keyFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                            </keyFactSet>
                                        </keyFinding>
                                    </keyFindings>
                                    <conceptFindings>
                                        <conceptFinding id="RodeFinding" scoringMethod="None">
                                            <conceptFactSet>
                                                <conceptFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFactSet>
                                            <conceptFactSet>
                                                <conceptFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFactSet>
                                            <conceptFactSet>
                                                <conceptFact id="123[*]-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="123-Test1" occur="1">
                                                        <catchAllValue/>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="456[*]-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="456-Test2" occur="1">
                                                        <catchAllValue/>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFactSet>
                                        </conceptFinding>
                                    </conceptFindings>
                                    <aspectReferences/>
                                </solution>

    Private step5 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                    <keyFindings>
                                        <keyFinding id="RodeFinding" scoringMethod="None">
                                            <keyFactSet>
                                                <keyFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                                <keyFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </keyValue>
                                                </keyFact>
                                            </keyFactSet>
                                        </keyFinding>
                                    </keyFindings>
                                    <conceptFindings>
                                        <conceptFinding id="RodeFinding" scoringMethod="None">
                                            <conceptFactSet>
                                                <conceptFact id="123-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="123-Test1" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="456-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="456-Test2" occur="1">
                                                        <booleanValue>
                                                            <typedValue>false</typedValue>
                                                        </booleanValue>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFactSet>
                                            <conceptFactSet>
                                                <conceptFact id="123[*]-Test1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="123-Test1" occur="1">
                                                        <catchAllValue/>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="456[*]-Test2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="456-Test2" occur="1">
                                                        <catchAllValue/>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFactSet>
                                        </conceptFinding>
                                    </conceptFindings>
                                    <aspectReferences/>
                                </solution>


End Class
