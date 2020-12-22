Imports System.Xml.Serialization

Public Class GeogebraScoringParameter : Inherits StringScoringParameter

    Private _geogebraKey As String = String.Empty

    <XmlAttribute("geogebraKey")> _
    Public Property GeogebraKey As String
        Get
            Return _geogebraKey
        End Get
        Set
            If value <> _geogebraKey Then
                _geogebraKey = value
                NotifyPropertyChanged("GeogebraKey")
            End If
        End Set
    End Property

End Class
