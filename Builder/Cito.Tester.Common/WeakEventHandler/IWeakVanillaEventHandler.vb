Namespace WeakEventHandler
    Public Interface IWeakEventHandler(Of THandler)
        ReadOnly Property Handler() As THandler

        ReadOnly Property IsAlive As Boolean
    End Interface
End Namespace