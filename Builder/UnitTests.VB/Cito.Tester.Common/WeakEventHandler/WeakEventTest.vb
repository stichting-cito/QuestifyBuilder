
Imports System.Threading
Imports Cito.Tester.Common.WeakEventHandler

<TestClass()>
Public Class WeakEventTest

    'Why do we want a weak event handler?
    'So that the handler is not kept alive by reference by the event.

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyHandlerIsCollected()
        'Arrange
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.
        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e) RemoveHandler test.MyEvent, e)

        'Act
        aHandler = Nothing 'No more refs to eventHandler
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        'Assert
        Assert.AreEqual(False, weak_refToHandler.IsAlive) 'Object should not be alive
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyEventIsNotDeregisterd()
        'Arrange
        Dim RemoveHandlerCalled As Boolean = False
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.
        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e)
                                                                                               RemoveHandler test.MyEvent, e
                                                                                               RemoveHandlerCalled = True
                                                                                           End Sub)
        'Act
        aHandler = Nothing 'No more refs to eventHandler
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.


        'Assert
        Assert.AreEqual(False, weak_refToHandler.IsAlive) 'Object should not be alive
        Assert.AreEqual(False, RemoveHandlerCalled) 'Deregister has not occurred since event was not fired, which will check if handler needs to be removed.
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CallEventAfterCleanup_ExpectsEventHandlerDeregisterd()
        'Arrange
        Dim RemoveHandlerCalled As Boolean = False
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.
        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e)
                                                                                               RemoveHandler test.MyEvent, e
                                                                                               RemoveHandlerCalled = True
                                                                                           End Sub)
        'Act
        aHandler = Nothing 'No more refs to eventHandler
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        'Fires event. Since handler is gone, the event will be de-registered.
        test.FireEvent()

        'Assert
        Assert.AreEqual(False, weak_refToHandler.IsAlive) 'Object should not be alive
        Assert.AreEqual(True, RemoveHandlerCalled) 'Deregister has occurred 
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub HandlerNotReleased_CallEventAfterGC_ExpectsEventFired()
        'Arrange
        Dim count As Integer
        Dim RemoveHandlerCalled As Boolean = False
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.

        'Locally increment.
        aHandler.DiveredCall = Sub() Interlocked.Increment(count)

        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e)
                                                                                               RemoveHandler test.MyEvent, e
                                                                                               RemoveHandlerCalled = True
                                                                                           End Sub)
        'Act
        'We do not release ref to handler!

        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        'Fires event. Since handler is gone, the event will be de-registered.
        test.FireEvent()

        'Assert
        Assert.IsNotNull(aHandler) 'Remove/Comment this line and the test will fail!
        Assert.AreEqual(True, weak_refToHandler.IsAlive, "OMG Object is dead!!") 'Object should be alive
        Assert.AreEqual(False, RemoveHandlerCalled) 'Deregister has not occurred, ref to handler was not released.
        Assert.AreEqual(1, count)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyBehaviourOverMultipleCollects()
        'Path:
        '1)Collect
        '2)Fire Event
        '3)ReleaseHandler
        '4)Collect
        '5)Fire Event
        'Result: Count==1, handler is gone, event is deregistered

        'Arrange
        Dim count As Integer
        Dim RemoveHandlerCalled As Boolean = False
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.

        'Locally increment.
        aHandler.DiveredCall = Sub() count = count + 1

        AddHandler test.MyEvent, New EventHandler(AddressOf aHandler.SomeHandler).MakeWeak(Sub(e)
                                                                                               RemoveHandler test.MyEvent, e
                                                                                               RemoveHandlerCalled = True
                                                                                           End Sub)
       
        'Act
        '1)Collect
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.
        Assert.IsNotNull(aHandler.DiveredCall)
        
        '2)Fire Event
        test.FireEvent() 'We fire (count should be 1 after this)

        '3)ReleaseHandler
        aHandler = Nothing 'no more refs to eventHandler

        '4)Collect
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        '5)Fire Event
        test.FireEvent() 'Fires event, since handler is gone,.. the event will be de-registered.

        'Assert
        Assert.AreEqual(False, weak_refToHandler.IsAlive) 'Object should be alive
        Assert.AreEqual(True, RemoveHandlerCalled) 'Deregister has occurred 
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
