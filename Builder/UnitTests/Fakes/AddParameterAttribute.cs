using System;
using System.Runtime.CompilerServices;

namespace Questify.Builder.UnitTests.Fakes
{

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class AddParameterAttribute : OrderedAttribute
    {

        private readonly Type _typeOfScoreAttribute;

        public AddParameterAttribute(Type typeOfScoreAttribute, [CallerLineNumber]int order = 0)
            : base(order)
        {
            _typeOfScoreAttribute = typeOfScoreAttribute;
        }


        public Type TypeOfScoreAttribute { get { return _typeOfScoreAttribute; } }

        public string FindingOverride { get; set; }

        public string InlineID { get; set; }

        public string ControllerID { get; set; }

        public string Name { get; set; }

        public string SubParameterIds { get; set; }
    }

}
