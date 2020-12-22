
Imports System.Xml.Serialization

Public Class ResourceManifestMetadataMultiValueDefinition
    Inherits ResourceManifestMetadataDefinitionBase


    Private _listValues As New ResourceManifestMetadataListValueCollection
    Private _multiSelect As Boolean = False
    Private _code As Guid



    Public Property ListValues() As ResourceManifestMetadataListValueCollection
        Get
            Return _listValues
        End Get
        Set(value As ResourceManifestMetadataListValueCollection)
            _listValues = value
        End Set
    End Property


    <XmlAttribute("multiSelect")> _
    Public Property MultiSelect() As Boolean
        Get
            Return _multiSelect
        End Get
        Set(value As Boolean)
            _multiSelect = value
        End Set
    End Property



    <XmlAttribute("code")>
    Public Property Code() As Guid
        Get
            Return _code
        End Get
        Set(value As Guid)
            _code = value
        End Set
    End Property



    Public Sub New()

    End Sub
End Class
