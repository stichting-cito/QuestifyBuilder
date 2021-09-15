
Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.IO
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq

<TestClass>
Public Class ScoreParameterSolutionFixerIntergrationTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ValidSolution_NotModified()
        'Arrange
        Dim solution As New Solution
        Dim scoreParameterValueId As String = "A"
        Dim integerPrm = CreateIntegerScoreParam("SomeController", scoreParameterValueId)
        Dim scoreManipulator = integerPrm.GetScoreManipulator(solution)

        scoreManipulator.SetKey(scoreParameterValueId, 10) 'Set some score
        WriteSolution("Arrange", solution)

        'Act        -- !! INTEGRATION !! --
        solution.FixRemovedScoringParameters({integerPrm})

        'Assert
        WriteSolution("Assert", solution)

        Dim VerifyManipulator = integerPrm.GetScoreManipulator(solution)
        Dim result = VerifyManipulator.GetKeysAlreadyManipulated().ToArray()

        Assert.AreEqual(scoreParameterValueId, result(0))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub RemoveScoringParameter_SolutionIsFixed()
        'Arrange
        Dim solution As New Solution
        Dim scoreParameterValueId As String = "A"
        Dim integerPrm = CreateIntegerScoreParam("SomeController", scoreParameterValueId)
        Dim integerPrm2 = CreateIntegerScoreParam("SomeController2", scoreParameterValueId)
        Dim integerPrm3 = CreateIntegerScoreParam("SomeController3", scoreParameterValueId)

        Dim scoreManipulator = integerPrm.GetScoreManipulator(solution)
        scoreManipulator.SetKey(scoreParameterValueId, 10) 'Set some score

        scoreManipulator = integerPrm2.GetScoreManipulator(solution)
        scoreManipulator.SetKey(scoreParameterValueId, 11) 'Set some score

        scoreManipulator = integerPrm3.GetScoreManipulator(solution)
        scoreManipulator.SetKey(scoreParameterValueId, 12) 'Set some score

        WriteSolution("Arrange", solution)

        'Act        -- !! INTEGRATION !! --
        solution.FixRemovedScoringParameters({integerPrm, integerPrm3})

        'Assert
        WriteSolution("Assert", solution)

        Dim verifyManipulator = integerPrm.GetScoreManipulator(solution)
        Dim verifyManipulator2 = integerPrm2.GetScoreManipulator(solution)
        Dim verifyManipulator3 = integerPrm3.GetScoreManipulator(solution)

        Assert.AreEqual("A", verifyManipulator.GetKeysAlreadyManipulated()(0))
        Assert.AreEqual(0, verifyManipulator2.GetKeysAlreadyManipulated().Count, "It was expected no keys ware found for 'integerPrm2'")
        Assert.AreEqual("A", verifyManipulator3.GetKeysAlreadyManipulated()(0))

    End Sub

#Region "Helpers"

    Private Function CreateIntegerScoreParam(controllerId As String, scoreParameterValueId As String, Optional findingName As String = "finding") As IntegerScoringParameter
        Dim fieldA As New IntegerScoringParameter With {.ControllerId = controllerId, .FindingOverride = findingName}
        Dim subParameterSet As New ParameterSetCollection()
        Dim parameterCollection = New ParameterCollection() With {.Id = scoreParameterValueId}
        subParameterSet.Add(parameterCollection)
        fieldA.Value = subParameterSet

        Return fieldA
    End Function

    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)
            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

#End Region

End Class
