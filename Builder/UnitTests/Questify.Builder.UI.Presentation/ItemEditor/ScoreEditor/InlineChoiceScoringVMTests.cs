
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class InlineChoiceScoringVMTests : GenericScoreViewModelTests<InlineChoiceScoringParameter>
    {

        protected override InlineChoiceScoringParameter ScoreParam(string scoreId, params string[] ids)
        {
            var ret = new InlineChoiceScoringParameter() { ControllerId = scoreId };
            ret.Value = new ParameterSetCollection();
            foreach (var id in ids)
            {
                ret.Value.Add(new ParameterCollection() { Id = id });
            }
            return ret;
        }

        internal override IScoringViewModel CreateVM(Cinch.TestViewAwareStatus fakeVas)
        {
            return new InlineChoiceScoringViewModel(fakeVas);
        }


        protected override IScoringParameterWorkspaceFactory CreateFactory()
        {
            return new InlineChoiceScoringVMFactory();
        }

        protected override void SetSomeScore(InlineChoiceScoringParameter scorePrm)
        {
            scorePrm.Value.Add(new ParameterCollection() { Id = "A" });
            scorePrm.GetScoreManipulator(Solution).SetKey("A");
        }


        [TestMethod(), TestCategory("ViewModel")]
        public void CheckValueOfConstructedName()
        {
            InlineChoiceScoringParameter isp = new InlineChoiceScoringParameter() { Label = "thelabel", Name = "thename", ControllerId = "thecid", InlineId = "theiid", MinChoices = 1, MaxChoices = 1 };

            Solution solution = new Solution();
            KeyFinding keyFinding1 = new KeyFinding("kfId");
            KeyManipulator manipulator_1 = new KeyManipulator(keyFinding1);
            IChoiceScoringManipulator icsm = new ChoiceScoringManipulator(manipulator_1, isp);


            InlineChoiceBlockRowViewModel icvm = new InlineChoiceBlockRowViewModel(isp, icsm);

            string name = icvm.Name;

            isp.Label = null;
            string name2 = icvm.Name;

            isp.Name = null;
            string name3 = icvm.Name;

            isp.InlineId = null;
            string name4 = icvm.Name;

            isp.ControllerId = null;
            string name5 = icvm.Name;

            Assert.AreEqual("thelabel", name);
            Assert.AreEqual("thename", name2);
            Assert.AreEqual("theiid", name3);
            Assert.AreEqual("thecid", name4);
            Assert.AreEqual(null, name5);
        }
    }
}