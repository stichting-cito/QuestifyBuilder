
Imports System.Xml.Serialization

<Serializable> _
Public Class AspectReference
    Inherits ValidatingEntityBase


    Private _description As String
    Private _maxScore As Integer
    Private _sourceName As String
    Private _validator As AspectReferenceValidator



    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            If _validator Is Nothing Then
                _validator = New AspectReferenceValidator()
            End If
            Return _validator
        End Get
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

    <XmlAttribute("src")> _
    Public Property SourceName As String
        Get
            Return _sourceName
        End Get
        Set
            _sourceName = value
            Me.Validate("SourceName")
        End Set
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.SourceName
        End Get
    End Property



    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Description")
        Me.Validate("MaxScore")
        Me.Validate("SourceName")
    End Sub


End Class