using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Controls;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls
{
    [TestClass]
    public class DoubleUtilTests
    {
        [TestMethod]
        public void AreClose_SameDoubleValues_ReturnsTrue()
        {
            double value1 = 0.0;
            double value2 = 0.0;

            var result = DoubleUtil.AreClose(value1, value2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreClose_NearlySameDoubleValues_ReturnsTrue()
        {
            double value1 = 0.000000000000001;
            double value2 = 0.0;

            var result = DoubleUtil.AreClose(value1, value2);

            Assert.IsTrue(result);
        }
    }
}
