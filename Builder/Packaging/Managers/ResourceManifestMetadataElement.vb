Imports System.Xml.Serialization

Public Class ResourceManifestMetadataElement

    <XmlAttribute("title")>
    Public Property Title() As String

    <XmlAttribute("name")>
    Public Property Name() As String

    <XmlAttribute("metaDataType")>
    Public Property MetaDataType() As ResourceManifestMetadataDefinitionBase.enumMetaDataType

    <XmlElement("value")>
    Public ReadOnly Property Values() As New ResourceManifestMetadataValueCollection

    <XmlAttribute("reference")>
    Public Property Reference() As String

    Public Sub New()

    End Sub

    Public Sub New(name As String, title As String, metaDataType As ResourceManifestMetadataDefinitionBase.enumMetaDataType)

        Me.Name = name
        Me.Title = title
        Me.MetaDataType = metaDataType
    End Sub

End Class
