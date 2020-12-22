using System;
using Questify.Builder.Logic.Service.Cache;

namespace Questify.Builder.Logic.Service.InvalidateCache.Helper
{
    public class StopUsingBankCache : IDisposable
    {

        public StopUsingBankCache()
        {
            CacheBankService.StopUsingCache();
        }


        public void Dispose()
        {
            CacheBankService.StartUsingCache();
        }
    }
}
