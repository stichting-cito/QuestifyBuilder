
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class DecimalBlockRowViewModelFactoryTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithSingleParamCollection_NoSolution_CreatesSingleBlockRowVM()
        {
            var solution = new Solution();
            var param = new DecimalScoringParameter() { Value = new ParameterSetCollection() };

            param.Value.Add(new ParameterCollection() { Id = "A" });
            CombinedScoringMapKey combinedKey = param.AsCombinedScoringMap();
            var result = BlockRowViewModelFactory.CreateInstances(combinedKey, solution)
                .ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreatedTypeIs_DecimalBlockRowViewModel()
        {
            var solution = new Solution();
            var param = new DecimalScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
    .ToList();
            Assert.IsInstanceOfType(result.First(), typeof(DecimalBlockRowViewModel));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithThreeParamCollection_NoSolution_CreatesThreeBlockRowVM()
        {
            var solution = new Solution();
            var param = new DecimalScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
    .ToList();
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void GetAllBlockRowViewModelsFromFactSet0()
        {
            var solution = Deserialize<Solution>(Data);
            var param = new DecimalScoringParameter() { FindingOverride = "sharedDecimalFinding", ControllerId = "decimalScore" }.AddSubParameters("1", "2");
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
    .ToList();
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0].Name.EndsWith(".1"));
            Assert.IsTrue(result[1].Name.EndsWith(".1"));
            Assert.IsTrue(result[2].Name.EndsWith(".1"));
            Assert.IsTrue(result[3].Name.EndsWith(".2"));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void InsertingBlockRowViewModelUpdatesSolution()
        {
            var solution = Deserialize<Solution>(Data);
            var param = new DecimalScoringParameter() { FindingOverride = "sharedDecimalFinding", ControllerId = "decimalScore" }.AddSubParameters("1", "2");
            var viewModels = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0).ToList();
            var result = BlockRowViewModelFactory.InsertInstance(param, viewModels[1].ScoreKey, 0, 1, solution);
            Assert.AreEqual(2, result.Index);

            KeyValue kv = (KeyValue)(solution.Findings[0].KeyFactsets[0].Facts.First(x => x.Id == "1-decimalScore").Values.First()); Assert.AreEqual(4, kv.Values.Count);

            Assert.IsNull(kv.Values[result.Index]);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoreParameterPropertiesAreCopiedToBlockRowView()
        {
            var solution = Deserialize<Solution>(Data);
            var param = new DecimalScoringParameter() { FindingOverride = "sharedDecimalFinding", ControllerId = "decimalScore", IntegerPartMaxLength = 5, FractionPartMaxLength = 3 }.AddSubParameters("1", "2");
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
    .ToList();

            foreach (IBlockRowViewModel brvw in result)
            {
                DecimalBlockRowViewModel dbrvm = (DecimalBlockRowViewModel)(brvw);
                Assert.AreEqual(5, dbrvm.IntegerPartMaxLength.DataValue);
                Assert.AreEqual(3, dbrvm.FractionPartMaxLength.DataValue);
            }
        }



        public static XElement Data =
            XElement.Parse(@"<solution>
                                <keyFindings>
                                  <keyFinding id=""sharedDecimalFinding"" scoringMethod=""Dichotomous"">
                                    <keyFactSet>
                                      <keyFact id=""1-decimalScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""decimalScore"" occur=""1"">
                                          <decimalValue>
                                            <typedValue>6.0</typedValue>
                                          </decimalValue>
                                          <decimalValue>
                                            <typedValue>7.0</typedValue>
                                          </decimalValue>
                                          <decimalValue>
                                            <typedValue>8.0</typedValue>
                                          </decimalValue>
                                        </keyValue>
                                      </keyFact>
                                      <keyFact id=""2-decimalScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""decimalScore"" occur=""1"">
                                          <decimalValue>
                                            <typedValue>14.0</typedValue>
                                          </decimalValue>
                                        </keyValue>
                                      </keyFact>
                                    </keyFactSet>        
                                  </keyFinding>
                                </keyFindings>
                                <aspectReferences />                                            
                                </solution>");


        private T Deserialize<T>(XElement input)
        {
            T ret = default(T);
            var s = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var m = new System.IO.StringReader(input.ToString()))
            {
                ret = (T)s.Deserialize(m);
            }

            return ret;
        }




    }
}
