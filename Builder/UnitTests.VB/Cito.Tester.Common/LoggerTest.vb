
Imports Cito.Tester.Common.Logging

<TestClass()> Public Class LoggerTest

    <TestMethod()> Public Sub LogMessageWithCurlyBracesTest()
        Const messageString As String = "The error is: {0}"

        Log.TraceError(TraceCategory.Error, messageString, New Exception("This error message contains {004590} {{ } } and <!-- -->. ' text/html %20"))

    End Sub

    <TestMethod()> Public Sub LogEmptyMessageTest()
        Const messageString As String = "The error is: {0}"

        Log.TraceError(TraceCategory.Error, messageString, New Exception())

    End Sub

End Class