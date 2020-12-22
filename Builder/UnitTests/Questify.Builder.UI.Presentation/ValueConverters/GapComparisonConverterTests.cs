using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ValueConverters;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ValueConverters
{
    [TestClass]
    public class GapComparisonConverterTests
    {
        readonly GapComparisonTypeConverter _converter = new GapComparisonTypeConverter();

        [TestMethod]
        public void ConvertsEquals()
        {
            var actual = _converter.Convert(GapComparisonType.Equals, null, null, null);
            Assert.AreEqual("=", actual);
        }

        [TestMethod]
        public void ConvertEquivalent()
        {
            var actual = _converter.Convert(GapComparisonType.Equivalent, null, null, null);
            Assert.AreEqual("equivalent", actual);
        }

        [TestMethod]
        public void ConvertDependency()
        {
            var actual = _converter.Convert(GapComparisonType.Dependency, null, null, null);
            Assert.AreEqual("dependency", actual);
        }

        [TestMethod]
        public void ConvertsBackEquals()
        {
            var actual = _converter.ConvertBack("=", null, null, null);
            Assert.AreEqual(GapComparisonType.Equals, actual);
        }
    }
}
