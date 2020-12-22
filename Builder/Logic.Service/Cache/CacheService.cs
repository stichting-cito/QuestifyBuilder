using System.Collections.Generic;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Logic.Service.Cache
{
    internal class CacheService : ICacheService
    {
        private readonly IList<ICacheServiceHandler> _cacheServicesHandlers = new List<ICacheServiceHandler>();
        private CacheService()
        {
        }

        private static CacheService _instance;
        public static CacheService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CacheService();
                }
                return _instance;
            }
        }

        public static void Destroy()
        {
            _instance = null;
        }

        public void Register(ICacheServiceHandler service)
        {
            _cacheServicesHandlers.Add(service);
        }

        public void FlushAllCachePermissionsForCurrentUser()
        {
            foreach (var cacheServiceHandler in _cacheServicesHandlers)
            {
                cacheServiceHandler.FlushAllCachePermissionsForCurrentUser();
            }
        }
    }
}
