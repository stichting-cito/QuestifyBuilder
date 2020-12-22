using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Configuration;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    [ExportViewModel("ItemEditor.PresentationVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal class PresentationViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {
        static readonly PropertyChangedEventArgs EditControlEventArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.EditControl);
        static readonly PropertyChangedEventArgs ItemResourceEntityEventArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.ItemResourceEntity);
        static readonly PropertyChangedEventArgs AssessmentItemEventArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.AssessmentItem);
        static readonly PropertyChangedEventArgs ResourceManagerEventArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.ResourceManager);
        static readonly PropertyChangedEventArgs ParameterSetCollectionEventArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.ParameterSetCollection);
        static readonly PropertyChangedEventArgs ContextIdentifierEventArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.ContextIdentifier);
        static readonly PropertyChangedEventArgs IsOldItemArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.IsOldItem);
        static readonly PropertyChangedEventArgs LeftColumnWidthEventArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.ItemEditorLeftColumnWidth);
        static readonly PropertyChangedEventArgs RightColumnWidthEventArgs = ObservableHelper.CreateArgs<PresentationViewModel>(x => x.ItemEditorRightColumnWidth);


        private IPresentationControl _view;

        private readonly IViewAwareStatus _viewAwareStatusService;
        private readonly IResourceEditorService _resourceEditor;
        private IItemEditorViewModel _itemEditorVm;



        [ImportingConstructor]
        public PresentationViewModel(IViewAwareStatus viewAwareStatusService, IResourceEditorService resourceEditor)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;
            _resourceEditor = resourceEditor;

            ItemResourceEntity = new DataWrapper<ItemResourceEntity>(this, ItemResourceEntityEventArgs);
            AssessmentItem = new DataWrapper<AssessmentItem>(this, AssessmentItemEventArgs);
            ResourceManager = new DataWrapper<ResourceManagerBase>(this, ResourceManagerEventArgs);
            ParameterSetCollection = new DataWrapper<ParameterSetCollection>(this, ParameterSetCollectionEventArgs);
            ContextIdentifier = new DataWrapper<int?>(this, ContextIdentifierEventArgs);
            IsOldItem = new DataWrapper<bool>(this, IsOldItemArgs);

            EditControl = new DataWrapper<object>(this, EditControlEventArgs);
            EditControl.PropertyChanged += HandleEditControlPropertyChanged;
            EditGenericResource = new SimpleCommand<object, string>(RetrieveResourceAndEdit);

            ItemEditorLeftColumnWidth = new DataWrapper<GridLength>(this, LeftColumnWidthEventArgs);
            ItemEditorRightColumnWidth = new DataWrapper<GridLength>(this, RightColumnWidthEventArgs);
        }

        private void HandleEditControlPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _itemEditorVm.EditorChange(((DataWrapper<object>)sender).DataValue);
        }

        private void RetrieveResourceAndEdit(string resourceName)
        {
            Debug.Assert(_itemEditorVm != null);
            if (!string.IsNullOrWhiteSpace(resourceName))
            {
                var bankId = _itemEditorVm.ItemResourceEntity.DataValue.BankId;
                var resource = _itemEditorVm.ItemEditorObjectFactory.GetGenericResource(bankId, resourceName);
                _resourceEditor.Edit(resource.ResourceId, resource.MediaType);
            }
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            if (_itemEditorVm != null)
            {
                _itemEditorVm.Updated -= ItemEditor_Updated;
            }

            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }

        void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                _view = (IPresentationControl)view;
                var workspaceData = (IWorkSpaceAware)view;
                _itemEditorVm = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                _itemEditorVm.Updated += ItemEditor_Updated; if (!_itemEditorVm.IsLoading)
                    Update();
            }
        }

        private void ItemEditor_Updated(object sender, StringEventArgs e)
        {
            if (e.StringValue == "DoUpdate")
                Update();
        }


        public DataWrapper<object> EditControl { get; }
        public DataWrapper<ItemResourceEntity> ItemResourceEntity { get; }
        public DataWrapper<AssessmentItem> AssessmentItem { get; }
        public DataWrapper<ResourceManagerBase> ResourceManager { get; }
        public DataWrapper<ParameterSetCollection> ParameterSetCollection { get; }
        public DataWrapper<int?> ContextIdentifier { get; }
        public DataWrapper<bool> IsOldItem { get; }
        public DataWrapper<GridLength> ItemEditorLeftColumnWidth { get; }
        public DataWrapper<GridLength> ItemEditorRightColumnWidth { get; }
        public SimpleCommand<object, string> EditGenericResource { get; private set; }

        internal void Update()
        {
            if (_itemEditorVm.HasError.DataValue)
            {
                return;
            }

            if (string.IsNullOrEmpty(_itemEditorVm.ItemResourceEntity.DataValue.ItemLayoutTemplateUsedName))
            {
                _itemEditorVm.ReloadDependentResources();
            }

            IsOldItem.DataValue = _itemEditorVm.IsOlderItem.DataValue;
            ItemResourceEntity.DataValue = _itemEditorVm.ItemResourceEntity.DataValue;
            AssessmentItem.DataValue = _itemEditorVm.AssessmentItem.DataValue;
            ResourceManager.DataValue = _itemEditorVm.ResourceManager.DataValue;
            ContextIdentifier.DataValue = _itemEditorVm.ContextIdentifier.DataValue;
            ParameterSetCollection.DataValue = _itemEditorVm.ParameterSetCollection.DataValue;

            var leftColumnGridLength = UserSettings.ItemEditorLeftColumnWidth > 1 && UserSettings.ItemEditorRightColumnWidth > 1
                ? new GridLength(UserSettings.ItemEditorLeftColumnWidth, GridUnitType.Star)
                : new GridLength(1, GridUnitType.Star);

            var rightColumnGridLength = UserSettings.ItemEditorLeftColumnWidth > 1 && UserSettings.ItemEditorRightColumnWidth > 1
                ? new GridLength(UserSettings.ItemEditorRightColumnWidth, GridUnitType.Star)
                : new GridLength(1, GridUnitType.Star);

            ItemEditorLeftColumnWidth.DataValue = leftColumnGridLength;
            ItemEditorRightColumnWidth.DataValue = rightColumnGridLength;

            _view.ResourceManagerBase = ResourceManager.DataValue;
            _view.BankId = ItemResourceEntity.DataValue.BankId;
            _view.AssessmentItem = AssessmentItem.DataValue;
            _view.ContextIdentifier = ContextIdentifier.DataValue;

            if (_itemEditorVm.SelectedTab.DataValue == 0 && !_itemEditorVm.CurrentItemClosing)
            {
                _view.RefreshPreview();
            }

            _itemEditorVm.EnableElementsOnCompletion();
        }

        public void DoPreSaveTasks()
        {
            var cmdSupp = _view as ICommandSupport;
            cmdSupp?.DoPreSaveTasks();
        }

        public void DoPostSaveTasks()
        {
            Update();
            var cmdSupp = _view as ICommandSupport;
            cmdSupp?.DoPostSaveTasks();
        }

        public void DoTaskBeforeClosing()
        {
            var cmdSupp = _view as ICommandSupport;
            cmdSupp?.DoTaskBeforeClosing();
        }
        public void KillView()
        {
            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }
        }
        protected override void OnDispose()
        {
            if (EditControl != null)
            {
                EditControl.PropertyChanged -= HandleEditControlPropertyChanged;
            }

            if (_itemEditorVm != null)
            {
                _itemEditorVm.Updated -= ItemEditor_Updated; _itemEditorVm = null;
            }

            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }

            base.OnDispose();
        }
    }
}
