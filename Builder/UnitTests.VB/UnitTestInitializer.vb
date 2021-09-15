
'Code taken from http://blogs.iis.net/yigalatz/archive/2011/03/31/unit-tests-should-not-debug-assert.aspx
Imports System.Diagnostics

<TestClass>
Public Class UnitTestInitializer

    Private Shared _initialized As Boolean = False
    Private Shared _lock As New Object()

    <AssemblyInitialize>
    Public Shared Sub Initialize(context As TestContext)
        SyncLock _lock
            If _initialized Then
                Return
            End If

            Dim removeListener As TraceListener = Nothing
            For Each listener As TraceListener In Debug.Listeners
                If TypeOf listener Is DefaultTraceListener Then
                    removeListener = listener
                    Exit For
                End If
            Next
            Debug.Listeners.Remove(removeListener)
        End SyncLock
        Debug.Listeners.Add(FailOnAssert.GetInstance())
    End Sub

End Class