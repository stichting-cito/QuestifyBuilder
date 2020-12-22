
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{

    [TestClass]
    public abstract class Runner : IBaseScoreFactoryTest
    {
        public IBaseScoreFactoryTest decoree { get; private set; }

        public Runner(IBaseScoreFactoryTest decoree)
        {
            this.decoree = decoree;
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void Can_Handle_SpecificScoringParameterParameter()
        {
            decoree.Can_Handle_SpecificScoringParameterParameter();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanNot_Handle_NULL_ScoringParameter()
        {
            decoree.CanNot_Handle_NULL_ScoringParameter();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void CanNot_Handle_someOtherScoreParam()
        {
            decoree.CanNot_Handle_someOtherScoreParam();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void WorkspaceData_IsUsableByViewModel()
        {
            decoree.WorkspaceData_IsUsableByViewModel();
        }

        public Func<Cinch.TestViewAwareStatus, object> CreateViewModel
        {
            get
            {
                return decoree.CreateViewModel;
            }
            set
            {
                decoree.CreateViewModel = value;
            }
        }

    }
}
