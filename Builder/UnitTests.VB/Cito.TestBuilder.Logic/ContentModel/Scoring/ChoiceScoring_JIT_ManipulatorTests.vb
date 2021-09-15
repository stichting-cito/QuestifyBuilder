
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.HelperFunctions

<TestClass>
Public Class ChoiceScoring_JIT_ManipulatorTests
    Inherits ChoiceScoringManipulatorTests

    Private solution As Solution

    <TestInitialize>
    Public Sub Init()
        solution = New Solution()
    End Sub

    <TestCleanup>
    Public Sub Clean()
        solution = Nothing
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetKeys_SolutionShouldNotBeAlterd()
        'Arrange
        Dim param = New ChoiceScoringParameter() With {.ControllerId = "MC", .MaxChoices = 1}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator As IChoiceScoringManipulator = New ChoiceScoringManipulator(GetKeyManipulator(MCKeyFinding("MC")), param)
        
        'Act
        Dim res = manipulator.GetKeyStatus()
        
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(0, solution.Findings.Count)
        Assert.AreEqual(3, res.Count)
    End Sub

    Friend Overrides Function GetKeyManipulator(key As KeyFinding) As FindingManipulatorBase
        Dim jit = New CreateObjectJIT(Of KeyFinding)(Nothing, Function()
                                                                  Dim finding = key
                                                                  finding.Id = ""
                                                                  solution.Findings.Add(finding)
                                                                  Return finding
                                                              End Function)

        Return New KeyManipulator(jit)
    End Function

End Class
