Imports System.Xml.Serialization

Public Class AspectScoringParameter
    Inherits ScoringParameter

    Public Const DEFAULT_ASPECT_NAME As String = "DefaultAspect"

    Private _singleAspectScoringEditor As Boolean
    Private _aspectScoreEditorBoundAspect As String
    Private _autoScoringOffPrm As Boolean

    <XmlAttribute("singleAspectScoringEditor")>
    Public Property SingleAspectScoringEditor As Boolean
        Get
            Return _singleAspectScoringEditor
        End Get
        Set
            If Value <> _singleAspectScoringEditor Then
                _singleAspectScoringEditor = Value
                NotifyPropertyChanged("SingleAspectScoringEditor")
            End If
        End Set
    End Property

    <XmlAttribute("aspectScoreEditorBoundAspect")>
    Public Property AspectScoreEditorBoundAspect As String
        Get
            If String.IsNullOrEmpty(_aspectScoreEditorBoundAspect) Then
                _aspectScoreEditorBoundAspect = DEFAULT_ASPECT_NAME
            End If
            Return _aspectScoreEditorBoundAspect
        End Get
        Set
            If Value <> _aspectScoreEditorBoundAspect Then
                _aspectScoreEditorBoundAspect = Value
                NotifyPropertyChanged("AspectScoreEditorBoundAspect")
            End If
        End Set
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property AlternativesCount As Integer?
        Get
            Return Nothing
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IsSingleValue As Boolean
        Get
            Return True
        End Get
    End Property

    <XmlIgnore>
    Public Property AutoScoringOffPrm As Boolean
        Get
            Return _autoScoringOffPrm
        End Get
        Set(value As Boolean)
            If value <> _autoScoringOffPrm Then
                _autoScoringOffPrm = value
            End If
        End Set
    End Property
End Class