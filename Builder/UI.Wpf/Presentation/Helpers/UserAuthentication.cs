using System;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Direct;
using Questify.Builder.Security;
using Questify.Builder.Security.ActiveDirectory;

namespace Questify.Builder.UI.Wpf.Presentation.Helpers
{
    internal sealed class UserAuthenticator
    {
        public event EventHandler<AuthenticationLoginEventArgs> CredentialsNeeded;

        private IAuthenticationProvider withEventsField__theAuthenticationProvider;

        private IAuthenticationProvider _theAuthenticationProvider
        {
            get { return withEventsField__theAuthenticationProvider; }
            set
            {
                if (withEventsField__theAuthenticationProvider != null)
                {
                    withEventsField__theAuthenticationProvider.GetLoginCredentials -= myAuthenticationProvider_GetLoginCredentials;
                }
                withEventsField__theAuthenticationProvider = value;
                if (withEventsField__theAuthenticationProvider != null)
                {
                    withEventsField__theAuthenticationProvider.GetLoginCredentials += myAuthenticationProvider_GetLoginCredentials;
                }
            }

        }

        public AuthenticationResult AuthenticateUser(bool reportCalls)
        {
            if (!SecurityFactory.Isinstantiated)
            {
                ISecurityService securityService = new CacheSecurityService(new SecurityService());
                SecurityFactory.Instantiate(securityService);
            }

            if (!PermissionFactory.IsInstantiated)
            {
                IPermissionService permissionService = new CachePermissionService(new PermissionService());
                PermissionFactory.Instantiate(permissionService);
            }

            _theAuthenticationProvider = SecurityFactory.AuthenticationProvider;

            var result = _theAuthenticationProvider.Authenticate();

            return result;
        }


        private void myAuthenticationProvider_GetLoginCredentials(object sender, AuthenticationLoginEventArgs e)
        {
            CredentialsNeeded(sender, e);
        }
    }
}

