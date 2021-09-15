
using System;
using System.Linq;
using System.Reflection;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]      
    public class InlineChoiceBlockRowViewModelFactoryTests
    {
        [TestMethod,TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreateInstancesOfInlineChoiceBlockRowViewModel()
        {
            try
            {
                //Arrange
                var solution = new Solution();
                var param = new InlineChoiceScoringParameter() { Label = "Roman", Value = new ParameterSetCollection() };
                param.Value.Add(new ParameterCollection() { Id = "A" });
                param.Value.Add(new ParameterCollection() { Id = "B" });
                param.Value.Add(new ParameterCollection() { Id = "C" });

                param.Value[0].InnerParameters.Add(new PlainTextParameter() { Value = "1e keuze" });
                param.Value[1].InnerParameters.Add(new PlainTextParameter() { Value = "2e keuze" });
                param.Value[2].InnerParameters.Add(new PlainTextParameter() { Value = "3e keuze" });
                var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
                    .ToList();

                //Assert
                Assert.AreEqual(1, result.Count);
                Assert.IsInstanceOfType(result.First(), typeof(InlineChoiceBlockRowViewModel));
                var vm = (InlineChoiceBlockRowViewModel)result.First();
                Assert.AreEqual(3, vm.Choices.DataValue.Count);
            }
            catch (Exception ex)
            {
                if (ex is System.Reflection.ReflectionTypeLoadException)
                {
                    var typeLoadException = ex as ReflectionTypeLoadException;
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                    foreach (var loaderException in loaderExceptions)
                    {
                        Console.WriteLine("LOADEREXCEPTION: {0}", loaderException.Message);
                    }
                }
                throw;
            }
            
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void DisplayValueShouldBeMappedOnScoreKey()
        {
            //Arrange
            var solution = new Solution();
            solution.Findings.Add(new KeyFinding("Opgave"));
            solution.Findings[0].Facts.Add(new KeyFact("B-InlineA"));
            var param = new InlineChoiceScoringParameter() { FindingOverride="Opgave", InlineId="InlineA", Label = "Roman", Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });

            param.Value[0].InnerParameters.Add(new PlainTextParameter() { Value = "1e keuze" });
            param.Value[1].InnerParameters.Add(new PlainTextParameter() { Value = "2e keuze" });
            param.Value[2].InnerParameters.Add(new PlainTextParameter() { Value = "3e keuze" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution).ToList();

            //Assert
            Assert.AreEqual(1, result.Count);
            var vm = (InlineChoiceBlockRowViewModel)result.First();

            Assert.AreEqual("B", vm.DisplayValue);

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void  ChoiceIn2FactSets_VerifyResultSet1()
        {
            //Arrange
            var solution = new Solution();
            solution.Findings.Add(new KeyFinding("Opgave"));
            solution.Findings[0].KeyFactsets.Add(new KeyFactSet());
            solution.Findings[0].KeyFactsets.Add(new KeyFactSet());
            solution.Findings[0].KeyFactsets[0].Facts.Add(new KeyFact("B-InlineA"));
            solution.Findings[0].KeyFactsets[1].Facts.Add(new KeyFact("C-InlineA"));
            var param = new InlineChoiceScoringParameter() { FindingOverride = "Opgave", InlineId = "InlineA", Label = "Roman", Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });

            param.Value[0].InnerParameters.Add(new PlainTextParameter() { Value = "1e keuze" });
            param.Value[1].InnerParameters.Add(new PlainTextParameter() { Value = "2e keuze" });
            param.Value[2].InnerParameters.Add(new PlainTextParameter() { Value = "3e keuze" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0,1), solution,0)
               .ToList();
            //Assert
            Assert.AreEqual(1, result.Count);
            var vm = (InlineChoiceBlockRowViewModel)result.First();
            Assert.AreEqual("B", vm.DisplayValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ChoiceIn2FactSets_VerifyResultSet2()
        {
            //Arrange
            var solution = new Solution();
            solution.Findings.Add(new KeyFinding("Opgave"));
            solution.Findings[0].KeyFactsets.Add(new KeyFactSet());
            solution.Findings[0].KeyFactsets.Add(new KeyFactSet());
            solution.Findings[0].KeyFactsets[0].Facts.Add(new KeyFact("B-InlineA"));
            solution.Findings[0].KeyFactsets[1].Facts.Add(new KeyFact("C-InlineA"));
            var param = new InlineChoiceScoringParameter() { FindingOverride = "Opgave", InlineId = "InlineA", Label = "Roman", Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });

            param.Value[0].InnerParameters.Add(new PlainTextParameter() { Value = "1e keuze" });
            param.Value[1].InnerParameters.Add(new PlainTextParameter() { Value = "2e keuze" });
            param.Value[2].InnerParameters.Add(new PlainTextParameter() { Value = "3e keuze" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0, 1), solution,1)
               .ToList();
            //Assert
            Assert.AreEqual(1, result.Count);
            var vm = (InlineChoiceBlockRowViewModel)result.First();
            Assert.AreEqual("C", vm.DisplayValue);
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ChangeValueShouldUpdateDisplayValue()
        {
            //Arrange
            var solution = new Solution();
            solution.Findings.Add(new KeyFinding("Opgave"));
            solution.Findings[0].Facts.Add(new KeyFact("B-InlineA"));
            var param = new InlineChoiceScoringParameter() { FindingOverride = "Opgave", InlineId = "InlineA", Label = "Roman", Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });

            param.Value[0].InnerParameters.Add(new PlainTextParameter() { Value = "1e keuze" });
            param.Value[1].InnerParameters.Add(new PlainTextParameter() { Value = "2e keuze" });
            param.Value[2].InnerParameters.Add(new PlainTextParameter() { Value = "3e keuze" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
               .ToList();

            Assert.AreEqual(1, result.Count);
            var vm = (InlineChoiceBlockRowViewModel)result.First();
            vm.Value.DataValue = "C";
            
            Assert.AreEqual("C", vm.DisplayValue);
        }    
    }
}
