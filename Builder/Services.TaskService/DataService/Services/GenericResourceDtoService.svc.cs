using System.ServiceModel.Activation;
using Questify.Builder.Services.TasksService.DataService.CustomFactories;
using Questify.Builder.Services.TasksService.DataService.Decorators;

namespace Questify.Builder.Services.TasksService.DataService.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GenericResourceDtoService : WcfGenericResourceDtoService
    {
        public GenericResourceDtoService()
            : base(ServiceFactory.GetGenericService())
        { }
    }
}
