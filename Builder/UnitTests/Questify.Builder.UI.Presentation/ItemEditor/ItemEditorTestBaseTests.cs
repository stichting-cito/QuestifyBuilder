
using System;
using System.Linq;
using MEFedMVVM.ViewModelLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemEditorTestBaseTests : ItemEditorTestBase
    {
        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void Test_ItemEditorTestBase_Services()
        {
            Assert.IsNotNull(FakeBankService);
            Assert.IsNotNull(FakeResourceService);
            Assert.IsNotNull(Fake_Factory);
        }


        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        [ExpectedException(typeof(Exception))]
        public void Test_ItemEditorTestBase_ThrowsException()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void Test_ItemEditorTestBase_ReturnsNull()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result = fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.DefaultObjects)]
        public void Test_ItemEditorTestBase_ReturnsSomething()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result = fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
            Assert.IsNotNull(result);
        }


        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.DefaultObjects, IsOldItem = true)]
        public void Test_ItemEditorTestBase_ReturnsOldItem()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result = fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
            Assert.IsTrue(result.IsTransformedFromV1ToV2);
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.DefaultObjects, IsOldItem = false)]
        public void Test_ItemEditorTestBase_ReturnsCurrentItem()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result = fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
            Assert.IsFalse(result.IsTransformedFromV1ToV2);
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void Test_BankProperties_ValueNotSet_ReturnsNull()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result = fact.GetCustomBankPropertiesForBranch(bankId: 1); Assert.IsFalse(result.Any());
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull, BankProperties = ItemEditorBankObjectStrategy.GiveException)]
        [ExpectedException(typeof(Exception))]
        public void Test_BankProperties_GiveException_ExpectsException()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result = fact.GetCustomBankPropertiesForBranch(bankId: 1);
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull, BankProperties = ItemEditorBankObjectStrategy.ReturnsNull)]
        public void Test_BankProperties_GiveNull_ExpectsNullReturned()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result = fact.GetCustomBankPropertiesForBranch(bankId: 1); Assert.IsFalse(result.Any());
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull, BankProperties = ItemEditorBankObjectStrategy.StaticExample1)]
        public void Test_GetStaticConceptStructure()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result = fact.GetCustomBankPropertiesForBranch(bankId: 1); Assert.IsInstanceOfType(result[0], typeof(ConceptStructureCustomBankPropertyEntity));
        }

        [TestMethod]
        [FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ValidItem_WithConcept, ConceptId = ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1, BankProperties = ItemEditorBankObjectStrategy.StaticExample1)]
        public void Test_GetStaticConceptStructure_AndItemWithMatchingConcept()
        {
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            var result1 = fact.GetRequiredObjectsForItemWithId(new Guid());
            var result2 = fact.GetCustomBankPropertiesForBranch(bankId: 1);
            ConceptStructureCustomBankPropertyValueEntity x = result1.ItemResourceEntity.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().First();
            var concept = result2.First(e => ((CustomBankPropertyEntity)e).CustomBankPropertyId == x.CustomBankPropertyId);
            Assert.IsNotNull(concept, "A concept was expected at this time");
        }





        [TestMethod, FakeServiceHandler(ResourceIsContainedInBank = false)]
        public void ResourceIsContainedInBank_Will_NeverExistsInBank()
        {
            Assert.IsFalse(FakeResourceService.ResourceExists(1, "", true));
        }

        [TestMethod, FakeServiceHandler(ResourceIsContainedInBank = true)]
        public void ResourceIsContainedInBank_Will_AlwaysExsistsInBank()
        {
            Assert.IsTrue(FakeResourceService.ResourceExists(1, "", true));
        }
    }
}
