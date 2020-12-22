Imports System.Drawing
Imports System.Xml.Serialization


Public Class CircleShape
    Inherits Shape

    <XmlElement>
    Public Property AnchorPoint As Point

    <XmlAttribute("radius")>
    Public Property Radius As Integer

End Class
