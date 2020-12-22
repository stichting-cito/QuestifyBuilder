using System;
using System.Collections.Generic;
using Cito.Tester.Common;
using Enums;
using HelperClasses;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.ResourceProperties;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IResourceService
    {



        ResourcePropertyValueCollection GetResourcePropertyValues(ResourceEntity resourceEntity);

        ResourceEntity GetResourceByNameWithOption(int bankId, string name, ResourceRequestDTO request);

        EntityCollection GetResourcesByNamesWithOption(int bankId, List<string> names, ResourceRequestDTO request);

        ResourceEntity GetResourceByIdWithOption(Guid resourceId, ResourceRequestDTO request);

        ResourceEntity GetResourceByIdWithOption(Guid resourceId, IEntityFactory2 factory, ResourceRequestDTO request);

        EntityCollection GetResourcesByIdsWithOption(List<Guid> resourceIds, ResourceRequestDTO request);

        EntityCollection GetResourcesByIdsWithOption(List<Guid> resourceIds, IEntityFactory2 factory, ResourceRequestDTO request);

        ResourceDataEntity GetResourceData(ResourceEntity resource);

        ResourceDataEntity GetResourceDataByResourceId(Guid resourceId);

        EntityCollection GetResourceDataByResourceIds(List<Guid> resourceId);

        EntityCollection GetResourcesForBank(int bankId);

        EntityCollection GetPauseItemsForBank(int bankId);

        bool ResourceExists(int bankId, string resourceName, bool checkInHierarchy);

        bool ResourceExists(int bankId, string resourceName, bool checkInHierarchy, IEntityFactory2 factory);

        bool ResourceExists(int bankId, Guid resourceId, bool checkInHierarchy);

        bool ResourceExists(int bankId, Guid resourceId, bool checkInHierarchy, IEntityFactory2 factory);

        EntityCollection GetResourcesForBank(int bankId, bool fetchCompleteBranch);

        EntityCollection GetResourcesForBank(int bankId, IEntityFactory2 factory, bool fetchCompleteBranch);

        EntityCollection GetResourceHistoryForResource(Guid resourceId);

        ResourceHistoryEntity GetResourceHistory(ResourceHistoryEntity resourceHistory);

        ItemResourceEntity GetItem(ItemResourceEntity item, ResourceRequestDTO request);

        EntityCollection GetItemsForBank(int bankId);

        EntityCollection GetItemsForBankWithFullCustomProperties(int bankId);

        EntityCollection GetItemsForBank(int bankId, string searchKeyWords, bool searchInBankProperties, bool searchInItemText, Guid testContextResourceId, int maxRecords);

        EntityCollection GetItemsForBank(int bankId, RelationPredicateBucket bucket);

        ItemResourceEntityCollection GetItemsByCodes(List<string> itemcodeList, int bankId, ItemResourceRequestDTO request);

        AssessmentTestResourceEntityCollection GetTestsByCodes(List<string> testcodeList, int bankId, bool withCustomProperties);

        AssessmentTestResourceEntity GetAssessmentTest(AssessmentTestResourceEntity test);

        EntityCollection GetAssessmentTestsForBank(int bankId);

        EntityCollection GetAssessmentTestsForBank(int bankId, bool includeParentBanks, bool includeChildBanks, bool withCustomProperties);

        EntityCollection GetPackagesForBank(int bankId);

        EntityCollection GetTestPackagesForBank(int bankId);

        TestPackageResourceEntity GetTestPackage(TestPackageResourceEntity testPackage);

        PackageResourceEntity GetPackage(PackageResourceEntity package);

        AspectResourceEntity GetAspect(AspectResourceEntity aspect);

        EntityCollection GetAspectsForBank(int bankId);

        EntityCollection GetAssessmentTestTemplatesForBank(int bankId);

        ItemLayoutTemplateResourceEntity GetItemLayoutTemplate(ItemLayoutTemplateResourceEntity itemLayoutTemplate);

        EntityCollection GetItemLayoutTemplatesForBankWithItemTypeFilter(int bankId, List<ItemTypeEnum> itemTypes, bool exclude);

        EntityCollection GetItemLayoutTemplatesForBankWithListOfNamesFilter(int bankId, List<string> iltNames);

        Dictionary<string, int> GetItemLayoutTemplatesForBankList(int bankId, bool checkCompleteBankHierarchy);

        Dictionary<string, int> GetControlTemplatesForBankList(int bankId, bool checkCompleteBankHierarchy);

        EntityCollection GetItemLayoutTemplatesForBank(int bankId);

        EntityCollection GetItemLayoutTemplatesFromListOfResourceIds(IEnumerable<Guid> resourceIds, bool withDependencies);

        List<string> GetItemLayoutTemplateSourceNamesForItemCodeList(int bankId, List<string> itemCodes);

        ControlTemplateResourceEntity GetControlTemplate(ControlTemplateResourceEntity controlTemplate);

        EntityCollection GetControlTemplatesForBank(int bankId);

        GenericResourceEntity GetGenericResource(GenericResourceEntity genericResource);

        EntityCollection GetGenericResourceForBank(int bankId, string filter, string filePrefix, bool templatesOnly);

        List<KeyValuePair<Guid, string>> UpdateBankIdOfResourceEntitiesAndCustomBankProperties(int bankIdValue, List<Guid> resourceIdsToUpdate, List<Guid> customBankPropertyIdsToUpdate);

        string UpdateAspectResource(AspectResourceEntity resource);

        string UpdateAspectResource(AspectResourceEntity resource, bool refetch, bool recurse);

        string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource);

        string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource, bool refetch, bool recurse);

        string UpdateTestPackageResource(TestPackageResourceEntity resource);

        string UpdateTestPackageResource(TestPackageResourceEntity resource, bool refetch, bool recurse);

        string UpdatePackageResource(PackageResourceEntity resource);

        string UpdateItemResource(ItemResourceEntity resource);

        string UpdateItemResource(ItemResourceEntity resource, bool refetch, bool recurse);

        string UpdateItemResource(ItemResourceEntity resource, bool refetch, bool recurse, bool saveResourceData);

        string UpdateItemResources(IEnumerable<ItemResourceEntity> resource);

        string UpdateResourceHistory(ResourceHistoryEntity resourceHistory);

        string UpdateResourceHistory(ResourceHistoryEntity resourceHistory, bool refetch, bool recurse);

        string UpdateResourceVisibility(Guid resourceId, int setAtBankId, bool makeResourceVisible);

        string UpdateGenericResource(GenericResourceEntity resource);

        string UpdateGenericResource(GenericResourceEntity resource, bool refetch, bool recurse);

        string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource);

        string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource, bool refetch, bool recurse);

        string UpdateControlTemplateResource(ControlTemplateResourceEntity resource);

        string UpdateControlTemplateResource(ControlTemplateResourceEntity resource, bool refetch, bool recurse);

        string DeleteResources(EntityCollection resourcesToDelete, ref EntityCollection notDeletedResources);

        EntityCollection GetReferencesForResource(ResourceEntity resource);

        EntityCollection GetDependenciesForResource(ResourceEntity resource);

        EntityCollection GetAvailableStates();

        StateEntity GetState(int stateId);

        ActionEntity GetStateAction(int stateId, string target);

        ActionEntity GetStateAction(int bankId, string resourceName, string target);

        IDictionary<string, ActionEntity> GetStateActions(int bankId, string[] resourceNames, string target);

        bool ChangeCreatorModifier(int currentUserId, int newUserId);

        DataSourceResourceEntity GetDataSource(DataSourceResourceEntity dataSource);

        EntityCollection GetDataSourcesForBank(int bankId, Nullable<bool> isTemplate, params string[] behaviours);

        string UpdateDataSourceResource(DataSourceResourceEntity resource);

        string UpdateDataSourceResource(DataSourceResourceEntity resource, bool refetch, bool recurse);

        string UpdateItemResource(ItemResourceEntity itemResource, bool refetch, bool recurse, bool saveResourceData, bool skipStateCheck);
    }
}
