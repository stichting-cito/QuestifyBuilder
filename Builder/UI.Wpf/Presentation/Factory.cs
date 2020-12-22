using System;
using System.Diagnostics;
using MEFedMVVM.ViewModelLocator;

namespace Questify.Builder.UI.Wpf.Presentation
{
    class Factory
    {
        internal static Lazy<T> GetExportLazy<T>()
        {
            Debug.Assert(ViewModelRepository.Instance != null);
            Debug.Assert(ViewModelRepository.Instance.Resolver != null);
            Debug.Assert(ViewModelRepository.Instance.Resolver.Container != null);

            var lazy = ViewModelRepository.Instance.Resolver.Container.GetExport<T>();
            Debug.Assert(lazy != null);
            return lazy;
        }

        internal static T GetExport<T>()
        {
            var lazy = GetExportLazy<T>();
            return lazy.Value;
        }
    }
}
