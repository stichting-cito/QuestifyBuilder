
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class V2ScoringViewModelTests : UsesTheItemEditorVM
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(MultiChoiceScoringParameter))]
        public void OneScoringParameter_One_ScoreEditor()
        {
            var fakeItemEdtrVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(1, V2Score.KeyDefinitionScoreViewModels.Sum(k => k.ScoreEditorsViews.Count));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(MultiChoiceScoringParameter))]
        [AddParameter(typeof(MultiChoiceScoringParameter))]
        public void TwoScoringParameter_One_ScoreEditor()
        {
            var fakeItemEdtrVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(2, V2Score.KeyDefinitionScoreViewModels.Sum(k => k.ScoreEditorsViews.Count));
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void ItemHasConcepts_butNO_ScoreParam_NoScoreEditors()
        {
            var fakeItemEdtrVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(0, V2Score.KeyDefinitionScoreViewModels.Sum(k => k.ScoreEditorsViews.Count), "Contains a Conceptscore editor without a score parameter????");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(MultiChoiceScoringParameter))]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void ItemHasConcepts_And_ScoreParam_HasOneScoreEditor_HasOneConceptScoreEditor()
        {
            var fakeItemEdtrVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(1, V2Score.KeyDefinitionScoreViewModels.Sum(k => k.ScoreEditorsViews.Count));
            Assert.AreEqual(1, V2Score.ConceptEditorsViews.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(MultiChoiceScoringParameter))]
        [AddParameter(typeof(MultiChoiceScoringParameter))]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void ItemHasConcepts_And_2ScoreParam_HasTwoScoreEditor_HasOneConceptScoreEditor()
        {
            var fakeItemEdtrVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(2, V2Score.KeyDefinitionScoreViewModels.Sum(k => k.ScoreEditorsViews.Count));
            Assert.AreEqual(1, V2Score.ConceptEditorsViews.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(StringScoringParameter), ControllerID = "ControllerID")]
        public void StringScoringParamShouldBeAbleToExistAlone()
        {
            var fakeItemEdtrVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent(); Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(XHtmlParameter))]
        [AddParameter(typeof(StringScoringParameter), ControllerID = "ControllerID")]
        public void EnrichingScoreingParametersShouldNotBeak_2()
        {
            var fakeItemEdtrVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent(); Assert.IsTrue(true);
        }


        protected override IEnumerable<ComposablePartCatalog> GetTypesForInjection()
        {
            return new[] { MyComposer.GetTestTypesForCinch(),
                        MyComposer.GetScoreEditors()};
        }

        internal override void SetFakeViewDataContext(ref IWorkSpaceAware fakeView, IItemEditorViewModel itemEditorViewModel)
        {
            fakeView.WorkSpaceContextualData.DataValue = itemEditorViewModel;
        }
    }
}
