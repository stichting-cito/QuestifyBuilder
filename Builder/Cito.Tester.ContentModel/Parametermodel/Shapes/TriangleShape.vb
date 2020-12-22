Imports System.Drawing
Imports System.Xml.Serialization

Public MustInherit Class TriangleShape
    Inherits Shape


    <XmlElement>
    Property BaseLeft As Point

    <XmlElement>
    Property BaseRight As Point

    <XmlElement>
    Property Top As Point

End Class
