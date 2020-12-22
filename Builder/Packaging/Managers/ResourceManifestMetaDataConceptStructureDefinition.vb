

Public Class ResourceManifestMetaDataConceptStructureDefinition
    Inherits ResourceManifestMetaDataStructureDefinitionBase

    Private _conceptStructurePartDefinitions As New ResourceManifestMetadataConceptStructurePartCollection

    Public Property ConceptStructurePartDefinitions() As ResourceManifestMetadataConceptStructurePartCollection
        Set(value As ResourceManifestMetadataConceptStructurePartCollection)
            _conceptStructurePartDefinitions = value
        End Set
        Get
            Return _conceptStructurePartDefinitions
        End Get
    End Property

End Class
