using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Cinch;
using Cito.Tester.Common;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels
{
    [ExportViewModel("ResourcePropertyDialog.DependenciesViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class DependenciesViewModel : ViewModelBase
    {

        static readonly PropertyChangedEventArgs DependentResourcesArgs = ObservableHelper.CreateArgs<DependenciesViewModel>(x => x.DependentResources);



        private readonly ICustomMessageBoxService customMessageBoxService;
        private readonly IViewAwareStatus _viewAwareStatusService;
        private readonly IWinFormsWindowService winFormsWindowService;
        private ListSortDirection _currentSortDirection = ListSortDirection.Ascending;
        private Dictionary<int, string> bankPaths;

        public IResourcePropertyDialogViewModel ResourcePropertyDialogVM { get; private set; }
        public EntityCollection<DependentResourceEntity> SelectedItems { get; private set; }



        public SimpleCommand<object, object> AddDependentResource { get; private set; }
        public SimpleCommand<object, object> RemoveDependentResource { get; private set; }
        public SimpleCommand<object, string> Sort { get; private set; }

        public DataWrapper<IEnumerable<DependentResourceViewModel>> DependentResources { get; private set; }



        [ImportingConstructor]
        public DependenciesViewModel(IViewAwareStatus viewAwareStatusService)
        {
            SelectedItems = new EntityCollection<DependentResourceEntity>();

            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;

            InitCommands();
            InitDataWrappers();

            winFormsWindowService = ViewModelRepository.Instance.Resolver.Container.GetExport<IWinFormsWindowService>().Value;
            customMessageBoxService = ViewModelRepository.Instance.Resolver.Container.GetExport<ICustomMessageBoxService>().Value;
        }



        private void InitDataWrappers()
        {
            DependentResources = new DataWrapper<IEnumerable<DependentResourceViewModel>>(this, DependentResourcesArgs);
        }

        private void InitCommands()
        {
            AddDependentResource = new SimpleCommand<object, object>(o => DoAddDependentResource());
            RemoveDependentResource = new SimpleCommand<object, object>(o => DoRemoveDependentResource());
            Sort = new SimpleCommand<object, string>(o => DoSort(o));
        }

        private void DoAddDependentResource()
        {
            var newDependencies = winFormsWindowService.OpenSelectDependentResourceDialog(ResourcePropertyDialogVM.PropertyEntity.DataValue);
            if (newDependencies.Any())
            {
                AddDependentResources(newDependencies.ToList());
                DependentResources.DataValue = ToViewModel(ResourcePropertyDialogVM.PropertyEntity.DataValue.DependentResourceCollection); DoSort("Name");
            }
        }

        private IEnumerable<DependentResourceViewModel> ToViewModel(EntityCollection<DependentResourceEntity> dependentResources)
        {
            var dpViewModels = new List<DependentResourceViewModel>();
            foreach (var dp in dependentResources)
            {
                var bankId = dp.DependentResource.BankId;
                var bp = bankPaths.ContainsKey(bankId) ? bankPaths[bankId] : dp.DependentResource.GetFullBankPath();
                if (!bankPaths.ContainsKey(bankId)) { bankPaths.Add(bankId, bp); };
                dpViewModels.Add(new DependentResourceViewModel() { Entity = dp, BankPath = bp });
            }
            return dpViewModels;
        }

        private void DoSort(string propertyName)
        {
            var sorted = new List<DependentResourceViewModel>();
            Func<DependentResourceViewModel, object> orderByFunc = DetermineOrderByFunc(propertyName);

            if (_currentSortDirection == ListSortDirection.Ascending)
            {
                sorted.AddRange(DependentResources.DataValue.OrderBy(orderByFunc));
            }
            else
            {
                sorted.AddRange(DependentResources.DataValue.OrderByDescending(orderByFunc));
            }

            _currentSortDirection = _currentSortDirection ^ ListSortDirection.Descending; DependentResources.DataValue = sorted;
        }

        private Func<DependentResourceViewModel, object> DetermineOrderByFunc(string propertyName)
        {
            switch (propertyName)
            {
                case "Name":
                    return depRes => depRes.Entity.DependentResource.Name;
                case "Title":
                    return depRes => depRes.Entity.DependentResource.Title;
                case "BankPath":
                    return depRes => depRes.BankPath;
                default:
                    return null;
            }
        }

        private void DoRemoveDependentResource()
        {
            var result = customMessageBoxService.ShowYesNo((string)Application.Current.FindResource("ResourcePropertyDialog.Tab.Dependencies.RemoveButton.Confirm"), string.Empty, CustomDialogIcons.Question);

            if (result == CustomDialogResults.Yes)
            {
                for (int i = 0; i < SelectedItems.Count; i++)
                {
                    ResourcePropertyDialogVM.PropertyEntity.DataValue.DependentResourceCollection.Remove(SelectedItems[i]);
                }

                DependentResources.DataValue = ToViewModel(ResourcePropertyDialogVM.PropertyEntity.DataValue.DependentResourceCollection);
            }
        }

        private void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;
                ResourcePropertyDialogVM = (IResourcePropertyDialogViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                bankPaths = new Dictionary<int, string>();

                GetDependencies(ResourcePropertyDialogVM.PropertyEntity.DataValue);
                DependentResources.DataValue = ToViewModel(ResourcePropertyDialogVM.PropertyEntity.DataValue.DependentResourceCollection);
                DoSort("Name");
            }
        }

        private void GetDependencies(IPropertyEntity datasource)
        {
            if (!ResourcePropertyDialogVM.PropertyEntity.DataValue.DependentResourceCollection.Any())
            {
                var resource = ResourceFactory.Instance.GetResourceByIdWithOption(datasource.Id, new ResourceRequestDTO() { WithDependencies = true });
                if (resource.DependentResourceCollection.Any())
                {
                    AddDependentResources(resource.DependentResourceCollection.Select(dr => dr.DependentResourceId).ToList());
                }
            }
        }

        private void AddDependentResources(List<Guid> resourceIds)
        {
            var dependentResources = ResourceFactory.Instance.GetResourcesByIdsWithOption(resourceIds, new ResourceRequestDTO());
            dependentResources.ToList().ForEach(dr =>
            {
                var entity = dr as ResourceEntity;
                if (entity != null)
                {
                    ResourcePropertyDialogVM.PropertyEntity.DataValue.DependentResourceCollection.Add(new DependentResourceEntity { DependentResource = entity });
                }
            });
        }


    }
}
