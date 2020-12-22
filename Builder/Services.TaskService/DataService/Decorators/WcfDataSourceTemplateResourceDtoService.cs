using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Services.TasksService.DataService.CustomFactories;

namespace Questify.Builder.Services.TasksService.DataService.Decorators
{
    public class WcfDataSourceTemplateResourceDtoService : WcfResourceDtoServiceDecorator<DataSourceResourceDto>, IDataSourceTemplateResourceDtoRepository
    {
        public WcfDataSourceTemplateResourceDtoService()
            : base(ServiceFactory.GetDataSourceTemplateService())
        { }
    }
}