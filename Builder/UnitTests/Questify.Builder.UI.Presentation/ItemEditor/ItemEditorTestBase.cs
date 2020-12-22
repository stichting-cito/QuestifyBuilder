
using System;
using System.Reflection;
using System.Windows;
using MEFedMVVM.ViewModelLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.UI.Wpf.Presentation;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;
using Questify.Builder.UnitTests.Framework.Faketory.@interface;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemEditorTestBase : MVVMTestBase
    {

        IItemEditorObjectFactory fake_Factory;
        HandlerFakeItemEditorLoader _currentHandler;
        IFakeServices _fakeServices;
        FakeServiceHandlerAttribute _fakeServiceHandlerAttribute;
        FakeResourceExistsInBankHierarchyHandlerAttribute _fakeResourceExistsInBankHierarchyHandler;
        HandleFakeService _fakeServicesHandler;
        HandleFakeResourceExistsInBankHierarchyService _fakeResourceExistsServiceHandler;




        public ItemEditorTestBase()
        {
            AddAttributteInitializer<FakeObjectFactoryBehaviorAttribute>(DealWith_FakeObjectFactoryBehaviorAttribute);
            AddAttributteInitializer<FakeServiceHandlerAttribute>(DealWith_SimpleFakeServiceBehavior);
            AddAttributteInitializer<FakeResourceExistsInBankHierarchyHandlerAttribute>(DealWith_FakeResourceExistsInBankHierarchyHandler);
        }

        private void DealWith_FakeObjectFactoryBehaviorAttribute(Attribute a)
        {
            var att = a as FakeObjectFactoryBehaviorAttribute;

            fake_Factory = FakeItemEditorObjectFactory.MakeNewFake();
            _currentHandler = new HandlerFakeItemEditorLoader(fake_Factory, att);
        }

        private void DealWith_SimpleFakeServiceBehavior(Attribute a)
        {
            var att = a as FakeServiceHandlerAttribute;

            _fakeServiceHandlerAttribute = att;
        }

        private void DealWith_FakeResourceExistsInBankHierarchyHandler(Attribute a)
        {
            var att = a as FakeResourceExistsInBankHierarchyHandlerAttribute;
            _fakeResourceExistsInBankHierarchyHandler = att;
        }



        [TestInitialize]
        public void Initialize()
        {
            FakeMessageBoxService.MakeNewFake(); LocatorBootstrapper.ApplyComposer(
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


            FakeDal.Init();
            _fakeServices = FakeDal.FakeServices;

            if (_fakeServiceHandlerAttribute != null)
            {
                _fakeServicesHandler = new HandleFakeService(_fakeServices, _fakeServiceHandlerAttribute);
            }

            if (_fakeResourceExistsInBankHierarchyHandler != null)
                _fakeResourceExistsServiceHandler = new HandleFakeResourceExistsInBankHierarchyService(_fakeServices, _fakeResourceExistsInBankHierarchyHandler);
        }

        [TestCleanup]
        public void Clean()
        {
            FakeItemEditorObjectFactory.MakeNewFake(); FakeMessageBoxService.MakeNewFake(); FakeCustomMessageBoxService.MakeNewFake();
            FakeInputBox.MakeNewFake();
            FakeDal.Deinit();

            if (_fakeServices != null)
                _fakeServices.CleanFakeServices();
            _fakeServices = null;

            fake_Factory = null;
        }



        public IItemEditorObjectFactory Fake_Factory
        {
            get { return fake_Factory; }
        }

        public IResourceService FakeResourceService
        {
            get
            {
                if (_fakeServices != null) return _fakeServices.FakeResourceService;
                return null;
            }
        }

        public IBankService FakeBankService
        {
            get
            {
                if (_fakeServices != null) return _fakeServices.FakeBankService;
                return null;
            }
        }

    }
}
