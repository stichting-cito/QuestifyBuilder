
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel

<TestClass>
Public Class ChoiceScoringManipulator_ExistingValues_Tests
    Inherits ChoiceScoringManipulatorTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetKey_A_ShouldBeSet()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})

        Dim key = MCKeyFinding("MC", "A")
        Dim manipulator As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), param)

        Dim res = manipulator.GetKeyStatus()

        Write("Assert", key)
        Assert.IsTrue(res.ContainsKey("A"), "Key is not contained")
        Assert.IsTrue(res("A"), "Key is not set!")
        Assert.AreEqual(1, res.Keys.Count)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetKey_AD_ShouldBeSet()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim key = MCKeyFinding("MC", "A", "D")
        Dim manipulator As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), param)

        Dim res = manipulator.GetKeyStatus()

        Write("Assert", key)
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.IsTrue(res("A"))

        Assert.IsTrue(res.ContainsKey("D"))
        Assert.IsTrue(res("D"))
        Assert.AreEqual(2, res.Keys.Count)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetKey_SolutionHas_AD_ParamHasABC_ShouldBeSet()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim key = MCKeyFinding("MC", "A", "D")
        Dim manipulator As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), param)

        Dim res = manipulator.GetKeyStatus()

        Write("Assert", key)

        Assert.IsTrue(res.ContainsKey("A"))
        Assert.IsTrue(res("A"))
        Assert.IsFalse(res.ContainsKey("D"))
        Assert.AreEqual(3, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub UnSetKey_D_From_AD_Expects_A_toRemain()
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "D"})

        Dim key = MCKeyFinding("MC", "A", "D")
        Dim manipulator As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(key), param)

        Write("Arrange", key)

        manipulator.RemoveKey("D")

        Write("Assert", key)
        Dim res = manipulator.GetKeyStatus()
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.IsTrue(res("A"))

        Assert.IsTrue(res.ContainsKey("D"))
        Assert.IsFalse(res("D"))
        Assert.AreEqual(2, res.Keys.Count)
    End Sub

    Friend Overrides Function GetKeyManipulator(key As KeyFinding) As FindingManipulatorBase
        Return New KeyManipulator(key)
    End Function

End Class
