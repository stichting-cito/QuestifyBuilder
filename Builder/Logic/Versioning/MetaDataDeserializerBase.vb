Imports Versioning

Friend MustInherit Class MetaDataDeserializerBase

    Protected _successor As MetaDataDeserializerBase

    Friend Sub New(ByVal successor As MetaDataDeserializerBase)
        _successor = successor
    End Sub

    Friend MustOverride Function DeserializeMetaData(ByVal metaData As Byte()) As MetaData

End Class
