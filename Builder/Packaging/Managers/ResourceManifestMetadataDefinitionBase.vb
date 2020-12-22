Imports System.Xml.Serialization

Public MustInherit Class ResourceManifestMetadataDefinitionBase

    Public Enum enumMetaDataType
        unknown = 0
        BankMetaData = 1
        BankCustomProperty = 2
    End Enum

    <XmlAttribute("name")>
    Public Property Name() As String

    <XmlAttribute("title")>
    Public Property Title() As String

    <XmlAttribute("metaDataType")>
    Public Property MetaDataType() As enumMetaDataType

    <XmlAttribute("applicableTo")>
    Public Property ApplicableTo() As Integer = 0

    <XmlAttribute("publishable")>
    Public Property Publishable() As Boolean = False
    <XmlAttribute("scorable")>
    Public Property Scorable() As Boolean = False
End Class
