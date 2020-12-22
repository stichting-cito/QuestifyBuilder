using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Cito.Tester.Common;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;
using Versioning;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(IHistoryService))]
    class HistoryService : IHistoryService
    {

        private const string ViewName = "VersionDifferences";

        private readonly IWPF2WinVisualizerService _uiVisualizer;



        [ImportingConstructor]
        public HistoryService(IWPF2WinVisualizerService uiVisualizer)
        {
            _uiVisualizer = uiVisualizer;
        }


        public List<object> GetOpenWindows()
        {
            return _uiVisualizer.GetOpenWindows();
        }


        public void ShowDifferencesWindow(ResourceHistoryEntity resourceHistoryEntity1, ResourceHistoryEntity resourceHistoryEntity2, Type type, ResourceManagerBase resourceManager)
        {
            var viewModel = new VersionDifferencesViewModel(resourceHistoryEntity1, resourceHistoryEntity2, type, resourceManager);

            _uiVisualizer.ShowDialog(ViewName, viewModel, true);
        }

        public void ShowDifferencesWindow(MetaDataCompareResult metaDataCompareResult, string oldVersion, string newVersion)
        {
            var viewModel = new VersionDifferencesViewModel(metaDataCompareResult, oldVersion, newVersion);

            _uiVisualizer.ShowDialog(ViewName, viewModel, true);
        }



    }
}
