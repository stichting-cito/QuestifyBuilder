using System;
using System.Collections.Generic;
using System.ServiceModel;
using ResourceDto = Questify.Builder.Logic.Service.Model.Entities.ResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IResourceDtoRepository<TResource> : IDtoRepository<TResource, Guid>
        where TResource : ResourceDto
    {
        [OperationContract]
        IEnumerable<TResource> GetResourcesForBank(int bankId);

        [OperationContract]
        IEnumerable<ResourceDto> GetDependencies(Guid id);

        [OperationContract(Name = "GetByName")]
        TResource Get(int bankId, string name);

        [OperationContract]
        void BankChanged(int bankId);
    }
}
