
Imports Cito.Tester.ContentModel.Datasources

<TestClass()>
Public Class ResourceRefTest

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ResourceRef_TwoDifferentInstancesSameId_AreEquals()
        Dim r1 As New ResourceRef("id123")
        Dim r2 As New ResourceRef("id123")

        Assert.IsFalse(ReferenceEquals(r1, r2))
        Assert.AreEqual(r1, r2)
        Assert.AreEqual(r1.GetHashCode(), r2.GetHashCode())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ResourceRef_TwoDifferentInstancesSameOtherId_AreEquals()
        Dim r1 As New ResourceRef("id123")
        Dim r2 As New ResourceRef("321id")

        Assert.IsFalse(ReferenceEquals(r1, r2))
        Assert.AreNotEqual(r1, r2)
    End Sub

End Class
