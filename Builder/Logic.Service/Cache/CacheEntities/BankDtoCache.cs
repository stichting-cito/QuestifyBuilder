using System.Collections.Generic;
using BankDto = Questify.Builder.Logic.Service.Model.Entities.BankDto;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class BankDtoCache : DtoCache<BankDto, int>
    {

        public BankDtoCache(int cacheExpiryInSeconds, bool sliding)
            : base(cacheExpiryInSeconds, sliding, "BankDtoCache") { }



        public IEnumerable<int> GetRootBanksOfBankId(int id)
        {
            var key = string.Format("RootBanksOfBankId_{0}", id);
            return (IEnumerable<int>)GetValue(key);
        }


        public void PutRootBanksOfBankId(int id, IEnumerable<int> list)
        {
            var key = string.Format("RootBanksOfBankId_{0}", id);
            base.Put(key, list);
        }


        public void RemoveRootBanksOfBankId(int id)
        {
            var key = string.Format("RootBanksOfBankId_{0}", id);
            base.Remove(key);
        }
    }
}
