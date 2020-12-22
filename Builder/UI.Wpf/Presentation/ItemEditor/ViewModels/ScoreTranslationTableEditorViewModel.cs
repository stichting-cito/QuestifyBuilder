using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    [ExportViewModel("ItemEditor.ScoreTranslationTableEditorVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ScoreTranslationTableEditorViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {

        static readonly PropertyChangedEventArgs ScoreTranslationTableArgs = ObservableHelper.CreateArgs<ScoreTranslationTableEditorViewModel>(x => x.ScoreTranslationTable); static readonly PropertyChangedEventArgs ScoreTranslationVisibilityArgs = ObservableHelper.CreateArgs<ScoreTranslationTableEditorViewModel>(x => x.ScoreTranslationVisibility);



        IViewAwareStatus _viewAwareStatusService;
        IItemEditorViewModel _ItemEditorVM = null;
        string _lastFindingsSerialization = null;
        int _lastMaxRawSolutionScore = -1;
        System.Windows.Threading.DispatcherTimer _solutionPollTimer;



        [ImportingConstructor]
        public ScoreTranslationTableEditorViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;

            ScoreTranslationTable = new DataWrapper<ItemScoreTranslationTable>(this, ScoreTranslationTableArgs);
            ScoreTranslationVisibility = new DataWrapper<System.Windows.Visibility>(this, ScoreTranslationVisibilityArgs);

            _solutionPollTimer = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.ApplicationIdle);
            _solutionPollTimer.Interval = new TimeSpan(0, 0, 1);
            _solutionPollTimer.Tick += _solutionPollTimer_Tick;
        }



        public DataWrapper<ItemScoreTranslationTable> ScoreTranslationTable { private set; get; }
        public DataWrapper<System.Windows.Visibility> ScoreTranslationVisibility { private set; get; }



        private void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                IWorkSpaceAware workspaceData = (IWorkSpaceAware)view;

                _ItemEditorVM = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;

                if (_ItemEditorVM.HasError.DataValue) return;
                Solution itemSolution = _ItemEditorVM.AssessmentItem.DataValue.Solution;

                bool tableChanged = false;
                ScoreTranslationTable.DataValue = DimensionItemScoreTranslationTable(itemSolution.MaxSolutionRawScore, itemSolution.ItemScoreTranslationTable, out tableChanged);

                ScoreTranslationVisibility.DataValue = (itemSolution.MaxSolutionRawScore > 0 && itemSolution.Findings.Any()) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

                _solutionPollTimer.Start();
            }
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            SyncScoring();
            if (_solutionPollTimer != null)
            {
                _solutionPollTimer.Stop();
            }

            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }



        public void DoPreSaveTasks()
        {
            if (_ItemEditorVM != null)
            {
                Solution itemSolution = _ItemEditorVM.AssessmentItem.DataValue.Solution;
                bool tableChanged = false;

                ScoreTranslationTable.DataValue = DimensionItemScoreTranslationTable(itemSolution.MaxSolutionRawScore, itemSolution.ItemScoreTranslationTable, out tableChanged);
            }
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void KillView()
        {
        }

        protected override void OnDispose()
        {
            _ItemEditorVM = null;

            base.OnDispose();
        }

        public void DoPostSaveTasks() { }



        void _solutionPollTimer_Tick(object sender, EventArgs e)
        {
            SyncScoring();
        }

        private bool CheckForChangedScoringMethod(string previouslySerializedFindings, KeyFindingCollection currentFindings)
        {
            bool scoringMethodHasChanged = false;

            if (!string.IsNullOrEmpty(previouslySerializedFindings))
            {
                KeyFindingCollection previousFindings = (KeyFindingCollection)Cito.Tester.Common.SerializeHelper.XmlDeserializeFromString(_lastFindingsSerialization, typeof(KeyFindingCollection));

                foreach (KeyFinding finding in currentFindings)
                {
                    KeyFinding prevFinding = previousFindings.FirstOrDefault(x => x.Id == finding.Id);

                    if (prevFinding != null)
                    {
                        scoringMethodHasChanged = (finding.Method != prevFinding.Method);
                        if (scoringMethodHasChanged)
                        {
                            break;
                        }
                    }
                }
            }

            return scoringMethodHasChanged;
        }


        private ItemScoreTranslationTable DimensionItemScoreTranslationTable(int maxRawScore, ItemScoreTranslationTable currentItemScoreTranslationTable, out bool tableChanged)
        {
            _lastMaxRawSolutionScore = maxRawScore;

            tableChanged = false;

            if (maxRawScore > currentItemScoreTranslationTable.Count - 1 && maxRawScore > 0)
            {
                tableChanged = true;

                for (int i = currentItemScoreTranslationTable.Count; i - 1 < maxRawScore; i++)
                {
                    currentItemScoreTranslationTable.Add(new ItemScoreTranslationTableEntry(i, (double)i));
                }
            }
            else if (maxRawScore < currentItemScoreTranslationTable.Count - 1)
            {
                tableChanged = true;

                for (int i = currentItemScoreTranslationTable.Count - 1; i > maxRawScore; i--)
                {
                    currentItemScoreTranslationTable.Remove(currentItemScoreTranslationTable[i]);
                }

                if (maxRawScore == 0 && currentItemScoreTranslationTable.Count == 1)
                {
                    currentItemScoreTranslationTable.Remove(currentItemScoreTranslationTable[0]);
                }
            }

            return currentItemScoreTranslationTable;
        }

        private void SyncScoring()
        {
            if (_ItemEditorVM != null)
            {
                lock (_ItemEditorVM)
                {
                    var scoreVM = _ItemEditorVM?.ScoreWorkspace?.DataValue?.ViewModelInstance as IOnSwitchTabItemVMTasks;
                    if (scoreVM != null)
                        scoreVM.DoActionToPushChangesToModel();

                    if (_ItemEditorVM?.AssessmentItem?.DataValue?.Solution != null)
                    {
                        Solution itemSolution = _ItemEditorVM.AssessmentItem.DataValue.Solution;
                        string currentFindingsSerialization = Cito.Tester.Common.SerializeHelper.XmlSerializeToString(itemSolution.Findings);

                        if (currentFindingsSerialization.CompareTo(_lastFindingsSerialization) != 0)
                        {
                            bool scoringMethodHasChanged = CheckForChangedScoringMethod(_lastFindingsSerialization, itemSolution.Findings);

                            _lastFindingsSerialization = currentFindingsSerialization;

                            int? maxRawScore = ScoringPropertiesCalculator.GetRawScore(itemSolution);

                            if (scoringMethodHasChanged || maxRawScore.HasValue && maxRawScore.Value != _lastMaxRawSolutionScore)
                            {
                                bool tableChanged = false;

                                if (scoringMethodHasChanged)
                                {
                                    DimensionItemScoreTranslationTable(0, itemSolution.ItemScoreTranslationTable, out tableChanged);
                                }

                                ScoreTranslationTable.DataValue = null;
                                ItemScoreTranslationTable tmp = DimensionItemScoreTranslationTable(maxRawScore.Value, itemSolution.ItemScoreTranslationTable, out tableChanged);

                                if (tableChanged)
                                {
                                    ScoreTranslationTable.DataValue = null;
                                    ScoreTranslationTable.DataValue = tmp;
                                }

                                ScoreTranslationVisibility.DataValue = (itemSolution.MaxSolutionRawScore > 0 && itemSolution.Findings.Any()) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
        }
    }
}
