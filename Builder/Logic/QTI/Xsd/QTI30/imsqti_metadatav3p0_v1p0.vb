
Option Strict Off
Option Explicit On

Imports System.Xml.Serialization

Namespace QTI.Xsd.QTI30

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="QTIMetadata.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v3p0"),
 System.Xml.Serialization.XmlRootAttribute("qtiMetadata", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v3p0", IsNullable:=False)>
    Partial Public Class QTIMetadataType

        Private itemTemplateField As Boolean

        Private itemTemplateFieldSpecified As Boolean

        Private timeDependentField As Boolean

        Private timeDependentFieldSpecified As Boolean

        Private compositeField As Boolean

        Private compositeFieldSpecified As Boolean

        Private interactionTypeField() As QTIMetadataTypeInteractionType

        Private portableCustomInteractionContextField As PCIContextType

        Private feedbackTypeField As QTIMetadataTypeFeedbackType

        Private feedbackTypeFieldSpecified As Boolean

        Private solutionAvailableField As Boolean

        Private solutionAvailableFieldSpecified As Boolean

        Private scoringModeField() As QTIMetadataTypeScoringMode

        Private toolNameField As String

        Private toolVersionField As String

        Private toolVendorField As String

        Public Property itemTemplate() As Boolean
            Get
                Return Me.itemTemplateField
            End Get
            Set
                Me.itemTemplateField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property itemTemplateSpecified() As Boolean
            Get
                Return Me.itemTemplateFieldSpecified
            End Get
            Set
                Me.itemTemplateFieldSpecified = Value
            End Set
        End Property

        Public Property timeDependent() As Boolean
            Get
                Return Me.timeDependentField
            End Get
            Set
                Me.timeDependentField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property timeDependentSpecified() As Boolean
            Get
                Return Me.timeDependentFieldSpecified
            End Get
            Set
                Me.timeDependentFieldSpecified = Value
            End Set
        End Property

        Public Property composite() As Boolean
            Get
                Return Me.compositeField
            End Get
            Set
                Me.compositeField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property compositeSpecified() As Boolean
            Get
                Return Me.compositeFieldSpecified
            End Get
            Set
                Me.compositeFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("interactionType")>
        Public Property interactionType() As QTIMetadataTypeInteractionType()
            Get
                Return Me.interactionTypeField
            End Get
            Set
                Me.interactionTypeField = Value
            End Set
        End Property

        Public Property portableCustomInteractionContext() As PCIContextType
            Get
                Return Me.portableCustomInteractionContextField
            End Get
            Set
                Me.portableCustomInteractionContextField = Value
            End Set
        End Property

        Public Property feedbackType() As QTIMetadataTypeFeedbackType
            Get
                Return Me.feedbackTypeField
            End Get
            Set
                Me.feedbackTypeField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property feedbackTypeSpecified() As Boolean
            Get
                Return Me.feedbackTypeFieldSpecified
            End Get
            Set
                Me.feedbackTypeFieldSpecified = Value
            End Set
        End Property

        Public Property solutionAvailable() As Boolean
            Get
                Return Me.solutionAvailableField
            End Get
            Set
                Me.solutionAvailableField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property solutionAvailableSpecified() As Boolean
            Get
                Return Me.solutionAvailableFieldSpecified
            End Get
            Set
                Me.solutionAvailableFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("scoringMode")>
        Public Property scoringMode() As QTIMetadataTypeScoringMode()
            Get
                Return Me.scoringModeField
            End Get
            Set
                Me.scoringModeField = Value
            End Set
        End Property

        Public Property toolName() As String
            Get
                Return Me.toolNameField
            End Get
            Set
                Me.toolNameField = Value
            End Set
        End Property

        Public Property toolVersion() As String
            Get
                Return Me.toolVersionField
            End Get
            Set
                Me.toolVersionField = Value
            End Set
        End Property

        Public Property toolVendor() As String
            Get
                Return Me.toolVendorField
            End Get
            Set
                Me.toolVendorField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v3p0")>
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

        portableCustomInteraction

        positionObjectInteraction

        selectPointInteraction

        sliderInteraction

        textEntryInteraction

        uploadInteraction
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="PCIContext.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v3p0")>
    Partial Public Class PCIContextType

        Private customTypeIdentifierField As String

        Private interactionKindField As String

        <System.Xml.Serialization.XmlElementAttribute(DataType:="normalizedString")>
        Public Property customTypeIdentifier() As String
            Get
                Return Me.customTypeIdentifierField
            End Get
            Set
                Me.customTypeIdentifierField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(DataType:="normalizedString")>
        Public Property interactionKind() As String
            Get
                Return Me.interactionKindField
            End Get
            Set
                Me.interactionKindField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v3p0")>
    Public Enum QTIMetadataTypeFeedbackType

        adaptive

        nonadaptive

        none
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v3p0")>
    Public Enum QTIMetadataTypeScoringMode

        human

        externalmachine

        responseprocessing
    End Enum
End Namespace