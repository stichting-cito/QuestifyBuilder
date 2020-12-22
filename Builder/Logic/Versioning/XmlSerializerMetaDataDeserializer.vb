Imports Cito.Tester.Common

Friend Class XmlSerializerMetaDataDeserializer
    Inherits MetaDataDeserializerBase

    Friend Sub New(ByVal successor As MetaDataDeserializerBase)
        MyBase.New(successor)
    End Sub


    Friend Overrides Function DeserializeMetaData(metaData As Byte()) As Versioning.MetaData
        Try
            Return CType(SerializeHelper.XmlDeserializeFromByteArray(metaData, GetType(Versioning.MetaData)), Versioning.MetaData)
        Catch ioe As InvalidOperationException
            If _successor IsNot Nothing Then
                Return _successor.DeserializeMetaData(metaData)
            End If

            Throw New MetaDataDeserializationException(ioe.Message, ioe)
        End Try
    End Function

End Class
