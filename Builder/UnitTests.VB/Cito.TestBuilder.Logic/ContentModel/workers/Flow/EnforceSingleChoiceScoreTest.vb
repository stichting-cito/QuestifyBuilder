
Imports System.Xml.Linq
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class EnforceSingleChoiceScoreTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveKeyCFromConcept()
        'Arrange
        Dim solution = MCKeyFindingAndConceptFindingDiffer.To(Of Solution)()
        Dim sp = New ChoiceScoringParameter() With {.MaxChoices = 1, .ControllerId = "mc_1", .FindingOverride = "Opgave"}.AddSubParameters("A", "B", "C")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringMapKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New EnforceSingleChoiceScore(), inputs)
        
        'Assert
        Assert.IsTrue(sp.IsSingleChoice)
        Assert.AreEqual(1, solution.ConceptFindings.First().Facts.Count, "fact c should be deleted")
        Assert.AreEqual("A"c, solution.ConceptFindings.First().Facts.First().Id(0), "fact c should be deleted")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveKeyCFromConcept_AnswerCatagoryB_ShouldNotPoseAnyProblen()
        'Arrange
        Dim solution = MCKeyFindingAndConceptFindingDiffer_HasAnswerCategory.To(Of Solution)()
        Dim sp = New ChoiceScoringParameter() With {.MaxChoices = 1, .ControllerId = "mc_1", .FindingOverride = "Opgave"}.AddSubParameters("A", "B", "C")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringMapKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New EnforceSingleChoiceScore(), inputs)
        
        'Assert
        Assert.IsTrue(sp.IsSingleChoice)
        Assert.AreEqual(2, solution.ConceptFindings.First().Facts.Count, "fact c should be deleted")
        Assert.AreEqual("A"c, solution.ConceptFindings.First().Facts(0).Id(0), "fact c should be deleted")
        Assert.AreEqual("B"c, solution.ConceptFindings.First().Facts(1).Id(0), "fact c should be deleted")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ActivityDoesNotLookAtActualKey()
        'Arrange
        Dim solution = KeyIsA_ButInConceptKeyIs_A_C.To(Of Solution)()
        Dim sp = New ChoiceScoringParameter() With {.MaxChoices = 1, .ControllerId = "mc_1", .FindingOverride = "Opgave"}.AddSubParameters("A", "B", "C")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().First()
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringMapKey", combinedScoringMap}, {"Solution", solution}}
        
        'Act
        WorkflowInvoker.Invoke(New EnforceSingleChoiceScore(), inputs)
        
        'Assert
        Assert.AreEqual("A", DirectCast(DirectCast(solution.Findings.First().Facts.First().Values.First(), KeyValue).Values.First(), StringValue).Value)
        Assert.AreEqual("C", DirectCast(DirectCast(solution.ConceptFindings.First().Facts.First().Values.First(), KeyValue).Values.First(), StringValue).Value)
    End Sub

#Region "Data"

    ReadOnly MCKeyFindingAndConceptFindingDiffer As XElement = <solution>
                                                                   <keyFindings>
                                                                       <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                                           <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <keyValue domain="mc_1" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>A</typedValue>
                                                                                   </stringValue>
                                                                               </keyValue>
                                                                           </keyFact>
                                                                       </keyFinding>
                                                                   </keyFindings>
                                                                   <conceptFindings>
                                                                       <conceptFinding id="Opgave" scoringMethod="None">

                                                                           <conceptFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <conceptValue domain="mc_1" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>C</typedValue>
                                                                                   </stringValue>
                                                                               </conceptValue>
                                                                               <concepts/>
                                                                           </conceptFact>

                                                                           <conceptFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <conceptValue domain="A-mc_1" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>A</typedValue>
                                                                                   </stringValue>
                                                                               </conceptValue>
                                                                               <concepts/>
                                                                           </conceptFact>

                                                                       </conceptFinding>
                                                                   </conceptFindings>
                                                                   <aspectReferences/>
                                                                   <ItemScoreTranslationTable>
                                                                       <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                       <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                   </ItemScoreTranslationTable>
                                                               </solution>

    ReadOnly MCKeyFindingAndConceptFindingDiffer_HasAnswerCategory As XElement = <solution>
                                                                                     <keyFindings>
                                                                                         <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                                                             <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <keyValue domain="mc_1" occur="1">
                                                                                                     <stringValue>
                                                                                                         <typedValue>A</typedValue>
                                                                                                     </stringValue>
                                                                                                 </keyValue>
                                                                                             </keyFact>
                                                                                         </keyFinding>
                                                                                     </keyFindings>
                                                                                     <conceptFindings>
                                                                                         <conceptFinding id="Opgave" scoringMethod="None">

                                                                                             <conceptFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <conceptValue domain="mc_1" occur="1">
                                                                                                     <stringValue>
                                                                                                         <typedValue>C</typedValue>
                                                                                                     </stringValue>
                                                                                                 </conceptValue>
                                                                                                 <concepts/>
                                                                                             </conceptFact>

                                                                                             <conceptFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <conceptValue domain="A-mc_1" occur="1">
                                                                                                     <stringValue>
                                                                                                         <typedValue>A</typedValue>
                                                                                                     </stringValue>
                                                                                                 </conceptValue>
                                                                                                 <concepts/>
                                                                                             </conceptFact>

                                                                                             <conceptFact id="B[1]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <conceptValue domain="B-mc_1" occur="1">
                                                                                                     <stringValue>
                                                                                                         <typedValue>B</typedValue>
                                                                                                     </stringValue>
                                                                                                 </conceptValue>
                                                                                                 <concepts/>
                                                                                             </conceptFact>

                                                                                         </conceptFinding>
                                                                                     </conceptFindings>
                                                                                     <aspectReferences/>
                                                                                     <ItemScoreTranslationTable>
                                                                                         <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                                         <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                                     </ItemScoreTranslationTable>
                                                                                 </solution>

    ReadOnly KeyIsA_ButInConceptKeyIs_A_C As XElement = <solution>
                                                            <keyFindings>
                                                                <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                                    <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <keyValue domain="mc_1" occur="1">
                                                                            <stringValue>
                                                                                <typedValue>A</typedValue>
                                                                            </stringValue>
                                                                        </keyValue>
                                                                    </keyFact>
                                                                </keyFinding>
                                                            </keyFindings>
                                                            <conceptFindings>
                                                                <conceptFinding id="Opgave" scoringMethod="None">

                                                                    <conceptFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <conceptValue domain="A-mc_1" occur="1">
                                                                            <stringValue>
                                                                                <typedValue>A</typedValue>
                                                                            </stringValue>
                                                                        </conceptValue>
                                                                        <concepts/>
                                                                    </conceptFact>

                                                                    <conceptFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <conceptValue domain="mc_1" occur="1">
                                                                            <stringValue>
                                                                                <typedValue>C</typedValue>
                                                                            </stringValue>
                                                                        </conceptValue>
                                                                        <concepts/>
                                                                    </conceptFact>

                                                                </conceptFinding>
                                                            </conceptFindings>
                                                            <aspectReferences/>
                                                            <ItemScoreTranslationTable>
                                                                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                            </ItemScoreTranslationTable>
                                                        </solution>

#End Region

End Class
