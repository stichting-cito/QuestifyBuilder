using System;
using System.Runtime.CompilerServices;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class AddXhtmlParameterAttribute : OrderedAttribute
    {
        private readonly string _xhtmlParameter;

        public AddXhtmlParameterAttribute(string xhtmlParameter, [CallerLineNumber]int order = 0)
            : base(order)
        {
            _xhtmlParameter = xhtmlParameter;
        }
        public string XhtmlParameter { get { return _xhtmlParameter; } }
    }
}
