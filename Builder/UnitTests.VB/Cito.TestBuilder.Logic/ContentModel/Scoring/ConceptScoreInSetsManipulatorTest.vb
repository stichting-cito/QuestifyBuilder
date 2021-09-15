
Imports System.Xml.Linq
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ConceptScoreInSetsManipulatorTest
    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsFrom_exampleNotSynced_Expects_1and2()
        'Arrange
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim conceptIds = manipulator.GetConceptIds().ToList() 'These are set numbers.
        
        'Assert
        Assert.AreEqual(2 + 1, conceptIds.Count) '+ Catch all
        Assert.AreEqual("0", conceptIds(0))
        Assert.AreEqual("1", conceptIds(1))
        Assert.AreEqual("2", conceptIds(2))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFactFrom_set0_exampleNotSynced_Expects_6and14()
        'Arrange
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim displayValue = manipulator.GetDisplayValueForConceptId("0")
        
        'Assert
        Assert.AreEqual("6&14", displayValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFactFrom_set1_exampleNotSynced_Expects_14and6()
        'Arrange
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim displayValue = manipulator.GetDisplayValueForConceptId("1")
        
        'Assert
        Assert.AreEqual("14&6", displayValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub IsConceptIdDeletable_set0_exampleNotSynced_Expects_false()
        'Arrange
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim boolResult1 = manipulator.IsConceptIdDeletable("0")
        
        'Assert
        Assert.IsFalse(boolResult1, "set 0 is part of the Keyfining and thus NOT deletable")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub IsConceptIdDeletable_set1_exampleNotSynced_Expects_false()
        'Arrange
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim boolResult1 = manipulator.IsConceptIdDeletable("0")
        
        'Assert
        Assert.IsFalse(boolResult1, "set 1 is part of the Keyfining and thus NOT deletable")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_set1_exampleNotSynced_Expects_false()
        'Arrange
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()

        Dim inputs = New Dictionary(Of String, Object) From {
           {"ScoreParameter", integerScoringParameter},
           {"Solution", solution}
        }

        WorkflowInvoker.Invoke(New SynchronizeKeyFindingToConceptFindingActivity(), inputs)

        Dim manipulator = ScoringParameterFactory.GetConceptManipulatorBare(combinedScoringMapKey, solution)
        
        'Act
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsFalse(boolResult1, "Catch all was not expected")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_AfterGettingConceptManipulaotr_exampleNotSynced_Expects_true()
        'Arrange
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsTrue(boolResult1, "Catch all was  expected")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub AddCatchAllAnswerCategory_ContainsCatchAllAnswerCategory_set1_exampleNotSynced_Expects_true()
        'Arrange
        Dim solution = exampleNotSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        manipulator.AddCatchAllAnswerCategory()
        Dim boolResult1 = combinedScoringMapKey.GetConceptManipulator(solution).ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsTrue(boolResult1, "Catch all was not expected")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsFrom_exampleWithConceptFindingSynced_Expects_1()
        'Arrange
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim conceptIds = manipulator.GetConceptIds().ToList()
        
        'Assert
        Assert.AreEqual(2 + 1, conceptIds.Count) '+ Catch all
        Assert.AreEqual("0", conceptIds(0))
        Assert.AreEqual("1", conceptIds(1))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFactFrom_set0_exampleWithConceptFindingSynced_Expects_6and14()
        'Arrange
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim displayValue = manipulator.GetDisplayValueForConceptId("0")
        
        'Assert
        Assert.AreEqual("6&14", displayValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFactFrom_set1_exampleWithConceptFindingSynced_Expects_14and6()
        'Arrange
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim displayValue = manipulator.GetDisplayValueForConceptId("1")
        
        'Assert
        Assert.AreEqual("14&6", displayValue)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub IsConceptIdDeletable_set0_exampleWithConceptFindingSynced_Expects_false()
        'Arrange
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim boolResult1 = manipulator.IsConceptIdDeletable("0")
        
        'Assert
        Assert.IsFalse(boolResult1, "set 0 is part of the Keyfining and thus NOT deletable")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub IsConceptIdDeletable_set1_exampleWithConceptFindingSynced_Expects_false()
        'Arrange
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim boolResult1 = manipulator.IsConceptIdDeletable("1")
        
        'Assert
        Assert.IsFalse(boolResult1, "set 1 is part of the Keyfining and thus NOT deletable")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_set1_exampleWithConceptFindingSynced_Expects_false()
        'Arrange
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = ScoringParameterFactory.GetConceptManipulatorBare(combinedScoringMapKey, solution)
        
        'Act
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsFalse(boolResult1, "Catch all was not expected")
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_AfterGettingConceptManipulator_exampleWithConceptFindingSynced_Expects_true()
        'Arrange
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsTrue(boolResult1, "Catch all was expected")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub AddCatchAllAnswerCategory_ContainsCatchAllAnswerCategory_set1_exampleWithConceptFindingSynced_Expects_true()
        'Arrange
        Dim solution = exampleWithConceptFindingSynced.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        manipulator.AddCatchAllAnswerCategory()
        Dim boolResult1 = combinedScoringMapKey.GetConceptManipulator(solution).ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsTrue(boolResult1, "Catch all was not expected")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsFrom_exampleWithAddedConceptFacts_Expects_0_1_2_3_()
        'Arrange
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim conceptIds = manipulator.GetConceptIds().ToList()  'These are fact set numbers
        
        'Assert
        Assert.AreEqual(4 + 1, conceptIds.Count)
        Assert.AreEqual("0", conceptIds(0))
        Assert.AreEqual("1", conceptIds(1))
        Assert.AreEqual("2", conceptIds(2))
        Assert.AreEqual("3", conceptIds(3))
        Assert.AreEqual("4", conceptIds(4))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFactFrom_exampleWithAddedConceptFacts_Expects_6and14_14and6_6and6_14and14()
        'Arrange
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim displayValue1 = manipulator.GetDisplayValueForConceptId("0")
        Dim displayValue2 = manipulator.GetDisplayValueForConceptId("1")
        Dim displayValue3 = manipulator.GetDisplayValueForConceptId("2")
        Dim displayValue4 = manipulator.GetDisplayValueForConceptId("3")
        
        'Assert
        Assert.AreEqual("6&14", displayValue1)
        Assert.AreEqual("14&6", displayValue2)
        Assert.AreEqual("6&6", displayValue3)
        Assert.AreEqual("14&14", displayValue4)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub IsConceptIdDeletable_exampleWithAddedConceptFacts_Expects_false_true_true()
        'Arrange
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim boolResult1 = manipulator.IsConceptIdDeletable("0")
        Dim boolResult2 = manipulator.IsConceptIdDeletable("1")
        Dim boolResult3 = manipulator.IsConceptIdDeletable("2")
        Dim boolResult4 = manipulator.IsConceptIdDeletable("3")
        
        'Assert
        Assert.IsFalse(boolResult1, "0 is part of the Keyfining and thus NOT deletable")
        Assert.IsFalse(boolResult2, "1 is part of the Keyfining and thus NOT deletable")
        Assert.IsTrue(boolResult3, "2 is NOT part of the Keyfining and thus deletable")
        Assert.IsTrue(boolResult4, "3 is NOT part of the Keyfining and thus deletable")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub RemoveConcept_exampleWithAddedConceptFacts()
        'Arrange
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")

        Dim combinedScoringMapKey =
        New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution) 'Note that this will also add a catch all value.

        Dim beforeConceptScoringMapKey =
                New ConceptScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()

        'Act
        manipulator.RemoveConcept("2")
        
        'Assert
        Dim resultCombinedScoringMapKey =
                New ConceptScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()

        Assert.AreEqual(4 + 1, beforeConceptScoringMapKey.SetNumbers.Count(), "Should have started with 4 sets. (+ Catch all)")
        Assert.AreEqual(3 + 1, resultCombinedScoringMapKey.SetNumbers.Count(), "a single set should have been removed. (+ Catch all)")
        'Since factSet id are the numbers of position of factSets, conceptid 3 will become conceptid 2. 
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_set1_exampleWithAddedConceptFacts_Expects_false()
        'Arrange
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = ScoringParameterFactory.GetConceptManipulatorBare(combinedScoringMapKey, solution)
        
        'Act
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsFalse(boolResult1, "Catch all was not expected")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ContainsCatchAllAnswerCategory_GetConceptManipulator_exampleWithAddedConceptFacts_Expects_true()
        'Arrange
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim boolResult1 = manipulator.ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsTrue(boolResult1, "Catch all was expected")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub AddCatchAllAnswerCategory_ContainsCatchAllAnswerCategory_set1_exampleWithAddedConceptFacts_Expects_true()
        'Arrange
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        manipulator.AddCatchAllAnswerCategory()
        Dim boolResult1 = combinedScoringMapKey.GetConceptManipulator(solution).ContainsCatchAllAnswerCategory()
        
        'Assert
        Assert.IsTrue(boolResult1, "Catch all was not expected")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ExampleWithoutPreProcessing_NoneHaveParameterSets()
        'Arrange
        Dim solution = exampleWithAddedConceptFacts.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim result1 = manipulator.HasPreProcessingRules("0")
        Dim result2 = manipulator.HasPreProcessingRules("1")
        Dim result3 = manipulator.HasPreProcessingRules("2")
        Dim result4 = manipulator.HasPreProcessingRules("3")
        Dim result5 = manipulator.HasPreProcessingRules("4")
        
        'Assert
        Assert.IsFalse(result1)
        Assert.IsFalse(result2)
        Assert.IsFalse(result3)
        Assert.IsFalse(result4)
        Assert.IsFalse(result5)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub ExampleWithPreProcessing_4thHaveParameterSets()
        'Arrange
        Dim solution = exampleWithPreprocessingRules.To(Of Solution)()
        Dim integerScoringParameter =
                New IntegerScoringParameter() _
                With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1","2")
        Dim combinedScoringMapKey =
                New ScoringMap(New ScoringParameter() {integerScoringParameter}, solution).GetMap().First()
        Dim manipulator = combinedScoringMapKey.GetConceptManipulator(solution)
        
        'Act
        Dim result1 = manipulator.HasPreProcessingRules("0")
        Dim result2 = manipulator.HasPreProcessingRules("1")
        Dim result3 = manipulator.HasPreProcessingRules("2")
        Dim result4 = manipulator.HasPreProcessingRules("3")
        Dim result5 = manipulator.HasPreProcessingRules("4")
        
        'Assert
        Assert.IsFalse(result1)
        Assert.IsFalse(result2)
        Assert.IsFalse(result3)
        Assert.IsTrue(result4)
        Assert.IsFalse(result5) 'Is catch all.
    End Sub

#Region "Data"

    Private exampleNotSynced As XElement = <solution>
                                               <keyFindings>
                                                   <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                       <keyFactSet>
                                                           <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <keyValue domain="integerScore" occur="1">
                                                                   <integerValue>
                                                                       <typedValue>6</typedValue>
                                                                   </integerValue>
                                                               </keyValue>
                                                           </keyFact>
                                                           <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <keyValue domain="integerScore" occur="1">
                                                                   <integerValue>
                                                                       <typedValue>14</typedValue>
                                                                   </integerValue>
                                                               </keyValue>
                                                           </keyFact>
                                                       </keyFactSet>
                                                       <keyFactSet>
                                                           <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <keyValue domain="integerScore" occur="1">
                                                                   <integerValue>
                                                                       <typedValue>14</typedValue>
                                                                   </integerValue>
                                                               </keyValue>
                                                           </keyFact>
                                                           <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <keyValue domain="integerScore" occur="1">
                                                                   <integerValue>
                                                                       <typedValue>6</typedValue>
                                                                   </integerValue>
                                                               </keyValue>
                                                           </keyFact>
                                                       </keyFactSet>
                                                   </keyFinding>
                                               </keyFindings>
                                               <aspectReferences/>
                                           </solution>

    Private exampleWithConceptFindingSynced As XElement = <solution>
                                                              <keyFindings>
                                                                  <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                      <keyFactSet>
                                                                          <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="integerScore" occur="1">
                                                                                  <integerValue>
                                                                                      <typedValue>6</typedValue>
                                                                                  </integerValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                          <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="integerScore" occur="1">
                                                                                  <integerValue>
                                                                                      <typedValue>14</typedValue>
                                                                                  </integerValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFactSet>
                                                                      <keyFactSet>
                                                                          <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="integerScore" occur="1">
                                                                                  <integerValue>
                                                                                      <typedValue>14</typedValue>
                                                                                  </integerValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                          <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue domain="integerScore" occur="1">
                                                                                  <integerValue>
                                                                                      <typedValue>6</typedValue>
                                                                                  </integerValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFactSet>
                                                                  </keyFinding>
                                                              </keyFindings>
                                                              <conceptFindings>
                                                                  <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                      <conceptFactSet>
                                                                          <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <conceptValue domain="1-integerScore" occur="1">
                                                                                  <integerValue>
                                                                                      <typedValue>6</typedValue>
                                                                                  </integerValue>
                                                                              </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <conceptValue domain="2-integerScore" occur="1">
                                                                                  <integerValue>
                                                                                      <typedValue>14</typedValue>
                                                                                  </integerValue>
                                                                              </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                      </conceptFactSet>
                                                                      <conceptFactSet>
                                                                          <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <conceptValue domain="1-integerScore" occur="1">
                                                                                  <integerValue>
                                                                                      <typedValue>14</typedValue>
                                                                                  </integerValue>
                                                                              </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <conceptValue domain="2-integerScore" occur="1">
                                                                                  <integerValue>
                                                                                      <typedValue>6</typedValue>
                                                                                  </integerValue>
                                                                              </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                      </conceptFactSet>
                                                                  </conceptFinding>
                                                              </conceptFindings>
                                                              <aspectReferences/>
                                                          </solution>

    Private exampleWithAddedConceptFacts As XElement = <solution>
                                                           <keyFindings>
                                                               <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                   <keyFactSet>
                                                                       <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <keyValue domain="integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>6</typedValue>
                                                                               </integerValue>
                                                                           </keyValue>
                                                                       </keyFact>
                                                                       <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <keyValue domain="integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>14</typedValue>
                                                                               </integerValue>
                                                                           </keyValue>
                                                                       </keyFact>
                                                                   </keyFactSet>
                                                                   <keyFactSet>
                                                                       <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <keyValue domain="integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>14</typedValue>
                                                                               </integerValue>
                                                                           </keyValue>
                                                                       </keyFact>
                                                                       <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <keyValue domain="integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>6</typedValue>
                                                                               </integerValue>
                                                                           </keyValue>
                                                                       </keyFact>
                                                                   </keyFactSet>
                                                               </keyFinding>
                                                           </keyFindings>
                                                           <conceptFindings>
                                                               <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                   <conceptFactSet>
                                                                       <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <conceptValue domain="1-integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>6</typedValue>
                                                                               </integerValue>
                                                                           </conceptValue>
                                                                       </conceptFact>
                                                                       <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <conceptValue domain="2-integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>14</typedValue>
                                                                               </integerValue>
                                                                           </conceptValue>
                                                                       </conceptFact>
                                                                       <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                   </conceptFactSet>
                                                                   <conceptFactSet>
                                                                       <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <conceptValue domain="1-integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>14</typedValue>
                                                                               </integerValue>
                                                                           </conceptValue>
                                                                       </conceptFact>
                                                                       <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <conceptValue domain="2-integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>6</typedValue>
                                                                               </integerValue>
                                                                           </conceptValue>
                                                                       </conceptFact>
                                                                       <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                   </conceptFactSet>
                                                                   <conceptFactSet>
                                                                       <conceptFact id="1[2]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <conceptValue domain="1[2]-integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>6</typedValue>
                                                                               </integerValue>
                                                                           </conceptValue>
                                                                       </conceptFact>
                                                                       <conceptFact id="2[2]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <conceptValue domain="2[2]-integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>6</typedValue>
                                                                               </integerValue>
                                                                           </conceptValue>
                                                                       </conceptFact>
                                                                       <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                   </conceptFactSet>
                                                                   <conceptFactSet>
                                                                       <conceptFact id="1[3]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <conceptValue domain="1[3]-integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>14</typedValue>
                                                                               </integerValue>
                                                                           </conceptValue>
                                                                       </conceptFact>
                                                                       <conceptFact id="2[3]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                           <conceptValue domain="2[3]-integerScore" occur="1">
                                                                               <integerValue>
                                                                                   <typedValue>14</typedValue>
                                                                               </integerValue>
                                                                           </conceptValue>
                                                                       </conceptFact>
                                                                       <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                   </conceptFactSet>
                                                               </conceptFinding>
                                                           </conceptFindings>
                                                           <aspectReferences/>
                                                       </solution>

    Private exampleWithPreprocessingRules As XElement = <solution>
                                                            <keyFindings>
                                                                <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                    <keyFactSet>
                                                                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>6</typedValue>
                                                                                </integerValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>14</typedValue>
                                                                                </integerValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                    </keyFactSet>
                                                                    <keyFactSet>
                                                                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>14</typedValue>
                                                                                </integerValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>6</typedValue>
                                                                                </integerValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                    </keyFactSet>
                                                                </keyFinding>
                                                            </keyFindings>
                                                            <conceptFindings>
                                                                <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                    <conceptFactSet>
                                                                        <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="1-integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>6</typedValue>
                                                                                </integerValue>
                                                                            </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="2-integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>14</typedValue>
                                                                                </integerValue>
                                                                            </conceptValue>
                                                                        </conceptFact>
                                                                        <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                    </conceptFactSet>
                                                                    <conceptFactSet>
                                                                        <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="1-integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>14</typedValue>
                                                                                </integerValue>
                                                                            </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="2-integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>6</typedValue>
                                                                                </integerValue>
                                                                            </conceptValue>
                                                                        </conceptFact>
                                                                        <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                    </conceptFactSet>
                                                                    <conceptFactSet>
                                                                        <conceptFact id="1[2]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="1[2]-integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>6</typedValue>
                                                                                </integerValue>
                                                                            </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFact id="2[2]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="2[2]-integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>6</typedValue>
                                                                                </integerValue>
                                                                            </conceptValue>
                                                                        </conceptFact>
                                                                        <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                    </conceptFactSet>
                                                                    <conceptFactSet>
                                                                        <conceptFact id="1[3]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="1[3]-integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>14</typedValue>
                                                                                </integerValue>
                                                                            </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFact id="2[3]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <conceptValue domain="2[3]-integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>14</typedValue>
                                                                                </integerValue>
                                                                                <preprocessingRule>
                                                                                    <ruleName>t</ruleName>
                                                                                </preprocessingRule>
                                                                            </conceptValue>
                                                                        </conceptFact>
                                                                        <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                    </conceptFactSet>
                                                                </conceptFinding>
                                                            </conceptFindings>
                                                            <aspectReferences/>
                                                        </solution>

#End Region

End Class
