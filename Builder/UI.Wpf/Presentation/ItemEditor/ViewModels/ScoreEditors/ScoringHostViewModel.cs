using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    [ExportViewModel("ItemEditor.ScoringHostVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ScoringHostViewModel : ViewModelBase, IViewModel2ViewCommandSupport, IOnSwitchTabItemVMTasks
    {

        static PropertyChangedEventArgs ScoreEditorArgs = ObservableHelper.CreateArgs<ScoringHostViewModel>(x => x.ScoreEditor); static PropertyChangedEventArgs ScoreTranslationTableEditorArgs = ObservableHelper.CreateArgs<ScoringHostViewModel>(x => x.ScoreTranslationTableEditor); static PropertyChangedEventArgs AspectScoreEditorArgs = ObservableHelper.CreateArgs<ScoringHostViewModel>(x => x.AspectScoreEditor);
        public SimpleCommand<object, object> AutoScoringOnCommand { private set; get; }
        public SimpleCommand<object, object> AutoScoringOffCommand { private set; get; }



        private readonly IViewAwareStatus _viewAwareStatusService;
        private IItemEditorViewModel _itemEditorVm;


        [ImportingConstructor]
        public ScoringHostViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewDeactivated;
            ScoreEditor = new DataWrapper<WorkspaceData>(this, ScoreEditorArgs);
            ScoreTranslationTableEditor = new DataWrapper<WorkspaceData>(this, ScoreTranslationTableEditorArgs);
            AspectScoreEditor = new DataWrapper<WorkspaceData>(this, AspectScoreEditorArgs);
            InitCommands();
        }

        private void InitCommands()
        {
            AutoScoringOnCommand = new SimpleCommand<object, object>((c) => CanExecuteAutoScoringOnCommand(), (a) => ExecuteAutoScoringOnCommand());
            AutoScoringOffCommand = new SimpleCommand<object, object>((c) => CanExecuteAutoScoringOffCommand(), (a) => ExecuteAutoScoringOffCommand());
        }

        private void viewAwareStatusService_ViewDeactivated()
        {
            DoPreSaveTasks();

            DisposeEditors();

            if (AspectScoreEditor.DataValue != null)
            {
                AspectScoreEditor.DataValue.Dispose();
                AspectScoreEditor.DataValue = null;
            }

            if (_itemEditorVm != null)
            {
                _itemEditorVm.Updated -= ItemEditor_Updated;
            }

            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewDeactivated;
        }

        private void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                DisposeEditors();

                if (AspectScoreEditor.DataValue != null)
                {
                    AspectScoreEditor.DataValue.Dispose();
                    AspectScoreEditor.DataValue = null;
                }

                var workspaceData = (IWorkSpaceAware)_viewAwareStatusService.View;

                _itemEditorVm = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;
                _itemEditorVm.Updated += ItemEditor_Updated;

                if (_itemEditorVm.HasError.DataValue) return;
                var scoringParameters = _itemEditorVm.ParameterSetCollection.DataValue.DeepFetchScoringParameters();
                if (scoringParameters.Any(sp => sp.GetType() != typeof(AspectScoringParameter)))
                {
                    bool loadV2BasicScoreEditor = false;
                    loadV2BasicScoreEditor |= V2ScoringViewModel.LoadMeInstead;

                    if (loadV2BasicScoreEditor)
                    {
                        ScoreEditor.DataValue = new WorkspaceData(string.Empty, Constants.ScoringWorkSpaceV2, _itemEditorVm, string.Empty, false);
                    }
                    else
                    {
                        ScoreEditor.DataValue = new WorkspaceData(string.Empty, Constants.ScoringWorkSpaceV2Adv, _itemEditorVm, string.Empty, false);
                    }
                }
                else if (!scoringParameters.Any(sp => sp.GetType() != typeof(AspectScoringParameter)))
                {
                    ScoreEditor.DataValue = null;
                }

                if (scoringParameters.Any(sp => sp.GetType() == typeof(AspectScoringParameter) && ((AspectScoringParameter)sp).SingleAspectScoringEditor == false))
                {
                    AspectScoringParameter aspectScoringPrm = (AspectScoringParameter)scoringParameters.First(sp => sp.GetType() == typeof(AspectScoringParameter) && ((AspectScoringParameter)sp).SingleAspectScoringEditor == false);
                    if (AspectScoreEditor.DataValue == null)
                    {
                        var aspectEditorDataValue = new Tuple<IItemEditorViewModel, String, String>(
                            _itemEditorVm,
                            aspectScoringPrm.ControllerId,
                            aspectScoringPrm.AspectScoreEditorBoundAspect);
                        AspectScoreEditor.DataValue = new WorkspaceData(String.Empty, Constants.AspectScoringAdvancedWorkSpace, aspectEditorDataValue, String.Empty, false);
                    }
                }
                else if (scoringParameters.Any(sp => sp.GetType() == typeof(AspectScoringParameter) && ((AspectScoringParameter)sp).SingleAspectScoringEditor))
                {
                    AspectScoringParameter aspectScoringPrm = (AspectScoringParameter)scoringParameters.First(sp => sp.GetType() == typeof(AspectScoringParameter) && ((AspectScoringParameter)sp).SingleAspectScoringEditor);
                    if (AspectScoreEditor.DataValue == null)
                    {
                        var aspectEditorDataValue = new Tuple<IItemEditorViewModel, IAspectReferencesScoringViewModel, String, String, bool, bool, bool>(
                            _itemEditorVm,
                            null,
                            aspectScoringPrm.AspectScoreEditorBoundAspect,
                            aspectScoringPrm.ControllerId,
                            (ScoreEditor.DataValue == null) || _itemEditorVm.AssessmentItem.DataValue.Solution.AutoScoring == false,
                            false,
                            true); AspectScoreEditor.DataValue = new WorkspaceData(String.Empty, Constants.AspectScoringSingleWorkSpace, aspectEditorDataValue, String.Empty, false);
                    }
                }
                else if (!scoringParameters.Any(sp => sp.GetType() == typeof(AspectScoringParameter)))
                {
                    AspectScoreEditor.DataValue = null;
                }

                if (ScoreTranslationTableEditor.DataValue == null)
                {
                    ScoreTranslationTableEditor.DataValue = new WorkspaceData(string.Empty, Constants.ScoreTranslationTableWorkSpace, _itemEditorVm, string.Empty, false);
                }

                NotifyPropertyChanged(ScoreEditorArgs);
                NotifyPropertyChanged(ScoreTranslationTableEditorArgs);
                NotifyPropertyChanged(AspectScoreEditorArgs);

                _itemEditorVm.EnableElementsOnCompletion();

                if (!_itemEditorVm.IsLoading)
                    Update();
            }
        }

        private void ItemEditor_Updated(object sender, StringEventArgs e)
        {
            if (e.StringValue == "DoUpdate")
                Update();
        }

        internal void Update()
        {
            if (_itemEditorVm.HasError.DataValue)
            {
                return;
            }

            if (AspectScoreEditor.DataValue != null)
            {
                if (AspectScoreEditor?.DataValue?.ViewModelInstance is AspectReferenceScoringViewModel)
                {
                    var aspectScoringViewModel = AspectScoreEditor?.DataValue?.ViewModelInstance as AspectReferenceScoringViewModel;
                    aspectScoringViewModel?.Update();
                }
            }
        }

        public DataWrapper<WorkspaceData> ScoreEditor { private set; get; }
        public DataWrapper<WorkspaceData> ScoreTranslationTableEditor { private set; get; }
        public DataWrapper<WorkspaceData> AspectScoreEditor { private set; get; }

        public void DoPreSaveTasks()
        {
            if (ScoreEditor.DataValue?.ViewModelInstance == null && AspectScoreEditor.DataValue?.ViewModelInstance == null)
            {
                return;
            }

            IViewModel2ViewCommandSupport CmdSupp;
            if (ScoreEditor.DataValue?.ViewModelInstance != null)
            {
                CmdSupp = ScoreEditor.DataValue.ViewModelInstance as IViewModel2ViewCommandSupport;
                CmdSupp?.DoPreSaveTasks();

                if (ScoreEditor.DataValue.ViewModelInstance is V2ScoringViewModel)
                {
                    var scoringParameters = ((V2ScoringViewModel)ScoreEditor.DataValue.ViewModelInstance).ScoringParameters;
                    if (_itemEditorVm?.ItemResourceEntity?.DataValue != null)
                    {
                        _itemEditorVm.ItemResourceEntity.DataValue.AlternativesCount = ScoringPropertiesCalculator.GetAlternativesCount(scoringParameters);
                    }
                }
            }
            if (AspectScoreEditor.DataValue?.ViewModelInstance != null)
            {
                CmdSupp = AspectScoreEditor.DataValue.ViewModelInstance as IViewModel2ViewCommandSupport;
                CmdSupp?.DoPreSaveTasks();
            }

            if (ScoreTranslationTableEditor.DataValue?.ViewModelInstance != null)
            {
                CmdSupp = ScoreTranslationTableEditor.DataValue.ViewModelInstance as IViewModel2ViewCommandSupport;
                CmdSupp?.DoPreSaveTasks();
            }
        }

        public bool CanExecuteAutoScoringOnCommand()
        {
            if (_itemEditorVm?.AssessmentItem?.DataValue?.Solution?.AutoScoring == null || _itemEditorVm.AssessmentItem.DataValue.Solution.AutoScoring)
            {
                return false;
            }
            var scoringParameters = _itemEditorVm.ParameterSetCollection.DataValue.DeepFetchScoringParameters();
            return scoringParameters.Any(sp => sp.GetType() != typeof(AspectScoringParameter));
        }

        public void ExecuteAutoScoringOnCommand()
        {
            _itemEditorVm.AssessmentItem.DataValue.Solution.AutoScoring = true;

            var scoringParameters = _itemEditorVm.ParameterSetCollection.DataValue.DeepFetchScoringParameters();
            scoringParameters.Where(sp => sp.GetType() != typeof(AspectScoringParameter)).ToList().ForEach(sp => DefaultValuesSetter.Create(sp, _itemEditorVm.AssessmentItem.DataValue.Solution).Execute(string.Empty, 0));

            Mediator.Instance.NotifyColleagues(Constants.AutoScoringChanged, true);
        }

        public bool CanExecuteAutoScoringOffCommand()
        {
            if (_itemEditorVm?.AssessmentItem?.DataValue?.Solution?.AutoScoring == null || !_itemEditorVm.AssessmentItem.DataValue.Solution.AutoScoring)
            {
                return false;
            }
            var scoringParameters = _itemEditorVm.ParameterSetCollection.DataValue.DeepFetchScoringParameters();
            return scoringParameters.Any(sp => sp.GetType() != typeof(AspectScoringParameter));
        }

        public void ExecuteAutoScoringOffCommand()
        {
            _itemEditorVm.AssessmentItem.DataValue.Solution.AutoScoring = false;

            var scoringParameters = _itemEditorVm.ParameterSetCollection.DataValue.DeepFetchScoringParameters();
            _itemEditorVm.AssessmentItem.DataValue.Solution.FixRemovedScoringParameters(scoringParameters);

            Mediator.Instance.NotifyColleagues(Constants.AutoScoringChanged, false);
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void KillView()
        {
            if (_viewAwareStatusService.View != null)
            {
                ((IDisposable)_viewAwareStatusService.View).Dispose();
            }
        }

        private void DisposeEditors()
        {
            if (ScoreEditor.DataValue != null)
            {
                ScoreEditor.DataValue.Dispose();
                ScoreEditor.DataValue = null;
            }

            if (ScoreTranslationTableEditor.DataValue != null)
            {
                ScoreTranslationTableEditor.DataValue.Dispose();
                ScoreTranslationTableEditor.DataValue = null;
            }

            if (AspectScoreEditor.DataValue != null)
            {
                AspectScoreEditor.DataValue.Dispose();
                AspectScoreEditor.DataValue = null;
            }
        }

        protected override void OnDispose()
        {
            if (ScoreEditor != null)
            {
                DoWorkspaceKillView(ScoreEditor);
            }

            if (AspectScoreEditor != null)
            {
                DoWorkspaceKillView(AspectScoreEditor);
            }

            if (ScoreTranslationTableEditor != null)
            {
                DoWorkspaceKillView(ScoreTranslationTableEditor);
            }

            DisposeEditors();

            if (_itemEditorVm != null)
            {
                _itemEditorVm.Updated -= ItemEditor_Updated; _itemEditorVm = null;
            }

            base.OnDispose();
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

        public void DoPostSaveTasks() { }

        public void DoActionToPushChangesToModel()
        {
            if (ScoreEditor.DataValue != null && ScoreEditor.DataValue.ViewModelInstance != null)
            {
                IOnSwitchTabItemVMTasks CmdSupp = ScoreEditor.DataValue.ViewModelInstance as IOnSwitchTabItemVMTasks;
                CmdSupp?.DoActionToPushChangesToModel();
            }

            if (AspectScoreEditor.DataValue != null && AspectScoreEditor.DataValue.ViewModelInstance != null)
            {
                IOnSwitchTabItemVMTasks CmdSupp = AspectScoreEditor.DataValue.ViewModelInstance as IOnSwitchTabItemVMTasks;
                CmdSupp?.DoActionToPushChangesToModel();
            }
        }

    }
}
