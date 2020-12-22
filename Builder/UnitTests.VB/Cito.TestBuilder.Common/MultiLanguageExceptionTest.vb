
Imports Questify.Builder.Configuration

<TestClass()> _
Public Class MultiLanguageExceptionTest

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
    Public Sub MultiLanguageException_ConstructorTest1()
        Dim message As String = "A Message!"

        Dim target As New MultiLanguageException(message)

        Assert.AreEqual(Of String)(message, target.Message, "Message is not set correctly")
    End Sub

    <TestMethod()> _
    Public Sub MultiLanguageException_ConstructorTest2()
        Dim message As String = "A Message!"
        Dim innerException As New Exception("Hoho")

        Dim target As New MultiLanguageException(message, innerException)

        Assert.AreEqual(Of String)(message, target.Message, "Message is not set correctly")
        Assert.AreEqual(Of Exception)(innerException, target.InnerException, "Innerexception is not set correctly")
    End Sub

End Class
