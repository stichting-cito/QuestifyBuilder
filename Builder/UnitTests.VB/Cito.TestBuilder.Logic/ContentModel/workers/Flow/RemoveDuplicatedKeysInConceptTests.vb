
Imports System.Xml.Linq
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class RemoveDuplicatedKeysInConceptTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveAnswerCategroy()
        Dim solution = sampleSomthingCanBeRemoved.To(Of Solution)()
        Dim scoringParameter = New ChoiceScoringParameter() With {.FindingOverride = "Opgave", .ControllerId = "mc_1", .MaxChoices = 1}.AddSubParameters("A", "B")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {scoringParameter}, solution).GetMap().First()
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringMapKey", combinedScoringMap}, {"Solution", solution}}

        WorkflowInvoker.Invoke(New RemoveDuplicatedKeysInConcept(), inputs)

        Assert.IsTrue(scoringParameter.IsSingleChoice)
        Assert.AreEqual(1, solution.ConceptFindings.First().Facts.Count, "Since potential double fact has been found, delete it.")
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CannotRemoveAnswerCategroy()
        Dim solution = noChangesRequired.To(Of Solution)()
        Dim scoringParameter = New ChoiceScoringParameter() With {.FindingOverride = "Opgave", .ControllerId = "mc_1", .MaxChoices = 1}.AddSubParameters("A", "B")
        Dim combinedScoringMap = New ScoringMap(New ScoringParameter() {scoringParameter}, solution).GetMap().First()
        Dim inputs As New Dictionary(Of String, Object) From {{"CombinedScoringMapKey", combinedScoringMap}, {"Solution", solution}}

        WorkflowInvoker.Invoke(New RemoveDuplicatedKeysInConcept(), inputs)

        Assert.IsTrue(scoringParameter.IsSingleChoice)
        Assert.AreEqual(2, solution.ConceptFindings.First().Facts.Count, "Nothing to do")
    End Sub



    ReadOnly sampleSomthingCanBeRemoved As XElement = <solution>
                                                          <keyFindings>
                                                              <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                                  <keyFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="mc_1" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>B</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                              </keyFinding>
                                                          </keyFindings>
                                                          <conceptFindings>
                                                              <conceptFinding id="Opgave" scoringMethod="None">
                                                                  <conceptFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <conceptValue domain="A-mc_1" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>B</typedValue>
                                                                          </stringValue>
                                                                      </conceptValue>
                                                                      <concepts/>
                                                                  </conceptFact>
                                                                  <conceptFact id="B[1]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <conceptValue domain="mc_1" occur="1">
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

    ReadOnly noChangesRequired As XElement = <solution>
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
                                                         <conceptFact id="B[1]-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                             <conceptValue domain="mc_1" occur="1">
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


End Class
