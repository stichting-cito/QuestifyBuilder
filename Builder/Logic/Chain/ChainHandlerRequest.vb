Namespace Chain
    Public MustInherit Class ChainHandlerRequest

        Public Sub New()
        End Sub

    End Class

    Public Class ChainHandlerRequest(Of T)
        Inherits ChainHandlerRequest

        Private ReadOnly _items As List(Of T)

        Public Sub New()
            _items = New List(Of T)()
        End Sub

        Public Sub New(items As IEnumerable(Of T))
            Me.New()
            _items.AddRange(items)
        End Sub

        Public ReadOnly Property Items() As IList(Of T)
            Get
                Return _items
            End Get
        End Property

    End Class
End Namespace