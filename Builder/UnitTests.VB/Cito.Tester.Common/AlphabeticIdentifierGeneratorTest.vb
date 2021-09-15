
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
        'Arrange
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator(0)

        'Act

        'Assert
    End Sub

    <TestMethod()>
    Public Sub GetLastIdentifierFromStartIndexZero()
        'Arrange
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator()

        'Act
        Dim newId As String = idGenerator.GetLastGeneratedIdentifier()

        'Assert
        Assert.AreEqual(newId, "A")
    End Sub

    <TestMethod()>
    Public Sub GetLastIdentifierFromStartIndexThree()
        'Arrange
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator(3)

        'Act
        Dim newId As String = idGenerator.GetLastGeneratedIdentifier()

        'Assert
        Assert.AreEqual(newId, "C")
    End Sub

    <TestMethod()>
    Public Sub GenerateNewIdentifierFromStartIndexZero()
        'Arrange
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator()

        'Act
        Dim newId As String = idGenerator.GetNewIdentifier()

        'Assert
        Assert.AreEqual(newId, "A")
    End Sub

    <TestMethod()>
    Public Sub GenerateNewIdentifierFromStartIndexThree()
        'Arrange
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator(3)

        'Act
        Dim newId As String = idGenerator.GetNewIdentifier()

        'Assert
        Assert.AreEqual(newId, "C")
    End Sub

    <TestMethod()>
    Public Sub GenerateTwoTimesNewIdentifierFromStartIndexZero()
        'Arrange
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator()

        'Act
        Dim newId As String = idGenerator.GetNewIdentifier()
        newId = idGenerator.GetNewIdentifier()

        'Assert
        Assert.AreEqual(newId, "B")
    End Sub

    <TestMethod()>
    Public Sub GetLastIdentifierAfterTwoTimesNewIdentifierFromStartIndexZero()
        'Arrange
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator()

        'Act
        Dim newId As String = idGenerator.GetNewIdentifier()
        newId = idGenerator.GetNewIdentifier()

        newId = idGenerator.GetLastGeneratedIdentifier()

        'Assert
        Assert.AreEqual(newId, "C")
    End Sub

    <TestMethod()>
    Public Sub GetLastIdentifierAfterTwoTimesNewIdentifierFromStartIndexThree()
        'Arrange
        Dim idGenerator As IIdentifierGenerator(Of String) = New AlphabeticIdentifierGenerator(3)

        'Act
        Dim newId As String = idGenerator.GetNewIdentifier()
        newId = idGenerator.GetNewIdentifier()

        newId = idGenerator.GetLastGeneratedIdentifier()

        'Assert
        Assert.AreEqual(newId, "E")
    End Sub

End Class
