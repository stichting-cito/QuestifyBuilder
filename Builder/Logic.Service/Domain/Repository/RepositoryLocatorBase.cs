using System;
using System.Collections.Generic;
using System.Linq;

namespace Questify.Builder.Logic.Service.Domain.Repository
{
    public abstract class RepositoryLocatorBase
        : IRepositoryLocator
    {
        protected Dictionary<Type, object> RepositoryMap = new Dictionary<Type, object>();

        public TEntity Save<TEntity>(TEntity instance) where TEntity : class
        {
            return GetRepository<TEntity>().Save(instance);
        }

        public void Update<TEntity>(TEntity instance) where TEntity : class
        {
            GetRepository<TEntity>().Update(instance);
        }

        public void Remove<TEntity>(TEntity instance) where TEntity : class
        {
            GetRepository<TEntity>().Remove(instance);
        }

        public TEntity GetById<TEntity>(Guid id) where TEntity : class
        {
            return GetRepository<TEntity>().GetById(id);
        }

        public IQueryable<TEntity> FindAll<TEntity>() where TEntity : class
        {
            return GetRepository<TEntity>().FindAll();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (RepositoryMap.Keys.Contains(type)) return RepositoryMap[type] as IRepository<TEntity>;
            var repository = CreateRepository<TEntity>();
            RepositoryMap.Add(type, repository);
            return repository;
        }

        public virtual void FlushModifications()
        {
        }

        protected abstract IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
    }
}
