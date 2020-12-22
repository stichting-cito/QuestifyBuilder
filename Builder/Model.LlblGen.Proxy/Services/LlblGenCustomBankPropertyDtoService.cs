using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Enums;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.Factory;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenCustomBankPropertyDtoService : ICustomBankPropertyDtoRepository
    {

        public IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranch(int bankId)
        {
            return GetCustomBankPropertiesForBranchWithFilter(bankId, ResourceTypeEnum.AllResources.ToString());
        }

        public IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranchWithFilter(int bankId, string type)
        {
            ResourceTypeEnum resourceType;
            if (!Enum.TryParse(type, true, out resourceType))
            {
                resourceType = ResourceTypeEnum.AllResources;
            }

            var proxy = new LlblGenBankProxyFactory();
            var bank = BankFactoryWithoutPermissionCheck.Instance.GetBank(bankId);
            var customProperties = BankFactoryWithoutPermissionCheck.Instance.GetCustomBankPropertiesForBranch(bank, resourceType);
            return (from c in customProperties
                    select proxy.ConvertCustomProperty((CustomBankPropertyEntity)c)).ToList();
        }

        public string GetSelectedValueDisplayValue(Guid selectedValue, int bankId)
        {
            Debug.Assert(false, "MAKE SURE THIS IS GOT FROM THE CACHE!");
            return GetCustomBankPropertiesForBranch(bankId).Select(c =>
            {
                var customPropertyValuePoco = c.Values.FirstOrDefault(cv => cv.CustomPropertyValueId == selectedValue);
                return customPropertyValuePoco != null ? customPropertyValuePoco.DisplayValue : null;
            }).FirstOrDefault();
        }

        public CustomBankPropertyDto Get(Guid id)
        {
            var list = GetMulti(new List<Guid> { id });
            if (list == null)
            {
                return null;
            }
            return list.FirstOrDefault();
        }

        public IEnumerable<CustomBankPropertyDto> GetMulti(IEnumerable<Guid> ids)
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
                    select proxy.ConvertCustomProperty(c));
        }



        public void Save(CustomBankPropertyDto entity)
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
        }

        public void EntitiesChanged(IEnumerable<Guid> keys)
        {
        }

        public void BankChanged(int id)
        {
        }

        public void BanksChanged(IEnumerable<int> bankIds)
        {
        }

    }
}
