
Imports Questify.Builder.Logic.ContentModel.Scoring

<TestClass>
Public Class ConceptIdSorterTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub SortSimpleList_ShouldBeAlphabetical()
        Dim lst = New List(Of String)()
        lst.Add("A")
        lst.Add("B")
        lst.Add("C")

        lst.Sort(New ConceptIdSorter())

        Assert.AreEqual("A", lst(0))
        Assert.AreEqual("B", lst(1))
        Assert.AreEqual("C", lst(2))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub SortListWithAnswerCategoryId_ShouldBeAlphabetical()
        Dim lst = New List(Of String)()
        lst.Add("A[1]")
        lst.Add("A[2]")
        lst.Add("A[3]")

        lst.Sort(New ConceptIdSorter())

        Assert.AreEqual("A[1]", lst(0))
        Assert.AreEqual("A[2]", lst(1))
        Assert.AreEqual("A[3]", lst(2))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub SortListWithAnswerCategoryIdAndCatchAll()
        Dim lst = New List(Of String)()
        lst.Add("A[1]")
        lst.Add("A[2]")
        lst.Add("A[*]")
        lst.Add("A[3]")

        lst.Sort(New ConceptIdSorter())

        Assert.AreEqual("A[1]", lst(0))
        Assert.AreEqual("A[2]", lst(1))
        Assert.AreEqual("A[3]", lst(2))
        Assert.AreEqual("A[*]", lst(3))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub SortListWithAnswerCategoryIdAndCatchAll_ShouldBeAlphabetical()
        Dim lst = New List(Of String)()
        lst.Add("A[1]")
        lst.Add("B[2]")
        lst.Add("A[*]")
        lst.Add("A[3]")

        lst.Sort(New ConceptIdSorter())

        Assert.AreEqual("A[1]", lst(0))
        Assert.AreEqual("A[3]", lst(1))
        Assert.AreEqual("B[2]", lst(2))
        Assert.AreEqual("A[*]", lst(3))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub SortListWithTwoKeysAndAnserCatagories_ListShouldStartWithRegular()
        Dim lst = New List(Of String)()
        lst.Add("B[*]")
        lst.Add("A[1]")
        lst.Add("B")
        lst.Add("A[*]")
        lst.Add("B[0]")
        lst.Add("A")
        lst.Add("A[3]")

        lst.Sort(New ConceptIdSorter())

        Assert.AreEqual("A", lst(0))
        Assert.AreEqual("B", lst(1))
        Assert.AreEqual("A[1]", lst(2))
        Assert.AreEqual("A[3]", lst(3))
        Assert.AreEqual("B[0]", lst(4))
        Assert.AreEqual("A[*]", lst(5))
        Assert.AreEqual("B[*]", lst(6))
    End Sub

End Class
