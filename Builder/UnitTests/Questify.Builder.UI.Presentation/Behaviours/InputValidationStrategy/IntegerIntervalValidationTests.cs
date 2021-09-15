﻿
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.Behaviours.InputValidationStrategy
{
    [TestClass]
    public class IntegerIntervalValidationTests
    {
        [TestMethod]
        public void ValidIntegerIntervalParts_AreValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator();

            //Act, Assert
            Assert.IsTrue(validator.IsInputAllowed("["));
            Assert.IsTrue(validator.IsInputAllowed("[1"));
            Assert.IsTrue(validator.IsInputAllowed("[1;"));
            Assert.IsTrue(validator.IsInputAllowed("[1;1]"));
            Assert.IsTrue(validator.IsInputAllowed("[;"));
            Assert.IsTrue(validator.IsInputAllowed("[;]"));
            Assert.IsTrue(validator.IsInputAllowed(";]"));
            Assert.IsTrue(validator.IsInputAllowed("]"));
            Assert.IsTrue(validator.IsInputAllowed(";"));
            Assert.IsTrue(validator.IsInputAllowed(""));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample_MaxLength1_isValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator() { MaxLength = 1 };

            //Act, Assert
            Assert.IsTrue(validator.IsInputValid("[-1;1]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample2_MaxLength1_isValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator() { MaxLength = 1 };

            //Act, Assert
            Assert.IsTrue(validator.IsInputValid("[0;1]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample_MaxLength2_isValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator() { MaxLength = 2};

            //Act, Assert
            Assert.IsTrue(validator.IsInputValid("[10;20]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample_MaxLength3_isValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator() { MaxLength = 3 };  

            //Act, Assert
            Assert.IsTrue(validator.IsInputValid("[1;100]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void RangeExample_MaxLength3_SignIsOmmittedFromLength_isValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator() { MaxLength = 3 };

            //Act, Assert
            Assert.IsTrue(validator.IsInputValid("[-100;100]"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void DecreasingRangeExample_MaxLength3_SignIsOmmittedFromLength_isValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator() { MaxLength = 3 };

            //Act, Assert
            Assert.IsTrue(validator.IsInputValid("[100;-100]"));
        }


        [TestMethod]
        public void InValidIntegerIntervalParts_AreNotValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator();

            //Act, Assert
            Assert.IsFalse(validator.IsInputAllowed("]["));
            Assert.IsFalse(validator.IsInputAllowed("[a;b]"));
            Assert.IsFalse(validator.IsInputAllowed("(1;1)"));
            Assert.IsFalse(validator.IsInputAllowed("{1;1}"));
        }

        [TestMethod]
        public void InValidIntegerIntervals_AreNotValid()
        {
            //Arrange
            var validator = new IntegerIntervalValidator() { MaxLength = 1 };

            //Act, Assert
            Assert.IsFalse(validator.IsInputValid("[]"));
            Assert.IsFalse(validator.IsInputValid("[1,1]"));
            Assert.IsFalse(validator.IsInputValid("[1.1]"));
            Assert.IsFalse(validator.IsInputValid("[1.1]"));
        }
    }
}
