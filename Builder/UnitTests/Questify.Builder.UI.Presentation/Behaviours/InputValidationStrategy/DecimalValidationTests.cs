
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.Behaviours.InputValidationStrategy
{
    [TestClass]
    public class DecimalInputValidationTests
    {

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreValid_1dot0()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 1 };
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;


            Assert.IsTrue(decimalValidator.IsInputAllowed("1" + decimalSeparator + "0"));
            Assert.IsTrue(decimalValidator.IsInputValid("1" + decimalSeparator + "0"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreValid_10dot0()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 1 };
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;


            Assert.IsTrue(decimalValidator.IsInputAllowed("10" + decimalSeparator + "0"));
            Assert.IsTrue(decimalValidator.IsInputValid("10" + decimalSeparator + "0"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreValid_1dot01()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 2 };
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(decimalValidator.IsInputAllowed("1" + decimalSeparator + "01"));
            Assert.IsTrue(decimalValidator.IsInputValid("1" + decimalSeparator + "01"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreValid_10dot10()
        {
            var validadator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 };
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(validadator.IsInputAllowed("10" + decimalSeparator + "10"));
            Assert.IsTrue(validadator.IsInputValid("10" + decimalSeparator + "10"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreValid_10()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 };
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(decimalValidator.IsInputAllowed("10"));
            Assert.IsTrue(decimalValidator.IsInputValid("10"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreValid_1()
        {
            var validadator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 }; ;
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(validadator.IsInputAllowed("1"));
            Assert.IsTrue(validadator.IsInputValid("1"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreValid_min1()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 };
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(decimalValidator.IsInputAllowed("-1"));
            Assert.IsTrue(decimalValidator.IsInputValid("-1"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreValid_min0dot01()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 }; ;
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(decimalValidator.IsInputAllowed("-0" + decimalSeparator + "01"));
            Assert.IsTrue(decimalValidator.IsInputValid("-0" + decimalSeparator + "01"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreInValid_01dot0()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 1 }; ;
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;


            Assert.IsFalse(decimalValidator.IsInputAllowed("01" + decimalSeparator + "0"));
            Assert.IsFalse(decimalValidator.IsInputValid("01" + decimalSeparator + "0"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreInValid_100dot0()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 1 }; ;
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsFalse(decimalValidator.IsInputAllowed("100" + decimalSeparator + "0"));
            Assert.IsFalse(decimalValidator.IsInputValid("100" + decimalSeparator + "0"));

        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreInValid_1dot101()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 2 }; ;
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsFalse(decimalValidator.IsInputAllowed("1" + decimalSeparator + "101"));
            Assert.IsFalse(decimalValidator.IsInputValid("1" + decimalSeparator + "101"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreInValid_100dot10()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 }; ;
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsFalse(decimalValidator.IsInputAllowed("100" + decimalSeparator + "10"));
            Assert.IsFalse(decimalValidator.IsInputValid("100" + decimalSeparator + "10"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreInValid_100()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 0 }; ;

            Assert.IsFalse(decimalValidator.IsInputAllowed("100"));
            Assert.IsFalse(decimalValidator.IsInputValid("100"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AreInValid_min00dot01()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 1, FractionPartMaxLength = 2 }; ;
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsFalse(decimalValidator.IsInputAllowed("-00" + decimalSeparator + "01"));
            Assert.IsFalse(decimalValidator.IsInputValid("-00" + decimalSeparator + "01"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidDecimalValues_AllowedToEndWithSepearatorWhileEnteringButNotValid()
        {
            var decimalValidator = new DecimalValidator() { IntegerPartMaxLength = 2, FractionPartMaxLength = 2 }; ;
            var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Assert.IsTrue(decimalValidator.IsInputAllowed("10" + decimalSeparator));
            Assert.IsFalse(decimalValidator.IsInputValid("10" + decimalSeparator));
        }
    }
}
