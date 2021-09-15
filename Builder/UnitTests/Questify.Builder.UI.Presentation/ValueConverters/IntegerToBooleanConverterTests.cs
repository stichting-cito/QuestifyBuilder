
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{
    [TestClass]
    public class IntegerToBooleanConverterTests
    {

        [TestMethod, TestCategory("ValueConverters")]
        public void GetTrueValue()
        {
            //Arrange
            var converter = new IntegerToBooleanConverter();
            //Act
            var result = converter.Convert(2, typeof(bool), 2, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.IsTrue((bool)result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void GetFalseValue()
        {
            //Arrange
            var converter = new IntegerToBooleanConverter();
            //Act
            var result = converter.Convert(2, typeof(bool), 1000, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.IsFalse((bool)result);
        }

    }
}
