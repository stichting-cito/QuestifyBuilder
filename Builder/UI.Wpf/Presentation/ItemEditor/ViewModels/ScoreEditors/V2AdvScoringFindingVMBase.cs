using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Controls;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.FactoriesAdv;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{

    abstract class V2AdvScoringFindingVMBase : ViewModelBase
    {
        static PropertyChangedEventArgs SelectedBlockRowsChangeArgs = ObservableHelper.CreateArgs<V2AdvScoringFindingVMBase>(m => m.SelectedBlockRows);
        protected static PropertyChangedEventArgs SelectedScoringMethodArgs = ObservableHelper.CreateArgs<V2AdvScoringFindingVMBase>(x => x.SelectedScoringMethod);

        private readonly Solution _solution;



        protected V2AdvScoringFindingVMBase(Solution solution)
        {
            _solution = solution;
            BlockGridData = new ObservableCollection<ObservableCollection<ObservableDictionary<string, IEnumerable>>>();
            SelectedBlockRows = new DataWrapper<List<BlockRow>>(this, SelectedBlockRowsChangeArgs) { DataValue = new List<BlockRow>() };
            InitCommands();

        }



        public ObservableCollection<ObservableCollection<ObservableDictionary<string, IEnumerable>>> BlockGridData { get; protected set; }

        public SimpleCommand<object, EventToCommandArgs> CanInsertBlockGridElementEventCommand { private set; get; }
        public SimpleCommand<object, EventToCommandArgs> InsertBlockGridElementEventCommand { private set; get; }
        public SimpleCommand<object, EventToCommandArgs> CanDeleteBlockGridElementEventCommand { private set; get; }
        public SimpleCommand<object, EventToCommandArgs> DeleteBlockGridElementEventCommand { private set; get; }

        public SimpleCommand<object, object> GroupInteractionsCommand { private set; get; }
        public SimpleCommand<object, object> UngroupCommand { private set; get; }
        public SimpleCommand<object, object> RemoveSetCommand { private set; get; }

        protected Solution Solution { get { return _solution; } }

        public DataWrapper<List<BlockRow>> SelectedBlockRows { get; private set; }

        public DataWrapper<EnumScoringMethod> SelectedScoringMethod { get; protected set; }
        public virtual bool ShowSelectedScoringMethod { get { return false; } }



        private void InitCommands()
        {
            CanInsertBlockGridElementEventCommand = new SimpleCommand<object, EventToCommandArgs>((a) => CanInsertBlockGridElement(a));
            InsertBlockGridElementEventCommand = new SimpleCommand<object, EventToCommandArgs>((a) => InsertblockGridElement(a));
            CanDeleteBlockGridElementEventCommand = new SimpleCommand<object, EventToCommandArgs>((a) => CanDeleteBlockGridElement(a));
            DeleteBlockGridElementEventCommand = new SimpleCommand<object, EventToCommandArgs>((a) => DeleteBlockGridElement(a));

            GroupInteractionsCommand = new SimpleCommand<object, object>((c) => CanExecuteGroupInteractionsCommand(c), (a) => ExecuteGroupInteractionsCommand(a));
            UngroupCommand = new SimpleCommand<object, object>((c) => CanExecuteUngroupCommand(c), (a) => ExecuteUngroupCommand(a));
            RemoveSetCommand = new SimpleCommand<object, object>((c) => CanExecuteRemoveSetCommand(c), (a) => ExecuteRemoveSetCommand(a));
        }

        protected IEnumerable<CombinedScoringMapKey> GetScoreMap()
        {
            return get_ScoreMap();
        }

        protected abstract IEnumerable<CombinedScoringMapKey> get_ScoreMap();



        protected virtual bool CanExecuteGroupInteractionsCommand(object c)
        {
            var selectedBlockRows = SelectedBlockRows.DataValue;
            if (selectedBlockRows == null || selectedBlockRows.Count == 0) { return false; }

            var selectedInteractions = new List<ScoringMapKey>();
            foreach (var item in selectedBlockRows)
            {
                IBlockRowViewModel blockRowViewModel = (IBlockRowViewModel)item.Item;
                var scoringMapKey = new ScoringMapKey(blockRowViewModel.ScoringParameter, blockRowViewModel.ScoreKey);
                if (!selectedInteractions.Contains(scoringMapKey))
                {
                    selectedInteractions.Add(scoringMapKey);
                }
            }

            var manipulator = new FactTargetManipulator(Solution);
            var scoringMap = GetScoreMap();
            return manipulator.CanGroupInteractions(selectedInteractions, scoringMap);
        }

        private void ExecuteGroupInteractionsCommand(object a)
        {
            var selectedBlockRows = SelectedBlockRows.DataValue;
            if (selectedBlockRows.Count == 1)
            {
                AddInteractionToGroup(selectedBlockRows.First());
            }
            else
            {
                GroupInteractions(selectedBlockRows);
            }
        }

        private void GroupInteractions(IEnumerable<BlockRow> selectedBlockRows)
        {
            var interactions = new List<ScoringMapKey>();
            foreach (var item in selectedBlockRows)
            {
                IBlockRowViewModel blockRowViewModel = (IBlockRowViewModel)item.Item;
                var scoringMapKey = new ScoringMapKey(blockRowViewModel.ScoringParameter, blockRowViewModel.ScoreKey);
                if (!interactions.Contains(scoringMapKey))
                {
                    interactions.Add(scoringMapKey);
                }
            }

            var manipulator = new FactTargetManipulator(_solution);
            var firstFactSetNumber = manipulator.GroupInteractions(interactions);


            var scoreMap = GetScoreMap();
            var combined = scoreMap.FirstOrDefault(c => c.SetNumbers.Any(n => n == firstFactSetNumber));
            RefreshBlockGridData();
            Mediator.Instance.NotifyColleagues(Constants.SolutionGroupChanged, false);

            var secondFactSetNumber = manipulator.AddFactSet(combined);
            var block = CreateBlock(combined, secondFactSetNumber);
            var row = BlockGridData.First(r => r.Any(b => b.Values.Cast<IEnumerable<IBlockRowViewModel>>().Any(bvrms => bvrms.Any(bvrm => bvrm.FactSetNumber == firstFactSetNumber))));
            if (row.Count == 1) { row.Add(block); }
        }

        private void AddInteractionToGroup(BlockRow selectedBlockRow)
        {
            IBlockRowViewModel blockRowViewModel = (IBlockRowViewModel)selectedBlockRow.Item;

            var groups = GetScoreMap().Where(m => m.IsGroup).ToList();

            CombinedScoringMapKey selectedGroup = null;
            if (groups.Count > 1)
            {
                var chooser = ViewModelRepository.Instance.Resolver.Container.GetExport<IChooseDialogService>().Value;
                var choosables = new List<object>();
                choosables.AddRange(groups);
                selectedGroup = chooser.ShowSelection(Properties.Resources.SelectInteractionGroupTitle, Properties.Resources.SelectInteractionGroupDescription, choosables) as CombinedScoringMapKey;
            }
            else if (groups.Count == 1)
            {
                selectedGroup = groups.First();
            }
            if (selectedGroup != null)
            {
                var scoringMapKey = selectedGroup.First();
                var manipulator = new FactTargetManipulator(_solution);
                var factSetNumbers = manipulator.GetFactSetNumbers(scoringMapKey.ScoreKey, scoringMapKey.ScoringParameter);
                manipulator.AddToGroup(new ScoringMapKey(blockRowViewModel.ScoringParameter, blockRowViewModel.ScoreKey), factSetNumbers);
            }
            RefreshBlockGridData();
            Mediator.Instance.NotifyColleagues(Constants.SolutionGroupChanged, true);
        }

        private bool CanExecuteUngroupCommand(object c)
        {
            var selectedBlockRows = SelectedBlockRows.DataValue;
            if (selectedBlockRows == null || selectedBlockRows.Count < 1) { return false; }

            IBlockRowViewModel blockRowViewModel = (IBlockRowViewModel)selectedBlockRows.First().Item;
            if (!blockRowViewModel.FactSetNumber.HasValue) { return false; }

            if (blockRowViewModel.ScoringParameter.MustRemainGrouped) { return false; }

            var manipulator = new FactTargetManipulator(_solution);
            var scoringMapKey = new ScoringMapKey(blockRowViewModel.ScoringParameter, blockRowViewModel.ScoreKey);
            var scoringMap = GetScoreMap();
            return manipulator.CanRemoveFromGroup(scoringMapKey, blockRowViewModel.FactSetNumber.Value, scoringMap);
        }

        private void ExecuteUngroupCommand(object a)
        {
            var selectedBlockRow = SelectedBlockRows.DataValue[0];
            IBlockRowViewModel blockRowViewModel = (IBlockRowViewModel)selectedBlockRow.Item;

            var manipulator = new FactTargetManipulator(_solution);
            var scoringMapKey = new ScoringMapKey(blockRowViewModel.ScoringParameter, blockRowViewModel.ScoreKey);
            var scoringMap = GetScoreMap();

            manipulator.RemoveFromGroup(scoringMapKey, scoringMap);

            RefreshBlockGridData();
            Mediator.Instance.NotifyColleagues(Constants.SolutionGroupChanged, true);
        }

        private bool CanExecuteRemoveSetCommand(object c)
        {
            var selectedBlockRows = SelectedBlockRows.DataValue;
            if (selectedBlockRows == null || selectedBlockRows.Count < 1) { return false; }

            IBlockRowViewModel blockRowViewModel = (IBlockRowViewModel)selectedBlockRows.First().Item;

            var manipulator = new FactTargetManipulator(_solution);
            var scoreMapKey = new ScoringMapKey(blockRowViewModel.ScoringParameter, blockRowViewModel.ScoreKey);

            return manipulator.CanRemoveFactSet(scoreMapKey, blockRowViewModel.FactSetNumber);
        }

        protected virtual void ExecuteRemoveSetCommand(object a)
        {
            var selectedBlockRow = SelectedBlockRows.DataValue[0];
            IBlockRowViewModel blockRowViewModel = (IBlockRowViewModel)selectedBlockRow.Item;

            var manipulator = new FactTargetManipulator(Solution);
            var scoreMapKey = new ScoringMapKey(blockRowViewModel.ScoringParameter, blockRowViewModel.ScoreKey);
            var scoringMap = GetScoreMap();

            manipulator.RemoveFactSet(scoreMapKey, blockRowViewModel.FactSetNumber.Value);

            RefreshBlockGridData();
        }

        internal virtual bool CanAddFactSet(IBlockRowViewModel blockRowViewModel)
        {
            var manipulator = new FactTargetManipulator(_solution);
            var interactions = CombinedScoringMapKey.Create(new ScoringMapKey(blockRowViewModel.ScoringParameter, blockRowViewModel.ScoreKey));

            return manipulator.CanAddFactSet(interactions);
        }

        internal ObservableDictionary<string, IEnumerable> AddFactSet(ObservableCollection<ObservableDictionary<string, IEnumerable>> aRowInBlockGrid, Solution solution)
        {
            var rowNumber = BlockGridData.IndexOf(aRowInBlockGrid);
            var rowTemplate = GetScoreMap().ToList()[rowNumber];

            var factSetNumber = new FactTargetManipulator(solution).AddFactSet(rowTemplate);

            var newBlock = CreateBlock(rowTemplate, factSetNumber);

            aRowInBlockGrid.Add(newBlock);

            return newBlock;
        }

        private void CanInsertBlockGridElement(EventToCommandArgs args)
        {
            if (args == null || args.EventArgs == null)
            {
                return;
            }

            BlockGridCanInsertElementEventArgs ciArgs = (BlockGridCanInsertElementEventArgs)args.EventArgs;
            IBlockRowViewModel blockRowViewModel = ciArgs.BlockRowDataContext.BlockRowDataItem as IBlockRowViewModel;

            switch (ciArgs.TypeOfElementToInsert)
            {
                case BlockGridElement.BlockGridBlockRow:
                    ciArgs.Cancel = true;
                    break;
                case BlockGridElement.BlockInRow:
                    ciArgs.Cancel = CanAddFactSet(blockRowViewModel) == false;
                    break;
                case BlockGridElement.BlockRow:
                    ciArgs.Cancel = CanAddAlternative(blockRowViewModel) == false;
                    break;
                default:
                    break;
            }

        }

        private bool CanAddAlternative(IBlockRowViewModel blockRowViewModel)
        {
            if (blockRowViewModel == null) { return false; }
            if (blockRowViewModel.CorrectInteractionResponseIsDefinedByOneDistinctValue) { return false; }
            if (!blockRowViewModel.ScoringParameter.AlternativesCanBeAdded) { return false; }
            return true;
        }

        private void InsertblockGridElement(EventToCommandArgs args)
        {
            if (args == null || args.EventArgs == null)
            {
                return;
            }

            BlockGridInsertElementEventArgs ciArgs = (BlockGridInsertElementEventArgs)args.EventArgs;

            IBlockRowViewModel blockRowViewModel = ciArgs.BlockRowDataContext.BlockRowDataItem as IBlockRowViewModel;

            switch (ciArgs.TypeOfElementToInsert)
            {
                case BlockGridElement.BlockGridBlockRow:
                    break;
                case BlockGridElement.BlockInRow:
                    var blockGridBlockRowData = (ObservableCollection<ObservableDictionary<string, IEnumerable>>)ciArgs.BlockRowDataContext.BlockGridBlockRowDataItem;
                    ciArgs.InsertedDataItem = AddFactSet(blockGridBlockRowData, _solution);
                    break;
                case BlockGridElement.BlockRow:
                    var blockInRowData = (ObservableDictionary<string, IEnumerable>)ciArgs.BlockRowDataContext.BlockInRowDataItem;
                    ciArgs.InsertedDataItem = AddScoreAlternative(blockInRowData, blockRowViewModel, _solution);
                    break;
            }

        }

        internal IBlockRowViewModel AddScoreAlternative(ObservableDictionary<string, IEnumerable> blockInRowData, IBlockRowViewModel addAlternativeLikeThisOne, Solution solution)
        {
            var newDictonary = new Dictionary<string, IEnumerable>();

            var key = addAlternativeLikeThisOne.Name;
            var values = new List<IBlockRowViewModel>(blockInRowData[key].Cast<IBlockRowViewModel>());
            var index = values.IndexOf(addAlternativeLikeThisOne);
            var addedBlockRow = BlockRowViewModelFactory.InsertInstance(
                            addAlternativeLikeThisOne.ScoringParameter,
                            addAlternativeLikeThisOne.ScoreKey,
                            addAlternativeLikeThisOne.FactSetNumber,
                            addAlternativeLikeThisOne.Index,
                            solution);
            values.Insert(index + 1, addedBlockRow);
            for (int i = index + 2; i < values.Count; i++)
            {
                values[i].Index++;
            }
            foreach (var oldKey in blockInRowData.Keys)
            {
                if (oldKey == key)
                    newDictonary.Add(oldKey, values);
                else
                    newDictonary.Add(oldKey, blockInRowData[oldKey]);
            }

            blockInRowData.Clear();

            foreach (string oldKey in newDictonary.Keys)
            {
                blockInRowData.Add(oldKey, newDictonary[oldKey]);
            }

            return addedBlockRow;
        }


        internal bool RemoveScoreAlternative(ObservableDictionary<string, IEnumerable> blockInRowData, IBlockRowViewModel alternativeToRemove, Solution solution)
        {
            bool blockRowRemoved = false;

            Dictionary<string, IEnumerable> newDictonary = new Dictionary<string, IEnumerable>();

            foreach (string key in blockInRowData.Keys)
            {
                IEnumerable values = blockInRowData[key];
                List<IBlockRowViewModel> newValues = new List<IBlockRowViewModel>();

                foreach (IBlockRowViewModel blockInRow in values)
                {
                    if (blockInRow.Equals(alternativeToRemove))
                    {
                        blockRowRemoved = true;

                        alternativeToRemove.RemoveValueFromScore();
                    }
                    else
                    {
                        if (blockRowRemoved && blockInRow.ScoreKey.Equals(alternativeToRemove.ScoreKey))
                        {
                            blockInRow.Index -= 1;
                        }

                        newValues.Add(blockInRow);
                    }
                }


                if (newValues.Count > 0)
                {
                    newDictonary.Add(key, newValues);
                }
            }

            if (blockRowRemoved)
            {
                blockInRowData.Clear();
                foreach (string key in newDictonary.Keys)
                {
                    blockInRowData.Add(key, newDictonary[key]);
                }

                newDictonary.Clear();
            }

            return blockRowRemoved;
        }


        private void CanDeleteBlockGridElement(EventToCommandArgs args)
        {
            if (args?.EventArgs == null)
            {
                return;
            }

            BlockGridCanDeleteElementEventArgs cdArgs = (BlockGridCanDeleteElementEventArgs)args.EventArgs;
            IBlockRowViewModel ibrvm = cdArgs.BlockRowDataContext.BlockRowDataItem as IBlockRowViewModel;

            switch (cdArgs.TypeOfElementToDelete)
            {
                case BlockGridElement.BlockGridBlockRow:
                    break;
                case BlockGridElement.BlockInRow:
                    break;
                case BlockGridElement.BlockRow:
                    bool cancelDelete = true;

                    if (ibrvm != null)
                    {
                        IDictionary<string, IEnumerable> blockInRowData = (IDictionary<string, IEnumerable>)cdArgs.BlockRowDataContext.BlockInRowDataItem;
                        if (blockInRowData.Keys.Contains(ibrvm.Name))
                        {
                            IList<IBlockRowViewModel> values = (IList<IBlockRowViewModel>)blockInRowData[ibrvm.Name];
                            cancelDelete = (values.Count <= 1);
                        }
                    }

                    cdArgs.Cancel = cancelDelete;
                    break;
            }
        }

        private void DeleteBlockGridElement(EventToCommandArgs args)
        {
            if (args == null || args.EventArgs == null)
            {
                return;
            }

            BlockGridDeleteElementEventArgs dArgs = (BlockGridDeleteElementEventArgs)args.EventArgs;

            IBlockRowViewModel blockRowViewModel = dArgs.BlockRowDataContext.BlockRowDataItem as IBlockRowViewModel;

            switch (dArgs.TypeOfElementToDelete)
            {
                case BlockGridElement.BlockGridBlockRow:
                    break;
                case BlockGridElement.BlockInRow:
                    break;
                case BlockGridElement.BlockRow:
                    RemoveScoreAlternative((ObservableDictionary<string, IEnumerable>)dArgs.BlockRowDataContext.BlockInRowDataItem, blockRowViewModel, _solution);
                    break;
                default:
                    break;
            }
        }



        protected void RefreshBlockGridData()
        {
            BlockGridData.Clear();
            AddBlockGridData();
        }

        internal void AddBlockGridData()
        {
            var scoreMap = GetScoreMap();

            foreach (var combinedScoringMapKey in scoreMap)
            {
                BlockGridData.Add(AddRow(combinedScoringMapKey));
            }
        }

        private ObservableCollection<ObservableDictionary<string, IEnumerable>> AddRow(CombinedScoringMapKey combinedScoringMapKey)
        {
            var ret = new ObservableCollection<ObservableDictionary<string, IEnumerable>>();

            if (combinedScoringMapKey.IsGroup)
            {
                foreach (int setNumber in combinedScoringMapKey.SetNumbers)
                    ret.Add(CreateBlock(combinedScoringMapKey, setNumber));

                if (!ret.Any())
                {
                    var factSetNumber = new FactTargetManipulator(_solution).AddFactSet(combinedScoringMapKey);
                    ret.Add(CreateBlock(combinedScoringMapKey, factSetNumber));
                }

            }
            else
            {
                ret.Add(CreateBlock(combinedScoringMapKey));
            }

            return ret;
        }

        protected ObservableDictionary<string, IEnumerable> CreateBlock(CombinedScoringMapKey combinedScoringMapKey, int? factSet = null)
        {
            var ret = new ObservableDictionary<string, IEnumerable>();

            var viewModels = BlockRowViewModelFactory.CreateInstances(combinedScoringMapKey, _solution, factSet);

            foreach (var vm in viewModels)
            {
                if (!ret.ContainsKey(vm.Name)) ret.Add(vm.Name, new List<IBlockRowViewModel>());

                ((List<IBlockRowViewModel>)ret[vm.Name]).Add(vm);
            }


            return ret;
        }


    }
}
