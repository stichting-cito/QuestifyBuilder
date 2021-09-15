
Imports Cito.Tester.Common
Imports System.Xml
Imports System.IO


'''<summary>
'''This is a test class for Cito.Tester.Common.QbProtectedConfigurationProvider and is intended
'''to contain all Cito.Tester.Common.QbProtectedConfigurationProvider Unit Tests
'''</summary>
<TestClass()> _
Public Class QbProtectedConfigurationProviderTest


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

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''A test for CreateKey(ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub QbProtectedConfigurationProvider_CreateKeyTest()
        Dim target As QbProtectedConfigurationProvider = New QbProtectedConfigurationProvider
        Try
            Dim filePath As String = "keytest.txt"
            target.CreateKey(filePath)

            Dim keyFileInfo As New FileInfo(filePath)

            target.Dispose()
            Assert.IsTrue(keyFileInfo.Exists, "File not created")
        Catch ex As Exception
            Throw
        Finally
            target.Dispose()
        End Try
    End Sub

    '''<summary>
    '''A test for Encrypt(ByVal System.Xml.XmlNode)
    '''</summary>
    <TestMethod()> _
    Public Sub QbProtectedConfigurationProvider_EncryptAndDecryptTest()
        Dim target As QbProtectedConfigurationProvider = New QbProtectedConfigurationProvider

        Try
            target.Initialize("TripleDESProvider", Nothing)

            ' create xml document
            Dim xml As String = "<root><tests><test key='test1'>value1</test><test key='test2'>value2</test></tests></root>"
            Dim document As New XmlDocument()
            document.LoadXml(xml)

            ' get a node from document
            Dim node As XmlNode = document.FirstChild

            ' encrypt node
            Dim encryptedNode As XmlNode = target.Encrypt(node)
            Assert.IsNotNull(encryptedNode, "Xml is not encrypted")
            Assert.AreNotEqual(node.OuterXml, encryptedNode.OuterXml, "Node doesn't seem to be encrypted, because it has the same information as the source.")

            ' now decrypt the node
            Dim decryptedNode As XmlNode = target.Decrypt(encryptedNode)

            Assert.AreEqual(node.OuterXml, decryptedNode.OuterXml, "Original and decrypted node are not the same.")
        Catch ex As Exception
            Throw
        Finally
            target.Dispose()
        End Try
    End Sub

    '''<summary>
    '''A test for Name()
    '''</summary>
    <TestMethod()> _
    Public Sub QbProtectedConfigurationProvider_NameTest()
        Dim target As QbProtectedConfigurationProvider = New QbProtectedConfigurationProvider
        Dim val As String = "TripleDESProviderTest"
        target.Initialize(val, Nothing)

        target.Dispose()

        Assert.AreEqual(val, target.Name, "Cito.Tester.Common.QbProtectedConfigurationProvider.Name was not set correctly.")
    End Sub

End Class
