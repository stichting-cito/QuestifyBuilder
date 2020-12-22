Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("CutOffScoreCondition")> _
Public Class CutOffScoreCondition
    Inherits ValidatingEntityBase

    Private _levelId As Guid
    <XmlAttribute("levelId")> _
    Public Property LevelId As Guid
        Get
            Return _levelId
        End Get
        Set
            _levelId = value
        End Set
    End Property

    Private _levelName As String
    <XmlAttribute("levelName")> _
    Public Property LevelName As String
        Get
            Return _levelName
        End Get
        Set
            _levelName = value
        End Set
    End Property

    Private _levelVisualName As String
    <XmlIgnore> _
    Public Property LevelVisualName As String
        Get
            Return _levelVisualName
        End Get
        Set
            _levelVisualName = value
        End Set
    End Property

    Private _score As Double
    <XmlAttribute("score")> _
    Public Property Score As Double
        Get
            Return _score
        End Get
        Set
            _score = value
        End Set
    End Property

    Private _unit As CutOffScoreConditionUnit
    <XmlAttribute("unit")> _
    Public Property Unit As CutOffScoreConditionUnit
        Get
            Return _unit
        End Get
        Set
            _unit = value
        End Set
    End Property

    Public Overrides Sub ValidateAllProperties()
        Me.Validate("LevelId")
        Me.Validate("LevelName")
        Me.Validate("Score")
        Me.Validate("Unit")
    End Sub

    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return _levelName
        End Get
    End Property

End Class
