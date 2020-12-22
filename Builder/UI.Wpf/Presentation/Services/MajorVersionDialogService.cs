using System.ComponentModel.Composition;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.VersionDialog.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(IMajorVersionDialogService))]
    class MajorVersionDialogService : IMajorVersionDialogService
    {
        const string VIEWNAME = "MajorVersionDialog";
        private readonly IWPF2WinVisualizerService _uiVisualizer;


        [ImportingConstructor]
        public MajorVersionDialogService(IWPF2WinVisualizerService uiVisualizer)
        {
            _uiVisualizer = uiVisualizer;
        }



        public bool Show(ResourceEntity versionableEntity)
        {
            if (!(versionableEntity is IVersionable))
                return false;

            var viewModel = new MajorVersionDialogViewModel((IVersionable)versionableEntity);
            var result = _uiVisualizer.ShowDialog(VIEWNAME, viewModel, true);

            return result.HasValue && result.Value;
        }

    }
}
