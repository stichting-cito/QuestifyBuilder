using System;
using System.ComponentModel.Composition;
using Cito.Tester.Common;
using FakeItEasy;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.UnitTests.Fakes
{

    [PartCreationPolicy(CreationPolicy.Shared), ExportService(ServiceType.Both, typeof(ILegacyInputBox))]
    public class FakeInputBox : ILegacyInputBox
    {


        static ILegacyInputBox _fake;

        public static ILegacyInputBox MakeNewFake()
        {
            _fake = A.Fake<ILegacyInputBox>();
            return _fake;
        }



        public FakeInputBox()
        {
        }


        public InputBoxResult Show(string prompt, bool masked, string title, string def, Func<string, string> validator, int XPos, int YPos)
        {
            return _fake.Show(prompt, masked, title, def, validator, XPos, YPos);
        }

        public InputBoxResult Show(string prompt, bool masked, string title, string def, Func<string, string> validator)
        {
            return _fake.Show(prompt, masked, title, def, validator);
        }

        public InputBoxResult Show(string prompt, bool masked, string title, string def)
        {
            return _fake.Show(prompt, masked, title, def);
        }

        public InputBoxResult Show(string prompt, bool masked, string title)
        {
            return _fake.Show(prompt, masked, title);
        }

        public InputBoxResult Show(string prompt, bool masked)
        {
            return _fake.Show(prompt, masked);
        }
    }
}
