Imports System.Xml.Serialization

Public Class DateScoringParameter : Inherits ScoringParameter

    Private _dateFormat As String

    <XmlAttribute("dateFormat")> _
    Public Property DateFormat As String
        Get
            Return _dateFormat
        End Get
        Set
            If _dateFormat <> value Then
                _dateFormat = value
                NotifyPropertyChanged("DateFormat")
            End If
        End Set
    End Property


    <XmlIgnore> _
    Public Overrides ReadOnly Property AlternativesCount As Nullable(Of Integer)
        Get
            Return Nothing
        End Get
    End Property
End Class
