
Imports System.Xml.Serialization

<Serializable> _
Public Class ListValue

    Private _key As String
    Private _displayvalue As String

    <XmlAttribute("key")> _
    Public Property Key As String
        Get
            Return _key
        End Get
        Set
            _key = value
        End Set
    End Property

    <XmlText> _
    Public Property DisplayValue As String
        Get
            Return _displayvalue
        End Get
        Set
            _displayvalue = value
        End Set
    End Property
End Class
