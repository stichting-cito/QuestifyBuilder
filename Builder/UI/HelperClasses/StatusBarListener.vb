Public Class StatusBarListener


    Private Shared _listenerInstance As StatusBarListener



    Public Shared ReadOnly Property MainInstance() As StatusBarListener
        Get
            If _listenerInstance Is Nothing Then _listenerInstance = New StatusBarListener()
            Return _listenerInstance
        End Get
    End Property



    Public Event StatusBarMessagePublished As EventHandler(Of StatusBarMessagePublishedEventArgs)

    Private Sub OnStatusBarMessagePublished(ByVal sender As Object, ByVal e As StatusBarMessagePublishedEventArgs)
        RaiseEvent StatusBarMessagePublished(sender, e)
    End Sub



    Public Sub PublishMessage(ByVal sender As Object, ByVal message As String)
        OnStatusBarMessagePublished(sender, New StatusBarMessagePublishedEventArgs(message))
    End Sub

    Public Sub ClearMessage(ByVal sender As Object)
        OnStatusBarMessagePublished(sender, New StatusBarMessagePublishedEventArgs(String.Empty))
    End Sub


End Class


Public Class StatusBarMessagePublishedEventArgs
    Inherits EventArgs

    Private _message As String

    Public ReadOnly Property Message() As String
        Get
            Return _message
        End Get
    End Property

    Public Sub New(ByVal message As String)
        _message = message
    End Sub

End Class

