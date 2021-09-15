
using System.Globalization;
using System.Threading;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class CurrencyBlockRowViewModelTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void FormattedDisplayValueForCurrency()
        {
            //Arrange
            var solution = new Solution();
            var param = new CurrencyScoringParameter() { FindingOverride = "Opgave", Value = new ParameterSetCollection(), IntegerPartMaxLength = 4, FractionPartMaxLength = 2 };
            param.Value.Add(new ParameterCollection() { Id = "A", });
            //Act
            var manipulator = param.GetScoreManipulator(solution);
            manipulator.ReplaceKeyValueAt("A", 123m, 0);

            var vm = new CurrencyBlockRowViewModel(param, manipulator, "A", 0);

            var sep1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var money = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;

            Assert.AreEqual(money+" 0123" + sep1 + "00", vm.DisplayValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void FormattedDisplayValueForCurrencyWithSpecificCulture()
        {
            //Arrange
            var solution = new Solution();
            var param = new CurrencyScoringParameter() { CurrencyCulture="ja-JP", FindingOverride = "Opgave", Value = new ParameterSetCollection(), IntegerPartMaxLength = 4, FractionPartMaxLength = 2 };
            param.Value.Add(new ParameterCollection() { Id = "A", });
            //Act
            var manipulator = param.GetScoreManipulator(solution);
            manipulator.ReplaceKeyValueAt("A", 123m, 0);

            var vm = new CurrencyBlockRowViewModel(param, manipulator, "A", 0);

            var culture = new CultureInfo("ja-JP");
            var sep1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var money = culture.NumberFormat.CurrencySymbol;

            Assert.AreEqual(money + " 0123" + sep1 + "00", vm.DisplayValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void NonExistingCurrencyCultureShouldFallbackToCurrentCulture()
        {
            //Arrange
            var solution = new Solution();
            var param = new CurrencyScoringParameter() { CurrencyCulture = "ja-JP", FindingOverride = "Opgave", Value = new ParameterSetCollection(), IntegerPartMaxLength = 4, FractionPartMaxLength = 2 };
            param.Value.Add(new ParameterCollection() { Id = "A", });
            //Act
            var manipulator = param.GetScoreManipulator(solution);
            manipulator.ReplaceKeyValueAt("A", 123m, 0);

            var vm = new CurrencyBlockRowViewModel(param, manipulator, "A", 0);

            var culture = new CultureInfo("ja-JP");
            var sep1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var money = culture.NumberFormat.CurrencySymbol;

            Assert.AreEqual(money + " 0123" + sep1 + "00", vm.DisplayValue);
        }
    }
}
