
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class EncodingScoringVMTest_WithSpecificData : UsesTheItemEditorVM
    {
        private Solution _Solution;
        private IItemEditorObjectFactory fake_Factory;

        public EncodingScoringVMTest_WithSpecificData()
        {
            AddAttributteInitializer<SetSolutionAttribute>(
                att => DealWith_SetSolutionAttribute(att as SetSolutionAttribute));
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.SolutionWithConceptsDefinedOnFacts.Id)]
        [AddParameter(typeof(ChoiceScoringParameter), InlineID = "Score", SubParameterIds = "ABCD")]
        public void LoadDataWithConceptScoringSet_ExpectsPartsToBeSelected()
        {
            //SomeConcept4
            //+Root
            //   +Part 1
            //   |-Part A 
            //   |-Part B
            //   +Part 2
            //   |-Part A 
            //   |-Part B

            //Arrange

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var partAs = csVM.Data.Where(e => e.PartName == "Part a").ToList();
            var p1a = partAs[0];
            var p2a = partAs[1];

            WriteSolution("Arrange");

            //Act
            //No actions required,.. just testing that part is selected.

            //Assert
            Assert.IsTrue(p1a.Selected.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.SolutionWithConceptsDefinedOnFacts.Id)]
        [AddParameter(typeof(ChoiceScoringParameter), InlineID = "Score", SubParameterIds = "ABCD")]
        public void LoadDataWithConceptScoringSet_RefillSingleConceptScore_ExpectsPartsToBeSelected()
        {
            //Arrange

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var partAs = csVM.Data.Where(e => e.PartName == "Part a").ToList();
            var p1a = partAs[0];
            var p2a = partAs[1];

            WriteSolution("Arrange");

            //Act
            //Assert
            Assert.IsTrue(p1a.Selected.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.SolutionMRGroupedInTwoDistinctSets.Id)]
        [AddParameter(typeof(MultiChoiceScoringParameter), FindingOverride = "Opgave", ControllerID = "mc_1", SubParameterIds = "ABCD", Name = "keuze")]
        public void LoadDataWithConceptScoringSet_ChangingCurrentScoringMap_ExpectsPartsToBeDeSelected()
        {
            //Arrange
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            var inlineSP = (MultiChoiceScoringParameter)
                            FakeItemEditorVM.ParameterSetCollection.DataValue.DeepFetchScoringParameters()
                                .First(prm => prm.Name == "keuze");
            inlineSP.MaxChoices = 4;

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var partAs = csVM.Data.Where(e => e.PartName == "Part 1").ToList();
            var p1 = partAs[0];

            WriteSolution("Arrange");

            //Act
            csVM.CurrentScoringMap = csVM.AvailableParams.Keys.ElementAt<CombinedScoringMapKey>(0);
            bool p1SelectedWithFirstScoringMapKey = p1.Selected.DataValue;
            csVM.CurrentScoringMap = csVM.AvailableParams.Keys.ElementAt<CombinedScoringMapKey>(1);
            bool p1SelectedWithSecondScoringMapKey = p1.Selected.DataValue;

            //Assert
            Assert.IsTrue(p1SelectedWithFirstScoringMapKey);
            Assert.IsFalse(p1SelectedWithSecondScoringMapKey);
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.SolutionWithConceptsDefinedOnFacts.Id)]
        [AddParameter(typeof(ChoiceScoringParameter), InlineID = "Score", SubParameterIds = "ABCD")]
        public void LoadDataWithConceptScoringSet_RefillSingleConceptScore_ExpectsSamePartsInOtherPartInHierarchyToBeSelected()
        {
            //Arrange

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var partAs = csVM.Data.Where(e => e.PartName == "Part a").ToList();
            var p1a = partAs[0];
            var p2a = partAs[1];

            WriteSolution("Arrange");

            //Act
            //Assert
            Assert.IsTrue(p2a.Selected.DataValue);
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.SolutionWithMC.Id)]
        [AddParameter(typeof(ChoiceScoringParameter), FindingOverride = "Opgave", ControllerID = "mc_1",
            SubParameterIds = "ABCDEF")]
        public void DataGridColumnsShouldBeAlphabetical()
        {
            //Arrange

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            WriteSolution("Arrange");
            //Act
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(6 + 2, csVM.DataGridColumns.Count,
                "Expects 6 columns with id's and 2 for partname and selector");
            Assert.AreEqual("B", csVM.DataGridColumns[2].Header.ToString()); //Since it is the key!
            Assert.AreEqual("A", csVM.DataGridColumns[3].Header.ToString()); //Continue Alpha
            Assert.AreEqual("C", csVM.DataGridColumns[4].Header.ToString());
            Assert.AreEqual("D", csVM.DataGridColumns[5].Header.ToString());
            Assert.AreEqual("E", csVM.DataGridColumns[6].Header.ToString());
            Assert.AreEqual("F", csVM.DataGridColumns[7].Header.ToString());
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.Solution2FactSets1Fact.Id)]
        [AddParameter(typeof(IntegerScoringParameter), FindingOverride = "sharedIntegerFinding",
            ControllerID = "integerScore", SubParameterIds = "12345")]
        public void AvailableParamsFor2SetsAnd1Finding_ShouldBe3()
        {
            //Arrange
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            WriteSolution("Arrange");
            //Act
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(3, csVM.AvailableParams.Count, "Expected 3, 2 sets of fact set and 1 finding.");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.Solution2FactSets1Fact_NoConcept.Id)]
        [AddParameter(typeof(IntegerScoringParameter), FindingOverride = "sharedIntegerFinding",
            ControllerID = "integerScore", SubParameterIds = "12345")]
        public void AvailableParamsFor2SetsAnd1FindingNoConcepts_ShouldBe3()
        {
            //Arrange
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            WriteSolution("Arrange");
            //Act
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(3, csVM.AvailableParams.Count, "Expected 3, 2 sets of fact set and 1 finding.");
            /*If there was no synchronisation form keyfacts to concept then the result = 5*/
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.Solution2InlineParamsInSet.Id)]
        [AddParameter(typeof(InlineChoiceScoringParameter), FindingOverride = "Opgave", InlineID = "Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc", SubParameterIds = "ABCD")]
        [AddParameter(typeof(InlineChoiceScoringParameter), FindingOverride = "Opgave", InlineID = "I16746288-1b56-4c53-880d-2d54d060fba8", SubParameterIds = "ABCD")]
        public void DataGridColumns_With2InlineChoice_FactsNotRepeatedInConceptFinding()
        {
            //Arrange
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            WriteSolution("Arrange");
            //Act
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //Assert
            Assert.AreEqual(2 + 1 + 1 + 1, csVM.DataGridColumns.Count, "2 columns + 1 set + 1 additional define column.+catch all");
            /*If there was no synchronisation form keyfacts to concept then the result = 5*/
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.Solution2InlineParamsInSet.Id)]
        [AddParameter(typeof(InlineChoiceScoringParameter), FindingOverride = "Opgave", InlineID = "Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc", SubParameterIds = "ABCD", Name = "Inline 1")]
        [AddParameter(typeof(InlineChoiceScoringParameter), FindingOverride = "Opgave", InlineID = "I16746288-1b56-4c53-880d-2d54d060fba8", SubParameterIds = "ABCD", Name = "Inline 2")]
        public void DataGridColumns_With2InlineChoice_SetKey_WillRepeatFactsInConceptFinding()
        {
            //Arrange
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            WriteSolution("Arrange");
            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var inlineSP =
                (InlineChoiceScoringParameter)
                    FakeItemEditorVM.ParameterSetCollection.DataValue.DeepFetchScoringParameters()
                        .First(prm => prm.Name == "Inline 2");
            var manipulator = inlineSP.GetScoreManipulator(_Solution);

            //Act
            manipulator.SetKey("B");
            csVM.RefreshScore.Execute(/*parameter*/null);

            //Assert
            Assert.AreEqual(4 + 1, csVM.DataGridColumns.Count, "Expected 1 set (+1 column for additional answers button. + 1 catch all");
            /*If there was no synchronisation form keyfacts to concept then the result = 5*/
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.SolutionMRGroupedInTwoDistinctSets.Id)]
        [AddParameter(typeof(ChoiceScoringParameter), FindingOverride = "Opgave", ControllerID = "mc_1", SubParameterIds = "ABCD", Name = "keuze")]
        public void DataGridColumns_MR_Grouped_ColumnsShouldIncludeCatchAllAndAdd()
        {
            //Arrange
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            WriteSolution("Arrange");

            var inlineSP =
                (ChoiceScoringParameter)
                    FakeItemEditorVM.ParameterSetCollection.DataValue.DeepFetchScoringParameters()
                        .First(prm => prm.Name == "keuze");
            inlineSP.MaxChoices = 4;

            var map = new ScoringMap(new[] { inlineSP }, _Solution).GetMap().ToList();

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            //Act
            csVM.RefreshScore.Execute(/*parameter*/null);

            //Assert
            Assert.IsFalse(condition: inlineSP.IsSingleChoice);
            Assert.AreEqual(7, csVM.DataGridColumns.Count, "2 trailing columns checkbox and name, ;Expected 2 set; +1 set that is answercatagory; + catch all; + addButton ");
            Assert.AreEqual("add", csVM.DataGridColumns.Last().Header);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.SolutionWithCatchAll.Id)]
        [AddParameter(typeof(IntegerScoringParameter), FindingOverride = "sharedIntegerFinding", ControllerID = "integerScore", SubParameterIds = "1", Name = "Int")]
        public void ColumnsOrder_ShouldBe_Answer_AnswerCategory_CatchAll_Add()
        {
            //Arrange
            var encodingViewModel = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            WriteSolution("Arrange");
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //Act
            var columns = encodingViewModel.DataGridColumns;
            //Assert
            Assert.AreEqual("6", columns[2].Header.ToString());
            Assert.AreEqual("7", columns[3].Header.ToString());
            Assert.AreEqual("8", columns[4].Header.ToString());
            Assert.AreEqual("{∗}", columns[5].Header.ToString());
            Assert.AreEqual("add", columns[6].Header.ToString());

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [SetSolution(Defined_Solutions.SolutionWithSetsAndCathcAll.Id)]
        [AddParameter(typeof(IntegerScoringParameter), FindingOverride = "sharedIntegerFinding", ControllerID = "integerScore", SubParameterIds = "12345", Name = "Int")]
        public void WithSetColumnsOrder_ShouldBe_Answer_AnswerCategory_CatchAll_Add()
        {
            //Arrange
            var encodingViewModel = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            WriteSolution("Arrange");
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //Act
            var columns = encodingViewModel.DataGridColumns;
            //Assert
            Assert.AreEqual("6&14", columns[2].Header.ToString());
            Assert.AreEqual("14&6", columns[3].Header.ToString());
            Assert.AreEqual("7&15", columns[4].Header.ToString());
            Assert.AreEqual("15&7", columns[5].Header.ToString());
            Assert.AreEqual("{∗}&{∗}", columns[6].Header.ToString());
            Assert.AreEqual("add", columns[7].Header.ToString());
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [AddParameter(typeof(ChoiceScoringParameter), InlineID = "Score1", SubParameterIds = "ABCD", Name = "Choice 1")]
        [AddParameter(typeof(ChoiceScoringParameter), InlineID = "Score2", SubParameterIds = "ABCD", Name = "Choice 2")]
        [SetSolution(Defined_Solutions.EmptySolution.Id)]
        public void SetScore_Choice1_SetScore_Choice2_SwitchToChoice1_PartsAreSelectable()
        {
            //SomeConcept4
            //+Root
            //   +Part 1
            //   |-Part A 
            //   |-Part B
            //   +Part 2
            //   |-Part A 
            //   |-Part B
            //Arrange

            SetScoreForChoiceKeyTo("A");
            var encodingVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var p1 = encodingVM.Data.Single(e => e.PartName == "Part 1");
            var partAs = encodingVM.Data.Where(e => e.PartName.StartsWith("Part") && e.Parent.PartName == "Part 2").ToList();
            var p1a = partAs[0];
            var p2a = partAs[1];

            p1.Selected.DataValue = true;
            p1a.Selected.DataValue = true;
            p2a.Selected.DataValue = true;

            setConceptScoring(p1, new int?[] { 1, 1, 1, 1 });
            setConceptScoring(p1a, new int?[] { 2, 2, 2, 2 });
            setConceptScoring(p2a, new int?[] { 3, 3, 3, 3 });

            WriteSolution("Arrange");

            //Act

            //Select another 
            encodingVM.CurrentScoringMap =
                encodingVM.AvailableParams.Keys.First(key => encodingVM.CurrentScoringMap != key);

            WriteSolution("Act");

            //Assert
            p1 = encodingVM.Data.Single(e => e.PartName == "Part 1");
            partAs = encodingVM.Data.Where(e => e.PartName.StartsWith("Part") && e.Parent.PartName == "Part 2").ToList();
            p1a = partAs[0];
            p2a = partAs[1];
            
            Assert.IsTrue(p1.CanSelect.DataValue);
            Assert.IsFalse(p1a.CanSelect.DataValue);
            Assert.IsFalse(p2a.CanSelect.DataValue);

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        [AddParameter(typeof(ChoiceScoringParameter), InlineID = "Score1", SubParameterIds = "ABCD", Name = "Choice 1")]
        [AddParameter(typeof(ChoiceScoringParameter), InlineID = "Score2", SubParameterIds = "ABCD", Name = "Choice 2")]
        [SetSolution(Defined_Solutions.EmptySolution.Id)]
        public void SetScore_Choice1_SwitchToChoice2_PartsAreNotSelectable()
        {
            //SomeConcept4
            //+Root
            //   +Part 1
            //   |-Part A 
            //   |-Part B
            //   +Part 2
            //   |-Part A 
            //   |-Part B
            //Arrange

          

            SetScoreForChoiceKeyTo("A");
            var encodingVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var p1 = encodingVM.Data.Single(e => e.PartName == "Part 1");
            var partAs = encodingVM.Data.Where(e => e.PartName.StartsWith("Part") && e.Parent.PartName == "Part 2").ToList();
            var p1a = partAs[0];
            var p2a = partAs[1];

            p1.Selected.DataValue = true;
            p1a.Selected.DataValue = true;
            p2a.Selected.DataValue = true;

            setConceptScoring(p1, new int?[] { 1, 1, 1, 1 });
            setConceptScoring(p1a, new int?[] { 2, 2, 2, 2 });
            setConceptScoring(p2a, new int?[] { 3, 3, 3, 3 });

            WriteSolution("Arrange");

            //Act

            //Select another 
            encodingVM.CurrentScoringMap =
                encodingVM.AvailableParams.Keys.First(key => encodingVM.CurrentScoringMap != key);

            WriteSolution("Act");

            //Assert
            encodingVM.CurrentScoringMap =
                encodingVM.AvailableParams.Keys.First(key => encodingVM.CurrentScoringMap != key);

            p1 = encodingVM.Data.Single(e => e.PartName == "Part 1");
            partAs = encodingVM.Data.Where(e => e.PartName.StartsWith("Part") && e.Parent.PartName == "Part 2").ToList();
            p1a = partAs[0];
            p2a = partAs[1];

            Assert.IsTrue(p1.CanSelect.DataValue);
            Assert.IsTrue(p1a.CanSelect.DataValue);
            Assert.IsTrue(p2a.CanSelect.DataValue);

        }

        private void setConceptScoring(EncodingScoreHierarchyPartVM part, int?[] conceptScores)
        {
            Debug.Assert(part.Selected.DataValue);
            Debug.Assert(part.ConceptScorePart.Count == conceptScores.Length);

            for (int i = 0; i < conceptScores.Length; i++)
                part.ConceptScorePart[i].IntScore = conceptScores[i];
        }

        private int?[] getConceptScoring(EncodingScoreHierarchyPartVM part)
        {
            Debug.Assert(part.Selected.DataValue);
            return part.ConceptScorePart.Select(scorePart => scorePart.IntScore).ToArray();
        }

        private void SetScoreForChoiceKeyTo(string scoreKey)
        {
            var choiceSParams = FakeItemEditorVM.ParameterSetCollection.DataValue.DeepFetchScoringParameters().Where(sp => sp is ChoiceScoringParameter);

            choiceSParams.Cast<ChoiceScoringParameter>().ToList()
                .ForEach(choiceSPrm => choiceSPrm.GetScoreManipulator(_Solution).SetKey(scoreKey));
        }

        //-------------


        internal override void SetFakeViewDataContext(ref IWorkSpaceAware fakeView,
            IItemEditorViewModel itemEditorViewModel)
        {
            var prms =
                itemEditorViewModel.ParameterSetCollection.DataValue.FlattenParameters()
                    .Where(p => p is ScoringParameter)
                    .Select(e => (ScoringParameter)e);
            var lst = prms.ToList();
            fakeView.WorkSpaceContextualData.DataValue =
                new Tuple<IEnumerable<ScoringParameter>, Solution, IItemEditorViewModel>(lst, _Solution,
                    itemEditorViewModel);
        }

        /// <summary>
        /// Deals the with_ set solution attribute. This method is called before the unit test is calle before SetFakeViewDataContext.
        /// </summary>
        /// <param name="setSolutionAttribute">The set solution attribute.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void DealWith_SetSolutionAttribute(SetSolutionAttribute setSolutionAttribute)
        {
            switch (setSolutionAttribute.SolutionName)
            {
                case Defined_Solutions.EmptySolution.Id:
                    _Solution = new Solution();
                    break;
                case Defined_Solutions.SolutionWithConceptsDefinedOnFacts.Id:
                    _Solution =
                        (Solution)
                            SerializeHelper.XmlDeserializeFromString(
                                Defined_Solutions.SolutionWithConceptsDefinedOnFacts.Data.ToString(), typeof(Solution));
                    break;

                case Defined_Solutions.SolutionWithMC.Id:
                    _Solution =
                        (Solution)
                            SerializeHelper.XmlDeserializeFromString(Defined_Solutions.SolutionWithMC.Data.ToString(),
                                typeof(Solution));
                    break;
                case Defined_Solutions.Solution2FactSets1Fact.Id:
                    _Solution =
                        (Solution)
                            SerializeHelper.XmlDeserializeFromString(
                                Defined_Solutions.Solution2FactSets1Fact.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.Solution2FactSets1Fact_NoConcept.Id:
                    _Solution =
                        (Solution)
                            SerializeHelper.XmlDeserializeFromString(
                                Defined_Solutions.Solution2FactSets1Fact_NoConcept.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.Solution2InlineParamsInSet.Id:
                    _Solution =
                       (Solution)
                           SerializeHelper.XmlDeserializeFromString(
                               Defined_Solutions.Solution2InlineParamsInSet.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.SolutionMRGroupedInTwoDistinctSets.Id:
                    _Solution =
                       (Solution)
                           SerializeHelper.XmlDeserializeFromString(
                               Defined_Solutions.SolutionMRGroupedInTwoDistinctSets.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.SolutionWithCatchAll.Id:
                    _Solution =
                        (Solution)SerializeHelper.XmlDeserializeFromString(
                            Defined_Solutions.SolutionWithCatchAll.Data.ToString(), typeof(Solution));
                    break;
                case Defined_Solutions.SolutionWithSetsAndCathcAll.Id:
                    _Solution =
                        (Solution)SerializeHelper.XmlDeserializeFromString(
                            Defined_Solutions.SolutionWithSetsAndCathcAll.Data.ToString(), typeof(Solution));
                    break;
                default:
                    Debug.Assert(false, "Not handled");
                    break;
            }

            Debug.Assert(_Solution != null, "It was expected a solution has been set.");
        }


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            //Create new IItemEditorObjectFactory
            fake_Factory = FakeItemEditorObjectFactory.MakeNewFake();

            //Ensure that when the property of the faked item editor returns the 'fake_Factory'
            A.CallTo(() => FakeItemEditorVM.ItemEditorObjectFactory).ReturnsLazily((arg) => fake_Factory);

            //fake factory will respond to 'PopulateConceptCustomBankPropertyHierarchy'
            A.CallTo(() => fake_Factory.PopulateConceptCustomBankPropertyHierarchy(A<Guid>.Ignored))
                 .ReturnsLazily((args) => base.FakeConceptHandler.GetPartById(args.GetArgument<Guid>(0)));
        }

        [TestCleanup]
        public override void Clean()
        {
            base.Clean();

            fake_Factory = null;
            FakeItemEditorObjectFactory.MakeNewFake(); //Resets used fake object.
        }

        protected override IEnumerable<System.ComponentModel.Composition.Primitives.ComposablePartCatalog>
            GetTypesForInjection()
        {
            return new[] { MyComposer.NoExportTypes() };
        }

        public void WriteSolution(string currentState)
        {
            var xml = new XmlSerializer(typeof(Solution));
            Debug.WriteLine(String.Empty);
            Debug.WriteLine(string.Format("WriteSolution for State [{0}]", currentState));
            using (var stream = new StringWriter())
            {
                xml.Serialize(stream, _Solution);
                Debug.WriteLine(stream.ToString());
            }
        }

        #region DATA USED

        private static class Defined_Solutions
        {
            public static class EmptySolution
            {
                public const string Id = "98A0CBB1-7956-4B75-8CAE-568228B33774";
            }

            public static class SolutionWithConceptsDefinedOnFacts
            {
                public const string Id = "A2C566A9-DA35-4E15-9182-D6B53B65EC34";
                //this value is not visible but should be unique. 

                public static XElement Data =
                    XElement.Parse(
                        @"<solution xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
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
                            <conceptFindings>
                            <conceptFinding id=""Score"" scoringMethod=""None"">
                                <conceptFact id=""A-Score"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                <conceptValue domain=""A-Score"" occur=""1"">
                                    <stringValue>
                                    <typedValue>A</typedValue>
                                    </stringValue>
                                </conceptValue>
                                <concepts>
                                    <concept value=""0"" code=""Part a"" />
                                </concepts>
                                </conceptFact>
                            </conceptFinding>
                            </conceptFindings>
                            <aspectReferences />
                        </solution>");
            }

            public static class SolutionWithMC
            {
                public const string Id = "A2C577A9-DA35-4E15-9182-D6B53B65EC34"; //Value not visible

                public static XElement Data =
                    XElement.Parse(
                        @"<solution xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">  
                                                                   <keyFindings>
                                                                      <keyFinding id=""Opgave"" scoringMethod=""Dichotomous"">
                                                                        <keyFact id=""B-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <keyValue domain=""mc_1"" occur=""1"">
                                                                            <stringValue>
                                                                              <typedValue>B</typedValue>
                                                                            </stringValue>
                                                                          </keyValue>
                                                                        </keyFact>
                                                                      </keyFinding>
                                                                    </keyFindings>
                                                                    <conceptFindings>
                                                                      <conceptFinding id=""Opgave"" scoringMethod=""None"">
                                                                        <conceptFact id=""B-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <conceptValue domain=""mc_1"" occur=""1"">
                                                                            <stringValue>
                                                                              <typedValue>B</typedValue>
                                                                            </stringValue>
                                                                          </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFact id=""A[1]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <conceptValue domain=""mc_1"" occur=""1"">
                                                                            <stringValue>
                                                                              <typedValue>A</typedValue>
                                                                            </stringValue>
                                                                          </conceptValue>
                                                                        </conceptFact>
		                                                                <conceptFact id=""F[1]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <conceptValue domain=""mc_1"" occur=""1"">
                                                                            <stringValue>
                                                                              <typedValue>F</typedValue>
                                                                            </stringValue>
                                                                          </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFact id=""C[1]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <conceptValue domain=""mc_1"" occur=""1"">
                                                                            <stringValue>
                                                                              <typedValue>C</typedValue>
                                                                            </stringValue>
                                                                          </conceptValue>
                                                                        </conceptFact>
        
		                                                                <conceptFact id=""D[1]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <conceptValue domain=""mc_1"" occur=""1"">
                                                                            <stringValue>
                                                                              <typedValue>D</typedValue>
                                                                            </stringValue>
                                                                          </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFact id=""E[1]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <conceptValue domain=""mc_1"" occur=""1"">
                                                                            <stringValue>
                                                                              <typedValue>E</typedValue>
                                                                            </stringValue>
                                                                          </conceptValue>
                                                                        </conceptFact>
                                                                      </conceptFinding>
                                                                    </conceptFindings>
 
                                                                      <aspectReferences />
                                                                      <ItemScoreTranslationTable>
                                                                        <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                                                                        <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                                                                      </ItemScoreTranslationTable>
                                                                </solution>");
            }

            public static class Solution2FactSets1Fact
            {
                public const string Id = "A2C577A9-DA35-4E15-9182-88853B65EC34"; //Value not visible
                public static XElement Data = XElement.Parse(@"  <solution>
                                                                    <keyFindings>
                                                                      <keyFinding id=""sharedIntegerFinding"" scoringMethod=""Dichotomous"">
                                                                        <keyFact id=""5-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <keyValue domain=""integerScore"" occur=""1"">
                                                                            <integerValue>
                                                                              <typedValue>42</typedValue>
                                                                            </integerValue>
                                                                          </keyValue>
                                                                        </keyFact>
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
                                                                    <conceptFindings>
                                                                      <conceptFinding id=""sharedIntegerFinding"" scoringMethod=""None"">
                                                                        <conceptFact id=""5-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <conceptValue domain=""5-integerScore"" occur=""1"">
                                                                            <integerValue>
                                                                              <typedValue>42</typedValue>
                                                                            </integerValue>
                                                                          </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""1-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""2-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>14</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""1-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>14</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""2-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""3-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""3-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>3</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""4-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""4-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>7</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""3-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""3-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>7</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""4-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""4-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>3</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                        </conceptFactSet>
                                                                      </conceptFinding>
                                                                    </conceptFindings>
                                                                    <aspectReferences />
                                                                    <ItemScoreTranslationTable>
                                                                      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                                                                      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                                                                    </ItemScoreTranslationTable>
                                                                  </solution>");
            }

            public static class Solution2FactSets1Fact_NoConcept
            {
                public const string Id = "3DADDC92-7D36-4147-AF0A-639BE20ECD85"; //Value not visible
                public static XElement Data = XElement.Parse(@"  <solution>
                                                                    <keyFindings>
                                                                      <keyFinding id=""sharedIntegerFinding"" scoringMethod=""Dichotomous"">
                                                                        <keyFact id=""5-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <keyValue domain=""integerScore"" occur=""1"">
                                                                            <integerValue>
                                                                              <typedValue>42</typedValue>
                                                                            </integerValue>
                                                                          </keyValue>
                                                                        </keyFact>
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
                                                                  </solution>");
            }

            public static class Solution2InlineParamsInSet
            {
                public const string Id = "EC1BE9D2-9EA2-4928-B21E-82599C1815B3"; //Value not visible
                public static XElement Data = XElement.Parse(@"<solution>
	                                                                <keyFindings>
                                                                      <keyFinding id=""Opgave"" scoringMethod=""Dichotomous"">
                                                                        <keyFactSet>
                                                                          <keyFact id=""D-Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc"" occur=""1"">
                                                                              <stringValue>
                                                                                <typedValue>D</typedValue>
                                                                              </stringValue>
                                                                            </keyValue>
                                                                          </keyFact>
                                                                          <keyFact id=""A-I16746288-1b56-4c53-880d-2d54d060fba8"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""I16746288-1b56-4c53-880d-2d54d060fba8"" occur=""1"">
                                                                              <stringValue>
                                                                                <typedValue>A</typedValue>
                                                                              </stringValue>
                                                                            </keyValue>
                                                                          </keyFact>
                                                                        </keyFactSet>
                                                                      </keyFinding>
                                                                    </keyFindings>
                                                                </solution>");
            }

            public static class SolutionMRGroupedInTwoDistinctSets
            {
                public const string Id = "EC1BE9D2-9EA2-4928-B21E-8123123324B3"; //Value not visible
                public static XElement Data = XElement.Parse(@"<solution>
	                                                            <keyFindings>
                                                                        <keyFinding id=""Opgave"" scoringMethod=""Dichotomous"">
                                                                        <keyFactSet>
                                                                            <keyFact id=""D-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                                </booleanValue>
                                                                            </keyValue>
                                                                            </keyFact>
                                                                            <keyFact id=""C-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </keyValue>
                                                                            </keyFact>
                                                                        </keyFactSet>
                                                                        <keyFactSet>
                                                                            <keyFact id=""C-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                                </booleanValue>
                                                                            </keyValue>
                                                                            </keyFact>
                                                                            <keyFact id=""D-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </keyValue>
                                                                            </keyFact>
                                                                        </keyFactSet>
                                                                        <keyFactSet>
                                                                            <keyFact id=""A-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                                </booleanValue>
                                                                            </keyValue>
                                                                            </keyFact>
                                                                            <keyFact id=""B-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </keyValue>
                                                                            </keyFact>
                                                                        </keyFactSet>
                                                                        <keyFactSet>
                                                                            <keyFact id=""A-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </keyValue>
                                                                            </keyFact>
                                                                            <keyFact id=""B-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <keyValue domain=""mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </keyValue>
                                                                            </keyFact>
                                                                        </keyFactSet>
                                                                        </keyFinding>
                                                                    </keyFindings>
                                                                    <conceptFindings>
                                                                        <conceptFinding id=""Opgave"" scoringMethod=""None"">
                                                                        <conceptFactSet>
                                                                            <conceptFact id=""D-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""D-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <conceptFact id=""C-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""C-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                            <conceptFact id=""C-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""C-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <conceptFact id=""D-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""D-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                            <conceptFact id=""A-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""A-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>false</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <conceptFact id=""B-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""B-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                                <concept value=""8"" code=""Part 1"" />
                                                                            </concepts>
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                            <conceptFact id=""A-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""A-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <conceptFact id=""B-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""B-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                            <conceptFact id=""A[*]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""A[*]-mc_1"" occur=""1"">
                                                                                <catchAllValue />
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <conceptFact id=""B[*]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""B[*]-mc_1"" occur=""1"">
                                                                                <catchAllValue />
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                            <conceptFact id=""A[3]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""A[3]-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <conceptFact id=""B[3]-mc_1"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""B[3]-mc_1"" occur=""1"">
                                                                                <booleanValue>
                                                                                <typedValue>true</typedValue>
                                                                                </booleanValue>
                                                                            </conceptValue>
                                                                            </conceptFact>
                                                                            <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        </conceptFinding>
                                                                    </conceptFindings>
                                                                </solution>");
            }

            public static class SolutionWithCatchAll
            {
                public const string Id = "8532E917-A013-4705-8C76-5D6A25A0C4EB"; //Value not visible
                public static XElement Data = XElement.Parse(@"<solution>
                                                                <keyFindings>
                                                                  <keyFinding id=""sharedIntegerFinding"" scoringMethod=""Dichotomous"">
                                                                    <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                      <keyValue domain=""integerScore"" occur=""1"">
                                                                        <integerValue>
                                                                          <typedValue>6</typedValue>
                                                                        </integerValue>
                                                                      </keyValue>
                                                                    </keyFact>
                                                                  </keyFinding>
                                                                </keyFindings>
                                                                <conceptFindings>
                                                                  <conceptFinding id=""sharedIntegerFinding"" scoringMethod=""None"">
                                                                    <conceptFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                      <conceptValue domain=""1-integerScore"" occur=""1"">
                                                                        <integerValue>
                                                                          <typedValue>6</typedValue>
                                                                        </integerValue>
                                                                      </conceptValue>
                                                                      <concepts />
                                                                    </conceptFact>
                                                                    <conceptFact id=""1[*]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                      <conceptValue domain=""1[*]-integerScore"" occur=""1"">
                                                                        <catchAllValue />
                                                                      </conceptValue>
                                                                      <concepts />
                                                                    </conceptFact>
                                                                    <conceptFact id=""1[2]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                      <conceptValue domain=""1[2]-integerScore"" occur=""1"">
                                                                        <integerValue>
                                                                          <typedValue>7</typedValue>
                                                                        </integerValue>
                                                                      </conceptValue>
                                                                      <concepts />
                                                                    </conceptFact>
                                                                    <conceptFact id=""1[3]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                      <conceptValue domain=""1[3]-integerScore"" occur=""1"">
                                                                        <integerValue>
                                                                          <typedValue>8</typedValue>
                                                                        </integerValue>
                                                                      </conceptValue>
                                                                      <concepts />
                                                                    </conceptFact>
                                                                  </conceptFinding>
                                                                </conceptFindings>
                                                                <aspectReferences />
                                                                <ItemScoreTranslationTable>
                                                                  <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                                                                  <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                                                                </ItemScoreTranslationTable>
                                                              </solution>");
            }

            public static class SolutionWithSetsAndCathcAll
            {
                public const string Id = "684BBF34-4184-4A22-9670-417A93A9D6C6"; //Value not visible
                public static XElement Data = XElement.Parse(@"<solution>
                                                                    <keyFindings>
                                                                      <keyFinding id=""sharedIntegerFinding"" scoringMethod=""Dichotomous"">
                                                                        <keyFact id=""5-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <keyValue domain=""integerScore"" occur=""1"">
                                                                            <integerValue>
                                                                              <typedValue>42</typedValue>
                                                                            </integerValue>
                                                                          </keyValue>
                                                                        </keyFact>
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
                                                                    <conceptFindings>
                                                                      <conceptFinding id=""sharedIntegerFinding"" scoringMethod=""None"">
                                                                        <conceptFact id=""5-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                          <conceptValue domain=""5-integerScore"" occur=""1"">
                                                                            <integerValue>
                                                                              <typedValue>42</typedValue>
                                                                            </integerValue>
                                                                          </conceptValue>
                                                                        </conceptFact>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""1-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""2-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>14</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""1-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>14</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""2-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>6</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""3-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""3-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>3</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""4-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""4-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>7</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""3-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""3-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>7</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""4-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""4-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>3</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""1[*]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""1[*]-integerScore"" occur=""1"">
                                                                              <catchAllValue />
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""2[*]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""2[*]-integerScore"" occur=""1"">
                                                                              <catchAllValue />
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""1[3]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""1[3]-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>7</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""2[3]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""2[3]-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>15</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                        <conceptFactSet>
                                                                          <conceptFact id=""1[4]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""1[4]-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>15</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <conceptFact id=""2[4]-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                                                                            <conceptValue domain=""2[4]-integerScore"" occur=""1"">
                                                                              <integerValue>
                                                                                <typedValue>7</typedValue>
                                                                              </integerValue>
                                                                            </conceptValue>
                                                                          </conceptFact>
                                                                          <concepts xmlns=""http://Cito.Tester.Server/xml/serialization"" />
                                                                        </conceptFactSet>
                                                                      </conceptFinding>
                                                                    </conceptFindings>
                                                                    <aspectReferences />
                                                                    <ItemScoreTranslationTable>
                                                                      <ItemScoreTranslationTableEntry rawScore=""0"" translatedScore=""0"" />
                                                                      <ItemScoreTranslationTableEntry rawScore=""1"" translatedScore=""1"" />
                                                                    </ItemScoreTranslationTable>
                                                                  </solution>");
            }

        }

        #endregion
    }
}