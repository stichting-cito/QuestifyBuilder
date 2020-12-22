using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Questify.Builder.Logic.Service.Cache.CacheEntities;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using ResourceDto = Questify.Builder.Logic.Service.Model.Entities.ResourceDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheResourceDtoService<TResource> : BaseResourceDtoServiceDecorator<TResource> where TResource : ResourceDto
    {


        private readonly int _secondsInCache = 28800; private readonly bool _sliding;
        private readonly ResourceDtoCache<TResource> _resourceDtoCache;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();



        public CacheResourceDtoService(IResourceDtoRepository<TResource> decoree)
            : base(decoree)
        {
            var cacheSettingsDto = ConfigPluginHelper.GetCacheSettingsByType("DtoResourceEntity");
            if (cacheSettingsDto != null)
            {
                _secondsInCache = cacheSettingsDto.TimeInSeconds;
                _sliding = cacheSettingsDto.Sliding;
            }
            _resourceDtoCache = new ResourceDtoCache<TResource>(_secondsInCache, _sliding);
        }




        public override IEnumerable<TResource> GetResourcesForBank(int id)
        {
            var referencedIds = _resourceDtoCache.GetParentBanks(id); if (referencedIds != null)
            {
                IEnumerable<TResource> cachedList = new List<TResource>();
                foreach (var referenceBank in referencedIds)
                {
                    var list = _resourceDtoCache.GetList(referenceBank);
                    if (list == null)
                    {
                        list = base.GetResourcesForBank(referenceBank);
                        StoreList(referenceBank, list);
                    }

                    if (list == null)
                    {
                        continue;
                    }

                    int bank = referenceBank;
                    cachedList = cachedList.Union(list.Where(r => r.BankId == bank));
                }
                return cachedList.ToList();
            }
            var retrievedList = base.GetResourcesForBank(id).ToList();
            StoreList(id, retrievedList);
            return retrievedList;
        }


        public override TResource Get(Guid id)
        {
            var cachedEntity = _resourceDtoCache.Get(id);
            if (cachedEntity != null)
            {
                return cachedEntity;
            }
            {
                var entity = base.Get(id);
                PutSingleResource(entity, true);
                return entity;
            }
        }



        public override void BankChanged(int bankId)
        {
            IEnumerable<int> banksToInvalidate = new List<int> { bankId };
            var cachedChild = _resourceDtoCache.GetChildBanks(bankId);
            if (cachedChild != null)
            {
                banksToInvalidate = banksToInvalidate.Union(cachedChild);
            }
            foreach (var bankToInvalidate in banksToInvalidate)
            {
                var cachedResourceIds = _resourceDtoCache.GetResourcesByBank(bankToInvalidate);
                var newReferences = new List<Guid>();
                if (cachedResourceIds == null)
                {
                    continue;
                }
                foreach (var resourceId in cachedResourceIds)
                {
                    var resource = _resourceDtoCache.Get(resourceId);
                    if (resource != null && resource.CustomPropertyDisplayValues != null &&
    resource.CustomPropertyDisplayValues.Select(c => c.BankId == bankId).Any())
                    {
                        _resourceDtoCache.Remove(resourceId);
                    }
                    else
                    {
                        newReferences.Add(resourceId);
                    }
                }
                _resourceDtoCache.RemoveResourcesByBank(bankToInvalidate);
                _resourceDtoCache.PutResourcesByBank(bankToInvalidate, newReferences);
            }
            _resourceDtoCache.RemoveChildBanks(bankId);
            _resourceDtoCache.RemoveList(bankId);
            _resourceDtoCache.RemoveParentBanks(bankId);
            _resourceDtoCache.RemoveResourcesByBank(bankId);
        }

        public override void EntityChanged(Guid key)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Log(LogLevel.Info, string.Format("CacheResourceDtoService - EntityChanged: key: {0}", key));
            }
            EntitiesChanged(new List<Guid> { key });
        }

        public override void EntitiesChanged(IEnumerable<Guid> keys)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Log(LogLevel.Info, string.Format("CacheResourceDtoService - EntitiesChanged: keys: {0}", string.Join(";", keys)));
            }
            var resourceIds = keys as IList<Guid> ?? keys.ToList();
            var updatedResources = base.GetMulti(resourceIds);
            if (updatedResources == null)
            {
                return;
            }
            var updatedResourcesList = updatedResources.ToList();
            List<int> bankIds = new List<int>();

            if (keys.Count() > 1)
            {
                bankIds = UniqueBankIdsInEntitiesChanged(resourceIds, updatedResourcesList);
            }

            if ((resourceIds.Count() > 1) && (!updatedResourcesList.Any()))
            {
                BulkUpdateForDeletedResources(bankIds, resourceIds);
            }
            else if ((resourceIds.Count() > 1) && bankIds.Count() == 1)
            {
                UpdateListInCache(bankIds, resourceIds, updatedResourcesList);
            }
            else
            {
                UpdateCachePerResource(resourceIds, updatedResourcesList);
            }
        }

        private void BulkUpdateForDeletedResources(List<int> bankIds, IList<Guid> resourceIds)
        {
            foreach (int? bankId in bankIds)
            {
                if (bankId.HasValue)
                {
                    UpdateListCache_DeleteAction(bankId.Value, resourceIds);
                }
            }
            foreach (var resourceId in resourceIds)
            {
                _resourceDtoCache.Remove(resourceId);
            }
        }

        private void UpdateListInCache(List<int> bankIds, IList<Guid> resourceIds, List<TResource> updatedResourcesList)
        {
            int bankId = bankIds[0];
            var bankList = _resourceDtoCache.GetList(bankId);
            if (bankList == null)
            {
                return;
            }
            var newList = bankList.ToList();

            foreach (var resourceId in resourceIds)
            {
                newList = newList.Where(r => r.ResourceId != resourceId).ToList();
                var updatedResource = updatedResourcesList.FirstOrDefault(r => r.ResourceId == resourceId);
                if (updatedResource == null) continue;
                newList.Add(updatedResource);
                PutSingleResource(updatedResource, false);
            }

            _resourceDtoCache.RemoveList(bankId);
            _resourceDtoCache.PutList(bankId, newList);
            StoreResourceIdsByBank(bankId, newList.Select(r => r.ResourceId));
            StoreChildBanksByBank(bankId, newList.Where(r => r.CustomPropertyDisplayValues != null && r.CustomPropertyDisplayValues.Count() > 0).SelectMany(r => r.CustomPropertyDisplayValues.Select(c => c.BankId).Distinct()));
        }

        private void UpdateCachePerResource(IList<Guid> resourceIds, List<TResource> updatedResourcesList)
        {
            foreach (var resourceId in resourceIds)
            {
                var updatedResource = updatedResourcesList.FirstOrDefault(r => r.ResourceId == resourceId);
                int? bankId;
                bankId = updatedResource == null ? _resourceDtoCache.GetBankIdForResource(resourceId) : updatedResource.BankId;
                if (bankId.HasValue)
                {
                    UpdateListCache(bankId.Value, resourceId, updatedResource);
                }
                _resourceDtoCache.Remove(resourceId);
                if (updatedResource != null)
                {
                    _resourceDtoCache.Put(resourceId, updatedResource);
                }
            }
        }




        private List<int> UniqueBankIdsInEntitiesChanged(IList<Guid> resourceIds, List<TResource> updatedResourcesList)
        {
            List<int?> result = resourceIds.GroupBy(r => _resourceDtoCache.GetBankIdForResource(r)).Where(y => y.Key.HasValue).Select(y => y.Key).ToList();
            if (updatedResourcesList.Any())
            {
                foreach (int bankId in updatedResourcesList.GroupBy(u => u.BankId).Select(z => z.Key).ToList())
                {
                    if (!result.Contains(bankId))
                    {
                        result.Add(bankId);
                    }
                }
            }
            return result.Where(r => r.HasValue).Select(r => r.Value).ToList();
        }

        private void UpdateListCache(int bankId, Guid resourceId, TResource updatedResource)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Log(LogLevel.Info, string.Format("CacheResourceDtoService - UpdateListCache: bankId: {0}, resourceId: {1}", bankId, resourceId));
            }
            var bankList = _resourceDtoCache.GetList(bankId);
            if (bankList == null)
            {
                return;
            }

            var bankListFull = bankList.ToList();

            var newList = bankListFull.Where(r => r.ResourceId != resourceId).ToList();
            if (updatedResource != null)
            {
                newList.Add(updatedResource);
                PutSingleResource(updatedResource, true);
            }
            _resourceDtoCache.RemoveList(bankId);
            _resourceDtoCache.PutList(bankId, newList);
        }

        private void UpdateListCache_DeleteAction(int bankId, IEnumerable<Guid> resourceIds)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Log(LogLevel.Info, string.Format("CacheResourceDtoService - UpdateListCache_DeleteAction: bankId: {0}, resourceIds: {1}", bankId, string.Join(";", resourceIds)));
            }
            var bankList = _resourceDtoCache.GetList(bankId);
            if (bankList == null)
            {
                return;
            }

            var bankListFull = bankList.ToList();
            var remainingResources = bankListFull.Select(x => x.ResourceId).Except(resourceIds);
            var newList = bankListFull.Where(r => remainingResources.Contains(r.ResourceId)).ToList();
            _resourceDtoCache.RemoveList(bankId);
            _resourceDtoCache.PutList(bankId, newList);
        }

        public void PutSingleResource(TResource resource, bool updateBankCachesAtOnce)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Log(LogLevel.Info, string.Format("CacheResourceDtoService - PutSingleResource: resourceId: {0}", resource.Name));
            }
            var bankId = resource.BankId;
            var resourceId = resource.ResourceId;
            if (updateBankCachesAtOnce)
            {
                IEnumerable<Guid> guidList = new List<Guid> { resourceId };
                StoreResourceIdsByBank(bankId, guidList);

                if (resource.CustomPropertyDisplayValues != null && resource.CustomPropertyDisplayValues.Count() > 0)
                {
                    StoreChildBanksByBank(resource.BankId, resource.CustomPropertyDisplayValues.Select(c => c.BankId).Distinct());
                }
            }

            _resourceDtoCache.Remove(resource.ResourceId);
            _resourceDtoCache.Put(resource.ResourceId, resource);
            _resourceDtoCache.PutBankIdForResource(resource.ResourceId, resource.BankId);
        }

        public void StoreResourceIdsByBank(int bankId, IEnumerable<Guid> guidList)
        {
            List<Guid> guids = guidList.ToList();
            var cacheIds = _resourceDtoCache.GetResourcesByBank(bankId);
            if (cacheIds != null)
            {
                guids = guids.Union(cacheIds).ToList();
                _resourceDtoCache.RemoveResourcesByBank(bankId);
            }
            _resourceDtoCache.PutResourcesByBank(bankId, guids);
        }

        public void StoreChildBanksByBank(int bankId, IEnumerable<int> customPropertiesBankIds)
        {
            foreach (var customPropertyBankId in customPropertiesBankIds)
            {
                IEnumerable<int> bankList = new List<int> { bankId };
                var cacheBankIds = _resourceDtoCache.GetChildBanks(customPropertyBankId);
                if (cacheBankIds != null)
                {
                    bankList = cacheBankIds.Union(bankList);
                    _resourceDtoCache.RemoveChildBanks(customPropertyBankId);
                }
                _resourceDtoCache.PutChildBanks(customPropertyBankId, bankList.ToList());
            }
        }

        private void StoreList(int bankId, IEnumerable<TResource> list)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Log(LogLevel.Info, string.Format("CacheResourceDtoService - StoreList - Start: bankId: {0}", bankId));
            }

            var storeList = list as IList<TResource> ?? list.ToList();

            foreach (var resource in storeList.Where(r => r != null))
            {
                PutSingleResource(resource, false);
            }

            var referencedBankIds = storeList.Where(r => r != null).Select(r => r.BankId).Distinct();
            IEnumerable<int> bankIds = new List<int> { bankId };
            bankIds = bankIds.Union(referencedBankIds as IList<int> ?? referencedBankIds.ToList());
            _resourceDtoCache.PutParentBanks(bankId, bankIds.ToList());
            foreach (var refbankId in bankIds)
            {
                var listPerBank = storeList.Where(r => r != null && r.BankId == refbankId).ToList();
                _resourceDtoCache.RemoveList(bankId);
                _resourceDtoCache.PutList(refbankId, listPerBank);
                StoreResourceIdsByBank(refbankId, listPerBank.Select(r => r.ResourceId));
                StoreChildBanksByBank(refbankId, listPerBank.Where(r => r.CustomPropertyDisplayValues != null && r.CustomPropertyDisplayValues.Count() > 0).SelectMany(r => r.CustomPropertyDisplayValues.Select(c => c.BankId).Distinct()));
            }

            if (_logger.IsInfoEnabled)
            {
                _logger.Log(LogLevel.Info, string.Format("CacheResourceDtoService - StoreList - End: bankId: {0}", bankId));
            }
        }


    }
}
