
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class EncodingScoreHierarchyPartVmTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void TopLevel_ConceptScoreHierarchyPartVM_ShouldHaveDepth_Minus1()
        {
            //Arrange

            //Act
            var cshPart = new EncodingScoreHierarchyPartVM(null);
            //Assert
            Assert.AreEqual(-1, cshPart.Depth);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void ChildOfTop_ShouldHaveDept_Zero()
        {
            //Arrange
            var cshPart = new EncodingScoreHierarchyPartVM(null);

            //Act
            var cshPart2 = new EncodingScoreHierarchyPartVM(null, cshPart);
            //Assert
            Assert.AreEqual(0, cshPart2.Depth);
        }
    }
}