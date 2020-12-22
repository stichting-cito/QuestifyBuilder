using Questify.Builder.Logic.Service.Exceptions;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Logic.Service.Factories
{
    public abstract class AuthorizationFactory
    {
        private static IAuthorizationService _serviceInstance;

        public static IAuthorizationService Instance
        {
            get
            {
                if (_serviceInstance == null)
                {

                    throw new ServiceException($"AuthorizationService {Service.Properties.Resources.ResourceManager.GetString("FactoryBase_NotInstantiated")}");
                }

                return _serviceInstance;
            }
        }

        private AuthorizationFactory()
        {
        }

        public static IAuthorizationService Instantiate(IAuthorizationService service)
        {
            if (_serviceInstance == null)
            {
                _serviceInstance = service;
            }
            else
            {
                throw new ServiceException($"AuthorizationService {Properties.Resources.FactoryBase_AlreadyInstantiated}");
            }

            return _serviceInstance;
        }

        public static void Destroy()
        {
            _serviceInstance = null;
        }
    }
}