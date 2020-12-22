using System;
using System.Threading;
using Questify.Builder.Logic.Service.Cache.CacheEntities;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CachePermissionService : Decorators.BasePermissionServiceDecorator, ICacheServiceHandler, IDisposable
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", MessageId = "args")]
        protected readonly PermissionCache PermissionCache;
        private static int _stopUsingCache;
        private bool _disposed;



        public CachePermissionService(IPermissionService decoree)
            : this(decoree, 360, true, true)
        {
        }

        public CachePermissionService(IPermissionService decoree, int expInSec, bool sliding, bool fromConfig)
    : base(decoree)
        {
            var cacheExpiryInSeconds = expInSec;
            var useSliding = sliding;


            if (fromConfig)
            {
                var cacheSettingsPremission = ConfigPluginHelper.GetCacheSettingsByType("TestBuilderPermissionCache");
                if (cacheSettingsPremission != null)
                {
                    cacheExpiryInSeconds = cacheSettingsPremission.TimeInSeconds;
                    useSliding = cacheSettingsPremission.Sliding;
                }

            }
            PermissionCache = new PermissionCache(cacheExpiryInSeconds, useSliding);
            CacheService.Instance.Register(this);
        }



        public override bool TryUserIsPermittedTo(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, int bankId)
        {
            var cachedItem = PermissionCache.Get(access, permissionTarget, bankId);
            if (cachedItem.HasValue)
            {
                return cachedItem.Value;
            }

            var gp = base.TryUserIsPermittedTo(access, permissionTarget, bankId);
            PermissionCache.Put(access, permissionTarget, bankId, gp);
            return gp;
        }


        public override bool TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, TestBuilderPermissionNamedTask targettedNamedTask, int bankId, int entityInstanceId)
        {
            var cachedItem = PermissionCache.Get(access, permissionTarget, targettedNamedTask, bankId, entityInstanceId);
            if (cachedItem.HasValue)
            {
                return cachedItem.Value;
            }
            var gp = base.TryUserIsPermittedToNamedTask(access, permissionTarget, targettedNamedTask, bankId, entityInstanceId);
            PermissionCache.Put(access, permissionTarget, targettedNamedTask, bankId, entityInstanceId, gp);
            return gp;
        }

        public void RemovePermissionFromCache(int bankId)
        {
            PermissionCache.RemovePermission(bankId);
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
            PermissionCache.RemoveCachePermissionsForCurrentUser();
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
                PermissionCache?.Dispose();
            }
            _disposed = true;
        }

        ~CachePermissionService()
        {
            Dispose(false);
        }
    }
}
