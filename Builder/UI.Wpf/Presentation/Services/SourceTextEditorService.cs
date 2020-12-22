using System;
using System.ComponentModel.Composition;
using Cinch;
using Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(ISourceTextEditorService))]
    class SourceTextEditorService : ISourceTextEditorService, IDisposable
    {
        const string viewname = "SourceTextEditor";

        private readonly IWPF2WinVisualizerService _uiVisualizer;
        private SourceTextEditorViewModel _viewModel;
        private bool _disposed;



        [ImportingConstructor]
        public SourceTextEditorService(IWPF2WinVisualizerService uiVisualizer)
        {
            _uiVisualizer = uiVisualizer;
        }



        public void Show(Guid sourceTextEntityId)
        {
            if (_viewModel == null)
            {
                _viewModel = new SourceTextEditorViewModel();

                var b = _uiVisualizer.Show(viewname, _viewModel, true, HandleUICompleted);
            }
            _viewModel.SourceTextId.DataValue = sourceTextEntityId;
        }

        public void ShowDialog(Guid sourceTextEntityId)
        {
            SourceTextEditorViewModel viewModel = new SourceTextEditorViewModel();

            viewModel.SourceTextId.DataValue = sourceTextEntityId;

            var b = _uiVisualizer.ShowDialog(viewname, viewModel, false);
        }


        public void Make_NewSourceTextTemplate(int bankId, bool isSourceTextTemplate)
        {
            if (_viewModel == null)
            {
                _viewModel = new SourceTextEditorViewModel();

                var b = _uiVisualizer.Show(viewname, _viewModel, true, HandleUICompleted);
            }

            _viewModel.CreateNewItem(bankId, isSourceTextTemplate);
        }


        private void HandleUICompleted(object sender, UICompletedEventArgs e)
        {
            _viewModel = null;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _viewModel?.Dispose();
            }

            _disposed = true;
        }

        ~SourceTextEditorService()
        {
            Dispose(false);
        }
    }
}
