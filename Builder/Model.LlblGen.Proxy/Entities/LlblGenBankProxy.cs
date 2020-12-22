using System.Linq;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Interface;

namespace Questify.Builder.Model.LlblGen.Proxy.Entities
{
    class LlblGenBankProxy : IConvertBank
    {
        public BankDto Convert(BankEntity bankEntity)
        {
            if (bankEntity == null)
            {
                return null;
            }
            var bank = new BankDto()
            {
                Id = bankEntity.Id,
                ParentBankId = bankEntity.ParentBankId,
                Name = bankEntity.Name,
                CreatedBy = bankEntity.CreatedBy,
                CreationDate = bankEntity.CreationDate,
                ModifiedBy = bankEntity.ModifiedBy,
                ModifiedDate = bankEntity.ModifiedDate
            };
            if (bankEntity.BankCollection == null)
            {
                return bank;
            }
            var collectionList = bankEntity.BankCollection.Select(b => Convert(b)).ToList();
            bank.BankCollection = collectionList;
            return bank;
        }
    }
}
