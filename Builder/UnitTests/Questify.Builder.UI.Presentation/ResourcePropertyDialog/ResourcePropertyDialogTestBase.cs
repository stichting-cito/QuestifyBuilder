
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
    /// <summary>
    /// This is a base class for unit tests related to versioning and the resource dialogs accompanied.
    /// </summary>
	[TestClass]
	public class ResourcePropertyDialogTestBase : MVVMTestBase
	{
		#region Fields

		IResourcePropertyDialogObjectFactory _fakeFactory;
		HandlerFakeResourcePropertyDialogLoader _currentResourcePropertyLoader; //Deals with IItemEditorObjectFactory 

		protected FakeServices _fakeServices;
		HandleResourcePropertyDialogFakeService _fakeServicesHandler;

		#endregion


		#region Constructor & Initializer

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

		#endregion

		[TestInitialize]
		public void Initialize()
		{

			FakeDal.Init();
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
		}

		[TestCleanup]
		public void Clean()
		{
			FakeDal.Deinit();
			FakeResourcePropertyDialogObjectFactory.MakeNewFake(); //Resets used fake object.
			FakeMessageBoxService.MakeNewFake(); //Resets used fake object.
			FakeCustomMessageBoxService.MakeNewFake();
			FakeInputBox.MakeNewFake(); //Resets used fake object.

			if (_fakeServices != null)
				_fakeServices.CleanFakeServices();
			_fakeServices = null;

			_fakeFactory = null;      
            //Clear resources.
		    Application.Current.Resources = new ResourceDictionary();
		}

		/// <summary>
		/// Creates the and store item resource entity.
		/// </summary>
		/// <param name="resourceId">The resource identifier.</param>
		/// <param name="version">The version.</param>
		protected void CreateAndStoreItemResourceEntity(Guid resourceId, string version)
		{
			FakeDal.Add.Item("SomeItemResourceEntity", (i) =>
			{
				i.ResourceId = resourceId;
				i.Version = version;
			});

		}

		#region Properties

		public IResourcePropertyDialogObjectFactory FakeFactory
		{
			get { return _fakeFactory; }
		}

		#endregion
	}
}
