
Imports Questify.Builder.Model.ContentModel.EntityClasses.Workers
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq

<TestClass()>
Public Class MetaDataGeneratorForEntityTests

	<TestMethod>
	Public Sub GenerateMetadataFor_ItemResourceEntity_NoMetadataForResourceId()
		'Arrange
		Dim item = CreateItem()
		Dim generator = New MetaDataGeneratorForEntity(item)
		
	    'Act
		Dim result = generator.GetEntitySpecificMetadata().ToList()
		
	    'Assert
		Assert.IsFalse(result.Exists(Function(m)
										 Return m.Name.Equals("resourceid", StringComparison.CurrentCultureIgnoreCase)
									 End Function))
	End Sub

	<TestMethod>
	Public Sub GenerateMetadataFor_ItemResourceEntity_OnlyContainsMaxScoreAsMetaData()
		'Arrange
		Dim item = CreateItem()
		Dim generator = New MetaDataGeneratorForEntity(item)
		
	    'Act
		Dim result = generator.GetEntitySpecificMetadata().ToList()
		
	    'Assert
		Assert.AreEqual(1, result.Count)
		Assert.IsTrue(result.Exists(Function(m)
										Return m.Name.Equals("maxscore", StringComparison.CurrentCultureIgnoreCase)
									End Function))
	End Sub


	<TestMethod>
	Public Sub GenerateDefaultMetadataFor_DefaultItemResourceEntity_ExpectsOriginalResourceId_Equals_ResourceId()
		'Arrange
		Dim item = New ItemResourceEntity(Guid.NewGuid())
		Dim generator = New MetaDataGeneratorForEntity(item)
		
	    'Act
		Dim result = generator.DefaultMetadata().ToList()
		
	    'Assert
		Assert.AreEqual(1, result.Count)
		Assert.IsTrue(result.Exists(Function(m)
										Return m.Name.Equals("originalresourceid", StringComparison.CurrentCultureIgnoreCase)
									End Function))
		Assert.AreEqual(item.ResourceId.ToString("D"), result(0).Value)
	End Sub

	<TestMethod>
	Public Sub GenerateDefaultMetadataFor_ItemResourceEntity_ShouldContainVersionInfo()
		'Arrange
		Dim item = CreateItem()
		Dim generator = New MetaDataGeneratorForEntity(item)
		
	    'Act
		Dim result = generator.DefaultMetadata().ToList()
		
	    'Assert
		Assert.IsTrue(result.Exists(Function(m)
										Return m.Name.Equals("version", StringComparison.CurrentCultureIgnoreCase)
									End Function))
	End Sub
       
	Private Function CreateItem() As ItemResourceEntity
		Return New ItemResourceEntity(Guid.NewGuid()) With
				   {.Name = "Item Name", .MaxScore = 3, .ModifiedByUser = User(), .Version = "a", .MajorVersionLabel = "aa"}
	End Function

	''' <summary>
	''' Create and return UserEntity
	''' </summary>
	''' <returns></returns>
	''' <remarks>
	'''     The UserEntity validator use database connection and this unit test fails. 
    '''     Using UserEntityFake and UserValidatorFake instead in order to evoid a call to the database.
	''' </remarks>
	Private Function User() As UserEntityFake
		Return New UserEntityFake(42) With
			   {
				   .UserName = "Klaasje",
				   .Password = "plaintext",
				   .FullName = "Klaasje Klazen Klas"
			}
	End Function

End Class
