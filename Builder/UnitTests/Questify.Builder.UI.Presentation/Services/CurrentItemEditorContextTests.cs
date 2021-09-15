
using Cinch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.Services
{
    [TestClass]
    public class CurrentItemEditorContextTests
    {
       
        [TestMethod, TestCategory("Services")]
        public void TestMessagesIsActive()
        {
            //Arrange
            var testMediator = Mediator.Instance;            
            var ctx = new CurrentItemEditorContext();
           
            //Act
            testMediator.NotifyColleagues(MediatorMessages.ItemEditor.IsActive, true);
            
            //Assert
            Assert.AreEqual(true, ctx.IsActive);

        }

        [TestMethod, TestCategory("Services")]
        public void TestMessagesBank()
        {
            //Arrange
            var testMediator = Mediator.Instance; ;
            var ctx = new CurrentItemEditorContext();

            //Act
            testMediator.NotifyColleagues(MediatorMessages.ItemEditor.Bank, 4447);

            //Assert
            Assert.AreEqual(4447, ctx.BankIdentifier);

        }


        [TestMethod, TestCategory("Services")]
        public void TestMessagesTitle()
        {
            //Arrange
            var testMediator = Mediator.Instance;
            var ctx = new CurrentItemEditorContext();

            //Act
            testMediator.NotifyColleagues(MediatorMessages.ItemEditor.Title, "42");

            //Assert
            Assert.AreEqual("42", ctx.Title);

        }
    }
}
