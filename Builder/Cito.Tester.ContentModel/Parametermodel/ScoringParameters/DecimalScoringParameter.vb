Imports System.Xml.Serialization

Public Class DecimalScoringParameter : Inherits ScoringParameter

    Protected _integerPartMaxLength As Integer
    Protected _fractionPartMaxLength As Integer

    <XmlAttribute("integerPartMaxLength")> _
    Public Property IntegerPartMaxLength As Integer
        Get
            Return _integerPartMaxLength
        End Get
        Set
            If value <> _integerPartMaxLength Then
                _integerPartMaxLength = value
                NotifyPropertyChanged("IntegerPartMaxLength")
            End If
        End Set
    End Property

    <XmlAttribute("fractionPartMaxLength")> _
    Public Property FractionPartMaxLength As Integer
        Get
            Return _fractionPartMaxLength
        End Get
        Set
            If value <> _fractionPartMaxLength Then
                _fractionPartMaxLength = value
                NotifyPropertyChanged("FractionPartMaxLength")
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
