Public Class CommandExecuteRequestEventArgs
    Inherits EventArgs

    Private _command As TestEditorCommands

    Public ReadOnly Property Command() As TestEditorCommands
        Get
            Return _command
        End Get
    End Property

    Public Sub New(ByVal command As TestEditorCommands)
        _command = command
    End Sub

End Class
