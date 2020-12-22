using System;
using System.Collections.Generic;
using System.Threading;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Logic.Service.Decorators
{
    public abstract class BaseDtoServiceDecorator<TEntity, TKey> : IDtoRepository<TEntity, TKey>, IDisposable
    where TEntity : class
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private readonly IDtoRepository<TEntity, TKey> _decoree;

        public virtual IDtoRepository<TEntity, TKey> Decoree
        {
            get { return _decoree; }
        }

        public virtual ReaderWriterLockSlim CacheLock
        {
            get { return _cacheLock; }
        }

        public BaseDtoServiceDecorator(IDtoRepository<TEntity, TKey> decoree)
        {
            _decoree = decoree;
        }

        public virtual TEntity Get(TKey id)
        {
            var returnValue = Decoree.Get(id);
            return returnValue;
        }

        public IEnumerable<TEntity> GetMulti(IEnumerable<TKey> keys)
        {
            var returnValue = Decoree.GetMulti(keys);
            return returnValue;
        }

        public virtual void Save(TEntity entity)
        {
            Decoree.Save(entity);
        }

        public virtual void DeleteEntity(TKey key)
        {
            Decoree.DeleteEntity(key);
        }

        public virtual void DeleteEntities(IEnumerable<TKey> keys)
        {
            Decoree.DeleteEntities(keys);
        }

        public virtual void EntityChanged(TKey key)
        {
            Decoree.EntityChanged(key);
        }

        public virtual void EntitiesChanged(IEnumerable<TKey> keys)
        {
            Decoree.EntitiesChanged(keys);
        }

        public void Dispose()
        {
            _cacheLock?.Dispose();
        }
    }
}
