

Public Class ResourceManifestMetaDataTreeStructureDefinition
    Inherits ResourceManifestMetaDataStructureDefinitionBase

    Private _treeStructurePartDefinitions As New ResourceManifestMetadataTreeStructurePartCollection

    Public Property TreeStructurePartDefinitions() As ResourceManifestMetadataTreeStructurePartCollection
        Set(value As ResourceManifestMetadataTreeStructurePartCollection)
            _treeStructurePartDefinitions = value
        End Set
        Get
            Return _treeStructurePartDefinitions
        End Get
    End Property

End Class
