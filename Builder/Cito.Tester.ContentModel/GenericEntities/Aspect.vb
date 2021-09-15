
Imports System.Linq
Imports System.Xml.Serialization

<Serializable, XmlRoot("aspect")>
Public Class Aspect
    Inherits ValidatingEntityBase

    Private _identifier As String
    Private _title As String
    Private _description As String
    Private _maxScore As Integer
    Private _stylesheet As String
    Private _validator As AspectValidator
    Private _aspectScoreTranslationTable As AspectScoreTranslationTable


    <XmlAttribute("identifier")>
    Public Property Identifier As String
        Get
            Return _identifier
        End Get
        Set
            _identifier = Value
            Me.Validate("Identifier")
        End Set
    End Property

    <XmlAttribute("title")>
    Public Property Title As String
        Get
            Return _title
        End Get
        Set
            _title = Value
            Me.Validate("Title")
        End Set
    End Property

    <XmlText>
    Public Property Description As String
        Get
            Return _description
        End Get
        Set
            _description = Value
            Me.Validate("Description")
        End Set
    End Property

    <XmlAttribute("maxScore")>
    Public Property MaxScore As Integer
        Get
            Return _maxScore
        End Get
        Set
            _maxScore = Value
            Me.Validate("MaxScore")
        End Set
    End Property

    <XmlAttribute("stylesheet")>
    Public Property Stylesheet As String
        Get
            Return _stylesheet
        End Get
        Set
            _stylesheet = Value
            Me.Validate("Stylesheet")
        End Set
    End Property

    <XmlIgnore>
    Public ReadOnly Property AspectScoreTranslationTableSpecified() As Boolean
        Get
            Return _aspectScoreTranslationTable IsNot Nothing AndAlso
                _aspectScoreTranslationTable.Any()
        End Get
    End Property

    Public ReadOnly Property AspectScoreTranslationTable() As AspectScoreTranslationTable
        Get
            Return _aspectScoreTranslationTable
        End Get
    End Property

    Public Sub New()
        Me.MaxScore = 0
        Me.Description = String.Empty
        Me.Stylesheet = String.Empty
        _aspectScoreTranslationTable = New AspectScoreTranslationTable()
    End Sub

    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            If _validator Is Nothing Then
                _validator = New AspectValidator()
            End If
            Return _validator
        End Get
    End Property

    Public Overrides Sub ValidateAllProperties()
        Me.Validate("MaxScore")
        Me.Validate("Description")
        Me.Validate("Stylesheet")
    End Sub

    <XmlIgnore>
    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Identifier
        End Get
    End Property

End Class
