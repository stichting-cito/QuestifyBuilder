using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Services.TasksService.DataService.CustomFactories;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfTestPackageResourceDtoService : WcfResourceDtoServiceDecorator<TestPackageResourceDto>, ITestPackageResourceDtoRepository
    {
        public WcfTestPackageResourceDtoService()
            : base(ServiceFactory.GetTestPackageService())
        { }

    }
}