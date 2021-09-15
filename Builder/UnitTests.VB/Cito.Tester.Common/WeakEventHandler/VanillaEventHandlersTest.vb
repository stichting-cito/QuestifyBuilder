
Imports System.Windows.Forms
Imports Cito.Tester.Common.WeakEventHandler

<TestClass()>
Public Class VanillaEventHandlersTest

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
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
        'Arrange
        Dim tester As New Tester

        'Act
        AddHandler tester.keyEvnt, New KeyEventHandler(AddressOf dummyKeyHandler).MakeWeak(Sub(e)

                                                                                           End Sub)

        'Verify
        'No actual test if construct works, just that the weakEventHandler has been created.
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub Demonstrate_FormClosedEventHandlerTest()
        'Arrange
        Dim tester As New Tester

        'Act
        AddHandler tester.formClose, New FormClosedEventHandler(AddressOf dummyFrmCloseHandler).MakeWeak(Sub(e)

                                                                                                         End Sub)
        'Verify
        'No actual test if construct works, just that the weakEventHandler has been created.
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
