using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{
    [TestClass]
    public class DateStringConverterTests
    {
        [TestMethod]
        public void NullConvertsToNull()
        {
            var converter = new DateStringConverter();

            var result = converter.Convert(null, typeof(DateTime), null, null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void InvalidDateConvertsToNull()
        {
            var converter = new DateStringConverter();

            var result = converter.Convert("aaaa", typeof(DateTime), null, null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DateStringConvertsToDateTime()
        {
            var converter = new DateStringConverter();

            string dt = new DateTime(2001, 12, 2).ToString("d");
            var result = converter.Convert(dt, typeof(DateTime), null, null);

            Assert.IsNotNull(result);
            DateTime r = (DateTime)result;

            Assert.AreEqual(2001, r.Year);
            Assert.AreEqual(12, r.Month);
            Assert.AreEqual(2, r.Day);
        }

        [TestMethod]
        public void ConvertBackUsesShortDateFormatString()
        {
            var converter = new DateStringConverter();
            var dt = new DateTime(2012, 4, 13);
            var result = converter.ConvertBack(dt, typeof(string), null, null);
            Assert.IsNotNull(result);
            Assert.AreEqual(dt.ToString("d"), result);

        }

        [TestMethod]
        public void ConvertBackNullValue()
        {
            var converter = new DateStringConverter();
            var result = converter.ConvertBack(null, typeof(string), null, null);
            Assert.IsNull(result);
        }
    }
}
