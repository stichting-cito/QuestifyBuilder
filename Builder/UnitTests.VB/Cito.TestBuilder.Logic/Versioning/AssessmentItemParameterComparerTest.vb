
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Direct
Imports Versioning

<TestClass()>
Public Class AssessmentItemParameterComparerTest
    Inherits ComparerTestBase

    Private _iltName1 As String = "ilt.compare.integer1"
    Private _iltName2 As String = "ilt.compare.boolean1"

    <TestMethod(), Description("Compare two AssessmentItem objects. One of type integer and one of type Boolean."), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithDifferentParameterTypes()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim integerParam1 As New IntegerParameter()
        Dim booleanParam1 As New BooleanParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        integerParam1.Name = "parameter1"
        booleanParam1.Name = "parameter1"
        integerParam1.Value = 10
        booleanParam1.Value = True

        paramCollection1.InnerParameters.Add(integerParam1)
        paramCollection2.InnerParameters.Add(booleanParam1)

        Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
        Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsNotNull(resourceHistoryEntity1.BinData)
        Assert.IsNotNull(resourceHistoryEntity2.BinData)
        Assert.IsNotNull(metaDataCompareResults)
        Assert.IsTrue(metaDataCompareResults.Count = 1)
        Assert.IsTrue(metaDataCompareResults(0).OldValue.StartsWith("Type mismatch"))
        Assert.IsTrue(metaDataCompareResults(0).NewValue.StartsWith("Type mismatch"))
    End Sub

End Class
