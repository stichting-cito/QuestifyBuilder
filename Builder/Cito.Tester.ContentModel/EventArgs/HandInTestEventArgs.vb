Public Class HandInTestEventArgs
    Inherits EventArgs

    Private _handInTestState As HandInTestState
    Private _userAllreadyConfirmedExit As Boolean

    Public ReadOnly Property HandInTestState As HandInTestState
        Get
            Return _handInTestState
        End Get
    End Property

    Public ReadOnly Property UserAlreadyConfirmedExit As Boolean
        Get
            Return _userAllreadyConfirmedExit
        End Get
    End Property

    Public Sub New(state As HandInTestState, userAllreadyConfirmedExit As Boolean)
        _handInTestState = state
        _userAllreadyConfirmedExit = userAllreadyConfirmedExit
    End Sub

    Public Sub New(state As HandInTestState)
        Me.New(state, False)
    End Sub

End Class
