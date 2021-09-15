
Imports Cito.Tester.Common.Logging

<TestClass()> Public Class LoggerTest

    ''' <summary>
    ''' A test for tracing error log with curly braces in it
    ''' </summary>
    <TestMethod()> Public Sub LogMessageWithCurlyBracesTest()
        'Arrange
        Const messageString As String = "The error is: {0}"

        'Act
        Log.TraceError(TraceCategory.Error, messageString, New Exception("This error message contains {004590} {{ } } and <!-- -->. ' text/html %20"))

        'Assert
    End Sub

    ''' <summary>
    ''' A test for tracing error log with empy message
    ''' </summary>
    <TestMethod()> Public Sub LogEmptyMessageTest()
        'Arrange
        Const messageString As String = "The error is: {0}"

        'Act
        Log.TraceError(TraceCategory.Error, messageString, New Exception())

        'Assert
    End Sub

End Class