
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Direct
Imports Versioning

<TestClass()>
Public Class AssessmentItemIntegerParameterComparerTest
    Inherits ComparerTestBase

    Private _iltName1 As String = "ilt.compare.integer1"
    Private _iltName2 As String = "ilt.compare.integer2"

    <TestMethod(), Description("Compare two AssessmentItem objects of type integer. The number of parameters are the same and also their values."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalIntegerParametersAndSameValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim integerParam1 As New IntegerParameter()
        Dim integerParam2 As New IntegerParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        integerParam1.Name = "parameter1"   'Same parameter names!
        integerParam2.Name = "parameter1"   'Same parameter names!
        integerParam1.Value = 10 'Same values!
        integerParam2.Value = 10 'Same values!

        paramCollection1.InnerParameters.Add(integerParam1)
        paramCollection2.InnerParameters.Add(integerParam2)

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

    <TestMethod(), Description("Compare two AssessmentItem objects of type integer. The number of parameters are different but their values are the same."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithDifferentIntegerParametersButSameValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim integerParam1 As New IntegerParameter()
        Dim integerParam2 As New IntegerParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        integerParam1.Name = "parameter1" 'Different parameter names!
        integerParam2.Name = "parameter2" 'Different parameter names!
        integerParam1.Value = 10 'Same values!
        integerParam2.Value = 10 'Same values!

        paramCollection1.InnerParameters.Add(integerParam1)
        paramCollection2.InnerParameters.Add(integerParam2)

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
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Integer) = 10)
        Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
        Assert.IsTrue(CType(metaDataCompareResults(1).NewValue, Integer) = 10)
        Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects. One of type integer and one of type Boolean."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithDifferentParameterTypes()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim integerParam1 As New IntegerParameter()
        Dim booleanParam1 As New BooleanParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        integerParam1.Name = "parameter1" 'Same parameter names!
        booleanParam1.Name = "parameter1" 'Same parameter names!
        integerParam1.Value = 10
        booleanParam1.Value = True

        paramCollection1.InnerParameters.Add(integerParam1)
        paramCollection2.InnerParameters.Add(booleanParam1)

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
        Assert.IsTrue(metaDataCompareResults(0).OldValue.StartsWith("Type mismatch"))
        Assert.IsTrue(metaDataCompareResults(0).NewValue.StartsWith("Type mismatch"))
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type integer. The number of parameters are different and also their values."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithDifferentIntegerParametersAndDifferentValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim integerParam1 As New IntegerParameter()
        Dim integerParam2 As New IntegerParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        integerParam1.Name = "parameter1" 'Different parameter names!
        integerParam2.Name = "parameter2" 'Different parameter names!
        integerParam1.Value = 10 'Different values!
        integerParam2.Value = 20 'Different values!

        paramCollection1.InnerParameters.Add(integerParam1)
        paramCollection2.InnerParameters.Add(integerParam2)

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
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Integer) = 10)
        Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
        Assert.IsTrue(CType(metaDataCompareResults(1).NewValue, Integer) = 20)
        Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type integer. Then number of parameters are the same but their values differ."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalIntegerParametersAndDifferentValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim integerParam1 As New IntegerParameter()
        Dim integerParam2 As New IntegerParameter()
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"
        integerParam1.Name = "parameter1"   'Same parameter names!
        integerParam2.Name = "parameter1"   'Same parameter names!
        integerParam1.Value = 10 'Different values!
        integerParam2.Value = 20 'Different values!

        paramCollection1.InnerParameters.Add(integerParam1)
        paramCollection2.InnerParameters.Add(integerParam2)

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
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Integer) = 10)
        Assert.IsTrue(CType(metaDataCompareResults(0).NewValue, Integer) = 20)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(0).PropertyName)
    End Sub

End Class
