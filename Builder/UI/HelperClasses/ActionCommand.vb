Public Class ActionCommand

    Private Shared _actionCommandInstance As ActionCommand
    Private _currentActionCommands As IActionCommands
    Private _currentBankId As Integer
    Public Event ContextChanged As EventHandler(Of ContextChangedEventArgs)

    Public Sub OnContextChanged(ByVal e As ContextChangedEventArgs)
        RaiseEvent ContextChanged(Me, e)
    End Sub

    Public ReadOnly Property CurrentActionCommands() As IActionCommands
        Get
            Return _currentActionCommands
        End Get
    End Property

    Public ReadOnly Property CurrentBankId() As Integer
        Get
            Return _currentBankId
        End Get
    End Property

    Public Shared ReadOnly Property Instance() As ActionCommand
        Get
            If _actionCommandInstance Is Nothing Then _actionCommandInstance = New ActionCommand
            Return _actionCommandInstance
        End Get
    End Property

    Public Sub SetNewContext(ByVal actionCommands As IActionCommands, ByVal bankId As Integer)
        _currentActionCommands = actionCommands
        _currentBankId = bankId
        OnContextChanged(New ContextChangedEventArgs(actionCommands, bankId))
    End Sub

    Public Sub SetNoContext()
        _currentActionCommands = Nothing
        _currentBankId = 0
        OnContextChanged(New ContextChangedEventArgs(Nothing, 0))
    End Sub

End Class