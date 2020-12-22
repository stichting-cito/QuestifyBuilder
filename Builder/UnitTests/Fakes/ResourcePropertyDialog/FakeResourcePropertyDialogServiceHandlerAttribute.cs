using System;

namespace Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class FakeResourcePropertyDialogServiceHandlerAttribute : Attribute
    {
        public FakeResourcePropertyDialogServiceHandlerAttribute()
        {

        }
    }
}
