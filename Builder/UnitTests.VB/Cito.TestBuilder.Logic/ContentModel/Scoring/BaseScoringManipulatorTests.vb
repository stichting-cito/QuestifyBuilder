
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

<TestClass>
Public MustInherit Class BaseScoringManipulatorTests(Of TFinding As {BaseFinding, New},
                                                         TFact As {BaseFact, New},
                                                         TFactValue As {BaseFactValue, New})

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Emptyfinding_Id_IsEmpty()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetIds().ToList()

        Assert.AreEqual(0, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ABCfinding_Id_Has3()
        Dim key = finding_WithABC()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetIds().ToList()

        Assert.AreEqual(3, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ABCfinding_Clear_Has0Facts()
        Dim key = finding_WithABC()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.Clear()
        Dim result = manipulator.GetIds().ToList()

        Assert.AreEqual(0, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ABCfinding_Get_ExistingFact()
        Dim key = finding_WithABC()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetFacts("A").FirstOrDefault()

        Assert.IsNotNull(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ABCfinding_Get_NONExistingFact()
        Dim key = finding_WithABC()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetFacts("I DO NOT EXSIST").FirstOrDefault()

        Assert.IsNull(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub AddScoreToEmptyFindingWillAddFact()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey(Of String)("ScoreId", "A")

        Assert.AreEqual(1, key.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub AddScoreWillHaveScore1()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey("ScoreId", "A")

        Assert.AreEqual(1, DirectCast(key.Facts(0), KeyFact).Score)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub AddScoreWithAlternativesWillHaveScore1()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKeyWithOptionals("a", 1, 2, 3, 4)

        Assert.AreEqual(1, DirectCast(key.Facts(0), KeyFact).Score)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_String_ScoreToEmptyFindingWillAdd_Value()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey(Of String)("ScoreId", "A")

        Assert.AreEqual(1, key.Facts(0).Values.Count)
        Assert.AreEqual(1, DirectCast(key.Facts(0).Values(0), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_String_ScoreToEmptyFindingWillAdd_Value_ThatMatchesA()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey(Of String)("ScoreId", "A")

        Assert.IsInstanceOfType(key.Facts(0).Values(0), GetType(KeyValue))
        Assert.AreEqual("A", DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), StringValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_integer_ScoreToEmptyFindingWillAdd_Value_ThatMatches_42()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey(Of Integer)("ScoreId", 42)

        Assert.IsInstanceOfType(key.Facts(0).Values(0), GetType(KeyValue))
        Assert.AreEqual(42, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), IntegerValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_Decimal_ScoreToEmptyFindingWillAdd_Value_ThatMatches_PI()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey(Of Decimal)("ScoreId", 3.141592D)

        Assert.IsInstanceOfType(key.Facts(0).Values(0), GetType(KeyValue))
        Assert.AreEqual(3.141592D, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), DecimalValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_Binary_ScoreToEmptyFindingWillAdd_Value_ThatMatches_Pattern()
        Dim pattern As Byte() = {5, 3, 2}
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey(Of Byte())("ScoreId", pattern)

        Assert.IsInstanceOfType(key.Facts(0).Values(0), GetType(KeyValue))
        Assert.IsTrue(Enumerable.SequenceEqual(pattern, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), BinaryValue).Value))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Adding_KeysWithSameID_CountOfFactsShouldRemain1()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey("ScoreId", "A")
        manipulator.SetKey("ScoreId", "B")

        Assert.AreEqual(1, key.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Adding_KeysWithSameID_CountOfFactValuesShould_Equals2()

        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey("ScoreId", "A")
        manipulator.SetKey("ScoreId", "B")

        Assert.AreEqual(2, key.Facts(0).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Adding_KeysWithSameID_CountOfValuesShould_Equals1()

        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey("ScoreId", "A")
        manipulator.SetKey("ScoreId", "B")

        Assert.AreEqual(1, DirectCast(key.Facts(0).Values(0), KeyValue).Values.Count)
        Assert.AreEqual(1, DirectCast(key.Facts(0).Values(1), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub AddingKeyWith_3_Multiples_will_add_3_Values()

        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKeyWithOptionals("ScoreId", 1, 2, 3)

        Assert.AreEqual(3, DirectCast(key.Facts(0).Values(0), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TwoTimes_AddingKeyWith_3_Multiples_will_add_6_Values()

        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKeyWithOptionals("ScoreId", 1, 2, 3)
        manipulator.SetKeyWithOptionals("ScoreId", 4, 5, 6)

        Assert.AreEqual(3, DirectCast(key.Facts(0).Values(0), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetScoringMethod_of_Empty_Expects_NONE()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetScoringMethod()

        Assert.AreEqual(EnumScoringMethod.None, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetScoringMethod_of_Empty_Expects_NONE()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetScoringMethod(EnumScoringMethod.Polytomous)
        Dim result = manipulator.GetScoringMethod()

        Assert.AreEqual(EnumScoringMethod.Polytomous, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_With2KeyFacts_Finds2()
        Dim key = finding_MC_MultipleFacts("A", "B")
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetFacts("MC").ToList()

        Assert.AreEqual(2, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_MC_Example_ShouldReturn2Entries()
        Dim key = finding_MC_MultipleFacts("A", "B")
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetKeys("MC")

        Assert.AreEqual(2, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_MC_Example_ShouldReturn2Entries_EachWithOneBaseValue()
        Dim key = finding_MC_MultipleFacts("A", "B")
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetKeys("MC")

        For Each e In result
            Assert.AreEqual(1, e.Count)
        Next
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_With1KeyFacts_ButMultipleBaseValues_Finds1Fact()
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetFacts("NUM").ToList()

        Assert.AreEqual(1, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_NUM_Example_ShouldReturn1Entries()
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetKeys("NUM")

        Assert.AreEqual(1, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_NUM_Example_ShouldReturn1Entries_EachWith4BaseValues()
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.GetKeys("NUM")

        For Each e In result
            Assert.AreEqual(4, e.Count)
        Next
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ReplaceMistake()
        Dim key = finding_NUM_MultipleFactsValues(1, 3, 3, 4)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        manipulator.ReplaceKeyWithSpecificOptionals("NUM", 2, 1)

        Assert.AreEqual(1, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), IntegerValue).Value)
        Assert.AreEqual(2, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(1), IntegerValue).Value)
        Assert.AreEqual(3, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(2), IntegerValue).Value)
        Assert.AreEqual(4, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(3), IntegerValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_NUM_Example_DeleteKey()
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim res = manipulator.UnSetKey("NUM")

        Assert.AreEqual(1, key.Facts.Count, "Fact should be present")
        Assert.AreEqual(1, res, "1 time delete action")
        Assert.AreEqual(0, DirectCast(key.Facts(0), KeyFact).Score, "Fact should have 0 score")
        Assert.AreEqual(0, key.Facts(0).Values.Count, "No values should be here")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_NUM_Example_DeleteNotExistingKey()
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim res = manipulator.UnSetKey("I Do Not exist")

        Assert.AreEqual(1, key.Facts.Count)
        Assert.AreEqual(0, res)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_MC_Example_RemoveSingleExistingKey()
        Dim key = finding_MC_MultipleFacts()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        manipulator.SetKey("A", "ValueA")
        manipulator.SetKey("B", "ValueB")
        manipulator.SetKey("C", "ValueC")

        manipulator.RemoveFact("B")

        Assert.AreEqual(2, key.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_MC_Example_RemoveSingleNonExistingKey()
        Dim key = finding_MC_MultipleFacts()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        manipulator.SetKey("A", "ValueA")
        manipulator.SetKey("B", "ValueB")
        manipulator.SetKey("C", "ValueC")

        manipulator.RemoveFact("D")

        Assert.AreEqual(3, key.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetAndGetPreProcessingMethods()
        Dim key = finding_NUM_MultipleFactsValues(1)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim lst = New List(Of String)
        lst.Add("type")

        manipulator.SetPreProcessingMethods("NUM", lst)

        Dim result = manipulator.GetPreProcessingMethods("NUM").ToList()

        Assert.AreEqual(1, result.Count)
        Assert.AreEqual("type", result(0))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CanTheManipulatorManipulateFactSets()
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.CanManipulateSets

        Assert.AreEqual(GetPerConcreteManipulator_CanManipulateSets(), result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SolutionWithSet_HasSets_EqualsTrue()
        If (Not GetPerConcreteManipulator_CanManipulateSets) Then Assert.Inconclusive()

        Dim key = findingWithSet()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim result = manipulator.HasSets

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SolutionWithSet_NoTargetSet()
        If (Not GetPerConcreteManipulator_CanManipulateSets) Then Assert.Inconclusive()

        Dim key = findingWithSet() : Dim keyfinding = TryCast(key, KeyFinding)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        manipulator.SetKey(Of String)("MC1", "XX")

        If (keyfinding Is Nothing) Then Assert.Fail("Should Not occur")

        Assert.AreEqual(0, keyfinding.Facts.Count, "Expected 0 facts")
        Assert.AreEqual(1, keyfinding.KeyFactsets.Count, "Expect 1 factSet")
    End Sub

    Public MustOverride Function CreateFindingManipulator(key As TFinding) As IFindingManipulator
    Public MustOverride Function MakeFact(id As String) As TFact
    Public MustOverride Function MakeFactValue(id As String, occur As Int16) As TFactValue
    Public MustOverride Sub AddToFactValueValues(val As BaseValue, toFactValue As TFactValue)

    Public MustOverride Sub AddFactsToFactSet(finding As TFinding, position As Integer, ParamArray facts As TFact())


    Public MustOverride ReadOnly Property GetPerConcreteManipulator_CanManipulateSets As Boolean

    Private Function EmptyFinding() As TFinding
        Dim ret As New TFinding()

        Return ret
    End Function

    Private Function finding_WithABC() As TFinding
        Dim ret As New TFinding()
        ret.Facts.Add(MakeFact("A"))
        ret.Facts.Add(MakeFact("B"))
        ret.Facts.Add(MakeFact("C"))
        Return ret
    End Function

    Private Function finding_MC_MultipleFacts(ParamArray answer As String()) As TFinding
        Dim ret As New TFinding()

        For Each e In answer
            Dim fact = MakeFact("MC")
            Dim factValue = MakeFactValue("MC", 1)
            AddToFactValueValues(New StringValue(e), factValue)
            fact.Values.Add(factValue)
            ret.Facts.Add(fact)
        Next

        Return ret
    End Function

    Private Function finding_NUM_MultipleFactsValues(ParamArray answer As Integer()) As TFinding
        Dim ret As New TFinding()

        Dim fact = MakeFact("NUM")
        Dim factValue = MakeFactValue("NUM", 1)

        For Each e In answer
            AddToFactValueValues(New IntegerValue(e), factValue)
        Next

        fact.Values.Add(factValue)

        ret.Facts.Add(fact)

        Return ret
    End Function

    Private Function findingWithSet() As TFinding
        Dim ret As New TFinding()

        Dim fact = MakeFact("MC1")
        Dim factValue = MakeFactValue("MC1", 1)
        AddToFactValueValues(New StringValue("A"), factValue)

        Dim fact2 = MakeFact("MC2")
        Dim factValue2 = MakeFactValue("MC2", 1)
        AddToFactValueValues(New StringValue("A"), factValue2)

        AddFactsToFactSet(ret, 0, fact, fact2)

        Return ret
    End Function

End Class
