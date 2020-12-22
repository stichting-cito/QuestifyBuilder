Public Class ResourceManifestMetadataValueCollection
    Inherits List(Of ResourceManifestMetadataValue)

    Public Overloads Property Item(name As String) As ResourceManifestMetadataValue
        Get
            For i As Integer = 0 To Me.Count - 1
                If Me.Item(i).Name.Equals(name, StringComparison.OrdinalIgnoreCase) Then
                    Return Me.Item(i)
                End If
            Next

            Return Nothing
        End Get
        Set(value As ResourceManifestMetadataValue)
            Me.Item(name) = value
        End Set
    End Property

End Class
