
Option Strict Off
Option Explicit On

Imports System.Xml.Serialization
Imports Questify.Builder.Logic.QTI.Xsd.QTI30.loose

Namespace QTI.Xsd.QTI30

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="CurriculumStandardsMetadataSet.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscsmd_v1p1"),
 System.Xml.Serialization.XmlRootAttribute("curriculumStandardsMetadataSet", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscsmd_v1p1", IsNullable:=False)>
    Partial Public Class CurriculumStandardsMetadataSetType

        Private curriculumStandardsMetadataField() As CurriculumStandardsMetadataType

        Private resourceLabelField As String

        Private resourcePartIdField As String

        <System.Xml.Serialization.XmlElementAttribute("curriculumStandardsMetadata")>
        Public Property curriculumStandardsMetadata() As CurriculumStandardsMetadataType()
            Get
                Return Me.curriculumStandardsMetadataField
            End Get
            Set
                Me.curriculumStandardsMetadataField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property resourceLabel() As String
            Get
                Return Me.resourceLabelField
            End Get
            Set
                Me.resourceLabelField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property resourcePartId() As String
            Get
                Return Me.resourcePartIdField
            End Get
            Set
                Me.resourcePartIdField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="CurriculumStandardsMetadata.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscsmd_v1p1")>
    Partial Public Class CurriculumStandardsMetadataType

        Private setOfGUIDsField() As SetOfGUIDsType

        Private providerIdField As String

        <System.Xml.Serialization.XmlElementAttribute("setOfGUIDs")>
        Public Property setOfGUIDs() As SetOfGUIDsType()
            Get
                Return Me.setOfGUIDsField
            End Get
            Set
                Me.setOfGUIDsField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property providerId() As String
            Get
                Return Me.providerIdField
            End Get
            Set
                Me.providerIdField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="SetOfGUIDs.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscsmd_v1p1")>
    Partial Public Class SetOfGUIDsType

        Private labelledGUIDField() As LabelledGUIDType

        Private regionField As String

        Private versionField As String

        <System.Xml.Serialization.XmlElementAttribute("labelledGUID")>
        Public Property labelledGUID() As LabelledGUIDType()
            Get
                Return Me.labelledGUIDField
            End Get
            Set
                Me.labelledGUIDField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property region() As String
            Get
                Return Me.regionField
            End Get
            Set
                Me.regionField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property version() As String
            Get
                Return Me.versionField
            End Get
            Set
                Me.versionField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="LabelledGUID.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscsmd_v1p1")>
    Partial Public Class LabelledGUIDType

        Private labelField As String

        Private caseItemURIField As String

        Private gUIDField As String

        <System.Xml.Serialization.XmlElementAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me.labelField
            End Get
            Set
                Me.labelField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(DataType:="anyURI")>
        Public Property caseItemURI() As String
            Get
                Return Me.caseItemURIField
            End Get
            Set
                Me.caseItemURIField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(DataType:="normalizedString")>
        Public Property GUID() As String
            Get
                Return Me.gUIDField
            End Get
            Set
                Me.gUIDField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Manifest.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1"),
 System.Xml.Serialization.XmlRootAttribute("manifest", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1", IsNullable:=False)>
    Partial Public Class ManifestType

        Private metadataField As ManifestMetadataType

        Private organizationsField As OrganizationsType

        Private resourcesField As ResourcesType

        Private identifierField As String

        Private baseField As String

        Public Property metadata() As ManifestMetadataType
            Get
                Return Me.metadataField
            End Get
            Set
                Me.metadataField = Value
            End Set
        End Property

        Public Property organizations() As OrganizationsType
            Get
                Return Me.organizationsField
            End Get
            Set
                Me.organizationsField = Value
            End Set
        End Property

        Public Property resources() As ResourcesType
            Get
                Return Me.resourcesField
            End Get
            Set
                Me.resourcesField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me.baseField
            End Get
            Set
                Me.baseField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="ManifestMetadata.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Partial Public Class ManifestMetadataType

        Private schemaField As ManifestMetadataTypeSchema

        Private schemaversionField As ManifestMetadataTypeSchemaversion

        Private curriculumStandardsMetadataSetField As CurriculumStandardsMetadataSetType

        Private lomField As LOMType

        Public Property schema() As ManifestMetadataTypeSchema
            Get
                Return Me.schemaField
            End Get
            Set
                Me.schemaField = Value
            End Set
        End Property

        Public Property schemaversion() As ManifestMetadataTypeSchemaversion
            Get
                Return Me.schemaversionField
            End Get
            Set
                Me.schemaversionField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscsmd_v1p1")>
        Public Property curriculumStandardsMetadataSet() As CurriculumStandardsMetadataSetType
            Get
                Return Me.curriculumStandardsMetadataSetField
            End Get
            Set
                Me.curriculumStandardsMetadataSetField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
        Public Property lom() As LOMType
            Get
                Return Me.lomField
            End Get
            Set
                Me.lomField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Public Enum ManifestMetadataTypeSchema

        <System.Xml.Serialization.XmlEnumAttribute("QTI Package")>
        QTIPackage

        <System.Xml.Serialization.XmlEnumAttribute("QTI Test Bank")>
        QTITestBank

        <System.Xml.Serialization.XmlEnumAttribute("QTI Item Bank")>
        QTIItemBank

        <System.Xml.Serialization.XmlEnumAttribute("QTI Object Bank")>
        QTIObjectBank

        <System.Xml.Serialization.XmlEnumAttribute("QTI Test")>
        QTITest

        <System.Xml.Serialization.XmlEnumAttribute("QTI Section")>
        QTISection

        <System.Xml.Serialization.XmlEnumAttribute("QTI Item")>
        QTIItem
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Public Enum ManifestMetadataTypeSchemaversion

        <System.Xml.Serialization.XmlEnumAttribute("3.0.0")>
        Item300
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Organizations.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Partial Public Class OrganizationsType
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Resources.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Partial Public Class ResourcesType

        Private resourceField() As ResourceType

        Private baseField As String

        <System.Xml.Serialization.XmlElementAttribute("resource")>
        Public Property resource() As ResourceType()
            Get
                Return Me.resourceField
            End Get
            Set
                Me.resourceField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me.baseField
            End Get
            Set
                Me.baseField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Resource.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Partial Public Class ResourceType

        Private metadataField As ResourceMetadataType

        Private fileField() As FileType

        Private dependencyField() As DependencyType

        Private variantField() As VariantType

        Private identifierField As String

        Private typeField As ResourceTypeType

        Private baseField As String

        Private hrefField As String

        Public Property metadata() As ResourceMetadataType
            Get
                Return Me.metadataField
            End Get
            Set
                Me.metadataField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("file")>
        Public Property file() As FileType()
            Get
                Return Me.fileField
            End Get
            Set
                Me.fileField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("dependency")>
        Public Property dependency() As DependencyType()
            Get
                Return Me.dependencyField
            End Get
            Set
                Me.dependencyField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("variant", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_extensionv1p2")>
        Public Property [variant]() As VariantType()
            Get
                Return Me.variantField
            End Get
            Set
                Me.variantField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property identifier() As String
            Get
                Return Me.identifierField
            End Get
            Set
                Me.identifierField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property type() As ResourceTypeType
            Get
                Return Me.typeField
            End Get
            Set
                Me.typeField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me.baseField
            End Get
            Set
                Me.baseField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me.hrefField
            End Get
            Set
                Me.hrefField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="ResourceMetadata.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Partial Public Class ResourceMetadataType

        Private qtiMetadataField As QTIMetadataType

        Private curriculumStandardsMetadataSetField As CurriculumStandardsMetadataSetType

        Private lomField As LOMType

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_metadata_v3p0")>
        Public Property qtiMetadata() As QTIMetadataType
            Get
                Return Me.qtiMetadataField
            End Get
            Set
                Me.qtiMetadataField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscsmd_v1p1")>
        Public Property curriculumStandardsMetadataSet() As CurriculumStandardsMetadataSetType
            Get
                Return Me.curriculumStandardsMetadataSetField
            End Get
            Set
                Me.curriculumStandardsMetadataSetField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://ltsc.ieee.org/xsd/LOM")>
        Public Property lom() As LOMType
            Get
                Return Me.lomField
            End Get
            Set
                Me.lomField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="File.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Partial Public Class FileType

        Private metadataField As FileMetadataType

        Private hrefField As String

        Public Property metadata() As FileMetadataType
            Get
                Return Me.metadataField
            End Get
            Set
                Me.metadataField = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me.hrefField
            End Get
            Set
                Me.hrefField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="FileMetadata.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Partial Public Class FileMetadataType

        Private anyField() As System.Xml.XmlElement

        <System.Xml.Serialization.XmlAnyElementAttribute()>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me.anyField
            End Get
            Set
                Me.anyField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="Dependency.Type", [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Partial Public Class DependencyType

        Private identifierrefField As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="IDREF")>
        Public Property identifierref() As String
            Get
                Return Me.identifierrefField
            End Get
            Set
                Me.identifierrefField = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),
 System.SerializableAttribute(),
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/qti/qtiv3p0/imscp_v1p1")>
    Public Enum ResourceTypeType

        imsqti_test_xmlv3p0

        imsqti_section_xmlv3p0

        imsqti_item_xmlv3p0

        imsqti_resprocessing_xmlv3p0

        imsqti_outcomes_xmlv3p0

        imsqti_stimulus_xmlv3p0

        imsqti_fragment_xmlv3p0

        imsqti_rptemplate_xmlv3p0

        <System.Xml.Serialization.XmlEnumAttribute("associatedcontent/learning-application-resource")>
        associatedcontentlearningapplicationresource

        webcontent

        imslti_xmlv1p1

        imsltia_xmlv1p0

        controlfile

        <System.Xml.Serialization.XmlEnumAttribute("resourcemetadata/xml")>
        resourcemetadataxml

        <System.Xml.Serialization.XmlEnumAttribute("resourceextmetadata/xml")>
        resourceextmetadataxml

        <System.Xml.Serialization.XmlEnumAttribute("qtiusagedata/xml")>
        qtiusagedataxml

        pls

        css2

        css3

        extension
    End Enum
End Namespace