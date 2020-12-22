using System;
using Questify.Builder.Logic.Service.Domain.AppServices;
using Questify.Builder.Logic.Service.Domain.Repository;
using Questify.Builder.Logic.Service.Domain.TransManager;
using Questify.Builder.Logic.Service.Messages;

namespace Questify.Builder.Logic.Service.Domain.Services
{
    public class ServiceBase
    {
        protected TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
            where TResult : class, IDtoResponseEnvelop
        {
            using (ITransManager manager = Container.GlobalContext.TransFactory.CreateManager())
            {
                return manager.ExecuteCommand(command);
            }
        }
    }
}
