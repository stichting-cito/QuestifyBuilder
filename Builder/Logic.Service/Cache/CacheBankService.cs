using System;
using System.Globalization;
using System.Threading;
using Enums;
using Questify.Builder.Logic.Service.Cache.CacheEntities;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Security;
using IBankService = Questify.Builder.Logic.Service.Interfaces.IBankService;

namespace Questify.Builder.Logic.Service.Cache
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", MessageId = "args")]
    public class CacheBankService : Decorators.BaseBankServiceDecorator, IDisposable
    {
        protected readonly EntityCache<BankEntity> BankCache;
        protected readonly CustomPropertyEntityCollectionCache CustomBankPropertyCache;
        protected readonly BankHierarchyCache BankHierarchyCache;

        private static int _stopUsingCache;

        private bool _disposed;



        public CacheBankService(IBankService decoree)
    : this(decoree, 10, false, 3600, true, 10, true, true)
        {

        }


        public CacheBankService(
        IBankService decoree,
        int expInSecBank,
        bool slidingBank,
        int expInSecBankHierarchy,
        bool slidingBankHierarchy,
        int expInSecCustomProperties,
        bool slidingCustomProperties,
        bool fromConfig)
    : base(decoree)
        {
            int expiry = expInSecBank;
            bool sliding = slidingBank;
            int expiryInHierarchy = expInSecBankHierarchy;
            bool slidingHierarchy = slidingBankHierarchy;
            int expiryCustom = expInSecCustomProperties;
            bool slidingCustom = slidingCustomProperties;

            if (fromConfig)
            {
                var cacheSettingsBank = ConfigPluginHelper.GetCacheSettingsByType("BankEntity");
                var cacheSettingsBankHierarchy = ConfigPluginHelper.GetCacheSettingsByType("BankHierarchy");
                var cacheSettingsCustomProperties = ConfigPluginHelper.GetCacheSettingsByType("CustomPropertyEntityCollection");
                if (cacheSettingsBank != null)
                {
                    expiry = cacheSettingsBank.TimeInSeconds;
                    sliding = cacheSettingsBank.Sliding;
                }
                if (cacheSettingsBankHierarchy != null)
                {
                    expiryInHierarchy = cacheSettingsBankHierarchy.TimeInSeconds;
                    slidingHierarchy = cacheSettingsBankHierarchy.Sliding;
                }
                if (cacheSettingsCustomProperties != null)
                {
                    expiryCustom = cacheSettingsCustomProperties.TimeInSeconds;
                    slidingCustom = cacheSettingsCustomProperties.Sliding;
                }

            }
            BankCache = new EntityCache<BankEntity>(expiry, sliding);
            CustomBankPropertyCache = new CustomPropertyEntityCollectionCache(expiryCustom, slidingCustom);
            BankHierarchyCache = new BankHierarchyCache(expiryInHierarchy, slidingHierarchy);
        }



        public override EntityCollection GetCustomBankPropertiesForBranch(BankEntity bank, ResourceTypeEnum applicableTo)
        {
            var cacheProperties = CustomBankPropertyCache.Get(bank.Id, applicableTo);
            if (cacheProperties != null || CustomBankPropertyCache.IsEmpty(bank.Id, applicableTo))
            {
                return cacheProperties;
            }
            var customBankProperties = base.GetCustomBankPropertiesForBranch(bank, applicableTo);
            CustomBankPropertyCache.Put(bank.Id, applicableTo, customBankProperties);
            return customBankProperties;
        }

        public override BankEntity GetBank(int bankId)
        {
            var cachedBank = BankCache.Get(bankId.ToString(CultureInfo.InvariantCulture), bankId);
            if (cachedBank != null)
            {
                return cachedBank;
            }
            var bank = base.GetBank(bankId);
            BankCache.Put(bankId.ToString(CultureInfo.InvariantCulture), bankId, bank);
            return bank;
        }

        public override SerializableDictionaryInteger FetchAllRelations()
        {
            {
                if (_stopUsingCache == 0)
                {
                    var cachedBankCollection = BankHierarchyCache.Get();
                    if (cachedBankCollection != null)
                    {
                        return cachedBankCollection;
                    }
                }
                var bankRelations = base.FetchAllRelations();
                BankHierarchyCache.Remove();
                BankHierarchyCache.Put(bankRelations);
                return bankRelations;
            }
        }

        public override TreeStructureCustomBankPropertyEntity PopulateTreeCustomBankPropertyHierarchy(Guid id)
        {

            var result = CustomBankPropertyCache.Get(id) as TreeStructureCustomBankPropertyEntity;
            if (result != null)
            {
                return result;
            }

            result = base.PopulateTreeCustomBankPropertyHierarchy(id);
            if (result != null)
            {
                CustomBankPropertyCache.Put(result);
            }
            return result;
        }


        public override string DeleteBank(BankEntity bank)
        {
            ClearCache(bank.Id);
            return base.DeleteBank(bank);
        }

        public override string ClearAndDeleteBankHierarchical(int bankId)
        {
            ClearCache(bankId);
            return base.ClearAndDeleteBankHierarchical(bankId);
        }

        public override void UpdateBank(BankEntity bank)
        {
            base.UpdateBank(bank);
            ClearCache(bank.Id);
        }

        public override bool ClearBank(int bankId)
        {
            var ret = base.ClearBank(bankId);
            ClearCache(bankId);
            return ret;
        }

        public override void UpdateBankHierarchy(EntityCollection banks)
        {
            base.UpdateBankHierarchy(banks);
            BankHierarchyCache.Remove();
        }


        public override string DeleteCustomProperties(EntityCollection entitiesToRemove)
        {
            var removedCustomProperties = base.DeleteCustomProperties(entitiesToRemove);
            CustomBankPropertyCache.Clear();
            return removedCustomProperties;
        }

        public override string DeleteCustomPropertiesForced(EntityCollection entitiesToRemove)
        {
            var removedCustomProperties = base.DeleteCustomPropertiesForced(entitiesToRemove);
            CustomBankPropertyCache.Clear();
            return removedCustomProperties;
        }

        public override string DeleteCustomPropertyValues(EntityCollection entitiesToRemove)
        {
            var removedCustomProperties = base.DeleteCustomPropertyValues(entitiesToRemove);
            CustomBankPropertyCache.Clear();
            return removedCustomProperties;
        }

        public override string UpdateCustomProperties(EntityCollection entitiesToUpdate)
        {
            var removedCustomProperties = base.UpdateCustomProperties(entitiesToUpdate);
            CustomBankPropertyCache.Clear();
            return removedCustomProperties;
        }

        public override string UpdateCustomProperty(CustomBankPropertyEntity customBankProperty)
        {
            var removedCustomProperties = base.UpdateCustomProperty(customBankProperty);
            CustomBankPropertyCache.Clear();
            return removedCustomProperties;
        }




        public static void StopUsingCache()
        {
            Interlocked.Increment(ref _stopUsingCache);
        }

        public static void StartUsingCache()
        {
            Interlocked.Decrement(ref _stopUsingCache);
        }



        private void ClearCache(int bankId)
        {
            BankCache.Remove(bankId.ToString(CultureInfo.InvariantCulture));
            BankHierarchyCache.Remove();
            CustomBankPropertyCache.Clear();
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
                BankCache?.Dispose();
                CustomBankPropertyCache?.Dispose();
                BankHierarchyCache?.Dispose();
            }
            _disposed = true;
        }

        ~CacheBankService()
        {
            Dispose(false);
        }

    }
}
