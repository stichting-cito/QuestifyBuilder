using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.ViewModels
{

    [ExportViewModel(Constants.ViewResourceMovingDetails)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ViewResourceMovingDetailsViewModel : BaseWizardSubPageViewModel
    {

        static readonly PropertyChangedEventArgs MovingValidationResultArgs = ObservableHelper.CreateArgs<ViewResourceMovingDetailsViewModel>(x => x.MovingValidationResult);


        private readonly IViewAwareStatus _viewAwareStatusService;



        [ImportingConstructor]
        public ViewResourceMovingDetailsViewModel(IViewAwareStatus viewAwareStatusService) : base(viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;


            InitProperties();
        }



        private void InitProperties()
        {
            MovingValidationResult = new DataWrapper<ResourceAndCustomBankPropertyMoveResult>(this, MovingValidationResultArgs);
        }



        public DataWrapper<ResourceAndCustomBankPropertyMoveResult> MovingValidationResult { get; private set; }


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

        public override bool? CanExecuteFinish
        {
            get
            {
                return (MovingValidationResult.DataValue != null && MovingValidationResult.DataValue.AllCanBeMoved);
            }
            set
            {
                base.CanExecuteFinish = value;
            }
        }



        void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                Model.IResourceMoverWizardParams param = (Model.IResourceMoverWizardParams)this.Tag;

                LoadMovingDetails(param.SourceBankId, param.TargetBankId, param.ResourcesToMoveIds);
            }
        }

        private void LoadMovingDetails(int sourceBankId, int targetBankId, Guid[] resourcesToMoveIds)
        {
            ResourceAndCustomBankPropertyMover moveValidator = new ResourceAndCustomBankPropertyMover(sourceBankId, targetBankId);

            MovingValidationResult.DataValue = moveValidator.ValidateResourcesMove(resourcesToMoveIds);
        }

    }
}
