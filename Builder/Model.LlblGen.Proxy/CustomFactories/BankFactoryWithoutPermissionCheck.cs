using System;
using System.Diagnostics.CodeAnalysis;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Model.LlblGen.Proxy.CustomFactories
{
    public class BankFactoryWithoutPermissionCheck
    {
        private static IBankService _serviceInstance;
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static IBankService Instance
        {
            get
            {
                if (_serviceInstance == null)
                {
                    throw new Exception("BankService Not Instantiated");
                }

                return _serviceInstance;
            }
        }

        protected BankFactoryWithoutPermissionCheck()
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
                throw new Exception("BankService Already Instantiated");
            }

            return _serviceInstance;
        }

        public static void Destroy()
        {
            _serviceInstance = null;
        }

    }
}