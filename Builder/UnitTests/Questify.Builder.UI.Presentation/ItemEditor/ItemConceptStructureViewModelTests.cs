
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using Cinch;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemConceptStructureViewModelTests : UsesTheItemEditorVM
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(IntegerParameter), Name = "Een Naam")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void EnsureThatConceptIsNotDeletedWhenCreatingViewModel()
        {
            //Arrange
            var fakeItemEdtrVm = FakeItemEditorVM;
            var solution = fakeItemEdtrVm.AssessmentItem.DataValue.Solution;
            solution.ConceptFindings.Add(new ConceptFinding("id"));

            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new ItemConceptStructureViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(1, solution.ConceptFindings.Count,"Do just drop concept when loading this viewmodel.");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(IntegerParameter), Name = "Een Naam")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void EnsureThatConceptDeletedWhenPartIsChanged()
        {
            //Arrange
            var fakeItemEdtrVm = FakeItemEditorVM;
            var solution = fakeItemEdtrVm.AssessmentItem.DataValue.Solution;
            solution.ConceptFindings.Add(new ConceptFinding("id"));

            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new ItemConceptStructureViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Act
            Assert.AreNotSame(V2Score.SelectedConceptStructurePart.DataValue , V2Score.ConceptStructureParts[1]); //ref check

            V2Score.SelectedConceptStructurePart.DataValue = V2Score.ConceptStructureParts[1];
            //Assert
            Assert.AreEqual(0, solution.ConceptFindings.Count, "Do just drop concept when loading this viewmodel.");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(IntegerParameter), Name = "Een Naam")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void EnsureThatConceptDeletedWhenConceptIsChanged()
        {
            //Arrange
            var fakeItemEdtrVm = FakeItemEditorVM;
            var solution = fakeItemEdtrVm.AssessmentItem.DataValue.Solution;
            solution.ConceptFindings.Add(new ConceptFinding("id"));

            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new ItemConceptStructureViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Act
            Assert.AreNotSame(V2Score.SelectedConceptProperty.DataValue , V2Score.ConceptProperties[0]); //ref check

            V2Score.SelectedConceptProperty.DataValue = V2Score.ConceptProperties[0];
            //Assert
            Assert.AreEqual(0, solution.ConceptFindings.Count, "Do just drop concept when loading this viewmodel.");
        }
        

        protected override IEnumerable<ComposablePartCatalog> GetTypesForInjection()
        {
            return new[] { MyComposer.GetTestTypesForCinch(),
                        MyComposer.GetScoreEditors()};
        }

        internal override void SetFakeViewDataContext(ref IWorkSpaceAware fakeView, IItemEditorViewModel itemEditorViewModel)
        {
            //vanilla option,.. just the itemEditor ViewModel
            fakeView.WorkSpaceContextualData.DataValue = itemEditorViewModel;
        }
    }
}
