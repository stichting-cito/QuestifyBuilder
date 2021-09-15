
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Direct
Imports Versioning

<TestClass()>
Public Class AssessmentItemBooleanParameterComparerTest
    Inherits ComparerTestBase

    Private _iltName1 As String = "ilt.compare.boolean1"
    Private _iltName2 As String = "ilt.compare.boolean2"

    <TestMethod(), Description("Compare two AssessmentItem objects of type boolean. The number of parameters are the same and also their values."), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalBooleanParametersAndSameValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim booleanParam1 As New BooleanParameter()
        Dim booleanParam2 As New BooleanParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        booleanParam1.Name = "parameter1"   'Same parameter names! ParameterName must match the name of the parameter in the ILT
        booleanParam2.Name = "parameter1"   'Same parameter names! ParameterName must match the name of the parameter in the ILT
        booleanParam1.Value = True 'Same values!
        booleanParam2.Value = True 'Same values!

        paramCollection1.InnerParameters.Add(booleanParam1)
        paramCollection2.InnerParameters.Add(booleanParam2)

        'Arrange
        Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
        Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

        'Act
        Dim resourceHistoryEntity1 As ResourceHistoryEntity =ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
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

    <TestMethod(), Description("Compare two AssessmentItem objects of type boolean. The number of parameters are different but their values are the same."), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithDifferentBooleanParametersButSameValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim booleanParam1 As New BooleanParameter()
        Dim booleanParam2 As New BooleanParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        booleanParam1.Name = "parameter1" 'Different parameter names!
        booleanParam2.Name = "parameter2" 'Different parameter names!
        booleanParam1.Value = True 'Same values!
        booleanParam2.Value = True 'Same values!

        paramCollection1.InnerParameters.Add(booleanParam1)
        paramCollection2.InnerParameters.Add(booleanParam2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 2)
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Boolean) = True)
        Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(0).PropertyName)
        Assert.IsTrue(CType(metaDataCompareResults(1).NewValue, Boolean) = True)
        Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type boolean. The number of parameters are different and also their values."), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithDifferentBooleanParametersAndDifferentValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim booleanParam1 As New BooleanParameter()
        Dim booleanParam2 As New BooleanParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        booleanParam1.Name = "parameter1" 'Different parameter names!
        booleanParam2.Name = "parameter2" 'Different parameter names!
        booleanParam1.Value = True 'Different values!
        booleanParam2.Value = False 'Different values!

        paramCollection1.InnerParameters.Add(booleanParam1)
        paramCollection2.InnerParameters.Add(booleanParam2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 2)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(0).PropertyName)
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Boolean) = True)
        Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
        Assert.IsTrue(CType(metaDataCompareResults(1).NewValue, Boolean) = False)
        Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type boolean. Then number of parameters are the same but their values differ."), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalBooleanParametersAndDifferentValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim booleanParam1 As New BooleanParameter()
        Dim booleanParam2 As New BooleanParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        booleanParam1.Name = "parameter1"   'Same parameter names!
        booleanParam2.Name = "parameter1"   'Same parameter names!
        booleanParam1.Value = True 'Different values!
        booleanParam2.Value = False 'Different values!
   
        paramCollection1.InnerParameters.Add(booleanParam1)
        paramCollection2.InnerParameters.Add(booleanParam2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 1)
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Boolean) = True)
        Assert.IsTrue(CType(metaDataCompareResults(0).NewValue, Boolean) = False)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(0).PropertyName)
    End Sub

End Class
