
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class GapViewModelTest
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GapViewModel_RemoveItemCommand_WillCallAction()
        {
            bool called = false;
            var gapVm = new GapValueViewModel<string>("key", "value", 1);
            gapVm.DoRemove = e => called = true;
            gapVm.RemoveItem.Execute(null);
            Assert.IsTrue(called);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GapViewModel_RemoveItemCommand_WithoutSettingCallback_WillNotCauseProblems()
        {
            var gapVm = new GapValueViewModel<string>("key", "value", 1);
            gapVm.RemoveItem.Execute(null);
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GapViewModel_SetValueAndComparisonType()
        {
            var gapVm = new GapValueViewModel<int>("key", 5, 1);
            gapVm.Value = 3;
            gapVm.ComparisonType = GapComparisonType.GreaterThan;
            Assert.AreEqual(3, gapVm.GapValue.Value);
            Assert.AreEqual(GapComparisonType.GreaterThan, gapVm.GapValue.Comparison);
        }
    }
}
