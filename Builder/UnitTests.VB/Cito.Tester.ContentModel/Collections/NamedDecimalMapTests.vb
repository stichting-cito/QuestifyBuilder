
Imports System.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class NamedDecimalMapTests

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub NewObjectShouldBeValid()
        Dim map = New NamedDecimalMap()

        Dim result = map.IsValid()

        Assert.IsTrue(result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SkewedValuesShouldBeInvalid()
        Dim map = New NamedDecimalMap()
        map.SetValuesFor("a", {1, 2, 3})
        map.SetValuesFor("b", {1, 2, 3, 4})

        Dim result = map.IsValid()

        Assert.IsFalse(result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub SkewedValuesShouldThrowWhenToStringIsCalled()
        Dim map = New NamedDecimalMap()
        map.SetValuesFor("a", {1, 2, 3})
        map.SetValuesFor("b", {1, 2, 3, 4})

        map.ToString()

        Assert.Fail()
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub PropperValuesShouldGiveExpectedResult()
        Dim map = New NamedDecimalMap()
        map.SetValuesFor("a", {4, 9, 2})
        map.SetValuesFor("b", {3, 5, 7})
        map.SetValuesFor("c", {8, 1, 6})

        Dim result = map.ToString()

        Assert.AreEqual("{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}", result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromDecimal()
        Dim map = NamedDecimalMap.FromString("{a:4.1}")

        Dim result = map.GetValuesFor("a")

        Assert.IsTrue(result.SequenceEqual(New Decimal() {4.1D}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromNegativeDecimal()
        Dim map = NamedDecimalMap.FromString("{a:-4.1}")

        Dim result = map.GetValuesFor("a")

        Assert.IsTrue(result.SequenceEqual(New Decimal() {-4.1D}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromSet()
        Dim map = NamedDecimalMap.FromString("{a:4,b:3,c:8}")

        Dim result = map.GetNames()

        Assert.IsTrue(result.SequenceEqual(New String() {"a", "b", "c"}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromSets()
        Dim map = NamedDecimalMap.FromString("{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}")

        Dim result = map.GetValuesFor("a")

        Assert.IsTrue(result.SequenceEqual(New Decimal() {4, 9, 2}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromSetsWithFractions()
        Dim map = NamedDecimalMap.FromString("{a:4.3,b:3.131235,c:8.73}{a:9.8523,b:5.46,c:1.1002}{a:2.003,b:7.1415,c:6.7789}")

        Dim result = map.GetValuesFor("b")

        Assert.IsTrue(result.SequenceEqual(New Decimal() {3.131235D, 5.46D, 7.1415D}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub GetObjectFromSkewedSets()
        Dim map = NamedDecimalMap.FromString("{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}{a:1,b:1}")

        Dim result = map.GetValuesFor("a")

        Assert.IsTrue(result.SequenceEqual(New Decimal() {4, 9, 2}))
    End Sub

End Class