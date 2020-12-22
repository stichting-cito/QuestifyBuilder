Imports System.Xml.Serialization
Public Class GapImageParameter : Inherits ResourceParameter : Implements IGapChoice

    Public Enum GapImageParameterContentType
        Image
        Text
        FormulaImage
    End Enum

    Private _matchMax As Integer = 1
    Private _contentType As GapImageParameterContentType

    <XmlAttribute("id")>
    Public Property Id As String Implements IGapChoice.Id

    <XmlAttribute("matchMax")>
    Public Property MatchMax As Integer Implements IGapChoice.MatchMax
        Get
            Return _matchMax
        End Get
        Set(v As Integer)
            _matchMax = v
            NotifyPropertyChanged("MatchMax")
        End Set
    End Property

    <XmlAttribute("contentType")>
    Public Property ContentType As GapImageParameterContentType
        Get
            Return _contentType
        End Get
        Set
            _contentType = value
            NotifyPropertyChanged("ContentType")
        End Set
    End Property

    <XmlAttribute("enteredText")>
    Public Property EnteredText As String

End Class