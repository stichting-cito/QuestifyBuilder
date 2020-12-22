using System;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Model.LlblGen.Proxy.CustomFactories
{
    public class ResourceFactoryWithoutPermissionCheck
    {
        private static IResourceService _serviceInstance;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static IResourceService Instance
        {
            get
            {
                if (_serviceInstance == null)
                {
                    throw new Exception("ResourceService Not Instantiated");
                }

                return _serviceInstance;
            }
        }

        protected ResourceFactoryWithoutPermissionCheck()
        {
        }

        public static IResourceService Instantiate(IResourceService service)
        {
            if (_serviceInstance == null)
            {
                _serviceInstance = service;
            }
            else
            {
                throw new Exception("ResourceService AlreadyInstantiated");
            }
            return _serviceInstance;
        }

        public static void Destroy()
        {
            _serviceInstance = null;
        }

    }

}