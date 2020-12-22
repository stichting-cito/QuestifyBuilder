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
    public class HotTextCorrectionBlockRowViewModelFactoryTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithSingleParamCollection_NoSolution_CreatesSingleBlockRowVM()
        {
            var solution = new Solution();

            var param = new HotTextCorrectionScoringParameter() { Name = "hottextcorrection", ControllerId = "HTC", CorrectionIsApplicable = true };
            param.RelatedControlLabelParameter = new PlainTextParameter() { Name = "someplaintextpar", Value = "some text" };

            CombinedScoringMapKey combinedKey = param.AsCombinedScoringMap();
            var result = BlockRowViewModelFactory.CreateInstances(combinedKey, solution)
                .ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreatedTypeIs_HotTextCorrectionBlockRowViewModel()
        {
            var solution = new Solution();
            ParameterCollection pc = new ParameterCollection() { Id = "guid123" };
            pc.InnerParameters.Add(new PlainTextParameter() { Name = HotTextScoringParameter.ContentLabel, Value = "label content text" });

            var param = new HotTextCorrectionScoringParameter() { Name = "hottextcorrection", ControllerId = "HTC" };
            param.Value = new ParameterSetCollection();
            param.Value.Add(pc);

            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
    .ToList();
            Assert.IsInstanceOfType(result.First(), typeof(HotTextCorrectionBlockRowViewModel));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void BlockRowViewModelCreatedViaInsertingIsOfExpectedType()
        {
            var solution = Deserialize<Solution>(SolutionData);
            var htcparam = new HotTextCorrectionScoringParameter() { FindingOverride = "sharedHotTextFinding", ControllerId = "HTC", InlineId = "guid123", CorrectionIsApplicable = true };
            htcparam.RelatedControlLabelParameter = new PlainTextParameter() { Name = "controlLabel", Value = "some label" };

            var viewModels = BlockRowViewModelFactory.CreateInstances(htcparam.AsCombinedScoringMap(0), solution, 0).ToList();
            var result = BlockRowViewModelFactory.InsertInstance(htcparam, viewModels[0].ScoreKey, 0, 0, solution);
            Assert.IsInstanceOfType(result, typeof(HotTextCorrectionBlockRowViewModel));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void TheControlLabelOfTheInlineElementsBecomeTheNameOfTheViewModel()
        {
            var solution = new Solution();
            var htcparam = new HotTextCorrectionScoringParameter() { FindingOverride = "sharedHotTextFinding", ControllerId = "HTC", CorrectionIsApplicable = true };
            htcparam.RelatedControlLabelParameter = new PlainTextParameter() { Name = "controlLabel", Value = "some text" };

            var result = BlockRowViewModelFactory.CreateInstances(htcparam.AsCombinedScoringMap(0), solution, 0)
    .ToList();

            Assert.AreEqual("some text", ((HotTextCorrectionBlockRowViewModel)result[0]).Name);
        }

        private static XElement SolutionData =
            XElement.Parse(@"<solution>
                                <keyFindings>
                                  <keyFinding id=""sharedOrderFinding"" scoringMethod=""Dichotomous"">
                                      <keyFactSet>
                                          <keyFact id=""Id123-HTC"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                            <keyValue domain=""HTC"" occur=""1"">
                                              <stringValue>
                                                <typedValue>correctie op Id123</typedValue>
                                              </stringValue>
                                            </keyValue>
                                          </keyFact>
                                          <keyFact id=""Id456-HTC"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                            <keyValue domain=""HTC"" occur=""1"">
                                              <stringValue>
                                                <typedValue>correctie op Id456</typedValue>
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
