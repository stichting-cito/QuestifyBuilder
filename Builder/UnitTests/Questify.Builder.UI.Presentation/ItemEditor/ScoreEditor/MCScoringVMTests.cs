
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class MCScoringVMTests : GenericScoreViewModelTests<MultiChoiceScoringParameter>
    {

        protected override MultiChoiceScoringParameter ScoreParam(string scoreId, params string[] ids)
        {
            var ret = new MultiChoiceScoringParameter() { ControllerId = scoreId };
            ret.Value = new ParameterSetCollection();
            foreach (var id in ids)
            {
                ret.Value.Add(new ParameterCollection() { Id = id });
            }
            return ret;
        }


        internal override IScoringViewModel CreateVM(Cinch.TestViewAwareStatus fakeVas)
        {
            return new MCScoringViewModel(fakeVas);
        }


        protected override IScoringParameterWorkspaceFactory CreateFactory()
        {
            return new MCScoringVWFactory();
        }

        protected override void SetSomeScore(MultiChoiceScoringParameter scorePrm)
        {
            scorePrm.Value.Add(new ParameterCollection() { Id = "A" });
            scorePrm.GetScoreManipulator(Solution).SetKey("A");
        }
    }
}
