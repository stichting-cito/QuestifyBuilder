
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UnitTests.Fakes;
using Constants = Questify.Builder.UI.Wpf.Presentation.ItemEditor.Constants;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class V2ScoringBaseTests : UsesTheItemEditorVM
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(IntegerScoringParameter))]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void UsingMediatorShouldNotKeepStuff()
        {
            var target = new TestV2ScoreClass(new TestViewAwareStatus());
            var weakRef = new WeakReference<TestV2ScoreClass>(target);
            target = null;
            GC.Collect();
            TestV2ScoreClass test;
            var gotIt = weakRef.TryGetTarget(out test);
            Assert.IsFalse(gotIt);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddParameter(typeof(IntegerScoringParameter))]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void Mediator_SolutionGroupChanged_IsCalled()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Mediator.Instance.NotifyColleagues(Constants.SolutionGroupChanged, true);
            var result = target.HandleSolutionGroupChangedCalled;
            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddXhtmlParameter(XhtmlWithoutInlineElements)]
        [AddXhtmlParameter(XhtmlWithInlineStringScoringParameter)]
        public void Mediator_AutoScoringChanged_ToFalse_AspectScoringPrmAdded()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            var nrOfAspectScoringPrmsBefore = target.ScoringParameters.Count(s => s is AspectScoringParameter);
            Mediator.Instance.NotifyColleagues(Constants.AutoScoringChanged, false);
            var autoScoringChangedCalled = target.HandleAutoScoringChangedCalled;
            var nrOfAspectScoringPrmsAfter = target.ScoringParameters.Count(s => s is AspectScoringParameter);
            Assert.IsTrue(autoScoringChangedCalled);
            Assert.AreEqual(0, nrOfAspectScoringPrmsBefore);
            Assert.AreEqual(1, nrOfAspectScoringPrmsAfter);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [SetAssessmentItem(TestItem)]
        [AddParameter(typeof(StringScoringParameter), ControllerID = "MyController", FindingOverride = "mc", Name = "stringScoringPrm")]
        [AddAspectScoringParameter(AutoScoringOffPrm = true, ControllerID = "MyController", FindingOverride = "mc", Name = "aspectScoringPrm", PrmCollToAddToId = "autoScoring", PrmCollToAddToIsDynCollection = true)]
        public void Mediator_AutoScoringChanged_ToTrue_AspectScoringPrmRemoved()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            AddResource("TestItem", TestItem, ResourceEntityType.Item);
            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            var nrOfAspectScoringPrmsBefore = target.ScoringParameters.Count(s => s is AspectScoringParameter);
            Mediator.Instance.NotifyColleagues(Constants.AutoScoringChanged, true);
            var autoScoringChangedCalled = target.HandleAutoScoringChangedCalled;
            var nrOfAspectScoringPrmsAfter = target.ScoringParameters.Count(s => s is AspectScoringParameter);
            Assert.IsTrue(autoScoringChangedCalled);
            Assert.AreEqual(1, nrOfAspectScoringPrmsBefore);
            Assert.AreEqual(0, nrOfAspectScoringPrmsAfter);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddXhtmlParameter(XhtmlWithInlineStringScoringParameter)]
        public void EnrichScoringParametersWithDesignerSettingsForPreprocessingRulesTest()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.IsTrue(target.ScoringParameters.Any(s => s.DesignerSettings.Count > 0));
            Assert.IsTrue(target.ScoringParameters.Any(s => s.DesignerSettings.Any(d => d.Key == "PreprocessRules")));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddXhtmlParameter(XhtmlWithoutInlineElements)]
        [AddXhtmlParameter(XhtmlWithInlineStringScoringParameter)]
        public void EnrichScoringParametersWithDesignerSettingsForPreprocessingRulesXhtmlWithAndWithoutStringInlineElement()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);

            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.IsTrue(target.ScoringParameters.Any(s => s.DesignerSettings.Count > 0));
            Assert.IsTrue(target.ScoringParameters.Any(s => s.DesignerSettings.Any(d => d.Key == "PreprocessRules")));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddXhtmlParameter(XhtmlWithInlineImage)]
        [AddXhtmlParameter(XhtmlWithInlineStringScoringParameter)]
        public void EnrichScoringParametersWithDesignerSettingsForPreprocessingRulesXhtmlWithAndWithoutStringInlineElementWithInlineImage()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.IsTrue(target.ScoringParameters.Any(s => s.DesignerSettings.Count > 0));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        public void EnrichScoringParametersWithDesignerSettingsNoXhtml()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(target.ScoringParameters.Count(), 0);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddXhtmlParameter(XhtmlWithoutInlineElements)]
        public void EnrichScoringParametersWithDesignerSettingsXhtmlNoInline()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(target.ScoringParameters.Count(), 0);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddXhtmlParameter(XhtmlWithInlineImage)]
        [AddXhtmlParameter(XhtmlWithoutInlineElements)]
        public void EnrichScoringParametersWithDesignerSettingsXhtmlWithoutInlineString()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(target.ScoringParameters.Count(), 0);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [AddXhtmlParameter(XhtmlWithInlineImage)]
        [AddXhtmlParameter(XhtmlWithoutInlineElements)]
        [AddXhtmlParameter(XhtmlWithInlineStringScoringParameter)]
        public void EnrichScoringParametersWithDesignerSettingsXhtmlAllTypes()
        {
            var target = new TestV2ScoreClass(ViewAwareStatus);
            AddResource("InlineGapStringLayoutTemplate", InlineGapStringLayoutTemplate, ResourceEntityType.ItemTemplate);
            AddResource("InlineGap.String", InlineGapStringControlTemplate, ResourceEntityType.ControlTemplate);
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.IsTrue(target.ScoringParameters.Any(s => s.DesignerSettings.Count > 0));
        }

        internal override void SetFakeViewDataContext(ref Cinch.IWorkSpaceAware fakeView, global::Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.IItemEditorViewModel itemEditorViewModel)
        {
            var collectionParameter = new CollectionParameter();

            fakeView.WorkSpaceContextualData.DataValue = itemEditorViewModel;
        }

        protected override IEnumerable<ComposablePartCatalog> GetTypesForInjection()
        {
            return Enumerable.Empty<ComposablePartCatalog>();
        }

        public const string XhtmlWithInlineStringScoringParameter = @"<XHtmlParameter xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" name=""itemQuestion"">
	                                                            <p id=""c1-id-8"">text<cito:InlineElement id=""I00971962-9e5b-472d-9a6c-4c723a38d949"" layoutTemplateSourceName=""InlineGapStringLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
			                                                            <cito:parameters>
				                                                            <cito:parameterSet id=""entireItem"">
					                                                            <cito:plaintextparameter name=""gapId"">I00971962-9e5b-472d-9a6c-4c723a38d949</cito:plaintextparameter>
					                                                            <cito:plaintextparameter name=""gapLabel"">interactie1</cito:plaintextparameter>
					                                                            <cito:plaintextparameter name=""controlType"">input</cito:plaintextparameter>
					                                                            <cito:listedparameter name=""gapType"">String</cito:listedparameter>
					                                                            <cito:listedparameter name=""autoInputProcessing"">None</cito:listedparameter>
					                                                            <cito:integerparameter name=""nrOfCharacters"">5</cito:integerparameter>
					                                                            <cito:plaintextparameter name=""gapMaskCharacter"">_</cito:plaintextparameter>
					                                                            <cito:stringScoringParameter name=""stringScoring"" label=""interactie1"" ControllerId=""gapController"" findingOverride=""gapController"" expectedLength=""5"">
						                                                            <cito:subparameterset id=""1"">
							                                                            <cito:booleanparameter name=""fictiveString"">True</cito:booleanparameter>
						                                                            </cito:subparameterset>
						                                                            <cito:definition id="""">
							                                                            <cito:booleanparameter name=""fictiveString""/>
						                                                            </cito:definition>
					                                                            </cito:stringScoringParameter>
				                                                            </cito:parameterSet>
			                                                            </cito:parameters>
		                                                            </cito:InlineElement>
	                                                            </p>
                                                            </XHtmlParameter>";

        public const string XhtmlWithoutInlineElements = @"<XHtmlParameter xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" name=""bodyLeft"">
	                                                            <p id=""c1-id-8"">some text</p>
                                                            </XHtmlParameter>";

        public const string XhtmlWithInlineImage = @"<XHtmlParameter xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" name=""bodyLeft"">
	                                                            <p id=""c1-id-8"">some text<cito:InlineElement id=""5e360048-6481-494e-bb28-d80f8eecfc81"" layoutTemplateSourceName=""InlineImageLayoutTemplate"" xmlns:cito=""http://www.cito.nl/citotester"">
	                                                                <cito:parameters>
		                                                                <cito:parameterSet id=""entireItem"">
			                                                                <cito:resourceparameter name=""source"">Image.png</cito:resourceparameter>
			                                                                <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
			                                                                <cito:integerparameter name=""width"">193</cito:integerparameter>
			                                                                <cito:integerparameter name=""height"">136</cito:integerparameter>
			                                                                <cito:booleanparameter name=""useBorder"">False</cito:booleanparameter>
		                                                                </cito:parameterSet>
	                                                                </cito:parameters>
                                                                </cito:InlineElement> </p>
                                                            </XHtmlParameter>";

        public const string TestItem =
            @"<assessmentItem xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" identifier=""2001"" title=""2001"" layoutTemplateSrc=""ilt.customInteraction"">
                                                              <solution>
                                                                <keyFindings />
                                                                <aspectReferences />
                                                              </solution>
                                                              <parameters>
                                                                <parameterSet id=""invoer"">
                                                                  <stringScoringParameter ControllerId=""gapController"" name=""stringScoring"" findingOverride=""gapController"">
                                                                    <subparameterset id=""X"" />
                                                                  </stringScoringParameter>
                                                                </parameterSet>
                                                                <parameterSet id=""autoscoring"" isDynamicCollection=""true"">
                                                                  <aspectScoringParameter name=""aspectScoring"">
                                                                    <subparameterset id=""X"" />
                                                                  </aspectScoringParameter>
                                                                </parameterSet>
                                                              </parameters>                                                            
                                                            </assessmentItem>";

        public const string InlineGapStringLayoutTemplate = @"<Template xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" definitionVersion=""1"">
	                                                            <Description>Dit is een test omschrijving voor ItemLayoutTemplate</Description>
	                                                            <Targets>
		                                                            <Target xsi:type=""ItemLayoutTemplateTarget"" enabled=""true"" name=""ces"">
			                                                            <Description>TestPlayer 1.x / 2.x</Description>
			                                                            <Template><![CDATA[
				                                                                    <html xmlns:cito=""http://www.cito.nl/citotester"">
                                                                                    <cito:control id=""entireItem"" type=""InlineGap.String"" />
                                                                                        </html>
			                                                                    ]]></Template>
		                                                            </Target>
	                                                            </Targets>
                                                            </Template>";

        public const string InlineGapStringControlTemplate =
            @"<Template xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" definitionVersion=""1"">
                                                            <Description/>
                                                            <Targets>
                                                                <Target xsi:type=""ControlTemplateTarget"" enabled=""true"" name=""ces"">
                                                                    <Description/>
                                                                    <Template><![CDATA[
				                                                    ]]></Template>
                                                                </Target>
                                                            </Targets>
                                                            <SharedParameterSet>
                                                                <plaintextparameter name=""gapLabel"">testlabel</plaintextparameter>
                                                                <integerparameter name=""nrOfCharacters"">15</integerparameter>
                                                                <stringScoringParameter ControllerId=""gapController"" name=""stringScoring"" findingOverride=""gapController"">
                                                                    <attributereference name=""label"">gapLabel</attributereference>
                                                                    <attributereference name=""expectedLength"">nrOfCharacters</attributereference>
                                                                    <designersetting key=""PreprocessRules"">(ConvertToLower,ConvertYToIJ,RemoveAllSpaces,RemoveApostrophs,RemoveDiacritics,RemoveHyphens)</designersetting>
                                                                    <designersetting key=""visible"">true</designersetting>
                                                                    <designersetting key=""minimumLength"">1</designersetting>
                                                                    <designersetting key=""maximumLength"">1</designersetting>
                                                                    <definition>
                                                                        <booleanparameter name=""fictiveString"">
                                                                            <designersetting key=""label""/>
                                                                            <designersetting key=""description""/>
                                                                            <designersetting key=""defaultvalue"">true</designersetting>
                                                                        </booleanparameter>
                                                                    </definition>
                                                                </stringScoringParameter>
                                                            </SharedParameterSet>
                                                        </Template>";

        private class TestV2ScoreClass : V2ScoringBase
        {

            public bool HandleSolutionGroupChangedCalled { get; set; }

            public bool HandleAutoScoringChangedCalled { get; set; }

            public TestV2ScoreClass(IViewAwareStatus viewAwareStatusService)
                : base(viewAwareStatusService)
            {

            }

            public new void EnrichScoringParametersWithDesignerSettings()
            {
                base.EnrichScoringParametersWithDesignerSettings();
            }

            public override void OnViewIsLoaded()
            {

            }

            protected override void OnHandleSolutionGroupChanged(bool fixRemovedScoringPrms)
            {
                HandleSolutionGroupChangedCalled = true;
                base.OnHandleSolutionGroupChanged(fixRemovedScoringPrms);
            }

            protected override void OnHandleAutoScoringChanged(bool autoScoring)
            {
                HandleAutoScoringChangedCalled = true;
                base.OnHandleAutoScoringChanged(autoScoring);
            }
        }
    }
}
