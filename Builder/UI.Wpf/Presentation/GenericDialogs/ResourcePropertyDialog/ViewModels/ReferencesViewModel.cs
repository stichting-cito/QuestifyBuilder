using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels
{
    [ExportViewModel("ResourcePropertyDialog.ReferencesViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ReferencesViewModel : ViewModelBase
    {

        static readonly PropertyChangedEventArgs ReferencesArgs = ObservableHelper.CreateArgs<ReferencesViewModel>(x => x.References);



        private readonly IViewAwareStatus _viewAwareStatusService;
        public IResourcePropertyDialogViewModel ResourcePropertyDialogVM { get; private set; }



        [ImportingConstructor]
        public ReferencesViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;

            var app = Application.Current;

            InitDataWrappers();
        }



        public DataWrapper<IEnumerable<ReferencedEntityViewModel>> References { get; private set; }



        private void InitDataWrappers()
        {
            References = new DataWrapper<IEnumerable<ReferencedEntityViewModel>>(this, ReferencesArgs);
        }

        private EntityCollection GetReferences(IPropertyEntity datasource)
        {
            return ResourcePropertyDialogVM.ResourcePropertyDialogObjectFactory.GetReferences(datasource);
        }

        private void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;
                ResourcePropertyDialogVM = (IResourcePropertyDialogViewModel)workspaceData.WorkSpaceContextualData.DataValue;

                References.DataValue = ToViewModel(GetReferences(ResourcePropertyDialogVM.PropertyEntity.DataValue));
            }
        }

        private IEnumerable<ReferencedEntityViewModel> ToViewModel(EntityCollection entityCollection)
        {
            var reViewModels = new List<ReferencedEntityViewModel>();
            var bankPaths = new Dictionary<int, string>();
            foreach (var resourceEntity in entityCollection.Items.OfType<ResourceEntity>())
            {
                var bankId = resourceEntity.BankId;
                var bp = bankPaths.ContainsKey(bankId) ? bankPaths[bankId] : resourceEntity.GetFullBankPath();
                if (!bankPaths.ContainsKey(bankId)) { bankPaths.Add(bankId, bp); };
                var newReViewModel = new ReferencedEntityViewModel()
                {
                    Entity = resourceEntity,
                    BankPath = resourceEntity.GetFullBankPath()
                };

                if (!reViewModels.Any(m => m.BankPath.Equals(newReViewModel.BankPath) && m.Entity.Equals(newReViewModel.Entity)))
                {
                    reViewModels.Add(newReViewModel);
                }
            }
            return reViewModels;
        }


    }
}
