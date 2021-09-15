
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class integration_OrderedTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Set Order Parameter, and compare to expected solution, note that this is stored in a KeyFactSet by default!")>
    Public Sub IntegrationTest_Step1()
        'Arrange     
        Dim solution = New Solution()
        Dim sp = OrderedChoiceScoringParameters()
        Dim manipulator = sp(0).GetScoreManipulator(solution)
        manipulator.SetFactSetTarget(0) 'Is required since this SP is grouped initially

        WriteToDebug(solution, "Arrange")

        'Act
        'A=5 , B=4, C=3, D=2, E=1, 
        manipulator.SetKey("A", 1)
        manipulator.SetKey("B", 2)
        manipulator.SetKey("C", 3)
        manipulator.SetKey("D", 4)
        manipulator.SetKey("E", 5)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Get Concept Manipulator, whole structure is created")>
    Public Sub IntegrationTest_Step2()
        'Arrange     
        Dim solution = Step1.To(Of Solution)()
        Dim sp = OrderedChoiceScoringParameters()

        WriteToDebug(solution, "Arrange")
        
        'Act
        Dim combinedScoringMap = New ScoringMap(sp, solution).GetMap().Single()
        Dim manipulator = combinedScoringMap.GetConceptManipulator(solution)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step2.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Add 2nd KeyFactSet")>
    Public Sub IntegrationTest_Step3()
        'Arrange     
        Dim solution = Step2.To(Of Solution)()
        Dim sp = OrderedChoiceScoringParameters()

        Dim map = New ScoringMap(sp, solution).GetMap().Single()
        Dim manipulator = sp(0).GetScoreManipulator(solution)
        Dim factSetManipulator = New FactTargetManipulator(solution)
        factSetManipulator.AddFactSet(map)

        manipulator.SetFactSetTarget(1)

        WriteToDebug(solution, "Arrange")

        'Act
        manipulator.SetKey("A", 5)
        manipulator.SetKey("B", 4)
        manipulator.SetKey("C", 3)
        manipulator.SetKey("D", 2)
        manipulator.SetKey("E", 1)

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step3.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("get ConceptManipulator again")>
    Public Sub IntegrationTest_Step4()
        'Arrange     
        Dim solution = Step3.To(Of Solution)()
        Dim sp = OrderedChoiceScoringParameters()

        WriteToDebug(solution, "Arrange")
        'Act
        Dim combinedScoringMap = New ScoringMap(sp, solution).GetMap().Single()
        Dim manipulator = combinedScoringMap.GetConceptManipulator(solution)

        'Assert
        WriteToDebug(solution, "Assert")

         Assert.IsTrue(UnitTestHelper.AreSame(Step4.ToString(), solution.DoSerialize().ToString()))
    End Sub

#Region "Data"

    ReadOnly Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="orderFO" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="orderFO" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="orderFO" scoringMethod="None">
                                             <conceptFactSet>
                                                 <conceptFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="E[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step3 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="orderFO" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                             <keyFactSet>
                                                 <keyFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="orderFO" scoringMethod="None">
                                             <conceptFactSet>
                                                 <conceptFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="E[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
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
                                         <keyFinding id="orderFO" scoringMethod="None">
                                             <keyFactSet>
                                                 <keyFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                             <keyFactSet>
                                                 <keyFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                                 <keyFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <keyValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </keyValue>
                                                 </keyFact>
                                             </keyFactSet>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="orderFO" scoringMethod="None">
                                             <conceptFactSet>
                                                 <conceptFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="E[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <catchAllValue/>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                             <conceptFactSet>
                                                 <conceptFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>5</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>4</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>3</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="D-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>2</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                                 <conceptFact id="E-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                     <conceptValue domain="OC" occur="1">
                                                         <integerValue>
                                                             <typedValue>1</typedValue>
                                                         </integerValue>
                                                     </conceptValue>
                                                 </conceptFact>
                                             </conceptFactSet>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>

#End Region

    Function OrderedChoiceScoringParameters() As OrderScoringParameter()
        Return New OrderScoringParameter() {
          New OrderScoringParameter() With {.FindingOverride = "orderFO", .ControllerId = "OC"}.AddSubParameters("A", "B", "C", "D", "E")
      }
    End Function

End Class
