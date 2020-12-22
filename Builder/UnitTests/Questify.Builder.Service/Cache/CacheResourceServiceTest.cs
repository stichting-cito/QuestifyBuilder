
using System;
using System.Collections.Generic;
using System.Threading;
using Cito.Tester.Common;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.FactoryClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Security;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    [TestClass]
    public class CacheResourceServiceTest
    {

        [TestMethod, TestCategory("Cache")]
        public void QuickRepeatedCallWillUseObjectInCache()
        {

            IResourceService resourceService = A.Fake<IResourceService>();
            var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            var theCall2 = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, request));
            theCall2.ReturnsLazily(args => GetData(1, "FakeItemLayoutTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 2, true, 2, 50, true, false); var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request); var il2 = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);

            theCall2.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void RepeatedOutsideCachingTimeBounds_WillNotUseCachedValue()
        {
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall2 = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall2.ReturnsLazily(args => GetData(1, "FakeItemLayoutTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 1, true, 1, 50, true, false); var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(2000); var il2 = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);


            theCall2.MustHaveHappened(Repeated.Exactly.Twice);
        }

        [TestMethod, TestCategory("Cache")]
        public void CacheWillRenewCacheTimeWhenUsed()
        {
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall2 = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall2.ReturnsLazily(args => GetData(1, "FakeItemLayoutTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 4, true, 4, 50, true, false);
            var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);

            theCall2.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void CacheUsedWhenObjectIsNull()
        {
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceData(A<ResourceEntity>.Ignored));
            theCall.ReturnsLazily(args => GetEmptyResourceData());

            var resourceSrvDecorator = new CacheResourceService(resourceService, 2, true, 2, 50, true, false);
            ResourceEntity fakeResourceEntity = new ControlTemplateResourceEntity() { BankId = 1003, Name = "FakeResource" };

            var resourceData = resourceSrvDecorator.GetResourceData(fakeResourceEntity);
            resourceData = resourceSrvDecorator.GetResourceData(fakeResourceEntity);

            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void EditedStyleSheetShouldNotGotFromCache()
        {
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceData(A<ResourceEntity>.Ignored));
            theCall.ReturnsLazily(args => GetEmptyResourceData());
            var theUpdateCall = A.CallTo(() => resourceService.UpdateGenericResource(A<GenericResourceEntity>.Ignored));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 2, true, 2, 50, true, false); var styleSheet = GetStyleSheet();

            var resourceData = resourceSrvDecorator.GetResourceData(styleSheet);
            resourceData = resourceSrvDecorator.GetResourceData(styleSheet); resourceSrvDecorator.UpdateGenericResource(styleSheet);
            resourceData = resourceSrvDecorator.GetResourceData(styleSheet); theCall.MustHaveHappened(Repeated.Exactly.Twice);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByNameWithSameOptions_ShouldGotFromCache()
        {
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData(1, "FakeTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            var request = new ResourceRequestDTO { WithDependencies = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);

            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByNameWithDifferentOptions_ShouldGotFromDatabase()
        {
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData(1, "FakeTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            var request = new ResourceRequestDTO { WithDependencies = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request); Thread.Sleep(500);
            request.WithCustomProperties = true;
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);
            theCall.MustHaveHappened(Repeated.Exactly.Twice);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByIdWithSameOptions_ShouldGotFromCache()
        {
            Guid newGuid = Guid.NewGuid();
            var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, request));
            theCall.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);

            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByIdWithDifferentOptions_ShouldGotFromDatabase()
        {
            Guid newGuid = Guid.NewGuid();
            IResourceService resourceService = A.Fake<IResourceService>();
            var request = new ResourceRequestDTO { WithDependencies = true, WithReferences = true };
            var theCall = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, request));
            theCall.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request); Thread.Sleep(500);
            request.WithCustomProperties = true;
            request.WithUserInfo = true;
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            theCall.MustHaveHappened(Repeated.Exactly.Twice);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByIdWithOptions_ThenWithoutOptions_ShouldGotFromDatabase()
        {
            Guid newGuid = Guid.NewGuid();
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            var request = new ResourceRequestDTO { WithDependencies = true, WithReferences = true };
            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            Thread.Sleep(500);
            request = new ResourceRequestDTO() { WithDependencies = true, WithReferences = true, WithCustomProperties = true, WithState = true, WithUserInfo = true, WithHiddenResources = true };
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            theCall.MustHaveHappened(Repeated.Exactly.Twice);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByIdWithAllOptions_ThenWithoutOptions_ShouldGotFromCache()
        {
            Guid newGuid = Guid.NewGuid();
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            var request = new ResourceRequestDTO
            {
                WithDependencies = true,
                WithReferences = true,
                WithCustomProperties = true,
                WithUserInfo = true,
                WithState = true,
                WithHiddenResources = true
            };
            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, new ResourceRequestDTO());
            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourcesByIdsWithOptionTest()
        {
            Guid newGuid = Guid.Empty;
            Guid newGuid1 = Guid.NewGuid();
            Guid newGuid2 = Guid.NewGuid();
            Guid newGuid3 = Guid.NewGuid();
            IResourceService resourceService = A.Fake<IResourceService>();
            var request = new ResourceRequestDTO { WithDependencies = true };
            var theCall = A.CallTo(() => resourceService.GetResourcesByIdsWithOption(A<List<Guid>>.Ignored, A<IEntityFactory2>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData_Resources(1, new List<Guid> { newGuid1, newGuid2, newGuid3 }));
            var theCall2 = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall2.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            newGuid = newGuid1;
            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid1, request); Thread.Sleep(500);
            newGuid = newGuid2;
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid2, request); Thread.Sleep(500);
            var ils = resourceSrvDecorator.GetResourcesByIdsWithOption(new List<Guid> { newGuid1, newGuid2 }, request); Thread.Sleep(500);
            newGuid = newGuid3;
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid3, new ResourceRequestDTO()); Thread.Sleep(500);
            ils = resourceSrvDecorator.GetResourcesByIdsWithOption(new List<Guid> { newGuid1, newGuid2, newGuid3 }, request); Thread.Sleep(500);
            theCall2.MustHaveHappened(Repeated.Exactly.Times(3)); theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourcesByNamesWithOptionTest()
        {
            string name = string.Empty;
            string name1 = "FakeTemplate1";
            string name2 = "FakeTemplate2";
            string name3 = "FakeTemplate3";
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourcesByNamesWithOption(A<int>.Ignored, A<List<string>>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData_Resources(1, new List<string> { name1, name2, name3 }));
            var theCall2 = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall2.ReturnsLazily(args => GetData(1, name));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            name = name1;
            var request = new ResourceRequestDTO { WithDependencies = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, name1, request); Thread.Sleep(500);
            name = name2;
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, name2, request); Thread.Sleep(500);
            var ils = resourceSrvDecorator.GetResourcesByNamesWithOption(1, new List<string> { name1, name2 }, request); Thread.Sleep(500);
            name = name3;
            request.WithDependencies = false;
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, name3, request); Thread.Sleep(500);
            request.WithDependencies = true;
            ils = resourceSrvDecorator.GetResourcesByNamesWithOption(1, new List<string> { name1, name2, name3 }, request); Thread.Sleep(500);
            theCall2.MustHaveHappened(Repeated.Exactly.Times(3)); theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void GetItemLayoutTemplatesFromListOfResourceIds_ResourceIdsIsNull_ReturnsNull()
        {
            var resourceService = A.Fake<IResourceService>();
            var resourceSrvDecorator = new CacheResourceService(resourceService);

            var result = resourceSrvDecorator.GetItemLayoutTemplatesFromListOfResourceIds(null, true);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetItemLayoutTemplatesFromListOfResourceIds_ResourceIdsIsEmpty_ReturnsNull()
        {
            var resourceService = A.Fake<IResourceService>();
            var resourceSrvDecorator = new CacheResourceService(resourceService);

            var result = resourceSrvDecorator.GetItemLayoutTemplatesFromListOfResourceIds(new List<Guid>(), true);

            Assert.IsNull(result);
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

        private GenericResourceEntity GetStyleSheet()
        {
            return new GenericResourceEntity()
            {
                Title = "StyleSheet",
                Size = 1,
                BankId = 1
            };
        }

        private ResourceEntity GetData(int bankId, string name)
        {
            return new ControlTemplateResourceEntity() { BankId = bankId, Name = name, Title = "ControlTemplate" };
        }

        private ResourceEntity GetData(int bankId, Guid resourceId)
        {
            return new ControlTemplateResourceEntity() { ResourceId = resourceId, BankId = bankId, Name = "FakeTemplate", Title = "ControlTemplate" };
        }

        private EntityCollection GetData_Resources(int bankId, List<string> names)
        {
            var result = new EntityCollection(new ResourceEntityFactory());
            names.ForEach(n => result.Add(new ControlTemplateResourceEntity() { BankId = bankId, Name = n, Title = "ControlTemplate" }));
            return result;
        }

        private EntityCollection GetData_Resources(int bankId, List<Guid> resourceIds)
        {
            var result = new EntityCollection(new ResourceEntityFactory());
            resourceIds.ForEach(r => result.Add(new ControlTemplateResourceEntity() { ResourceId = r, BankId = bankId, Name = string.Format("Fake_{0}", r.ToString()), Title = "ControlTemplate" }));
            return result;
        }

        private ResourceDataEntity GetEmptyResourceData()
        {
            return null;
        }

    }
}
