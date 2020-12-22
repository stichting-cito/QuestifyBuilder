Imports System.Xml.Serialization

Public Class InlineChoiceScoringParameter : Inherits ChoiceScoringParameter

    <XmlIgnore> _
    Public Overrides ReadOnly Property IsSingleChoice As Boolean
        Get
            Return True
        End Get
    End Property


    <XmlIgnore> _
    Public Overrides ReadOnly Property IsSingleValue As Boolean
        Get
            Return True
        End Get
    End Property
End Class
