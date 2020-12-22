using System.Windows;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    static class ApplicationExtensions
    {
        public static T GetResource<T>(string resourceKey, T defaultValue) where T : class
        {
            var app = Application.Current;
            if (app == null)
            {
                return defaultValue;
            }

            var result = (T)app.TryFindResource(resourceKey);
            if (result == null)
            {
                return defaultValue;
            }

            return result;
        }
    }
}
