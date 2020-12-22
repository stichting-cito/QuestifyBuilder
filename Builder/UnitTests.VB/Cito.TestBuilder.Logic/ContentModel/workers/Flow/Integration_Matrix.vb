
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class Integration_Matrix

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Add Matrix scoring a=3 b=1 c=2 ")>
    Public Sub IntegrationTest_Step1()
        Dim solution = New Solution()
        WriteToDebug(solution, "Arrange")

        Dim sp = MatrixScoringParameters()
        sp.GetScoreManipulator(solution).SetKey("A", "3")
        sp.GetScoreManipulator(solution).SetKey("B", "1")
        sp.GetScoreManipulator(solution).SetKey("C", "2")

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Add Matrix scoring a=3 b=1 c=2")>
    Public Sub IntegrationTest_Step2()
        Dim solution = Step1.To(Of Solution)()
        WriteToDebug(solution, "Arrange")

        Dim sp = MatrixScoringParameters()
        Dim map = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()

        map(0).GetConceptManipulator(solution)

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step2.ToString(), solution.DoSerialize().ToString()))
    End Sub


    ReadOnly Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="matrixFO" scoringMethod="None">
                                             <keyFact id="A-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="MXCA" occur="1">
                                                     <stringValue>
                                                         <typedValue>3</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="B-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="MXCB" occur="1">
                                                     <stringValue>
                                                         <typedValue>1</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="C-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="MXCC" occur="1">
                                                     <stringValue>
                                                         <typedValue>2</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="matrixFO" scoringMethod="None">
                                             <keyFact id="A-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="MXCA" occur="1">
                                                     <stringValue>
                                                         <typedValue>3</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="B-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="MXCB" occur="1">
                                                     <stringValue>
                                                         <typedValue>1</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="C-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="MXCC" occur="1">
                                                     <stringValue>
                                                         <typedValue>2</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="matrixFO" scoringMethod="None">
                                             <conceptFact id="A-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="MXCA" occur="1">
                                                     <stringValue>
                                                         <typedValue>3</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="B-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="MXCB" occur="1">
                                                     <stringValue>
                                                         <typedValue>1</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="C-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="MXCC" occur="1">
                                                     <stringValue>
                                                         <typedValue>2</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="A[*]-MXC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="MXCA" occur="1">
                                                     <catchAllValue/>
                                                 </conceptValue>
                                             </conceptFact>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>


    Private Function MatrixScoringParameters() As MatrixScoringParameter
        Dim toReturn = New MatrixScoringParameter() With {.Name = "matrix", .ControllerId = "MXC", .FindingOverride = "matrixFO"}.AddSubParameters("A", "B", "C")
        toReturn.MatrixColumnsDefinition = New MultiChoiceScoringParameter().AddSubParameters("1", "2", "3")
        Return toReturn
    End Function
End Class
