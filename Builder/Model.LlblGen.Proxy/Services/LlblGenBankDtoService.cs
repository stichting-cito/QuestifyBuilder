using System;
using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.Factory;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenBankDtoService : IBankDtoRepository
    {

        public BankDto Get(int id)
        {
            var bank = BankFactoryWithoutPermissionCheck.Instance.GetBank(id);
            var returnValue = new LlblGenBankProxyFactory().ConvertBank(bank);
            bank = null;
            return returnValue;
        }

        public IEnumerable<BankDto> GetMulti(IEnumerable<int> id)
        {
            var banks = id.Select(bankId => BankFactoryWithoutPermissionCheck.Instance.GetBank(bankId)).ToList();
            var returnValue = banks.Select(bank => new LlblGenBankProxyFactory().ConvertBank(bank)).ToList();
            banks = null;
            return returnValue;
        }

        public IEnumerable<BankDto> All()
        {
            return All(false);
        }

        public IEnumerable<BankDto> All(bool forced)
        {
            List<BankDto> returnValue;
            using (var banks = BankFactoryWithoutPermissionCheck.Instance.GetBankHierarchy())
            {
                var bankProxy = new LlblGenBankProxyFactory();
                returnValue = banks.OfType<BankEntity>().Select(bankProxy.ConvertBank).ToList();
            }
            return returnValue;
        }



        public void Save(BankDto entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int key)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntities(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }


        public void EntityChanged(int key)
        {
        }

        public void EntitiesChanged(IEnumerable<int> keys)
        {
        }

        public IEnumerable<BankDto> GetBankAndParents(int id)
        {
            throw new NotImplementedException();
        }

    }
}
