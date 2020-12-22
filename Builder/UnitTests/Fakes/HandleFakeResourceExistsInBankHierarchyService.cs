using System;
using System.Linq;
using FakeItEasy;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.UnitTests.Framework.Faketory.@interface;

namespace Questify.Builder.UnitTests.Fakes
{
    class HandleFakeResourceExistsInBankHierarchyService
    {
        private IFakeServices _fakeServices;
        private Fakes.FakeResourceExistsInBankHierarchyHandlerAttribute _att;

        public HandleFakeResourceExistsInBankHierarchyService(IFakeServices services, FakeResourceExistsInBankHierarchyHandlerAttribute att)
        {
            _fakeServices = services;
            _att = att;

            InitHandlers();
        }

        private void InitHandlers()
        {
            var f = _fakeServices.FakeResourceService;
            A.CallTo(() => f.ResourceExists(A<int>.Ignored, A<string>.Ignored, A<bool>.Ignored)).
                ReturnsLazily(arg =>
                    {
                        var ret = new EntityCollection();
                        var name = arg.GetArgument<string>(1);
                        ret.AddRange(_att.ResourcesContainedInBank.Where(r => r.Equals(name, StringComparison.CurrentCultureIgnoreCase)).Select(r => new ResourceEntity { Name = name }));
                        return (ret.Count > 0);
                    });
        }
    }
}
