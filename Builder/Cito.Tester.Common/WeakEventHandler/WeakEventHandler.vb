
Namespace WeakEventHandler



    Public Class WeakEventHandler(Of T As Class, E As EventArgs)
        Inherits WeakEventHandlerGeneric(Of T, E, EventHandler(Of E))
        Implements IWeakGenericEventHandler(Of E)


        Public Sub New(eventHandler As EventHandler(Of E), unregister As UnregisterDelegate(Of EventHandler(Of E)))
            MyBase.New(eventHandler, unregister)
        End Sub

        Public Overrides ReadOnly Property Handler As EventHandler(Of E) Implements IWeakEventHandler(Of EventHandler(Of E)).Handler
            Get
                Return MyBase.Handler
            End Get
        End Property

        Public Overrides ReadOnly Property IsAlive As Boolean Implements IWeakEventHandler(Of EventHandler(Of E)).IsAlive
            Get
                Return MyBase.IsAlive
            End Get
        End Property

    End Class



    Public Class WeakVanillaEventHandler(Of T As Class, E As EventArgs, H As Class)
        Inherits WeakEventHandlerGeneric(Of T, E, H)
        Implements IWeakEventHandler(Of H)

        Public Sub New(eventHandler As H, unregister As UnregisterDelegate(Of H))
            MyBase.New(eventHandler, unregister)
        End Sub

        Public Overrides ReadOnly Property Handler As H Implements IWeakEventHandler(Of H).Handler
            Get
                Return MyBase.Handler
            End Get
        End Property

        Public Overrides ReadOnly Property IsAlive As Boolean Implements IWeakEventHandler(Of H).IsAlive
            Get
                Return MyBase.IsAlive
            End Get
        End Property
    End Class




End Namespace