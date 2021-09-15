
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class Integration_IntegerTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Set scores to a=131 b=42")>
    Public Sub IntegrationTest_Step1()
        'Arrange     
        Dim solution = New Solution()
        Dim sp = IntegerScoringParameters()
        WriteToDebug(solution, "Arrange")
        
        'Act
        sp.GetScoreManipulator(solution).SetKey("A", 131)
        sp.GetScoreManipulator(solution).SetKey("B", 42)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("change b to 42 or 150")>
    Public Sub IntegrationTest_Step2()
        'Arrange     
        Dim solution = Step1.To(Of Solution)()
        Dim sp = IntegerScoringParameters()

        WriteToDebug(solution, "Arrange")
        
        'Act
        sp.GetScoreManipulator(solution).RemoveKey("B")
        sp.GetScoreManipulator(solution).SetKey("B", 42, 150)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step2.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Group A & B")>
    Public Sub IntegrationTest_Step3()
        'Arrange     
        Dim solution = Step2.To(Of Solution)()
        Dim sp = IntegerScoringParameters()
        Dim map = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        WriteToDebug(solution, "Arrange")
        
        'Act
        Dim grouper = New FactTargetManipulator(solution)
        grouper.GroupInteractions(map.SelectMany(Function(csm) csm))

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step3.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")> <Description("Add a factset")>
    Public Sub IntegrationTest_Step4()
        'Arrange     
        Dim solution = Step3.To(Of Solution)()
        Dim sp = IntegerScoringParameters()
        Dim map = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        WriteToDebug(solution, "Arrange")
        
        'Act
        Dim grouper = New FactTargetManipulator(solution)
        Dim factId = grouper.AddFactSet(map.First())

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step4.ToString(), solution.DoSerialize().ToString()))
        Assert.AreEqual(1, factId)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")> <Description("Create an alternative solution")>
    Public Sub IntegrationTest_Step5()
        'Arrange     
        Dim solution = Step4.To(Of Solution)()
        Dim sp = IntegerScoringParameters()
        Dim manipulator = sp.GetScoreManipulator(solution)
        manipulator.SetFactSetTarget(1)
        WriteToDebug(solution, "Arrange")

        'Act
        manipulator.SetKey("A", 42, 150)
        manipulator.SetKey("B", 131)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step5.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")> <Description("Start with concept")>
    Public Sub IntegrationTest_Step6()
        'Arrange     
        Dim solution = Step5.To(Of Solution)()
        Dim sp = IntegerScoringParameters()
        Dim combinedScoringMapKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        WriteToDebug(solution, "Arrange")

        'Act
        combinedScoringMapKey.GetConceptManipulator(solution)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(step6.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")> <Description("remove facset")>
    Public Sub IntegrationTest_Step7()
        'Arrange     
        Dim solution = step6.To(Of Solution)()
        Dim sp = IntegerScoringParameters()
        Dim combinedScoringMapKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        WriteToDebug(solution, "Arrange")

        'Act
        Dim grouper = New FactTargetManipulator(solution)
        grouper.RemoveFactSet(combinedScoringMapKey.First(), 1)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(step7.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")> <Description("get concept manipulator, after renoving KeyFactSet")>
    Public Sub IntegrationTest_Step8()
        'Arrange     
        Dim solution = step7.To(Of Solution)()
        Dim sp = IntegerScoringParameters()
        Dim combinedScoringMapKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        WriteToDebug(solution, "Arrange")

        'Act
        combinedScoringMapKey.GetConceptManipulator(solution)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(step8.ToString(), solution.DoSerialize().ToString()))
    End Sub

    Private Function IntegerScoringParameters() As IntegerScoringParameter
        Return New IntegerScoringParameter() With {.Name = "INT", .FindingOverride = "Opgave", .ControllerId = "Integer"}.AddSubParameters("A", "B")
    End Function

#Region "Data"

    ReadOnly Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">

                                             <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="Integer" occur="1">
                                                     <integerValue>
                                                         <typedValue>131</typedValue>
                                                     </integerValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="Integer" occur="1">
                                                     <integerValue>
                                                         <typedValue>42</typedValue>
                                                     </integerValue>
                                                 </keyValue>
                                             </keyFact>

                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">

                                             <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="Integer" occur="1">
                                                     <integerValue>
                                                         <typedValue>131</typedValue>
                                                     </integerValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="Integer" occur="1">
                                                     <integerValue>
                                                         <typedValue>42</typedValue>
                                                     </integerValue>
                                                     <integerValue>
                                                         <typedValue>150</typedValue>
                                                     </integerValue>
                                                 </keyValue>
                                             </keyFact>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step3 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step4 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step5 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly step6 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="Opgave" scoringMethod="None">
                                             <conceptFactSet>
                                                 <conceptFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A[*]-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B[*]-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly step7 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="Opgave" scoringMethod="None">
                                             <conceptFactSet>
                                                 <conceptFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A[*]-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B[*]-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly step8 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Opgave" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="Opgave" scoringMethod="None">
                                             <conceptFactSet>
                                                 <conceptFact id="A-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>131</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <integerValue>
                                                             <typedValue>42</typedValue>
                                                         </integerValue>
                                                         <integerValue>
                                                             <typedValue>150</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A[*]-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B[*]-Integer" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="Integer" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>

#End Region

End Class
