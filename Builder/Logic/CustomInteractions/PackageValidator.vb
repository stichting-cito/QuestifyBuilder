Namespace CustomInteractions

    Public MustInherit Class PackageValidator

        Protected ReadOnly Path As String

        Public Sub New(path As String)
            Me.Path = path
        End Sub

        Public MustOverride Function TryValidate(ByRef errorMessages As List(Of String)) As Boolean

        Public Overridable Function TryValidate(ByRef errorMessages As List(Of String), ByRef metaData As MetadataRoot) As Boolean

        End Function

        Public MustOverride Function GetMetaData() As MetadataRoot

    End Class
End Namespace