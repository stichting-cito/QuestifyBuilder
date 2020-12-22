Imports System.Xml.Serialization

Public Class IntegerScoringParameter : Inherits ScoringParameter

    Private _maxLength As Integer

    <XmlAttribute("maxLength")> _
    Public Property MaxLength As Integer
        Get
            Return _maxLength
        End Get
        Set
            If value <> _maxLength Then
                _maxLength = value
                NotifyPropertyChanged("MaxLength")
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
