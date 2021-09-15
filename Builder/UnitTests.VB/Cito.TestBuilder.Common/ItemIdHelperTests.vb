
Imports Questify.Builder.Logic.Service.HelperFunctions

''' <remarks>When any of these test fails, the uniqueness of the PokIdentifier may be compromised</remarks>
<TestClass()>
Public Class ItemIdHelperTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Workers")>
    Public Sub GetItemTest1()
        'Arrange
        Dim expected As String = "xdpg6y"
        
        'Act
        Dim result = ItemIdHelper.GetItemId(985315486)
        
        'Assert
        Assert.AreEqual(expected, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Workers")>
    Public Sub GetItemIdTest2()
        'Arrange
        Dim expected As String = "mzhe"
        
        'Act
        Dim result = ItemIdHelper.GetItemId(654828)
        
        'Assert
        Assert.AreEqual(expected, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Workers"), ExpectedException(GetType(System.ArgumentOutOfRangeException))>
    Public Sub GetItemIdTestNegative()
        'Arrange
        Const ONETOOLARGE As Integer = 1073741824
        Dim expected As String = ""
        
        'Act
        Dim result = ItemIdHelper.GetItemId(ONETOOLARGE)
        
        'Assert
        Assert.AreEqual(expected, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Workers"), ExpectedException(GetType(System.ArgumentOutOfRangeException))>
    Public Sub GetItemIdTestTooLargeValue()
        'Arrange
        Const NEGATIVENUMBER As Integer = -1
        Dim expected As String = ""
        
        'Act
        Dim result = ItemIdHelper.GetItemId(NEGATIVENUMBER)
        
        'Assert
        Assert.AreEqual(expected, result)
    End Sub

End Class