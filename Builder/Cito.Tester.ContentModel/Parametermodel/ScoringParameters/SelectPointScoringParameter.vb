Imports System.Xml.Serialization

Public Class SelectPointScoringParameter
    Inherits HotspotScoringParameter

    <XmlIgnore> _
    Public Overrides ReadOnly Property IsSingleChoice As Boolean
        Get
            Return MaxChoices = 1
        End Get
    End Property

End Class
