Imports System.Xml.Serialization

Public Class TimeScoringParameter : Inherits ScoringParameter

    Private _timeFormat As String

    <XmlAttribute("timeFormat")> _
    Public Property TimeFormat As String
        Get
            Return _timeFormat
        End Get
        Set
            If _timeFormat <> value Then
                _timeFormat = value
                NotifyPropertyChanged("TimeFormat")
            End If
        End Set
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property AlternativesCount As Integer?
        Get
            Return Nothing
        End Get
    End Property
End Class
