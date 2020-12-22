
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{
    [TestClass]
    public class ListHasNoItemsToVisibilityConverterTests
    {
        [TestMethod, TestCategory("ValueConverters")]
        public void Returns_System_Windows_Visibility_Type()
        {
            var converter = new ListHasNoItemsToVisibilityConverter();
            var result = converter.Convert(null, typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            Assert.IsInstanceOfType(result, typeof(System.Windows.Visibility));
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void Null_Returns_NotCollapsed()
        {
            var converter = new ListHasNoItemsToVisibilityConverter();
            var result = (System.Windows.Visibility)converter.Convert(null,
    typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void EmptyArray_Returns_NotCollapsed()
        {
            var converter = new ListHasNoItemsToVisibilityConverter();
            string[] arr = new string[0];
            var result = (System.Windows.Visibility)converter.Convert(arr,
    typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void EmptyListOfString_Returns_NotCollapsed()
        {
            var converter = new ListHasNoItemsToVisibilityConverter();
            var result = (System.Windows.Visibility)converter.Convert(new List<String>(),
    typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void FilledArray_Returns_NotVisible()
        {
            var converter = new ListHasNoItemsToVisibilityConverter();
            string[] arr = new[] { "1" };
            var result = (System.Windows.Visibility)converter.Convert(arr,
    typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void FilledListOfString_Returns_NotVisible()
        {
            var converter = new ListHasNoItemsToVisibilityConverter();
            var result = (System.Windows.Visibility)converter.Convert(new List<String>(new[] { "a" }),
    typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            Assert.AreEqual(Visibility.Collapsed, result);
        }
    }
}
