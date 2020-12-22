using System;
using Questify.Builder.Logic.Service.Domain.AppServices;
using Questify.Builder.Logic.Service.Domain.Repository;
using Questify.Builder.Logic.Service.Messages;

namespace Questify.Builder.Logic.Service.Domain.TransManager
{
    public abstract class TransManagerBase
       : ITransManager
    {
        protected bool IsInTranx;

        protected IRepositoryLocator Locator { get; set; }

        public TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
            where TResult : class, IDtoResponseEnvelop
        {
            try
            {
                BeginTransaction();
                var result = command.Invoke(Locator);
                CommitTransaction();
                CheckForWarnings(result);
                return result;
            }
            catch (BusinessException exception)
            {
                if (IsInTranx)
                {
                    Rollback();
                }
                var type = typeof(TResult);
                var instance = Activator.CreateInstance(type, true) as IDtoResponseEnvelop;
                if (instance != null)
                {
                    instance.Response.AddBusinessException(exception);
                }
                return instance as TResult;
            }
        }

        public virtual void BeginTransaction()
        {
            IsInTranx = true;
        }

        public virtual void CommitTransaction()
        {
            IsInTranx = false;
        }

        public virtual void Rollback()
        {
            IsInTranx = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected bool IsDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }


            if (!IsDisposed && IsInTranx)
            {
                Rollback();
            }
            Locator = null;
            IsDisposed = true;
        }

        ~TransManagerBase()
        {
            Dispose(false);
        }


        private void CheckForWarnings<TResult>(TResult result)
        {
            var response = result as IDtoResponseEnvelop;
            if (response == null)
            {
                return;
            }
            var notifier = Container.RequestContext.Notifier;
            if (notifier.HasWarnings)
            {
                response.Response.AddBusinessWarnings(notifier.RetrieveWarnings());
            }
        }
    }
}
