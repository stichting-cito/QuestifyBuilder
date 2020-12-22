
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
            var converter = new IntegerToBooleanConverter();
            var result = converter.Convert(2, typeof(bool), 2, Thread.CurrentThread.CurrentCulture);
            Assert.IsTrue((bool)result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void GetFalseValue()
        {
            var converter = new IntegerToBooleanConverter();
            var result = converter.Convert(2, typeof(bool), 1000, Thread.CurrentThread.CurrentCulture);
            Assert.IsFalse((bool)result);
        }

    }
}
