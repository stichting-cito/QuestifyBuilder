Imports System.Xml.Serialization

<Serializable, XmlRoot("attributereference")>
Public Class AttributeReference

    Public Enum WhatToCpy
        Value = 0
        Parameter
    End Enum

    <XmlAttribute("name")> _
    Public Property Name As String

    <XmlAttribute("whattocopy")> _
    Public Property WhatToCopy As WhatToCpy

    <XmlText> _
    Public Property Value As String

End Class
