using System;
using System.ComponentModel.Composition;
using FakeItEasy;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog
{
    [PartCreationPolicy(CreationPolicy.Shared), ExportService(ServiceType.Both, typeof(IResourcePropertyDialogService))]
    public class FakeResourcePropertyDialogService : IResourcePropertyDialogService
    {

        static IResourcePropertyDialogService _fake;

        public static IResourcePropertyDialogService MakeNewFake()
        {
            return _fake;
        }



        static FakeResourcePropertyDialogService()
        {
            if (_fake == null)
                _fake = A.Fake<IResourcePropertyDialogService>();
        }

        public FakeResourcePropertyDialogService()
        {
        }



        public bool Show(Guid resourceEntityId, Type type, int initialTabIndex = 0)
        {
            return _fake.Show(resourceEntityId, type);
        }

    }
}
