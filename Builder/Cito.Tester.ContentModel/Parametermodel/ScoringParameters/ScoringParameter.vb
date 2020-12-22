Imports System.Xml.Serialization

Public MustInherit Class ScoringParameter
    Inherits CollectionParameter

    Private _controllerId As String
    Private _findingOverride As String
    Private _inlineId As String
    Private _collectionIdx As String
    Private _label As String
    Private _groupInitially As Boolean = False
    Private _supportCasScoring As Boolean = False
    Public Const ElementLabelParameterName As String = "elementLabel"

    <XmlAttribute("label")>
    Public Property Label As String
        Get
            Return _label
        End Get
        Set
            If value <> _label Then
                _label = value
                NotifyPropertyChanged("Label")
            End If
        End Set
    End Property

    <XmlAttribute("ControllerId")>
    Public Property ControllerId As String
        Get
            Return _controllerId
        End Get
        Set
            If value <> _controllerId Then
                _controllerId = value
                NotifyPropertyChanged("ControllerId")
                NotifyPropertyChanged("FindingId")
            End If
        End Set
    End Property

    <XmlAttribute("findingOverride")>
    Public Property FindingOverride As String
        Get
            Return _findingOverride
        End Get
        Set
            If value <> _findingOverride Then
                _findingOverride = value
                NotifyPropertyChanged("FindingOverride")
            End If
        End Set
    End Property

    <XmlAttribute("supportCasScoring")>
    Public Property SupportCasScoring As Boolean
        Get
            Return _supportCasScoring
        End Get
        Set
            If value <> _supportCasScoring Then
                _supportCasScoring = value
                NotifyPropertyChanged("supportCasScoring")
            End If
            SupportCasScoringSpecified = value
        End Set
    End Property

    <XmlIgnore>
    Public Property SupportCasScoringSpecified As Boolean

    <XmlIgnore>
    Public Overridable Property InlineId As String
        Get
            Return _inlineId
        End Get
        Set
            If value <> _inlineId Then
                _inlineId = value
                NotifyPropertyChanged("InlineId")
                NotifyPropertyChanged("FindingId")
            End If
        End Set
    End Property

    <XmlIgnore>
    Public ReadOnly Property FindingId As String
        Get
            If Not String.IsNullOrEmpty(FindingOverride) Then
                Return FindingOverride
            ElseIf Not String.IsNullOrEmpty(InlineId) Then
                Return InlineId
            End If
            Return ControllerId
        End Get
    End Property

    <XmlIgnore>
    Public Property CollectionIdx As String
        Get
            Return _collectionIdx
        End Get
        Set
            If value <> _collectionIdx Then
                _collectionIdx = value
                NotifyPropertyChanged("CollectionIdx")
            End If
        End Set
    End Property

    <XmlIgnore>
    Public MustOverride ReadOnly Property AlternativesCount As Nullable(Of Integer)

    Public Overridable Function GetParametersWithDesignerSettings() As IEnumerable(Of ParameterBase)
        Return Nothing
    End Function

    Public Overridable Function GetLabelFor(scoreKey As String) As String
        Return Label
    End Function

    <XmlIgnore>
    Public Overridable ReadOnly Property IsSingleChoice As Boolean
        Get
            Return False
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property IsSingleValue As Boolean
        Get
            Return False
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property GroupInitially As Boolean
        Get
            Return False
        End Get
    End Property


    <XmlIgnore>
    Public Overridable ReadOnly Property MustRemainGrouped As Boolean
        Get
            Return False
        End Get
    End Property


    Public Overridable ReadOnly Property ShouldSuffix As Boolean
        Get
            Return Value IsNot Nothing AndAlso Value.Count > 1
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property Groupable As Boolean
        Get
            Return True
        End Get
    End Property

    <XmlIgnore>
    Public Overridable ReadOnly Property AlternativesCanBeAdded As Boolean
        Get
            Return True
        End Get
    End Property

End Class
