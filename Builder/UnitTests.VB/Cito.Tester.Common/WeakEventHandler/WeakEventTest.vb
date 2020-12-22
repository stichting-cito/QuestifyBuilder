
Imports System.Threading
Imports Cito.Tester.Common.WeakEventHandler

<TestClass()>
Public Class WeakEventTest


    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyHandlerIsCollected()
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)
        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e) RemoveHandler test.MyEvent, e)

        aHandler = Nothing
        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        Assert.AreEqual(False, weak_refToHandler.IsAlive)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyEventIsNotDeregisterd()
        Dim RemoveHandlerCalled As Boolean = False
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)
        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e)
                                                                                               RemoveHandler test.MyEvent, e
                                                                                               RemoveHandlerCalled = True
                                                                                           End Sub)
        aHandler = Nothing
        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()


        Assert.AreEqual(False, weak_refToHandler.IsAlive)
        Assert.AreEqual(False, RemoveHandlerCalled)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CallEventAfterCleanup_ExpectsEventHandlerDeregisterd()
        Dim RemoveHandlerCalled As Boolean = False
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)
        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e)
                                                                                               RemoveHandler test.MyEvent, e
                                                                                               RemoveHandlerCalled = True
                                                                                           End Sub)
        aHandler = Nothing
        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        test.FireEvent()

        Assert.AreEqual(False, weak_refToHandler.IsAlive)
        Assert.AreEqual(True, RemoveHandlerCalled)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub HandlerNotReleased_CallEventAfterGC_ExpectsEventFired()
        Dim count As Integer
        Dim RemoveHandlerCalled As Boolean = False
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)

        aHandler.DiveredCall = Sub() Interlocked.Increment(count)

        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e)
                                                                                               RemoveHandler test.MyEvent, e
                                                                                               RemoveHandlerCalled = True
                                                                                           End Sub)

        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        test.FireEvent()

        Assert.IsNotNull(aHandler)
        Assert.AreEqual(True, weak_refToHandler.IsAlive, "OMG Object is dead!!")
        Assert.AreEqual(False, RemoveHandlerCalled)
        Assert.AreEqual(1, count)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyBehaviourOverMultipleCollects()

        Dim count As Integer
        Dim RemoveHandlerCalled As Boolean = False
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler)

        aHandler.DiveredCall = Sub() count = count + 1

        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e)
                                                                                               RemoveHandler test.MyEvent, e
                                                                                               RemoveHandlerCalled = True
                                                                                           End Sub)

        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()
        Assert.IsNotNull(aHandler.DiveredCall)

        test.FireEvent()

        aHandler = Nothing

        GC.Collect(GC.MaxGeneration)
        GC.WaitForPendingFinalizers()

        test.FireEvent()

        Assert.AreEqual(False, weak_refToHandler.IsAlive)
        Assert.AreEqual(True, RemoveHandlerCalled)
        Assert.AreEqual(1, count)
    End Sub

    Class Tester

        Public Event MyEvent As EventHandler

        Public Sub FireEvent()
            RaiseEvent MyEvent(Me, EventArgs.Empty)
        End Sub

    End Class

    Class AHandler

        Private _diveredCall As Action

        Public Sub SomeHandler(sender As Object, args As EventArgs)
            If DiveredCall IsNot Nothing Then
                _diveredCall()
            End If
        End Sub

        Public Property DiveredCall() As Action
            Get
                Return _diveredCall
            End Get
            Set(value As Action)
                _diveredCall = value
            End Set
        End Property

    End Class

End Class
