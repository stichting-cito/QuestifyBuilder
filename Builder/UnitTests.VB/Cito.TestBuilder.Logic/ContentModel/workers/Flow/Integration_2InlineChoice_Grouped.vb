
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class Integration_2InlineChoice_Grouped

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Add 2 inlineChoices and group these")>
    Public Sub IntegrationTest_Step1()
        Dim solution = New Solution()
        Dim sp = InlineChoiceScoringParameters()
        sp(0).GetScoreManipulator(solution).SetKey("B")
        sp(1).GetScoreManipulator(solution).SetKey("3")

        Dim map = New ScoringMap(sp, solution).GetMap().ToList()

        WriteToDebug(solution, "Arrange")

        Dim grouper = New FactTargetManipulator(solution)
        Dim factId = grouper.GroupInteractions(map.SelectMany(Function(csm) csm))

        WriteToDebug(solution, "Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
        Assert.AreEqual(0, factId, "No Keyfacts present, so only logical that it is added at position 0")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Get Concept Manipulator, whole concept structure is created")>
    Public Sub IntegrationTest_Step2()
        Dim solution = Step1.To(Of Solution)()
        Dim sp = InlineChoiceScoringParameters()
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
        Dim sp = InlineChoiceScoringParameters()
        Dim combinedScoringMap = New ScoringMap(sp, solution).GetMap().Single()
        WriteToDebug(solution, "Arrange")

        Dim conceptManipulator = combinedScoringMap.GetConceptManipulator(solution)

        WriteToDebug(solution, "Assert")

        Assert.AreEqual("B&3", conceptManipulator.GetDisplayValueForConceptId("0"))
        Assert.AreEqual("{∗}&{∗}&{∗}&{∗}&{∗}&{∗}", conceptManipulator.GetDisplayValueForConceptId("1"))
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Change Score and refresh concept.")>
    Public Sub IntegrationTest_Step4()
        Dim solution = Step2.To(Of Solution)()
        Dim sp = InlineChoiceScoringParameters()
        Dim combinedScoringMap = New ScoringMap(sp, solution).GetMap().Single()
        WriteToDebug(solution, "Arrange")

        sp(0).GetScoreManipulator(solution).SetKey("C")
        sp(1).GetScoreManipulator(solution).SetKey("2")
        combinedScoringMap.GetConceptManipulator(solution)

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step4.ToString(), solution.DoSerialize().ToString()))
    End Sub


    ReadOnly Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
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

    ReadOnly Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
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

    ReadOnly Step4 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Integratie" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="C-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Een" occur="1">
                                                         <stringValue>
                                                             <typedValue>C</typedValue>
                                                         </stringValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="2-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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
                                                 <conceptFact id="C-Een" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Een" occur="1">
                                                         <stringValue>
                                                             <typedValue>C</typedValue>
                                                         </stringValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="2-Twee" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Twee" occur="1">
                                                         <stringValue>
                                                             <typedValue>2</typedValue>
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





    Protected Overridable Function InlineChoiceScoringParameters() As ChoiceScoringParameter()
        Return New InlineChoiceScoringParameter() {
            New InlineChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Een", .MaxChoices = 1}.AddSubParameters("A", "B", "C"),
            New InlineChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Twee", .MaxChoices = 1}.AddSubParameters("1", "2", "3")
        }
    End Function
End Class
