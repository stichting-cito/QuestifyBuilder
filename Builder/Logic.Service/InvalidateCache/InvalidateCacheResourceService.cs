using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.InvalidateCache.Helper;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using IResourceService = Questify.Builder.Logic.Service.Interfaces.IResourceService;

namespace Questify.Builder.Logic.Service.InvalidateCache
{
    public class InvalidateCacheResourceService : Decorators.BaseResourceServiceDecorator
    {

        private static List<int> _banksToInvalidate = new List<int>();
        private static List<Guid> _resourceToInvalidate = new List<Guid>();
        private static List<Guid> _customPropertyToInvalidate = new List<Guid>();
        private static readonly ReaderWriterLockSlim CacheLock = new ReaderWriterLockSlim();
        private static int _batchNotifierCount;



        public InvalidateCacheResourceService(IResourceService decoree)
            : base(decoree)
        {
        }


        public override string UpdateAspectResource(AspectResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateAspectResource(resource);
            if (_batchNotifierCount == 0) { DtoFactory.Aspect.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateAspectResource(AspectResourceEntity resource, bool refetch, bool recurse)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateAspectResource(resource, refetch, recurse);
            if (_batchNotifierCount == 0) { DtoFactory.Aspect.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var isTemplate = resource.IsTemplate;
            var result = base.UpdateAssessmentTestResource(resource);
            if (_batchNotifierCount == 0)
            {
                if (isTemplate)
                {
                    DtoFactory.TestTemplate.EntityChanged(resourceId);
                }
                else { DtoFactory.Test.EntityChanged(resourceId); }
            }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateAssessmentTestResource(AssessmentTestResourceEntity resource, bool refetch, bool recurse)
        {
            var resourceId = resource.ResourceId;
            var isTemplate = resource.IsTemplate;
            var result = base.UpdateAssessmentTestResource(resource, refetch, recurse);
            if (_batchNotifierCount == 0)
            {
                if (isTemplate) { DtoFactory.TestTemplate.EntityChanged(resourceId); }
                else { DtoFactory.Test.EntityChanged(resourceId); }
            }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override List<KeyValuePair<Guid, string>> UpdateBankIdOfResourceEntitiesAndCustomBankProperties(int bankIdValue, List<Guid> resourceIdsToUpdate,
            List<Guid> customBankPropertyIdsToUpdate)
        {
            var result = base.UpdateBankIdOfResourceEntitiesAndCustomBankProperties(bankIdValue, resourceIdsToUpdate, customBankPropertyIdsToUpdate);
            if (_batchNotifierCount == 0)
            {
                InvalidateCacheHelper.ClearCacheForResourcesOfAnyType(resourceIdsToUpdate);
                InvalidateCacheHelper.ClearCustomProperties(customBankPropertyIdsToUpdate);
                InvalidateCacheHelper.ClearCacheForBank(bankIdValue);
            }
            else
            {
                RegisterBatchActions(resourceIdsToUpdate, customBankPropertyIdsToUpdate);
                RegisterBatchActions(bankIdValue);
            }

            return result;
        }

        public override string UpdateControlTemplateResource(ControlTemplateResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateControlTemplateResource(resource);
            if (_batchNotifierCount == 0) { DtoFactory.ControlTemplate.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateControlTemplateResource(ControlTemplateResourceEntity resource, bool refetch, bool recurse)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateControlTemplateResource(resource, refetch, recurse);
            if (_batchNotifierCount == 0) { DtoFactory.ControlTemplate.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateDataSourceResource(DataSourceResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var isTemplate = resource.IsTemplate;
            var result = base.UpdateDataSourceResource(resource);
            if (_batchNotifierCount == 0)
            {
                if (isTemplate)
                {
                    DtoFactory.DatasourceTemplate.EntityChanged(resourceId);
                }
                else
                {
                    var resourcesToInvalidateCacheFor = resource.DependentResourceCollection.Select(x => x.DependentResourceId).ToList();
                    foreach (var removedDependentEntity in resource.RemovedDependentEntities)
                    {
                        DependentResourceEntity dre = removedDependentEntity as DependentResourceEntity;
                        if (dre != null)
                        {
                            resourcesToInvalidateCacheFor.Add(dre.DependentResource.ResourceId);
                        }
                    }
                    DtoFactory.Datasource.EntityChanged(resourceId);
                    DtoFactory.Item.EntitiesChanged(resourcesToInvalidateCacheFor);
                }
            }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateDataSourceResource(DataSourceResourceEntity resource, bool refetch, bool recurse)
        {
            var resourceId = resource.ResourceId;
            var isTemplate = resource.IsTemplate;

            var result = base.UpdateDataSourceResource(resource, refetch, recurse);
            if (_batchNotifierCount == 0)
            {
                if (isTemplate)
                {
                    DtoFactory.DatasourceTemplate.EntityChanged(resourceId);
                }
                else
                {
                    DtoFactory.Datasource.EntityChanged(resourceId);
                }
            }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateGenericResource(GenericResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateGenericResource(resource);
            if (_batchNotifierCount == 0) { DtoFactory.Generic.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateGenericResource(GenericResourceEntity resource, bool refetch, bool recurse)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateGenericResource(resource, refetch, recurse);
            if (_batchNotifierCount == 0) { DtoFactory.Generic.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateItemLayoutTemplateResource(resource);
            if (_batchNotifierCount == 0) { DtoFactory.ItemLayoutTemplate.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource, bool refetch, bool recurse)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateItemLayoutTemplateResource(resource, refetch, recurse);
            if (_batchNotifierCount == 0) { DtoFactory.ItemLayoutTemplate.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }


        public override string UpdateItemResource(ItemResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateItemResource(resource);
            if (_batchNotifierCount == 0) { DtoFactory.Item.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateItemResource(ItemResourceEntity resource, bool refetch, bool recurse)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateItemResource(resource, refetch, recurse);
            if (_batchNotifierCount == 0) { DtoFactory.Item.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdatePackageResource(PackageResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdatePackageResource(resource);
            if (_batchNotifierCount == 0) { DtoFactory.TestPackage.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateTestPackageResource(TestPackageResourceEntity resource)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateTestPackageResource(resource);
            if (_batchNotifierCount == 0) { DtoFactory.TestPackage.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateTestPackageResource(TestPackageResourceEntity resource, bool refetch, bool recurse)
        {
            var resourceId = resource.ResourceId;
            var result = base.UpdateTestPackageResource(resource, refetch, recurse);
            if (_batchNotifierCount == 0) { DtoFactory.TestPackage.EntityChanged(resourceId); }
            else { RegisterBatchActions(resourceId); }
            return result;
        }

        public override string UpdateResourceVisibility(Guid resourceId, int setAtBankId, bool makeResourceVisible)
        {
            var result = base.UpdateResourceVisibility(resourceId, setAtBankId, makeResourceVisible);

            if (_batchNotifierCount == 0)
            {
                InvalidateCacheHelper.ClearCacheForResourcesOfAnyType(new Guid[] { resourceId });
            }
            else { RegisterBatchActions(resourceId); }

            return result;
        }

        public override string DeleteResources(EntityCollection resourcesToDelete, ref EntityCollection notSuccessfullDeletedResources)
        {
            var resourceIds = resourcesToDelete.OfType<ResourceEntity>().Select(r => new Tuple<Guid, Type>(r.ResourceId, r.GetType())).ToList();

            resourcesToDelete.OfType<ResourceEntity>().ToList().ForEach(rtd => rtd.DependentResourceCollection.ToList().ForEach(dr =>
{
    if (!resourceIds.Any(r => r.Item1.Equals(dr.DependentResourceId)))
    {
        if (dr.Resource == null)
        {
            var request = new ResourceRequestDTO();
            resourceIds.Add(new Tuple<Guid, Type>(dr.ResourceId, base.GetResourceByIdWithOption(dr.ResourceId, request).GetType()));
        }
        else
        {
            resourceIds.Add(new Tuple<Guid, Type>(dr.DependentResourceId, dr.DependentResource.GetType()));
        }
    }
}));

            resourcesToDelete.OfType<ResourceEntity>().ToList().ForEach(rtd => rtd.ReferencedResourceCollection.ToList().ForEach(rr =>
{
    if (!resourceIds.Any(r => r.Item1.Equals(rr.ResourceId)))
    {
        if (rr.Resource == null)
        {
            var request = new ResourceRequestDTO();
            resourceIds.Add(new Tuple<Guid, Type>(rr.ResourceId, base.GetResourceByIdWithOption(rr.ResourceId, request).GetType()));
        }
        else
        {
            resourceIds.Add(new Tuple<Guid, Type>(rr.ResourceId, rr.Resource.GetType()));
        }
    }
}));

            var result = base.DeleteResources(resourcesToDelete, ref notSuccessfullDeletedResources);
            if (_batchNotifierCount == 0)
            {
                var notSuccessfullDeletedResourcesCount = (notSuccessfullDeletedResources == null ? 0 : notSuccessfullDeletedResources.Count);
                if (resourcesToDelete.Count > notSuccessfullDeletedResourcesCount)
                {
                    InvalidateCacheHelper.ClearCacheForResourcesOfAnyType(resourceIds);
                }
            }
            else { RegisterBatchActions(resourceIds.Select(r => r.Item1)); }
            return result;
        }



        public static void StartUsingBatchNotifier()
        {
            Interlocked.Increment(ref _batchNotifierCount);
        }

        public static void StopUsingBatchNotifier()
        {
            Interlocked.Decrement(ref _batchNotifierCount);
        }

        public static void ExecuteBatchInvalidation()
        {
            CacheLock.EnterWriteLock();
            try
            {
                InvalidateCacheHelper.ClearCacheForResourcesOfAnyType(_resourceToInvalidate);

                InvalidateCacheHelper.ClearCustomProperties(_customPropertyToInvalidate);
                foreach (var bankId in _banksToInvalidate)
                {
                    InvalidateCacheHelper.ClearCacheForBank(bankId);
                }
                _resourceToInvalidate = new List<Guid>();
                _customPropertyToInvalidate = new List<Guid>();
                _banksToInvalidate = new List<int>();
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }



        private void RegisterBatchActions(int bankId)
        {
            if (_batchNotifierCount > 0 && (!_banksToInvalidate.Contains(bankId)))
            {
                _banksToInvalidate.Add(bankId);
            }
        }

        private void RegisterBatchActions(Guid resourceId)
        {
            RegisterBatchActions(new List<Guid> { resourceId });
        }

        private void RegisterBatchActions(IEnumerable<Guid> resourceIds)
        {
            RegisterBatchActions(resourceIds, null);
        }

        private void RegisterBatchActions(IEnumerable<Guid> resourceIds, IEnumerable<Guid> customPropertyIds)
        {
            if (resourceIds != null)
            {
                foreach (var resourceId in resourceIds)
                {
                    if (_batchNotifierCount > 0 && (!_resourceToInvalidate.Contains(resourceId)))
                    {
                        _resourceToInvalidate.Add(resourceId);
                    }
                }
            }
            if (customPropertyIds != null)
            {
                foreach (var customPropertyId in customPropertyIds)
                {
                    if (_batchNotifierCount > 0 && (!_customPropertyToInvalidate.Contains(customPropertyId)))
                    {
                        _customPropertyToInvalidate.Add(customPropertyId);
                    }
                }
            }
        }

    }
}
