
using System;
using System.Collections.Generic;
using Enums;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    [TestClass]
    public class CacheCustomBankPropertyDtoServiceTest
    {
        [TestMethod, TestCategory("Cache")]
        public void GetCustomPropertyIsCached()
        {
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankProperty = A.CallTo(() => customPropertyDtoService.Get(A<Guid>.Ignored));

            fakeGetCustomBankProperty.ReturnsLazily(args => GetCustomProperty());
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            customBankPropertySrvDecorator.Get(_customPropertyGuid);
            customBankPropertySrvDecorator.Get(_customPropertyGuid);
            customBankPropertySrvDecorator.Get(_customPropertyGuid);
            customBankPropertySrvDecorator.Get(_customPropertyGuid);
            fakeGetCustomBankProperty.MustHaveHappened(Repeated.Exactly.Times(1));
        }

        [TestMethod, TestCategory("Cache")]
        public void GetCustomBankPropertiesForBranchIsCached()
        {
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankPropertiesForBranch = A.CallTo(() => customPropertyDtoService.GetCustomBankPropertiesForBranch(A<int>.Ignored));

            fakeGetCustomBankPropertiesForBranch.ReturnsLazily(args => GetCustomPropertyForBankBranch());
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);
            fakeGetCustomBankPropertiesForBranch.MustHaveHappened(Repeated.Exactly.Times(1));
        }

        [TestMethod, TestCategory("Cache")]
        public void GetCustomBankPropertiesForBranchWithTypeIsCached()
        {
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankPropertiesForBranchWithFilter = A.CallTo(() => customPropertyDtoService.GetCustomBankPropertiesForBranchWithFilter(A<int>.Ignored, A<string>.Ignored));

            fakeGetCustomBankPropertiesForBranchWithFilter.ReturnsLazily(args => GetCustomPropertyForBankBranch());
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            var type = ResourceTypeEnum.ItemResource.ToString();
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);
            fakeGetCustomBankPropertiesForBranchWithFilter.MustHaveHappened(Repeated.Exactly.Times(1));
        }


        [TestMethod, TestCategory("Cache")]
        public void CustomBankPropertiesForBranchWithFilterIsInvalidated()
        {
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankPropertiesForBranchWithFilter = A.CallTo(() => customPropertyDtoService.GetCustomBankPropertiesForBranchWithFilter(A<int>.Ignored, A<string>.Ignored));

            fakeGetCustomBankPropertiesForBranchWithFilter.ReturnsLazily(args => GetCustomPropertyForBankBranch((int)args.Arguments[0]));
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            var type = ResourceTypeEnum.ItemResource.ToString();
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(2, type); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(3, type); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(2, type); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(3, type); customBankPropertySrvDecorator.BankChanged(2);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(2, type); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(3, type); fakeGetCustomBankPropertiesForBranchWithFilter.MustHaveHappened(Repeated.Exactly.Times(5));
        }

        [TestMethod, TestCategory("Cache")]
        public void CustomBankPropertiesForBranchIsInvalidated()
        {
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankPropertiesForBranch = A.CallTo(() => customPropertyDtoService.GetCustomBankPropertiesForBranch(A<int>.Ignored));

            fakeGetCustomBankPropertiesForBranch.ReturnsLazily(args => GetCustomPropertyForBankBranch((int)args.Arguments[0]));
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(2); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(3); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(2); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(3); customBankPropertySrvDecorator.BankChanged(2);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(2); customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(3); fakeGetCustomBankPropertiesForBranch.MustHaveHappened(Repeated.Exactly.Times(5));
        }


        [TestMethod, TestCategory("Cache")]
        public void CustomBankPropertiesGetIsInvalidated()
        {
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankProperty = A.CallTo(() => customPropertyDtoService.Get(A<Guid>.Ignored));

            fakeGetCustomBankProperty.ReturnsLazily(args => GetCustomProperty(_customPropertyGuid, 1)); var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            customBankPropertySrvDecorator.Get(_customPropertyGuid); customBankPropertySrvDecorator.BankChanged(1); customBankPropertySrvDecorator.Get(_customPropertyGuid); customBankPropertySrvDecorator.Get(_customPropertyGuid); fakeGetCustomBankProperty.MustHaveHappened(Repeated.Exactly.Times(2));
        }



        [TestInitialize()]
        public void Init()
        {
            FakeDal.Init();
        }

        [TestCleanup()]
        public void DeInit()
        {
            FakeDal.Deinit();
        }

        private readonly Guid _customPropertyGuid = Guid.NewGuid();

        private CustomBankPropertyDto GetCustomProperty(Guid id, int bankId)
        {
            return new CustomBankPropertyDto
            {
                BankId = bankId,
                CustomBankPropertyId = id,
                Name = "customProperty"
            };
        }

        private CustomBankPropertyDto GetCustomProperty()
        {
            return GetCustomProperty(_customPropertyGuid, 1);
        }

        private IEnumerable<CustomBankPropertyDto> GetCustomPropertyForBankBranch()
        {
            return new List<CustomBankPropertyDto>
            {
               GetCustomProperty(_customPropertyGuid, 1),
               GetCustomProperty(Guid.NewGuid(), 1),
               GetCustomProperty(Guid.NewGuid(), 1),
               GetCustomProperty(Guid.NewGuid(), 2),
                GetCustomProperty(Guid.NewGuid(), 2)
            };
        }

        private IEnumerable<CustomBankPropertyDto> GetCustomPropertyForBankBranch(int bankId)
        {
            switch (bankId)
            {
                case 1:
                    return new List<CustomBankPropertyDto>
                    {
                        GetCustomProperty(Guid.NewGuid(), 1),
                        GetCustomProperty(Guid.NewGuid(), 1),
                        GetCustomProperty(Guid.NewGuid(), 1),
                        GetCustomProperty(_customPropertyGuid, 2),
                        GetCustomProperty(Guid.NewGuid(), 2),
                        GetCustomProperty(Guid.NewGuid(), 3),
                        GetCustomProperty(Guid.NewGuid(), 3)
                    };
                case 2:
                    return new List<CustomBankPropertyDto>
                    {
                        GetCustomProperty(_customPropertyGuid, 2),
                        GetCustomProperty(Guid.NewGuid(), 3),
                        GetCustomProperty(Guid.NewGuid(), 3)
                    };
                default:
                    return new List<CustomBankPropertyDto>
                    {
                        GetCustomProperty(Guid.NewGuid(), bankId),
                        GetCustomProperty(Guid.NewGuid(), bankId),
                        GetCustomProperty(Guid.NewGuid(), bankId)
                    };
            }
        }
    }


}

