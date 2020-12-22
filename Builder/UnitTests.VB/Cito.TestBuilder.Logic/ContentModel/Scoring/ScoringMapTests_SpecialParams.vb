
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ScoringMapTests_SpecialParams

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub OrderScoringParameter_NoSolution_GetMap_ShouldBeGrouped()
        Dim param = New OrderScoringParameter() With {.ControllerId = "Order1", .FindingOverride = "SharedFinding"}.AddSubParameters("A", "B", "C", "D")

        Dim result = New ScoringMap(New ScoringParameter() {param}, New Solution()).GetMap().ToList()

        Assert.AreEqual(1, result.Count, "OrderScoringParameter are grouped thus should act as such")
        Assert.AreEqual(True, result(0).IsGroup, "OrderScoringParameter are grouped thus should act as such")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub OrderScoringParameter_NoSolution_GetMap_SetNumberShouldBe_Empty()
        Dim param = New OrderScoringParameter() With {.ControllerId = "Order1", .FindingOverride = "SharedFinding"}.AddSubParameters("A", "B", "C", "D")

        Dim result = New ScoringMap(New ScoringParameter() {param}, New Solution()).GetMap().ToList().First()

        Assert.AreEqual(True, result.IsGroup, "OrderScoringParameter are grouped thus should act as such")
        Assert.AreEqual(0, result.SetNumbers.Count(), "OrderScoringParameter is special,.. it is forcibly grouped")
    End Sub

End Class