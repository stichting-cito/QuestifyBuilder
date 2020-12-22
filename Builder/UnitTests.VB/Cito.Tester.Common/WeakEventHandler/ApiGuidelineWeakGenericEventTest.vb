
Imports System.Linq
Imports System.Threading
Imports Cito.Tester.Common.WeakEventHandler

<TestClass()>
Public Class ApiGuidelineWeakGenericEventTest

    Class Tester

        Private myHandlers As IList(Of IWeakGenericEventHandler(Of EventArgs))



        Public Custom Event MyEvent As EventHandler(Of EventArgs)
            AddHandler(value As EventHandler(Of EventArgs))
                AddWeakGenericEventHandler(myHandlers, value, Sub(e) RemoveHandler MyEvent, e)
            End AddHandler

            RemoveHandler(value As EventHandler(Of EventArgs))
                RemoveWeakGenericEventHandler(myHandlers, value)
            End RemoveHandler

            RaiseEvent(sender As Object, e As EventArgs)
                If (myHandlers IsNot Nothing) Then
                    For Each h As IWeakGenericEventHandler(Of EventArgs) In myHandlers
                        h.Handler.Invoke(sender, e)
                    Next
                End If
            End RaiseEvent
        End Event

        Public Sub FireEvent()
            RaiseEvent MyEvent(Me, EventArgs.Empty)
        End Sub

        Function NrOfAliveHandlers() As Integer
            Dim count As Integer = (From e In myHandlers Where e.IsAlive Select e).Count()
            Return count
        End Function

    End Class

    Class AHandler

        Public Sub SomeHandler(sender As Object, args As EventArgs)
            If DiveredCall IsNot Nothing Then
                _diveredCall()
            End If
        End Sub

        Private _diveredCall As Action
        Public Property DiveredCall() As Action
            Get
                Return _diveredCall
            End Get
            Set(value As Action)
                _diveredCall = value
            End Set
        End Property

    End Class

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub AddAndRemoveHandler_NoHandlerGetsExecuted()
        Dim count As Integer = 0
        Dim test As New Tester
        Dim aHandler As New AHandler
        aHandler.DiveredCall = Sub() Interlocked.Increment(count)

        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler
        RemoveHandler test.MyEvent, AddressOf aHandler.SomeHandler

        test.FireEvent()

        Assert.AreEqual(0, test.NrOfAliveHandlers())
        Assert.AreEqual(0, count)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyHandlerIsCollected()
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)
        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler

        aHandler = Nothing
        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        Assert.AreEqual(False, weak_refToHandler.IsAlive)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CallEventAfterCleanup_ExpectsEventHandlerDeregisterd()
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)
        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler

        aHandler = Nothing
        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        test.FireEvent()

        Assert.AreEqual(False, weak_refToHandler.IsAlive)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub HandlerNotReleased_CallEventAfterGC_ExpectsEventFired()
        Dim count As Integer
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)

        aHandler.DiveredCall = Sub() Interlocked.Increment(count)

        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler


        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        test.FireEvent()

        Assert.AreEqual(True, weak_refToHandler.IsAlive)
        Assert.AreEqual(1, test.NrOfAliveHandlers())
        Assert.AreEqual(1, count)
        Assert.IsNotNull(aHandler)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyBehaviourOverMultipleCollects()

        Dim count As Integer
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)

        aHandler.DiveredCall = Sub() Interlocked.Increment(count)

        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler

        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        test.FireEvent()


        Assert.IsNotNull(aHandler, "Should not be empty")
        aHandler = Nothing

        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        test.FireEvent()

        Assert.AreEqual(False, weak_refToHandler.IsAlive)
        Assert.AreEqual(0, test.NrOfAliveHandlers())
        Assert.AreEqual(1, count)
    End Sub

End Class
