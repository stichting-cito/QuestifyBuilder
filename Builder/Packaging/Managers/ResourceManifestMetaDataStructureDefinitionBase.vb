
Imports System.Xml.Serialization

Public Class ResourceManifestMetaDataStructureDefinitionBase
    Inherits ResourceManifestMetadataDefinitionBase

    Private _code As Guid

    <XmlAttribute("code")> _
    Public Property Code As Guid
        Get
            Return _code
        End Get
        Set(value As Guid)
            _code = value
        End Set
    End Property

End Class
