
Option Strict Off
Option Explicit On
Namespace QTI.Xsd.QTI22_Final


    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssessmentResult.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2"), _
    System.Xml.Serialization.XmlRootAttribute("assessmentResult", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2", IsNullable:=false)> _
    Partial Public Class AssessmentResultType

        Private contextField As ContextType

        Private testResultField As TestResultType

        Private itemResultField() As ItemResultType

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)> _
        Public Property context() As ContextType
            Get
                Return Me.contextField
            End Get
            Set
                Me.contextField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)> _
        Public Property testResult() As TestResultType
            Get
                Return Me.testResultField
            End Get
            Set
                Me.testResultField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("itemResult", Order:=2)> _
        Public Property itemResult() As ItemResultType()
            Get
                Return Me.itemResultField
            End Get
            Set
                Me.itemResultField = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="Context.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Partial Public Class ContextType

        Private sessionIdentifierField() As SessionIdentifierType

        Private sourcedIdField As String

        <System.Xml.Serialization.XmlElementAttribute("sessionIdentifier", Order:=0)> _
        Public Property sessionIdentifier() As SessionIdentifierType()
            Get
                Return Me.sessionIdentifierField
            End Get
            Set
                Me.sessionIdentifierField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")> _
        Public Property sourcedId() As String
            Get
                Return Me.sourcedIdField
            End Get
            Set
                Me.sourcedIdField = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="SessionIdentifier.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Partial Public Class SessionIdentifierType
        Inherits EmptyPrimitiveTypeType

        Private sourceIDField As String

        Private identifierField As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")> _
        Public Property sourceID() As String
            Get
                Return Me.sourceIDField
            End Get
            Set
                Me.sourceIDField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")> _
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="TestResult.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Partial Public Class TestResultType

        Private itemsField() As Object

        Private identifierField As String

        Private datestampField As Date

        <System.Xml.Serialization.XmlElementAttribute("outcomeVariable", GetType(OutcomeVariableType), Order:=0), _
    System.Xml.Serialization.XmlElementAttribute("responseVariable", GetType(ResponseVariableType), Order:=0), _
    System.Xml.Serialization.XmlElementAttribute("templateVariable", GetType(TemplateVariableType), Order:=0)> _
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")> _
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property datestamp() As Date
            Get
                Return Me.datestampField
            End Get
            Set
                Me.datestampField = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="OutcomeVariable.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Partial Public Class OutcomeVariableType

        Private valueField() As ValueType

        Private identifierField As String

        Private cardinalityField As OutcomeVariableTypeCardinality

        Private baseTypeField As OutcomeVariableTypeBaseType

        Private baseTypeFieldSpecified As Boolean

        Private viewField() As ViewType

        Private interpretationField As String

        Private longInterpretationField As String

        Private normalMaximumField As Double

        Private normalMaximumFieldSpecified As Boolean

        Private normalMinimumField As Double

        Private normalMinimumFieldSpecified As Boolean

        Private masteryValueField As Double

        Private masteryValueFieldSpecified As Boolean

        <System.Xml.Serialization.XmlElementAttribute("value", Order:=0)> _
        Public Property value() As ValueType()
            Get
                Return Me.valueField
            End Get
            Set
                Me.valueField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")> _
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property cardinality() As OutcomeVariableTypeCardinality
            Get
                Return Me.cardinalityField
            End Get
            Set
                Me.cardinalityField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property baseType() As OutcomeVariableTypeBaseType
            Get
                Return Me.baseTypeField
            End Get
            Set
                Me.baseTypeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property baseTypeSpecified() As Boolean
            Get
                Return Me.baseTypeFieldSpecified
            End Get
            Set
                Me.baseTypeFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property view() As ViewType()
            Get
                Return Me.viewField
            End Get
            Set
                Me.viewField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property interpretation() As String
            Get
                Return Me.interpretationField
            End Get
            Set
                Me.interpretationField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")> _
        Public Property longInterpretation() As String
            Get
                Return Me.longInterpretationField
            End Get
            Set
                Me.longInterpretationField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property normalMaximum() As Double
            Get
                Return Me.normalMaximumField
            End Get
            Set
                Me.normalMaximumField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property normalMaximumSpecified() As Boolean
            Get
                Return Me.normalMaximumFieldSpecified
            End Get
            Set
                Me.normalMaximumFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property normalMinimum() As Double
            Get
                Return Me.normalMinimumField
            End Get
            Set
                Me.normalMinimumField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property normalMinimumSpecified() As Boolean
            Get
                Return Me.normalMinimumFieldSpecified
            End Get
            Set
                Me.normalMinimumFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property masteryValue() As Double
            Get
                Return Me.masteryValueField
            End Get
            Set
                Me.masteryValueField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property masteryValueSpecified() As Boolean
            Get
                Return Me.masteryValueFieldSpecified
            End Get
            Set
                Me.masteryValueFieldSpecified = value
            End Set
        End Property
    End Class




    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Public Enum OutcomeVariableTypeCardinality

        multiple

        ordered

        record

        [single]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Public Enum OutcomeVariableTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="ResponseVariable.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Partial Public Class ResponseVariableType

        Private correctResponseField As CorrectResponseType

        Private candidateResponseField() As ValueType

        Private identifierField As String

        Private cardinalityField As ResponseVariableTypeCardinality

        Private baseTypeField As ResponseVariableTypeBaseType

        Private baseTypeFieldSpecified As Boolean

        Private choiceSequenceField() As String

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)> _
        Public Property correctResponse() As CorrectResponseType
            Get
                Return Me.correctResponseField
            End Get
            Set
                Me.correctResponseField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlArrayAttribute(Order:=1), _
    System.Xml.Serialization.XmlArrayItemAttribute("value", IsNullable:=false)> _
        Public Property candidateResponse() As ValueType()
            Get
                Return Me.candidateResponseField
            End Get
            Set
                Me.candidateResponseField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")> _
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property cardinality() As ResponseVariableTypeCardinality
            Get
                Return Me.cardinalityField
            End Get
            Set
                Me.cardinalityField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property baseType() As ResponseVariableTypeBaseType
            Get
                Return Me.baseTypeField
            End Get
            Set
                Me.baseTypeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property baseTypeSpecified() As Boolean
            Get
                Return Me.baseTypeFieldSpecified
            End Get
            Set
                Me.baseTypeFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")> _
        Public Property choiceSequence() As String()
            Get
                Return Me.choiceSequenceField
            End Get
            Set
                Me.choiceSequenceField = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Public Enum ResponseVariableTypeCardinality

        multiple

        ordered

        record

        [single]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Public Enum ResponseVariableTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateVariable.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Partial Public Class TemplateVariableType

        Private valueField() As ValueType

        Private identifierField As String

        Private cardinalityField As TemplateVariableTypeCardinality

        Private baseTypeField As TemplateVariableTypeBaseType

        Private baseTypeFieldSpecified As Boolean

        <System.Xml.Serialization.XmlElementAttribute("value", Order:=0)> _
        Public Property value() As ValueType()
            Get
                Return Me.valueField
            End Get
            Set
                Me.valueField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")> _
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property cardinality() As TemplateVariableTypeCardinality
            Get
                Return Me.cardinalityField
            End Get
            Set
                Me.cardinalityField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property baseType() As TemplateVariableTypeBaseType
            Get
                Return Me.baseTypeField
            End Get
            Set
                Me.baseTypeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property baseTypeSpecified() As Boolean
            Get
                Return Me.baseTypeFieldSpecified
            End Get
            Set
                Me.baseTypeFieldSpecified = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Public Enum TemplateVariableTypeCardinality

        multiple

        ordered

        record

        [single]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Public Enum TemplateVariableTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="ItemResult.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Partial Public Class ItemResultType

        Private itemsField() As Object

        Private candidateCommentField As String

        Private identifierField As String

        Private sequenceIndexField As String

        Private datestampField As Date

        Private sessionStatusField As ItemResultTypeSessionStatus

        <System.Xml.Serialization.XmlElementAttribute("outcomeVariable", GetType(OutcomeVariableType), Order:=0), _
    System.Xml.Serialization.XmlElementAttribute("responseVariable", GetType(ResponseVariableType), Order:=0), _
    System.Xml.Serialization.XmlElementAttribute("templateVariable", GetType(TemplateVariableType), Order:=0)> _
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)> _
        Public Property candidateComment() As String
            Get
                Return Me.candidateCommentField
            End Get
            Set
                Me.candidateCommentField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")> _
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")> _
        Public Property sequenceIndex() As String
            Get
                Return Me.sequenceIndexField
            End Get
            Set
                Me.sequenceIndexField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property datestamp() As Date
            Get
                Return Me.datestampField
            End Get
            Set
                Me.datestampField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property sessionStatus() As ItemResultTypeSessionStatus
            Get
                Return Me.sessionStatusField
            End Get
            Set
                Me.sessionStatusField = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_result_v2p2")> _
    Public Enum ItemResultTypeSessionStatus

        final

        initial

        pendingResponseProcessing

        pendingSubmission
    End Enum
End NameSpace