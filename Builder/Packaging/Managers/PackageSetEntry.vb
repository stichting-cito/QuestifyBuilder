Imports System.Xml.Serialization

<Serializable()> _
Public Class PackageSetEntry

    <XmlAttribute("name")>
    Public Property Name() As String

    <XmlElement("PackageSetEntryCollection")>
    Public ReadOnly Property PackageSetEntrySubCollection() As New PackageSetEntryCollection

    <XmlAttribute("packageUri")>
    Public Property PackageUri() As String

End Class