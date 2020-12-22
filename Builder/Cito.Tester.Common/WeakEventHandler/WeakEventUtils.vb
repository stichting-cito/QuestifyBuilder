Imports System.ComponentModel
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace WeakEventHandler

    Public Module WeakEventUtils


        <Extension()>
        Public Function MakeWeak(Of E As EventArgs)(eventHandler As EventHandler(Of E), unregister As UnregisterDelegate(Of EventHandler(Of E))) As EventHandler(Of E)
            Dim res As IWeakGenericEventHandler(Of E) = GetWeakGenericHandler(eventHandler, unregister)
            Return res.Handler
        End Function


        <Extension()> _
        Public Function MakeWeak(eventHandler As EventHandler, unregister As UnregisterDelegate(Of EventHandler)) As EventHandler
            CheckArgs(eventHandler, unregister)
            Dim generalType As Type = GetType(WeakVanillaEventHandler(Of ,,))
            Dim genericTypes As Type() = {eventHandler.Method.DeclaringType, GetType(EventArgs), eventHandler.GetType()}
            Dim constructorTypes As Type() = {GetType(EventHandler), GetType(UnregisterDelegate(Of EventHandler))}
            Dim constructorArgs As Object() = {eventHandler, unregister}

            Return DirectCast(GetWeakHandler(generalType, genericTypes, constructorTypes, constructorArgs), IWeakEventHandler(Of EventHandler)).Handler
        End Function


        <Extension()>
        Public Function MakeWeak(eventHandler As PropertyChangedEventHandler, unregister As UnregisterDelegate(Of PropertyChangedEventHandler)) As PropertyChangedEventHandler
            CheckArgs(eventHandler, unregister)

            Dim generalType As Type = GetType(WeakVanillaEventHandler(Of ,,))
            Dim genericTypes As Type() = {eventHandler.Method.DeclaringType, GetType(PropertyChangedEventArgs), eventHandler.GetType()}
            Dim constructorTypes As Type() = {GetType(PropertyChangedEventHandler), GetType(UnregisterDelegate(Of PropertyChangedEventHandler))}
            Dim constructorArgs As Object() = {eventHandler, unregister}

            Return DirectCast(GetWeakHandler(generalType, genericTypes, constructorTypes, constructorArgs), IWeakEventHandler(Of PropertyChangedEventHandler)).Handler
        End Function


        <Extension()> _
        Public Function MakeWeak(eventHandler As KeyEventHandler, unregister As UnregisterDelegate(Of KeyEventHandler)) As KeyEventHandler
            CheckArgs(eventHandler, unregister)
            Dim generalType As Type = GetType(WeakVanillaEventHandler(Of ,,))
            Dim genericTypes As Type() = {eventHandler.Method.DeclaringType, GetType(KeyEventArgs), eventHandler.GetType()}
            Dim constructorTypes As Type() = {GetType(KeyEventHandler), GetType(UnregisterDelegate(Of KeyEventHandler))}
            Dim constructorArgs As Object() = {eventHandler, unregister}

            Return DirectCast(GetWeakHandler(generalType, genericTypes, constructorTypes, constructorArgs), IWeakEventHandler(Of KeyEventHandler)).Handler
        End Function


        <Extension()> _
        Public Function MakeWeak(eventHandler As FormClosedEventHandler, unregister As UnregisterDelegate(Of FormClosedEventHandler)) As FormClosedEventHandler
            CheckArgs(eventHandler, unregister)
            Dim generalType As Type = GetType(WeakVanillaEventHandler(Of ,,))
            Dim genericTypes As Type() = {eventHandler.Method.DeclaringType, GetType(FormClosedEventArgs), eventHandler.GetType()}
            Dim constructorTypes As Type() = {GetType(FormClosedEventHandler), GetType(UnregisterDelegate(Of FormClosedEventHandler))}
            Dim constructorArgs As Object() = {eventHandler, unregister}

            Return DirectCast(GetWeakHandler(generalType, genericTypes, constructorTypes, constructorArgs), IWeakEventHandler(Of FormClosedEventHandler)).Handler
        End Function


        <Extension()> _
        Public Function MakeWeak(eventHandler As HtmlElementEventHandler, unregister As UnregisterDelegate(Of HtmlElementEventHandler)) As HtmlElementEventHandler
            CheckArgs(eventHandler, unregister)
            Dim generalType As Type = GetType(WeakVanillaEventHandler(Of ,,))
            Dim genericTypes As Type() = {eventHandler.Method.DeclaringType, GetType(HtmlElementEventArgs), eventHandler.GetType()}
            Dim constructorTypes As Type() = {GetType(HtmlElementEventHandler), GetType(UnregisterDelegate(Of HtmlElementEventHandler))}
            Dim constructorArgs As Object() = {eventHandler, unregister}

            Return DirectCast(GetWeakHandler(generalType, genericTypes, constructorTypes, constructorArgs), IWeakEventHandler(Of HtmlElementEventHandler)).Handler
        End Function



        <Extension()> _
        Public Function MakeWeak(eventHandler As WebBrowserDocumentCompletedEventHandler, unregister As UnregisterDelegate(Of WebBrowserDocumentCompletedEventHandler)) As WebBrowserDocumentCompletedEventHandler
            CheckArgs(eventHandler, unregister)
            Dim generalType As Type = GetType(WeakVanillaEventHandler(Of ,,))
            Dim genericTypes As Type() = {eventHandler.Method.DeclaringType, GetType(WebBrowserDocumentCompletedEventArgs), eventHandler.GetType()}
            Dim constructorTypes As Type() = {GetType(WebBrowserDocumentCompletedEventHandler), GetType(UnregisterDelegate(Of WebBrowserDocumentCompletedEventHandler))}
            Dim constructorArgs As Object() = {eventHandler, unregister}

            Return DirectCast(GetWeakHandler(generalType, genericTypes, constructorTypes, constructorArgs), IWeakEventHandler(Of WebBrowserDocumentCompletedEventHandler)).Handler
        End Function




        Public Sub AddWeakGenericEventHandler(Of EventArgsType As EventArgs)(ByRef handlers As IList(Of IWeakGenericEventHandler(Of EventArgsType)),
                                                                                    value As EventHandler(Of EventArgsType),
                                                                                    unregister As UnregisterDelegate(Of EventHandler(Of EventArgsType)))
            If handlers Is Nothing Then
                handlers = New List(Of IWeakGenericEventHandler(Of EventArgsType))
            End If

            Dim handlr As IWeakGenericEventHandler(Of EventArgsType) = GetWeakGenericHandler(value, unregister)
            handlers.Add(handlr)
        End Sub

        Public Sub RemoveWeakGenericEventHandler(Of EventArgsType As EventArgs)(ByRef handlers As IList(Of IWeakGenericEventHandler(Of EventArgsType)),
                                                                                       value As EventHandler(Of EventArgsType))
            If handlers IsNot Nothing Then
                Dim handlr As IWeakGenericEventHandler(Of EventArgsType) = GetWeakGenericHandler(value, Nothing)
                If (handlers.Contains(handlr)) Then
                    handlers.Remove(handlr)
                End If
            End If
        End Sub


        Public Sub AddWeakEventHandler(Of Handler As Class)(ByRef handlers As IList(Of IWeakEventHandler(Of Handler)),
                                                            value As Handler,
                                                            unregister As UnregisterDelegate(Of Handler))
            If handlers Is Nothing Then
                handlers = New List(Of IWeakEventHandler(Of Handler))
            End If
        End Sub

        Private Sub CheckArgs(eventHandler As [Delegate], unregister As [Delegate])
            If eventHandler Is Nothing Then
                Throw New ArgumentNullException("eventHandler")
            End If
            If eventHandler.Method.IsStatic OrElse eventHandler.Target Is Nothing Then
                Throw New ArgumentException("Only instance methods are supported.", "eventHandler")
            End If
        End Sub

        Private Function GetWeakHandler(generalType As Type, genericTypes As Type(), constructorArgTypes As Type(), constructorArgs As Object()) As Object
            Dim wehType As Type = generalType.MakeGenericType(genericTypes)
            Dim wehConstructor As ConstructorInfo = wehType.GetConstructor(constructorArgTypes)
            Return wehConstructor.Invoke(constructorArgs)
        End Function

        Private Function GetWeakGenericHandler(Of E As EventArgs)(eventHandler As EventHandler(Of E), unregister As UnregisterDelegate(Of EventHandler(Of E))) As IWeakGenericEventHandler(Of E)
            CheckArgs(eventHandler, unregister)

            Dim generalType As Type = GetType(WeakEventHandler(Of ,))
            Dim genericTypes As Type() = {eventHandler.Method.DeclaringType, GetType(E)}
            Dim constructorTypes As Type() = {GetType(EventHandler(Of E)), GetType(UnregisterDelegate(Of EventHandler(Of E)))}
            Dim constructorArgs As Object() = {eventHandler, unregister}

            Return DirectCast(GetWeakHandler(generalType, genericTypes, constructorTypes, constructorArgs), IWeakGenericEventHandler(Of E))
        End Function
    End Module
End Namespace