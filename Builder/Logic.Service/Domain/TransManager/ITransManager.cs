using System;
using Questify.Builder.Logic.Service.Domain.Repository;
using Questify.Builder.Logic.Service.Messages;

namespace Questify.Builder.Logic.Service.Domain.TransManager
{
    public interface ITransManager
     : IDisposable
    {
        TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
            where TResult : class, IDtoResponseEnvelop;

        void BeginTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
