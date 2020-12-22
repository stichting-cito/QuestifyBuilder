
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("concept")> _
Public Class Concept

    Private _value As Integer
    Private _code As String


    Public Sub New()
    End Sub

    Public Sub New(code As String, value As Integer)
        _code = code
        _value = value
    End Sub

    <XmlAttribute("value")> _
    Public Property Value As Integer
        Get
            Return _value
        End Get
        Set
            _value = value
        End Set
    End Property

    <XmlAttribute("code")> _
    Public Property Code As String
        Get
            Return _code
        End Get
        Set
            _code = value
        End Set
    End Property





End Class
