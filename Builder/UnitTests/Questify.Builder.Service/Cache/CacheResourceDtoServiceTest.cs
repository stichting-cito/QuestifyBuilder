
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.InvalidateCache;
using Questify.Builder.Logic.Service.InvalidateCache.Helper;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Security;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    [TestClass]
    public class CacheResourceDtoServiceTest
    {
        [TestMethod, TestCategory("Cache")]
        public void ListIsCached()
        {
            //Arrange
            var itemDtoService = A.Fake<IResourceDtoRepository<ItemResourceDto>>();
            var theCall = A.CallTo(() => itemDtoService.GetResourcesForBank(1));

            theCall.ReturnsLazily(args => GetItemList());
            var bankSrvDecorator = new CacheResourceDtoService<ItemResourceDto>(itemDtoService);
            //Act
            bankSrvDecorator.GetResourcesForBank(1);
            Thread.Sleep(100); // Give some time to store the resource into the cache
            bankSrvDecorator.GetResourcesForBank(1);
            bankSrvDecorator.GetResourcesForBank(1);
            bankSrvDecorator.GetResourcesForBank(1);
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Times(1)); //Object should be retrieved from service once
        }



        [TestMethod, TestCategory("Cache")]
        public void GetIsCached()
        {
            //Arrange
            var itemDtoService = A.Fake<IResourceDtoRepository<ItemResourceDto>>();
            var theCall = A.CallTo(() => itemDtoService.Get(_resourceId));

            theCall.ReturnsLazily(args => GetItem());
            var bankSrvDecorator = new CacheResourceDtoService<ItemResourceDto>(itemDtoService);
            //Act
            bankSrvDecorator.Get(_resourceId);
            bankSrvDecorator.Get(_resourceId);
            bankSrvDecorator.Get(_resourceId);
            bankSrvDecorator.Get(_resourceId);
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Times(1)); //Object should be retrieved from service once and from cache three times. 
        }

        [TestMethod, TestCategory("Cache")]
        public void CachedListIsInvalidated()
        {
            //Arrange
            var itemDtoService = A.Fake<IResourceDtoRepository<ItemResourceDto>>();
            var fakeGetFullList = A.CallTo(() => itemDtoService.GetResourcesForBank(1));
            var fakeGetMulti = A.CallTo(() => itemDtoService.GetMulti(A<IEnumerable<Guid>>.Ignored));

            fakeGetFullList.ReturnsLazily(args => GetItemList());
            fakeGetMulti.ReturnsLazily(args => GetMulti());

            var resourceSrvDecorator = new CacheResourceDtoService<ItemResourceDto>(itemDtoService);

            //Act
            resourceSrvDecorator.GetResourcesForBank(1);
            Thread.Sleep(250);
            resourceSrvDecorator.EntityChanged(_resourceId);
            var list = resourceSrvDecorator.GetResourcesForBank(1);

            //Assert
            var itemResourceDto = list.FirstOrDefault(r => r.ResourceId == _resourceId);
            Assert.IsTrue(itemResourceDto != null && itemResourceDto.Name == "otherName");
            fakeGetFullList.MustHaveHappened(Repeated.Exactly.Times(1)); //Object should be retrieved from service once
        }

        [TestMethod, TestCategory("Cache")]
        public void CacheIsInvalidatedOnceInBatchProcess()
        {
            try
            {
                var itemDtoService = A.Fake<IItemResourceDtoRepository>();
                var theCall = A.CallTo(() => itemDtoService.Get(_resourceId));
                theCall.ReturnsLazily(args => GetItem());
                var fakeGetFullList = A.CallTo(() => itemDtoService.GetResourcesForBank(1));
                var fakeGetMulti = A.CallTo(() => itemDtoService.GetMulti(A<IEnumerable<Guid>>.Ignored));
                fakeGetFullList.ReturnsLazily(args => GetItemList());
                fakeGetMulti.ReturnsLazily(args => GetMulti());

                var resourceService = A.Fake<IResourceService>();
                var theCall2 = A.CallTo(() => resourceService.UpdateItemResource(A<ItemResourceEntity>.Ignored));
                theCall2.ReturnsLazily(args => ReturnEmpty());
                var resourceSrvDecorator = new InvalidateCacheResourceService(new CacheResourceService(resourceService, 2, true, 2, 50, true, false)); //With timeout for 2 seconds.


                var itemResourceDtoSrvDecorator = new CacheItemResourceDtoService(itemDtoService);
                InitDtoFactory(itemResourceDtoSrvDecorator);
                itemResourceDtoSrvDecorator.GetResourcesForBank(1);
                var itemResource = new ItemResourceEntity { ResourceId = _resourceId };
                //Arrange
                using (new NotifyCacheAfterBatch())
                {
                    //Act
                    resourceSrvDecorator.UpdateItemResource(itemResource);
                    resourceSrvDecorator.UpdateItemResource(itemResource);
                    resourceSrvDecorator.UpdateItemResource(itemResource);
                    resourceSrvDecorator.UpdateItemResource(itemResource);
                    resourceSrvDecorator.UpdateItemResource(itemResource);
                    resourceSrvDecorator.UpdateItemResource(itemResource);
                }
                var list = itemResourceDtoSrvDecorator.GetResourcesForBank(1);
                //Assert
                var itemResourceDto = list.FirstOrDefault(r => r.ResourceId == _resourceId);
                Assert.IsTrue(itemResourceDto != null && itemResourceDto.Name == "otherName");
                fakeGetMulti.MustHaveHappened(Repeated.Exactly.Times(1)); //Object should be retrieved from service once
            }
            finally
            {
                DtoFactory.Destroy();
            }
        }

        [TestMethod, TestCategory("Cache")]
        public void CacheIsInvalidatedOnceNoBatchProcess()
        {
            try
            {
                var itemDtoService = A.Fake<IItemResourceDtoRepository>();
                var theCall = A.CallTo(() => itemDtoService.Get(_resourceId));
                theCall.ReturnsLazily(args => GetItem());
                var fakeGetFullList = A.CallTo(() => itemDtoService.GetResourcesForBank(1));
                var fakeGetMulti = A.CallTo(() => itemDtoService.GetMulti(A<IEnumerable<Guid>>.Ignored));
                fakeGetFullList.ReturnsLazily(args => GetItemList());
                fakeGetMulti.ReturnsLazily(args => GetMulti());

                var resourceService = A.Fake<IResourceService>();
                var theCall2 = A.CallTo(() => resourceService.UpdateItemResource(A<ItemResourceEntity>.Ignored));
                theCall2.ReturnsLazily(args => ReturnEmpty());
                var resourceSrvDecorator = new InvalidateCacheResourceService(new CacheResourceService(resourceService, 2, true, 2, 50, true, false)); //With timeout for 2 seconds.
                
                var itemResourceDtoSrvDecorator = new CacheItemResourceDtoService(itemDtoService);
                InitDtoFactory(itemResourceDtoSrvDecorator);
                itemResourceDtoSrvDecorator.GetResourcesForBank(1);
                var itemResource = new ItemResourceEntity { ResourceId = _resourceId };
                //Arrange

                //Act
                resourceSrvDecorator.UpdateItemResource(itemResource);
                resourceSrvDecorator.UpdateItemResource(itemResource);
                resourceSrvDecorator.UpdateItemResource(itemResource);
                resourceSrvDecorator.UpdateItemResource(itemResource);
                resourceSrvDecorator.UpdateItemResource(itemResource);
                resourceSrvDecorator.UpdateItemResource(itemResource);
                var list = itemResourceDtoSrvDecorator.GetResourcesForBank(1);
                //Assert
                var itemResourceDto = list.FirstOrDefault(r => r.ResourceId == _resourceId);
                Assert.IsTrue(itemResourceDto != null && itemResourceDto.Name == "otherName");
                fakeGetMulti.MustHaveHappened(Repeated.Exactly.Times(6));
            }
            finally
            {
                DtoFactory.Destroy();
            }
        }

        [TestMethod, TestCategory("Cache")]
        public void CachedListIsInvalidatedWhenResourceIsDeleted()
        {
            //Arrange
            var itemDtoService = A.Fake<IResourceDtoRepository<ItemResourceDto>>();
            var fakeGetFullList = A.CallTo(() => itemDtoService.GetResourcesForBank(1));
            var fakeGetMulti = A.CallTo(() => itemDtoService.GetMulti(A<IEnumerable<Guid>>.Ignored));

            fakeGetFullList.ReturnsLazily(args => GetItemList());
            fakeGetMulti.ReturnsLazily(args => GetNone());

            var bankSrvDecorator = new CacheResourceDtoService<ItemResourceDto>(itemDtoService);

            //Act
            bankSrvDecorator.GetResourcesForBank(1);
            Thread.Sleep(250);
            bankSrvDecorator.EntityChanged(_resourceId);
            var list = bankSrvDecorator.GetResourcesForBank(1);

            //Assert
            Assert.IsTrue(!list.Any());//if a resource is deleted it shouldn't be in the list
        }
        
        #region Helper Functions

        private Guid _resourceId = Guid.NewGuid();

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

        private IEnumerable<ItemResourceDto> GetItemList()
        {
            var collection = new List<ItemResourceDto>();
            collection.Add(new ItemResourceDto()
            {
                BankId = 1,
                Name = "some",
                ResourceId = _resourceId
            });
            return collection;
        }

        private IEnumerable<ItemResourceDto> GetMulti()
        {
            var collection = new List<ItemResourceDto>();
            collection.Add(new ItemResourceDto()
            {
                BankId = 1,
                Name = "otherName",
                ResourceId = _resourceId
            });
            return collection;
        }

        private IEnumerable<ItemResourceDto> GetNone()
        {
            return new List<ItemResourceDto>();
        }

        private string ReturnEmpty()
        {
            return String.Empty;
        }
        
        private ItemResourceDto GetItem()
        {
            return new ItemResourceDto()
            {
                BankId = 1,
                Name = "some",
                ResourceId = _resourceId
            };
        }
        
        private void InitDtoFactory(IItemResourceDtoRepository itemDtoService)
        {
            var dataSourceResourceService = A.Fake<IDataSourceResourceDtoRepository>();
            A.CallTo(() => dataSourceResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var testResourceService = A.Fake<ITestResourceDtoRepository>();
            A.CallTo(() => testResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var testPackageResourceService = A.Fake<ITestPackageResourceDtoRepository>();
            A.CallTo(() => testPackageResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var aspectResourceService = A.Fake<IAspectResourceDtoRepository>();
            A.CallTo(() => aspectResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var genericResourceService = A.Fake<IGenericResourceDtoRepository>();
            A.CallTo(() => genericResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var customBankPropertyResourceService = A.Fake<ICustomBankPropertyResourceDtoRepository>();
            A.CallTo(() => customBankPropertyResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var customBankPropertyService = A.Fake<ICustomBankPropertyDtoRepository>();
            A.CallTo(() => customBankPropertyResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var itemLayoutTemplateResourcePropertyService = A.Fake<IItemlayoutTemplateResourceDtoRepository>();
            A.CallTo(() => itemLayoutTemplateResourcePropertyService.Get(_resourceId)).ReturnsLazily(args => null);
            var datasourceTemplateResourceService = A.Fake<IDataSourceTemplateResourceDtoRepository>();
            A.CallTo(() => dataSourceResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var testTemplateResourceService = A.Fake<ITestTemplateResourceDtoRepository>();
            A.CallTo(() => dataSourceResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            var controlTemplateResourceService = A.Fake<IControlTemplateResourceDtoRepository>();
            A.CallTo(() => controlTemplateResourceService.Get(_resourceId)).ReturnsLazily(args => null);
            
            var bankService = A.Fake<IBankDtoRepository>();
            var cacheService = A.Fake<ICacheService>();


            DtoFactory.Instantiate(itemDtoService, dataSourceResourceService, testResourceService, testPackageResourceService,
                                    aspectResourceService, genericResourceService, customBankPropertyResourceService,
                                    customBankPropertyService, itemLayoutTemplateResourcePropertyService,
                                    datasourceTemplateResourceService, testTemplateResourceService, controlTemplateResourceService, bankService, cacheService);
        }

        #endregion
    }
}
