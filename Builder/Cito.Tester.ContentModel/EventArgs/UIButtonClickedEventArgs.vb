Public Class UIButtonClickedEventArgs
    Inherits EventArgs


    Private _UICommandId As UICommand

    Private _commandText As String

    Private _Handled As Boolean

    Private _Cancelled As Boolean

    Private _Data As Dictionary(Of String, String)



    Public Sub New(commandToExec As UICommand, commandText As String)
        _UICommandId = commandToExec
        _commandText = commandText
    End Sub

    Public Sub New(commandText As String)
        Me.New(UICommand.CustomCommand, commandText)
    End Sub



    Public ReadOnly Property UICommand As UICommand
        Get
            Return _UICommandId
        End Get
    End Property

    Public ReadOnly Property CommandText As String
        Get
            Return _commandText
        End Get
    End Property


    Public Property Handled As Boolean
        Get
            Return _Handled OrElse _Cancelled
        End Get
        Set
            _Handled = value
        End Set
    End Property

    Public Property Cancelled As Boolean
        Get
            Return _Cancelled
        End Get
        Set
            _Cancelled = value
        End Set
    End Property

    Public Property Data As Dictionary(Of String, String)
        Get
            Return _Data
        End Get
        Set
            _Data = value
        End Set
    End Property


End Class