using System.Diagnostics.CodeAnalysis;
using Questify.Builder.Logic.Service.Exceptions;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Logic.Service.Factories
{
    public class BankFactory
    {
        private static IBankService _serviceInstance;
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static IBankService Instance
        {
            get
            {
                if (_serviceInstance == null)
                {
                    throw new ServiceException(string.Format("BankService {0}", Properties.Resources.FactoryBase_NotInstantiated));
                }

                return _serviceInstance;
            }
        }

        protected BankFactory()
        {
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static IBankService Instantiate(IBankService service)
        {
            if (_serviceInstance == null)
            {
                _serviceInstance = service;
            }
            else
            {
                throw new ServiceException(string.Format("BankService {0}", Properties.Resources.FactoryBase_AlreadyInstantiated));
            }

            return _serviceInstance;
        }

        public static void Destroy()
        {
            _serviceInstance = null;
        }

    }
}
