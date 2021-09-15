
Imports System.Text

Imports Cito.Tester.Common

'''<summary>
'''This is a test class for Cito.Tester.Common.ArrayHelper and is intended
'''to contain all Cito.Tester.Common.ArrayHelper Unit Tests
'''</summary>
<TestClass()> _
Public Class ArrayHelperTest

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
            testContextInstance = value
        End Set
    End Property

    '''<summary>
    '''A test for CompareByteArray(ByVal Byte(), ByVal Byte())
    '''</summary>
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
