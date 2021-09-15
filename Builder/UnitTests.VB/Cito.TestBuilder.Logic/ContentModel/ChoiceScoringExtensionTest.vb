
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring

<TestClass>
Public Class ChoiceScoringExtensionTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Sub Getting_ScoreParameter_OnEmptySolution_WillAddKeyFinding()
        'Arrange
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim solution As New Solution()
        
        'Act
        Dim manipulator As IChoiceScoringManipulator = param.GetScoreManipulator(solution)
        manipulator.SetHowToScoreMethod(EnumScoringMethod.Dichotomous)   'Due to a change the finding will not be directly created.

        'Assert
        Assert.AreEqual(1, solution.Findings.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Sub SettingScore_A_WillAddKeyFinding()
        'Arrange
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim solution As New Solution()
        Dim manipulator As IChoiceScoringManipulator = param.GetScoreManipulator(solution)
        
        'Act
        manipulator.SetKey("A")
        
        'Assert
        Assert.AreEqual(1, DirectCast(solution.Findings(0).Facts(0).Values(0), KeyValue).Values.Count)
        Assert.AreEqual("A", DirectCast(DirectCast(solution.Findings(0).Facts(0).Values(0), KeyValue).Values(0), StringValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Sub Test_FindingOverride_SetOrNotSet()
        'Arrange
        Dim paramOverrideSet = New ChoiceScoringParameter() With {.ControllerId = "MC", .FindingOverride = "overriden_finding_id"}
        Dim paramOverrideNotSet = New ChoiceScoringParameter() With {.ControllerId = "MC"}
        Dim solutionOverrideSet As New Solution()
        Dim solutionOverrideNotSet As New Solution()

        'Act
        Dim manipulator1 As IChoiceScoringManipulator = paramOverrideSet.GetScoreManipulator(solutionOverrideSet)
        Dim manipulator2 As IChoiceScoringManipulator = paramOverrideNotSet.GetScoreManipulator(solutionOverrideNotSet)
        manipulator1.SetHowToScoreMethod(EnumScoringMethod.Dichotomous) 'Due to a change the finding will not be directly created.
        manipulator2.SetHowToScoreMethod(EnumScoringMethod.Dichotomous) 'Due to a change the finding will not be directly created.

        'Assert
        Assert.AreEqual("overriden_finding_id", solutionOverrideSet.Findings.First().Id)
        Assert.AreEqual("MC", solutionOverrideNotSet.Findings.First().Id)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    <ExpectedException(GetType(KeyNotFoundException))>
    Sub SettingScore_X__ThatDoesNotExistsOnParam_WillTHrow()
        'Arrange
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim solution As New Solution()
        Dim manipulator As IChoiceScoringManipulator = param.GetScoreManipulator(solution)
        
        'Act
        manipulator.SetKey("X")
        
        'Assert
        'Expects Exception
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Sub SettingScore_MaxChoices1_WillClearAndSet()
        'Arrange
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim solution As New Solution()
        Dim manipulator As IChoiceScoringManipulator = param.GetScoreManipulator(solution)
        
        'Act
        manipulator.SetKey("D")
        manipulator.SetKey("A")

        'Assert
        Assert.AreEqual(1, solution.Findings(0).Facts(0).Values.Count)
        Assert.AreEqual(1, DirectCast(solution.Findings(0).Facts(0).Values(0), KeyValue).Values.Count)
        Assert.AreEqual("A", DirectCast(DirectCast(solution.Findings(0).Facts(0).Values(0), KeyValue).Values(0), StringValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Sub SettingScore_MaxChoices2_WontClearAndSet()
        'Arrange
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 2}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim solution As New Solution()
        Dim manipulator As IChoiceScoringManipulator = param.GetScoreManipulator(solution)
        
        'Act
        manipulator.SetKey("D")
        manipulator.SetKey("A")

        'Assert
        Assert.AreEqual(2, solution.Findings(0).Facts.Count, "Should have 2 facts")

        Assert.AreEqual(1, solution.Findings(0).Facts(0).Values.Count)
        Assert.AreEqual(1, solution.Findings(0).Facts(1).Values.Count)

        Assert.AreEqual(1, DirectCast(solution.Findings(0).Facts(0).Values(0), KeyValue).Values.Count)
        Assert.AreEqual("D-MC", solution.Findings(0).Facts(0).Id)

        Assert.AreEqual(1, DirectCast(solution.Findings(0).Facts(1).Values(0), KeyValue).Values.Count)
        Assert.AreEqual("A-MC", solution.Findings(0).Facts(1).Id)
    End Sub

End Class
