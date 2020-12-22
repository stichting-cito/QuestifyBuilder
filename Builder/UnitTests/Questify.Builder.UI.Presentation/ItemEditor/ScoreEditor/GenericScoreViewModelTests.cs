
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Cinch;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.UI.Wpf.Presentation;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public abstract class GenericScoreViewModelTests<TScoreParam>
    where TScoreParam : ScoringParameter
    {
        TestViewAwareStatus _fakeVAS;
        Solution solution;
        IScoringParameterWorkspaceFactory _factory;

        IWorkSpaceAware workspaceAware;

        [TestInitialize]
        public void testInit()
        {
            _fakeVAS = new TestViewAwareStatus();
            solution = new Solution();
            _factory = CreateFactory();
            workspaceAware = A.Fake<IWorkSpaceAware>();
            _fakeVAS.View = workspaceAware;
            if (Application.Current != null) Application.Current.Resources = new ResourceDictionary();
            Bootstrapper.InitLanguageAndResources();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void EmptySolutions_ShouldBeInitialized_WithAScoringMethod_OtherThan_None()
        {
            ScoringViewModel<TScoreParam> vm = (ScoringViewModel<TScoreParam>)CreateVM(fakeVAS);
            var scorePrm = ScoreParam("Controller");
            var workspaceData = Factory.Create(scorePrm, Solution);

            var finding = solution.Findings.FindById(scorePrm.FindingId);
            var jit = new CreateObjectJIT<KeyFinding>(finding, () => solution.GetFindingOrMakeIt(scorePrm.FindingId));

            var findingViewModel = new KeyFindingGroupScoreViewModel(jit);
            findingViewModel.ScoreEditorsViews.Add(workspaceData);

            InitViewModel(workspaceData);

            Assert.AreNotEqual(EnumScoringMethod.None, findingViewModel.SelectedScoringMethod.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void EmptySolutions_ShouldNotBeAlterd()
        {
            ScoringViewModel<TScoreParam> vm = (ScoringViewModel<TScoreParam>)CreateVM(fakeVAS);
            var scorePrm = ScoreParam("Controller");
            var workspaceData = Factory.Create(scorePrm, Solution);

            var finding = solution.Findings.FindById(scorePrm.FindingId);
            var jit = new CreateObjectJIT<KeyFinding>(finding, () => solution.GetFindingOrMakeIt(scorePrm.FindingId));

            var findingViewModel = new KeyFindingGroupScoreViewModel(jit);
            findingViewModel.ScoreEditorsViews.Add(workspaceData);

            InitViewModel(workspaceData);

            Assert.AreEqual(0, Solution.Findings.Count, "No Findings should have been made.");

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void EmptySolutions_SetScoreMethod_FindingHasBeenCreated()
        {
            ScoringViewModel<TScoreParam> vm = (ScoringViewModel<TScoreParam>)CreateVM(fakeVAS);
            var scorePrm = ScoreParam("Controller");
            var workspaceData = Factory.Create(scorePrm, Solution);

            var finding = solution.Findings.FindById(scorePrm.FindingId);
            var jit = new CreateObjectJIT<KeyFinding>(finding, () => solution.GetFindingOrMakeIt(scorePrm.FindingId));

            var findingViewModel = new KeyFindingGroupScoreViewModel(jit);
            findingViewModel.ScoreEditorsViews.Add(workspaceData);

            InitViewModel(workspaceData);
            findingViewModel.SelectedScoringMethod.DataValue = EnumScoringMethod.Dichotomous;

            Assert.AreNotEqual(0, Solution.Findings.Count, "No Findings should have been made.");

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void EmptySolutions_SetScoreMethod2_FindingHasBeenCreated()
        {
            ScoringViewModel<TScoreParam> vm = (ScoringViewModel<TScoreParam>)CreateVM(fakeVAS);
            var scorePrm = ScoreParam("Controller");
            var workspaceData = Factory.Create(scorePrm, Solution);

            var finding = solution.Findings.FindById(scorePrm.FindingId);
            var jit = new CreateObjectJIT<KeyFinding>(finding, () => solution.GetFindingOrMakeIt(scorePrm.FindingId));

            var findingViewModel = new KeyFindingGroupScoreViewModel(jit);
            findingViewModel.ScoreEditorsViews.Add(workspaceData);

            InitViewModel(workspaceData);
            findingViewModel.SelectedScoringMethod.DataValue = EnumScoringMethod.Polytomous;

            Assert.AreNotEqual(0, Solution.Findings.Count, "No Findings should have been made.");

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void EmptySolutions_SetScore_FindingHasBeenCreated()
        {
            ScoringViewModel<TScoreParam> vm = (ScoringViewModel<TScoreParam>)CreateVM(fakeVAS);
            var scorePrm = ScoreParam("Controller");
            var workspaceData = Factory.Create(scorePrm, Solution);

            var finding = solution.Findings.FindById(scorePrm.FindingId);
            var jit = new CreateObjectJIT<KeyFinding>(finding, () => solution.GetFindingOrMakeIt(scorePrm.FindingId));

            var findingViewModel = new KeyFindingGroupScoreViewModel(jit);
            findingViewModel.ScoreEditorsViews.Add(workspaceData);

            InitViewModel(workspaceData);
            SetSomeScore(scorePrm);

            Assert.AreNotEqual(0, Solution.Findings.Count, "No Findings should have been made.");

        }


        internal virtual IScoringViewModel CreateVM(TestViewAwareStatus fakeVas)
        {
            Debug.Assert(false, "Please override this function");
            return null;
        }

        protected abstract TScoreParam ScoreParam(string scoreId, params string[] ids);
        protected abstract IScoringParameterWorkspaceFactory CreateFactory();
        protected abstract void SetSomeScore(TScoreParam scorePrm);

        public Solution Solution
        {
            get { return solution; }
        }

        public TestViewAwareStatus fakeVAS
        {
            get { return _fakeVAS; }
        }

        public IScoringParameterWorkspaceFactory Factory
        {
            get { return _factory; }
        }

        public void InitViewModel(WorkspaceData workspaceData)
        {
            workspaceAware.WorkSpaceContextualData.DataValue = workspaceData.DataValue;
            fakeVAS.SimulateViewIsLoadedEvent();
        }

        public void WriteSolution(string currentState)
        {
            var xml = new XmlSerializer(typeof(Solution));
            Debug.WriteLine(String.Empty);
            Debug.WriteLine(string.Format("WriteSolution for State [{0}]", currentState));
            using (var stream = new StringWriter())
            {
                xml.Serialize(stream, solution);
                Debug.WriteLine(stream.ToString());
            }
        }
    }
}
