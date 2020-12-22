using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace Questify.Builder.UI.Wpf.Presentation.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(Array))]
    internal class EnumExtension : MarkupExtension
    {
        public EnumExtension() { }

        public EnumExtension(Type enumType) { EnumType = enumType; }

        [ConstructorArgument("enumType")]
        public Type EnumType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (EnumType == null)
            {
                throw new System.ArgumentException("The enumeration's type is not set.");
            }

            var returnValue = new List<Tuple<object, string>>();
            var values = Enum.GetValues(EnumType);
            var app = System.Windows.Application.Current;

            foreach (var v in values)
            {
                var displayValue = (string)app.TryFindResource("ItemEditor.ScoreEditor.ScoreMethod." + v.ToString());
                returnValue.Add(new Tuple<object, string>(v, displayValue ?? v.ToString()));
            }
            return returnValue.ToArray();
        }
    }

}
