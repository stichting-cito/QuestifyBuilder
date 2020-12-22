
Imports System.Threading
Imports Cito.Tester.Common
Imports Enums
Imports HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.ResourceProperties
Imports Questify.Builder.Logic.Service.Direct
Imports Questify.Builder.Logic.Service.Interfaces
Imports SD.LLBLGen.Pro.ORMSupportClasses

<TestClass()>
Public Class SplitSqlQueryDecoratorTest
    Private fakeResourceService As FakeResourceServiceTestClass = Nothing
    Private sqlSplitQueryDecorator As SplitSqlQueryDecorator

    <TestInitialize()>
    Public Sub MyTestInitialize()
        fakeResourceService = New FakeResourceServiceTestClass
        sqlSplitQueryDecorator = New SplitSqlQueryDecorator(fakeResourceService)
    End Sub


    <TestMethod()>
    Public Sub ShouldReturnNoResources()

        Dim resources = sqlSplitQueryDecorator.GetResourcesByIdsWithOption(New List(Of Guid)(), New ResourceRequestDTO())

        Assert.AreEqual(0, resources.Count)
    End Sub

    <TestMethod()>
    Public Sub CanTheDecoratorSplitTheQuery()
        Dim resourceCount = 3000
        Dim times = CInt(IIf(resourceCount Mod SplitSqlQueryDecorator.MaxParametersPerQuery = 0, (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery), (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery) + 1))

        sqlSplitQueryDecorator.GetResourcesByIdsWithOption(GetListOfGuids(resourceCount), New ResourceRequestDTO())

        Assert.AreEqual(fakeResourceService.CallCount, times)
    End Sub

    <TestMethod()>
    Public Sub CanTheDecoratorSplitTheQuery1()

        sqlSplitQueryDecorator.GetResourcesByIdsWithOption(GetListOfGuids(1), New ResourceRequestDTO())

        Assert.AreEqual(fakeResourceService.CallCount, 1)
    End Sub

    <TestMethod()>
    Public Sub CanTheDecoratorSplitTheQuery2()
        Dim resourceCount = 1000
        Dim times = CInt(IIf(resourceCount Mod SplitSqlQueryDecorator.MaxParametersPerQuery = 0, (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery), (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery) + 1))

        sqlSplitQueryDecorator.GetResourcesByIdsWithOption(GetListOfGuids(resourceCount), New ResourceRequestDTO())

        Assert.AreEqual(fakeResourceService.CallCount, times)
    End Sub

    <TestMethod()>
    Public Sub CanTheDecoratorSplitTheQuery3()
        Dim resourceCount = 955
        Dim times = CInt(IIf(resourceCount Mod SplitSqlQueryDecorator.MaxParametersPerQuery = 0, (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery), (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery) + 1))

        sqlSplitQueryDecorator.GetResourcesByIdsWithOption(GetListOfGuids(resourceCount), New ResourceRequestDTO())

        Assert.AreEqual(fakeResourceService.CallCount, times)
    End Sub

    <TestMethod()>
    Public Sub CanTheDecoratorSplitTheQuery4()
        Dim resourceCount = 955
        Dim times = CInt(IIf(resourceCount Mod SplitSqlQueryDecorator.MaxParametersPerQuery = 0, (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery), (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery) + 1))

        sqlSplitQueryDecorator.GetResourcesByIdsWithOption(GetListOfGuids(resourceCount), New ResourceRequestDTO())

        Assert.AreEqual(fakeResourceService.CallCount, times)
    End Sub

    <TestMethod()>
    Public Sub CanGetResourcesFromDb()
        Dim resourceCount = 2000

        Dim result = sqlSplitQueryDecorator.GetResourcesByIdsWithOption(GetListOfGuids(resourceCount), New ResourceRequestDTO())

        Assert.AreEqual(resourceCount, result.Count)
    End Sub

    <TestMethod()>
    Public Sub CanGetResourcesFromDb1()
        Dim resourceCount = 1000
        Dim times = CInt(IIf(resourceCount Mod SplitSqlQueryDecorator.MaxParametersPerQuery = 0, (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery), (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery) + 1))

        Dim result = sqlSplitQueryDecorator.GetResourcesByIdsWithOption(GetListOfGuids(resourceCount), New ResourceRequestDTO())

        Assert.AreEqual(resourceCount, result.Count)
    End Sub

    <TestMethod()>
    Public Sub CanGetResourcesFromDb2()
        Dim resourceCount = 2000
        Dim times = CInt(IIf(resourceCount Mod SplitSqlQueryDecorator.MaxParametersPerQuery = 0, (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery), (resourceCount \ SplitSqlQueryDecorator.MaxParametersPerQuery) + 1))

        Dim result = sqlSplitQueryDecorator.GetResourcesByIdsWithOption(GetListOfGuids(resourceCount), New ResourceRequestDTO())

        For Each entity As ItemResourceEntity In result
            Assert.IsTrue(result.Contains(entity))
        Next
        Assert.AreEqual(resourceCount, result.Count)
    End Sub

    Private Function GetListOfGuids(ByVal i As Integer) As List(Of Guid)
        Dim ret = New List(Of Guid)
        For i = 1 To i
            ret.Add(Guid.NewGuid())
        Next
        Return ret
    End Function

    Public Class FakeResourceServiceTestClass
        Implements IResourceService

        Public CallCount As Integer = 0

        Public Function GetResourcePropertyValues(resourceEntity As ResourceEntity) As ResourcePropertyValueCollection Implements IResourceService.GetResourcePropertyValues
            Throw New NotImplementedException
        End Function

        Public Function GetResourcesByIdsWithOption(resourceIds As List(Of Guid), factory As IEntityFactory2, request As ResourceRequestDTO) As EntityCollection Implements IResourceService.GetResourcesByIdsWithOption
            Return GetResourcesByIdsWithOption(resourceIds, request)
        End Function

        Public Function GetResourceData(resource As ResourceEntity) As ResourceDataEntity Implements IResourceService.GetResourceData
            Throw New NotImplementedException
        End Function

        Public Function GetResourceDataByResourceId(resourceId As Guid) As ResourceDataEntity Implements IResourceService.GetResourceDataByResourceId
            Throw New NotImplementedException
        End Function

        Public Function GetResourceDataByResourceIds(resourceId As List(Of Guid)) As EntityCollection Implements IResourceService.GetResourceDataByResourceIds
            Throw New NotImplementedException
        End Function

        Public Function GetResourcesForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetResourcesForBank
            Throw New NotImplementedException
        End Function

        Public Function GetPauseItemsForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetPauseItemsForBank
            Throw New NotImplementedException
        End Function

        Public Function ResourceExists(bankId As Integer, resourceName As String, checkInHierarchy As Boolean) As Boolean Implements IResourceService.ResourceExists
            Throw New NotImplementedException
        End Function

        Public Function ResourceExists(bankId As Integer, resourceName As String, checkInHierarchy As Boolean, factory As IEntityFactory2) As Boolean Implements IResourceService.ResourceExists
            Throw New NotImplementedException
        End Function

        Public Function ResourceExists(bankId As Integer, resourceId As Guid, checkInHierarchy As Boolean) As Boolean Implements IResourceService.ResourceExists
            Throw New NotImplementedException
        End Function

        Public Function ResourceExists(bankId As Integer, resourceId As Guid, checkInHierarchy As Boolean, factory As IEntityFactory2) As Boolean Implements IResourceService.ResourceExists
            Throw New NotImplementedException
        End Function

        Public Function GetResourcesForBank(bankId As Integer, fetchCompleteBranch As Boolean) As EntityCollection Implements IResourceService.GetResourcesForBank
            Throw New NotImplementedException
        End Function

        Public Function GetResourcesForBank(bankId As Integer, factory As IEntityFactory2, fetchCompleteBranch As Boolean) As EntityCollection Implements IResourceService.GetResourcesForBank
            Throw New NotImplementedException
        End Function

        Public Function GetResourceHistoryForResource(resourceId As Guid) As EntityCollection Implements IResourceService.GetResourceHistoryForResource
            Throw New NotImplementedException
        End Function

        Public Function GetResourceHistory(resourceHistory As ResourceHistoryEntity) As ResourceHistoryEntity Implements IResourceService.GetResourceHistory
            Throw New NotImplementedException
        End Function

        Public Function GetItem(item As ItemResourceEntity, request As ResourceRequestDTO) As ItemResourceEntity Implements IResourceService.GetItem
            Throw New NotImplementedException
        End Function

        Public Function GetItemsForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetItemsForBank
            Throw New NotImplementedException
        End Function

        Public Function GetItemsForBankWithFullCustomProperties(bankId As Integer) As EntityCollection Implements IResourceService.GetItemsForBankWithFullCustomProperties
            Throw New NotImplementedException
        End Function

        Public Function GetItemsForBank(bankId As Integer, searchKeyWords As String, searchInBankProperties As Boolean, searchInItemText As Boolean, testContextResourceId As Guid, maxRecords As Integer) As EntityCollection Implements IResourceService.GetItemsForBank
            Throw New NotImplementedException
        End Function

        Public Function GetItemsForBank(bankId As Integer, bucket As RelationPredicateBucket) As EntityCollection Implements IResourceService.GetItemsForBank
            Throw New NotImplementedException
        End Function

        Public Function GetItemsByCodes(itemcodeList As List(Of String), bankId As Integer, request As ItemResourceRequestDTO) As ItemResourceEntityCollection Implements IResourceService.GetItemsByCodes
            Throw New NotImplementedException
        End Function

        Public Function GetTestsByCodes(testcodeList As List(Of String), bankId As Integer, withCustomProperties As Boolean) As AssessmentTestResourceEntityCollection Implements IResourceService.GetTestsByCodes
            Throw New NotImplementedException
        End Function

        Public Function GetAssessmentTest(test As AssessmentTestResourceEntity) As AssessmentTestResourceEntity Implements IResourceService.GetAssessmentTest
            Throw New NotImplementedException
        End Function

        Public Function GetAssessmentTestsForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetAssessmentTestsForBank
            Throw New NotImplementedException
        End Function

        Public Function GetPackagesForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetPackagesForBank
            Throw New NotImplementedException
        End Function

        Public Function GetTestPackagesForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetTestPackagesForBank
            Throw New NotImplementedException
        End Function

        Public Function GetTestPackage(testPackage As TestPackageResourceEntity) As TestPackageResourceEntity Implements IResourceService.GetTestPackage
            Throw New NotImplementedException
        End Function

        Public Function GetPackage(package As PackageResourceEntity) As PackageResourceEntity Implements IResourceService.GetPackage
            Throw New NotImplementedException
        End Function

        Public Function GetAspect(aspect As AspectResourceEntity) As AspectResourceEntity Implements IResourceService.GetAspect
            Throw New NotImplementedException
        End Function

        Public Function GetAspectsForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetAspectsForBank
            Throw New NotImplementedException
        End Function

        Public Function GetAssessmentTestTemplatesForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetAssessmentTestTemplatesForBank
            Throw New NotImplementedException
        End Function

        Public Function GetItemLayoutTemplate(itemLayoutTemplate As ItemLayoutTemplateResourceEntity) As ItemLayoutTemplateResourceEntity Implements IResourceService.GetItemLayoutTemplate
            Throw New NotImplementedException
        End Function

        Public Function GetItemLayoutTemplatesForBankWithItemTypeFilter(bankId As Integer, itemTypes As List(Of ItemTypeEnum), exclude As Boolean) As EntityCollection Implements IResourceService.GetItemLayoutTemplatesForBankWithItemTypeFilter
            Throw New NotImplementedException
        End Function

        Public Function GetItemLayoutTemplatesForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetItemLayoutTemplatesForBank
            Throw New NotImplementedException
        End Function

        Public Function GetItemLayoutTemplateSourceNamesForItemCodeList(bankId As Integer, itemCodes As List(Of String)) As List(Of String) Implements IResourceService.GetItemLayoutTemplateSourceNamesForItemCodeList
            Throw New NotImplementedException
        End Function

        Public Function GetControlTemplate(controlTemplate As ControlTemplateResourceEntity) As ControlTemplateResourceEntity Implements IResourceService.GetControlTemplate
            Throw New NotImplementedException
        End Function

        Public Function GetControlTemplatesForBank(bankId As Integer) As EntityCollection Implements IResourceService.GetControlTemplatesForBank
            Throw New NotImplementedException
        End Function

        Public Function GetGenericResource(genericResource As GenericResourceEntity) As GenericResourceEntity Implements IResourceService.GetGenericResource
            Throw New NotImplementedException
        End Function

        Public Function GetGenericResourceForBank(bankId As Integer, filter As String, filePrefix As String, templatesOnly As Boolean) As EntityCollection Implements IResourceService.GetGenericResourceForBank
            Throw New NotImplementedException
        End Function

        Public Function UpdateBankIdOfResourceEntitiesAndCustomBankProperties(bankIdValue As Integer, resourceIdsToUpdate As List(Of Guid), customBankPropertyIdsToUpdate As List(Of Guid)) As List(Of KeyValuePair(Of Guid, String)) Implements IResourceService.UpdateBankIdOfResourceEntitiesAndCustomBankProperties
            Throw New NotImplementedException
        End Function

        Public Function UpdateAspectResource(resource As AspectResourceEntity) As String Implements IResourceService.UpdateAspectResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateAspectResource(resource As AspectResourceEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateAspectResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateAssessmentTestResource(resource As AssessmentTestResourceEntity) As String Implements IResourceService.UpdateAssessmentTestResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateAssessmentTestResource(resource As AssessmentTestResourceEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateAssessmentTestResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateTestPackageResource(resource As TestPackageResourceEntity) As String Implements IResourceService.UpdateTestPackageResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateTestPackageResource(resource As TestPackageResourceEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateTestPackageResource
            Throw New NotImplementedException
        End Function

        Public Function UpdatePackageResource(resource As PackageResourceEntity) As String Implements IResourceService.UpdatePackageResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateItemResource(resource As ItemResourceEntity) As String Implements IResourceService.UpdateItemResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateItemResource(resource As ItemResourceEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateItemResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateItemResource(resource As ItemResourceEntity, refetch As Boolean, recurse As Boolean, saveResourceData As Boolean) As String Implements IResourceService.UpdateItemResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateItemResources(resource As IEnumerable(Of ItemResourceEntity)) As String Implements IResourceService.UpdateItemResources
            Throw New NotImplementedException
        End Function

        Public Function UpdateResourceHistory(resourceHistory As ResourceHistoryEntity) As String Implements IResourceService.UpdateResourceHistory
            Throw New NotImplementedException
        End Function

        Public Function UpdateResourceHistory(resourceHistory As ResourceHistoryEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateResourceHistory
            Throw New NotImplementedException
        End Function

        Public Function UpdateResourceVisibility(resourceId As Guid, setAtBankId As Integer, makeResourceVisible As Boolean) As String Implements IResourceService.UpdateResourceVisibility
            Throw New NotImplementedException
        End Function

        Public Function UpdateGenericResource(resource As GenericResourceEntity) As String Implements IResourceService.UpdateGenericResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateGenericResource(resource As GenericResourceEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateGenericResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateItemLayoutTemplateResource(resource As ItemLayoutTemplateResourceEntity) As String Implements IResourceService.UpdateItemLayoutTemplateResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateItemLayoutTemplateResource(resource As ItemLayoutTemplateResourceEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateItemLayoutTemplateResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateControlTemplateResource(resource As ControlTemplateResourceEntity) As String Implements IResourceService.UpdateControlTemplateResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateControlTemplateResource(resource As ControlTemplateResourceEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateControlTemplateResource
            Throw New NotImplementedException
        End Function

        Public Function DeleteResources(resourcesToDelete As EntityCollection, ByRef notDeletedResources As EntityCollection) As String Implements IResourceService.DeleteResources
            Throw New NotImplementedException
        End Function

        Public Function GetReferencesForResource(resource As ResourceEntity) As EntityCollection Implements IResourceService.GetReferencesForResource
            Throw New NotImplementedException
        End Function

        Public Function GetDependenciesForResource(resource As ResourceEntity) As EntityCollection Implements IResourceService.GetDependenciesForResource
            Throw New NotImplementedException
        End Function

        Public Function GetAvailableStates() As EntityCollection Implements IResourceService.GetAvailableStates
            Throw New NotImplementedException
        End Function

        Public Function GetState(stateId As Integer) As StateEntity Implements IResourceService.GetState
            Throw New NotImplementedException
        End Function

        Public Function GetStateAction(stateId As Integer, target As String) As ActionEntity Implements IResourceService.GetStateAction
            Throw New NotImplementedException
        End Function

        Public Function GetStateAction(bankId As Integer, resourceName As String, target As String) As ActionEntity Implements IResourceService.GetStateAction
            Throw New NotImplementedException
        End Function

        Public Function GetStateActions(bankId As Integer, resourceNames As String(), target As String) As IDictionary(Of String, ActionEntity) Implements IResourceService.GetStateActions
            Throw New NotImplementedException
        End Function

        Public Function ChangeCreatorModifier(currentUserId As Integer, newUserId As Integer) As Boolean Implements IResourceService.ChangeCreatorModifier
            Throw New NotImplementedException
        End Function

        Public Function GetDataSource(dataSource As DataSourceResourceEntity) As DataSourceResourceEntity Implements IResourceService.GetDataSource
            Throw New NotImplementedException
        End Function

        Public Function GetDataSourcesForBank(bankId As Integer, isTemplate As Boolean?, ParamArray behaviours As String()) As EntityCollection Implements IResourceService.GetDataSourcesForBank
            Throw New NotImplementedException
        End Function

        Public Function UpdateDataSourceResource(resource As DataSourceResourceEntity) As String Implements IResourceService.UpdateDataSourceResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateDataSourceResource(resource As DataSourceResourceEntity, refetch As Boolean, recurse As Boolean) As String Implements IResourceService.UpdateDataSourceResource
            Throw New NotImplementedException
        End Function

        Public Function UpdateItemResource(itemResource As ItemResourceEntity, refetch As Boolean, recurse As Boolean, saveResourceData As Boolean, skipStateCheck As Boolean) As String Implements IResourceService.UpdateItemResource
            Throw New NotImplementedException
        End Function

        Public Function GetControlTemplatesForBankList(bankId As Integer, checkCompleteBankHierarchy As Boolean) As Dictionary(Of String, Integer) Implements IResourceService.GetControlTemplatesForBankList
            Throw New NotImplementedException
        End Function

        Public Function GetItemLayoutTemplatesForBankList(bankId As Integer, checkCompleteBankHierarchy As Boolean) As Dictionary(Of String, Integer) Implements IResourceService.GetItemLayoutTemplatesForBankList
            Throw New NotImplementedException
        End Function

        Public Function GetItemLayoutTemplatesForBankWithListOfNamesFilter(bankId As Integer, iltNames As List(Of String)) As EntityCollection Implements IResourceService.GetItemLayoutTemplatesForBankWithListOfNamesFilter
            Throw New NotImplementedException
        End Function

        Public Function GetAssessmentTestsForBank(bankId As Integer, includeParentBanks As Boolean, includeChildBanks As Boolean, withCustomProperties As Boolean) As EntityCollection Implements IResourceService.GetAssessmentTestsForBank
            Throw New NotImplementedException()
        End Function

        Public Function GetItemLayoutTemplatesFromListOfResourceIds(resourceIds As IEnumerable(Of Guid), withDependencies As Boolean) As EntityCollection Implements IResourceService.GetItemLayoutTemplatesFromListOfResourceIds
            Throw New NotImplementedException()
        End Function

        Public Function GetResourceByNameWithOption(bankId As Integer, name As String, request As ResourceRequestDTO) As ResourceEntity Implements IResourceService.GetResourceByNameWithOption
            Throw New NotImplementedException()
        End Function

        Public Function GetResourceByIdWithOption(resourceId As Guid, request As ResourceRequestDTO) As ResourceEntity Implements IResourceService.GetResourceByIdWithOption
            Throw New NotImplementedException()
        End Function

        Public Function GetResourceByIdWithOption(resourceId As Guid, factory As IEntityFactory2, request As ResourceRequestDTO) As ResourceEntity Implements IResourceService.GetResourceByIdWithOption
            Throw New NotImplementedException
        End Function

        Public Function GetResourcesByIdsWithOption(resourceIds As List(Of Guid), request As ResourceRequestDTO) As EntityCollection Implements IResourceService.GetResourcesByIdsWithOption
            Dim resources As New EntityCollection(New ResourceEntityFactory)
            For Each resourceId In resourceIds
                Dim resource As New ItemResourceEntity(resourceId)
                resources.Add(resource)
            Next
            Interlocked.Increment(CallCount)
            Return resources
        End Function

        Public Function GetResourcesByNamesWithOption(bankId As Integer, names As List(Of String), request As ResourceRequestDTO) As EntityCollection Implements IResourceService.GetResourcesByNamesWithOption
            Throw New NotImplementedException()
        End Function
    End Class

End Class
