
using System.Threading;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Security;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    /// <summary>
    /// Summary description for CacheServiceTest
    /// </summary>
    [TestClass]
    public class CacheServiceTest
    {
        /// <summary>
        /// testing if we can delete securityPermission and permission information from the cache.
        /// </summary>
        [TestMethod]
        public void CanFlushCacheInfo()
        {
            //Arrange

            //PermissionCache
            IPermissionService permissonService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissonService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => true);
            var permissionSrvDecorator = new CachePermissionService(permissonService, 1000, true, false); //with timeout 1000 seconds

            //SecurityCache
            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
            ISecurityService securityService = A.Fake<ISecurityService>();
            var securityCall = A.CallTo(() => securityService.FetchGrantedPermissions(A<int[]>.Ignored));
            //theCall.ReturnsLazily(args => true);
            int[] banks = new int[] { 1, 2, 3, 4, 5 };
            var securitySrvDecorator = new CacheSecurityService(securityService, 2, true, false); //With timeout for 2 second.


            //Act
            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
            //adding permission cache info
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 1);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 2);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 3);
            var theCall2 = A.CallTo(() => permissonService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored));
            theCall2.ReturnsLazily(args => false);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 4);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 5);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 6);


            //Act
            //adding securityCache info
            securitySrvDecorator.FetchGrantedPermissions(banks);

            var cacheService = new CacheDtoService();
            //clear cache info
            cacheService.FlushAllCachePermissionsForCurrentUser();

            //add some more info to the cache
            securitySrvDecorator.FetchGrantedPermissions(banks);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 2);
           
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Times(7));
            securityCall.MustHaveHappened(Repeated.Exactly.Times(2));
        }
    }
}
