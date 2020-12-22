using System.ServiceModel.Activation;
using Questify.Builder.Services.TasksService.DataService.CustomFactories;
using Questify.Builder.Services.TasksService.DataService.Decorators;

namespace Questify.Builder.Services.TasksService.DataService.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BankDtoService : WcfBankDtoService
    {
        public BankDtoService()
            : base(ServiceFactory.GetBankService())
        { }
    }
}
