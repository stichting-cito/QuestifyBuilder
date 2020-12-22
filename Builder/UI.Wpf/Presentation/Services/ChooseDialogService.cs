using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ChooseDialog.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [ExportService(ServiceType.Both, typeof(IChooseDialogService))]
    public class ChooseDialogService : IChooseDialogService
    {

        const string viewname = "ResourceMoverWizard";

        private readonly IWPF2WinVisualizerService _uiVisualizer;



        [ImportingConstructor]
        public ChooseDialogService(IWPF2WinVisualizerService uiVisualizer)
        {
            _uiVisualizer = uiVisualizer;
        }



        public object ShowSelection(string title, string description, IEnumerable<object> choosables)
        {
            var model = new ChooseDialogViewModel(title, description);
            model.ChoosableObjects.DataValue = choosables.ToList();

            _uiVisualizer.ShowDialog("ChooseDialog", model, true);
            return model.SelectedObject.DataValue;
        }
    }
}
