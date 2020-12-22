using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using CustomClasses;
using Enums;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Properties;
using Questify.Builder.Model.ContentModel;
using Questify.Builder.Model.ContentModel.DatabaseSpecific;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.ResourceProperties;
using Questify.Builder.Security;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.Logic.Service.Direct
{
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    public class BankService : IBankService
    {
        private readonly IPermissionService _permissionService;
        private readonly ISecurityService _securityService;

        private readonly ExcludeIncludeFieldsHelper _excludeIncludeFieldsHelper;


        public BankService()
        {
            _permissionService = PermissionFactory.Instance;
            _securityService = SecurityFactory.Instance;
            _excludeIncludeFieldsHelper = new ExcludeIncludeFieldsHelper();
        }

        public BankService(IPermissionService permissionServiceOverride)
        {
            _permissionService = permissionServiceOverride;
            _securityService = SecurityFactory.Instance;
            _excludeIncludeFieldsHelper = new ExcludeIncludeFieldsHelper();
        }



        public EntityCollection GetBankHierarchy()
        {
            var banks = new EntityCollection(new BankEntityFactory());
            var bankCollection = new EntityCollection(new BankEntityFactory());

            using (var adapter = new DataAccessAdapter())
            {
                IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.BankEntity));
                prefetchPath.Add(BankEntity.PrefetchPathCustomBankPropertyCollection);

                adapter.FetchEntityCollection(banks, null, prefetchPath);
            }

            IPredicate rootFilter = BankFields.ParentBankId == DBNull.Value;
            List<int> rootBanks = null;
            rootBanks = banks.FindMatches(rootFilter);

            foreach (var index in rootBanks)
            {
                var rootBank = (BankEntity)banks[index];
                var userHasExplicitViewPermissionForRootBank = false;
                var userHasAPermissionForRootBank = false;

                userHasExplicitViewPermissionForRootBank = _permissionService.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, rootBank.Id);

                userHasAPermissionForRootBank = userHasExplicitViewPermissionForRootBank;
                if (!userHasAPermissionForRootBank)
                {
                    userHasAPermissionForRootBank = _permissionService.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, rootBank.Id);
                }

                AddChildren(index, banks, userHasExplicitViewPermissionForRootBank);
                if (userHasAPermissionForRootBank || (rootBank.BankCollection.Count > 0))
                {
                    bankCollection.Add(rootBank);
                }
            }
            return bankCollection;
        }

        public SerializableDictionaryInteger FetchAllRelations()
        {
            var s = new SerializableDictionaryInteger();
            using (var adapter = new DataAccessAdapter())
            {
                var banks = new EntityCollection(new BankEntityFactory());
                adapter.FetchEntityCollection(banks, null);

                foreach (var entity_loopVariable in banks)
                {
                    var bank = (BankEntity)entity_loopVariable;
                    s.Add(bank.Id, bank.ParentBankId);
                }
            }
            return s;
        }

        private void AddChildren(int rootIndex, CollectionCore<EntityBase2> banks,
    bool userHasExplicitViewPermissonForAncestorBank)
        {
            var rootBank = (BankEntity)banks[rootIndex];
            List<int> childBanks = null;
            IPredicate childFilter = BankFields.ParentBankId == rootBank.Id;

            childBanks = banks.FindMatches(childFilter);

            if (!childBanks.Any())
            {
                return;
            }

            var explicitViewPermissonForAncestorBank = userHasExplicitViewPermissonForAncestorBank;
            if (!userHasExplicitViewPermissonForAncestorBank)
            {
                explicitViewPermissonForAncestorBank = _permissionService.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, rootBank.Id);
            }

            foreach (var index in childBanks)
            {
                var childBank = (BankEntity)banks[index];
                var userHasAPermissionForBank = false;

                userHasAPermissionForBank = userHasExplicitViewPermissonForAncestorBank;
                if (!userHasAPermissionForBank)
                {
                    userHasAPermissionForBank = _permissionService.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, childBank.Id);
                }

                AddChildren(index, banks, explicitViewPermissonForAncestorBank);

                if (userHasAPermissionForBank || childBank.BankCollection.Count > 0)
                {
                    rootBank.BankCollection.Add(childBank);
                }
            }
        }

        public void UpdateBankHierarchy(EntityCollection banks)
        {
            using (var adapter = new DataAccessAdapter())
            {
                foreach (var b in banks)
                {
                    SaveBanksRecurseHierarchy((BankEntity)b, adapter);
                }
            }
        }

        private void SaveBanksRecurseHierarchy(BankEntity b, DataAccessAdapter adapter)
        {
            if (b.IsDirty)
            {
                adapter.SaveEntity(b);
            }

            foreach (var sb in b.BankCollection)
            {
                SaveBanksRecurseHierarchy(sb, adapter);
            }
        }

        public EntityCollection GetBankHierarchyFilteredByBankIds(int[] bankIds)
        {
            var banks = new EntityCollection(new BankEntityFactory());
            var filteredCollection = new EntityCollection(new BankEntityFactory());

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(banks, null);
            }

            IPredicate rootFilter = BankFields.ParentBankId == DBNull.Value;
            List<int> rootBanks = null;
            rootBanks = banks.FindMatches(rootFilter);

            foreach (var index in rootBanks)
            {
                var rootBank = (BankEntity)banks[index];

                AddChildren(bankIds, index, banks);

                if (rootBank.BankCollection.Count > 0 || ((IList)bankIds).Contains(rootBank.Id))
                {
                    filteredCollection.Add(rootBank);
                }
            }
            return filteredCollection;
        }


        private void AddChildren(int[] bankIds, int rootIndex, EntityCollection banks)
        {
            var rootBank = (BankEntity)banks[rootIndex];
            List<int> childBanks;
            IPredicate childFilter = BankFields.ParentBankId == rootBank.Id;

            childBanks = banks.FindMatches(childFilter);
            if (!childBanks.Any())
            {
                return;
            }

            foreach (var index in childBanks)
            {
                var childBank = (BankEntity)banks[index];

                AddChildren(bankIds, index, banks);

                if (childBank.BankCollection.Count > 0 || ((IList)bankIds).Contains(childBank.Id))
                    rootBank.BankCollection.Add(childBank);
            }
        }




        public BankEntity GetBank(int bankId)
        {
            return GetBankWithOptions(bankId, false, false);
        }

        public List<int> GetListOfBankIds()
        {
            var banks = new EntityCollection(new BankEntityFactory());

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(banks, null, 0, null, null, _excludeIncludeFieldsHelper.GetBankIncludedFieldList());
            }
            return banks.Select(b => ((BankEntity)b).Id).ToList();
        }

        public bool BankExists(int bankId)
        {
            var banks = new EntityCollection(new BankEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var _with1 = filter;
            _with1.PredicateExpression.Add(BankFields.Id == bankId);
            var resourceExists = false;
            using (var adapter = new DataAccessAdapter())
            {
                resourceExists = adapter.GetDbCount(banks, filter) > 0;
            }
            return resourceExists;
        }

        public string GetBankPath(int bankId)
        {
            StringBuilder bankPath = null;
            var bank = new BankEntity(bankId);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(bank);
                bankPath = new StringBuilder(bank.Name);

                while (bank.ParentBankId.HasValue)
                {
                    bank = new BankEntity(bank.ParentBankId.Value);
                    adapter.FetchEntity(bank);
                    if (bank != null)
                    {
                        bankPath.Insert(0, $"{bank.Name}/");
                    }
                }
            }

            return bankPath.ToString();
        }

        public BankEntity GetBankWithOptions(int bankId, bool withEditInfo, bool withCustomProperties)
        {
            BankEntity returnValue = null;

            using (var adapter = new DataAccessAdapter())
            {
                var bank = new BankEntity(bankId);
                IPrefetchPath2 prefetchPath = null;

                if (withEditInfo)
                {
                    prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.BankEntity));
                    prefetchPath.Add(BankEntity.PrefetchPathCreatedByUser,
                        _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                    prefetchPath.Add(BankEntity.PrefetchPathModifiedByUser,
                        _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                }
                if (withCustomProperties)
                {
                    if (prefetchPath == null)
                        prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.BankEntity));
                    var customPropertyPath = prefetchPath.Add(BankEntity.PrefetchPathCustomBankPropertyCollection);

                    customPropertyPath.SubPath.Add(ListCustomBankPropertyEntity
                        .PrefetchPathListValueCustomBankPropertyCollection);
                    var conceptStructurePartPath =
                        customPropertyPath.SubPath.Add(ConceptStructureCustomBankPropertyEntity
                            .PrefetchPathConceptStructurePartCustomBankPropertyCollection);
                    var depPath2 = conceptStructurePartPath.SubPath.Add(ConceptStructurePartCustomBankPropertyEntity
                        .PrefetchPathChildConceptStructurePartCustomBankPropertyCollection);
                    depPath2.SubPath.Add(ChildConceptStructurePartCustomBankPropertyEntity
                        .PrefetchPathChildConceptStructurePartCustomBankProperty);

                    var treeStructurePartPath = customPropertyPath.SubPath.Add(TreeStructureCustomBankPropertyEntity
                        .PrefetchPathTreeStructurePartCustomBankPropertyCollection);
                    var depPath3 = treeStructurePartPath.SubPath.Add(TreeStructurePartCustomBankPropertyEntity
                        .PrefetchPathChildTreeStructurePartCustomBankPropertyCollection);

                    customPropertyPath.SubPath.Add(CustomBankPropertyEntity.PrefetchPathModifiedByUser,
                        _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                    customPropertyPath.SubPath.Add(CustomBankPropertyEntity.PrefetchPathCreatedByUser,
                        _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                }

                if (_permissionService.TryUserIsPermittedTo(TestBuilderPermissionAccess.DALRead,
                    TestBuilderPermissionTarget.BankEntity, bankId))
                    if (prefetchPath != null)
                    {
                        if (adapter.FetchEntity(bank, prefetchPath))
                        {
                            returnValue = bank;
                        }
                    }
                    else
                    {
                        if (adapter.FetchEntity(bank))
                        {
                            returnValue = bank;
                        }
                    }
            }
            return returnValue;
        }

        public string GetBankName(int bankId)
        {
            var bank = GetBankWithOptions(bankId, false, false);
            if (bank != null)
            {
                return bank.Name;
            }

            return string.Empty;
        }

        public void UpdateBank(BankEntity bank)
        {
            if (bank == null)
            {
                throw new ArgumentNullException(nameof(bank));
            }

            if (bank.IsNew)
            {
                var parentBankId = 0;

                if (bank.ParentBankId.HasValue)
                {
                    parentBankId = bank.ParentBankId.Value;
                }

                _permissionService.UserIsPermittedTo(TestBuilderPermissionAccess.DALCreate, TestBuilderPermissionTarget.BankEntity, parentBankId);
            }
            else
            {
                _permissionService.UserIsPermittedTo(TestBuilderPermissionAccess.DALUpdate, TestBuilderPermissionTarget.BankEntity, bank.Id);
            }

            using (var adapter = new DataAccessAdapter())
            {
                SaveChildStructurePartCustomBankProperties(bank.CustomBankPropertyCollection, adapter);

                adapter.SaveEntity(bank, true);
            }
        }

        private void SaveChildStructurePartCustomBankProperties(IEnumerable customBankPropertyCollection,
    DataAccessAdapter adapter)
        {
            foreach (CustomBankPropertyEntity customBankPropertyEntity in customBankPropertyCollection)
            {
                if (customBankPropertyEntity is ConceptStructureCustomBankPropertyEntity)
                {
                    SaveChildConceptStructurePartCustomBankProperty((ConceptStructureCustomBankPropertyEntity)customBankPropertyEntity, adapter);

                }
                else if (customBankPropertyEntity is TreeStructureCustomBankPropertyEntity)
                {
                    adapter.SaveEntity((TreeStructureCustomBankPropertyEntity)customBankPropertyEntity, true, true);
                }
            }
        }

        private void SaveChildConceptStructurePartCustomBankProperty(
    ConceptStructureCustomBankPropertyEntity conceptStructureCustomBankPropertyEntity,
    DataAccessAdapter adapter)
        {
            adapter.SaveEntity(conceptStructureCustomBankPropertyEntity, true, false);

            foreach (var conceptStructurePartCustomBankPropertyEntity in conceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection)
            {
                if (conceptStructurePartCustomBankPropertyEntity.IsNew)
                {
                    adapter.SaveEntity(conceptStructurePartCustomBankPropertyEntity, true, false);
                }
            }

            foreach (var conceptStructurePartCustomBankPropertyEntity in conceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection)
            {
                if (!conceptStructurePartCustomBankPropertyEntity.IsNew)
                {
                    continue;
                }

                foreach (var childConceptStructurePartCustomBankPropertyEntity in
                    conceptStructurePartCustomBankPropertyEntity
                        .ChildConceptStructurePartCustomBankPropertyCollection)
                {
                    adapter.SaveEntity(childConceptStructurePartCustomBankPropertyEntity, true, false);
                }
            }
        }

        public string DeleteBank(BankEntity bank)
        {
            string result = null;

            if (bank == null)
            {
                throw new ArgumentNullException(nameof(bank));
            }

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    _permissionService.UserIsPermittedTo(TestBuilderPermissionAccess.DALDelete,
    TestBuilderPermissionTarget.BankEntity, bank.Id);

                    if (_securityService.IsBankAssignedToUserThroughBankRole(bank.Id) &&
!_permissionService.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Delete,
TestBuilderPermissionTarget.BankEntity, TestBuilderPermissionNamedTask.None, 0, 0))
                    {
                        result = string.Format(Resources.BankAssignedThroughAUserRole, bank.Name);
                    }
                    else
                    {
                        var currentBankResources = new EntityCollection();

                        addCurrentBankResources(currentBankResources,
                            ResourceFactory.Instance.GetAssessmentTestsForBank(bank.Id), bank.Id);
                        addCurrentBankResources(currentBankResources,
                            ResourceFactory.Instance.GetAssessmentTestTemplatesForBank(bank.Id), bank.Id);
                        addCurrentBankResources(currentBankResources, ResourceFactory.Instance.GetItemsForBank(bank.Id),
                            bank.Id);
                        addCurrentBankResources(currentBankResources,
                            ResourceFactory.Instance.GetItemLayoutTemplatesForBank(bank.Id), bank.Id);
                        addCurrentBankResources(currentBankResources,
                            ResourceFactory.Instance.GetGenericResourceForBank(bank.Id, string.Empty, string.Empty,
                                false), bank.Id);
                        addCurrentBankResources(currentBankResources,
                            ResourceFactory.Instance.GetControlTemplatesForBank(bank.Id), bank.Id);

                        if (currentBankResources.Count == 0)
                        {
                            adapter.DeleteEntity(bank);
                        }

                        else result = Resources.BankIsNotEmptyAllResourcesShouldBeDeletedFirst;
                    }
                }
                catch (ORMQueryExecutionException ex)
                {
                    result = string.Format(Resources.BankForeignKeyViolation, bank.Name);
                }
                catch (SecurityException securityException)
                {
                    result = securityException.Message;
                }
            }

            return result;
        }


        private void addCurrentBankResources(EntityCollection currentResources, EntityCollection bankresources, int bankId)
        {
            foreach (ResourceEntity resourceEntity in bankresources)
            {
                if (resourceEntity.BankId == bankId)
                {
                    currentResources.Add(resourceEntity);
                }
            }
        }

        public ResourcePropertyDefinitionCollection GetResourcePropertyDefinitions(BankEntity bank)
        {
            var result = new ResourcePropertyDefinitionCollection();
            var builder = new BankResourcePropertyBuilder(this);

            result.AddRange(builder.AddStaticPropertyDefinitionsOfResource());
            result.AddRange(builder.AddDynamicPropertyDefinitionsOfResource(bank.Id));

            return result;
        }

        public EntityCollection GetReferencesForCustomBankProperty(CustomBankPropertyEntity customBankProperty)
        {
            IPrefetchPath2 customBankPropertyPrefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.CustomBankPropertyEntity));

            var customBankPropertyValueCollectionPrefethPath = CustomBankPropertyEntity.PrefetchPathCustomBankPropertyValueCollection;
            customBankPropertyValueCollectionPrefethPath.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathResource);
            customBankPropertyPrefetchPath.Add(customBankPropertyValueCollectionPrefethPath);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(customBankProperty, customBankPropertyPrefetchPath);
            }

            var referencedResources = new EntityCollection(new ResourceEntityFactory());

            foreach (var customBankPropertyValueEntity in customBankProperty.CustomBankPropertyValueCollection)
            {
                if (customBankPropertyValueEntity.Resource != null)
                {
                    referencedResources.Add(customBankPropertyValueEntity.Resource);
                }
            }
            return referencedResources;
        }

        public EntityCollection GetReferencedCustomBankPropertiesForListOfResources(List<Guid> resourceIds)
        {
            var customProperties = new EntityCollection(new CustomBankPropertyEntityFactory());
            var customPropertyIds = new List<Guid>();
            var resourceOffset = 0;
            const int maxBatch = 1000;

            while (resourceOffset != resourceIds.Count)
            {
                var resourcesInThisBatch = maxBatch;
                if (resourceIds.Count - resourceOffset < maxBatch)
                {
                    resourcesInThisBatch = resourceIds.Count - resourceOffset;
                }

                var batchResourceList = (Guid[])Array.CreateInstance(typeof(Guid), resourcesInThisBatch);
                resourceIds.CopyTo(resourceOffset, batchResourceList, 0, resourcesInThisBatch);
                var bucket = new RelationPredicateBucket();
                var _with2 = bucket;
                _with2.PredicateExpression.Add(new FieldCompareRangePredicate(CustomBankPropertyValueFields.ResourceId, null, batchResourceList));

                var fields = new ResultsetFields(1);
                fields.DefineField(CustomBankPropertyValueFields.CustomBankPropertyId, 0, "customBankPropertyId");

                IGroupByCollection groupByClause = new GroupByCollection();
                groupByClause.Add(fields["customBankPropertyId"]);

                var dynamicList = new DataTable();
                using (var adapter = new DataAccessAdapter())
                {
                    adapter.FetchTypedList(fields, dynamicList, bucket, 0, null, true, groupByClause);
                }

                foreach (DataRow row in dynamicList.Rows)
                {
                    if (!customPropertyIds.Contains((Guid)row["customBankPropertyId"]))
                    {
                        customPropertyIds.Add((Guid)row["customBankPropertyId"]);
                    }
                }

                resourceOffset += resourcesInThisBatch;
            }

            if (customPropertyIds.Count > 0)
            {
                IRelationPredicateBucket filter = new RelationPredicateBucket();
                var _with3 = filter;
                var index = 0;
                foreach (var customPropertyId_loopVariable in customPropertyIds)
                {
                    var customPropertyId = customPropertyId_loopVariable;
                    if (index == 0)
                    {
                        _with3.PredicateExpression.Add(CustomBankPropertyFields.CustomBankPropertyId == customPropertyId);
                    }
                    else
                    {
                        _with3.PredicateExpression.AddWithOr(CustomBankPropertyFields.CustomBankPropertyId == customPropertyId);
                    }
                    index += 1;
                }
                using (var adapter = new DataAccessAdapter())
                {
                    adapter.FetchEntityCollection(customProperties, filter, 0);
                }
            }
            return customProperties;
        }



        public BankStatistics GetBankStatistics(int bankId, string userName)
        {
            var resultSet = RetrievalProcedures.GetBankStatistics(bankId, userName);
            return new DataSetToBanklStatistics().Convert(resultSet);
        }

        public bool HasDependingResourcesInSubBanks(int bankId)
        {
            var resultSet = RetrievalProcedures.HasDependingResourcesInSubBanks(bankId);
            var count = (int)resultSet.Rows[0][0];
            return count > 0;
        }

        public bool ClearBank(int bankId)
        {
            var returnValue = false;
            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    adapter.CommandTimeOut = 600;

                    if (!HasDependingResourcesInSubBanks(bankId))
                    {
                        ActionProcedures.ClearBank(bankId, adapter);
                        returnValue = true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return returnValue;
        }

        public EntityCollection GetAllConceptStructuresForProperty(
    ConceptStructureCustomBankPropertyEntity conceptStructureCustomBankPropertyEntity)
        {
            var returnValue = new EntityCollection(new ConceptStructurePartCustomBankPropertyEntityFactory());

            var filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(
                new PredicateExpression(ConceptStructurePartCustomBankPropertyFields.CustomBankPropertyId ==
                                        conceptStructureCustomBankPropertyEntity.CustomBankPropertyId));

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, 0);
            }

            return returnValue;
        }

        public EntityCollection GetAllTreeStructuresForProperty(
    TreeStructureCustomBankPropertyEntity treeStructureCustomBankPropertyEntity)
        {
            var returnValue = new EntityCollection(new TreeStructurePartCustomBankPropertyEntityFactory());

            var filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(
                new PredicateExpression(TreeStructurePartCustomBankPropertyFields.CustomBankPropertyId ==
                                        treeStructureCustomBankPropertyEntity.CustomBankPropertyId));

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, 0);
            }

            return returnValue;
        }

        public EntityCollection GetAllConceptTypes()
        {
            var collectionToReturn = new EntityCollection(new ConceptTypeEntityFactory());
            var filter = new RelationPredicateBucket();

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(collectionToReturn, filter, 0);
            }

            return collectionToReturn;
        }




        public EntityCollection GetCustomBankPropertiesForBranchById(int bankId, ResourceTypeEnum applicableTo)
        {
            var collectionToReturn = new EntityCollection(new CustomBankPropertyEntityFactory());
            using (var adapter = new DataAccessAdapter())
            {
                int[] ids = null;

                var bankBranchHelper = new BankBranchIdHelper();
                ids = bankBranchHelper.GetBankBrancheIds(bankId, TestBuilderPermissionTarget.Any,
                    TestBuilderPermissionAccess.AnyTask);

                var filter = new RelationPredicateBucket();
                var fieldCompareRange = new FieldCompareRangePredicate(CustomBankPropertyFields.BankId, null, ids);
                filter.PredicateExpression.Add(fieldCompareRange);

                if (applicableTo != ResourceTypeEnum.AllResources)
                    filter.PredicateExpression.Add(
                        CustomBankPropertyFields.ApplicableToMask.SetExpression(new Expression(
                            CustomBankPropertyFields.ApplicableToMask, ExOp.BitwiseAnd,
                            Convert.ToInt32(applicableTo))) == Convert.ToInt32(applicableTo));

                IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.CustomBankPropertyEntity));
                prefetchPath.Add(CustomBankPropertyEntity.PrefetchPathCreatedByUser,
                    _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                prefetchPath.Add(CustomBankPropertyEntity.PrefetchPathModifiedByUser,
                    _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
                prefetchPath.Add(CustomBankPropertyEntity.PrefetchPathBank);
                prefetchPath.Add(ListCustomBankPropertyEntity.PrefetchPathListValueCustomBankPropertyCollection);

                var conceptStructurePartPath = prefetchPath.Add(ConceptStructureCustomBankPropertyEntity
                    .PrefetchPathConceptStructurePartCustomBankPropertyCollection);
                conceptStructurePartPath.SubPath.Add(ConceptStructurePartCustomBankPropertyEntity
                    .PrefetchPathConceptType);
                var depPath = conceptStructurePartPath.SubPath.Add(ConceptStructurePartCustomBankPropertyEntity
                    .PrefetchPathChildConceptStructurePartCustomBankPropertyCollection);
                depPath.SubPath.Add(ChildConceptStructurePartCustomBankPropertyEntity
                    .PrefetchPathChildConceptStructurePartCustomBankProperty);

                var treeStructurePartPath = prefetchPath.Add(TreeStructureCustomBankPropertyEntity
                    .PrefetchPathTreeStructurePartCustomBankPropertyCollection);
                var depPath2 = treeStructurePartPath.SubPath.Add(TreeStructurePartCustomBankPropertyEntity
                    .PrefetchPathChildTreeStructurePartCustomBankPropertyCollection);

                var sorter = new SortExpression(CustomBankPropertyFields.Title | SortOperator.Ascending);

                adapter.FetchEntityCollection(collectionToReturn, filter, 0, sorter, prefetchPath);
            }

            return collectionToReturn;
        }


        public EntityCollection GetCustomBankPropertiesForBranch(BankEntity bank, ResourceTypeEnum applicableTo)
        {
            if (bank == null)
                throw new ArgumentNullException(nameof(bank));
            return GetCustomBankPropertiesForBranchById(bank.Id, applicableTo);
        }

        public TreeStructureCustomBankPropertyEntity GetTreeStructureCustomBankProperty(
            TreeStructureCustomBankPropertyEntity customProperty)
        {
            if (customProperty == null)
                throw new ArgumentNullException(nameof(customProperty));

            var fetchedResource = new TreeStructureCustomBankPropertyEntity(customProperty.CustomBankPropertyId);
            PrefetchCustomBankProperties(fetchedResource, EntityType.TreeStructureCustomBankPropertyEntity);

            return fetchedResource;
        }

        public ConceptStructureCustomBankPropertyEntity GetConceptStructureCustomBankProperty(
            ConceptStructureCustomBankPropertyEntity customProperty)
        {
            if (customProperty == null)
            {
                throw new ArgumentNullException(nameof(customProperty));
            }

            var fetchedResource = new ConceptStructureCustomBankPropertyEntity(customProperty.CustomBankPropertyId);
            PrefetchCustomBankProperties(fetchedResource, EntityType.ConceptStructureCustomBankPropertyEntity);

            return fetchedResource;
        }

        public ListCustomBankPropertyEntity GetListCustomBankProperty(ListCustomBankPropertyEntity customProperty)
        {
            if (customProperty == null)
            {
                throw new ArgumentNullException(nameof(customProperty));
            }

            var fetchedResource = new ListCustomBankPropertyEntity(customProperty.CustomBankPropertyId);
            PrefetchCustomBankProperties(fetchedResource, EntityType.ListValueCustomBankPropertyEntity);

            return fetchedResource;
        }

        public FreeValueCustomBankPropertyEntity GetFreeValueCustomBankProperty(
            FreeValueCustomBankPropertyEntity customProperty)
        {
            if (customProperty == null)
            {
                throw new ArgumentNullException(nameof(customProperty));
            }

            var fetchedResource = new FreeValueCustomBankPropertyEntity(customProperty.CustomBankPropertyId);
            PrefetchCustomBankProperties(fetchedResource, EntityType.FreeValueCustomBankPropertyEntity);

            return fetchedResource;
        }

        public RichTextValueCustomBankPropertyEntity GetRichTextValueCustomBankProperty(
            RichTextValueCustomBankPropertyEntity customProperty)
        {
            if (customProperty == null)
            {
                throw new ArgumentNullException(nameof(customProperty));
            }

            var fetchedResource = new RichTextValueCustomBankPropertyEntity(customProperty.CustomBankPropertyId);
            PrefetchCustomBankProperties(fetchedResource, EntityType.RichTextValueCustomBankPropertyEntity);

            return fetchedResource;
        }

        public CustomBankPropertyEntity GetCustomBankProperty(Guid customBankPropertyId)
        {
            return GetCustomBankProperties(new List<Guid> { customBankPropertyId }).FirstOrDefault();
        }

        public IList<CustomBankPropertyEntity> GetCustomBankProperties(IList<Guid> customBankPropertyIds)
        {
            var customProperties = new EntityCollection(new CustomBankPropertyEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var _with4 = filter;
            foreach (var id_loopVariable in customBankPropertyIds)
                _with4.PredicateExpression.AddWithOr(CustomBankPropertyFields.CustomBankPropertyId == id_loopVariable);

            IPrefetchPath2 customBankPropertyPrefetchPath =
    new PrefetchPath2(Convert.ToInt32(EntityType.CustomBankPropertyEntity));
            var treeStructurePartPath = customBankPropertyPrefetchPath.Add(TreeStructureCustomBankPropertyEntity
                .PrefetchPathTreeStructurePartCustomBankPropertyCollection);
            treeStructurePartPath.SubPath.Add(TreeStructurePartCustomBankPropertyEntity
                .PrefetchPathChildTreeStructurePartCustomBankPropertyCollection);

            var conceptStructurePartPath = customBankPropertyPrefetchPath.Add(ConceptStructureCustomBankPropertyEntity
    .PrefetchPathConceptStructurePartCustomBankPropertyCollection);
            conceptStructurePartPath.SubPath.Add(ConceptStructurePartCustomBankPropertyEntity
                .PrefetchPathChildConceptStructurePartCustomBankPropertyCollection);

            var listValuesPath = customBankPropertyPrefetchPath.Add(ListCustomBankPropertyEntity
    .PrefetchPathListValueCustomBankPropertyCollection);

            customBankPropertyPrefetchPath
                .Add(CustomBankPropertyEntity.PrefetchPathState,
                    _excludeIncludeFieldsHelper.GetStateExcludedFieldList()).SubPath
                .Add(StateEntity.PrefetchPathStateActionCollection).SubPath.Add(StateActionEntity.PrefetchPathAction);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(customProperties, filter, customBankPropertyPrefetchPath);
            }

            return customProperties.OfType<CustomBankPropertyEntity>().ToList();
        }

        private void PrefetchCustomBankProperties(CustomBankPropertyEntity resource, EntityType type)
        {
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(type));
            prefetchPath.Add(CustomBankPropertyEntity.PrefetchPathCreatedByUser,
                _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
            prefetchPath.Add(CustomBankPropertyEntity.PrefetchPathModifiedByUser,
                _excludeIncludeFieldsHelper.GetUserIncludedFieldList());
            prefetchPath.Add(CustomBankPropertyEntity.PrefetchPathBank);

            var customBankPropertyValuesPath =
                prefetchPath.Add(ResourceEntity.PrefetchPathCustomBankPropertyValueCollection);
            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                .PrefetchPathConceptStructureCustomBankProperty);
            customBankPropertyValuesPath.SubPath.Add(ConceptStructureCustomBankPropertyValueEntity
                .PrefetchPathConceptStructureCustomBankPropertySelectedPartCollection);
            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                .PrefetchPathTreeStructureCustomBankProperty);
            customBankPropertyValuesPath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                .PrefetchPathTreeStructureCustomBankPropertySelectedPartCollection);
            customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListCustomBankPropertySelectedValueCollection);
            customBankPropertyValuesPath.SubPath.Add(ListCustomBankPropertyValueEntity
                .PrefetchPathListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue);
            customBankPropertyValuesPath.SubPath.Add(CustomBankPropertyValueEntity.PrefetchPathCustomBankProperty);

            prefetchPath
                .Add(CustomBankPropertyEntity.PrefetchPathState,
                    _excludeIncludeFieldsHelper.GetStateExcludedFieldList()).SubPath
                .Add(StateEntity.PrefetchPathStateActionCollection).SubPath.Add(StateActionEntity.PrefetchPathAction);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntity(resource, prefetchPath);
            }
        }

        public string UpdateCustomProperty(CustomBankPropertyEntity customBankProperty)
        {
            string result = null;

            if (customBankProperty == null)
            {
                throw new ArgumentNullException(nameof(customBankProperty));
            }

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    if (customBankProperty is ConceptStructureCustomBankPropertyEntity)
                    {
                        SaveChildConceptStructurePartCustomBankProperty((ConceptStructureCustomBankPropertyEntity)customBankProperty, adapter);
                    }
                    else if (customBankProperty is TreeStructureCustomBankPropertyEntity)
                    {
                        adapter.SaveEntity((TreeStructureCustomBankPropertyEntity)customBankProperty, true, true);
                    }

                    adapter.SaveEntity(customBankProperty, true, true);
                }
                catch (ORMQueryExecutionException ex)
                {
                    result = "Error updating entity";
                }
                catch (SecurityException securityException)
                {
                    result = securityException.Message;
                }

                return result;
            }
        }

        public string UpdateCustomPropertyValue(CustomBankPropertyValueEntity customBankPropertyValue)
        {
            string result = null;

            if (customBankPropertyValue == null)
            {
                throw new ArgumentNullException(nameof(customBankPropertyValue));
            }

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    adapter.SaveEntity(customBankPropertyValue, true);
                }
                catch (ORMQueryExecutionException ex)
                {
                    result = "Error updating entity";
                }
                catch (SecurityException securityException)
                {
                    result = securityException.Message;
                }

                return result;
            }
        }

        public string UpdateCustomPropertyValues(EntityCollection customBankPropertyValues)
        {
            string result = null;

            if (customBankPropertyValues == null)
            {
                throw new ArgumentNullException(nameof(customBankPropertyValues));
            }

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    adapter.SaveEntityCollection(customBankPropertyValues, false, false);
                }
                catch (ORMQueryExecutionException ex)
                {
                    result = string.Format("Error updating Custom Properties Values.{0}Error: {1}", Environment.NewLine,
                        ex.Message);
                }
                catch (SecurityException securityException)
                {
                    result = securityException.Message;
                }

                return result;
            }
        }

        public string UpdateCustomProperties(EntityCollection entitiesToUpdate)
        {
            string result = null;

            if (entitiesToUpdate == null)
            {
                throw new ArgumentNullException(nameof(entitiesToUpdate));
            }

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    SaveChildStructurePartCustomBankProperties(entitiesToUpdate, adapter);

                    adapter.SaveEntityCollection(entitiesToUpdate, false, true);
                }
                catch (ORMQueryExecutionException ex)
                {
                    result = string.Format("Error updating Custom Properties.{0}Error: {1}", Environment.NewLine,
                        ex.Message);
                }
                catch (SecurityException securityException)
                {
                    result = securityException.Message;
                }

                return result;
            }
        }

        public string DeleteCustomProperties(EntityCollection entitiesToRemove)
        {
            string result = null;

            if (entitiesToRemove == null)
            {
                throw new ArgumentNullException(nameof(entitiesToRemove));
            }

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    adapter.DeleteEntityCollection(entitiesToRemove);
                }
                catch (ORMQueryExecutionException ex)
                {
                    result = string.Format("Error updating Custom Properties.{0}Error: {1}", Environment.NewLine,
                        ex.Message);
                }
                catch (SecurityException securityException)
                {
                    result = securityException.Message;
                }
            }

            return result;
        }

        public string DeleteCustomPropertiesForced(EntityCollection entitiesToRemove)
        {
            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    foreach (var entityToRemove in entitiesToRemove)
                    {
                        var filter = new RelationPredicateBucket();
                        if (entityToRemove is FreeValueCustomBankPropertyEntity)
                        {
                            var cpF = (FreeValueCustomBankPropertyEntity)entityToRemove;
                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(FreeValueCustomBankPropertyValueFields.CustomBankPropertyId == cpF.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(FreeValueCustomBankPropertyValueEntity), filter);

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(CustomBankPropertyValueFields.CustomBankPropertyId == cpF.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(CustomBankPropertyValueEntity), filter);
                        }
                        if (entityToRemove is ListCustomBankPropertyEntity)
                        {
                            var cpL = (ListCustomBankPropertyEntity)entityToRemove;
                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(ListCustomBankPropertyValueFields.CustomBankPropertyId == cpL.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(ListCustomBankPropertyValueEntity), filter);

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(ListCustomBankPropertySelectedValueFields.CustomBankPropertyId == cpL.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(ListCustomBankPropertySelectedValueEntity), filter);

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(CustomBankPropertyValueFields.CustomBankPropertyId == cpL.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(CustomBankPropertyValueEntity), filter);
                        }

                        if (entityToRemove is ListValueCustomBankPropertyEntity)
                        {
                            var cpLv = (ListValueCustomBankPropertyEntity)entityToRemove;
                            var valueReferences = GetListCustomBankPropertyValueReferences(cpLv.ListValueBankCustomPropertyId);
                            List<Guid> resourceIds = valueReferences.Select(r => ((ListCustomBankPropertySelectedValueEntity)r).ResourceId).ToList();

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(ListCustomBankPropertySelectedValueFields.ListValueBankCustomPropertyId == cpLv.ListValueBankCustomPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(ListCustomBankPropertySelectedValueEntity), filter);


                            var remainingCombinations = GetListCustomBankPropertyValueReferences(cpLv.CustomBankPropertyId, resourceIds);
                            List<Guid> resourceIdsToRemove = resourceIds.Except(remainingCombinations.Select(r => ((ListCustomBankPropertySelectedValueEntity)r).ResourceId).ToList()).ToList();

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(CustomBankPropertyValueFields.CustomBankPropertyId == cpLv.CustomBankPropertyId);
                            filter.PredicateExpression.Add(new FieldCompareRangePredicate(CustomBankPropertyValueFields.ResourceId, null, resourceIdsToRemove));
                            adapter.DeleteEntitiesDirectly(typeof(CustomBankPropertyValueEntity), filter);
                        }

                        if (entityToRemove is ConceptStructureCustomBankPropertyEntity)
                        {
                            var cs = (ConceptStructureCustomBankPropertyEntity)entityToRemove;

                            foreach (var conceptStructurePartCustomBankPropertyEntity in cs.ConceptStructurePartCustomBankPropertyCollection)
                            {
                                filter.PredicateExpression.Clear();
                                filter.PredicateExpression.Add(ChildConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId == conceptStructurePartCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyId);
                                adapter.DeleteEntitiesDirectly(typeof(ChildConceptStructurePartCustomBankPropertyEntity), filter);

                                filter.PredicateExpression.Clear();
                                filter.PredicateExpression.Add(ConceptStructureCustomBankPropertySelectedPartFields.ConceptStructurePartId == conceptStructurePartCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyId);
                                adapter.DeleteEntitiesDirectly(typeof(ConceptStructureCustomBankPropertySelectedPartEntity), filter);
                            }

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(ConceptStructurePartCustomBankPropertyFields.CustomBankPropertyId == cs.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(ConceptStructurePartCustomBankPropertyEntity), filter);

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(CustomBankPropertyValueFields.CustomBankPropertyId == cs.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(CustomBankPropertyValueEntity), filter);
                        }

                        if (entityToRemove is ConceptStructurePartCustomBankPropertyEntity)
                        {
                            var csp = (ConceptStructurePartCustomBankPropertyEntity)entityToRemove;

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(ChildConceptStructurePartCustomBankPropertyFields.ConceptStructurePartCustomBankPropertyId == csp.ConceptStructurePartCustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(ChildConceptStructurePartCustomBankPropertyEntity), filter);

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(ConceptStructurePartCustomBankPropertyFields.CustomBankPropertyId == csp.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(ConceptStructurePartCustomBankPropertyEntity), filter);

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(CustomBankPropertyValueFields.CustomBankPropertyId == csp.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(CustomBankPropertyValueEntity), filter);
                        }

                        if (entityToRemove is TreeStructureCustomBankPropertyEntity)
                        {
                            var cs = (TreeStructureCustomBankPropertyEntity)entityToRemove;

                            foreach (var treeStructurePartCustomBankPropertyEntity in cs.TreeStructurePartCustomBankPropertyCollection)
                            {
                                foreach (TreeStructurePartCustomBankPropertyEntity part in GetLeaves(treeStructurePartCustomBankPropertyEntity))
                                {
                                    filter.PredicateExpression.Clear();
                                    filter.PredicateExpression.Add(ChildTreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId == part.TreeStructurePartCustomBankPropertyId);
                                    adapter.DeleteEntitiesDirectly(typeof(ChildTreeStructurePartCustomBankPropertyEntity), filter);

                                    filter.PredicateExpression.Clear();
                                    filter.PredicateExpression.Add(TreeStructureCustomBankPropertySelectedPartFields.TreeStructurePartId == part.TreeStructurePartCustomBankPropertyId);
                                    adapter.DeleteEntitiesDirectly(typeof(TreeStructureCustomBankPropertySelectedPartEntity), filter);
                                }
                            }

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(TreeStructurePartCustomBankPropertyFields.CustomBankPropertyId == cs.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(TreeStructurePartCustomBankPropertyEntity), filter);

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(CustomBankPropertyValueFields.CustomBankPropertyId == cs.CustomBankPropertyId);
                            adapter.DeleteEntitiesDirectly(typeof(CustomBankPropertyValueEntity), filter);
                        }

                        if (entityToRemove is TreeStructurePartCustomBankPropertyEntity)
                        {
                            var csp = (TreeStructurePartCustomBankPropertyEntity)entityToRemove;
                            var valueReferences = GetTreeStructureCustomBankPropertyValueReferences(csp.TreeStructurePartCustomBankPropertyId);
                            List<Guid> resourceIds = valueReferences.Select(r => ((TreeStructureCustomBankPropertySelectedPartEntity)r).ResourceId).ToList();

                            foreach (TreeStructurePartCustomBankPropertyEntity part in GetLeaves(csp))
                            {
                                filter.PredicateExpression.Clear();
                                filter.PredicateExpression.Add(TreeStructureCustomBankPropertySelectedPartFields.TreeStructurePartId == part.TreeStructurePartCustomBankPropertyId);
                                adapter.DeleteEntitiesDirectly(typeof(TreeStructureCustomBankPropertySelectedPartEntity), filter);

                                filter.PredicateExpression.Clear();
                                filter.PredicateExpression.Add(ChildTreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId == part.TreeStructurePartCustomBankPropertyId);
                                adapter.DeleteEntitiesDirectly(typeof(ChildTreeStructurePartCustomBankPropertyEntity), filter);
                            }


                            var remainingCombinations = GetTreeStructureCustomBankPropertyValueReferences(csp.CustomBankPropertyId, resourceIds);
                            List<Guid> resourceIdsToRemove = resourceIds.Except(remainingCombinations.Select(r => ((TreeStructureCustomBankPropertySelectedPartEntity)r).ResourceId).ToList()).ToList();

                            filter.PredicateExpression.Clear();
                            filter.PredicateExpression.Add(CustomBankPropertyValueFields.CustomBankPropertyId == csp.CustomBankPropertyId);
                            filter.PredicateExpression.Add(new FieldCompareRangePredicate(CustomBankPropertyValueFields.ResourceId, null, resourceIdsToRemove));
                            adapter.DeleteEntitiesDirectly(typeof(CustomBankPropertyValueEntity), filter);

                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                var result = DeleteCustomProperties(entitiesToRemove);
                return result;
            }
        }

        private List<TreeStructurePartCustomBankPropertyEntity> GetLeaves(TreeStructurePartCustomBankPropertyEntity part)
        {
            List<TreeStructurePartCustomBankPropertyEntity> result = new List<TreeStructurePartCustomBankPropertyEntity>();
            GetLeavesRecursive(part, ref result);

            return result;
        }

        private void GetLeavesRecursive(TreeStructurePartCustomBankPropertyEntity part, ref List<TreeStructurePartCustomBankPropertyEntity> list)
        {
            list.Add(part);
            if (part.ChildTreeStructurePartCustomBankPropertyCollection.Any())
            {
                foreach (ChildTreeStructurePartCustomBankPropertyEntity child in part.ChildTreeStructurePartCustomBankPropertyCollection)
                {
                    var treePart = GetTreeStructurePartCustomBankProperties(new List<Guid>(new[] { child.ChildTreeStructurePartCustomBankPropertyId }), false).OfType<TreeStructurePartCustomBankPropertyEntity>().FirstOrDefault();
                    if (treePart != null)
                    {
                        GetLeavesRecursive(treePart, ref list);
                    }
                }
            }
        }

        public string DeleteCustomPropertyValues(EntityCollection entitiesToRemove)
        {
            string result = null;

            if (entitiesToRemove == null) throw new ArgumentNullException("entitiesToRemove");

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    adapter.DeleteEntityCollection(entitiesToRemove);
                }
                catch (ORMQueryExecutionException ex)
                {
                    result = "Error deleting entities";
                }
                catch (SecurityException securityException)
                {
                    result = securityException.Message;
                }
            }

            return result;
        }

        public ConceptStructurePartCustomBankPropertyEntity PopulateConceptCustomBankPropertyHierarchy(Guid id)
        {
            var rootPartEntity = new ConceptStructurePartCustomBankPropertyEntity(id);
            using (var adapter = new DataAccessAdapter())
            {
                var childPropertyPath =
                    new PrefetchPath2(Convert.ToInt32(EntityType.ConceptStructurePartCustomBankPropertyEntity));
                var _with5 = childPropertyPath.Add(ConceptStructurePartCustomBankPropertyEntity
                    .PrefetchPathChildConceptStructurePartCustomBankPropertyCollection);
                _with5.SubPath.Add(ChildConceptStructurePartCustomBankPropertyEntity
                    .PrefetchPathChildConceptStructurePartCustomBankProperty);

                adapter.FetchEntity(rootPartEntity, childPropertyPath);
            }
            return rootPartEntity;
        }

        public TreeStructureCustomBankPropertyEntity PopulateTreeCustomBankPropertyHierarchy(Guid id)
        {
            var rootEntity = new TreeStructureCustomBankPropertyEntity(id);
            using (var adapter = new DataAccessAdapter())
            {
                IPrefetchPath2 customPropertyPath =
                    new PrefetchPath2(Convert.ToInt32(EntityType.TreeStructureCustomBankPropertyEntity));
                var treeStructureValuePath = customPropertyPath.Add(TreeStructureCustomBankPropertyEntity
                    .PrefetchPathTreeStructureCustomBankPropertyValueCollection);
                var treeSelectedValuePath =
                    treeStructureValuePath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                        .PrefetchPathTreeStructureCustomBankPropertySelectedPartCollection);
                var treeStructureParts = treeStructureValuePath.SubPath.Add(TreeStructureCustomBankPropertyValueEntity
                    .PrefetchPathTreeStructurePartCustomBankPropertyCollectionViaTreeStructureCustomBankPropertySelectedPart);
                treeStructureParts.SubPath.Add(TreeStructurePartCustomBankPropertyEntity
                    .PrefetchPathChildTreeStructurePartCustomBankPropertyCollection);

                adapter.FetchEntity(rootEntity, customPropertyPath);
            }
            return rootEntity;
        }

        public EntityCollection GetTreeStructurePartCustomBankPropertiesByCustomBankPropertyIds(List<Guid> ids)
        {
            var resources = new EntityCollection(new TreeStructurePartCustomBankPropertyEntityFactory());
            if (ids.Count == 0)
            {
                return resources;
            }
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var _with6 = filter;
            var index = 0;
            foreach (var id in ids)
            {
                if (index == 0)
                    _with6.PredicateExpression.Add(TreeStructurePartCustomBankPropertyFields.CustomBankPropertyId ==
                                                   id);
                else
                    _with6.PredicateExpression.AddWithOr(
                        TreeStructurePartCustomBankPropertyFields.CustomBankPropertyId == id);
                index += 1;
            }
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(resources, filter);
            }
            return resources;
        }

        public EntityCollection CustomBankPropertyExistsInBankhierarchy(BankEntity anchorBank, string resourceName)
        {
            var customBankProperties = new EntityCollection(new CustomBankPropertyEntityFactory());

            var bankBranchHelper = new BankBranchIdHelper();
            var ids = bankBranchHelper.GetBankBrancheIds(anchorBank, false);

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var _with7 = filter;
            _with7.PredicateExpression.Add(CustomBankPropertyFields.Name == resourceName);
            _with7.PredicateExpression.AddWithAnd(
    new FieldCompareRangePredicate(CustomBankPropertyFields.BankId, null, ids));
            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(customBankProperties, filter);
            }

            return customBankProperties;
        }


        public bool IsCustomBankPropertyValueReferenced(Guid id, CustomBankPropertyType customBankPropertyType)
        {
            IEntityCollection2 entityCollection = null;
            IRelationPredicateBucket filter = new RelationPredicateBucket();

            switch (customBankPropertyType)
            {
                case CustomBankPropertyType.Concept:
                    entityCollection = new EntityCollection<ConceptStructureCustomBankPropertySelectedPartEntity>();
                    filter.PredicateExpression.Add(
                        ConceptStructureCustomBankPropertySelectedPartFields.ResourceId == id);

                    break;
                case CustomBankPropertyType.ListMultipleSelect:
                case CustomBankPropertyType.ListSingleSelect:
                    entityCollection = new EntityCollection<ListCustomBankPropertySelectedValueEntity>();
                    filter.PredicateExpression.Add(ListCustomBankPropertySelectedValueFields
                                                       .ListValueBankCustomPropertyId == id);

                    break;
                case CustomBankPropertyType.Tree:
                    entityCollection = new EntityCollection<TreeStructureCustomBankPropertySelectedPartEntity>();
                    filter.PredicateExpression.Add(
                        TreeStructureCustomBankPropertySelectedPartFields.TreeStructurePartId == id);

                    break;
                default:
                    return false;
            }

            using (var adapter = new DataAccessAdapter())
            {
                return adapter.GetDbCount(entityCollection, filter) > 0;
            }
        }

        public EntityCollection GetListCustomBankPropertyValueReferences(Guid id)
        {
            var returnValue = new EntityCollection(new ListCustomBankPropertySelectedValueEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(ListCustomBankPropertySelectedValueFields.ListValueBankCustomPropertyId == id);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, 0);
            }
            return returnValue;
        }

        public EntityCollection GetListCustomBankPropertyValueReferences(Guid customBankPropertyid, List<Guid> resourceIds)
        {
            var returnValue = new EntityCollection(new ListCustomBankPropertySelectedValueEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(ListCustomBankPropertySelectedValueFields.CustomBankPropertyId == customBankPropertyid);
            filter.PredicateExpression.Add(new FieldCompareRangePredicate(ListCustomBankPropertySelectedValueFields.ResourceId, null, resourceIds));

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, 0);
            }
            return returnValue;
        }

        public EntityCollection GetListValueCustomBankProperties(List<Guid> ids)
        {
            var returnValue = new EntityCollection(new ListValueCustomBankPropertyEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var _with8 = filter;
            foreach (var id in ids)
            {
                _with8.PredicateExpression.AddWithOr(ListValueCustomBankPropertyFields.ListValueBankCustomPropertyId == id);
            }

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, 0);
            }
            return returnValue;
        }

        public EntityCollection GetTreeStructureCustomBankPropertyValueReferences(Guid id)
        {
            var returnValue = new EntityCollection(new TreeStructureCustomBankPropertySelectedPartEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(TreeStructureCustomBankPropertySelectedPartFields.TreeStructurePartId == id);

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, 0);
            }
            return returnValue;
        }

        public EntityCollection GetTreeStructureCustomBankPropertyValueReferences(Guid customBankPropertyid, List<Guid> resourceIds)
        {
            var returnValue = new EntityCollection(new TreeStructureCustomBankPropertySelectedPartEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(TreeStructureCustomBankPropertySelectedPartFields.CustomBankPropertyId == customBankPropertyid);
            filter.PredicateExpression.Add(new FieldCompareRangePredicate(TreeStructureCustomBankPropertySelectedPartFields.ResourceId, null, resourceIds));

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, 0);
            }
            return returnValue;
        }

        public EntityCollection GetTreeStructurePartCustomBankProperties(List<Guid> ids, bool withChildTreeStructure)
        {
            var returnValue = new EntityCollection(new TreeStructurePartCustomBankPropertyEntityFactory());
            IRelationPredicateBucket filter = new RelationPredicateBucket();
            var _with9 = filter;
            foreach (var id in ids)
            {
                _with9.PredicateExpression.AddWithOr(TreeStructurePartCustomBankPropertyFields.TreeStructurePartCustomBankPropertyId == id);
            }

            IPrefetchPath2 customPropertyPath = new PrefetchPath2(Convert.ToInt32(EntityType.TreeStructurePartCustomBankPropertyEntity));
            if (withChildTreeStructure)
            {
                var treeStructureParts = customPropertyPath.Add(TreeStructurePartCustomBankPropertyEntity.PrefetchPathChildTreeStructurePartCustomBankPropertyCollection);
            }

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, customPropertyPath);
            }
            return returnValue;
        }

        public EntityCollection GetCustomBankPropertyValuesByCustomPropertyIdAndResourceId(List<Guid> customPropertyIds,
            List<Guid> resourceIds, bool onlyWithEmptyDisplayValue)
        {
            var returnValue = new EntityCollection(new CustomBankPropertyValueEntityFactory());
            IPrefetchPath2 prefetchPath = new PrefetchPath2(Convert.ToInt32(EntityType.CustomBankPropertyValueEntity));
            prefetchPath.Add(ListCustomBankPropertyValueEntity.PrefetchPathListCustomBankPropertySelectedValueCollection);
            prefetchPath.Add(TreeStructureCustomBankPropertyValueEntity.PrefetchPathTreeStructureCustomBankPropertySelectedPartCollection);
            prefetchPath.Add(ConceptStructureCustomBankPropertyValueEntity.PrefetchPathConceptStructureCustomBankPropertySelectedPartCollection);

            IRelationPredicateBucket filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(new PredicateExpression(CustomBankPropertyValueFields.CustomBankPropertyId == customPropertyIds.ToArray()));
            filter.PredicateExpression.AddWithAnd(new PredicateExpression(CustomBankPropertyValueFields.ResourceId == resourceIds.ToArray()));
            if (onlyWithEmptyDisplayValue)
            {
                filter.PredicateExpression.AddWithAnd(new FieldCompareNullPredicate(CustomBankPropertyValueFields.DisplayValue, null));
            }

            using (var adapter = new DataAccessAdapter())
            {
                adapter.FetchEntityCollection(returnValue, filter, prefetchPath);
            }

            return returnValue;
        }

        public string ClearAndDeleteBankHierarchical(int bankId)
        {
            string result = null;

            if (bankId <= 0)
            {
                throw new ArgumentNullException(nameof(bankId));
            }

            using (var adapter = new DataAccessAdapter())
            {
                try
                {
                    _permissionService.UserIsPermittedTo(TestBuilderPermissionAccess.DALDelete, TestBuilderPermissionTarget.BankEntity, bankId);

                    if (_securityService.IsBankAssignedToUserThroughBankRole(bankId) &&
!_permissionService.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Delete, TestBuilderPermissionTarget.BankEntity, TestBuilderPermissionNamedTask.None, 0, 0))
                    {
                        result = string.Format(Resources.BankAssignedThroughAUserRole, GetBank(bankId).Name);
                    }
                    else
                    {
                        ActionProcedures.ClearAndDeleteBankHierarchical(bankId, adapter);
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }

            return result;
        }

    }
}