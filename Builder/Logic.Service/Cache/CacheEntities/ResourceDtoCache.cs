using System;
using System.Collections.Generic;
using ResourceDto = Questify.Builder.Logic.Service.Model.Entities.ResourceDto;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class ResourceDtoCache<TResource> : DtoCache<TResource, Guid> where TResource : ResourceDto
    {

        public ResourceDtoCache(int cacheExpiryInSeconds, bool sliding)
            : base(cacheExpiryInSeconds, sliding, "ResourceDtoCache") { }



        public IEnumerable<TResource> GetList(int id)
        {
            var key = string.Format("List_{0}", id);
            return (IEnumerable<TResource>)GetValue(key);
        }

        public IEnumerable<int> GetChildBanks(int id)
        {
            var key = string.Format("ChildBanks_{0}", id);
            return (IEnumerable<int>)GetValue(key);
        }
        public IEnumerable<int> GetParentBanks(int id)
        {
            var key = string.Format("ParentBanks_{0}", id);
            return (IEnumerable<int>)GetValue(key);
        }

        public int? GetBankIdForResource(Guid id)
        {
            var key = string.Format("BankIdResourceId_{0}", id);
            if (GetValue(key) != null)
            {
                return (int)GetValue(key);
            }
            return null;
        }
        public IList<Guid> GetResourcesByBank(int id)
        {
            var key = string.Format("BankResources_{0}", id);
            return (IList<Guid>)GetValue(key);
        }




        public void PutList(int id, IEnumerable<TResource> refencedList)
        {
            var key = string.Format("List_{0}", id);
            base.Put(key, refencedList);
        }

        public void PutChildBanks(int id, IEnumerable<int> childbankIds)
        {
            var key = string.Format("ChildBanks_{0}", id);
            base.Put(key, childbankIds);
        }

        public void PutParentBanks(int id, IEnumerable<int> parentBankIds)
        {
            var key = string.Format("ParentBanks_{0}", id);
            base.Put(key, parentBankIds);
        }

        public void PutBankIdForResource(Guid resourceid, int bankId)
        {
            var key = string.Format("BankIdResourceId_{0}", resourceid);
            base.Put(key, bankId);
        }

        public void PutResourcesByBank(int id, IEnumerable<Guid> ids)
        {
            var key = string.Format("BankResources_{0}", id);
            base.Put(key, ids);
        }




        public void RemoveList(int id)
        {
            var key = string.Format("List_{0}", id);
            base.Remove(key);
        }

        public void RemoveChildBanks(int id)
        {
            var key = string.Format("ChildBanks_{0}", id);
            base.Remove(key);
        }

        public void RemoveBankIdForResource(Guid resourceid)
        {
            var key = string.Format("BankIdResourceId_{0}", resourceid);
            base.Remove(key);
        }

        public void RemoveResourcesByBank(int id)
        {
            var key = string.Format("BankResources_{0}", id);
            base.Remove(key);
        }

        public void RemoveParentBanks(int id)
        {
            var key = string.Format("ParentBanks_{0}", id);
            base.Remove(key);
        }

    }
}
