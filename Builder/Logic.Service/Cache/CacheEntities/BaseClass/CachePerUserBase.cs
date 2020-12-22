using System;
using System.Threading;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass
{
    public abstract class CachePerUserBase : CacheBase
    {
        public CachePerUserBase(string name, int cacheExpiryInSeconds, bool sliding)
    : base(name, cacheExpiryInSeconds, sliding)
        {
        }

        private int ValidatePrincipal()
        {
            if (Thread.CurrentPrincipal.Identity is TestBuilderIdentity)
                return ((TestBuilderIdentity)Thread.CurrentPrincipal.Identity).UserId;
            throw new ArgumentException("Unable to determine UserId. CurrentPrincipal.Identity is not of type TestBuilderIdentity.");
        }

        protected object GetPerUser(string key)
        {
            var userId = ValidatePrincipal();

            return GetValue(string.Format("{0}-{1}", key, userId));
        }

        protected bool IsCachedPerUser(string key)
        {
            var userId = ValidatePrincipal();

            return IsCached(string.Format("{0}-{1}", key, userId));
        }

        protected bool IsEmptyPerUser(string key)
        {
            var userId = ValidatePrincipal();

            return IsEmpty(string.Format("{0}-{1}", key, userId));
        }

        protected void PutPerUser(string key, object entity)
        {
            var userId = ValidatePrincipal();

            Put(string.Format("{0}-{1}", key, userId), entity);
        }

        protected void RemovePerUser(string key)
        {
            var userId = ValidatePrincipal();

            Remove(string.Format("{0}-{1}", key, userId));
        }

        internal void RemoveCachePermissionsForCurrentUser()
        {
            var userId = Convert.ToString(ValidatePrincipal());

            FlushCachePermission(userId);
        }
    }
}
