
Imports Cito.Tester.Common.Controls.Canvas.LabelGenerator

<TestClass()> _
Public Class AlphabeticIdentifierGeneratorTest

    <TestInitialize()> _
    Public Sub TestInitialize()
    End Sub

    <TestCleanup()> _
    Public Sub TestCleanup()
    End Sub

    <TestMethod()>
    Public Sub StartIndexTooLow()
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator(0)


    End Sub

    <TestMethod()>
    Public Sub GetLastIdentifierFromStartIndexZero()
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator()

        Dim newId As String = idGenerator.GetLastGeneratedIdentifier()

        Assert.AreEqual(newId, "A")
    End Sub

    <TestMethod()>
    Public Sub GetLastIdentifierFromStartIndexThree()
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator(3)

        Dim newId As String = idGenerator.GetLastGeneratedIdentifier()

        Assert.AreEqual(newId, "C")
    End Sub

    <TestMethod()>
    Public Sub GenerateNewIdentifierFromStartIndexZero()
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator()

        Dim newId As String = idGenerator.GetNewIdentifier()

        Assert.AreEqual(newId, "A")
    End Sub

    <TestMethod()>
    Public Sub GenerateNewIdentifierFromStartIndexThree()
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator(3)

        Dim newId As String = idGenerator.GetNewIdentifier()

        Assert.AreEqual(newId, "C")
    End Sub

    <TestMethod()>
    Public Sub GenerateTwoTimesNewIdentifierFromStartIndexZero()
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator()

        Dim newId As String = idGenerator.GetNewIdentifier()
        newId = idGenerator.GetNewIdentifier()

        Assert.AreEqual(newId, "B")
    End Sub

    <TestMethod()>
    Public Sub GetLastIdentifierAfterTwoTimesNewIdentifierFromStartIndexZero()
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator()

        Dim newId As String = idGenerator.GetNewIdentifier()
        newId = idGenerator.GetNewIdentifier()

        newId = idGenerator.GetLastGeneratedIdentifier()

        Assert.AreEqual(newId, "C")
    End Sub

    <TestMethod()>
    Public Sub GetLastIdentifierAfterTwoTimesNewIdentifierFromStartIndexThree()
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator(3)

        Dim newId As String = idGenerator.GetNewIdentifier()
        newId = idGenerator.GetNewIdentifier()

        newId = idGenerator.GetLastGeneratedIdentifier()

        Assert.AreEqual(newId, "E")
    End Sub

End Class
