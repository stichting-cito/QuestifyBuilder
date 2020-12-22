using System;
using System.Runtime.Caching;
using System.Threading;
using Questify.Builder.Model.ContentModel.HelperClasses;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass
{
    public abstract class EntityCollectionCacheBase : CachePerUserBase
    {

        private readonly MemoryCache _collectionCountCache; private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private bool _disposed;




        protected EntityCollectionCacheBase(string name, int cacheExpiryInSeconds, bool sliding)
    : base(name, cacheExpiryInSeconds, sliding)
        {
            if (cacheExpiryInSeconds <= 0)
            {
                return;
            }

            _collectionCountCache = new MemoryCache("collectionCacheCount");
            CachePolicy = new CacheItemPolicy();
            if (sliding)
            {
                CachePolicy.SlidingExpiration = new TimeSpan(0, 0, cacheExpiryInSeconds);
            }
        }



        internal void Put(string key, EntityCollection entityCollection)
        {
            if (!ShouldCache)
            {
                return;
            }

            base.Put(key, entityCollection);
            if (entityCollection == null) return;

            if (!_cacheLock.TryEnterWriteLock(5000))
            {
                return;
            }

            try
            {
                _collectionCountCache.Add(key, entityCollection.Count, CachePolicy);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        protected EntityCollection GetCollection(string key)
        {
            if (!ShouldCache)
            {
                return null;
            }
            var collection = (EntityCollection)GetValue(key);
            int? entityCollectionCount = GetCachCount(key);
            if (collection != null && entityCollectionCount.HasValue && collection.Count != entityCollectionCount)
            {
                Remove(key);
                return null;
            }
            {
                return collection;
            }
        }

        public override bool IsCached(string key)
        {
            var cached = base.IsCached(key);
            if (!cached)
            {
                return false;
            }
            var entityCollectionCount = GetCachCount(key);
            var collection = (EntityCollection)GetValue(key);
            if (entityCollectionCount != null && (collection != null && collection.Count != entityCollectionCount.Value))
            {
                return false;
            }
            return true;
        }

        private int? GetCachCount(string key)
        {
            int? entityCollectionCount = null;
            _cacheLock.EnterReadLock();
            try
            {
                var cacheCount = _collectionCountCache.GetCacheItem(key);
                if (cacheCount != null)
                {
                    entityCollectionCount = (int)cacheCount.Value;
                }
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
            return entityCollectionCount;
        }


        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _cacheLock?.Dispose();
                _collectionCountCache?.Dispose();
            }

            _disposed = true;
            base.Dispose(true);
        }

    }
}
