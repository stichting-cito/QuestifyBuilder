
Imports Questify.Builder.Logic.ContentModel

<TestClass()>
Public Class DefaultStringOperationsTests

    <TestMethod(), TestCategory("Logic")>
    Public Sub KnowIdsShouldMatch1()
        'Arrange
        Dim s1 = "1-test"
        Dim s2 = "1[x]-test"
        
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub KnowIdsShouldNotMatch1()
        'Arrange
        Dim s1 = "1-test"
        Dim s2 = "2[x]-test"
        
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub KnowIdsShouldMatch2()
        'Arrange
        Dim s1 = "1-test"
        Dim s2 = "1-test"
        
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub KnowIdsShouldNotMatch2()
        'Arrange
        Dim s1 = "1-test"
        Dim s2 = "2-test"
       
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldEqual3()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
       
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-456d-b3d3-c92265282cf8"
        
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3_2()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "2-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
       
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
       
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldNotEqual3_3()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Icf32c0ba-73db-456d-b3d3-c92265282cf7"
       
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3_4()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-63db-456d-b3d3-c92265282cf7"
        
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
       
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3_5()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-356d-b3d3-c92265282cf7"
       
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
       
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3_6()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-456d-C3d3-c92265282cf7"
       
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
       
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub CatchAllIdIsSame()
        'Arrange
        Dim s1 = "1[*]-integerScore"
        Dim s2 = "1-integerScore"
        
        'Act
        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)
        
        'Assert
        Assert.IsTrue(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub ScoringMapKeyTest_A()
        'Arrange
        Dim key = "A"
        
        'Act
        Dim result = DefaultStringOperations.GetSubParameterId(key)
        
        'Assert
        Assert.AreEqual("A", result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub ScoringMapKeyTest_A_AnswerCategory()
        'Arrange
        Dim key = "A[1]"
        
        'Act
        Dim result = DefaultStringOperations.GetSubParameterId(key)
        
        'Assert
        Assert.AreEqual("A", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ScoringMapKeyTest_A_CatchAll()
        'Arrange
        Dim key = "A[*]"
        
        'Act
        Dim result = DefaultStringOperations.GetSubParameterId(key)
        
        'Assert
        Assert.AreEqual("A", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetAnswerCategoryId_ShouldReturn1()
        'Arrange
        Dim key = "A[1]"
        
        'Act
        Dim result = DefaultStringOperations.GetAnswerCategoryId(key)
        
        'Assert
        Assert.AreEqual("[1]", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetAnswerCategoryId_ShouldReturnZ()
        'Arrange
        Dim key = "A[Z]"
        
        'Act
        Dim result = DefaultStringOperations.GetAnswerCategoryId(key)
        
        'Assert
        Assert.AreEqual("[Z]", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetAnswerCategoryId_ShouldReturnStar()
        'Arrange
        Dim key = "A[*]"
        
        'Act
        Dim result = DefaultStringOperations.GetAnswerCategoryId(key)
        
        'Assert
        Assert.AreEqual("[*]", result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub GetAnswerCategoryId_Empty()
        'Arrange
        Dim key = "A"
        
        'Act
        Dim result = DefaultStringOperations.GetAnswerCategoryId(key)
        
        'Assert
        Assert.AreEqual(String.Empty, result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub IsCatchAllFactId_ShouldReturnFalse()
        'Arrange
        Dim key = "A[1]"
        
        'Act
        Dim result = DefaultStringOperations.IsCatchAllFactId(key)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub IsCatchAllFactId_ShouldReturnZ()
        'Arrange
        Dim key = "A[Z]"
        
        'Act
        Dim result = DefaultStringOperations.IsCatchAllFactId(key)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub IsCatchAllFactId_ShouldReturnStar()
        'Arrange
        Dim key = "A[*]"
        
        'Act
        Dim result = DefaultStringOperations.IsCatchAllFactId(key)
        
        'Assert
        Assert.IsTrue(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub IsCatchAllFactId_Empty()
        'Arrange
        Dim key = "A"
        
        'Act
        Dim result = DefaultStringOperations.IsCatchAllFactId(key)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetNumberFromFactId_FromIdThatIsNotAnAnswerCategory()
        'Arrange
        Dim key = "A"
        
        'Act
        Dim result = DefaultStringOperations.GetNumberFromFactId(key)
        
        'Assert
        Assert.IsNull(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub GetNumberFromFactId_Expects_1()
        'Arrange
        Dim key = "A[1]"
        
        'Act
        Dim result = DefaultStringOperations.GetNumberFromFactId(key)
        
        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetNumberFromFactId_Expects_599()
        'Arrange
        Dim key = "A[599]"
        
        'Act
        Dim result = DefaultStringOperations.GetNumberFromFactId(key)
        
        'Assert
        Assert.AreEqual(599, result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetNumberFromFactId_NegativeNumber_Expects_Null()
        'Arrange
        Dim key = "A[-1]"
        
        'Act
        Dim result = DefaultStringOperations.GetNumberFromFactId(key)
        
        'Assert
        Assert.IsNull(result)
    End Sub

End Class
