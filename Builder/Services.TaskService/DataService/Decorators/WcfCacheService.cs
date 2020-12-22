using Questify.Builder.Logic.Service.Decorators;
using Questify.Builder.Services.TasksService.DataService.CustomFactories;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfCacheService : BaseCacheServiceDecorator
    {
        public WcfCacheService()
    : base(ServiceFactory.GetCacheService())
        {
        }
    }
}