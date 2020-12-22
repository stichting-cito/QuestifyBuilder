using System;
using System.Windows;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Presentation.ValueConverters
{
    public class EntityTypeToFriendlyNameConverter : System.Windows.Markup.MarkupExtension, IValueConverter
    {
        private static System.Collections.Generic.Dictionary<string, string> _xlateEntityTypes = new System.Collections.Generic.Dictionary<string, string>();
        private static bool _xlateTableInitialized;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var s = value as string;

            InitializeEntityTypeNameTable();

            if (_xlateEntityTypes.ContainsKey(s))
            {
                return _xlateEntityTypes[s];
            }

            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        private static void InitializeEntityTypeNameTable()
        {
            if (_xlateTableInitialized)
            {
                return;
            }

            _xlateTableInitialized = true;

            _xlateEntityTypes = new System.Collections.Generic.Dictionary<string, string>() {
                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.Item").ToString()},

                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.ItemLayoutTemplateResourceEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.ItemLayoutTemplate").ToString()},
                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.ControlTemplateResourceEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.ControlTemplate").ToString()},

                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.AssessmentTestResourceEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.Test").ToString()},
                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.TestPackageResourceEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.TestPackage").ToString()},

                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.Media").ToString()},

                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.ListCustomBankPropertyEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.ListCustomBankProperty").ToString()},
                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.FreeValueCustomBankPropertyEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.FreeValueCustomBankProperty").ToString()},
                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.ConceptStructureCustomBankPropertyEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.ConceptStructure").ToString()},

                {typeof(Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity).FullName, Application.Current.FindResource("EntityTypeToFriendlyNameConverter.Aspect").ToString()}
            };
        }
    }
}
