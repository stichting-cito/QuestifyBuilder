
Imports System.Windows.Forms
Imports Cito.Tester.Common.WeakEventHandler

<TestClass()>
Public Class VanillaEventHandlersTest

    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub Demonstrate_KeyEventHandlerTest()
        Dim tester As New Tester

        AddHandler tester.keyEvnt, New KeyEventHandler(AddressOf dummyKeyHandler).MakeWeak(Sub(e)

                                                                                           End Sub)

    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub Demonstrate_FormClosedEventHandlerTest()
        Dim tester As New Tester

        AddHandler tester.formClose, New FormClosedEventHandler(AddressOf dummyFrmCloseHandler).MakeWeak(Sub(e)

                                                                                                         End Sub)
    End Sub

    Private Sub dummyKeyHandler(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub dummyFrmCloseHandler(sender As Object, e As FormClosedEventArgs)

    End Sub

    Class Tester

        Public Event keyEvnt As KeyEventHandler
        Public Event formClose As FormClosedEventHandler

    End Class

End Class
