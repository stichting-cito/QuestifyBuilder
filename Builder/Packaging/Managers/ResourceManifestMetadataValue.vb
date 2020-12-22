Imports System.Xml.Serialization

Public Class ResourceManifestMetadataValue
    <XmlAttribute("name")>
    Public Property Name() As String = String.Empty

    <XmlAttribute("value")>
    Public Property Value() As String = String.Empty

    Public Sub New()
    End Sub

    Public Sub New(name As String, value As String)
        Me.Name = name
        Me.Value = value
    End Sub

End Class
