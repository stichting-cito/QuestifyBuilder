
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class Intergration_MC_NotGrouped


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("MC set Key to X. GetConceptManipulator, validate outcome")>
    Public Sub IntegrationTest_Step1()
        Dim solution = New Solution()
        Dim sp = ChoiceScoringParameter()
        sp.GetScoreManipulator(solution).SetKey("X")

        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()

        WriteToDebug(solution, "Arrange")

        Dim conceptManipulator = combinedScoringMap.GetConceptManipulator(solution)

        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step1.ToString(), solution.DoSerialize().ToString()))
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Integration")>
    <Description("From step1, set key to Z. X no longer key, should end up as answerCatagory, GetConceptManipulator, validate outcome")>
    Public Sub IntegrationTest_Step2()
        Dim solution = Step1.To(Of Solution)()
        Dim sp = New ChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Controller", .MaxChoices = 1}.AddSubParameters("X", "Y", "Z")

        sp.GetScoreManipulator(solution).SetKey("Z")

        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()

        WriteToDebug(solution, "Arrange")

        Dim conceptManipulator = combinedScoringMap.GetConceptManipulator(solution)
        WriteToDebug(solution, "Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(Step2.ToString(), solution.DoSerialize().ToString()))
    End Sub

    ReadOnly Step1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Integratie" scoringMethod="None">
                                             <keyFact id="X-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="Controller" occur="1">
                                                     <stringValue>
                                                         <typedValue>X</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="Integratie" scoringMethod="None">
                                             <conceptFact id="X-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="Controller" occur="1">
                                                     <stringValue>
                                                         <typedValue>X</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="Y[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="Controller" occur="1">
                                                     <stringValue>
                                                         <typedValue>Y</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="Z[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="Controller" occur="1">
                                                     <stringValue>
                                                         <typedValue>Z</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>

    ReadOnly Step2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                     <keyFindings>
                                         <keyFinding id="Integratie" scoringMethod="None">
                                             <keyFact id="Z-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <keyValue domain="Controller" occur="1">
                                                     <stringValue>
                                                         <typedValue>Z</typedValue>
                                                     </stringValue>
                                                 </keyValue>
                                             </keyFact>
                                         </keyFinding>
                                     </keyFindings>
                                     <conceptFindings>
                                         <conceptFinding id="Integratie" scoringMethod="None">
                                             <conceptFact id="Y[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="Controller" occur="1">
                                                     <stringValue>
                                                         <typedValue>Y</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="Z-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="Controller" occur="1">
                                                     <stringValue>
                                                         <typedValue>Z</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                             <conceptFact id="X[1]-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                 <conceptValue domain="Controller" occur="1">
                                                     <stringValue>
                                                         <typedValue>X</typedValue>
                                                     </stringValue>
                                                 </conceptValue>
                                             </conceptFact>
                                         </conceptFinding>
                                     </conceptFindings>
                                     <aspectReferences/>
                                 </solution>

    Private Function ChoiceScoringParameter() As ChoiceScoringParameter
        Return New ChoiceScoringParameter() With {.FindingOverride = "Integratie", .ControllerId = "Controller", .MaxChoices = 1}.AddSubParameters("X", "Y", "Z")
    End Function

End Class
