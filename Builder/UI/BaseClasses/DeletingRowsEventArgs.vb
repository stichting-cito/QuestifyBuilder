Imports Janus.Windows.GridEX

Public Class DeletingRowsEventArgs
    Inherits EventArgs

    Private _cancel As Boolean
    Private _toBeDeleted As GridEXSelectedItemCollection

    Public ReadOnly Property ToBeDeleted() As GridEXSelectedItemCollection
        Get
            Return _toBeDeleted
        End Get
    End Property


    Public Property Cancel() As Boolean
        Get
            Return _cancel
        End Get
        Set(value As Boolean)
            _cancel = value
        End Set
    End Property

    Public Sub New(toBeDeleted As GridEXSelectedItemCollection)
        _toBeDeleted = toBeDeleted
    End Sub

End Class