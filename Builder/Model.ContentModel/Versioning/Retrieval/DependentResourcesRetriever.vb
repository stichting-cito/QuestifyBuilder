Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports System.Collections.Generic
Namespace Versioning.Retrieval
    Public Class DependentResourcesRetriever
        Inherits MetaDataRetrieverBase(Of List(Of DependentResourceMetaData))

        Public Sub New(ByVal propertyEntity As IPropertyEntity)
            MyBase.New(propertyEntity)
        End Sub

        Public Overrides Function CreateMetaData() As List(Of DependentResourceMetaData)
            Dim result As New List(Of DependentResourceMetaData)()

            For Each dependentResource As DependentResourceEntity In PropertyEntity.DependentResourceCollection
                If dependentResource IsNot Nothing AndAlso dependentResource.DependentResource IsNot Nothing Then
                    result.Add(New DependentResourceMetaData(dependentResource.DependentResource.ResourceId, dependentResource.DependentResource.Name, dependentResource.DependentResource.Version))
                End If
            Next

            Return result
        End Function
    End Class
End Namespace