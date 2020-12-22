Public Class StateNeededEventArgs
    Inherits EventArgs


    Private _itemId As String
    Private _data As Response



    Public ReadOnly Property ItemId As String
        Get
            Return _itemId
        End Get
    End Property


    Public Property Data As Response
        Get
            Return _data
        End Get
        Set
            _data = value
        End Set
    End Property



    Public Sub New(itemId As String)
        _itemId = itemId
    End Sub


End Class
