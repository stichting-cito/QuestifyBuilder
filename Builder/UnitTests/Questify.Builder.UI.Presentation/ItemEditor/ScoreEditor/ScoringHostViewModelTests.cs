
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.DTO;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;
using Questify.Builder.UnitTests.Framework.Faketory;
using Constants = Questify.Builder.UI.Wpf.Presentation.ItemEditor.Constants;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor.ScoreEditor
{
    [TestClass]
    public class ScoringHostViewModelTests : ItemEditorTestBase
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [FakeObjectFactoryBehavior(ItemEditorObjectStrategy.ReturnNull)]
        public void ScoreEditorWithVmInError()
        {
            var ItemEditorVm_InError = new ItemEditorViewModel(); ItemEditorVm_InError.DoActualLoadOnItemId(Guid.NewGuid());
            var view = A.Fake<IWorkSpaceAware>();
            var viewAwareStatus = new TestViewAwareStatus(); view.WorkSpaceContextualData.DataValue = ItemEditorVm_InError;
            var scoreHostVm = new ScoringHostViewModel(viewAwareStatus);
            viewAwareStatus.View = view;
            viewAwareStatus.SimulateViewIsLoadedEvent();
            Assert.IsNull(scoreHostVm.ScoreEditor.DataValue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        public void SwitchTabTest_Test()
        {
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_MultiChoice(args));

            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);
            var fakeIOnSwitchTabItemVMTasks = A.Fake<IOnSwitchTabItemVMTasks>();
            scoreHostVm.ScoreEditor.DataValue.ViewModelInstance = fakeIOnSwitchTabItemVMTasks;

            itemEditorVm.SelectedTab.DataValue = 3;
            A.CallTo(() => fakeIOnSwitchTabItemVMTasks.DoActionToPushChangesToModel()).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        [FakeObjectFactoryBehavior(ItemEditorObjectStrategy.AbsoluteValidMinimum)]
        [FakeServiceHandler()]
        public void SaveTestEvilIsCalled_Test()
        {
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_MultiChoice(args));

            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);
            var fakeIViewModel2ViewCommandSupport = A.Fake<IViewModel2ViewCommandSupport>();
            scoreHostVm.ScoreEditor.DataValue.ViewModelInstance = fakeIViewModel2ViewCommandSupport;

            FakeDal.Add.ItemTemplate("FakeLayoutTemplateSourceName", x => x.ResourceData = new ResourceDataEntity() { BinData = new System.Text.UTF8Encoding().GetBytes("<ItemLayoutTemplate></ItemLayoutTemplate>") });

            itemEditorVm.Save.Execute(null);

            A.CallTo(() => fakeIViewModel2ViewCommandSupport.DoPreSaveTasks()).MustHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring")]
        public void Get_V2_ScoreEditorTest()
        {
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_MultiChoice(args));

            var ItemEditorVm = new ItemEditorViewModel(); ItemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            var view = A.Fake<IWorkSpaceAware>();
            var viewAwareStatus = new TestViewAwareStatus(); view.WorkSpaceContextualData.DataValue = ItemEditorVm;
            var scoreHostVm = new ScoringHostViewModel(viewAwareStatus);
            viewAwareStatus.View = view;
            viewAwareStatus.SimulateViewIsLoadedEvent();
            var shouldBeTrue = (Constants.ScoringWorkSpaceV2 == scoreHostVm.ScoreEditor.DataValue.ViewLookupKey) || (Constants.ScoringWorkSpaceV2Adv == scoreHostVm.ScoreEditor.DataValue.ViewLookupKey);
            Assert.IsTrue(shouldBeTrue);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void SetAutoScoringOff_IntegerMultiChoiceCombi_RemovesFindings()
        {
            Solution solution = _mixedSolutionMultiChoiceInteger.To<Solution>();
            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_IntegerAndChoice(args, solution));

            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);
            int keyFindingsBeforeAutoScoringOff = itemEditorVm.AssessmentItem.DataValue.Solution.Findings.Count;

            scoreHostVm.ExecuteAutoScoringOffCommand();

            Assert.AreEqual(1, keyFindingsBeforeAutoScoringOff); Assert.AreEqual(0, itemEditorVm.AssessmentItem.DataValue.Solution.Findings.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void SetAutoScoringOff_AspectMultiChoiceCombi_RemovesFindings_LeavesAspectRef()
        {
            Solution solution = _mixedSolutionMultiChoiceAspect.To<Solution>();
            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_AspectAndChoice(args, solution));

            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);
            int keyFindingsBeforeAutoScoringOff = itemEditorVm.AssessmentItem.DataValue.Solution.Findings.Count;

            scoreHostVm.ExecuteAutoScoringOffCommand();

            Assert.AreEqual(1, keyFindingsBeforeAutoScoringOff); Assert.AreEqual(0, itemEditorVm.AssessmentItem.DataValue.Solution.Findings.Count); Assert.AreEqual(1, itemEditorVm.AssessmentItem.DataValue.Solution.AspectReferenceSetCollection.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void ToggleAutoScoringOffAndBackOn_IntegerMultiChoiceCombi_LeavesEmptyFindings()
        {
            Solution solution = _mixedSolutionMultiChoiceInteger.To<Solution>();
            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_IntegerAndChoice(args, solution));

            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);
            int keyFindingsBeforeAutoScoringOff = itemEditorVm.AssessmentItem.DataValue.Solution.Findings.Count;

            scoreHostVm.ExecuteAutoScoringOffCommand();
            int keyFindingsAfterAutoScoringOff = itemEditorVm.AssessmentItem.DataValue.Solution.Findings.Count;
            scoreHostVm.ExecuteAutoScoringOnCommand();

            Assert.AreEqual(1, keyFindingsBeforeAutoScoringOff);
            Assert.AreEqual(0, keyFindingsAfterAutoScoringOff);
            Assert.AreEqual(1, itemEditorVm.AssessmentItem.DataValue.Solution.Findings.Count);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CanSetAutoScoringOff_AspectMultiChoiceCombi_ReturnsTrue()
        {
            Solution solution = _mixedSolutionMultiChoiceAspect.To<Solution>();
            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_AspectAndChoice(args, solution));

            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);

            bool result = scoreHostVm.CanExecuteAutoScoringOffCommand();

            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CanSetAutoScoringOff_AspectMultiChoiceCombi_AutoScoringAlreadyOff_ReturnsFalse()
        {
            Solution solution = _mixedSolutionMultiChoiceAspectAutoScoringOff.To<Solution>();
            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_AspectAndChoice(args, solution));

            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);

            bool result = scoreHostVm.CanExecuteAutoScoringOffCommand();

            Assert.IsFalse(result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CanSetAutoScoringOn_AspectMultiChoiceCombi_ReturnsTrue()
        {
            Solution solution = _mixedSolutionMultiChoiceAspectAutoScoringOff.To<Solution>();
            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_AspectAndChoice(args, solution));

            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);

            bool result = scoreHostVm.CanExecuteAutoScoringOnCommand();

            Assert.IsTrue(result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CanSetAutoScoringOn_AspectMultiChoiceCombi_AutoScoringAlreadyOn_ReturnsFalse()
        {
            Solution solution = _mixedSolutionMultiChoiceAspect.To<Solution>();
            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_AspectAndChoice(args, solution));

            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);

            bool result = scoreHostVm.CanExecuteAutoScoringOnCommand();

            Assert.IsFalse(result);
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), TestCategory("Scoring"), TestCategory("ScoringAdv")]
        public void CanSetAutoScoringOn_AspectInteractionOnly_ReturnsFalse()
        {
            Solution solution = _mixedSolutionMultiChoiceAspect.To<Solution>();
            ItemEditorViewModel itemEditorVm = null;
            ScoringHostViewModel scoreHostVm = null;
            var objFact = FakeItemEditorObjectFactory.MakeNewFake();
            A.CallTo(() => objFact.GetRequiredObjectsForItemWithId(A<Guid>.Ignored)).ReturnsLazily(args => GetParameterSetWithScoringParam_AspectOnly(args, solution));

            GetItemEditorViewModel(ref itemEditorVm, ref scoreHostVm);

            bool result = scoreHostVm.CanExecuteAutoScoringOnCommand();

            Assert.IsFalse(result);
        }

        private void GetItemEditorViewModel(ref ItemEditorViewModel itemEditorVm, ref ScoringHostViewModel scoreHostVm)
        {
            itemEditorVm = new ItemEditorViewModel();
            itemEditorVm.DoActualLoadOnItemId(Guid.NewGuid());
            scoreHostVm = GetScoringHostVM(itemEditorVm);
            itemEditorVm.ScoreWorkspace.DataValue.ViewModelInstance = scoreHostVm; itemEditorVm.SelectedTab.DataValue = 2;
        }

        private ItemEditorObjectFactoryResult GetParameterSetWithScoringParam_MultiChoice(FakeItEasy.Core.IFakeObjectCall args, Solution solution = null)
        {
            var scoringPrms = new List<ScoringParameter>() { new MultiChoiceScoringParameter() };
            return GetParameterSetWithScoringParam(args, scoringPrms, solution);
        }

        private ItemEditorObjectFactoryResult GetParameterSetWithScoringParam_IntegerAndChoice(FakeItEasy.Core.IFakeObjectCall args, Solution solution = null)
        {
            var scoringPrms = new List<ScoringParameter>
            {
                new IntegerScoringParameter() { ControllerId = "integerScore", FindingOverride = "DefaultFinding" }.AddSubParameters("1", "2"),
                new ChoiceScoringParameter() { ControllerId = "McScore", FindingOverride = "DefaultFinding", MaxChoices = 1 }.AddSubParameters("A", "B")
            };
            return GetParameterSetWithScoringParam(args, scoringPrms, solution);
        }

        private ItemEditorObjectFactoryResult GetParameterSetWithScoringParam_AspectAndChoice(FakeItEasy.Core.IFakeObjectCall args, Solution solution = null)
        {
            var scoringPrms = new List<ScoringParameter>
            {
                new AspectScoringParameter() { ControllerId = "extendedTextEntryController" },
                new ChoiceScoringParameter() { ControllerId = "McScore", FindingOverride = "DefaultFinding", MaxChoices = 1 }.AddSubParameters("A", "B")
            };
            return GetParameterSetWithScoringParam(args, scoringPrms, solution);
        }

        private ItemEditorObjectFactoryResult GetParameterSetWithScoringParam_AspectOnly(FakeItEasy.Core.IFakeObjectCall args, Solution solution = null)
        {
            var scoringPrms = new List<ScoringParameter>() { new AspectScoringParameter() { ControllerId = "extendedTextEntryController" } };
            return GetParameterSetWithScoringParam(args, scoringPrms, solution);
        }

        private ItemEditorObjectFactoryResult GetParameterSetWithScoringParam(FakeItEasy.Core.IFakeObjectCall args, List<ScoringParameter> scoringPrms, Solution solution = null)
        {
            var prmset = new ParameterSetCollection();
            var prmColl = new ParameterCollection(); prmset.Add(prmColl);
            scoringPrms.ForEach(sp => prmColl.InnerParameters.Add(sp));

            AssessmentItem item = new AssessmentItem();
            if (solution != null)
            {
                item.Solution = solution;
            }

            return ItemEditorObjectFactoryResult.Create(
                new ItemResourceEntity(),
                item,
                prmset,
                FakeResourceManager.MakeResourceManagerBase(),
                null,
                false);
        }

        private ScoringHostViewModel GetScoringHostVM(ItemEditorViewModel itemEditorVM)
        {
            var view = A.Fake<IWorkSpaceAware>();
            var viewAwareStatus = new TestViewAwareStatus(); view.WorkSpaceContextualData.DataValue = itemEditorVM;
            var scoreHostVm = new ScoringHostViewModel(viewAwareStatus);

            viewAwareStatus.View = view;
            viewAwareStatus.SimulateViewIsLoadedEvent();
            return scoreHostVm;
        }

        private readonly XElement _mixedSolutionMultiChoiceInteger =
            XElement.Parse(@"<solution>
             <keyFindings>
                 <keyFinding id=""DefaultFinding"" scoringMethod=""Dichotomous"">
                     <keyFactSet>
                         <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                             <keyValue domain=""integerScore"" occur=""1"">
                                 <integerValue>
                                     <typedValue>3</typedValue>
                                 </integerValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                             <keyValue domain=""integerScore"" occur=""1"">
                                 <integerValue>
                                     <typedValue>7</typedValue>
                                 </integerValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id=""A-McScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                             <keyValue domain=""McScore"" occur=""1"">
                                 <stringValue>
                                     <typedValue>A</typedValue>
                                 </stringValue>
                             </keyValue>
                         </keyFact>
                     </keyFactSet>
                     <keyFactSet>
                         <keyFact id=""1-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                             <keyValue domain=""integerScore"" occur=""1"">
                                 <integerValue>
                                     <typedValue>7</typedValue>
                                 </integerValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id=""2-integerScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                             <keyValue domain=""integerScore"" occur=""1"">
                                 <integerValue>
                                     <typedValue>3</typedValue>
                                 </integerValue>
                             </keyValue>
                         </keyFact>
                         <keyFact id=""B-McScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                             <keyValue domain=""McScore"" occur=""1"">
                                 <stringValue>
                                     <typedValue>B</typedValue>
                                 </stringValue>
                             </keyValue>
                         </keyFact>
                     </keyFactSet>
                 </keyFinding>
             </keyFindings>
             <aspectReferences/>
         </solution>");

        private readonly XElement _mixedSolutionMultiChoiceAspect =
            XElement.Parse(@"<solution>
             <keyFindings>
                 <keyFinding id=""DefaultFinding"" scoringMethod=""Dichotomous"">
                     <keyFact id=""A-McScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                         <keyValue domain=""McScore"" occur=""1"">
                             <stringValue>
                                 <typedValue>B</typedValue>
                             </stringValue>
                         </keyValue>
                     </keyFact>
                 </keyFinding>
             </keyFindings>
             <aspectReferences>
                 <aspectReferenceSet id=""extendedTextEntryController"">
                     <aspectReference maxScore=""2"" src=""Inhoud"">&lt;p id=""c1-id-12"" xmlns=""http://www.w3.org/1999/xhtml""&gt;te&lt;/p&gt;</aspectReference>
                 </aspectReferenceSet>
             </aspectReferences>
         </solution>");

        private readonly XElement _mixedSolutionMultiChoiceAspectAutoScoringOff =
            XElement.Parse(@"<solution autoScoring=""false"">
             <keyFindings>
                 <keyFinding id=""DefaultFinding"" scoringMethod=""Dichotomous"">
                     <keyFact id=""A-McScore"" score=""1"" xmlns=""http://Cito.Tester.Server/xml/serialization"">
                         <keyValue domain=""McScore"" occur=""1"">
                             <stringValue>
                                 <typedValue>B</typedValue>
                             </stringValue>
                         </keyValue>
                     </keyFact>
                 </keyFinding>
             </keyFindings>
             <aspectReferences>
                 <aspectReferenceSet id=""extendedTextEntryController"">
                     <aspectReference maxScore=""2"" src=""Inhoud"">&lt;p id=""c1-id-12"" xmlns=""http://www.w3.org/1999/xhtml""&gt;te&lt;/p&gt;</aspectReference>
                 </aspectReferenceSet>
             </aspectReferences>
         </solution>");
    }
}
