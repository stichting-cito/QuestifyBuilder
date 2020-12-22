
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ConceptScoreInFindingManipulatorTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsFrom_exampleNotSynced_Expects_1()
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim conceptIds = manipulator.GetConceptIds().ToList()

        Assert.AreEqual(1 + 1, conceptIds.Count)
        Assert.AreEqual("1", conceptIds.First())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFactFrom_exampleNotSynced_Expects_6()
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim displayValue = manipulator.GetDisplayValueForConceptId("1")

        Assert.AreEqual("6", displayValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub IsConceptIdDeletable_exampleNotSynced_Expects_false()
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim boolResult1 = manipulator.IsConceptIdDeletable("1")

        Assert.IsFalse(boolResult1, "1 is part of the Keyfining and thus NOT deletable")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_exampleNotSynced_Expects_true()
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()

        Assert.IsTrue(boolResult1, "Catch All Should be present ")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub AddCatchAllAnswerCategory_ContainsCatchAllAnswerCategory_exampleNotSynced_Expects_true()
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        manipulator.AddCatchAllAnswerCategory()
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()

        Assert.IsTrue(boolResult1, "Catch All Should be present ")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsFrom_exampleWithConceptFindingSynced_Expects_1()
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim conceptIds = manipulator.GetConceptIds().ToList()

        Assert.AreEqual(1 + 1, conceptIds.Count, "Expected a single concept id + a catch all id.")
        Assert.AreEqual("1", conceptIds.First())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFactFrom_exampleWithConceptFindingSynced_Expects_6()
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim displayValue = manipulator.GetDisplayValueForConceptId("1")

        Assert.AreEqual("6", displayValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub IsConceptIdDeletable_exampleWithConceptFindingSynced_Expects_false()
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim boolResult1 = manipulator.IsConceptIdDeletable("1")

        Assert.IsFalse(boolResult1, "1 is part of the Keyfining and thus NOT deletable")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_exampleWithConceptFindingSynced_Expects_true()
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()

        Assert.IsTrue(boolResult1, "Catch All Should be present ")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub AddCatchAllAnswerCategory_ContainsCatchAllAnswerCategory_exampleWithConceptFindingSynced_Expects_true()
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        manipulator.AddCatchAllAnswerCategory()
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()

        Assert.IsTrue(boolResult1, "Catch All Should be present ")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsFrom_exampleWithAddedConceptFacts_Expects_1_11_12()
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim conceptIds = manipulator.GetConceptIds().ToList()

        Assert.AreEqual(3 + 1, conceptIds.Count)
        Assert.AreEqual("1", conceptIds(0))
        Assert.AreEqual("1[1]", conceptIds(1))
        Assert.AreEqual("1[2]", conceptIds(2))
        Assert.AreEqual("1[*]", conceptIds(3))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFactFrom_exampleWithAddedConceptFacts_Expects_6_8_9()
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim displayValue1 = manipulator.GetDisplayValueForConceptId("1")
        Dim displayValue2 = manipulator.GetDisplayValueForConceptId("1[1]")
        Dim displayValue3 = manipulator.GetDisplayValueForConceptId("1[2]")

        Assert.AreEqual("6", displayValue1)
        Assert.AreEqual("8", displayValue2)
        Assert.AreEqual("9", displayValue3)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub IsConceptIdDeletable_exampleWithAddedConceptFacts_Expects_false_true_true()
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim boolResult1 = manipulator.IsConceptIdDeletable("1")
        Dim boolResult2 = manipulator.IsConceptIdDeletable("1[1]")
        Dim boolResult3 = manipulator.IsConceptIdDeletable("1[2]")

        Assert.IsFalse(boolResult1, "1 is part of the Keyfining and thus NOT deletable")
        Assert.IsTrue(boolResult2, "1[1] is NOT part of the Keyfining and thus deletable")
        Assert.IsTrue(boolResult3, "1[2] is NOT part of the Keyfining and thus deletable")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub RemoveConcept_exampleWithAddedConceptFacts()
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1",
                                                                                                                  "2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim beforeIds = combinedScoringMapKey.GetConceptManipulator(solution).GetConceptIds().ToList()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        manipulator.RemoveConcept("1[1]")

        Dim resultIds = combinedScoringMapKey.GetConceptManipulator(solution).GetConceptIds().ToList()

        Assert.AreEqual(3 + 1, beforeIds.Count(), "Before state expected 3 scoringMapKeys (+1 Catch all)")
        Assert.AreEqual(2 + 1, resultIds.Count(), "after state expected 2 scoringMapKeys (+1 Catch all)")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_exampleWithAddedConceptFacts_Expects_true()
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()

        Assert.IsTrue(boolResult1, "Catch All Should be present ")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub AddCatchAllAnswerCategory_ContainsCatchAllAnswerCategory_exampleWithAddedConceptFacts_Expects_true()
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        manipulator.AddCatchAllAnswerCategory()
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()

        Assert.IsTrue(boolResult1, "Catch All Should be present ")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsForMR_4SubPrms_ShouldReturn_2()
        Dim solution = New Solution()
        Dim mrParameter = New MultiChoiceScoringParameter() With {.ControllerId = "MRScore", .FindingOverride = "sharedFinding", .MaxChoices = 4}.AddSubParameters("1", "2", "3", "4")

        mrParameter.GetScoreManipulator(solution).SetKey("1")
        mrParameter.GetScoreManipulator(solution).RemoveKey("2")
        mrParameter.GetScoreManipulator(solution).RemoveKey("3")
        mrParameter.GetScoreManipulator(solution).SetKey("4")
        Dim combinedScoringMapKey = New ScoringMap(New ScoringParameter() {mrParameter}, solution).GetMap().First()

        solution.WriteToDebug("Arrange")

        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        Dim ids = manipulator.GetConceptIds()

        solution.WriteToDebug("Assert")
        Assert.IsFalse(mrParameter.IsSingleChoice)
        Assert.AreEqual(2, ids.Count())
        Assert.AreEqual("1", ids(0))
        Assert.AreEqual("1[1]", ids(1))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsForMC_4SubPrms_ShouldReturn_1()
        Dim solution = New Solution()
        Dim mcParameter = New MultiChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "sharedFinding", .MaxChoices = 1}.AddSubParameters("1", "2", "3", "4")

        mcParameter.GetScoreManipulator(solution).SetKey("1")
        mcParameter.GetScoreManipulator(solution).RemoveKey("2")
        mcParameter.GetScoreManipulator(solution).RemoveKey("3")
        mcParameter.GetScoreManipulator(solution).SetKey("4")
        Dim combinedScoringMapKey = New ScoringMap(New ScoringParameter() {mcParameter}, solution).GetMap().First()

        solution.WriteToDebug("Arrange")

        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        Dim ids = manipulator.GetConceptIds()

        solution.WriteToDebug("Assert")
        Assert.IsTrue(mcParameter.IsSingleChoice)
        Assert.AreEqual(4, ids.Count())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ScoreHasNoPreprocessingRules_PropertyShouldReturnFalse()
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim boolResult1 = manipulator.HasPreProcessingRules("1[1]")

        Assert.IsFalse(boolResult1, "No Preprocessing rule should be present.")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ScoreHasPreProcessingRules_PropertyShouldReturnTrue()
        Dim solution = exampleWithPreProcessingRules.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)

        Dim boolResult1 = manipulator.HasPreProcessingRules("1[1]")

        Assert.IsTrue(boolResult1, "No Preprocessing rule should be present.")
    End Sub


    Private exampleNotSynced As XElement = <solution>
                                               <keyFindings>
                                                   <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                       <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <keyValue domain="integerScore" occur="1">
                                                               <integerValue>
                                                                   <typedValue>6</typedValue>
                                                               </integerValue>
                                                           </keyValue>
                                                       </keyFact>
                                                   </keyFinding>
                                               </keyFindings>
                                               <aspectReferences/>
                                           </solution>

    Private exampleWithConceptFindingSynced As XElement = <solution>
                                                              <keyFindings>
                                                                  <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <keyValue domain="integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                          </keyValue>
                                                                      </keyFact>
                                                                  </keyFinding>
                                                              </keyFindings>
                                                              <conceptFindings>
                                                                  <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                      <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <conceptValue domain="1-integerScore" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                          </conceptValue>
                                                                          <concepts/>
                                                                      </conceptFact>
                                                                  </conceptFinding>
                                                              </conceptFindings>
                                                              <aspectReferences/>
                                                          </solution>

    Private exampleWithAddedConceptFacts As XElement = <solution>
                                                           <keyFindings>
                                                               <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                   <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="integerScore" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>6</typedValue>
                                                                           </integerValue>
                                                                       </keyValue>
                                                                   </keyFact>
                                                               </keyFinding>
                                                           </keyFindings>
                                                           <conceptFindings>
                                                               <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                   <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <conceptValue domain="1-integerScore" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>6</typedValue>
                                                                           </integerValue>
                                                                       </conceptValue>
                                                                       <concepts/>
                                                                   </conceptFact>
                                                                   <conceptFact id="1[1]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <conceptValue domain="1[1]-integerScore" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>8</typedValue>
                                                                           </integerValue>
                                                                       </conceptValue>
                                                                       <concepts/>
                                                                   </conceptFact>
                                                                   <conceptFact id="1[2]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <conceptValue domain="1[2]-integerScore" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>9</typedValue>
                                                                           </integerValue>
                                                                       </conceptValue>
                                                                       <concepts/>
                                                                   </conceptFact>
                                                               </conceptFinding>
                                                           </conceptFindings>
                                                           <aspectReferences/>
                                                       </solution>

    Private exampleWithPreProcessingRules As XElement = <solution>
                                                            <keyFindings>
                                                                <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                    <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <keyValue domain="integerScore" occur="1">
                                                                            <integerValue>
                                                                                <typedValue>6</typedValue>
                                                                            </integerValue>
                                                                        </keyValue>
                                                                    </keyFact>
                                                                </keyFinding>
                                                            </keyFindings>
                                                            <conceptFindings>
                                                                <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                    <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <conceptValue domain="1-integerScore" occur="1">
                                                                            <integerValue>
                                                                                <typedValue>42</typedValue>
                                                                            </integerValue>
                                                                        </conceptValue>
                                                                        <concepts/>
                                                                    </conceptFact>
                                                                    <conceptFact id="1[1]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <conceptValue domain="1[1]-integerScore" occur="1">
                                                                            <integerValue>
                                                                                <typedValue>8</typedValue>
                                                                            </integerValue>
                                                                            <preprocessingRule>
                                                                                <ruleName>t</ruleName>
                                                                            </preprocessingRule>
                                                                        </conceptValue>
                                                                        <concepts/>
                                                                    </conceptFact>
                                                                    <conceptFact id="1[2]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <conceptValue domain="1[2]-integerScore" occur="1">
                                                                            <integerValue>
                                                                                <typedValue>9</typedValue>
                                                                            </integerValue>
                                                                        </conceptValue>
                                                                        <concepts/>
                                                                    </conceptFact>
                                                                </conceptFinding>
                                                            </conceptFindings>
                                                            <aspectReferences/>
                                                        </solution>


End Class