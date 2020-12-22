using System;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class AddTreeAttribute : Attribute
    {
        public AddTreeAttribute(string guidTreeId)
        {
            TreeId = guidTreeId;
        }

        public string TreeId { get; private set; }
        public string TreePartId { get; set; }

    }
}
