
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using MEFedMVVM.ViewModelLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class StringBlockRowViewModelFactoryTests
    {
        [TestInitialize]
        public void Init()
        {
            LocatorBootstrapper.ApplyComposer(new MyComposer(new[] { MyComposer.GetItemEditorContext() }));

            typeof(ViewModelRepository).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithSingleParamCollection_NoSolution_CreatesSingleBlockRowVM()
        {
            var solution = new Solution();
            var param = new StringScoringParameter() { Value = new ParameterSetCollection() };

            param.Value.Add(new ParameterCollection() { Id = "A" });
            CombinedScoringMapKey combinedKey = param.AsCombinedScoringMap();
            var result = BlockRowViewModelFactory.CreateInstances(combinedKey, solution)
                .ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreatedTypeIs_StringBlockRowViewModel()
        {
            var solution = new Solution();
            var param = new StringScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
    .ToList();
            Assert.IsInstanceOfType(result.First(), typeof(StringBlockRowViewModel));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithThreeParamCollection_NoSolution_CreatesThreeBlockRowVM()
        {
            var solution = new Solution();
            var param = new StringScoringParameter() { Value = new ParameterSetCollection() };
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
            var param = new StringScoringParameter() { FindingOverride = "sharedStringFinding", ControllerId = "stringScore" }.AddSubParameters("1", "2");
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
            var param = new StringScoringParameter() { FindingOverride = "sharedStringFinding", ControllerId = "stringScore" }.AddSubParameters("1", "2");
            var viewModels = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0).ToList();
            var result = BlockRowViewModelFactory.InsertInstance(param, viewModels[1].ScoreKey, 0, 1, solution);
            Assert.AreEqual(2, result.Index);

            KeyValue kv = (KeyValue)(solution.Findings[0].KeyFactsets[0].Facts.First(x => x.Id == "1-stringScore").Values.First()); Assert.AreEqual(4, kv.Values.Count);

            Assert.AreEqual(string.Empty, kv.Values[result.Index].ToString());
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoreParameterPropertiesAreCopiedToBlockRowView()
        {
            var solution = Deserialize<Solution>(Data);
            var param = new StringScoringParameter() { FindingOverride = "sharedStringFinding", ControllerId = "stringScore", ExpectedLength = 6, PatternMask = "abc" }.AddSubParameters("1", "2");

            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
    .ToList();

            foreach (IBlockRowViewModel brvw in result)
            {
                StringBlockRowViewModel sbrvm = (StringBlockRowViewModel)(brvw);
                Assert.AreEqual(6, sbrvm.ExpectedLength);
                Assert.AreEqual("abc", sbrvm.PatternMask);
            }
        }

        public static XElement Data =
            XElement.Parse(@"<solution>
                                <keyFindings>
                                  <keyFinding id=""sharedStringFinding"" scoringMethod=""Dichotomous"">
                                    <keyFactSet>
                                      <keyFact id=""1-stringScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""stringScore"" occur=""1"">
                                          <stringValue>
                                            <typedValue>zes</typedValue>
                                          </stringValue>
                                          <stringValue>
                                            <typedValue>zeven</typedValue>
                                          </stringValue>
                                          <stringValue>
                                            <typedValue>acht</typedValue>
                                          </stringValue>
                                        </keyValue>
                                      </keyFact>
                                      <keyFact id=""2-stringScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""stringScore"" occur=""1"">
                                          <stringValue>
                                            <typedValue>veertien</typedValue>
                                          </stringValue>
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
