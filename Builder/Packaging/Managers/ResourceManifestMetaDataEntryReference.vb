
Imports System.Xml.Serialization

Public Class ResourceManifestMetaDataEntryReference


    Private _name As String
    Private _metaData As New ResourceManifestMetadataCollection



    <XmlAttribute("name")> _
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public ReadOnly Property MetaData() As ResourceManifestMetadataCollection
        Get
            Return Me._metaData
        End Get
    End Property



    Public Sub New()
    End Sub


End Class
