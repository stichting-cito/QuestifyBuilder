
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache.CacheEntities;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    public abstract class EntityCacheTestBase<T> where T : new()
	{

		public void PutAndGetEntity_SlidedTest()
		{
			//Arrange
			var cache = new EntityCache<T>(600, true);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNotNull(objectRetrievedFromCache);
			Assert.AreEqual(entityToCache, objectRetrievedFromCache);
			Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
			Assert.IsTrue(cache.IsCached(cacheKey, bankId));
		}

		public void PutAndGetEntity_NonSlidedTest()
		{
			//Arrange
			var cache = new EntityCache<T>(600, false);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNotNull(objectRetrievedFromCache);
			Assert.AreEqual(entityToCache, objectRetrievedFromCache);
			Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
			Assert.IsTrue(cache.IsCached(cacheKey, bankId));
		}

		public void PutAndRemoveAndGetEntity_SlidedTest()
		{
			//Arrange
			var cache = new EntityCache<T>(600, true);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			cache.Remove(cacheKey, bankId);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNull(objectRetrievedFromCache);
			Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
			Assert.IsFalse(cache.IsCached(cacheKey, bankId));
		}

        public void PutAndRemoveAndGetEntity_NonSlidedTest()
		{
			//Arrange
			var cache = new EntityCache<T>(600, false);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			cache.Remove(cacheKey, bankId);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNull(objectRetrievedFromCache);
			Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
			Assert.IsFalse(cache.IsCached(cacheKey, bankId));
		}

        public void CacheExpired_SlidedTest()
		{
			//Arrange
			var cache = new EntityCache<T>(1, true);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			System.Threading.Thread.Sleep(2000);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNull(objectRetrievedFromCache);
		}

        public void CacheExpired_NonSlidedTest()
		{
			//Arrange
			var cache = new EntityCache<T>(1, false);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			System.Threading.Thread.Sleep(2000);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNull(objectRetrievedFromCache);
			Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
			Assert.IsFalse(cache.IsCached(cacheKey, bankId));
		}

        public void CacheNotExpired_SlidedTest()
		{
			//Arrange
			var cache = new EntityCache<T>(2, true);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			System.Threading.Thread.Sleep(1000);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNotNull(objectRetrievedFromCache);
			Assert.AreEqual(entityToCache, objectRetrievedFromCache);
			Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
			Assert.IsTrue(cache.IsCached(cacheKey, bankId));
		}

        public void CacheNotExpired_AccessedMultipleTimesWithinSlidingWindow_SliderTest()
		{
			//Arrange
			var cache = new EntityCache<T>(1, true);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			System.Threading.Thread.Sleep(100);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);
			System.Threading.Thread.Sleep(100);
			objectRetrievedFromCache = cache.Get(cacheKey, bankId);
			System.Threading.Thread.Sleep(100);
			objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNotNull(objectRetrievedFromCache);
			Assert.AreEqual(entityToCache, objectRetrievedFromCache);
			Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
			Assert.IsTrue(cache.IsCached(cacheKey, bankId));
		}

        public void CacheNotExpired_AccessedMultipleTimesWithinSlidingWindow_NonSliderTest()
		{
			//Arrange
			var cache = new EntityCache<T>(2, false);
			var bankId = 128;
			var entityToCache = new T();
			var cacheKey = "EntityToCache";

			//Execute
			cache.Put(cacheKey, bankId, entityToCache);
			System.Threading.Thread.Sleep(1000);
			var objectRetrievedFromCache = cache.Get(cacheKey, bankId);
			System.Threading.Thread.Sleep(1000);
			objectRetrievedFromCache = cache.Get(cacheKey, bankId);
			System.Threading.Thread.Sleep(1000);
			objectRetrievedFromCache = cache.Get(cacheKey, bankId);

			//Assert
			Assert.IsNull(objectRetrievedFromCache);
			Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
			Assert.IsFalse(cache.IsCached(cacheKey, bankId));
		}

        public void CacheNotExpired_NonSlidedTest()
        {
            //Arrange
            var cache = new EntityCache<T>(2, false);
            var bankId = 128;
            var entityToCache = new T();
            var cacheKey = "EntityToCache";

            //Execute
            cache.Put(cacheKey, bankId, entityToCache);
            System.Threading.Thread.Sleep(1000);
            var objectRetrievedFromCache = cache.Get(cacheKey, bankId);

            //Assert
            Assert.IsNotNull(objectRetrievedFromCache);
            Assert.AreEqual(entityToCache, objectRetrievedFromCache);
            Assert.IsFalse(cache.IsEmpty(cacheKey, bankId));
            Assert.IsTrue(cache.IsCached(cacheKey, bankId));
        }

        
        public void CachedNewItemsShouldGetNewExpiryTime_NonSlidedTest()
        {
            //Arrange
            var cache = new EntityCache<T>(2, false);
            const int bankId = 128;
            var entityToCache1 = new T();
            var entityToCache2 = new T();
            const string cacheKey1 = "EntityToCache1";
            const string cacheKey2 = "EntityToCache2";

            //Execute
            cache.Put(cacheKey1, bankId, entityToCache1);
            Thread.Sleep(500);
            cache.Put(cacheKey2, bankId, entityToCache2);
            Thread.Sleep(1600);
            
            var objectRetrievedFromCache1 = cache.Get(cacheKey1, bankId);
            var objectRetrievedFromCache2 = cache.Get(cacheKey2, bankId);

            //Assert
            Assert.IsNull(objectRetrievedFromCache1, "entityToCache1 should not be in the cache anymore");
            Assert.IsNotNull(objectRetrievedFromCache2, "entityToCache2 should still be in the cache"); 
            Assert.IsFalse(cache.IsEmpty(cacheKey1, bankId));
            Assert.IsFalse(cache.IsCached(cacheKey1, bankId));
            Assert.IsFalse(cache.IsEmpty(cacheKey2, bankId));
            Assert.IsTrue(cache.IsCached(cacheKey2, bankId));
        }
	}
}
