
using System;
using System.Reflection;
using System.Windows;
using MEFedMVVM.ViewModelLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.UI.Wpf.Presentation;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;
using Questify.Builder.UnitTests.Framework.Faketory;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ResourcePropertyDialog
{
    [TestClass]
    public class ResourcePropertyDialogTestBase : MVVMTestBase
    {

        IResourcePropertyDialogObjectFactory _fakeFactory;
        HandlerFakeResourcePropertyDialogLoader _currentResourcePropertyLoader;
        protected FakeServices _fakeServices;
        HandleResourcePropertyDialogFakeService _fakeServicesHandler;




        public ResourcePropertyDialogTestBase()
        {
            AddAttributteInitializer<FakeResourcePropertyDialogObjectFactoryBehaviorAttribute>(DealWith_FakeDialogObjectFactoryBehaviorAttribute);
            AddAttributteInitializer<FakeResourcePropertyDialogServiceHandlerAttribute>(DealWith_FakeResourcePropertyDialogService);
        }

        private void DealWith_FakeDialogObjectFactoryBehaviorAttribute(Attribute a)
        {
            var att = a as FakeResourcePropertyDialogObjectFactoryBehaviorAttribute;
            _fakeFactory = FakeResourcePropertyDialogObjectFactory.MakeNewFake();
            _currentResourcePropertyLoader = new HandlerFakeResourcePropertyDialogLoader(_fakeFactory, att);
        }

        private void DealWith_FakeResourcePropertyDialogService(Attribute a)
        {
            var att = a as FakeResourcePropertyDialogServiceHandlerAttribute;
            _fakeServices = new FakeServices();
            _fakeServices.SetupFakeServices();
            _fakeServicesHandler = new HandleResourcePropertyDialogFakeService(_fakeServices, att);
        }


        [TestInitialize]
        public void Initialize()
        {

            FakeDal.Init();
            FakeMessageBoxService.MakeNewFake();
            LocatorBootstrapper.ApplyComposer(
    new MyComposer(
        new[] { MyComposer.GetTestTypesForCinch(),
                        MyComposer.GetRepositories(),
                        MyComposer.GetCustomUITypes()
        }
        )
    );

            typeof(ViewModelRepository).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);


            if (Application.Current != null) Application.Current.Resources = new ResourceDictionary();
            Bootstrapper.InitLanguageAndResources();
        }

        [TestCleanup]
        public void Clean()
        {
            FakeDal.Deinit();
            FakeResourcePropertyDialogObjectFactory.MakeNewFake(); FakeMessageBoxService.MakeNewFake(); FakeCustomMessageBoxService.MakeNewFake();
            FakeInputBox.MakeNewFake();
            if (_fakeServices != null)
                _fakeServices.CleanFakeServices();
            _fakeServices = null;

            _fakeFactory = null;
            Application.Current.Resources = new ResourceDictionary();
        }

        protected void CreateAndStoreItemResourceEntity(Guid resourceId, string version)
        {
            FakeDal.Add.Item("SomeItemResourceEntity", (i) =>
            {
                i.ResourceId = resourceId;
                i.Version = version;
            });

        }


        public IResourcePropertyDialogObjectFactory FakeFactory
        {
            get { return _fakeFactory; }
        }

    }
}
