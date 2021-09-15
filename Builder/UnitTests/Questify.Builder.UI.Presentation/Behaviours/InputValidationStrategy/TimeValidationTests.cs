
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.Behaviours.InputValidationStrategy
{
    [TestClass]
    public class TimeValidationTests
    {

        #region hh:mm

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmm_0000()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm"};

            //Act, Assert
            Assert.IsTrue(timeValidator.IsInputValid("00:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmm_2359()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            //Act, Assert
            Assert.IsTrue(timeValidator.IsInputValid("23:59"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreInValid_hhmm_235900()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("23:59:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreInValid_hhmm_2400()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("24:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreInValid_hhmm_0060()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("00:60"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreInValid_hhmm_1111()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("1111"));
        }

        #endregion

        #region hh:mm:ss

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_000000()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            //Act, Assert
            Assert.IsTrue(timeValidator.IsInputValid("00:00:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_235959()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            //Act, Assert
            Assert.IsTrue(timeValidator.IsInputValid("23:59:59"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_240000()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("24:00:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_006000()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("00:60:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_000060()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("00:00:60"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_111111()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("111111"));
        }

        #endregion

        #region mm:ss

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_0000()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            //Act, Assert
            Assert.IsTrue(timeValidator.IsInputValid("00:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_5959()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            //Act, Assert
            Assert.IsTrue(timeValidator.IsInputValid("59:59"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_6000()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("60:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_0060()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("00:60"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_1111()
        {
            //Arrange
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            //Act, Assert
            Assert.IsFalse(timeValidator.IsInputValid("1111"));
        }

        #endregion
    }
}
