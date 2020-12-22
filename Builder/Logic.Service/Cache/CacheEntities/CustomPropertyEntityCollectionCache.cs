using System;
using Enums;
using Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class CustomPropertyEntityCollectionCache : EntityCollectionCacheBase
    {

        public CustomPropertyEntityCollectionCache(int cacheExpiryInSeconds, bool sliding)
    : base("CustomPropertyEntityCollectionCache", cacheExpiryInSeconds, sliding) { }

        public EntityCollection Get(int bankId, ResourceTypeEnum applicableTo)
        {
            string key = string.Format("{0}-{1}", bankId, applicableTo);
            return base.GetCollection(key);
        }

        public CustomBankPropertyEntity Get(Guid customPropertyId)
        {
            return GetValue(customPropertyId.ToString()) as CustomBankPropertyEntity;
        }

        public void Put(int bankId, ResourceTypeEnum applicableTo, EntityCollection customPropertyCollection)
        {
            var key = string.Format("{0}-{1}", bankId, applicableTo);
            Put(key, customPropertyCollection);
            foreach (var entity in customPropertyCollection)
            {
                Put((CustomBankPropertyEntity)entity);
            }
        }

        public void Put(CustomBankPropertyEntity entity)
        {
            var key = entity.CustomBankPropertyId.ToString();
            base.Put(key, entity);
        }

        public bool IsEmpty(int bankId, ResourceTypeEnum applicableTo)
        {
            string key = string.Format("{0}-{1}", bankId, applicableTo);
            return base.IsEmpty(key);
        }

        public bool IsCached(int bankId, ResourceTypeEnum applicableTo)
        {
            string key = string.Format("{0}-{1}", bankId, applicableTo);
            return base.IsCached(key);
        }

        public void Remove(int bankId)
        {
            foreach (ResourceTypeEnum resourceType in Enum.GetValues(typeof(ResourceTypeEnum)))
            {
                var key = string.Format("{0}-{1}", bankId, resourceType);
                base.Remove(key);
            }
        }


    }
}


