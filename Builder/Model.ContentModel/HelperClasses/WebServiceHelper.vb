' ///////////////////////////////////////////////////////////////
' This is generated code. 
' //////////////////////////////////////////////////////////////
' Code is generated using LLBLGen Pro version: 2.6
' Code is generated on: vrijdag 2 december 2016 12:02:09
' Code is generated using templates: SD.TemplateBindings.SharedTemplates.NET20
' Templates vendor: Solutions Design.
' Templates version: 
' //////////////////////////////////////////////////////////////

Imports System.Xml.Serialization
Imports System.Xml.Schema
Imports System.Xml
Imports System.IO

#Region "Entity Classes"

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ActionEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ActionEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ActionEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ActionEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class AspectResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='AspectResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='AspectResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("AspectResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class AssessmentTestResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='AssessmentTestResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='AssessmentTestResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("AssessmentTestResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class BankEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='BankEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='BankEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("BankEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class CachedItemLayoutTemplateResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='CachedItemLayoutTemplateResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='CachedItemLayoutTemplateResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("CachedItemLayoutTemplateResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ChildConceptStructurePartCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ChildConceptStructurePartCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ChildConceptStructurePartCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ChildConceptStructurePartCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ChildTreeStructurePartCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ChildTreeStructurePartCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ChildTreeStructurePartCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ChildTreeStructurePartCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ClientEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ClientEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ClientEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ClientEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ConceptStructureCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ConceptStructureCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ConceptStructureCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ConceptStructureCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ConceptStructureCustomBankPropertySelectedPartEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ConceptStructureCustomBankPropertySelectedPartEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ConceptStructureCustomBankPropertySelectedPartEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ConceptStructureCustomBankPropertySelectedPartEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ConceptStructureCustomBankPropertyValueEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ConceptStructureCustomBankPropertyValueEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ConceptStructureCustomBankPropertyValueEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ConceptStructureCustomBankPropertyValueEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ConceptStructurePartCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ConceptStructurePartCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ConceptStructurePartCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ConceptStructurePartCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ConceptTypeEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ConceptTypeEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ConceptTypeEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ConceptTypeEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ControlTemplateResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ControlTemplateResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ControlTemplateResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ControlTemplateResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class CustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='CustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='CustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("CustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class CustomBankPropertyValueEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='CustomBankPropertyValueEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='CustomBankPropertyValueEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("CustomBankPropertyValueEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class DataSourceResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='DataSourceResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='DataSourceResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("DataSourceResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class DeliveryResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='DeliveryResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='DeliveryResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("DeliveryResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class DependentResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='DependentResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='DependentResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("DependentResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class FreeValueCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='FreeValueCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='FreeValueCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("FreeValueCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class FreeValueCustomBankPropertyValueEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='FreeValueCustomBankPropertyValueEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='FreeValueCustomBankPropertyValueEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("FreeValueCustomBankPropertyValueEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class GenericResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='GenericResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='GenericResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("GenericResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class HiddenResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='HiddenResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='HiddenResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("HiddenResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ItemLayoutTemplateResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ItemLayoutTemplateResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ItemLayoutTemplateResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ItemLayoutTemplateResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ItemResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ItemResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ItemResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ItemResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ListCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ListCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ListCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ListCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ListCustomBankPropertySelectedValueEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ListCustomBankPropertySelectedValueEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ListCustomBankPropertySelectedValueEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ListCustomBankPropertySelectedValueEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ListCustomBankPropertyValueEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ListCustomBankPropertyValueEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ListCustomBankPropertyValueEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ListCustomBankPropertyValueEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ListValueCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ListValueCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ListValueCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ListValueCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class PackageResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='PackageResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='PackageResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("PackageResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class PermissionEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='PermissionEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='PermissionEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("PermissionEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class PermissionTargetEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='PermissionTargetEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='PermissionTargetEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("PermissionTargetEntity", namespaceToUse)
        End Function
    End Class
    
    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class PsychometricsResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='PsychometricsResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='PsychometricsResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("PsychometricsResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ResourceDataEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ResourceDataEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ResourceDataEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ResourceDataEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ResourceExposureLogEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ResourceExposureLogEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ResourceExposureLogEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ResourceExposureLogEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class ResourceHistoryEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='ResourceHistoryEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='ResourceHistoryEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("ResourceHistoryEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class RichTextValueCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='RichTextValueCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='RichTextValueCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("RichTextValueCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class RichTextValueCustomBankPropertyValueEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='RichTextValueCustomBankPropertyValueEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='RichTextValueCustomBankPropertyValueEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("RichTextValueCustomBankPropertyValueEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class RoleEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='RoleEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='RoleEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("RoleEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class RolePermissionEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='RolePermissionEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='RolePermissionEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("RolePermissionEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class StateEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='StateEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='StateEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("StateEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class StateActionEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='StateActionEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='StateActionEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("StateActionEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class TestPackageResourceEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='TestPackageResourceEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='TestPackageResourceEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("TestPackageResourceEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class TreeStructureCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='TreeStructureCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='TreeStructureCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("TreeStructureCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class TreeStructureCustomBankPropertySelectedPartEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='TreeStructureCustomBankPropertySelectedPartEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='TreeStructureCustomBankPropertySelectedPartEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("TreeStructureCustomBankPropertySelectedPartEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class TreeStructureCustomBankPropertyValueEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public Shadows Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='TreeStructureCustomBankPropertyValueEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='TreeStructureCustomBankPropertyValueEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("TreeStructureCustomBankPropertyValueEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class TreeStructurePartCustomBankPropertyEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='TreeStructurePartCustomBankPropertyEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='TreeStructurePartCustomBankPropertyEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("TreeStructurePartCustomBankPropertyEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class UserEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='UserEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='UserEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("UserEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class UserApplicationRoleEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='UserApplicationRoleEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='UserApplicationRoleEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("UserApplicationRoleEntity", namespaceToUse)
        End Function
    End Class

    <XmlSchemaProvider("GetEntitySchema")> _
    Public Partial Class UserBankRoleEntity
        ''' <summary>
        ''' Method which provides a schema for IXmlSerializable implementation so no proxies are generated.
        ''' </summary>
        ''' <param name="schemaSet">schema set which is the current schema for the type to produce</param>
        ''' <returns></returns>
        Public  Shared Function GetEntitySchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName
            Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
            Dim xs As XmlSchema = XmlSchema.Read( _
                New StringReader( _
                    String.Format("<xs:schema id='UserBankRoleEntitySchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='UserBankRoleEntity'><xs:sequence><xs:any minOccurs='0'/></xs:sequence></xs:complexType></xs:schema>", _
                        namespaceToUse)), Nothing)
            schemaSet.XmlResolver = New XmlUrlResolver()
            schemaSet.Add(xs)
            Return New XmlQualifiedName("UserBankRoleEntity", namespaceToUse)
        End Function
    End Class

End Namespace
#End Region

#Region "EntityCollection"
Namespace Questify.Builder.Model.ContentModel.HelperClasses
	<XmlSchemaProvider("GetEntityCollectionSchema")> _
	Public Partial Class EntityCollection
		Public Shared Function GetEntityCollectionSchema(schemaSet As XmlSchemaSet ) As XmlQualifiedName 
			Dim namespaceToUse As String = "http://Questify.Builder.Model.ContentModel/xml/serialization"
			Dim xs As XmlSchema = XmlSchema.Read( _
				New StringReader( _
					String.Format("<xs:schema id='EntityCollectionSchema' targetNamespace='{0}' elementFormDefault='qualified' xmlns='{0}' xmlns:mstns='{0}' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='EntityCollection'></xs:complexType></xs:schema>", _
						namespaceToUse)), Nothing)
			schemaSet.XmlResolver = New XmlUrlResolver()
			schemaSet.Add(xs)
			Return New XmlQualifiedName("EntityCollection", namespaceToUse)
		End Function
	End Class
End Namespace
#End Region

#Region "TypedView Classes"
Namespace Questify.Builder.Model.ContentModel.TypedViewClasses

End Namespace
#End Region

#Region "TypedList Classes"
Namespace Questify.Builder.Model.ContentModel.TypedListClasses

End Namespace
#End Region
