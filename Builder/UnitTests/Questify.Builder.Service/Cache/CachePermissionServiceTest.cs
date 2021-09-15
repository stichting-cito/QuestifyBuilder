
using System;
using System.Threading;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Security;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    [TestClass]
    public class CachePermissionServiceTest
    {
        [TestMethod, TestCategory("Cache")]
        public void PermissionIsCached()
        {
            //Arrange
            var permissionService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissionService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => GetPermission());

            var permissionSrvDecorator = new CachePermissionService(permissionService, 2, true, false); //With timeout for 2 second.
            var tpa = TestBuilderPermissionAccess.View;
            var tpt = TestBuilderPermissionTarget.AllTargets;
            var bankId = 1;
            //Act
            var il = permissionSrvDecorator.TryUserIsPermittedTo(tpa, tpt, bankId); //make sure it is cached
            il = permissionSrvDecorator.TryUserIsPermittedTo(tpa, tpt, bankId);
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //Object should be retrieved from service once. For the seccond call the objects should be returned from cache.
        }

        [TestMethod, TestCategory("Cache")]
        public void PermissionIsCached2()
        {
            //Arrange
            var permissonService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissonService.TryUserIsPermittedToNamedTask(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<TestBuilderPermissionNamedTask>.Ignored, A<int>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => GetPermission());

            var permissionSrvDecorator = new CachePermissionService(permissonService, 2, true, false); //With timeout for 2 second.
            var tpa = TestBuilderPermissionAccess.View;
            var tpt = TestBuilderPermissionTarget.AllTargets;
            var tbpt = TestBuilderPermissionNamedTask.RestrictedPackagePublication;
            var entityInstance = 1;
            var bankId = 1;
            //Act
            var il = permissionSrvDecorator.TryUserIsPermittedToNamedTask(tpa, tpt, tbpt, bankId, entityInstance); //make sure it is cached
            il = permissionSrvDecorator.TryUserIsPermittedToNamedTask(tpa, tpt, tbpt, bankId, entityInstance);
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //Object should be retrieved from service once. For the second call the objects should be returned from cache.
        }

        [TestMethod]
        public void RemoveFromCacheTest()
        {
            //Arrange
            var permissonService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissonService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => GetPermission());

            var permissionSrvDecorator = new CachePermissionService(permissonService, 2, true, false); //With timeout for 2 second.
            var tpa = TestBuilderPermissionAccess.View;
            var tpt = TestBuilderPermissionTarget.AllTargets;
            var bankId = 1;

            //Act
            var il = permissionSrvDecorator.TryUserIsPermittedTo(tpa, tpt, bankId); //make sure it is cached
            permissionSrvDecorator.RemovePermissionFromCache(bankId);
            il = permissionSrvDecorator.TryUserIsPermittedTo(tpa, tpt, bankId);

            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //Object should be retrieved from service twice because the cache is cleared for bankId.
        }

        [TestMethod]
        public void CanRemoveAllUserCachePermissions()
        {
            //Arrange
            var permissonService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissonService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => GetPermission());

           
            var permissionSrvDecorator = new CachePermissionService(permissonService, 1000, true, false); //with timeout 1000 seconds
            //Act

            //adding cache the cache extra information for user number 1. (10 times)
            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
            //Bank cache
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 1);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 2);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 3);
            //theCall.ReturnsLazily(args => false);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 4);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 5);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 6);

            //other cache
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, 20);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, 25);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, 37);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, 35);

            //adding to the cache extra information to user number 2.
            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(2, "user001", "default"));
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 1);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 2);

            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));

            permissionSrvDecorator.FlushAllCachePermissionsForCurrentUser();
            //Assert
            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(2, "user001", "default"));
            //adding an existing object to the cache. here we expect the code to return the information from the cache and not to make a call to "TryUserIsPermittedTo".
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 2);
            //in total we added 13 objects to the cache. the last action no extra call to the service is nedded. therefor 12 calles to the "TryUserIsPermittedTo"
            theCall.MustHaveHappened(Repeated.Exactly.Times(12));
        }

        #region Helper Functions



        [TestInitialize()]
        public void Init()
        {
            System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
            FakeDal.Init();
        }

        [TestCleanup()]
        public void DeInit()
        {
            FakeDal.Deinit();
        }

        void il_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private BankEntity GetFakeBank()
        {
            return new BankEntity
            {
                Id = 1003,
                Name = "FakeBank",
            };
        }

        private bool GetPermission()
        {
            return true;
        }

        private ResourceDataEntity GetEmptyResourceData()
        {
            return null;
        }

        #endregion
    }
}
