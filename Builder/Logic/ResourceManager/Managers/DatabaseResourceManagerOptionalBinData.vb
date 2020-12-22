Imports System.IO
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories

Namespace ResourceManager

    Public Class DatabaseResourceManagerOptionalBinData
        Inherits DataBaseResourceManager
        Implements IResourceManagerForceBinData



        Public Sub New(ByVal bankId As Integer, Optional retrieveCustomBankProperties As Boolean = False)
            MyBase.New(bankId, retrieveCustomBankProperties)
        End Sub


        Public Overrides Function GetResource(ByVal name As String) As StreamResource
            Return GetResource(name, False)
        End Function

        Public Function GetResourceForceBinData(ByVal name As String) As StreamResource Implements IResourceManagerForceBinData.GetResourceForceBinData
            Return GetResource(name, True)
        End Function

        Private Overloads Function GetResource(ByVal name As String, ByVal forceBinData As Boolean) As StreamResource
            Dim resource As ResourceEntity

            If CachedResourceEntity IsNot Nothing AndAlso CachedResourceEntity.Name = name Then
                resource = cachedResourceEntity
            Else
                Dim request = new ResourceRequestDTO With {.WithCustomProperties = True, .WithDependencies = True}
                resource = ResourceFactory.Instance.GetResourceByNameWithOption(bankId, name, request)
                CachedResourceEntity = resource
            End If

            If resource IsNot Nothing Then
                Dim dependentResources As New DependentResourceCollection
                For Each dResource As DependentResourceEntity In resource.DependentResourceCollection
                    dependentResources.Add(New DependentResource(dResource.DependentResource.Name))
                Next

                Dim resourceStream As Stream = Nothing
                If forceBinData OrElse Not (TypeOf resource Is GenericResourceEntity) Then
                    Dim resourceData = ResourceFactory.Instance.GetResourceData(resource)
                    resourceStream = resourceData.GetStream().ResourceObject
                End If

                Dim sResourceToReturn As StreamResource
                If Not String.IsNullOrEmpty(resource.OriginalVersion) Then
                    sResourceToReturn = New StreamResource(name, ResourceVersionConverter.ConvertVersion(resource.Version), resource.GetType().Name, False, resourceStream, dependentResources, resource.OriginalName, ResourceVersionConverter.ConvertVersion(resource.OriginalVersion), resource.StateId.GetValueOrDefault())
                Else
                    sResourceToReturn = New StreamResource(name, ResourceVersionConverter.ConvertVersion(resource.Version), resource.GetType().Name, False, resourceStream, dependentResources, resource.StateId.GetValueOrDefault())
                End If
                sResourceToReturn.MetaData.Clear()

                resource.AddMetaDataTo(sResourceToReturn)

                If IncludeMetaData = MetaDataType.All OrElse IncludeMetaData = MetaDataType.Publishable Then
                    AddCustomProperties(resource, sResourceToReturn, IncludeMetaData)
                End If

                Return sResourceToReturn
            Else
                Return Nothing
            End If
        End Function

    End Class
End NameSpace