Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml

Public Class QbProtectedConfigurationProvider
    Inherits ProtectedConfigurationProvider
    Implements IDisposable

    Private IV_VALUE As String
    Private KEY_VALUE As String

    Private _des As TripleDESCryptoServiceProvider
    Private _pName As String
    Private _disposedValue As Boolean


    Public Overrides ReadOnly Property Name() As String
        Get
            Return _pName
        End Get
    End Property


    Public Sub New()
        _des = New TripleDESCryptoServiceProvider()
        ReadKeysFromFile()
    End Sub

    Public Overrides Sub Initialize(name As String, config As NameValueCollection)
        _pName = name
        _des.Key = HexToByte(KEY_VALUE)
        _des.IV = HexToByte(IV_VALUE)
    End Sub

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
    Public Overrides Function Encrypt(node As XmlNode) As XmlNode
        ReflectionHelper.CheckIsNotNothing(node, "config node")
        Dim encryptedData As String = EncryptString(node.OuterXml)

        Dim xmlDoc As XmlDocument = New XmlDocument()
        xmlDoc.PreserveWhitespace = True
        xmlDoc.LoadXml("<EncryptedData>" & encryptedData & "</EncryptedData>")

        Return xmlDoc.DocumentElement
    End Function

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")>
    Public Overrides Function Decrypt(encryptedNode As XmlNode) As XmlNode
        ReflectionHelper.CheckIsNotNothing(encryptedNode, "encrypted configuration node")
        Dim decryptedData As String = DecryptString(encryptedNode.InnerText)

        Dim xmlDoc As XmlDocument = New XmlDocument()
        xmlDoc.PreserveWhitespace = True
        xmlDoc.LoadXml(decryptedData)

        Return xmlDoc.DocumentElement
    End Function

    Public Sub CreateKey(filePath As String)
        _des.GenerateKey()
        _des.GenerateIV()

        Dim sw As StreamWriter = New StreamWriter(filePath, False)
        sw.WriteLine("Key: " + ByteToHex(_des.Key))
        sw.WriteLine("IV: " + ByteToHex(_des.IV))
        sw.Close()
    End Sub





    Private Function EncryptString(encryptValue As String) As String

        Dim valBytes() As Byte = Encoding.Unicode.GetBytes(encryptValue)

        Dim transform As ICryptoTransform = _des.CreateEncryptor()

        Using ms As MemoryStream = New MemoryStream()
            Using cs As CryptoStream = New CryptoStream(ms, transform, CryptoStreamMode.Write)
                cs.Write(valBytes, 0, valBytes.Length)
                cs.FlushFinalBlock()
                Dim returnBytes() As Byte = ms.ToArray()
                cs.Close()
                Return Convert.ToBase64String(returnBytes)
            End Using
        End Using

    End Function



    Private Function DecryptString(encryptedValue As String) As String
        Dim valBytes() As Byte = Convert.FromBase64String(encryptedValue)

        Dim transform As ICryptoTransform = _des.CreateDecryptor()

        Dim ms As MemoryStream = New MemoryStream()
        Dim cs As CryptoStream = New CryptoStream(ms, transform, CryptoStreamMode.Write)
        cs.Write(valBytes, 0, valBytes.Length)
        cs.FlushFinalBlock()
        Dim returnBytes() As Byte = ms.ToArray()
        cs.Close()

        Return Encoding.Unicode.GetString(returnBytes)
    End Function


    Private Shared Function ByteToHex(byteArray As Byte()) As String
        Dim outStringBuilder As New StringBuilder()

        For Each b As Byte In byteArray
            outStringBuilder.Append(b.ToString("X2"))
        Next

        Return outStringBuilder.ToString()
    End Function



    Private Shared Function HexToByte(hexString As String) As Byte()
        Dim returnBytes() As Byte = New Byte(CInt((hexString.Length / 2) - 1)) {}

        For i As Integer = 0 To returnBytes.Length - 1
            returnBytes(i) = Convert.ToByte(hexString.Substring(i * 2, 2), 16)
        Next

        Return returnBytes
    End Function

    <SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId:="_des")>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me._disposedValue Then
            If disposing Then
                If _des IsNot Nothing Then
                    _des = Nothing
                End If
            End If
        End If
        Me._disposedValue = True
    End Sub

    Private Sub ReadKeysFromFile()
        Dim folder As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        Dim filePath = Path.Combine(folder, "CryptoKey.txt")
        If (File.Exists(filePath)) Then
            Dim lines = File.ReadLines(filePath)
            IV_VALUE = lines.First()
            KEY_VALUE = lines.Last()
        Else
            _des.GenerateIV()
            _des.GenerateKey()
            IV_VALUE = ByteToHex(_des.IV)
            KEY_VALUE = ByteToHex(_des.Key)
        End If

    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Private Class TripleDesProviderKeys
        Public Key As String
        Public Value As String
    End Class

End Class
