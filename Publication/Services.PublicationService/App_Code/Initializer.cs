using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Direct;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Security;
using Questify.Builder.Security.ActiveDirectory;

namespace Questify.Builder.Services.PublicationService
{
    public class Initializer
    {
        public static void AppInitialize()
        {
            InitFactories();
        }

        private static void InitFactories()
        {
            PermissionFactory.Instantiate(new CachePermissionService(new PermissionService()));
            SecurityFactory.Instantiate(new CacheSecurityService(new SecurityService()));
            BankFactory.Instantiate(new CacheBankService(new SplitSqlQueryBankServiceDecorator(new BankService())));
            ResourceFactory.Instantiate(new CacheResourceService(new SplitSqlQueryDecorator(new ResourceService())));//bug#24070 
            AuthorizationFactory.Instantiate(new AuthorizationService());            
        }
    }
}