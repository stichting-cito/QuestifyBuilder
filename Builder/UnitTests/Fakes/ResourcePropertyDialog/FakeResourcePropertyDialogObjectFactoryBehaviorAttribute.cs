using System;

namespace Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class FakeResourcePropertyDialogObjectFactoryBehaviorAttribute : Attribute
    {
        readonly ResourcePropertyDialogObjectStrategy _strategy;

        public FakeResourcePropertyDialogObjectFactoryBehaviorAttribute(ResourcePropertyDialogObjectStrategy strategy)
        {
            _strategy = strategy;
        }

        internal ResourcePropertyDialogObjectStrategy Strategy
        {
            get { return _strategy; }
        }
    }
}
