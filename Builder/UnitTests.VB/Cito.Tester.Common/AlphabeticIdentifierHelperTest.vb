
Imports Cito.Tester.Common

<TestClass()>
Public Class AlphabeticIdentifierHelperTest

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_1()
        Dim result As String = String.Empty

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(1)

        Assert.AreEqual("A", result, "Result should be 'A'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_26()
        Dim result As String = String.Empty

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(26)

        Assert.AreEqual("Z", result, "Result should be 'Z'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_27()
        Dim result As String = String.Empty

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(27)

        Assert.AreEqual("ZA", result, "Result should be 'ZA'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_77()
        Dim result As String = String.Empty

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(77)

        Assert.AreEqual("ZZY", result, "Result should be 'ZZY'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromNumber_100()
        Dim result As String = String.Empty

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(100)

        Assert.AreEqual("ZZZV", result, "Result should be 'ZZZV'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromString_A()
        Dim result As String = String.Empty

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier("A")

        Assert.AreEqual("A", result, "Result should be 'A'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_1()
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 27)

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        Assert.AreEqual("ZA", result, "Result should be 'ZA'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_2()
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 28)

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        Assert.AreEqual("ZB", result, "Result should be 'ZB'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_3()
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 77)

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        Assert.AreEqual("ZZY", result, "Result should be 'ZZY'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_4()
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 100)

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        Assert.AreEqual("ZZZV", result, "Result should be 'ZZZV'")
    End Sub

    <TestMethod(), TestCategory("AlphabeticIdentifierHelper")>
    Public Sub GetAlphabeticIdentifierFromSpecialCharacter_5()
        Dim result As String = String.Empty
        Dim input As String = ChrW(64 + 63)

        result = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(input)

        Assert.AreEqual("ZZK", result, "Result should be 'ZZK'")
    End Sub

End Class
