
Imports System.Xml.Serialization

Public Class BooleanScoringParameter
    Inherits ScoringParameter

    <XmlIgnore>
    Public Overrides ReadOnly Property AlternativesCount As Integer?
        Get
            Return Nothing
        End Get
    End Property
End Class
