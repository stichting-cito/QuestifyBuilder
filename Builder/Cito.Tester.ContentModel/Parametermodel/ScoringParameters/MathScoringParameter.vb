Imports System.Xml.Serialization


Public Class MathScoringParameter : Inherits ScoringParameter
    <XmlIgnore> _
    Public Overrides ReadOnly Property AlternativesCount As Nullable(Of Integer)
        Get
            Return Nothing
        End Get
    End Property
End Class
