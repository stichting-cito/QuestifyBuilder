using System.ServiceModel;

namespace Questify.Builder.Logic.Service.Interfaces
{
    [ServiceContract]
    public interface ICacheService
    {
        [OperationContract]
        void FlushAllCachePermissionsForCurrentUser();
    }
}
