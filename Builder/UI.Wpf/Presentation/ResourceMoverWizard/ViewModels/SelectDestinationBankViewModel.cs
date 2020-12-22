using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.GenericDialogs.GenericWizard.ViewModels;

namespace Questify.Builder.UI.Wpf.Presentation.ResourceMoverWizard.ViewModels
{

    [ExportViewModel(Constants.SelectDestinationBankWorkSpace)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class SelectDestinationBankViewModel : BaseWizardSubPageViewModel
    {

        static readonly PropertyChangedEventArgs BankHierarchyRootAsListArgs = ObservableHelper.CreateArgs<SelectDestinationBankViewModel>(x => x.BankHierarchyRootAsList);


        private readonly IViewAwareStatus _viewAwareStatusService;
        private Model.IResourceMoverWizardParams _resourceMoverWizardParams;



        [ImportingConstructor]
        public SelectDestinationBankViewModel(IViewAwareStatus viewAwareStatusService) : base(viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;


            InitProperties();
        }



        private void InitProperties()
        {
            BankHierarchyRootAsList = new DataWrapper<List<BankHierarchyNode>>(this, BankHierarchyRootAsListArgs);
        }



        public DataWrapper<List<BankHierarchyNode>> BankHierarchyRootAsList { get; private set; }

        public int SelectedBankId
        {
            get
            {
                if (_resourceMoverWizardParams != null)
                    return _resourceMoverWizardParams.TargetBankId;

                return 0;
            }

            set { _resourceMoverWizardParams.TargetBankId = value; NotifyPropertyChanged(BaseWizardPageViewModel.IsValidArgs); }
        }

        public override bool IsValid
        {
            get
            {
                return (SelectedBankId > 0);
            }
            set
            {

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




        void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                _resourceMoverWizardParams = (Model.IResourceMoverWizardParams)this.Tag;

                LoadBankTree(_resourceMoverWizardParams.SourceBankId);
            }
        }

        private void LoadBankTree(int bankId)
        {
            List<BankHierarchyNode> rootAsList = new List<BankHierarchyNode>();

            rootAsList.Add(new BankHierarchy(bankId).Root);

            BankHierarchyRootAsList.DataValue = rootAsList;
        }

    }
}
