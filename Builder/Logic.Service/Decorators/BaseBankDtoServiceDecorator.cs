using System.Collections.Generic;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Security;
using BankDto = Questify.Builder.Logic.Service.Model.Entities.BankDto;

namespace Questify.Builder.Logic.Service.Decorators
{
    public abstract class BaseBankDtoServiceDecorator : BaseDtoServiceDecorator<BankDto, int>, IBankDtoRepository
    {
        public BaseBankDtoServiceDecorator(IBankDtoRepository decoree)
            : base(decoree) { }

        public virtual IEnumerable<BankDto> All()
        {
            var list = ((IBankDtoRepository)Decoree).All();
            return list;
        }

        public override void EntityChanged(int key)
        {
            ((CachePermissionService)PermissionFactory.Instance).RemovePermissionFromCache(key);
            ((CacheSecurityService)SecurityFactory.Instance).RemoveSecurityFromCache(key);

            base.EntityChanged(key);
        }

        public virtual IEnumerable<BankDto> GetBankAndParents(int id)
        {
            var list = ((IBankDtoRepository)Decoree).GetBankAndParents(id);
            return list;
        }
    }
}