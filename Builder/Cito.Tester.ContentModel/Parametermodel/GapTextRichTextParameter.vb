Imports System.Xml.Serialization

Public Class GapTextRichTextParameter : Inherits XHtmlParameter : Implements IGapChoice

    Private _matchMax As Integer = 1

    <XmlAttribute("id")>
    Public Property Id As String Implements IGapChoice.Id

    <XmlAttribute("matchMax")>
    Public Property MatchMax As Integer Implements IGapChoice.MatchMax
        Get
            Return _matchMax
        End Get
        Set(matchMaxValue As Integer)
            _matchMax = matchMaxValue
            NotifyPropertyChanged("MatchMax")
        End Set
    End Property

End Class
