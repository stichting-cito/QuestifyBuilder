Imports System.Xml.Serialization

Public Class CurrencyScoringParameter : Inherits DecimalScoringParameter

    Private _currencyCulture As String

    <XmlAttribute("currencyCulture")> _
    Public Property CurrencyCulture As String
        Get
            Return _currencyCulture
        End Get
        Set
            If value <> _currencyCulture Then
                _currencyCulture = value
                NotifyPropertyChanged("CurrencyCulture")
            End If
        End Set
    End Property

End Class
