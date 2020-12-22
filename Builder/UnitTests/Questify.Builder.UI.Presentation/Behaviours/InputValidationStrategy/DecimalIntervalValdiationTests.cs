
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.Behaviours.InputValidationStrategy
{

    [TestClass]
    public class DecimalIntervalValdiationTests
    {

        [TestInitialize]
        public void init()
        {
            decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        public string decimalSeparator { get; set; }

        [TestMethod]
        public void ValidDecimalIntervalParts_AreValid()
        {
            var validator = new DecimalIntervalValidator();

            Assert.IsTrue(validator.IsInputAllowed("["));
            Assert.IsTrue(validator.IsInputAllowed("[1" + decimalSeparator + "0"));
            Assert.IsTrue(validator.IsInputAllowed("[1" + decimalSeparator + ";"));
            Assert.IsTrue(validator.IsInputAllowed("[1;1" + decimalSeparator + "1]"));
            Assert.IsTrue(validator.IsInputAllowed("[1" + decimalSeparator + "0;1" + decimalSeparator + "1]"));
            Assert.IsTrue(validator.IsInputAllowed("[;"));
            Assert.IsTrue(validator.IsInputAllowed("[;]"));
            Assert.IsTrue(validator.IsInputAllowed(";]"));
            Assert.IsTrue(validator.IsInputAllowed("]"));
            Assert.IsTrue(validator.IsInputAllowed(";"));
            Assert.IsTrue(validator.IsInputAllowed(""));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample_1_Integer_2_Fraction_IsValid()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 2 };

            Assert.IsTrue(validator.IsInputValid("[-1" + decimalSeparator + "00;1" + decimalSeparator + "99]"));
        }
        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample_1_Integer_0_Fraction_IsValid()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 0 };
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(validator.IsInputValid("[0;1]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void RangeExampleDecreasing_1_Integer_0_Fraction_IsValid()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 0 };
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(validator.IsInputValid("[9;2]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample_2_Integer_2_Fraction_IsValid()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 };

            Assert.IsTrue(validator.IsInputValid("[10" + decimalSeparator + "0;20" + decimalSeparator + "99]"));
        }
        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample_1_Integer_2Fraction_IsValid()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 3, FractionPartMaxLength = 1 };

            Assert.IsTrue(validator.IsInputValid("[1" + decimalSeparator + "0;100" + decimalSeparator + "9]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void ValidDecimalIntervals_AreValid()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 3, FractionPartMaxLength = 1 };
            Assert.IsTrue(validator.IsInputValid("[-100" + decimalSeparator + "0;100" + decimalSeparator + "0]"));
        }



        [TestMethod, TestCategory("Behaviours")]
        public void InValidDecimalIntervalParts_AreNotValid()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 };
            Assert.IsFalse(validator.IsInputAllowed("]["));
            Assert.IsFalse(validator.IsInputAllowed("[a;b]"));
            Assert.IsFalse(validator.IsInputValid("(10" + decimalSeparator + "00;20" + decimalSeparator + "99)"));
            Assert.IsFalse(validator.IsInputValid("{10" + decimalSeparator + "00;20" + decimalSeparator + "99}"));
            Assert.IsFalse(validator.IsInputValid("[10" + decimalSeparator + "00|20" + decimalSeparator + "99]"));
            Assert.IsFalse(validator.IsInputValid("[10" + decimalSeparator + "00,20" + decimalSeparator + "99]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void InValidDecimalIntervals_AreNotValid()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 };
            Assert.IsFalse(validator.IsInputValid("[]"));
            Assert.IsFalse(validator.IsInputValid("[1,1]"));
            Assert.IsFalse(validator.IsInputValid("[1.1]"));

        }

        [TestMethod, TestCategory("Behaviours")]
        public void InValidDecimalIntervalParts_AreNotValid2()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 2 };
            Assert.IsFalse(validator.IsInputValid("[10" + decimalSeparator + "0;20" + decimalSeparator + "99]"));

        }

        [TestMethod, TestCategory("Behaviours")]
        public void InValidDecimalIntervalParts_AreNotValid3()
        {
            var validator = new DecimalIntervalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 1 };
            Assert.IsFalse(validator.IsInputValid("[10" + decimalSeparator + "0;20" + decimalSeparator + "99]"));
        }

    }
}
