Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Versioning.Retrieval

Friend Class Helper

    Private Shared _now As DateTime

    Friend Shared Function CreateAssessmentTest(ByVal title As String) As AssessmentTest2
        Dim assessmentTest2 As New AssessmentTest2()

        assessmentTest2.Identifier = Guid.NewGuid().ToString()
        assessmentTest2.Title = title

        Return assessmentTest2
    End Function

    Friend Shared Function CreateAssessmentItem(ByVal title As String) As AssessmentItem
        Return CreateAssessmentItem(title, Nothing)
    End Function

    Friend Shared Function CreateAssessmentItem(ByVal title As String, ByVal iltName As String) As AssessmentItem
        Dim assessmentItem As New AssessmentItem()

        assessmentItem.Identifier = Guid.NewGuid().ToString()
        assessmentItem.Title = title
        assessmentItem.LayoutTemplateSourceName = iltName

        Return assessmentItem
    End Function

    Friend Shared Function CreateResourceEntity(ByVal propertyEntity As IPropertyEntity, resourceData As Object) As IVersionable
        propertyEntity.Id = Guid.NewGuid()
        propertyEntity.Name = "TestItem - Name"
        propertyEntity.Title = "TestItem - Title"
        propertyEntity.Description = "TestItem - Description"
        propertyEntity.StateId = 1
        propertyEntity.ModifiedDate = _now
        propertyEntity.ModifiedBy = 1
        propertyEntity.ResourceData = New ResourceDataEntity()

        If resourceData IsNot Nothing Then
            propertyEntity.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(resourceData)
        End If

        Return CType(propertyEntity, IVersionable)
    End Function

    Friend Shared Function CreateMetaData(ByVal propertyEntity As IPropertyEntity, ByVal userName As String) As Versioning.MetaData
        Dim metaData As New Versioning.MetaData(New CustomPropertiesRetriever(propertyEntity).CreateMetaData(),
                                     New ConceptStructureRetriever(propertyEntity).CreateMetaData(),
                                     New DependentResourcesRetriever(propertyEntity).CreateMetaData(),
                                     New PropertyEntityRetriever(propertyEntity, userName).CreateMetaData(),
                                     New TreeStructureRetriever(propertyEntity).CreateMetaData())

        Return metaData
    End Function

End Class
