Public Class ContextChangedEventArgs
    Inherits EventArgs

    Private _actionCommands As IActionCommands
    Private _bankId As Integer

    Public Sub New(ByVal actionCommands As IActionCommands, ByVal bankId As Integer)
        _actionCommands = actionCommands
        _bankId = bankId
    End Sub

    Public ReadOnly Property ActionCommands() As IActionCommands
        Get
            Return _actionCommands
        End Get
    End Property

    Public ReadOnly Property BankId() As Integer
        Get
            Return _bankId
        End Get
    End Property
End Class