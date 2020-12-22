using Questify.Builder.Logic.Service.Exceptions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Properties;

namespace Questify.Builder.Logic.Service.Factories
{
    public class ResourceFactory
    {
        private static IResourceService _serviceInstance;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public static IResourceService Instance
        {
            get
            {
                if (_serviceInstance == null)
                {
                    throw new ServiceException(string.Format("ResourceService {0}", Resources.FactoryBase_NotInstantiated));
                }

                return _serviceInstance;
            }
        }

        protected ResourceFactory()
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
                throw new ServiceException($"ResourceService {Resources.FactoryBase_AlreadyInstantiated}");
            }

            return _serviceInstance;
        }

        public static void Destroy()
        {
            _serviceInstance = null;
        }

    }
}