
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.Behaviours.InputValidationStrategy
{
    [TestClass]
    public class IntegerValidationTests
    {

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidIntegerValues_AreValid_0()
        {
            var intValidator = new IntegerValidator() { MaxLength = 1 };


            Assert.IsTrue(intValidator.IsInputValid("0"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidIntegerValues_AreValid_10()
        {
            var intValidator = new IntegerValidator() { MaxLength = 2 };

            Assert.IsTrue(intValidator.IsInputValid("10"));

        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidIntegerValues_AreValid_1()
        {
            var intValidator = new IntegerValidator() { MaxLength = 2 };

            Assert.IsTrue(intValidator.IsInputValid("1"));

        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidIntegerValues_AreValid_min1()
        {
            var intValidator = new IntegerValidator() { MaxLength = 2 };

            Assert.IsTrue(intValidator.IsInputValid("-1"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestInValidIntegerValues_AreNotValid_10()
        {
            var intValidator = new IntegerValidator() { MaxLength = 1 };


            Assert.IsFalse(intValidator.IsInputValid("10"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestInValidIntegerValues_AreNotValid_001()
        {
            var intValidator = new IntegerValidator() { MaxLength = 3 };

            Assert.IsFalse(intValidator.IsInputValid("001"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestInValidIntegerValues_AreNotValid_min100()
        {
            var intValidator = new IntegerValidator() { MaxLength = 2 }; ;


            Assert.IsFalse(intValidator.IsInputValid("-100"));
        }
    }
}
