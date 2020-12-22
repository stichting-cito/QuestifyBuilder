
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Logic.Service.Direct

<TestClass()>
Public Class ResourceHistoryCreatorTest

    <TestMethod>
    Public Sub CreateSimpleResourceHistoryEntity_ItemResourceEntity()
        DoCreateSimpleResourceHistoryEntity(Of ItemResourceEntity)()
    End Sub

    <TestMethod>
    Public Sub CreateSimpleResourceHistoryEntity_AssessmentTestResourceEntity()
        DoCreateSimpleResourceHistoryEntity(Of AssessmentTestResourceEntity)()
    End Sub

    <TestMethod>
    Public Sub CreateSimpleResourceHistoryEntity_DataSourceResourceEntity()
        DoCreateSimpleResourceHistoryEntity(Of DataSourceResourceEntity)()
    End Sub

    <TestMethod>
    Public Sub CreateSimpleResourceHistoryEntity_AspectResourceEntity()
        DoCreateSimpleResourceHistoryEntity(Of AspectResourceEntity)()
    End Sub

    <TestMethod>
    Public Sub CreateSimpleResourceHistoryEntity_GenericResourceEntity()
        DoCreateSimpleResourceHistoryEntity(Of GenericResourceEntity)()
    End Sub

    <TestMethod>
    Public Sub CreateSimpleResourceHistoryEntity_ItemLayoutTemplateResourceEntity()
        DoCreateSimpleResourceHistoryEntity(Of ItemLayoutTemplateResourceEntity)()
    End Sub

    <TestMethod>
    Public Sub CreateSimpleResourceHistoryEntity_ControlTemplateResourceEntity()
        DoCreateSimpleResourceHistoryEntity(Of ControlTemplateResourceEntity)()
    End Sub

    <TestMethod>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub CreateSimpleResourceHistoryEntity_CustomBankPropertyEntity()
        DoCreateSimpleResourceHistoryEntity(Of CustomBankPropertyEntity)()
    End Sub

    Private Sub DoCreateSimpleResourceHistoryEntity(Of T As {New})()
        Dim versionable As IVersionable = TryCast(New T(), IVersionable)

        If versionable Is Nothing Then
            Throw New ArgumentException("Must implement IVersionable")
        End If

        Dim propertyEntity As IPropertyEntity = CType(versionable, IPropertyEntity)

        propertyEntity.Version = "0.1"
        propertyEntity.ResourceData = New ResourceDataEntity()
        propertyEntity.ResourceData.IsDirty = False
        propertyEntity.ResourceData.IsNew = False
        propertyEntity.IsDirty = False
        propertyEntity.IsNew = False

        Dim resourceHistory As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(CType(propertyEntity, IVersionable), "remcor")
        Dim metaData = CType(SerializeHelper.XmlDeserializeFromByteArray(resourceHistory.MetaData, GetType(Versioning.MetaData)), Versioning.MetaData)

        Assert.IsNotNull(resourceHistory)
        Assert.IsFalse(propertyEntity.IsNew)
        Assert.IsFalse(propertyEntity.IsDirty)
        Assert.IsFalse(propertyEntity.ResourceData.IsNew)
        Assert.IsFalse(propertyEntity.ResourceData.IsDirty)
        Assert.IsTrue(resourceHistory.MetaData.Length > 0)
        Assert.IsTrue(metaData.DependentResourcesMetaData.Count = 0)
        Assert.IsTrue(metaData.ConceptStructureMetaData.Count = 0)
        Assert.IsTrue(metaData.CustomPropertiesMetaData.Count = 0)
        Assert.IsTrue(metaData.TreeStructureMetaData.Count = 0)
        Assert.IsTrue(metaData.PropertyEntityMetaData.Count >= 0)
        Assert.IsTrue(resourceHistory.BinData.Length = 0)
        Assert.IsFalse(propertyEntity.HasChangesInTopology())
    End Sub

End Class
