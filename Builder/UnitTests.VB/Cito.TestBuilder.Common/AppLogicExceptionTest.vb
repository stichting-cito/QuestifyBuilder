
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports Questify.Builder.Logic.Service.Exceptions


<TestClass()>
Public Class AppLogicExceptionTest


    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestMethod()>
    Public Sub AppLogicException_ConstructorTest1()
        Dim message As String = "A Message!"
        Dim target As New AppLogicException(message)

        Assert.AreEqual(Of String)(message, target.Message, "Message is not set correctly")
    End Sub

    <TestMethod()>
    Public Sub AppLogicException_ConstructorTest2()
        Dim message As String = "This is the message!"
        Dim innerException As New Exception("Huhu")
        Dim target As New AppLogicException(message, innerException)

        Assert.AreEqual(Of String)(message, target.Message, "Message is not set correctly")
        Assert.AreEqual(Of Exception)(innerException, target.InnerException, "Innerexception is not set correctly")
    End Sub

    <TestMethod(), Description("FXCOP rule")>
    Public Sub AppLogicException_SerializerConstructor()
        Dim data As String = String.Empty

        Using ms = New MemoryStream
            Dim formatter = New BinaryFormatter()
            formatter.Serialize(ms, New AppLogicException("Test"))

            data = Convert.ToBase64String(ms.ToArray())
        End Using

        Dim result As AppLogicException
        Using ms2 = New MemoryStream(Convert.FromBase64String(data))
            Dim formatter2 = New BinaryFormatter()
            result = TryCast(formatter2.Deserialize(ms2), AppLogicException)

        End Using

        Assert.IsNotNull(result)
    End Sub


End Class
