
using System.Diagnostics;
using Cinch;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.GenericControls.ConsumerInterfaces;
using Questify.Builder.UI.Wpf.Presentation.GenericControls.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.GenericControls
{
    [TestClass]
    public class MetaDataViewModelTest
    {
        TestViewAwareStatus _viewAwareStatus;
        TestViewAwareStatus ViewAwareStatus { get { return _viewAwareStatus; } }
        private IMetadataViewConsumer _fakeMetadataViewConsumer;

        [TestInitialize]
        public void InitializeTest()
        {
            _viewAwareStatus = new TestViewAwareStatus();
            var fakeView = A.Fake<IWorkSpaceAware>();

            _fakeMetadataViewConsumer = A.Fake<IMetadataViewConsumer>();
            fakeView.WorkSpaceContextualData.DataValue = _fakeMetadataViewConsumer;
            Debug.Assert(fakeView.WorkSpaceContextualData != null, "You should had correctly initialized the view");

            _viewAwareStatus.View = fakeView;
        }

        [TestMethod]
        public void ChangeNameValueWithLegalValue_AllIsOk()
        {
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                A.Fake<IMessageBoxService>(),
                                                A.Fake<IResourcePropertyDialogService>(),
                                                A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            metaDataViewModel.Name.DataValue = "SomeValue";
            Assert.IsTrue(metaDataViewModel.Name.IsValid, "Expected to be valid");
        }

        [TestMethod]
        public void ChangeNameValueWithNullValue()
        {
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                A.Fake<IMessageBoxService>(),
                                                A.Fake<IResourcePropertyDialogService>(),
                                                A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            metaDataViewModel.Name.DataValue = null;
            Assert.IsTrue(metaDataViewModel.Name.IsValid, "Expected to be valid, default value.");
        }

        [TestMethod]
        public void ChangeNameValueWithStringEmptyValue_ShouldNotBeValid()
        {
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                A.Fake<IMessageBoxService>(),
                                                A.Fake<IResourcePropertyDialogService>(),
                                                A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            metaDataViewModel.Name.DataValue = string.Empty;
            Assert.IsFalse(metaDataViewModel.Name.IsValid, "Expected to be invalid");
        }


        [TestMethod]
        public void ChangeNameValueWithIlegalValue()
        {
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                A.Fake<IMessageBoxService>(),
                                                A.Fake<IResourcePropertyDialogService>(),
                                                A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            metaDataViewModel.Name.DataValue = "!";
            Assert.IsFalse(metaDataViewModel.Name.IsValid, "Expected to be invalid");
        }

        [TestMethod]
        public void VerifyNameIsPushedto_ResourcePropertiesName()
        {
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                A.Fake<IMessageBoxService>(),
                                                A.Fake<IResourcePropertyDialogService>(),
                                                A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            metaDataViewModel.Name.DataValue = "A New Name";

            Assert.AreEqual("A New Name", metaDataViewModel.ResourceProperties.DataValue.Name, "Should be the same");
        }

        [TestMethod]
        public void VerifyNameIsPushedto_ResourcePropertiesTitle()
        {
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                A.Fake<IMessageBoxService>(),
                                                A.Fake<IResourcePropertyDialogService>(),
                                                A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            metaDataViewModel.Title.DataValue = "A New Title";

            Assert.AreEqual("A New Title", metaDataViewModel.ResourceProperties.DataValue.Title, "Should be the same");
        }

        [TestMethod]
        public void VerifyERRNameIsPushedTo_ResourcePropertiesTitle()
        {
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                A.Fake<IMessageBoxService>(),
                                                A.Fake<IResourcePropertyDialogService>(),
                                                A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            metaDataViewModel.Name.DataValue = "An Error! Name )*^^*( ";
            Assert.AreEqual("An Error! Name )*^^*( ", metaDataViewModel.ResourceProperties.DataValue.Name, "Should be the same");
        }
    }
}
