
Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization

<Serializable, XmlRoot("assessmentItem")>
Public Class AssessmentItem
    Inherits ValidatingEntityBase


    Private _identifier As String
    Private _title As String
    Private _layoutTemplateSourceName As String
    Private _solution As Solution
    Private _parameters As ParameterSetCollection
    Private _itemRefContext As ItemReferenceViewBase
    Private _itemId As String




    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return New AssessmentItemValidator
        End Get
    End Property





    <XmlAttribute("identifier")>
    Public Property Identifier As String
        Get
            Return Me._identifier
        End Get
        Set
            Me._identifier = value
            Me.Validate("Identifier")
        End Set
    End Property


    <XmlAttribute("itemid")>
    Public Property ItemId As String
        Get
            Return Me._itemId
        End Get
        Set
            Me._itemId = value
        End Set
    End Property


    <XmlAttribute("title")>
    Public Property Title As String
        Get
            Return Me._title
        End Get
        Set
            Me._title = value
            Me.Validate("Title")
        End Set
    End Property


    <XmlAttribute("layoutTemplateSrc")>
    Public Property LayoutTemplateSourceName As String
        Get
            Return _layoutTemplateSourceName
        End Get
        Set
            Me._layoutTemplateSourceName = value
            Me.Validate("LayoutTemplateSourceName")
        End Set
    End Property


    <XmlElement("solution")>
    Public Property Solution As Solution
        Get
            Return _solution
        End Get
        Set
            _solution = value
        End Set
    End Property


    <XmlArray("parameters"),
XmlArrayItem("parameterSet", GetType(ParameterCollection))>
    Public ReadOnly Property Parameters As ParameterSetCollection
        Get
            Return _parameters
        End Get
    End Property



    <XmlIgnore>
    Public Property ItemRefContextV2 As ItemReferenceViewBase
        Get
            Return _itemRefContext
        End Get
        Set
            _itemRefContext = value
        End Set
    End Property


    <XmlIgnore>
    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Identifier
        End Get
    End Property




    Public Sub ScoreItem(itemRef As ItemReferenceViewBase, response As Response)
        response.RawScore = 0
        response.TranslatedScore = 0

        If response IsNot Nothing AndAlso Me.Solution IsNot Nothing AndAlso Me.Solution.Findings.Count > 0 Then
            response.RawScore = Me.Solution.ScoreSolution(response)

            If Solution.ItemScoreTranslationTable IsNot Nothing AndAlso Solution.ItemScoreTranslationTable.Count > 0 Then
                response.TranslatedScore = Solution.ItemScoreTranslationTable.TranslateScore(response.RawScore)
            Else
                If itemRef IsNot Nothing AndAlso itemRef.ItemScoreTranslationTable IsNot Nothing Then
                    response.TranslatedScore = itemRef.ItemScoreTranslationTable.TranslateScore(response.RawScore)
                Else
                    response.TranslatedScore = response.RawScore
                End If
            End If

            If itemRef IsNot Nothing Then
                With itemRef
                    response.Active = itemRef.Active
                    If .Weight > 0.0 Then
                        response.TranslatedScore = response.TranslatedScore * .Weight
                    End If
                End With
            End If
        End If
    End Sub


    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Identifier")
        Me.Validate("Title")
        Me.Validate("LayoutTemplateSourceName")
    End Sub




    <SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")>
    Public Sub New()
        Me._parameters = New ParameterSetCollection()
        Me._solution = New Solution

        Me.Identifier = String.Empty
        Me.LayoutTemplateSourceName = String.Empty
        Me.Title = String.Empty
    End Sub


End Class