Imports System.Runtime.CompilerServices
Imports Questify.Builder.Model.ContentModel.EntityClasses.Workers
Imports System.Linq
Imports Cito.Tester.Common

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Public Module ExtensionMethods

        <Extension>
        Public Sub AddMetaDataTo(ByVal resource As EntityClasses.ResourceEntity, ByRef sResourceToReturn As StreamResource)
            Dim generator = New MetaDataGeneratorForEntity(resource)
            sResourceToReturn.MetaData.AddRange(generator.GetEntitySpecificMetadata())
            sResourceToReturn.MetaData.AddRange(generator.DefaultMetadata())
        End Sub

        <Extension>
        Public Function GetIdFromDependencyByName(ByVal resource As EntityClasses.ResourceEntity, name As String) As Guid
            Dim dep = resource.DependentResourceCollection.FirstOrDefault(Function(dependentResourceEntity) dependentResourceEntity.DependentResource.Name = name)
            If (dep IsNot Nothing) Then Return dep.DependentResourceId

            Return Guid.Empty
        End Function

    End Module



End Namespace
