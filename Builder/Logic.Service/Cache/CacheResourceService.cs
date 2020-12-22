using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using CustomClasses;
using NLog;
using Questify.Builder.Logic.Service.Cache.CacheEntities;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using IResourceService = Questify.Builder.Logic.Service.Interfaces.IResourceService;

namespace Questify.Builder.Logic.Service.Cache
{
    [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", MessageId = "args")]
    public class CacheResourceService : Decorators.BaseResourceServiceDecorator, IDisposable
    {
        protected readonly EntityCache<ResourceEntityWithOptions> ResourceCache;
        protected readonly EntityCache<ResourceDataEntity> ResourceDataCache;
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private bool _disposed;
        private readonly int _sizeInKb;


        public CacheResourceService(IResourceService decoree)
            : this(decoree, 10, true, 10, 50, true, true)
        {
        }

        public CacheResourceService(
            IResourceService decoree,
            int expInSecResource,
            bool slidingResource,
            int expInSecResourceData,
            int sizeInKbResourceData,
            bool slidingResourceData,
            bool fromConfig)
            : base(decoree)
        {

            int expRes = expInSecResource;
            bool slidingRes = slidingResource;
            int expData = expInSecResourceData;
            bool slidingData = slidingResourceData;
            int sizeData = sizeInKbResourceData;

            if (fromConfig)
            {
                var cacheSettingsResource = ConfigPluginHelper.GetCacheSettingsByType("ResourceEntity");
                var cacheSettingsResourceData = ConfigPluginHelper.GetCacheSettingsByType("ResourceDataEntity");
                if (cacheSettingsResource != null)
                {
                    expRes = cacheSettingsResource.TimeInSeconds;
                    slidingRes = cacheSettingsResource.Sliding;
                }
                if (cacheSettingsResourceData != null)
                {
                    expData = cacheSettingsResourceData.TimeInSeconds;
                    slidingData = cacheSettingsResourceData.Sliding;
                    sizeData = cacheSettingsResourceData.MaxSizeInKb;
                }
            }
            _sizeInKb = sizeData;
            ResourceCache = new EntityCache<ResourceEntityWithOptions>(expRes, slidingRes);
            ResourceDataCache = new EntityCache<ResourceDataEntity>(expData, slidingData);
        }




        public override EntityCollection GetItemLayoutTemplatesForBank(int bankId)
        {
            var requestedOptions = CachingWithOptionsConverter.WithOptionsToArray(false, false, false, false, false, true);
            var resourcesFromDatabase = base.GetItemLayoutTemplatesForBank(bankId);

            foreach (var resource in resourcesFromDatabase.OfType<ItemLayoutTemplateResourceEntity>())
            {
                var isEmpty = false;

                var cachedItem = GetFromCache(resource.ResourceId, ref isEmpty, true, requestedOptions);
                if (!isEmpty && cachedItem == null)
                {
                    AddResourceToCache(new ResourceEntityWithOptions(resource, requestedOptions));
                }
            }

            return resourcesFromDatabase;
        }

        public override ItemLayoutTemplateResourceEntity GetItemLayoutTemplate(ItemLayoutTemplateResourceEntity itemLayoutTemplate)
        {
            var requestedOptions = CachingWithOptionsConverter.WithOptionsToArray(true, false, false, false, false, true);
            var isEmpty = false;

            var cachedItem = GetFromCache(itemLayoutTemplate.Name, itemLayoutTemplate.BankId, ref isEmpty, true, requestedOptions);
            if (cachedItem != null && cachedItem.resource.GetType() == typeof(ItemLayoutTemplateResourceEntity))
            {
                return (ItemLayoutTemplateResourceEntity)cachedItem.resource;
            }

            if (isEmpty)
            {
                return null;
            }

            var ilt = base.GetItemLayoutTemplate(itemLayoutTemplate);
            if (ilt != null)
            {
                AddResourceToCache(new ResourceEntityWithOptions(ilt, requestedOptions));
            }
            return ilt;
        }

        public override AspectResourceEntity GetAspect(AspectResourceEntity aspect)
        {
            var requestedOptions = CachingWithOptionsConverter.WithOptionsToArray(true, false);
            var isEmpty = false;

            var cachedItem = GetFromCache(aspect.Name, aspect.BankId, ref isEmpty, true, requestedOptions);
            if (cachedItem != null && cachedItem.resource.GetType() == typeof(AspectResourceEntity))
            {
                return (AspectResourceEntity)cachedItem.resource;
            }

            if (isEmpty)
            {
                return null;
            }

            var aspct = base.GetAspect(aspect);
            if (aspct != null)
            {
                AddResourceToCache(new ResourceEntityWithOptions(aspct, requestedOptions));
            }
            return aspct;
        }

        public override ResourceDataEntity GetResourceData(ResourceEntity resource)
        {
            if (resource == null)
            {
                return null;
            }
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - GetResourceData: resourceId: {0}", resource.ResourceId));
            }
            var cachedItem = ResourceDataCache.Get(resource.Name, resource.BankId);
            if (cachedItem != null)
            {
                return cachedItem;
            }
            if (ResourceDataCache.IsEmpty(resource.Name, resource.BankId))
            {
                return null;
            }
            var resourceData = base.GetResourceData(resource);
            if (ShouldCache(resource, _sizeInKb))
            {
                AddResourceToCache(new ResourceEntityWithOptions(resource, new List<short>()));
                ResourceDataCache.Put(resource.Name, resource.BankId, resourceData);
            }
            return resourceData;
        }

        public override EntityCollection GetItemLayoutTemplatesFromListOfResourceIds(IEnumerable<Guid> resourceIds, bool withDependencies)
        {
            var resourceIdsList = resourceIds?.ToList();
            if (resourceIdsList == null || !resourceIdsList.Any())
            {
                return null;
            }
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - GetItemLayoutTemplatesFromListOfResourceIds: guid: {0}, withDependencies: {1}", string.Join(";", resourceIdsList), withDependencies));
            }
            EntityCollection collection;

            var resources = new List<ResourceEntity>();
            var idsToRemove = new List<Guid>();
            var requestedOptions = CachingWithOptionsConverter.WithOptionsToArray(withDependencies, false);

            foreach (var resourceId in resourceIdsList)
            {
                var isEmpty = false;

                var cachedItem = GetFromCache(resourceId, ref isEmpty, true, requestedOptions);

                if (cachedItem != null)
                {
                    resources.Add(cachedItem.resource);
                    idsToRemove.Add(resourceId);
                }
                else if (isEmpty)
                {
                    idsToRemove.Add(resourceId);
                }
            }
            var resourceIdsToRetrieveFromDatabase = resourceIdsList.Except(idsToRemove).ToList();
            if (resourceIdsToRetrieveFromDatabase.Any())
            {
                var resourceFromDatabase = base.GetItemLayoutTemplatesFromListOfResourceIds(resourceIdsToRetrieveFromDatabase, withDependencies).OfType<ResourceEntity>().ToList();
                resourceFromDatabase.Where(r => ShouldCache(r, null)).ToList().ForEach(r => AddResourceToCache(new ResourceEntityWithOptions(r, requestedOptions)));

                resources.AddRange(resourceFromDatabase);
            }

            try
            {
                collection = new EntityCollection();
                collection.AddRange(resources);
            }
            finally
            {
                resources.Clear();
                idsToRemove.Clear();
            }

            return collection;
        }

