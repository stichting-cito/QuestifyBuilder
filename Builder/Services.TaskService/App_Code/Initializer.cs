using Questify.Builder.Security;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Direct;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Security.ActiveDirectory;

namespace Questify.Builder.Services.TasksService
{
    /// <summary>
    /// This initializes the services necessary to access Questify Builder data.
    /// </summary>
    public class Initializer
    {
        public static void AppInitialize()
        {
            InitFactories();
        }

        //[oriS] 27-02-2015 updated bug #23477
        //[oriS] 13-3-2015 updated. added extra SplitSqlQueryDecorator in case we wat to perform 'ItemHarmonization' bug#24070
        private static void InitFactories()
        {
            PermissionFactory.Instantiate(new CachePermissionService(new PermissionService()));
            SecurityFactory.Instantiate(new CacheSecurityService(new SecurityService()));
            BankFactory.Instantiate(new CacheBankService(new SplitSqlQueryBankServiceDecorator(new BankService())));
            BankFactoryWithoutPermissionCheck.Instantiate(new CacheBankService(new SplitSqlQueryBankServiceDecorator(new BankService(new PermissionServiceWithoutPermissionCheck()))));
            ResourceFactory.Instantiate(new CacheResourceService(new SplitSqlQueryDecorator(new ResourceService())));// bug#24070 
            ResourceFactoryWithoutPermissionCheck.Instantiate(new CacheResourceService(new SplitSqlQueryDecorator(new ResourceService(new PermissionServiceWithoutPermissionCheck(), false)))); //bug#23477
            AuthorizationFactory.Instantiate(new AuthorizationService());
        }
    }
}