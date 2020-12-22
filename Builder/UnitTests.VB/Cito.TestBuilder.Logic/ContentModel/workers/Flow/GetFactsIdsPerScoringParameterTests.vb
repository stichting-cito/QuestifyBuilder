
Imports System.Activities
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.workers.Flow
Imports System.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class GetFactsIdsPerScoringParameterTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDictionaryOfFactIds()
        Dim sp = New IntegerScoringParameter() With {.ControllerId = "Some_ControllerID"}.AddSubParameters("A", "B")
        Dim inputs As New Dictionary(Of String, Object) From {{"ScoringParameters", New ScoringParameter() {sp}}}

        Dim result = WorkflowInvoker.Invoke(New GetFactsIdsPerScoringParameter(), inputs)

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual("A-Some_ControllerID", result.Keys.ToList()(0))
        Assert.AreEqual("B-Some_ControllerID", result.Keys.ToList()(1))
    End Sub

End Class