        public override ResourceEntity GetResourceByNameWithOption(int bankId, string name, ResourceRequestDTO request)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - GetResourceByNameWithOption: bankId: {0}, name: {1}, withDependencies: {2}, withCustomProperties: {3}", bankId, name, request.WithDependencies, request.WithCustomProperties));
            }

            var requestedOptions = CachingWithOptionsConverter.WithOptionsToArray(request.WithDependencies, request.WithCustomProperties);
            var isEmpty = false;

            var cachedItem = GetFromCache(name, bankId, ref isEmpty, true, requestedOptions);
            if (cachedItem != null)
            {
                return cachedItem.resource;
            }

            if (isEmpty)
            {
                return null;
            }

            var resource = base.GetResourceByNameWithOption(bankId, name, request);
            if (resource != null && ShouldCache(resource, null))
            {
                AddResourceToCache(new ResourceEntityWithOptions(resource, requestedOptions));
            }
            return resource;
        }

        public override EntityCollection GetResourcesByNamesWithOption(int bankId, List<string> names, ResourceRequestDTO request)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - GetResourcesByNamesWithOption: bankId: {0}, names: {1}, withDependencies: {2}, withCustomProperties: {3}", bankId, string.Join(";", names), request.WithDependencies, request.WithCustomProperties));
            }
            var resources = new List<ResourceEntity>();
            var namesToRemove = new List<string>();
            var requestedOptions = CachingWithOptionsConverter.WithOptionsToArray(request.WithDependencies, request.WithCustomProperties);

            foreach (var name in names)
            {
                var isEmpty = false;

                var cachedItem = GetFromCache(name, bankId, ref isEmpty, true, requestedOptions);
                if (cachedItem != null)
                {
                    resources.Add(cachedItem.resource);
                    namesToRemove.Add(name);
                }
                else if (isEmpty)
                {
                    namesToRemove.Add(name);
                }
            }
            var resourceNamesToRetrieveFromDatabase = names.Except(namesToRemove).ToList();
            if (resourceNamesToRetrieveFromDatabase.Any())
            {
                var resourceFromDatabase = base.GetResourcesByNamesWithOption(bankId, resourceNamesToRetrieveFromDatabase, request).OfType<ResourceEntity>().ToList();
                resourceFromDatabase.Where(r => ShouldCache(r, null)).ToList().ForEach(r => AddResourceToCache(new ResourceEntityWithOptions(r, requestedOptions)));
                resources.AddRange(resourceFromDatabase);
            }
            var collection = new EntityCollection();
            collection.AddRange(resources);
            resources.Clear();
            return collection;
        }

        public override ResourceEntity GetResourceByIdWithOption(Guid resourceId, ResourceRequestDTO request)
        {
            return GetResourceByIdWithOption(resourceId, new ResourceEntityFactory(), request);
        }

        public override ResourceEntity GetResourceByIdWithOption(Guid resourceId, IEntityFactory2 factory, ResourceRequestDTO request)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - GetResourceByIdWithOption: resourceId: {0}, withDependencies: {1}, withReferences: {2}, withCustomProperties: {3}, withUserInfo: {4}, withState: {5}, withHiddenResources: {6}", resourceId,
                    request.WithDependencies, request.WithReferences, request.WithCustomProperties, request.WithUserInfo, request.WithState, request.WithHiddenResources));
            }

            var requestedOptions = CachingWithOptionsConverter.WithOptionsToArray(
                request.WithDependencies,
                request.WithReferences,
                request.WithCustomProperties,
                request.WithUserInfo,
                request.WithState,
                request.WithHiddenResources);

            var isEmpty = false;

            var cachedItem = GetFromCache(resourceId, ref isEmpty, true, requestedOptions);
            if (cachedItem != null)
            {
                return cachedItem.resource;
            }

            if (isEmpty)
            {
                return null;
            }

            var resource = base.GetResourceByIdWithOption(resourceId, factory, request);
            if (resource != null && ShouldCache(resource, null))
            {
                AddResourceToCache(new ResourceEntityWithOptions(resource, requestedOptions));
            }
            return resource;
        }

        public override EntityCollection GetResourcesByIdsWithOption(List<Guid> resourceIds, ResourceRequestDTO request)
        {
            return GetResourcesByIdsWithOption(resourceIds, new ResourceEntityFactory(), request);
        }

        public override EntityCollection GetResourcesByIdsWithOption(List<Guid> resourceIds, IEntityFactory2 factory, ResourceRequestDTO request)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - GetResourcesByIdsWithOption: guid: {0}, withDependencies: {1}, withReferences: {2}, withCustomProperties: {3}, withUserInfo: {4}, withState: {5}, withHiddenResources: {6}", string.Join(";", resourceIds),
                    request.WithDependencies, request.WithReferences, request.WithCustomProperties, request.WithUserInfo, request.WithState, request.WithHiddenResources));
            }
            var resources = new List<ResourceEntity>();
            var idsToRemove = new List<Guid>();
            var requestedOptions = CachingWithOptionsConverter.WithOptionsToArray(
                request.WithDependencies,
                request.WithReferences,
                request.WithCustomProperties,
                request.WithUserInfo,
                request.WithState,
                request.WithHiddenResources);

            foreach (var resourceId in resourceIds)
            {
                var isEmpty = false;

                var cachedItem = GetFromCache(resourceId, ref isEmpty, true, requestedOptions);
                if (cachedItem != null)
                {
                    resources.Add(cachedItem.resource);
                    idsToRemove.Add(resourceId);
                }
                else if (isEmpty)
                {
                    idsToRemove.Add(resourceId);
                }
            }
            var resourceIdsToRetrieveFromDatabase = resourceIds.Except(idsToRemove).ToList();
            if (resourceIdsToRetrieveFromDatabase.Any())
            {
                var resourceFromDatabase = base.GetResourcesByIdsWithOption(resourceIdsToRetrieveFromDatabase, factory, request).OfType<ResourceEntity>().ToList();
                resourceFromDatabase.Where(r => ShouldCache(r, null)).ToList().ForEach(r => AddResourceToCache(new ResourceEntityWithOptions(r, requestedOptions)));
                resources.AddRange(resourceFromDatabase);
            }
            var collection = new EntityCollection();
            collection.AddRange(resources);
            resources.Clear();
            return collection;
        }



        public override string UpdateAspectResource(AspectResourceEntity resource)
        {
            var ret = base.UpdateAspectResource(resource);
            RemoveResourceFromCache(resource);
            return ret;
        }

        public override string UpdateAspectResource(AspectResourceEntity resource, bool refetch, bool recurse)
        {
            var ret = base.UpdateAspectResource(resource, refetch, recurse);
            RemoveResourceFromCache(resource);
            return ret;
        }

        public override string UpdateControlTemplateResource(ControlTemplateResourceEntity resource)
        {
            var ret = base.UpdateControlTemplateResource(resource);
            RemoveResourceFromCache(resource);
            return ret;
        }

        public override string UpdateGenericResource(GenericResourceEntity resource)
        {
            var ret = base.UpdateGenericResource(resource);
            RemoveResourceFromCache(resource);
            return ret;
        }

        public override string UpdateGenericResource(GenericResourceEntity resource, bool refetch, bool recurse)
        {
            var ret = base.UpdateGenericResource(resource, refetch, recurse);
            RemoveResourceFromCache(resource);
            return ret;
        }

        public override string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource)
        {
            var ret = base.UpdateItemLayoutTemplateResource(resource);
            RemoveResourceFromCache(resource);
            return ret;
        }

        public override string UpdateItemLayoutTemplateResource(ItemLayoutTemplateResourceEntity resource, bool refetch, bool recurse)
        {
            var ret = base.UpdateItemLayoutTemplateResource(resource, refetch, recurse);
            RemoveResourceFromCache(resource);
            return ret;
        }

        public override string UpdateControlTemplateResource(ControlTemplateResourceEntity resource, bool refetch, bool recurse)
        {
            var ret = base.UpdateControlTemplateResource(resource, refetch, recurse);
            RemoveResourceFromCache(resource);
            return ret;
        }

        public override string UpdateResourceVisibility(Guid resourceId, int setAtBankId, bool makeResourceVisible)
        {
            var ret = base.UpdateResourceVisibility(resourceId, setAtBankId, makeResourceVisible);
            var request = new ResourceRequestDTO();
            var resource = base.GetResourceByIdWithOption(resourceId, request);
            if (resource != null)
            {
                RemoveResourceFromCache(resource);
            }
            return ret;
        }




        private void AddResourceToCache(ResourceEntityWithOptions resourceWithOptions)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - AddResourceToCache: resourceId: {0}, name: {1}", resourceWithOptions.resource.ResourceId, resourceWithOptions.resource.Name));
            }
            ResourceCache.Put(resourceWithOptions.resource.ResourceId, resourceWithOptions);
            ResourceCache.Put(resourceWithOptions.resource.Name, resourceWithOptions.resource.BankId, resourceWithOptions);
        }

        private ResourceEntityWithOptions GetFromCache(string name, int bankId, ref bool isEmpty, bool checkRequestedOptions, List<short> requestedOptions)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - GetFromCache: name: {0}, bankId: {1}", name, bankId));
            }
            var cachedItem = ResourceCache.Get(name, bankId);
            if (cachedItem != null)
            {
                if (checkRequestedOptions && requestedOptions.Except(cachedItem.hasOptions).Any())
                {
                    RemoveResourceFromCache(cachedItem.resource);
                    return null;
                }
                return cachedItem;
            }
            isEmpty = ResourceCache.IsEmpty(name, bankId);
            return null;
        }

        private ResourceEntityWithOptions GetFromCache(Guid resourceId, ref bool isEmpty, bool checkRequestedOptions, List<short> requestedOptions)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Log(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "CacheResourceService - GetFromCache: resourceId: {0}", resourceId));
            }
            var cachedItem = ResourceCache.Get(resourceId);
            if (cachedItem != null)
            {
                if (checkRequestedOptions && requestedOptions.Except(cachedItem.hasOptions).Any())
                {
                    RemoveResourceFromCache(cachedItem.resource);
                    return null;
                }
                return cachedItem;
            }
            isEmpty = ResourceCache.IsEmpty(resourceId);
            return null;
        }

        private void RemoveResourceFromCache(ResourceEntity resource)
        {
            if (resource != null)
            {
                var name = resource.Name;
                var bankId = resource.BankId;
                ResourceCache.Remove(resource.ResourceId);
                ResourceCache.Remove(name, bankId);
                ResourceDataCache.Remove(resource.ResourceId);
                ResourceDataCache.Remove(name, bankId);
            }
        }

        private static bool ShouldCache(ResourceEntity resource, int? maxSize)
        {
            var shouldCache = false;
            if (resource != null)
            {
                switch (resource.GetType().ToString())
                {
                    case "Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity":
                    case "Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity":
                    case "Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity":
                        shouldCache = true;
                        break;
                    case "Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity":
                        if (!maxSize.HasValue ||
                            ((GenericResourceEntity)resource).Size < maxSize.Value
                        )
                        {
                            shouldCache = true;
                        }
                        break;
                }
            }
            else
            {
                shouldCache = true;
            }
            return shouldCache;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                ResourceCache?.Dispose();
                ResourceDataCache?.Dispose();
            }

            _disposed = true;
        }

        ~CacheResourceService()
        {
            Dispose(false);
        }


    }
}
