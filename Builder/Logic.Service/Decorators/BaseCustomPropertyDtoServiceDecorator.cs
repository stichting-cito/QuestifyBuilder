using System;
using System.Collections.Generic;
using System.ServiceModel;
using Questify.Builder.Logic.Service.Interfaces;
using CustomBankPropertyDto = Questify.Builder.Logic.Service.Model.Entities.CustomBankPropertyDto;

namespace Questify.Builder.Logic.Service.Decorators
{
    [ServiceBehavior]
    public abstract class BaseCustomPropertyDtoServiceDecorator : BaseDtoServiceDecorator<CustomBankPropertyDto, Guid>, ICustomBankPropertyDtoRepository
    {
        public BaseCustomPropertyDtoServiceDecorator(ICustomBankPropertyDtoRepository decoree)
            : base(decoree)
        {
        }

        public virtual IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranch(int bankId)
        {
            var returnValue = ((ICustomBankPropertyDtoRepository)Decoree).GetCustomBankPropertiesForBranch(bankId);
            return returnValue;
        }

        public virtual IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranchWithFilter(int bankId, string type)
        {
            var returnValue = ((ICustomBankPropertyDtoRepository)Decoree).GetCustomBankPropertiesForBranchWithFilter(bankId, type);
            return returnValue;
        }

        public virtual string GetSelectedValueDisplayValue(Guid selectedValue, int bankId)
        {
            var returnValue = ((ICustomBankPropertyDtoRepository)Decoree).GetSelectedValueDisplayValue(selectedValue, bankId);
            return returnValue;
        }

        public virtual void BankChanged(int bankId)
        {
            ((ICustomBankPropertyDtoRepository)Decoree).BankChanged(bankId);
        }

        public virtual void BanksChanged(IEnumerable<int> bankIds)
        {
            ((ICustomBankPropertyDtoRepository)Decoree).BanksChanged(bankIds);
        }
    }
}
