
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
            //Arrange
            var converter = new NumberToDash();
            //Act
            var result = converter.Convert(1, typeof(string), null, null);
            //Assert
            Assert.AreEqual("-", result);
        }

        [TestMethod, TestCategory("WPF")]
        public void TheNumber_2_results_in_TwoDashes()
        {
            //Arrange
            var converter = new NumberToDash();
            //Act
            var result = converter.Convert(2, typeof(string), null, null);
            //Assert
            Assert.AreEqual("--", result);
        }

        [TestMethod, TestCategory("WPF")]
        public void TheNumber_0_results_in_EmptyString()
        {
            //Arrange
            var converter = new NumberToDash();
            //Act
            var result = converter.Convert(0, typeof(string), null, null);
            //Assert
            Assert.AreEqual(string.Empty , result);
        }

        [TestMethod, TestCategory("WPF")]
        public void SingleDash_IsConvertedInto_1()
        {
            //Arrange
            var converter = new NumberToDash();
            //Act
            var result = converter.ConvertBack("-", typeof(int), null, null);
            //Assert
            Assert.AreEqual(1, result);
        }
    }
}
