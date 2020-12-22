
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class KeyFindingCollectionTest

    <TestMethod()> <TestCategory("ContentModel")> <TestCategory("KeyFinding")>
    Public Sub TestSort()
        Dim kfc = New KeyFindingCollection()
        kfc.Add(New KeyFinding("C"))
        kfc.Add(New KeyFinding("A"))
        kfc.Add(New KeyFinding("B"))

        kfc.Sort(Function(findingA, findingB)
                     Return String.Compare(findingA.Id, findingB.Id, StringComparison.Ordinal)
                 End Function)

        Assert.AreEqual("A", kfc(0).Id)
        Assert.AreEqual("B", kfc(1).Id)
        Assert.AreEqual("C", kfc(2).Id)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <TestCategory("KeyFinding")>
    Public Sub TestBindById()
        Dim kfc = New KeyFindingCollection()
        kfc.Add(New KeyFinding("C"))
        kfc.Add(New KeyFinding("A"))
        kfc.Add(New KeyFinding("B"))

        Dim result = kfc.FindById("A")

        Assert.AreEqual("A", result.Id)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <TestCategory("KeyFinding")>
    Public Sub TestBindById_WithNull_ExpectsNull()
        Dim kfc = New KeyFindingCollection()
        kfc.Add(New KeyFinding("C"))
        kfc.Add(New KeyFinding("A"))
        kfc.Add(New KeyFinding("B"))

        Dim result = kfc.FindById(Nothing)

        Assert.IsNull(result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <TestCategory("KeyFinding")>
    Public Sub KeyFindCollectionWithOneElement_Clear_ElementShouldNotBeInCollectionAnymore()
        Dim kfc = New KeyFindingCollection()
        kfc.Add(New KeyFinding("A"))

        kfc.Clear()

        Assert.IsFalse(kfc.Contains("A"), "Element A should not be in the collection anymore because we cleared it. Basic list stuff.")
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <TestCategory("KeyFinding")>
    Public Sub RemoveAForm_KeyFindingCollection_ShouldNotContains()
        Dim kfc = New KeyFindingCollection()
        kfc.Add(New KeyFinding("A"))
        kfc.Add(New KeyFinding("B"))
        kfc.Add(New KeyFinding("C"))

        kfc.Remove(kfc(0))

        Assert.IsFalse(kfc.Contains("A"))
        Assert.IsTrue(kfc.Contains("B"))
        Assert.IsTrue(kfc.Contains("C"))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <TestCategory("KeyFinding")>
    Public Sub RemoveFromCollectionCasted_ShouldNotContainA()
        Dim kfc = New KeyFindingCollection()
        kfc.Add(New KeyFinding("A"))
        kfc.Add(New KeyFinding("B"))
        kfc.Add(New KeyFinding("C"))

        Dim collection = DirectCast(kfc, ICollection(Of KeyFinding))
        collection.Remove(kfc(0))

        Assert.IsFalse(kfc.Contains("A"))
        Assert.IsTrue(kfc.Contains("B"))
        Assert.IsTrue(kfc.Contains("C"))
    End Sub

End Class
