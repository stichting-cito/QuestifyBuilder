using System;
using System.Threading;
using Questify.Builder.Logic.Service.Cache.CacheEntities;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheSecurityService : Decorators.BaseSecurityServiceDecorator, ICacheServiceHandler, IDisposable
    {
        protected TestBuilderPermissionCache TestBuilderPermissionCache;
        private bool _disposed;


        private static int _stopUsingCache;



        public CacheSecurityService(ISecurityService decoree)
            : this(decoree, 360, true, true)
        {
        }

        public CacheSecurityService(ISecurityService decoree, int expInSec, bool sliding, bool fromConfig)
            : base(decoree)
        {
            var cacheExpireyInSeconds = expInSec;
            var useSliding = sliding;

            if (fromConfig)
            {
                var cacheSettingsPremission = ConfigPluginHelper.GetCacheSettingsByType("TestBuilderPermissionCache");
                if (cacheSettingsPremission != null)
                {
                    cacheExpireyInSeconds = cacheSettingsPremission.TimeInSeconds;
                    useSliding = cacheSettingsPremission.Sliding;
                }
            }
            TestBuilderPermissionCache = new TestBuilderPermissionCache(cacheExpireyInSeconds, useSliding);
            CacheService.Instance.Register(this);
        }


        public override SerializableDictionaryIntegerPermission FetchGrantedPermissions(int[] bankIds)
        {
            CheckCacheState();
            var cachedItem = TestBuilderPermissionCache.Get(bankIds);
            if (cachedItem != null)
            {
                return cachedItem;
            }
            var gp = base.FetchGrantedPermissions(bankIds);
            TestBuilderPermissionCache.Put(bankIds, gp);
            return gp;
        }



        private void CheckCacheState()
        {
            if (_stopUsingCache != 0)
            {
                TestBuilderPermissionCache.Clear();
            }
        }

        public void RemoveSecurityFromCache(int bankId)
        {
            TestBuilderPermissionCache.Remove(bankId);
        }

        public static void StopUsingCache()
        {
            Interlocked.Increment(ref _stopUsingCache);
        }

        public static void StartUsingCache()
        {
            Interlocked.Decrement(ref _stopUsingCache);
        }

        public void FlushAllCachePermissionsForCurrentUser()
        {
            TestBuilderPermissionCache.RemoveCachePermissionsForCurrentUser();
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
                TestBuilderPermissionCache?.Dispose();
            }

            _disposed = true;
        }

        ~CacheSecurityService()
        {
            Dispose(false);
        }
    }
}