
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Direct
Imports Versioning

<TestClass()>
Public Class AssessmentItemCollectionParameterComparerTest
    Inherits ComparerTestBase

    Private _iltName1 As String = "ilt.compare.collection1"
    Private _iltName2 As String = "ilt.compare.collection2"

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are identical and also the names of the integerParameters and the values of the integer parameters."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalCollectionParametersAndSameIntegerParametersWithSameValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim integerParameter1 As New IntegerParameter()
        Dim integerParameter2 As New IntegerParameter()
        integerParameter1.Name = "integerParameter1"
        integerParameter2.Name = "integerParameter1"
        integerParameter1.Value = 10
        integerParameter2.Value = 10

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1)
        integerParamCollection2.InnerParameters.Add(integerParameter2)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam1"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam1)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 0)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are identical and also the names of the integerParameters. The values of the integer parameter differ."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalCollectionParametersAndIdenticalIntegerParametersWithDifferentValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim integerParameter1 As New IntegerParameter()
        Dim integerParameter2 As New IntegerParameter()
        integerParameter1.Name = "integerParameter1"
        integerParameter2.Name = "integerParameter1"
        integerParameter1.Value = 10
        integerParameter2.Value = 20

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1)
        integerParamCollection2.InnerParameters.Add(integerParameter2)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam1"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam2)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Integer) = 10)
        Assert.IsTrue(CType(metaDataCompareResults(0).NewValue, Integer) = 20)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = integerParameter1.Name)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(0).PropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are identical and also the names of the integerParameters. The values of the integer parameter differ."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalCollectionParametersAndDifferentIntegerParametersWithIdenticalValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim integerParameter1 As New IntegerParameter()
        Dim integerParameter2 As New IntegerParameter()
        integerParameter1.Name = "integerParameter1"
        integerParameter2.Name = "integerParameter2"
        integerParameter1.Value = 10
        integerParameter2.Value = 10

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1)
        integerParamCollection2.InnerParameters.Add(integerParameter2)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam1"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam2)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 2)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(0).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = integerParameter1.Name)
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Integer) = 10)
        Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).PropertyName = integerParameter2.Name)
        Assert.IsTrue(CType(metaDataCompareResults(1).NewValue, Integer) = 10)
        Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName = metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are different but the names of the integerParameters are the same and also their values."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithDifferentCollectionParametersAndIdenticalIntegerParametersWithSameValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
        Dim integerParameter1 As New IntegerParameter()
        Dim integerParameter2 As New IntegerParameter()
        integerParameter1.Name = "integerParameter1"
        integerParameter2.Name = "integerParameter1"
        integerParameter1.Value = 10
        integerParameter2.Value = 10

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1)
        integerParamCollection2.InnerParameters.Add(integerParameter2)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam2"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam2)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 2)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(0).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = collectionParam1.Name)
        Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).NewValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).PropertyName = collectionParam2.Name)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName = metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are different but the names of the integerParameters are the same but their values differ."), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithDifferentCollectionParametersAndIdenticalIntegerParametersWithDifferentValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
        Dim integerParameter1 As New IntegerParameter()
        Dim integerParameter2 As New IntegerParameter()
        integerParameter1.Name = "integerParameter1"
        integerParameter2.Name = "integerParameter1"
        integerParameter1.Value = 10
        integerParameter2.Value = 20

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1)
        integerParamCollection2.InnerParameters.Add(integerParameter2)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam2"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam2)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 2)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(0).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = collectionParam1.Name)
        Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).NewValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).PropertyName = collectionParam2.Name)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName = metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are identical but the number of integerParameters are different and two values are the same. The first collection contains one and the seconds contains two integer parameters"), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalCollectionParametersAndDifferentNumberOfIntegerParametersWithDifferentValues1()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim integerParameter1_1 As New IntegerParameter()
        Dim integerParameter2_1 As New IntegerParameter()
        Dim integerParameter2_2 As New IntegerParameter()
        integerParameter1_1.Name = "integerParameter1"
        integerParameter2_1.Name = "integerParameter2"
        integerParameter2_2.Name = "integerParameter1"
        integerParameter1_1.Value = 10
        integerParameter2_1.Value = 20
        integerParameter2_2.Value = 10

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1_1)
        integerParamCollection2.InnerParameters.Add(integerParameter2_1)
        integerParamCollection2.InnerParameters.Add(integerParameter2_2)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam1"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam2)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(CType(metaDataCompareResults(0).NewValue, Integer) = 20)
        Assert.IsTrue(metaDataCompareResults(0).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = integerParameter2_1.Name)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(0).PropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are identical but the number of integerParameters are different and two values are the same. The first collection contains one and the seconds contains two integer parameters"), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalCollectionParametersAndDifferentNumberOfIntegerParametersWithDifferentValues2()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim integerParameter1_1 As New IntegerParameter()
        Dim integerParameter2_1 As New IntegerParameter()
        Dim integerParameter2_2 As New IntegerParameter()
        integerParameter1_1.Name = "integerParameter1"
        integerParameter2_1.Name = "integerParameter2"
        integerParameter2_2.Name = "integerParameter1"
        integerParameter1_1.Value = 10
        integerParameter2_1.Value = 20
        integerParameter2_2.Value = 30

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1_1)
        integerParamCollection2.InnerParameters.Add(integerParameter2_1)
        integerParamCollection2.InnerParameters.Add(integerParameter2_2)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam1"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam2)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 2)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(0).PropertyName)
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Integer) = 10)
        Assert.IsTrue(CType(metaDataCompareResults(0).NewValue, Integer) = 30)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = integerParameter1_1.Name)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = integerParameter2_2.Name)
        Assert.IsTrue(CType(metaDataCompareResults(1).NewValue, Integer) = 20)
        Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).PropertyName = integerParameter2_1.Name)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName = metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are identical but the number of integerParameters are different and two values are the same. The first collection contains two and the seconds contains one integer parameter"), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalCollectionParametersAndDifferentNumberOfIntegerParametersWithDifferentValues3()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim integerParameter1_1 As New IntegerParameter()
        Dim integerParameter1_2 As New IntegerParameter()
        Dim integerParameter2_1 As New IntegerParameter()
        integerParameter1_1.Name = "integerParameter1"
        integerParameter1_2.Name = "integerParameter2"
        integerParameter2_1.Name = "integerParameter1"
        integerParameter1_1.Value = 10
        integerParameter1_2.Value = 20
        integerParameter2_1.Value = 10

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1_1)
        integerParamCollection1.InnerParameters.Add(integerParameter1_2)
        integerParamCollection2.InnerParameters.Add(integerParameter2_1)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam1"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam2)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Integer) = 20)
        Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = integerParameter1_2.Name)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(0).PropertyName)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type collection parameter. The names of the collectionparameters are identical but the number of integerParameters are different and also their values. The first collection contains two and the seconds contains one integer parameter"), TestCategory("Versioning"), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticalCollectionParametersAndDifferentNumberOfIntegerParametersWithDifferentValues4()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim integerParameter1_1 As New IntegerParameter()
        Dim integerParameter1_2 As New IntegerParameter()
        Dim integerParameter2_1 As New IntegerParameter()
        integerParameter1_1.Name = "integerParameter1"
        integerParameter1_2.Name = "integerParameter2"
        integerParameter2_1.Name = "integerParameter1"
        integerParameter1_1.Value = 10
        integerParameter1_2.Value = 20
        integerParameter2_1.Value = 30

        Dim integerParamCollection1 As New ParameterCollection()
        Dim integerParamCollection2 As New ParameterCollection()
        integerParamCollection1.InnerParameters.Add(integerParameter1_1)
        integerParamCollection1.InnerParameters.Add(integerParameter1_2)
        integerParamCollection2.InnerParameters.Add(integerParameter2_1)

        Dim integerParamSet1 As New ParameterSetCollection()
        Dim integerParamSet2 As New ParameterSetCollection()

        integerParamSet1.Add(integerParamCollection1)
        integerParamSet2.Add(integerParamCollection2)

        Dim collectionParam1 As New CollectionParameter()
        Dim collectionParam2 As New CollectionParameter()
        collectionParam1.Name = "collectionParam1"
        collectionParam2.Name = "collectionParam1"
        collectionParam1.Value = integerParamSet1
        collectionParam2.Value = integerParamSet2
        collectionParam1.BluePrint = New ParameterCollection()
        collectionParam2.BluePrint = New ParameterCollection()

        Dim parameterCollection1 As ParameterCollection = New ParameterCollection()
        Dim parameterCollection2 As ParameterCollection = New ParameterCollection()
        parameterCollection1.Id = "invoer"
        parameterCollection2.Id = "invoer"
        parameterCollection1.InnerParameters.Add(collectionParam1)
        parameterCollection2.InnerParameters.Add(collectionParam2)

        assessmentItem1.Parameters.Add(parameterCollection1)
        assessmentItem2.Parameters.Add(parameterCollection2)

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
        Assert.IsTrue(metaDataCompareResults.Count = 2)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName = metaDataCompareResults(0).PropertyName)
        Assert.IsTrue(CType(metaDataCompareResults(0).OldValue, Integer) = 10)
        Assert.IsTrue(CType(metaDataCompareResults(0).NewValue, Integer) = 30)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = integerParameter1_1.Name)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = integerParameter2_1.Name)
        Assert.IsTrue(CType(metaDataCompareResults(1).OldValue, Integer) = 20)
        Assert.IsTrue(metaDataCompareResults(1).NewValue = String.Empty)
        Assert.IsTrue(metaDataCompareResults(1).PropertyName = integerParameter1_2.Name)
        Assert.IsTrue(metaDataCompareResults(1).LocalizedPropertyName = metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName <> metaDataCompareResults(1).PropertyName)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(1).LocalizedPropertyName)
    End Sub

End Class
