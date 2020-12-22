
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class MatrixScoringManipulatorTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub SetKeyForARow_ShouldCreateFactForEachRow()
        Dim param = CreateMatrixScoringParameter("matrix", 2, 3)
        Dim solution As New Solution()

        Dim manipulator = DirectCast(param.GetScoreManipulator(solution), IChoiceArrayScoringManipulator)
        manipulator.SetKey("1", "A")
        manipulator.SetKey("2", "C")

        Assert.AreEqual(1, solution.Findings.Count)
        Assert.AreEqual(2, solution.Findings(0).Facts.Count)
        Assert.AreEqual("1-matrix", solution.Findings(0).Facts(0).Id)
        Assert.AreEqual(1, solution.Findings(0).Facts(0).Values.Count)
        Assert.AreEqual("A", DirectCast(DirectCast(solution.Findings(0).Facts(0).Values(0), KeyValue).Values(0), StringValue).Value)
        Assert.AreEqual("2-matrix", solution.Findings(0).Facts(1).Id)
        Assert.AreEqual(1, solution.Findings(0).Facts(1).Values.Count)
        Assert.AreEqual("C", DirectCast(DirectCast(solution.Findings(0).Facts(1).Values(0), KeyValue).Values(0), StringValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ScoreDisplayValueForMatrix()
        Dim param = CreateMatrixScoringParameter("matrix", 2, 3)
        Dim solution As New Solution()

        Dim manipulator = DirectCast(param.GetScoreManipulator(solution), IChoiceArrayScoringManipulator)
        manipulator.SetKey("1", "A")
        manipulator.SetKey("2", "C")

        solution.WriteToDebug("pp")
        Dim keyValueString = New ScoringDisplayValueCalculator({param}, solution).GetScoreDisplayValue()

        keyValueString = ScoringPropertiesCalculator.GetKeyValuesAsString(solution, 0)

        Assert.AreEqual("A&C", keyValueString)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), ExpectedException(GetType(KeyNotFoundException))>
    Public Sub SetKeyForANonExistingRow_ShouldThrowException()
        Dim param = CreateMatrixScoringParameter("matrix", 2, 3)
        Dim solution As New Solution()

        Dim manipulator = DirectCast(param.GetScoreManipulator(solution), IChoiceArrayScoringManipulator)
        manipulator.SetKey("3", "A")

        Assert.Fail()
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), ExpectedException(GetType(KeyNotFoundException))>
    Public Sub SetKeyForANonExistinColumn_ShouldThrowException()
        Dim param = CreateMatrixScoringParameter("matrix", 2, 3)
        Dim solution As New Solution()

        Dim manipulator = DirectCast(param.GetScoreManipulator(solution), IChoiceArrayScoringManipulator)
        manipulator.SetKey("1", "D")

        Assert.Fail()
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub DomainForEachMatrixShouldBeIdentifiable()
        Dim param = CreateMatrixScoringParameter(controllerId:="matrix", rows:=2, columns:=3)
        Dim solution As New Solution()

        Dim manipulator = DirectCast(param.GetScoreManipulator(solution), IChoiceArrayScoringManipulator)
        manipulator.SetKey("1", "B")
        manipulator.SetKey("2", "C")

        Dim result1 = solution.Findings.Single().Facts.First().Values.Single()
        Dim result2 = solution.Findings.Single().Facts.Skip(1).First.Values.Single()

        Assert.AreEqual("matrix1", result1.Domain)
        Assert.AreEqual("matrix2", result2.Domain)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    <ExpectedException(GetType(KeyNotFoundException))>
    Public Sub SettingIncorrectSolutionShouldThrowAnException_1()
        Dim param = CreateMatrixScoringParameter(controllerId:="matrix", rows:=2, columns:=3)
        Dim solution As New Solution()

        Dim manipulator = DirectCast(param.GetScoreManipulator(solution), IChoiceArrayScoringManipulator)
        manipulator.SetKey("B", "1")

    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    <ExpectedException(GetType(KeyNotFoundException))>
    Public Sub SettingIncorrectSolutionShouldThrowAnException_2()
        Dim param = CreateMatrixScoringParameter(controllerId:="matrix", rows:=2, columns:=3)
        Dim solution As New Solution()

        Dim manipulator = DirectCast(param.GetScoreManipulator(solution), IChoiceArrayScoringManipulator)
        manipulator.SetKey("1", "Z")

    End Sub


    Private Function CreateMatrixScoringParameter(controllerId As String, rows As Integer, columns As Integer) As MatrixScoringParameter
        Dim param As New MatrixScoringParameter() With {.ControllerId = controllerId}
        param.Value = New ParameterSetCollection()

        For index As Integer = 1 To rows
            Dim rowParam As New ParameterCollection()
            rowParam.Id = index.ToString()
            param.Value.Add(rowParam)
        Next
        param.MatrixColumnsDefinition = New MultiChoiceScoringParameter()
        param.MatrixColumnsDefinition.MaxChoices = 1
        param.MatrixColumnsDefinition.MinChoices = 1

        Dim subParamIds As New List(Of String)
        For index As Integer = 1 To columns
            subParamIds.Add(Chr(64 + index))
        Next
        param.MatrixColumnsDefinition.AddSubParameters(subParamIds.ToArray())
        Return param
    End Function

End Class
