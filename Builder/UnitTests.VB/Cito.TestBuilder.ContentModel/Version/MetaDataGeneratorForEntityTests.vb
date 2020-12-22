
Imports Questify.Builder.Model.ContentModel.EntityClasses.Workers
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq

<TestClass()>
Public Class MetaDataGeneratorForEntityTests

    <TestMethod>
    Public Sub GenerateMetadataFor_ItemResourceEntity_NoMetadataForResourceId()
        Dim item = CreateItem()
        Dim generator = New MetaDataGeneratorForEntity(item)

        Dim result = generator.GetEntitySpecificMetadata().ToList()

        Assert.IsFalse(result.Exists(Function(m)
                                         Return m.Name.Equals("resourceid", StringComparison.CurrentCultureIgnoreCase)
                                     End Function))
    End Sub

    <TestMethod>
    Public Sub GenerateMetadataFor_ItemResourceEntity_OnlyContainsMaxScoreAsMetaData()
        Dim item = CreateItem()
        Dim generator = New MetaDataGeneratorForEntity(item)

        Dim result = generator.GetEntitySpecificMetadata().ToList()

        Assert.AreEqual(1, result.Count)
        Assert.IsTrue(result.Exists(Function(m)
                                        Return m.Name.Equals("maxscore", StringComparison.CurrentCultureIgnoreCase)
                                    End Function))
    End Sub


    <TestMethod>
    Public Sub GenerateDefaultMetadataFor_DefaultItemResourceEntity_ExpectsOriginalResourceId_Equals_ResourceId()
        Dim item = New ItemResourceEntity(Guid.NewGuid())
        Dim generator = New MetaDataGeneratorForEntity(item)

        Dim result = generator.DefaultMetadata().ToList()

        Assert.AreEqual(1, result.Count)
        Assert.IsTrue(result.Exists(Function(m)
                                        Return m.Name.Equals("originalresourceid", StringComparison.CurrentCultureIgnoreCase)
                                    End Function))
        Assert.AreEqual(item.ResourceId.ToString("D"), result(0).Value)
    End Sub

    <TestMethod>
    Public Sub GenerateDefaultMetadataFor_ItemResourceEntity_ShouldContainVersionInfo()
        Dim item = CreateItem()
        Dim generator = New MetaDataGeneratorForEntity(item)

        Dim result = generator.DefaultMetadata().ToList()

        Assert.IsTrue(result.Exists(Function(m)
                                        Return m.Name.Equals("version", StringComparison.CurrentCultureIgnoreCase)
                                    End Function))
    End Sub

    Private Function CreateItem() As ItemResourceEntity
        Return New ItemResourceEntity(Guid.NewGuid()) With
                   {.Name = "Item Name", .MaxScore = 3, .ModifiedByUser = User(), .Version = "a", .MajorVersionLabel = "aa"}
    End Function

    Private Function User() As UserEntityFake
        Return New UserEntityFake(42) With
               {
                   .UserName = "Klaasje",
                   .Password = "plaintext",
                   .FullName = "Klaasje Klazen Klas"
            }
    End Function

End Class
