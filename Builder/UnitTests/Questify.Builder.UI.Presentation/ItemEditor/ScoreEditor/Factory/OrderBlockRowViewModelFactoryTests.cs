
using System;
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
    public class OrderBlockRowViewModelFactoryTests
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithSingleParamCollection_NoSolution_CreatesSingleBlockRowVM()
        {
            var solution = new Solution();
            var param = new OrderScoringParameter() { Value = new ParameterSetCollection() };

            param.Value.Add(new ParameterCollection() { Id = "A" });
            CombinedScoringMapKey combinedKey = param.AsCombinedScoringMap();
            var result = BlockRowViewModelFactory.CreateInstances(combinedKey, solution)
                .ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CreatedTypeIs_OrderBlockRowViewModel()
        {
            var solution = new Solution();
            var param = new OrderScoringParameter() { Value = new ParameterSetCollection() };
            param.Value.Add(new ParameterCollection() { Id = "A" });
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(), solution)
    .ToList();
            Assert.IsInstanceOfType(result.First(), typeof(OrderBlockRowViewModel));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ScoringParameterWithThreeParamCollection_NoSolution_CreatesThreeBlockRowVM()
        {
            var solution = new Solution();
            var param = new OrderScoringParameter() { Value = new ParameterSetCollection() };
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
            var solution = _data.To<Solution>();
            var param = new OrderScoringParameter() { FindingOverride = "sharedOrderFinding", ControllerId = "OC" }.AddSubParameters("A", "B");
            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
    .ToList();
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0].Name.EndsWith("1"));
            Assert.IsTrue(result[1].Name.EndsWith("2"));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv"), ExpectedException(typeof(NotImplementedException))]
        public void InsertingBlockRowViewModelThrowsException()
        {
            var solution = _data.To<Solution>();
            var param = new OrderScoringParameter() { FindingOverride = "sharedOrderFinding", ControllerId = "OC" }.AddSubParameters("A", "B");
            var viewModels = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0).ToList();
            var result = BlockRowViewModelFactory.InsertInstance(param, viewModels[1].ScoreKey, 0, 1, solution);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void EachScoreSubParameterBecomesAMovableElement()
        {
            var solution = _data.To<Solution>();
            var param = new OrderScoringParameter() { FindingOverride = "sharedOrderFinding", ControllerId = "OC", }.AddSubParameters("A", "B");

            param.Value[0].InnerParameters.Add(new PlainTextParameter() { Name = "elementLabel", Value = "alinea 1" });

            var result = BlockRowViewModelFactory.CreateInstances(param.AsCombinedScoringMap(0), solution, 0)
    .ToList();

            foreach (IBlockRowViewModel brvw in result)
            {
                OrderBlockRowViewModel obrvm = (OrderBlockRowViewModel)(brvw);
                Assert.AreEqual(2, obrvm.MovableElements.DataValue.Count);
            }

            Assert.AreEqual("alinea 1", ((OrderBlockRowViewModel)result[0]).MovableElements.DataValue[0].Value);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void MovableElementValuesShouldNotBeEscaped()
        {
            var item = _data2.To<AssessmentItem>();
            var parameters = item.Parameters.DeepFetchInlineScoringParameters();
            var param = parameters.OfType<OrderScoringParameter>().First();

            var combinedScoringMap = param.AsCombinedScoringMap();
            var result = BlockRowViewModelFactory.CreateInstances(combinedScoringMap, item.Solution, 0).ToList();

            var vm = (OrderBlockRowViewModel)result.First();
            Assert.AreEqual(">", vm.MovableElements.DataValue[0].Value);
            Assert.AreEqual("<", vm.MovableElements.DataValue[1].Value);
            Assert.AreEqual("&", vm.MovableElements.DataValue[2].Value);
        }

        readonly XElement _data =
    XElement.Parse(@"<solution>
                                <keyFindings>
                                  <keyFinding id=""sharedOrderFinding"" scoringMethod=""Dichotomous"">
                                    <keyFactSet>
                                      <keyFact id=""1-OC"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""A-OC"" occur=""1"">
                                          <integerValue>
                                            <typedValue>2</typedValue>
                                          </integerValue>
                                        </keyValue>
                                      </keyFact>
                                      <keyFact id=""2-OC"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                        <keyValue domain=""B-OC"" occur=""1"">
                                          <integerValue>
                                            <typedValue>1</typedValue>
                                          </integerValue>
                                        </keyValue>
                                      </keyFact>
                                    </keyFactSet>        
                                  </keyFinding>
                                </keyFindings>
                                <aspectReferences />                                            
                                </solution>");



        private readonly XElement _data2 =
            XElement.Parse(@" 
            <assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""order"" title=""order"" layoutTemplateSrc=""Cito.Generic.Order.SC"">
              <solution>
                <keyFindings>
                  <keyFinding id=""orderController"" scoringMethod=""Dichotomous"">
                    <keyFactSet>
                      <keyFact id=""A-orderController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                        <keyValue domain=""orderController"" occur=""1"">
                          <integerValue>
                            <typedValue>1</typedValue>
                          </integerValue>
                        </keyValue>
                      </keyFact>
                      <keyFact id=""B-orderController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                        <keyValue domain=""orderController"" occur=""1"">
                          <integerValue>
                            <typedValue>2</typedValue>
                          </integerValue>
                        </keyValue>
                      </keyFact>
                      <keyFact id=""C-orderController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                        <keyValue domain=""orderController"" occur=""1"">
                          <integerValue>
                            <typedValue>3</typedValue>
                          </integerValue>
                        </keyValue>
                      </keyFact>
                    </keyFactSet>
                  </keyFinding>
                </keyFindings>
                <aspectReferences />
                <ItemScoreTranslationTable>
                  <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                  <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                </ItemScoreTranslationTable>
              </solution>
              <parameters>
                <parameterSet id=""entireItem"">
                  <booleanparameter name=""dualColumnLayout"">False</booleanparameter>
                  <booleanparameter name=""showCalculatorButton"" />
                  <booleanparameter name=""displayVerklankingOnTheRight"">True</booleanparameter>
                  <collectionparameter name=""numberOfAudioContentItems"">
                    <definition id="""">
                      <resourceparameter name=""audiocontent"" />
                      <xhtmlparameter name=""description"" />
                    </definition>
                  </collectionparameter>
                  <texttospeechparameter name=""verklankingLinks"" />
                  <texttospeechparameter name=""verklankingRechts"" />
                  <xhtmlparameter name=""leftBody"" />
                  <xhtmlresourceparameter name=""leftSource"" />
                  <integerparameter name=""sourceHeight"">200</integerparameter>
                  <integerparameter name=""sourcePositionTop"">0</integerparameter>
                  <xhtmlparameter name=""itemQuestion"">
              <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">?</p>
            </xhtmlparameter>
                  <listedparameter name=""itemOrientation"">vertical</listedparameter>
                  <collectionparameter name=""choices"">
                    <definition id="""">
                      <xhtmlparameter name=""choice"" />
                    </definition>
                  </collectionparameter>
                  <orderScoringParameter name=""orderScoring"" ControllerId=""orderController"" findingOverride=""orderController"" minChoices=""0"" maxChoices=""0"">
                    <subparameterset id=""A"">
                      <plaintextparameter name=""elementLabel"">&gt;</plaintextparameter>
                      <xhtmlparameter name=""movableElement"">
              <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">&gt;</p>
            </xhtmlparameter>
                    </subparameterset>
                    <subparameterset id=""B"">
                      <plaintextparameter name=""elementLabel"">&lt;</plaintextparameter>
                      <xhtmlparameter name=""movableElement"">
              <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">&lt;</p>
            </xhtmlparameter>
                    </subparameterset>
                    <subparameterset id=""C"">
                      <plaintextparameter name=""elementLabel"">&amp;</plaintextparameter>
                      <xhtmlparameter name=""movableElement"">
              <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">&amp;</p>
            </xhtmlparameter>
                    </subparameterset>
                    <definition id="""">
                      <plaintextparameter name=""elementLabel"" />
                      <xhtmlparameter name=""movableElement"" />
                    </definition>
                  </orderScoringParameter>
                  <booleanparameter name=""widthOfVerticalAlternatives"">False</booleanparameter>
                  <integerparameter name=""buttonWidth"">70</integerparameter>
                  <integerparameter name=""buttonHeight"">25</integerparameter>
                  <xhtmlparameter name=""itemGeneral"" />
                </parameterSet>
                <parameterSet id=""kenmerken"">
                  <booleanparameter name=""showCalculatorButton"">False</booleanparameter>
                  <integerparameter name=""hightOfScrollText"" />
                  <integerparameter name=""fixedWidthMatrixColumn"">100</integerparameter>
                  <booleanparameter name=""showChoicesBottomLayout"" />
                  <integerparameter name=""fixedHeightAlternatives"">0</integerparameter>
                  <booleanparameter name=""showGroup"">False</booleanparameter>
                  <plaintextparameter name=""calculatorDescription"" />
                  <listedparameter name=""calculatorMode"">basic</listedparameter>
                  <booleanparameter name=""showReset"" />
                  <booleanparameter name=""showNotepad"" />
                  <plaintextparameter name=""notepadDescription"" />
                  <booleanparameter name=""showSymbolPicker"" />
                  <plaintextparameter name=""symbolPickerDescription"" />
                  <plaintextparameter name=""symbols"" />
                  <booleanparameter name=""showRuler"" />
                  <plaintextparameter name=""rulerDescription"" />
                  <plaintextparameter name=""rulerStartValue"" />
                  <plaintextparameter name=""rulerEndValue"" />
                  <plaintextparameter name=""rulerStepValue"" />
                  <integerparameter name=""rulerStart"" />
                  <integerparameter name=""rulerEnd"" />
                  <integerparameter name=""rulerStep"" />
                  <integerparameter name=""rulerStepSize"" />
                  <listedparameter name=""rulerLengthUnit"">centimeter</listedparameter>
                  <booleanparameter name=""showProtractor"" />
                  <plaintextparameter name=""protractorPickerDescription"" />
                  <booleanparameter name=""protractorEnableRotation"" />
                  <booleanparameter name=""showTriangle"" />
                  <plaintextparameter name=""trianglePickerDescription"" />
                  <integerparameter name=""triangleMinDegrees"" />
                  <integerparameter name=""triangleMaxDegrees"" />
                  <booleanparameter name=""showSpellCheck"">False</booleanparameter>
                  <listedparameter name=""spellCheckCulture"">nl-NL</listedparameter>
                  <booleanparameter name=""showFormulaList"">False</booleanparameter>
                  <plaintextparameter name=""formulaListDescription"" />
                  <listedparameter name=""formulaType"">default</listedparameter>
                  <booleanparameter name=""showTextMarker"">False</booleanparameter>
                  <plaintextparameter name=""textMarkerDescription"" />
                </parameterSet>
              </parameters>
            </assessmentItem>");


    }
}
