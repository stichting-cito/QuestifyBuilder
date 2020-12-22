
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ScoringMapKeyTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub LabesIsMoreImportantThanName()

        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty", .Label = "labelProperty"}.AddSubParameters("A")

        Dim result = New ScoringMapKey(sp, "A")

        Assert.AreEqual("labelProperty", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyWillTakeNamePropertyAsName()
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty"}.AddSubParameters("A")

        Dim result = New ScoringMapKey(sp, "A")

        Assert.AreEqual("nameProperty", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub LabesIsMoreImportantThanName_MoreThanOneScoringPrmExpectsSuffix()

        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty", .Label = "labelProperty"}.AddSubParameters("A", "B")

        Dim result = New ScoringMapKey(sp, "A")

        Assert.AreEqual("labelProperty.A", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKey_ShouldSuffixDeterminedByScoringParameter()
        Dim sp = New GapMatchScoringParameter() With {.Name = "Name"}.AddSubParameters("A", "B")

        Dim result = New ScoringMapKey(sp, "A")

        Assert.AreEqual("Name", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyWillTakeNamePropertyAsName_MoreThanOneScoringPrmExpectsSuffix()
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty"}.AddSubParameters("A", "B")

        Dim result = New ScoringMapKey(sp, "B")

        Assert.AreEqual("nameProperty.B", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyWillTakeNamePropertyAsName_MoreThanOneScoringPrmExpectsSuffix_NoValidationThatKeyExists()
        Dim sp = New IntegerScoringParameter() With {.Name = "nameProperty"}.AddSubParameters("A", "B")

        Dim result = New ScoringMapKey(sp, "X")

        Assert.AreEqual("nameProperty.X", result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyHandlesScoringParameterWihoutNameOrLabel_NameIs_EmptyString()
        Dim sp = New IntegerScoringParameter().AddSubParameters("A")

        Dim result = New ScoringMapKey(sp, "A")

        Assert.AreEqual(String.Empty, result.Name)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoringMapKeyHandlesScoringParameterWihoutNameOrLabel_WithMultipleSubParameters_NameIs_Key()
        Dim sp = New IntegerScoringParameter().AddSubParameters("A", "B")

        Dim result = New ScoringMapKey(sp, "A")

        Assert.AreEqual("A", result.Name)
    End Sub

End Class
