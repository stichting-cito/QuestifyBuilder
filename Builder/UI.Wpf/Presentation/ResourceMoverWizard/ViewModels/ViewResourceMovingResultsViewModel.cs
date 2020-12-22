using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.ViewModels
{

    [ExportViewModel(Constants.ViewResourceMovingResults)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ViewResourceMovingResultsViewModel : BaseWizardSubPageViewModel
    {

        static readonly PropertyChangedEventArgs MovingResultArgs = ObservableHelper.CreateArgs<ViewResourceMovingResultsViewModel>(x => x.MovingResult);


        private readonly IViewAwareStatus _viewAwareStatusService;



        [ImportingConstructor]
        public ViewResourceMovingResultsViewModel(IViewAwareStatus viewAwareStatusService) : base(viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;

            InitProperties();
        }



        private void InitProperties()
        {
            MovingResult = new DataWrapper<ResourceAndCustomBankPropertyMoveResult>(this, MovingResultArgs);
        }



        public DataWrapper<ResourceAndCustomBankPropertyMoveResult> MovingResult { get; private set; }


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

        public override bool? CanExecuteCancel
        {
            get
            {
                return true;
            }
            set
            {
                base.CanExecuteCancel = value;
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

        public override CloseMethod CloseMethod
        {
            get
            {
                return CloseMethod.CLOSE;
            }
            set
            {
            }
        }

        public override bool OnClose()
        {
            return true;
        }





        void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                Model.IResourceMoverWizardParams param = (Model.IResourceMoverWizardParams)this.Tag;
                MovingResult.DataValue = param.MovingResult;
            }
        }


    }
}
