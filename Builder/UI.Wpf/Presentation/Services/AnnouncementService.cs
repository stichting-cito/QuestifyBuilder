using System.ComponentModel.Composition;
using Questify.Builder.UI.Wpf.Presentation.AnnouncementDialog.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(IAnnouncementService))]
    class AnnouncementService : IAnnouncementService
    {
        const string ViewName = "AnnouncementDialog";

        private readonly IWPF2WinVisualizerService _uiVisualizer;
        private AnnouncementDialogViewModel _viewModel;
        private bool _disposed;

        [ImportingConstructor]
        public AnnouncementService(IWPF2WinVisualizerService uiVisualizer)
        {
            _uiVisualizer = uiVisualizer;
        }

        public void Show()
        {
            if (AnnouncementDialogViewModel.ReferenceEquals(_viewModel, null))
            {
                _viewModel = new AnnouncementDialogViewModel();
            }

            _viewModel.SelectedTab.DataValue = 0;
            _uiVisualizer.Show(AnnouncementService.ViewName, _viewModel, false, null);
        }
    }
}
