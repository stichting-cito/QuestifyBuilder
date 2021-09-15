using System;
using System.Runtime.CompilerServices;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    abstract class OrderedAttribute : Attribute
    {
        private readonly int _order;

        protected OrderedAttribute([CallerLineNumber]int order = 0)
        {
            _order = order;
        }

        public int Order { get { return _order; } }

        public string PrmCollToAddToId { get; set; }

        public bool PrmCollToAddToIsDynCollection { get; set; }
    }
}
