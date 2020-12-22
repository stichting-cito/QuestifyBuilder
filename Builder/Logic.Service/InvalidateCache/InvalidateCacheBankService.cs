using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Questify.Builder.Logic.Service.InvalidateCache.Helper;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using IBankService = Questify.Builder.Logic.Service.Interfaces.IBankService;

namespace Questify.Builder.Logic.Service.InvalidateCache
{
    public class InvalidateCacheBankService : Decorators.BaseBankServiceDecorator
    {

        private static List<int> _banksToInvalidate = new List<int>();
        private static List<Guid> _customPropertyToInvalidate = new List<Guid>();
        private static readonly ReaderWriterLockSlim CacheLock = new ReaderWriterLockSlim();
        private static int _batchNotifierCount;


        public InvalidateCacheBankService(IBankService decoree)
            : base(decoree)
        {
        }


        public override bool ClearBank(int bankId)
        {
            var result = base.ClearBank(bankId);
            ClearCache(new List<int> { bankId });
            return result;
        }

        public override string ClearAndDeleteBankHierarchical(int bankId)
        {
            var flattenedBanks = new List<int>();
            GetFlatBankStructure(base.GetBank(bankId), ref flattenedBanks);
            var result = base.ClearAndDeleteBankHierarchical(bankId);
            ClearCache(flattenedBanks);
            return result;
        }

        public override string DeleteBank(BankEntity bank)
        {
            if (bank == null) return null;
            var bankId = bank.Id;
            var result = base.DeleteBank(bank);
            ClearCache(new List<int> { bankId });
            return result;
        }

        public override string DeleteCustomProperties(EntityCollection entitiesToRemove)
        {
            var bankIds = entitiesToRemove.OfType<CustomBankPropertyEntity>().Select(r => r.BankId).Distinct().ToList();
            var ids = entitiesToRemove.OfType<CustomBankPropertyEntity>().Select(r => r.CustomBankPropertyId).ToList();
            var result = base.DeleteCustomProperties(entitiesToRemove);
            ClearCache(bankIds, ids);
            return result;
        }

        public override string DeleteCustomPropertiesForced(EntityCollection entitiesToRemove)
        {
            var bankIds = entitiesToRemove.OfType<CustomBankPropertyEntity>().Select(r => r.BankId).Distinct().ToList();
            var ids = entitiesToRemove.OfType<CustomBankPropertyEntity>().Select(r => r.CustomBankPropertyId).ToList();
            var result = base.DeleteCustomPropertiesForced(entitiesToRemove);
            ClearCache(bankIds, ids);
            return result;
        }

        public override string DeleteCustomPropertyValues(EntityCollection entitiesToRemove)
        {
            var bankIds = entitiesToRemove.OfType<CustomBankPropertyEntity>().Select(r => r.BankId).Distinct().ToList();
            var ids = entitiesToRemove.OfType<CustomBankPropertyEntity>().Select(r => r.CustomBankPropertyId).ToList();
            var result = base.DeleteCustomPropertyValues(entitiesToRemove);
            ClearCache(bankIds, ids);
            return result;
        }

        public override void UpdateBank(BankEntity bank)
        {
            if (bank != null)
            {
                var id = bank.Id;
                base.UpdateBank(bank);
                if (id == 0)
                {
                    id = bank.Id;
                }
                InvalidateCacheHelper.ClearCacheForBank(id);
            }
        }

        public override void UpdateBankHierarchy(EntityCollection banks)
        {
            var flattenedBanks = new List<int>();
            banks.OfType<BankEntity>().ToList().ForEach(b => GetFlatBankStructure(b, ref flattenedBanks));
            base.UpdateBankHierarchy(banks);
            ClearCache(flattenedBanks);
        }

        public override string UpdateCustomProperties(EntityCollection entitiesToUpdate)
        {
            var bankIds = entitiesToUpdate.OfType<CustomBankPropertyEntity>().Select(r => r.BankId).Distinct().ToList();
            var ids = entitiesToUpdate.OfType<CustomBankPropertyEntity>().Select(r => r.CustomBankPropertyId).ToList();
            var result = base.UpdateCustomProperties(entitiesToUpdate);
            ClearCache(bankIds, ids);
            return result;
        }

        public override string UpdateCustomProperty(CustomBankPropertyEntity customBankProperty)
        {
            if (customBankProperty == null) return null;
            var id = customBankProperty.CustomBankPropertyId;
            var bankId = customBankProperty.BankId;
            var result = base.UpdateCustomProperty(customBankProperty);
            ClearCache(new List<int> { bankId }, new List<Guid> { id });
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
                InvalidateCacheHelper.ClearCustomProperties(_customPropertyToInvalidate);
                foreach (var bankId in _banksToInvalidate)
                {
                    InvalidateCacheHelper.ClearCacheForBank(bankId);
                }
                _customPropertyToInvalidate = new List<Guid>();
                _banksToInvalidate = new List<int>();
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }

        }



        private void GetFlatBankStructure(BankEntity rootBank, ref List<int> banks)
        {
            banks = banks.Union(new List<int> { rootBank.Id }).ToList();
            banks = banks.Union(rootBank.BankCollection.Select(b => b.Id)).ToList();
            foreach (var bankinLoop in rootBank.BankCollection)
            {
                var bank = bankinLoop;
                GetFlatBankStructure(bank, ref banks);
            }
        }

        private void RegisterBatchActions(int bankId)
        {
            RegisterBatchActions(new List<int> { bankId });
        }

        private void RegisterBatchActions(IEnumerable<int> bankIds)
        {
            foreach (var bankId in bankIds)
            {
                if (_batchNotifierCount > 0 && (!_banksToInvalidate.Contains(bankId)))
                {
                    _banksToInvalidate.Add(bankId);
                }
            }
        }

        private void RegisterBatchActions(IEnumerable<Guid> customPropertyIds)
        {
            if (customPropertyIds == null) return;
            foreach (var customPropertyId in customPropertyIds)
            {
                if (_batchNotifierCount > 0 && (!_customPropertyToInvalidate.Contains(customPropertyId)))
                {
                    _customPropertyToInvalidate.Add(customPropertyId);
                }
            }
        }

        private void ClearCache(List<int> bankIds, List<Guid> ids = null)
        {
            if (_batchNotifierCount == 0)
            {
                bankIds.ForEach(InvalidateCacheHelper.ClearCacheForBank);
                InvalidateCacheHelper.ClearCustomProperties(ids);
            }
            else
            {
                RegisterBatchActions(bankIds);
                RegisterBatchActions(ids);
            }
        }

    }
}
