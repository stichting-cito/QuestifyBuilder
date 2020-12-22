
using System;
using System.Diagnostics;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    internal class ScoreFactoryBoilerplate<TFactory, TScoreParam, TScoringViewModel> : IBaseScoreFactoryTest
    where TFactory : IScoringParameterWorkspaceFactory, new()
    where TScoreParam : ScoringParameter, new()
    where TScoringViewModel : ScoringViewModel<TScoreParam>
    {

        public void Can_Handle_SpecificScoringParameterParameter()
        {
            var fact = new TFactory();
            var result = fact.CanHandle(new TScoreParam());
            Assert.IsTrue(result);
        }


        public void CanNot_Handle_someOtherScoreParam()
        {
            var fact = new TFactory();
            var result = fact.CanHandle(new someOtherScoreParam());
            Assert.IsFalse(result);
        }

        public void CanNot_Handle_NULL_ScoringParameter()
        {
            var fact = new TFactory();
            var result = fact.CanHandle(null);
            Assert.Fail();
        }


        public void WorkspaceData_IsUsableByViewModel()
        {
            var fakeVAS = new Cinch.TestViewAwareStatus();

            Debug.Assert(CreateViewModel != null, "Please set this lambda!");

            TScoringViewModel MCVM = CreateViewModel(fakeVAS);
            var fact = new TFactory();
            var workspaceData = fact.Create(new TScoreParam() { ControllerId = "test" }, new Solution());
            var fakeView = A.Fake<Cinch.IWorkSpaceAware>();
            fakeView.WorkSpaceContextualData.DataValue = workspaceData.DataValue;
            fakeVAS.View = fakeView;
            fakeVAS.SimulateViewIsLoadedEvent();
            Assert.IsNotNull(MCVM.ScoreParameter, "Scoring parameter should not be null");
        }

        public Func<Cinch.TestViewAwareStatus, TScoringViewModel> CreateViewModel { get; set; }


        class someOtherScoreParam : ScoringParameter
        {
            public override int? AlternativesCount
            {
                get { return null; }
            }
        }


        Func<Cinch.TestViewAwareStatus, object> IBaseScoreFactoryTest.CreateViewModel
        {
            get
            {
                return CreateViewModel;
            }
            set
            {
                CreateViewModel = cinch => (TScoringViewModel)value(cinch);
            }
        }
    }
}
