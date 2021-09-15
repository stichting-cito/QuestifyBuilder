using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    [ExportViewModel("ItemEditor.ScoreTranslationTableEditorVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ScoreTranslationTableEditorViewModel : ViewModelBase, IViewModel2ViewCommandSupport
    {
        static readonly PropertyChangedEventArgs ScoreTranslationTableArgs = ObservableHelper.CreateArgs<ScoreTranslationTableEditorViewModel>(x => x.ScoreTranslationTable);
        static readonly PropertyChangedEventArgs ScoreTranslationVisibilityArgs = ObservableHelper.CreateArgs<ScoreTranslationTableEditorViewModel>(x => x.ScoreTranslationVisibility);

        private IViewAwareStatus _viewAwareStatusService;
        private IItemEditorViewModel _itemEditorVM = null;
        private string _lastFindingsSerialization = null;
        private int _lastMaxRawSolutionScore = -1;
        private System.Windows.Threading.DispatcherTimer _solutionPollTimer;

        [ImportingConstructor]
        public ScoreTranslationTableEditorViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += ViewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded += ViewAwareStatusService_ViewUnloaded;

            ScoreTranslationTable = new DataWrapper<ItemScoreTranslationTable>(this, ScoreTranslationTableArgs);
            ScoreTranslationVisibility = new DataWrapper<System.Windows.Visibility>(this, ScoreTranslationVisibilityArgs);

            _solutionPollTimer = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.ApplicationIdle);
            _solutionPollTimer.Interval = new TimeSpan(0, 0, 1);
            _solutionPollTimer.Tick += SolutionPollTimer_Tick;
        }

        public DataWrapper<ItemScoreTranslationTable> ScoreTranslationTable { private set; get; }
        public DataWrapper<System.Windows.Visibility> ScoreTranslationVisibility { private set; get; }

        public void DoPreSaveTasks()
        {
            if (_itemEditorVM != null)
            {
                Solution itemSolution = _itemEditorVM.AssessmentItem.DataValue.Solution;

                ScoreTranslationTable.DataValue = DimensionItemScoreTranslationTable(GetMaxScore(),
                                                            itemSolution.ItemScoreTranslationTable,
                                                            out _);
            }
        }

        public void DoTaskBeforeClosing() { }

        public void KillView() { }

        protected override void OnDispose()
        {
            _itemEditorVM = null;
            base.OnDispose();
        }

        public void DoPostSaveTasks() { }

        private void ViewAwareStatusService_ViewLoaded()
        {
            if (Designer.IsInDesignMode)
            {
                return;
            }

            var workspaceData = (IWorkSpaceAware)_viewAwareStatusService.View;
            _itemEditorVM = (IItemEditorViewModel)workspaceData.WorkSpaceContextualData.DataValue;

            if (_itemEditorVM.HasError.DataValue)
            {
                return;
            }

            Solution itemSolution = _itemEditorVM.AssessmentItem.DataValue.Solution;

            ScoreTranslationTable.DataValue = DimensionItemScoreTranslationTable(GetMaxScore(),
                                                                     itemSolution.ItemScoreTranslationTable,
                                                                     out _);

            SetVisibility();

            _solutionPollTimer.Start();
        }

        private void ViewAwareStatusService_ViewUnloaded()
        {
            SyncScoring();

            if (_solutionPollTimer != null)
            {
                _solutionPollTimer.Stop();
            }

            _viewAwareStatusService.ViewLoaded -= ViewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= ViewAwareStatusService_ViewUnloaded;
        }


        private void SolutionPollTimer_Tick(object sender, EventArgs e)
        {
            SyncScoring();
        }

        private bool CheckForChangedScoringMethod(string previouslySerializedFindings, KeyFindingCollection currentFindings)
        {
            if (string.IsNullOrEmpty(previouslySerializedFindings))
            {
                return false;
            }

            var previousFindings = (KeyFindingCollection)Cito.Tester.Common.SerializeHelper.XmlDeserializeFromString(_lastFindingsSerialization,
                                                                                                                     typeof(KeyFindingCollection));

            foreach (KeyFinding finding in currentFindings)
            {
                KeyFinding prevFinding = previousFindings.FirstOrDefault(x => x.Id == finding.Id);

                if (prevFinding != null && finding.Method != prevFinding.Method)
                {
                    return true;
                }
            }

            return false;
        }

        private int GetMaxScore()
        {
            Solution itemSolution = _itemEditorVM.AssessmentItem.DataValue.Solution;
            var aspectsReferences = _itemEditorVM.AssessmentItem.DataValue.Solution.AspectReferenceSetCollection.SelectMany(a => a.Items);

            if (!aspectsReferences.Any())
            {
                return itemSolution.MaxSolutionRawScore;
            }

            var bankId = _itemEditorVM.ItemResourceEntity.DataValue.BankId;
            var aspectsInBank = DtoFactory.Aspect.GetResourcesForBank(bankId);

            int sum = 0;

            foreach (var reference in aspectsReferences)
            {
                var aspectDto = aspectsInBank.FirstOrDefault(a => a.Name == reference.SourceName);
                var aspect = GetAspect(aspectDto?.ResourceId);

                if (aspect == null)
                {
                    continue;
                }

                if (aspect.AspectScoreIsTranslated())
                {
                    sum += (int)aspect.AspectScoreTranslationTable.Max(t => t.TranslatedScore);
                }
                else
                {
                    sum += aspect.MaxScore;
                }
            }

            return sum;
        }

        private Aspect GetAspect(Guid? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            var aspectResource = ResourceFactory.Instance.GetResourceByIdWithOption(id.Value, new Cito.Tester.Common.ResourceRequestDTO()) as AspectResourceEntity;
            aspectResource.EnsureResourceData();
            if (aspectResource.ResourceData != null && aspectResource.ResourceData.BinData.Any())
            {
                return Cito.Tester.Common.SerializeHelper.XmlDeserializeFromByteArray(aspectResource.ResourceData.BinData, typeof(Aspect), true) as Aspect;
            }
            return null;
        }

        private ItemScoreTranslationTable DimensionItemScoreTranslationTable(int maxRawScore, ItemScoreTranslationTable currentItemScoreTranslationTable, out bool tableChanged)
        {
            int nrOfRowsBeforeSync = currentItemScoreTranslationTable.Count;

            currentItemScoreTranslationTable = DimensionItemScoreTranslationTable(maxRawScore, currentItemScoreTranslationTable);

            tableChanged = (nrOfRowsBeforeSync == currentItemScoreTranslationTable.Count);
            return currentItemScoreTranslationTable;
        }

        private ItemScoreTranslationTable DimensionItemScoreTranslationTable(int maxRawScore, ItemScoreTranslationTable currentItemScoreTranslationTable)
        {
            _lastMaxRawSolutionScore = maxRawScore;

            ItemScoreTranslationTableCalculator calc = new ItemScoreTranslationTableCalculator(maxRawScore, currentItemScoreTranslationTable);
            currentItemScoreTranslationTable = (ItemScoreTranslationTable)calc.SynchronizeScoreTranslationTableWithMaxRawScore();

            return currentItemScoreTranslationTable;
        }

        private void SetVisibility()
        {
            Solution itemSolution = _itemEditorVM.AssessmentItem.DataValue.Solution;
            var aspects = _itemEditorVM.AssessmentItem.DataValue.Solution.AspectReferenceSetCollection;

            ScoreTranslationVisibility.DataValue = (itemSolution.MaxSolutionRawScore > 0 && itemSolution.Findings.Any()) || aspects.Any()
                                            ? System.Windows.Visibility.Visible
                                            : System.Windows.Visibility.Collapsed;
        }

        private void SyncScoring()
        {
            if (_itemEditorVM == null)
            {
                return;
            }

            lock (_itemEditorVM)
            {
                var scoreVM = _itemEditorVM?.ScoreWorkspace?.DataValue?.ViewModelInstance as IOnSwitchTabItemVMTasks;
                if (scoreVM != null)
                    scoreVM.DoActionToPushChangesToModel();

                if (_itemEditorVM?.AssessmentItem?.DataValue?.Solution == null)
                {
                    return;
                }

                Solution itemSolution = _itemEditorVM.AssessmentItem.DataValue.Solution;
                string currentFindingsSerialization = Cito.Tester.Common.SerializeHelper.XmlSerializeToString(itemSolution.Findings);

                if (currentFindingsSerialization.CompareTo(_lastFindingsSerialization) == 0)
                {
                    return;
                }

                bool scoringMethodHasChanged = CheckForChangedScoringMethod(_lastFindingsSerialization, itemSolution.Findings);

                _lastFindingsSerialization = currentFindingsSerialization;

                int? maxRawScore = GetMaxScore();

                if (scoringMethodHasChanged || maxRawScore.HasValue && maxRawScore.Value != _lastMaxRawSolutionScore)
                {
                    if (scoringMethodHasChanged)
                    {
                        DimensionItemScoreTranslationTable(0, itemSolution.ItemScoreTranslationTable);
                    }

                    ScoreTranslationTable.DataValue = null;
                    ItemScoreTranslationTable tmp = DimensionItemScoreTranslationTable(maxRawScore.Value,
                                                                                        itemSolution.ItemScoreTranslationTable,
                                                                                        out bool tableChanged);

                    if (tableChanged)
                    {
                        ScoreTranslationTable.DataValue = null;
                        ScoreTranslationTable.DataValue = tmp;
                    }

                    SetVisibility();
                }
            }
        }
    }
}
