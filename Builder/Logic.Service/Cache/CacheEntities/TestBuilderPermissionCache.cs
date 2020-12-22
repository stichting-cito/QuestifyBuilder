using System;
using Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class TestBuilderPermissionCache : CachePerUserBase
    {

        public TestBuilderPermissionCache(int cacheExpiryInSeconds, bool sliding)
    : base("TestBuilderPermissionCache", cacheExpiryInSeconds, sliding) { }

        public SerializableDictionaryIntegerPermission Get(int[] ids)
        {
            var key = String.Join("-", ids);
            return (SerializableDictionaryIntegerPermission)GetPerUser(key);
        }

        public void Put(int[] ids, SerializableDictionaryIntegerPermission permission)
        {
            var key = String.Format("{0}", String.Join("-", ids));
            PutPerUser(key, permission);
        }
        public bool IsCached(int[] ids)
        {
            var key = String.Join("-", ids);
            return IsCachedPerUser(key);
        }


        public void Remove(int[] ids)
        {
            var key = String.Join("-", ids);
            RemovePerUser(key);
        }

        public void Remove(int bankId)
        {
            RemovePerUser(bankId.ToString());
        }
    }
}
