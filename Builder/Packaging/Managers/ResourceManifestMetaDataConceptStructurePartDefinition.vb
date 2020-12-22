
Imports System.Xml.Serialization

Public Class ResourceManifestMetaDataConceptStructurePartDefinition
    Inherits ResourceManifestMetaDataStructurePartDefinition

    Private _conceptTypeId As Integer
    Private _childConceptStructureParts As New ResourceManifestMetadataConceptStructurePartCollection

    <XmlAttribute("concepttypeid")> _
    Public Property ConceptTypeId As Integer
        Get
            Return _conceptTypeId
        End Get
        Set(value As Integer)
            _conceptTypeId = value
        End Set
    End Property

    Public Property ChildConceptStructureParts As ResourceManifestMetadataConceptStructurePartCollection
        Get
            Return _childConceptStructureParts
        End Get
        Set(value As ResourceManifestMetadataConceptStructurePartCollection)
            _childConceptStructureParts = value
        End Set
    End Property
End Class
