Namespace QTI.Xsd.QTI22_Final
#Disable Warning



    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Manifest.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1"),
        System.Xml.Serialization.XmlRootAttribute("manifest", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1", IsNullable:=False)>
    Partial Public Class ManifestType

        Private _metadata As ManifestMetadataType

        Private _organizations As OrganizationsType

        Private _resources As ResourcesType

        Private _manifest() As ManifestType

        Private _any() As System.Xml.XmlElement

        Private _identifier As String

        Private _version As String

        Private _base As String

        Private _anyAttr() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property metadata() As ManifestMetadataType
            Get
                Return Me._metadata
            End Get
            Set
                Me._metadata = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>
        Public Property organizations() As OrganizationsType
            Get
                Return Me._organizations
            End Get
            Set
                Me._organizations = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property resources() As ResourcesType
            Get
                Return Me._resources
            End Get
            Set
                Me._resources = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("manifest", Order:=3)>
        Public Property manifest() As ManifestType()
            Get
                Return Me._manifest
            End Get
            Set
                Me._manifest = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=4)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property version() As String
            Get
                Return Me._version
            End Get
            Set
                Me._version = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me._base
            End Get
            Set
                Me._base = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ManifestMetadata.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class ManifestMetadataType

        Private _schema As String

        Private _schemaversion As String

        Private _any() As System.Xml.XmlElement

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property schema() As String
            Get
                Return Me._schema
            End Get
            Set
                Me._schema = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>
        Public Property schemaversion() As String
            Get
                Return Me._schemaversion
            End Get
            Set
                Me._schemaversion = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=2)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Dependency.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class DependencyType

        Private _any() As System.Xml.XmlElement

        Private _identifierref As String

        Private _anyAttr() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=0)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property identifierref() As String
            Get
                Return Me._identifierref
            End Get
            Set
                Me._identifierref = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="File.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class FileType

        Private _metadata As MetadataType

        Private _any() As System.Xml.XmlElement

        Private _href As String

        Private _anyAttr() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property metadata() As MetadataType
            Get
                Return Me._metadata
            End Get
            Set
                Me._metadata = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=1)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me._href
            End Get
            Set
                Me._href = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Metadata.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class MetadataType

        Private _schema As String

        Private _schemaversion As String

        Private _any() As System.Xml.XmlElement

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property schema() As String
            Get
                Return Me._schema
            End Get
            Set
                Me._schema = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>
        Public Property schemaversion() As String
            Get
                Return Me._schemaversion
            End Get
            Set
                Me._schemaversion = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=2)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Resource.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class ResourceType

        Private _metadata As MetadataType

        Private _file() As FileType

        Private _dependency() As DependencyType

        Private _any() As System.Xml.XmlElement

        Private _identifier As String

        Private _type As String

        Private _base As String

        Private _href As String

        Private _anyAttr() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property metadata() As MetadataType
            Get
                Return Me._metadata
            End Get
            Set
                Me._metadata = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("file", Order:=1)>
        Public Property file() As FileType()
            Get
                Return Me._file
            End Get
            Set
                Me._file = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("dependency", Order:=2)>
        Public Property dependency() As DependencyType()
            Get
                Return Me._dependency
            End Get
            Set
                Me._dependency = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=3)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property type() As String
            Get
                Return Me._type
            End Get
            Set
                Me._type = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me._base
            End Get
            Set
                Me._base = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me._href
            End Get
            Set
                Me._href = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Public Enum ResourceTypeType

        imsqti_deptest

        imsqti_depitem

        imsqti_depdriver

        imsqti_depmodule

        <System.Xml.Serialization.XmlEnumAttribute("associatedcontent/dep_xmlv1p0/learning-application-resource")>
        associatedcontentdep_xmlv1p0learningapplicationresource

        <System.Xml.Serialization.XmlEnumAttribute("controlfile/dep_xmlv1p0")>
        controlfiledep_xmlv1p0

        webcontent
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Resources.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class ResourcesType

        Private _resource() As ResourceType

        Private _any() As System.Xml.XmlElement

        Private _base As String

        Private _anyAttr() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlElementAttribute("resource", Order:=0)>
        Public Property resource() As ResourceType()
            Get
                Return Me._resource
            End Get
            Set
                Me._resource = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=1)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me._base
            End Get
            Set
                Me._base = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Item.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class ItemType

        Private _title As String

        Private _item() As ItemType

        Private _metadata As MetadataType

        Private _any() As System.Xml.XmlElement

        Private _identifier As String

        Private _identifierref As String

        Private _isvisible As Boolean

        Private _parameters As String

        Private _anyAttr() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("item", Order:=1)>
        Public Property item() As ItemType()
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property metadata() As MetadataType
            Get
                Return Me._metadata
            End Get
            Set
                Me._metadata = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=3)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property identifierref() As String
            Get
                Return Me._identifierref
            End Get
            Set
                Me._identifierref = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property isvisible() As Boolean
            Get
                Return Me._isvisible
            End Get
            Set
                Me._isvisible = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property parameters() As String
            Get
                Return Me._parameters
            End Get
            Set
                Me._parameters = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Organization.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class OrganizationType

        Private _title As String

        Private _item() As ItemType

        Private _metadata As MetadataType

        Private _any() As System.Xml.XmlElement

        Private _identifier As String

        Private _structure As String

        Private _anyAttr() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("item", Order:=1)>
        Public Property item() As ItemType()
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property metadata() As MetadataType
            Get
                Return Me._metadata
            End Get
            Set
                Me._metadata = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=3)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [structure]() As String
            Get
                Return Me._structure
            End Get
            Set
                Me._structure = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Organizations.Type", [Namespace]:="http://www.imsglobal.org/xsd/imscp_v1p1")>
    Partial Public Class OrganizationsType

        Private _organization() As OrganizationType

        Private _any() As System.Xml.XmlElement

        Private _default As String

        Private _anyAttr() As System.Xml.XmlAttribute

        <System.Xml.Serialization.XmlElementAttribute("organization", Order:=0)>
        Public Property organization() As OrganizationType()
            Get
                Return Me._organization
            End Get
            Set
                Me._organization = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=1)>
        Public Property Any() As System.Xml.XmlElement()
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="IDREF")>
        Public Property [default]() As String
            Get
                Return Me._default
            End Get
            Set
                Me._default = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property
    End Class
#Enable Warning
End NameSpace