
Imports Questify.Builder.Logic.ContentModel

<TestClass()>
Public Class DefaultStringOperationsTests

    <TestMethod(), TestCategory("Logic")>
    Public Sub KnowIdsShouldMatch1()
        Dim s1 = "1-test"
        Dim s2 = "1[x]-test"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub KnowIdsShouldNotMatch1()
        Dim s1 = "1-test"
        Dim s2 = "2[x]-test"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub KnowIdsShouldMatch2()
        Dim s1 = "1-test"
        Dim s2 = "1-test"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub KnowIdsShouldNotMatch2()
        Dim s1 = "1-test"
        Dim s2 = "2-test"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldEqual3()
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-456d-b3d3-c92265282cf7"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3()
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-456d-b3d3-c92265282cf8"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3_2()
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "2-Ice32c0ba-73db-456d-b3d3-c92265282cf7"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldNotEqual3_3()
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Icf32c0ba-73db-456d-b3d3-c92265282cf7"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3_4()
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-63db-456d-b3d3-c92265282cf7"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3_5()
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-356d-b3d3-c92265282cf7"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub StringEqualsRegex_shouldNotEqual3_6()
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-456d-C3d3-c92265282cf7"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub CatchAllIdIsSame()
        Dim s1 = "1[*]-integerScore"
        Dim s2 = "1-integerScore"

        Dim result = DefaultStringOperations.FactIdEquals(s1, s2)

        Assert.IsTrue(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub ScoringMapKeyTest_A()
        Dim key = "A"

        Dim result = DefaultStringOperations.GetSubParameterId(key)

        Assert.AreEqual("A", result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub ScoringMapKeyTest_A_AnswerCategory()
        Dim key = "A[1]"

        Dim result = DefaultStringOperations.GetSubParameterId(key)

        Assert.AreEqual("A", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ScoringMapKeyTest_A_CatchAll()
        Dim key = "A[*]"

        Dim result = DefaultStringOperations.GetSubParameterId(key)

        Assert.AreEqual("A", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetAnswerCategoryId_ShouldReturn1()
        Dim key = "A[1]"

        Dim result = DefaultStringOperations.GetAnswerCategoryId(key)

        Assert.AreEqual("[1]", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetAnswerCategoryId_ShouldReturnZ()
        Dim key = "A[Z]"

        Dim result = DefaultStringOperations.GetAnswerCategoryId(key)

        Assert.AreEqual("[Z]", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetAnswerCategoryId_ShouldReturnStar()
        Dim key = "A[*]"

        Dim result = DefaultStringOperations.GetAnswerCategoryId(key)

        Assert.AreEqual("[*]", result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub GetAnswerCategoryId_Empty()
        Dim key = "A"

        Dim result = DefaultStringOperations.GetAnswerCategoryId(key)

        Assert.AreEqual(String.Empty, result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub IsCatchAllFactId_ShouldReturnFalse()
        Dim key = "A[1]"

        Dim result = DefaultStringOperations.IsCatchAllFactId(key)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub IsCatchAllFactId_ShouldReturnZ()
        Dim key = "A[Z]"

        Dim result = DefaultStringOperations.IsCatchAllFactId(key)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub IsCatchAllFactId_ShouldReturnStar()
        Dim key = "A[*]"

        Dim result = DefaultStringOperations.IsCatchAllFactId(key)

        Assert.IsTrue(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub IsCatchAllFactId_Empty()
        Dim key = "A"

        Dim result = DefaultStringOperations.IsCatchAllFactId(key)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetNumberFromFactId_FromIdThatIsNotAnAnswerCategory()
        Dim key = "A"

        Dim result = DefaultStringOperations.GetNumberFromFactId(key)

        Assert.IsNull(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub GetNumberFromFactId_Expects_1()
        Dim key = "A[1]"

        Dim result = DefaultStringOperations.GetNumberFromFactId(key)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetNumberFromFactId_Expects_599()
        Dim key = "A[599]"

        Dim result = DefaultStringOperations.GetNumberFromFactId(key)

        Assert.AreEqual(599, result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub GetNumberFromFactId_NegativeNumber_Expects_Null()
        Dim key = "A[-1]"

        Dim result = DefaultStringOperations.GetNumberFromFactId(key)

        Assert.IsNull(result)
    End Sub

End Class
