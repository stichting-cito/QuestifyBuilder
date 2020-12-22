Public MustInherit Class ResourceManagerDecorator
    Inherits ResourceManagerBase

    ReadOnly _decoree As ResourceManagerBase

    Public Sub New(decoree As ResourceManagerBase)
        If (decoree Is Nothing) Then Throw New ArgumentNullException("decoree")
        _decoree = decoree
    End Sub



    Public Overrides Function GetTypedResource(name As String, usingType As Type, request As ResourceRequestDTO) As BinaryResource
        Return _decoree.GetTypedResource(name, usingType, request)
    End Function

    Public Overrides Function GetResource(name As String, processingMethod As ResourceProcessingFunction, request As ResourceRequestDTO) As BinaryResource
        Return _decoree.GetResource(name, processingMethod, request)
    End Function

    Public Overrides Function GetResourcesOfType(type As String) As ResourceEntryCollection
        Return _decoree.GetResourcesOfType(type)
    End Function


    Public Overrides Function GetResource(name As String) As StreamResource
        Return _decoree.GetResource(name)
    End Function


    Public Overrides Function GetResource(name As String, request As ResourceRequestDTO) As StreamResource
        Return _decoree.GetResource(name, request)
    End Function


    Public Overrides Function GetResourceMetaData(name As String) As MetaDataCollection
        Return _decoree.GetResourceMetaData(name)
    End Function


    Public Overrides Function GetResourceEntry(name As String) As ResourceEntry
        Return _decoree.GetResourceEntry(name)
    End Function

    Public Overrides Function GetDependentResourcesForResource(name As String) As DependentResourceCollection
        Return _decoree.GetDependentResourcesForResource(name)
    End Function


    Public Overrides Function GetDependentResourcesForResource(name As String, getCopies As Boolean) As DependentResourceCollection
        Return _decoree.GetDependentResourcesForResource(name, getCopies)
    End Function

    Public Overrides Function GetGenericResourceMimeType(name As String) As String
        Return _decoree.GetGenericResourceMimeType(name)
    End Function



    Public Overrides Sub PutResource(resource As StreamResource)
        _decoree.PutResource(resource)
    End Sub

    Public Overrides Sub UpdateResource(resource As StreamResource)
        _decoree.UpdateResource(resource)
    End Sub


End Class
