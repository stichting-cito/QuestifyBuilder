Public Class TitleBarButtonStateEventArgs
    Inherits EventArgs

    Private _buttonName As String
    Private _stateValue As Boolean

    Public ReadOnly Property ButtonName As String
        Get
            Return _buttonName
        End Get
    End Property


    Public Property StateValue As Boolean
        Get
            Return _stateValue
        End Get
        Set
            _stateValue = value
        End Set
    End Property


    Public Sub New(buttonName As String, stateValue As Boolean)
        _buttonName = buttonName
        _stateValue = stateValue
    End Sub

    Public Sub New(buttonName As String)
        _buttonName = buttonName
    End Sub

End Class
