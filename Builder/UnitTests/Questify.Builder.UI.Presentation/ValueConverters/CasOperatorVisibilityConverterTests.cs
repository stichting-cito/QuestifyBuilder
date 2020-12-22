using System.Windows;
using System.Windows.Controls;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{
    [TestClass]
    public class CasOperatorVisibilityConverterTests
    {
        [TestMethod]
        public void Convert_NoValues_ReturnsVisibility()
        {
            var converter = new CasOperatorVisibilityConverter();

            var result = converter.Convert(null, typeof(string), null, null);

            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod]
        public void Convert_DoNotSupportCasValue_ReturnsVisible()
        {
            var converter = new CasOperatorVisibilityConverter();
            var scoringParameter = new CasEqualStepsScoringParameter();
            var combobox = new ComboBoxItem { Content = "=" };

            object[] values = { scoringParameter, combobox };

            var result = converter.Convert(values, typeof(string), null, null);

            Assert.AreEqual(Visibility.Visible, result);
        }
    }
}
