Imports System.Xml.Serialization

<Serializable>
Public MustInherit Class ScoreTranslationTableEntry

    Private _rawScore As Integer
    Private _translatedScore As Double

    <XmlAttribute("rawScore")>
    Public Property RawScore As Integer
        Get
            Return _rawScore
        End Get
        Set
            _rawScore = Value
        End Set
    End Property

    <XmlAttribute("translatedScore")>
    Public Property TranslatedScore As Double
        Get
            Return _translatedScore
        End Get
        Set
            _translatedScore = Value
        End Set
    End Property

    Public Sub New(rawScore As Integer, translatedScore As Double)
        Me.RawScore = rawScore
        Me.TranslatedScore = translatedScore
    End Sub

    Public Sub New()
    End Sub

End Class
