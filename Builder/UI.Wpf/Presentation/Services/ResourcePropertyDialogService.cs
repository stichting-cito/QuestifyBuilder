using System;
using System.ComponentModel.Composition;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(IResourcePropertyDialogService))]
    class ResourcePropertyDialogService : IResourcePropertyDialogService
    {

        const string viewname = "ResourcePropertyDialog";

        private readonly IWPF2WinVisualizerService _uiVisualizer;
        private ResourcePropertyDialogViewModel _viewModel;



        [ImportingConstructor]
        public ResourcePropertyDialogService(IWPF2WinVisualizerService uiVisualizer)
        {
            _uiVisualizer = uiVisualizer;
        }



        public bool Show(Guid propertyEntityId, Type type, int initialTabIndex = 0)
        {
            if (_viewModel == null)
            {
                _viewModel = new ResourcePropertyDialogViewModel();
            }

            _viewModel.PropertyType = type;
            _viewModel.PropertyId.DataValue = propertyEntityId; _viewModel.SelectedTab.DataValue = initialTabIndex;
            var result = _uiVisualizer.ShowDialog(viewname, _viewModel, true);

            if (result.HasValue) return result.Value; else return false;
        }



    }
}
