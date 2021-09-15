
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using Cinch;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class V2_Advanced_ScoringViewModelTests : UsesTheItemEditorVM
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(MultiChoiceScoringParameter), Name="Een Naam")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void AConcept_AConceptScoreEditor()
        {
            //Arrange
            var fakeItemEdtrVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.IsTrue(V2Score.ConceptParametersPresent);
        }
        
        protected override IEnumerable<ComposablePartCatalog> GetTypesForInjection()
        {
            return new[] { MyComposer.GetTestTypesForCinch(),
                        MyComposer.GetScoreEditors()};
        }

        internal override void SetFakeViewDataContext(ref IWorkSpaceAware fakeView, IItemEditorViewModel itemEditorViewModel)
        {
            //vanilla option,.. just the itemEditor ViewModel
            fakeView.WorkSpaceContextualData.DataValue  = itemEditorViewModel;
        }
    }
}
