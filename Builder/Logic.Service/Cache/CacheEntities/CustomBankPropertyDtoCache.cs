using System;
using System.Collections.Generic;
using System.Linq;
using CustomBankPropertyDto = Questify.Builder.Logic.Service.Model.Entities.CustomBankPropertyDto;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class CustomBankPropertyDtoCache : DtoCache<CustomBankPropertyDto, Guid>
    {
        private const string TypeIdentifier = "TYPEKEY";


        public CustomBankPropertyDtoCache(int cacheExpiryInSeconds, bool sliding)
            : base(cacheExpiryInSeconds, sliding, "CustomBankPropertyDtoCache") { }



        public IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranch(int bankId, string type)
        {
            var key = string.Format("CustomBankPropertiesForBranch_{0}_{1}", bankId, type);
            return (IEnumerable<CustomBankPropertyDto>)GetValue(key);
        }

        public IEnumerable<int> GetBankIdsForCustomBankPropertyId(Guid customPropertyId)
        {
            var key = string.Format("CustomBankPropertiesForBranch_{0}", customPropertyId);
            return (IEnumerable<int>)GetValue(key);
        }

        public IEnumerable<Guid> GetCustomPropertiesForBank(int bankId)
        {
            var key = string.Format("CustomBankPropertyForBank_{0}", bankId);
            return (IEnumerable<Guid>)GetValue(key);
        }


        public void PutCustomBankPropertiesForBranch(int bankId, string type, IEnumerable<CustomBankPropertyDto> list)
        {
            var key = string.Format("CustomBankPropertiesForBranch_{0}_{1}", bankId, type);
            var newtypes = new List<string> { type };
            var currentTypes = (List<string>)GetValue(TypeIdentifier);
            if (currentTypes != null)
            {
                newtypes = newtypes.Union(currentTypes).ToList();
            }
            base.Remove(TypeIdentifier);
            base.Put(TypeIdentifier, newtypes);
            base.Put(key, list);
        }

        public void PutBankIdForCustomBankPropertyIds(Guid customPropertyId, List<int> bankIds)
        {
            var key = string.Format("CustomBankPropertiesForBranch_{0}", customPropertyId);
            base.Put(key, bankIds);
        }

        public void PutCustomPropertiesForBank(int bankId, IEnumerable<Guid> list)
        {
            var key = string.Format("CustomBankPropertyForBank_{0}", bankId);
            base.Put(key, list);
        }



        public void RemoveCustomBankPropertiesForBranch(int bankId, string type)
        {
            var key = string.Format("CustomBankPropertiesForBranch_{0}_{1}", bankId, type);
            base.Remove(key);
        }

        public void RemoveCustomBankPropertiesForBranch(int bankId)
        {
            var types = (List<string>)GetValue(TypeIdentifier);
            if (types == null || !types.Any())
            {
                return;
            }

            foreach (var type in types)
            {
                var key = string.Format("CustomBankPropertiesForBranch_{0}_{1}", bankId, type);
                base.Remove(key);
            }
        }

        public void RemoveBankIdsForCustomBankPropertyId(Guid customPropertyId)
        {
            var key = string.Format("CustomBankPropertiesForBranch_{0}", customPropertyId);
            base.Remove(key);
        }

        public void RemoveCustomPropertiesForBank(int bankId)
        {
            var key = string.Format("CustomBankPropertyForBank_{0}", bankId);
            base.Remove(key);
        }

        public void RemoveAllCustomPropertiesCachingForBank(int bankId)
        {
            var customPropertyIds = GetCustomPropertiesForBank(bankId);
            if (customPropertyIds != null && customPropertyIds.Any())
            {
                customPropertyIds.ToList().ForEach(c =>
                {
                    Remove(c);
                    var bankIdsThatContainCustomPropertyThatMightBeChanged = GetBankIdsForCustomBankPropertyId(c);
                    if (bankIdsThatContainCustomPropertyThatMightBeChanged != null && bankIdsThatContainCustomPropertyThatMightBeChanged.Any())
                    {
                        foreach (var childBankId in bankIdsThatContainCustomPropertyThatMightBeChanged)
                        {
                            RemoveCustomBankPropertiesForBranch(childBankId);
                            RemoveCustomPropertiesForBank(childBankId);
                        }
                    }
                    RemoveBankIdsForCustomBankPropertyId(c);
                });
            }

            RemoveCustomPropertiesForBank(bankId);
            RemoveCustomBankPropertiesForBranch(bankId);
        }
    }
}
