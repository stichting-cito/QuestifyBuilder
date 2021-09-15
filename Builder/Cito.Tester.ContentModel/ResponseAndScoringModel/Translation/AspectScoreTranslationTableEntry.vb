Imports System.Xml.Serialization

<Serializable, XmlRoot("aspectScoreTranslationTableEntry")>
Public Class AspectScoreTranslationTableEntry
    Inherits ScoreTranslationTableEntry

    Public Sub New(rawScore As Integer, translatedScore As Double)
        MyBase.New(rawScore, translatedScore)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
