
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class Integration_2MC_Grouped


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Add 2 mc and group these")>
    Public Sub IntegrationTest_Step1()
        Dim solution = New Solution()
        Dim sp = ChoiceScoringParameters()
        sp(0).GetScoreManipulator(solution).SetKey("B")
        sp(1).GetScoreManipulator(solution).SetKey("3")

        Dim map = New ScoringMap(sp, solution).GetMap().ToList()

        WriteToDebug(solution, "Arrange")

        Dim grouper = New FactTargetManipulator(solution)
        Dim factId = grouper.GroupInteractions(map.SelectMany(Function(csm) csm))

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
        Assert.AreEqual(0, factId, "No Keyfacts present,.. so only logical that it is added at position 0")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Get Concept Manipulator, whole structure is created")>
    Public Sub IntegrationTest_Step2()
        Dim solution = Step1.To(Of Solution)()
        Dim sp = ChoiceScoringParameters()
        Dim combinedScoringMap = New ScoringMap(sp, solution).GetMap().Single()
        WriteToDebug(solution, "Arrange")

        Dim conceptManipulator = combinedScoringMap.GetConceptManipulator(solution)
        Dim conceptIds = conceptManipulator.GetConceptIds().ToList()

        WriteToDebug(solution, "Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(Step2.ToString(), solution.DoSerialize().ToString()))
        Assert.AreEqual("0", conceptIds(0))
        Assert.AreEqual("1", conceptIds(1))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Get Display Value")>
    Public Sub IntegrationTest_Step3()
        Dim solution = Step2.To(Of Solution)()
        Dim sp = ChoiceScoringParameters()
        Dim combinedScoringMap = New ScoringMap(sp, solution).GetMap().Single()
        WriteToDebug(solution, "Arrange")

        Dim conceptManipulator = combinedScoringMap.GetConceptManipulator(solution)

        WriteToDebug(solution, "Assert")

        Assert.AreEqual("B&3", conceptManipulator.GetDisplayValueForConceptId("0"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <WorkItem(20802)>
    Public Sub FixBug()
        Dim solution = FixBugData.To(Of Solution)()
        Dim sp = ChoiceScoringParameters()
        Dim combinedScoringMap = New ScoringMap(sp, solution).GetMap().Single()
        WriteToDebug(solution, "Arrange")

        combinedScoringMap.GetConceptManipulator(solution)

        WriteToDebug(solution, "Assert")

        Assert.AreEqual(3, solution.ConceptFindings.First().KeyFactsets.Count)
    End Sub

    Dim Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                <keyFindings>
                                    <keyFinding id="Integratie" scoringMethod="None">
                                        <keyFactSet>
                                            <keyFact id="B-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="Een" occur="1">
                                                    <stringValue>
                                                        <typedValue>B</typedValue>
                                                    </stringValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="3-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="Twee" occur="1">
                                                    <stringValue>
                                                        <typedValue>3</typedValue>
                                                    </stringValue>
                                                </keyValue>
                                            </keyFact>
                                        </keyFactSet>
                                    </keyFinding>
                                </keyFindings>
                                <aspectReferences/>
                            </solution>

    Dim Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                <keyFindings>
                                    <keyFinding id="Integratie" scoringMethod="None">
                                        <keyFactSet>
                                            <keyFact id="B-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="Een" occur="1">
                                                    <stringValue>
                                                        <typedValue>B</typedValue>
                                                    </stringValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="3-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="Twee" occur="1">
                                                    <stringValue>
                                                        <typedValue>3</typedValue>
                                                    </stringValue>
                                                </keyValue>
                                            </keyFact>
                                        </keyFactSet>
                                    </keyFinding>
                                </keyFindings>
                                <conceptFindings>
                                    <conceptFinding id="Integratie" scoringMethod="None">

                                        <conceptFactSet>
                                            <conceptFact id="B-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <conceptValue domain="Een" occur="1">
                                                    <stringValue>
                                                        <typedValue>B</typedValue>
                                                    </stringValue>
                                                </conceptValue>
                                            </conceptFact>
                                            <conceptFact id="3-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <conceptValue domain="Twee" occur="1">
                                                    <stringValue>
                                                        <typedValue>3</typedValue>
                                                    </stringValue>
                                                </conceptValue>
                                            </conceptFact>
                                        </conceptFactSet>

                                        <conceptFactSet>
                                            <conceptFact id="A[*]-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <conceptValue domain="Een" occur="1">
                                                    <catchAllValue/>
                                                </conceptValue>
                                            </conceptFact>
                                            <conceptFact id="B[*]-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <conceptValue domain="Een" occur="1">
                                                    <catchAllValue/>
                                                </conceptValue>
                                            </conceptFact>
                                            <conceptFact id="C[*]-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <conceptValue domain="Een" occur="1">
                                                    <catchAllValue/>
                                                </conceptValue>
                                            </conceptFact>
                                            <conceptFact id="1[*]-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <conceptValue domain="Twee" occur="1">
                                                    <catchAllValue/>
                                                </conceptValue>
                                            </conceptFact>
                                            <conceptFact id="2[*]-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <conceptValue domain="Twee" occur="1">
                                                    <catchAllValue/>
                                                </conceptValue>
                                            </conceptFact>
                                            <conceptFact id="3[*]-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <conceptValue domain="Twee" occur="1">
                                                    <catchAllValue/>
                                                </conceptValue>
                                            </conceptFact>
                                        </conceptFactSet>

                                    </conceptFinding>
                                </conceptFindings>
                                <aspectReferences/>
                            </solution>


    Dim FixBugData As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Integratie" scoringMethod="None">

                                             <keyFactSet>
                                                 <keyFact id="B-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Een" occur="1">
                                                         <stringValue>
                                                             <typedValue>B</typedValue>
                                                         </stringValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="3-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Twee" occur="1">
                                                         <stringValue>
                                                             <typedValue>3</typedValue>
                                                         </stringValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>

                                             <keyFactSet>
                                                 <keyFact id="B-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Een" occur="1">
                                                         <stringValue>
                                                             <typedValue>A</typedValue>
                                                         </stringValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="3-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Twee" occur="1">
                                                         <stringValue>
                                                             <typedValue>2</typedValue>
                                                         </stringValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>

                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="Integratie" scoringMethod="None">

                                             <conceptFactSet>
                                                 <conceptFact id="B-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="B-Een" occur="1">
                                                         <stringValue>
                                                             <typedValue>B</typedValue>
                                                         </stringValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="3-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="3-Twee" occur="1">
                                                         <stringValue>
                                                             <typedValue>3</typedValue>
                                                         </stringValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>

                                             <conceptFactSet>
                                                 <conceptFact id="A[*]-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="A[*]-Een" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B[*]-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="B[*]-Een" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C[*]-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="C[*]-Een" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="1[*]-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="1[*]-Twee" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="2[*]-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="2[*]-Twee" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="3[*]-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="3[*]-Twee" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>

                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>

    Private Function ChoiceScoringParameters() As ChoiceScoringParameter()
        Return New ChoiceScoringParameter() {
            New ChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Een", .MaxChoices = 1}.AddSubParameters("A", "B", "C"),
            New ChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Twee", .MaxChoices = 1}.AddSubParameters("1", "2", "3")
        }
    End Function
End Class
