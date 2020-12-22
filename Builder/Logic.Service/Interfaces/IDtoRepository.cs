using System.Collections.Generic;
using System.ServiceModel;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface IDtoRepository<TEntity, in TKey> where TEntity : class
    {
        [OperationContract]
        TEntity Get(TKey id);
        [OperationContract]
        IEnumerable<TEntity> GetMulti(IEnumerable<TKey> keys);
        [OperationContract]
        void Save(TEntity entity);

        [OperationContract]
        void DeleteEntity(TKey key);

        [OperationContract]
        void DeleteEntities(IEnumerable<TKey> keys);

        [OperationContract]
        void EntityChanged(TKey key);

        [OperationContract]
        void EntitiesChanged(IEnumerable<TKey> resourceGuidList);
    }
}
