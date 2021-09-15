
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
    /// <summary>
    /// MetaDataViewModel Test
    /// </summary>
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

            //Init data for view
            _fakeMetadataViewConsumer = A.Fake<IMetadataViewConsumer>();
            fakeView.WorkSpaceContextualData.DataValue = _fakeMetadataViewConsumer;
            Debug.Assert(fakeView.WorkSpaceContextualData != null, "You should had correctly initialized the view");

            _viewAwareStatus.View = fakeView;
        }

       /// <summary>
       /// Testing the validation of the Name text box with legal value.
       /// </summary>
        [TestMethod]
        public void ChangeNameValueWithLegalValue_AllIsOk()
        {
            //Arrange
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                            A.Fake<IMessageBoxService>(),
                                                            A.Fake<IResourcePropertyDialogService>(),
                                                            A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //act
            metaDataViewModel.Name.DataValue = "SomeValue";
            //assert
            Assert.IsTrue(metaDataViewModel.Name.IsValid, "Expected to be valid");
        }

        /// <summary>
        /// Testing the validation when the Name text is null. Is valid,.. just as default value.
        /// </summary>
        [TestMethod]
        public void ChangeNameValueWithNullValue()
        {
            //Arrange
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                            A.Fake<IMessageBoxService>(),
                                                            A.Fake<IResourcePropertyDialogService>(),
                                                            A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //act
            metaDataViewModel.Name.DataValue = null;
            //assert
            Assert.IsTrue(metaDataViewModel.Name.IsValid, "Expected to be valid, default value.");
        }

        /// <summary>
        /// Testing the validation when the Name text is null. Is Now valid.
        /// </summary>
        [TestMethod]
        public void ChangeNameValueWithStringEmptyValue_ShouldNotBeValid()
        {
            //Arrange
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                            A.Fake<IMessageBoxService>(),
                                                            A.Fake<IResourcePropertyDialogService>(),
                                                            A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //act
            metaDataViewModel.Name.DataValue = string.Empty;
            //assert
            Assert.IsFalse(metaDataViewModel.Name.IsValid, "Expected to be invalid");
        }


        /// <summary>
        /// Testing the validation of the Name text box with legal value.
        /// </summary>
        [TestMethod]
        public void ChangeNameValueWithIlegalValue()
        {
            //Arrange
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                            A.Fake<IMessageBoxService>(),
                                                            A.Fake<IResourcePropertyDialogService>(),
                                                            A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //act
            metaDataViewModel.Name.DataValue = "!";
            //assert
            Assert.IsFalse(metaDataViewModel.Name.IsValid, "Expected to be invalid");
        }

        /// <summary>
        /// Testing the validation of the Name text box with legal value.
        /// </summary>
        [TestMethod]
        public void VerifyNameIsPushedto_ResourcePropertiesName()
        {
            //Arrange
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                            A.Fake<IMessageBoxService>(),
                                                            A.Fake<IResourcePropertyDialogService>(),
                                                            A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //act
            metaDataViewModel.Name.DataValue = "A New Name";
            //assert

            Assert.AreEqual("A New Name", metaDataViewModel.ResourceProperties.DataValue.Name,"Should be the same");
        }

        /// <summary>
        /// Testing the validation of the Name text box with legal value.
        /// </summary>
        [TestMethod]
        public void VerifyNameIsPushedto_ResourcePropertiesTitle()
        {
            //Arrange
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                            A.Fake<IMessageBoxService>(),
                                                            A.Fake<IResourcePropertyDialogService>(),
                                                            A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //act
            metaDataViewModel.Title.DataValue = "A New Title";
            //assert

            Assert.AreEqual("A New Title", metaDataViewModel.ResourceProperties.DataValue.Title, "Should be the same");
        }

        [TestMethod]
        public void VerifyERRNameIsPushedTo_ResourcePropertiesTitle()
        {
            //Arrange
            var metaDataViewModel = new MetaDataViewModel(ViewAwareStatus,
                                                            A.Fake<IMessageBoxService>(),
                                                            A.Fake<IResourcePropertyDialogService>(),
                                                            A.Fake<ISourceTextEditorObjectFactory>());
            ViewAwareStatus.SimulateViewIsLoadedEvent();
            //act
            metaDataViewModel.Name.DataValue = "An Error! Name )*^^*( ";
            //assert
            Assert.AreEqual("An Error! Name )*^^*( ", metaDataViewModel.ResourceProperties.DataValue.Name, "Should be the same");
        }
    }
}
