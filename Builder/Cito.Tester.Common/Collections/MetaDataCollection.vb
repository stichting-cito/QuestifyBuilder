Public Class MetaDataCollection
    Inherits List(Of MetaData)

    Public Overloads Property Item(name As String, metadatatype As MetaData.enumMetaDataType) As MetaData
        Get
            For i As Integer = 0 To Me.Count - 1
                If Me.Item(i).Name.Equals(name, StringComparison.OrdinalIgnoreCase) AndAlso Me.Item(i).MetaDatatype = metadatatype Then
                    Return Me.Item(i)
                End If
            Next

            Return Nothing
        End Get
        Set(value As MetaData)
            Me.Item(name, metadatatype) = value
        End Set
    End Property

End Class
