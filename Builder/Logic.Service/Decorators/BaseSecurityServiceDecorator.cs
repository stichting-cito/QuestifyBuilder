using System;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Decorators
{

    public abstract class BaseSecurityServiceDecorator : ISecurityService
    {

        private ISecurityService decoree;

        public BaseSecurityServiceDecorator(ISecurityService decoree)
        {
            this.decoree = decoree;
        }

        public virtual AuthenticationResult Authenticate(string username, string password, string type)
        {
            return decoree.Authenticate(username, password, type);
        }

        public virtual bool IsAuthenticated()
        {
            return decoree.IsAuthenticated();
        }

        public virtual void Signout()
        {
            decoree.Signout();
        }

        public virtual SerializableDictionaryIntegerPermission FetchGrantedPermissions(Int32[] bankIds)
        {
            return decoree.FetchGrantedPermissions(bankIds);
        }

        public virtual bool IsBankAssignedToUserThroughBankRole(Int32 bankId)
        {
            return decoree.IsBankAssignedToUserThroughBankRole(bankId);
        }

    }
}
