using System;
using System.Xml.Linq;
using Cito.Tester.ContentModel;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class SetAssessmentItemAttribute : Attribute
    {

        private readonly string _serializedAssessmentItem;

        public SetAssessmentItemAttribute(string serializedAssessmentItem)
        {
            _serializedAssessmentItem = serializedAssessmentItem;
        }

        public AssessmentItem GetAssessmentItem()
        {
            return XElement.Parse(_serializedAssessmentItem).To<AssessmentItem>();
        }

    }
}
