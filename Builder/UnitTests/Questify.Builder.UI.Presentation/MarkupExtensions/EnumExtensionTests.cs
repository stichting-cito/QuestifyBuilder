
using System;
using System.Linq;
using System.Windows.Markup;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.MarkupExtensions;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.MarkupExtensions
{
    [TestClass]
    public class EnumExtensionTests
    {
        [TestMethod, TestCategory("WPF")]
        public void ReturnedTypeIsAsAdvertisedByAttribute()
        {
            var converter = new EnumExtension(typeof(MyEnum));
            var meReturnType = (MarkupExtensionReturnTypeAttribute)converter.GetType().GetCustomAttributes(typeof(MarkupExtensionReturnTypeAttribute), false).First();
            var result = converter.ProvideValue(A.Fake<IServiceProvider>());

            Assert.IsTrue(meReturnType.ReturnType.IsAssignableFrom(result.GetType()));
        }

        [TestMethod, TestCategory("WPF")]
        public void ReturnsExpectedResult()
        {
            var converter = new EnumExtension(typeof(MyEnum));
            var result = (Array)converter.ProvideValue(A.Fake<IServiceProvider>());
            Assert.AreEqual(2, result.Length);
        }

        [TestMethod, TestCategory("WPF")]
        [ExpectedException(typeof(ArgumentException))]
        public void NoGivenTypeWillThrow()
        {
            var converter = new EnumExtension();
            var result = converter.ProvideValue(A.Fake<IServiceProvider>());

        }

        private enum MyEnum
        {
            Test1, Test2
        }
    }
}
