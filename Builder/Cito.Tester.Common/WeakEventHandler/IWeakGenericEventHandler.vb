Namespace WeakEventHandler
    Public Interface IWeakGenericEventHandler(Of E As EventArgs)
        Inherits IWeakEventHandler(Of EventHandler(Of E))
    End Interface
End Namespace