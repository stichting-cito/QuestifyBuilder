
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.Behaviours.InputValidationStrategy
{
    [TestClass]
    public class AlphaNumericRangeValidatorTests
    {
        [TestMethod]
        public void OnlyLettersAreValidWhenAlphaRange()
        {
            //Arrange
            var validator = new AlphaNumericRangeValidator();
            validator.NumericIdentifiers = false;
            validator.AlternativesCount = 26;

            //Act, Assert
            Assert.IsTrue(validator.IsInputAllowed("A"));
            Assert.IsFalse(validator.IsInputAllowed("1"));
            Assert.IsFalse(validator.IsInputAllowed("."));
            Assert.IsFalse(validator.IsInputAllowed("AA"));
        }

        [TestMethod]
        public void OnlyDigitsAreValidWhenNumericRange()
        {
            //Arrange
            var validator = new AlphaNumericRangeValidator();
            validator.NumericIdentifiers = true;
            validator.AlternativesCount = 11;

            //Act, Assert
            Assert.IsFalse(validator.IsInputAllowed("A"));
            Assert.IsTrue(validator.IsInputAllowed("1"));
            Assert.IsFalse(validator.IsInputAllowed("."));
            Assert.IsFalse(validator.IsInputAllowed("AA"));
        }

        [TestMethod]
        public void OnlyLettersInRangAreValidWhenAlphaRange()
        {
            var validator = new AlphaNumericRangeValidator();
            validator.NumericIdentifiers = false;
            validator.AlternativesCount = 3;

            Assert.IsTrue(validator.IsInputAllowed("A"));
            Assert.IsTrue(validator.IsInputAllowed("B"));
            Assert.IsTrue(validator.IsInputAllowed("C"));
            Assert.IsFalse(validator.IsInputAllowed("D"));
            Assert.IsFalse(validator.IsInputAllowed("E"));
            Assert.IsFalse(validator.IsInputAllowed("F"));
        }

        [TestMethod]
        public void WithNumericIdentifiersTheValueShouldBeInRange()
        {
            var validator = new AlphaNumericRangeValidator();
            validator.NumericIdentifiers = true;
            validator.AlternativesCount = 19;

            Assert.IsTrue(validator.IsInputValid("19"));
            Assert.IsFalse(validator.IsInputValid("20"));

        }
    }
}
