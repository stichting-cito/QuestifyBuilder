
using System;

namespace Questify.Builder.UnitTests.Fakes
{

    /// <summary>
    /// Sets a solution (Cito.Tester.ContentModel.Solution). This attribute defined as a generic construct to define what solution you wish to use.
    /// 
    /// used in the:
    /// - EncodingScoringVMTest_WithSpecificData
    /// </summary>
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
