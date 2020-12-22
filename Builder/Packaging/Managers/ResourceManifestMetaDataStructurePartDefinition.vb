Imports System.Xml.Serialization

Public Class ResourceManifestMetaDataStructurePartDefinition

    <XmlAttribute("name")>
    Public Property Name As String
    <XmlAttribute("title")>
    Public Property Title As String

    <XmlAttribute("code")>
    Public Property Code As Guid

    <XmlAttribute("visualOrder")>
    Public Property VisualOrder As Integer

End Class
