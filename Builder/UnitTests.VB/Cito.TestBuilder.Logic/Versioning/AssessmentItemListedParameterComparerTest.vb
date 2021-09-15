
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Direct
Imports Versioning

<TestClass()>
Public Class AssessmentItemListedParameterComparerTest
    Inherits ComparerTestBase

    Private _iltName1 As String = "ilt.compare.listed1"

    <TestMethod(), Description("Compare two AssessmentItem objects of type listed parameter. The names of the listedparameters are identical."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalListedParameters()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim listedParam1 As New ListedParameter()
        Dim listedParam2 As New ListedParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        listedParam1.Name = "listedParameter1"
        listedParam2.Name = "listedParameter1"
        listedParam1.Value = "3"
        listedParam2.Value = "3"

        paramCollection1.InnerParameters.Add(listedParam1)
        paramCollection2.InnerParameters.Add(listedParam2)

        'Arrange
        Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
        Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

        'Act
        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        'Assert
        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsNotNull(resourceHistoryEntity1.BinData)
        Assert.IsNotNull(resourceHistoryEntity2.BinData)
        Assert.IsNotNull(metaDataCompareResults)
        Assert.IsTrue(metaDataCompareResults.Count = 0)
    End Sub

End Class
