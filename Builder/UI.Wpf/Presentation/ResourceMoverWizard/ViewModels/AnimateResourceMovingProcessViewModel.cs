using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.ViewModels
{

    [ExportViewModel(Constants.AnimateResourceMovingProcess)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class AnimateResourceMovingProcessViewModel : BaseWizardSubPageViewModel
    {



        private readonly IViewAwareStatus _viewAwareStatusService;
        private System.Globalization.CultureInfo _UICultureInfoUIThread;



        [ImportingConstructor]
        public AnimateResourceMovingProcessViewModel(IViewAwareStatus viewAwareStatusService) : base(viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;
        }





        public override bool IsValid
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public override bool? CanExecuteFinish
        {
            get
            {
                return false;
            }
            set
            {
                base.CanExecuteFinish = value;
            }
        }

        public override bool? CanExecuteNext
        {
            get
            {
                return false;
            }
            set
            {
                base.CanExecuteNext = value;
            }
        }

        public override bool? CanExecutePrevious
        {
            get
            {
                return false;
            }
            set
            {
                base.CanExecutePrevious = value;
            }
        }





        void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                Model.IResourceMoverWizardParams param = (Model.IResourceMoverWizardParams)this.Tag;

                _UICultureInfoUIThread = System.Threading.Thread.CurrentThread.CurrentUICulture;

                BackgroundWorker bgWorker = new BackgroundWorker();

                bgWorker.DoWork += bgWorker_DoWork;
                bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;

                bgWorker.RunWorkerAsync(param);
            }
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((WizardViewModel)_wizardViewModel).Next.Execute(null);
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Model.IResourceMoverWizardParams param = (Model.IResourceMoverWizardParams)e.Argument;

            System.Threading.Thread.CurrentThread.CurrentUICulture = _UICultureInfoUIThread;

            ResourceAndCustomBankPropertyMover moveValidatorAndMover = new ResourceAndCustomBankPropertyMover(param.SourceBankId, param.TargetBankId);

            param.MovingResult = moveValidatorAndMover.MoveResources(param.ResourcesToMoveIds);
        }

    }
}
