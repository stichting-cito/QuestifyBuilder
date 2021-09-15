
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
            //Arrange
            System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
            ISecurityService securityService = A.Fake<ISecurityService>();
            var theCall = A.CallTo(() => securityService.FetchGrantedPermissions(A<int[]>.Ignored));
            theCall.ReturnsLazily(args => GetPermissions());

            var securitySrvDecorator = new CacheSecurityService(securityService, 2, true, false); //With timeout for 2 second.

            int[] banks = new int[] { 1, 2, 3, 4, 5 };
            //Act
            var permissions = securitySrvDecorator.FetchGrantedPermissions(banks); //make sure it is cached
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            permissions = securitySrvDecorator.FetchGrantedPermissions(banks);
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //Object should be retrieved from service once. For the seccond call the objects should be returned from cache.
        }


        private SerializableDictionaryIntegerPermission GetPermissions()
        {
            return new SerializableDictionaryIntegerPermission();
        }
    }
}
