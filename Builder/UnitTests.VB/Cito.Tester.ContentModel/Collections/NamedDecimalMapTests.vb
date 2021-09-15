
Imports System.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class NamedDecimalMapTests

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub NewObjectShouldBeValid()
        'Arrange
        Dim map = New NamedDecimalMap()
        
        'Act
        Dim result = map.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SkewedValuesShouldBeInvalid()
        'Arrange
        Dim map = New NamedDecimalMap()
        map.SetValuesFor("a", {1, 2, 3})
        map.SetValuesFor("b", {1, 2, 3, 4})
      
        'Act
        Dim result = map.IsValid()
       
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub SkewedValuesShouldThrowWhenToStringIsCalled()
        'Arrange
        Dim map = New NamedDecimalMap()
        map.SetValuesFor("a", {1, 2, 3})
        map.SetValuesFor("b", {1, 2, 3, 4})
       
        'Act
        map.ToString()
       
        'Assert
        Assert.Fail()
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub PropperValuesShouldGiveExpectedResult()
        ' a 4 9 2
        ' b 3 5 7
        ' c 8 1 6
        'Arrange
        Dim map = New NamedDecimalMap()
        map.SetValuesFor("a", {4, 9, 2})
        map.SetValuesFor("b", {3, 5, 7})
        map.SetValuesFor("c", {8, 1, 6})
      
        'Act
        Dim result = map.ToString()
       
        'Assert
        Assert.AreEqual("{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}", result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromDecimal()
        'Arrange
        Dim map = NamedDecimalMap.FromString("{a:4.1}")
       
        'Act
        Dim result = map.GetValuesFor("a")
      
        'Assert
        Assert.IsTrue(result.SequenceEqual(New Decimal() {4.1D}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromNegativeDecimal()
        'Arrange
        Dim map = NamedDecimalMap.FromString("{a:-4.1}")
       
        'Act
        Dim result = map.GetValuesFor("a")
      
        'Assert
        Assert.IsTrue(result.SequenceEqual(New Decimal() {-4.1D}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromSet()
        'Arrange
        Dim map = NamedDecimalMap.FromString("{a:4,b:3,c:8}")
        
        'Act
        Dim result = map.GetNames()
       
        'Assert
        Assert.IsTrue(result.SequenceEqual(New String() {"a", "b", "c"}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromSets()
        'Arrange
        Dim map = NamedDecimalMap.FromString("{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}")
       
        'Act
        Dim result = map.GetValuesFor("a")
       
        'Assert
        Assert.IsTrue(result.SequenceEqual(New Decimal() {4, 9, 2}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetObjectFromSetsWithFractions()
        'Arrange
        Dim map = NamedDecimalMap.FromString("{a:4.3,b:3.131235,c:8.73}{a:9.8523,b:5.46,c:1.1002}{a:2.003,b:7.1415,c:6.7789}")
        
        'Act
        Dim result = map.GetValuesFor("b")
       
        'Assert
        Assert.IsTrue(result.SequenceEqual(New Decimal() {3.131235D, 5.46D, 7.1415D}))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub GetObjectFromSkewedSets()
        'Arrange
        Dim map = NamedDecimalMap.FromString("{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}{a:1,b:1}")
        
        'Act
        Dim result = map.GetValuesFor("a")
        
        'Assert
        Assert.IsTrue(result.SequenceEqual(New Decimal() {4, 9, 2}))
    End Sub

End Class