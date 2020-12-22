using FakeItEasy;
using Questify.Builder.UnitTests.Framework.Faketory.@interface;

namespace Questify.Builder.UnitTests.Fakes
{
    class HandleFakeService
    {
        private IFakeServices _fakeServices;
        private Fakes.FakeServiceHandlerAttribute _att;

        public HandleFakeService(IFakeServices fakeServices, FakeServiceHandlerAttribute att)
        {
            _fakeServices = fakeServices;
            _att = att;

            if (_att.ResourceIsContainedInBank) AllResourcesAreFound(); else NoResourcesAreFound();
        }


        private void AllResourcesAreFound()
        {
            var f = _fakeServices.FakeResourceService;
            A.CallTo(() => f.ResourceExists(A<int>.Ignored, A<string>.Ignored, A<bool>.Ignored)).
                 ReturnsLazily(arg => true);
        }

        private void NoResourcesAreFound()
        {
            var f = _fakeServices.FakeResourceService;
            A.CallTo(() => f.ResourceExists(A<int>.Ignored, A<string>.Ignored, A<bool>.Ignored)).
                ReturnsLazily(arg => false);
        }


    }
}
