Imports System.Xml.Serialization

<Serializable, XmlRoot("group")> _
Public Class Group

    <XmlAttribute("identifier")> _
    Public Property Identifier As String

    <XmlAttribute("sortIndex")> _
    Public Property SortIndex As Integer

    <XmlAttribute("title")> _
    Public Property Title As String

    <XmlAttribute("collapsed")> _
    Public Property Collapsed As Boolean

End Class
