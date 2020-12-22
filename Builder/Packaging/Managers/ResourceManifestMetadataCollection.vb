Public Class ResourceManifestMetadataCollection
    Inherits List(Of ResourceManifestMetadataElement)


    Public Overloads Property Item(name As String, metadatatype As ResourceManifestMetadataDefinitionBase.enumMetaDataType) As ResourceManifestMetadataElement
        Get
            For i As Integer = 0 To Me.Count - 1
                If Me.Item(i).Name.Equals(name, StringComparison.OrdinalIgnoreCase) AndAlso Me.Item(i).MetaDataType = metadatatype Then
                    Return Me.Item(i)
                End If
            Next

            Return Nothing
        End Get
        Set(value As ResourceManifestMetadataElement)
            Me.Item(name, metadatatype) = value
        End Set
    End Property


End Class

