using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Cache.CacheEntities;
using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.InvalidateCache.Helper;
using BankDto = Questify.Builder.Logic.Service.Model.Entities.BankDto;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheBankDtoService : BaseBankDtoServiceDecorator
    {


        private readonly int _secondsInCache = 28800; private readonly bool _sliding;
        private readonly BankDtoCache _bankCache;
        private const string Fullhierarchie = "FULL_BANK_HIERARCHY";



        public CacheBankDtoService(IBankDtoRepository decoree)
    : base(decoree)
        {
            var cacheSettingsDto = ConfigPluginHelper.GetCacheSettingsByType("DtoBankEntity");
            if (cacheSettingsDto != null)
            {
                _secondsInCache = cacheSettingsDto.TimeInSeconds;
                _sliding = cacheSettingsDto.Sliding;
            }
            _bankCache = new BankDtoCache(_secondsInCache, _sliding);
        }


        public override BankDto Get(int id)
        {
            var cachedBankhierarchie = All();
            if (cachedBankhierarchie != null)
            {
                return cachedBankhierarchie.Flattened().FirstOrDefault(b => b.Id == id);
            }
            return null;
        }

        public override IEnumerable<BankDto> All()
        {
            var cachedBankhierarchie = _bankCache.GetList(Fullhierarchie);
            if (cachedBankhierarchie != null)
            {
                return cachedBankhierarchie;
            }
            {
                var bankHierarchy = base.All();
                var banks = bankHierarchy as IList<BankDto> ?? bankHierarchy.ToList();
                _bankCache.PutList(Fullhierarchie, banks);
                return banks;
            }
        }

        public override IEnumerable<BankDto> GetBankAndParents(int id)
        {
            var returnList = new List<BankDto>();
            var bank = Get(id);
            if (bank == null)
            {
                return returnList;
            }

            returnList.Add(bank);
            while (bank.ParentBankId != null)
            {
                bank = Get(bank.ParentBankId.Value);
                returnList.Add(bank);
            }
            return returnList;
        }


        public override void EntityChanged(int key)
        {
            EntitiesChanged(new List<int> { key });
        }

        public override void EntitiesChanged(IEnumerable<int> keys)
        {
            _bankCache.RemoveList(Fullhierarchie);
            using (new StopUsingPermissionAndSecurityCache())
            {
                All();
            }
        }
    }
}
