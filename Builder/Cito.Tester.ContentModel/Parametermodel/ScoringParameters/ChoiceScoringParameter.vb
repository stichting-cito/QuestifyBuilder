Imports System.Xml.Serialization

Public Class ChoiceScoringParameter : Inherits ScoringParameter


    Private _minChoices As Integer
    Private _maxChoices As Integer

    <XmlAttribute("minChoices")>
    Public Property MinChoices As Integer
        Get
            Return _minChoices
        End Get
        Set
            If Value <> _minChoices Then
                _minChoices = Value
                NotifyPropertyChanged("MinChoices")
            End If
        End Set
    End Property

    <XmlAttribute("maxChoices")>
    Public Property MaxChoices As Integer
        Get
            Return _maxChoices
        End Get
        Set
            If Value <> _maxChoices Then
                _maxChoices = Value
                NotifyPropertyChanged("MaxChoices")
            End If
        End Set
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property AlternativesCount As Nullable(Of Integer)
        Get
            If Value IsNot Nothing AndAlso Value.Count > 0 Then
                Return Value.Count
            Else
                Return Nothing
            End If
        End Get
    End Property


    <XmlIgnore>
    Public Overrides ReadOnly Property IsSingleChoice As Boolean
        Get
            Return MaxChoices = 1
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IsSingleValue As Boolean
        Get
            Return True
        End Get
    End Property
End Class
