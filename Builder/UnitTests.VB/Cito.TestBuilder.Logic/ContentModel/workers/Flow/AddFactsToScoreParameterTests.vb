
Imports System.Activities
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class AddFactsToScoreParameterTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MCSetB_KeysForAANdBAreCreated()
        'Arrange
        Dim solution As New Solution
        Dim choiceSp = New ChoiceScoringParameter() With {.ControllerId = "Control", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        choiceSp.GetScoreManipulator(solution).SetKey("B")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {choiceSp}, solution).GetMap().Single()
        SyncSolution(solution, choiceSp)
        solution.WriteToDebug("Arrange")
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        
        'Assert
        solution.WriteToDebug("Assert")
         Assert.IsTrue(UnitTestHelper.AreSame(expected.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MCSetB_KeysForAANdBAreCreated_MultiRun_StillSameExample()
        'Arrange
        Dim solution As New Solution
        Dim choiceSp = New ChoiceScoringParameter() With {.ControllerId = "Control", .MaxChoices = 1}.AddSubParameters("A", "B", "C")
        choiceSp.GetScoreManipulator(solution).SetKey("B")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {choiceSp}, solution).GetMap().Single()
        SyncSolution(solution, choiceSp)
        solution.WriteToDebug("Arrange")
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        
        'Assert
        solution.WriteToDebug("Assert")
         Assert.IsTrue(UnitTestHelper.AreSame(expected.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MRDeSetA_KeysA_AndInverted_A_Created()
        'Arrange
        Dim solution As New Solution
        Dim choiceSp = New ChoiceScoringParameter() With {.ControllerId = "mr_1", .MaxChoices = 0, .FindingOverride = "Opgave"}.AddSubParameters("A")
        choiceSp.GetScoreManipulator(solution).RemoveKey("A")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {choiceSp}, solution).GetMap().Single()
        SyncSolution(solution, choiceSp)
        solution.WriteToDebug("Arrange")
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        
        'Assert
        solution.WriteToDebug("Assert")
        Assert.IsFalse(choiceSp.IsSingleChoice)
         Assert.IsTrue(UnitTestHelper.AreSame(_mrExpectedFalse.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MRDeSetA_KeysA_AndInverted_A_Created_MultiRun()
        'Arrange
        Dim solution As New Solution
        Dim choiceSp = New ChoiceScoringParameter() With {.ControllerId = "mr_1", .MaxChoices = 0, .FindingOverride = "Opgave"}.AddSubParameters("A")
        choiceSp.GetScoreManipulator(solution).RemoveKey("A")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {choiceSp}, solution).GetMap().Single()
        SyncSolution(solution, choiceSp)
        solution.WriteToDebug("Arrange")
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        
        'Assert
        solution.WriteToDebug("Assert")
        Assert.IsFalse(choiceSp.IsSingleChoice)
        Assert.IsTrue(UnitTestHelper.AreSame(_mrExpectedFalse.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MRSetA_KeysA_AndInverted_A_Created()
        'Arrange
        Dim solution As New Solution
        Dim choiceSp = New ChoiceScoringParameter() With {.ControllerId = "mr_1", .MaxChoices = 0, .FindingOverride = "Opgave"}.AddSubParameters("A")
        choiceSp.GetScoreManipulator(solution).SetKey("A")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {choiceSp}, solution).GetMap().Single()
        SyncSolution(solution, choiceSp)
        solution.WriteToDebug("Arrange")
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        
        'Assert
        solution.WriteToDebug("Assert")
        Assert.IsFalse(choiceSp.IsSingleChoice)
        Assert.IsTrue(UnitTestHelper.AreSame(_mrExpectedTrue.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MRSetA_KeysA_AndInverted_A_Created_MultiRun()
        'Arrange
        Dim solution As New Solution
        Dim choiceSp = New ChoiceScoringParameter() With {.ControllerId = "mr_1", .MaxChoices = 0, .FindingOverride = "Opgave"}.AddSubParameters("A")
        choiceSp.GetScoreManipulator(solution).SetKey("A")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {choiceSp}, solution).GetMap().Single()
        SyncSolution(solution, choiceSp)
        solution.WriteToDebug("Arrange")
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringKey", combinedScoringMap}, {"Solution", solution}}
       
        'Act
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        
        'Assert
        solution.WriteToDebug("Assert")
        Assert.IsFalse(choiceSp.IsSingleChoice)
        Assert.IsTrue(UnitTestHelper.AreSame(_mrExpectedTrue.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub MrGrouped_AddFactsForC_AntwoordWordtGemaaktInFinding()
        'Arrange
        Dim solution = MrABCD_ABGrouped.To(Of Solution)()
        Dim choiceSp = New ChoiceScoringParameter() With {.ControllerId = "mc_1", .MaxChoices = 0, .FindingOverride = "Opgave"}.AddSubParameters("A", "B", "C", "D")

        Dim combinedScoringMaps = New ScoringMap(New ScoringParameter() {choiceSp}, solution).GetMap()
        Dim combinedScoringMap = combinedScoringMaps.Single(Function(scoringMap) scoringMap.Name = "C")

        solution.WriteToDebug("Arrange")

        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New AddFactsToScoreParameter(), inputs)
        
        'Assert
        solution.WriteToDebug("Assert")
        Assert.IsFalse(choiceSp.IsSingleChoice)

        Dim catchAllFact = solution.ConceptFindings.First().Facts.FirstOrDefault(Function(fact) fact.Id = "C[1]-mc_1")
        Assert.IsNotNull(catchAllFact, "CatchAll Fact was expected")
    End Sub
  
#Region "Data"
    ReadOnly expected As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                        <keyFindings>
                                            <keyFinding id="Control" scoringMethod="None">
                                                <keyFact id="B-Control" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <keyValue domain="Control" occur="1">
                                                        <stringValue>
                                                            <typedValue>B</typedValue>
                                                        </stringValue>
                                                    </keyValue>
                                                </keyFact>
                                            </keyFinding>
                                        </keyFindings>
                                        <conceptFindings>
                                            <conceptFinding id="Control" scoringMethod="None">
                                                <conceptFact id="B-Control" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="Control" occur="1">
                                                        <stringValue>
                                                            <typedValue>B</typedValue>
                                                        </stringValue>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="A[1]-Control" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="Control" occur="1">
                                                        <stringValue>
                                                            <typedValue>A</typedValue>
                                                        </stringValue>
                                                    </conceptValue>
                                                </conceptFact>
                                                <conceptFact id="C[1]-Control" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                    <conceptValue domain="Control" occur="1">
                                                        <stringValue>
                                                            <typedValue>C</typedValue>
                                                        </stringValue>
                                                    </conceptValue>
                                                </conceptFact>
                                            </conceptFinding>
                                        </conceptFindings>
                                        <aspectReferences/>
                                    </solution>
    
    ReadOnly _mrExpectedFalse As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                <keyFindings>
                                                    <keyFinding id="Opgave" scoringMethod="None">
                                                        <keyFact id="A-mr_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <keyValue domain="A-mr_1" occur="1">
                                                                <booleanValue>
                                                                    <typedValue>false</typedValue>
                                                                </booleanValue>
                                                            </keyValue>
                                                        </keyFact>
                                                    </keyFinding>
                                                </keyFindings>
                                                <conceptFindings>
                                                    <conceptFinding id="Opgave" scoringMethod="None">
                                                        <conceptFact id="A-mr_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="A-mr_1" occur="1">
                                                                <booleanValue>
                                                                    <typedValue>false</typedValue>
                                                                </booleanValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <conceptFact id="A[1]-mr_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="A[1]-mr_1" occur="1">
                                                                <booleanValue>
                                                                    <typedValue>true</typedValue>
                                                                </booleanValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                    </conceptFinding>
                                                </conceptFindings>
                                                <aspectReferences/>
                                            </solution>

    ReadOnly _mrExpectedTrue As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                               <keyFindings>
                                                   <keyFinding id="Opgave" scoringMethod="None">
                                                       <keyFact id="A-mr_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="A-mr_1" occur="1">
                                                               <booleanValue>
                                                                   <typedValue>true</typedValue>
                                                               </booleanValue>
                                                           </keyValue>
                                                       </keyFact>
                                                   </keyFinding>
                                               </keyFindings>
                                               <conceptFindings>
                                                   <conceptFinding id="Opgave" scoringMethod="None">
                                                       <conceptFact id="A-mr_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <conceptValue domain="A-mr_1" occur="1">
                                                               <booleanValue>
                                                                   <typedValue>true</typedValue>
                                                               </booleanValue>
                                                           </conceptValue>
                                                       </conceptFact>
                                                       <conceptFact id="A[1]-mr_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <conceptValue domain="A[1]-mr_1" occur="1">
                                                               <booleanValue>
                                                                   <typedValue>false</typedValue>
                                                               </booleanValue>
                                                           </conceptValue>
                                                       </conceptFact>
                                                   </conceptFinding>
                                               </conceptFindings>
                                               <aspectReferences/>
                                           </solution>


    ReadOnly MrABCD_ABGrouped As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                <keyFindings>
                                                    <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                        <keyFact id="C-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <keyValue domain="C-mc_1" occur="1">
                                                                <booleanValue>
                                                                    <typedValue>false</typedValue>
                                                                </booleanValue>
                                                            </keyValue>
                                                        </keyFact>
                                                        <keyFact id="D-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <keyValue domain="D-mc_1" occur="1">
                                                                <booleanValue>
                                                                    <typedValue>false</typedValue>
                                                                </booleanValue>
                                                            </keyValue>
                                                        </keyFact>
                                                        <keyFactSet>
                                                            <keyFact id="A-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <keyValue domain="A-mc_1" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </keyValue>
                                                            </keyFact>
                                                            <keyFact id="B-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <keyValue domain="B-mc_1" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </keyValue>
                                                            </keyFact>
                                                        </keyFactSet>
                                                        <keyFactSet>
                                                            <keyFact id="A-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <keyValue domain="A-mc_1" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </keyValue>
                                                            </keyFact>
                                                            <keyFact id="B-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <keyValue domain="B-mc_1" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </keyValue>
                                                            </keyFact>
                                                        </keyFactSet>
                                                    </keyFinding>
                                                </keyFindings>
                                                <conceptFindings>
                                                    <conceptFinding id="Opgave" scoringMethod="None">
                                                        <conceptFact id="C-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="C-mc_1" occur="1">
                                                                <booleanValue>
                                                                    <typedValue>false</typedValue>
                                                                </booleanValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <conceptFact id="D-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <conceptValue domain="D-mc_1" occur="1">
                                                                <booleanValue>
                                                                    <typedValue>false</typedValue>
                                                                </booleanValue>
                                                            </conceptValue>
                                                        </conceptFact>
                                                        <conceptFactSet>
                                                            <conceptFact id="A-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="A-mc_1" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </conceptValue>
                                                            </conceptFact>
                                                            <conceptFact id="B-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="B-mc_1" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </conceptValue>
                                                            </conceptFact>
                                                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                        </conceptFactSet>
                                                        <conceptFactSet>
                                                            <conceptFact id="A-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="A-mc_1" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </conceptValue>
                                                            </conceptFact>
                                                            <conceptFact id="B-mc_1" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="B-mc_1" occur="1">
                                                                    <booleanValue>
                                                                        <typedValue>false</typedValue>
                                                                    </booleanValue>
                                                                </conceptValue>
                                                            </conceptFact>
                                                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                        </conceptFactSet>
                                                        <conceptFactSet>
                                                            <conceptFact id="A[*]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="A[*]-mc_1" occur="1">
                                                                    <catchAllValue/>
                                                                </conceptValue>
                                                            </conceptFact>
                                                            <conceptFact id="B[*]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <conceptValue domain="B[*]-mc_1" occur="1">
                                                                    <catchAllValue/>
                                                                </conceptValue>
                                                            </conceptFact>
                                                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                        </conceptFactSet>
                                                    </conceptFinding>
                                                </conceptFindings>
                                                <aspectReferences/>
                                            </solution>

#End Region

    Private Sub SyncSolution(ByVal solution As Solution, ByVal scoringParameter As ScoringParameter)
        Dim inputs As New Dictionary(Of String, Object) From {{"ScoreParameter", scoringParameter}, {"Solution", solution}}
        WorkflowInvoker.Invoke(New SynchronizeKeyFindingToConceptFindingActivity(), inputs)
    End Sub

End Class
