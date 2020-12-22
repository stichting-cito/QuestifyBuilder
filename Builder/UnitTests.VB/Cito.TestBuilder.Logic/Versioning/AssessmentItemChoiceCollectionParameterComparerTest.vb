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
Public Class AssessmentItemChoiceCollectionParameterComparerTest
	Inherits ComparerTestBase

	Private _iltName1 As String = "ilt.compare.choiceCollection1"
	Private _iltName2 As String = "ilt.compare.choiceCollection2"

	<TestMethod(), Owner("remcor"), Description("Compare two AssessmentItem objects of type choice collection parameter. The values of the three choices are the same."), TestCategory("Logic")>
	Public Sub TestTwoAssessmentItemsWithIdenticalChoiceCollectionParametersAndSameValues()
		Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
		Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
		Dim simpleChoice1_1 As New SimpleChoice()
		Dim simpleChoice1_2 As New SimpleChoice()
		Dim simpleChoice1_3 As New SimpleChoice()
		Dim simpleChoice2_1 As New SimpleChoice()
		Dim simpleChoice2_2 As New SimpleChoice()
		Dim simpleChoice2_3 As New SimpleChoice()
		simpleChoice1_1.Identifier = "ChoiceId1"
		simpleChoice1_2.Identifier = "ChoiceId2"
		simpleChoice1_3.Identifier = "ChoiceId3"
		simpleChoice2_1.Identifier = "ChoiceId1"
		simpleChoice2_2.Identifier = "ChoiceId2"
		simpleChoice2_3.Identifier = "ChoiceId3"
		simpleChoice1_1.Value = "Choice1"
		simpleChoice1_2.Value = "Choice2"
		simpleChoice1_3.Value = "Choice3"
		simpleChoice2_1.Value = "Choice1"
		simpleChoice2_2.Value = "Choice2"
		simpleChoice2_3.Value = "Choice3"

		Dim choiceCollectionParam1 As New ChoiceCollectionParameter()
		Dim choiceCollectionParam2 As New ChoiceCollectionParameter()
		choiceCollectionParam1.Name = "choiceCollectionParam1"
		choiceCollectionParam2.Name = "choiceCollectionParam1"
		choiceCollectionParam1.Choices.Add(simpleChoice1_1)
		choiceCollectionParam1.Choices.Add(simpleChoice1_2)
		choiceCollectionParam1.Choices.Add(simpleChoice1_3)
		choiceCollectionParam2.Choices.Add(simpleChoice2_1)
		choiceCollectionParam2.Choices.Add(simpleChoice2_2)
		choiceCollectionParam2.Choices.Add(simpleChoice2_3)

		Dim paramCollection1 As ParameterCollection = New ParameterCollection()
		Dim paramCollection2 As ParameterCollection = New ParameterCollection()
		paramCollection1.Id = "invoer" 'Must match the name of the ParameterCollection in the ILT
		paramCollection2.Id = "invoer" 'Must match the name of the ParameterCollection in the ILT
		paramCollection1.InnerParameters.Add(choiceCollectionParam1)
		paramCollection2.InnerParameters.Add(choiceCollectionParam2)

		assessmentItem1.Parameters.Add(paramCollection1)
		assessmentItem2.Parameters.Add(paramCollection2)

		'Arrange
		Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
		Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

		'Act
		Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "remcor")
		Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "remcor")
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

	<TestMethod(), Owner("remcor"), Description("Compare two AssessmentItem objects of type choice collection parameter. The first collection has two simple choices and the second collection has three."), TestCategory("Logic")>
	Public Sub TestTwoAssessmentItemsWithDifferentNumberOfChoiceCollectionParameters1()
		Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
		Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
		Dim simpleChoice1_1 As New SimpleChoice()
		Dim simpleChoice1_2 As New SimpleChoice()
		Dim simpleChoice2_1 As New SimpleChoice()
		Dim simpleChoice2_2 As New SimpleChoice()
		Dim simpleChoice2_3 As New SimpleChoice()
		simpleChoice1_1.Identifier = "ChoiceId1"
		simpleChoice1_2.Identifier = "ChoiceId2"
		simpleChoice2_1.Identifier = "ChoiceId1"
		simpleChoice2_2.Identifier = "ChoiceId2"
		simpleChoice2_3.Identifier = "ChoiceId3"
		simpleChoice1_1.Value = "Choice1"
		simpleChoice1_2.Value = "Choice2"
		simpleChoice2_1.Value = "Choice1"
		simpleChoice2_2.Value = "Choice2"
		simpleChoice2_3.Value = "Choice3"

		Dim choiceCollectionParam1 As New ChoiceCollectionParameter()
		Dim choiceCollectionParam2 As New ChoiceCollectionParameter()
		choiceCollectionParam1.Name = "choiceCollectionParam1"
		choiceCollectionParam2.Name = "choiceCollectionParam1"
		choiceCollectionParam1.Choices.Add(simpleChoice1_1)
		choiceCollectionParam1.Choices.Add(simpleChoice1_2)
		choiceCollectionParam2.Choices.Add(simpleChoice2_1)
		choiceCollectionParam2.Choices.Add(simpleChoice2_2)
		choiceCollectionParam2.Choices.Add(simpleChoice2_3)

		Dim paramCollection1 As ParameterCollection = New ParameterCollection()
		Dim paramCollection2 As ParameterCollection = New ParameterCollection()
		paramCollection1.Id = "invoer" 'Must match the name of the ParameterCollection in the ILT
		paramCollection2.Id = "invoer" 'Must match the name of the ParameterCollection in the ILT
		paramCollection1.InnerParameters.Add(choiceCollectionParam1)
		paramCollection2.InnerParameters.Add(choiceCollectionParam2)

		assessmentItem1.Parameters.Add(paramCollection1)
		assessmentItem2.Parameters.Add(paramCollection2)

		'Arrange
		Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
		Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

		'Act
		Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "remcor")
		Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "remcor")
		Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
		metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

		'Assert
		Assert.IsNotNull(resourceHistoryEntity1.MetaData)
		Assert.IsNotNull(resourceHistoryEntity2.MetaData)
		Assert.IsNotNull(resourceHistoryEntity1.BinData)
		Assert.IsNotNull(resourceHistoryEntity2.BinData)
		Assert.IsNotNull(metaDataCompareResults)
		Assert.IsTrue(metaDataCompareResults.Count = 1)
		Assert.IsTrue(metaDataCompareResults(0).PropertyName = simpleChoice2_3.Identifier)
		Assert.IsTrue(metaDataCompareResults(0).OldValue = simpleChoice2_3.Value)
		Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
	End Sub

	<TestMethod(), Owner("remcor"), Description("Compare two AssessmentItem objects of type choice collection parameter. The first collection has three simple choices and the second collection has two."), TestCategory("Logic")>
	Public Sub TestTwoAssessmentItemsWithDifferentNumberOfChoiceCollectionParameters2()
		Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
		Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
		Dim simpleChoice1_1 As New SimpleChoice()
		Dim simpleChoice1_2 As New SimpleChoice()
		Dim simpleChoice1_3 As New SimpleChoice()
		Dim simpleChoice2_1 As New SimpleChoice()
		Dim simpleChoice2_2 As New SimpleChoice()
		simpleChoice1_1.Identifier = "ChoiceId1"
		simpleChoice1_2.Identifier = "ChoiceId2"
		simpleChoice1_3.Identifier = "ChoiceId3"
		simpleChoice2_1.Identifier = "ChoiceId1"
		simpleChoice2_2.Identifier = "ChoiceId2"
		simpleChoice1_1.Value = "Choice1"
		simpleChoice1_2.Value = "Choice2"
		simpleChoice1_3.Value = "Choice3"
		simpleChoice2_1.Value = "Choice1"
		simpleChoice2_2.Value = "Choice2"

		Dim choiceCollectionParam1 As New ChoiceCollectionParameter()
		Dim choiceCollectionParam2 As New ChoiceCollectionParameter()
		choiceCollectionParam1.Name = "choiceCollectionParam1"
		choiceCollectionParam2.Name = "choiceCollectionParam1"
		choiceCollectionParam1.Choices.Add(simpleChoice1_1)
		choiceCollectionParam1.Choices.Add(simpleChoice1_2)
		choiceCollectionParam1.Choices.Add(simpleChoice1_3)
		choiceCollectionParam2.Choices.Add(simpleChoice2_1)
		choiceCollectionParam2.Choices.Add(simpleChoice2_2)

		Dim paramCollection1 As ParameterCollection = New ParameterCollection()
		Dim paramCollection2 As ParameterCollection = New ParameterCollection()
		paramCollection1.Id = "invoer" 'Must match the name of the ParameterCollection in the ILT
		paramCollection2.Id = "invoer" 'Must match the name of the ParameterCollection in the ILT
		paramCollection1.InnerParameters.Add(choiceCollectionParam1)
		paramCollection2.InnerParameters.Add(choiceCollectionParam2)

		assessmentItem1.Parameters.Add(paramCollection1)
		assessmentItem2.Parameters.Add(paramCollection2)

		'Arrange
		Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
		Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

		'Act
		Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "remcor")
		Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "remcor")
		Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
		metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

		'Assert
		Assert.IsNotNull(resourceHistoryEntity1.MetaData)
		Assert.IsNotNull(resourceHistoryEntity2.MetaData)
		Assert.IsNotNull(resourceHistoryEntity1.BinData)
		Assert.IsNotNull(resourceHistoryEntity2.BinData)
		Assert.IsNotNull(metaDataCompareResults)
		Assert.IsTrue(metaDataCompareResults.Count = 1)
		Assert.IsTrue(metaDataCompareResults(0).PropertyName = simpleChoice1_3.Identifier)
		Assert.IsTrue(metaDataCompareResults(0).OldValue = simpleChoice1_3.Value)
		Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
	End Sub

	<TestMethod(), Owner("remcor"), Description("Compare two AssessmentItem objects of type choice collection parameter. Both are the same except for the names of the choice collection parameters."), TestCategory("Logic")>
	Public Sub TestTwoAssessmentItemsWithDifferentChoiceCollectionNamesAndIdenticalChoices()
		Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
		Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName2)
		Dim simpleChoice1_1 As New SimpleChoice()
		Dim simpleChoice1_2 As New SimpleChoice()
		Dim simpleChoice1_3 As New SimpleChoice()
		Dim simpleChoice2_1 As New SimpleChoice()
		Dim simpleChoice2_2 As New SimpleChoice()
		Dim simpleChoice2_3 As New SimpleChoice()
		simpleChoice1_1.Identifier = "ChoiceId1"
		simpleChoice1_2.Identifier = "ChoiceId2"
		simpleChoice1_3.Identifier = "ChoiceId3"
		simpleChoice2_1.Identifier = "ChoiceId1"
		simpleChoice2_2.Identifier = "ChoiceId2"
		simpleChoice2_3.Identifier = "ChoiceId3"
		simpleChoice1_1.Value = "Choice1"
		simpleChoice1_2.Value = "Choice2"
		simpleChoice1_3.Value = "Choice3"
		simpleChoice2_1.Value = "Choice1"
		simpleChoice2_2.Value = "Choice2"
		simpleChoice2_3.Value = "Choice3"

		Dim choiceCollectionParam1 As New ChoiceCollectionParameter()
		Dim choiceCollectionParam2 As New ChoiceCollectionParameter()
		choiceCollectionParam1.Name = "choiceCollectionParam1"
		choiceCollectionParam2.Name = "choiceCollectionParam2"
		choiceCollectionParam1.Choices.Add(simpleChoice1_1)
		choiceCollectionParam1.Choices.Add(simpleChoice1_2)
		choiceCollectionParam1.Choices.Add(simpleChoice1_3)
		choiceCollectionParam2.Choices.Add(simpleChoice2_1)
		choiceCollectionParam2.Choices.Add(simpleChoice2_2)
		choiceCollectionParam2.Choices.Add(simpleChoice2_3)

		Dim paramCollection1 As ParameterCollection = New ParameterCollection()
		Dim paramCollection2 As ParameterCollection = New ParameterCollection()
		paramCollection1.Id = "invoer" 'Must match the name of the ParameterCollection in the ILT
		paramCollection2.Id = "invoer" 'Must match the name of the ParameterCollection in the ILT
		paramCollection1.InnerParameters.Add(choiceCollectionParam1)
		paramCollection2.InnerParameters.Add(choiceCollectionParam2)

		assessmentItem1.Parameters.Add(paramCollection1)
		assessmentItem2.Parameters.Add(paramCollection2)

		'Arrange
		Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
		Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

		'Act
		Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "remcor")
		Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "remcor")
		Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
		metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

		'Assert
		Assert.IsNotNull(resourceHistoryEntity1.MetaData)
		Assert.IsNotNull(resourceHistoryEntity2.MetaData)
		Assert.IsNotNull(resourceHistoryEntity1.BinData)
		Assert.IsNotNull(resourceHistoryEntity2.BinData)
		Assert.IsNotNull(metaDataCompareResults)
		Assert.IsTrue(metaDataCompareResults.Count = 2)
		Assert.IsTrue(metaDataCompareResults(0).PropertyName = choiceCollectionParam1.Name)
		Assert.IsTrue(metaDataCompareResults(0).OldValue = String.Empty)
		Assert.IsTrue(metaDataCompareResults(0).NewValue = String.Empty)
		Assert.IsTrue(metaDataCompareResults(1).PropertyName = choiceCollectionParam2.Name)
		Assert.IsTrue(metaDataCompareResults(1).OldValue = String.Empty)
		Assert.IsTrue(metaDataCompareResults(1).NewValue = String.Empty)
	End Sub

End Class
