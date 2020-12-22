using System.Collections.Generic;
using System.ComponentModel.Composition;
using Enums;
using FakeItEasy;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UnitTests.Fakes
{
    [PartCreationPolicy(CreationPolicy.Shared), ExportService(ServiceType.Both, typeof(ISelectDialogFactory))]
    public class FakeSelectDialogFactory : ISelectDialogFactory
    {

        static ISelectDialogFactory _fake;

        public static ISelectDialogFactory MakeNewFake()
        {
            _fake = A.Fake<ISelectDialogFactory>();
            return _fake;
        }



        static FakeSelectDialogFactory()
        {
            if (_fake == null)
                _fake = A.Fake<ISelectDialogFactory>();
        }

        public FakeSelectDialogFactory()
        {
        }



        public ISelectIltDialog GetSelectItemLayoutTemplate(int bankId, List<ItemTypeEnum> itemTypes, bool exclude, string currentIltName)
        {
            return _fake.GetSelectItemLayoutTemplate(bankId, itemTypes, exclude, currentIltName);
        }

    }
}
