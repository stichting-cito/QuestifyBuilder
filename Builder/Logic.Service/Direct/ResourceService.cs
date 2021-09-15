
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

        #region " Constructors "

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceService" /> class.
        /// </summary>
        public ResourceService()
        {
            _fetchListWithCompleteBank = true;
            PermissionService = PermissionFactory.Instance;
            _excludeIncludeFieldsHelper = new ExcludeIncludeFieldsHelper();
        }


        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceService" /> class.
        /// </summary>
        /// <param name="permissionServiceOverride">The permission service override.</param>
        /// <param name="fetchListWithCompleteBank"></param>
        public ResourceService(IPermissionService permissionServiceOverride, bool fetchListWithCompleteBank)
        {
            PermissionService = permissionServiceOverride;
            _fetchListWithCompleteBank = fetchListWithCompleteBank;
            _excludeIncludeFieldsHelper = new ExcludeIncludeFieldsHelper();
        }

        #endregion

        #region " Deletion "

        /// <summary>
        ///     Deletes resources from the database
        /// </summary>
        /// <param name="resourcesToDelete">The resource.</param>
        /// <param name="notDeletedResources"></param>
        public string DeleteResources(EntityCollection resourcesToDelete, ref EntityCollection notDeletedResources)
        {
            var checkedPermissionTargetsPerBank = new Dictionary<int, List<Tuple<TestBuilderPermissionTarget, bool>>>();
            //The Key (Integer) contains the bankId, the Boolean in the Tuple indicates if the user has sufficient permissions
            var errorBuilder = new StringBuilder();
            var errorBuilderValidationException = new StringBuilder();
            var errorBuilderSecurityException = new StringBuilder();

            // because of locking issues we now delete each entity seperately. Within the delete functionality the ValidateEntityBeforeDelete
            // function checks for dependent resources and instantiates his own adapter for it. The second call (2nd entity to delete in the transaction) 
            // times out cause the first call has locked the table.
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

                    //Add to dictionary, so that permission doesn't have to be determined for the next resource of the same type and in the same bank
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
                //Permission for this type of resource has already been determined
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

        #endregion

        #region "Resources"

        private bool ResourceExistsInBankHierarchy(int anchorBankId, Guid resourceId, bool checkInHierarchy, IEntityFactory2 factory)
        {
            // Collection to hold result
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

            // Setup filter to query resources where ResourceFields.id = resourceId and ResourceFields.BankId in ids.
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

        /// <summary>
        ///     Checks if a resource with the given name exists somewhere within the bank hierarchy of which
        ///     anchorBank is a member.
        /// </summary>
        /// <param name="anchorBankId">The bank to start at.</param>
        /// <param name="resourceName">The name of resource for which to check for existence.</param>
        /// <returns>True if the resource exists, otherwise false</returns>
        public bool ResourceExists(int anchorBankId, string resourceName, bool checkInHierarchy, IEntityFactory2 factory)
        {
            // Collection to hold result
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

            // Setup filter to query resources where ResourceFields.Name = resourceName and ResourceFields.BankId in ids.
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

        /// <summary>
        ///     Return the ResourceEntity prefetch path as to be used by GetResourceByName and GetResourceById.
        ///     Was extracted, with no further modifications, from GetResourceById because of the introduction of the
        ///     GetResourceById function.
        /// </summary>
        private PrefetchPath2 GetResourcePrefetchPath(ResourceRequestDTO request)
        {
            // add prefetch path to join dependent resources
            var resourcePrefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));
            resourcePrefetchPath.Add(ResourceEntity.PrefetchPathBank);
            if (request.WithDependencies)
                resourcePrefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);

            if (!request.WithCustomProperties) return resourcePrefetchPath;

            // Add prefetch path to join customerpropertiescollection of the resource
            var customBankPropertyValuesPath =
                resourcePrefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListCustomBankPropertySelectedValueCollection);
            customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            // add prefetch path to join ListCustomBankProperty
            var listCustomBankPath =
                customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                    .PrefetchPathListCustomBankProperty);
            // add prefetch path to add listvalues for customproperties of type 'ListCustomBankPropertyEntity'
            listCustomBankPath.SubPath.Add(ListCustomBankPropertyEntity
                .PrefetchPathListValueCustomBankPropertyCollection);

            // add prefetch path to join FreeValueCustomBankProperty
            customBankPropertyValuesPath.SubPath.Add(CustomBankPropertyValueEntity
                .PrefetchPathCustomBankProperty);
            // add prefetch path to join RichTextValueCustomBankProperty
            customBankPropertyValuesPath.SubPath.Add(RichTextValueCustomBankPropertyValueEntity
                .PrefetchPathRichTextValueCustomBankProperty);

            // add prefetch path to join ConceptStructureCustomBankPropertySelectedPartEntity info.
            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                .PrefetchPathConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart);
            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                .PrefetchPathConceptStructureCustomBankPropertySelectedPartCollection);
            // add prefetch path to join ConceptStructureCustomBankProperty
            var conceptStructureCustomBankPath =
                customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                    .PrefetchPathConceptStructureCustomBankProperty);
            // add prefetch path to add concept structure parts for customproperties of type 'ConceptStructureCustomBankPropertyEntity'
            conceptStructureCustomBankPath.SubPath.Add(ConceptStructureCustomBankPropertyEntity
                .PrefetchPathConceptStructurePartCustomBankPropertyCollection);

            // add prefetch path to join TreeStructureCustomBankPropertySelectedPartEntity info.
            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                .PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart);
            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                .PrefetchPathTreeStructureCustomBankPropertySelectedPartCollection);

            // add prefetch path to join TreeStructureCustomBankProperty
            var treeStructureCustomBankPath =
                customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                    .PrefetchPathTreeStructureCustomBankProperty);
            // add prefetch path to add tree structure parts for customproperties of type 'TreeStructureCustomBankPropertyEntity'
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

        /// <summary>
        ///     Adds the dependent resource path.
        /// </summary>
        /// <param name="resourcePrefetchPath">The resource prefetch path.</param>
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

        /// <summary>
        ///     Gets the specified resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        public ResourceDataEntity GetResourceData(ResourceEntity resource)
        {
            // validate parameters
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            return GetResourceData(resource.ResourceId);
        }

        /// <summary>
        ///     Gets the resource data by resource identifier.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourceDataEntity GetResourceDataByResourceId(Guid resourceId)
        {
            return GetResourceData(resourceId);
        }

        /// <summary>
        ///     Gets the resource data by resource ids.
        /// </summary>
        /// <param name="resourceIds">The resource ids.</param>
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

        /// <summary>
        ///     Gets the resource data.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <remarks>This function does not implement the IResourceService interface</remarks>
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

        /// <summary>
        ///     Gets the resources for bank branch.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetResourcesForBank(int bankId)
        {
            return GetResourcesForBank(bankId, true);
        }

        /// <summary>
        ///     Gets all 'resources' for the specified bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="fetchCompleteBranch"></param>
        public EntityCollection GetResourcesForBank(int bankId, bool fetchCompleteBranch)
        {
            return GetResourcesForBank(bankId, new ResourceEntityFactory(), fetchCompleteBranch);
        }

        /// <summary>
        ///     Gets all 'resources' for the specified bank using factory
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="fetchCompleteBranch">if set to <c>true</c> [fetch complete branch].</param>
        public EntityCollection GetResourcesForBank(int bankId, IEntityFactory2 factory, bool fetchCompleteBranch)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            return GetResourceCollection(bankId, factory, ResourceFields.BankId, prefetchPath, null,
                fetchCompleteBranch, 0);
        }


        /// <summary>
        ///     Gets the references for resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        public EntityCollection GetReferencesForResource(ResourceEntity resource)
        {
            var resources = new EntityCollection(new ResourceEntityFactory());

            if (resource.IsNew)
            {
                // Don't fetch from db - it does not exist there, fetching it will result in an out-of-sync entity.
                if (resource.ReferencedResourceCollection != null)
                    resources.AddRange(resource.ReferencedResourceCollection.Select(r => r.Resource));
            }
            else
            {
                // get the dependent resources
                IPrefetchPath2 resourcePrefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

                var depResourcePrefethPath = ResourceEntity.PrefetchPathReferencedResourceCollection;
                depResourcePrefethPath.SubPath.Add(DependentResourceEntity.PrefetchPathResource);
                resourcePrefetchPath.Add(depResourcePrefethPath);

                using (var adapter = new DataAccessAdapter())
                {
                    adapter.FetchEntity(resource, resourcePrefetchPath);
                }

                // now only return a collection of the referenced resources, no dependent resource collection.
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

        /// <summary>
        ///     Gets the dependencies for the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>The dependencies for the resource.</returns>
        public EntityCollection GetDependenciesForResource(ResourceEntity resource)
        {
            // get the dependent resources
            var depResourcePrefethPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));
            depResourcePrefethPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
            depResourcePrefethPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(resource, depResourcePrefethPath);
            }

            // now only return a collection of the dependend resources, no reference resource collection.
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

        #endregion

        #region "Updates"

        /// <summary>
        ///     Sets the bankId of the resources and custom bank properties identified by the resourceIdsToUpdate and
        ///     customBankPropertyIdsToUpdate parameters to the
        ///     value of the bankIdValue parameter.
        /// </summary>
        /// <param name="bankIdValue">
        ///     The int value that identifies the bank to which the resources and custom bank properties must
        ///     be moved.
        /// </param>
        /// <param name="resourceIdsToUpdate">A list with guids that identify the resources to move.</param>
        /// <param name="customBankPropertyIdsToUpdate">A list with guids that identify the custom bank properties toe move.</param>
        /// <returns>
        ///     In case of success an empty list, otherwise a list of key/value pairs where the key is the guid of the resource
        ///     or cbp for which the update gave problems and the value is a description of the problem.
        /// </returns>
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

        /// <summary>
        ///     Updates the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        private string UpdateResource(ResourceEntity resource)
        {
            return UpdateResource(resource, true, true, true);
        }

        /// <summary>
        ///     Updates the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="refetch">if set to <c>true</c> [refetch].</param>
        /// <param name="recurse">if set to <c>true</c> [recurse].</param>
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

                // Check if resource is not marked dirty while topology contains changes.
                // If this is the case then the resource should also update 
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
                        //ItemId can only be set after the itemresource has been assigned an ItemAutoId by saving to the database. Item was already saved, so we can set the itemId directly when necessary
                        if (!isNew && !skipSetItemIdIfNeeded)
                        {
                            SetItemIdIfNeeded(resource, useTransaction, adapter, recurse, updateResourceData, refetch, false);
                        }
                        adapter.Commit();

                        //Item was new and was saved just now; now set the ItemId
                        if (isNew && !skipSetItemIdIfNeeded)
                        {
                            SetItemIdIfNeeded(resource, useTransaction, adapter, recurse, updateResourceData, refetch, true);
                        }

                        if (!skipSaveResourceHistory)
                        {
                            SaveResourceHistory(resource, adapter);
                        }
                        //Do not enclose this in the transaction! It results in timeouts because of locking.
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
                        //Do not enclose this in the transaction! It results in timeouts because of locking.
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

                //Clear the label so it is empty when a new version is created.
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

        /// <summary>
        ///     Refetches the resource. This is sometimes needed because when custom properties are changed for the first time, the
        ///     needed record(s) are not in the database yet.
        ///     When the item is saved, the record(s) will be created but isn't yet available in the ItemResourceEntity object. So
        ///     a new fetch of the ItemResourceEntity is needed.
        /// </summary>
        /// <param name="resourceEntity">The resource entity.</param>
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

        /// <summary>
        ///     Updates the assessmenttest resource.
        /// </summary>
        /// <param name="resource">The resource to be updated in the datasource.</param>
        public string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource)
        {
            return UpdateResource(resource);
        }


        /// <summary>
        ///     Updates the assessment test resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="refetch">if set to <c>true</c> [refetch].</param>
        /// <param name="recurse">if set to <c>true</c> [recurse].</param>
        public string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        /// <summary>
        ///     Updates the item layout template resource.
        /// </summary>
        /// <param name="resource">The resource to be updated in the datasource.</param>
        public string UpdateControlTemplateResource(ControlTemplateResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        /// <summary>
        ///     Updates the control template resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="refetch">if set to <c>true</c> [refetch].</param>
        /// <param name="recurse">if set to <c>true</c> [recurse].</param>
        public string UpdateControlTemplateResource(ControlTemplateResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        /// <summary>
        ///     Updates the generic resource.
        /// </summary>
        /// <param name="resource">The resource to be updated in the datasource.</param>
        public string UpdateGenericResource(GenericResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        /// <summary>
        ///     Updates the generic resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="refetch">if set to <c>true</c> [refetch].</param>
        /// <param name="recurse">if set to <c>true</c> [recurse].</param>
        public string UpdateGenericResource(GenericResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        /// <summary>
        ///     Updates the item layout template resource.
        /// </summary>
        /// <param name="resource">The resource to be updated in the datasource.</param>
        public string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        /// <summary>
        ///     Updates the item layout template resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="refetch">if set to <c>true</c> [refetch].</param>
        /// <param name="recurse">if set to <c>true</c> [recurse].</param>
        public string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource, bool refetch,
            bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        /// <summary>
        ///     Updates the item resource.
        /// </summary>
        /// <param name="resource">The resource to be updated in the datasource.</param>
        /// <param name="refetch"></param>
        /// <param name="recurse"></param>
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

        /// <summary>
        ///     Updates the item resources.
        /// </summary>
        /// <param name="resources">The resources.</param>
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

        /// <summary>
        ///     Updates the item resource.
        /// </summary>
        /// <param name="resource">The resource to be updated in the datasource.</param>
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

        /// <summary>
        ///     Marks a resource as visible or invisible starting at bank level setAtBankId
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="setAtBankId"></param>
        /// <param name="makeResourceVisible"></param>
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
                //We make the resource visible in the setAtBankId by deleting the HiddenResource record for that bank.
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
                    //We make the resource invisible by creating an HiddenResource record for it. 
                    //But prior to that we remove all HiddenResource records that may have been created at sub-banks of the bankd identified by setAtBankId
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

        /// <summary>
        ///     Replaces the user currently linked to data as creator and/or modifier by a new one.
        /// </summary>
        /// <param name="currentUserId">The current user id.</param>
        /// <param name="newUserId">The new user id.</param>
        public bool ChangeCreatorModifier(int currentUserId, int newUserId)
        {
            ActionProcedures.ChangeCreatorModifier(currentUserId, newUserId);
            return true;
        }

        #endregion

        #region " Items "

        /// <summary>
        ///     Gets or sets the removed dependent entities.
        /// </summary>
        public ResourcePropertyValueCollection GetResourcePropertyValues(ResourceEntity resourceEntity)
        {
            var result = new ResourcePropertyValueCollection();
            foreach (var resourcePropertyDefinition in BankFactory.Instance.GetResourcePropertyDefinitions(resourceEntity.Bank))
            {
                result.Add(resourcePropertyDefinition, resourcePropertyDefinition.RetrievePropertyValuesFromResource(resourceEntity));
            }
            return result;
        }

        /// <summary>
        ///     Gets the specified item-resource.
        /// </summary>
        /// <param name="item">The item.</param>
        public ItemResourceEntity GetItem(ItemResourceEntity item, ResourceRequestDTO request)
        {
            // validate parameters
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

        /// <summary>
        ///     Gets the resource history for resource.
        /// </summary>
        /// <param name="resourceId">The resource.</param>
        public EntityCollection GetResourceHistoryForResource(Guid resourceId)
        {
            var result = new EntityCollection(new ResourceHistoryEntityFactory());

            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceHistoryEntity));
            var filterPredicate = new PredicateExpression();
            IRelationPredicateBucket bucket = new RelationPredicateBucket();

            // Add prefetch path to join bankentity in which this resource is placed
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
                // determine permissions for bank structure and get banks
                const TestBuilderPermissionTarget FETCH_TARGET = TestBuilderPermissionTarget.Any;
                var bankBranchHelper = new BankBranchIdHelper(PermissionService);
                var ids = bankBranchHelper.GetBankBrancheIds(bankId, FETCH_TARGET, TestBuilderPermissionAccess.DALRead);

                var tempItemCodesList = new List<string>(itemcodeList);

                // retrieve items in batches
                var offSet = 0;
                const int BATCH = 250;
                while (offSet != itemcodeList.Count)
                {
                    // create batch
                    var tempResultSet = new ItemResourceEntityCollection();
                    var length = itemcodeList.Count - offSet;
                    if (length > BATCH)
                    {
                        length = BATCH;
                    }

                    var itemCodeBatchArray = (string[])Array.CreateInstance(typeof(string), length);
                    tempItemCodesList.CopyTo(offSet, itemCodeBatchArray, 0, length);

                    // filter item codes and bank hierarchy
                    IRelationPredicateBucket filter = new RelationPredicateBucket();
                    filter.PredicateExpression.Add(new FieldCompareRangePredicate(ItemResourceFields.Name, null, itemCodeBatchArray));
                    filter.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ItemResourceFields.BankId, null, ids));

                    // fetch custom properties?

                    IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

                    // Do we need the full tree incl. all joins?
                    if (request.WithFullCustomProperties)
                    {
                        GetFullCustomBankPropertiesPrefetchPath(prefetchPath);
                    }
                    else if (request.WithCustomProperties) // or is a more limited set enough?
                    {
                        // Create new prefetch path for this entity
                        // Add prefetch path to include the Bank
                        prefetchPath.Add(ResourceEntity.PrefetchPathBank);
                        prefetchPath.Add(ResourceEntity.PrefetchPathCreatedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                        prefetchPath.Add(ResourceEntity.PrefetchPathModifiedByUser, _excludeIncludeFieldsHelper.GetUserIncludedFieldList());

                        // Add prefetch path to join customerpropertiescollection, the (selected)values, etc
                        var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
                        path1.SubPath.Add(ListCustomBankPropertyValueEntity
                            .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);
                        path1.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                            .PrefetchPathConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart);
                        path1.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                            .PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart);
                        // Add prefetch path to join CustomBankPropertyEntity
                        var path2 = path1.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);


                        // Add prefetch path to join ListValues of ListCustomBankProperty
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

                        // Add prefetch path to include the State, CreatedByUser and ModifiedByUser
                        prefetchPath.Add(ResourceEntity.PrefetchPathState, _excludeIncludeFieldsHelper.GetStateExcludedFieldList());
                        prefetchPath.Add(ResourceEntity.PrefetchPathReferencedResourceCollection);
                    }

                    if (request.WithDependencies)
                    {
                        if (!request.WithCustomProperties && !request.WithReportData)
                            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

                        // get the dependent resources
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
                // make sure the sequence is preserved.
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

        /// <summary>
        ///     Gets the test by code.
        /// </summary>
        /// <param name="testcodeList">The testcode list.</param>
        /// <param name="bankId">The bank.</param>
        /// <param name="withCustomProperties">if set to <c>true</c> [with custom properties].</param>
        public AssessmentTestResourceEntityCollection GetTestByCode(List<string> testcodeList, int bankId,
            bool withCustomProperties)
        {
            var returnValue = new AssessmentTestResourceEntityCollection();
            if (!testcodeList.Any())
            {
                return returnValue;
            }

            // determine permissions for bank structure and get banks
            const TestBuilderPermissionTarget FETCH_TARGET = TestBuilderPermissionTarget.Any;
            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, FETCH_TARGET, TestBuilderPermissionAccess.DALRead);

            // filter item codes and bank hierarcht
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var testCodeArray = new string[testcodeList.Count + 1];
            testcodeList.CopyTo(testCodeArray, 0);
            filter.PredicateExpression.Add(new FieldCompareRangePredicate(AssessmentTestResourceFields.Name, null, testCodeArray));
            filter.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(AssessmentTestResourceFields.BankId, null, ids));

            // fetch custom properties?
            IPrefetchPath2 prefetchPath = null;
            if (withCustomProperties)
            {
                // Create new prefetch path for this entity
                prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

                // Add prefetch path to include the Bank
                prefetchPath.Add(ResourceEntity.PrefetchPathBank);

                // Add prefetch path to join customerpropertiescollection, the (selected)values, etc
                var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
                path1.SubPath.Add(ListCustomBankPropertyValueEntity
                    .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

                //' Add prefetch path to join CustomBankPropertyEntity
                var path2 = path1.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);

                //' Add prefetch path to join ListValues of ListCustomBankProperty
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

        /// <summary>
        ///     Gets the pause items from bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetPauseItemsForBank(int bankId)
        {
            var filter = new RelationPredicateBucket();

            // Add Relations using aliasses in order to join the ItemTemplateLayoutResource entity and be able to set a filter on them 
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

            // Add prefetch path to join customerpropertiescollection, the (selected)values, etc
            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            // Add prefetch path to join CustomBankPropertyEntity
            var path2 = path1.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);

            // Add prefetch path to join ListValues of ListCustomBankProperty
            path2.SubPath.Add(ListCustomBankPropertyEntity.PrefetchPathListValueCustomBankPropertyCollection);

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            var depCollPrefetch = prefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
            depCollPrefetch.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);

            // Get data and return results
            return GetResourceCollection(bankId, new ItemResourceEntityFactory(), ItemResourceFields.BankId,
                prefetchPath, filter);
        }

        /// <summary>
        ///     Gets all 'item-resources' for the specified bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetItemsForBank(int bankId)
        {
            return GetItemsForBank(bankId, false, null);
        }

        /// <summary>
        ///     Gets the items for bank with full custom properties.
        /// </summary>
        /// <param name="bankId">The bank identifier.</param>
        public EntityCollection GetItemsForBankWithFullCustomProperties(int bankId)
        {
            return GetItemsForBank(bankId, true, null);
        }

        /// <summary>
        ///     Searches for items in bank by predicate
        /// </summary>
        /// <param name="bankId">The bank</param>
        /// <param name="bucket">The predicate</param>
        public EntityCollection GetItemsForBank(int bankId, RelationPredicateBucket bucket)
        {
            return GetItemsForBank(bankId, false, bucket);
        }

        /// <summary>
        ///     Gets all 'item-resources' for the specified bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="withCustomProperties"></param>
        /// <param name="bucket">The bucket.</param>
        public EntityCollection GetItemsForBank(int bankId, bool withCustomProperties, RelationPredicateBucket bucket)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));
            // Add prefetch path to join customerpropertiescollection, the (selected)values, etc
            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            if (withCustomProperties)
            {
                path1.SubPath.Add(ListCustomBankPropertyValueEntity.PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);
                path1.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity.PrefetchPathConceptStructurePartCustomBankPropertyCollectionViaConceptStructureCustomBankPropertySelectedPart);
                path1.SubPath.Add(TreeStructureCustomBankPropertyValueEntity.PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart);
            }

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            var depCollPrefetch = prefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
            var depResSubPath = depCollPrefetch.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
            depResSubPath.EntityFactoryToUse = new ItemLayoutTemplateResourceEntityFactory();
            depResSubPath.SubPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);

            // Get data and return results
            return GetResourceCollection(bankId, new ItemResourceEntityFactory(), ItemResourceFields.BankId, prefetchPath, bucket);
        }

        /// <summary>
        ///     Searches for items in bank using keywords
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="searchKeyWords">The search key word.</param>
        /// <param name="searchInBankProperties">if set to <c>true</c> [search in bank properties].</param>
        /// <param name="searchInItemText">if set to <c>true</c> [search in item text].</param>
        /// <param name="testContextResourceId"></param>
        /// <param name="maxRecords"></param>
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

                // ** SPECIALIZED CASE WHEN NO SEARCH TERM IS GIVEN, BUT A TEST IS SELECTED. DISPLAY ALL ITEMS IN TEST
                specificTestBucket.PredicateExpression.Add(new FieldCompareSetPredicate(ItemResourceFields.ResourceId,
                    null, DependentResourceFields.DependentResourceId, null, SetOperator.In,
                    DependentResourceFields.ResourceId == testContextResourceId));

                // get items and add to main result collection
                var subResultTestItems = GetItemsForBank(bankId, specificTestBucket);
                AddSubResultItemCollectionToResultItemCollection(subResultTestItems, resultCollection);
            }

            // Get data and return results
            return resultCollection;
        }

        private void GetItemsFromKeywordInBankProperties(int bankId, string searchKeyWords, Guid testContextResourceId, string fullTextSearchString, EntityCollection resultCollection)
        {
            if (string.IsNullOrEmpty(searchKeyWords)) return;

            // ** FREE VALUE CUSTOM PROPERTIES
            var freeValuePropertiesBucket = new RelationPredicateBucket();

            // use test context
            if (testContextResourceId != Guid.Empty)
            {
                freeValuePropertiesBucket.PredicateExpression.Add(new FieldCompareSetPredicate(
                    ItemResourceFields.ResourceId, null, DependentResourceFields.DependentResourceId, null,
                    SetOperator.In, DependentResourceFields.ResourceId == testContextResourceId));
            }

            // free value custom properties
            freeValuePropertiesBucket.Relations.Add(ResourceEntity.Relations.CustomBankPropertyValueEntityUsingResourceId);
            var freeValueCustomBankPropertyValueRelation = freeValuePropertiesBucket.Relations.Add(
                CustomBankPropertyValueEntity.Relations.GetSubTypeRelation("FreeValueCustomBankPropertyValueEntity"), JoinHint.Inner);
            freeValueCustomBankPropertyValueRelation.CustomFilter = new PredicateExpression(
                new FieldFullTextSearchPredicate(FreeValueCustomBankPropertyValueFields.Value, null, FullTextSearchOperator.Contains, fullTextSearchString));

            // get items and add to main result collection
            var subResultFreeValue = GetItemsForBank(bankId, freeValuePropertiesBucket);
            AddSubResultItemCollectionToResultItemCollection(subResultFreeValue, resultCollection);

            // ** MULTI-SELECT CUSTOM PROPERTIES
            var multiSelectPropertiesBucket = new RelationPredicateBucket();

            // use test context
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

            // get items and add to main result collection
            var subResultMultiValue = GetItemsForBank(bankId, multiSelectPropertiesBucket);
            AddSubResultItemCollectionToResultItemCollection(subResultMultiValue, resultCollection);

            // ** OTHER METADATA
            var otherMetaDataBucket = new RelationPredicateBucket();
            var filters = new PredicateExpression();
            // wrapper predicate to wrap all OR expressions. This expression is later added to the bank id context expressions.

            // use test context
            if (testContextResourceId != Guid.Empty)
            {
                otherMetaDataBucket.PredicateExpression.Add(new FieldCompareSetPredicate(
                    ItemResourceFields.ResourceId, null, DependentResourceFields.DependentResourceId, null,
                    SetOperator.In, DependentResourceFields.ResourceId == testContextResourceId));
            }
            // other meta-data columns
            DateTime searchForDate;
            if (DateTime.TryParse(searchKeyWords, out searchForDate) &&
                searchForDate >= SqlDateTime.MinValue.Value && searchForDate <= SqlDateTime.MaxValue.Value)
            {
                // search term is a date, so also search in 'created date' and 'modified date'
                var searchFromDate = new DateTime(searchForDate.Year, searchForDate.Month, searchForDate.Day, 0, 0,
                    0);
                var searchToDate = new DateTime(searchForDate.Year, searchForDate.Month, searchForDate.Day, 23, 59,
                    59);
                filters.Add(new FieldBetweenPredicate(ResourceFields.CreationDate, null, searchFromDate, searchToDate));
                filters.AddWithOr(new FieldBetweenPredicate(ResourceFields.ModifiedDate, null, searchFromDate, searchToDate));
            }

            // search in 'created by' and 'modified by'
            var createdByRelation = otherMetaDataBucket.Relations.Add(ResourceEntity.Relations.UserEntityUsingCreatedBy, "CreatedBy", JoinHint.Inner);
            filters.AddWithOr(UserFields.FullName.SetObjectAlias("CreatedBy") == searchKeyWords);

            var modifiedByRelation = otherMetaDataBucket.Relations.Add(ResourceEntity.Relations.UserEntityUsingModifiedBy, "ModifiedBy", JoinHint.Inner);
            filters.AddWithOr(UserFields.FullName.SetObjectAlias("ModifiedBy") == searchKeyWords);

            // search in main, title and description
            var fields = new List<EntityField2> { ResourceFields.Name, ResourceFields.Title, ResourceFields.Description };
            filters.AddWithOr(new FieldFullTextSearchPredicate(fields, FullTextSearchOperator.Contains, fullTextSearchString));

            // get items and add to main result collection
            otherMetaDataBucket.PredicateExpression.Add(filters);
            var subResultOtherMetaData = GetItemsForBank(bankId, otherMetaDataBucket);
            AddSubResultItemCollectionToResultItemCollection(subResultOtherMetaData, resultCollection);
        }

        private void GetItemsFromKeywordInItemText(int bankId, Guid testContextResourceId, string fullTextSearchString, EntityCollection resultCollection)
        {
            var itemTextBucket = new RelationPredicateBucket();

            // use test context
            if (testContextResourceId != Guid.Empty)
            {
                itemTextBucket.PredicateExpression.Add(new FieldCompareSetPredicate(ItemResourceFields.ResourceId,
                    null, DependentResourceFields.DependentResourceId, null, SetOperator.In,
                    DependentResourceFields.ResourceId == testContextResourceId));
            }

            // When using FORMSOF INFLECTIONAL, the full-text engine will go through the stemming process, generating
            // the words light, lightest, lit, and so on. But unlike FULLTEXT, it’ll stop and not go to the
            // thesaurus to add any more words.
            IPredicate itemTextPredicate = new FieldFullTextSearchPredicate(ResourceDataFields.BinData, null,
                FullTextSearchOperator.Contains, fullTextSearchString);

            itemTextBucket.Relations.Add(ResourceEntity.Relations.ResourceDataEntityUsingResourceId);
            itemTextBucket.PredicateExpression.Add(itemTextPredicate);

            // get items and add to main result collection
            var subResultItemText = GetItemsForBank(bankId, itemTextBucket);
            AddSubResultItemCollectionToResultItemCollection(subResultItemText, resultCollection);
        }


        /// <summary>
        ///     Adds the sub result collection to result collection.
        /// </summary>
        /// <param name="subResultCollection">The sub result collection.</param>
        /// <param name="resultCollection">The result collection.</param>
        private void AddSubResultItemCollectionToResultItemCollection(EntityCollection subResultCollection,
            EntityCollection resultCollection)
        {
            foreach (ItemResourceEntity subResult in subResultCollection)
            {
                // does not exist in result collection?
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

        #endregion

        #region " Test(template)s "

        /// <summary>
        ///     Gets the specified test-resource.
        /// </summary>
        /// <param name="test">The test.</param>
        public AssessmentTestResourceEntity GetAssessmentTest(AssessmentTestResourceEntity test)
        {
            // validate parameters
            if (test == null)
                throw new ArgumentNullException(nameof(test));

            var fetchedResource = new AssessmentTestResourceEntity(test.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithCustomProperties = true };
            PrefetchResource(fetchedResource, EntityType.AssessmentTestResourceEntity, request);

            return fetchedResource;
        }

        /// <summary>
        ///     Gets all 'AssessmentTests' for the specified bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <returns></returns>
        public EntityCollection GetAssessmentTestsForBank(int bankId)
        {
            return GetAssessmentTestsForBank(bankId, true, false, true);
        }

        /// <summary>
        ///     Get all 'test-resources' for the specified bank (plus possible parent/child banks)
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="includeParentBanks"></param>
        /// <param name="includeChildBanks"></param>
        /// <param name="withCustomProperties"></param>
        public EntityCollection GetAssessmentTestsForBank(int bankId, bool includeParentBanks, bool includeChildBanks,
            bool withCustomProperties)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.AssessmentTestResourceEntity));

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            // Add prefetch path to join customerpropertiescollection, the (selected)values, etc
            if (withCustomProperties)
            {
                var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
                path1.SubPath.Add(ListCustomBankPropertyValueEntity
                    .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);
            }

            // Create filter to only get the tests
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(AssessmentTestResourceFields.IsTemplate == false);

            // Get data and return results
            return GetResourceCollection(bankId, new AssessmentTestResourceEntityFactory(),
                AssessmentTestResourceFields.BankId, prefetchPath, filter, includeParentBanks, includeChildBanks, 0);
        }

        /// <summary>
        ///     Gets the 'test template resources' for the specified bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetAssessmentTestTemplatesForBank(int bankId)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.AssessmentTestResourceEntity));

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            // Create filter to only get the test templates
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(AssessmentTestResourceFields.IsTemplate == true);

            // Get data and return results
            return GetResourceCollection(bankId, new AssessmentTestResourceEntityFactory(),
                AssessmentTestResourceFields.BankId, prefetchPath, filter);
        }

        #endregion

        #region " TestPackage "

        /// <summary>
        ///     Gets the test packages for bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetTestPackagesForBank(int bankId)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.AssessmentTestResourceEntity));

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            // Add prefetch path to join customerpropertiescollection, the (selected)values, etc
            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            // Get data and return results
            return GetResourceCollection(bankId, new TestPackageResourceEntityFactory(),
                TestPackageResourceFields.BankId, prefetchPath, null);
        }

        /// <summary>
        ///     Gets the testPackage.
        /// </summary>
        /// <param name="testPackage">The testPackage.</param>
        public TestPackageResourceEntity GetTestPackage(TestPackageResourceEntity testPackage)
        {
            // validate parameters
            if (testPackage == null)
                throw new ArgumentNullException(nameof(testPackage));

            var fetchedResource = new TestPackageResourceEntity(testPackage.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true };
            PrefetchResource(fetchedResource, EntityType.TestPackageResourceEntity, request);

            return fetchedResource;
        }

        #endregion

        #region "Package"

        /// <summary>
        ///     Gets the packages for bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetPackagesForBank(int bankId)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.PackageResourceEntity));

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            // Get data and return results
            return GetResourceCollection(bankId, new PackageResourceEntityFactory(), PackageResourceFields.BankId,
                prefetchPath, null);
        }


        /// <summary>
        ///     Gets the package.
        /// </summary>
        /// <param name="package">The package.</param>
        public PackageResourceEntity GetPackage(PackageResourceEntity package)
        {
            // validate parameters
            if (package == null)
                throw new ArgumentNullException(nameof(package));

            var fetchedResource = new PackageResourceEntity(package.ResourceId);
            PrefetchResource(fetchedResource, EntityType.PackageResourceEntity);

            return fetchedResource;
        }

        #endregion

        #region " ItemLayoutTemplates "

        /// <summary>
        ///     Gets the item template.
        /// </summary>
        /// <param name="itemLayoutTemplate">The item layout template.</param>
        public ItemLayoutTemplateResourceEntity GetItemLayoutTemplate(
            ItemLayoutTemplateResourceEntity itemLayoutTemplate)
        {
            // validate parameters
            if (itemLayoutTemplate == null)
                throw new ArgumentNullException(nameof(itemLayoutTemplate));

            var fetchedResource = new ItemLayoutTemplateResourceEntity(itemLayoutTemplate.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithHiddenResources = true };
            PrefetchResource(fetchedResource, EntityType.ItemLayoutTemplateResourceEntity, request);

            return fetchedResource;
        }

        /// <summary>
        ///     Gets the item layout templates for bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetItemLayoutTemplatesForBank(int bankId)
        {
            IRelationPredicateBucket bucket = new RelationPredicateBucket();
            return GetItemLayoutTemplates(bankId, bucket);
        }

        /// <summary>
        ///     Gets the item layout templates for bank with item type filter.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="itemTypes">The item types.</param>
        /// <param name="exclude">if set to <c>true</c> [exclude]. otherwise include types</param>
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

        /// <summary>
        ///     Gets the item layout templates.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="bucket">The bucket.</param>
        private EntityCollection GetItemLayoutTemplates(int bankId, IRelationPredicateBucket bucket)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath =
                new PrefetchPath2(Convert.ToInt32(EntityType.ItemLayoutTemplateResourceEntity));
            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);
            // Add prefetch path to join the hiddenresource entities which when present identify the banks at which this item layout template (resource) is set to hidden.
            prefetchPath.Add(ResourceEntity.PrefetchPathHiddenResourceCollection);
            // Get data and return results
            return GetResourceCollection(bankId, new ItemLayoutTemplateResourceEntityFactory(),
                ItemLayoutTemplateResourceFields.BankId, prefetchPath, bucket);
        }


        /// <summary>
        ///     Gets the item layout template source names for item code list.
        /// </summary>
        /// <param name="bankId">The bankId.</param>
        /// <param name="itemCodes">The item codes.</param>
        public List<string> GetItemLayoutTemplateSourceNamesForItemCodeList(int bankId, List<string> itemCodes)
        {
            var returnValue = new List<string>();
            if (!itemCodes.Any())
            {
                return returnValue;
            }

            // Get all ids for selected bank and parent banks
            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, TestBuilderPermissionTarget.Any,
                TestBuilderPermissionAccess.AnyTask);

            var itemOffset = 0;
            const int ITEM_MAX_BATCH = 1000;

            // execute the function in batches, because of the limitation of the sql parameters.
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

            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath =
                new PrefetchPath2(Convert.ToInt32(EntityType.ItemLayoutTemplateResourceEntity));


            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);
            if (withDependencies)
            {
                var depPath = prefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
                depPath.SubPath.Add(DependentResourceEntity.PrefetchPathDependentResource);
            }

            // Get data and return results                                             
            var filter = new RelationPredicateBucket();
            var fieldCompareRange = new FieldCompareRangePredicate(ItemLayoutTemplateResourceFields.ResourceId, null, resourceIds);
            filter.PredicateExpression.Add(fieldCompareRange);
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter, 0, null, prefetchPath);
            }
            return resources;
        }

        #endregion

        #region " ControlTemplates "

        /// <summary>
        ///     Gets the specified control template-resource.
        /// </summary>
        /// <param name="controlTemplate">The control template.</param>
        public ControlTemplateResourceEntity GetControlTemplate(ControlTemplateResourceEntity controlTemplate)
        {
            // validate parameters
            if (controlTemplate == null)
                throw new ArgumentNullException(nameof(controlTemplate));

            var fetchedResource = new ControlTemplateResourceEntity(controlTemplate.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithHiddenResources = true };
            PrefetchResource(fetchedResource, EntityType.ControlTemplateResourceEntity, request);

            return fetchedResource;
        }

        /// <summary>
        ///     Gets all 'control template-resources' for the specified bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetControlTemplatesForBank(int bankId)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ControlTemplateResourceEntity));

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            // Get data and return results
            return GetResourceCollection(bankId, new ControlTemplateResourceEntityFactory(),
                ControlTemplateResourceFields.BankId, prefetchPath, null);
        }

        #endregion

        #region " GenericResources "

        /// <summary>
        ///     Gets the specified generic resource.
        /// </summary>
        /// <param name="genericResource">The generic resource.</param>
        public GenericResourceEntity GetGenericResource(GenericResourceEntity genericResource)
        {
            // validate parameters
            if (genericResource == null)
                throw new ArgumentNullException(nameof(genericResource));

            var fetchedResource = new GenericResourceEntity(genericResource.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithCustomProperties = true };
            PrefetchResource(fetchedResource, EntityType.GenericResourceEntity, request);

            return fetchedResource;
        }

        /// <summary>
        ///     Gets all 'generic-resources' for the specified bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="filter">
        /// </param>
        /// <param name="filePrefix"></param>
        /// <param name="templatesOnly"></param>
        public EntityCollection GetGenericResourceForBank(
            int bankId,
            string filter,
            string filePrefix,
            bool templatesOnly)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.GenericResourceEntity));

            // Add prefetch path to join customerpropertiescollection, the (selected)values, etc
            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity.PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);

            // Create filter which only retrieves all resources with mediatype 'like' the given string.
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

            // Create filter to only retrieve resources marked as template
            if (templatesOnly)
            {
                if (bucket == null)
                {
                    bucket = new RelationPredicateBucket();
                }
                bucket.PredicateExpression.AddWithAnd(GenericResourceFields.IsTemplate == true);
            }

            // Create filter to only retrieve files with a specific prefix
            if (!string.IsNullOrEmpty(filePrefix))
            {
                if (bucket == null)
                {
                    bucket = new RelationPredicateBucket();
                }
                bucket.PredicateExpression.Add(GenericResourceFields.Name % (filePrefix + "%"));
            }

            // Get data and return results
            return GetResourceCollection(bankId, new GenericResourceEntityFactory(), GenericResourceFields.BankId,
                prefetchPath, bucket);
        }

        #endregion

        #region " States "

        /// <summary>
        ///     Gets the available states for all resources.
        /// </summary>
        public EntityCollection GetAvailableStates()
        {
            // Collection to hold result
            var states = new EntityCollection(new StateEntityFactory());

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(states, null);
            }

            return states;
        }

        /// <summary>
        ///     Gets the state and actions for all targets
        /// </summary>
        /// <param name="stateId">The state id.</param>
        public StateEntity GetState(int stateId)
        {
            var stateToFetch = new StateEntity(stateId);

            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.StateEntity));

            // Add prefetch path to join StateAction and action to state
            prefetchPath.Add(StateEntity.PrefetchPathStateActionCollection).SubPath
                .Add(StateActionEntity.PrefetchPathAction);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(stateToFetch, prefetchPath);
            }

            return stateToFetch;
        }

        /// <summary>
        ///     Gets the stateAction for stateId.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="target">The target.</param>
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

        #endregion

        #region "Private helper functions"

        /// <summary>
        ///     Gets the state action name from resource.
        /// </summary>
        /// <param name="stateCollection">The state collection.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="target"></param>
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

        /// <summary>
        ///     Gets all states.
        /// </summary>
        private EntityCollection GetAllStates()
        {
            // Collection to hold result
            var states = new EntityCollection(new StateEntityFactory());

            using (var adapter = new DataAccessAdapter())
            {
                // Create new prefetch path for this entity
                IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.StateEntity));

                // Add prefetch path to join StateAction and action to state
                prefetchPath.Add(StateEntity.PrefetchPathStateActionCollection).SubPath
                    .Add(StateActionEntity.PrefetchPathAction);

                adapter.FetchEntityCollection(states, null, prefetchPath);
            }

            return states;
        }

        /// <summary>
        ///     Gets the name of the resource by.
        /// </summary>
        /// <param name="bankIds">The bank ids.</param>
        /// <param name="name">The name.</param>
        /// <param name="request"></param>
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
            //for the first step to support versioning in TestBuilder we will raise the version each
            //time the component is saved. In the next step we'll have to make a copy of the original component (at least when its 
            //referenced in another component) In this case we should add the predicateExpression and specify the right version.

            _with7.PredicateExpression.AddWithAnd(new FieldCompareRangePredicate(ResourceFields.BankId, null, bankIds));

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter, 1, null,
                    GetResourcePrefetchPath(request));

                // Check for valid result
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

        /// <summary>
        ///     Gets all states from resources.
        /// </summary>
        /// <param name="resourceNames">The resource names.</param>
        /// <param name="bankId">The bank.</param>
        private EntityCollection GetAllStatesFromResources(string[] resourceNames, int bankId)
        {
            var returnValue = new EntityCollection(new ResourceEntityFactory());
            if (!resourceNames.Any())
            {
                return returnValue;
            }

            // Get all ids for selected bank and parent banks
            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, false, TestBuilderPermissionTarget.Any,
                TestBuilderPermissionAccess.AnyTask);

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var with9 = filter;
            with9.PredicateExpression.Add(new FieldCompareRangePredicate(ResourceFields.Name, null, resourceNames));
            //for the first step to support versioning in TestBuilder we will raise the version each
            //time the component is saved. In the next step we'll have to make a copy of the original component (at least when its 
            //referenced in another component) In this case we should add the predicateExpression and specify the right version.

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
                // get the dependent resources
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

        /// <summary>
        ///     Gets the resource collection.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="fld">The FLD.</param>
        /// <param name="prefetchPath">The prefetch path.</param>
        /// <param name="filter">The filter.</param>
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

        /// <summary>
        ///     Gets the resource collection.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="fld">The FLD.</param>
        /// <param name="prefetchPath">The prefetch path.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="includeParentBanks">if set to <c>true</c> [fetch parent banks as well].</param>
        /// <param name="includeChildBanks">if set to <c>true</c> [fetch child banks as well].</param>
        /// <param name="maxRecords">max number of records to return.</param>
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
            // Get all ids for selected bank and parent banks
            var fetchTarget = ContentModelObjectToPermissionTarget(factory);

            // Add prefetch path to join dependent resource entities, to count the dependent itemresources of this template (shown in grid)
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

            //Only if there is at least one bank where the current user has permission to access the FetchTarget in the requested way.

            if (ids.Length <= 0)
                return resources;

            using (var adapter = new DataAccessAdapter())
            {
                IRelationPredicateBucket filtr = filter;
                if (filter == null)
                {
                    filtr = new RelationPredicateBucket();
                }

                // Setup FieldCompareRange to return all entities for given bank ids
                var fieldCompareRange = new FieldCompareRangePredicate(fld, null, ids);
                filtr.PredicateExpression.Add(fieldCompareRange);

                //conditionally use prefetch path
                if (prefetchPath == null)
                {
                    adapter.FetchEntityCollection(resources, filtr);
                }
                else
                {
                    // WorkFlow Prefetch data : resEnt->State->StateAction<-Action
                    prefetchPath
                        .Add(ResourceEntity.PrefetchPathState,
                            _excludeIncludeFieldsHelper.GetStateExcludedFieldList()).SubPath
                        .Add(StateEntity.PrefetchPathStateActionCollection).SubPath
                        .Add(StateActionEntity.PrefetchPathAction);

                    // add prefetch paths to join auditing information 
                    prefetchPath.Add(ResourceEntity.PrefetchPathCreatedByUser,
                        _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                    prefetchPath.Add(ResourceEntity.PrefetchPathModifiedByUser,
                        _excludeIncludeFieldsHelper.GetUserIncludedFieldList());

                    adapter.FetchEntityCollection(resources, filtr, maxRecords, null, prefetchPath);
                }
            }

            // return all resources for given bank and optionally all parent banks
            return resources;
        }

        /// <summary>
        ///     Fetches the dependent resource entities of resource using projection.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="adapter">The adapter.</param>
        /// <remarks>
        ///     This function is used instead of a prefetch path, because the dependent resource collection must
        ///     only consist of ResourceEntities and not it's subclasses. This gives problems with webservices communication.
        /// </remarks>
        private void FetchDependentResourceEntitiesOfResource(ResourceEntity resource, DataAccessAdapter adapter)
        {
            if (!resource.DependentResourceCollection.Any())
            {
                return;
            }

            // get dependent resource objects via projection.
            var dependentResources = new EntityCollection(new ResourceEntityFactory());

            // get resourceguid's to fetch
            var fetchTheseResources = new List<Guid>();
            foreach (var dresource in resource.DependentResourceCollection)
            {
                fetchTheseResources.Add(dresource.DependentResourceId);
            }

            // construct field-information for projection
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
                // find resource in projected resultset
                var resultIndex = dependentResources.FindMatches(ResourceFields.ResourceId == depResource.DependentResourceId);
                if (resultIndex.Count == 1)
                {
                    dependentResources[resultIndex[0]].IsNew = false;
                    dependentResources[resultIndex[0]].IsDirty = false;
                    depResource.DependentResource = (ResourceEntity)dependentResources[resultIndex[0]];
                }
            }
        }

        /// <summary>
        ///     Map a content model object to a security permission target.
        /// </summary>
        /// <param name="resourceObject">Type of the resource.</param>
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

        #endregion

        #region " AspectResources  "

        /// <summary>
        ///     Gets the specified aspect-resource.
        /// </summary>
        /// <param name="aspect">The aspect.</param>
        public AspectResourceEntity GetAspect(AspectResourceEntity aspect)
        {
            // validate parameters
            if (aspect == null)
                throw new ArgumentNullException(nameof(aspect));

            var fetchedResource = new AspectResourceEntity(aspect.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true, WithState = true, WithUserInfo = true };
            PrefetchResource(fetchedResource, EntityType.AspectResourceEntity, request);

            return fetchedResource;
        }

        /// <summary>
        ///     Gets all 'aspect-resources' for the specified bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        public EntityCollection GetAspectsForBank(int bankId)
        {
            // Create new prefetch path for this entity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.ResourceEntity));

            // todo: code between lines is generic for all resources (at least it's the same as in GetItemsForBank, but should be for all I think) 
            //       and could therefore refactored into a method
            //-------------------------------
            // Add prefetch path to join customerpropertiescollection, the (selected)values, etc
            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity.PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            //' Add prefetch path to join CustomBankPropertyEntity
            var path2 = path1.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);

            //' Add prefetch path to join ListValues of ListCustomBankProperty
            path2.SubPath.Add(ListCustomBankPropertyEntity.PrefetchPathListValueCustomBankPropertyCollection);

            // Add prefetch path to join bankentity in which this resource is placed
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);
            //-------------------------------------------------------------------------------------------------------------------------------------

            // Get data and return results
            return GetResourceCollection(bankId, new AspectResourceEntityFactory(), AspectResourceFields.BankId, prefetchPath, null);
        }

        /// <summary>
        ///     Updates the aspect resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        public string UpdateAspectResource(AspectResourceEntity resource)
        {
            return UpdateResource(resource);
        }

        /// <summary>
        ///     Updates the aspect resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="refetch">if set to <c>true</c> [refetch].</param>
        /// <param name="recurse">if set to <c>true</c> [recurse].</param>
        public string UpdateAspectResource(AspectResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        #endregion


        #region " DataSources "

        /// <summary>
        ///     Gets the specified data source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public DataSourceResourceEntity GetDataSource(DataSourceResourceEntity dataSource)
        {
            //' validate parameters
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }

            var fetchedResource = new DataSourceResourceEntity(dataSource.ResourceId);
            var request = new ResourceRequestDTO() { WithDependencies = true };
            PrefetchResource(fetchedResource, EntityType.DataSourceResourceEntity, request);

            return fetchedResource;
        }

        /// <summary>
        ///     Gets the data sources for bank.
        /// </summary>
        /// <param name="bankId">The bank.</param>
        /// <param name="isTemplate">Filter by template, set to nothing for all</param>
        /// <param name="behaviours">Filter by behaviour, set nothing for all.</param>
        public EntityCollection GetDataSourcesForBank(int bankId, bool? isTemplate, params string[] behaviours)
        {
            //' Create new prefetch path for this entity to join related bankentity
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.DataSourceResourceEntity));
            prefetchPath.Add(ResourceEntity.PrefetchPathBank);
            prefetchPath.Add(ResourceEntity.PrefetchPathDependentResourceCollection);
            //' Add prefetch path to join customerpropertiescollection, the (selected)values, etc
            var path1 = prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            path1.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);

            IRelationPredicateBucket filter = null;

            if (isTemplate.HasValue)
            {
                //' Create filter to filter out the templates
                filter = new RelationPredicateBucket();

                var isTemplatePredicate = DataSourceResourceFields.IsTemplate == isTemplate.Value;
                filter.PredicateExpression.Add(isTemplatePredicate);
            }

            if (behaviours != null && behaviours.Length > 0)
            {
                //' Create filter to filter out the specific behaviour
                if (filter == null)
                {
                    filter = new RelationPredicateBucket();
                }

                //' Group the predicates
                var behaviourPredicate = new PredicateExpression(DataSourceResourceFields.DataSourceType == behaviours[0]);
                for (var i = 1; i <= behaviours.Length - 1; i++)
                {
                    behaviourPredicate = (PredicateExpression)behaviourPredicate.AddWithOr(new PredicateExpression(DataSourceResourceFields.DataSourceType == behaviours[i]));
                }

                filter.PredicateExpression.Add(behaviourPredicate);
            }

            //' Get data and return results
            var col = GetResourceCollection(bankId, new DataSourceResourceEntityFactory(), DataSourceResourceFields.BankId, prefetchPath, filter);

            return col;
        }

        /// <summary>
        ///     Updates the dataSource resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        public string UpdateDataSourceResource(DataSourceResourceEntity resource)
        {
            return UpdateResource(resource);
        }


        /// <summary>
        ///     Updates the data source resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="refetch">if set to <c>true</c> [refetch].</param>
        /// <param name="recurse">if set to <c>true</c> [recurse].</param>
        public string UpdateDataSourceResource(DataSourceResourceEntity resource, bool refetch, bool recurse)
        {
            return UpdateResource(resource, refetch, recurse);
        }

        public ResourceEntity GetResourceByNameWithOption(int bankId, string name, ResourceRequestDTO request)
        {
            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            // Get all ids for selected bank and parent banks
            var ids = bankBranchHelper.GetBankBrancheIds(bankId, TestBuilderPermissionTarget.Any, TestBuilderPermissionAccess.AnyTask);
            return GetResourceByName(ids, name, request);
        }

        public EntityCollection GetResourcesByNamesWithOption(int bankId, List<string> names, ResourceRequestDTO request)
        {
            var bankBranchHelper = new BankBranchIdHelper(PermissionService);
            // Get all ids for selected bank and parent banks
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
                    // Get the referencing resources as well.
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

        #endregion
    }
}