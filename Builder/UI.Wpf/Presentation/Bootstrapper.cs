using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using Cinch;
using Questify.Builder.Logic;
using Questify.Builder.Logic.Service.Domain.Services;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;


namespace Questify.Builder.UI.Wpf.Presentation
{
    public static class Bootstrapper
    {
        static Application _app = null;

        public static void Initialize()
        {
            try
            {
                var assmblyColl = new[]
                {
                    typeof (Bootstrapper).Assembly, typeof (ServiceBase).Assembly, typeof (Cinch.CinchBootStrapper).Assembly
                };
                using (var skipNativeDll = new SkipNativeDll())
                { CinchBootStrapper.Initialise(assmblyColl); };
            }
            catch (Exception e)
            {
                CheckForLoaderExceptions(e);
                throw;
            }
        }


        public static void InitializeWpf2WinVisualizerService()
        {
            try
            {
                var assmblyColl = new[]
                {
                    typeof (Bootstrapper).Assembly, typeof (ServiceBase).Assembly, typeof (Cinch.CinchBootStrapper).Assembly
                };
                var export = Factory.GetExportLazy<IWPF2WinVisualizerService>();
                if (export == null) return;
                var uiVisualizerService = export.Value;
                foreach (var ass in assmblyColl)
                {
                    foreach (var type in ass.GetTypes())
                    {
                        foreach (
                            var attrib in
                                type.GetCustomAttributes(typeof(PopupNameToViewLookupKeyMetadataAttribute), true))
                        {
                            var viewMetadataAtt = (PopupNameToViewLookupKeyMetadataAttribute)attrib;
                            uiVisualizerService.Register(viewMetadataAtt.PopupName, viewMetadataAtt.ViewLookupKey);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                CheckForLoaderExceptions(e);
                throw e;
            }
        }

        public static void InitLanguageAndResources()
        {
            if (Application.Current == null)
            {
                _app = new Application(); _app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }
            else
            {
                _app = Application.Current;
            }

            if (CultureInfo.DefaultThreadCurrentUICulture == null)
            {
                CultureInfo.DefaultThreadCurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            }
            string lang = CultureInfo.DefaultThreadCurrentUICulture.TwoLetterISOLanguageName;
            switch (lang)
            {
                case "nl":
                    _app.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri(@"/Questify.Builder.UI.Wpf;component/Presentation/Localization/nl.xaml", UriKind.Relative)
                    });
                    break;
                default:
                    _app.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri(@"/Questify.Builder.UI.Wpf;component/Presentation/Localization/en.xaml", UriKind.Relative)
                    });
                    break;
            }

            _app.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source =
        new Uri(@"/Questify.Builder.UI.Wpf;component/Presentation/Localization/languageIndependent.xaml",
            UriKind.Relative)
            });
        }


        private static void CheckForLoaderExceptions(Exception e)
        {
            if (e == null) return;
            if (e is ReflectionTypeLoadException)
            {
                handle_ReflectionTypeLoadException(e as ReflectionTypeLoadException);
                return;
            }
            if (e.InnerException != null)
            {
                CheckForLoaderExceptions(e.InnerException);
            }
        }

        private static void handle_ReflectionTypeLoadException(ReflectionTypeLoadException reflectionTypeLoadException)
        {
            foreach (Exception ex in reflectionTypeLoadException.LoaderExceptions)
            {
                MessageBox.Show(ex.Message, "LoadException.LoaderExceptions");
            }
        }

    }
}
