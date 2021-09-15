
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Controls;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class V2_Advanced_ScoringViewModel_Binding_Tests : UsesTheItemEditorVM
    {
        private Solution _Solution;
        #region Constructor

        public V2_Advanced_ScoringViewModel_Binding_Tests()
        {
            AddAttributteInitializer<SetSolutionAttribute>(att => DealWith_SetSolutionAttribute(att as SetSolutionAttribute));
        }

        #endregion

        [TestCleanup]
        public override void Clean()
        {
            base.Clean();
            _Solution = null;
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        [AddParameter(typeof(ChoiceScoringParameter), Name = "Naam")]
        [SetSolution(Defined_Solutions.WithMultiChoice_KeyIsA.Id)]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void AConcept_AConceptScoreEditor()
        {
            //Arrange
            var fakeItemEditorVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.IsTrue(V2Score.ConceptParametersPresent, "Concept Editor not found!");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        [AddParameter(typeof(ChoiceScoringParameter), Name = "Naam", ControllerID = "Score", SubParameterIds = "ABCD")]
        [SetSolution(Defined_Solutions.WithMultiChoice_KeyIsA.Id)]
        public void FindingGroupIsPopulated()
        {
            //Arrange
            var fakeItemEditorVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(1, V2Score.FindingGroups.Count, "A single finding group was expected.");
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        [AddParameter(typeof(IntegerScoringParameter), Name = "antwoord", ControllerID = "integerScore", SubParameterIds = "12")]
        [SetSolution(Defined_Solutions.IntegerTwoFactSets.Id)]
        public void BlockViewModelForTwoKeyFactSets()
        {
            //Arrange
            var fakeItemEditorVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(1, V2Score.FindingGroups.Count, "A single finding group was expected.");
            var blockgridData = V2Score.FindingGroups[0].BlockGridData;

            // We expect one row with two blocks (for each keyset a block)
            Assert.AreEqual(1, blockgridData.Count);
            var row1 = blockgridData.First();
            Assert.AreEqual(2, row1.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        [AddParameter(typeof(IntegerScoringParameter), Name = "antwoord", ControllerID = "integerScore", SubParameterIds = "12345")]
        [SetSolution(Defined_Solutions.IntegerFactSetsFactAlternatives.Id)]
        public void BlockViewModelForTwoKeyFactSetsFactAlternatives()
        {
            //Arrange
            var fakeItemEditorVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(1, V2Score.FindingGroups.Count, "A single finding group was expected.");
            var blockgridData = V2Score.FindingGroups[0].BlockGridData.ToList();

            // We expect three rows with two blocks (for each keyset a block)
            Assert.AreEqual(3, blockgridData.Count);
            var row1 = blockgridData[0];
            Assert.AreEqual(2, row1.Count);
            var row2 = blockgridData[1];
            Assert.AreEqual(2, row2.Count);
            var row3 = blockgridData[2];
            Assert.AreEqual(1, row3.Count);
            Assert.IsTrue(row3.First().ContainsKey("antwoord.5"));
            Assert.AreEqual(2, ((IEnumerable<IBlockRowViewModel>)row3.First()["antwoord.5"]).Count());
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        [AddParameter(typeof(IntegerScoringParameter), Name = "antwoord", ControllerID = "integerScore", SubParameterIds = "12345")]
        [SetSolution(Defined_Solutions.IntegerFactSetsFactAlternatives.Id)]
        public void BlockViewModelForTwoKeyFactSetsFactAlternatives_CheckAlternatives()
        {
            //Arrange
            var fakeItemEditorVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(1, V2Score.FindingGroups.Count, "A single finding group was expected.");
            var blockgridData = V2Score.FindingGroups[0].BlockGridData.ToList();

            // We expect three rows with two blocks (for each keyset a block)

            var row1 = blockgridData[0];
            Assert.AreEqual(2, row1.Count, "Row 0 should have 2 blocks");
            var block1r1 = blockgridData[0][0];

            Assert.AreEqual(2, block1r1.Keys.Count, "Expecting 2 keys");

            //verify nr of alternatives
            Assert.IsTrue(block1r1.ContainsKey("antwoord.1"), "antwoord.1 shoule be exist");
            Assert.AreEqual(3, ((IList)block1r1["antwoord.1"]).Count);

            Assert.IsTrue(block1r1.ContainsKey("antwoord.2"), "antwoord.2 shoule be exist");
            Assert.AreEqual(1, ((IList)block1r1["antwoord.2"]).Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        [AddParameter(typeof(IntegerScoringParameter), Name = "antwoord", ControllerID = "integerScore", SubParameterIds = "12345")]
        [SetSolution(Defined_Solutions.IntegerFactSetsFactAlternatives.Id)]
        public void AddFactSet_NewFactSetCreated()
        {
            //Arrange
            var fakeItemEditorVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            viewAwareStatus.SimulateViewIsLoadedEvent();

            var nrOfFactSetsB4 = _Solution.Findings[0].KeyFactsets.Count;
            //Act            
            ObservableDictionary<string, IEnumerable> result = V2Score.FindingGroups[0].AddFactSet(V2Score.FindingGroups[0].BlockGridData[0],_Solution);
            
            //Assert
            var nrOfFactSetsNow = _Solution.Findings[0].KeyFactsets.Count;

            Assert.AreEqual(2,result.Keys.Count);
            Assert.AreEqual(nrOfFactSetsB4 + 1, nrOfFactSetsNow);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        [AddParameter(typeof(IntegerScoringParameter), FindingOverride = "DefaultFinding", Name = "antwoord", ControllerID = "integerScore", SubParameterIds = "1")]
        [AddParameter(typeof(MultiChoiceScoringParameter), FindingOverride = "DefaultFinding", Name = "McScore", ControllerID = "McScore", SubParameterIds = "ABC")]
        [SetSolution(Defined_Solutions.IntMc2FactSets.Id)]
        public void IntMCInFact2Sets()
        {
            //Arrange
            var fakeItemEditorVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);
            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(1, V2Score.FindingGroups.Count, "A single finding group was expected.");
            var blockgridData = V2Score.FindingGroups[0].BlockGridData.ToList();

            // We expect three rows with two blocks (for each keyset a block)

            var row1 = blockgridData[0];
            Assert.AreEqual(2, row1.Count, "Row 0 should have 2 blocks");
            var block1r1 = blockgridData[0][0];
            var block2r1 = blockgridData[0][1];
            Assert.AreEqual(2, block1r1.Keys.Count, "Expecting 2 keys");
            var blockRowVM = ((IList<IBlockRowViewModel>)block1r1["McScore"]).First();
            Assert.AreEqual("A", ((ChoiceBlockRowViewModel)blockRowVM).Value.DataValue);

            Assert.AreEqual(2, block2r1.Keys.Count, "Expecting 2 keys");
            blockRowVM = ((IList<IBlockRowViewModel>)block2r1["McScore"]).First();
            Assert.AreEqual("B", ((ChoiceBlockRowViewModel)blockRowVM).Value.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        [AddParameter(typeof(IntegerScoringParameter), FindingOverride = "DefaultFinding", Name = "antwoord", ControllerID = "integerScore", SubParameterIds = "1")]
        [AddParameter(typeof(MultiChoiceScoringParameter), FindingOverride = "DefaultFinding", Name = "McScore", ControllerID = "McScore", SubParameterIds = "ABC")]
        [SetSolution(Defined_Solutions.EmptySolution.Id)]
        public void GroupIntMCWithNoFacts()
        {
            //Arrange
            var fakeItemEditorVm = FakeItemEditorVM;
            var viewAwareStatus = ViewAwareStatus;
            var V2Score = new V2_Advanced_ScoringViewModel(viewAwareStatus);
            InjectStuff(V2Score);

            //Act
            viewAwareStatus.SimulateViewIsLoadedEvent();

            // group row 1 and 2
            var blockgridData = V2Score.FindingGroups[0].BlockGridData.ToList();
            var block1r1 = blockgridData[0][0];
            var block2r1 = blockgridData[1][0];

            V2Score.FindingGroups[0].SelectedBlockRows.DataValue = new List<BlockRow>() { new BlockRow() { Item = ((IList<IBlockRowViewModel>)block1r1["antwoord"]).First() }, new BlockRow() { Item = ((IList<IBlockRowViewModel>)block2r1["McScore"]).First() } };
            V2Score.FindingGroups[0].GroupInteractionsCommand.Execute(blockgridData);
            //Assert
            Assert.AreEqual(1, V2Score.FindingGroups.Count, "A single finding group was expected.");

            // We expect three rows with two blocks (for each keyset a block)
            Assert.AreEqual(2, blockgridData.Count, "Expect two rows");
            var row1 = blockgridData[0];
            Assert.AreEqual(1, row1.Count, "Row 0 should have 2 blocks");
        }
        //-----------------------------------------------------------------------------------------------
        protected override IEnumerable<ComposablePartCatalog> GetTypesForInjection()
        {
            return new[] { MyComposer.GetTestTypesForCinch(),
                        MyComposer.GetScoreEditors()};
        }

        internal override void SetFakeViewDataContext(ref IWorkSpaceAware fakeView, IItemEditorViewModel itemEditorViewModel)
        {
            Debug.Assert(_Solution != null);
            itemEditorViewModel.AssessmentItem.DataValue.Solution = _Solution;
            fakeView.WorkSpaceContextualData.DataValue = itemEditorViewModel;
        }

        #region DATA USED & Handler

        /// <summary>
        /// Deals the with_ set solution attribute. This method is called before the unit test is called before SetFakeViewDataContext.
        /// </summary>
        /// <param name="setSolutionAttribute">The set solution attribute.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void DealWith_SetSolutionAttribute(SetSolutionAttribute setSolutionAttribute)
        {
            switch (setSolutionAttribute.SolutionName)
            {
                case Defined_Solutions.WithMultiChoice_KeyIsA.Id:
                    _Solution = (Solution)SerializeHelper.XmlDeserializeFromString(Defined_Solutions.WithMultiChoice_KeyIsA.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.IntegerTwoFactSets.Id:
                    _Solution = (Solution)SerializeHelper.XmlDeserializeFromString(Defined_Solutions.IntegerTwoFactSets.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.IntegerFactSetsFactAlternatives.Id:
                    _Solution = (Solution)SerializeHelper.XmlDeserializeFromString(Defined_Solutions.IntegerFactSetsFactAlternatives.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.IntMc2FactSets.Id:
                    _Solution = (Solution)SerializeHelper.XmlDeserializeFromString(Defined_Solutions.IntMc2FactSets.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.EmptySolution.Id:
                    _Solution = (Solution)SerializeHelper.XmlDeserializeFromString(Defined_Solutions.EmptySolution.Data.ToString(), typeof(Solution));
                    break;
                default:
                    Debug.Assert(false, "Not handled");
                    break;
            }

            Debug.Assert(_Solution != null, "It was expected a solution has been set.");

        }

        private static class Defined_Solutions
        {
            public static class EmptySolution
            {
                public const string Id = "C20D731F-CC46-470B-91B2-70063B4A80C3"; //this value is not visible but should be unique. 
                public static XElement Data = XElement.Parse(@"<solution xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""></solution>");


            }

            public static class WithMultiChoice_KeyIsA
            {
                public const string Id = "A2C566A9-DA35-4E15-9182-D6B53B65EC34"; //this value is not visible but should be unique. 
                public static XElement Data = XElement.Parse(@"<solution xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                                              <keyFindings>
                                                                <keyFinding id=""Score"" scoringMethod=""None"">
                                                                  <keyFact id=""A-Score"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                    <keyValue domain=""Score"" occur=""1"">
                                                                      <stringValue>
                                                                        <typedValue>A</typedValue>
                                                                      </stringValue>
                                                                    </keyValue>
                                                                  </keyFact>
                                                                </keyFinding>
                                                              </keyFindings>                                                             
                                                              <aspectReferences />
                                                            </solution>");

            }

            public static class IntegerTwoFactSets
            {
                public const string Id = "84A4242C-F87B-42CD-A14B-D7090DFF1BEC"; //this value is not visible but should be unique. 
                public static XElement Data =
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
                public const string Id = "{FA85DDE1-2EDC-4812-A831-411CEF1D244E}"; //this value is not visible but should be unique. 
                public static XElement Data =
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

            public static class IntMc2FactSets
            {
                public const string Id = "{07606F8B-4403-490A-9181-BC8035CB558C}"; //this value is not visible but should be unique. 
                public static XElement Data =
                    XElement.Parse(@"<solution>
                                        <keyFindings>
                                          <keyFinding id=""DefaultFinding"" scoringMethod=""Dichotomous"">
                                            <keyFactSet>
                                              <keyFact id=""A-McScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""McScore"" occur=""1"">
                                                  <stringValue>
                                                    <typedValue>A</typedValue>
                                                  </stringValue>
                                                </keyValue>
                                              </keyFact>
                                              <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""integerScore"" occur=""1"">
                                                  <integerValue>
                                                    <typedValue>2</typedValue>
                                                  </integerValue>
                                                </keyValue>
                                              </keyFact>
                                            </keyFactSet>
                                            <keyFactSet>
                                              <keyFact id=""B-McScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""McScore"" occur=""1"">
                                                  <stringValue>
                                                    <typedValue>B</typedValue>
                                                  </stringValue>
                                                </keyValue>
                                              </keyFact>
                                              <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                <keyValue domain=""integerScore"" occur=""1"">
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
            }



        #endregion

        }
    }

}
