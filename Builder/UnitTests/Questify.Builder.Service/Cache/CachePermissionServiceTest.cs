
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
            var permissionService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissionService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => GetPermission());

            var permissionSrvDecorator = new CachePermissionService(permissionService, 2, true, false); var tpa = TestBuilderPermissionAccess.View;
            var tpt = TestBuilderPermissionTarget.AllTargets;
            var bankId = 1;
            var il = permissionSrvDecorator.TryUserIsPermittedTo(tpa, tpt, bankId); il = permissionSrvDecorator.TryUserIsPermittedTo(tpa, tpt, bankId);
            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void PermissionIsCached2()
        {
            var permissonService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissonService.TryUserIsPermittedToNamedTask(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<TestBuilderPermissionNamedTask>.Ignored, A<int>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => GetPermission());

            var permissionSrvDecorator = new CachePermissionService(permissonService, 2, true, false); var tpa = TestBuilderPermissionAccess.View;
            var tpt = TestBuilderPermissionTarget.AllTargets;
            var tbpt = TestBuilderPermissionNamedTask.RestrictedPackagePublication;
            var entityInstance = 1;
            var bankId = 1;
            var il = permissionSrvDecorator.TryUserIsPermittedToNamedTask(tpa, tpt, tbpt, bankId, entityInstance); il = permissionSrvDecorator.TryUserIsPermittedToNamedTask(tpa, tpt, tbpt, bankId, entityInstance);
            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void RemoveFromCacheTest()
        {
            var permissonService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissonService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => GetPermission());

            var permissionSrvDecorator = new CachePermissionService(permissonService, 2, true, false); var tpa = TestBuilderPermissionAccess.View;
            var tpt = TestBuilderPermissionTarget.AllTargets;
            var bankId = 1;

            var il = permissionSrvDecorator.TryUserIsPermittedTo(tpa, tpt, bankId); permissionSrvDecorator.RemovePermissionFromCache(bankId);
            il = permissionSrvDecorator.TryUserIsPermittedTo(tpa, tpt, bankId);

            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void CanRemoveAllUserCachePermissions()
        {
            var permissonService = A.Fake<IPermissionService>();
            var theCall = A.CallTo(() => permissonService.TryUserIsPermittedTo(A<TestBuilderPermissionAccess>.Ignored, A<TestBuilderPermissionTarget>.Ignored, A<int>.Ignored));
            theCall.ReturnsLazily(args => GetPermission());


            var permissionSrvDecorator = new CachePermissionService(permissonService, 1000, true, false);
            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 1);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 2);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 3);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 4);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 5);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 6);

            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, 20);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, 25);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, 37);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.AnyTask, TestBuilderPermissionTarget.Any, 35);

            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(2, "user001", "default"));
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 1);
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 2);

            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));

            permissionSrvDecorator.FlushAllCachePermissionsForCurrentUser();
            Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(2, "user001", "default"));
            permissionSrvDecorator.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.BankEntity, 2);
            theCall.MustHaveHappened(Repeated.Exactly.Times(12));
        }




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

    }
}
