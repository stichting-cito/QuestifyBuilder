
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class GraphGapMatchBlockRowViewModelFactoryTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void NoScoreIsSupportedGraphicGapMatch()
        {
            var item = _assessmentItemNoValueCorrect.To<AssessmentItem>();
            var parameters = item.Parameters.DeepFetchInlineScoringParameters();
            var param = parameters.OfType<GraphGapMatchScoringParameter>().First();

            var manipulator = param.GetScoreManipulator(item.Solution);
            var vm1 = new GraphGapMatchBlockRowViewModel(param, manipulator, "A");
            var vm2 = new GraphGapMatchBlockRowViewModel(param, manipulator, "B");

            Assert.AreEqual(GapComparisonType.NoValue, vm1.ComparisonType.DataValue);
            Assert.AreEqual(GapComparisonType.Equals, vm2.ComparisonType.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void NoScoreIsSupportedGraphicGapMatchVariant2()
        {
            var item = _assessmentItemNoValueCorrectVariant2.To<AssessmentItem>();
            var parameters = item.Parameters.DeepFetchInlineScoringParameters();
            var param = parameters.OfType<GraphGapMatchScoringParameter>().First();

            var manipulator = param.GetScoreManipulator(item.Solution);
            var vm1 = new GraphGapMatchVar2BlockRowViewModel(param, manipulator, "A");
            var vm2 = new GraphGapMatchVar2BlockRowViewModel(param, manipulator, "B");

            Assert.AreEqual(GapComparisonType.NoValue, vm1.ComparisonType.DataValue);
            Assert.AreEqual(GapComparisonType.Equals, vm2.ComparisonType.DataValue);
        }


        private readonly XElement _assessmentItemNoValueCorrect =
            XElement.Parse(@"
            <assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""gdgdg"" itemid=""3228w3"" title=""fgdgdfg"" layoutTemplateSrc=""Cito.Generic.GraphicGapMatch.SC"">
              <solution>
                <keyFindings>
                  <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
                    <keyFact id=""A-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""A-gapMatchController"" occur=""1"">
                        <noValue />
                      </keyValue>
                    </keyFact>
                    <keyFact id=""B-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""B-gapMatchController"" occur=""1"">
                        <stringValue>
                          <typedValue>C</typedValue>
                        </stringValue>
                      </keyValue>
                    </keyFact>
                  </keyFinding>
                </keyFindings>
                <conceptFindings />
                <aspectReferences />
                <ItemScoreTranslationTable>
                  <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                  <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                </ItemScoreTranslationTable>
              </solution>
              <parameters>
                <parameterSet id=""kenmerken"">
                  <booleanparameter name=""showCalculatorButton"">False</booleanparameter>
                  <integerparameter name=""hightOfScrollText"">200</integerparameter>
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
                  <listedparameter name=""itemLanguage"">nl-NL</listedparameter>
                </parameterSet>
                <parameterSet id=""entireItem"">
                  <booleanparameter name=""dualColumnLayout"">False</booleanparameter>
                  <booleanparameter name=""isCategorizationItem"">False</booleanparameter>
                  <booleanparameter name=""showCalculatorButton"" />
                  <xhtmlparameter name=""leftBody"" />
                  <xhtmlresourceparameter name=""leftSource"" />
                  <integerparameter name=""sourceHeight"">200</integerparameter>
                  <integerparameter name=""sourcePositionTop"">0</integerparameter>
                  <xhtmlparameter name=""itemBody"">
                    <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">bladifd ffhf </p>
                  </xhtmlparameter>
                  <graphGapMatchScoringParameter name=""graphicGapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"" iscategorizationvariant=""false"">
                    <subparameterset id=""A"">
                      <gapImageParameter name=""alternative"" matchMax=""1"" width=""0"" height=""0"" contentType=""Image"" enteredText="""">UK.jpg</gapImageParameter>
                    </subparameterset>
                    <subparameterset id=""B"">
                      <gapImageParameter name=""alternative"" matchMax=""1"" width=""0"" height=""0"" contentType=""Text"" enteredText=""a"" />
                    </subparameterset>
                    <subparameterset id=""C"">
                      <gapImageParameter name=""alternative"" matchMax=""1"" width=""0"" height=""0"" contentType=""Text"" enteredText=""c"" />
                    </subparameterset>
                    <definition id="""">
                      <gapImageParameter name=""alternative"" matchMax=""1"" width=""0"" height=""0"" contentType=""Image"" />
                    </definition>
                    <areaparameter name=""itemQuestionArea"">
                      <subparameterset id=""A"">
                        <resourceparameter name=""clickableImage"">UK.jpg</resourceparameter>
                      </subparameterset>
                      <definition id="""">
                        <resourceparameter name=""clickableImage"" />
                      </definition>
                      <Shapes>
                        <Circle id=""A"" label=""A"" radius=""52"">
                          <AnchorPoint>
                            <X>86</X>
                            <Y>139</Y>
                          </AnchorPoint>
                        </Circle>
                        <Circle id=""B"" label=""B"" radius=""43"">
                          <AnchorPoint>
                            <X>149</X>
                            <Y>58</Y>
                          </AnchorPoint>
                        </Circle>
                      </Shapes>
                    </areaparameter>
                  </graphGapMatchScoringParameter>
                  <areaparameter name=""itemQuestionArea"">
                    <definition id="""">
                      <resourceparameter name=""clickableImage"" />
                    </definition>
                    <Shapes />
                  </areaparameter>
                  <integerparameter name=""nrOfAnswers"">1</integerparameter>
                  <collectionparameter name=""numberOfImages"">
                    <definition id="""">
                      <resourceparameter name=""imgSource"" />
                      <plaintextparameter name=""imgSourceText"" />
                      <xhtmlparameter name=""imgSourceFormula"" />
                      <booleanparameter name=""editSize"" />
                      <integerparameter name=""width"" />
                      <integerparameter name=""height"" />
                      <integerparameter name=""nrOfConnections"" />
                    </definition>
                  </collectionparameter>
                  <listedparameter name=""itemOrientation"">vertical</listedparameter>
                  <listedparameter name=""nrAlternativesPerLine"">1</listedparameter>
                  <integerparameter name=""percentageTransparency"">0</integerparameter>
                  <xhtmlparameter name=""itemGeneral"">
                    <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
                  </xhtmlparameter>
                </parameterSet>
              </parameters>
            </assessmentItem>");

        private readonly XElement _assessmentItemNoValueCorrectVariant2 =
            XElement.Parse(@"
            <assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""TI_GGM_Variant2"" itemid="""" title=""TI_GGM_Variant2"" layoutTemplateSrc=""Cito.Generic.GraphicGapMatch.Categorize.SC"">
              <solution>
                <keyFindings>
                  <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
                    <keyFact id=""A-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""A-gapMatchController"" occur=""1"">
                        <noValue />
                      </keyValue>
                    </keyFact>
                    <keyFact id=""B-gapMatchController"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                      <keyValue domain=""B-gapMatchController"" occur=""1"">
                        <stringValue>
                          <typedValue>A</typedValue>
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
                <parameterSet id=""kenmerken"">
                  <booleanparameter name=""showCalculatorButton"">False</booleanparameter>
                  <integerparameter name=""hightOfScrollText"">200</integerparameter>
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
                  <booleanparameter name=""protractorEnableRotation"">True</booleanparameter>
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
                  <listedparameter name=""itemLanguage"">nl-NL</listedparameter>
                </parameterSet>
                <parameterSet id=""entireItem"">
                  <booleanparameter name=""dualColumnLayout"">False</booleanparameter>
                  <booleanparameter name=""isCategorizationItem"">True</booleanparameter>
                  <booleanparameter name=""showCalculatorButton"" />
                  <xhtmlparameter name=""leftBody"" />
                  <xhtmlresourceparameter name=""leftSource"" />
                  <integerparameter name=""sourceHeight"">200</integerparameter>
                  <integerparameter name=""sourcePositionTop"">0</integerparameter>
                  <xhtmlparameter name=""itemBody"" />
                  <graphGapMatchScoringParameter name=""graphicGapMatchScoring"" ControllerId=""gapMatchController"" findingOverride=""gapMatchController"" iscategorizationvariant=""true"">
                    <subparameterset id=""A"">
                      <gapImageParameter name=""alternative"" matchMax=""1"" width=""0"" height=""0"" contentType=""Text"" enteredText=""a"" />
                    </subparameterset>
                    <subparameterset id=""B"">
                      <gapImageParameter name=""alternative"" matchMax=""1"" width=""0"" height=""0"" contentType=""Text"" enteredText=""b"" />
                    </subparameterset>
                    <definition id="""">
                      <gapImageParameter name=""alternative"" matchMax=""1"" width=""0"" height=""0"" contentType=""Image"" />
                    </definition>
                    <areaparameter name=""itemQuestionArea"">
                      <subparameterset id=""A"">
                        <resourceparameter name=""clickableImage"">UK.jpg</resourceparameter>
                      </subparameterset>
                      <definition id="""">
                        <resourceparameter name=""clickableImage"" />
                      </definition>
                      <Shapes>
                        <Rectangle id=""A"" label=""A"">
                          <TopLeft>
                            <X>1</X>
                            <Y>1</Y>
                          </TopLeft>
                          <BottomRight>
                            <X>101</X>
                            <Y>251</Y>
                          </BottomRight>
                        </Rectangle>
                        <Rectangle id=""B"" label=""B"">
                          <TopLeft>
                            <X>101</X>
                            <Y>1</Y>
                          </TopLeft>
                          <BottomRight>
                            <X>201</X>
                            <Y>251</Y>
                          </BottomRight>
                        </Rectangle>
                      </Shapes>
                    </areaparameter>
                  </graphGapMatchScoringParameter>
                  <areaparameter name=""itemQuestionArea"">
                    <definition id="""">
                      <resourceparameter name=""clickableImage"" />
                    </definition>
                    <Shapes />
                  </areaparameter>
                  <integerparameter name=""nrOfAnswers"">1</integerparameter>
                  <collectionparameter name=""numberOfImages"">
                    <definition id="""">
                      <resourceparameter name=""imgSource"" />
                      <plaintextparameter name=""imgSourceText"" />
                      <xhtmlparameter name=""imgSourceFormula"" />
                      <booleanparameter name=""editSize"" />
                      <integerparameter name=""width"" />
                      <integerparameter name=""height"" />
                      <integerparameter name=""nrOfConnections"" />
                    </definition>
                  </collectionparameter>
                  <listedparameter name=""itemOrientation"">vertical</listedparameter>
                  <listedparameter name=""nrAlternativesPerLine"">1</listedparameter>
                  <integerparameter name=""percentageTransparency"">0</integerparameter>
                  <xhtmlparameter name=""itemGeneral"" />
                </parameterSet>
              </parameters>
            </assessmentItem>");

    }
}
