using System;
using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Cache.CacheEntities;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using CustomBankPropertyDto = Questify.Builder.Logic.Service.Model.Entities.CustomBankPropertyDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheCustomBankPropertyDtoService : BaseCustomPropertyDtoServiceDecorator
    {


        private readonly int _secondsInCache = 28800; private readonly bool _sliding;
        private readonly CustomBankPropertyDtoCache _customBankPropertyCache;


        public CacheCustomBankPropertyDtoService(ICustomBankPropertyDtoRepository decoree)
    : base(decoree)
        {
            var cacheSettingsDto = ConfigPluginHelper.GetCacheSettingsByType("DtoCustomBankPropertyEntity");
            if (cacheSettingsDto != null)
            {
                _secondsInCache = cacheSettingsDto.TimeInSeconds;
                _sliding = cacheSettingsDto.Sliding;
            }
            _customBankPropertyCache = new CustomBankPropertyDtoCache(_secondsInCache, _sliding);
        }



        public override CustomBankPropertyDto Get(Guid id)
        {
            var cachedEntity = _customBankPropertyCache.Get(id);
            if (cachedEntity != null)
            {
                return cachedEntity;
            }
            {
                var customProperty = base.Get(id);
                PutSingleCustomProperty(customProperty);
                return customProperty;
            }
        }

        public override IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranchWithFilter(int bankId, string type)
        {
            var cachedList = _customBankPropertyCache.GetCustomBankPropertiesForBranch(bankId, type);
            if (cachedList != null)
            {
                return cachedList;
            }
            {
                var list = type == "AllResources" ? base.GetCustomBankPropertiesForBranch(bankId).ToList() : base.GetCustomBankPropertiesForBranchWithFilter(bankId, type).ToList();
                _customBankPropertyCache.PutCustomBankPropertiesForBranch(bankId, type, list);
                foreach (var c in list)
                {
                    var newList = new List<int> { bankId };
                    var currentList = _customBankPropertyCache.GetBankIdsForCustomBankPropertyId(c.CustomBankPropertyId);
                    if (currentList != null)
                    {
                        newList = newList.Union(currentList).ToList();
                    }
                    _customBankPropertyCache.RemoveBankIdsForCustomBankPropertyId(c.CustomBankPropertyId);
                    _customBankPropertyCache.PutBankIdForCustomBankPropertyIds(c.CustomBankPropertyId, newList);
                    PutSingleCustomProperty(c);
                }
                return list.ToList();
            }
        }

        public override IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranch(int bankId)
        {

            return GetCustomBankPropertiesForBranchWithFilter(bankId, "AllResources");
        }

        public override string GetSelectedValueDisplayValue(Guid selectedValue, int bankId)
        {
            return GetCustomBankPropertiesForBranch(bankId).Select(c =>
            {
                var customPropertyValueDto = c.Values.FirstOrDefault(cv => cv.CustomPropertyValueId == selectedValue);
                return customPropertyValueDto != null ? customPropertyValueDto.DisplayValue : null;
            }).FirstOrDefault();
        }



        public override void BankChanged(int bankId)
        {
            BanksChanged(new List<int> { bankId });

        }

        public override void BanksChanged(IEnumerable<int> bankIds)
        {
            foreach (var bankId in bankIds)
            {
                _customBankPropertyCache.RemoveAllCustomPropertiesCachingForBank(bankId);
            }
        }



        private void PutSingleCustomProperty(CustomBankPropertyDto customBankProperty)
        {
            var customPropertyId = customBankProperty.CustomBankPropertyId;
            var bankId = customBankProperty.BankId;
            _customBankPropertyCache.Remove(customPropertyId);
            _customBankPropertyCache.Put(customBankProperty.CustomBankPropertyId, customBankProperty);
            var newList = new List<Guid> { customPropertyId };
            var currentList = _customBankPropertyCache.GetCustomPropertiesForBank(bankId);
            if (currentList != null)
            {
                newList = newList.Union(currentList).ToList();
            }
            _customBankPropertyCache.RemoveCustomPropertiesForBank(bankId);
            _customBankPropertyCache.PutCustomPropertiesForBank(bankId, newList);
        }


    }
}
