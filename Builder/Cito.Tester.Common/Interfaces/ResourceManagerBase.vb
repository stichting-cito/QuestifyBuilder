Public MustInherit Class ResourceManagerBase


    Public MustOverride Function GetTypedResource(name As String, usingType As Type, request As ResourceRequestDTO) As BinaryResource

    Public MustOverride Function GetResource(name As String, processingMethod As ResourceProcessingFunction, request As ResourceRequestDTO) As BinaryResource

    Public MustOverride Function GetResourcesOfType(type As String) As ResourceEntryCollection

    Public Overridable Function GetResource(name As String) As StreamResource
        Throw New NotImplementedException
    End Function

    Public MustOverride Function GetResource(name As String, request As ResourceRequestDTO) As StreamResource

    Public Overridable Function GetResourceMetaData(name As String) As MetaDataCollection
        Throw New NotImplementedException
    End Function

    Public Overridable Function GetResourceEntry(name As String) As ResourceEntry
        Throw New NotImplementedException
    End Function

    Public Overridable Function GetGenericResourceMimeType(name As String) As String
        Throw New NotImplementedException
    End Function

    Public Overridable Function GetDependentResourcesForResource(name As String) As DependentResourceCollection
        Throw New NotImplementedException
    End Function

    Public Overridable Function GetDependentResourcesForResource(name As String, getCopies As Boolean) As DependentResourceCollection
        Throw New NotImplementedException
    End Function



    Public Overridable Sub PutResource(resource As StreamResource)
        Throw New NotImplementedException
    End Sub

    Public Overridable Sub PutResource(resource As StreamResource, refetch As Boolean)
        Throw New NotImplementedException
    End Sub

    Public Overridable Sub UpdateResource(resource As StreamResource)
        Throw New NotImplementedException
    End Sub

    Public Overridable Sub UpdateResource(resource As StreamResource, refetch As Boolean)
        Throw New NotImplementedException
    End Sub


    Protected Sub New()
    End Sub

End Class

