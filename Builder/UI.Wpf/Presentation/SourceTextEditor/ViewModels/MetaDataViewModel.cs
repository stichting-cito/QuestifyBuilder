using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using Cinch;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.GenericControls.ConsumerInterfaces;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.Views;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.ViewModels
{

    [ExportViewModel("SourceTextEditor.MetaDataVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class MetaDataViewModel : ViewModelBase, IMetadataViewConsumer, IViewModel2ViewCommandSupport
    {

        static readonly PropertyChangedEventArgs GenericMetadataWorkspaceArgs = ObservableHelper.CreateArgs<MetaDataViewModel>(x => x.GenericMetadataWorkSpace);


        private IMetaDataControl _view = null;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IViewAwareStatus _viewAwareStatusService;
        private ISourceTextEditorViewModel _sourceTextEditorVM;

        private readonly string _msgActionNotAllowedNeedSave;



        [ImportingConstructor]
        public MetaDataViewModel(IViewAwareStatus viewAwareStatusService, IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;

            var app = Application.Current;
            _msgActionNotAllowedNeedSave = (string)app.FindResource("SourceTextEditor.MetaDataViewModel.ActionNotAllowedNeedSave");

            InitProperties();
        }



        public DataWrapper<WorkspaceData> GenericMetadataWorkSpace { get; private set; }



        private void InitProperties()
        {
            GenericMetadataWorkSpace = new DataWrapper<WorkspaceData>(this, GenericMetadataWorkspaceArgs);
            GenericMetadataWorkSpace.DataValue = new WorkspaceData(string.Empty, GenericControls.Constants.MetadataWorkSpace, this, string.Empty, false);
        }

        void ViewAwareStatusServiceViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                _view = (IMetaDataControl)view;
                var workspaceData = (IWorkSpaceAware)view;
                _sourceTextEditorVM = (ISourceTextEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                _sourceTextEditorVM.Updated += (s, e) => { if (e.StringValue == "DoUpdate") Update(); }; if (!_sourceTextEditorVM.IsLoading) Update();

            }

        }

        private void Update()
        {
            if (_sourceTextEditorVM.HasError.DataValue) return;
            _view.Update((GenericResourceEntity)_sourceTextEditorVM.GenericResourceEntity.DataValue);
        }

        private void ExecuteIfSaved(Action action)
        {
            Debug.Assert(_sourceTextEditorVM != null); if (_sourceTextEditorVM.NeedSave())
            {
                _messageBoxService.ShowInformation(_msgActionNotAllowedNeedSave);
            }
            else
            {
                action();
            }
        }



        public void DoPreSaveTasks()
        {
            _view.PreSaveTasks();
        }

        public void DoTaskBeforeClosing()
        { }

        public void KillView() { }
        public void DoPostSaveTasks() { }



        IPropertyEntity IMetadataViewConsumer.ResourceToEditMetadataFor
        {
            get { return _sourceTextEditorVM.GenericResourceEntity.DataValue; }
        }

        bool IMetadataViewConsumer.EditNameAllowed
        {
            get { return _sourceTextEditorVM.GenericResourceEntity.DataValue != null && _sourceTextEditorVM.GenericResourceEntity.DataValue.IsNew; }
        }

        bool IMetadataViewConsumer.EditTitleAllowed
        {
            get { return true; }
        }

        bool IMetadataViewConsumer.OpenResourcePropertyDialogButtonVisible
        {
            get { return true; }
        }

    }
}
