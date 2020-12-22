using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Logic.Service.Decorators
{
    public abstract class BaseCacheServiceDecorator : ICacheService
    {
        public ICacheService Decoree { private get; set; }

        public BaseCacheServiceDecorator(ICacheService decoree)
        {
            Decoree = decoree;
        }

        public virtual void FlushAllCachePermissionsForCurrentUser()
        {
            Decoree.FlushAllCachePermissionsForCurrentUser();
        }
    }
}
