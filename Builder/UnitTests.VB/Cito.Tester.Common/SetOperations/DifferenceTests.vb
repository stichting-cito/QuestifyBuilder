
Imports Cito.Tester.Common

<TestClass()>
Public Class DifferenceTests

    <TestMethod>
    <Description("Test workings as advertised")>
    <TestCategory("Sets")>
    Public Sub ValidateInnerWorkingsAsAdvertised()

        Dim setA() As Integer = New Integer() {1, 2, 3}
        Dim setB() As Integer = New Integer() {2, 3, 4}

        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        Assert.AreEqual(Of Integer)(1, result.Count)
        Assert.AreEqual(Of Integer)(1, result(0))
    End Sub

    <TestMethod>
    <Description("Test workings as advertised")>
    <TestCategory("Sets")>
    Public Sub ValidateInnerWorkingsAsAdvertised_alternative2()

        Dim setA() As Integer = New Integer() {2, 3, 4}
        Dim setB() As Integer = New Integer() {1, 2, 3}

        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        Assert.AreEqual(Of Integer)(1, result.Count)
        Assert.AreEqual(Of Integer)(4, result(0))
    End Sub


    <TestMethod()>
    <Description("Test with non Intersecting Sets")>
    <TestCategory("Sets")>
    Public Sub DifferenceBetweenNonInterSectingSets_ShouldBeSetA()

        Dim setA() As Integer = New Integer() {1, 2}
        Dim setB() As Integer = New Integer() {100, 200}

        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        Assert.AreEqual(Of Integer)(2, result.Count)
        Assert.AreEqual(Of Integer)(1, result(0))
        Assert.AreEqual(Of Integer)(2, result(1))
    End Sub

    <TestMethod()>
    <Description("There should be no difference between same sets.")>
    <TestCategory("Sets")>
    Public Sub DifferenceBetweenSameSet_ShouldBeEmpty()

        Dim setA() As Integer = New Integer() {1, 2}
        Dim setB() As Integer = New Integer() {1, 2}

        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        Assert.AreEqual(Of Integer)(0, result.Count)
    End Sub


    <TestMethod()>
    <TestCategory("Sets")>
    Public Sub DifferenceBetweenSetA_and_EmptySet_ShouldBeSetA()

        Dim setA() As Integer = New Integer() {1, 2}
        Dim setB() As Integer = New Integer() {}

        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        Assert.AreEqual(Of Integer)(2, result.Count)
    End Sub


    <TestMethod()>
    <TestCategory("Sets")>
    Public Sub DifferenceBetweenEmptySet_and_AnySet_ShouldBeEmpty()

        Dim setA() As Integer = New Integer() {}
        Dim setB() As Integer = New Integer() {1, 2, 3, 4, 5, 6}

        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        Assert.AreEqual(Of Integer)(0, result.Count)
    End Sub

End Class
