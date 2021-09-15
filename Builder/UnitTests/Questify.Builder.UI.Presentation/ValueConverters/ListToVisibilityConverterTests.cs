
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{
    [TestClass]
    public class ListToVisibilityConverterTests
    {
        [TestMethod, TestCategory("ValueConverters")]
        public void Returns_System_Windows_Visibility_Type()
        {
            //Arrange
            var converter = new ListHasItemsToVisibilityConverter();
            //Act
            var result = converter.Convert(null, typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Windows.Visibility));
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void Null_Returns_Collapsed()
        {
            //Arrange
            var converter = new ListHasItemsToVisibilityConverter();
            //Act
            var result = (System.Windows.Visibility)converter.Convert(null,
                typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void EmptyArray_Returns_Collapsed()
        {
            //Arrange
            var converter = new ListHasItemsToVisibilityConverter();
            string[] arr = new string[0];
            //Act
            var result = (System.Windows.Visibility)converter.Convert(arr,
                typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void EmptyListOfString_Returns_Collapsed()
        {
            //Arrange
            var converter = new ListHasItemsToVisibilityConverter();
            //Act
            var result = (System.Windows.Visibility)converter.Convert(new List<String>(),
                typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void FilledArray_Returns_Visible()
        {
            //Arrange
            var converter = new ListHasItemsToVisibilityConverter();
            string[] arr = new[] { "1" };
            //Act
            var result = (System.Windows.Visibility)converter.Convert(arr,
                typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod, TestCategory("ValueConverters")]
        public void FilledListOfString_Returns_Visible()
        {
            //Arrange
            var converter = new ListHasItemsToVisibilityConverter();
            //Act
            var result = (System.Windows.Visibility)converter.Convert(new List<String>(new[]{"a"}),
                typeof(System.Windows.Visibility), null, Thread.CurrentThread.CurrentCulture);
            //Assert
            Assert.AreEqual(Visibility.Visible, result);
        }
    }
}
