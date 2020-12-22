
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Direct
Imports Versioning

<TestClass()>
Public Class DependentResourcesComparerTest
    Inherits ComparerTestBase

    <TestMethod(), Description("Compare two resourceHistory entities with identical DependentResource names."), TestCategory("Logic")>
    Public Sub TestCompareTwoResourceHistoryEntitiesWithTwoIdenticalDependentResources()
        Dim versionableEntity1 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
        Dim versionableEntity2 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))
        Dim propertyEntity1 As IPropertyEntity = CType(versionableEntity1, IPropertyEntity)
        Dim propertyEntity2 As IPropertyEntity = CType(versionableEntity2, IPropertyEntity)
        Dim dependentResourceEntityId As Guid = Guid.NewGuid()
        Dim dependentResourceId As Guid = Guid.NewGuid()
        Dim dependentResourceEntity1 As New DependentResourceEntity()
        Dim dependentResourceEntity2 As New DependentResourceEntity()
        dependentResourceEntity1.DependentResourceId = dependentResourceId
        dependentResourceEntity2.DependentResourceId = dependentResourceId
        Dim resourceEntity1 As New ResourceEntity(dependentResourceEntityId)
        Dim resourceEntity2 As New ResourceEntity(dependentResourceEntityId)
        resourceEntity1.Name = "Resource 1 as dependency"
        resourceEntity2.Name = "Resource 1 as dependency"
        dependentResourceEntity1.DependentResource = resourceEntity1
        dependentResourceEntity2.DependentResource = resourceEntity2

        propertyEntity1.DependentResourceCollection.Add(dependentResourceEntity1)
        propertyEntity2.DependentResourceCollection.Add(dependentResourceEntity2)

        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsTrue(metaDataCompareResults.Count = 0)
    End Sub

    <TestMethod(), Description("Compare two resourceHistory entities each with a DependentResource. They have the same id but different names."), TestCategory("Logic")>
    Public Sub TestCompareTwoResourceHistoryEntitiesWithTwoDependentResourcesWithSameIdButDifferentNames()
        Dim versionableEntity1 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
        Dim versionableEntity2 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))
        Dim propertyEntity1 As IPropertyEntity = CType(versionableEntity1, IPropertyEntity)
        Dim propertyEntity2 As IPropertyEntity = CType(versionableEntity2, IPropertyEntity)
        Dim dependentResourceEntityId As Guid = Guid.NewGuid()
        Dim dependentResourceId As Guid = Guid.NewGuid()
        Dim dependentResourceEntity1 As New DependentResourceEntity()
        Dim dependentResourceEntity2 As New DependentResourceEntity()
        dependentResourceEntity1.DependentResourceId = dependentResourceId
        dependentResourceEntity2.DependentResourceId = dependentResourceId
        Dim resourceEntity1 As New ResourceEntity(dependentResourceEntityId)
        Dim resourceEntity2 As New ResourceEntity(dependentResourceEntityId)
        resourceEntity1.Name = "Resource 1 as dependency"
        resourceEntity2.Name = "Resource 2 as dependency"
        dependentResourceEntity1.DependentResource = resourceEntity1
        dependentResourceEntity2.DependentResource = resourceEntity2

        propertyEntity1.DependentResourceCollection.Add(dependentResourceEntity1)
        propertyEntity2.DependentResourceCollection.Add(dependentResourceEntity2)

        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsTrue(metaDataCompareResults.Count = 1)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = resourceEntity2.Name)
    End Sub

    <TestMethod(), Description("Compare two resourceHistory entities with different DependentResources. The first propertyEntity contains a DependentResource, the second doesn't"), TestCategory("Logic")>
    Public Sub TestCompareTwoResourceHistoryEntitiesWithDifferentDependentResources1()
        Dim versionableEntity1 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
        Dim versionableEntity2 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))
        Dim propertyEntity1 As IPropertyEntity = CType(versionableEntity1, IPropertyEntity)
        Dim propertyEntity2 As IPropertyEntity = CType(versionableEntity2, IPropertyEntity)
        Dim dependentResourceEntity1 As New DependentResourceEntity()
        dependentResourceEntity1.DependentResourceId = Guid.NewGuid()
        Dim resourceEntity1 As New ResourceEntity(Guid.NewGuid())
        resourceEntity1.Name = "Resource 1 as dependency"
        dependentResourceEntity1.DependentResource = resourceEntity1

        propertyEntity1.DependentResourceCollection.Add(dependentResourceEntity1)

        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsTrue(metaDataCompareResults.Count = 1)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = resourceEntity1.Name)
    End Sub

    <TestMethod(), Description("Compare two resourceHistory entities with different DependentResources. The second propertyEntity contains a DependentResource, the first doesn't"), TestCategory("Logic")>
    Public Sub TestCompareTwoResourceHistoryEntitiesWithDifferentDependentResources2()
        Dim versionableEntity1 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
        Dim versionableEntity2 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))
        Dim propertyEntity1 As IPropertyEntity = CType(versionableEntity1, IPropertyEntity)
        Dim propertyEntity2 As IPropertyEntity = CType(versionableEntity2, IPropertyEntity)
        Dim dependentResourceEntity1 As New DependentResourceEntity()
        dependentResourceEntity1.DependentResourceId = Guid.NewGuid()
        Dim resourceEntity1 As New ResourceEntity(Guid.NewGuid())
        resourceEntity1.Name = "Resource 1 as dependency"
        dependentResourceEntity1.DependentResource = resourceEntity1

        propertyEntity2.DependentResourceCollection.Add(dependentResourceEntity1)

        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsTrue(metaDataCompareResults.Count = 1)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = resourceEntity1.Name)
    End Sub

    <TestMethod(), Description("Compare two resourceHistory entities with different DependentResources. They both have one dependent resource but are different from each other."), TestCategory("Logic")>
    Public Sub TestCompareTwoResourceHistoryEntitiesWithTwoDifferentResources()
        Dim versionableEntity1 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
        Dim versionableEntity2 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))
        Dim propertyEntity1 As IPropertyEntity = CType(versionableEntity1, IPropertyEntity)
        Dim propertyEntity2 As IPropertyEntity = CType(versionableEntity2, IPropertyEntity)
        Dim dependentResourceEntity1 As New DependentResourceEntity()
        Dim dependentResourceEntity2 As New DependentResourceEntity()
        dependentResourceEntity1.DependentResourceId = Guid.NewGuid()
        dependentResourceEntity2.DependentResourceId = Guid.NewGuid()
        Dim resourceEntity1 As New ResourceEntity(Guid.NewGuid())
        Dim resourceEntity2 As New ResourceEntity(Guid.NewGuid())
        resourceEntity1.Name = "Resource 1 as dependency"
        resourceEntity2.Name = "Resource 2 as dependency"
        dependentResourceEntity1.DependentResource = resourceEntity1
        dependentResourceEntity2.DependentResource = resourceEntity2

        propertyEntity1.DependentResourceCollection.Add(dependentResourceEntity1)
        propertyEntity2.DependentResourceCollection.Add(dependentResourceEntity2)

        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsTrue(metaDataCompareResults.Count = 2)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = resourceEntity1.Name)
        Assert.IsTrue(metaDataCompareResults(1).PropertyName = resourceEntity2.Name)
    End Sub

    <TestMethod(), Description("Compare two resourceHistory entities with different number of DependentResources. The first has two dependent resources and the second has three. They share only one dependent resource."), TestCategory("Logic")>
    Public Sub TestCompareSeveralResourceHistoryEntitiesWithOneSharedResource()
        Dim versionableEntity1 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
        Dim versionableEntity2 As IVersionable = CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))
        Dim propertyEntity1 As IPropertyEntity = CType(versionableEntity1, IPropertyEntity)
        Dim propertyEntity2 As IPropertyEntity = CType(versionableEntity2, IPropertyEntity)
        Dim dependentResourceEntity1_1 As New DependentResourceEntity()
        Dim dependentResourceEntity1_2 As New DependentResourceEntity()
        Dim dependentResourceEntity2_1 As New DependentResourceEntity()
        Dim dependentResourceEntity2_2 As New DependentResourceEntity()
        Dim dependentResourceEntity2_3 As New DependentResourceEntity()

        dependentResourceEntity1_1.DependentResourceId = Guid.NewGuid()
        dependentResourceEntity1_2.DependentResourceId = Guid.NewGuid()
        dependentResourceEntity2_1.DependentResourceId = Guid.NewGuid()
        dependentResourceEntity2_2.DependentResourceId = Guid.NewGuid()
        dependentResourceEntity2_3.DependentResourceId = Guid.NewGuid()
        Dim resourceEntityShared As New ResourceEntity(Guid.NewGuid())
        Dim resourceEntity1_1 As New ResourceEntity(Guid.NewGuid())
        Dim resourceEntity2_1 As New ResourceEntity(Guid.NewGuid())
        Dim resourceEntity2_2 As New ResourceEntity(Guid.NewGuid())
        resourceEntityShared.Name = "Shared Resource"
        resourceEntity1_1.Name = "Resource 1 as dependency 1"
        resourceEntity2_1.Name = "Resource 2 as dependency 1"
        resourceEntity2_2.Name = "Resource 2 as dependency 2"
        dependentResourceEntity1_1.DependentResource = resourceEntityShared
        dependentResourceEntity1_2.DependentResource = resourceEntity1_1
        dependentResourceEntity2_1.DependentResource = resourceEntityShared
        dependentResourceEntity2_2.DependentResource = resourceEntity2_1
        dependentResourceEntity2_3.DependentResource = resourceEntity2_2

        propertyEntity1.DependentResourceCollection.Add(dependentResourceEntity1_1)
        propertyEntity1.DependentResourceCollection.Add(dependentResourceEntity1_2)
        propertyEntity2.DependentResourceCollection.Add(dependentResourceEntity2_1)
        propertyEntity2.DependentResourceCollection.Add(dependentResourceEntity2_2)
        propertyEntity2.DependentResourceCollection.Add(dependentResourceEntity2_3)

        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsTrue(metaDataCompareResults.Count = 3)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = resourceEntity1_1.Name)
        Assert.IsTrue(metaDataCompareResults(1).PropertyName = resourceEntity2_1.Name)
        Assert.IsTrue(metaDataCompareResults(2).PropertyName = resourceEntity2_2.Name)
    End Sub

    Private Function CreateResourceEntity(ByVal propertyEntity As IPropertyEntity, resourceData As Object) As IVersionable
        propertyEntity.Id = Guid.NewGuid()
        propertyEntity.Name = "TestItem - Name"
        propertyEntity.Title = "TestItem - Title"
        propertyEntity.Description = "TestItem - Description"
        propertyEntity.ResourceData = New ResourceDataEntity()
        propertyEntity.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(resourceData)

        Return CType(propertyEntity, IVersionable)
    End Function

End Class