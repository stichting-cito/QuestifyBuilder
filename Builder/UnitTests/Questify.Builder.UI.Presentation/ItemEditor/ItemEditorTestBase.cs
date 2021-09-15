
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
    /// <summary>
    /// Defines a base for Tests with the ItemEditor
    /// </summary>
    /// <remarks>
    /// 
    /// Makes a MessageBoxService available.
    /// Makes ObjectFactory available.
    /// 
    /// Use this class in conjunction with the FakeObjectFactoryBehavior attribute
    /// This will set the behavior what kind of objects will be returned.
    /// 
    /// Usage:
    /// 
    /// [TestClass]
    /// public MyTestClass : ItemEditorTestBase
    /// 
    /// [TestMethod, FakeObjectFactoryBehavior(FakeItemEditorObjectStrategy.DefaultObjects)]
    /// public void MyTestMethod
    /// </remarks>
    [TestClass]
    public class ItemEditorTestBase: MVVMTestBase
    {
        #region Fields

        IItemEditorObjectFactory fake_Factory;
        HandlerFakeItemEditorLoader _currentHandler; //Deals with IItemEditorObjectFactory 

        IFakeServices _fakeServices;
        FakeServiceHandlerAttribute _fakeServiceHandlerAttribute;
        FakeResourceExistsInBankHierarchyHandlerAttribute _fakeResourceExistsInBankHierarchyHandler;
        HandleFakeService _fakeServicesHandler;
        HandleFakeResourceExistsInBankHierarchyService _fakeResourceExistsServiceHandler;

        //IMessageBoxService _fakeMsgBox;

        #endregion

        #region Constructor & Initializer
        
        public ItemEditorTestBase()
        {            
            //Defines what attributes are handled.
            AddAttributteInitializer<FakeObjectFactoryBehaviorAttribute>(DealWith_FakeObjectFactoryBehaviorAttribute);
            AddAttributteInitializer<FakeServiceHandlerAttribute>(DealWith_SimpleFakeServiceBehavior);
            AddAttributteInitializer<FakeResourceExistsInBankHierarchyHandlerAttribute>(DealWith_FakeResourceExistsInBankHierarchyHandler);
        }

        private void DealWith_FakeObjectFactoryBehaviorAttribute(Attribute a)
        {
            var att  = a as FakeObjectFactoryBehaviorAttribute;

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

        #endregion

        #region TestInit / Cleanup
        
        [TestInitialize]
        public void Initialize()
        {
            FakeMessageBoxService.MakeNewFake(); //Resets used fake object.
            // and one of these too! 
            LocatorBootstrapper.ApplyComposer(
                new MyComposer(
                    new[] { MyComposer.GetTestTypesForCinch(),
                        MyComposer.GetRepositories(),
                        MyComposer.GetCustomUITypes()
                    }
                    )
                );

			//We need to clear the instance by reflection because there is no other way. This instance has to be cleared because it conflicts with other instances.
			typeof(ViewModelRepository).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);

            //Needed for resources.      
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
            FakeItemEditorObjectFactory.MakeNewFake(); //Resets used fake object.
            FakeMessageBoxService.MakeNewFake(); //Resets used fake object.
            FakeCustomMessageBoxService.MakeNewFake();
            FakeInputBox.MakeNewFake(); //Resets used fake object.

            FakeDal.Deinit();

            if (_fakeServices != null)
                _fakeServices.CleanFakeServices();
            _fakeServices = null;

            fake_Factory = null;
        }

        #endregion    

        #region Properties
                
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

        #endregion
    }
}
