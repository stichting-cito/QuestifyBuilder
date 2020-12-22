
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass()> Public Class ScoringParamValidatiorTests

    <TestMethod(), TestCategory("Logic")>
    Public Sub Default_MultiChoiceScoringParameter_Test()
        Dim s = New MultiChoiceScoringParameter

        Dim result = s.IsValid()

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Default_MultiChoiceScoringParameter_Test_2()
        Dim s = New MultiChoiceScoringParameter() With {.ControllerId = "mc"}.AddSubParameters("A", "B")

        Dim result = s.IsValid()

        Assert.IsTrue(result)
    End Sub

End Class