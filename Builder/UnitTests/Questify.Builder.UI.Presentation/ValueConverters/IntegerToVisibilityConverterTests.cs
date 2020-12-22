
using System.Threading;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{
    [TestClass]
    public class IntegerToVisibilityConverterTests
    {

        [TestMethod, TestCategory("ValueConverters")]
        public void GetVisibilityValue()
        {
            var converter = new IntegerToVisibilityConverter();
            var result = converter.Convert(2, typeof(bool), 2, Thread.CurrentThread.CurrentCulture);
            Assert.AreEqual(Visibility.Visible, (Visibility)result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void GetHiddenValue()
        {
            var converter = new IntegerToVisibilityConverter();
            var result = converter.Convert(2, typeof(bool), 1000, Thread.CurrentThread.CurrentCulture);
            Assert.AreEqual(Visibility.Collapsed, (Visibility)result);
        }

    }
}
