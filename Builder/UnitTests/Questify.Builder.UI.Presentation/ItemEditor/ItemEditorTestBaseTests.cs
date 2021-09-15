
using System;
using System.Linq;
using MEFedMVVM.ViewModelLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    /// <summary>
    /// Self Test 4 ItemEditorTestBase
    /// </summary>
    [TestClass]
    public class ItemEditorTestBaseTests : ItemEditorTestBase
    {
        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)] //No extra arguments
        public void Test_ItemEditorTestBase_Services()
        {
            //Base test class should have all services initialized.
            //Assert
            Assert.IsNotNull(FakeBankService);
            Assert.IsNotNull(FakeResourceService);
            Assert.IsNotNull(Fake_Factory);
        }

        //FakeObjectFactoryBehavior
        #region FakeObjectFactoryBehavior

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        [ExpectedException(typeof(Exception))]
        public void Test_ItemEditorTestBase_ThrowsException()
        {
            //Arrange
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            fact.GetRequiredObjectsForItemWithId(Guid.NewGuid()); //Throws
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void Test_ItemEditorTestBase_ReturnsNull()
        {
            //Arrange
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result = fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.DefaultObjects)]
        public void Test_ItemEditorTestBase_ReturnsSomething()
        {
            //Arrange            
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result = fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.DefaultObjects, IsOldItem = true)]
        public void Test_ItemEditorTestBase_ReturnsOldItem()
        {
            //Arrange            
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result = fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
            //Assert
            Assert.IsTrue(result.IsTransformedFromV1ToV2);
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.DefaultObjects, IsOldItem = false)]
        public void Test_ItemEditorTestBase_ReturnsCurrentItem()
        {
            //Arrange            
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result = fact.GetRequiredObjectsForItemWithId(Guid.NewGuid());
            //Assert
            Assert.IsFalse(result.IsTransformedFromV1ToV2);
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void Test_BankProperties_ValueNotSet_ReturnsNull()
        {
            //Arrange            
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result = fact.GetCustomBankPropertiesForBranch(bankId: 1); //Bank is not important here.
            //Assert
            Assert.IsFalse(result.Any());
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull, BankProperties = ItemEditorBankObjectStrategy.GiveException)]
        [ExpectedException(typeof(Exception))]
        public void Test_BankProperties_GiveException_ExpectsException()
        {
            //Arrange            
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result = fact.GetCustomBankPropertiesForBranch(bankId: 1); //Bank is not important here.
            //Assert
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull, BankProperties = ItemEditorBankObjectStrategy.ReturnsNull)]
        public void Test_BankProperties_GiveNull_ExpectsNullReturned()
        {
            //Arrange            
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result = fact.GetCustomBankPropertiesForBranch(bankId: 1); //Bank is not important here.
            //Assert
            Assert.IsFalse(result.Any());
        }

        [TestMethod, FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull, BankProperties = ItemEditorBankObjectStrategy.StaticExample1)]
        public void Test_GetStaticConceptStructure()
        {
            //Arrange            
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result = fact.GetCustomBankPropertiesForBranch(bankId: 1); //Bank is not important here.
            //Assert
            Assert.IsInstanceOfType(result[0], typeof(ConceptStructureCustomBankPropertyEntity));
        }

        [TestMethod]
        [FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ValidItem_WithConcept, ConceptId = ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1, BankProperties = ItemEditorBankObjectStrategy.StaticExample1)]
        public void Test_GetStaticConceptStructure_AndItemWithMatchingConcept()
        {
            //Arrange            
            var fact = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>().Value;
            //Act
            var result1 = fact.GetRequiredObjectsForItemWithId(new Guid());
            var result2 = fact.GetCustomBankPropertiesForBranch(bankId: 1); //Bank is not important here.
            //Assert

            ConceptStructureCustomBankPropertyValueEntity x = result1.ItemResourceEntity.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().First();
            var concept = result2.First(e => ((CustomBankPropertyEntity)e).CustomBankPropertyId == x.CustomBankPropertyId);
            Assert.IsNotNull(concept, "A concept was expected at this time");
        }



        #endregion

        //FakeServiceHandler
        #region FakeServiceHandler

        [TestMethod, FakeServiceHandler(ResourceIsContainedInBank = false)]
        public void ResourceIsContainedInBank_Will_NeverExistsInBank()
        {
            //Assert
            Assert.IsFalse(FakeResourceService.ResourceExists(1, "", true));
        }

        [TestMethod, FakeServiceHandler(ResourceIsContainedInBank = true)]
        public void ResourceIsContainedInBank_Will_AlwaysExsistsInBank()
        {
            //Assert
            Assert.IsTrue(FakeResourceService.ResourceExists(1, "", true));
        }
        #endregion
    }
}
