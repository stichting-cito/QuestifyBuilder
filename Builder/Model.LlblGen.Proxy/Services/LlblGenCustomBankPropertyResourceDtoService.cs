using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Logic.Service.Model.Entities.Custom;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.Factory;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenCustomBankPropertyResourceDtoService : ICustomBankPropertyResourceDtoRepository
    {

        public CustomBankPropertyResourceDto Get(Guid id)
        {
            var list = GetMulti(new List<Guid> { id });
            if (list == null)
            {
                return null;
            }
            return list.FirstOrDefault();
        }

        public IEnumerable<CustomBankPropertyResourceDto> GetMulti(IEnumerable<Guid> ids)
        {
            var proxy = new LlblGenBankProxyFactory();
            if (ids == null)
            {
                return null;
            }
            var list = BankFactoryWithoutPermissionCheck.Instance.GetCustomBankProperties(ids.ToList());
            if (list == null)
            {
                return null;
            }
            return (from c in list
                    select proxy.ConvertCustomResourceProperty(c)).ToList();
        }

        public IEnumerable<CustomBankPropertyResourceDto> GetResourcesForBank(int id)
        {
            var proxy = new LlblGenBankProxyFactory();
            var list = BankFactoryWithoutPermissionCheck.Instance.GetCustomBankPropertiesForBranchById(id, ResourceTypeEnum.AllResources);
            if (list == null)
            {
                return null;
            }
            return (from c in list
                    select proxy.ConvertCustomResourceProperty(c as CustomBankPropertyEntity)).ToList();
        }

        public IEnumerable<ResourceDto> GetDependencies(Guid id)
        {
            throw new NotImplementedException();
        }



        public CustomBankPropertyResourceDto Get(int bankId, string name)
        {
            throw new NotImplementedException();
        }

        public void Save(CustomBankPropertyResourceDto entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(Guid key)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntities(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public void EntityChanged(Guid key)
        {
            throw new NotImplementedException();
        }

        public void EntitiesChanged(IEnumerable<Guid> keys)
        {
            throw new NotImplementedException();
        }

        public void BankChanged(int bankId)
        {
            throw new NotImplementedException();
        }
    }
}
