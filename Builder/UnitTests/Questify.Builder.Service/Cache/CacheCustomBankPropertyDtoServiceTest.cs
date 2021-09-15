
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
            //Arrange
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankProperty = A.CallTo(() => customPropertyDtoService.Get(A<Guid>.Ignored));

            fakeGetCustomBankProperty.ReturnsLazily(args => GetCustomProperty());
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            //Act
            customBankPropertySrvDecorator.Get(_customPropertyGuid);
            customBankPropertySrvDecorator.Get(_customPropertyGuid);
            customBankPropertySrvDecorator.Get(_customPropertyGuid);
            customBankPropertySrvDecorator.Get(_customPropertyGuid);
            //Assert
            fakeGetCustomBankProperty.MustHaveHappened(Repeated.Exactly.Times(1)); //Object should be retrieved 1 from service  
        }

        [TestMethod, TestCategory("Cache")]
        public void GetCustomBankPropertiesForBranchIsCached()
        {
            //Arrange
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankPropertiesForBranch = A.CallTo(() => customPropertyDtoService.GetCustomBankPropertiesForBranch(A<int>.Ignored));

            fakeGetCustomBankPropertiesForBranch.ReturnsLazily(args => GetCustomPropertyForBankBranch());
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            //Act
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);
            //Assert
            fakeGetCustomBankPropertiesForBranch.MustHaveHappened(Repeated.Exactly.Times(1)); //Object should be retrieved 1 from service  
        }

        [TestMethod, TestCategory("Cache")]
        public void GetCustomBankPropertiesForBranchWithTypeIsCached()
        {
            //Arrange
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankPropertiesForBranchWithFilter = A.CallTo(() => customPropertyDtoService.GetCustomBankPropertiesForBranchWithFilter(A<int>.Ignored, A<string>.Ignored));

            fakeGetCustomBankPropertiesForBranchWithFilter.ReturnsLazily(args => GetCustomPropertyForBankBranch());
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            var type = ResourceTypeEnum.ItemResource.ToString();
            //Act
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);
            //Assert
            fakeGetCustomBankPropertiesForBranchWithFilter.MustHaveHappened(Repeated.Exactly.Times(1)); //Object should be retrieved 1 from service  
        }


        [TestMethod, TestCategory("Cache")]
        public void CustomBankPropertiesForBranchWithFilterIsInvalidated()
        {
            //Arrange
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankPropertiesForBranchWithFilter = A.CallTo(() => customPropertyDtoService.GetCustomBankPropertiesForBranchWithFilter(A<int>.Ignored, A<string>.Ignored));

            fakeGetCustomBankPropertiesForBranchWithFilter.ReturnsLazily(args => GetCustomPropertyForBankBranch((int)args.Arguments[0]));
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            var type = ResourceTypeEnum.ItemResource.ToString();
            //Act
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);//service call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(2, type);//service call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(3, type);//service call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);//cache call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(2, type);//cache call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(3, type);//cache call
            customBankPropertySrvDecorator.BankChanged(2);
             //customproperty with id: _customPropertyGuid contains in list 2 because its is a childBank of 1, 
            //and can potentially be changed, so do a service call, better safe than sorry..)
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(1, type);//service call - see info above line.
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(2, type);//service call bank 2 is invalidated
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranchWithFilter(3, type);//cache call
            //Assert
            fakeGetCustomBankPropertiesForBranchWithFilter.MustHaveHappened(Repeated.Exactly.Times(5)); //Object should be retrieved 5 from service  
        }

        [TestMethod, TestCategory("Cache")]
        public void CustomBankPropertiesForBranchIsInvalidated()
        {
            //Arrange
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankPropertiesForBranch = A.CallTo(() => customPropertyDtoService.GetCustomBankPropertiesForBranch(A<int>.Ignored));

            fakeGetCustomBankPropertiesForBranch.ReturnsLazily(args => GetCustomPropertyForBankBranch((int)args.Arguments[0]));
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            //Act
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);//service call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(2);//service call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(3);//service call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);//cache call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(2);//cache call
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(3);//cache call
            customBankPropertySrvDecorator.BankChanged(2);
            //customproperty with id: _customPropertyGuid contains in list 2 because its is a childBank of 1, 
            //and can potentially be changed, so do a service call, better safe than sorry..)
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(1);//service call - see info above line.
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(2);//service call bank 2 is invalidated
            customBankPropertySrvDecorator.GetCustomBankPropertiesForBranch(3);//cache call
            //Assert
            fakeGetCustomBankPropertiesForBranch.MustHaveHappened(Repeated.Exactly.Times(5)); //Object should be retrieved 5 from service  
        }


        [TestMethod, TestCategory("Cache")]
        public void CustomBankPropertiesGetIsInvalidated()
        {
            //Arrange
            var customPropertyDtoService = A.Fake<ICustomBankPropertyDtoRepository>();
            var fakeGetCustomBankProperty = A.CallTo(() => customPropertyDtoService.Get(A<Guid>.Ignored));

            fakeGetCustomBankProperty.ReturnsLazily(args => GetCustomProperty(_customPropertyGuid, 1));//get customproperty with bankId '1' and 
            var customBankPropertySrvDecorator = new CacheCustomBankPropertyDtoService(customPropertyDtoService);
            //Act
            customBankPropertySrvDecorator.Get(_customPropertyGuid);//service call
            customBankPropertySrvDecorator.BankChanged(1); //invalide all custom properties with bank Id '1'
            customBankPropertySrvDecorator.Get(_customPropertyGuid);//service call
            customBankPropertySrvDecorator.Get(_customPropertyGuid);//cache call
           //Assert
            fakeGetCustomBankProperty.MustHaveHappened(Repeated.Exactly.Times(2)); //Object should be retrieved 2 from service  
        }


        #region Helper Functions

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


        #endregion
}

