
using System.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class ChoiceSP_BlockRowViewModelFactoryTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void Proof_1_FactoryForChoiceShouldCreateOne_VM_PerParameter_Expects1()
        {
            var solution = new Solution();
            var param = new ChoiceScoringParameter() { Value = new ParameterSetCollection(), MinChoices = 1, MaxChoices = 1 };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution).ToList();

            Assert.AreEqual(1, result.Count, "Expects Exactly 1 instance");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void Proof_2_FactoryForChoiceShouldCreateOne_VM_PerParameter_Expects1()
        {
            var solution = new Solution();
            var param = new ChoiceScoringParameter() { Value = new ParameterSetCollection(), MinChoices = 1, MaxChoices = 1 };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });

            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution).ToList();

            Assert.AreEqual(1, result.Count, "Expects Exactly 1 instance");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void Proof_3_FactoryForChoiceShouldCreateOne_VM_PerParameter_Expects1()
        {
            var solution = new Solution();
            var param = new ChoiceScoringParameter() { Value = new ParameterSetCollection(), MinChoices = 1, MaxChoices = 1 };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });
            param.Value.Add(new ParameterCollection() { Id = "D" });

            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution).ToList();

            Assert.AreEqual(1, result.Count, "Expects Exactly 1 instance");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void Proof_4_FactoryForChoiceShouldCreateForMultiResponse()
        {
            var solution = new Solution();
            var param = new ChoiceScoringParameter() { Value = new ParameterSetCollection(), MaxChoices = 4, MinChoices = 2, ControllerId = "mr" };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });
            param.Value.Add(new ParameterCollection() { Id = "D" });


            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(solution), solution).ToList();

            Assert.AreEqual(1, result.Count, "Expects 1 instance for each option");
            Assert.IsInstanceOfType(result[0], typeof(MultiResponseBlockRowViewModel));
        }
    }
}
