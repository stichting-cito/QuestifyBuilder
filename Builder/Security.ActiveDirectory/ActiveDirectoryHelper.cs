using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace Questify.Builder.Security.ActiveDirectory
{
    public static class ActiveDirectoryHelper
    {
        public static bool IsComputerInDomain
        {
            get
            {
                try
                {
                    Domain.GetComputerDomain();
                }
                catch (ActiveDirectoryObjectNotFoundException)
                {
                    return false;
                }
                catch (System.Security.Authentication.AuthenticationException)
                {
                    return false;
                }

                return true;
            }
        }

        public static bool ValidateUser(string domain, string user, string password)
        {
            bool valid;
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, user, password);
            try
            {
                valid = (new DirectorySearcher(entry)).FindOne() != null;
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
