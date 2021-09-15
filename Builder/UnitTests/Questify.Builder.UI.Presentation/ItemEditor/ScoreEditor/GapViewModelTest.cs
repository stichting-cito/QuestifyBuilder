
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
            //Arrange
            bool called = false;
            var gapVm = new GapValueViewModel<string>("key", "value", 1);
            gapVm.DoRemove = e => called = true;
            //Act
            gapVm.RemoveItem.Execute(null);
            //Assert
            Assert.IsTrue(called);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GapViewModel_RemoveItemCommand_WithoutSettingCallback_WillNotCauseProblems()
        {
            //Arrange
            var gapVm = new GapValueViewModel<string>("key", "value", 1);
            //Act
            gapVm.RemoveItem.Execute(null);
            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GapViewModel_SetValueAndComparisonType()
        {
            //Arrange
            var gapVm = new GapValueViewModel<int>("key", 5, 1);
            //Act
            gapVm.Value = 3;
            gapVm.ComparisonType = GapComparisonType.GreaterThan;
            //Assert
            Assert.AreEqual(3, gapVm.GapValue.Value);
            Assert.AreEqual(GapComparisonType.GreaterThan, gapVm.GapValue.Comparison);
        }
    }
}
