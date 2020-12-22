using Questify.Builder.UnitTests.Framework.Faketory;

namespace Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog
{
    class HandleResourcePropertyDialogFakeService
    {
        private FakeServices _fakeServices;
        private FakeResourcePropertyDialogServiceHandlerAttribute _att;

        public HandleResourcePropertyDialogFakeService(FakeServices fakeServices, FakeResourcePropertyDialogServiceHandlerAttribute att)
        {
            _fakeServices = fakeServices;
            _att = att;
        }
    }
}
