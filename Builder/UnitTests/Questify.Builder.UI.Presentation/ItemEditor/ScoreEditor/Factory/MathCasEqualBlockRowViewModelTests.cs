
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.IoC;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using IMathMlEditorPlugin = Questify.Builder.Logic.IMathMlEditorPlugin;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor.Factory
{
    [TestClass]
    public class MathCasEqualBlockRowViewModelTests
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void NoScoreIsSupportedGapMatch()
        {
            IMathMlEditorPlugin mathMlEditorPlugin = A.Fake<IMathMlEditorPlugin>();

            IoCHelper.Init(new List<IMathMlEditorPlugin>{mathMlEditorPlugin});
            PluginHelper.MathMlPlugin = IoCHelper.GetInstances<IMathMlEditorPlugin>().FirstOrDefault();

            var theCall = A.CallTo(() => mathMlEditorPlugin.RenderPng(A<string>.Ignored));
            theCall.ReturnsLazily(args => GetMathMlImg());

            //Arrange
            var item = _assessmentItemAllValuesCorrect.To<AssessmentItem>();
            var parameters = item.Parameters.DeepFetchInlineScoringParameters();
            var param = parameters.OfType<MathCasEqualScoringParameter>().ToList();
            var param2 = parameters.OfType<CasEqualStepsScoringParameter>().ToList();
            //Act
            var manipulator1 = param[0].GetScoreManipulator(item.Solution);
            var manipulator2 = param2[0].GetScoreManipulator(item.Solution);
            var manipulator3 = param[1].GetScoreManipulator(item.Solution);
            var vm1 = new MathCasEqualBlockRowViewModel(param[0], manipulator1, "First", 0);
            var vm3 = new MathCasEqualBlockRowViewModel(param[1], manipulator3, "Last", 0);
            var vm2 = new BooleanBlockRowViewModel(param2[0], manipulator2, "Second", 0);
            
            //Assert
            Assert.AreEqual(GapComparisonType.EqualEquation, vm1.ComparisonType.DataValue);
            Assert.AreEqual(GapComparisonType.Equals, vm2.ComparisonType.DataValue);
            Assert.AreEqual(GapComparisonType.Equals, vm3.ComparisonType.DataValue);
        }

        private byte[] GetMathMlImg()
        {
            return Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAACEAAAASCAYAAADVCrdsAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABt0lEQVRIie3Uz4uNYRQH8M+duQwbTBaEEWEyG8WKFFlMZmMoNUUp2zFFs7GQUjKyUpqGspHEZv4FC4WmpFlQLCxs2Ew0RWH8mGvxnDtz7zvvO/Pemh2nTs+v73nP9/me87z8t2SVFvEj2L3MHMarLYB3oYZLy0zicyvgm+jM7G3Lwa3EPnQrVroprqwSW/EF09iAMziMLZGwFrhNQfY1eoPEiYjrwikcweoYa9BWksQQbsf8J57jmYU3vYw3Uu/0YTuuxdkMnmAy+/EyJDbiD6ZiPY0JfMvB/sbRmP+IhHtjPYUXsd9kZcoxhLESOLhgXp2K1BePC7D1Es4pUcUN7M8A16MDH0uSqGE25gNYhasF2LlS1pW4joPowfEG4DncWSRpUff34DwO4dNScXUlLkrv/5gkIazBOrxvkcRmXEE/PuBsWRLwFC8xHOtB3F2EQB6JTtzDOA5IJektEddkA/iOHRgtwHThPl7hKx7hdJw9kPqi0UfibGfEvZVe2EOczEtQxbvwPQUkKmiPsSKp2djgKzLetlRceybBLH5hLW4VkKDheZm/cT0+61lsXtwC65B+zf+W/QU0JFY71brlJwAAAABJRU5ErkJggg==");
        }
        
        #region Data

        private readonly XElement _assessmentItemAllValuesCorrect =
            XElement.Parse(@"
<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""Test Met Alle Opties Aan"" title=""Test Met Alle Opties Aan"" layoutTemplateSrc=""Cito.Generic.Gaps.Inline.DC"">
  <solution>
    <keyFindings>
      <keyFinding id=""gapController"" scoringMethod=""Dichotomous"">
        <keyFact id=""First-Ib381f21b-87ea-41f1-9349-a35d73dc1312"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""Ib381f21b-87ea-41f1-9349-a35d73dc1312"" occur=""1"">
            <stringComparisonValue>
              <typedComparisonValue>&lt;math xmlns=""http://www.w3.org/1998/Math/MathML""&gt;&lt;apply&gt;&lt;times&gt;&lt;/times&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;ci&gt;X&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedComparisonValue>
              <comparisonType>EqualEquation</comparisonType>
            </stringComparisonValue>
          </keyValue>
        </keyFact>
        <keyFact id=""Last-Ib381f21b-87ea-41f1-9349-a35d73dc1312"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""Ib381f21b-87ea-41f1-9349-a35d73dc1312"" occur=""1"">
            <stringValue>
              <typedValue>&lt;math xmlns=""http://www.w3.org/1998/Math/MathML""&gt;&lt;apply&gt;&lt;divide&gt;&lt;/divide&gt;&lt;ci&gt;X&lt;/ci&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
            </stringValue>
          </keyValue>
        </keyFact>
        <keyFact id=""Second-Ib381f21b-87ea-41f1-9349-a35d73dc1312"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
          <keyValue domain=""Ib381f21b-87ea-41f1-9349-a35d73dc1312"" occur=""1"">
            <booleanValue>
              <typedValue>true</typedValue>
            </booleanValue>
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
    <parameterSet id=""entireItem"">    
      <listedparameter name=""inlineType"">input</listedparameter>
      <plaintextparameter name=""inlineClass"" />
      <integerparameter name=""maxChoices"">0</integerparameter>
      <booleanparameter name=""dualColumnLayout"">True</booleanparameter>
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
      <xhtmlparameter name=""leftItemInlineInput"" />
      <integerparameter name=""leftItemInlineInputWidth"">0</integerparameter>
      <xhtmlresourceparameter name=""leftSource"" />
      <resourceparameter name=""wordSourceText"" />
      <integerparameter name=""sourceHeight"">200</integerparameter>
      <integerparameter name=""sourcePositionTop"">0</integerparameter>
      <xhtmlparameter name=""itemBody"" />
      <xhtmlparameter name=""itemQuestion"" />
      <xhtmlparameter name=""itemInlineInput"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml"">
          <cito:InlineElement id=""Ib381f21b-87ea-41f1-9349-a35d73dc1312"" layoutTemplateSourceName=""InlineGapFormulaCasEqualLayoutTemplate"" inlineFO="""" xmlns:cito=""http://www.cito.nl/citotester"">
  <cito:parameters>
    <cito:parameterSet id=""entireItem"">
      <cito:plaintextparameter name=""gapId"">Ib381f21b-87ea-41f1-9349-a35d73dc1312</cito:plaintextparameter>
      <cito:plaintextparameter name=""gapLabel"">Test Met Alleen Equal Equation</cito:plaintextparameter>
      <cito:plaintextparameter name=""controlType"">input</cito:plaintextparameter>
      <cito:listedparameter name=""gapType"">Formula</cito:listedparameter>
      <cito:plaintextparameter name=""gapMaskCharacter"">_</cito:plaintextparameter>
      <cito:booleanparameter name=""hasMathMLScoring"">True</cito:booleanparameter>
      <cito:mathMLParameter name=""initialMathML"" />
      <cito:booleanparameter name=""multiLineMathML"">True</cito:booleanparameter>
      <cito:booleanparameter name=""cursorOnNewLine"">True</cito:booleanparameter>
      <cito:listedparameter name=""editorControlPreSet"">all</cito:listedparameter>
      <cito:mathCasEqualScoringParameter name=""mathCasEqualScoringFirst"" ControllerId=""firstMathScore"" findingOverride=""gapController"">
        <cito:subparameterset id=""First"">
          <cito:booleanparameter name=""fictiveMathML"">True</cito:booleanparameter>
        </cito:subparameterset>
        <cito:definition id="""">
          <cito:booleanparameter name=""fictiveMathML"" />
        </cito:definition>
      </cito:mathCasEqualScoringParameter>
      <cito:casEqualStepsScoringParameter name=""casEqualSteps"" ControllerId=""casEqualStepsScore"" findingOverride=""gapController"">
        <cito:subparameterset id=""Second"">
          <cito:booleanparameter name=""fictiveString"">True</cito:booleanparameter>
        </cito:subparameterset>
        <cito:definition id="""">
          <cito:booleanparameter name=""fictiveString"" />
        </cito:definition>
      </cito:casEqualStepsScoringParameter>
      <cito:mathCasEqualScoringParameter name=""mathCasEqualScoringLast"" ControllerId=""lastMathScore"" findingOverride=""gapController"">
        <cito:subparameterset id=""Last"">
          <cito:booleanparameter name=""fictiveMathML"">True</cito:booleanparameter>
        </cito:subparameterset>
        <cito:definition id="""">
          <cito:booleanparameter name=""fictiveMathML"" />
        </cito:definition>
      </cito:mathCasEqualScoringParameter>
    </cito:parameterSet>
  </cito:parameters>
</cito:InlineElement> </p>
      </xhtmlparameter>
      <xhtmlparameter name=""itemGeneral"">
        <p id=""c1-id-11"" xmlns=""http://www.w3.org/1999/xhtml""> </p>
      </xhtmlparameter>
      <collectionparameter name=""choices"">
        <definition id="""">
          <plaintextparameter name=""choice"" />
          <integerparameter name=""nrOfConnections"" />
        </definition>
      </collectionparameter>
      <booleanparameter name=""autoScoring"">True</booleanparameter>
    </parameterSet>
    <parameterSet id=""kenmerken"">
      <booleanparameter name=""showCalculatorButton"">False</booleanparameter>
      <integerparameter name=""hightOfScrollText"">200</integerparameter>
      <integerparameter name=""fixedWidthMatrixColumn"">100</integerparameter>
      <booleanparameter name=""showChoicesBottomLayout"" />
      <integerparameter name=""fixedHeightAlternatives"">0</integerparameter>
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
  </parameters>
</assessmentItem>");

        #endregion
    }
}
