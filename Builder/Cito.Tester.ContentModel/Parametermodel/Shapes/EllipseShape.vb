
Imports System.Drawing
Imports System.Xml.Serialization


Public Class EllipseShape
    Inherits Shape

    <XmlElement>
    Public Property AnchorPoint As Point

    <XmlAttribute("hradius")>
    Public Property HRadius As Integer

    <XmlAttribute("vradius")>
    Public Property VRadius As Integer

End Class

