using System;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class FakeServiceHandlerAttribute : Attribute
    {

        public bool ResourceIsContainedInBank { get; set; }

        public FakeServiceHandlerAttribute() { }
    }
}
