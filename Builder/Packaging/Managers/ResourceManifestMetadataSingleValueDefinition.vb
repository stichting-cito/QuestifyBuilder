
Imports System.Xml.Serialization

Public Class ResourceManifestMetadataSingleValueDefinition
    Inherits ResourceManifestMetadataDefinitionBase

    Private _richText As Boolean = False

    <XmlAttribute("richText")> _
    Public Property RichText() As Boolean
        Get
            Return _richText
        End Get
        Set(value As Boolean)
            _richText = value
        End Set
    End Property

End Class
