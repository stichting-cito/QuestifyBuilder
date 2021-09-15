
using Enums;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Security;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
    [TestClass]
    public class CacheBankServiceTest
    {

        [TestMethod, TestCategory("Cache")]
        public void FetchAllRelationsIsCached()
        {
            //Arrange
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.FetchAllRelations());
            theCall.ReturnsLazily(args => GetAllRelations());

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); //With timeout for 2 second.
            //Act
            var relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //Object should be retrieved from service once. 
        }


        [TestMethod, TestCategory("Cache")]
        public void FetchAllRelationsIsMakedInvalid()
        {
            //Arrange
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.FetchAllRelations());
            theCall.ReturnsLazily(args => GetAllRelations());
            var theUpdateCall = A.CallTo(() => bankService.UpdateBank(A<BankEntity>.Ignored));

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); //With timeout for 2 second.
            //Act
            var relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            bankSrvDecorator.UpdateBank(new BankEntity() { Id = 1, Name = "Bank" });

            relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Twice); //Object should be retrieved from service twice and twice from cache. 
        }

        [TestMethod, TestCategory("Cache")]
        public void GetBankIsCached()
        {
            //Arrange
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.GetBank(A<int>.Ignored));
            theCall.ReturnsLazily(args => GetBank());

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); //With timeout for 2 second.
            //Act
            var fakeBank = GetBank();
            var bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //Object should be retrieved from service twice and twice from cache. 
        }

        [TestMethod, TestCategory("Cache")]
        public void GetCacheBankIsUpdatedWhenUpdatingBank()
        {
            //Arrange
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.GetBank(A<int>.Ignored));
            theCall.ReturnsLazily(args => GetBank());
            var theUpdateCall = A.CallTo(() => bankService.UpdateBank(A<BankEntity>.Ignored));

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); //With timeout for 2 second.
            //Act
            var fakeBank = GetBank();
            var bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bankSrvDecorator.UpdateBank(fakeBank);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);

            //update other bank should not have influence on cache
            bankSrvDecorator.UpdateBank(new BankEntity() { Id = 2, Name = "OtherBank" });
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Once); //Object should be retrieved from service twice and twice once cache. 
        }

        [TestMethod, TestCategory("Cache")]
        public void GetCacheCustomPropertiesIsUpdatedWhenUpdatingBank()
        {
            //Arrange
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.GetCustomBankPropertiesForBranch(A<BankEntity>.Ignored, A<ResourceTypeEnum>.Ignored));
            theCall.ReturnsLazily(args => GetCustomProperties());
            var theUpdateCall = A.CallTo(() => bankService.UpdateBank(A<BankEntity>.Ignored));

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); //With timeout for 2 second.
            //Act
            var fakeBank = GetBank();
            var customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); //service call - 1
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); //cache call
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); //service call - 2
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); //cache call
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); //cache call
            //update bank should clear cache
            bankSrvDecorator.UpdateBank(new BankEntity() { Id = fakeBank.Id, Name = fakeBank.Name });
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); //service call - 3
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); //service call - 4
            //update other bank should have
            bankSrvDecorator.UpdateBank(new BankEntity() { Id = 2, Name = "OtherBank" });
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources);//service call - 5
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); //service call - 6
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources);//cache call
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); //cache call
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Times(6)); 
        }

        /// <summary>
        /// Entities the collection cleared.
        /// </summary>
        [TestMethod, TestCategory("Cache")]
        public void EntityCollectionCleared()
        {
            //Arrange
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.GetCustomBankPropertiesForBranch(A<BankEntity>.Ignored, A<ResourceTypeEnum>.Ignored));
            theCall.ReturnsLazily(args => GetCustomProperties());
            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); //With timeout for 2 second.
            var fakeBank = GetBank();
            //Act
            var customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); //service call - 1
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); //cached call - 1
            customProperties.Clear(); //Clear collection should force a to get a new collection
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); //service call - 2
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); //cached call - 1
            //Assert
            theCall.MustHaveHappened(Repeated.Exactly.Times(2)); //Object should be retrieved from service twice and twice from cache. 
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

        private SerializableDictionaryInteger GetAllRelations()
        {
            return new SerializableDictionaryInteger();
        }

        private BankEntity GetBank()
        {
            return new BankEntity() { Id = 1, Name = "Bank" };
        }

        private EntityCollection GetCustomProperties()
        {
            var collection =  new EntityCollection();
            collection.Add(new CustomBankPropertyEntity());
            return collection;
        }

        #endregion

    }
}
