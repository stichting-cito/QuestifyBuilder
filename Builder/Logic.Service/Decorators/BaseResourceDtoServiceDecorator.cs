using System;
using System.Collections.Generic;
using Questify.Builder.Logic.Service.Interfaces;
using ResourceDto = Questify.Builder.Logic.Service.Model.Entities.ResourceDto;

namespace Questify.Builder.Logic.Service.Decorators
{
    public class BaseResourceDtoServiceDecorator<TResource> : BaseDtoServiceDecorator<TResource, Guid>, IResourceDtoRepository<TResource>
    where TResource : ResourceDto
    {

        public BaseResourceDtoServiceDecorator(IResourceDtoRepository<TResource> decoree)
            : base(decoree)
        {
        }

        public virtual IEnumerable<TResource> GetResourcesForBank(int id)
        {
            var returnValue = ((IResourceDtoRepository<TResource>)Decoree).GetResourcesForBank(id);
            return returnValue;
        }

        public virtual IEnumerable<ResourceDto> GetDependencies(Guid id)
        {
            var returnValue = ((IResourceDtoRepository<TResource>)Decoree).GetDependencies(id);
            return returnValue;
        }

        public virtual TResource Get(int bankId, string name)
        {
            var returnValue = ((IResourceDtoRepository<TResource>)Decoree).Get(bankId, name);
            return returnValue;
        }

        public virtual void BankChanged(int bankId)
        {
            ((IResourceDtoRepository<TResource>)Decoree).BankChanged(bankId);
        }

    }
}
