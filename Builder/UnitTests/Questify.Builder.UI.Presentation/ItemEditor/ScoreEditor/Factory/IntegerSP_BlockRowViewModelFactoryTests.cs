
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
    public class IntegerSP_BlockRowViewModelFactoryTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithSingleParamCollection_NoSolution_CreatesSingleBlockRowVM()
        {
            var solution = new Solution();
            var param = new IntegerScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            CombinedScoringMapKey combinedKey = param.AsCombinedScoringMap();
            var result = BlockRowViewModelFactory.CreateInstances(combinedKey, solution)
                .ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreatedTypeIs_IntegerBlockRowViewModel()
        {
            var solution = new Solution();
            var param = new IntegerScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
    .ToList();
            Assert.IsInstanceOfType(result.First(), typeof(IntegerBlockRowViewModel));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithThreeParamCollection_NoSolution_CreatesThreeBlockRowVM()
        {
            var solution = new Solution();
            var param = new IntegerScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
    .ToList();
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void GetAllBlokRowViewModelsFromFactSet0()
        {
            var solution = Deserialize<Solution>(Data);
            var param = new IntegerScoringParameter() { FindingOverride = "sharedIntegerFinding", ControllerId = "integerScore" }.AddSubParameters("1", "2");
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
    .ToList();
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0].Name.EndsWith(".1"));
            Assert.IsTrue(result[1].Name.EndsWith(".1"));
            Assert.IsTrue(result[2].Name.EndsWith(".1"));
            Assert.IsTrue(result[3].Name.EndsWith(".2"));
        }



        public static XElement Data =
            XElement.Parse(@"<solution>
                                <keyFindings>
                                  <keyFinding id=""sharedIntegerFinding"" scoringMethod=""Dichotomous"">
                                    <keyFactSet>
                                      <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""integerScore"" occur=""1"">
                                          <integerValue>
                                            <typedValue>6</typedValue>
                                          </integerValue>
                                          <integerValue>
                                            <typedValue>7</typedValue>
                                          </integerValue>
                                          <integerValue>
                                            <typedValue>8</typedValue>
                                          </integerValue>
                                        </keyValue>
                                      </keyFact>
                                      <keyFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""integerScore"" occur=""1"">
                                          <integerValue>
                                            <typedValue>14</typedValue>
                                          </integerValue>
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
