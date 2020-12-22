
using System;
using Cinch;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UnitTests.Fakes;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class OpenItemEditor_SubView_WithErrors : ItemEditorTestBase
    {

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void ItmVmInError_PresentationVM_Test()
        {
            var ItemEditorVm_InError = new ItemEditorViewModel(); ItemEditorVm_InError.DoActualLoadOnItemId(Guid.NewGuid());
            var view = A.Fake<IPresentationControl>(); view.WorkSpaceContextualData.DataValue = ItemEditorVm_InError;
            var fake_MsgBoxServ = FakeMessageBoxService.MakeNewFake();
            var viewAwareStatus = new TestViewAwareStatus();
            var resouceEditor = A.Fake<IResourceEditorService>();
            var presentationVm = new PresentationViewModel(viewAwareStatus, resouceEditor);
            viewAwareStatus.View = view;
            viewAwareStatus.SimulateViewIsLoadedEvent();
            A.CallTo(() => view.RefreshPreview()).MustNotHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void ItmVmInError_MetaDataVM_Test()
        {
            var ItemEditorVm_InError = new ItemEditorViewModel(); ItemEditorVm_InError.DoActualLoadOnItemId(Guid.NewGuid());
            var view = A.Fake<IMetaDataControl>(); view.WorkSpaceContextualData.DataValue = ItemEditorVm_InError;
            var fake_MsgBoxServ = FakeMessageBoxService.MakeNewFake();
            var viewAwareStatus = new TestViewAwareStatus();
            var metaVm = new MetaDataViewModel(viewAwareStatus, fake_MsgBoxServ, null);
            viewAwareStatus.View = view;
            viewAwareStatus.SimulateViewIsLoadedEvent();
            A.CallTo(() => view.Update(A<AssessmentItem>.Ignored, A<ResourceEntity>.Ignored)).MustNotHaveHappened();
        }

        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemEditor"), FakeObjectFactoryBehavior(ItemEditorObjectStrategy.GiveException)]
        public void ItmVmInError_SourceVm_Test()
        {
            var ItemEditorVm_InError = new ItemEditorViewModel(); ItemEditorVm_InError.DoActualLoadOnItemId(Guid.NewGuid());
            var view = A.Fake<ISourceControl>(); view.WorkSpaceContextualData.DataValue = ItemEditorVm_InError;
            var fake_MsgBoxServ = FakeMessageBoxService.MakeNewFake();
            var viewAwareStatus = new TestViewAwareStatus();
            var sourceVm = new SourceViewerViewModel(viewAwareStatus);
            viewAwareStatus.View = view;
            viewAwareStatus.SimulateViewIsLoadedEvent();
            A.CallTo(() => view.SetAssessmentItem(A<AssessmentItem>.Ignored)).MustNotHaveHappened();
        }
    }
}
