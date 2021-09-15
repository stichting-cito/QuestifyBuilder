﻿
using System.Collections.Generic;
using System.Linq;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    /// <summary>
    /// This class tests the StringGapViewModel.
    /// 
    /// There are more scoring view models, if you think you can pull a certain test to the
    /// GenericScoreViewModelTests<...> then please do,.. you will cover much more.
    /// </summary>
    /// <remarks>
    /// The StringGapViewModel uses a generic GapViewModel[string] to store value. Updating an removing originate from that
    /// view model and will be handled by 
    /// </remarks>
    [TestClass]
    public class StringGapViewModelTests : GenericScoreViewModelTests<StringScoringParameter>
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void ScoreParam_Empty_WillHave0_ScorableItems()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller"), Solution);
            InitViewModel(workspaceData);
            //Act            
            var result = vm.ScorableItems.Keys;
            //Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void ScoreParam_Has1Value_WillHave1_ScorableItem()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller", "a"), Solution);
            InitViewModel(workspaceData);
            //Act            
            var result = vm.ScorableItems["a"];
            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void EmptyGapWillNotValidate()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller", "a"), Solution);
            InitViewModel(workspaceData);
            //Act            
            var result = vm.ScorableItems["a"].First();
            //Assert
            Assert.AreEqual(string.Empty, result.Value, "this value was supposed to be empty");
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GapValueShouldHaveActualValue()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller", "a"), Solution);
            InitViewModel(workspaceData);

            var result = vm.ScorableItems["a"].First();

            //Act            
            result.Value = "     "; //Spaces are not allowed

            //Assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void SetGapValue()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller", "a"), Solution);
            InitViewModel(workspaceData);

            var result = vm.ScorableItems["a"].First();
            //Act            
            result.Value = "Some Value";
            //Assert

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void SingleGapValue_RemoveGapValue()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller", "a"), Solution);
            InitViewModel(workspaceData);

            var largestNrOfVals = vm.ScorableItems["a"].Count;
            var result = vm.ScorableItems["a"].First();
            //Act            
            result.RemoveItem.Execute(null);
            //Assert
            Assert.AreEqual(1, vm.ScorableItems["a"].Count);
            Assert.AreEqual(string.Empty, vm.ScorableItems["a"].First().Value);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void MultipleGapValue_RemoveGapValue()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller", "a"), Solution);
            InitViewModel(workspaceData);

            vm.AddItem.Execute("a");
            var largestNrOfVals = vm.ScorableItems["a"].Count;

            var result = vm.ScorableItems["a"].Last();

            //Act            
            result.RemoveItem.Execute(null);

            //Assert
            Assert.AreEqual(2, largestNrOfVals);
            Assert.AreEqual(1, vm.ScorableItems["a"].Count);
            Assert.AreEqual(string.Empty, vm.ScorableItems["a"].First().Value);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [ExpectedException(typeof(KeyNotFoundException))]
        [Description("althrough no specific validation is placed on entering an existing key, for now this is ok.")]
        public void AddNonExistingKey()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller"), Solution);
            InitViewModel(workspaceData);

            //Act
            vm.AddItem.Execute("a");

            //Assert
            //Expecting exception.
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void AddNewEmptyScoringOption()
        {
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var workspaceData = Factory.Create(ScoreParam("Controller", "a"), Solution);
            InitViewModel(workspaceData);
            //Act
            vm.AddItem.Execute("a");
            var result = vm.ScorableItems["a"];
            //Assert
            Assert.AreEqual(2, result.Count);
        }
        
        //---------------------------------
        
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void Get2Types()
        {
            //Arrange
            var fakeContext = A.Fake<ICurrentItemEditorContext>();
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, fakeContext);
            var str = "(type1,type2)";
            //Act
            var lst = PreProcessingHelper.UnCollapseStrToList(str);
            //Assert
            Assert.AreEqual(2, lst.Count);
            Assert.AreEqual("type1", lst[0]);
        }
        
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GetNothingFromEmptyInputString()
        {
            //Arrange
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var str = "";
            //Act
            var lst = PreProcessingHelper.UnCollapseStrToList(str);
            //Assert
            Assert.AreEqual(0, lst.Count);
        }
        
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GetNothingFromWrongInputString()
        {
            //Arrange
            StringGapViewModel vm = new StringGapViewModel(fakeVAS, A.Fake<ICurrentItemEditorContext>());
            var str = "adk;sgaklsdjfhjkahw48yua-sdvha;kjw34hrlaushdcv7iaph38&()*(&^%*%$";
            //Act
            var lst = PreProcessingHelper.UnCollapseStrToList(str);
            //Assert
            Assert.AreEqual(0, lst.Count);
        }

        //---------------------------------

        protected override StringScoringParameter ScoreParam(string scoreId, params string[] ids)
        {
            var ret = new StringScoringParameter() { ControllerId = scoreId };
            ret.Value = new ParameterSetCollection();
            foreach (var id in ids)
            {
                ret.Value.Add(new ParameterCollection() { Id = id });
            }
            return ret;
        }

        internal override IScoringViewModel CreateVM(Cinch.TestViewAwareStatus fakeVas)
        {
            return new StringGapViewModel(fakeVas, A.Fake<ICurrentItemEditorContext>());
        }

        protected override IScoringParameterWorkspaceFactory CreateFactory()
        {
            return new StringScoringFactory();
        }

        protected override void SetSomeScore(StringScoringParameter scorePrm)
        {
            var manipulator = scorePrm.GetScoreManipulator(Solution);
            manipulator.SetKey("A", "txt");
        }
    }
}
