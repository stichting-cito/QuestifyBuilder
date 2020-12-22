
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Security;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    [TestClass]
    public class CacheSecurityServiceTest
    {
        [TestMethod]
        public void FetchGrantedPermissionsIsCached()
        {
            System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
            ISecurityService securityService = A.Fake<ISecurityService>();
            var theCall = A.CallTo(() => securityService.FetchGrantedPermissions(A<int[]>.Ignored));
            theCall.ReturnsLazily(args => GetPermissions());

            var securitySrvDecorator = new CacheSecurityService(securityService, 2, true, false);
            int[] banks = new int[] { 1, 2, 3, 4, 5 };
            var permissions = securitySrvDecorator.FetchGrantedPermissions(banks); permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }


        private SerializableDictionaryIntegerPermission GetPermissions()
        {
            return new SerializableDictionaryIntegerPermission();
        }
    }
}
