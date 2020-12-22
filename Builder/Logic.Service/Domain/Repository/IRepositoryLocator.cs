using System;
using System.Linq;

namespace Questify.Builder.Logic.Service.Domain.Repository
{
    public interface IRepositoryLocator
    {

        TEntity Save<TEntity>(TEntity instance) where TEntity : class;
        void Update<TEntity>(TEntity instance) where TEntity : class;
        void Remove<TEntity>(TEntity instance) where TEntity : class;



        TEntity GetById<TEntity>(Guid id) where TEntity : class;
        IQueryable<TEntity> FindAll<TEntity>() where TEntity : class;


        IRepository<T> GetRepository<T>() where T : class;
        void FlushModifications();

    }
}
