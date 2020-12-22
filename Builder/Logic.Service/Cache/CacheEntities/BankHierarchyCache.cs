using Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class BankHierarchyCache : CachePerUserBase
    {

        public BankHierarchyCache(int cacheExpiryInSeconds, bool sliding)
    : base("BankHierarchyCache", cacheExpiryInSeconds, sliding) { }

        public SerializableDictionaryInteger Get()
        {
            return (SerializableDictionaryInteger)GetValue("Banks");
        }

        public void Put(SerializableDictionaryInteger bankHierarchy)
        {
            base.Put("Banks", bankHierarchy);
        }

        public bool IsCached()
        {
            return base.IsCached("Banks");
        }

        public void Remove()
        {
            base.Remove("Banks");
        }


    }
}
