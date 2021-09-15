
Imports System.IO
Imports System.Text
Imports System.Xml
Imports Cito.Tester.Common

'''<summary>
'''This is a test class for Cito.Tester.Common.SerializableUri and is intended
'''to contain all Cito.Tester.Common.SerializableUri Unit Tests
'''</summary>
<TestClass()> _
Public Class SerializableUriTest


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

    '''<summary>
    '''A test for GetSchema()
    '''</summary>
    <TestMethod()> _
    Public Sub SerializableUri_GetSchemaTest()
        'Arrange

        'Act
        Dim target As SerializableUri = New SerializableUri

        'Assert
        Assert.IsNull(target.GetSchema, "No schema is expected!")
    End Sub

    '''<summary>
    '''A test for New()
    '''</summary>
    <TestMethod()> _
    Public Sub SerializableUri_ConstructorTest()
        'Arrange
        
        'Act
        Dim target As SerializableUri = New SerializableUri

        'Arrange
        Assert.IsNotNull(target, "Object not created!")
    End Sub

    '''<summary>
    '''A test for New(ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub SerializableUri_ConstructorTest1()
        'Arrange
        Dim sampleUri As String = "http://localhost/test.html"

        'Act
        Dim target As SerializableUri = New SerializableUri(sampleUri)
        Dim expected As New Uri(sampleUri)

        'Assert
        Assert.AreEqual(expected, target.Uri, "Constructor doesn't set Uri")
    End Sub

    '''<summary>
    '''A test for New(ByVal System.Uri)
    '''</summary>
    <TestMethod()> _
    Public Sub SerializableUri_ConstructorTest2()
        'Arrange
        Dim sampleUri As String = "http://localhost/test.html"

        'Act
        Dim expected As New Uri(sampleUri)
        Dim target As SerializableUri = New SerializableUri(expected)

        'Assert
        Assert.AreEqual(expected, target.Uri, "Constructor doesn't set Uri")
    End Sub

    '''<summary>
    '''A test for ReadXml(ByVal System.Xml.XmlReader)
    '''</summary>
    <TestMethod()> _
    Public Sub SerializableUri_WriteAndReadXmlTest()
        'Arrange
        Dim sampleUri As String = "http://localhost/test.html"
        Dim expected As New Uri(sampleUri)
        Dim target As SerializableUri = New SerializableUri(expected)

        'Act
        'Serialize to xml
        Dim memory As New MemoryStream()
        Dim writer As New XmlTextWriter(memory, Encoding.UTF8)
        writer.WriteStartElement("Uri")
        target.WriteXml(writer)
        writer.WriteEndElement()
        writer.Flush()

        'Deserialize from xml
        memory.Seek(0, SeekOrigin.Begin)
        Dim reader As New XmlTextReader(memory)
        reader.Read()
        Dim actual As New SerializableUri()
        actual.ReadXml(reader)

        memory.Dispose()

        'Assert
        Assert.AreEqual(Of Uri)(expected, actual.Uri, "Deserialized URI is not the same as the constructed URI")
    End Sub

    '''<summary>
    '''A test for ToString()
    '''</summary>
    <TestMethod()> _
    Public Sub SerializableUri_ToStringTest()
        'Arrange
        Dim sampleUri As String = "http://localhost/test.html"

        'Act
        Dim target As SerializableUri = New SerializableUri(sampleUri)

        'Assert
        Assert.AreEqual(sampleUri, target.ToString, "Cito.Tester.Common.SerializableUri.ToString did not return the expected value.")
    End Sub

End Class
