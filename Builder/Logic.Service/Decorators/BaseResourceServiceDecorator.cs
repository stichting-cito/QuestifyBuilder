using System;
using System.Collections.Generic;
using Cito.Tester.Common;
using Enums;
using HelperClasses;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.Logic.Service.Decorators
{

    public abstract class BaseResourceServiceDecorator : IResourceService
    {

        private IResourceService decoree;

        public BaseResourceServiceDecorator(IResourceService decoree)
        {
            this.decoree = decoree;
        }

        public virtual Questify.Builder.Model.ContentModel.ResourceProperties.ResourcePropertyValueCollection GetResourcePropertyValues(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity resourceEntity)
        {
            return decoree.GetResourcePropertyValues(resourceEntity);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ResourceDataEntity GetResourceData(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity resource)
        {
            return decoree.GetResourceData(resource);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ResourceDataEntity GetResourceDataByResourceId(System.Guid resourceId)
        {
            return decoree.GetResourceDataByResourceId(resourceId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetResourceDataByResourceIds(System.Collections.Generic.List<System.Guid> resourceId)
        {
            return decoree.GetResourceDataByResourceIds(resourceId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetResourcesForBank(System.Int32 bankId)
        {
            return decoree.GetResourcesForBank(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetPauseItemsForBank(System.Int32 bankId)
        {
            return decoree.GetPauseItemsForBank(bankId);
        }

        public virtual System.Boolean ResourceExists(System.Int32 bankId, System.String resourceName, bool checkInHierarchy, IEntityFactory2 factory)
        {
            return decoree.ResourceExists(bankId, resourceName, checkInHierarchy, factory);
        }

        public virtual System.Boolean ResourceExists(System.Int32 bankId, System.String resourceName, bool checkInHierarchy)
        {
            return decoree.ResourceExists(bankId, resourceName, checkInHierarchy);
        }

        public virtual System.Boolean ResourceExists(System.Int32 bankId, System.Guid resourceId, bool checkInHierarchy, IEntityFactory2 factory)
        {
            return decoree.ResourceExists(bankId, resourceId, checkInHierarchy, factory);
        }

        public virtual System.Boolean ResourceExists(System.Int32 bankId, System.Guid resourceId, bool checkInHierarchy)
        {
            return decoree.ResourceExists(bankId, resourceId, checkInHierarchy);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetResourcesForBank(System.Int32 bankId, System.Boolean fetchCompleteBranch)
        {
            return decoree.GetResourcesForBank(bankId, fetchCompleteBranch);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetResourcesForBank(System.Int32 bankId, SD.LLBLGen.Pro.ORMSupportClasses.IEntityFactory2 factory, System.Boolean fetchCompleteBranch)
        {
            return decoree.GetResourcesForBank(bankId, factory, fetchCompleteBranch);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetResourceHistoryForResource(System.Guid resourceId)
        {
            return decoree.GetResourceHistoryForResource(resourceId);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ResourceHistoryEntity GetResourceHistory(Questify.Builder.Model.ContentModel.EntityClasses.ResourceHistoryEntity resourceHistory)
        {
            return decoree.GetResourceHistory(resourceHistory);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity GetItem(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity item, ResourceRequestDTO request)
        {
            return decoree.GetItem(item, request);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetItemsForBank(System.Int32 bankId)
        {
            return decoree.GetItemsForBank(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetItemsForBankWithFullCustomProperties(System.Int32 bankId)
        {
            return decoree.GetItemsForBankWithFullCustomProperties(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetItemsForBank(System.Int32 bankId, System.String searchKeyWords, System.Boolean searchInBankProperties, System.Boolean searchInItemText, System.Guid testContextResourceId, System.Int32 maxRecords)
        {
            return decoree.GetItemsForBank(bankId, searchKeyWords, searchInBankProperties, searchInItemText, testContextResourceId, maxRecords);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetItemsForBank(System.Int32 bankId, SD.LLBLGen.Pro.ORMSupportClasses.RelationPredicateBucket bucket)
        {
            return decoree.GetItemsForBank(bankId, bucket);
        }

        public virtual AssessmentTestResourceEntityCollection GetTestsByCodes(System.Collections.Generic.List<System.String> testcodeList, System.Int32 bankId, System.Boolean withCustomProperties)
        {
            return decoree.GetTestsByCodes(testcodeList, bankId, withCustomProperties);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.AssessmentTestResourceEntity GetAssessmentTest(Questify.Builder.Model.ContentModel.EntityClasses.AssessmentTestResourceEntity test)
        {
            return decoree.GetAssessmentTest(test);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetAssessmentTestsForBank(System.Int32 bankId)
        {
            return decoree.GetAssessmentTestsForBank(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetAssessmentTestsForBank(int bankId, bool includeParentBanks, bool includeChildBanks, bool withCustomProperties)
        {
            return decoree.GetAssessmentTestsForBank(bankId, includeParentBanks, includeChildBanks, withCustomProperties);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetPackagesForBank(System.Int32 bankId)
        {
            return decoree.GetPackagesForBank(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetTestPackagesForBank(System.Int32 bankId)
        {
            return decoree.GetTestPackagesForBank(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.TestPackageResourceEntity GetTestPackage(Questify.Builder.Model.ContentModel.EntityClasses.TestPackageResourceEntity testPackage)
        {
            return decoree.GetTestPackage(testPackage);
        }


        public virtual Questify.Builder.Model.ContentModel.EntityClasses.PackageResourceEntity GetPackage(Questify.Builder.Model.ContentModel.EntityClasses.PackageResourceEntity package)
        {
            return decoree.GetPackage(package);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity GetAspect(Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity aspect)
        {
            return decoree.GetAspect(aspect);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetAspectsForBank(System.Int32 bankId)
        {
            return decoree.GetAspectsForBank(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetAssessmentTestTemplatesForBank(System.Int32 bankId)
        {
            return decoree.GetAssessmentTestTemplatesForBank(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity GetItemLayoutTemplate(Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity itemLayoutTemplate)
        {
            return decoree.GetItemLayoutTemplate(itemLayoutTemplate);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetItemLayoutTemplatesForBankWithItemTypeFilter(System.Int32 bankId, System.Collections.Generic.List<ItemTypeEnum> itemTypes, System.Boolean exclude)
        {
            return decoree.GetItemLayoutTemplatesForBankWithItemTypeFilter(bankId, itemTypes, exclude);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetItemLayoutTemplatesForBankWithListOfNamesFilter(System.Int32 bankId, System.Collections.Generic.List<System.String> iltNames)
        {
            return decoree.GetItemLayoutTemplatesForBankWithListOfNamesFilter(bankId, iltNames);
        }

        public virtual System.Collections.Generic.Dictionary<System.String, System.Int32> GetItemLayoutTemplatesForBankList(System.Int32 bankId, System.Boolean checkCompleteBankHierarchy)
        {
            return decoree.GetItemLayoutTemplatesForBankList(bankId, checkCompleteBankHierarchy);
        }

        public virtual System.Collections.Generic.Dictionary<System.String, System.Int32> GetControlTemplatesForBankList(System.Int32 bankId, System.Boolean checkCompleteBankHierarchy)
        {
            return decoree.GetControlTemplatesForBankList(bankId, checkCompleteBankHierarchy);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetItemLayoutTemplatesForBank(System.Int32 bankId)
        {
            return decoree.GetItemLayoutTemplatesForBank(bankId);
        }

        public virtual System.Collections.Generic.List<System.String> GetItemLayoutTemplateSourceNamesForItemCodeList(System.Int32 bankId, System.Collections.Generic.List<System.String> itemCodes)
        {
            return decoree.GetItemLayoutTemplateSourceNamesForItemCodeList(bankId, itemCodes);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity GetControlTemplate(Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity controlTemplate)
        {
            return decoree.GetControlTemplate(controlTemplate);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetControlTemplatesForBank(System.Int32 bankId)
        {
            return decoree.GetControlTemplatesForBank(bankId);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity GetGenericResource(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity genericResource)
        {
            return decoree.GetGenericResource(genericResource);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetGenericResourceForBank(System.Int32 bankId, System.String filter, System.String filePrefix, System.Boolean templatesOnly)
        {
            return decoree.GetGenericResourceForBank(bankId, filter, filePrefix, templatesOnly);
        }

        public virtual System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Guid, System.String>> UpdateBankIdOfResourceEntitiesAndCustomBankProperties(System.Int32 bankIdValue, System.Collections.Generic.List<System.Guid> resourceIdsToUpdate, System.Collections.Generic.List<System.Guid> customBankPropertyIdsToUpdate)
        {
            return decoree.UpdateBankIdOfResourceEntitiesAndCustomBankProperties(bankIdValue, resourceIdsToUpdate, customBankPropertyIdsToUpdate);
        }

        public virtual System.String UpdateAspectResource(Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity resource)
        {
            return decoree.UpdateAspectResource(resource);
        }

        public virtual System.String UpdateAspectResource(Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity resource, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateAspectResource(resource, refetch, recurse);
        }

        public virtual System.String UpdateAssessmentTestResource(Questify.Builder.Model.ContentModel.EntityClasses.AssessmentTestResourceEntity resource)
        {
            return decoree.UpdateAssessmentTestResource(resource);
        }

        public virtual System.String UpdateAssessmentTestResource(Questify.Builder.Model.ContentModel.EntityClasses.AssessmentTestResourceEntity resource, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateAssessmentTestResource(resource, refetch, recurse);
        }

        public virtual System.String UpdateTestPackageResource(Questify.Builder.Model.ContentModel.EntityClasses.TestPackageResourceEntity resource)
        {
            return decoree.UpdateTestPackageResource(resource);
        }

        public virtual System.String UpdateTestPackageResource(Questify.Builder.Model.ContentModel.EntityClasses.TestPackageResourceEntity resource, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateTestPackageResource(resource, refetch, recurse);
        }

        public virtual System.String UpdatePackageResource(Questify.Builder.Model.ContentModel.EntityClasses.PackageResourceEntity resource)
        {
            return decoree.UpdatePackageResource(resource);
        }

        public virtual System.String UpdateItemResource(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity resource)
        {
            return decoree.UpdateItemResource(resource);
        }

        public virtual System.String UpdateItemResource(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity resource, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateItemResource(resource, refetch, recurse);
        }

        public virtual System.String UpdateItemResource(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity resource, System.Boolean refetch, System.Boolean recurse, System.Boolean saveResourceData)
        {
            return decoree.UpdateItemResource(resource, refetch, recurse, saveResourceData);
        }

        public virtual string UpdateItemResource(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity resource, System.Boolean refetch, System.Boolean recurse, System.Boolean saveResourceData, bool skipStateCheck)
        {
            return decoree.UpdateItemResource(resource, refetch, recurse, saveResourceData, skipStateCheck);
        }

        public virtual System.String UpdateItemResources(System.Collections.Generic.IEnumerable<Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity> resource)
        {
            return decoree.UpdateItemResources(resource);
        }

        public virtual System.String UpdateResourceHistory(Questify.Builder.Model.ContentModel.EntityClasses.ResourceHistoryEntity resourceHistory)
        {
            return decoree.UpdateResourceHistory(resourceHistory);
        }

        public virtual System.String UpdateResourceHistory(Questify.Builder.Model.ContentModel.EntityClasses.ResourceHistoryEntity resourceHistory, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateResourceHistory(resourceHistory, refetch, recurse);
        }

        public virtual System.String UpdateResourceVisibility(System.Guid resourceId, System.Int32 setAtBankId, System.Boolean makeResourceVisible)
        {
            return decoree.UpdateResourceVisibility(resourceId, setAtBankId, makeResourceVisible);
        }

        public virtual System.String UpdateGenericResource(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity resource)
        {
            return decoree.UpdateGenericResource(resource);
        }

        public virtual System.String UpdateGenericResource(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity resource, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateGenericResource(resource, refetch, recurse);
        }

        public virtual System.String UpdateItemLayoutTemplateResource(Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity resource)
        {
            return decoree.UpdateItemLayoutTemplateResource(resource);
        }

        public virtual System.String UpdateItemLayoutTemplateResource(Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity resource, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateItemLayoutTemplateResource(resource, refetch, recurse);
        }

        public virtual System.String UpdateControlTemplateResource(Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity resource)
        {
            return decoree.UpdateControlTemplateResource(resource);
        }

        public virtual System.String UpdateControlTemplateResource(Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity resource, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateControlTemplateResource(resource, refetch, recurse);
        }

        public virtual System.String DeleteResources(Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection resourcesToDelete, ref Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection notDeletedResources)
        {
            return decoree.DeleteResources(resourcesToDelete, ref notDeletedResources);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetReferencesForResource(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity resource)
        {
            return decoree.GetReferencesForResource(resource);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetDependenciesForResource(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity resource)
        {
            return decoree.GetDependenciesForResource(resource);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetAvailableStates()
        {
            return decoree.GetAvailableStates();
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.StateEntity GetState(System.Int32 stateId)
        {
            return decoree.GetState(stateId);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ActionEntity GetStateAction(System.Int32 stateId, System.String target)
        {
            return decoree.GetStateAction(stateId, target);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.ActionEntity GetStateAction(System.Int32 bankId, System.String resourceName, System.String target)
        {
            return decoree.GetStateAction(bankId, resourceName, target);
        }

        public virtual System.Collections.Generic.IDictionary<System.String, Questify.Builder.Model.ContentModel.EntityClasses.ActionEntity> GetStateActions(System.Int32 bankId, System.String[] resourceNames, System.String target)
        {
            return decoree.GetStateActions(bankId, resourceNames, target);
        }

        public virtual System.Boolean ChangeCreatorModifier(System.Int32 currentUserId, System.Int32 newUserId)
        {
            return decoree.ChangeCreatorModifier(currentUserId, newUserId);
        }

        public virtual Questify.Builder.Model.ContentModel.EntityClasses.DataSourceResourceEntity GetDataSource(Questify.Builder.Model.ContentModel.EntityClasses.DataSourceResourceEntity dataSource)
        {
            return decoree.GetDataSource(dataSource);
        }

        public virtual Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection GetDataSourcesForBank(System.Int32 bankId, System.Nullable<System.Boolean> isTemplate, params System.String[] behaviours)
        {
            return decoree.GetDataSourcesForBank(bankId, isTemplate, behaviours);
        }

        public virtual System.String UpdateDataSourceResource(Questify.Builder.Model.ContentModel.EntityClasses.DataSourceResourceEntity resource)
        {
            return decoree.UpdateDataSourceResource(resource);
        }

        public virtual System.String UpdateDataSourceResource(Questify.Builder.Model.ContentModel.EntityClasses.DataSourceResourceEntity resource, System.Boolean refetch, System.Boolean recurse)
        {
            return decoree.UpdateDataSourceResource(resource, refetch, recurse);
        }

        public virtual EntityCollection GetItemLayoutTemplatesFromListOfResourceIds(IEnumerable<Guid> resourceIds, bool withDependencies)
        {
            return decoree.GetItemLayoutTemplatesFromListOfResourceIds(resourceIds, withDependencies);
        }

        public virtual ResourceEntity GetResourceByNameWithOption(int bankId, string name, ResourceRequestDTO request)
        {
            return decoree.GetResourceByNameWithOption(bankId, name, request);
        }

        public virtual EntityCollection GetResourcesByNamesWithOption(int bankId, List<string> names, ResourceRequestDTO request)
        {
            return decoree.GetResourcesByNamesWithOption(bankId, names, request);
        }

        public virtual ResourceEntity GetResourceByIdWithOption(Guid resourceId, ResourceRequestDTO request)
        {
            return decoree.GetResourceByIdWithOption(resourceId, request);
        }

        public virtual ResourceEntity GetResourceByIdWithOption(Guid resourceId, IEntityFactory2 factory, ResourceRequestDTO request)
        {
            return decoree.GetResourceByIdWithOption(resourceId, factory, request);
        }

        public virtual EntityCollection GetResourcesByIdsWithOption(List<Guid> resourceIds, ResourceRequestDTO request)
        {
            return decoree.GetResourcesByIdsWithOption(resourceIds, request);
        }

        public virtual EntityCollection GetResourcesByIdsWithOption(List<Guid> resourceIds, IEntityFactory2 factory, ResourceRequestDTO request)
        {
            return decoree.GetResourcesByIdsWithOption(resourceIds, factory, request);
        }

        public virtual ItemResourceEntityCollection GetItemsByCodes(List<string> itemcodeList, int bankId, ItemResourceRequestDTO request)
        {
            return decoree.GetItemsByCodes(itemcodeList, bankId, request);
        }
    }
}
