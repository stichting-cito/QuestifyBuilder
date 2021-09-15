
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports System.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class Integration_GapMatchTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Add GapMatch, set some score")>
    Public Sub IntegrationTest_Step1()
        'Arrange     
        Dim solution = New Solution()
        WriteToDebug(solution, "Arrange")
        
        'Act
        Dim sp = GapMatchScoringParameter()
        sp.GetScoreManipulator(solution).SetKey("1", "A")
        sp.GetScoreManipulator(solution).SetKey("2", "C")
        
        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Change score")>
    Public Sub IntegrationTest_Step2()
        'Arrange     
        Dim solution = Step1.To(Of Solution)()
        WriteToDebug(solution, "Arrange")
        
        'Act
        Dim sp = GapMatchScoringParameter()
        sp.GetScoreManipulator(solution).SetKey("2", "A")
        sp.GetScoreManipulator(solution).SetKey("1", "B")

        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step2.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("Get Concept Manipulator")>
    Public Sub IntegrationTest_Step3()
        'Arrange     
        Dim solution = Step1.To(Of Solution)()

        Dim sp = GapMatchScoringParameter()
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        
        'Act
        combinedScoringMap.GetConceptManipulator(solution)
        
        'Assert
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step3.ToString(), solution.DoSerialize().ToString()))
    End Sub

#Region "Data"
    ReadOnly Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="gapMatchFinding" scoringMethod="None">
                                             <keyFact id="1-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="1" occur="1">
                                                     <stringValue>
                                                         <typedValue>A</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="2-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="2" occur="1">
                                                     <stringValue>
                                                         <typedValue>C</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="gapMatchFinding" scoringMethod="None">
                                             <keyFact id="2-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="2" occur="1">
                                                     <stringValue>
                                                         <typedValue>A</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="1-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="1" occur="1">
                                                     <stringValue>
                                                         <typedValue>B</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                         </keyFinding>
                                     </keyFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step3 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="gapMatchFinding" scoringMethod="None">
                                             <keyFact id="1-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="1" occur="1">
                                                     <stringValue>
                                                         <typedValue>A</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                             <keyFact id="2-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="2" occur="1">
                                                     <stringValue>
                                                         <typedValue>C</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="gapMatchFinding" scoringMethod="None">
                                             <conceptFact id="1-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="1" occur="1">
                                                     <stringValue>
                                                         <typedValue>A</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="2-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="2" occur="1">
                                                     <stringValue>
                                                         <typedValue>C</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="1[*]-gapMatch" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="1" occur="1">
                                                     <catchAllValue/>
                                                 </conceptValue>
                                             </conceptFact>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>
#End Region

    Private Function GapMatchScoringParameter() As GapMatchScoringParameter
        Dim toReturn = New GapMatchScoringParameter() With {.Name = "gap", .ControllerId = "gapMatch", .FindingOverride = "gapMatchFinding"}.AddSubParameters("A", "B", "C")

        toReturn.Value.Item(0).InnerParameters.Add(New GapTextParameter() With {.Name = "gapText", .MatchMax = 1, .Value = "Apple"})
        toReturn.Value.Item(1).InnerParameters.Add(New GapTextParameter() With {.Name = "gapText", .MatchMax = 1, .Value = "Banana"})
        toReturn.Value.Item(2).InnerParameters.Add(New GapTextParameter() With {.Name = "gapText", .MatchMax = 1, .Value = "Clementine"})

        toReturn.GapXhtmlParameter.AddInlineGapMatch("1", "one")
        toReturn.GapXhtmlParameter.AddInlineGapMatch("2", "two")

        toReturn = toReturn.Transform

        Return toReturn
    End Function

End Class
