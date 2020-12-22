
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class EncodingScoringVMTest_WithSpecificData2 : UsesTheItemEditorVM
    {
        private AssessmentItem _currentAssessmentItem;

        public EncodingScoringVMTest_WithSpecificData2()
        {
            AddAttributteInitializer<SetAssessmentItemAttribute>(
                att => DealWith_SetAssessmentItemAttribute(att as SetAssessmentItemAttribute));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [SetAssessmentItem(itemWithEncodedText)]
        public void AvailableParamsNamesAreHtmlDecoded()
        {
            var encodingViewModel = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            var availableParams = encodingViewModel.AvailableParams.ToList();
            Assert.AreEqual(1, availableParams.Count);

            Assert.AreEqual("De firma V&D heeft in het jaar 2015 zware probleme...", availableParams[0].Value);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _currentAssessmentItem = null;
        }

        private void DealWith_SetAssessmentItemAttribute(SetAssessmentItemAttribute setAssessmentItemAttribute)
        {
            _currentAssessmentItem = setAssessmentItemAttribute.GetAssessmentItem();
        }

        internal override void SetFakeViewDataContext(ref IWorkSpaceAware fakeView, IItemEditorViewModel itemEditorViewModel)
        {
            Debug.Assert(_currentAssessmentItem != null, "Please use the SetAssessmentItemAttribute");
            var prms = _currentAssessmentItem.Parameters.FlattenParameters().OfType<ScoringParameter>().ToList();

            fakeView.WorkSpaceContextualData.DataValue =
                new Tuple<IEnumerable<ScoringParameter>, Solution, IItemEditorViewModel>(prms, _currentAssessmentItem.Solution, itemEditorViewModel);
        }

        protected override IEnumerable<ComposablePartCatalog> GetTypesForInjection()
        {
            return new[] { MyComposer.NoExportTypes() };
        }


        const string itemWithEncodedText = @"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""TI_HotText"" title=""TI_HotText"" layoutTemplateSrc=""ilt.hottext"">
              <solution>
                <keyFindings />
                <aspectReferences />
              </solution>
              <parameters>
                <parameterSet id=""invoer"">
                  <plaintextparameter name=""foo"" />
                  <integerparameter name=""maxChoicesParam"">0</integerparameter>
                  <booleanparameter name=""isHotTextCorrectionItem"">False</booleanparameter>
                  <xhtmlparameter name=""hottexttext"">
              <p id=""c1-id-8"" xmlns=""http://www.w3.org/1999/xhtml"">
                <cito:InlineElement id=""I8dbf1e8f-28e5-4546-b1f2-c7fb0bb377bd"" layoutTemplateSourceName=""ilt.inline.hottext"" inlineFO="""" xmlns:cito=""http://www.cito.nl/citotester"">
              <cito:parameters>
                <cito:parameterSet id=""entireItem"">
                  <cito:listedparameter name=""controlType"">hottext</cito:listedparameter>
                  <cito:plaintextparameter name=""controlId"">I8dbf1e8f-28e5-4546-b1f2-c7fb0bb377bd</cito:plaintextparameter>
                  <cito:plaintextparameter name=""controlLabel"">De firma V&amp;D heeft in het jaar 2015 zware probleme...</cito:plaintextparameter>
                  <cito:booleanparameter name=""addHottextCorrection"">False</cito:booleanparameter>
                  <cito:plaintextparameter name=""hottextValue"" />
                  <cito:hotTextCorrectionScoringParameter name=""domainHotTextCorrection"" ControllerId=""hottexcorrection"" findingOverride=""hottextController"" expectedLength=""0"" correctionIsApplicable=""false"">
                    <cito:definition id="""" />
                    <cito:relatedControlLabel name=""controlLabel"">De firma V&amp;D heeft in het jaar 2015 zware probleme...</cito:relatedControlLabel>
                  </cito:hotTextCorrectionScoringParameter>
                </cito:parameterSet>
              </cito:parameters>
            </cito:InlineElement>
                <span id=""SI8dbf1e8f-28e5-4546-b1f2-c7fb0bb377bd"" style=""background-color: #C7B8CE;"">De firma V&amp;D heeft in het jaar 2015 zware problemen ondervonden.</span>
              </p>
            </xhtmlparameter>
                  <hotTextScoringParameter name=""domainHotText"" ControllerId=""hottext"" findingOverride=""hottextController"" minChoices=""1"" maxChoices=""0"" multiChoice=""Check"" isCorrectionVariant=""false"">
                    <subparameterset id=""I8dbf1e8f-28e5-4546-b1f2-c7fb0bb377bd"">
                      <plaintextparameter name=""contentLabel"">De firma V&amp;D heeft in het jaar 2015 zware probleme...</plaintextparameter>
                    </subparameterset>
                    <definition id="""">
                      <plaintextparameter name=""contentLabel"" />
                    </definition>
                  </hotTextScoringParameter>
                  <plaintextparameter name=""bar"" />
                </parameterSet>
              </parameters>
            </assessmentItem>";

    }
}
