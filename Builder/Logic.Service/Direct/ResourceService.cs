
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Enums;
using Extended_Classes.HelperClasses;
using HelperClasses;
using Questify.Builder.Logic.Service.Exceptions;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Properties;
using Questify.Builder.Model.ContentModel;
using Questify.Builder.Model.ContentModel.DatabaseSpecific;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.Model.ContentModel.ResourceProperties;
using Questify.Builder.Security;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SecurityException = Questify.Builder.Security.SecurityException;

namespace Questify.Builder.Logic.Service.Direct
{
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    public class ResourceService : IResourceService
    {
        private readonly ExcludeIncludeFieldsHelper _excludeIncludeFieldsHelper;
        private readonly bool _fetchListWithCompleteBank;
        public IPermissionService PermissionService { get; }


        public ResourceService()
        {
            _fetchListWithCompleteBank = true;
            PermissionService = PermissionFactory.Instance;
            _excludeIncludeFieldsHelper = new ExcludeIncludeFieldsHelper();
        }


        public ResourceService(IPermissionService permissionServiceOverride, bool fetchListWithCompleteBank)
        {
            PermissionService = permissionServiceOverride;
            _fetchListWithCompleteBank = fetchListWithCompleteBank;
            _excludeIncludeFieldsHelper = new ExcludeIncludeFieldsHelper();
        }



        public string DeleteResources(EntityCollection resourcesToDelete, ref EntityCollection notDeletedResources)
        {
            var checkedPermissionTargetsPerBank = new Dictionary<int, List<Tuple<TestBuilderPermissionTarget, bool>>>();
            var errorBuilder = new StringBuilder();
            var errorBuilderValidationException = new StringBuilder();
            var errorBuilderSecurityException = new StringBuilder();

            foreach (ResourceEntity resource in resourcesToDelete)
            {
                var permissionTargetToDelete = ContentModelObjectToPermissionTarget(resource);

                try
                {
                    DeleteResource(checkedPermissionTargetsPerBank, resource, permissionTargetToDelete);
                }
                catch (ORMEntityValidationException)
                {
                    notDeletedResources.Add(resource);
                    errorBuilderValidationException.AppendFormat(", '{0}'", resource.Name);
                }
                catch (SecurityException)
                {
                    notDeletedResources.Add(resource);
                    errorBuilderSecurityException.AppendFormat(", '{0}'", resource.Name);

                    if (checkedPermissionTargetsPerBank.ContainsKey(resource.BankId))
                    {
                        if (!checkedPermissionTargetsPerBank[resource.BankId]
                            .Any(p => p.Item1.Equals(permissionTargetToDelete)))
                            checkedPermissionTargetsPerBank[resource.BankId]
                                .Add(new Tuple<TestBuilderPermissionTarget, bool>(permissionTargetToDelete, false));
                    }
                    else
                    {
                        checkedPermissionTargetsPerBank.Add(resource.BankId,
                            new List<Tuple<TestBuilderPermissionTarget, bool>>
                            {
                                new Tuple<TestBuilderPermissionTarget, bool>(permissionTargetToDelete, false)
                            });
                    }
                }
                catch (Exception ex)
                {
                    throw new ServiceException("Error while deleting resources!", ex);
                }
            }

            if (!string.IsNullOrEmpty(errorBuilderValidationException.ToString()))
            {
                errorBuilder.AppendLine(string.Format(Resources.DeleteFailureMessage_Validation,
                    errorBuilderValidationException.ToString().Remove(0, 2)));
            }

            if (!string.IsNullOrEmpty(errorBuilderSecurityException.ToString()))
            {
                if (!string.IsNullOrEmpty(errorBuilder.ToString()))
                {
                    errorBuilder.AppendLine(Environment.NewLine);
                    errorBuilder.AppendLine(string.Format(Resources.DeleteFailureMessage_Security,
                        errorBuilderSecurityException.ToString().Remove(0, 2)));
                }
            }
            return errorBuilder.ToString();
        }

        private void DeleteResource(
            Dictionary<int, List<Tuple<TestBuilderPermissionTarget, bool>>> checkedPermissionTargetsPerBank,
            ResourceEntity resource,
            TestBuilderPermissionTarget permissionTargetToDelete)
        {
            if (checkedPermissionTargetsPerBank.ContainsKey(resource.BankId) &&
                checkedPermissionTargetsPerBank[resource.BankId]
                    .Any(p => p.Item1.Equals(permissionTargetToDelete)))
            {
                if (checkedPermissionTargetsPerBank[resource.BankId]
        .FirstOrDefault(p => p.Item1.Equals(permissionTargetToDelete)).Item2 == false)
                {
                    throw new SecurityException();
                }
            }
            else
            {
                PermissionFactory.Instance.UserIsPermittedToNamedTask(TestBuilderPermissionAccess.Delete,
                    permissionTargetToDelete, TestBuilderPermissionNamedTask.None, resource.BankId, 0);
                if (checkedPermissionTargetsPerBank.ContainsKey(resource.BankId))
                {
                    checkedPermissionTargetsPerBank[resource.BankId]
                        .Add(new Tuple<TestBuilderPermissionTarget, bool>(permissionTargetToDelete, true));
                }
                else
                {
                    checkedPermissionTargetsPerBank.Add(resource.BankId,
                        new List<Tuple<TestBuilderPermissionTarget, bool>>
                        {
                            new Tuple<TestBuilderPermissionTarget, bool>(permissionTargetToDelete, true)
                        });
                }
            }
            using (var adapter = new DataAccessAdapter())
            {
                adapter.DeleteEntity(resource);
            }
        }



        private bool ResourceExistsInBankHierarchy(int anchorBankId, Guid resourceId, bool checkInHierarchy, IEntityFactory2 factory)
        {
            var resources = new EntityCollection(factory);

            int[] ids;
            if (checkInHierarchy)
            {
                var bankBranchHelper = new BankBranchIdHelper(PermissionService);
                ids = bankBranchHelper.GetBankBrancheIds(anchorBankId, false);
            }
            else
            {
                ids = new[] { anchorBankId };
            }

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var with1 = filter;
            with1.PredicateExpression.Add(ResourceFields.ResourceId == resourceId);
            with1.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ResourceFields.BankId, null, ids));
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter, 1, null, null, _excludeIncludeFieldsHelper.GetResourceIncludedFieldList());
            }

            var result = resources.Count > 0;
            resources.Clear();
            resources = null;
            return result;
        }

        bool IResourceService.ResourceExists(int anchorBankId, Guid resourceId, bool checkInHierarchy, IEntityFactory2 factory)
        {
            return ResourceExistsInBankHierarchy(anchorBankId, resourceId, checkInHierarchy, factory);
        }

        bool IResourceService.ResourceExists(int anchorBankId, Guid resourceId, bool checkInHierarchy)
        {
            return ResourceExistsInBankHierarchy(anchorBankId, resourceId, checkInHierarchy, new ResourceEntityFactory());
        }

        public bool ResourceExists(int anchorBankId, string resourceName, bool checkInHierarchy, IEntityFactory2 factory)
        {
            var resources = new EntityCollection(factory);
            if (String.IsNullOrEmpty(resourceName))
            {
                return false;
            }

            int[] ids;
            if (checkInHierarchy)
            {
                var bankBranchHelper = new BankBranchIdHelper(PermissionService);
                ids = bankBranchHelper.GetBankBrancheIds(anchorBankId, false);
            }
            else
            {
                ids = new[] { anchorBankId };
            }

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var with2 = filter;
            with2.PredicateExpression.Add(ResourceFields.Name == resourceName);
            with2.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ResourceFields.BankId, null, ids));
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter, 1, null, null, _excludeIncludeFieldsHelper.GetResourceIncludedFieldList());
            }

            var result = resources.Count > 0;
            resources.Clear();
            resources = null;
            return result;
        }

        bool IResourceService.ResourceExists(int anchorBankId, string resourceName, bool checkInHierarchy, IEntityFactory2 factory)
        {
            return ResourceExists(anchorBankId, resourceName, checkInHierarchy, factory);
        }

        bool IResourceService.ResourceExists(int anchorBankId, string resourceName, bool checkInHierarchy)
        {
            return ResourceExists(anchorBankId, resourceName, checkInHierarchy, new ResourceEntityFactory());
        }

        private PrefetchPath2 GetResourcePrefetchPath(ResourceRequestDTO request)
        {
            var resourcePrefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));
            resourcePrefetchPath.Add(ResourceEntity.PrefetchPathBank);
            if (request.WithDependencies)
                resourcePrefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);

            if (!request.WithCustomProperties) return resourcePrefetchPath;

            var customBankPropertyValuesPath =
    resourcePrefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListCustomBankPropertySelectedValueCollection);
            customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            var listCustomBankPath =
    customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
        .PrefetchPathListCustomBankProperty);
            listCustomBankPath.SubPath.Add(ListCustomBankPropertyEntity
    .PrefetchPathListValueCustomBankPropertyCollection);

            customBankPropertyValuesPath.SubPath.Add(CustomBankPropertyValueEntity
    .PrefetchPathCustomBankProperty);
            customBankPropertyValuesPath.SubPath.Add(RichTextValueCustomBankPropertyValueEntity
    .PrefetchPathRichTextValueCustomBankProperty);

            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
    .PrefetchPathConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart);
            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                .PrefetchPathConceptStructureCustomBankPropertySelectedPartCollection);
            var conceptStructureCustomBankPath =
    customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
        .PrefetchPathConceptStructureCustomBankProperty);
            conceptStructureCustomBankPath.SubPath.Add(ConceptStructureCustomBankPropertyEntity
    .PrefetchPathConceptStructurePartCustomBankPropertyCollection);

            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
    .PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart);
            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                .PrefetchPathTreeStructureCustomBankPropertySelectedPartCollection);

            var treeStructureCustomBankPath =
    customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
        .PrefetchPathTreeStructureCustomBankProperty);
            treeStructureCustomBankPath.SubPath.Add(TreeStructureCustomBankPropertyEntity
    .PrefetchPathTreeStructurePartCustomBankPropertyCollection);
            return resourcePrefetchPath;
        }

        private void AddUserResourcePrefetchPath(ref PrefetchPath2 resourcePrefetchPath)
        {
            var userCreatedPathIsAdded = false;
            var userModifiedPathIsAdded = false;
            if (resourcePrefetchPath.Count > 0)
                for (var i = 0; i <= resourcePrefetchPath.Count - 1; i++)
                {
                    if (resourcePrefetchPath[i].Equals(ResourceEntity.PrefetchPathCreatedByUser))
                    {
                        userCreatedPathIsAdded = true;
                    }
                    if (resourcePrefetchPath[i].Equals(ResourceEntity.PrefetchPathModifiedByUser))
                    {
                        userModifiedPathIsAdded = true;
                    }
                }
            if (!userCreatedPathIsAdded)
            {
                resourcePrefetchPath.Add(ResourceEntity.PrefetchPathCreatedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
            }
            if (!userModifiedPathIsAdded)
            {
                resourcePrefetchPath.Add(ResourceEntity.PrefetchPathModifiedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
            }
        }

        private void AddDependentResourcePrefetchPath(ref PrefetchPath2 resourcePrefetchPath)
        {
            var dependentResourceIsAdded = false;
            if (resourcePrefetchPath.Count > 0)
                for (var i = 0; i <= resourcePrefetchPath.Count - 1; i++)
                    if (resourcePrefetchPath[i].Equals(ResourceEntity.PrefetchPathDependentResourceCollection))
                    {
                        var depResourceAdded = false;
                        if (resourcePrefetchPath[i].SubPath.Count > 0)
                            for (var index = 0; index <= resourcePrefetchPath[i].SubPath.Count - 1; index++)
                                if (resourcePrefetchPath[i].Equals(DependentResourceEntity.PrefetchPathDependentResource))
                                {
                                    depResourceAdded = true;
                                    break;
                                }
                        if (!depResourceAdded)
                        {
                            resourcePrefetchPath[i].SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
                            dependentResourceIsAdded = true;
                            break;
                        }
                    }
            if (!dependentResourceIsAdded)
            {
                var depResourcePrefethPath = ResourceEntity.PrefetchPathDependentResourceCollection;
                depResourcePrefethPath.SubPath.Add(DependentResourceEntity.PrefetchPathResource);
                resourcePrefetchPath.Add(depResourcePrefethPath);
            }
        }

        public ResourceDataEntity GetResourceData(ResourceEntity resource)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            return GetResourceData(resource.ResourceId);
        }

        public ResourceDataEntity GetResourceDataByResourceId(Guid resourceId)
        {
            return GetResourceData(resourceId);
        }

        public EntityCollection GetResourceDataByResourceIds(List<Guid> resourceIds)
        {
            var resources = new EntityCollection(new ResourceDataEntityFactory());
            if (!resourceIds.Any())
            {
                return resources;
            }

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var with3 = filter;
            var index = 0;
            foreach (var resourceId in resourceIds)
            {
                if (index == 0)
                    with3.PredicateExpression.Add(ResourceDataFields.ResourceId == resourceId);
                else
                    with3.PredicateExpression.AddWithOr(ResourceDataFields.ResourceId == resourceId);
                index += 1;
            }
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter);
            }
            return resources;
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private ResourceDataEntity GetResourceData(Guid resourceId)
        {
            var resourceData = new ResourceDataEntity(resourceId);
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(resourceData);
            }

            return resourceData;
        }

        public EntityCollection GetResourcesForBank(int bankId)
        {
            return GetResourcesForBank(bankId, true);
        }

        public EntityCollection GetResourcesForBank(int bankId, bool fetchCompleteBranch)
        {
            return GetResourcesForBank(bankId, new ResourceEntityFactory(), fetchCompleteBranch);
        }

        public EntityCollection GetResourcesForBank(int bankId, IEntityFactory2 factory, bool fetchCompleteBranch)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            return GetResourceCollection(bankId, factory, ResourceFields.BankId, prefetchPath, null,
                fetchCompleteBranch, 0);
        }


        public EntityCollection GetReferencesForResource(ResourceEntity resource)
        {
            var resources = new EntityCollection(new ResourceEntityFactory());

            if (resource.IsNew)
            {
                if (resource.ReferencedResourceCollection != null)
                    resources.AddRange(resource.ReferencedResourceCollection.Select(r => r.Resource));
            }
            else
            {
                IPrefetchPath2 resourcePrefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

                var depResourcePrefethPath = ResourceEntity.PrefetchPathReferencedResourceCollection;
                depResourcePrefethPath.SubPath.Add(DependentResourceEntity.PrefetchPathResource);
                resourcePrefetchPath.Add(depResourcePrefethPath);

                using (var adapter = new DataAccessAdapter())
                {
                    adapter.FetchEntity(resource, resourcePrefetchPath);
                }

                foreach (var e in resource.ReferencedResourceCollection)
                {
                    if (e.Resource != null)
                    {
                        resources.Add(e.Resource);
                    }
                }
            }

            return resources;
        }

        public EntityCollection GetDependenciesForResource(ResourceEntity resource)
        {
            var depResourcePrefethPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));
            depResourcePrefethPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
            depResourcePrefethPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(resource, depResourcePrefethPath);
            }

            var resources = new EntityCollection(new ResourceEntityFactory());
            foreach (var e in resource.DependentResourceCollection)
            {
                if (e.DependentResource != null)
                {
                    resources.Add(e.DependentResource);
                }
            }

            return resources;
        }



        public List<KeyValuePair<Guid, string>> UpdateBankIdOfResourceEntitiesAndCustomBankProperties(
    int bankIdValue,
    List<Guid> resourceIdsToUpdate,
    List<Guid> customBankPropertyIdsToUpdate)
        {
            var failedUpdates = new List<KeyValuePair<Guid, string>>();

            using (var adapter = new DataAccessAdapter())
            {
                var resOrCbpId = default(Guid);

                try
                {
                    adapter.OpenConnection();
                    adapter.StartTransaction(IsolationLevel.ReadCommitted, "UpdateBankIdResourcesAndCbpsTransaction");

                    foreach (var resOrCbpIdLoopVariable in resourceIdsToUpdate)
                    {
                        resOrCbpId = resOrCbpIdLoopVariable;
                        ActionProcedures.UpdateResourceBankId(resOrCbpId, bankIdValue, adapter);
                    }

                    foreach (var resOrCbpIdLoopVariable in customBankPropertyIdsToUpdate)
                    {
                        resOrCbpId = resOrCbpIdLoopVariable;
                        ActionProcedures.UpdateCustomBankPropertyBankId(resOrCbpId, bankIdValue, adapter);
                    }
                }
                catch (Exception e)
                {
                    failedUpdates.Add(new KeyValuePair<Guid, string>(resOrCbpId, e.Message));
                }
                finally
                {
                    if (failedUpdates.Count == 0)
                        adapter.Commit();
                    else
                        adapter.Rollback();

                    adapter.CloseConnection();
                }
            }

            return failedUpdates;
        }

        private string UpdateResource(ResourceEntity resource)
        {
            return UpdateResource(resource, true, true, true);
        }

        private string UpdateResource(ResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, true, refetch, recurse);
        }

        private string UpdateResource(ResourceEntity resource, bool useTransaction,
            bool refetch, bool recurse)
        {
            using (var adapter = new DataAccessAdapter())
            {
                return UpdateResource(resource, adapter, useTransaction, refetch, recurse, false);
            }
        }

        private string UpdateResource(
            ResourceEntity resource,
            DataAccessAdapter adapter,
            bool useTransaction,
            bool refetch,
            bool recurse,
            bool updateResourceData)
        {
            return UpdateResource(resource, adapter, useTransaction, refetch, recurse, updateResourceData, false, false, false);
        }

        public bool UserIsAllowedToUpdateAccordingToState(ResourceEntity resource)
        {
            var currentStateAction = GetStateAction(resource.BankId, resource.Name, "resourceediting");
            switch (currentStateAction?.Name.ToLower())
            {
                case "warn":
                case "allow":
                    return true;

                case "prohibit":
                    var workFlowChangePermitted = PermissionFactory.Instance.TryUserIsPermittedToNamedTask(
                        TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask,
                        TestBuilderPermissionNamedTask.ChangeWorkflowMetadataWhenProhibittedByState,
                        resource.BankId, 0);

                    return resource.OnlyChangesInWorkflowMetaData() && workFlowChangePermitted;

                default:
                    return true;
            }
        }

        private string UpdateResource(
            ResourceEntity resource,
            DataAccessAdapter adapter,
            bool useTransaction,
            bool refetch,
            bool recurse,
            bool updateResourceData,
            bool skipSaveResourceHistory,
            bool skipSetItemIdIfNeeded,
            bool skipStateCheck)
        {
            var result = string.Empty;
            var transactionUsed = false;
            var isNew = resource.IsNew;
            try
            {
                var permissionTargetResource = ContentModelObjectToPermissionTarget(resource);
                if (resource.IsNew)
                {
                    PermissionFactory.Instance.UserIsPermittedToNamedTask(TestBuilderPermissionAccess.DALCreate,
                        permissionTargetResource, TestBuilderPermissionNamedTask.None, resource.BankId, 0);
                }
                else
                {
                    PermissionFactory.Instance.UserIsPermittedToNamedTask(TestBuilderPermissionAccess.DALUpdate,
                        permissionTargetResource, TestBuilderPermissionNamedTask.None, resource.BankId, 0);
                }

                if (!skipStateCheck && !UserIsAllowedToUpdateAccordingToState(resource))
                {
                    throw new SecurityException("Cannot update resource due to current state");
                }

                if (resource.IsDirty == false && resource.HasChangesInTopology())
                {
                    resource.IsDirty = true;
                }

                if (resource.RemovedDependentEntities.Any())
                {
                    PermissionFactory.Instance.UserIsPermittedToNamedTask(TestBuilderPermissionAccess.DeleteDependency,
                        permissionTargetResource, TestBuilderPermissionNamedTask.None, resource.BankId, 0);

                    transactionUsed = StartTransactionIfNeeded(adapter, useTransaction, false);

                    foreach (DependentResourceEntity removedDependentResource in resource.RemovedDependentEntities)
                    {
                        adapter.DeleteEntity(removedDependentResource);
                    }
                }


                transactionUsed = UpdateCustomBank(resource, adapter, useTransaction, transactionUsed);

                if (recurse && ShouldSetItemIdAfterSave(resource))
                {
                    adapter.SaveEntity(resource, true, false);
                }
                else
                {
                    bool shouldRefetch = (refetch || (resource is IVersionable && !(resource is ItemResourceEntity && ItemIdHelper.UseItemId())));
                    adapter.SaveEntity(resource, shouldRefetch, recurse);
                }
                if (recurse == false && updateResourceData && !ShouldSetItemIdAfterSave(resource))
                {
                    adapter.SaveEntity(resource.ResourceData, refetch, false);
                }
            }
            catch (ORMEntityValidationException ex)
            {
                result = ex.Message;
            }
            catch (SecurityException ex)
            {
                result = ex.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                throw new ServiceException($"An error occurred while updating resource with id '{resource.ResourceId}'", ex);
            }
            finally
            {
                if (transactionUsed)
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        if (!isNew && !skipSetItemIdIfNeeded)
                        {
                            SetItemIdIfNeeded(resource, useTransaction, adapter, recurse, updateResourceData, refetch, false);
                        }
                        adapter.Commit();

                        if (isNew && !skipSetItemIdIfNeeded)
                        {
                            SetItemIdIfNeeded(resource, useTransaction, adapter, recurse, updateResourceData, refetch, true);
                        }

                        if (!skipSaveResourceHistory)
                        {
                            SaveResourceHistory(resource, adapter);
                        }
                    }
                    else
                    {
                        adapter.Rollback();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        if (!skipSetItemIdIfNeeded)
                        {
                            SetItemIdIfNeeded(resource, useTransaction, adapter, recurse, updateResourceData, refetch, isNew);
                        }

                        if (!skipSaveResourceHistory)
                        {
                            SaveResourceHistory(resource, adapter);
                        }
                    }
                }

                if (resource is IVersionable)
                {
                    ((IVersionable)resource).MajorVersionLabel = string.Empty;
                }

                adapter.CloseConnection();
            }
            return result;
        }

        private bool UpdateCustomBank(ResourceEntity resource, DataAccessAdapter adapter, bool useTransaction, bool transactionUsed)
        {
            bool transactionUsedForUpdate = transactionUsed;
            if (resource.CustomBankPropertyValueCollection != null &&
                resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker != null &&
                resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Count > 0)
            {
                transactionUsedForUpdate = StartTransactionIfNeeded(adapter, useTransaction, transactionUsed);
                adapter.DeleteEntityCollection(resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker);
                resource.CustomBankPropertyValueCollection.RemovedEntitiesTracker.Clear();
            }

            if (resource.CustomBankPropertyValueCollection == null) return transactionUsedForUpdate;

            foreach (var valueEntity in resource.CustomBankPropertyValueCollection.OfType<ListCustomBankPropertyValueEntity>())
            {
                if (valueEntity.ListCustomBankPropertySelectedValueCollection != null &&
                    valueEntity.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker != null &&
                    valueEntity.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker.Count > 0)
                {
                    transactionUsedForUpdate = StartTransactionIfNeeded(adapter, useTransaction, transactionUsedForUpdate);
                    adapter.DeleteEntityCollection(valueEntity.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker);
                    valueEntity.ListCustomBankPropertySelectedValueCollection.RemovedEntitiesTracker.Clear();
                }
            }

            return transactionUsedForUpdate;
        }

        private bool StartTransactionIfNeeded(DataAccessAdapter adapter, bool useTransaction, bool transactionUsed)
        {
            if (useTransaction && !transactionUsed)
            {
                adapter.OpenConnection();
                adapter.StartTransaction(IsolationLevel.ReadCommitted, "ResourceUpdateTransaction");
                return true;
            }
            return transactionUsed;
        }

        private void SetItemIdIfNeeded(ResourceEntity resource, bool useTransaction, DataAccessAdapter adapter,
            bool recurse, bool updateResourceData, bool refetch, bool shouldUpdateResource)
        {
            if (ItemIdHelper.UseItemId() && resource is ItemResourceEntity)
            {
                var itemResource = (ItemResourceEntity)resource;
                var correctItemId = ItemIdHelper.GetItemId(itemResource.ItemAutoId);
                if (itemResource.ItemId != correctItemId)
                {
                    UpdateItemResourceItemId(ref itemResource, correctItemId);
                    if (shouldUpdateResource)
                        UpdateResource(itemResource, adapter, useTransaction, refetch, recurse, updateResourceData, true, true, true);
                }
            }
        }

        private bool ShouldSetItemIdAfterSave(ResourceEntity resource)
        {
            return (ItemIdHelper.UseItemId() && resource is ItemResourceEntity && resource.IsNew);
        }

        private void UpdateItemResourceItemId(ref ItemResourceEntity itemResource, string newItemId)
        {
            itemResource.ItemId = newItemId;
            var assesmentItem =
                (AssessmentItem)SerializeHelper.XmlDeserializeFromByteArray(itemResource.ResourceData.BinData,
                    typeof(AssessmentItem), true);
            assesmentItem.ItemId = newItemId;
            itemResource.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(assesmentItem);
        }

        private void SaveResourceHistory(ResourceEntity resourceEntity, DataAccessAdapter adapter)
        {
            if (resourceEntity is IVersionable)
            {
                var versionableResourceEntity = (IVersionable)resourceEntity;
                var principal = (TestBuilderPrincipal)Thread.CurrentPrincipal;
                var identity = (TestBuilderIdentity)principal.Identity;

                versionableResourceEntity = RefetchResource(versionableResourceEntity);
                var label = ((IVersionable)resourceEntity).MajorVersionLabel;
                if (label != null)
                {
                    versionableResourceEntity.MajorVersionLabel = label;
                }

                var getResourceData = false;
                try
                {
                    if (versionableResourceEntity.SaveObjectAsBinary &&
                        (((ResourceEntity)versionableResourceEntity).ResourceData == null || ((ResourceEntity)versionableResourceEntity).ResourceData.BinData == null))
                    {
                        getResourceData = true;
                    }
                }
                catch (ORMEntityOutOfSyncException e)
                {
                    getResourceData = true;
                }
                finally
                {
                    if (getResourceData)
                    {
                        ((ResourceEntity)versionableResourceEntity).ResourceData = GetResourceData(resourceEntity);
                    }
                }

                adapter.SaveEntity(ResourceHistoryCreator.CreateResourceHistoryEntity(versionableResourceEntity, identity.Name), false, true);
            }
        }

        private IVersionable RefetchResource(IVersionable resourceEntity)
        {
            var itemResourceEntity = resourceEntity as ItemResourceEntity;
            if (itemResourceEntity != null)
            {
                var fetchedResource = new ItemResourceEntity(itemResourceEntity.ResourceId);
                PrefetchResource(fetchedResource, EntityType.ItemResourceEntity, new ResourceRequestDTO());
                return fetchedResource;
            }
            return resourceEntity;
        }

        public string UpdateTestPackageResource(TestPackageResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        public string UpdateTestPackageResource(TestPackageResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        public string UpdatePackageResource(PackageResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        public string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource)
        {
            return UpdateResource(resource);
        }


        public string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        public string UpdateControlTemplateResource(ControlTemplateResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        public string UpdateControlTemplateResource(ControlTemplateResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        public string UpdateGenericResource(GenericResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        public string UpdateGenericResource(GenericResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        public string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        public string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource, bool refetch,
    bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        public string UpdateItemResource(ItemResourceEntity resource, bool refetch, bool recurse)
        {
            var result = UpdateResource(resource, refetch, recurse);
            return result;
        }

        public string UpdateItemResource(ItemResourceEntity resource, bool refetch, bool recurse, bool saveResourceData)
        {
            return this.UpdateItemResource(resource, refetch, recurse, saveResourceData, false);
        }

        public string UpdateItemResource(ItemResourceEntity resource, bool refetch, bool recurse, bool saveResourceData, bool skipStateCheck)
        {
            string result;
            using (var adapter = new DataAccessAdapter())
            {
                result = UpdateResource(resource, adapter, false, refetch, recurse, saveResourceData, false, false, skipStateCheck);
            }
            return result;
        }

        public string UpdateItemResources(IEnumerable<ItemResourceEntity> resources)
        {
            var result = string.Empty;
            using (var adapter = new DataAccessAdapter())
            {
                foreach (var item in resources)
                    result = string.Concat(result, UpdateResource(item, adapter, false, false, false, true));
            }
            return result;
        }

        public string UpdateItemResource(ItemResourceEntity resource)
        {
            return UpdateItemResource(resource, true, true);
        }

        public string UpdateResourceHistory(ResourceHistoryEntity resourceHistory)
        {
            return UpdateResourceHistory(resourceHistory, true, true);
        }

        public string UpdateResourceHistory(ResourceHistoryEntity resourceHistory, bool refetch, bool recurse)
        {
            var result = string.Empty;

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    adapter.SaveEntity(resourceHistory, refetch, recurse);
                }
                catch (ORMEntityValidationException ex)
                {
                    result = ex.Message;
                }
                catch (SecurityException ex)
                {
                    result = ex.Message;
                }
                catch (Exception ex)
                {
                    throw new ServiceException(
                        $"An error occurred while updating resource with id '{resourceHistory.ResourceId}'", ex);
                }
            }

            return result;
        }

        public string UpdateResourceVisibility(Guid resourceId, int setAtBankId, bool makeResourceVisible)
        {
            var result = string.Empty;
            var resourcesToDeleteCollection = new EntityCollection<HiddenResourceEntity>();
            HiddenResourceEntity hiddenResourceToAdd = null;

            var request = new ResourceRequestDTO() { WithHiddenResources = true };
            var resourceToToggleTheVisibilityOf = GetResourceByIdWithOption(resourceId, request);

            if (resourceToToggleTheVisibilityOf == null) return result;

            using (var adapter = new DataAccessAdapter())
            {
                if (makeResourceVisible)
                {
                    var hiddenResourceToDelete = resourceToToggleTheVisibilityOf.HiddenResourceCollection.FirstOrDefault(
                        x => x.ResourceId == resourceId && x.BankId == setAtBankId);
                    if (hiddenResourceToDelete != null)
                    {
                        resourcesToDeleteCollection.Add(hiddenResourceToDelete);
                    }
                }
                else
                {
                    var bankIds = BankstructureHelper.GetBankBrancheIds(adapter, setAtBankId);
                    var hiddenResourcesToDelete = resourceToToggleTheVisibilityOf.HiddenResourceCollection
                        .Where(x => x.BankId != setAtBankId && bankIds.Contains(x.BankId)).ToList();

                    resourcesToDeleteCollection.AddRange(hiddenResourcesToDelete);
                    hiddenResourceToAdd = resourceToToggleTheVisibilityOf.HiddenResourceCollection.FirstOrDefault(x => x.BankId == setAtBankId);
                    if (hiddenResourceToAdd == null)
                    {
                        hiddenResourceToAdd = new HiddenResourceEntity(resourceId, setAtBankId);
                    }
                }

                try
                {
                    if (resourcesToDeleteCollection.Any())
                    {
                        adapter.DeleteEntityCollection(resourcesToDeleteCollection);
                    }
                    if (hiddenResourceToAdd != null)
                    {
                        adapter.SaveEntity(hiddenResourceToAdd, false, false);
                    }
                }
                catch (ORMEntityValidationException ex)
                {
                    result = ex.Message;
                }
                catch (SecurityException ex)
                {
                    result = ex.Message;
                }
                catch (Exception ex)
                {
                    throw new ServiceException(
                        $"An error occurred while setting the visibility of resource with id '{resourceId}'", ex);
                }
            }
            return result;
        }

        public bool ChangeCreatorModifier(int currentUserId, int newUserId)
        {
            ActionProcedures.ChangeCreatorModifier(currentUserId, newUserId);
            return true;
        }



        public ResourcePropertyValueCollection GetResourcePropertyValues(ResourceEntity resourceEntity)
        {
            var result = new ResourcePropertyValueCollection();
            foreach (var resourcePropertyDefinition in BankFactory.Instance.GetResourcePropertyDefinitions(resourceEntity.Bank))
            {
                result.Add(resourcePropertyDefinition, resourcePropertyDefinition.RetrievePropertyValuesFromResource(resourceEntity));
            }
            return result;
        }

        public ItemResourceEntity GetItem(ItemResourceEntity item, ResourceRequestDTO request)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var fetchedResource = new ItemResourceEntity(item.ResourceId);
            PrefetchResource(fetchedResource, EntityType.ItemResourceEntity, request);

            return fetchedResource;
        }

        public ResourceHistoryEntity GetResourceHistory(ResourceHistoryEntity resourceHistory)
        {
            if (resourceHistory == null)
            {
                throw new ArgumentException(nameof(resourceHistory));
            }

            var fetchedResource = new ResourceHistoryEntity(resourceHistory.Id);
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceHistoryEntity));
            prefetchPath.Add(ResourceHistoryEntity.PrefetchPathResource);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(fetchedResource, prefetchPath);
            }

            return fetchedResource;
        }

        public EntityCollection GetResourceHistoryForResource(Guid resourceId)
        {
            var result = new EntityCollection(new ResourceHistoryEntityFactory());

            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceHistoryEntity));
            var filterPredicate = new PredicateExpression();
            IRelationPredicateBucket bucket = new RelationPredicateBucket();

            var prefetchPathResource = prefetchPath.Add(ResourceHistoryEntity.PrefetchPathResource);
            prefetchPathResource.SubPath.Add(ResourceEntity.PrefetchPathBank);
            var depResourcePrefetchPath = ResourceEntity.PrefetchPathDependentResourceCollection;
            depResourcePrefetchPath.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
            prefetchPathResource.SubPath.Add(depResourcePrefetchPath);

            filterPredicate.AddWithAnd(ResourceHistoryFields.ResourceId == resourceId);
            bucket.PredicateExpression.Add(filterPredicate);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(result, bucket, prefetchPath);
            }

            return result;
        }

        public ItemResourceEntityCollection GetItemsByCodes(
            List<string> itemcodeList,
            int bankId,
            ItemResourceRequestDTO request)
        {
            var returnValue = new ItemResourceEntityCollection();
            if (itemcodeList.Count == 0)
                return returnValue;

            using (var itemItemresourceCollection = new ItemResourceEntityCollection())
            {
                const TestBuilderPermissionTarget FETCH_TARGET = TestBuilderPermissionTarget.Any;
                var bankBranchHelper = new BankBranchIdHelper(PermissionService);
                var ids = bankBranchHelper.GetBankBrancheIds(bankId, FETCH_TARGET, TestBuilderPermissionAccess.DALRead);

                var tempItemCodesList = new List<string>(itemcodeList);

                var offSet = 0;
                const int BATCH = 250;
                while (offSet != itemcodeList.Count)
                {
                    var tempResultSet = new ItemResourceEntityCollection();
                    var length = itemcodeList.Count - offSet;
                    if (length > BATCH)
                    {
                        length = BATCH;
                    }

                    var itemCodeBatchArray = (string[])Array.CreateInstance(typeof(string), length);
                    tempItemCodesList.CopyTo(offSet, itemCodeBatchArray, 0, length);

                    IRelationPredicateBucket filter = new RelationPredicateBucket();
                    filter.PredicateExpression.Add(new FieldCompareRangePredicate(ItemResourceFields.Name, null, itemCodeBatchArray));
                    filter.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ItemResourceFields.BankId, null, ids));


                    IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

                    if (request.WithFullCustomProperties)
                    {
                        GetFullCustomBankPropertiesPrefetchPath(prefetchPath);
                    }
                    else if (request.WithCustomProperties)
                    {
                        prefetchPath.Add(ResourceEntity.PrefetchPathBank);
                        prefetchPath.Add(ResourceEntity.PrefetchPathCreatedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                        prefetchPath.Add(ResourceEntity.PrefetchPathModifiedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());

                        var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
                        path1.SubPath.Add(ListCustomBankPropertyValueEntity
                            .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);
                        path1.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                            .PrefetchPathConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart);
                        path1.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                            .PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart);
                        var path2 = path1.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);


                        path2.SubPath.Add(ListCustomBankPropertyEntity
    .PrefetchPathListValueCustomBankPropertyCollection);
                        path2.SubPath.Add(ConceptStructureCustomBankPropertyEntity
                            .PrefetchPathConceptStructurePartCustomBankPropertyCollection);
                    }

                    if (request.WithReportData)
                    {
                        if (!request.WithCustomProperties)
                        {
                            prefetchPath.Add(ResourceEntity.PrefetchPathBank);
                            prefetchPath.Add(ResourceEntity.PrefetchPathCreatedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                            prefetchPath.Add(ResourceEntity.PrefetchPathModifiedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                        }

                        prefetchPath.Add(ResourceEntity.PrefetchPathState, _excludeIncludeFieldsHelper.GetStateExcludedFieldList());
                        prefetchPath.Add(ResourceEntity.PrefetchPathReferencedResourceCollection);
                    }

                    if (request.WithDependencies)
                    {
                        if (!request.WithCustomProperties && !request.WithReportData)
                            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

                        var depResourcePrefethPath = ResourceEntity.PrefetchPathDependentResourceCollection;
                        depResourcePrefethPath.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
                        prefetchPath.Add(depResourcePrefethPath);
                    }

                    using (var adapter = new DataAccessAdapter())
                    {
                        adapter.FetchEntityCollection(tempResultSet, filter, prefetchPath);
                    }

                    itemItemresourceCollection.AddRange(tempResultSet);
                    offSet += length;
                }
                itemcodeList.ToList().ForEach(code =>
{
    var itemListToAdd = itemItemresourceCollection.Items.Where(item => item.Name == code).ToList();
    if (itemListToAdd.Count() == 1)
    {
        returnValue.Add(itemListToAdd.First());
    }
});
            }
            return returnValue;
        }

        public AssessmentTestResourceEntityCollection GetTestByCode(List<string> testcodeList, int bankId,
    bool withCustomProperties)
        {
            var returnValue = new AssessmentTestResourceEntityCollection();
            if (!testcodeList.Any())
            {
                return returnValue;
            }

            const TestBuilderPermissionTarget FETCH_TARGET = TestBuilderPermissionTarget.Any;
            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, FETCH_TARGET, TestBuilderPermissionAccess.DALRead);

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var testCodeArray = new string[testcodeList.Count + 1];
            testcodeList.CopyTo(testCodeArray, 0);
            filter.PredicateExpression.Add(new FieldCompareRangePredicate(AssessmentTestResourceFields.Name, null, testCodeArray));
            filter.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(AssessmentTestResourceFields.BankId, null, ids));

            IPrefetchPath2 prefetchPath = null;
            if (withCustomProperties)
            {
                prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

                prefetchPath.Add(ResourceEntity.PrefetchPathBank);

                var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
                path1.SubPath.Add(ListCustomBankPropertyValueEntity
                    .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

                var path2 = path1.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);

                path2.SubPath.Add(ListCustomBankPropertyEntity.PrefetchPathListValueCustomBankPropertyCollection);
            }

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, prefetchPath);
            }

            return returnValue;
        }

        AssessmentTestResourceEntityCollection IResourceService.GetTestsByCodes(List<string> testcodeList, int bankId,
            bool withCustomProperties)
        {
            return GetTestByCode(testcodeList, bankId, withCustomProperties);
        }

        public EntityCollection GetPauseItemsForBank(int bankId)
        {
            var filter = new RelationPredicateBucket();

            filter.Relations.Add(ResourceEntity.Relations.DependentResourceEntityUsingResourceId,
    "ResourceToDepResource");
            filter.Relations.Add(DependentResourceEntity.Relations.ResourceEntityUsingDependentResourceId,
                "ResourceToDepResource", "DepResourceToItemLayoutResource", JoinHint.Inner);
            filter.Relations.Add(ResourceEntity.Relations.GetSubTypeRelation("ItemLayoutTemplateResourceEntity"),
                "DepResourceToItemLayoutResource", "ItemLayoutResource", JoinHint.Inner);
            filter.PredicateExpression.Add(
                ItemLayoutTemplateResourceFields.ItemType.SetObjectAlias("ItemLayoutResource") ==
                ItemTypeEnum.Pause.ToString());

            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            var path2 = path1.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);

            path2.SubPath.Add(ListCustomBankPropertyEntity.PrefetchPathListValueCustomBankPropertyCollection);

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            var depCollPrefetch = prefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
            depCollPrefetch.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);

            return GetResourceCollection(bankId, new ItemResourceEntityFactory(), ItemResourceFields.BankId,
    prefetchPath, filter);
        }

        public EntityCollection GetItemsForBank(int bankId)
        {
            return GetItemsForBank(bankId, false, null);
        }

        public EntityCollection GetItemsForBankWithFullCustomProperties(int bankId)
        {
            return GetItemsForBank(bankId, true, null);
        }

        public EntityCollection GetItemsForBank(int bankId, RelationPredicateBucket bucket)
        {
            return GetItemsForBank(bankId, false, bucket);
        }

        public EntityCollection GetItemsForBank(int bankId, bool withCustomProperties, RelationPredicateBucket bucket)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));
            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            if (withCustomProperties)
            {
                path1.SubPath.Add(ListCustomBankPropertyValueEntity.PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);
                path1.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity.PrefetchPathConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart);
                path1.SubPath.Add(TreeStructureCustomBankPropertyValueEntity.PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart);
            }

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            var depCollPrefetch = prefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
            var depResSubPath = depCollPrefetch.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
            depResSubPath.EntityFactoryToUse = new ItemLayoutTemplateResourceEntityFactory();
            depResSubPath.SubPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);

            return GetResourceCollection(bankId, new ItemResourceEntityFactory(), ItemResourceFields.BankId, prefetchPath, bucket);
        }

        public EntityCollection GetItemsForBank(
    int bankId,
    string searchKeyWords,
    bool searchInBankProperties,
    bool searchInItemText,
    Guid testContextResourceId,
    int maxRecords)
        {
            var resultCollection = new EntityCollection();
            var fullTextSearchString = searchKeyWords;
            if (searchKeyWords.IndexOf('"') < 0)
            {
                fullTextSearchString = string.Concat('"', fullTextSearchString, '"');
            }
            if (searchInBankProperties)
            {
                GetItemsFromKeywordInBankProperties(bankId, searchKeyWords, testContextResourceId, fullTextSearchString, resultCollection);
            }

            if (searchInItemText && !string.IsNullOrEmpty(searchKeyWords))
            {
                GetItemsFromKeywordInItemText(bankId, testContextResourceId, fullTextSearchString, resultCollection);
            }

            if (string.IsNullOrEmpty(searchKeyWords) && testContextResourceId != Guid.Empty)
            {
                var specificTestBucket = new RelationPredicateBucket();

                specificTestBucket.PredicateExpression.Add(new FieldCompareSetPredicate(ItemResourceFields.ResourceId,
    null, DependentResourceFields.DependentResourceId, null, SetOperator.In,
    DependentResourceFields.ResourceId == testContextResourceId));

                var subResultTestItems = GetItemsForBank(bankId, specificTestBucket);
                AddSubResultItemCollectionToResultItemCollection(subResultTestItems, resultCollection);
            }

            return resultCollection;
        }

        private void GetItemsFromKeywordInBankProperties(int bankId, string searchKeyWords, Guid testContextResourceId, string fullTextSearchString, EntityCollection resultCollection)
        {
            if (string.IsNullOrEmpty(searchKeyWords)) return;

            var freeValuePropertiesBucket = new RelationPredicateBucket();

            if (testContextResourceId != Guid.Empty)
            {
                freeValuePropertiesBucket.PredicateExpression.Add(new FieldCompareSetPredicate(
                    ItemResourceFields.ResourceId, null, DependentResourceFields.DependentResourceId, null,
                    SetOperator.In, DependentResourceFields.ResourceId == testContextResourceId));
            }

            freeValuePropertiesBucket.Relations.Add(ResourceEntity.Relations.CustomBankPropertyValueEntityUsingResourceId);
            var freeValueCustomBankPropertyValueRelation = freeValuePropertiesBucket.Relations.Add(
                CustomBankPropertyValueEntity.Relations.GetSubTypeRelation("FreeValueCustomBankPropertyValueEntity"), JoinHint.Inner);
            freeValueCustomBankPropertyValueRelation.CustomFilter = new PredicateExpression(
                new FieldFullTextSearchPredicate(FreeValueCustomBankPropertyValueFields.Value, null, FullTextSearchOperator.Contains, fullTextSearchString));

            var subResultFreeValue = GetItemsForBank(bankId, freeValuePropertiesBucket);
            AddSubResultItemCollectionToResultItemCollection(subResultFreeValue, resultCollection);

            var multiSelectPropertiesBucket = new RelationPredicateBucket();

            if (testContextResourceId != Guid.Empty)
            {
                multiSelectPropertiesBucket.PredicateExpression.Add(new FieldCompareSetPredicate(
                    ItemResourceFields.ResourceId, null, DependentResourceFields.DependentResourceId, null,
                    SetOperator.In, DependentResourceFields.ResourceId == testContextResourceId));
            }

            multiSelectPropertiesBucket.Relations.Add(ResourceEntity.Relations.CustomBankPropertyValueEntityUsingResourceId);
            multiSelectPropertiesBucket.Relations.Add(CustomBankPropertyValueEntity.Relations.GetSubTypeRelation("ListCustomBankPropertyValueEntity"), JoinHint.Left);
            multiSelectPropertiesBucket.Relations.Add(ListCustomBankPropertyValueEntity.Relations.ListCustomBankPropertySelectedValueEntityUsingCustomBankPropertyIdResourceId, JoinHint.Left);
            var listCustomBankPropertySelectedValueRelation = multiSelectPropertiesBucket.Relations.Add(
                ListCustomBankPropertySelectedValueEntity.Relations.ListValueCustomBankPropertyEntityUsingListValueBankCustomPropertyId, JoinHint.Inner);
            listCustomBankPropertySelectedValueRelation.CustomFilter = new PredicateExpression(
                new FieldFullTextSearchPredicate(new IEntityField2[]
                {
                    ListValueCustomBankPropertyFields.Name,
                    ListValueCustomBankPropertyFields.Title
                }, FullTextSearchOperator.Contains, fullTextSearchString));

            var subResultMultiValue = GetItemsForBank(bankId, multiSelectPropertiesBucket);
            AddSubResultItemCollectionToResultItemCollection(subResultMultiValue, resultCollection);

            var otherMetaDataBucket = new RelationPredicateBucket();
            var filters = new PredicateExpression();

            if (testContextResourceId != Guid.Empty)
            {
                otherMetaDataBucket.PredicateExpression.Add(new FieldCompareSetPredicate(
                    ItemResourceFields.ResourceId, null, DependentResourceFields.DependentResourceId, null,
                    SetOperator.In, DependentResourceFields.ResourceId == testContextResourceId));
            }
            DateTime searchForDate;
            if (DateTime.TryParse(searchKeyWords, out searchForDate) &&
                searchForDate >= SqlDateTime.MinValue.Value && searchForDate <= SqlDateTime.MaxValue.Value)
            {
                var searchFromDate = new DateTime(searchForDate.Year, searchForDate.Month, searchForDate.Day, 0, 0,
    0);
                var searchToDate = new DateTime(searchForDate.Year, searchForDate.Month, searchForDate.Day, 23, 59,
                    59);
                filters.Add(new FieldBetweenPredicate(ResourceFields.CreationDate, null, searchFromDate, searchToDate));
                filters.AddWithOr(new FieldBetweenPredicate(ResourceFields.ModifiedDate, null, searchFromDate, searchToDate));
            }

            var createdByRelation = otherMetaDataBucket.Relations.Add(ResourceEntity.Relations.UserEntityUsingCreatedBy, "CreatedBy", JoinHint.Inner);
            filters.AddWithOr(UserFields.FullName.SetObjectAlias("CreatedBy") == searchKeyWords);

            var modifiedByRelation = otherMetaDataBucket.Relations.Add(ResourceEntity.Relations.UserEntityUsingModifiedBy, "ModifiedBy", JoinHint.Inner);
            filters.AddWithOr(UserFields.FullName.SetObjectAlias("ModifiedBy") == searchKeyWords);

            var fields = new List<EntityField2> { ResourceFields.Name, ResourceFields.Title, ResourceFields.Description };
            filters.AddWithOr(new FieldFullTextSearchPredicate(fields, FullTextSearchOperator.Contains, fullTextSearchString));

            otherMetaDataBucket.PredicateExpression.Add(filters);
            var subResultOtherMetaData = GetItemsForBank(bankId, otherMetaDataBucket);
            AddSubResultItemCollectionToResultItemCollection(subResultOtherMetaData, resultCollection);
        }

        private void GetItemsFromKeywordInItemText(int bankId, Guid testContextResourceId, string fullTextSearchString, EntityCollection resultCollection)
        {
            var itemTextBucket = new RelationPredicateBucket();

            if (testContextResourceId != Guid.Empty)
            {
                itemTextBucket.PredicateExpression.Add(new FieldCompareSetPredicate(ItemResourceFields.ResourceId,
                    null, DependentResourceFields.DependentResourceId, null, SetOperator.In,
                    DependentResourceFields.ResourceId == testContextResourceId));
            }

            IPredicate itemTextPredicate = new FieldFullTextSearchPredicate(ResourceDataFields.BinData, null,
FullTextSearchOperator.Contains, fullTextSearchString);

            itemTextBucket.Relations.Add(ResourceEntity.Relations.ResourceDataEntityUsingResourceId);
            itemTextBucket.PredicateExpression.Add(itemTextPredicate);

            var subResultItemText = GetItemsForBank(bankId, itemTextBucket);
            AddSubResultItemCollectionToResultItemCollection(subResultItemText, resultCollection);
        }


        private void AddSubResultItemCollectionToResultItemCollection(EntityCollection subResultCollection,
    EntityCollection resultCollection)
        {
            foreach (ItemResourceEntity subResult in subResultCollection)
            {
                var found = false;
                foreach (ItemResourceEntity item in resultCollection)
                    if (item.ResourceId == subResult.ResourceId)
                    {
                        found = true;
                        break;
                    }

                if (!found)
                    resultCollection.Add(subResult);
            }
        }



        public AssessmentTestResourceEntity GetAssessmentTest(AssessmentTestResourceEntity test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test));

            var fetchedResource = new AssessmentTestResourceEntity(test.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithCustomProperties = true };
            PrefetchResource(fetchedResource, EntityType.AssessmentTestResourceEntity, request);

            return fetchedResource;
        }

        public EntityCollection GetAssessmentTestsForBank(int bankId)
        {
            return GetAssessmentTestsForBank(bankId, true, false, true);
        }

        public EntityCollection GetAssessmentTestsForBank(int bankId, bool includeParentBanks, bool includeChildBanks,
    bool withCustomProperties)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.AssessmentTestResourceEntity));

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            if (withCustomProperties)
            {
                var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
                path1.SubPath.Add(ListCustomBankPropertyValueEntity
                    .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);
            }

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(AssessmentTestResourceFields.IsTemplate == false);

            return GetResourceCollection(bankId, new AssessmentTestResourceEntityFactory(),
    AssessmentTestResourceFields.BankId, prefetchPath, filter, includeParentBanks, includeChildBanks, 0);
        }

        public EntityCollection GetAssessmentTestTemplatesForBank(int bankId)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.AssessmentTestResourceEntity));

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(AssessmentTestResourceFields.IsTemplate == true);

            return GetResourceCollection(bankId, new AssessmentTestResourceEntityFactory(),
    AssessmentTestResourceFields.BankId, prefetchPath, filter);
        }



        public EntityCollection GetTestPackagesForBank(int bankId)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.AssessmentTestResourceEntity));

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            return GetResourceCollection(bankId, new TestPackageResourceEntityFactory(),
    TestPackageResourceFields.BankId, prefetchPath, null);
        }

        public TestPackageResourceEntity GetTestPackage(TestPackageResourceEntity testPackage)
        {
            if (testPackage == null)
                throw new ArgumentNullException(nameof(testPackage));

            var fetchedResource = new TestPackageResourceEntity(testPackage.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true };
            PrefetchResource(fetchedResource, EntityType.TestPackageResourceEntity, request);

            return fetchedResource;
        }



        public EntityCollection GetPackagesForBank(int bankId)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.PackageResourceEntity));

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            return GetResourceCollection(bankId, new PackageResourceEntityFactory(), PackageResourceFields.BankId,
    prefetchPath, null);
        }


        public PackageResourceEntity GetPackage(PackageResourceEntity package)
        {
            if (package == null)
                throw new ArgumentNullException(nameof(package));

            var fetchedResource = new PackageResourceEntity(package.ResourceId);
            PrefetchResource(fetchedResource, EntityType.PackageResourceEntity);

            return fetchedResource;
        }



        public ItemLayoutTemplateResourceEntity GetItemLayoutTemplate(
    ItemLayoutTemplateResourceEntity itemLayoutTemplate)
        {
            if (itemLayoutTemplate == null)
                throw new ArgumentNullException(nameof(itemLayoutTemplate));

            var fetchedResource = new ItemLayoutTemplateResourceEntity(itemLayoutTemplate.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithHiddenResources = true };
            PrefetchResource(fetchedResource, EntityType.ItemLayoutTemplateResourceEntity, request);

            return fetchedResource;
        }

        public EntityCollection GetItemLayoutTemplatesForBank(int bankId)
        {
            IRelationPredicateBucket bucket = new RelationPredicateBucket();
            return GetItemLayoutTemplates(bankId, bucket);
        }

        public EntityCollection GetItemLayoutTemplatesForBankWithItemTypeFilter(int bankId,
    List<ItemTypeEnum> itemTypes, bool exclude)
        {
            if (!itemTypes.Any())
            {
                return null;
            }

            var filterPredicate = new PredicateExpression();
            if (exclude)
                itemTypes.ToList().ForEach(type => filterPredicate.AddWithAnd(
                    ItemLayoutTemplateResourceFields.ItemType !=
                    Enum.GetName(typeof(ItemTypeEnum), type)));
            else
                itemTypes.ToList().ForEach(type => filterPredicate.AddWithAnd(
                    ItemLayoutTemplateResourceFields.ItemType ==
                    Enum.GetName(typeof(ItemTypeEnum), type)));
            IRelationPredicateBucket bucket = new RelationPredicateBucket();
            bucket.PredicateExpression.Add(filterPredicate);
            return GetItemLayoutTemplates(bankId, bucket);
        }

        public EntityCollection GetItemLayoutTemplatesForBankWithListOfNamesFilter(int bankId, List<string> iltNames)
        {
            if (!iltNames.Any())
            {
                return null;
            }

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(
                new PredicateExpression(ItemLayoutTemplateResourceFields.Name == iltNames.ToArray()));
            return GetItemLayoutTemplates(bankId, filter);
        }

        public Dictionary<string, int> GetControlTemplatesForBankList(int bankId, bool checkCompleteBankHierarchy)
        {
            var returnValue = new Dictionary<string, int>();
            int[] ids;

            if (checkCompleteBankHierarchy)
            {
                var bankBranchHelper = new BankBranchIdHelper(PermissionService);
                ids = bankBranchHelper.GetBankBrancheIds(bankId, TestBuilderPermissionTarget.Any,
                    TestBuilderPermissionAccess.AnyTask);
            }
            else
            {
                ids = new[] { bankId };
            }

            var bucket = new RelationPredicateBucket();
            var with4 = bucket;
            with4.PredicateExpression.Add(new FieldCompareRangePredicate(ResourceFields.BankId, null, ids));

            var fields = new ResultsetFields(2);
            fields.DefineField(ControlTemplateResourceFields.Name, 0, "ControlTemplateName");
            fields.DefineField(ControlTemplateResourceFields.BankId, 1, "BankId");

            var dynamicList = new DataTable();
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchTypedList(fields, dynamicList, bucket, 0, null, true);
            }

            foreach (DataRow row in dynamicList.Rows)
            {
                if (!returnValue.ContainsKey(row["ControlTemplateName"].ToString()))
                {
                    returnValue.Add(row["ControlTemplateName"].ToString(), Convert.ToInt32(row["BankId"].ToString()));
                }
            }
            return returnValue;
        }

        public Dictionary<string, int> GetItemLayoutTemplatesForBankList(int bankId, bool checkCompleteBankHierarchy)
        {
            var returnValue = new Dictionary<string, int>();
            int[] ids;

            if (checkCompleteBankHierarchy)
            {
                var bankBranchHelper = new BankBranchIdHelper(PermissionService);
                ids = bankBranchHelper.GetBankBrancheIds(bankId, TestBuilderPermissionTarget.Any,
                    TestBuilderPermissionAccess.AnyTask);
            }
            else
            {
                ids = new[] { bankId };
            }

            var bucket = new RelationPredicateBucket();
            var with5 = bucket;
            with5.PredicateExpression.Add(new FieldCompareRangePredicate(ResourceFields.BankId, null, ids));

            var fields = new ResultsetFields(2);
            fields.DefineField(ItemLayoutTemplateResourceFields.Name, 0, "ItemLayoutTemplateName");
            fields.DefineField(ItemLayoutTemplateResourceFields.BankId, 1, "BankId");

            var dynamicList = new DataTable();
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchTypedList(fields, dynamicList, bucket, 0, null, true);
            }

            foreach (DataRow row in dynamicList.Rows)
            {
                if (!returnValue.ContainsKey(row["ItemLayoutTemplateName"].ToString()))
                {
                    returnValue.Add(row["ItemLayoutTemplateName"].ToString(), Convert.ToInt32(row["BankId"].ToString()));
                }
            }
            return returnValue;
        }

        private EntityCollection GetItemLayoutTemplates(int bankId, IRelationPredicateBucket bucket)
        {
            IPrefetchPath2 prefetchPath =
    new PrefetchPath2(Convert.ToInt32(EntityType.ItemLayoutTemplateResourceEntity));
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);
            prefetchPath.Add(ResourceEntity.PrefetchPathHiddenResourceCollection);
            return GetResourceCollection(bankId, new ItemLayoutTemplateResourceEntityFactory(),
    ItemLayoutTemplateResourceFields.BankId, prefetchPath, bucket);
        }


        public List<string> GetItemLayoutTemplateSourceNamesForItemCodeList(int bankId, List<string> itemCodes)
        {
            var returnValue = new List<string>();
            if (!itemCodes.Any())
            {
                return returnValue;
            }

            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, TestBuilderPermissionTarget.Any,
                TestBuilderPermissionAccess.AnyTask);

            var itemOffset = 0;
            const int ITEM_MAX_BATCH = 1000;

            while (itemOffset != itemCodes.Count)
            {
                var itemsInThisBatch = ITEM_MAX_BATCH;
                if (itemCodes.Count - itemOffset < ITEM_MAX_BATCH)
                    itemsInThisBatch = itemCodes.Count - itemOffset;

                var batchItemList = (string[])Array.CreateInstance(typeof(string), itemsInThisBatch);
                itemCodes.CopyTo(itemOffset, batchItemList, 0, itemsInThisBatch);
                var bucket = new RelationPredicateBucket();
                var with6 = bucket;
                with6.PredicateExpression.Add(new FieldCompareRangePredicate(ResourceFields.Name, null, batchItemList));
                with6.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ResourceFields.BankId, null, ids));
                with6.Relations.Add(ItemLayoutTemplateResourceEntity.Relations.DependentResourceEntityUsingDependentResourceId);
                with6.Relations.Add(DependentResourceEntity.Relations.ResourceEntityUsingResourceId);

                var fields = new ResultsetFields(1);
                fields.DefineField(ItemLayoutTemplateResourceFields.Name, 0, "ItemLayoutTemplateName");

                IGroupByCollection groupByClause = new GroupByCollection();
                groupByClause.Add(fields["ItemLayoutTemplateName"]);

                var dynamicList = new DataTable();
                using (var adapter = new DataAccessAdapter())
                {
                    adapter.FetchTypedList(fields, dynamicList, bucket, 0, null, true, groupByClause);
                }

                foreach (DataRow row in dynamicList.Rows)
                {
                    if (!returnValue.Contains(row["ItemLayoutTemplateName"].ToString()))
                    {
                        returnValue.Add(row["ItemLayoutTemplateName"].ToString());
                    }
                }

                itemOffset += itemsInThisBatch;
            }

            return returnValue;
        }

        public EntityCollection GetItemLayoutTemplatesFromListOfResourceIds(IEnumerable<Guid> resourceIds,
            bool withDependencies)
        {
            var resources = new EntityCollection(new ItemLayoutTemplateResourceEntityFactory());

            IPrefetchPath2 prefetchPath =
    new PrefetchPath2(Convert.ToInt32(EntityType.ItemLayoutTemplateResourceEntity));


            prefetchPath.Add(ResourceEntity.PrefetchPathBank);
            if (withDependencies)
            {
                var depPath = prefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
                depPath.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
            }

            var filter = new RelationPredicateBucket();
            var fieldCompareRange = new FieldCompareRangePredicate(ItemLayoutTemplateResourceFields.ResourceId, null, resourceIds);
            filter.PredicateExpression.Add(fieldCompareRange);
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter, 0, null, prefetchPath);
            }
            return resources;
        }



        public ControlTemplateResourceEntity GetControlTemplate(ControlTemplateResourceEntity controlTemplate)
        {
            if (controlTemplate == null)
                throw new ArgumentNullException(nameof(controlTemplate));

            var fetchedResource = new ControlTemplateResourceEntity(controlTemplate.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithHiddenResources = true };
            PrefetchResource(fetchedResource, EntityType.ControlTemplateResourceEntity, request);

            return fetchedResource;
        }

        public EntityCollection GetControlTemplatesForBank(int bankId)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ControlTemplateResourceEntity));

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            return GetResourceCollection(bankId, new ControlTemplateResourceEntityFactory(),
    ControlTemplateResourceFields.BankId, prefetchPath, null);
        }



        public GenericResourceEntity GetGenericResource(GenericResourceEntity genericResource)
        {
            if (genericResource == null)
                throw new ArgumentNullException(nameof(genericResource));

            var fetchedResource = new GenericResourceEntity(genericResource.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithCustomProperties = true };
            PrefetchResource(fetchedResource, EntityType.GenericResourceEntity, request);

            return fetchedResource;
        }

        public EntityCollection GetGenericResourceForBank(
    int bankId,
    string filter,
    string filePrefix,
    bool templatesOnly)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.GenericResourceEntity));

            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity.PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            IRelationPredicateBucket bucket = null;

            if (!string.IsNullOrEmpty(filter))
            {
                var filterArray = filter.Split('|');
                var filterPredicate = new PredicateExpression();
                foreach (var filterValue in filterArray)
                {
                    if (bucket == null)
                    {
                        bucket = new RelationPredicateBucket();
                    }
                    filterPredicate.AddWithOr(GenericResourceFields.MediaType % (filterValue + "%"));
                }
                bucket?.PredicateExpression.Add(filterPredicate);
            }

            if (templatesOnly)
            {
                if (bucket == null)
                {
                    bucket = new RelationPredicateBucket();
                }
                bucket.PredicateExpression.AddWithAnd(GenericResourceFields.IsTemplate == true);
            }

            if (!string.IsNullOrEmpty(filePrefix))
            {
                if (bucket == null)
                {
                    bucket = new RelationPredicateBucket();
                }
                bucket.PredicateExpression.Add(GenericResourceFields.Name % (filePrefix + "%"));
            }

            return GetResourceCollection(bankId, new GenericResourceEntityFactory(), GenericResourceFields.BankId,
    prefetchPath, bucket);
        }



        public EntityCollection GetAvailableStates()
        {
            var states = new EntityCollection(new StateEntityFactory());

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(states, null);
            }

            return states;
        }

        public StateEntity GetState(int stateId)
        {
            var stateToFetch = new StateEntity(stateId);

            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.StateEntity));

            prefetchPath.Add(StateEntity.PrefetchPathStateActionCollection).SubPath
    .Add(StateActionEntity.PrefetchPathAction);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(stateToFetch, prefetchPath);
            }

            return stateToFetch;
        }

        public ActionEntity GetStateAction(int stateId, string target)
        {
            var state = GetState(stateId);

            if (state != null)
            {
                foreach (var stateAction in state.StateActionCollection)
                    if (stateAction.Target.Equals(target, StringComparison.InvariantCultureIgnoreCase))
                        return stateAction.Action;
                return null;
            }
            return null;
        }

        public ActionEntity GetStateAction(int bankId, string resourceName, string target)
        {
            var resource = default(ResourceEntity);
            resource = GetResourceByNameWithOption(bankId, resourceName, new ResourceRequestDTO());

            if (resource != null && resource.StateId.HasValue)
            {
                var stateId = resource.StateId.Value;
                return GetStateAction(stateId, target);
            }
            return null;
        }

        public IDictionary<string, ActionEntity> GetStateActions(int bankId, string[] resourceNames, string target)
        {
            Dictionary<string, ActionEntity> returnValue = null;

            var stateCollection = GetAllStates();

            var resources = GetAllStatesFromResources(resourceNames, bankId);
            if (resources != null)
            {
                returnValue = new Dictionary<string, ActionEntity>();
                foreach (ResourceEntity resource in resources)
                {
                    var action = GetStateActionNameFromResource(stateCollection, resource, target);
                    returnValue.Add(resource.Name, action);
                }
            }
            return returnValue;
        }



        private ActionEntity GetStateActionNameFromResource(EntityCollection stateCollection, ResourceEntity resource,
    string target)
        {
            if (!resource.StateId.HasValue)
                return null;

            IPredicate filter = StateFields.StateId == resource.StateId.Value;

            var indexes = stateCollection.FindMatches(filter);
            if (indexes.Count == 1)
            {
                var state = (StateEntity)stateCollection[indexes[0]];
                if (state == null)
                    return null;

                foreach (var stateAction in state.StateActionCollection)
                    if (stateAction.Target.Equals(target, StringComparison.InvariantCultureIgnoreCase))
                        return stateAction.Action;
            }
            return null;
        }

        private EntityCollection GetAllStates()
        {
            var states = new EntityCollection(new StateEntityFactory());

            using (var adapter = new DataAccessAdapter())
            {
                IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.StateEntity));

                prefetchPath.Add(StateEntity.PrefetchPathStateActionCollection).SubPath
    .Add(StateActionEntity.PrefetchPathAction);

                adapter.FetchEntityCollection(states, null, prefetchPath);
            }

            return states;
        }

        private ResourceEntity GetResourceByName(int[] bankIds, string name, ResourceRequestDTO request)
        {
            ResourceEntity returnValue = null;
            if (String.IsNullOrEmpty(name))
            {
                return returnValue;
            }

            var resources = new EntityCollection(new ResourceEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();

            var _with7 = filter;
            _with7.PredicateExpression.Add(ResourceFields.Name == name);

            _with7.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ResourceFields.BankId, null, bankIds));

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter, 1, null,
                    GetResourcePrefetchPath(request));

                if (resources.Count == 1)
                {
                    var fetchedEntity = (ResourceEntity)resources[0];
                    if (request.WithDependencies)
                        FetchDependentResourceEntitiesOfResource(fetchedEntity, adapter);

                    returnValue = fetchedEntity;
                }
            }

            return returnValue;
        }

        private EntityCollection GetResourcesByNames(int[] bankIds, List<string> names, ResourceRequestDTO request)
        {
            var resources = new EntityCollection(new ResourceEntityFactory());
            if (!names.Any())
            {
                return resources;
            }

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var with8 = filter;
            var index = 0;
            IPredicateExpression a = new PredicateExpression();
            foreach (var name in names)
            {
                if (index == 0)
                    a.Add(ResourceFields.Name == name);
                else
                    a.AddWithOr(ResourceFields.Name == name);
                index += 1;
            }
            with8.PredicateExpression.Add(a);
            with8.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ResourceFields.BankId, null, bankIds));

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter, 0, null,
                    GetResourcePrefetchPath(request));
            }

            return resources;
        }

        private EntityCollection GetAllStatesFromResources(string[] resourceNames, int bankId)
        {
            var returnValue = new EntityCollection(new ResourceEntityFactory());
            if (!resourceNames.Any())
            {
                return returnValue;
            }

            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, false, TestBuilderPermissionTarget.Any,
                TestBuilderPermissionAccess.AnyTask);

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var with9 = filter;
            with9.PredicateExpression.Add(new FieldCompareRangePredicate(ResourceFields.Name, null, resourceNames));

            with9.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ResourceFields.BankId, null, ids));

            using (var adapter = new DataAccessAdapter())
            {
                var includedFields = new IncludeFieldsList();
                includedFields.Add(ResourceFields.Name);
                includedFields.Add(ResourceFields.StateId);
                adapter.FetchEntityCollection(returnValue, includedFields, filter);
            }
            return returnValue;
        }

        private void PrefetchResource(ResourceEntity resource, EntityType type)
        {
            PrefetchResource(resource, type, new ResourceRequestDTO() { WithCustomProperties = true, WithDependencies = true, WithHiddenResources = true, WithReferences = true, WithState = true, WithUserInfo = true });
        }

        private void PrefetchResource(ResourceEntity resource, EntityType type, ResourceRequestDTO request)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(type));
            if (request.WithUserInfo)
            {
                prefetchPath.Add(ResourceEntity.PrefetchPathCreatedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                prefetchPath.Add(ResourceEntity.PrefetchPathModifiedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
            }

            if (request.WithReferences)
                prefetchPath.Add(ResourceEntity.PrefetchPathReferencedResourceCollection);

            if (request.WithHiddenResources)
                prefetchPath.Add(ResourceEntity.PrefetchPathHiddenResourceCollection);

            if (request.WithCustomProperties)
            {
                GetFullCustomBankPropertiesPrefetchPath(prefetchPath);
            }

            if (request.WithState)
            {
                prefetchPath.Add(ResourceEntity.PrefetchPathState,
                        _excludeIncludeFieldsHelper.GetStateExcludedFieldList())
                    .SubPath.Add(StateEntity.PrefetchPathStateActionCollection).SubPath
                    .Add(StateActionEntity.PrefetchPathAction);
            }

            if (request.WithDependencies)
            {
                var depResourcePrefethPath = ResourceEntity.PrefetchPathDependentResourceCollection;
                depResourcePrefethPath.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
                prefetchPath.Add(depResourcePrefethPath);
            }

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(resource, prefetchPath);
            }
        }

        private static void GetFullCustomBankPropertiesPrefetchPath(IPrefetchPath2 prefetchPath)
        {
            var customBankPropertyPath = prefetchPath.Add(ResourceEntity.PrefetchPathBank);
            customBankPropertyPath.SubPath.Add(BankEntity.PrefetchPathCustomBankPropertyCollection);

            var customBankPropertyValuesPath =
                prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                .PrefetchPathTreeStructureCustomBankProperty);
            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                .PrefetchPathTreeStructureCustomBankPropertySelectedPartCollection);
            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                .PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart);
            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                .PrefetchPathConceptStructureCustomBankProperty);
            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                .PrefetchPathConceptStructureCustomBankPropertySelectedPartCollection);
            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                .PrefetchPathConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart);
            customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListCustomBankPropertySelectedValueCollection);
            customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);
            customBankPropertyValuesPath.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);

        }

        private EntityCollection GetResourceCollection(int bankId, IEntityFactory2 factory, EntityField2 fld,
    IPrefetchPath2 prefetchPath, IRelationPredicateBucket filter)
        {
            return GetResourceCollection(bankId, factory, fld, prefetchPath, filter, true, 0);
        }

        private EntityCollection GetResourceCollection(int bankId, IEntityFactory2 factory, EntityField2 fld,
            IPrefetchPath2 prefetchPath, IRelationPredicateBucket filter, bool fetchCompleteBranch, int maxRecords)
        {
            return GetResourceCollection(bankId, factory, fld, prefetchPath, filter, fetchCompleteBranch, false,
                maxRecords);
        }

        private EntityCollection GetResourceCollection(
    int bankId,
    IEntityFactory2 factory,
    EntityField2 fld,
    IPrefetchPath2 prefetchPath,
    IRelationPredicateBucket filter,
    bool includeParentBanks,
    bool includeChildBanks,
    int maxRecords)
        {
            var fetchTarget = ContentModelObjectToPermissionTarget(factory);

            prefetchPath?.Add(ResourceEntity.PrefetchPathReferencedResourceCollection);

            int[] ids = null;

            if ((includeParentBanks || includeChildBanks) && _fetchListWithCompleteBank)
            {
                var bankBranchHelper = new BankBranchIdHelper(PermissionService);
                ids = bankBranchHelper.GetBankBrancheIds(bankId, includeParentBanks, includeChildBanks, fetchTarget,
                    TestBuilderPermissionAccess.DALRead);
            }
            else
            {
                ids = new[] { bankId };
            }

            var resources = new EntityCollection(factory);


            if (ids.Length <= 0)
                return resources;

            using (var adapter = new DataAccessAdapter())
            {
                IRelationPredicateBucket filtr = filter;
                if (filter == null)
                {
                    filtr = new RelationPredicateBucket();
                }

                var fieldCompareRange = new FieldCompareRangePredicate(fld, null, ids);
                filtr.PredicateExpression.Add(fieldCompareRange);

                if (prefetchPath == null)
                {
                    adapter.FetchEntityCollection(resources, filtr);
                }
                else
                {
                    prefetchPath
    .Add(ResourceEntity.PrefetchPathState,
        _excludeIncludeFieldsHelper.GetStateExcludedFieldList()).SubPath
    .Add(StateEntity.PrefetchPathStateActionCollection).SubPath
    .Add(StateActionEntity.PrefetchPathAction);

                    prefetchPath.Add(ResourceEntity.PrefetchPathCreatedByUser,
    _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                    prefetchPath.Add(ResourceEntity.PrefetchPathModifiedByUser,
                        _excludeIncludeFieldsHelper.GetUserIncludedFieldList());

                    adapter.FetchEntityCollection(resources, filtr, maxRecords, null, prefetchPath);
                }
            }

            return resources;
        }

        private void FetchDependentResourceEntitiesOfResource(ResourceEntity resource, DataAccessAdapter adapter)
        {
            if (!resource.DependentResourceCollection.Any())
            {
                return;
            }

            var dependentResources = new EntityCollection(new ResourceEntityFactory());

            var fetchTheseResources = new List<Guid>();
            foreach (var dresource in resource.DependentResourceCollection)
            {
                fetchTheseResources.Add(dresource.DependentResourceId);
            }

            var valueProjectors = new List<IDataValueProjector>();
            var fields = EntityFieldsFactory.CreateEntityFieldsObject(EntityType.ResourceEntity);
            var resultFields = new ResultsetFields(fields.Count);
            foreach (IEntityField2 fld in fields)
            {
                valueProjectors.Add(new DataValueProjector(fld.Name, fld.FieldIndex, fld.DataType));
                resultFields.DefineField(fld, fld.FieldIndex);
            }

            var projector = new DataProjectorToIEntityCollection2(dependentResources);
            var filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(
                new FieldCompareRangePredicate(ResourceFields.ResourceId, null, fetchTheseResources.ToArray()));
            adapter.FetchProjection(valueProjectors, projector, resultFields, filter, 0, true);

            foreach (var depResource in resource.DependentResourceCollection)
            {
                var resultIndex = dependentResources.FindMatches(ResourceFields.ResourceId == depResource.DependentResourceId);
                if (resultIndex.Count == 1)
                {
                    dependentResources[resultIndex[0]].IsNew = false;
                    dependentResources[resultIndex[0]].IsDirty = false;
                    depResource.DependentResource = (ResourceEntity)dependentResources[resultIndex[0]];
                }
            }
        }

        private TestBuilderPermissionTarget ContentModelObjectToPermissionTarget(object resourceObject)
        {
            if (resourceObject is AssessmentTestResourceEntity || resourceObject is AssessmentTestResourceEntityFactory)
                return TestBuilderPermissionTarget.TestEntity;
            if (resourceObject is ControlTemplateResourceEntity ||
                resourceObject is ControlTemplateResourceEntityFactory)
                return TestBuilderPermissionTarget.ControlTemplateEntity;
            if (resourceObject is GenericResourceEntity || resourceObject is GenericResourceEntityFactory)
                return TestBuilderPermissionTarget.MediaEntity;
            if (resourceObject is ItemResourceEntity || resourceObject is ItemResourceEntityFactory)
                return TestBuilderPermissionTarget.ItemEntity;
            if (resourceObject is ItemLayoutTemplateResourceEntity ||
                resourceObject is ItemLayoutTemplateResourceEntityFactory)
                return TestBuilderPermissionTarget.ItemLayoutTemplateEntity;
            if (resourceObject is AspectResourceEntity || resourceObject is AspectResourceEntityFactory)
                return TestBuilderPermissionTarget.AspectEntity;
            if (resourceObject is DataSourceResourceEntity || resourceObject is DataSourceResourceEntityFactory)
                return TestBuilderPermissionTarget.DataSourceEntity;
            if (resourceObject is TestPackageResourceEntity || resourceObject is TestPackageResourceEntityFactory)
                return TestBuilderPermissionTarget.TestPackageEntity;
            if (resourceObject is ResourceEntity || resourceObject is ResourceEntityFactory)
                return TestBuilderPermissionTarget.Any;

            Debug.Assert(true, "resourceType is not handled!");

            return TestBuilderPermissionTarget.None;
        }



        public AspectResourceEntity GetAspect(AspectResourceEntity aspect)
        {
            if (aspect == null)
                throw new ArgumentNullException(nameof(aspect));

            var fetchedResource = new AspectResourceEntity(aspect.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true };
            PrefetchResource(fetchedResource, EntityType.AspectResourceEntity, request);

            return fetchedResource;
        }

        public EntityCollection GetAspectsForBank(int bankId)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity.PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            var path2 = path1.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);

            path2.SubPath.Add(ListCustomBankPropertyEntity.PrefetchPathListValueCustomBankPropertyCollection);

            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            return GetResourceCollection(bankId, new AspectResourceEntityFactory(), AspectResourceFields.BankId, prefetchPath, null);
        }

        public string UpdateAspectResource(AspectResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        public string UpdateAspectResource(AspectResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }




        public DataSourceResourceEntity GetDataSource(DataSourceResourceEntity dataSource)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }

            var fetchedResource = new DataSourceResourceEntity(dataSource.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true };
            PrefetchResource(fetchedResource, EntityType.DataSourceResourceEntity, request);

            return fetchedResource;
        }

        public EntityCollection GetDataSourcesForBank(int bankId, bool? isTemplate, params string[] behaviours)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.DataSourceResourceEntity));
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);
            prefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            IRelationPredicateBucket filter = null;

            if (isTemplate.HasValue)
            {
                filter = new RelationPredicateBucket();

                var isTemplatePredicate = DataSourceResourceFields.IsTemplate == isTemplate.Value;
                filter.PredicateExpression.Add(isTemplatePredicate);
            }

            if (behaviours != null && behaviours.Length > 0)
            {
                if (filter == null)
                {
                    filter = new RelationPredicateBucket();
                }

                var behaviourPredicate = new PredicateExpression(DataSourceResourceFields.DataSourceType == behaviours[0]);
                for (var i = 1; i <= behaviours.Length - 1; i++)
                {
                    behaviourPredicate = (PredicateExpression)behaviourPredicate.AddWithOr(new PredicateExpression(DataSourceResourceFields.DataSourceType == behaviours[i]));
                }

                filter.PredicateExpression.Add(behaviourPredicate);
            }

            var col = GetResourceCollection(bankId, new DataSourceResourceEntityFactory(), DataSourceResourceFields.BankId, prefetchPath, filter);

            return col;
        }

        public string UpdateDataSourceResource(DataSourceResourceEntity resource)
        {
            return UpdateResource(resource);
        }


        public string UpdateDataSourceResource(DataSourceResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        public ResourceEntity GetResourceByNameWithOption(int bankId, string name, ResourceRequestDTO request)
        {
            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, TestBuilderPermissionTarget.Any, TestBuilderPermissionAccess.AnyTask);
            return GetResourceByName(ids, name, request);
        }

        public EntityCollection GetResourcesByNamesWithOption(int bankId, List<string> names, ResourceRequestDTO request)
        {
            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, TestBuilderPermissionTarget.Any, TestBuilderPermissionAccess.AnyTask);
            return GetResourcesByNames(ids, names, request);
        }

        public ResourceEntity GetResourceByIdWithOption(
            Guid resourceId,
            ResourceRequestDTO request
           )
        {
            return GetResourcesByIdsWithOption(new List<Guid>(new[] { resourceId }), request).OfType<ResourceEntity>()
                .FirstOrDefault();
        }

        public ResourceEntity GetResourceByIdWithOption(
            Guid resourceId,
            IEntityFactory2 factory,
            ResourceRequestDTO request
        )
        {
            return GetResourcesByIdsWithOption(new List<Guid>(new[] { resourceId }), factory, request).OfType<ResourceEntity>()
                .FirstOrDefault();
        }

        public EntityCollection GetResourcesByIdsWithOption(
            List<Guid> resourceIds,
            ResourceRequestDTO request)
        {
            return GetResourcesByIdsWithOption(resourceIds, new ResourceEntityFactory(), request);
        }

        public EntityCollection GetResourcesByIdsWithOption(
            List<Guid> resourceIds,
            IEntityFactory2 factory,
            ResourceRequestDTO request)
        {
            var resources = new EntityCollection(factory);
            if (resourceIds.Count == 0)
            {
                return resources;
            }

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var with10 = filter;
            var index = 0;
            foreach (var resourceId in resourceIds)
            {
                if (index == 0)
                {
                    with10.PredicateExpression.Add(ResourceFields.ResourceId == resourceId);
                }
                else
                {
                    with10.PredicateExpression.AddWithOr(ResourceFields.ResourceId == resourceId);
                }
                index += 1;
            }
            using (var adapter = new DataAccessAdapter())
            {
                var resourcePrefetchPath = GetResourcePrefetchPath(request);
                if (request.WithDependencies)
                {
                    AddDependentResourcePrefetchPath(ref resourcePrefetchPath);
                }
                if (request.WithUserInfo)
                {
                    AddUserResourcePrefetchPath(ref resourcePrefetchPath);
                }
                if (request.WithReferences)
                {
                    var refResourcePrefethPath = ResourceEntity.PrefetchPathReferencedResourceCollection;
                    refResourcePrefethPath.SubPath.Add(DependentResourceEntity.PrefetchPathResource);
                    resourcePrefetchPath.Add(refResourcePrefethPath);
                }
                if (request.WithState)
                {
                    resourcePrefetchPath.Add(ResourceEntity.PrefetchPathState, _excludeIncludeFieldsHelper.GetStateExcludedFieldList());
                }
                if (request.WithHiddenResources)
                {
                    resourcePrefetchPath.Add(ResourceEntity.PrefetchPathHiddenResourceCollection);
                }
                adapter.FetchEntityCollection(resources, filter, resourcePrefetchPath);
            }
            return resources;
        }

    }
}