
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.Behaviors.Strategies;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.Behaviours.InputValidationStrategy
{
    [TestClass]
    public class TimeValidationTests
    {


        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmm_0000()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            Assert.IsTrue(timeValidator.IsInputValid("00:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmm_2359()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            Assert.IsTrue(timeValidator.IsInputValid("23:59"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreInValid_hhmm_235900()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            Assert.IsFalse(timeValidator.IsInputValid("23:59:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreInValid_hhmm_2400()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            Assert.IsFalse(timeValidator.IsInputValid("24:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreInValid_hhmm_0060()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            Assert.IsFalse(timeValidator.IsInputValid("00:60"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreInValid_hhmm_1111()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm" };

            Assert.IsFalse(timeValidator.IsInputValid("1111"));
        }



        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_000000()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            Assert.IsTrue(timeValidator.IsInputValid("00:00:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_235959()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            Assert.IsTrue(timeValidator.IsInputValid("23:59:59"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_240000()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            Assert.IsFalse(timeValidator.IsInputValid("24:00:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_006000()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            Assert.IsFalse(timeValidator.IsInputValid("00:60:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_000060()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            Assert.IsFalse(timeValidator.IsInputValid("00:00:60"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_hhmmss_111111()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "hh:mm:ss" };

            Assert.IsFalse(timeValidator.IsInputValid("111111"));
        }



        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_0000()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            Assert.IsTrue(timeValidator.IsInputValid("00:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_5959()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            Assert.IsTrue(timeValidator.IsInputValid("59:59"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_6000()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            Assert.IsFalse(timeValidator.IsInputValid("60:00"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_0060()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            Assert.IsFalse(timeValidator.IsInputValid("00:60"));
        }

        [TestMethod, TestCategory("Behaviours")]
        public void TestValidTimeValues_AreValid_mmss_1111()
        {
            var timeValidator = new TimeValidator() { TimeFormat = "mm:ss" };

            Assert.IsFalse(timeValidator.IsInputValid("1111"));
        }

    }
}
