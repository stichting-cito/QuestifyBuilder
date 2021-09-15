
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{

    [TestClass]
    public class NumericRangeConverterTests
    {
        [TestMethod]
        public void DisplayOfRangeThroughConverter()
        {
            var converter =new RangeConverter();
            
            object[] values = { 3,5};
            var formattedText = converter.Convert(values, typeof(string), null, null);
            Assert.AreEqual("[3;5]", formattedText);
        }

        [TestMethod]
        public void ConvertBackShouldReturnMultiTypeIntegerArray()
        {
            var converter = new RangeConverter();

            var result = converter.ConvertBack("[5;6]", new Type[] { typeof(int), typeof(int) }, "ConvertToMultiTypeInteger", null);
            MultiType type = (MultiType)result[0];
            Assert.AreEqual(5, type.IntegerValue);
            type = (MultiType)result[1];
            Assert.AreEqual(6, type.IntegerValue);
        }

        [TestMethod]
        public void ConvertBackShouldReturnMultiTypeDecimalArray()
        {
            var converter = new RangeConverter();
            var input = string.Format("[{0};{1}]", 123.56m, 899.99m);

            var result = converter.ConvertBack(input, new Type[] { typeof(decimal), typeof(decimal) }, "ConvertToMultiTypeDecimal", null);
            MultiType type = (MultiType)result[0];
            Assert.AreEqual(123.56m, type.DecimalValue);
            type = (MultiType)result[1];
            Assert.AreEqual(899.99m, type.DecimalValue);
        }

        [TestMethod]
        public void ConvertBackShouldReturnDecimalArray()
        {
            var converter = new RangeConverter();
            var input = string.Format("[{0};{1}]", 123.56m, 899.99m);

            var result = converter.ConvertBack(input, new Type[] { typeof(decimal), typeof(decimal) }, "ConvertToDecimal", null);
            Assert.AreEqual(123.56m, result[0]);
            Assert.AreEqual(899.99m, result[1]);
        }
    }
}
