
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class MCScoringViewModelTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void GetBrokenRules_ForUninitialized()
        {
            //Arrange
            MCScoringViewModel vm = GetViewModel(min:1,max:1, solution: new Solution());
            //Act
            var result = vm.GetBrokenRules("ScorableItems");
            //Assert
            Assert.AreEqual(1, result.Count,"Only not key set rule should be here.");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void SettingKey_A_2Times_WillClearKey()
        {
            //Arrange
            var s = new Solution();
            MCScoringViewModel vm = GetViewModel(min: 1, max: 1, solution: s);
            
            //Act
            vm.ManipulateKey("A");
            vm.ManipulateKey("A");

            var result = vm.GetBrokenRules("ScorableItems");
            //Assert
            Assert.AreEqual(1, result.Count, "Only not key set rule should be here.");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void SettingKey_A_Then_E_WithMaxAndMin1_WillHaveNoError()
        {
            //Arrange
            var s = new Solution();
            MCScoringViewModel vm = GetViewModel(min: 1, max: 1, solution: s);

            //Act
            vm.ManipulateKey("A");
            vm.ManipulateKey("E");

            var result = vm.GetBrokenRules("ScorableItems");
            //Assert
            Assert.AreEqual(0, result.Count, "No Error");
        }

        #region No key set rule
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void NoKey_Rule_NoScore_HasError()
        {
            //Arrange
            MCScoringViewModel vm = GetViewModel(min: 1, max: 1, solution: new Solution());
            var rule = vm.Get_NoValueRule(MCScoringViewModel.CNoKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsTrue(hasError);            
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void NoKey_Rule_HasScore_HasNoError()
        {
            //Arrange
            var solution = new Solution();
            MCScoringViewModel vm = GetViewModel(min: 1, max: 1, solution: solution );
            vm.ManipulateKey("A");
            var rule = vm.Get_NoValueRule(MCScoringViewModel.CNoKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsFalse(hasError);
        }
        #endregion

        #region Too Little rule
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void TooLittle_Rule_NoScore_HasNoError()
        {
            //Arrange
            MCScoringViewModel vm = GetViewModel(min: 2, max: 3, solution: new Solution());
            var rule = vm.Get_TooLittleValueRule(MCScoringViewModel.CMinKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsFalse(hasError,"This rule will not validate when no scoring is present");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void TooLittle_Rule_1Score_HasError()
        {
            //Arrange
            var solution = new Solution();
            MCScoringViewModel vm = GetViewModel(min: 2, max: 3, solution: solution);
            vm.ManipulateKey("A");
            var rule = vm.Get_TooLittleValueRule(MCScoringViewModel.CNoKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsTrue(hasError,"2 is the minimum, so this has to be error nous");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void TooLittle_Rule_MinimumReached_HasNoError()
        {
            //Arrange
            var solution = new Solution();
            MCScoringViewModel vm = GetViewModel(min: 2, max: 3, solution: solution);
            vm.ManipulateKey("A");
            vm.ManipulateKey("B");
            var rule = vm.Get_TooLittleValueRule(MCScoringViewModel.CNoKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsFalse(hasError, "2 scores given so no error.");
        }
        #endregion

        #region Too Many rule
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void TooMany_Rule_NoScore_HasNoError()
        {
            //Arrange
            MCScoringViewModel vm = GetViewModel(min: 2, max: 3, solution: new Solution());
            var rule = vm.Get_TooManyValueRule(MCScoringViewModel.CMinKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsFalse(hasError, "The limit of scores has not been violated");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void TooMany_Rule_1Score_HasnoError()
        {
            //Arrange
            var solution = new Solution();
            MCScoringViewModel vm = GetViewModel(min: 2, max: 3, solution: solution);
            vm.ManipulateKey("A");
            var rule = vm.Get_TooManyValueRule(MCScoringViewModel.CNoKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsFalse(hasError, "The limit of scores has not been violated");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void TooMany_Rule_MaximumSurpassed_HasError()
        {
            //Arrange
            var solution = new Solution();
            MCScoringViewModel vm = GetViewModel(min: 2, max: 3, solution: solution);
            vm.ManipulateKey("A");
            vm.ManipulateKey("B");
            vm.ManipulateKey("C");
            vm.ManipulateKey("D");
            var rule = vm.Get_TooManyValueRule(MCScoringViewModel.CNoKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsTrue(hasError, "more than 3 scores given, so error.");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        public void TooMany_Rule_NoMaxChoices_HasNoError()
        {
            //Arrange
            var solution = new Solution();
            MCScoringViewModel vm = GetViewModel(min: 0, max: 0, solution: solution);
            vm.ManipulateKey("A");
            vm.ManipulateKey("B");
            vm.ManipulateKey("C");
            vm.ManipulateKey("D");
            var rule = vm.Get_TooManyValueRule(MCScoringViewModel.CNoKeyRule);
            //Act
            var hasError = rule.ValidateRule(vm);
            //Assert
            Assert.IsFalse(hasError, "The limit of scores has not been violated");
        }
        #endregion

        private MCScoringViewModel GetViewModel(int min, int max, Solution solution)
        {
            //Creates a Viewmodel with A,B and C
            var fakeVAS = new Cinch.TestViewAwareStatus();
            var ret = new MCScoringViewModel(fakeVAS);
            var fact = new MCScoringVWFactory();
            var param = new MultiChoiceScoringParameter() { ControllerId = "test", MinChoices = min, MaxChoices = max };
            param.Value = new ParameterSetCollection();
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });
            param.Value.Add(new ParameterCollection() { Id = "D" });
            param.Value.Add(new ParameterCollection() { Id = "E" });
            param.Value.Add(new ParameterCollection() { Id = "F" });
            var workspaceData = fact.Create(param, solution);
            var fakeView = A.Fake<Cinch.IWorkSpaceAware>();
            fakeView.WorkSpaceContextualData.DataValue = workspaceData.DataValue;
            fakeVAS.View = fakeView;
            
            //Act
            fakeVAS.SimulateViewIsLoadedEvent();

            return ret; //Returns initialized VM
        }

    }
}
