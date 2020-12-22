using System;
using System.ComponentModel.Composition;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    internal abstract class EditorFactoryForTypeAttribute : ExportAttribute
    {

        private Type _valueHoldingType;

        public EditorFactoryForTypeAttribute(Type exportType, Type valueHoldingType)
    : base(exportType)
        {
            _valueHoldingType = valueHoldingType;
        }

        public Type ValueHoldingType { get { return _valueHoldingType; } }
    }

}
