
Imports Cito.Tester.ContentModel

<TestClass()> Public Class KeyFactTest

    <TestMethod()> Public Sub DefaultScore_Equals_One()
        'Arrange

        'Act
        Dim keyFact As New KeyFact()

        'Assert
        Assert.AreEqual(1, keyFact.Score)
    End Sub

End Class