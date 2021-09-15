
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class GapMatchBlockRowViewModelTests
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ChoiceValuesShouldNotBeEscaped()
        {
            //Arrange
            var item = _assessmentItem.To<AssessmentItem>();
            var parameters = item.Parameters.DeepFetchInlineScoringParameters();
            var param = parameters.OfType<GapMatchScoringParameter>().First();

            //Act
            var combinedScoringMap = param.AsCombinedScoringMap(/*GroupIdentifier*/new[] { 0 });
            var result = BlockRowViewModelFactory.CreateInstances(combinedScoringMap, item.Solution, 0).ToList();

            //Assert
            var vm = (GapMatchBlockRowViewModel)result.First();
            Assert.AreEqual(string.Empty, vm.Choices.DataValue[0].Value);
            Assert.AreEqual("<", vm.Choices.DataValue[1].Value);
            Assert.AreEqual(">", vm.Choices.DataValue[2].Value);
            Assert.AreEqual("&", vm.Choices.DataValue[3].Value);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void NoScoreIsSupportedGapMatch()
        {
            //Arrange
            var item = _assessmentItemNoValueCorrect.To<AssessmentItem>();
            var parameters = item.Parameters.DeepFetchInlineScoringParameters();
            var param = parameters.OfType<GapMatchScoringParameter>().First();

            //Act
            var manipulator = param.GetScoreManipulator(item.Solution);
            var vm1 = new GapMatchBlockRowViewModel(param, manipulator, "I4e556201-0c3f-4e62-896c-ec45409ad1b6");
            var vm2 = new GapMatchBlockRowViewModel(param, manipulator, "If29a4b35-6cb4-4b97-ba86-43d91ef87eb8");

            //Assert
            Assert.AreEqual(GapComparisonType.NoValue, vm1.ComparisonType.DataValue);
            Assert.AreEqual(GapComparisonType.Equals, vm2.ComparisonType.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void NoScoreIsSupportedGapMatchRichText()
        {
            //Arrange
            var item = _assessmentItemNoValueCorrectRichText.To<AssessmentItem>();
            var parameters = item.Parameters.DeepFetchInlineScoringParameters();
            var param = parameters.OfType<GapMatchRichTextScoringParameter>().First();

            //Act
            var manipulator = param.GetScoreManipulator(item.Solution);
            var vm1 = new GapMatchBlockRowViewModel(param, manipulator, "I4e556201-0c3f-4e62-896c-ec45409ad1b6");
            var vm2 = new GapMatchBlockRowViewModel(param, manipulator, "If29a4b35-6cb4-4b97-ba86-43d91ef87eb8");

            //Assert
            Assert.AreEqual(GapComparisonType.NoValue, vm1.ComparisonType.DataValue);
            Assert.AreEqual(GapComparisonType.Equals, vm2.ComparisonType.DataValue);
        }


        #region Data

        private readonly XElement _assessmentItem =
                    XElement.Parse(@"
            <assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""22230"" title=""22230"" layoutTemplateSrc=""Cito.Generic.GapMatch.Inline.SC"">
              <solution>
                <keyFindings>
                  <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
                    <keyFact id=""I4e556201-0c3f-4e62-896c-ec45409ad1b6-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""I4e556201-0c3f-4e62-896c-ec45409ad1b6"" occur=""1"">
                        <stringValue>
                          <typedValue>A</typedValue>
                        </stringValue>
                      </keyValue>
                    </keyFact>
                    <keyFact id=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8"" occur=""1"">
                        <stringValue>
                          <typedValue>B</typedValue>
                        </stringValue>
                      </keyValue>
                    </keyFact>
                  </keyFinding>
                </keyFindings>
                <aspectReferences />
                <ItemScoreTranslationTable>
                  <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                  <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                </ItemScoreTranslationTable>
              </solution>
              <parameters>
                <parameterSet id=""styling"">
                  <listedparameter name=""itemStyle"">Default</listedparameter>
                </parameterSet>
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
                  <xhtmlparameter name=""itemBody"" />
                  <xhtmlparameter name=""itemQuestion"" />
                  <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
                    <subparameterset id=""A"">
                      <gapTextParameter name=""gapText"" matchMax=""1"">&lt;</gapTextParameter>
                    </subparameterset>
                    <subparameterset id=""B"">
                      <gapTextParameter name=""gapText"" matchMax=""1"">&gt;</gapTextParameter>
                    </subparameterset>
                    <subparameterset id=""C"">
                      <gapTextParameter name=""gapText"" matchMax=""1"">&amp;</gapTextParameter>
                    </subparameterset>
                    <subparameterset id=""D"">
                      <gapTextParameter name=""gapText"" matchMax=""1"">?</gapTextParameter>
                    </subparameterset>
                    <definition id="""">
                      <gapTextParameter name=""gapText"" matchMax=""1"" />
                    </definition>
                    <xhtmlParameter name=""gapMatchInlineInput"">
              <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">Als A <cito:InlineElement id=""I4e556201-0c3f-4e62-896c-ec45409ad1b6"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" inlineFO="""" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:plaintextparameter name=""inlineGapMatchId"">I4e556201-0c3f-4e62-896c-ec45409ad1b6</cito:plaintextparameter>
                  <cito:plaintextparameter name=""inlineGapMatchLabel"">gat1</cito:plaintextparameter>
                  <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                  <cito:integerparameter name=""width"" />
                  <cito:integerparameter name=""height"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement> B dan <cito:InlineElement id=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" inlineFO="""" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:plaintextparameter name=""inlineGapMatchId"">If29a4b35-6cb4-4b97-ba86-43d91ef87eb8</cito:plaintextparameter>
                  <cito:plaintextparameter name=""inlineGapMatchLabel"">gat2</cito:plaintextparameter>
                  <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                  <cito:integerparameter name=""width"" />
                  <cito:integerparameter name=""height"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement></p>
            </xhtmlParameter>
                  </gapMatchScoringParameter>
                  <xhtmlparameter name=""itemInlineInput"" />
                  <xhtmlparameter name=""itemGeneral"" />
                  <collectionparameter name=""choices"">
                    <definition id="""">
                      <plaintextparameter name=""choice"" />
                      <integerparameter name=""nrOfConnections"" />
                    </definition>
                  </collectionparameter>
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

        private readonly XElement _assessmentItemNoValueCorrect =
            XElement.Parse(@"
            <assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""22230"" title=""22230"" layoutTemplateSrc=""Cito.Generic.GapMatch.Inline.SC"">
              <solution>
                <keyFindings>
                  <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
                    <keyFact id=""I4e556201-0c3f-4e62-896c-ec45409ad1b6-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""I4e556201-0c3f-4e62-896c-ec45409ad1b6"" occur=""1"">
                        <noValue>
                        </noValue>
                      </keyValue>
                    </keyFact>
                    <keyFact id=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8"" occur=""1"">
                        <stringValue>
                          <typedValue>B</typedValue>
                        </stringValue>
                      </keyValue>
                    </keyFact>
                  </keyFinding>
                </keyFindings>
                <aspectReferences />
                <ItemScoreTranslationTable>
                  <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                  <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                </ItemScoreTranslationTable>
              </solution>
              <parameters>
                <parameterSet id=""styling"">
                  <listedparameter name=""itemStyle"">Default</listedparameter>
                </parameterSet>
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
                  <xhtmlparameter name=""itemBody"" />
                  <xhtmlparameter name=""itemQuestion"" />
                  <gapMatchScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
                    <subparameterset id=""A"">
                      <gapTextParameter name=""gapText"" matchMax=""1"">&lt;</gapTextParameter>
                    </subparameterset>
                    <subparameterset id=""B"">
                      <gapTextParameter name=""gapText"" matchMax=""1"">&gt;</gapTextParameter>
                    </subparameterset>
                    <subparameterset id=""C"">
                      <gapTextParameter name=""gapText"" matchMax=""1"">&amp;</gapTextParameter>
                    </subparameterset>
                    <subparameterset id=""D"">
                      <gapTextParameter name=""gapText"" matchMax=""1"">?</gapTextParameter>
                    </subparameterset>
                    <definition id="""">
                      <gapTextParameter name=""gapText"" matchMax=""1"" />
                    </definition>
                    <xhtmlParameter name=""gapMatchInlineInput"">
              <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">Als A <cito:InlineElement id=""I4e556201-0c3f-4e62-896c-ec45409ad1b6"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" inlineFO="""" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:plaintextparameter name=""inlineGapMatchId"">I4e556201-0c3f-4e62-896c-ec45409ad1b6</cito:plaintextparameter>
                  <cito:plaintextparameter name=""inlineGapMatchLabel"">gat1</cito:plaintextparameter>
                  <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                  <cito:integerparameter name=""width"" />
                  <cito:integerparameter name=""height"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement> B dan <cito:InlineElement id=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" inlineFO="""" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:plaintextparameter name=""inlineGapMatchId"">If29a4b35-6cb4-4b97-ba86-43d91ef87eb8</cito:plaintextparameter>
                  <cito:plaintextparameter name=""inlineGapMatchLabel"">gat2</cito:plaintextparameter>
                  <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                  <cito:integerparameter name=""width"" />
                  <cito:integerparameter name=""height"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement></p>
            </xhtmlParameter>
                  </gapMatchScoringParameter>
                  <xhtmlparameter name=""itemInlineInput"" />
                  <xhtmlparameter name=""itemGeneral"" />
                  <collectionparameter name=""choices"">
                    <definition id="""">
                      <plaintextparameter name=""choice"" />
                      <integerparameter name=""nrOfConnections"" />
                    </definition>
                  </collectionparameter>
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

        private readonly XElement _assessmentItemNoValueCorrectRichText =
            XElement.Parse(@"
            <assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""22230"" title=""22230"" layoutTemplateSrc=""Cito.Generic.GapMatch.Inline.SC"">
              <solution>
                <keyFindings>
                  <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
                    <keyFact id=""I4e556201-0c3f-4e62-896c-ec45409ad1b6-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""I4e556201-0c3f-4e62-896c-ec45409ad1b6"" occur=""1"">
                        <noValue>
                        </noValue>
                      </keyValue>
                    </keyFact>
                    <keyFact id=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8"" occur=""1"">
                        <stringValue>
                          <typedValue>B</typedValue>
                        </stringValue>
                      </keyValue>
                    </keyFact>
                  </keyFinding>
                </keyFindings>
                <aspectReferences />
                <ItemScoreTranslationTable>
                  <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                  <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                </ItemScoreTranslationTable>
              </solution>
              <parameters>
                <parameterSet id=""styling"">
                  <listedparameter name=""itemStyle"">Default</listedparameter>
                </parameterSet>
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
                  <xhtmlparameter name=""itemBody"" />
                  <xhtmlparameter name=""itemQuestion"" />
                  <gapMatchRichTextScoringParameter name=""gapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"">
                    <subparameterset id=""A"">
                      <gapTextRichTextParameter name=""gapText"" matchMax=""1"">&lt;</gapTextRichTextParameter>
                    </subparameterset>
                    <subparameterset id=""B"">
                      <gapTextRichTextParameter name=""gapText"" matchMax=""1"">&gt;</gapTextRichTextParameter>
                    </subparameterset>
                    <subparameterset id=""C"">
                      <gapTextRichTextParameter name=""gapText"" matchMax=""1"">&amp;</gapTextRichTextParameter>
                    </subparameterset>
                    <subparameterset id=""D"">
                      <gapTextRichTextParameter name=""gapText"" matchMax=""1"">?</gapTextRichTextParameter>
                    </subparameterset>
                    <definition id="""">
                      <gapTextRichTextParameter name=""gapText"" matchMax=""1"" />
                    </definition>
                    <xhtmlParameter name=""gapMatchInlineInput"">
              <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">Als A <cito:InlineElement id=""I4e556201-0c3f-4e62-896c-ec45409ad1b6"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" inlineFO="""" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:plaintextparameter name=""inlineGapMatchId"">I4e556201-0c3f-4e62-896c-ec45409ad1b6</cito:plaintextparameter>
                  <cito:plaintextparameter name=""inlineGapMatchLabel"">gat1</cito:plaintextparameter>
                  <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                  <cito:integerparameter name=""width"" />
                  <cito:integerparameter name=""height"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement> B dan <cito:InlineElement id=""If29a4b35-6cb4-4b97-ba86-43d91ef87eb8"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"" inlineFO="""" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:plaintextparameter name=""inlineGapMatchId"">If29a4b35-6cb4-4b97-ba86-43d91ef87eb8</cito:plaintextparameter>
                  <cito:plaintextparameter name=""inlineGapMatchLabel"">gat2</cito:plaintextparameter>
                  <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                  <cito:integerparameter name=""width"" />
                  <cito:integerparameter name=""height"" />
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement></p>
            </xhtmlParameter>
                  </gapMatchRichTextScoringParameter>
                  <xhtmlparameter name=""itemInlineInput"" />
                  <xhtmlparameter name=""itemGeneral"" />
                  <collectionparameter name=""choices"">
                    <definition id="""">
                      <plaintextparameter name=""choice"" />
                      <integerparameter name=""nrOfConnections"" />
                    </definition>
                  </collectionparameter>
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

        #endregion
    }
}
