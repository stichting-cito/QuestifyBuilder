using System.Collections.Generic;
using System.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class UsesTheItemEditorVMTest : UsesTheItemEditorVM
    {

        [TestMethod]
        [AddParameter(typeof(MultiChoiceScoringParameter))]
        public void AddParameterTest()
        {
            var result = FakeItemEditorVM.ParameterSetCollection.DataValue;
            Assert.AreEqual(1, result.Count); Assert.AreEqual(1, result[0].InnerParameters.Count); Assert.IsInstanceOfType(result[0].InnerParameters[0], typeof(MultiChoiceScoringParameter));
        }

        [TestMethod]
        [AddParameter(typeof(MultiChoiceScoringParameter))]
        [AddParameter(typeof(IntegerParameter))]
        public void Add2ParameterTest()
        {

            var result = FakeItemEditorVM.ParameterSetCollection.DataValue;
            Assert.AreEqual(1, result.Count); Assert.AreEqual(2, result[0].InnerParameters.Count);
            var x = result[0].InnerParameters[0];
            var y = result[0].InnerParameters[1];

            Assert.IsFalse(x == y);
            Assert.IsTrue(x != y);

            Assert.IsTrue((x.GetType() == typeof(MultiChoiceScoringParameter)) || (y.GetType() == typeof(MultiChoiceScoringParameter)));
            Assert.IsTrue((x.GetType() == typeof(IntegerParameter)) || (y.GetType() == typeof(IntegerParameter)));
        }



        [TestMethod]
        [AddConcept(ConceptCreator.SomeConcept)]
        public void With_AddConceptAttribute_CustomBankProperties_ShouldContainConcept()
        {
            var result = FakeItemEditorVM;
            var anyIsConcept = result.CustomBankProperties.Any(e => e is ConceptStructureCustomBankPropertyEntity);
            Assert.IsTrue(anyIsConcept);
        }

        [TestMethod]
        [AddConcept(ConceptCreator.SomeConcept)]
        public void With_AddConceptAttribute_CustomBankProperties_ItemShouldContainConcept()
        {
            var result = FakeItemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.ConceptStructureCustomBankPropertySelectedPartCollection.Count);
        }

        [TestMethod]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void With_AddConceptAttribute_CustomBankProperties_ShouldContainConcept_WithPart()
        {
            var result = FakeItemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<ConceptStructureCustomBankPropertyValueEntity>().FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ConceptStructureCustomBankPropertySelectedPartCollection.Count);
        }

        [TestMethod]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void With_AddConceptAttribute_IsConceptDefinedOnBank_ShouldBeTrue()
        {
            var result = FakeItemEditorVM;
            Assert.IsTrue(result.IsConceptDefinedOnBankBranch);
        }



        [TestMethod]
        [AddTree(TreeCreator.SomeTree1)]
        public void With_AddTreeAttribute_CustomBankProperties_ShouldContainTree()
        {
            var result = FakeItemEditorVM;
            var anyIsTree = result.CustomBankProperties.Any(e => e is TreeStructureCustomBankPropertyEntity);
            Assert.IsTrue(anyIsTree);
        }

        [TestMethod]
        [AddTree(TreeCreator.SomeTree1)]
        public void With_AddTreeAttribute_CustomBankProperties_ItemShouldContainTree()
        {
            var result = FakeItemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.TreeStructureCustomBankPropertySelectedPartCollection.Count);
        }

        [TestMethod]
        [AddTree(TreeCreator.SomeTree1, TreePartId = TreeCreator.SomeTree1_Part1)]
        public void With_AddTreeAttribute_CustomBankProperties_ShouldContainTree_WithPart()
        {
            var result = FakeItemEditorVM.ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.OfType<TreeStructureCustomBankPropertyValueEntity>().FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.TreeStructureCustomBankPropertySelectedPartCollection.Count);
        }


        protected override IEnumerable<System.ComponentModel.Composition.Primitives.ComposablePartCatalog> GetTypesForInjection()
        {
            return new[] { MyComposer.GetTestTypesForCinch(),
                        MyComposer.GetScoreEditors()};
        }

        internal override void SetFakeViewDataContext(ref Cinch.IWorkSpaceAware fakeView, global::Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.IItemEditorViewModel itemEditorViewModel)
        {
            fakeView.WorkSpaceContextualData.DataValue = itemEditorViewModel;
        }
    }
}
