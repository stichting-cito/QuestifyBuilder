
Imports System.Text.RegularExpressions
Imports Questify.Builder.Logic.ContentModel

<TestClass()>
Public Class StringExtensionTests

    Private Shared ReadOnly _regex As New Regex("(\w+-)*(\w+)(?:\[\w\])?(-\w+)*")

    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldEqual()
        'Arrange
        Dim s1 = "1-test"
        Dim s2 = "1[x]-test"
        
        'Act
        Dim result = s1.EqualStringByRegex(s2, _regex)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldNotEqual()
        'Arrange
        Dim s1 = "1-test"
        Dim s2 = "2[x]-test"
        
        'Act
        Dim result = s1.EqualStringByRegex(s2, _regex)
        
        'Assert
        Assert.IsFalse(result)
    End Sub              

    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldEqual2()
        'Arrange
        Dim s1 = "I23a0653b_b574_4d5e_ad66_e05af1a169da-test"
        Dim s2 = "I23a0653b_b574_4d5e_ad66_e05af1a169da[x]-test"
        
        'Act
        Dim result = s1.EqualStringByRegex(s2, _regex)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldNotEqual2()
        'Arrange
        Dim s1 = "I23a0653b_b574_4d5e_ad66_e05af1a169da"
        Dim s2 = "I23a0653b_b574_4d5e_ad66_e05af1a169df"
        
        'Act
        Dim result = s1.EqualStringByRegex(s2, _regex)
        
        'Assert
        Assert.IsFalse(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldEqual3()
        'Arrange
        Dim s1 = "I23a0653b-b574-4d5e-ad66-e05af1a169da-test"
        Dim s2 = "I23a0653b-b574-4d5e-ad66-e05af1a169da[x]-test"
        
        'Act
        Dim result = s1.EqualStringByRegex(s2, _regex)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldNotEqual3()
        'Arrange
        Dim s1 = "I23a0653b-b574-4d5e-ad66-e05af1a169da"
        Dim s2 = "I23a0653b-b574-4d5e-ad66-e05af1a169df"
        
        'Act
        Dim result = s1.EqualStringByRegex(s2, _regex)
        
        'Assert
        Assert.IsFalse(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldEqual4()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        
        'Act
        Dim result = s1.EqualStringByRegex(s2, _regex)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub stringEqualsRegex_shouldNotEqual4()
        'Arrange
        Dim s1 = "1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7"
        Dim s2 = "1-Ice32c0ba-73db-456d-b3d3-c92265282cf8"
        
        'Act
        Dim result = s1.EqualStringByRegex(s2, _regex)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub truncate_empty()
        'Arrange

        'Act
        Dim result = "".TruncateWithEllipsis(4)
        
        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(result))
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub truncate_null()
        'Arrange
        Dim s As String = Nothing
        
        'Act
        Dim result = s.TruncateWithEllipsis(4)
        
        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(result))
    End Sub

    <TestMethod(), TestCategory("Logic")>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub truncate_MaxLengthMustBeGreaterThan3()
        'Arrange
        
        'Act
        Dim result = "asdfadfadsf".TruncateWithEllipsis(3)
        
        'Assert
        Assert.Fail()
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub truncate_strLength5_Max4_resultIs4()
        'Arrange

        'Act
        Dim result = "12345".TruncateWithEllipsis(4)
        'Assert
        Assert.AreEqual(4, result.Length)
    End Sub

End Class
