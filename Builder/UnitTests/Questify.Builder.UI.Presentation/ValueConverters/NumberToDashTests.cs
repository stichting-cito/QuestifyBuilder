
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{
    [TestClass]
    public class NumberToDashTests
    {
        [TestMethod, TestCategory("WPF")]
        public void TheNumber_1_results_in_SingleDash()
        {
            var converter = new NumberToDash();
            var result = converter.Convert(1, typeof(string), null, null);
            Assert.AreEqual("-", result);
        }

        [TestMethod, TestCategory("WPF")]
        public void TheNumber_2_results_in_TwoDashes()
        {
            var converter = new NumberToDash();
            var result = converter.Convert(2, typeof(string), null, null);
            Assert.AreEqual("--", result);
        }

        [TestMethod, TestCategory("WPF")]
        public void TheNumber_0_results_in_EmptyString()
        {
            var converter = new NumberToDash();
            var result = converter.Convert(0, typeof(string), null, null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod, TestCategory("WPF")]
        public void SingleDash_IsConvertedInto_1()
        {
            var converter = new NumberToDash();
            var result = converter.ConvertBack("-", typeof(int), null, null);
            Assert.AreEqual(1, result);
        }
    }
}
