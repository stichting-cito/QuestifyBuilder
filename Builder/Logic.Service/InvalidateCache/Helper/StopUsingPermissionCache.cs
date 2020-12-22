using System;
using Questify.Builder.Logic.Service.Cache;

namespace Questify.Builder.Logic.Service.InvalidateCache.Helper
{
    public class StopUsingPermissionAndSecurityCache : IDisposable
    {

        public StopUsingPermissionAndSecurityCache()
        {
            CachePermissionService.StopUsingCache();
            CacheSecurityService.StopUsingCache();
        }


        public void Dispose()
        {
            CachePermissionService.StartUsingCache();
            CacheSecurityService.StartUsingCache();
        }
    }
}
