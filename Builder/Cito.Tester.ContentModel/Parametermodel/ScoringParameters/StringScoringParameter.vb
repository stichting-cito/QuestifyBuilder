Imports System.Xml.Serialization

Public Class StringScoringParameter : Inherits ScoringParameter

    Private _expectedLength As Integer
    Private _patternMask As String
    Private _preprocessRules As String = String.Empty

    Private _usablePreprocessors As IEnumerable(Of IResponseKeyValuePreprocessor)

    <XmlAttribute("expectedLength")> _
    Public Property ExpectedLength As Integer
        Get
            Return _expectedLength
        End Get
        Set
            If value <> _expectedLength Then
                _expectedLength = value
                NotifyPropertyChanged("ExpectedLength")
            End If
        End Set
    End Property

    <XmlAttribute("patternMask")> _
    Public Property PatternMask As String
        Get
            Return _patternMask
        End Get
        Set
            If value <> _patternMask Then
                _patternMask = value
                NotifyPropertyChanged("PatternMask")
            End If
        End Set
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property AlternativesCount As Nullable(Of Integer)
        Get
            Return Nothing
        End Get
    End Property


    <XmlIgnore>
    Public Property PreprocessRules As String
        Get
            Return _preprocessRules
        End Get
        Set
            If value <> _preprocessRules Then
                _preprocessRules = value
                NotifyPropertyChanged("PreprocessRules")
            End If
        End Set
    End Property

End Class
