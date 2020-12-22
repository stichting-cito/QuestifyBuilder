
Option Strict Off
Option Explicit On
Namespace QTI.Xsd.QTI22_Final


    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="UsageData.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_usagedata_v2p2"), _
    System.Xml.Serialization.XmlRootAttribute("usageData", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_usagedata_v2p2", IsNullable:=false)> _
    Partial Public Class UsageDataType

        Private itemsField() As Object

        Private glossaryField As String

        <System.Xml.Serialization.XmlElementAttribute("categorizedStatistic", GetType(CategorizedStatisticType), Order:=0), _
    System.Xml.Serialization.XmlElementAttribute("ordinaryStatistic", GetType(OrdinaryStatisticType), Order:=0)> _
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")> _
        Public Property glossary() As String
            Get
                Return Me.glossaryField
            End Get
            Set
                Me.glossaryField = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="CategorizedStatistic.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_usagedata_v2p2")> _
    Partial Public Class CategorizedStatisticType

        Private targetObjectField() As TargetObjectType

        Private mappingField As MappingType

        Private nameField As String

        Private glossaryField As String

        Private contextField As String

        Private caseCountField As String

        Private stdErrorField As Double

        Private stdErrorFieldSpecified As Boolean

        Private stdDeviationField As Double

        Private stdDeviationFieldSpecified As Boolean

        Private lastUpdatedField As Date

        Private lastUpdatedFieldSpecified As Boolean

        <System.Xml.Serialization.XmlElementAttribute("targetObject", Order:=0)> _
        Public Property targetObject() As TargetObjectType()
            Get
                Return Me.targetObjectField
            End Get
            Set
                Me.targetObjectField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)> _
        Public Property mapping() As MappingType
            Get
                Return Me.mappingField
            End Get
            Set
                Me.mappingField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")> _
        Public Property name() As String
            Get
                Return Me.nameField
            End Get
            Set
                Me.nameField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")> _
        Public Property glossary() As String
            Get
                Return Me.glossaryField
            End Get
            Set
                Me.glossaryField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")> _
        Public Property context() As String
            Get
                Return Me.contextField
            End Get
            Set
                Me.contextField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")> _
        Public Property caseCount() As String
            Get
                Return Me.caseCountField
            End Get
            Set
                Me.caseCountField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property stdError() As Double
            Get
                Return Me.stdErrorField
            End Get
            Set
                Me.stdErrorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property stdErrorSpecified() As Boolean
            Get
                Return Me.stdErrorFieldSpecified
            End Get
            Set
                Me.stdErrorFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property stdDeviation() As Double
            Get
                Return Me.stdDeviationField
            End Get
            Set
                Me.stdDeviationField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property stdDeviationSpecified() As Boolean
            Get
                Return Me.stdDeviationFieldSpecified
            End Get
            Set
                Me.stdDeviationFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="date")> _
        Public Property lastUpdated() As Date
            Get
                Return Me.lastUpdatedField
            End Get
            Set
                Me.lastUpdatedField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property lastUpdatedSpecified() As Boolean
            Get
                Return Me.lastUpdatedFieldSpecified
            End Get
            Set
                Me.lastUpdatedFieldSpecified = value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="TargetObject.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_usagedata_v2p2")> _
    Partial Public Class TargetObjectType
        Inherits EmptyPrimitiveTypeType

        Private identifierField As String

        Private partIdentifierField As String

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")> _
        Public Property partIdentifier() As String
            Get
                Return Me.partIdentifierField
            End Get
            Set
                Me.partIdentifierField = value
            End Set
        End Property
    End Class

    <System.Xml.Serialization.XmlIncludeAttribute(GetType(TargetObjectType)), _
        System.Xml.Serialization.XmlIncludeAttribute(GetType(MapEntryType)), _
        System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
        System.SerializableAttribute(), _
        System.Diagnostics.DebuggerStepThroughAttribute(), _
        System.ComponentModel.DesignerCategoryAttribute("code"), _
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="EmptyPrimitiveType.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_usagedata_v2p2")> _
    Partial Public Class EmptyPrimitiveTypeType
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"), _
    System.SerializableAttribute(), _
    System.Diagnostics.DebuggerStepThroughAttribute(), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Xml.Serialization.XmlTypeAttribute(TypeName:="OrdinaryStatistic.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_usagedata_v2p2")> _
    Partial Public Class OrdinaryStatisticType

        Private targetObjectField() As TargetObjectType

        Private valueField As ValueType

        Private nameField As String

        Private glossaryField As String

        Private contextField As String

        Private caseCountField As String

        Private stdErrorField As Double

        Private stdErrorFieldSpecified As Boolean

        Private stdDeviationField As Double

        Private stdDeviationFieldSpecified As Boolean

        Private lastUpdatedField As Date

        Private lastUpdatedFieldSpecified As Boolean

        <System.Xml.Serialization.XmlElementAttribute("targetObject", Order:=0)> _
        Public Property targetObject() As TargetObjectType()
            Get
                Return Me.targetObjectField
            End Get
            Set
                Me.targetObjectField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)> _
        Public Property value() As ValueType
            Get
                Return Me.valueField
            End Get
            Set
                Me.valueField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")> _
        Public Property name() As String
            Get
                Return Me.nameField
            End Get
            Set
                Me.nameField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")> _
        Public Property glossary() As String
            Get
                Return Me.glossaryField
            End Get
            Set
                Me.glossaryField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")> _
        Public Property context() As String
            Get
                Return Me.contextField
            End Get
            Set
                Me.contextField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")> _
        Public Property caseCount() As String
            Get
                Return Me.caseCountField
            End Get
            Set
                Me.caseCountField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property stdError() As Double
            Get
                Return Me.stdErrorField
            End Get
            Set
                Me.stdErrorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property stdErrorSpecified() As Boolean
            Get
                Return Me.stdErrorFieldSpecified
            End Get
            Set
                Me.stdErrorFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property stdDeviation() As Double
            Get
                Return Me.stdDeviationField
            End Get
            Set
                Me.stdDeviationField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property stdDeviationSpecified() As Boolean
            Get
                Return Me.stdDeviationFieldSpecified
            End Get
            Set
                Me.stdDeviationFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="date")> _
        Public Property lastUpdated() As Date
            Get
                Return Me.lastUpdatedField
            End Get
            Set
                Me.lastUpdatedField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property lastUpdatedSpecified() As Boolean
            Get
                Return Me.lastUpdatedFieldSpecified
            End Get
            Set
                Me.lastUpdatedFieldSpecified = value
            End Set
        End Property
    End Class
End NameSpace