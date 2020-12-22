
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.Factories;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Fakes.ResourcePropertyDialog;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    class MyComposer : IComposer
    {

        private List<ComposablePartCatalog> _catalogs;


        public MyComposer(IEnumerable<ComposablePartCatalog> catalogs)
        {
            _catalogs = new List<ComposablePartCatalog>(catalogs);
        }

        public MyComposer()
        {
        }



        public IEnumerable<ExportProvider> GetCustomExportProviders()
        {
            return null;
        }

        public ComposablePartCatalog InitializeContainer()
        {
            return new AggregateCatalog(_catalogs);
        }

        public static ComposablePartCatalog GetTestTypesForCinch()
        {
            var ret = new TypeCatalog(new[]  {
                typeof(FakeMessageBoxService),
                typeof(FakeCustomMessageBoxService),
                typeof(FakeResourcePropertyDialogService)
            });
            return ret;
        }

        public static ComposablePartCatalog GetRepositories()
        {
            var ret = new TypeCatalog(new[] {
                typeof(FakeItemEditorObjectFactory)
            });
            return ret;
        }

        public static ComposablePartCatalog GetCustomUITypes()
        {
            var ret = new TypeCatalog(new[] {
                typeof(FakeInputBox),
                typeof(FakeSelectDialogFactory),

            });
            return ret;
        }

        public static ComposablePartCatalog GetScoreEditors()
        {
            var ret = new TypeCatalog(new[] {
                typeof(MCScoringVWFactory)
            });
            return ret;
        }

        public static ComposablePartCatalog NoExportTypes()
        {
            var ret = new TypeCatalog(new List<Type>());
            return ret;
        }

        public static ComposablePartCatalog GetItemEditorContext()
        {
            var ret = new TypeCatalog(new[] {
                typeof(CurrentItemEditorContext)
            });
            return ret;
        }

    }
}
