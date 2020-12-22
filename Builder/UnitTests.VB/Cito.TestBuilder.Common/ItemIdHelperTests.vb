
Imports Questify.Builder.Logic.Service.HelperFunctions

<TestClass()>
Public Class ItemIdHelperTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Workers")>
    Public Sub GetItemTest1()
        Dim expected As String = "xdpg6y"

        Dim result = ItemIdHelper.GetItemId(985315486)

        Assert.AreEqual(expected, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Workers")>
    Public Sub GetItemIdTest2()
        Dim expected As String = "mzhe"

        Dim result = ItemIdHelper.GetItemId(654828)

        Assert.AreEqual(expected, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Workers"), ExpectedException(GetType(System.ArgumentOutOfRangeException))>
    Public Sub GetItemIdTestNegative()
        Const ONETOOLARGE As Integer = 1073741824
        Dim expected As String = ""

        Dim result = ItemIdHelper.GetItemId(ONETOOLARGE)

        Assert.AreEqual(expected, result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Workers"), ExpectedException(GetType(System.ArgumentOutOfRangeException))>
    Public Sub GetItemIdTestTooLargeValue()
        Const NEGATIVENUMBER As Integer = -1
        Dim expected As String = ""

        Dim result = ItemIdHelper.GetItemId(NEGATIVENUMBER)

        Assert.AreEqual(expected, result)
    End Sub

End Class