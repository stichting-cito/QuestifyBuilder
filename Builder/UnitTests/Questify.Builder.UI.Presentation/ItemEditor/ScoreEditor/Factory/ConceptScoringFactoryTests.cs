
using System;
using System.Collections.Generic;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class ConceptScoringFactoryTests
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanNot_HandleNull()
        {
            var factory = new ConceptScoringFactory();
            factory.CanHandle(null);
            Assert.Fail("Expected ArgumentNull Exception");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void CanNot_HandleEmptyCollection()
        {
            var factory = new ConceptScoringFactory();
            var result = factory.CanHandle(new List<ScoringParameter>());
            Assert.IsFalse(result);
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void Can_HandleCollection()
        {
            var factory = new ConceptScoringFactory();
            var result = factory.CanHandle(new List<ScoringParameter>(new[] { new MultiChoiceScoringParameter() }));
            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void WorkspaceData_IsUsableByViewModel()
        {
            var fakeVAS = new Cinch.TestViewAwareStatus();
            var ConceptVM = new EncodingScoringViewModel(fakeVAS, A.Fake<IAddAnswerCategory>());
            var fact = new ConceptScoringFactory();
            var workspaceData = fact.Create(new List<ScoringParameter>(new[] { new MultiChoiceScoringParameter() }), new Solution(), A.Fake<IItemEditorViewModel>());
            var fakeView = A.Fake<Cinch.IWorkSpaceAware>();
            fakeView.WorkSpaceContextualData.DataValue = workspaceData.DataValue;
            fakeVAS.View = fakeView;
            fakeVAS.SimulateViewIsLoadedEvent();

        }
    }
}
