using System;
using System.Runtime.CompilerServices;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class AddAspectScoringParameterAttribute : OrderedAttribute
    {
        public AddAspectScoringParameterAttribute([CallerLineNumber]int order = 0)
            : base(order)
        {
        }

        public bool AutoScoringOffPrm { get; set; }

        public string FindingOverride { get; set; }

        public string ControllerID { get; set; }

        public string Name { get; set; }
    }
}
