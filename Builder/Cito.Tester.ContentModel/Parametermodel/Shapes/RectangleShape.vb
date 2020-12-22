Imports System.Drawing
Imports System.Xml.Serialization


Public Class RectangleShape
    Inherits Shape


    <XmlElement>
    Property TopLeft As Point

    <XmlElement>
    Property BottomRight As Point

End Class
