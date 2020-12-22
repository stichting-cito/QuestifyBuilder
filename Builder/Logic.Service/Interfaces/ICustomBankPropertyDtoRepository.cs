using System;
using System.Collections.Generic;
using System.ServiceModel;
using CustomBankPropertyDto = Questify.Builder.Logic.Service.Model.Entities.CustomBankPropertyDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface ICustomBankPropertyDtoRepository : IDtoRepository<CustomBankPropertyDto, Guid>
    {
        [OperationContract]
        IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranch(int bankId);
        [OperationContract]
        IEnumerable<CustomBankPropertyDto> GetCustomBankPropertiesForBranchWithFilter(int bankId, string type);
        [OperationContract]
        string GetSelectedValueDisplayValue(Guid selectedValue, int bankId);
        [OperationContract(Name = "BankEntityChanged")]
        void BankChanged(int bankId);[OperationContract(Name = "EntitiesChangedChanged")]
        void BanksChanged(IEnumerable<int> bankIds);
    }
}
