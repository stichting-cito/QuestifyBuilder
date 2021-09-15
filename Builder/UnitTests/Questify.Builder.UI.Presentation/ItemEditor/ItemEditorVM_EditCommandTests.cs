
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces.UI;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Button = Questify.Builder.Logic.Service.Interfaces.UI.Button;
using Control = System.Windows.Forms.Control;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemEditorVM_EditCommandTests : ItemEditorTestBase
    {
        #region ICutPaste

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void Paste_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            ICutPaste fake = A.Fake<ICutPaste>();
            A.CallTo(() => fake.CanPaste).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.PasteAsText.CanExecute(null))
                ItemEditorVm.PasteAsText.Execute(null);
            //Assert
            A.CallTo(() => fake.PasteAsText()).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void Cut_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            ICutPaste fake = A.Fake<ICutPaste>();
            A.CallTo(() => fake.CanCut).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.Cut.CanExecute(null))
                ItemEditorVm.Cut.Execute(null);
            //Assert
            A.CallTo(() => fake.Cut()).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void Copy_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            ICutPaste fake = A.Fake<ICutPaste>();
            A.CallTo(() => fake.CanCopy).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.Copy.CanExecute(null))
                ItemEditorVm.Copy.Execute(null);
            //Assert
            A.CallTo(() => fake.Copy()).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        [Description("The cannot state is not handled, and will result in a call")]
        public void CannotPaste_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            ICutPaste fake = A.Fake<ICutPaste>();
            A.CallTo(() => fake.CanPaste).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.PasteAsText.CanExecute(null))
                ItemEditorVm.PasteAsText.Execute(null);
            //Assert
            A.CallTo(() => fake.PasteAsText()).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        [Description("The cannot state is not handled, and will result in a call")]
        public void CannotCut_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            ICutPaste fake = A.Fake<ICutPaste>();
            A.CallTo(() => fake.CanCut).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.Cut.CanExecute(null))
                ItemEditorVm.Cut.Execute(null);
            //Assert
            A.CallTo(() => fake.Cut()).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        [Description("The cannot state is not handled, and will result in a call")]
        public void CannotCopy_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            ICutPaste fake = A.Fake<ICutPaste>();
            A.CallTo(() => fake.CanCopy).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.Copy.CanExecute(null))
                ItemEditorVm.Copy.Execute(null);
            //Assert
            A.CallTo(() => fake.Copy()).MustHaveHappened();
        }

        #endregion

        #region IMedia

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void AddPicture_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IMedia fake = A.Fake<IMedia>();
            A.CallTo(() => fake.CanAddImage).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.AddPicture.CanExecute(null))
                ItemEditorVm.AddPicture.Execute(null);
            //Assert
            A.CallTo(() => fake.AddImage()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotAddPicture_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IMedia fake = A.Fake<IMedia>();
            A.CallTo(() => fake.CanAddImage).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.AddPicture.CanExecute(null))
                ItemEditorVm.AddPicture.Execute(null);
            //Assert
            A.CallTo(() => fake.AddImage()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void AddVideo_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IMedia fake = A.Fake<IMedia>();
            A.CallTo(() => fake.CanAddVideo).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.AddVideo.CanExecute(null))
                ItemEditorVm.AddVideo.Execute(null);
            //Assert
            A.CallTo(() => fake.AddVideo()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotAddVideo_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IMedia fake = A.Fake<IMedia>();
            A.CallTo(() => fake.CanAddVideo).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.AddVideo.CanExecute(null))
                ItemEditorVm.AddVideo.Execute(null);
            //Assert
            A.CallTo(() => fake.AddVideo()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void AddAudio_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IMedia fake = A.Fake<IMedia>();
            A.CallTo(() => fake.CanAddAudio).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.AddAudio.CanExecute(null))
                ItemEditorVm.AddAudio.Execute(null);
            //Assert
            A.CallTo(() => fake.AddAudio()).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotAddAudio_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IMedia fake = A.Fake<IMedia>();
            A.CallTo(() => fake.CanAddAudio).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            if (ItemEditorVm.AddAudio.CanExecute(null))
                ItemEditorVm.AddAudio.Execute(null);
            //Assert
            A.CallTo(() => fake.AddAudio()).MustNotHaveHappened();
        }

        

        #endregion

        #region IRichText

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoBold_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanBold).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Bold;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeBold()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoBold_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanBold).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Bold;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeBold()).MustNotHaveHappened();
        }


        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoItalic_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanItalic).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Italic;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeItalic()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoItalic_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanItalic).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Italic;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeItalic()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoUnderline_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanUnderlined).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Underline;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeUnderlined()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoUnderline_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanUnderlined).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Underline;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeUnderlined()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoSuperScript_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanSuperScript).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.SuperScript;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeSuperScript()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoSuperScript_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanSuperScript).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.SuperScript;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeSuperScript()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoSubScript_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanSubScript).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.SubScript;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeSubScript()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoSubScript_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanSubScript).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.SubScript;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeSubScript()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoStrikeThrough_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanStrikethrough).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.StrikeThrough;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeStrikethrough()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoStrikeThrough_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanStrikethrough).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.StrikeThrough;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeStrikethrough()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoAlignLeft_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAlignLeft).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AlignLeft;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AlignLeft()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoAlignLeft_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAlignLeft).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AlignLeft;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AlignLeft()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoAlignMiddle_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAlignMiddle).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AlignMiddle;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AlignMiddle()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoAlignMiddle_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAlignMiddle).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AlignMiddle;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AlignMiddle()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoAlignRight_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAlignRight).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AlignRight;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AlignRight()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoAlignRight_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAlignRight).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AlignRight;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AlignRight()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoClearFormatting_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanClearStyling).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.ClearFormatting;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.ClearStyling()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoClearFormatting_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanClearStyling).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.ClearFormatting;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.ClearStyling()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoLock_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanLock).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Lock;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.Lock()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoLock_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanLock).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Lock;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.Lock()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoNumberedList_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanMakeNumbered).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.NumberedList;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeNumbered()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoNumberedList_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanMakeNumbered).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.NumberedList;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeNumbered()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoBulletedList_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanMakeBulleted).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.BulletedList;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeBulleted()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoBulletedList_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanMakeBulleted).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.BulletedList;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.MakeBulleted()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoIndent_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanIndent).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Indent;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.DoIndent()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoIndent_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanIndent).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.Indent;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.DoIndent()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoDeIndent_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanUnIndent).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.DeIndent;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.DoUnIndent()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoDeIndent_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanUnIndent).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.DeIndent;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.DoUnIndent()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoAddTable_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAddTable).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AddTable;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AddTable()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoAddTable_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAddTable).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AddTable;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AddTable()).MustNotHaveHappened();
        }

        //--

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoAddFormula_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAddFormula).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AddFormula;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AddFormula()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoAddFormula_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAddFormula).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AddFormula;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AddFormula()).MustNotHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoAddSymbol_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IAddSymbolDialog fake = A.Fake<IAddSymbolDialog>();
            var fakeRichText = new FakeRichTextControl();
            A.CallTo(() => fake.IsDisposed()).Returns(false);
            var fakeDialogshow = A.CallTo(() => fake.Show(A<IWin32Window>.Ignored, A<System.Drawing.Point>.Ignored));
            ItemEditorVm.EditorChange(fakeRichText);
            ItemEditorVm.SetSymbolDialog(fake);
            //Act
            var command = ItemEditorVm.OpenSymbolDialog;
            if (command.CanExecute(null)) command.Execute(null);

            //Assert
            fakeDialogshow.MustHaveHappened();             
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoAddSymbol_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fakeRichText = A.Fake<IRichText>();
            A.CallTo(() => fakeRichText.CanAddSpecialSymbol).Returns(false);
            IAddSymbolDialog fake = A.Fake<IAddSymbolDialog>();
            A.CallTo(() => fake.IsDisposed()).Returns(false);
            var fakeDialogshow = A.CallTo(() => fake.Show(A<IWin32Window>.Ignored, A<System.Drawing.Point>.Ignored));
            ItemEditorVm.EditorChange(fakeRichText);
            ItemEditorVm.SetSymbolDialog(fake);
            //Act
            var command = ItemEditorVm.OpenSymbolDialog;
            if (command.CanExecute(null)) command.Execute(null);

            //Assert
            fakeDialogshow.MustNotHaveHappened();   
        }



        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoAddInlineControl_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAddInlineControl).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AddInlineControl;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AddInlineControl()).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoAddInlineControl_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAddInlineControl).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AddInlineControl;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AddInlineControl()).MustNotHaveHappened();
        }

        //--


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void DoAddReference_IsExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAddReference).Returns(true);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AddReference;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AddReference(A<string>.Ignored)).MustHaveHappened();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void CannotDoAddReference_IsNotExecuted_Test()
        {
            //Arrange            
            var ItemEditorVm = new ItemEditorViewModel();
            IRichText fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanAddReference).Returns(false);
            ItemEditorVm.EditorChange(fake);
            //Act
            var command = ItemEditorVm.AddReference;

            if (command.CanExecute(null)) command.Execute(null);
            //Assert
            A.CallTo(() => fake.AddReference(A<string>.Ignored)).MustNotHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void MuteTextToSpeech_IsNotExecuted_Test()
        {
            // Arrange
            var itemEditorVm = new ItemEditorViewModel();
            var fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanSetTextToSpeechOptions).Returns(false);
            itemEditorVm.EditorChange(fake);

            // Act
            var command = itemEditorVm.MuteTextToSpeech;
            if (command.CanExecute(null)) command.Execute(null);
            
            // Assert
            A.CallTo(() => fake.MuteTextToSpeech()).MustNotHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor")]
        public void MuteTextToSpeech_IsExecuted_Test()
        {
            // Arrange
            var itemEditorVm = new ItemEditorViewModel();
            var fake = A.Fake<IRichText>();
            A.CallTo(() => fake.CanSetTextToSpeechOptions).Returns(true);
            itemEditorVm.EditorChange(fake);

            // Act
            var command = itemEditorVm.MuteTextToSpeech;
            if (command.CanExecute(null)) command.Execute(null);

            // Assert
            A.CallTo(() => fake.MuteTextToSpeech()).MustHaveHappened(Repeated.Exactly.Once);
        }


        private class FakeRichTextControl : Control, IRichText
        {
            public event Handlers.IsButtonCheckedChangedEventHandler IsButtonCheckedChanged;
            public bool IsButtonChecked(Button btn)
            {
                        return false;
            }

            public string CurrentStyle { get; private set; }
            public event Handlers.CurrentStyleChangedEventHandler CurrentStyleChanged;
            public string CurrentLanguage { get; private set; }
            public event Handlers.CurrentLanguageChangedEventHandler CurrentLanguageChanged;
            public bool CanBold { get; private set; }
            public bool CanItalic { get; private set; }
            public bool CanUnderlined { get; private set; }
            public bool CanSuperScript { get; private set; }
            public bool CanSubScript { get; private set; }
            public bool CanStrikethrough { get; private set; }
            public bool CanAlignLeft { get; private set; }
            public bool CanAlignMiddle { get; private set; }
            public bool CanAlignRight { get; private set; }
            public bool CanIndent { get; private set; }
            public bool CanUnIndent { get; private set; }
            public bool CanMakeNumbered { get; private set; }
            public bool CanMakeRomanNumbered { get; private set; }
            public bool CanMakeBulleted { get; private set; }
            public bool CanClearStyling { get; private set; }
            public bool CanLock { get; private set; }
            public bool CanAddTable { get; private set; }
            public bool CanAddTableByRowsColums { get; private set; }
            public bool CanAddFormula { get; private set; }
            public bool CanAddSpecialSymbol
            {
                get { return true; }
            }

            public bool CanSetTextToSpeechOptions { get; private set; }

            public bool CanAddInlineControl { get; private set; }
            public bool CanAddPopup { get; private set; }
            public bool CanAddCustomInteraction { get; private set; }
            public bool ShowAddCustomInteraction { get; private set; }
            public bool CanAddReference { get; private set; }
            public IList<string> UserStyles { get { return new List<string>(); }}
            public IList<string> Languages { get { return new List<string>(); } }

            public bool CanSetFormatting => false;
            public bool CanRemoveTTS => false;
            public event Handlers.SelectionChangedEventHandler SelectionChanged;

            public void MakeBold()
            {
                throw new NotImplementedException();
            }

            public void MakeItalic()
            {
                throw new NotImplementedException();
            }

            public void MakeUnderlined()
            {
                throw new NotImplementedException();
            }

            public void MakeSuperScript()
            {
                throw new NotImplementedException();
            }

            public void MakeSubScript()
            {
                throw new NotImplementedException();
            }

            public void MakeStrikethrough()
            {
                throw new NotImplementedException();
            }

            public void AlignLeft()
            {
                throw new NotImplementedException();
            }

            public void AlignMiddle()
            {
                throw new NotImplementedException();
            }

            public void AlignRight()
            {
                throw new NotImplementedException();
            }

            public void DoIndent()
            {
                throw new NotImplementedException();
            }

            public void DoUnIndent()
            {
                throw new NotImplementedException();
            }

            public void MakeNumbered()
            {
                throw new NotImplementedException();
            }

            public void MakeRomanNumbered()
            {
                throw new NotImplementedException();
            }

            public void MakeBulleted()
            {
                throw new NotImplementedException();
            }

            public void ClearStyling()
            {
                throw new NotImplementedException();
            }

            public void Lock()
            {
                throw new NotImplementedException();
            }

            public void AddTable()
            {
                throw new NotImplementedException();
            }

            public void AddTableByRowsColums(int columns, int row)
            {
                throw new NotImplementedException();
            }

            public void AddFormula()
            {
                throw new NotImplementedException();
            }

            public void AddSpecialSymbol(char symbol)
            {
                throw new NotImplementedException();
            }

            public void AddInlineControl()
            {
                throw new NotImplementedException();
            }

            public void AddReference(string referenceId)
            {
                throw new NotImplementedException();
            }

            public void ApplyStyle(string styleName)
            {
                throw new NotImplementedException();
            }

            public void ApplyLanguage(string languageName)
            {
                throw new NotImplementedException();
            }

            public void InsertElementReference()
            {
                throw new NotImplementedException();
            }

            public void InsertSymbolReference()
            {
                throw new NotImplementedException();
            }

            public void InsertHighlightReference()
            {
                throw new NotImplementedException();
            }

            public void RemoveReference()
            {
                throw new NotImplementedException();
            }

            public void OverviewReferences()
            {
                throw new NotImplementedException();
            }

            public void RemoveCursor()
            {
            }

            public void SetCursor()
            {
            }

            public void AddCI()
            {
                throw new NotImplementedException();
            }

            public void AddPopup()
            {
                throw new NotImplementedException();
            }

            public void SetFocusVisibility(bool setFocus)
            {
                // Do nothing
            }

            public void MuteTextToSpeech()
            {
                throw new NotImplementedException();
            }

            public void AlternativeTextToSpeech()
            {
                throw new NotImplementedException();
            }

            public void PauseTextToSpeech(int? pause)
            {
                throw new NotImplementedException();
            }

            public void RemoveTextToSpeech()
            {
                throw new NotImplementedException();
            }

            public void ResetCurrentSelection()
            {
                throw new NotImplementedException();
            }
        }


        #endregion

    }
}
