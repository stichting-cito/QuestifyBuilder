
Imports System.Text

Imports Cito.Tester.Common

<TestClass()> _
Public Class ArrayHelperTest

    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestMethod()>
    Public Sub ArrayHelper_CompareByteArrayTest()
        Dim dataArrayA() As Byte = Encoding.ASCII.GetBytes("ThisIsAString")
        Dim dataArrayB() As Byte = Encoding.ASCII.GetBytes("ThisIsAString")
        Dim dataArrayC() As Byte = Encoding.ASCII.GetBytes("ThisIsAString2")


        Dim result1 As Boolean = ArrayHelper.CompareByteArray(dataArrayA, dataArrayB)
        Assert.AreEqual(True, result1, "Cito.Tester.Common.ArrayHelper.CompareByteArray did not return the expected value.")

        Dim result2 As Boolean = ArrayHelper.CompareByteArray(dataArrayA, dataArrayC)
        Assert.AreEqual(False, result2, "Cito.Tester.Common.ArrayHelper.CompareByteArray did not return the expected value.")
    End Sub

End Class
