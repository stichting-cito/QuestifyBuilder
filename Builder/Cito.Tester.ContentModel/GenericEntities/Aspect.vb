
Imports System.Xml.Serialization

<Serializable, XmlRoot("aspect")> _
Public Class Aspect
    Inherits ValidatingEntityBase

    Private _identifier As String
    Private _title As String
    Private _description As String
    Private _maxScore As Integer
    Private _stylesheet As String
    Private _validator As AspectValidator

    <XmlAttribute("identifier")> _
    Public Property Identifier As String
        Get
            Return _identifier
        End Get
        Set
            _identifier = value
            Me.Validate("Identifier")
        End Set
    End Property

    <XmlAttribute("title")> _
    Public Property Title As String
        Get
            Return _title
        End Get
        Set
            _title = value
            Me.Validate("Title")
        End Set
    End Property

    <XmlText> _
    Public Property Description As String
        Get
            Return _description
        End Get
        Set
            _description = value
            Me.Validate("Description")
        End Set
    End Property

    <XmlAttribute("maxScore")> _
    Public Property MaxScore As Integer
        Get
            Return _maxScore
        End Get
        Set
            _maxScore = value
            Me.Validate("MaxScore")
        End Set
    End Property

    <XmlAttribute("stylesheet")> _
    Public Property Stylesheet As String
        Get
            Return _stylesheet
        End Get
        Set
            _stylesheet = value
            Me.Validate("Stylesheet")
        End Set
    End Property

    Public Sub New()
        Me.MaxScore = 0
        Me.Description = String.Empty
        Me.Stylesheet = String.Empty
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


    <XmlIgnore> _
    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Identifier
        End Get
    End Property

End Class
