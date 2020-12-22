using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Versioning;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels
{
    [ExportViewModel("ResourcePropertyDialog.HistoryViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class HistoryViewModel : ViewModelBase
    {
        static readonly PropertyChangedEventArgs HistoryArgs = ObservableHelper.CreateArgs<HistoryViewModel>(x => x.HistoryList);
        static readonly PropertyChangedEventArgs CompareButtonVisibleArgs = ObservableHelper.CreateArgs<HistoryViewModel>(x => x.CompareButtonVisible);


        private readonly IHistoryService _historyService;
        private readonly IViewAwareStatus _viewAwareStatusService;
        private readonly IItemEditorObjectFactory _itemEditorObjectFactory;
        private readonly IAssessmentTestEditorObjectFactory _assessmentTestEditorObjectFactory;
        private readonly ITestPackageEditorObjectFactory _testPackageEditorObjectFactory;
        private readonly IDataSourceEditorObjectFactory _dataSourceEditorObjectFactory;
        private readonly IAspectEditorObjectFactory _aspectEditorObjectFactory;
        private readonly IGenericResourceEditorObjectFactory _genericResourceEditorObjectFactory;
        private readonly IItemLayoutTemplateEditorObjectFactory _itemLayoutTemplateObjectFactory;
        private readonly IControlTemplateEditorObjectFactory _controlTemplateObjectFactory;
        private readonly ICustomBankPropertyEditorObjectFactory _customBankPropertyEditorObjectFactory;

        public IResourcePropertyDialogViewModel ResourcePropertyDialogVM { get; private set; }
        public ICustomMessageBoxService CustomMessageBoxService { get; private set; }



        public SimpleCommand<object, MetaDataCompareResult> ShowDifferenceCommand { get; private set; }
        public SimpleCommand<object, object> ShowAllDifferencesOfTwoVersionsCommand { get; private set; }
        public SimpleCommand<object, ResourceHistoryEntity> CheckBoxCheckedCommand { get; private set; }
        public SimpleCommand<object, ResourceHistoryEntity> CheckBoxUncheckedCommand { get; private set; }
        public SimpleCommand<object, ResourceHistoryEntity> ExpanderExpandedCommand { get; private set; }
        public SimpleCommand<object, ResourceHistoryEntity> RevertButtonClickCommand { get; private set; }

        public DataWrapper<BindingList<History>> HistoryList { get; private set; }
        public DataWrapper<bool> CompareButtonVisible { get; private set; }



        [ImportingConstructor]
        public HistoryViewModel(IViewAwareStatus viewAwareStatusService, IHistoryService historyService, IItemEditorObjectFactory itemEditorObjectFactory, IAssessmentTestEditorObjectFactory assessmentTestEditorObjectFactory,
            ITestPackageEditorObjectFactory testPackageEditorObjectFactory, IDataSourceEditorObjectFactory dataSourceEditorObjectFactory, IAspectEditorObjectFactory aspectEditorFactory, IGenericResourceEditorObjectFactory genericResourceEditorObjectFactory,
            IItemLayoutTemplateEditorObjectFactory itemLayoutTemplateEditorObjectFactory, IControlTemplateEditorObjectFactory controlTemplateEditorObjectFactory, ICustomBankPropertyEditorObjectFactory customBankPropertyEditorObjectFactory)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;

            CustomMessageBoxService = ViewModelRepository.Instance?.Resolver?.Container?.GetExport<ICustomMessageBoxService>()?.Value;

            InitCommands();
            InitDataWrappers();

            _historyService = historyService;
            _itemEditorObjectFactory = itemEditorObjectFactory;
            _assessmentTestEditorObjectFactory = assessmentTestEditorObjectFactory;
            _testPackageEditorObjectFactory = testPackageEditorObjectFactory;
            _dataSourceEditorObjectFactory = dataSourceEditorObjectFactory;
            _aspectEditorObjectFactory = aspectEditorFactory;
            _genericResourceEditorObjectFactory = genericResourceEditorObjectFactory;
            _itemLayoutTemplateObjectFactory = itemLayoutTemplateEditorObjectFactory;
            _controlTemplateObjectFactory = controlTemplateEditorObjectFactory;
            _customBankPropertyEditorObjectFactory = customBankPropertyEditorObjectFactory;
        }



        private void InitDataWrappers()
        {
            HistoryList = new DataWrapper<BindingList<History>>(this, HistoryArgs);
            CompareButtonVisible = new DataWrapper<bool>(this, CompareButtonVisibleArgs);
        }

        private void InitCommands()
        {
            ShowDifferenceCommand = new SimpleCommand<object, MetaDataCompareResult>(DoShowDifference);
            ShowAllDifferencesOfTwoVersionsCommand = new SimpleCommand<object, object>(o => DoShowAllDifferencesOfTwoVersions());
            CheckBoxCheckedCommand = new SimpleCommand<object, ResourceHistoryEntity>(DoHandleChecked);
            CheckBoxUncheckedCommand = new SimpleCommand<object, ResourceHistoryEntity>(DoHandleUnchecked);
            ExpanderExpandedCommand = new SimpleCommand<object, ResourceHistoryEntity>(DoHandleExpand);
            RevertButtonClickCommand = new SimpleCommand<object, ResourceHistoryEntity>(DoHandleRevertButtonClick);
        }

        private void LoadHistory()
        {
            HistoryList.DataValue = new BindingList<History>();

            if (ResourcePropertyDialogVM.PropertyEntity.DataValue is IVersionable)
                if (ResourcePropertyDialogVM.PropertyEntity.DataValue is ResourceEntity)
                {
                    var resourceHistoryCollection = ResourcePropertyDialogVM.ResourcePropertyDialogObjectFactory.GetResourceHistoryByResource((ResourceEntity)ResourcePropertyDialogVM.PropertyEntity.DataValue);

                    foreach (ResourceHistoryEntity resourceHistoryEntity in resourceHistoryCollection)
                        HistoryList.DataValue.Add(new History(resourceHistoryCollection.OrderByDescending(x => ((ResourceHistoryEntity)x).Id).First() as ResourceHistoryEntity, resourceHistoryEntity));

                    HistoryList.DataValue = new BindingList<History>(HistoryList.DataValue.OrderByDescending(i => i.ResourceHistoryEntity.ModifiedDate).ToList());
                }
        }

        private void DoHandleRevertButtonClick(ResourceHistoryEntity selectedResourceHistoryEntity)
        {
            if (CustomMessageBoxService.ShowYesNo((string)Application.Current.FindResource("ResourcePropertyDialog.Tab.History.RevertButton.Confirm"), string.Empty, CustomDialogIcons.Question, CustomDialogResults.Yes) == CustomDialogResults.Yes)
            {
                var conflictingOpenEditors = GetConflictingOpenEditors(selectedResourceHistoryEntity.Resource).ToList();

                if (conflictingOpenEditors.Any())
                {
                    if (CustomMessageBoxService.ShowYesNo((string)Application.Current.FindResource("ResourcePropertyDialog.Tab.History.RevertButton.CloseOpenEditors"), string.Empty, CustomDialogIcons.Question) == CustomDialogResults.Yes)
                    {
                        var previousNrOfHistoryRecords = HistoryList.DataValue.Count;
                        CloseConflictingOpenEditors(conflictingOpenEditors);

                        LoadHistory();
                        if (HistoryList.DataValue.Count != previousNrOfHistoryRecords)
                        {
                            var versionNrOfNewRecord = HistoryList.DataValue.OrderByDescending(x => x.ResourceHistoryEntity.Id).First().ResourceHistoryEntity.Version;
                            CustomMessageBoxService.ShowInformation(string.Format((string)Application.Current.FindResource("ResourcePropertyDialog.Tab.History.RevertButton.ReinitiateRevert"), versionNrOfNewRecord), string.Empty, CustomDialogIcons.Exclamation);
                            return;
                        }
                    }
                    else
                        return;
                }

                var versionReverter = new VersionReverter(HistoryList.DataValue.First().ResourceHistoryEntity, selectedResourceHistoryEntity);
                var newPropertyEntity = versionReverter.Revert() as IPropertyEntity;

                if (IsResourceEntityDirty(newPropertyEntity))
                {
                    var errorMessage = UpdateResource(newPropertyEntity);

                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        Mediator.Instance.NotifyColleagues(ItemEditor.Constants.RefreshGridAndSelectResource, new EventArgs<IPropertyEntity>(newPropertyEntity));
                        LoadHistory(); ResourcePropertyDialogVM.PropertyEntity.DataValue = ResourcePropertyDialogVM.ResourcePropertyDialogObjectFactory.GetRequiredObjectsForPropertyEntityWithId(newPropertyEntity.Id, newPropertyEntity.GetType());
                    }
                    else
                    {
                        CustomMessageBoxService.ShowError(errorMessage, string.Empty, CustomDialogIcons.Exclamation);
                    }
                }
                else
                {
                    CustomMessageBoxService.ShowInformation(Application.Current.FindResource("ResourcePropertyDialog.Tab.History.RevertButton.NoChangesBetweenVersions").ToString(), string.Empty, CustomDialogIcons.Information);
                }
            }
        }

        private void CloseConflictingOpenEditors(IEnumerable<object> conflictingOpenEditors)
        {
            foreach (var conflictingOpenEditor in conflictingOpenEditors)
            {
                if (conflictingOpenEditor is System.Windows.Forms.Form)
                    ((System.Windows.Forms.Form)conflictingOpenEditor).Close();
                else if (conflictingOpenEditor is IItemEditorWindow)
                    ((IItemEditorWindow)conflictingOpenEditor).Close();
                else
                { }
            }
        }

        private IEnumerable<object> GetConflictingOpenEditors(IPropertyEntity propertyEntity)
        {
            var openWindows = _historyService.GetOpenWindows();

            if (propertyEntity is ItemResourceEntity)
                return openWindows.Where(x => x is IItemEditorWindow);
            if (propertyEntity is AssessmentTestResourceEntity)
                return openWindows.Where(x => x is System.Windows.Forms.Form && ((System.Windows.Forms.Form)x).Name == "TestEditor_v2");
            if (propertyEntity is GenericResourceEntity)
                return openWindows.Where(x => x is System.Windows.Forms.Form && ((System.Windows.Forms.Form)x).Name == "GenericResourceEditor");

            return new List<object>();
        }

        private string UpdateResource(IPropertyEntity propertyEntity)
        {
            if (propertyEntity is ItemResourceEntity)
                return _itemEditorObjectFactory.UpdateItemResource((ItemResourceEntity)propertyEntity);
            if (propertyEntity is AssessmentTestResourceEntity)
                return _assessmentTestEditorObjectFactory.UpdateAssessmentTestResource((AssessmentTestResourceEntity)propertyEntity);
            if (propertyEntity is DataSourceResourceEntity)
                return _dataSourceEditorObjectFactory.UpdateDataSourceResource((DataSourceResourceEntity)propertyEntity);
            if (propertyEntity is TestPackageResourceEntity)
                return _testPackageEditorObjectFactory.UpdateTestPackageResource((TestPackageResourceEntity)propertyEntity);
            if (propertyEntity is AspectResourceEntity)
                return _aspectEditorObjectFactory.UpdateAspectResource((AspectResourceEntity)propertyEntity);
            if (propertyEntity is GenericResourceEntity)
                return _genericResourceEditorObjectFactory.UpdateGenericResource((GenericResourceEntity)propertyEntity);
            if (propertyEntity is CustomBankPropertyEntity)
                return _customBankPropertyEditorObjectFactory.UpdateCustomBankProperty((CustomBankPropertyEntity)propertyEntity);
            if (propertyEntity is ItemLayoutTemplateResourceEntity)
                return _itemLayoutTemplateObjectFactory.UpdateItemLayoutTemplateResource((ItemLayoutTemplateResourceEntity)propertyEntity);
            if (propertyEntity is ControlTemplateResourceEntity)
                return _controlTemplateObjectFactory.UpdateControlTemplateResource((ControlTemplateResourceEntity)propertyEntity);

            throw new ArgumentException("HistoryViewModel.UpdateResource, Unsupported type: " + propertyEntity.GetType().ToString());
        }

        private Boolean IsResourceEntityDirty(IPropertyEntity propertyEntity)
        {
            return propertyEntity.IsDirty || propertyEntity.HasChangesInTopology();
        }

        private void DoHandleExpand(ResourceHistoryEntity resourceHistoryEntity)
        {
            var history = HistoryList.DataValue.FirstOrDefault(x => x.ResourceHistoryEntity.Id == resourceHistoryEntity.Id);
            if (history == null)
            {
                return;
            }

            var previousVersion = GetPreviousVersion(resourceHistoryEntity);
            history.NewVersion = resourceHistoryEntity.Version;

            if (previousVersion != null)
            {
                history.OldVersion = previousVersion.Version;

                if (history.MetaDataCompareResults.Count == 0)
                    history.MetaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(previousVersion, resourceHistoryEntity, ResourcePropertyDialogVM.PropertyEntity.DataValue.GetType(), ResourcePropertyDialogVM.ResourceManager.DataValue));
            }
        }

        private void DoHandleUnchecked(ResourceHistoryEntity resourceHistoryEntity)
        {
            if (HistoryList.DataValue.Count(x => x.IsChecked) == 2)
                SetComboBoxStatus(true);
            HistoryList.DataValue.First(i => i.ResourceHistoryEntity.Id == resourceHistoryEntity.Id).IsChecked = false;

            CompareButtonVisible.DataValue = HistoryList.DataValue.Count(i => i.IsChecked) == 2;
        }

        private void DoHandleChecked(ResourceHistoryEntity resourceHistoryEntity)
        {
            if (ResourcePropertyDialogVM.PropertyEntity.DataValue is IVersionable)
            {
                var history = HistoryList.DataValue.FirstOrDefault(x => x.ResourceHistoryEntity.Id == resourceHistoryEntity.Id);
                if (history != null)
                {
                    history.IsChecked = true;
                }

                if (HistoryList.DataValue.Count(x => x.IsChecked) == 2)
                    SetComboBoxStatus(false);
                CompareButtonVisible.DataValue = HistoryList.DataValue.Count(i => i.IsChecked) == 2;
            }
            else
                throw new ArgumentException("Unsupported type for versioning. Type is: " + ResourcePropertyDialogVM.PropertyEntity.DataValue.GetType());
        }

        private void SetComboBoxStatus(bool status)
        {
            foreach (var hist in HistoryList.DataValue)
                if (!hist.IsChecked)
                    hist.IsHistoryCheckBoxEnabled = status;
        }

        private ResourceHistoryEntity GetPreviousVersion(ResourceHistoryEntity currentResourceHistoryEntity)
        {
            var previousVersion = HistoryList.DataValue.FirstOrDefault(i => i.ResourceHistoryEntity.Id != currentResourceHistoryEntity.Id && DateTime.Compare(i.ResourceHistoryEntity.ModifiedDate, currentResourceHistoryEntity.ModifiedDate) < 0);
            return previousVersion?.ResourceHistoryEntity;
        }

        private void DoShowDifference(MetaDataCompareResult metaDataCompareResult)
        {
            History selectedHistory = null;

            foreach (var history in HistoryList.DataValue)
                if (history.MetaDataCompareResults.FirstOrDefault(i => i.Id == metaDataCompareResult.Id) != null)
                {
                    selectedHistory = history;
                    break;
                }

            _historyService.ShowDifferencesWindow(metaDataCompareResult, selectedHistory?.OldVersion, selectedHistory?.NewVersion);
        }

        private void DoShowAllDifferencesOfTwoVersions()
        {
            var checkedResourceHistoryEntities = HistoryList.DataValue.Where(i => i.IsChecked).ToList();

            _historyService.ShowDifferencesWindow(checkedResourceHistoryEntities.Last().ResourceHistoryEntity, checkedResourceHistoryEntities.First().ResourceHistoryEntity, ResourcePropertyDialogVM.PropertyEntity.DataValue.GetType(), ResourcePropertyDialogVM.ResourceManager.DataValue);
        }

        private void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;

                ResourcePropertyDialogVM = (IResourcePropertyDialogViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                CompareButtonVisible.DataValue = false;
                LoadHistory();
            }
        }


    }
}
