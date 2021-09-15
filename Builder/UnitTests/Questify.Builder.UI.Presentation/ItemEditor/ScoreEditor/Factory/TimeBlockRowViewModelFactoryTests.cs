
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
    public class TimeBlockRowViewModelFactoryTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithSingleParamCollection_NoSolution_CreatesSingleBlockRowVM()
        {
            //Arrange
            var solution = new Solution();
            var param = new TimeScoringParameter() { Value = new ParameterSetCollection() };
            
            param.Value.Add(new ParameterCollection() { Id = "A" });
            //Act
            CombinedScoringMapKey combinedKey = param.AsCombinedScoringMap();
            var result = BlockRowViewModelFactory.CreateInstances(combinedKey, solution)
                .ToList();
            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreatedTypeIs_TimeBlockRowViewModel()
        {
            //Arrange
            var solution = new Solution();
            var param = new TimeScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            //Act
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
                .ToList();
            //Assert
            Assert.IsInstanceOfType(result.First(), typeof(TimeBlockRowViewModel));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithThreeParamCollection_NoSolution_CreatesThreeBlockRowVM()
        {
            //Arrange
            var solution = new Solution();
            var param = new TimeScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            param.Value.Add(new ParameterCollection() { Id = "B" });
            param.Value.Add(new ParameterCollection() { Id = "C" });
            //Act
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
                .ToList();
            //Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void GetAllBlockRowViewModelsFromFactSet0()
        {
            //Arrange
            var solution = Deserialize<Solution>(Data);
            var param = new TimeScoringParameter() { FindingOverride = "sharedTimeFinding", ControllerId = "timeScore" }.AddSubParameters("1", "2");
            //Act
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
                .ToList();
            //Assert
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result[0].Name.EndsWith(".1"));
            Assert.IsTrue(result[1].Name.EndsWith(".1"));
            Assert.IsTrue(result[2].Name.EndsWith(".1"));
            Assert.IsTrue(result[3].Name.EndsWith(".2"));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void InsertingBlockRowViewModelUpdatesSolution()
        {
            //Arrange
            var solution = Deserialize<Solution>(Data);
            var param = new TimeScoringParameter() { FindingOverride = "sharedTimeFinding", ControllerId = "timeScore" }.AddSubParameters("1", "2");
            var viewModels = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0).ToList();
            //Act
            var result = BlockRowViewModelFactory.InsertInstance(param, viewModels[1].ScoreKey, 0, 1, solution);
            //Assert
            Assert.AreEqual(2, result.Index);

            KeyValue kv = (KeyValue)(solution.Findings[0].KeyFactsets[0].Facts.First(x => x.Id == "1-timeScore").Values.First()); // We cannot use an array indexer on Facts to get the fact we need 
                                                                                                                                  // because the position of the fact has changed due to solution manipulation.
            Assert.AreEqual(4, kv.Values.Count);

            Assert.AreEqual(string.Empty, kv.Values[result.Index].ToString());  // The value associated with the inserted block row view should be empty.
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoreParameterPropertiesAreCopiedToBlockRowView()
        {
            //Arrange
            var solution = Deserialize<Solution>(Data);
            var param = new TimeScoringParameter() { FindingOverride = "sharedTimeFinding", ControllerId = "timeScore",  TimeFormat = "hh:mm" }.AddSubParameters("1", "2");
            //Act
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
                .ToList();

            //Assert
            foreach (IBlockRowViewModel brvw in result)
            {
                TimeBlockRowViewModel tbrvm = (TimeBlockRowViewModel)(brvw);
                Assert.AreEqual("hh:mm", tbrvm.TimeFormat);
            }
        }


        public static XElement Data = 
            XElement.Parse(@"<solution>
                                <keyFindings>
                                  <keyFinding id=""sharedTimeFinding"" scoringMethod=""Dichotomous"">
                                    <keyFactSet>
                                      <keyFact id=""1-timeScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""timeScore"" occur=""1"">
                                          <stringValue>
                                            <typedValue>06:00</typedValue>
                                          </stringValue>
                                          <stringValue>
                                            <typedValue>07:00</typedValue>
                                          </stringValue>
                                          <stringValue>
                                            <typedValue>08:00</typedValue>
                                          </stringValue>
                                        </keyValue>
                                      </keyFact>
                                      <keyFact id=""2-timeScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""timeScore"" occur=""1"">
                                          <stringValue>
                                            <typedValue>14:00</typedValue>
                                          </stringValue>
                                        </keyValue>
                                      </keyFact>
                                    </keyFactSet>        
                                  </keyFinding>
                                </keyFindings>
                                <aspectReferences />                                            
                                </solution>");


        private T Deserialize<T>( XElement input)
        {
            T ret = default(T);
            var s = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using( var m = new System.IO.StringReader(input.ToString()))
            {
                ret = (T)s.Deserialize(m);
            }

            return ret;
        }

    }
}
