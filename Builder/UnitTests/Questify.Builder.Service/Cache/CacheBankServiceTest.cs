
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
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.FetchAllRelations());
            theCall.ReturnsLazily(args => GetAllRelations());

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); var relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }


        [TestMethod, TestCategory("Cache")]
        public void FetchAllRelationsIsMakedInvalid()
        {
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.FetchAllRelations());
            theCall.ReturnsLazily(args => GetAllRelations());
            var theUpdateCall = A.CallTo(() => bankService.UpdateBank(A<BankEntity>.Ignored));

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); var relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            bankSrvDecorator.UpdateBank(new BankEntity() { Id = 1, Name = "Bank" });

            relations = bankSrvDecorator.FetchAllRelations();
            relations = bankSrvDecorator.FetchAllRelations();
            theCall.MustHaveHappened(Repeated.Exactly.Twice);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetBankIsCached()
        {
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.GetBank(A<int>.Ignored));
            theCall.ReturnsLazily(args => GetBank());

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); var fakeBank = GetBank();
            var bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetCacheBankIsUpdatedWhenUpdatingBank()
        {
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.GetBank(A<int>.Ignored));
            theCall.ReturnsLazily(args => GetBank());
            var theUpdateCall = A.CallTo(() => bankService.UpdateBank(A<BankEntity>.Ignored));

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); var fakeBank = GetBank();
            var bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bankSrvDecorator.UpdateBank(fakeBank);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);

            bankSrvDecorator.UpdateBank(new BankEntity() { Id = 2, Name = "OtherBank" });
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            bank = bankSrvDecorator.GetBank(fakeBank.Id);
            theCall.MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod, TestCategory("Cache")]
        public void GetCacheCustomPropertiesIsUpdatedWhenUpdatingBank()
        {
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.GetCustomBankPropertiesForBranch(A<BankEntity>.Ignored, A<ResourceTypeEnum>.Ignored));
            theCall.ReturnsLazily(args => GetCustomProperties());
            var theUpdateCall = A.CallTo(() => bankService.UpdateBank(A<BankEntity>.Ignored));

            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); var fakeBank = GetBank();
            var customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); bankSrvDecorator.UpdateBank(new BankEntity() { Id = fakeBank.Id, Name = fakeBank.Name });
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); bankSrvDecorator.UpdateBank(new BankEntity() { Id = 2, Name = "OtherBank" });
            customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AssessmentTestResource); theCall.MustHaveHappened(Repeated.Exactly.Times(6));
        }

        [TestMethod, TestCategory("Cache")]
        public void EntityCollectionCleared()
        {
            IBankService bankService = A.Fake<IBankService>();
            var theCall = A.CallTo(() => bankService.GetCustomBankPropertiesForBranch(A<BankEntity>.Ignored, A<ResourceTypeEnum>.Ignored));
            theCall.ReturnsLazily(args => GetCustomProperties());
            var bankSrvDecorator = new CacheBankService(bankService, 2, false, 2, false, 2, false, false); var fakeBank = GetBank();
            var customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties.Clear(); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); customProperties = bankSrvDecorator.GetCustomBankPropertiesForBranch(fakeBank, ResourceTypeEnum.AllResources); theCall.MustHaveHappened(Repeated.Exactly.Times(2));
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
            var collection = new EntityCollection();
            collection.Add(new CustomBankPropertyEntity());
            return collection;
        }


    }
}
