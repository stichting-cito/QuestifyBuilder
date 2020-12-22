Imports System.Text
Imports System.Xml.Serialization

Public Class SerializationRequestDTO
    Public Property StripNameSpaces As Boolean

    Public Property NameSpaces As XmlSerializerNamespaces

    Public Property OmitXmlDeclaration As Boolean

    Public Property Encoding As Encoding = new UTF8Encoding(False)

    Public Property OverrideDefaultNameSpace As String

    Public Property OverrideClassName As String

    Public Property Indent As Boolean
End Class