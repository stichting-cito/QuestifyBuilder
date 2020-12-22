Imports System.Xml.Serialization

Public Class ResourceManifestMetadataListValue

    <XmlAttribute("title")>
    Public Property Title() As String

    <XmlAttribute("name")>
    Public Property Name() As String

    <XmlAttribute("value")>
    Public Property Value() As String

    <XmlAttribute("reference")>
    Public Property Reference() As String

    <XmlAttribute("code")>
    Public Property Code() As Guid

    Public Sub New()
    End Sub

    Public Sub New(name As String, title As String, value As String, code As Guid)
        Me.Name = name
        Me.Title = title
        Me.Value = value
        Me.Code = code
    End Sub

End Class
