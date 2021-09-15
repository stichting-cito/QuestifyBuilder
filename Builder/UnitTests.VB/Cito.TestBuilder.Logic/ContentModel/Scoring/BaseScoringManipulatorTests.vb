
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

''' <summary>
''' This class deals with common unit tests that are equal for KeyScoring and ConceptScoring.
''' </summary>
<TestClass>
Public MustInherit Class BaseScoringManipulatorTests(Of TFinding As {BaseFinding, New},
                                                         TFact As {BaseFact, New},
                                                         TFactValue As {BaseFactValue, New})

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Emptyfinding_Id_IsEmpty()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
       
        'Act
        Dim result = manipulator.GetIds().ToList()
        
        'Assert
        Assert.AreEqual(0, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ABCfinding_Id_Has3()
        'Arrange
        Dim key = finding_WithABC()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetIds().ToList()
        
        'Assert
        Assert.AreEqual(3, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ABCfinding_Clear_Has0Facts()
        'Arrange
        Dim key = finding_WithABC()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.Clear()
        Dim result = manipulator.GetIds().ToList()
        
        'Assert
        Assert.AreEqual(0, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ABCfinding_Get_ExistingFact()
        'Arrange
        Dim key = finding_WithABC()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetFacts("A").FirstOrDefault()
        
        'Assert
        Assert.IsNotNull(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ABCfinding_Get_NONExistingFact()
        'Arrange
        Dim key = finding_WithABC()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetFacts("I DO NOT EXSIST").FirstOrDefault()
        
        'Assert
        Assert.IsNull(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub AddScoreToEmptyFindingWillAddFact()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKey(Of String)("ScoreId", "A")
        
        'Assert
        Assert.AreEqual(1, key.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub AddScoreWillHaveScore1()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKey("ScoreId", "A")
        
        'Assert
        Assert.AreEqual(1, DirectCast(key.Facts(0), KeyFact).Score)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub AddScoreWithAlternativesWillHaveScore1()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKeyWithOptionals("a", 1, 2, 3, 4)
        
        'Assert
        Assert.AreEqual(1, DirectCast(key.Facts(0), KeyFact).Score)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_String_ScoreToEmptyFindingWillAdd_Value()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKey(Of String)("ScoreId", "A")
        
        'Assert
        Assert.AreEqual(1, key.Facts(0).Values.Count)
        Assert.AreEqual(1, DirectCast(key.Facts(0).Values(0), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_String_ScoreToEmptyFindingWillAdd_Value_ThatMatchesA()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKey(Of String)("ScoreId", "A")
        
        'Assert
        Assert.IsInstanceOfType(key.Facts(0).Values(0), GetType(KeyValue))
        Assert.AreEqual("A", DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), StringValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_integer_ScoreToEmptyFindingWillAdd_Value_ThatMatches_42()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKey(Of Integer)("ScoreId", 42)
        
        'Assert
        Assert.IsInstanceOfType(key.Facts(0).Values(0), GetType(KeyValue))
        Assert.AreEqual(42, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), IntegerValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_Decimal_ScoreToEmptyFindingWillAdd_Value_ThatMatches_PI()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
       
        'Act
        manipulator.SetKey(Of Decimal)("ScoreId", 3.141592D)
        
        'Assert
        Assert.IsInstanceOfType(key.Facts(0).Values(0), GetType(KeyValue))
        Assert.AreEqual(3.141592D, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), DecimalValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Add_Binary_ScoreToEmptyFindingWillAdd_Value_ThatMatches_Pattern()
        'Arrange
        Dim pattern As Byte() = {5, 3, 2}
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKey(Of Byte())("ScoreId", pattern)
        
        'Assert
        Assert.IsInstanceOfType(key.Facts(0).Values(0), GetType(KeyValue))
        Assert.IsTrue(Enumerable.SequenceEqual(pattern, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), BinaryValue).Value))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Adding_KeysWithSameID_CountOfFactsShouldRemain1()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
       
        'Act
        manipulator.SetKey("ScoreId", "A")
        manipulator.SetKey("ScoreId", "B")
        
        'Assert
        Assert.AreEqual(1, key.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Adding_KeysWithSameID_CountOfFactValuesShould_Equals2()
        'Multiple ...Values denote an AND statement.
        'So if you require the key to be A & B,
        'you need two KeyValues: one with Value A, the other with Value B.
        'This is true for MultipleChoice where the response will be A & B & F 

        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
       
        'Act
        manipulator.SetKey("ScoreId", "A")
        manipulator.SetKey("ScoreId", "B")
        
        'Assert
        Assert.AreEqual(2, key.Facts(0).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Adding_KeysWithSameID_CountOfValuesShould_Equals1()
        'Multiple ...Values denote an AND statement.
        'So if you require the key to be A & B,
        'you need two KeyValues: one with Value A, the other with Value B.
        'This is true for MultipleChoice where the response will be A & B & F 
        
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKey("ScoreId", "A")
        manipulator.SetKey("ScoreId", "B")
        
        'Assert
        Assert.AreEqual(1, DirectCast(key.Facts(0).Values(0), KeyValue).Values.Count)
        Assert.AreEqual(1, DirectCast(key.Facts(0).Values(1), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub AddingKeyWith_3_Multiples_will_add_3_Values()
        'Multiple ...Values denote an AND statement.
        'Multiple BaseValue's (i.e. values within ...values) denote OR.
        'So if you require the key to be A | B,
        'you need two baseValues to a KeyValue

        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKeyWithOptionals("ScoreId", 1, 2, 3)
        
        'Assert
        Assert.AreEqual(3, DirectCast(key.Facts(0).Values(0), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub TwoTimes_AddingKeyWith_3_Multiples_will_add_6_Values()
        'Multiple ...Values denote an AND statement.
        'Multiple BaseValue's (i.e. values within ...values) denote OR.
        'So if you require the key to be A | B,
        'you need two baseValues to a KeyValue

        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetKeyWithOptionals("ScoreId", 1, 2, 3)
        manipulator.SetKeyWithOptionals("ScoreId", 4, 5, 6)
        
        'Assert
        Assert.AreEqual(3, DirectCast(key.Facts(0).Values(0), KeyValue).Values.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetScoringMethod_of_Empty_Expects_NONE()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetScoringMethod()
        
        'Assert
        Assert.AreEqual(EnumScoringMethod.None, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetScoringMethod_of_Empty_Expects_NONE()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        manipulator.SetScoringMethod(EnumScoringMethod.Polytomous)
        Dim result = manipulator.GetScoringMethod()
        
        'Assert
        Assert.AreEqual(EnumScoringMethod.Polytomous, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_With2KeyFacts_Finds2()
        'Arrange
        Dim key = finding_MC_MultipleFacts("A", "B") 'Has Multiple Facts
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetFacts("MC").ToList()
        
        'Assert
        Assert.AreEqual(2, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_MC_Example_ShouldReturn2Entries()
        'Arrange
        Dim key = finding_MC_MultipleFacts("A", "B") 'Has Multiple Facts
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetKeys("MC")
        
        'Assert
        Assert.AreEqual(2, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_MC_Example_ShouldReturn2Entries_EachWithOneBaseValue()
        'Arrange
        Dim key = finding_MC_MultipleFacts("A", "B") 'Has Multiple Facts
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetKeys("MC")
        
        'Assert
        For Each e In result
            Assert.AreEqual(1, e.Count)
        Next
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_With1KeyFacts_ButMultipleBaseValues_Finds1Fact()
        'Arrange
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4) 'Has single fact
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetFacts("NUM").ToList()
        
        'Assert
        Assert.AreEqual(1, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_NUM_Example_ShouldReturn1Entries()
        'Arrange
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4) 'Has single fact
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetKeys("NUM")
        
        'Assert
        Assert.AreEqual(1, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_NUM_Example_ShouldReturn1Entries_EachWith4BaseValues()
        'Arrange
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4) 'Has single fact
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim result = manipulator.GetKeys("NUM")
        
        'Assert
        For Each e In result
            Assert.AreEqual(4, e.Count)
        Next
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ReplaceMistake()
        'Arrange
        Dim key = finding_NUM_MultipleFactsValues(1, 3, 3, 4) 'Has single fact
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        'Act
        manipulator.ReplaceKeyWithSpecificOptionals("NUM", 2, 1)

        'Assert
        Assert.AreEqual(1, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(0), IntegerValue).Value)
        Assert.AreEqual(2, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(1), IntegerValue).Value)
        Assert.AreEqual(3, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(2), IntegerValue).Value)
        Assert.AreEqual(4, DirectCast(DirectCast(key.Facts(0).Values(0), KeyValue).Values(3), IntegerValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_NUM_Example_DeleteKey()
        'Arrange
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4) 'Has single fact
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim res = manipulator.UnSetKey("NUM") 'UnSetKey does not delete Fact
        
        'Assert
        Assert.AreEqual(1, key.Facts.Count, "Fact should be present")
        Assert.AreEqual(1, res, "1 time delete action") 'Deleted 1 fact.
        Assert.AreEqual(0, DirectCast(key.Facts(0), KeyFact).Score, "Fact should have 0 score")
        Assert.AreEqual(0, key.Facts(0).Values.Count, "No values should be here")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_NUM_Example_DeleteNotExistingKey()
        'Arrange
        Dim key = finding_NUM_MultipleFactsValues(1, 2, 3, 4) 'Has single fact
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        
        'Act
        Dim res = manipulator.UnSetKey("I Do Not exist")
        
        'Assert
        Assert.AreEqual(1, key.Facts.Count)
        Assert.AreEqual(0, res) 'Deleted 0 fact.
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_MC_Example_RemoveSingleExistingKey()
        'Arrange
        Dim key = finding_MC_MultipleFacts()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        manipulator.SetKey("A", "ValueA")
        manipulator.SetKey("B", "ValueB")
        manipulator.SetKey("C", "ValueC")

        'Act
        manipulator.RemoveFact("B")

        'Assert
        Assert.AreEqual(2, key.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getkeys_MC_Example_RemoveSingleNonExistingKey()
        'Arrange
        Dim key = finding_MC_MultipleFacts()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)
        manipulator.SetKey("A", "ValueA")
        manipulator.SetKey("B", "ValueB")
        manipulator.SetKey("C", "ValueC")

        'Act
        manipulator.RemoveFact("D") 'Trying to remove a non-existing key should fail silently

        'Assert
        Assert.AreEqual(3, key.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetAndGetPreProcessingMethods()
        'Arrange
        Dim key = finding_NUM_MultipleFactsValues(1)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        Dim lst = New List(Of String)
        lst.Add("type")

        manipulator.SetPreProcessingMethods("NUM", lst)
        
        'Act
        Dim result = manipulator.GetPreProcessingMethods("NUM").ToList()

        'Assert
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual("type", result(0))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CanTheManipulatorManipulateFactSets()
        'Arrange
        Dim key = EmptyFinding()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        'Act
        Dim result = manipulator.CanManipulateSets

        'Assert
        Assert.AreEqual(GetPerConcreteManipulator_CanManipulateSets(), result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SolutionWithSet_HasSets_EqualsTrue()
        'ESCAPE IF THE MANIPULATOR CAN NOT MODIFY.
        'This can occur when a response finding manipulator is ever made.
        If (Not GetPerConcreteManipulator_CanManipulateSets) Then Assert.Inconclusive()

        'Arrange
        Dim key = findingWithSet()
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        'Act
        Dim result = manipulator.HasSets

        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SolutionWithSet_NoTargetSet()
        'ESCAPE IF THE MANIPULATOR CAN NOT MODIFY.
        'This can occur when a response finding manipulator is ever made.
        If (Not GetPerConcreteManipulator_CanManipulateSets) Then Assert.Inconclusive()

        'Arrange
        Dim key = findingWithSet() : Dim keyfinding = TryCast(key, KeyFinding)
        Dim manipulator As IFindingManipulator = CreateFindingManipulator(key)

        'Act
        manipulator.SetKey(Of String)("MC1", "XX")

        'Assert
        If (keyfinding Is Nothing) Then Assert.Fail("Should Not occur") 'Casting finding to KeyFinding since this is the current base class for Findings with a FactSet.

        Assert.AreEqual(0, keyfinding.Facts.Count, "Expected 0 facts")
        Assert.AreEqual(1, keyfinding.KeyFactsets.Count, "Expect 1 factSet")
    End Sub

    Public MustOverride Function CreateFindingManipulator(key As TFinding) As IFindingManipulator
    Public MustOverride Function MakeFact(id As String) As TFact
    Public MustOverride Function MakeFactValue(id As String, occur As Int16) As TFactValue
    Public MustOverride Sub AddToFactValueValues(val As BaseValue, toFactValue As TFactValue)

    Public MustOverride Sub AddFactsToFactSet(finding As TFinding, position As Integer, ParamArray facts As TFact())


    ''' <summary>
    ''' Gets a value indicating whether the manipulator should be able to manipulate sets.
    ''' </summary>
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
