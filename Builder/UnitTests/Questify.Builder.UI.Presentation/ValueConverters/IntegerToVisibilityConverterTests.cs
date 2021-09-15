
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
            //Arrange
            var converter = new IntegerToVisibilityConverter();
            //Act
            var result = converter.Convert(2, typeof(bool), 2, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.AreEqual(Visibility.Visible,(Visibility)result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void GetHiddenValue()
        {
            //Arrange
            var converter = new IntegerToVisibilityConverter();
            //Act
            var result = converter.Convert(2, typeof(bool), 1000, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.AreEqual(Visibility.Collapsed, (Visibility)result);
        }

    }
}
