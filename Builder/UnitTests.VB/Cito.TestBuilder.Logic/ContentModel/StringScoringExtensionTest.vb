
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring

<TestClass>
Public Class StringScoringExtensionTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getting_ScoreParameter_OnEmptySolution_WillAddKeyFinding()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "String"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim solution As New Solution()
        
        'Act
        Dim manipulator As IGapScoringManipulator(Of String) = param.GetScoreManipulator(solution)
        manipulator.SetHowToScoreMethod(EnumScoringMethod.Dichotomous) 'Due to a change the finding will not be directly created.
        
        'Assert
        Assert.AreEqual(1, solution.Findings.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Sub Test_FindingOverride_SetOrNotSet()
        'Arrange
        Dim paramOverrideSet = New StringScoringParameter() With {.ControllerId = "MC", .FindingOverride = "overriden_finding_id"}
        Dim paramOverrideNotSet = New StringScoringParameter() With {.ControllerId = "MC"}
        Dim solutionOverrideSet As New Solution()
        Dim solutionOverrideNotSet As New Solution()

        'Act
        Dim manipulator1 = paramOverrideSet.GetScoreManipulator(solutionOverrideSet)
        Dim manipulator2 = paramOverrideNotSet.GetScoreManipulator(solutionOverrideNotSet)

        manipulator1.SetHowToScoreMethod(EnumScoringMethod.Dichotomous) 'Due to a change the finding will not be directly created.
        manipulator2.SetHowToScoreMethod(EnumScoringMethod.Dichotomous) 'Due to a change the finding will not be directly created.

        'Assert
        Assert.AreEqual("overriden_finding_id", solutionOverrideSet.Findings.First().Id)
        Assert.AreEqual("MC", solutionOverrideNotSet.Findings.First().Id)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Getting_ScoreParameter_OnEmptySolution_KeysAreSetToSingleArrayString()
        'Arrange
        Dim param = New StringScoringParameter() With {.ControllerId = "String"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim solution As New Solution()
        Dim manipulator As IGapScoringManipulator(Of String) = param.GetScoreManipulator(solution)
        
        'Act
        Dim result = manipulator.GetkeyStatus()
        
        'Assert
        Assert.AreEqual(3, result.Keys.Count)
        Assert.AreEqual(1, result("A").Count())
        Assert.AreEqual(1, result("B").Count())
        Assert.AreEqual(1, result("C").Count())
    End Sub

End Class
