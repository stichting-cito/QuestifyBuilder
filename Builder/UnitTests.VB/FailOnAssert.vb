
'Code taken from http://blogs.iis.net/yigalatz/archive/2011/03/31/unit-tests-should-not-debug-assert.aspx

''' <summary>
''' This class helps to let tests fail when an assert message is shown
''' 
''' Usage:
'''        
''' [TestInitialize]
''' Public Sub MyTestInitialize()
'''    FailOnAssert.Disable = True 'Disable 
''' End Sub
'''
''' [TestCleanup]
''' Public Sub MyTestCleanup()
'''    FailOnAssert.Disable = False
''' End Sub
''' </summary>
Public Class FailOnAssert
    Inherits Diagnostics.TraceListener
    <ThreadStatic> _
    Private Shared _disable As Boolean

    Private Shared _instance As FailOnAssert = Nothing

    Private Shared _lock As New Object()

    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As FailOnAssert
        If _instance IsNot Nothing Then
            Return _instance
        End If

        SyncLock _lock
            If _instance Is Nothing Then
                _instance = New FailOnAssert()
            End If
        End SyncLock
        Return _instance
    End Function

    Public Shared Property Disable() As Boolean
        Get
            Return _disable
        End Get
        Set(value As Boolean)
            _disable = value
        End Set
    End Property

    Public Overrides Sub Fail(message As String)
        If Not Disable Then
            Assert.Fail("Product raised an assert: " & message)
        End If
    End Sub

    Public Overrides Sub Fail(message As String, detailMessage As String)
        If Not Disable Then
            Assert.Fail("Product raised an assert: " & message & vbLf & detailMessage)
        End If
    End Sub

    Public Overrides Sub Write(message As String)
    End Sub

    Public Overrides Sub WriteLine(message As String)
    End Sub

End Class