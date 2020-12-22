
Imports System.IO
Imports System.Text
Imports System.Xml
Imports Cito.Tester.Common

<TestClass()> _
Public Class SerializableUriTest


    Private testContextInstance As TestContext

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestMethod()> _
    Public Sub SerializableUri_GetSchemaTest()

        Dim target As SerializableUri = New SerializableUri

        Assert.IsNull(target.GetSchema, "No schema is expected!")
    End Sub

    <TestMethod()> _
    Public Sub SerializableUri_ConstructorTest()

        Dim target As SerializableUri = New SerializableUri

        Assert.IsNotNull(target, "Object not created!")
    End Sub

    <TestMethod()> _
    Public Sub SerializableUri_ConstructorTest1()
        Dim sampleUri As String = "http://localhost/test.html"

        Dim target As SerializableUri = New SerializableUri(sampleUri)
        Dim expected As New Uri(sampleUri)

        Assert.AreEqual(expected, target.Uri, "Constructor doesn't set Uri")
    End Sub

    <TestMethod()> _
    Public Sub SerializableUri_ConstructorTest2()
        Dim sampleUri As String = "http://localhost/test.html"

        Dim expected As New Uri(sampleUri)
        Dim target As SerializableUri = New SerializableUri(expected)

        Assert.AreEqual(expected, target.Uri, "Constructor doesn't set Uri")
    End Sub

    <TestMethod()> _
    Public Sub SerializableUri_WriteAndReadXmlTest()
        Dim sampleUri As String = "http://localhost/test.html"
        Dim expected As New Uri(sampleUri)
        Dim target As SerializableUri = New SerializableUri(expected)

        Dim memory As New MemoryStream()
        Dim writer As New XmlTextWriter(memory, Encoding.UTF8)
        writer.WriteStartElement("Uri")
        target.WriteXml(writer)
        writer.WriteEndElement()
        writer.Flush()

        memory.Seek(0, SeekOrigin.Begin)
        Dim reader As New XmlTextReader(memory)
        reader.Read()
        Dim actual As New SerializableUri()
        actual.ReadXml(reader)

        memory.Dispose()

        Assert.AreEqual(Of Uri)(expected, actual.Uri, "Deserialized URI is not the same as the constructed URI")
    End Sub

    <TestMethod()> _
    Public Sub SerializableUri_ToStringTest()
        Dim sampleUri As String = "http://localhost/test.html"

        Dim target As SerializableUri = New SerializableUri(sampleUri)

        Assert.AreEqual(sampleUri, target.ToString, "Cito.Tester.Common.SerializableUri.ToString did not return the expected value.")
    End Sub

End Class
