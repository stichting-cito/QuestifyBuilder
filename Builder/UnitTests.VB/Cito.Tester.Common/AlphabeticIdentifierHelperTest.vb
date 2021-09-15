
Imports Cito.Tester.Common

<TestClass()>
Public Class AlphabeticIdentifierHelperTest

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_1()
        'Arrange
        Dim result As String = String.Empty

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(1)

        'Assert
        Assert.AreEqual("A", result, "Result should be 'A'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_26()
        'Arrange
        Dim result As String = String.Empty

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(26)

        'Assert
        Assert.AreEqual("Z", result, "Result should be 'Z'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_27()
        'Arrange
        Dim result As String = String.Empty

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(27)

        'Assert
        Assert.AreEqual("ZA", result, "Result should be 'ZA'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_77()
        'Arrange
        Dim result As String = String.Empty

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(77)

        'Assert
        Assert.AreEqual("ZZY", result, "Result should be 'ZZY'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_100()
        'Arrange
        Dim result As String = String.Empty

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(100)

        'Assert
        Assert.AreEqual("ZZZV", result, "Result should be 'ZZZV'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromString_A()
        'Arrange
        Dim result As String = String.Empty

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier("A")

        'Assert
        Assert.AreEqual("A", result, "Result should be 'A'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_1()
        'Arrange
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 27) '[

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        'Assert
        Assert.AreEqual("ZA", result, "Result should be 'ZA'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_2()
        'Arrange
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 28) '/

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        'Assert
        Assert.AreEqual("ZB", result, "Result should be 'ZB'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_3()
        'Arrange
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 77) 'empty

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        'Assert
        Assert.AreEqual("ZZY", result, "Result should be 'ZZY'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_4()
        'Arrange
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 100) '¤

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        'Assert
        Assert.AreEqual("ZZZV", result, "Result should be 'ZZZV'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_5()
        'Arrange
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 63) 'empty (Delete)

        'Act
        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        'Assert
        Assert.AreEqual("ZZK", result, "Result should be 'ZZK'")
    End Sub

End Class
