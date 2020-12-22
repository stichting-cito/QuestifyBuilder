using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.HelperFunctions;

namespace Questify.Builder.Model.LlblGen.Proxy.Services
{
    public class LlblGenResourceDtoService<TResourceDto, TResource> : IResourceDtoRepository<TResourceDto>, IDisposable
    where TResource : ResourceEntity
    where TResourceDto : ResourceDto
    {

        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private bool _disposed;


        public virtual ReaderWriterLockSlim CacheLock
        {
            get { return _cacheLock; }
        }

        public virtual TResourceDto Get(Guid id)
        {
            var request = new ResourceRequestDTO
            {
                WithDependencies = true,
                WithReferences = false,
                WithCustomProperties = true,
                WithUserInfo = true,
                WithState = true,
                WithHiddenResources = false
            };
            var resource = (TResource)ResourceFactoryWithoutPermissionCheck.Instance.GetResourceByIdWithOption(id, request);
            return resource.ConvertResourceEntityToDto<TResource, TResourceDto>();
        }

        public virtual TResourceDto Get(int bankId, string name)
        {
            var request = new ResourceRequestDTO
            {
                WithDependencies = true,
                WithCustomProperties = true,
            };
            var resource = (TResource)ResourceFactoryWithoutPermissionCheck.Instance.GetResourceByNameWithOption(bankId, name, request);
            return resource.ConvertResourceEntityToDto<TResource, TResourceDto>();
        }

        public virtual IEnumerable<TResourceDto> GetMulti(IEnumerable<Guid> ids)
        {
            var request = new ResourceRequestDTO
            {
                WithDependencies = true,
                WithReferences = false,
                WithCustomProperties = true,
                WithUserInfo = true,
                WithState = true,
                WithHiddenResources = false
            };
            var resources = ResourceFactoryWithoutPermissionCheck.Instance.GetResourcesByIdsWithOption(ids.ToList(), request).OfType<TResource>();
            return resources.Select(resource => resource.ConvertResourceEntityToDto<TResource, TResourceDto>());
        }

        public virtual IEnumerable<ResourceDto> GetDependencies(Guid id)
        {
            var request = new ResourceRequestDTO
            {
                WithDependencies = true,
            };
            var resource = ResourceFactoryWithoutPermissionCheck.Instance.GetResourceByIdWithOption(id, request);
            if (resource == null)
            {
                return null;
            }
            var dependentResources = ResourceFactoryWithoutPermissionCheck.Instance.GetDependenciesForResource(resource);
            return dependentResources.Select(r => ((ResourceEntity)r).ConvertResourceEntityToResourceDto()).ToList();
        }



        public virtual IEnumerable<TResourceDto> GetResourcesForBank(int bankId)
        {
#if DEBUG
            Debug.Assert(false);
#endif
            var resources =
                ResourceFactoryWithoutPermissionCheck.Instance.GetResourcesForBank(bankId).OfType<TResource>();
            return resources.Select(resource => resource.ConvertResourceEntityToDto<TResource, TResourceDto>());
        }




        public virtual void Save(TResourceDto entity)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteEntity(Guid key)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteEntities(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }



        public virtual void BankChanged(int bankId)
        {
        }

        public virtual void EntityChanged(Guid key)
        {
        }

        public virtual void EntitiesChanged(IEnumerable<Guid> keys)
        {
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _cacheLock?.Dispose();
            }

            _disposed = true;
        }
    }
}
