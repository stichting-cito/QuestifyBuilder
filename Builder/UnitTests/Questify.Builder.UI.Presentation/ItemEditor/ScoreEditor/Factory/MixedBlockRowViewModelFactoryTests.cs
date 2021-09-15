
using System.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class MixedBlockRowViewModelFactoryTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreateMC_And_INT_Expects2Rows()
        {
            //Arrange
            var solution = new Solution();
            var mcScoringParameter = new MultiChoiceScoringParameter() {MaxChoices = 1}.AddSubParameters("A", "B", "C",
                "D");
            var integerScoringParameter = new IntegerScoringParameter().AddSubParameters("A");

            var combinedScoringMap =
                CombinedScoringMapKey.Create(new[]
                {
                    new ScoringMapKey(mcScoringParameter, "A"), new ScoringMapKey(mcScoringParameter, "B"),
                    new ScoringMapKey(mcScoringParameter, "C"), new ScoringMapKey(mcScoringParameter, "D"),
                    new ScoringMapKey(integerScoringParameter, "A") //<-----The Difference
                }, new[] {0, 1});

            //Act
            var result = BlockRowViewModelFactory.CreateInstances(combinedScoringMap, solution, 0);

            //Assert
            Assert.AreEqual(2, result.Count(), "Expected 2 block rows");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreateInt_And_MC_Expects2Rows()
        {
            //Arrange
            var solution = new Solution();
            var mcScoringParameter = new MultiChoiceScoringParameter() {MaxChoices = 1}.AddSubParameters("A", "B", "C",
                "D");
            var integerScoringParameter = new IntegerScoringParameter().AddSubParameters("A");

            var combinedScoringMap =
                CombinedScoringMapKey.Create(new[]
                {
                    new ScoringMapKey(integerScoringParameter, "A"), //<-----The Difference
                    new ScoringMapKey(mcScoringParameter, "A"), new ScoringMapKey(mcScoringParameter, "B"),
                    new ScoringMapKey(mcScoringParameter, "C"), new ScoringMapKey(mcScoringParameter, "D")
                }, new[] {0, 1});

            //Act
            var result = BlockRowViewModelFactory.CreateInstances(combinedScoringMap, solution, 0);

            //Assert
            Assert.AreEqual(2, result.Count(), "Expected 2 block rows");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreateMR_And_INT_Expects5Rows()
        {
            //Arrange
            var solution = new Solution();

            var mcScoringParameter = new MultiChoiceScoringParameter() {MaxChoices = 2, ControllerId="mr"}.AddSubParameters("A", "B", "C",
                "D");
            var integerScoringParameter = new IntegerScoringParameter().AddSubParameters("A");

            var combinedScoringMap =
                CombinedScoringMapKey.Create(new[]
                {
                    new ScoringMapKey(mcScoringParameter, "A"), new ScoringMapKey(mcScoringParameter, "B"),
                    new ScoringMapKey(mcScoringParameter, "C"), new ScoringMapKey(mcScoringParameter, "D"),
                    new ScoringMapKey(integerScoringParameter, "A") //<-----The Difference
                }, new[] {0, 1});

            //Act
            var result = BlockRowViewModelFactory.CreateInstances(combinedScoringMap, solution, 0);

            //Assert
            Assert.AreEqual(5, result.Count(), "Expected 5 block rows");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreateINT_And_MR_Expects5Rows()
        {
            //Arrange
            var solution = new Solution();

            var mcScoringParameter = new MultiChoiceScoringParameter() {MaxChoices = 2, ControllerId="mr"}.AddSubParameters("A", "B", "C",
                "D");
            var integerScoringParameter = new IntegerScoringParameter().AddSubParameters("A");

            var combinedScoringMap =
                CombinedScoringMapKey.Create(new[]
                {
                    new ScoringMapKey(integerScoringParameter, "A"), //<-----The Difference
                    new ScoringMapKey(mcScoringParameter, "A"), new ScoringMapKey(mcScoringParameter, "B"),
                    new ScoringMapKey(mcScoringParameter, "C"), new ScoringMapKey(mcScoringParameter, "D")
                }, new[] {0, 1});

            //Act
            var result = BlockRowViewModelFactory.CreateInstances(combinedScoringMap, solution, 0);

            //Assert
            Assert.AreEqual(5, result.Count(), "Expected 5 block rows");
        }
    }
}