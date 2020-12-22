

Public Class ResourceManifestMetaDataTreeStructurePartDefinition
    Inherits ResourceManifestMetaDataStructurePartDefinition

    Private _childTreeStructureParts As New ResourceManifestMetadataTreeStructurePartCollection

    Public Property ChildTreeStructureParts As ResourceManifestMetadataTreeStructurePartCollection
        Get
            Return _childTreeStructureParts
        End Get
        Set(value As ResourceManifestMetadataTreeStructurePartCollection)
            _childTreeStructureParts = value
        End Set
    End Property
End Class
