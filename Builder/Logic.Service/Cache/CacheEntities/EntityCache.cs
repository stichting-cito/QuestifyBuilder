using System;
using Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class EntityCache<T> : CachePerUserBase
    {

        public EntityCache(int cacheExpiryInSeconds, bool sliding)
    : base(typeof(T).Name, cacheExpiryInSeconds, sliding) { }

        public T Get(string name, int bankId)
        {
            var key = string.Format("{0}-{1}", name, bankId);
            return (T)base.GetPerUser(key);
        }

        public T Get(Guid resourceId)
        {
            var key = resourceId.ToString();
            return (T)base.GetPerUser(key);
        }

        public void Put(string name, int bankId, T entity)
        {
            var key = string.Format("{0}-{1}", name, bankId);
            PutPerUser(key, entity);
        }

        public void Put(Guid resourceId, T entity)
        {
            var key = resourceId.ToString();
            PutPerUser(key, entity);
        }

        public bool IsCached(string name, int bankId)
        {
            var key = string.Format("{0}-{1}", name, bankId);
            return IsCachedPerUser(key);
        }

        public bool IsCached(Guid resourceId)
        {
            var key = resourceId.ToString();
            return IsCachedPerUser(key);
        }

        public void Remove(string name, int bankId)
        {
            var key = string.Format("{0}-{1}", name, bankId);
            RemovePerUser(key);
        }

        public void Remove(Guid resourceId)
        {
            var key = resourceId.ToString();
            RemovePerUser(key);
        }

        public bool IsEmpty(string name, int bankId)
        {
            var key = string.Format("{0}-{1}", name, bankId);
            return IsEmptyPerUser(key);
        }

        public bool IsEmpty(Guid resourceId)
        {
            var key = resourceId.ToString();
            return IsEmptyPerUser(key);
        }
    }
}
