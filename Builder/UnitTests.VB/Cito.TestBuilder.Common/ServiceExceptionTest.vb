
Imports Questify.Builder.Logic.Service.Exceptions

<TestClass()> _
Public Class ServiceExceptionTest

    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestMethod()> _
    Public Sub ServiceException_ConstructorTest1()
        Dim message As String = "A Message!"

        Dim target As New ServiceException(message)

        Assert.AreEqual(Of String)(message, target.Message, "Message is not set correctly")
    End Sub

    <TestMethod()> _
    Public Sub ServiceException_ConstructorTest2()
        Dim message As String = "A Message!"
        Dim innerException As New Exception("Haha")

        Dim target As New ServiceException(message, innerException)

        Assert.AreEqual(Of String)(message, target.Message, "Message is not set correctly")
        Assert.AreEqual(Of Exception)(innerException, target.InnerException, "Innerexception is not set correctly")
    End Sub

End Class
