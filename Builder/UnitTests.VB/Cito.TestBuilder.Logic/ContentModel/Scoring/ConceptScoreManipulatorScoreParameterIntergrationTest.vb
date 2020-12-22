
Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.IO
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ConceptScoreManipulatorScoreParameterIntergrationTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetListOfFactIds()
        Dim solution As New Solution
        Dim integerPrm = CreateIntegerScoreParam("SomeController")
        Dim scoreManipulator = integerPrm.GetScoreManipulator(solution)

        scoreManipulator.SetKey("A", 10)
        Dim conceptManipulator = CreateManipulator(integerPrm, solution)
        Dim result = conceptManipulator.GetConceptIds().ToList()

        WriteSolution("Assert", solution)
        Assert.AreEqual(1 + 1, result.Count)

        Assert.AreEqual("A", result(0))
        Assert.AreEqual("A-SomeController", solution.ConceptFindings(0).Facts(0).Id)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub TheIdReportedShouldBeUsable()
        Dim solution As New Solution
        Dim integerPrm = CreateIntegerScoreParam("SomeController")
        Dim scoreManipulator = integerPrm.GetScoreManipulator(solution) : scoreManipulator.SetKey("A", 10)
        Dim conceptManipulator = CreateManipulator(integerPrm, solution)

        Dim result = New List(Of String)
        For Each id As String In conceptManipulator.GetConceptIds()
            result.Add(conceptManipulator.GetDisplayValueForConceptId(id))
        Next

        WriteSolution("Assert", solution)
        Assert.AreEqual(1 + 1, result.Count)
        Assert.AreEqual("10", result(0))
    End Sub


    Private Function CreateIntegerScoreParam(controllerId As String, Optional findingName As String = "finding") As IntegerScoringParameter
        Dim fieldA = New IntegerScoringParameter() With {.ControllerId = controllerId, .FindingOverride = findingName}.AddSubParameters("A")
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


    Private Function CreateManipulator(prm As ScoringParameter, solution As Solution) As IConceptScoreManipulator
        Dim map = New ScoringMap(New ScoringParameter() {prm}, solution).GetMap()

        Dim combined = CombinedScoringMapKey.Create(map.First())

        Return combined.GetConceptManipulator(solution)
    End Function

End Class
