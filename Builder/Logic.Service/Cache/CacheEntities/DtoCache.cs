using System.Collections.Generic;
using Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class DtoCache<TEntity, TEntityKey> : CacheBase where TEntity : class
    {

        public DtoCache(int cacheExpiryInSeconds, bool sliding, string name)
            : base(name, cacheExpiryInSeconds, sliding) { }

        public DtoCache(int cacheExpiryInSeconds, bool sliding)
            : base("DtoCache", cacheExpiryInSeconds, sliding) { }


        public TEntity Get(TEntityKey id)
        {
            return (TEntity)GetValue(id.ToString());
        }

        public IEnumerable<TEntity> GetList(string id)
        {
            return (IEnumerable<TEntity>)GetValue(id);
        }



        public void Put(TEntityKey id, TEntity resourceEntity)
        {
            var key = string.Format("{0}", id);
            base.Put(key, resourceEntity);
        }

        public void PutList(string id, IEnumerable<TEntity> resourceEntity)
        {
            var key = string.Format("{0}", id);
            base.Put(key, resourceEntity);
        }



        public void Remove(TEntityKey id)
        {
            var key = string.Format("{0}", id);
            base.Remove(key);
        }

        public void RemoveList(string id)
        {
            var key = string.Format("{0}", id);
            base.Remove(key);
        }



    }
}
