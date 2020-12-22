using System;
using System.Diagnostics;
using System.IdentityModel.Policy;
using System.ServiceModel;
using Questify.Builder.Security;
using Questify.Builder.Security.ActiveDirectory;

namespace Questify.Builder.Services.TasksService
{
    public class QbAuthorizationPolicy : IAuthorizationPolicy
    {
        private SecurityService _security = new SecurityService();

        [DebuggerStepThrough]
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            var index = OperationContext.Current.IncomingMessageHeaders.FindHeader("QbCredentials", "http://www.Questify.eu");
            if (index >= 0)
            {
                var ident = OperationContext.Current.IncomingMessageHeaders.GetHeader<TestBuilderIdentity>("QbCredentials", "http://www.Questify.eu");
                var tbPrincipal = new TestBuilderPrincipal(ident);

                evaluationContext.Properties["SecurityService"] = _security;
                evaluationContext.Properties["Principal"] = tbPrincipal;
                return true;
            }
            evaluationContext.Properties["Principal"] = System.Threading.Thread.CurrentPrincipal;
            return false;
        }

        public System.IdentityModel.Claims.ClaimSet Issuer
        {
            get { throw new NotImplementedException(); }
        }

        public string Id
        {
            get { throw new NotImplementedException(); }
        }
    }
}