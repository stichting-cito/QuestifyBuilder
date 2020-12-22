using System;
using System.Linq;

namespace Questify.Builder.Logic.Service.Domain.Repository
{
    public interface IRepository<TEntity>
    {


        TEntity Save(TEntity instance);
        void Update(TEntity instance);
        void Remove(TEntity instance);



        TEntity GetById(Guid id);
        IQueryable<TEntity> FindAll();

    }
}
