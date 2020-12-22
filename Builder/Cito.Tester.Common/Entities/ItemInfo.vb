Public Class ItemInfo


    Private _itemIdentifier As String
    Private _tag As String



    Public Property ItemIdentifier() As String
        Get
            Return _itemIdentifier
        End Get
        Set(value As String)
            _itemIdentifier = value
        End Set
    End Property

    Public Property Tag() As String
        Get
            Return _tag
        End Get
        Set(value As String)
            _tag = value
        End Set
    End Property


End Class