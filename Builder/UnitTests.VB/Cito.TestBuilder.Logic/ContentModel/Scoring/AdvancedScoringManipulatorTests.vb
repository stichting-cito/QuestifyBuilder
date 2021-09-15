
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports System.IO
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Diagnostics
Imports System.Linq
Imports Questify.Builder.Logic
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class AdvancedScoringManipulatorTests
    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SetKey_InExistingSolutionWithKeyFactSet_WillSetKeyFact_In_FirstKeyFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_withFactSets)

        'CRITICAL!! MUST HAVE KEYFACTSETS WITH PRE EXISTING FACT
        Dim fieldA = New IntegerScoringParameter() With {.ControllerId = "FieldA", .FindingOverride = "finding"}.AddSubParameters("A")

        'Act
        Dim manipulator = fieldA.GetScoreManipulator(solution)

        manipulator.ReplaceKeyValueAt("A", 12, 0) _
        'THIS SHOULD AUTOMATICLY NOTICE THE FACT SETS AND TRY TO SET SCORE THERE.

        'Assert 
        WriteSolution("assert", solution)
        Dim keyVal = DirectCast(solution.Findings(0).KeyFactsets(0).Facts(0).Values(0), KeyValue)
        Assert.IsTrue(keyVal.Values(0).IsMatch(New IntegerValue(12)))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetKey_InExistingSolutionWithKeyFactSet_WillSetKeyFact_In_FirstKeyFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_withFactSets)

        'CRITICAL!! MUST HAVE KEYFACTSETS WITH PRE EXISTING FACT
        Dim fieldA = New IntegerScoringParameter() With {.ControllerId = "FieldA", .FindingOverride = "finding"}.AddSubParameters("A")

        'Act
        Dim manipulator = fieldA.GetScoreManipulator(solution)

        Dim result = manipulator.GetKeyStatus()

        'Assert 
        WriteSolution("assert", solution)

        Assert.AreEqual(result("A").First().Value.IntegerValue, 3)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetFactSetNumbers_WillReturnCorrectNumbers()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_withFactSets)

        Dim fieldA = New IntegerScoringParameter() With {.ControllerId = "FieldA", .FindingOverride = "finding"}.AddSubParameters("A")
        Dim manipulatorA = fieldA.GetScoreManipulator(solution)

        Dim fieldB = New IntegerScoringParameter() With {.ControllerId = "FieldB", .FindingOverride = "finding"}.AddSubParameters("A")
        Dim manipulatorB = fieldB.GetScoreManipulator(solution)

        Dim fieldC = New IntegerScoringParameter() With {.ControllerId = "FieldC", .FindingOverride = "finding"}.AddSubParameters("A")
        Dim manipulatorC = fieldC.GetScoreManipulator(solution)

        'Act
        Dim factSetNumbersA As IEnumerable(Of Integer) = manipulatorA.GetFactSetNumbers("A")
        Dim factSetNumbersB As IEnumerable(Of Integer) = manipulatorB.GetFactSetNumbers("A")
        Dim factSetNumbersC As IEnumerable(Of Integer) = manipulatorC.GetFactSetNumbers("A")

        'Assert 
        Assert.AreEqual(3, factSetNumbersA.Count)
        Assert.AreEqual(0, factSetNumbersA(0))
        Assert.AreEqual(1, factSetNumbersA(1))
        Assert.AreEqual(2, factSetNumbersA(2))

        Assert.AreEqual(2, factSetNumbersB.Count)
        Assert.AreEqual(0, factSetNumbersB(0))
        Assert.AreEqual(1, factSetNumbersB(1))

        Assert.AreEqual(1, factSetNumbersC.Count)
        Assert.AreEqual(2, factSetNumbersC(0))
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetFactSetNumbers_WillReturnExpectedNumbers()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolutionSameParameter)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim manipulatorA = integerScore.GetScoreManipulator(solution)

        'Act
        Dim numbers1 = manipulatorA.GetFactSetNumbers("1")
        Dim numbers2 = manipulatorA.GetFactSetNumbers("2")
        Dim numbers3 = manipulatorA.GetFactSetNumbers("3")

        'Assert
        Assert.AreEqual(2, numbers1.Count)
        Assert.AreEqual(0, numbers1(0))
        Assert.AreEqual(1, numbers1(1))

        Assert.AreEqual(2, numbers2.Count)
        Assert.AreEqual(0, numbers2(0))
        Assert.AreEqual(1, numbers2(1))

        Assert.AreEqual(0, numbers3.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GroupInteractions_SameInteraction()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_noFactSets_SameInteraction)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding", .Label = "Antwoord"}.AddSubParameters("1", "2")

        Dim manipulatorA = integerScore.GetScoreManipulator(solution)
        Dim factSetNumbersBeforeGroup = manipulatorA.GetFactSetNumbers("1")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(integerScore, "1"))
        interactions.Add(New ScoringMapKey(integerScore, "2"))

        'Act
        factTargetManipulator.GroupInteractions(interactions)

        WriteSolution("after group", solution)

        'Assert
        Dim factSetNumbersAfterGroup = manipulatorA.GetFactSetNumbers("1")
        Assert.AreEqual(0, factSetNumbersBeforeGroup.Count)
        Assert.AreEqual(1, factSetNumbersAfterGroup.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GroupInteractions_Integer_MultiChoice()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_noFactSets_Integer_MultiChoice)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding", .Label = "Antwoord"}.AddSubParameters("1")
        Dim choiceScore = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .Label = "MC", .MaxChoices = 1}.AddSubParameters("B")

        Dim manipulatorA = integerScore.GetScoreManipulator(solution)
        Dim factSetNumbersBeforeGroup = manipulatorA.GetFactSetNumbers("1")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(integerScore, "1"))
        interactions.Add(New ScoringMapKey(choiceScore, "B"))

        'Act
        factTargetManipulator.GroupInteractions(interactions)

        WriteSolution("after group", solution)

        'Assert
        Dim factSetNumbersAfterGroup = manipulatorA.GetFactSetNumbers("1")
        Assert.AreEqual(0, factSetNumbersBeforeGroup.Count)
        Assert.AreEqual(1, factSetNumbersAfterGroup.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GroupInteractions_Integer_MultiChoice_EmptyFinding()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_noFactSets_Integer_MultiChoice)
        solution.Findings(0).Facts.Clear()
        solution.Findings(0).KeyFactsets.Clear()

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding", .Label = "Antwoord"}.AddSubParameters("1")
        Dim choiceScore = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .Label = "MC", .MaxChoices = 1}.AddSubParameters("B")

        Dim manipulatorA = integerScore.GetScoreManipulator(solution)
        Dim factSetNumbersBeforeGroup = manipulatorA.GetFactSetNumbers("1")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(integerScore, "1"))
        interactions.Add(New ScoringMapKey(choiceScore, "B"))

        'Act
        factTargetManipulator.GroupInteractions(interactions)

        WriteSolution("after group", solution)

        'Assert
        Dim factSetNumbersAfterGroup = manipulatorA.GetFactSetNumbers("1")
        Assert.AreEqual(0, factSetNumbersBeforeGroup.Count)
        Assert.AreEqual(1, factSetNumbersAfterGroup.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GroupInteractions_MultiChoice_EmptyFinding()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_emptySolution)
        solution.Findings(0).Facts.Clear()
        solution.Findings(0).KeyFactsets.Clear()

        Dim choiceScore1 = New ChoiceScoringParameter() With {.ControllerId = "McScore1", .FindingOverride = "DefaultFinding", .Label = "MC1", .MaxChoices = 1}.AddSubParameters("A", "B")
        Dim choiceScore2 = New ChoiceScoringParameter() With {.ControllerId = "McScore2", .FindingOverride = "DefaultFinding", .Label = "MC2", .MaxChoices = 1}.AddSubParameters("A", "B")

        Dim manipulator1 = choiceScore1.GetScoreManipulator(solution)
        Dim factSetNumbersBeforeGroup = manipulator1.GetFactSetNumbers("A")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(choiceScore1, String.Empty))
        interactions.Add(New ScoringMapKey(choiceScore2, String.Empty))

        'Act
        factTargetManipulator.GroupInteractions(interactions)
        Dim inputs As New Dictionary(Of String, Object) From {{"Solution", solution}, {"ScoringParameters", {choiceScore1, choiceScore2}}}
        System.Activities.WorkflowInvoker.Invoke(New SolutionCleaner(), inputs)

        WriteSolution("after group", solution)

        'Assert
        Dim factSetNumbersAfterGroup = manipulator1.GetFactSetNumbers("A")
        Assert.AreEqual(0, factSetNumbersBeforeGroup.Count)
        Assert.AreEqual(0, factSetNumbersAfterGroup.Count)

    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub AddToGroup_InteractionIsInFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolutionSameParameter)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim manipulatorA = integerScore.GetScoreManipulator(solution)

        Dim factSetNumbersBeforeGroup = manipulatorA.GetFactSetNumbers("3")

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim factSetNumbers = New List(Of Integer)()
        factSetNumbers.AddRange({0, 1})

        'Act
        factTargetManipulator.AddToGroup(New ScoringMapKey(integerScore, "3"), factSetNumbers)

        WriteSolution("after add", solution)

        'Assert
        Assert.AreEqual(0, factSetNumbersBeforeGroup.Count)
        Dim factSetNumbersAfterGroup = manipulatorA.GetFactSetNumbers("3")
        Assert.AreEqual(2, factSetNumbersAfterGroup.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub AddToGroup_GapMatch()
        'Arrange
        Dim item = Deserialize(Of AssessmentItem)(_gapMatchItem)
        Dim parameters = item.Parameters.DeepFetchInlineScoringParameters()
        Dim param = parameters.OfType(Of GapMatchScoringParameter).First()
        Dim manipulator = param.GetScoreManipulator(item.Solution)
        Dim factSetNumbersBeforeGroup = manipulator.GetFactSetNumbers("I4a2df403-47ee-4c5a-a10e-af9dec4fa46a")
        Dim factTargetManipulator = New FactTargetManipulator(item.Solution)
        Dim factSetNumbers = New List(Of Integer)()
        factSetNumbers.Add(0)

        'Act
        factTargetManipulator.AddToGroup(New ScoringMapKey(param, "I4a2df403-47ee-4c5a-a10e-af9dec4fa46a"), factSetNumbers)

        WriteSolution("after add", item.Solution)

        'Assert
        Assert.AreEqual(0, factSetNumbersBeforeGroup.Count)
        Dim factSetNumbersAfterGroup = manipulator.GetFactSetNumbers("I4a2df403-47ee-4c5a-a10e-af9dec4fa46a")
        Assert.AreEqual(1, factSetNumbersAfterGroup.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub AddToGroup_FirstKeyInFactSetKeepsValues_OtherKeysGetDefaultValues()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolutionSameParameter)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim manipulatorA = integerScore.GetScoreManipulator(solution)

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim factSetNumbers = New List(Of Integer)()
        factSetNumbers.AddRange({0, 1})

        'Act
        factTargetManipulator.AddToGroup(New ScoringMapKey(integerScore, "3"), factSetNumbers)

        WriteSolution("after add", solution)

        'Assert
        Dim factSetNumbersAfterGroup = manipulatorA.GetFactSetNumbers("3")
        Assert.AreEqual(2, factSetNumbersAfterGroup.Count())
        manipulatorA.SetFactSetTarget(factSetNumbersAfterGroup(0))
        Assert.AreEqual(21, manipulatorA.GetKeyStatus("3").First().Value.IntegerValue)

        manipulatorA.SetFactSetTarget(factSetNumbers(1))

        Assert.IsFalse(manipulatorA.GetKeyStatus("3").First().Value.IntegerValue.HasValue)
        Assert.IsNull(manipulatorA.GetKeyStatus("3").First().Value.IntegerValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub AddToGroup_GapChoiceCombi_FirstKeyInFactSetKeepsValues_OtherKeysGetDefaultValues()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolution)

        Dim choice = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MaxChoices = 1}.AddSubParameters("A", "B")

        Dim manipulatorA = choice.GetScoreManipulator(solution)

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim factSetNumbers = New List(Of Integer)()
        factSetNumbers.AddRange({0, 1})

        'Act
        factTargetManipulator.AddToGroup(New ScoringMapKey(choice, "A"), factSetNumbers)

        WriteSolution("after add", solution)

        'Assert
        Dim factSetNumbersAfterGroup = manipulatorA.GetFactSetNumbers("A")

        manipulatorA.SetFactSetTarget(factSetNumbersAfterGroup(0))
        Assert.IsTrue(manipulatorA.GetKeyStatus("A"))

        manipulatorA.SetFactSetTarget(factSetNumbersAfterGroup(1))
        Assert.IsTrue(manipulatorA.GetKeyStatus("A"))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub RemoveFromGroup()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_3Facts2Sets)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim manipulatorA = integerScore.GetScoreManipulator(solution)

        Dim factSetNumbersBeforeRemove = manipulatorA.GetFactSetNumbers("3")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMapKey = New ScoringMapKey(integerScore, "3")
        Dim scoringMap = New ScoringMap({integerScore}, solution).GetMap()

        'Act
        factTargetManipulator.RemoveFromGroup(scoringMapKey, scoringMap)

        WriteSolution("after remove", solution)

        'Assert
        Assert.IsNotNull(factSetNumbersBeforeRemove)
        Assert.AreEqual(2, factSetNumbersBeforeRemove.Count)

        Dim factSetNumbersAfterRemove = manipulatorA.GetFactSetNumbers("3")
        Assert.AreEqual(0, factSetNumbersAfterRemove.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub RemoveFromGroup_GapMatch()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_gapMatch)
        Dim gapMatchScoringParameter = New GapMatchScoringParameter() With {.FindingOverride = "gapMatchController"}
        gapMatchScoringParameter.AddSubParameters("I214363b6-f877-4175-95ec-28042ac1b5a9", "I68a92575-2ae9-4cfc-8ce2-284844f72fa9", "I57c72f50-cf3a-499f-9473-3bd398a13282")
        Dim manipulator = gapMatchScoringParameter.GetScoreManipulator(solution)
        Dim factSetNumbersBeforeUnGroup = manipulator.GetFactSetNumbers("I68a92575-2ae9-4cfc-8ce2-284844f72fa9")
        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMapKey = New ScoringMapKey(gapMatchScoringParameter, "I68a92575-2ae9-4cfc-8ce2-284844f72fa9")
        Dim scoringMap = New ScoringMap({gapMatchScoringParameter}, solution).GetMap()

        'Act
        factTargetManipulator.RemoveFromGroup(scoringMapKey, scoringMap)

        WriteSolution("after add", solution)

        'Assert
        Assert.AreEqual(2, factSetNumbersBeforeUnGroup.Count)
        Dim factSetNumbersAfterUnGroup = manipulator.GetFactSetNumbers("I68a92575-2ae9-4cfc-8ce2-284844f72fa9")
        Assert.AreEqual(0, factSetNumbersAfterUnGroup.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub RemoveFromGroup_IntegerMultiChoiceCombi()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolution_MultiChoice_Integer)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2")
        Dim choiceScore = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MaxChoices = 1}.AddSubParameters("A", "B")

        Dim manipulatorA = choiceScore.GetScoreManipulator(solution)

        Dim factSetNumbersBeforeRemove = manipulatorA.GetFactSetNumbers(String.Empty)

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMapKey = New ScoringMapKey(choiceScore, "B")
        Dim scoringMap = New ScoringMap({integerScore, choiceScore}, solution).GetMap()

        WriteSolution("Arrange", solution)
        
        'Act
        factTargetManipulator.RemoveFromGroup(scoringMapKey, scoringMap)

        WriteSolution("after remove", solution)

        'Assert
        Assert.IsNotNull(factSetNumbersBeforeRemove)
        Assert.AreEqual(2, factSetNumbersBeforeRemove.Count)

        Dim factSetNumbersAfterRemove = manipulatorA.GetFactSetNumbers("B")
        Assert.AreEqual(0, factSetNumbersAfterRemove.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub RemoveFromGroup_IntegerMultiChoiceCombi_UnGroupsRemainingInteraction()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolution_MultiChoice_Integer)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2")
        Dim choiceScore = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MaxChoices = 1}.AddSubParameters("A", "B")

        Dim manipulatorA = choiceScore.GetScoreManipulator(solution)

        Dim factSetNumbersBeforeRemove = manipulatorA.GetFactSetNumbers(String.Empty)

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim integerScore1 = New ScoringMapKey(integerScore, "1")
        Dim integerScore2 = New ScoringMapKey(integerScore, "2")
        Dim scoringMap = New ScoringMap({integerScore, choiceScore}, solution).GetMap()

        solution.WriteToDebug("Assert")
        
        'Act
        factTargetManipulator.RemoveFromGroup(integerScore1, scoringMap)
        scoringMap = New ScoringMap({integerScore, choiceScore}, solution).GetMap()

        solution.WriteToDebug("Act before remove IntegerScore 2")

        factTargetManipulator.RemoveFromGroup(integerScore2, scoringMap)

        WriteSolution("after remove", solution)

        'Assert
        Assert.IsNotNull(factSetNumbersBeforeRemove)
        Assert.AreEqual(2, factSetNumbersBeforeRemove.Count)

        Dim factSetNumbersAfterRemove = manipulatorA.GetFactSetNumbers(String.Empty)
        Assert.AreEqual(0, factSetNumbersAfterRemove.Count)
        Assert.AreEqual("A", String.Join("", manipulatorA.GetKeysAlreadyManipulated))

    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub AddFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_3Facts2Sets)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim manipulator = integerScore.GetScoreManipulator(solution)
        Dim factSetNumbersBeforeAdd = manipulator.GetFactSetNumbers("3")
        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = CombinedScoringMapKey.Create(New ScoringMapKey(integerScore, "1"),
                                                        New ScoringMapKey(integerScore, "2"),
                                                        New ScoringMapKey(integerScore, "3"))

        'Act
        Dim newFactSetNumber = factTargetManipulator.AddFactSet(interactions)
        manipulator.SetFactSetTarget(newFactSetNumber)
        manipulator.SetKey("1", 1)

        'Assert
        Assert.IsNotNull(factSetNumbersBeforeAdd)
        Assert.AreEqual(2, factSetNumbersBeforeAdd.Count)
        Assert.IsFalse(factSetNumbersBeforeAdd.Contains(newFactSetNumber))

        Dim factSetNumbersAfterAdd = manipulator.GetFactSetNumbers("1")
        Assert.AreEqual(3, factSetNumbersAfterAdd.Count)
        Assert.IsTrue(factSetNumbersAfterAdd.Contains(newFactSetNumber))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub AddFactSet_MultipleResponse()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_multipleResponse)

        Dim mrScore = New MultiChoiceScoringParameter() With {.ControllerId = "mc_1", .FindingOverride = "Opgave"}.AddSubParameters("A", "B")

        Dim manipulator = mrScore.GetScoreManipulator(solution)
        Dim factSetNumbersBeforeAdd = manipulator.GetFactSetNumbers("A")
        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = CombinedScoringMapKey.Create(New ScoringMapKey(mrScore, "A"),
                                                        New ScoringMapKey(mrScore, "B"))

        'Act
        Dim newFactSetNumber = factTargetManipulator.AddFactSet(interactions)

        'Assert
        Assert.IsNotNull(factSetNumbersBeforeAdd)
        Assert.AreEqual(1, factSetNumbersBeforeAdd.Count)
        Assert.IsFalse(factSetNumbersBeforeAdd.Contains(newFactSetNumber))

        Dim factSetNumbersAfterAdd = manipulator.GetFactSetNumbers("A")
        Assert.AreEqual(2, factSetNumbersAfterAdd.Count)
        Assert.IsTrue(factSetNumbersAfterAdd.Contains(newFactSetNumber))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub Ungroup_MultipleResponse_ShouldNot_Result_In_Duplicate_boolean_values()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_groupedMultipleResponse)

        Dim mrScore = New MultiChoiceScoringParameter() With {.ControllerId = "mc_1", .FindingOverride = "Opgave"}.AddSubParameters("A", "B", "C")

        Dim manipulator = mrScore.GetScoreManipulator(solution)
        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = CombinedScoringMapKey.Create({New ScoringMapKey(mrScore, "A"),
                                                       New ScoringMapKey(mrScore, "B"),
                                                       New ScoringMapKey(mrScore, "C")}, {0, 1})

        'Act
        factTargetManipulator.RemoveFromGroup(New ScoringMapKey(mrScore, "B"), New List(Of CombinedScoringMapKey) From {interactions})

        'Assert
        Dim factSetNumbersAfterRemove = manipulator.GetFactSetNumbers("A")
        Assert.AreEqual(2, factSetNumbersAfterRemove.Count)
        factSetNumbersAfterRemove = manipulator.GetFactSetNumbers("B")
        Assert.AreEqual(0, factSetNumbersAfterRemove.Count)

        Assert.AreEqual(1, DirectCast(DirectCast(solution.Findings(0).Facts(0), KeyFact).Values(0), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub AddFactSet_MultiChoice_Integer()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolution_MultiChoice_Integer)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2")
        Dim choiceScore = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MaxChoices = 1}.AddSubParameters("A", "B")

        Dim manipulator = integerScore.GetScoreManipulator(solution)
        Dim factSetNumbersBeforeAdd = manipulator.GetFactSetNumbers("1")
        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = CombinedScoringMapKey.Create(New ScoringMapKey(choiceScore, "B"),
                                                        New ScoringMapKey(integerScore, "1"),
                                                        New ScoringMapKey(integerScore, "2"))

        'Act
        Dim newFactSetNumber = factTargetManipulator.AddFactSet(interactions)
        manipulator.SetFactSetTarget(newFactSetNumber)
        manipulator.SetKey("1", 1)

        WriteSolution("after add", solution)

        'Assert
        Assert.IsNotNull(factSetNumbersBeforeAdd)
        Assert.AreEqual(2, factSetNumbersBeforeAdd.Count)
        Assert.IsFalse(factSetNumbersBeforeAdd.Contains(newFactSetNumber))

        Dim factSetNumbersAfterAdd = manipulator.GetFactSetNumbers("1")
        Assert.AreEqual(3, factSetNumbersAfterAdd.Count)
        Assert.IsTrue(factSetNumbersAfterAdd.Contains(newFactSetNumber))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub RemoveFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_3Facts6Sets)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim manipulatorA = integerScore.GetScoreManipulator(solution)

        Dim factSetNumbersBeforeRemove = manipulatorA.GetFactSetNumbers("3")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMapKey = New ScoringMapKey(integerScore, "3")

        'Act
        factTargetManipulator.RemoveFactSet(scoringMapKey, 5)

        WriteSolution("after remove", solution)

        'Assert
        Assert.IsNotNull(factSetNumbersBeforeRemove)
        Assert.AreEqual(6, factSetNumbersBeforeRemove.Count)

        Dim factSetNumbersAfterRemove = manipulatorA.GetFactSetNumbers("3")
        Assert.AreEqual(5, factSetNumbersAfterRemove.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanRemoveFactSet_NothingSelected_ReturnsFalse()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_3Facts6Sets)
        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")
        Dim scoringMapKey = New ScoringMapKey(integerScore, "1")

        'Act
        Dim result = factTargetManipulator.CanRemoveFactSet(scoringMapKey, Nothing)

        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanRemoveFactSet_FactSetSelected_ReturnsTrue()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_3Facts6Sets)
        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim integerScore As New IntegerScoringParameter With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}
        integerScore.AddSubParameters("1", "2", "3")
        Dim scoringMapKey = New ScoringMapKey(integerScore, "1")

        'Act
        Dim result = factTargetManipulator.CanRemoveFactSet(scoringMapKey, 5)

        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanRemoveFactSet_LastSet_ReturnsFalse()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_2Facts1Set)
        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim integerScore As New IntegerScoringParameter With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}
        integerScore.AddSubParameters("1")

        Dim scoringMapKey = New ScoringMapKey(integerScore, "1")

        'Act
        Dim result = factTargetManipulator.CanRemoveFactSet(scoringMapKey, 5)

        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub TryCanGroupOneInteraction_ReturnsFalse()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_noFactSets_SameInteraction)

        Dim integerScore As New IntegerScoringParameter With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}
        integerScore.AddSubParameters("1")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(integerScore, "1"))
        Dim scoringMap = New ScoringMap({integerScore}, solution).GetMap()

        'Act
        Dim result = factTargetManipulator.CanGroupInteractions(interactions, scoringMap)

        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub TryCanGroupTwoInteractions_ReturnsTrue()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_noFactSets_SameInteraction)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(integerScore, "1"))
        interactions.Add(New ScoringMapKey(integerScore, "2"))
        Dim scoringMap = New ScoringMap({integerScore}, solution).GetMap()

        'Act
        Dim result = factTargetManipulator.CanGroupInteractions(interactions, scoringMap)

        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub TryGroup_OneInteractionAlreadyPartOfFactSet_ReturnsFalse()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_differentInteractions_OneAlreadyPartOfFactSet)

        Dim integerScoreA = New IntegerScoringParameter() With {.ControllerId = "integerScoreA", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")
        Dim integerScoreB = New IntegerScoringParameter() With {.ControllerId = "integerScoreB", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(integerScoreA, "1"))
        interactions.Add(New ScoringMapKey(integerScoreB, "1"))
        Dim scoringMap = New ScoringMap({integerScoreA, integerScoreB}, solution).GetMap()

        'Act
        Dim result = factTargetManipulator.CanGroupInteractions(interactions, scoringMap)

        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanAddToGroup()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolutionSameParameter)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(integerScore, "3"))
        Dim scoringMap = New ScoringMap({integerScore}, solution).GetMap()

        'Act
        Dim result = factTargetManipulator.CanGroupInteractions(interactions, scoringMap)

        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CannotAddToGroup()
        'Arrange
        Dim solution As New Solution

        Dim mathScore1 = New MathCasEqualScoringParameter() With {.ControllerId = "firstMathScore", .FindingOverride = "gapController", .Name = "mathCasEqualScoringFirst"}.AddSubParameters("First")
        Dim casEqual = New CasEqualStepsScoringParameter() With {.ControllerId = "casEqualStepsScore", .FindingOverride = "gapController", .Name = "casEqualSteps"}.AddSubParameters("Second")

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(mathScore1, "First"))
        interactions.Add(New ScoringMapKey(casEqual, "Second"))

        Dim scoringMap = New ScoringMap({mathScore1, casEqual}, solution).GetMap()

        'Act
        Dim result = factTargetManipulator.CanGroupInteractions(interactions, scoringMap)

        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanRemoveFromGroup()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolution_MultiChoice_Integer)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2")
        Dim choiceScore = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MaxChoices = 1}.AddSubParameters("A", "B")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMapKey = New ScoringMapKey(integerScore, "1")
        Dim scoringMap = New ScoringMap({integerScore, choiceScore}, solution).GetMap()

        'Act
        Dim result = factTargetManipulator.CanRemoveFromGroup(scoringMapKey, 0, scoringMap)

        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanRemoveFromGroup_ReturnsFalse_WhenFactsetNotExists()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolution_MultiChoice_Integer)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2")
        Dim choiceScore = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MaxChoices = 1}.AddSubParameters("A", "B")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMapKey = New ScoringMapKey(integerScore, "1")
        Dim scoringMap = New ScoringMap({integerScore, choiceScore}, solution).GetMap()

        'Act
        Dim result = factTargetManipulator.CanRemoveFromGroup(scoringMapKey, 2, scoringMap)

        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanAddFactSet_InteractionIsPartOfFactset_ReturnsTrue()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolutionSameParameter)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = CombinedScoringMapKey.Create(New ScoringMapKey(integerScore, "1"))

        'Act
        Dim result = factTargetManipulator.CanAddFactSet(interactions)

        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanAddFactSet_NothingSelected_ReturnsFalse()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolutionSameParameter)

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim interactions = CombinedScoringMapKey.Create()

        'Act
        Dim result = factTargetManipulator.CanAddFactSet(interactions)

        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CanAddFactSet_InteractionIsNotPartOfFactset_ReturnsFalse()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_mixedSolutionSameParameter)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3")

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim interactions = CombinedScoringMapKey.Create(New ScoringMapKey(integerScore, "3"))

        'Act
        Dim result = factTargetManipulator.CanAddFactSet(interactions)

        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub RemoveLastFactSet_IntegerSelected_UngroupsInteractions()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_2Facts1Set)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMapKey = New ScoringMapKey(integerScore, "1")

        'Act
        factTargetManipulator.RemoveFactSet(scoringMapKey, 0)

        'Assert
        Dim manipulator = integerScore.GetScoreManipulator(solution)
        Dim factSetNumbers = manipulator.GetFactSetNumbers("1")
        Assert.AreEqual(0, factSetNumbers.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub RemoveInteraction_UngroupsInteractions_KeepsValues()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_3Groups)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1", "2", "3", "4", "5")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim integerScore3 = New ScoringMapKey(integerScore, "3")
        Dim scoringMap = New ScoringMap({integerScore}, solution).GetMap()
        solution.WriteToDebug("Arrange")

        'Act
        factTargetManipulator.RemoveFromGroup(integerScore3, scoringMap)

        'Assert
        solution.WriteToDebug("Assert")
        Dim manipulator = integerScore.GetScoreManipulator(solution)
        Dim factSetNumbers = manipulator.GetFactSetNumbers("3")
        Assert.AreEqual(0, factSetNumbers.Count, "It is expected that 3 is no longer in a fact set.")
        Dim status = manipulator.GetKeyStatus("3")
        Assert.AreEqual(4, status.First().Value.IntegerValue)

        factSetNumbers = manipulator.GetFactSetNumbers("4")
        Assert.AreEqual(0, factSetNumbers.Count, "4 should not have remained in a group since it's the only one in a group.")
        status = manipulator.GetKeyStatus("4")
        Assert.AreEqual(5, status.First().Value.IntegerValue)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub RemoveLastFactSet_MultiChoiceSelected_UngroupsInteractions()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_2Facts1Set)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")
        Dim choiceScore = New ChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MaxChoices = 1}.AddSubParameters("B")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMapKey = New ScoringMapKey(choiceScore, "B")

        'Act
        factTargetManipulator.RemoveFactSet(scoringMapKey, 0)

        'Assert
        Dim manipulator = integerScore.GetScoreManipulator(solution)
        Dim factSetNumbers = manipulator.GetFactSetNumbers("1")
        Assert.AreEqual(0, factSetNumbers.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetFactSetNumbers_ViaFactTargetManipulator()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_3Facts6Sets)

        Dim integerScore = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        'Act
        Dim result = factTargetManipulator.GetFactSetNumbers("1", integerScore)

        'Assert
        Assert.AreEqual(6, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetMcScoreFromFact0_NoExplicitSetTarget_Expects_A()
        'Arrange
        Dim solution = Deserialize(Of Solution)(McIn2Facts)

        Dim mcScore = New MultiChoiceScoringParameter() With {.ControllerId = "McScore"}.AddSubParameters("A", "B", "C")

        Dim manipulator = mcScore.GetScoreManipulator(solution)

        'Act
        'So we are not doing this: manipulator.SetFactSetTarget(0)
        Dim result = manipulator.GetKeysAlreadyManipulated()
        
        'Assert
        Assert.AreEqual("A", result.First())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetMcScoreFromFact0_Expects_A()
        'Arrange
        Dim solution = Deserialize(Of Solution)(McIn2Facts)

        Dim mcScore = New MultiChoiceScoringParameter() With {.ControllerId = "McScore"}.AddSubParameters("A", "B", "C")

        Dim manipulator = mcScore.GetScoreManipulator(solution)

        'Act
        manipulator.SetFactSetTarget(0)
        Dim result = manipulator.GetKeysAlreadyManipulated()
       
        'Assert
        Assert.AreEqual("A", result.First())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetMcScoreFromFact1_Expects_B()
        'Arrange
        Dim solution = Deserialize(Of Solution)(McIn2Facts)

        Dim mcScore = New MultiChoiceScoringParameter() With {.ControllerId = "McScore"}.AddSubParameters("A", "B", "C")

        Dim manipulator = mcScore.GetScoreManipulator(solution)

        'Act
        manipulator.SetFactSetTarget(1)
        Dim result = manipulator.GetKeysAlreadyManipulated()
        
        'Assert
        Assert.AreEqual("B", result.First())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub Ungroup_Int1_And_Int2_Expets2EmptyFactSets_and_eqlTo_recordedSolution()
        'Arrange
        Dim solution = _solution2SetInteger_6_14_and_14_6.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.FindingOverride = "sharedIntegerFinding", .ControllerId = "integerScore"}.AddSubParameters("1", "2")

        Dim factTargetManipulator = New FactTargetManipulator(solution)
        Dim scoringMap = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap()

        Dim combinedScoringMapKey = scoringMap.First()
        Dim int1 = combinedScoringMapKey.First(Function(smk) smk.ScoreKey = "1")
        Dim int2 = combinedScoringMapKey.First(Function(smk) smk.ScoreKey = "2")

        solution.WriteToDebug("Arrange")
        
        'Act
        factTargetManipulator.RemoveFromGroup(int1, scoringMap)

        factTargetManipulator.RemoveFromGroup(int2, scoringMap)
        
        'Assert
        solution.WriteToDebug("Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(_solution2SetInteger_6_14_and_14_6_Ungroup_Expected.ToString(), solution.DoSerialize().ToString()))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub Group2Of3MatrixInteractions_DoesNotClearKeys()
        'Arrange
        Dim solution = _3MatrixInteractions_UnGrouped.To(Of Solution)()
        Dim sp = New MatrixScoringParameter() With {.FindingOverride = "matrix", .ControllerId = "matrix"}.AddSubParameters("1", "2", "3")

        Dim factTargetManipulator = New FactTargetManipulator(solution)

        Dim interactions = New List(Of ScoringMapKey)()
        interactions.Add(New ScoringMapKey(sp, "1"))
        interactions.Add(New ScoringMapKey(sp, "2"))

        solution.WriteToDebug("Arrange")

        'Act
        factTargetManipulator.GroupInteractions(interactions)

        'Assert
        solution.WriteToDebug("Assert")

        Assert.IsTrue(UnitTestHelper.AreSame(solution.DoSerialize().ToString(), _3MatrixInteractions_2Grouped_Expected.ToString()))
    End Sub
    
#Region "Data"

    Private _emptySolution As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="DefaultFinding"/>
            </keyFindings>
            <aspectReferences/>
        </solution>

    Private _withFactSets As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="finding">
                    <keyFactSet>
                        <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="FieldA" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="FieldB" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="FieldA" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="FieldB" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="FieldA" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-FieldC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="FieldC" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
        </solution>

    Private _mixedSolution As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFact id="A-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="McScore" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
        </solution>

    Private _mixedSolution_MultiChoice_Integer As XElement =
         <solution>
             <keyFindings>
                 <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">
                     <keyFactSet>
                         <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="integerScore" occur="1">
                                 <integerValue>
                                     <typedValue>3</typedValue>
                                 </integerValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="integerScore" occur="1">
                                 <integerValue>
                                     <typedValue>7</typedValue>
                                 </integerValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="A-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="McScore" occur="1">
                                 <stringValue>
                                     <typedValue>A</typedValue>
                                 </stringValue>
                             </keyValue>
                         </keyFact>
                     </keyFactSet>
                     <keyFactSet>
                         <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="integerScore" occur="1">
                                 <integerValue>
                                     <typedValue>7</typedValue>
                                 </integerValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="integerScore" occur="1">
                                 <integerValue>
                                     <typedValue>3</typedValue>
                                 </integerValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="B-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="McScore" occur="1">
                                 <stringValue>
                                     <typedValue>B</typedValue>
                                 </stringValue>
                             </keyValue>
                         </keyFact>
                     </keyFactSet>
                 </keyFinding>
             </keyFindings>
             <aspectReferences/>
         </solution>
    
    Private _mixedSolutionSameParameter As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>

                    <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>21</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>

                </keyFinding>
            </keyFindings>
            <aspectReferences/>
        </solution>

    Private _noFactSets_SameInteraction As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="DefaultFinding">
                    <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>7</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFinding>
            </keyFindings>
        </solution>

    Private _noFactSets_Integer_MultiChoice As XElement =
       <solution>
           <keyFindings>
               <keyFinding id="DefaultFinding">
                   <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                       <keyValue domain="integerScore" occur="1">
                           <integerValue>
                               <typedValue>3</typedValue>
                           </integerValue>
                       </keyValue>
                   </keyFact>
                   <keyFact id="B-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                       <keyValue domain="McScore" occur="1">
                           <stringValue>
                               <typedValue>B</typedValue>
                           </stringValue>
                       </keyValue>
                   </keyFact>
               </keyFinding>
           </keyFindings>
       </solution>
    
    Private _differentInteractions_OneAlreadyPartOfFactSet As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="DefaultFinding">
                    <keyFactSet>
                        <keyFact id="1-integerScoreA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-integerScoreA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-integerScoreA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>2</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-integerScoreA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFact id="1-integerScoreB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>7</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFinding>
            </keyFindings>
        </solution>

    Private _3Facts2Sets As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>21</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>7</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>3</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="integerScore" occur="1">
                                <integerValue>
                                    <typedValue>21</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
        </solution>

    Private _3Facts6Sets As XElement =
      <solution>
          <keyFindings>
              <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">
                  <keyFactSet>
                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>2</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>3</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>5</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                  </keyFactSet>
                  <keyFactSet>
                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>3</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>2</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>5</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                  </keyFactSet>
                  <keyFactSet>
                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>2</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>5</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>3</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                  </keyFactSet>
                  <keyFactSet>
                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>3</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>5</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>2</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                  </keyFactSet>
                  <keyFactSet>
                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>5</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>2</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>3</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                  </keyFactSet>
                  <keyFactSet>
                      <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>5</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>3</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                      <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                          <keyValue domain="integerScore" occur="1">
                              <integerValue>
                                  <typedValue>2</typedValue>
                              </integerValue>
                          </keyValue>
                      </keyFact>
                  </keyFactSet>
              </keyFinding>
          </keyFindings>
          <aspectReferences/>
      </solution>

    Private _3Groups As XElement =
    <solution>
        <keyFindings>
            <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">

                <keyFactSet>
                    <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>

                <keyFactSet>
                    <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>4</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>5</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>


                <keyFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
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

    Private _2Facts1Set As XElement =
    <solution>
        <keyFindings>
            <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">
                <keyFactSet>
                    <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="integerScore" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="McScore" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>
        </keyFindings>
        <aspectReferences/>
    </solution>

    Private McIn2Facts As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="McScore" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="A-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="McScore" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="B-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="McScore" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
        </solution>
    
    Private ReadOnly _gapMatch As XElement =
       <solution>
           <keyFindings>
               <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                   <keyFact id="I214363b6-f877-4175-95ec-28042ac1b5a9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                       <keyValue domain="I214363b6-f877-4175-95ec-28042ac1b5a9" occur="1">
                           <stringValue>
                               <typedValue>3</typedValue>
                           </stringValue>
                       </keyValue>
                   </keyFact>
                   <keyFactSet>
                       <keyFact id="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                           <keyValue domain="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" occur="1">
                               <stringValue>
                                   <typedValue>2</typedValue>
                               </stringValue>
                           </keyValue>
                       </keyFact>
                       <keyFact id="I57c72f50-cf3a-499f-9473-3bd398a13282" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                           <keyValue domain="I57c72f50-cf3a-499f-9473-3bd398a13282" occur="1">
                               <stringValue>
                                   <typedValue>1</typedValue>
                               </stringValue>
                           </keyValue>
                       </keyFact>
                   </keyFactSet>
                   <keyFactSet>
                       <keyFact id="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                           <keyValue domain="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" occur="1">
                               <stringValue>
                                   <typedValue>3</typedValue>
                               </stringValue>
                           </keyValue>
                       </keyFact>
                       <keyFact id="I57c72f50-cf3a-499f-9473-3bd398a13282" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                           <keyValue domain="I57c72f50-cf3a-499f-9473-3bd398a13282" occur="1">
                               <stringValue>
                                   <typedValue>4</typedValue>
                               </stringValue>
                           </keyValue>
                       </keyFact>
                   </keyFactSet>
               </keyFinding>
           </keyFindings>
       </solution>

    ReadOnly _gapMatchItem As XElement =
        <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="GapMatchInline1" title="GapMatchInline1" layoutTemplateSrc="Cito.Generic.GapMatch.Inline.SC">
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="I4a2df403-47ee-4c5a-a10e-af9dec4fa46a-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I4a2df403-47ee-4c5a-a10e-af9dec4fa46a" occur="1">
                                <stringValue>
                                    <typedValue>E</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="I97f0b37a-df8c-4f3d-868f-2b2d6490998d-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I97f0b37a-df8c-4f3d-868f-2b2d6490998d" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I62fae0a2-b76f-412f-9afd-5b75b79d9a79-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I62fae0a2-b76f-412f-9afd-5b75b79d9a79" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>
            <parameters>
                <parameterSet id="entireItem">
                    <gapMatchScoringParameter name="gapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController">
                        <subparameterset id="A">
                            <gapTextParameter name="gapText" matchMax="1">peer</gapTextParameter>
                        </subparameterset>
                        <subparameterset id="B">
                            <gapTextParameter name="gapText" matchMax="1">appel</gapTextParameter>
                        </subparameterset>
                        <subparameterset id="C">
                            <gapTextParameter name="gapText" matchMax="1">sloot</gapTextParameter>
                        </subparameterset>
                        <subparameterset id="D">
                            <gapTextParameter name="gapText" matchMax="1">boom</gapTextParameter>
                        </subparameterset>
                        <subparameterset id="E">
                            <gapTextParameter name="gapText" matchMax="1">goed</gapTextParameter>
                        </subparameterset>
                        <subparameterset id="F">
                            <gapTextParameter name="gapText" matchMax="1">fout</gapTextParameter>
                        </subparameterset>
                        <definition id="">
                            <gapTextParameter name="gapText" matchMax="1"/>
                        </definition>
                        <xhtmlParameter name="gapMatchInlineInput">
                            <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">De <cito:InlineElement id="I97f0b37a-df8c-4f3d-868f-2b2d6490998d" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineGapMatchId">I97f0b37a-df8c-4f3d-868f-2b2d6490998d</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineGapMatchLabel">gat1</cito:plaintextparameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement> valt niet ver van de <cito:InlineElement id="I62fae0a2-b76f-412f-9afd-5b75b79d9a79" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineGapMatchId">I62fae0a2-b76f-412f-9afd-5b75b79d9a79</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineGapMatchLabel">gat2</cito:plaintextparameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement>. <cito:InlineElement id="I4a2df403-47ee-4c5a-a10e-af9dec4fa46a" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineGapMatchId">I4a2df403-47ee-4c5a-a10e-af9dec4fa46a</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineGapMatchLabel">gat3</cito:plaintextparameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement>
                            </p>
                        </xhtmlParameter>
                    </gapMatchScoringParameter>
                </parameterSet>
            </parameters>
        </assessmentItem>

    Private _multipleResponse As XElement =
         <solution>
             <keyFindings>
                 <keyFinding id="Opgave" scoringMethod="Dichotomous">
                     <keyFactSet>
                         <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="mc_1" occur="1">
                                 <booleanValue>
                                     <typedValue>true</typedValue>
                                 </booleanValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="mc_1" occur="1">
                                 <booleanValue>
                                     <typedValue>false</typedValue>
                                 </booleanValue>
                             </keyValue>
                         </keyFact>
                     </keyFactSet>
                 </keyFinding>
             </keyFindings>
         </solution>
    
    Private _groupedMultipleResponse As XElement =
         <solution>
             <keyFindings>
                 <keyFinding id="Opgave" scoringMethod="Dichotomous">
                     <keyFactSet>
                         <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="mc_1" occur="1">
                                 <booleanValue>
                                     <typedValue>true</typedValue>
                                 </booleanValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="mc_1" occur="1">
                                 <booleanValue>
                                     <typedValue>false</typedValue>
                                 </booleanValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="mc_1" occur="1">
                                 <booleanValue>
                                     <typedValue>false</typedValue>
                                 </booleanValue>
                             </keyValue>
                         </keyFact>
                     </keyFactSet>
                     <keyFactSet>
                         <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="mc_1" occur="1">
                                 <booleanValue>
                                     <typedValue>false</typedValue>
                                 </booleanValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="mc_1" occur="1">
                                 <booleanValue>
                                     <typedValue>true</typedValue>
                                 </booleanValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                             <keyValue domain="mc_1" occur="1">
                                 <booleanValue>
                                     <typedValue>false</typedValue>
                                 </booleanValue>
                             </keyValue>
                         </keyFact>
                     </keyFactSet>
                 </keyFinding>
             </keyFindings>
         </solution>

    ReadOnly _solution2SetInteger_6_14_and_14_6 As XElement = <solution>
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
                                                              </solution>

    ReadOnly _solution2SetInteger_6_14_and_14_6_Ungroup_Expected As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                                   <keyFindings>
                                                                                       <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                                           <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                               <keyValue domain="integerScore" occur="1">
                                                                                                   <integerValue>
                                                                                                       <typedValue>6</typedValue>
                                                                                                   </integerValue>
                                                                                                   <integerValue>
                                                                                                       <typedValue>14</typedValue>
                                                                                                   </integerValue>
                                                                                               </keyValue>
                                                                                           </keyFact>
                                                                                           <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                               <keyValue domain="integerScore" occur="1">
                                                                                                   <integerValue>
                                                                                                       <typedValue>14</typedValue>
                                                                                                   </integerValue>
                                                                                                   <integerValue>
                                                                                                       <typedValue>6</typedValue>
                                                                                                   </integerValue>
                                                                                               </keyValue>
                                                                                           </keyFact>
                                                                                           <keyFactSet/>
                                                                                           <keyFactSet/>
                                                                                       </keyFinding>
                                                                                   </keyFindings>
                                                                                   <aspectReferences/>
                                                                               </solution>


    ReadOnly _3MatrixInteractions_UnGrouped As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="matrix" scoringMethod="Dichotomous">
                    <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="matrix" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="matrix" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="matrix" occur="1">
                            <stringValue>
                                <typedValue>A</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _3MatrixInteractions_2Grouped_Expected As XElement =
        <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
            <keyFindings>
                <keyFinding id="matrix" scoringMethod="Dichotomous">
                    <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="matrix" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>
#End Region

    Function DoSerialize(Of T)(obj As T) As XElement
        Dim s = New XmlSerializer(GetType(T))
        Dim ret As XElement = Nothing
        Using m As New StringWriter()
            s.Serialize(m, obj)
            ret = XElement.Parse(m.ToString())
        End Using
        Return ret
    End Function

    Protected Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)
            Debug.WriteLine(stream.ToString())
        End Using
    End Sub
End Class
