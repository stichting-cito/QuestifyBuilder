using System;
using System.ComponentModel.Composition;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard;
using Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.Model;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(IResourceMoverWizardService))]
    class ResourceMoverWizardService : IResourceMoverWizardService
    {

        const string viewname = "ResourceMoverWizard";

        private readonly IWPF2WinVisualizerService _uiVisualizer;



        [ImportingConstructor]
        public ResourceMoverWizardService(IWPF2WinVisualizerService uiVisualizer)
        {
            _uiVisualizer = uiVisualizer;
        }



        public bool? ShowDialog(int sourceBankId, Guid[] resourcesToMoveIds)
        {
            WizardViewModel viewModel = new WizardViewModel();

            ResourceMoverParams par = new ResourceMoverParams();

            par.SourceBankId = sourceBankId;
            par.ResourcesToMoveIds = resourcesToMoveIds;

            viewModel.Tag = par;

            WizardStartPageWorkspaceData startWorkspace = new WizardStartPageWorkspaceData(System.Windows.Application.Current.FindResource("ResourceMoverWizard.Header").ToString(), System.Windows.Application.Current.FindResource("ResourceMoverWizard.Introduction").ToString());

            WizardSubPageWorkspaceData selectDestinationBankSubPageWorkspace = new WizardSubPageWorkspaceData(Constants.SelectDestinationBankWorkSpace);
            WizardDefaultPageWorkspaceData selectDestinationBankPageWorkspace = new WizardDefaultPageWorkspaceData(System.Windows.Application.Current.FindResource("ResourceMoverWizard.SelectDestinationBank.Introduction").ToString(), selectDestinationBankSubPageWorkspace);

            WizardSubPageWorkspaceData viewMoveDetailsSubPageWorkspace = new WizardSubPageWorkspaceData(Constants.ViewResourceMovingDetails);
            WizardDefaultPageWorkspaceData viewMoveDetailsPageWorkspace = new WizardDefaultPageWorkspaceData(System.Windows.Application.Current.FindResource("ResourceMoverWizard.ViewResourceMovingDetails.Introduction").ToString(), viewMoveDetailsSubPageWorkspace);

            WizardSubPageWorkspaceData animateMovingProcessSubPageWorkspace = new WizardSubPageWorkspaceData(Constants.AnimateResourceMovingProcess);
            WizardDefaultPageWorkspaceData animateMovingProcessPageWorkspace = new WizardDefaultPageWorkspaceData(System.Windows.Application.Current.FindResource("ResourceMoverWizard.AnimateResourceMovingProcess.Introduction").ToString(), animateMovingProcessSubPageWorkspace);

            WizardSubPageWorkspaceData viewMovingResultsSubPageWorkspace = new WizardSubPageWorkspaceData(Constants.ViewResourceMovingResults);
            WizardDefaultPageWorkspaceData viewMovingResultsPageWorkspace = new WizardDefaultPageWorkspaceData(System.Windows.Application.Current.FindResource("ResourceMoverWizard.ViewResourceMovingResults.Introduction").ToString(), viewMovingResultsSubPageWorkspace);

            viewModel.Title = System.Windows.Application.Current.FindResource("ResourceMoverWizard.Title").ToString();
            viewModel.SetPages(new WizardPageWorkspaceData[] { startWorkspace, selectDestinationBankPageWorkspace, viewMoveDetailsPageWorkspace, animateMovingProcessPageWorkspace, viewMovingResultsPageWorkspace });

            return _uiVisualizer.ShowDialog(GenericDialogs.GenericWizard.Constants.WizardWorkSpace, viewModel, true);
        }

    }
}
