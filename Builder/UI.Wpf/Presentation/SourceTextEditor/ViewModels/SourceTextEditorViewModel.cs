using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Interfaces.UI;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UI.Wpf.Presentation.Types;
using Application = System.Windows.Application;
using MenuItem = Fluent.MenuItem;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.ViewModels
{
    internal class SourceTextEditorViewModel : EditorViewModelBase, ISourceTextEditorViewModel
    {

        private static readonly PropertyChangedEventArgs SelectedTabArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.SelectedTab); private static readonly PropertyChangedEventArgs SourceTextIdArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.SourceTextId);
        private static readonly PropertyChangedEventArgs IsRibbonMinimizedArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.IsRibbonMinimized); private static readonly PropertyChangedEventArgs SaveNeededArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.SaveNeeded); private static readonly PropertyChangedEventArgs WindowTitleArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.WindowTitle); private static readonly PropertyChangedEventArgs MetadataArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.MetadataWorkspace); private static readonly PropertyChangedEventArgs TextEditorArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.TextEditorWorkspace); private static readonly PropertyChangedEventArgs LinkedStyleSheetsArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.LinkedStyleSheetsWorkspace); private static readonly PropertyChangedEventArgs HasErrorArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.HasError); private static readonly PropertyChangedEventArgs IsWorkingArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.IsWorking);
        private static readonly PropertyChangedEventArgs GenericResourceEntityEventArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.GenericResourceEntity);
        private static readonly PropertyChangedEventArgs ResourceManagerEventArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.ResourceManager);
        private static readonly PropertyChangedEventArgs BankEventArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.BankId);
        private static readonly PropertyChangedEventArgs ContextIdentifierEventArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.ContextIdentifier);
        private static readonly PropertyChangedEventArgs AvailableStylesArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.AvailableStyles);
        private static readonly PropertyChangedEventArgs AvailableLanguagesArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.AvailableLanguages);
        private static readonly PropertyChangedEventArgs EditControlEventArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.EditControl);

        private static readonly PropertyChangedEventArgs ShowTextToSpeechArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.ShowTextToSpeech);
        private static readonly PropertyChangedEventArgs ShowTextToSpeechGroupArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.ShowTextToSpeechGroup);
        private static readonly PropertyChangedEventArgs AvailableDefaultDurationsArgs = ObservableHelper.CreateArgs<SourceTextEditorViewModel>(x => x.AvailableDefaultDurations);



        private bool _isLoading = true; private readonly IMessageBoxService _messageBoxService;
        private readonly ISourceTextEditorObjectFactory _objFactory;
        private readonly ILegacyInputBox _inputBox;

        private Lazy<Model.ContentModel.HelperClasses.EntityCollection> _lazyCustomBankPropertyCollection;

        private string _msgError;

        private string _msgPleaseConfirm;
        private string _msgPendingRequiredFields;
        private string _msgPendingChangesWishToLeave;
        private string _msgPleaseNewItemCode;
        private string _msgPleaseNewItemCodeDialigTitle;
        private string _msgDefaultPrefixNewCode;
        private string _msgNameNotUniqueInBank;



        public event EventHandler<StringEventArgs> Updated;



        public SourceTextEditorViewModel()
        {
            TestSessionContext.ResourceNeeded += Handle_TestSessionContext_ResourceNeeded;

            InitDataWrappers();

            InitCommands();

            InitLazy();

            _messageBoxService = ViewModelRepository.Instance.Resolver.Container.GetExport<IMessageBoxService>().Value;
            _objFactory = ViewModelRepository.Instance.Resolver.Container.GetExport<ISourceTextEditorObjectFactory>().Value;
            _inputBox = ViewModelRepository.Instance.Resolver.Container.GetExport<ILegacyInputBox>().Value;

            GetStrings();

            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += CheckIfSaveIsNecessary;
            timer.Start();
        }

        private void InitDataWrappers()
        {
            SelectedTab = new DataWrapper<int>(this, SelectedTabArgs) { DataValue = -1 };
            SourceTextId = new DataWrapper<Guid>(this, SourceTextIdArgs); SourceTextId.PropertyChanged += (s, e) => HandleSourceTextIdChanged();
            IsRibbonMinimized = new DataWrapper<bool>(this, IsRibbonMinimizedArgs);
            SaveNeeded = new DataWrapper<bool>(this, SaveNeededArgs);
            WindowTitle = new DataWrapper<string>(this, WindowTitleArgs);
            HasError = new DataWrapper<bool>(this, HasErrorArgs);
            IsWorking = new DataWrapper<bool>(this, IsWorkingArgs);

            GenericResourceEntity = new DataWrapper<IPropertyEntity>(this, GenericResourceEntityEventArgs);
            ResourceManager = new DataWrapper<ResourceManagerBase>(this, ResourceManagerEventArgs);
            BankId = new DataWrapper<int>(this, BankEventArgs);
            ContextIdentifier = new DataWrapper<int?>(this, ContextIdentifierEventArgs);
            AvailableStyles = new DataWrapper<IList<string>>(this, AvailableStylesArgs);
            AvailableLanguages = new DataWrapper<IList<string>>(this, AvailableLanguagesArgs);

            EditControl = new DataWrapper<object>(this, EditControlEventArgs);
            EditControl.PropertyChanged += (s, e) => EditorChange(EditControl.DataValue);

            MetadataWorkspace = new DataWrapper<WorkspaceData>(this, MetadataArgs);
            TextEditorWorkspace = new DataWrapper<WorkspaceData>(this, TextEditorArgs);
            LinkedStyleSheetsWorkspace = new DataWrapper<WorkspaceData>(this, LinkedStyleSheetsArgs);

            MetadataWorkspace.DataValue = new WorkspaceData(string.Empty, Constants.MetadataWorkSpace, this, string.Empty, false);
            TextEditorWorkspace.DataValue = new WorkspaceData(string.Empty, Constants.TextEditorWorkSpace, this, string.Empty, false);
            LinkedStyleSheetsWorkspace.DataValue = new WorkspaceData(string.Empty, Constants.LinkedStyleSheetsWorkSpace, this, string.Empty, false);

            ShowTextToSpeech = new DataWrapper<bool>(this, ShowTextToSpeechArgs);
            ShowTextToSpeechGroup = new DataWrapper<bool>(this, ShowTextToSpeechGroupArgs);
            AvailableDefaultDurations = new DataWrapper<List<MenuItem>>(this, AvailableDefaultDurationsArgs);
        }

        private void InitLazy()
        {
            _lazyCustomBankPropertyCollection = new Lazy<Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection>(
                () => _objFactory.GetCustomBankPropertiesForBranch(GenericResourceEntity.DataValue.BankId)
            );
        }

        internal void HandleSourceTextIdChanged()
        {
            if (StopBeforeLoosePendingChanges())
            {
                return;
            }

            _isLoading = true;
            SelectedTab.DataValue = 0; Dispatcher.CurrentDispatcher.InvokeAsynchronouslyInBackground(() =>
{
DoActualLoadOnSourceTextId(SourceTextId.DataValue);
});
        }

        internal void DoActualLoadOnSourceTextId(Guid id)
        {
            try
            {
                var data = _objFactory.GetRequiredObjectsForSourceTextWithId(id);

                if (ResourceManager.DataValue == null)
                {
                    TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(ResourceManager.DataValue);
                    TakeValuesFromFactory(data);
                    UpdateWindowTitle();
                    ContextIdentifier.DataValue = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(ResourceManager.DataValue);


                    SetStateVariables();
                    Updated?.Invoke(this, new StringEventArgs("DoUpdate"));
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex.Message, _msgError);
                OnErrorNullDataObjects();
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void TakeValuesFromFactory(Tuple<GenericResourceEntity, int, ResourceManagerBase> data)
        {
            GenericResourceEntity.DataValue = data.Item1;
            BankId.DataValue = data.Item2;
            ResourceManager.DataValue = data.Item3;
        }

        internal void CreateNewItem(int bankId, bool makeSourceTextTemplate)
        {
            if (StopBeforeLoosePendingChanges())
            {
                return;
            }

            _isLoading = true;
            SelectedTab.DataValue = 1; TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(ResourceManager.DataValue);

            var data = _objFactory.GetRequiredObjectsForNewSourceText(bankId, makeSourceTextTemplate);
            TakeValuesFromFactory(data);

            ContextIdentifier.DataValue = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(ResourceManager.DataValue);


            SetStateVariables();

            Updated?.Invoke(this, new StringEventArgs("DoUpdate"));
            _isLoading = false;
            IsWorking.DataValue = false;
        }

        void SetStateVariables()
        {
            HasError.DataValue = false;
        }

        private void GetStrings()
        {
            var prefix = "SourceTextEditor.SourceTextEditorViewModel.";
            var app = Application.Current;

            _msgError = (string)app.FindResource("Dialog.ErrorCaption");
            _msgPleaseConfirm = (string)app.FindResource("Dialog.PleaseConfirmCaption");
            _msgPendingRequiredFields = (string)app.FindResource(prefix + "PendingRequiredFields");
            _msgPendingChangesWishToLeave = (string)app.FindResource(prefix + "PendingChangesWishToLeave");
            _msgPleaseNewItemCode = (string)app.FindResource(prefix + "PleaseNewItemCode");
            _msgPleaseNewItemCodeDialigTitle = (string)app.FindResource(prefix + "PleaseNewItemCodeDialogTitle");
            _msgDefaultPrefixNewCode = (string)app.FindResource(prefix + "DefaultPrefixNewCode");
            _msgNameNotUniqueInBank = (string)app.FindResource(prefix + "NameNotUniqueInBank");
        }

        void OnErrorNullDataObjects()
        {
            HasError.DataValue = true;
            GenericResourceEntity.DataValue = null;
            ResourceManager.DataValue = null;
        }



        public DataWrapper<Guid> SourceTextId { get; private set; }
        public DataWrapper<IPropertyEntity> GenericResourceEntity { get; private set; }
        public DataWrapper<int> BankId { get; private set; }
        public DataWrapper<object> EditControl { get; private set; }
        public DataWrapper<WorkspaceData> MetadataWorkspace { get; private set; }
        public DataWrapper<WorkspaceData> TextEditorWorkspace { get; private set; }
        public DataWrapper<WorkspaceData> LinkedStyleSheetsWorkspace { get; private set; }



        public bool IsLoading
        {
            get { return _isLoading; }
        }

        public ISourceTextEditorObjectFactory SourceTextEditorObjectFactory
        {
            get { return _objFactory; }
        }

        public IEnumerable<SD.LLBLGen.Pro.ORMSupportClasses.EntityBase2> CustomBankProperties
        {
            get { return _lazyCustomBankPropertyCollection.Value; }
        }

        public bool CustomBankPropertiesRetrieved
        {
            get { return _lazyCustomBankPropertyCollection.IsValueCreated; }
        }



        public SimpleCommand<object, object> InsertSymbolReference { get; private set; }
        public SimpleCommand<object, object> InsertHighlightReference { get; private set; }
        public SimpleCommand<object, object> InsertElementReference { get; private set; }
        public SimpleCommand<object, object> RemoveReference { get; private set; }
        public SimpleCommand<object, object> OverViewReferences { get; private set; }



        private void InitCommands()
        {
            InitSaveCommands();

            WindowClosing = new SimpleCommand<object, EventToCommandArgs>(CanClose);

            ToggleRibbonMinimize = new SimpleCommand<object, object>(o => IsRibbonMinimized.DataValue = !IsRibbonMinimized.DataValue);
            InitCopyPasteCommands();
            InitMediaCommands();

            InitTextFormatCommands();

            AddTable = Command<IRichText>(can => can.CanAddTable, cmd => cmd.AddTable());
            AddFormula = Command<IRichText>(can => can.CanAddFormula, cmd => cmd.AddFormula());

            OpenSymbolDialog = new SimpleCommand<object, object>(o => CanAddSpecialSymbol(), o => DoOpenSymbolDialog());
            AddInlineControl = Command<IRichText>(can => can.CanAddInlineControl, cmd => cmd.AddInlineControl());

            InitReferenceCommands();

            InitTextToSpeechCommands();

            SetStyleCommand = new SimpleCommand<object, string>(SetStyle);
            SetLanguageCommand = new SimpleCommand<object, string>(SetLanguage);
        }

        private void InitReferenceCommands()
        {
            AddReference = Command<IRichText>(can => can.CanAddReference, cmd => cmd.AddReference(string.Empty));
            InsertSymbolReference = Command<IRichText>(can => true, cmd => cmd.InsertSymbolReference());
            InsertHighlightReference = Command<IRichText>(can => true, cmd => cmd.InsertHighlightReference());
            InsertElementReference = Command<IRichText>(can => true, cmd => cmd.InsertElementReference());
            RemoveReference = Command<IRichText>(can => true, cmd => cmd.RemoveReference());
            OverViewReferences = Command<IRichText>(can => true, cmd => cmd.OverviewReferences());
        }

        private void InitSaveCommands()
        {
            Save = new SimpleCommand<object, object>(o => DoSave());
            SaveAs = new SimpleCommand<object, object>(o => DoSaveAs());
            SaveAndClose = new SimpleCommand<object, object>(o =>
            {
                DoSave();
                RaiseCloseRequest(true);
            });
        }

        public void EditorChange(object editor)
        {
            if (editor != null || CurrentEditor != null)
            {
                var oldEditor = CurrentEditor as IRichText;
                CurrentEditor = editor;
                SetAvailableStyles(CurrentEditor as IRichText);
                SetAvailableLanguages(CurrentEditor as IRichText);
                SetTextToSpeechOptionsVisibility(CurrentEditor as IRichText);
                SetTextToSpeechOptionsEnabled(CurrentEditor as IRichText);

                if (CurrentEditor is IRichText)
                {
                    (CurrentEditor as IRichText).SelectionChanged += OnEditorSelectionChanged;
                }

                if (oldEditor != null)
                {
                    oldEditor.SelectionChanged -= OnEditorSelectionChanged;
                    oldEditor.ResetCurrentSelection();
                }

                CommandManager.InvalidateRequerySuggested();
            }
        }

        private void OnEditorSelectionChanged(object sender, EventArgs e)
        {
            if (CurrentEditor is IRichText)
                ShowTextToSpeech.DataValue = (CurrentEditor as IRichText).CanSetTextToSpeechOptions;
        }

        public void StylesheetsCollectionChanged()
        {
            if (Updated != null)
            {
                Updated(this, new StringEventArgs("DoStyleSheetUpdate"));
            }
        }



        private void CheckIfSaveIsNecessary(object sender, EventArgs e)
        {
            SaveNeeded.DataValue |= NeedSave();
        }

        public void DoSave()
        {
            IsWorking.DataValue = true;

            NecessaryEvil();

            var canSave = CanSave();

            if (NeedSave() && canSave)
            {
                UpdateWindowTitle();

                IsWorking.DataValue = true;

                var resourceEntity = GenericResourceEntity.DataValue as GenericResourceEntity;
                if (resourceEntity != null && resourceEntity.RequiresMajorVersionIncrement() && !IncrementMajorVersion(resourceEntity))
                {
                    IsWorking.DataValue = false;
                    return;
                }

                ((GenericResourceEntity)GenericResourceEntity.DataValue).UpdateDependencies();
                var errorMessage = _objFactory.UpdateSourceTextResource((GenericResourceEntity)GenericResourceEntity.DataValue);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    _messageBoxService.ShowError(errorMessage, _msgError);
                }

                IsWorking.DataValue = false;
            }
            else
            {
                if (!canSave)
                {
                    _messageBoxService.ShowError(_msgPendingRequiredFields, _msgError);
                }
            }
            SaveNeeded.DataValue = false;
            IsWorking.DataValue = false;
        }

        private static bool IncrementMajorVersion(ResourceEntity entity)
        {
            var majorVersionDialogService = ViewModelRepository.Instance.Resolver.Container.GetExport<IMajorVersionDialogService>().Value;
            return majorVersionDialogService.Show(entity);
        }

        private bool CanSave()
        {
            if (HasError.DataValue)
            {
                return false;
            }

            if (string.IsNullOrEmpty(GenericResourceEntity.DataValue.Title))
            {
                return false;
            }

            GenericResourceEntity.DataValue.ValidateEntity();
            var canAccordingToGenericResource = ((IDataErrorInfo)GenericResourceEntity.DataValue).Error == string.Empty;

            return canAccordingToGenericResource;
        }

        private bool StopBeforeLoosePendingChanges()
        {
            if (NeedSave())
            {
                var r = _messageBoxService.ShowYesNo(_msgPendingChangesWishToLeave, _msgPleaseConfirm, CustomDialogIcons.Exclamation);
                return r == CustomDialogResults.No;
            }
            return false;
        }

        private void CanClose(EventToCommandArgs e)
        {
            var args = (CancelEventArgs)e.EventArgs;
            args.Cancel = StopBeforeLoosePendingChanges();
            if (!args.Cancel)
            {
                if (ResourceManager.DataValue != null)
                {
                    TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(ResourceManager.DataValue);
                }
                TestSessionContext.ResourceNeeded -= Handle_TestSessionContext_ResourceNeeded;

                if (GenericResourceEntity.DataValue != null)
                {
                    Mediator.Instance.NotifyColleagues("ResourceEditor_RefreshGridAndSelectResource",
                        new EventArgs<IPropertyEntity>(GenericResourceEntity.DataValue));
                }
            }
        }

        private void UpdateWindowTitle()
        {
            WindowTitle.DataValue = CreateWindowTitle(GenericResourceEntity.DataValue.Title, GenericResourceEntity.DataValue.Name);
        }

        private static string CreateWindowTitle(string code, string title)
        {
            const string format = "{0} - ({1})";
            const int maxLength = 100;
            var t = title;

            if (t.Length > maxLength)
            {
                t = t.Substring(0, maxLength) + "...";
            }
            return string.Format(format, code, t);
        }

        private void DoSaveAs()
        {
            if (GenericResourceEntity.DataValue.IsNew)
            {
                return;
            }

            NecessaryEvil();

            if (!CanSave())
            {
                _messageBoxService.ShowError(_msgPendingRequiredFields, _msgError);
                return;
            }

            var result = _inputBox.Show(
                _msgPleaseNewItemCode,
                false,
                _msgPleaseNewItemCodeDialigTitle,
                string.Format(_msgDefaultPrefixNewCode, GenericResourceEntity.DataValue.Name),
                Cito.Tester.Common.ValidationHelper.IsValidResourceCode);

            if (result.ReturnCode != DialogResult.OK)
            {
                return;
            }

            var newName = result.Text.Trim();
            if (GenericResourceEntity.DataValue.BankId.IsResourceNameTaken(newName))
            {
                _messageBoxService.ShowError(string.Format(_msgNameNotUniqueInBank, newName), _msgError);
                return;
            }
            GenericResourceEntity.DataValue = ((GenericResourceEntity)GenericResourceEntity.DataValue).CopyToNew(newName);
            GenericResourceEntity.DataValue.Version = string.Empty;
            UpdateWindowTitle();
            IsWorking.DataValue = true;
            var errorMessage = _objFactory.UpdateSourceTextResource((GenericResourceEntity)GenericResourceEntity.DataValue); if (!string.IsNullOrEmpty(errorMessage))
            {
                _messageBoxService.ShowError(errorMessage, _msgError);
            }

            IsWorking.DataValue = false;
        }

        private void NecessaryEvil()
        {
            DoPreSaveTasksFor(MetadataWorkspace);
            DoPreSaveTasksFor(TextEditorWorkspace);
        }

        public bool NeedSave()
        {
            if (HasError.DataValue)
            {
                return false;
            }
            return (GenericResourceEntity.DataValue != null && (GenericResourceEntity.DataValue.HasChangesInTopology() || (GenericResourceEntity.DataValue.DependentResourceCollection.RemovedEntitiesTracker != null && GenericResourceEntity.DataValue.DependentResourceCollection.RemovedEntitiesTracker.Count > 0)));
        }

        private static void DoPreSaveTasksFor(DataWrapper<WorkspaceData> wsd)
        {
            if (wsd.DataValue != null)
            {
                var csp = wsd.DataValue.ViewModelInstance as IViewModel2ViewCommandSupport;
                if (csp != null)
                {
                    csp.DoPreSaveTasks();
                }
            }
        }



        ~SourceTextEditorViewModel()
        {
            Dispose(false);
        }

    }
}