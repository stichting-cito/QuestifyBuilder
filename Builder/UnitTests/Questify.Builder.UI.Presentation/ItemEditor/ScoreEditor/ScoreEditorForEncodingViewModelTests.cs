
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class ScoreEditorForEncodingViewModelTests : MVVMTestBase
    {
        private Solution _Solution = null;

        public ScoreEditorForEncodingViewModelTests()
        {
            AddAttributteInitializer<SetSolutionAttribute>(att => DealWith_SetSolutionAttribute(att as SetSolutionAttribute));
        }

        [TestCleanup]
        public void Clean()
        {
            _Solution = null;
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [SetSolution(Defined_Solutions.IntegerFactSetsFactAlternatives.Id)]
        public void ValidateAddingResponseCategoryLogicForSingle()
        {
            var scoringPrms = new[] {
                new IntegerScoringParameter() {FindingOverride="integerScore" , ControllerId="integerScore"}.AddSubParameters("1","2","3","4","5")
            };
            var map = new ScoringMap(scoringPrms, _Solution).GetMap();

            var single = map.Where(mapKey => !mapKey.IsGroup).First();
            single.GetConceptManipulator(_Solution);
            var test = new ScoreEditorForEncodingViewModel(single, _Solution);
            var manipulator = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            manipulator.SetKey("5", 100);

            test.ProcessResult();
            Assert.AreEqual(2 + 1, _Solution.ConceptFindings[0].Facts.Count); Assert.IsTrue(_Solution.ConceptFindings[0].Facts[1].Id.StartsWith("5[*]"));
            Assert.IsTrue(_Solution.ConceptFindings[0].Facts[2].Id.StartsWith("5[1]"));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [SetSolution(Defined_Solutions.IntegerFactSetsFactAlternatives.Id)]
        public void ValidateAddingResponseCategoryLogicForGroup()
        {
            var scoringPrms = new[] {
                new IntegerScoringParameter() {FindingOverride="integerScore" , ControllerId="integerScore"}.AddSubParameters("1","2","3","4","5")
            };
            var map = new ScoringMap(scoringPrms, _Solution).GetMap();

            var single = map.Where(mapKey => mapKey.IsGroup).First();
            single.GetConceptManipulator(_Solution);
            var test = new ScoreEditorForEncodingViewModel(single, _Solution);
            var manipulator = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            manipulator.SetFactSetTarget(0);
            manipulator.SetKey("1", 100);
            manipulator.SetKey("2", 100);

            test.ProcessResult();
            Assert.AreEqual(5 + 1, _Solution.ConceptFindings[0].KeyFactsets.Count); Assert.AreEqual("1[2]-integerScore", _Solution.ConceptFindings[0].KeyFactsets[5].Facts[0].Id);
            Assert.AreEqual("2[2]-integerScore", _Solution.ConceptFindings[0].KeyFactsets[5].Facts[1].Id);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [SetSolution(Defined_Solutions.TwoInlineString.Id)]
        public void AddScoreForTwoScoringParams()
        {
            var scoringPrms = new[] {
                new StringScoringParameter() {InlineId= "Ice32c0ba-73db-456d-b3d3-c92265282cf7" , FindingOverride= "gapController",Name="str 1"}.AddSubParameters("1"),
                new StringScoringParameter() {InlineId= "If62b4c70-e785-4b2d-892f-5ecaea3824a3" , FindingOverride= "gapController",Name="str 2"}.AddSubParameters("1")
            };
            var map = new ScoringMap(scoringPrms, _Solution).GetMap();

            var single = map.Where(mapKey => mapKey.IsGroup).First();
            var test = new ScoreEditorForEncodingViewModel(single, _Solution);

            var m1 = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            m1.SetFactSetTarget(0);
            m1.SetKey("1", "TestValue1");
            var m2 = scoringPrms[1].GetScoreManipulator(test.WorkingSolution);
            m2.SetFactSetTarget(0);
            m2.SetKey("1", "TestValue2");

            test.ProcessResult();
            Assert.AreEqual(2, _Solution.ConceptFindings[0].KeyFactsets.Count);
            Assert.IsTrue(_Solution.ConceptFindings[0].KeyFactsets[1].Facts[0].Id.StartsWith("1[1]-Ice32c0ba"));
            Assert.IsTrue(_Solution.ConceptFindings[0].KeyFactsets[1].Facts[1].Id.StartsWith("1[1]-If62b4c70"));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [SetSolution(Defined_Solutions.TwoInlineString.Id)]
        public void AddScoreForTwoScoringParams_IntegrationTest()
        {
            var scoringPrms = new[] {
                new StringScoringParameter() {InlineId= "Ice32c0ba-73db-456d-b3d3-c92265282cf7" , FindingOverride= "gapController",Name="str 1"}.AddSubParameters("1"),
                new StringScoringParameter() {InlineId= "If62b4c70-e785-4b2d-892f-5ecaea3824a3" , FindingOverride= "gapController",Name="str 2"}.AddSubParameters("1")
            };
            var map = new ScoringMap(scoringPrms, _Solution).GetMap();

            var single = map.Where(mapKey => mapKey.IsGroup).First();
            var test = new ScoreEditorForEncodingViewModel(single, _Solution);

            var m1 = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            m1.SetFactSetTarget(0);
            m1.SetKey("1", "TestValue1");
            var m2 = scoringPrms[1].GetScoreManipulator(test.WorkingSolution);
            m2.SetFactSetTarget(0);
            m2.SetKey("1", "TestValue2");

            test.ProcessResult();
            var result = single.GetConceptManipulator(_Solution).GetDisplayValueForConceptId("1");


            Assert.AreEqual("TestValue1&TestValue2", result);
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [SetSolution(Defined_Solutions.TwoInlineString.Id)]
        public void AddScoreForTwoScoringParams_Fix_IntegrationTest()
        {
            var scoringPrms = new[] {
                new StringScoringParameter() {InlineId= "Ice32c0ba-73db-456d-b3d3-c92265282cf7" , FindingOverride= "gapController",Name="str 1"}.AddSubParameters("1"),
                new StringScoringParameter() {InlineId= "If62b4c70-e785-4b2d-892f-5ecaea3824a3" , FindingOverride= "gapController",Name="str 2"}.AddSubParameters("1")
            };
            var map = new ScoringMap(scoringPrms, _Solution).GetMap();

            var single = map.Where(mapKey => mapKey.IsGroup).First();
            var test = new ScoreEditorForEncodingViewModel(single, _Solution);

            var m1 = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            m1.SetFactSetTarget(0);
            m1.SetKey("1", "TestValue1");
            var m2 = scoringPrms[1].GetScoreManipulator(test.WorkingSolution);
            m2.SetFactSetTarget(0);
            m2.SetKey("1", "TestValue2");

            test.ProcessResult();
            _Solution.FixRemovedScoringParameters(scoringPrms);
            var result = single.GetConceptManipulator(_Solution).GetDisplayValueForConceptId("1");


            Assert.AreEqual("TestValue1&TestValue2", result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [WorkItem(20592)]
        [SetSolution(Defined_Solutions.TwoGroupedMc.Id)]
        public void FixCrashWith2GroupedMC()
        {
            var scoringPrms = new[] {
                new ChoiceScoringParameter()  {FindingOverride = "Opgave", ControllerId = "een", MaxChoices = 1}.AddSubParameters("A", "B", "C"),
                new ChoiceScoringParameter()  {FindingOverride = "Opgave", ControllerId = "twee", MaxChoices = 1}.AddSubParameters("A", "B", "C")
            };
            var map = new ScoringMap(scoringPrms, _Solution).GetMap();

            var single = map.Where(mapKey => mapKey.IsGroup).First();
            single.GetConceptManipulator(_Solution);
            var test = new ScoreEditorForEncodingViewModel(single, _Solution);
            var manipulator = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            manipulator.SetFactSetTarget(0);
            manipulator.SetKey("B");
            manipulator = scoringPrms[1].GetScoreManipulator(test.WorkingSolution);
            manipulator.SetFactSetTarget(0);
            manipulator.SetKey("A");


            test.ProcessResult();
            Assert.AreEqual(3, _Solution.ConceptFindings[0].KeyFactsets.Count); Assert.AreEqual(2, _Solution.ConceptFindings[0].KeyFactsets[2].Facts.Count);
            Assert.AreEqual("B[1]-een", _Solution.ConceptFindings[0].KeyFactsets[2].Facts[0].Id);
            Assert.AreEqual("A[1]-twee", _Solution.ConceptFindings[0].KeyFactsets[2].Facts[1].Id);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [WorkItem(20498)]
        [SetSolution(Defined_Solutions.SingleGapMatch.Id)]
        public void AssertThatFactIdIsCreatedAsIntended()
        {
            var sp = new GapMatchScoringParameter() { FindingOverride = "gapMatchController", Name = "domainX" }.AddSubParameters(
    "1", "2", "3", "4").AddGapTextParameters();
            sp.GapXhtmlParameter = _gapXhtmlParameter.To<XHtmlParameter>();
            sp = sp.Transform();
            var scoringPrms = new[] { sp };

            var map = new ScoringMap(scoringPrms, _Solution).GetMap();
            var single = map.First();

            single.GetConceptManipulator(_Solution); var test = new ScoreEditorForEncodingViewModel(single, _Solution);
            var manipulator = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            manipulator.SetKey("I23a0653b-b574-4d5e-ad66-e05af1a169d", "2");

            test.ProcessResult();
            Assert.AreEqual(3, _Solution.ConceptFindings[0].Facts.Count);
            Assert.AreEqual("I23a0653b-b574-4d5e-ad66-e05af1a169d[1]", _Solution.ConceptFindings[0].Facts[2].Id);

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring")]
        [WorkItem(20498)]
        [SetSolution(Defined_Solutions.SingleGapMatch.Id)]
        public void AssertThatFactIdIsCreatedAsIntended_2()
        {
            var sp = new GapMatchScoringParameter() { FindingOverride = "gapMatchController", Name = "domainX" }.AddSubParameters(
    "1", "2", "3", "4").AddGapTextParameters();
            sp.GapXhtmlParameter = _gapXhtmlParameter.To<XHtmlParameter>();
            sp = sp.Transform();
            var scoringPrms = new[] { sp };

            var map = new ScoringMap(scoringPrms, _Solution).GetMap();
            var single = map.First();


            single.GetConceptManipulator(_Solution); var test = new ScoreEditorForEncodingViewModel(single, _Solution);
            var manipulator = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            manipulator.SetKey("I23a0653b-b574-4d5e-ad66-e05af1a169d", "1");
            test.ProcessResult();

            single.GetConceptManipulator(_Solution); test = new ScoreEditorForEncodingViewModel(single, _Solution);
            manipulator = scoringPrms[0].GetScoreManipulator(test.WorkingSolution);
            manipulator.SetKey("I23a0653b-b574-4d5e-ad66-e05af1a169d", "2");
            test.ProcessResult();

            Assert.AreEqual(4, _Solution.ConceptFindings[0].Facts.Count);
            Assert.AreEqual("I23a0653b-b574-4d5e-ad66-e05af1a169d[1]", _Solution.ConceptFindings[0].Facts[2].Id);
            Assert.AreEqual("I23a0653b-b574-4d5e-ad66-e05af1a169d[2]", _Solution.ConceptFindings[0].Facts[3].Id);
        }


        private void DealWith_SetSolutionAttribute(SetSolutionAttribute setSolutionAttribute)
        {
            switch (setSolutionAttribute.SolutionName)
            {

                case Defined_Solutions.IntegerTwoFactSets.Id:
                    _Solution = GetSolution(Defined_Solutions.IntegerTwoFactSets.Data);
                    break;
                case Defined_Solutions.IntegerFactSetsFactAlternatives.Id:
                    _Solution = GetSolution(Defined_Solutions.IntegerFactSetsFactAlternatives.Data);
                    break;
                case Defined_Solutions.TwoInlineString.Id:
                    _Solution = GetSolution(Defined_Solutions.TwoInlineString.Data);
                    break;
                case Defined_Solutions.TwoGroupedMc.Id:
                    _Solution = GetSolution(Defined_Solutions.TwoGroupedMc.Data);
                    break;
                case Defined_Solutions.SingleGapMatch.Id:
                    _Solution = GetSolution(Defined_Solutions.SingleGapMatch.Data);
                    break;
                default:
                    Debug.Assert(false, "Not handled");
                    break;
            }

            Debug.Assert(_Solution != null, "It was expected a solution has been set.");

        }

        Solution GetSolution(XElement data)
        {
            return (Solution)SerializeHelper.XmlDeserializeFromString(data.ToString(), typeof(Solution));
        }



        private static class Defined_Solutions
        {

            public static class IntegerTwoFactSets
            {
                public const string Id = "84A4242C-F87B-42CD-A14B-D7090DFF1BEC"; public static XElement Data =
    XElement.Parse(@"<solution>
                                            <keyFindings>
                                              <keyFinding id=""integerScore"" scoringMethod=""Dichotomous"">
                                                <keyFactSet>
                                                  <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                    <keyValue domain=""integerScore"" occur=""1"">
                                                      <integerValue>
                                                        <typedValue>6</typedValue>
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
                                                <keyFactSet>
                                                  <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                    <keyValue domain=""integerScore"" occur=""1"">
                                                      <integerValue>
                                                        <typedValue>14</typedValue>
                                                      </integerValue>
                                                    </keyValue>
                                                  </keyFact>
                                                  <keyFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                    <keyValue domain=""integerScore"" occur=""1"">
                                                      <integerValue>
                                                        <typedValue>6</typedValue>
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
                                          </solution>");
            }

            public static class IntegerFactSetsFactAlternatives
            {
                public const string Id = "{FA85DDE1-2EDC-4812-A831-411CEF1D244E}"; public static XElement Data =
    XElement.Parse(@"<solution>
                                        <keyFindings>
                                          <keyFinding id=""integerScore"" scoringMethod=""Dichotomous"">
                                            <keyFact id=""5-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                              <keyValue domain=""integerScore"" occur=""1"">
                                                <integerValue>
                                                  <typedValue>1</typedValue>
                                                </integerValue>
                                                <integerValue>
                                                  <typedValue>2</typedValue>
                                                </integerValue>
                                              </keyValue>
                                            </keyFact>
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
                                            <keyFactSet>
                                              <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""integerScore"" occur=""1"">
                                                  <integerValue>
                                                    <typedValue>14</typedValue>
                                                  </integerValue>
                                                </keyValue>
                                              </keyFact>
                                              <keyFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""integerScore"" occur=""1"">
                                                  <integerValue>
                                                    <typedValue>6</typedValue>
                                                  </integerValue>
                                                </keyValue>
                                              </keyFact>
                                            </keyFactSet>
                                            <keyFactSet>
                                              <keyFact id=""3-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""integerScore"" occur=""1"">
                                                  <integerValue>
                                                    <typedValue>3</typedValue>
                                                  </integerValue>
                                                </keyValue>
                                              </keyFact>
                                              <keyFact id=""4-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""integerScore"" occur=""1"">
                                                  <integerValue>
                                                    <typedValue>7</typedValue>
                                                  </integerValue>
                                                </keyValue>
                                              </keyFact>
                                            </keyFactSet>
                                            <keyFactSet>
                                              <keyFact id=""3-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""integerScore"" occur=""1"">
                                                  <integerValue>
                                                    <typedValue>7</typedValue>
                                                  </integerValue>
                                                </keyValue>
                                              </keyFact>
                                              <keyFact id=""4-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""integerScore"" occur=""1"">
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
                                      </solution>");

            }

            public static class TwoInlineString
            {
                public const string Id = "5CCA11C4-391D-4B7B-BA28-27C4F8A6B7AC]";

                public static XElement Data =
                    XElement.Parse(@"<solution>
	                                    <keyFindings>
                                          <keyFinding id=""gapController"" scoringMethod=""Dichotomous"">
		                                    <keyFactSet>
                                              <keyFact id=""1-Ice32c0ba-73db-456d-b3d3-c92265282cf7"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""Ice32c0ba-73db-456d-b3d3-c92265282cf7"" occur=""1"">
                                                  <stringValue>
                                                    <typedValue>1</typedValue>
                                                  </stringValue>
                                                </keyValue>
                                              </keyFact>
                                              <keyFact id=""1-If62b4c70-e785-4b2d-892f-5ecaea3824a3"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""If62b4c70-e785-4b2d-892f-5ecaea3824a3"" occur=""1"">
                                                  <stringValue>
                                                    <typedValue>2</typedValue>
                                                  </stringValue>
                                                </keyValue>
                                              </keyFact>
                                            </keyFactSet>
		                                    </keyFinding>
                                        </keyFindings>
                                        <conceptFindings>
                                          <conceptFinding id=""gapController"" scoringMethod=""None"">
                                            <conceptFactSet>
                                              <conceptFact id=""1-Ice32c0ba-73db-456d-b3d3-c92265282cf7"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <conceptValue domain=""1-Ice32c0ba-73db-456d-b3d3-c92265282cf7"" occur=""1"">
                                                  <stringValue>
                                                    <typedValue>1</typedValue>
                                                  </stringValue>
                                                </conceptValue>
                                              </conceptFact>
                                              <conceptFact id=""1-If62b4c70-e785-4b2d-892f-5ecaea3824a3"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <conceptValue domain=""1-If62b4c70-e785-4b2d-892f-5ecaea3824a3"" occur=""1"">
                                                  <stringValue>
                                                    <typedValue>2</typedValue>
                                                  </stringValue>
                                                </conceptValue>
                                              </conceptFact>
                                              <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                            </conceptFactSet>
                                          </conceptFinding>
                                        </conceptFindings>
                                      </solution>");

            }

            public static class TwoGroupedMc
            {
                public const string Id = "9262D5DD-5B92-46D4-88AF-8AB040284971";

                public static XElement Data =
                    XElement.Parse(@"<solution xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                       <keyFindings>
	                                       <keyFinding id=""Opgave"" scoringMethod=""Dichotomous"">
		                                       <keyFactSet>
			                                       <keyFact id=""C-een"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
				                                       <keyValue domain=""een"" occur=""1"">
					                                       <stringValue>
						                                       <typedValue>C</typedValue>
					                                       </stringValue>
				                                       </keyValue>
			                                       </keyFact>
			                                       <keyFact id=""A-twee"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
				                                       <keyValue domain=""twee"" occur=""1"">
					                                       <stringValue>
						                                       <typedValue>A</typedValue>
					                                       </stringValue>
				                                       </keyValue>
			                                       </keyFact>
		                                       </keyFactSet>
	                                       </keyFinding>
                                       </keyFindings>
                                       <conceptFindings>
	                                       <conceptFinding id=""Opgave"" scoringMethod=""None"">
		                                       <conceptFactSet>
			                                       <conceptFact id=""C-een"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
				                                       <conceptValue domain=""C-een"" occur=""1"">
					                                       <stringValue>
						                                       <typedValue>C</typedValue>
					                                       </stringValue>
				                                       </conceptValue>
			                                       </conceptFact>
			                                       <conceptFact id=""A-twee"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
				                                       <conceptValue domain=""A-twee"" occur=""1"">
					                                       <stringValue>
						                                       <typedValue>A</typedValue>
					                                       </stringValue>
				                                       </conceptValue>
			                                       </conceptFact>
			                                       <concepts xmlns=""http://Cito.Tester.Server/xml/serialization""/>
		                                       </conceptFactSet>
	                                       </conceptFinding>
                                       </conceptFindings>
                                    </solution>");
            }

            public static class SingleGapMatch
            {
                public const string Id = "107B0C83-FBDB-4B83-83A9-DBA7A1C942D1";

                public static XElement Data =
                    XElement.Parse(@"<solution>
                                        <keyFindings>
                                          <keyFinding id=""gapMatchController"" scoringMethod=""Dichotomous"">
                                            <keyFact id=""I23a0653b-b574-4d5e-ad66-e05af1a169d"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                              <keyValue domain=""I23a0653b-b574-4d5e-ad66-e05af1a169d"" occur=""1"">
                                                <stringValue>
                                                  <typedValue>1</typedValue>
                                                </stringValue>
                                              </keyValue>
                                            </keyFact>
                                          </keyFinding>
                                        </keyFindings>
                                        <aspectReferences />
                                      </solution>");
            }


        }



        private readonly XElement _gapXhtmlParameter =
            XElement.Parse(@"<XHtmlParameter name=""itemInlineInput"" xmlns:cito=""http://www.cito.nl/citotester"">
                                                          <cito:InlineElement id=""I23a0653b-b574-4d5e-ad66-e05af1a169d"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id=""entireItem"">
                                                                      <cito:plaintextparameter name=""inlineGapMatchId"">I23a0653b-b574-4d5e-ad66-e05af1a169d</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name=""inlineGapMatchLabel"">Gat 1</cito:plaintextparameter>
                                                                      <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                                                                      <cito:integerparameter name=""width""/>
                                                                      <cito:integerparameter name=""height""/>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement>
                                                          <cito:InlineElement id=""I47a1295a-c729-49d5-9da0-bac0799a019e"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id=""entireItem"">
                                                                      <cito:plaintextparameter name=""inlineGapMatchId"">I47a1295a-c729-49d5-9da0-bac0799a019e</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name=""inlineGapMatchLabel"">Gat 2</cito:plaintextparameter>
                                                                      <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                                                                      <cito:integerparameter name=""width""/>
                                                                      <cito:integerparameter name=""height""/>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement> maar een <cito:InlineElement id=""I27b8eca6-ae70-4f7b-bc23-b904b2e9045f"" layoutTemplateSourceName=""InlineGapMatchLayoutTemplate"">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id=""entireItem"">
                                                                      <cito:plaintextparameter name=""inlineGapMatchId"">I27b8eca6-ae70-4f7b-bc23-b904b2e9045f</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name=""inlineGapMatchLabel"">Gat 3</cito:plaintextparameter>
                                                                      <cito:booleanparameter name=""editSize"">True</cito:booleanparameter>
                                                                      <cito:integerparameter name=""width""/>
                                                                      <cito:integerparameter name=""height""/>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement>
                                                      </XHtmlParameter>");
    }
}
