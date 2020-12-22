Public Class SessionContextProvider : Implements IDisposable


    <ThreadStatic>
    Private Shared _sessionStack As Stack(Of ISessionContext)

    Private Shared ReadOnly Property SessionStack As Stack(Of ISessionContext)
        Get
            If (_sessionStack Is Nothing) Then
                _sessionStack = New Stack(Of ISessionContext)
            End If
            Return _sessionStack
        End Get
    End Property

    Friend Shared ReadOnly Property CurrentSession As ISessionContext
        Get
            If (SessionStack.Count > 0) Then Return SessionStack.Peek()
            Return Nothing
        End Get
    End Property



    Public Sub New(sessionContext As ISessionContext)
        SessionStack.Push(sessionContext)
    End Sub


    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                SessionStack.Pop()
            End If

        End If
        Me.disposedValue = True
    End Sub


    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
