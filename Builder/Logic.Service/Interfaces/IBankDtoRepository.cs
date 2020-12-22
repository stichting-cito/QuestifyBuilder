using System.Collections.Generic;
using System.ServiceModel;
using BankDto = Questify.Builder.Logic.Service.Model.Entities.BankDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IBankDtoRepository : IDtoRepository<BankDto, int>
    {
        [OperationContract]
        IEnumerable<BankDto> All();

        [OperationContract]
        IEnumerable<BankDto> GetBankAndParents(int id);
    }
}
