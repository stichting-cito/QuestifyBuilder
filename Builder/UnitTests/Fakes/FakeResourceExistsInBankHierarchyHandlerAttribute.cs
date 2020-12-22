using System;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class FakeResourceExistsInBankHierarchyHandlerAttribute : Attribute
    {
        public string[] ResourcesContainedInBank { get; set; }

        public FakeResourceExistsInBankHierarchyHandlerAttribute()
        { }
    }
}
