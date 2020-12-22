using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using Cinch;
using Cito.Tester.ContentModel;
using Enums;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ItemProcessing;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{

    [ExportViewModel("ItemEditor.MetaDataVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class MetaDataViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {
        static readonly PropertyChangedEventArgs ConceptStructureArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.ConceptStructureWorkspace); static readonly PropertyChangedEventArgs CanAddConceptStructuresArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.CanAddConceptStructures);
        static readonly PropertyChangedEventArgs TreeStructureArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.TreeStructureWorkspace); static readonly PropertyChangedEventArgs CanAddTreeStructuresArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.CanAddTreeStructures);
        static readonly PropertyChangedEventArgs CanChangeCodeArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.CanChangeCode);


        private IMetaDataControl _view;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IViewAwareStatus _viewAwareStatusService;
        private readonly ISelectDialogFactory _selectDialogFactory;
        private IItemEditorViewModel _itemEditorVm = null;

        private readonly string _msgActionNotAllowedNeedSave;
        private readonly string _msgActionNotAllowedWhenNew;



        [ImportingConstructor]
        public MetaDataViewModel(IViewAwareStatus viewAwareStatusService,
            IMessageBoxService messageBoxService,
            ISelectDialogFactory selectDialogFactory)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _messageBoxService = messageBoxService;
            _selectDialogFactory = selectDialogFactory;

            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;

            var app = Application.Current;
            _msgActionNotAllowedNeedSave = (string)app.FindResource("ItemEditor.MetaDataViewModel.ActionNotAllowedNeedSave");
            _msgActionNotAllowedWhenNew = (string)app.FindResource("ItemEditor.MetaDataViewModel.ActionNotAllowedWhenNew");

            ResourcePropertyDialogService = ViewModelRepository.Instance.Resolver.Container.GetExport<IResourcePropertyDialogService>().Value;

            InitProperties();
        }



        public IResourcePropertyDialogService ResourcePropertyDialogService { get; private set; }

        public DataWrapper<bool> CanAddConceptStructures { get; set; }

        public DataWrapper<WorkspaceData> ConceptStructureWorkspace { get; set; }

        public DataWrapper<bool> CanAddTreeStructures { get; set; }

        public DataWrapper<WorkspaceData> TreeStructureWorkspace { get; set; }

        private void InitCustomBankProperties()
        {
            if (_itemEditorVm == null) return;

            if (ConceptStructureWorkspace != null)
                ConceptStructureWorkspace.DataValue.DataValue = _itemEditorVm;
            CanAddConceptStructures.DataValue = _itemEditorVm.IsConceptDefinedOnBankBranch;

            TreeStructureWorkspace.DataValue.DataValue = _itemEditorVm;
            CanAddTreeStructures.DataValue = _itemEditorVm.IsTreeDefinedOnBankBranch;
        }

        public DataWrapper<bool> CanChangeCode { get; set; }



        private void InitProperties()
        {
            CanAddConceptStructures = new DataWrapper<bool>(this, CanAddConceptStructuresArgs) { DataValue = false };
            ConceptStructureWorkspace = ItemEditorWorkspaceFactory.Create((ItemEditorViewModel)_itemEditorVm, ConceptStructureArgs, ItemEditorWorkspaceFactory.CreateConceptStructure);

            CanAddTreeStructures = new DataWrapper<bool>(this, CanAddTreeStructuresArgs) { DataValue = false };
            TreeStructureWorkspace = ItemEditorWorkspaceFactory.Create((ItemEditorViewModel)_itemEditorVm, TreeStructureArgs, ItemEditorWorkspaceFactory.CreateTreeStructure);

            CanChangeCode = new DataWrapper<bool>(this, CanChangeCodeArgs) { DataValue = false };
        }

        private void ItemEditor_Updated(object sender, StringEventArgs e)
        {
            if (e.StringValue == "DoUpdate")
                Update();
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            if (_itemEditorVm != null)
                _itemEditorVm.Updated -= ItemEditor_Updated;
            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }

        void viewAwareStatusService_ViewLoaded()
        {
            if (Designer.IsInDesignMode) return;

            var view = _viewAwareStatusService.View;
            _view = (IMetaDataControl)view;
            var workspaceData = (IWorkSpaceAware)view;
            _itemEditorVm = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;
            _itemEditorVm.Updated += ItemEditor_Updated; if (!_itemEditorVm.IsLoading) Update();

            _view.DoItemRename = () => ExecuteIfSaved(RenameItem);
            _view.DoSwitchTemplate = () => ExecuteIfSaved(SwitchItemLayoutTemplate);
            _view.DoOpenResourcePropertyDialog = () => ExecuteIfNotNew(OpenResourcePropertyDialog);
        }

        private void Update()
        {
            if (_itemEditorVm.HasError.DataValue) return;
            CanChangeCode.DataValue = _itemEditorVm.CanChangeCode.DataValue;
            _view.Update(_itemEditorVm.AssessmentItem.DataValue, _itemEditorVm.ItemResourceEntity.DataValue);

            if (_itemEditorVm.ItemResourceEntity.DataValue != null)
                InitCustomBankProperties();

            _itemEditorVm.EnableElementsOnCompletion();
        }

        private void ExecuteIfNotNew(Action action)
        {
            Debug.Assert(_itemEditorVm != null); if (_itemEditorVm.ItemResourceEntity.DataValue.IsNew)
            {
                _messageBoxService.ShowInformation(_msgActionNotAllowedWhenNew);
            }
            else
            {
                action();
            }
        }

        private void ExecuteIfSaved(Action action)
        {
            Debug.Assert(_itemEditorVm != null); if (_itemEditorVm.NeedSave())
            {
                _messageBoxService.ShowInformation(_msgActionNotAllowedNeedSave);
            }
            else
            {
                action();
            }
        }

        private void OpenResourcePropertyDialog()
        {
            ResourcePropertyDialogService.Show(_itemEditorVm.ItemResourceEntity.DataValue.ResourceId, _itemEditorVm.ItemResourceEntity.DataValue.GetType(), 4);
        }

        internal void SwitchItemLayoutTemplate()
        {
            var worker = new ItemTemplateSwitching(_itemEditorVm.ItemResourceEntity.DataValue, _itemEditorVm.ResourceManager.DataValue, _messageBoxService);
            var exclude = new List<ItemTypeEnum>(new[] { ItemTypeEnum.Error, ItemTypeEnum.Inline });
            var dialog = _selectDialogFactory.GetSelectItemLayoutTemplate(_itemEditorVm.ItemResourceEntity.DataValue.BankId, exclude, true, _itemEditorVm.AssessmentItem.DataValue.LayoutTemplateSourceName);

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_itemEditorVm.ItemResourceEntity.DataValue.ResourceId != dialog.SelectedEntity.ResourceId)
                {
                    AssessmentItem newAssesmentItem = null;
                    if (worker.SwitchToTemplate(dialog.SelectedEntity.Name, ref newAssesmentItem))
                    {
                        _itemEditorVm.AssessmentItem.DataValue = newAssesmentItem;
                        _itemEditorVm.ParameterSetCollection.DataValue = newAssesmentItem.Parameters;
                        _itemEditorVm.ItemResourceEntity.DataValue.ReloadItemLayoutTemplateUsed();

                        Update();
                    }
                    else
                    {
                        if (worker.LastErrorOrWarning != null && worker.LastErrorOrWarning.Errors)
                            _messageBoxService.ShowError(worker.LastErrorOrWarning.ErrorList[0]);

                        _itemEditorVm.SaveNeeded.DataValue = false;
                    }
                }
            }
        }

        private void RenameItem()
        {
            string currName = _itemEditorVm.AssessmentItem.DataValue.Identifier;
            EntityCollection referencedResources;
            bool b = _itemEditorVm.ItemEditorObjectFactory.RenameItem(_itemEditorVm.ItemResourceEntity.DataValue, _itemEditorVm.AssessmentItem.DataValue, out referencedResources);
            if (b)
            {
                _itemEditorVm.DoSave();
                foreach (var r in referencedResources)
                {
                    if (r is AssessmentTestResourceEntity)
                    {
                        var atr = (AssessmentTestResourceEntity)r;
                        atr.RenameItemCode(currName, _itemEditorVm.AssessmentItem.DataValue.Identifier);
                    }
                }
                Update();
            }
        }

        public void DoPreSaveTasks()
        {
            DoPreSaveTaskForWorkspaces();

            var cmdSupp = _view as ICommandSupport;
            if (cmdSupp != null)
                cmdSupp.DoPreSaveTasks();
        }

        public void DoTaskBeforeClosing()
        {
        }

        private static void DoWorkspaceKillView(DataWrapper<WorkspaceData> wsd)
        {
            var csp = wsd.DataValue?.ViewModelInstance as IViewModel2ViewCommandSupport;
            csp?.KillView();
            if (csp != null)
            {
                ((ViewModelBase)wsd.DataValue?.ViewModelInstance).Dispose();
            }
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
            if (ConceptStructureWorkspace != null)
            {
                DoWorkspaceKillView(ConceptStructureWorkspace);
            }

            if (TreeStructureWorkspace != null)
            {
                DoWorkspaceKillView(TreeStructureWorkspace);
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

        public void DoPostSaveTasks()
        {
            DoPostSaveTaskForWorkspaces();

            var cmdSupp = _view as ICommandSupport;
            if (cmdSupp != null)
                cmdSupp.DoPostSaveTasks();
            Update();
        }

        private void DoPreSaveTaskForWorkspaces()
        {
            DoPreSaveTaskForWorkspace(ConceptStructureWorkspace);
            DoPreSaveTaskForWorkspace(TreeStructureWorkspace);
        }

        private void DoPostSaveTaskForWorkspaces()
        {
            DoPostSaveTaskForWorkspace(ConceptStructureWorkspace);
            DoPostSaveTaskForWorkspace(TreeStructureWorkspace);
        }

        private void DoPreSaveTaskForWorkspace(DataWrapper<WorkspaceData> workspace)
        {
            if (workspace.DataValue != null)
            {
                var csp = workspace.DataValue.ViewModelInstance as IViewModel2ViewCommandSupport;
                if (csp != null)
                {
                    csp.DoPreSaveTasks();
                }
            }
        }

        private void DoPostSaveTaskForWorkspace(DataWrapper<WorkspaceData> workspace)
        {
            if (workspace.DataValue != null)
            {
                var csp = workspace.DataValue.ViewModelInstance as IViewModel2ViewCommandSupport;
                if (csp != null)
                {
                    csp.DoPostSaveTasks();
                }
            }
        }

    }
}
