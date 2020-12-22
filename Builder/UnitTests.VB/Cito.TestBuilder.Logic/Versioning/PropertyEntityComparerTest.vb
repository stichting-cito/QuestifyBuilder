Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Cito.TestBuilder.ContentModel.Interfaces
Imports Cito.TestBuilder.ContentModel.EntityClasses
Imports Cito.TestBuilder.Common
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Cito.TestBuilder.Service.Direct
Imports Cito.TestBuilder.Logic

<TestClass()>
Public Class PropertyEntityComparerTest

	<TestMethod(), Owner("remcor"), Description("Compare two identical resourceHistory entities."), TestCategory("Versioning"), TestCategory("Logic")>
	Public Sub TestCompareTwoIdenticalResourceHistoryEntities()
		'Arrange
		Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
		Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))

		'Act
		Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "remcor")
		Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "remcor")
		Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
		metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity)))

		'Assert
		Assert.IsNotNull(resourceHistoryEntity1.MetaData)
		Assert.IsNotNull(resourceHistoryEntity2.MetaData)
		Assert.IsNotNull(resourceHistoryEntity1.BinData)
		Assert.IsNotNull(resourceHistoryEntity2.BinData)
		Assert.IsNotNull(metaDataCompareResults)
		Assert.IsTrue(metaDataCompareResults.Count = 0)
	End Sub

	<TestMethod(), Owner("remcor"), Description("Compare two resourceHistory entities with different titles in their propertyEntity."), TestCategory("Versioning"), TestCategory("Logic")>
	Public Sub TestCompareTwoResourceHistoryEntitiesWithDifferentTitleInPropertyEntities1()
		'Arrange
		Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
		Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))
		CType(versionableEntity2, IPropertyEntity).Title = "A new Title"

		'Act
		Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "remcor")
		Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "remcor")
		Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
		metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity)))

		'Assert
		Assert.IsNotNull(resourceHistoryEntity1.MetaData)
		Assert.IsNotNull(resourceHistoryEntity2.MetaData)
		Assert.IsNotNull(resourceHistoryEntity1.BinData)
		Assert.IsNotNull(resourceHistoryEntity2.BinData)
		Assert.IsNotNull(metaDataCompareResults)
		Assert.IsTrue(metaDataCompareResults.Count = 1)
		Assert.IsTrue(metaDataCompareResults(0).PropertyName = "Title")
		Assert.IsTrue(metaDataCompareResults(0).OldValue <> metaDataCompareResults(0).NewValue)
	End Sub

	<TestMethod(), Owner("remcor"), Description("Compare two resourceHistory entities with different titles in their propertyEntity."), TestCategory("Versioning"), TestCategory("Logic")>
	Public Sub TestCompareTwoResourceHistoryEntitiesWithDifferentTitleInPropertyEntities2()
		'Arrange
		Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 1"))
		Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), Helper.CreateAssessmentItem("Title of AssessmentItem 2"))
		CType(versionableEntity2, IPropertyEntity).Title = "A new Title"

		'Act
		Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "remcor")
		Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "remcor")
		Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
		metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity)))

		'Assert
		Assert.IsNotNull(resourceHistoryEntity1.MetaData)
		Assert.IsNotNull(resourceHistoryEntity2.MetaData)
		Assert.IsNotNull(resourceHistoryEntity1.BinData)
		Assert.IsNotNull(resourceHistoryEntity2.BinData)
		Assert.IsNotNull(metaDataCompareResults)
		Assert.IsTrue(metaDataCompareResults.Count = 1)
		Assert.IsTrue(metaDataCompareResults(0).PropertyName = "Title")
		Assert.IsTrue(metaDataCompareResults(0).OldValue <> metaDataCompareResults(0).NewValue)
	End Sub

End Class