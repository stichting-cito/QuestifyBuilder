Imports System.Xml.Serialization

Public Class MultiChoiceScoringParameter : Inherits ChoiceScoringParameter

    Private _multichoice As MultiChoiceType

    <XmlAttribute("multiChoice")> _
    Public Property MultiChoice As MultiChoiceType
        Get
            Return _multichoice
        End Get
        Set
            If value <> _multichoice Then
                _multichoice = value
                NotifyPropertyChanged("MultiChoice")
            End If
        End Set
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IsSingleValue As Boolean
        Get
            Return False
        End Get
    End Property

End Class
