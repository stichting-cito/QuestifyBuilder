
using System;

namespace Questify.Builder.UnitTests.Fakes
{

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class SetSolutionAttribute : Attribute
    {

        private readonly string _solutionName;

        public SetSolutionAttribute(string solutionName)
        {
            _solutionName = solutionName;
        }

        public string SolutionName { get { return _solutionName; } }

    }
}
