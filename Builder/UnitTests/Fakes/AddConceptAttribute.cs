using System;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class AddConceptAttribute : Attribute
    {
        public AddConceptAttribute(string guidConceptId)
        {
            ConceptId = guidConceptId;
        }

        public string ConceptId { get; private set; }
        public string ConceptPartId { get; set; }

    }
}
