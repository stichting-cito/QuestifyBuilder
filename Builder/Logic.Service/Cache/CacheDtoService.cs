using Questify.Builder.Logic.Service.Decorators;

namespace Questify.Builder.Logic.Service.Cache
{
    public class CacheDtoService : BaseCacheServiceDecorator
    {
        public CacheDtoService()
    : base(CacheService.Instance)
        {

        }
    }
}
