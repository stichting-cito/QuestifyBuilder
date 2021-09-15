
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

            //Arrange
            IResourceService resourceService = A.Fake<IResourceService>();
            var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            var theCall2 = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, request));
            theCall2.ReturnsLazily(args => GetData(1, "FakeItemLayoutTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 2, true, 2, 50, true, false); //With timeout for 2 second.
            //Act
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request); //make sure it is cached
            var il2 = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request); //make sure it is cached            

            //Assert

            theCall2.MustHaveHappened(Repeated.Exactly.Once); //Object should be retrieved from service once. For the seccond call the objects should be returned from cache.
        }

        [TestMethod, TestCategory("Cache")]
        public void RepeatedOutsideCachingTimeBounds_WillNotUseCachedValue()
        {
            //Arrange
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall2 = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall2.ReturnsLazily(args => GetData(1, "FakeItemLayoutTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 1, true, 1, 50, true, false); //With timeout for 1 second.
            //Act
            var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);
            Thread.Sleep(2000);//Wait 2 seconds
            var il2 = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeItemLayoutTemplate", request);

            //Assert

            theCall2.MustHaveHappened(Repeated.Exactly.Twice); //Object should have disappeared from cache, and retreived from service.
        }

        [TestMethod, TestCategory("Cache")]
        public void CacheWillRenewCacheTimeWhenUsed()
        {
            //Arrange
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall2 = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall2.ReturnsLazily(args => GetData(1, "FakeItemLayoutTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 4, true, 4, 50, true, false); //With timeout for 2 second.

            //Act
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

            //Assert
            theCall2.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void CacheUsedWhenObjectIsNull()
        {
            //Arrange
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceData(A<ResourceEntity>.Ignored));
            theCall.ReturnsLazily(args => GetEmptyResourceData());

            var resourceSrvDecorator = new CacheResourceService(resourceService, 2, true, 2, 50, true, false); //With timeout for 2 second.

            ResourceEntity fakeResourceEntity = new ControlTemplateResourceEntity() { BankId = 1003, Name = "FakeResource" };

            //Act
            var resourceData = resourceSrvDecorator.GetResourceData(fakeResourceEntity);
            resourceData = resourceSrvDecorator.GetResourceData(fakeResourceEntity);

            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void EditedStyleSheetShouldNotGotFromCache()
        {
            //Arrange
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceData(A<ResourceEntity>.Ignored));
            theCall.ReturnsLazily(args => GetEmptyResourceData());
            var theUpdateCall = A.CallTo(() => resourceService.UpdateGenericResource(A<GenericResourceEntity>.Ignored));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 2, true, 2, 50, true, false); //With timeout for 2 second.
            var styleSheet = GetStyleSheet();
            //Act

            var resourceData = resourceSrvDecorator.GetResourceData(styleSheet);
            resourceData = resourceSrvDecorator.GetResourceData(styleSheet); //without update is should be got from cache
            resourceSrvDecorator.UpdateGenericResource(styleSheet);
            resourceData = resourceSrvDecorator.GetResourceData(styleSheet); //resource is updated so shouldn't got from cache
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Twice); //called 3 times, should be called twice from service once from cache.
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByNameWithSameOptions_ShouldGotFromCache()
        {
            // Arrange
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData(1, "FakeTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            // Act
            var request = new ResourceRequestDTO { WithDependencies = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);

            // Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //called 2 times with same options, should be called once from service, and once from cache.
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByNameWithDifferentOptions_ShouldGotFromDatabase()
        {
            // Arrange
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData(1, "FakeTemplate"));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            // Act
            var request = new ResourceRequestDTO { WithDependencies = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);    // Requested with same options, should be from cache
            Thread.Sleep(500);
            request.WithCustomProperties = true;
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, "FakeTemplate", request);     // Requested more options, should be from service

            // Assert
            theCall.MustHaveHappened(Repeated.Exactly.Twice); //called 3 times with same options, should be called twice from service, and once from cache.
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByIdWithSameOptions_ShouldGotFromCache()
        {
            // Arrange
            Guid newGuid = Guid.NewGuid();
            var request = new ResourceRequestDTO { WithDependencies = true, WithCustomProperties = true };
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, request));
            theCall.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            // Act
            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);

            // Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //called 2 times with same options, should be called once from service, and once from cache.
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByIdWithDifferentOptions_ShouldGotFromDatabase()
        {
            // Arrange
            Guid newGuid = Guid.NewGuid();
            IResourceService resourceService = A.Fake<IResourceService>();
            var request = new ResourceRequestDTO { WithDependencies = true, WithReferences = true };
            var theCall = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, request));
            theCall.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            // Act
            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            Thread.Sleep(500);
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);   // Requested with same options, should be from cache
            Thread.Sleep(500);
            request.WithCustomProperties = true;
            request.WithUserInfo = true;
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);     // Requested more options, should be from service

            // Assert
            theCall.MustHaveHappened(Repeated.Exactly.Twice); //called 3 times with same options, should be called twice from service, and once from cache.
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByIdWithOptions_ThenWithoutOptions_ShouldGotFromDatabase()
        {
            // Arrange
            Guid newGuid = Guid.NewGuid();
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            // Act
            var request = new ResourceRequestDTO { WithDependencies = true, WithReferences = true };
            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);
            Thread.Sleep(500);
            request = new ResourceRequestDTO() { WithDependencies = true, WithReferences = true, WithCustomProperties = true, WithState = true, WithUserInfo = true, WithHiddenResources = true };
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, request);  // This will require all options = True, but call before had only two options True, so should be retrieved from database

            // Assert
            // Called 2 times different options, should be called twice from service, and zero times from cache.
            theCall.MustHaveHappened(Repeated.Exactly.Twice);       // GetResourceByIdWithOption - should be called twice from service
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourceByIdWithAllOptions_ThenWithoutOptions_ShouldGotFromCache()
        {
            // Arrange
            Guid newGuid = Guid.NewGuid();
            IResourceService resourceService = A.Fake<IResourceService>();
            var theCall = A.CallTo(() => resourceService.GetResourceByIdWithOption(A<Guid>.Ignored, A<IEntityFactory2>.Ignored, A<ResourceRequestDTO>.Ignored));
            theCall.ReturnsLazily(args => GetData(1, newGuid));

            var resourceSrvDecorator = new CacheResourceService(resourceService, 10, true, 10, 50, true, false);

            // Act
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
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid, new ResourceRequestDTO());  // This will require all options = False, and call before had all options True, so should be retrieved from cache

            // Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once);       // GetResourceByIdWithOption - should be called once from service, once from cache
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourcesByIdsWithOptionTest()
        {
            // Arrange
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

            // Act
            newGuid = newGuid1;
            var il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid1, request);     // From database
            Thread.Sleep(500);
            newGuid = newGuid2;
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid2, request);         // From database
            Thread.Sleep(500);
            var ils = resourceSrvDecorator.GetResourcesByIdsWithOption(new List<Guid> { newGuid1, newGuid2 }, request);  // Requesting the two resources above in a single call, with the same options, should come from cache
            Thread.Sleep(500);
            newGuid = newGuid3;
            il = resourceSrvDecorator.GetResourceByIdWithOption(newGuid3, new ResourceRequestDTO());         // From database
            Thread.Sleep(500);
            ils = resourceSrvDecorator.GetResourcesByIdsWithOption(new List<Guid> { newGuid1, newGuid2, newGuid3 }, request);  // Requesting the three resources above in a single call, with more options required then in the call above (resource 3), resource 3 should come from database, the other two from cache
            Thread.Sleep(500);
            // Assert
            theCall2.MustHaveHappened(Repeated.Exactly.Times(3));       // GetResourceByIdWithOption - should be called three times from service
            theCall.MustHaveHappened(Repeated.Exactly.Once);    // GetResourcesByIdsWithOption - should be retrieved from cache once, and once from service
        }

        [TestMethod, TestCategory("Cache")]
        public void GetResourcesByNamesWithOptionTest()
        {
            // Arrange
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

            // Act
            name = name1;
            var request = new ResourceRequestDTO { WithDependencies = true };
            var il = resourceSrvDecorator.GetResourceByNameWithOption(1, name1, request);     // From database
            Thread.Sleep(500);
            name = name2;
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, name2, request);       // From database
            Thread.Sleep(500);
            var ils = resourceSrvDecorator.GetResourcesByNamesWithOption(1, new List<string> { name1, name2 }, request);  // Requesting the two resources above in a single call, with the same options, should come from cache
            Thread.Sleep(500);
            name = name3;
            request.WithDependencies = false;
            il = resourceSrvDecorator.GetResourceByNameWithOption(1, name3, request);         // From database
            Thread.Sleep(500);
            request.WithDependencies = true;
            ils = resourceSrvDecorator.GetResourcesByNamesWithOption(1, new List<string> { name1, name2, name3 }, request);  // Requesting the three resources above in a single call, with more options required then in the call above (resource 3), resource 3 should come from database, the other two from cache
            Thread.Sleep(500);
            // Assert
            theCall2.MustHaveHappened(Repeated.Exactly.Times(3));       // GetResourceByNameWithOption - should be called three times from service
            theCall.MustHaveHappened(Repeated.Exactly.Once);    // GetResourcesByNamesWithOption - should be retrieved from cache once, and once from service
        }

        [TestMethod]
        public void GetItemLayoutTemplatesFromListOfResourceIds_ResourceIdsIsNull_ReturnsNull()
        {
            //Arrange
            var resourceService = A.Fake<IResourceService>();
            var resourceSrvDecorator = new CacheResourceService(resourceService);

            //Act
            var result = resourceSrvDecorator.GetItemLayoutTemplatesFromListOfResourceIds(null, true);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetItemLayoutTemplatesFromListOfResourceIds_ResourceIdsIsEmpty_ReturnsNull()
        {
            //Arrange
            var resourceService = A.Fake<IResourceService>();
            var resourceSrvDecorator = new CacheResourceService(resourceService);

            //Act
            var result = resourceSrvDecorator.GetItemLayoutTemplatesFromListOfResourceIds(new List<Guid>(), true);

            //Assert
            Assert.IsNull(result);
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

        #endregion
    }
}
