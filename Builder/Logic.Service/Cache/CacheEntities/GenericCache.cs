using Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class GenericCache : CacheBase
    {
        public GenericCache(int cacheExpiryInSeconds, bool sliding)
           : base("GenericCache", cacheExpiryInSeconds, sliding) { }

    }
}
