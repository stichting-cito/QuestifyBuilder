
Imports Questify.Builder.Logic.Service.Exceptions

'''<summary>
'''This is a test class for UIException and is intended
'''to contain all UIException Unit Tests
'''</summary>
<TestClass()> _
Public Class UIExceptionTest

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

    '''<summary>
    '''A test for New(ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub UIException_ConstructorTest1()
        'Arrange
        Dim message As String = "A Message!"

        'Act
        Dim target As New UIException(message)

        'Assert
        Assert.AreEqual(Of String)(message, target.Message, "Message is not set correctly")
    End Sub

    '''<summary>
    '''A test for New(ByVal String, ByVal System.Exception)
    '''</summary>
    <TestMethod()> _
    Public Sub UIException_ConstructorTest2()
        'Arrange
        Dim message As String = "A Message!"
        Dim innerException As New Exception("haha")

        'Act
        Dim target As New UIException(message, innerException)

        'Assert
        Assert.AreEqual(Of String)(message, target.Message, "Message is not set correctly")
        Assert.AreEqual(Of Exception)(innerException, target.InnerException, "Innerexception is not set correctly")
    End Sub

End Class
