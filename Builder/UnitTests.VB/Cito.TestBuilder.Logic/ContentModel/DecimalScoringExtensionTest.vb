
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq

<TestClass>
Public Class DecimalScoringExtensionTest
    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Sub Test_FindingOverride_SetOrNotSet()
        Dim paramOverrideSet = New DecimalScoringParameter() With {.ControllerId = "DC", .FindingOverride = "overriden_finding_id"}
        Dim paramOverrideNotSet = New DecimalScoringParameter() With {.ControllerId = "DC"}
        Dim solutionOverrideSet As New Solution()
        Dim solutionOverrideNotSet As New Solution()

        Dim manipulator1 = paramOverrideSet.GetScoreManipulator(solutionOverrideSet)
        Dim manipulator2 = paramOverrideNotSet.GetScoreManipulator(solutionOverrideNotSet)

        manipulator1.SetHowToScoreMethod(EnumScoringMethod.Dichotomous)
        manipulator2.SetHowToScoreMethod(EnumScoringMethod.Dichotomous)

        Assert.AreEqual("overriden_finding_id", solutionOverrideSet.Findings.First().Id)
        Assert.AreEqual("DC", solutionOverrideNotSet.Findings.First().Id)
    End Sub
End Class
