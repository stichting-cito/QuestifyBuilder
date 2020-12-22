using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Configuration;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Interfaces.UI;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring.Validator;
using Questify.Builder.Logic.ResourceManager;
using Questify.Builder.Logic.Service.DTO;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.HelperClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UI.Wpf.Presentation.Types;
using SD.LLBLGen.Pro.ORMSupportClasses;
using Application = System.Windows.Application;
using Button = Questify.Builder.Logic.Service.Interfaces.UI.Button;
using MenuItem = Fluent.MenuItem;
using Point = System.Drawing.Point;
using Settings = Questify.Builder.UI.Wpf.Properties.Settings;
using Size = System.Drawing.Size;
using Questify.Builder.Logic.ItemProcessing;
using System.IO;
using Enums;
using Questify.Builder.Logic;
using Questify.Builder.Logic.Service.Logging;
using Questify.Builder.Security;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    internal class ItemEditorViewModel : EditorViewModelBase, IItemEditorViewModel
    {

        public ItemEditorViewModel()
        {
            Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.IsActive, true);

            _session = new SessionContextProvider(new SessionContext());
            TestSessionContext.ResourceNeeded += Handle_TestSessionContext_ResourceNeeded;

            GetStrings();

            InitDataWrappers();

            InitCommands();

            InitLazy();

            _messageBoxService = ViewModelRepository.Instance.Resolver.Container.GetExport<ICustomMessageBoxService>()?.Value;
            ItemEditorObjectFactory = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorObjectFactory>()?.Value;
            _inputBox = ViewModelRepository.Instance.Resolver.Container.GetExport<ILegacyInputBox>()?.Value;

            Left = UserSettings.ItemEditorPosition.X;
            Top = UserSettings.ItemEditorPosition.Y;
            Width = UserSettings.ItemEditorSize.Width;
            Height = UserSettings.ItemEditorSize.Height;
            WindowState = UserSettings.ItemEditorFullScreen ? WindowState.Maximized : WindowState.Normal;
            SizeChanged = new SimpleCommand<object, EventToCommandArgs>(e =>
            {
                var sizeArgs = e.EventArgs as SizeChangedEventArgs;
                if (sizeArgs != null)
                {
                    Width = (int)sizeArgs.NewSize.Width;
                    Height = (int)sizeArgs.NewSize.Height;
                }
            });

            _timerNecessaryEvil = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            _timerNecessaryEvil.Tick += TimerNecessaryEvil_Tick;
            _timerNecessaryEvil.Start();

            IsQATSaveAsVisible = UserSettings.IsQATSaveAsVisible;
            IsQATSaveAndCloseVisible = UserSettings.IsQATSaveAndCloseVisible;
        }


        public void EnableElementsOnCompletion()
        {
            _isChildViewLoadingCompleted = true;
        }

        public void ReloadData()
        {
            if (!ItemResourceEntity.DataValue.IsNew)
                TakeValuesFromFactory(ItemEditorObjectFactory.GetRequiredObjectsForItemWithId(ItemResourceEntity.DataValue.ResourceId));
        }

        public void ReloadDependentResources()
        {
            TakeDependentResourcesFromFactory(ItemEditorObjectFactory.GetRequiredObjectsForItemWithId(ItemResourceEntity.DataValue.ResourceId));
        }

        private void InitButtonStates()
        {
            if (!(CurrentEditor is IRichText))
                return;

            var editor = (IRichText)CurrentEditor;

            SetButtonChecked(editor);
            SetCurrentStyleChanged(editor);
            SetCurrentLanguageChanged(editor);

            CanSetFormatting.DataValue = editor.CanSetFormatting;

            SetButtonCheckedChanged(editor);
        }

        private void InitDataWrappers()
        {
            SelectedTab = new DataWrapper<int>(this, SelectedTabArgs) { DataValue = -1 };
            SelectedTab.PropertyChanged += HandleSelectedTabChanges;
            ItemId = new DataWrapper<Guid>(this, ItemIdArgs); ItemId.PropertyChanged += HandleItemIdPropertyChanged;
            RibbonTabToShowOverride = new DataWrapper<int>(this, RibbonTabToShowOverrideArgs);

            IsRibbonMinimized = new DataWrapper<bool>(this, IsRibbonMinimizedArgs);
            IsOlderItem = new DataWrapper<bool>(this, IsOlderItemArgs);
            SaveNeeded = new DataWrapper<bool>(this, SaveNeededArgs, CommandManager.InvalidateRequerySuggested);
            WindowTitle = new DataWrapper<string>(this, WindowTitleArgs) { DataValue = _msgTitle };
            HasError = new DataWrapper<bool>(this, HasErrorArgs);
            CanMoveBackInList = new DataWrapper<bool>(this, CanMoveBackArgs);
            CanMoveNextInList = new DataWrapper<bool>(this, CanMoveNextArgs);
            CanChangeCode = new DataWrapper<bool>(this, CanChangeCodeArgs);

            ItemResourceEntity = new DataWrapper<ItemResourceEntity>(this, ItemResourceEntityEventArgs);
            AssessmentItem = new DataWrapper<AssessmentItem>(this, AssessmentItemEventArgs);
            ResourceManager = new DataWrapper<ResourceManagerBase>(this, ResourceManagerEventArgs);
            ParameterSetCollection = new DataWrapper<ParameterSetCollection>(this, ParameterSetCollectionEventArgs);
            ContextIdentifier = new DataWrapper<int?>(this, ContextIdentifierEventArgs);
            AvailableStyles = new DataWrapper<IList<string>>(this, AvailableStylesArgs);
            AvailableLanguages = new DataWrapper<IList<string>>(this, AvailableLanguagesArgs);
            ActionEntity = new DataWrapper<ActionEntity>(this, ActionEntityEventArgs);

            IsWorking = new DataWrapper<bool>(this, IsWorkingArgs);

            AvailableInlineControls = new DataWrapper<List<MenuItemViewModel>>(this, AvailableInlineControlsArgs);
            AvailableElementsToReference = new DataWrapper<List<MenuItemViewModel>>(this, AvailableElementsToReferenceArgs);

            PresentationWorkspace = ItemEditorWorkspaceFactory.Create(this, PresentationArgs, ItemEditorWorkspaceFactory.CreatePresentation);
            MetadataWorkspace = ItemEditorWorkspaceFactory.Create(this, MetaDataArgs, ItemEditorWorkspaceFactory.CreateMetaData);
            ScoreWorkspace = ItemEditorWorkspaceFactory.Create(this, ScoreArgs, ItemEditorWorkspaceFactory.CreateScore);
            SourceWorkspace = ItemEditorWorkspaceFactory.Create(this, SourceArgs, ItemEditorWorkspaceFactory.CreateSource);

            HasSolutionDefined = new DataWrapper<bool?>(this, HasSolutionDefinedArgs);
            ShowingAddCI = new DataWrapper<bool>(this, ShowingAddCIArgs);
            ShowTextToSpeech = new DataWrapper<bool>(this, ShowTextToSpeechArgs);
            ShowTextToSpeechGroup = new DataWrapper<bool>(this, ShowTextToSpeechGroupArgs);
            CanSetFormatting = new DataWrapper<bool>(this, CanSetFormattingArgs);
            AvailableDefaultDurations = new DataWrapper<List<MenuItem>>(this, AvailableDefaultDurationsArgs);

            ShowingDebug = new DataWrapper<bool>(this, ShowingDebugArgs);
        }

        private void InitLazy()
        {
            _lazyCustomBankPropertyCollection = new Lazy<EntityCollection>(
                () => ItemEditorObjectFactory.GetCustomBankPropertiesForBranch(ItemResourceEntity.DataValue.BankId)
            );

            _lazyIsConceptDefinedOnBankBranch = new Lazy<bool>(
                () => _lazyCustomBankPropertyCollection.Value.Any(customBankPropertyEntity => customBankPropertyEntity.GetType() == typeof(ConceptStructureCustomBankPropertyEntity))
            );
            _lazyIsTreeDefinedOnBankBranch = new Lazy<bool>(
                () => _lazyCustomBankPropertyCollection.Value.Any(customBankPropertyEntity => customBankPropertyEntity.GetType() == typeof(TreeStructureCustomBankPropertyEntity))
            );
        }

        private void SetButtonChecked(IRichText editor)
        {
            IsBoldChecked = editor.IsButtonChecked(Button.BOLD);
            IsItalicChecked = editor.IsButtonChecked(Button.ITALIC);
            IsUnderlineChecked = editor.IsButtonChecked(Button.UNDERLINE);
            IsSuperScriptChecked = editor.IsButtonChecked(Button.SUPERSCRIPT);
            IsSubScriptChecked = editor.IsButtonChecked(Button.SUBSCRIPT);
            IsStrikeThroughChecked = editor.IsButtonChecked(Button.STRIKETHROUGH);
            IsAlignLeftChecked = editor.IsButtonChecked(Button.ALIGNLEFT);
            IsAlignMiddleChecked = editor.IsButtonChecked(Button.ALIGNMIDDLE);
            IsAlignRightChecked = editor.IsButtonChecked(Button.ALIGNRIGHT);

            IsMakeNumberedChecked = editor.IsButtonChecked(Button.MAKENUMBERED);
            IsMakeBulletedChecked = editor.IsButtonChecked(Button.MAKEBULLETED);
        }

        private void SetCurrentStyleChanged(IRichText editor)
        {
            _currentStyle = editor.CurrentStyle;
            editor.CurrentStyleChanged += (s, o) =>
            {
                _currentStyle = editor.CurrentStyle;
                NotifyPropertyChanged(CurrentStyleArgs);
            };
        }

        private void SetCurrentLanguageChanged(IRichText editor)
        {
            editor.CurrentLanguageChanged += (s, o) =>
            {
                _currentLanguage = editor.CurrentLanguage;
                NotifyPropertyChanged(CurrentLanguageArgs);
            };
        }

        private void SetButtonCheckedChanged(IRichText editor)
        {
            editor.IsButtonCheckedChanged += (s, o) =>
            {
                switch (o.Button)
                {
                    case Button.BOLD:
                        IsBoldChecked = o.IsChecked;
                        NotifyPropertyChanged(IsBoldCheckedArgs);
                        break;
                    case Button.ITALIC:
                        IsItalicChecked = o.IsChecked;
                        NotifyPropertyChanged(IsItalicCheckedArgs);
                        break;
                    case Button.UNDERLINE:
                        IsUnderlineChecked = o.IsChecked;
                        NotifyPropertyChanged(IsUnderlineCheckedArgs);
                        break;
                    case Button.SUPERSCRIPT:
                        IsSuperScriptChecked = o.IsChecked;
                        NotifyPropertyChanged(IsSuperScriptCheckedArgs);
                        break;
                    case Button.SUBSCRIPT:
                        IsSubScriptChecked = o.IsChecked;
                        NotifyPropertyChanged(IsSubScriptCheckedArgs);
                        break;
                    case Button.STRIKETHROUGH:
                        IsStrikeThroughChecked = o.IsChecked;
                        NotifyPropertyChanged(IsStrikeThroughCheckedArgs);
                        break;
                    case Button.ALIGNLEFT:
                        IsAlignLeftChecked = o.IsChecked;
                        NotifyPropertyChanged(IsAlignLeftCheckedArgs);
                        break;
                    case Button.ALIGNMIDDLE:
                        IsAlignMiddleChecked = o.IsChecked;
                        NotifyPropertyChanged(IsAlignMiddleCheckedArgs);
                        break;
                    case Button.ALIGNRIGHT:
                        IsAlignRightChecked = o.IsChecked;
                        NotifyPropertyChanged(IsAlignRightCheckedArgs);
                        break;
                    case Button.MAKENUMBERED:
                        IsMakeNumberedChecked = o.IsChecked;
                        NotifyPropertyChanged(IsMakeNumberedCheckedArgs);
                        break;
                    case Button.MAKEROMANNUMBERED:
                        IsMakeRomanNumberedChecked = o.IsChecked;
                        NotifyPropertyChanged(IsMakeRomanNumberedCheckedArgs);
                        break;
                    case Button.MAKEBULLETED:
                        IsMakeBulletedChecked = o.IsChecked;
                        NotifyPropertyChanged(IsMakeBulletedCheckedArgs);
                        break;
                    default:
                        NotifyPropertyChanged(o.Button.ToString());
                        break;
                }
            };
        }

        internal void HandleItemIdChanged()
        {

            if (ItemResourceEntity.DataValue != null && !_openedByMove && (ItemResourceEntity.DataValue.IsNew || NeedSave() && ContinueCloseAfterAskForSave() == CustomDialogResults.Yes))
                return;

            _openedByMove = false; IsLoading = true;
            _isChildViewLoadingCompleted = false;
            IsWorking.DataValue = true;

            var currentTab = SelectedTab.DataValue; SelectedTab.DataValue = -1;
            if (currentTab >= 1)
            {
                var presentationViewModel = PresentationWorkspace?.DataValue?.ViewModelInstance as PresentationViewModel;
                presentationViewModel?.Update();
            }
            if (currentTab != 2)
            {
                var scoringViewModel = ScoreWorkspace?.DataValue?.ViewModelInstance as ScoringHostViewModel;
                scoringViewModel?.Update();
            }

            Dispatcher.CurrentDispatcher.InvokeAsynchronouslyInBackground(() => DoActualLoadOnItemId(ItemId.DataValue, currentTab));
        }

        internal void DoActualLoadOnItemId(Guid id, int selectedTabIndex = -1)
        {
            try
            {
                CurrentItemClosing = false;
                _isChildViewLoadingCompleted = false;
                var data = ItemEditorObjectFactory.GetRequiredObjectsForItemWithId(id);

                if (ResourceManager.DataValue != null &&
                    ReferenceEquals(ResourceManager.DataValue, data.ResourceManagerBase))
                    return;

                if (PresentationWorkspace.DataValue.ViewModelInstance != null && selectedTabIndex == 0)
                {
                    ((PresentationViewModel)PresentationWorkspace.DataValue.ViewModelInstance).KillView();
                    ((PresentationViewModel)PresentationWorkspace.DataValue.ViewModelInstance).Dispose();
                    GC.Collect();
                    PresentationWorkspace = ItemEditorWorkspaceFactory.Create(this, PresentationArgs, ItemEditorWorkspaceFactory.CreatePresentation);
                    NotifyPropertyChanged(PresentationArgs);
                }

                if (ScoreWorkspace.DataValue.ViewModelInstance != null && selectedTabIndex == 2)
                {
                    ReloadScoring();
                }

                TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(ResourceManager.DataValue);
                TakeValuesFromFactory(data);
                UpdateWindowTitle();
                ContextIdentifier.DataValue = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(ResourceManager.DataValue);

                if (Updated == null)
                {
                    AddAttributeReferences();
                }
                else
                {
                    Updated(this, new StringEventArgs("DoUpdate"));
                }

                LogHelper.TrackEvent(EventsToTrack.ItemLoaded, GetItemTrackingProperties());

                SetStateVariables();
                SelectTab(selectedTabIndex);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex.Message, _msgError);
                OnErrorNullDataObjects();
            }
            finally
            {
                IsLoading = false;
                IsWorking.DataValue = false;
                SaveNeeded.DataValue = NeedSave();
            }
        }

        private void SelectTab(int selectedTabIndex)
        {
            if (selectedTabIndex >= 0)
            {
                SelectedTab.DataValue = selectedTabIndex;
            }
            else if (_prevTab >= 0)
            {
                SelectedTab.DataValue = _prevTab;
            }
            else
            {
                SelectedTab.DataValue = 0;
            }
        }

        private void TakeValuesFromFactory(ItemEditorObjectFactoryResult data)
        {
            ItemResourceEntity.DataValue = data.ItemResourceEntity;
            GetCustomPropertiesValueCollectionRemovedEntitiesTracker();
            AssessmentItem.DataValue = data.AssessmentItem;
            IsOlderItem.DataValue = data.IsTransformedFromV1ToV2;
            ParameterSetCollection.DataValue = data.ParameterSetCollection;
            ResourceManager.DataValue = data.ResourceManagerBase;
            ActionEntity.DataValue = data.ActionEntity;

            try
            {
                using (var resourceManager = new DataBaseResourceManager(ItemResourceEntity.DataValue.BankId))
                {
                    var assesmentRenderer = new AssessmentItemRenderer(data.AssessmentItem, resourceManager);
                    Targets = assesmentRenderer.GetAvailableTargets();
                    NotifyPropertyChanged("Targets");
                }
            }
            catch
            {
            }

            Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.Bank, ItemResourceEntity.DataValue.BankId);

            var id = ItemResourceEntity.DataValue.GetIdFromDependencyByName(AssessmentItem.DataValue.LayoutTemplateSourceName);
            Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.ItemLayoutTemplate, id);
        }

        private void TakeDependentResourcesFromFactory(ItemEditorObjectFactoryResult data)
        {
            ItemResourceEntity.DataValue.DependentResourceCollection.Clear();
            ItemResourceEntity.DataValue.DependentResourceCollection.AddRange(data.ItemResourceEntity.DependentResourceCollection.ToList());
        }

        private IEntityCollection2 GetCustomPropertiesValueCollectionRemovedEntitiesTracker()
        {
            return ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.RemovedEntitiesTracker ??
                   (ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.RemovedEntitiesTracker =
                       new EntityCollection());
        }

        internal void CreateNewItem(Guid itemLayoutTemplateId, int bankId, bool canMoveBackward, bool canMoveForward)
        {
            CanMoveBackInList.DataValue = canMoveBackward;
            CanMoveNextInList.DataValue = canMoveForward;
            CanChangeCode.DataValue = true;

            if (ItemResourceEntity.DataValue != null && NeedSave() && ContinueCloseAfterAskForSave() == CustomDialogResults.Cancel)
                return;

            IsLoading = true;
            _isChildViewLoadingCompleted = false;
            IsWorking.DataValue = true;

            SelectedTab.DataValue = 1; ResourceManager.DataValue = new DataBaseResourceManager(bankId);
            Dispatcher.CurrentDispatcher.InvokeAsynchronouslyInBackground(() =>
            {
                ContextIdentifier.DataValue = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(ResourceManager.DataValue);
                var data = ItemEditorObjectFactory.GetObjectsForNewItem(itemLayoutTemplateId, bankId);

                TakeValuesFromFactory(data);

                SetStateVariables();

                UpdateWindowTitle();
                ItemId.DataValue = ItemResourceEntity.DataValue.ResourceId;
                OnItemIdChanged(Guid.Empty, ItemId.DataValue);

                Updated?.Invoke(this, new StringEventArgs("DoUpdate"));
                IsLoading = false;
                IsWorking.DataValue = false;
            });
        }

        private void OnItemIdChanged(Guid previousId, Guid newId)
        {
            NewItemIdAssigned?.Invoke(this, new IdChangedEventArgs(previousId, newId));
        }

        private void SetStateVariables()
        {
            _hash = GetHash();
            HasError.DataValue = false;
        }

        private void GetStrings()
        {
            _msgTitle = GetUiString("ItemEditor.Title", true);
            _msgError = GetUiString("Dialog.ErrorCaption", true);
            _msgPendingRequiredFields = GetUiString("PendingRequiredFields");
            _msgPleaseNewItemCode = GetUiString("PleaseNewItemCode");
            _msgPleaseNewItemCodeDialogTitle = GetUiString("PleaseNewItemCodeDialogTitle");
            _msgDefaultPrefixNewCode = GetUiString("DefaultPrefixNewCode");
            _msgNameNotUniqueInBank = GetUiString("NameNotUniqueInBank");
            _msgScoringChangedSaveYesNo = GetUiString("ScoringChangedSaveYesNo");
            _msgConceptEncodingOutOfSync = GetUiString("ConceptEncodingOutOfSync");
        }

        private static string GetUiString(string key, bool global = false)
        {
            const string prefix = "ItemEditor.ItemEditorViewModel.";
            return (string)Application.Current.FindResource($"{(global ? string.Empty : prefix)}{key}");
        }

        private void OnErrorNullDataObjects()
        {
            HasError.DataValue = true;
            ItemResourceEntity.DataValue = null;
            AssessmentItem.DataValue = null;
            ResourceManager.DataValue = null;
            ParameterSetCollection.DataValue = null;
            ActionEntity.DataValue = null;

            Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.Bank, 0);
        }
        private void DetermineItemHasKey()
        {
            if (AssessmentItem.DataValue?.Solution == null ||
                ItemResourceEntity.DataValue?.ItemTypeFromItemLayoutTemplate == ItemTypeEnum.Informational)
            {
                HasSolutionDefined.DataValue = null;
                return;
            }

            var hasKeyDefined = HasKeyDefined();

            if (!hasKeyDefined && AssessmentItem.DataValue.Solution.AspectReferenceSetCollection != null)
            {
                hasKeyDefined = AssessmentItem.DataValue.Solution.AspectReferenceSetCollection.Any(ars => ars.Items.Any());
            }

            HasSolutionDefined.DataValue = hasKeyDefined;
        }

        private bool HasKeyDefined()
        {
            var hasKeyDefined = false;
            foreach (var finding in AssessmentItem.DataValue.Solution.Findings)
            {
                hasKeyDefined = finding.Facts.Any(findingFact => findingFact.Values.OfType<KeyValue>()
                                    .Any(value => value.Values.Any(val => val != null && val.ToString() != string.Empty))) ||
                                finding.KeyFactsets.Any(factset => factset.Facts.Any(fact =>
                                    fact.Values.OfType<KeyValue>().Any(value =>
                                        value.Values.Any(val => val != null && val.ToString() != string.Empty))));

                if (hasKeyDefined)
                    break;
            }
            return hasKeyDefined;
        }


        private static readonly PropertyChangedEventArgs SelectedTabArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.SelectedTab);

        private static readonly PropertyChangedEventArgs ItemIdArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ItemId);

        private static readonly PropertyChangedEventArgs RibbonTabToShowOverrideArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.RibbonTabToShowOverride);

        private static readonly PropertyChangedEventArgs IsRibbonMinimizedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsRibbonMinimized);

        private static readonly PropertyChangedEventArgs IsOlderItemArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsOlderItem);

        private static readonly PropertyChangedEventArgs SaveNeededArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.SaveNeeded);

        private static readonly PropertyChangedEventArgs WindowTitleArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.WindowTitle);

        private static readonly PropertyChangedEventArgs PresentationArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.PresentationWorkspace);

        private static readonly PropertyChangedEventArgs MetaDataArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.MetadataWorkspace);

        private static readonly PropertyChangedEventArgs ScoreArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ScoreWorkspace);

        private static readonly PropertyChangedEventArgs SourceArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.SourceWorkspace);

        private static readonly PropertyChangedEventArgs HasErrorArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.HasError);

        private static readonly PropertyChangedEventArgs CanMoveBackArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.CanMoveBackInList);

        private static readonly PropertyChangedEventArgs CanMoveNextArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.CanMoveNextInList);

        private static readonly PropertyChangedEventArgs CanChangeCodeArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.CanChangeCode);

        private static readonly PropertyChangedEventArgs ItemResourceEntityEventArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ItemResourceEntity);
        private static readonly PropertyChangedEventArgs ActionEntityEventArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ActionEntity);
        private static readonly PropertyChangedEventArgs AssessmentItemEventArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.AssessmentItem);
        private static readonly PropertyChangedEventArgs ResourceManagerEventArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ResourceManager);
        private static readonly PropertyChangedEventArgs ParameterSetCollectionEventArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ParameterSetCollection);
        private static readonly PropertyChangedEventArgs ContextIdentifierEventArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ContextIdentifier);
        private static readonly PropertyChangedEventArgs AvailableStylesArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.AvailableStyles);
        private static readonly PropertyChangedEventArgs AvailableLanguagesArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.AvailableLanguages);

        private static readonly PropertyChangedEventArgs IsWorkingArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsWorking);
        private static readonly PropertyChangedEventArgs AvailableInlineControlsArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.AvailableInlineControls);
        private static readonly PropertyChangedEventArgs AvailableElementsToReferenceArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.AvailableElementsToReference);

        private static readonly PropertyChangedEventArgs ShowingDebugArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ShowingDebug);

        private static readonly PropertyChangedEventArgs HasSolutionDefinedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.HasSolutionDefined);
        private static readonly PropertyChangedEventArgs ShowingAddCIArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ShowingAddCI);
        private static readonly PropertyChangedEventArgs ShowTextToSpeechArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ShowTextToSpeech);
        private static readonly PropertyChangedEventArgs ShowTextToSpeechGroupArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.ShowTextToSpeechGroup);
        private static readonly PropertyChangedEventArgs CanSetFormattingArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.CanSetFormatting);
        private static readonly PropertyChangedEventArgs AvailableDefaultDurationsArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.AvailableDefaultDurations);

        private static readonly PropertyChangedEventArgs IsBoldCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsBoldChecked);
        private static readonly PropertyChangedEventArgs IsItalicCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsItalicChecked);
        private static readonly PropertyChangedEventArgs IsUnderlineCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsUnderlineChecked);
        private static readonly PropertyChangedEventArgs IsSuperScriptCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsSuperScriptChecked);
        private static readonly PropertyChangedEventArgs IsSubScriptCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsSubScriptChecked);
        private static readonly PropertyChangedEventArgs IsStrikeThroughCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsStrikeThroughChecked);
        private static readonly PropertyChangedEventArgs IsAlignLeftCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsAlignLeftChecked);
        private static readonly PropertyChangedEventArgs IsAlignMiddleCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsAlignMiddleChecked);
        private static readonly PropertyChangedEventArgs IsAlignRightCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsAlignRightChecked);

        private static readonly PropertyChangedEventArgs IsMakeNumberedCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsMakeNumberedChecked);
        private static readonly PropertyChangedEventArgs IsMakeRomanNumberedCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsMakeRomanNumberedChecked);
        private static readonly PropertyChangedEventArgs IsMakeBulletedCheckedArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.IsMakeBulletedChecked);
        private static readonly PropertyChangedEventArgs CurrentStyleArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.CurrentStyle);
        private static readonly PropertyChangedEventArgs CurrentLanguageArgs = ObservableHelper.CreateArgs<ItemEditorViewModel>(x => x.CurrentLanguage);


        private bool _isChildViewLoadingCompleted;
        private readonly ICustomMessageBoxService _messageBoxService;
        private bool _openedByMove;
        private readonly ILegacyInputBox _inputBox;

        private DispatcherTimer _timerNecessaryEvil;


        private Lazy<EntityCollection> _lazyCustomBankPropertyCollection;

        private Lazy<bool> _lazyIsConceptDefinedOnBankBranch;
        private Lazy<bool> _lazyIsTreeDefinedOnBankBranch;

        private string _hash;
        private int _prevTab = -1;
        private string _msgTitle;

        private string _msgError;
        private string _msgPendingRequiredFields;
        private string _msgPleaseNewItemCode;
        private string _msgPleaseNewItemCodeDialogTitle;
        private string _msgDefaultPrefixNewCode;
        private string _msgNameNotUniqueInBank;
        private string _msgScoringChangedSaveYesNo;
        private string _msgConceptEncodingOutOfSync;

        private readonly SessionContextProvider _session;


        private int _ribbonTabToShowOverride;



        public event EventHandler<StringEventArgs> Updated;
        public event EventHandler<IdChangedEventArgs> NewItemIdAssigned;



        public DataWrapper<Guid> ItemId { get; private set; }
        public DataWrapper<bool> IsOlderItem { get; private set; }
        public DataWrapper<int> RibbonTabToShowOverride { get; private set; }

        private void SetRibbonTabToShowOverride()
        {
            if (SelectedTab.DataValue == 2)
            {
                _ribbonTabToShowOverride = SelectedTab.DataValue;

                if (CurrentEditor is IRichText)
                {
                    _ribbonTabToShowOverride = 0;
                }
            }
            else
            {
                _ribbonTabToShowOverride = 0;
            }

            RibbonTabToShowOverride.DataValue = _ribbonTabToShowOverride;
        }

        public DataWrapper<bool> CanMoveBackInList { get; private set; }
        public DataWrapper<bool> CanMoveNextInList { get; private set; }
        public DataWrapper<bool> CanChangeCode { get; private set; }

        public DataWrapper<ItemResourceEntity> ItemResourceEntity { get; private set; }
        public DataWrapper<AssessmentItem> AssessmentItem { get; private set; }
        public DataWrapper<ParameterSetCollection> ParameterSetCollection { get; private set; }
        public DataWrapper<ActionEntity> ActionEntity { get; private set; }
        public DataWrapper<List<MenuItemViewModel>> AvailableInlineControls { get; private set; }
        public DataWrapper<List<MenuItemViewModel>> AvailableElementsToReference { get; private set; }

        public DataWrapper<WorkspaceData> PresentationWorkspace { get; private set; }

        public DataWrapper<WorkspaceData> MetadataWorkspace { get; private set; }
        public DataWrapper<WorkspaceData> ScoreWorkspace { get; private set; }
        public DataWrapper<WorkspaceData> SourceWorkspace { get; private set; }

        public DataWrapper<bool> ShowingDebug { get; private set; }

        public DataWrapper<bool?> HasSolutionDefined { get; private set; }
        public DataWrapper<bool> ShowingAddCI { get; private set; }
        public DataWrapper<bool> CanSetFormatting { get; private set; }

        public IEntityCollection2 CustomPropertiesValueCollectionRemovedEntitiesTracker => GetCustomPropertiesValueCollectionRemovedEntitiesTracker();




        public WindowState WindowState { get; set; }

        private int Width { get; set; }

        private int Height { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public bool IsQATSaveAsVisible { get; set; }

        public bool IsQATSaveAndCloseVisible { get; set; }

        public SimpleCommand<object, EventToCommandArgs> SizeChanged { get; }

        public string ItemName
        {
            get
            {
                var name = string.IsNullOrEmpty(ItemResourceEntity?.DataValue?.Name) ? GetUiString("UnSavedItem") : ItemResourceEntity.DataValue.Name;

                return name;
            }
        }

        public bool IsLoading { get; private set; } = true;

        public bool CurrentItemClosing { get; private set; }

        public IItemEditorObjectFactory ItemEditorObjectFactory { get; }

        public IEnumerable<EntityBase2> CustomBankProperties => _lazyCustomBankPropertyCollection.Value;

        public bool CustomBankPropertiesRetrieved => _lazyCustomBankPropertyCollection.IsValueCreated;

        public bool IsConceptDefinedOnBankBranch => _lazyIsConceptDefinedOnBankBranch.Value;

        public bool IsTreeDefinedOnBankBranch => _lazyIsTreeDefinedOnBankBranch.Value;




        public SimpleCommand<object, object> SaveAndMajorVersion { get; private set; }

        public SimpleCommand<object, object> MoveNextInList { get; private set; }
        public SimpleCommand<object, object> MoveBackInList { get; private set; }

        public SimpleCommand<object, EventToCommandArgs> WindowActivated { get; private set; }
        public SimpleCommand<object, EventToCommandArgs> WindowClosed { get; private set; }

        public SimpleCommand<object, object> ToggleShowDebug { get; private set; }
        public SimpleCommand<object, string> ShowTargetXml { get; private set; }

        public bool IsBoldChecked { get; set; }
        public bool IsItalicChecked { get; set; }
        public bool IsUnderlineChecked { get; set; }
        public bool IsSuperScriptChecked { get; set; }
        public bool IsSubScriptChecked { get; set; }
        public bool IsStrikeThroughChecked { get; set; }
        public bool IsAlignLeftChecked { get; set; }
        public bool IsAlignMiddleChecked { get; set; }
        public bool IsAlignRightChecked { get; set; }
        public bool IsMakeNumberedChecked { get; set; }
        public bool IsMakeRomanNumberedChecked { get; set; }
        public bool IsMakeBulletedChecked { get; set; }
        public IEnumerable<string> Targets { get; private set; }

        private string _currentStyle;
        private string _currentLanguage;
        private bool _canAccordingState;

        public string CurrentStyle
        {
            get
            {
                return _currentStyle;
            }
            set
            {
                _currentStyle = value;
                SetStyle(value);
            }
        }

        public string CurrentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
                SetLanguage(value);
            }
        }

        public SimpleCommand<object, object> AddCI { get; private set; }
        public SimpleCommand<object, object> AddPopup { get; private set; }



        private void InitCommands()
        {
            InitSaveCommands();
            InitMoveCommands();
            InitWindowCommands();

            ToggleRibbonMinimize = new SimpleCommand<object, object>(o => IsRibbonMinimized.DataValue = !IsRibbonMinimized.DataValue);
            InitCopyPasteCommands();
            InitMediaCommands();
            InitTextFormatCommands();
            InitOtherCommands();
            InitTextToSpeechCommands();

            ToggleShowDebug = new SimpleCommand<object, object>(a => ShowingDebug.DataValue = !ShowingDebug.DataValue);
            ShowTargetXml = new SimpleCommand<object, string>(s => ShowCompiledResultInDefaultApp(AssessmentItem.DataValue, ItemResourceEntity.DataValue.BankId, s));
        }

        private void InitSaveCommands()
        {
            Save = new SimpleCommand<object, object>(o => CanExecuteSave(), o => DoSave());
            SaveAs = new SimpleCommand<object, object>(o => DoSaveAs());
            SaveAndClose = new SimpleCommand<object, object>(o =>
            {
                DoSave();
                RaiseCloseRequest(true);
            });
            SaveAndMajorVersion = new SimpleCommand<object, object>(o => CanExecuteSaveAndMajorVersion(), o => ExecuteSaveAndMajorVersion());
        }

        private void InitMoveCommands()
        {
            MoveNextInList = new SimpleCommand<object, object>(CanExecuteMoveNextInList, ExecuteMoveNextInList);
            MoveBackInList = new SimpleCommand<object, object>(CanExecuteMoveBackInList, ExecuteMoveBackInList);
        }

        private void InitWindowCommands()
        {
            WindowClosing = new SimpleCommand<object, EventToCommandArgs>(ExecuteWindowClosing);
            WindowActivated = new SimpleCommand<object, EventToCommandArgs>(ExecuteWindowActivated);
            WindowClosed = new SimpleCommand<object, EventToCommandArgs>(o => OnClosed());
        }

        protected override void InitMediaCommands()
        {
            base.InitMediaCommands();
            AddCI = Command<IRichText>(can => can.CanAddCustomInteraction, cmd => cmd.AddCI());
        }

        private void InitOtherCommands()
        {
            AddTable = Command<IRichText>(can => can.CanAddTable, cmd => cmd.AddTable());
            AddFormula = Command<IRichText>(can => can.CanAddFormula, cmd => cmd.AddFormula());
            OpenSymbolDialog = new SimpleCommand<object, object>(o => CanAddSpecialSymbol(), o => DoOpenSymbolDialog());

            AddInlineControl = Command<IRichText>(can => can.CanAddInlineControl, cmd => cmd.AddInlineControl());
            AddReference = Command<IRichText>(can => can.CanAddReference, cmd => cmd.AddReference(string.Empty));
            AddPopup = Command<IRichText>(can => can.CanAddPopup, cmd => cmd.AddPopup());

            SetStyleCommand = new SimpleCommand<object, string>(SetStyle);
            SetLanguageCommand = new SimpleCommand<object, string>(SetLanguage);
        }

        private bool CanExecuteSaveAndMajorVersion()
        {
            return ItemResourceEntity.DataValue != null && !ItemResourceEntity.DataValue.IsNew;
        }

        private void ExecuteSaveAndMajorVersion()
        {
            if (IncrementMajorVersion())
                DoSave();
        }

        private bool CanExecuteSave()
        {
            return SaveNeeded.DataValue;
        }

        private void ExecuteMoveNextInList(object o)
        {
            CurrentItemClosing = CloseCurrentItem();
            if (CurrentItemClosing && _isChildViewLoadingCompleted)
            {
                _openedByMove = true;
                Mediator.Instance.NotifyColleagues(Constants.MoveNextItemMessage, new EventArgs<Guid>(ItemId.DataValue));
            }
        }

        private bool CanExecuteMoveNextInList(object o)
        {
            return CanMoveNextInList.DataValue && _isChildViewLoadingCompleted;
        }

        private void ExecuteMoveBackInList(object o)
        {
            CurrentItemClosing = CloseCurrentItem();
            if (CurrentItemClosing && _isChildViewLoadingCompleted)
            {
                _openedByMove = true;
                Mediator.Instance.NotifyColleagues(Constants.MovePreviousItemMessage, new EventArgs<Guid>(ItemId.DataValue));
            }
        }

        private bool CanExecuteMoveBackInList(object o)
        {
            return CanMoveBackInList.DataValue && _isChildViewLoadingCompleted;
        }

        private void ShowCompiledResultInDefaultApp(AssessmentItem assessmentItem, int source, string targetNameOfInterest)
        {
            using (var resourceManager = new DataBaseResourceManager(source))
            {
                var assesmentRenderer = new AssessmentItemRenderer(assessmentItem, resourceManager);
                if ((assesmentRenderer.GetAvailableTargets().Contains(targetNameOfInterest)))
                {
                    var result = assesmentRenderer.GetXmlData(targetNameOfInterest);
                    string fileName = Path.GetTempFileName().Replace(".tmp", ".xml");
                    result.Save(fileName);
                    Process.Start(fileName);
                }
            }
        }

        private void OnClosed()
        {
            Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.Bank, 0);
            Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.Title, string.Empty);
            Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.IsActive, false);
            Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.ItemLayoutTemplate, Guid.Empty);

            var saveUserSettings = false;
            if (WindowState == WindowState.Maximized)
            {
                if (!UserSettings.ItemEditorFullScreen)
                {
                    UserSettings.ItemEditorFullScreen = true;
                    saveUserSettings = true;
                }
            }
            else
            {
                if (UserSettings.ItemEditorFullScreen)
                {
                    UserSettings.ItemEditorFullScreen = false;
                    saveUserSettings = true;
                }
                var editorSize = new Size(Width, Height);
                if (UserSettings.ItemEditorSize != editorSize)
                {
                    UserSettings.ItemEditorSize = editorSize;
                    saveUserSettings = true;
                }
                var editorPosition = new Point((int)Left, (int)Top);
                if (UserSettings.ItemEditorPosition != editorPosition)
                {
                    UserSettings.ItemEditorPosition = editorPosition;
                    saveUserSettings = true;
                }
            }

            if (UserSettings.IsQATSaveAsVisible != IsQATSaveAsVisible)
            {
                UserSettings.IsQATSaveAsVisible = IsQATSaveAsVisible;
                saveUserSettings = true;
            }
            if (UserSettings.IsQATSaveAndCloseVisible != IsQATSaveAndCloseVisible)
            {
                UserSettings.IsQATSaveAndCloseVisible = IsQATSaveAndCloseVisible;
                saveUserSettings = true;
            }

            if (saveUserSettings)
            {
            }
            Settings.Default.Save();

            Dispose();
        }

        public void SetSymbolDialog(IAddSymbolDialog addSymbolDialog)
        {
            AddSymbolDialog = addSymbolDialog;
        }


        public void EditorChange(object editor)
        {
            if (editor != null || CurrentEditor != null)
            {
                var oldEditor = CurrentEditor as IRichText;
                CurrentEditor = editor;

                SetAvailableStyles(CurrentEditor as IRichText);
                SetAvailableInline(CurrentEditor as IInline);
                SetAvailableElementsToReference(CurrentEditor as IRichText);
                SetAvailableLanguages(CurrentEditor as IRichText);
                SetAddCIVisibility(CurrentEditor as IRichText);
                SetTextToSpeechOptionsVisibility(CurrentEditor as IRichText);
                SetTextToSpeechOptionsEnabled(CurrentEditor as IRichText);

                if (CurrentEditor is IRichText)
                {
                    (CurrentEditor as IRichText).SelectionChanged += OnEditorSelectionChanged;
                    (CurrentEditor as IRichText).SetFocusVisibility(true);
                }

                if (oldEditor != null)
                {
                    oldEditor.SelectionChanged -= OnEditorSelectionChanged;
                    oldEditor.ResetCurrentSelection();
                    oldEditor.SetFocusVisibility(false);
                }

                SetRibbonTabToShowOverride();

                CommandManager.InvalidateRequerySuggested();
                InitButtonStates();
            }
        }

        private void OnEditorSelectionChanged(object sender, EventArgs e)
        {
            if (CurrentEditor is IRichText)
                CanSetFormatting.DataValue = (CurrentEditor as IRichText).CanSetFormatting;
        }

        private void SetAvailableElementsToReference(IRichText richTextEditor)
        {
            if (ParameterSetCollection.DataValue == null)
                return;
            var references = ParameterSetCollection.DataValue.GetReferencesFromResourceParameters();
            var menuItemViewModels = new List<MenuItemViewModel>();

            foreach (var reference in references)
            {
                var menuItemViewModel = new MenuItemViewModel
                {
                    Title = reference.Description,
                    Command = new SimpleCommand<object, object>(o => richTextEditor.AddReference(reference.ID))
                };
                menuItemViewModels.Add(menuItemViewModel);
            }

            AvailableElementsToReference.DataValue = menuItemViewModels;
        }

        private void SetAddCIVisibility(IRichText richTextEditor)
        {
            ShowingAddCI.DataValue = richTextEditor?.ShowAddCustomInteraction ?? false;
        }

        private void SetAvailableInline(IInline inline)
        {
            var newLst = new List<MenuItemViewModel>();
            if (inline != null)
                foreach (var ic in inline.InlineControls)
                {
                    var add = new MenuItemViewModel();
                    var inlineTmpl = inline.GetInlineTemplate(ic);
                    add.Tag = inlineTmpl;
                    add.Title = ic;
                    add.Icon = inline.InlineIcon(ic);
                    add.Command = new SimpleCommand<object, object>(o => inline.CreateInline(ic));
                    newLst.Add(add);
                }

            AvailableInlineControls.DataValue = newLst;
        }



        private string GetHash()
        {
            if (AssessmentItem.DataValue == null)
                return null;

            var buffer = AssessmentItem.DataValue.GetMD5Hash();
            var hash = Convert.ToBase64String(buffer);
            Array.Resize(ref buffer, 0);
            return hash;
        }

        private void TimerNecessaryEvil_Tick(object sender, EventArgs e)
        {
            DetermineItemHasKey();
            var alreadyDirty = SaveNeeded.DataValue;
            if (alreadyDirty)
                return;

            SaveNeeded.DataValue = NeedSave();
        }

        private bool IncrementMajorVersion()
        {
            var service = ViewModelRepository.Instance.Resolver.Container.GetExport<IMajorVersionDialogService>();
            if (service != null)
            {
                var majorVersionDialogService = service.Value;
                return majorVersionDialogService.Show(ItemResourceEntity.DataValue);
            }
            return false;
        }

        private void AddAttributeReferences()
        {
            if (ParameterSetCollection.DataValue != null)
            {
                var scoreParameters = ParameterSetCollection.DataValue.DeepFetchInlineScoringParameters();
                foreach (var scorePrm in scoreParameters)
                    scorePrm.AddAttributeReferenceDrivenChangeHandlers(ParameterSetCollection.DataValue);
            }
        }

        public void DoSave()
        {
            if (ItemResourceEntity.DataValue == null)
            {
                return;
            }

            IsWorking.DataValue = true;
            var isNew = ItemResourceEntity.DataValue.IsNew;
            PreSaveNecessaryEvil();
            if (!NeedSave())
            {
                SaveNeeded.DataValue = false;
                IsWorking.DataValue = false;
                return;
            }

            AssessmentItem.DataValue.Parameters.ValidateHierarchical();
            AssessmentItem.DataValue.ValidateAllProperties();
            string validationErrorMessage;
            var canSave = CanSave(GetHash() != _hash, true, out validationErrorMessage);

            if (canSave)
            {
                canSave = ConfirmSave(true);
            }

            if (!canSave)
            {
                GetAndShowErrorMessage(validationErrorMessage);
                IsWorking.DataValue = false;
                return;
            }
            SaveItem(isNew);
            SaveNeeded.DataValue = false;
        }

        private bool ConfirmSave(bool canSave)
        {
            var scoreParameters = ParameterSetCollection.DataValue.DeepFetchInlineScoringParameters();
            var changedSolution = CheckForChangedSolution(scoreParameters);

            if (!changedSolution || ItemResourceEntity.DataValue.IsNew)
            {
                return canSave;
            }

            var dialogResult = _messageBoxService.ShowYesNo(_msgScoringChangedSaveYesNo, CustomDialogIcons.Warning);

            if (dialogResult == CustomDialogResults.No)
            {
                return false;
            }
            return canSave;
        }

        private void SaveItem(bool isNew)
        {
            ItemResourceEntity.DataValue.Name = AssessmentItem.DataValue.Identifier.Trim(); ItemResourceEntity.DataValue.Title = AssessmentItem.DataValue.Title.Trim(); AssessmentItem.DataValue.SetScorePropertiesForItem(ItemResourceEntity.DataValue);
            ItemResourceEntity.DataValue.SetAssessmentItem(AssessmentItem.DataValue);
            if (ItemResourceEntity.DataValue.RequiresMajorVersionIncrement() && !IncrementMajorVersion())
            {
                IsWorking.DataValue = false;
                return;
            }

            UpdateDirtyCustomBankPropertyValues();

            IsWorking.DataValue = true;

            var originalState = GetItemResourceState();
            ItemResourceEntity.DataValue.UpdateDependencies();
            var errorMessage = ItemEditorObjectFactory.UpdateItemResource(ItemResourceEntity.DataValue);
            PostSaveNecessaryEvil();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _messageBoxService.ShowError(errorMessage, _msgError);
                return;
            }
            var properties = GetItemTrackingProperties();
            LogHelper.TrackEvent(isNew ? EventsToTrack.ItemCreated : EventsToTrack.ItemModified, properties);

            if (isNew)
            {
                AssessmentItem.DataValue.ItemId = ItemResourceEntity.DataValue.ItemId;
            }

            UpdateWindowTitle();

            _hash = GetHash();

            if (StateHasChanged(originalState))
            {
                LoadNewStateAction();
            }

            IsWorking.DataValue = false;

            Mediator.Instance.NotifyColleaguesAsync(
    isNew ? Constants.RefreshGridAndSelectResource : Constants.UpdateGridAndSelectResource,
    new EventArgs<IPropertyEntity>(ItemResourceEntity.DataValue));
        }

        private Dictionary<string, string> GetItemTrackingProperties()
        {
            return new Dictionary<string, string>()
            {
                { "BankId", ItemResourceEntity.DataValue?.BankId.ToString() },
                { "ItemCode", ItemResourceEntity.DataValue?.Name },
                { "ItemType", ItemResourceEntity.DataValue?.ItemTypeFromItemLayoutTemplate.ToString() },
                { "ItemLayoutTemplate", ItemResourceEntity.DataValue?.ItemLayoutTemplateUsedName }
            };
        }

        private bool IsValid()
        {
            string validationErrorMessage;
            return CanSave(GetHash() != _hash, true, out validationErrorMessage);
        }

        private bool CheckForChangedSolution(ICollection<ScoringParameter> scoringParameters)
        {
            if (AssessmentItem.DataValue == null)
                return false;

            var h1 = Convert.ToBase64String(AssessmentItem.DataValue.GetMD5Hash());
            if (scoringParameters.Count > 0)
            {
                AssessmentItem.DataValue.Solution
    .FixRemovedScoringParameters(scoringParameters);
            }

            var h2 = Convert.ToBase64String(AssessmentItem.DataValue.GetMD5Hash());
            return h1 != h2;
        }

        private int GetItemResourceState()
        {
            var stateField = ItemResourceEntity.DataValue.Fields["StateId"];
            var state = 0;

            if (stateField != null && stateField.DbValue != null)
                state = (int)stateField.DbValue;

            return state;
        }

        private void LoadNewStateAction()
        {
            var data = ItemEditorObjectFactory.GetRequiredObjectsForItemWithId(ItemResourceEntity.DataValue.ResourceId);
            ActionEntity.DataValue = data.ActionEntity;
        }

        private bool StateHasChanged(int originalState)
        {
            var stateIdField = ItemResourceEntity.DataValue.Fields["StateId"];

            if (stateIdField?.CurrentValue == null)
                return false;

            var newState = (int)stateIdField.CurrentValue;

            return originalState != newState;
        }

        private bool CanSave(bool itemResourceBinaryDataIsDirty, bool shouldCheckState, out string errorMessage)
        {
            errorMessage = string.Empty;
            var errorMessages = new List<string>();

            if (HasError.DataValue)
                return false;

            if (!SolutionIsValid(AssessmentItem.DataValue))
                return false;

            AssessmentItem.DataValue.ValidateAllProperties();
            errorMessage = ((IDataErrorInfo)AssessmentItem.DataValue).Error;
            var missingResourcesMessage = string.Empty;
            var doesNotHaveMissingResources = AssessmentItem.DataValue.ValidateResources(ItemResourceEntity.DataValue.BankId, ref missingResourcesMessage);

            if (!doesNotHaveMissingResources)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                    errorMessage += Environment.NewLine;
                errorMessage = string.Concat(errorMessage, missingResourcesMessage);
            }
            var canAccordingAssementItem = errorMessage == string.Empty;
            var canAccordingParamSet = AssessmentItem.DataValue.Parameters.ValidateHierarchical();

            _canAccordingState = !shouldCheckState || StateAllowsSave(itemResourceBinaryDataIsDirty);

            if (!string.IsNullOrEmpty(errorMessage))
                errorMessages.Add(errorMessage);

            errorMessage = string.Empty;
            var conceptScoringIsValid = ConceptEncodingIsValid(AssessmentItem.DataValue, ref errorMessage);
            if (!conceptScoringIsValid && !string.IsNullOrEmpty(errorMessage))
                errorMessages.Add(errorMessage);

            if (errorMessages.Any())
                errorMessage = string.Join("\r\n\r\n", errorMessages);

            return canAccordingAssementItem && canAccordingParamSet && _canAccordingState && doesNotHaveMissingResources && conceptScoringIsValid;
        }

        private static bool SolutionIsValid(AssessmentItem item)
        {
            var validator = new ScoringValidator();
            Exception exception = null;

            try
            {
                validator.Validate(item);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Debug.Assert(exception == null);

            if (exception != null)
                return false;
            return true;
        }

        private bool ConceptEncodingIsValid(AssessmentItem item, ref string errorMessage)
        {
            if (item.Solution?.ConceptFindings?.Count > 0)
            {
                var validator = new ConceptEncodingValidator();
                Exception exception = null;

                try
                {
                    validator.Validate(item);
                }
                catch (Exception e)
                {
                    exception = e;
                }

                Debug.Assert(exception == null);

                if (exception != null)
                {
                    errorMessage = _msgConceptEncodingOutOfSync;
                    return false;
                }
            }
            return true;
        }

        private bool StateAllowsSave(bool itemResourceBinaryDataIsDirty)
        {
            var action = ActionEntity.DataValue;
            if (action == null)
                return true;

            var stateName = action.StateActionCollection.FirstOrDefault()?.State?.Name ?? string.Empty;
            switch (ActionEntity.DataValue.Name.ToLower())
            {
                case "permit":
                    return true;

                case "warn":
                    if (itemResourceBinaryDataIsDirty || ((IPropertyEntity)ItemResourceEntity.DataValue).HasChangesInTopology())
                        return _messageBoxService.ShowYesNo(
                                   string.Format(GetUiString("SaveChangesForResourceWithState"),
                                       ItemResourceEntity.DataValue.Name, stateName), GetUiString("SaveCaption"),
                                   CustomDialogIcons.Warning) == CustomDialogResults.Yes;
                    break;

                case "prohibit":
                    var workFlowChangePermitted = PermissionFactory.Instance.TryUserIsPermittedToNamedTask(
                        TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask,
                        TestBuilderPermissionNamedTask.ChangeWorkflowMetadataWhenProhibittedByState,
                        ItemResourceEntity.DataValue.BankId, 0);

                    if (itemResourceBinaryDataIsDirty || !(workFlowChangePermitted && ((IPropertyEntity)ItemResourceEntity.DataValue).OnlyChangesInWorkflowMetaData()))
                    {
                        _messageBoxService.ShowInformation(string.Format(GetUiString("NotAllowedToChangeResourceWithState"), ItemResourceEntity.DataValue.Name, stateName), GetUiString("SaveCaption"));
                        return false;
                    }
                    return true;

                default:
                    return true;
            }

            return true;
        }

        private CustomDialogResults ContinueCloseAfterAskForSave()
        {
            var result = _messageBoxService.ShowYesNoCancel(string.Format(GetUiString("Dialog.DoYouWantToSave", true), ItemName),
                null, CustomDialogIcons.Exclamation, GetUiString("Dialog.Save", true), GetUiString("Dialog.NotSave", true), GetUiString("Dialog.Cancel", true));

            if (result == CustomDialogResults.Yes)
                Save.Execute(null);
            return result;
        }

        private void ExecuteWindowClosing(EventToCommandArgs e)
        {
            CurrentItemClosing = CloseCurrentItem();
            var args = (CancelEventArgs)e.EventArgs;

            args.Cancel = !CurrentItemClosing;
            if (!args.Cancel && PresentationWorkspace != null)
                DoTaskBeforeClosing(PresentationWorkspace);
        }

        private void ExecuteWindowActivated(EventToCommandArgs e)
        {
            Mediator.Instance.NotifyColleagues(Constants.SelectResourceInGrid, new EventArgs<Guid>(ItemId.DataValue));
        }

        private bool CloseCurrentItem()
        {
            var hasChanges = NeedSave();

            if (!hasChanges)
                return true;

            CurrentItemClosing = true;
            var result = ContinueCloseAfterAskForSave();

            if (result == CustomDialogResults.Yes && !_canAccordingState)
                return false;

            if (result == CustomDialogResults.Yes && !IsValid())
                return false;

            return result != CustomDialogResults.Cancel;
        }

        private void UpdateWindowTitle()
        {
            WindowTitle.DataValue = CreateWindowTitle(AssessmentItem.DataValue.Identifier, AssessmentItem.DataValue.Title, AssessmentItem.DataValue.ItemId); Mediator.Instance.NotifyColleagues(MediatorMessages.ItemEditor.Title, WindowTitle.DataValue);
        }

        private string CreateWindowTitle(string code, string title, string itemId)
        {
            if (string.IsNullOrEmpty(code))
                return _msgTitle;

            var format = ItemIdHelper.UseItemId() ? "{0} ({1}) [{3}] - {2}" : "{0} ({1}) - {2}";

            const int maxLength = 100;

            var titl = title;
            if (title.Length > maxLength)
                titl = title.Substring(0, maxLength) + "...";
            return string.Format(format, titl, code, _msgTitle, itemId);
        }

        private void DoSaveAs()
        {
            PreSaveNecessaryEvil();

            string validationErrorMessage;

            if (!CanSave(false, false, out validationErrorMessage))
            {
                GetAndShowErrorMessage(validationErrorMessage);
                SaveNeeded.DataValue = false;
                IsWorking.DataValue = false;
                return;
            }

            var result = _inputBox.Show(_msgPleaseNewItemCode,
                false, _msgPleaseNewItemCodeDialogTitle,
                string.Format(_msgDefaultPrefixNewCode, ItemResourceEntity.DataValue.Name),
                ValidationHelper.IsValidResourceCode);

            if (result.ReturnCode != DialogResult.OK)
                return;

            var oldName = ItemResourceEntity.DataValue.Name;
            var newName = result.Text.Trim();
            if (!ItemResourceEntity.DataValue.BankId.IsResourceNameTaken(newName))
            {
                AssessmentItem.DataValue.Identifier = newName;
                AssessmentItem.DataValue.SetScorePropertiesForItem(ItemResourceEntity.DataValue);
                ItemResourceEntity.DataValue.Name = newName;
                ItemResourceEntity.DataValue.Title = AssessmentItem.DataValue.Title.Trim(); ItemResourceEntity.DataValue.SetAssessmentItem(AssessmentItem.DataValue); ItemResourceEntity.DataValue = ItemResourceEntity.DataValue.CopyToNew(newName, oldName); ItemResourceEntity.DataValue.Version = string.Empty;
                var previousId = ItemId.DataValue;
                ItemId.DataValue = ItemResourceEntity.DataValue.ResourceId;
                OnItemIdChanged(previousId, ItemId.DataValue);

                if (ItemResourceEntity.DataValue.RequiresMajorVersionIncrement() && !IncrementMajorVersion())
                {
                    IsWorking.DataValue = false;
                    return;
                }

                UpdateDirtyCustomBankPropertyValues();

                IsWorking.DataValue = true;

                var originalState = GetItemResourceState();
                ItemResourceEntity.DataValue.UpdateDependencies();
                var errorMessage = ItemEditorObjectFactory.UpdateItemResource(ItemResourceEntity.DataValue); LogHelper.TrackEvent(EventsToTrack.ItemCreated, GetItemTrackingProperties());

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    _messageBoxService.ShowError(errorMessage, _msgError);
                    return;
                }
                AssessmentItem.DataValue.ItemId = ItemResourceEntity.DataValue.ItemId;
                PostSaveNecessaryEvil();
                UpdateWindowTitle();

                _hash = GetHash();

                if (StateHasChanged(originalState))
                    LoadNewStateAction();

                IsWorking.DataValue = false;

                Mediator.Instance.NotifyColleaguesAsync(Constants.RefreshGridAndSelectResource, new EventArgs<IPropertyEntity>(ItemResourceEntity.DataValue));
            }
            else
            {
                _messageBoxService.ShowError(string.Format(_msgNameNotUniqueInBank, newName), _msgError);
            }

            SaveNeeded.DataValue = false;
            IsWorking.DataValue = false;
        }

        private void GetAndShowErrorMessage(string validationErrorMessage)
        {
            var errorMessage = validationErrorMessage;

            if (AssessmentItem.DataValue == null)
                return;

            var paramErrors = AssessmentItem.DataValue.Parameters.GetValidateHierarchicalErrors();
            if (!string.IsNullOrEmpty(validationErrorMessage) && !string.IsNullOrEmpty(paramErrors))
                errorMessage = string.Concat(validationErrorMessage, "\r\n\r\n");

            if (!string.IsNullOrEmpty(paramErrors))
                errorMessage = string.Concat(validationErrorMessage,
                    string.Format("{0}\r\n\r\n{1}", _msgPendingRequiredFields, paramErrors));

            if (!string.IsNullOrEmpty(errorMessage))
                _messageBoxService.ShowError(errorMessage, _msgError);
        }

        private void UpdateDirtyCustomBankPropertyValues()
        {
            foreach (var value in ItemResourceEntity.DataValue.CustomBankPropertyValueCollection.Where(cbpv => cbpv.IsDirty))
                value.SetCustomPropertyDisplayValue();
        }

        private void PreSaveNecessaryEvil()
        {
            DoPreSaveTasksFor(PresentationWorkspace);
            DoPreSaveTasksFor(MetadataWorkspace);
            DoPreSaveTasksFor(ScoreWorkspace);
        }

        private void PostSaveNecessaryEvil()
        {
            DoPostSaveTasksFor(PresentationWorkspace);
            DoPostSaveTasksFor(MetadataWorkspace);
            DoPostSaveTasksFor(ScoreWorkspace);
        }

        public bool NeedSave()
        {
            return GetHash() != _hash ||
                   ItemResourceEntity.DataValue != null &&
                   ItemResourceEntity.DataValue.HasChangesInTopology();
        }

        private static void DoPreSaveTasksFor(DataWrapper<WorkspaceData> wsd)
        {
            var csp = wsd.DataValue?.ViewModelInstance as IViewModel2ViewCommandSupport;
            csp?.DoPreSaveTasks();
        }

        private static void DoTaskBeforeClosing(DataWrapper<WorkspaceData> wsd)
        {
            var csp = wsd.DataValue?.ViewModelInstance as IViewModel2ViewCommandSupport;
            csp?.DoTaskBeforeClosing();
        }

        private static void DoPostSaveTasksFor(DataWrapper<WorkspaceData> wsd)
        {
            var csp = wsd.DataValue?.ViewModelInstance as IViewModel2ViewCommandSupport;
            csp?.DoPostSaveTasks();
        }

        private static void DoTabChangeTasksFor(DataWrapper<WorkspaceData> wsd)
        {
            var csp = wsd.DataValue?.ViewModelInstance as IOnSwitchTabItemVMTasks;
            csp?.DoActionToPushChangesToModel();
        }

        private static void DoWorkspaceKillView(DataWrapper<WorkspaceData> wsd)
        {
            var csp = wsd.DataValue?.ViewModelInstance as IViewModel2ViewCommandSupport;
            csp?.KillView();
            if (csp != null)
                ((ViewModelBase)wsd.DataValue?.ViewModelInstance).Dispose();
        }

        private void HandleItemIdPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HandleItemIdChanged();
        }

        private void HandleSelectedTabChanges(object sender, PropertyChangedEventArgs e)
        {
            if (!IsLoading && _prevTab == 2) DoTabChangeTasksFor(ScoreWorkspace);

            _prevTab = SelectedTab.DataValue;
            SetRibbonTabToShowOverride();
        }

        public void ReloadScoring()
        {
            ((ScoringHostViewModel)ScoreWorkspace.DataValue.ViewModelInstance).KillView();
            ((ScoringHostViewModel)ScoreWorkspace.DataValue.ViewModelInstance).Dispose();
            GC.Collect();
            ScoreWorkspace = ItemEditorWorkspaceFactory.Create(this, ScoreArgs, ItemEditorWorkspaceFactory.CreateScore);
            NotifyPropertyChanged(ScoreArgs);
        }



        protected override void CleanUp()
        {
            SelectedTab.PropertyChanged -= HandleSelectedTabChanges;
            ItemId.PropertyChanged -= HandleItemIdPropertyChanged;

            if (CurrentEditor is IRichText)
                (CurrentEditor as IRichText).SelectionChanged -= OnEditorSelectionChanged;

            if (MetadataWorkspace != null)
            {
                DoWorkspaceKillView(MetadataWorkspace);
            }

            if (ScoreWorkspace != null)
            {
                DoWorkspaceKillView(ScoreWorkspace);
            }

            if (SourceWorkspace != null)
            {
                DoWorkspaceKillView(SourceWorkspace);
            }

            if (PresentationWorkspace != null)
            {
                DoWorkspaceKillView(PresentationWorkspace);
            }

            if (ResourceManager.DataValue != null)
                TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(ResourceManager.DataValue);

            TestSessionContext.ResourceNeeded -= Handle_TestSessionContext_ResourceNeeded;

            if (_timerNecessaryEvil != null)
            {
                _timerNecessaryEvil.Stop();
                _timerNecessaryEvil.Tick -= TimerNecessaryEvil_Tick;
                _timerNecessaryEvil = null;
            }

            ItemResourceEntity.DataValue = null;
            AssessmentItem.DataValue = null;
            ResourceManager.DataValue = null;
            ParameterSetCollection.DataValue = null;
            ActionEntity.DataValue = null;

            _session.Dispose();
        }

        ~ItemEditorViewModel()
        {
            Dispose(false);
        }

    }
}