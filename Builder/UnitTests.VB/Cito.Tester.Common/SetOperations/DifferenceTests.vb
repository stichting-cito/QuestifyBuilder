
Imports Cito.Tester.Common

<TestClass()>
Public Class DifferenceTests

    <TestMethod>
    <Description("Test workings as advertised")>
    <TestCategory("Sets")>
    Public Sub ValidateInnerWorkingsAsAdvertised()
        'Arrange
        'Set difference of U and A, denoted U \ A
        'The set difference {1,2,3} \ {2,3,4} is {1}, while, conversely, the set difference {2,3,4} \ {1,2,3} is {4}.

        Dim setA() As Integer = New Integer() {1, 2, 3}
        Dim setB() As Integer = New Integer() {2, 3, 4}

        'Act
        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        'Assert
        Assert.AreEqual(Of Integer)(1, result.Count) 'Test nr of elements
        Assert.AreEqual(Of Integer)(1, result(0)) 'Should be 1
    End Sub

    <TestMethod>
    <Description("Test workings as advertised")>
    <TestCategory("Sets")>
    Public Sub ValidateInnerWorkingsAsAdvertised_alternative2()
        'Arrange
        'Set difference of U and A, denoted U \ A
        'The set difference {1,2,3} \ {2,3,4} is {1}, while, conversely, the set difference {2,3,4} \ {1,2,3} is {4}.
        '[Testing] conversely, the set difference {2,3,4} \ {1,2,3} is {4}.

        Dim setA() As Integer = New Integer() {2, 3, 4}
        Dim setB() As Integer = New Integer() {1, 2, 3}

        'Act
        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        'Assert
        Assert.AreEqual(Of Integer)(1, result.Count) 'Test nr of elements
        Assert.AreEqual(Of Integer)(4, result(0)) 'Should be 4
    End Sub


    <TestMethod()>
    <Description("Test with non Intersecting Sets")>
    <TestCategory("Sets")>
    Public Sub DifferenceBetweenNonInterSectingSets_ShouldBeSetA()
        'Arrange
        'Test implementation according to: http://en.wikipedia.org/wiki/Set_(mathematics)
        '{1, 2} \ {red, white} = {1, 2}.

        Dim setA() As Integer = New Integer() {1, 2}
        Dim setB() As Integer = New Integer() {100, 200}

        'Act
        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        'Assert
        Assert.AreEqual(Of Integer)(2, result.Count) 'Test nr of elements
        Assert.AreEqual(Of Integer)(1, result(0))
        Assert.AreEqual(Of Integer)(2, result(1))
    End Sub

    <TestMethod()>
    <Description("There should be no difference between same sets.")>
    <TestCategory("Sets")>
    Public Sub DifferenceBetweenSameSet_ShouldBeEmpty()
        'Arrange
        'Test implementation according to: http://en.wikipedia.org/wiki/Set_(mathematics)
        '{1, 2} \ {1, 2} = ∅.

        Dim setA() As Integer = New Integer() {1, 2}
        Dim setB() As Integer = New Integer() {1, 2}

        'Act
        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        'Assert
        Assert.AreEqual(Of Integer)(0, result.Count) 'Test nr of elements
    End Sub


    <TestMethod()>
    <TestCategory("Sets")>
    Public Sub DifferenceBetweenSetA_and_EmptySet_ShouldBeSetA()
        'Arrange
        'Test implementation according to: http://en.wikipedia.org/wiki/Set_(mathematics)
        '{1, 2} \ ∅ = {1, 2}.

        Dim setA() As Integer = New Integer() {1, 2}
        Dim setB() As Integer = New Integer() {}

        'Act
        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        'Assert
        Assert.AreEqual(Of Integer)(2, result.Count) '
    End Sub


    <TestMethod()>
    <TestCategory("Sets")>
    Public Sub DifferenceBetweenEmptySet_and_AnySet_ShouldBeEmpty()
        'Arrange
        'Test implementation according to: http://en.wikipedia.org/wiki/Set_(mathematics)
        '∅ \ {1, 2}= {1, 2}.

        Dim setA() As Integer = New Integer() {}
        Dim setB() As Integer = New Integer() {1, 2, 3, 4, 5, 6}

        'Act
        Dim result As IList(Of Integer)
        result = SetOperations.Difference(setA, setB, New IntegerEqualityComparer())

        'Assert
        Assert.AreEqual(Of Integer)(0, result.Count) '
    End Sub

End Class
