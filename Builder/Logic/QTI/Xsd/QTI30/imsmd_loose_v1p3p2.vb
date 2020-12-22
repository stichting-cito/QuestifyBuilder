
Option Strict Off
Option Explicit On

Imports System.Xml.Serialization

Namespace QTI.Xsd.QTI30.loose

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
System.SerializableAttribute(),
System.Diagnostics.DebuggerStepThroughAttribute(),
System.ComponentModel.DesignerCategoryAttribute("code"),
System.Xml.Serialization.XmlTypeAttribute(TypeName:="LOM.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM"),
System.Xml.Serialization.XmlRootAttribute("lom", [Namespace]:="http://ltsc.ieee.org/xsd/LOM", IsNullable:=False)>
    Partial Public Class LOMType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("annotation", GetType(AnnotationType)),
 System.Xml.Serialization.XmlElementAttribute("classification", GetType(ClassificationType)),
 System.Xml.Serialization.XmlElementAttribute("educational", GetType(EducationalType)),
 System.Xml.Serialization.XmlElementAttribute("general", GetType(GeneralType)),
 System.Xml.Serialization.XmlElementAttribute("lifeCycle", GetType(LifeCycleType)),
 System.Xml.Serialization.XmlElementAttribute("metaMetadata", GetType(MetaMetadataType)),
 System.Xml.Serialization.XmlElementAttribute("relation", GetType(RelationType)),
 System.Xml.Serialization.XmlElementAttribute("rights", GetType(RightsType)),
 System.Xml.Serialization.XmlElementAttribute("technical", GetType(TechnicalType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Annotation.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class AnnotationType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("date", GetType(DateTimeType)),
 System.Xml.Serialization.XmlElementAttribute("description", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("entity", GetType(CharacterStringType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="DateTime.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class DateTimeType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("dateTime", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("description", GetType(LangStringType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="CharacterString.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class CharacterStringType

        Private anyAttrField() As System.Xml.XmlAttribute

        Private valueField As String

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Value() As String
            Get
                Return Me.valueField
            End Get
            Set
                Me.valueField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Taxon.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class TaxonType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("entry", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("id", GetType(CharacterStringType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="LangString.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class LangStringType

        Private stringField() As LanguageStringType

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlElementAttribute("string")>
        Public Property [string]() As LanguageStringType()
            Get
                Return Me.stringField
            End Get
            Set
                Me.stringField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="LanguageString.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class LanguageStringType

        Private languageField As String

        Private anyAttrField() As System.Xml.XmlAttribute

        Private valueField As String

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property language() As String
            Get
                Return Me.languageField
            End Get
            Set
                Me.languageField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Value() As String
            Get
                Return Me.valueField
            End Get
            Set
                Me.valueField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="TaxonPath.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class TaxonPathType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("taxon", GetType(TaxonType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Purpose.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class PurposeType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType23

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType23()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType23

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Classification.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class ClassificationType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType24

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("description", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("keyword", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("purpose", GetType(PurposeType)),
 System.Xml.Serialization.XmlElementAttribute("taxonPath", GetType(TaxonPathType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType24()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType24

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        description

        keyword

        purpose

        taxonPath
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Resource.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class ResourceType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("description", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("identifier", GetType(IdentifierType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Identifier.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class IdentifierType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("catalog", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("entry", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        catalog

        entry
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Kind.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class KindType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType22

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType22()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType22

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Relation.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class RelationType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("kind", GetType(KindType)),
 System.Xml.Serialization.XmlElementAttribute("resource", GetType(ResourceType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="CopyrightAndOtherRestrictions.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class CopyrightAndOtherRestrictionsType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType21

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType21()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType21

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Cost.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class CostType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType20

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType20()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType20

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Rights.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class RightsType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("copyrightAndOtherRestrictions", GetType(CopyrightAndOtherRestrictionsType)),
 System.Xml.Serialization.XmlElementAttribute("cost", GetType(CostType)),
 System.Xml.Serialization.XmlElementAttribute("description", GetType(LangStringType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Difficulty.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class DifficultyType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType18

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType18()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType18

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Context.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class ContextType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType17

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType17()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType17

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="IntendedEndUserRole.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class IntendedEndUserRoleType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType16

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType16()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType16

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="SemanticDensity.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class SemanticDensityType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType15

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType15()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType15

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="InteractivityLevel.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class InteractivityLevelType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType14

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType14()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType14

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="LearningResourceType.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class LearningResourceTypeType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType13

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType13()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType13

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="InteractivityType.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class InteractivityTypeType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType12

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType12()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType12

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Educational.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class EducationalType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType19

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("context", GetType(ContextType)),
 System.Xml.Serialization.XmlElementAttribute("description", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("difficulty", GetType(DifficultyType)),
 System.Xml.Serialization.XmlElementAttribute("intendedEndUserRole", GetType(IntendedEndUserRoleType)),
 System.Xml.Serialization.XmlElementAttribute("interactivityLevel", GetType(InteractivityLevelType)),
 System.Xml.Serialization.XmlElementAttribute("interactivityType", GetType(InteractivityTypeType)),
 System.Xml.Serialization.XmlElementAttribute("language", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("learningResourceType", GetType(LearningResourceTypeType)),
 System.Xml.Serialization.XmlElementAttribute("semanticDensity", GetType(SemanticDensityType)),
 System.Xml.Serialization.XmlElementAttribute("typicalAgeRange", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("typicalLearningTime", GetType(DurationType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType19()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Duration.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class DurationType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("description", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("duration", GetType(CharacterStringType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType19

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        context

        description

        difficulty

        intendedEndUserRole

        interactivityLevel

        interactivityType

        language

        learningResourceType

        semanticDensity

        typicalAgeRange

        typicalLearningTime
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Name.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class NameType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType9

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType9()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType9

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Type.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class TypeType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType8

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType8()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType8

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="OrComposite.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class OrCompositeType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType10

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("maximumVersion", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("minimumVersion", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("name", GetType(NameType)),
 System.Xml.Serialization.XmlElementAttribute("type", GetType(TypeType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType10()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType10

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        maximumVersion

        minimumVersion

        name

        type
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Requirement.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class RequirementType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("orComposite", GetType(OrCompositeType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Technical.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class TechnicalType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType11

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("duration", GetType(DurationType)),
 System.Xml.Serialization.XmlElementAttribute("format", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("installationRemarks", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("location", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("otherPlatformRequirements", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("requirement", GetType(RequirementType)),
 System.Xml.Serialization.XmlElementAttribute("size", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType11()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType11

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        duration

        format

        installationRemarks

        location

        otherPlatformRequirements

        requirement

        size
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="RoleMetaMetadata.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class RoleMetaMetadataType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType6

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType6()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType6

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="ContributeMetaMetadata.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class ContributeMetaMetadataType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("date", GetType(DateTimeType)),
 System.Xml.Serialization.XmlElementAttribute("entity", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("role", GetType(RoleMetaMetadataType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="MetaMetadata.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class MetaMetadataType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType7

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("contribute", GetType(ContributeMetaMetadataType)),
 System.Xml.Serialization.XmlElementAttribute("identifier", GetType(IdentifierType)),
 System.Xml.Serialization.XmlElementAttribute("language", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("metadataschema", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType7()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType7

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        contribute

        identifier

        language

        metadataschema
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="RoleLifeCycle.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class RoleLifeCycleType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType5

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType5()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType5

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="ContributeLifeCycle.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class ContributeLifeCycleType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("date", GetType(DateTimeType)),
 System.Xml.Serialization.XmlElementAttribute("entity", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("role", GetType(RoleLifeCycleType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Status.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class StatusType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType4

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType4()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType4

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="LifeCycle.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class LifeCycleType

        Private itemsField() As Object

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("contribute", GetType(ContributeLifeCycleType)),
 System.Xml.Serialization.XmlElementAttribute("status", GetType(StatusType)),
 System.Xml.Serialization.XmlElementAttribute("version", GetType(LangStringType))>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="AggregationLevel.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class AggregationLevelType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType2

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType2()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType2

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Structure.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class StructureType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType1

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("source", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("value", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType1()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType1

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        source

        value
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="General.Type", [Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
    Partial Public Class GeneralType

        Private itemsField() As Object

        Private itemsElementNameField() As ItemsChoiceType3

        Private anyAttrField() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(),
 System.Xml.Serialization.XmlElementAttribute("aggregationLevel", GetType(AggregationLevelType)),
 System.Xml.Serialization.XmlElementAttribute("coverage", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("description", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("identifier", GetType(IdentifierType)),
 System.Xml.Serialization.XmlElementAttribute("keyword", GetType(LangStringType)),
 System.Xml.Serialization.XmlElementAttribute("language", GetType(CharacterStringType)),
 System.Xml.Serialization.XmlElementAttribute("structure", GetType(StructureType)),
 System.Xml.Serialization.XmlElementAttribute("title", GetType(LangStringType)),
 System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me.itemsField
            End Get
            Set
                Me.itemsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName"),
 System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType3()
            Get
                Return Me.itemsElementNameField
            End Get
            Set
                Me.itemsElementNameField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType3

        <System.Xml.Serialization.XmlEnumAttribute("##any:")>
        Item

        aggregationLevel

        coverage

        description

        identifier

        keyword

        language

        [structure]

        title
    End Enum
End Namespace