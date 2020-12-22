
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class MathBlockRowViewModelTests
    {

        [TestMethod, TestCategory("Scoring"), TestCategory("ViewModel"), TestCategory("Scoring")]
        public void WhenChangingTheOperator_ValueShouldClear()
        {
            var sp = new MathScoringParameter() { ControllerId = "math" }.AddSubParameters("A");
            var manipulator = sp.GetScoreManipulator(new Solution());
            var vm = new MathBlockRowViewModel(sp, manipulator, "A", 0);
            vm.Value.DataValue = MathVoorbeeld.ToString();
            vm.ComparisonType.DataValue = GapComparisonType.Evaluate;
            Assert.AreEqual(string.Empty, vm.Value.DataValue);
        }

        private readonly XElement MathVoorbeeld =
            XElement.Parse(
                @"<math  xmlns=""http://www.w3.org/1998/Math/MathML""><apply><power></power><ci>x</ci><cn>2</cn></apply></math>");
    }
}
