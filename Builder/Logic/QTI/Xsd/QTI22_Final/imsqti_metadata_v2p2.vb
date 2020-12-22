
Option Strict Off
Option Explicit On
Namespace QTI.Xsd.QTI22_Final


    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="QTIMetadata.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v2p2"), _
    System.Xml.Serialization.XmlRootAttribute("qtiMetadata", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v2p2", IsNullable:=false)> _
    Partial Public Class QTIMetadataType

        Private itemTemplateField As Boolean

        Private itemTemplateFieldSpecified As Boolean

        Private timeDependentField As Boolean

        Private timeDependentFieldSpecified As Boolean

        Private compositeField As Boolean

        Private compositeFieldSpecified As Boolean

        Private interactionTypeField() As QTIMetadataTypeInteractionType

        Private feedbackTypeField As QTIMetadataTypeFeedbackType

        Private feedbackTypeFieldSpecified As Boolean

        Private solutionAvailableField As Boolean

        Private solutionAvailableFieldSpecified As Boolean

        Private scoringModeField() As QTIMetadataTypeScoringMode

        Private toolNameField As String

        Private toolVersionField As String

        Private toolVendorField As String

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)> _
        Public Property itemTemplate() As Boolean
            Get
                Return Me.itemTemplateField
            End Get
            Set
                Me.itemTemplateField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property itemTemplateSpecified() As Boolean
            Get
                Return Me.itemTemplateFieldSpecified
            End Get
            Set
                Me.itemTemplateFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)> _
        Public Property timeDependent() As Boolean
            Get
                Return Me.timeDependentField
            End Get
            Set
                Me.timeDependentField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property timeDependentSpecified() As Boolean
            Get
                Return Me.timeDependentFieldSpecified
            End Get
            Set
                Me.timeDependentFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)> _
        Public Property composite() As Boolean
            Get
                Return Me.compositeField
            End Get
            Set
                Me.compositeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property compositeSpecified() As Boolean
            Get
                Return Me.compositeFieldSpecified
            End Get
            Set
                Me.compositeFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("interactionType", Order:=3)> _
        Public Property interactionType() As QTIMetadataTypeInteractionType()
            Get
                Return Me.interactionTypeField
            End Get
            Set
                Me.interactionTypeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=4)> _
        Public Property feedbackType() As QTIMetadataTypeFeedbackType
            Get
                Return Me.feedbackTypeField
            End Get
            Set
                Me.feedbackTypeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property feedbackTypeSpecified() As Boolean
            Get
                Return Me.feedbackTypeFieldSpecified
            End Get
            Set
                Me.feedbackTypeFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=5)> _
        Public Property solutionAvailable() As Boolean
            Get
                Return Me.solutionAvailableField
            End Get
            Set
                Me.solutionAvailableField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property solutionAvailableSpecified() As Boolean
            Get
                Return Me.solutionAvailableFieldSpecified
            End Get
            Set
                Me.solutionAvailableFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("scoringMode", Order:=6)> _
        Public Property scoringMode() As QTIMetadataTypeScoringMode()
            Get
                Return Me.scoringModeField
            End Get
            Set
                Me.scoringModeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=7)> _
        Public Property toolName() As String
            Get
                Return Me.toolNameField
            End Get
            Set
                Me.toolNameField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=8)> _
        Public Property toolVersion() As String
            Get
                Return Me.toolVersionField
            End Get
            Set
                Me.toolVersionField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=9)> _
        Public Property toolVendor() As String
            Get
                Return Me.toolVendorField
            End Get
            Set
                Me.toolVendorField = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v2p2")> _
    Public Enum QTIMetadataTypeInteractionType

        associateInteraction

        choiceInteraction

        customInteraction

        drawingInteraction

        endAttemptInteraction

        extendedTextInteraction

        gapMatchInteraction

        graphicAssociateInteraction

        graphicGapMatchInteraction

        graphicOrderInteraction

        hotspotInteraction

        hottextInteraction

        inlineChoiceInteraction

        matchInteraction

        mediaInteraction

        orderInteraction

        positionObjectInteraction

        selectPointInteraction

        sliderInteraction

        textEntryInteraction

        uploadInteraction
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v2p2")> _
    Public Enum QTIMetadataTypeFeedbackType

        adaptive

        nonadaptive

        none
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v2p2")> _
    Public Enum QTIMetadataTypeScoringMode

        human

        externalmachine

        responseprocessing
    End Enum
End NameSpace