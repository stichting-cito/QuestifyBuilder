
using System;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.CustomFactories;
using Questify.Builder.Model.LlblGen.Proxy.Services;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.LlblGen.Proxy.Entities
{
    [TestClass]
    public class AssessmentTestResourceDtoServiceTests
    {
        private Guid _currentGuidAssessment = Guid.Empty;
        [TestInitialize]
        public void Init()
        {
            _currentGuidAssessment = Guid.Empty;
            FakeDal.Init();
            ResourceFactoryWithoutPermissionCheck.Instantiate(FakeDal.FakeServices.FakeResourceService);
            DtoFactory.Instantiate(null, A.Fake<IDataSourceResourceDtoRepository>(), null, null, null, null, null, A.Fake<ICustomBankPropertyDtoRepository>(), null, null, null, null, null, null);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ResourceFactoryWithoutPermissionCheck.Destroy();
            DtoFactory.Destroy();
            FakeDal.Deinit();
        }

        [TestMethod, TestCategory("DtoService")]
        public void GetDataFromGenericService()
        {
            CreateTest();

            //arrange
            var service = new LlblGenAssessmentTestResourceDtoServiceAdapter(
                    new LlblGenResourceDtoService<AssessmentTestResourceDto, AssessmentTestResourceEntity>());
            //Act
            var dependencies = service.GetDependencies(_currentGuidAssessment).ToList() ;
            //Assert
            Assert.AreEqual(1,dependencies.Count);
        }

        private void CreateTest()
        {
            FakeDal.Add.AssessmentTest("Test1", x =>
            {
                x.ResourceId = Guid.NewGuid();
                _currentGuidAssessment = x.ResourceId;
            }).DependsOn.Item("item1");
        }
    }
}
