
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class Integration_MR_Grouped

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Add mr, set keys and group")>
    Public Sub IntegrationTest_Step1()
        Dim solution = New Solution()
        Dim sp = ChoiceScoringParameters()
        sp.GetScoreManipulator(solution).SetKey("A")
        sp.GetScoreManipulator(solution).SetKey("D")
        sp.GetScoreManipulator(solution).RemoveKey("B")
        sp.GetScoreManipulator(solution).RemoveKey("C")

        Dim map = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()

        WriteToDebug(solution, "Arrange")

        Dim grouper = New FactTargetManipulator(solution)
        Dim factId = grouper.GroupInteractions(map.SelectMany(Function(csm) csm))

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
        Assert.AreEqual(0, factId, "No Keyfacts present, so only logical that it is added at position 0")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Get concept")>
    Public Sub IntegrationTest_Step2()
        Dim solution = Step1.To(Of Solution)()
        Dim sp = ChoiceScoringParameters()
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().Single()
        WriteToDebug(solution, "Arrange")

        Dim conceptManipulator = combinedScoringMap.GetConceptManipulator(solution)
        Dim conceptIds = conceptManipulator.GetConceptIds().ToList()

        WriteToDebug(solution, "Assert")
        Assert.IsTrue(UnitTestHelper.AreSame(Step2.ToString(), solution.DoSerialize().ToString()))
        Assert.AreEqual("0", conceptIds(0))
        Assert.AreEqual("1", conceptIds(1))
    End Sub

    ReadOnly Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="A-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>true</typedValue>
                                                         </booleanValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="B-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>false</typedValue>
                                                         </booleanValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="C-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>false</typedValue>
                                                         </booleanValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="D-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="D-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>true</typedValue>
                                                         </booleanValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="A-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>true</typedValue>
                                                         </booleanValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="B-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>false</typedValue>
                                                         </booleanValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="C-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>false</typedValue>
                                                         </booleanValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="D-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="D-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>true</typedValue>
                                                         </booleanValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="Opgave" scoringMethod="None">
                                             <conceptFactSet>
                                                 <conceptFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="A-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>true</typedValue>
                                                         </booleanValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="B-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>false</typedValue>
                                                         </booleanValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="C-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>false</typedValue>
                                                         </booleanValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="D-mc_1" occur="1">
                                                         <booleanValue>
                                                             <typedValue>true</typedValue>
                                                         </booleanValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A[*]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="A-mc_1" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B[*]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="B-mc_1" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C[*]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="C-mc_1" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D[*]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="D-mc_1" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>


    Private Function ChoiceScoringParameters() As MultiChoiceScoringParameter
        Return New MultiChoiceScoringParameter() With {.Name = "MC1", .FindingOverride = "Opgave", .ControllerId = "mc_1"}.AddSubParameters("A", "B", "C", "D")
    End Function
End Class
