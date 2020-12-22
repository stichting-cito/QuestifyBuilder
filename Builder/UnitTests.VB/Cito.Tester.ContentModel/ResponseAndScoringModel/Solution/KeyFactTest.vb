
Imports Cito.Tester.ContentModel

<TestClass()> Public Class KeyFactTest

    <TestMethod()> Public Sub DefaultScore_Equals_One()

        Dim keyFact As New KeyFact()

        Assert.AreEqual(1, keyFact.Score)
    End Sub

End Class