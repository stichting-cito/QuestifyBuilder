
Imports Cito.Tester.Common
Imports System.Xml
Imports System.IO


<TestClass()> _
Public Class QbProtectedConfigurationProviderTest


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

    <TestMethod()> _
    Public Sub QbProtectedConfigurationProvider_EncryptAndDecryptTest()
        Dim target As QbProtectedConfigurationProvider = New QbProtectedConfigurationProvider

        Try
            target.Initialize("TripleDESProvider", Nothing)

            Dim xml As String = "<root><tests><test key='test1'>value1</test><test key='test2'>value2</test></tests></root>"
            Dim document As New XmlDocument()
            document.LoadXml(xml)

            Dim node As XmlNode = document.FirstChild

            Dim encryptedNode As XmlNode = target.Encrypt(node)
            Assert.IsNotNull(encryptedNode, "Xml is not encrypted")
            Assert.AreNotEqual(node.OuterXml, encryptedNode.OuterXml, "Node doesn't seem to be encrypted, because it has the same information as the source.")

            Dim decryptedNode As XmlNode = target.Decrypt(encryptedNode)

            Assert.AreEqual(node.OuterXml, decryptedNode.OuterXml, "Original and decrypted node are not the same.")
        Catch ex As Exception
            Throw
        Finally
            target.Dispose()
        End Try
    End Sub

    <TestMethod()> _
    Public Sub QbProtectedConfigurationProvider_NameTest()
        Dim target As QbProtectedConfigurationProvider = New QbProtectedConfigurationProvider
        Dim val As String = "TripleDESProviderTest"
        target.Initialize(val, Nothing)

        target.Dispose()

        Assert.AreEqual(val, target.Name, "Cito.Tester.Common.QbProtectedConfigurationProvider.Name was not set correctly.")
    End Sub

End Class
