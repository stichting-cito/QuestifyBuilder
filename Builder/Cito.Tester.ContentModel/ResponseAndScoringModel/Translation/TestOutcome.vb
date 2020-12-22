Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("testOutcome")> _
Public Class TestOutcome
    Inherits ValidatingEntityBase


    Private _inclusive As Boolean
    Private _outcome As String
    Private _score As Double



    <SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")> _
    Public Sub New()
        MyBase.New()

        Me.Score = 0
        Me.Outcome = String.Empty
    End Sub

    <SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")> _
    Public Sub New(inclusive As Boolean, outcomeValue As String)
        Me.New()

        Me.Inclusive = inclusive
        Me.Outcome = outcomeValue
    End Sub

    Public Sub New(score As Double, inclusive As Boolean, outcomeValue As String)
        Me.New()

        Me.Score = score
        Me.Inclusive = inclusive
        Me.Outcome = outcomeValue
    End Sub



    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return New TestOutcomeValidator
        End Get
    End Property



    <XmlAttribute("inclusive")> _
    Public Property Inclusive As Boolean
        Get
            Return _inclusive
        End Get
        Set
            _inclusive = value
        End Set
    End Property

    <XmlAttribute("outCome")> _
    Public Property Outcome As String
        Get
            Return _outcome
        End Get
        Set
            _outcome = value
            Me.Validate("Outcome")
        End Set
    End Property

    <XmlAttribute("score")> _
    Public Property Score As Double
        Get
            Return _score
        End Get
        Set
            _score = value
            Me.Validate("Score")
        End Set
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Outcome
        End Get
    End Property



    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Score")
        Me.Validate("Outcome")
    End Sub


End Class