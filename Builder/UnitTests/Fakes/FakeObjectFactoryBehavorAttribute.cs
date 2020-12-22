using System;

namespace Questify.Builder.UnitTests.Fakes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class FakeObjectFactoryBehaviorAttribute : Attribute
    {
        readonly ItemEditorObjectStrategy _Strategy;

        public FakeObjectFactoryBehaviorAttribute(ItemEditorObjectStrategy strategy)
        {
            this._Strategy = strategy;
        }

        public bool IsOldItem { get; set; }

        public ItemEditorBankObjectStrategy BankProperties { get; set; }

        public string ConceptId { get; set; }
        public string ConceptPartId { get; set; }

        public string TreeId { get; set; }
        public string TreePartId { get; set; }

        internal ItemEditorObjectStrategy Strategy
        {
            get { return _Strategy; }
        }
    }
}
