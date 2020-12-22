using System;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass
{
    public abstract class CacheBase : IDisposable
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private readonly MemoryCache _entityCache;
        private readonly MemoryCache _emptyCache;
        internal CacheItemPolicy CachePolicy;
        private readonly bool _sliding;
        internal bool ShouldCache;
        private readonly int _expTimeInSec;

        private bool _disposed;


        protected CacheBase(string name, int cacheExpiryInSeconds, bool sliding)
        {
            _sliding = sliding;
            _expTimeInSec = cacheExpiryInSeconds;
            if (_expTimeInSec <= 0)
            {
                ShouldCache = false;
            }
            else
            {
                ShouldCache = true;
                _entityCache = new MemoryCache(name);
                _emptyCache = new MemoryCache(string.Concat("empty", name));
                CachePolicy = new CacheItemPolicy();
                if (sliding)
                {
                    CachePolicy.SlidingExpiration = new TimeSpan(0, 0, _expTimeInSec);
                }
                else
                {
                    CachePolicy.AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(_expTimeInSec));
                    CachePolicy.SlidingExpiration = ObjectCache.NoSlidingExpiration;
                }
            }
        }


        public virtual object GetValue(string key)
        {
            if (!ShouldCache || !_entityCache.Contains(key))
            {
                return null;
            }

            _cacheLock.EnterReadLock();
            try
            {
                var cachedObject = _entityCache.GetCacheItem(key);
                if (cachedObject != null)
                {
                    return cachedObject.Value;
                }
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
            {
                return null;
            }
        }

        public virtual bool IsCached(string key)
        {
            if (ShouldCache && (GetValue(key) != null || IsEmpty(key)))
            {
                return true;
            }
            {
                return false;
            }
        }

        public virtual bool IsEmpty(string key)
        {
            if (!ShouldCache)
            {
                return false;
            }

            _cacheLock.EnterReadLock();
            try
            {
                return _emptyCache.Contains(key);
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
            return false;
        }

        public virtual void Remove(string key)
        {
            if (!_cacheLock.TryEnterWriteLock(5000))
            {
                return;
            }

            try
            {
                if (_entityCache != null && _entityCache.Contains(key))
                {
                    _entityCache.Remove(key);
                }
                if (_emptyCache != null && _emptyCache.Contains(key))
                {
                    _emptyCache.Remove(key);
                }
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            if (!ShouldCache || !_cacheLock.TryEnterWriteLock(5000))
            {
                return;
            }

            try
            {
                foreach (var a in _entityCache)
                {
                    _entityCache.Remove(a.Key);
                }
                foreach (var a in _emptyCache)
                {
                    _emptyCache.Remove(a.Key);
                }
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public virtual void Put(string key, object entity)
        {
            if (!ShouldCache)
            {
                return;
            }

            if (!_sliding)
            {
                CachePolicy.AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(_expTimeInSec));
                CachePolicy.SlidingExpiration = ObjectCache.NoSlidingExpiration;
            }

            if (!_entityCache.Contains(key) && entity != null)
            {
                if (_cacheLock.TryEnterWriteLock(5000))
                {
                    try
                    {
                        _entityCache.Add(new CacheItem(key, entity), CachePolicy);
                    }
                    finally
                    {
                        _cacheLock.ExitWriteLock();
                    }
                }

            }
            else
            {
                if (entity != null || _emptyCache.Contains(key) || !_cacheLock.TryEnterWriteLock(5000))
                {
                    return;
                }

                try
                {
                    _emptyCache.Add(new CacheItem(key, "empty"), CachePolicy);
                }
                finally
                {
                    _cacheLock.ExitWriteLock();
                }
            }
        }

        public virtual void FlushCachePermission(string key)
        {
            var cacheEntities = _entityCache
                      .Where(x => x.Key.EndsWith(key))
                    .ToList();
            foreach (var cacheEntity in cacheEntities)
            {
                Remove(cacheEntity.Key);
            }
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
                _emptyCache?.Dispose();
                _entityCache?.Dispose();
            }

            _disposed = true;
        }

        ~CacheBase()
        {
            Dispose(false);
        }
    }
}
