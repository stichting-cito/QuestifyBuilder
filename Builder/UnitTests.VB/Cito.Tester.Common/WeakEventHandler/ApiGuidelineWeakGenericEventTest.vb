
Imports System.Linq
Imports System.Threading
Imports Cito.Tester.Common.WeakEventHandler

''' <summary>
''' These are the same tests as the WeakEventTests, but this test serves as a demonstration how to incorporate the WeakEventhandler in your Design.
''' </summary>
''' <remarks></remarks>
<TestClass()>
Public Class ApiGuidelineWeakGenericEventTest

    Class Tester

        Private myHandlers As IList(Of IWeakGenericEventHandler(Of EventArgs))

        'A possible way to incorporate the WeakEventHandeler in your design
        'This allows code to just add an event handler, and have the possibility of forgetting
        'to remove the handler.

        'This construction will allow setting handlers just like normal, e.g.:
        'AddHandler test.MyEvent, AddressOf aHandler.SomeHandler
        'RemoveHandler test.MyEvent, AddressOf aHandler.SomeHandler

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
        'Arrange
        Dim count As Integer = 0
        Dim test As New Tester
        Dim aHandler As New AHandler
        aHandler.DiveredCall = Sub() Interlocked.Increment(count)

        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler
        RemoveHandler test.MyEvent, AddressOf aHandler.SomeHandler

        'Act
        test.FireEvent()

        'Assert
        Assert.AreEqual(0, test.NrOfAliveHandlers()) 'No handlers should exist
        Assert.AreEqual(0, count) 'Count should not have been increased.
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyHandlerIsCollected()
        'Arrange
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.
        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler

        'Act
        aHandler = Nothing 'No more refs to eventHandler
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        'Assert
        Assert.AreEqual(False, weak_refToHandler.IsAlive) 'Object should not be alive
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CallEventAfterCleanup_ExpectsEventHandlerDeregisterd()
        'Arrange
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.
        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler

        'Act
        aHandler = Nothing 'No more refs to eventHandler
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        'Fires event. Since handler is gone, the event will be de-registerd.
        test.FireEvent()

        'Assert
        Assert.AreEqual(False, weak_refToHandler.IsAlive) 'Object should not be alive
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub HandlerNotReleased_CallEventAfterGC_ExpectsEventFired()
        'Arrange
        Dim count As Integer
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.

        'Locally increment.
        aHandler.DiveredCall = Sub() Interlocked.Increment(count)

        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler

        'Act
        'We do not release the ref handler!

        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        'Fires event. Since handler is gone, the event will be de-registered.
        test.FireEvent()

        'Assert
        Assert.AreEqual(True, weak_refToHandler.IsAlive) 'Object should be alive
        Assert.AreEqual(1, test.NrOfAliveHandlers()) 'Deregister has not occurred since this 
        Assert.AreEqual(1, count)
        Assert.IsNotNull(aHandler)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub VerifyBehaviourOverMultipleCollects()
        'Path:
        '1)Collect
        '2)Fire Event
        '3)ReleaseHandler
        '4)Collect
        '5)Fire Event
        'Result: Count = 1, handler is gone, event is deregistered

        'Arrange
        Dim count As Integer
        Dim test As New Tester
        Dim aHandler As New AHandler
        Dim weak_refToHandler As New WeakReference(aHandler) 'Holds a reference to an object, but GC is free to reclaim the memory.

        'Locally increment.
        aHandler.DiveredCall = Sub() Interlocked.Increment(count)

        AddHandler test.MyEvent, AddressOf aHandler.SomeHandler
       
        'Act
        '1)Collect
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        '2)Fire Event
        test.FireEvent() 'We fire (count should be 1 after this)


        '3)ReleaseHandler
        Assert.IsNotNull(aHandler, "Should not be empty")
        aHandler = Nothing 'no more refs to eventHandler

        '4)Collect
        GC.Collect(GC.MaxGeneration) 'Collect memory
        GC.WaitForPendingFinalizers() 'Wait until all is cleaned.

        '5)Fire Event
        test.FireEvent() 'Fires event. Since handler is gone, the event will be de-registered.

        'Assert
        Assert.AreEqual(False, weak_refToHandler.IsAlive) 'Object should be alive
        Assert.AreEqual(0, test.NrOfAliveHandlers()) 'Deregister has occurred 
        Assert.AreEqual(1, count)
    End Sub

End Class
