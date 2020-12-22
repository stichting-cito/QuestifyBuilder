
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Cinch;
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
    public class EncodingScoringVM_Concept_Tests : UsesTheItemEditorVM
    {
        private Solution _solution;
        private IItemEditorObjectFactory fake_Factory;

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void NoConcepts_NoData()
        {
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            Assert.AreEqual(0, csVM.Data.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void ConceptHas2SubParts_DataShouldHaveCount2()
        {
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            Assert.AreEqual(2, csVM.Data.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void ConceptHas2SubParts_AllShouldBeSelectable()
        {
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            foreach (var e in csVM.Data)
            {
                Assert.IsTrue(e.CanSelect.DataValue);
            }
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part1)]
        public void ConceptHas2SubParts_ParentNotSelected_AllShouldBeUnSelectable()
        {
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            csVM.MainScoreHierarchyPart.Selected.DataValue = false;
            foreach (var e in csVM.Data)
            {
                Assert.IsFalse(e.CanSelect.DataValue);
            }
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part2)]
        public void AddConceptWithTotalOf5Childs_5NodesFound()
        {

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(5, csVM.Data.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept, ConceptPartId = ConceptCreator.SomeConcept_Part2)]
        public void Part2_2_Has_2_Children()
        {

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            var part2 = csVM.Data.First(e => e.PartName == "Part 2.1");
            Assert.AreEqual(2, part2.Children.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.ArjanConcept, ConceptPartId = ConceptCreator.ArjanConcept_ENG)]
        public void ExampleOfArjan()
        {

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.AreEqual(8, csVM.Data.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.ArjanConcept, ConceptPartId = ConceptCreator.ArjanConcept_ENG)]
        public void SwitchTest_SelectP1_ResultsIn_P1_2_pAS_NotSelectable()
        {

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p1_2 = csVM.Data.First(e => e.PartName == "Part 1.2");
            var pAS = csVM.Data.First(e => e.PartName == "AS");
            p1.Selected.DataValue = true;
            Assert.IsTrue(p1_2.CanSelect.DataValue);
            Assert.IsFalse(pAS.CanSelect.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.ArjanConcept, ConceptPartId = ConceptCreator.ArjanConcept_ENG)]
        public void SwitchTest_SelectP1_And_P1_2_ResultsIn_pAS_Selectable()
        {

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p1_2 = csVM.Data.First(e => e.PartName == "Part 1.2");
            var pAS = csVM.Data.First(e => e.PartName == "AS");
            p1.Selected.DataValue = true;
            p1_2.Selected.DataValue = true;
            Assert.IsTrue(pAS.CanSelect.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.ArjanConcept, ConceptPartId = ConceptCreator.ArjanConcept_ENG)]
        public void ArjanVoorbeeldCountChildren()
        {

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();
            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p1_1 = csVM.Data.First(e => e.PartName == "Part 1.1");
            var p1_2 = csVM.Data.First(e => e.PartName == "Part 1.2");

            var p2 = csVM.Data.First(e => e.PartName == "Part 2");
            var p2_1 = csVM.Data.First(e => e.PartName == "Part 2.1");
            var p2_2 = csVM.Data.First(e => e.PartName == "Part 2.2");

            Assert.AreEqual(2, p1.Children.Count, "Part 1 does not have 2 children");
            Assert.AreEqual(0, p1_1.Children.Count, "Part 1.1 should not have children");
            Assert.AreEqual(1, p1_2.Children.Count, "Part 1.2 should exactly 1 child");
            Assert.AreEqual(2, p2.Children.Count, "Part 2 does not have 2 children");
            Assert.AreEqual(0, p2_1.Children.Count, "Part 1.1 should not have children");
            Assert.AreEqual(1, p2_2.Children.Count, "Part 2.2 should exactly 1 child");
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.ArjanConcept, ConceptPartId = ConceptCreator.ArjanConcept_ENG)]
        [Description("voorbeeld van Arjan Aanink")]
        public void SwitchTest_SelectP1_And_P1_2_AndThenDeselect_p1_ResultsIn_pAS_NotSelectable()
        {

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p1_2 = csVM.Data.First(e => e.PartName == "Part 1.2");
            var pAS = csVM.Data.First(e => e.PartName == "AS");
            p1.Selected.DataValue = true;
            p1_2.Selected.DataValue = true;
            p1.Selected.DataValue = false;
            Assert.IsFalse(pAS.CanSelect.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept3, ConceptPartId = ConceptCreator.SomeConcept3_Root)]
        public void TopLevelPartsWillNotReOccurInChildren()
        {
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            Assert.AreEqual(6, csVM.Data.Count);

            Assert.AreEqual(0, csVM.Data.First(e => e.PartName == "Part 1").Depth);
            Assert.AreEqual(0, csVM.Data.First(e => e.PartName == "Part 2").Depth);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void CheckSharedPartEnabledState()
        {

            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p2 = csVM.Data.First(e => e.PartName == "Part 2");
            var p1a = p1.Children.First(e => e.PartName == "Part a");
            var p2a = p2.Children.First(e => e.PartName == "Part a");

            p2a.Selected.DataValue = true;

            Assert.IsTrue(p2a.Selected.DataValue);
            Assert.IsTrue(p1a.Selected.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void CheckSharedPartEnabledState_2()
        {


            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p2 = csVM.Data.First(e => e.PartName == "Part 2");
            var partAs = csVM.Data.Where(e => e.PartName == "Part a").ToList();
            var p1a = partAs[0];
            var p2a = partAs[1];

            p1a.Selected.DataValue = true;

            var single1 = p1a.ConceptScorePart.First();
            var single2 = p2a.ConceptScorePart.First();

            Assert.IsTrue(p1a.Selected.DataValue);
            Assert.IsTrue(p2a.Selected.DataValue);
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void UncheckSharedPart_BothShouldBeDisabled()
        {


            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p2 = csVM.Data.First(e => e.PartName == "Part 2");
            var partAs = csVM.Data.Where(e => e.PartName == "Part a").ToList();
            var p1a = partAs[0];
            var p2a = partAs[1];
            p1a.Selected.DataValue = true;

            p2a.Selected.DataValue = false;

            var single1 = p1a.ConceptScorePart.First();
            var single2 = p2a.ConceptScorePart.First();

            Assert.IsFalse(p1a.Selected.DataValue);
            Assert.IsFalse(p2a.Selected.DataValue);
        }



        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void SelectPartConceptScoresAreInitializedTo0()
        {


            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p2 = csVM.Data.First(e => e.PartName == "Part 2");
            var partAs = csVM.Data.Where(e => e.PartName == "Part a").ToList();
            var p1a = partAs[0];
            var p2a = partAs[1];

            WriteSolution("Arrange");

            p1a.Selected.DataValue = true;


            WriteSolution("Act");
            var _ConceptManipulator = csVM.CurrentScoringMap.GetConceptManipulator(_solution);
            var result = _ConceptManipulator.GetScoreForPart("Part a", new string[] { "A" });
            Assert.AreEqual(1, result.Count(), "A single result was expected");
            Assert.AreEqual(0, result.First());
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void UnSelectPartConceptScoresAreRemoved()
        {


            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p2 = csVM.Data.First(e => e.PartName == "Part 2");
            var partAs = csVM.Data.Where(e => e.PartName == "Part a").ToList();
            var p1a = partAs[0];
            p1a.Selected.DataValue = true;

            WriteSolution("Arrange");

            p1a.Selected.DataValue = false;

            WriteSolution("Act");
            var _ConceptManipulator = csVM.CurrentScoringMap.GetConceptManipulator(_solution);
            var result = _ConceptManipulator.GetScoreForPart("Part a", new string[] { "A" });
            Assert.AreEqual(1, result.Count(), "A single result was expected");
            Assert.IsNull(result.First(), "value is now null");

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void UnSelectPart1ConceptScoresAreRemovedAndForChildren()
        {


            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();

            var p1 = csVM.Data.First(e => e.PartName == "Part 1");
            var p2 = csVM.Data.First(e => e.PartName == "Part 2");
            var partAs = csVM.Data.Where(e => e.PartName == "Part a").ToList();
            var p1a = partAs[0];

            p1.Selected.DataValue = true;
            p1a.Selected.DataValue = true;

            WriteSolution("Arrange");

            p1.Selected.DataValue = false;

            WriteSolution("Act");
            var _ConceptManipulator = csVM.CurrentScoringMap.GetConceptManipulator(_solution);
            var result = _ConceptManipulator.GetScoreForPart("Part a", new string[] { "A" });
            Assert.AreEqual(1, result.Count(), "A single result was expected");
            Assert.IsNull(result.First(), "value is now null");

        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void LoadEncodingEditor_CurrentScoringMap_IsSet()
        {
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());

            ViewAwareStatus.SimulateViewIsLoadedEvent();
            WriteSolution("Act");
            Assert.IsNotNull(csVM.CurrentScoringMap);
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void LoadEncodingEditor_AvailableParams_IsSet()
        {
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent(); WriteSolution("Act");
            Assert.IsNotNull(csVM.AvailableParams);
            Assert.IsNotNull(csVM.AvailableParams.Count > 0);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        [AddConcept(ConceptCreator.SomeConcept4, ConceptPartId = ConceptCreator.SomeConcept4_Root)]
        public void LoadEncodingEditor_AvailableParams_contaisn_CurrentScoringMap()
        {
            var csVM = new EncodingScoringViewModel(ViewAwareStatus, A.Fake<IAddAnswerCategory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent(); WriteSolution("Act");
            Assert.IsTrue(csVM.AvailableParams.ContainsKey(csVM.CurrentScoringMap));
        }

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            fake_Factory = FakeItemEditorObjectFactory.MakeNewFake();

            A.CallTo(() => FakeItemEditorVM.ItemEditorObjectFactory).ReturnsLazily((arg) => fake_Factory);

            A.CallTo(() => fake_Factory.PopulateConceptCustomBankPropertyHierarchy(A<Guid>.Ignored))
     .ReturnsLazily((args) => base.FakeConceptHandler.GetPartById(args.GetArgument<Guid>(0)));
        }

        [TestCleanup]
        public override void Clean()
        {
            base.Clean();

            fake_Factory = null;
            FakeItemEditorObjectFactory.MakeNewFake();
        }


        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void CanAddAdditionalDerivates_False_For_EmptyCombinedScoringMapKey()
        {
            var combinedScoringMapKey = CombinedScoringMapKey.Create(new ScoringMapKey[0]);
            Assert.IsFalse(combinedScoringMapKey.CanAddAdditionalDerivates());
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void CanAddAdditionalDerivates_True_For_NonChoiceScoringParameter()
        {
            var combinedScoringMapKey = CombinedScoringMapKey.Create(new ScoringMapKey[] { new ScoringMapKey(new IntegerScoringParameter(), "1") });
            Assert.IsTrue(combinedScoringMapKey.CanAddAdditionalDerivates());
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void CanAddAdditionalDerivates_False_For_NonGroupedChoiceScoringParameter()
        {
            var combinedScoringMapKey = CombinedScoringMapKey.Create(new ScoringMapKey[] { new ScoringMapKey(new ChoiceScoringParameter(), "1") });
            Assert.IsFalse(combinedScoringMapKey.CanAddAdditionalDerivates());
        }
        [TestMethod, TestCategory("ViewModel"), TestCategory("Scoring"), TestCategory("Concept")]
        public void CanAddAdditionalDerivates_True_For_GroupedChoiceScoringParameter()
        {
            var combinedScoringMapKey = CombinedScoringMapKey.Create(new ScoringMapKey[] { new ScoringMapKey(new ChoiceScoringParameter(), "1") }, new int[] { 0, 1 });
            Assert.IsTrue(combinedScoringMapKey.CanAddAdditionalDerivates());
        }

        internal override void SetFakeViewDataContext(ref IWorkSpaceAware fakeView,
            IItemEditorViewModel itemEditorViewModel)
        {
            _solution = new Solution();
            var scorePrm = new ChoiceScoringParameter() { InlineId = "Score" };
            scorePrm.Value = new ParameterSetCollection();
            scorePrm.Value.Add(new ParameterCollection() { Id = "A" });
            scorePrm.Value.Add(new ParameterCollection() { Id = "B" });
            scorePrm.Value.Add(new ParameterCollection() { Id = "C" });
            scorePrm.GetScoreManipulator(_solution).SetKey("A");

            var prms =
                itemEditorViewModel.ParameterSetCollection.DataValue.FlattenParameters()
                    .Where(p => p is ScoringParameter)
                    .Select(e => (ScoringParameter)e);
            var lst = prms.ToList();
            lst.Add(scorePrm);
            fakeView.WorkSpaceContextualData.DataValue =
                new Tuple<IEnumerable<ScoringParameter>, Solution, IItemEditorViewModel>(lst, _solution,
                    itemEditorViewModel);
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
                xml.Serialize(stream, _solution);
                Debug.WriteLine(stream.ToString());
            }
        }
    }
}