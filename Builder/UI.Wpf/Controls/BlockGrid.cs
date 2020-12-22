using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors.DataTemplates;

namespace Questify.Builder.UI.Wpf.Controls
{


    public enum BlockGridElement
    {
        BlockGridBlockRow,
        BlockInRow,
        BlockRow
    }


    [TemplatePart(Name = BlockGrid.part_ScrollViewer, Type = typeof(ScrollViewer))]
    public class BlockGrid : MultiSelector
    {

        internal const string part_ScrollViewer = "PART_ScrollViewer";



        static BlockGrid()
        {
            Type ownerType = typeof(BlockGrid);

            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(typeof(BlockGrid)));

            CommandManager.RegisterClassInputBinding(ownerType, new InputBinding(BeginEditCommand, new KeyGesture(Key.F2)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(BeginEditCommand, (s, e) => ((BlockGrid)s).OnExecutedBeginEdit(e), (s, e) => ((BlockGrid)s).OnCanExecuteBeginEdit(e)));

            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(CommitEditCommand, (s, e) => ((BlockGrid)s).OnExecutedCommitEdit(e), (s, e) => ((BlockGrid)s).OnCanExecuteCommitEdit(e)));

            CommandManager.RegisterClassInputBinding(ownerType, new InputBinding(CancelEditCommand, new KeyGesture(Key.Escape)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(CancelEditCommand, (s, e) => ((BlockGrid)s).OnExecutedCancelEdit(e), (s, e) => ((BlockGrid)s).OnCanExecuteCancelEdit(e)));

            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(BlockGridCommands.InsertBlockGridElement, (s, e) => ((BlockGrid)s).OnExecutedInsertBlockGridElement(e), (s, e) => ((BlockGrid)s).OnCanExecuteInsertBlockGridElement(e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(BlockGridCommands.DeleteBlockGridElement, (s, e) => ((BlockGrid)s).OnExecutedDeleteBlockGridElement(e), (s, e) => ((BlockGrid)s).OnCanExecuteDeleteBlockGridElement(e)));
        }




        public static readonly DependencyProperty ColumnHeaderStyleProperty =
    DependencyProperty.Register("ColumnHeaderStyle", typeof(Style), typeof(BlockGrid), new FrameworkPropertyMetadata(null));



        public double ColumnHeaderHeight
        {
            get { return (double)GetValue(ColumnHeaderHeightProperty); }
            set { SetValue(ColumnHeaderHeightProperty, value); }
        }

        public static readonly DependencyProperty ColumnHeaderHeightProperty = DependencyProperty.Register("ColumnHeaderHeight", typeof(double), typeof(BlockGrid), new FrameworkPropertyMetadata(25.0, OnNotifyColumnHeaderPropertyChanged));



        public static readonly DependencyProperty CellStyleProperty =
    DependencyProperty.Register("CellStyle", typeof(Style), typeof(BlockGrid), new PropertyMetadata(null));



        public bool EnableFirstRowSelect
        {
            get { return (bool)GetValue(EnableFirstRowSelectProperty); }
            set { SetValue(EnableFirstRowSelectProperty, value); }
        }

        public static readonly DependencyProperty EnableFirstRowSelectProperty =
    DependencyProperty.Register("EnableFirstRowSelect", typeof(bool), typeof(BlockGrid), new PropertyMetadata(false));



        public int FirstRowCellIndexToFocus
        {
            get { return (int)GetValue(FirstRowCellIndexToFocusProperty); }
            set { SetValue(FirstRowCellIndexToFocusProperty, value); }
        }

        public static readonly DependencyProperty FirstRowCellIndexToFocusProperty =
    DependencyProperty.Register("FirstRowCellIndexToFocus", typeof(int), typeof(BlockGrid), new PropertyMetadata(-1));




        public static readonly RoutedCommand BeginEditCommand = new RoutedCommand("BeginEdit", typeof(BlockGrid));

        public static readonly RoutedCommand CommitEditCommand = new RoutedCommand("CommitEdit", typeof(BlockGrid));

        public static readonly RoutedCommand CancelEditCommand = new RoutedCommand("CancelEdit", typeof(BlockGrid));



        private readonly BlockGridColumnCollection _columns;
        private ScrollViewer _scrollViewer; private bool _measureNeverInvoked = true; private BindingGroup _defaultBindingGroup; private readonly IBlockGridSelectionHandler _cellSelectorHandler;
        private bool _hasCellValidationError; private bool _hasRowValidationError; private bool _isSettingFocus = false;



        public BlockGrid()
        {
            _columns = new BlockGridColumnCollection(this);
            _columns.CollectionChanged += HandleOnColumnsChanged;
            _cellSelectorHandler = new BlockInRowFullRowSelector(this);
        }



        public IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>> RowsOfBlocks
        {
            get { return (IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>>)ItemsSource; }
        }



        internal BlockGridHeaderPresenter ColumnHeadersPresenter { get; set; }

        public BlockGridColumnCollection Columns => _columns;

        private void HandleOnColumnsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    UpdateGridReference(e.NewItems, false);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    UpdateGridReference(e.OldItems, true);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    UpdateGridReference(e.OldItems, true);
                    UpdateGridReference(e.NewItems, false);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }


        internal void UpdateGridReference(IList list, bool clear)
        {
            int numItems = list.Count;
            for (int i = 0; i < numItems; i++)
            {
                BlockGridColumn column = (BlockGridColumn)list[i];
                if (clear)
                {
                    if (column.BlockGridOwner == this)
                    {
                        column.BlockGridOwner = null;
                    }
                }
                else
                {
                    if (column.BlockGridOwner != null && column.BlockGridOwner != this)
                    {
                        column.BlockGridOwner.Columns.Remove(column);
                    }

                    column.BlockGridOwner = this;
                }
            }
        }




        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is BlockGridBlockRow);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BlockGridBlockRow();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            BlockGridBlockRow blockGridBlockRow = element as BlockGridBlockRow;

            IEnumerable<IDictionary<string, IEnumerable>> data = item as IEnumerable<IDictionary<string, IEnumerable>>;
            if (blockGridBlockRow != null)
            {
                blockGridBlockRow.PrepareBlockGridBlockRow(data, this);
            }
            BindHorizontalOffsetToLocalProperty();
        }



        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            NotifyPropertyChanged(this, "Items", new DependencyPropertyChangedEventArgs(), BlockGridNotificationTarget.ColumnHeadersPresenter);
            base.ClearContainerForItemOverride(element, item);
        }



        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public bool HasRowValidationError
        {
            get { return _hasRowValidationError; }
            set
            {
                if (_hasRowValidationError != value)
                {
                    _hasRowValidationError = value;
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private bool HasCellValidationError
        {
            get { return _hasCellValidationError; }

            set
            {
                if (_hasCellValidationError != value)
                {
                    _hasCellValidationError = value;

                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
    DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(BlockGrid), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsReadOnlyChanged)));


        private static void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public bool TryBeginEdit(RoutedEventArgs editingEventArgs)
        {
            if (IsReadOnly)
            {
                return false;
            }

            BlockGridCell cellContainer = CurrentCellContainer;
            if (cellContainer == null)
            {
                return false;
            }

            if (!cellContainer.IsEditing &&
                BeginEditCommand.CanExecute(editingEventArgs, cellContainer))
            {
                BeginEditCommand.Execute(editingEventArgs, cellContainer);

                cellContainer = CurrentCellContainer;
            }

            return cellContainer.IsEditing;
        }

        public bool CancelEdit()
        {
            if (IsEditingCurrentCell)
            {
                return CancelEdit(BlockGridEditingUnit.Cell);
            }
            BlockGridCell currentCell = CurrentCellContainer;

            if (currentCell != null && (currentCell.BlockGridCellOwner.BlockRowOwner.IsEditingRowItem || currentCell.BlockGridCellOwner.BlockRowOwner.IsAddingNewItem))
            {
                return CancelEdit(BlockGridEditingUnit.Row);
            }

            return true;
        }

        internal bool CancelEdit(BlockGridCell cell)
        {
            BlockGridCell currentCell = CurrentCellContainer;
            if (currentCell != null && currentCell == cell && currentCell.IsEditing)
            {
                return CancelEdit(BlockGridEditingUnit.Cell);
            }
            return true;
        }

        public bool CancelEdit(BlockGridEditingUnit editingUnit)
        {
            return EndEdit(CancelEditCommand, CurrentCellContainer, editingUnit, true);
        }

        public bool CommitEdit()
        {
            if (IsEditingCurrentCell)
            {
                return CommitEdit(BlockGridEditingUnit.Cell, true);
            }

            BlockGridCell currentCell = CurrentCellContainer;

            if (currentCell != null && (currentCell.BlockGridCellOwner.BlockRowOwner.IsEditingRowItem
                                        || currentCell.BlockGridCellOwner.BlockRowOwner.IsAddingNewItem))
            {
                return CommitEdit(BlockGridEditingUnit.Row, true);
            }

            return true;
        }

        private bool CommitEdit(BlockGridEditingUnit editingUnit, bool exitEditingMode)
        {
            return EndEdit(CommitEditCommand, CurrentCellContainer, editingUnit, exitEditingMode);
        }

        private bool EndEdit(RoutedCommand command, BlockGridCell cellContainer, BlockGridEditingUnit editingUnit, bool exitEditMode)
        {
            bool cellLeftEditingMode = true;
            bool rowLeftEditingMode = true;

            if (cellContainer != null)
            {
                if (command.CanExecute(editingUnit, cellContainer))
                {
                    command.Execute(editingUnit, cellContainer);
                }

                cellLeftEditingMode = !cellContainer.IsEditing;
                rowLeftEditingMode = !cellContainer.BlockGridCellOwner.BlockRowOwner.IsEditingRowItem && cellContainer.BlockGridCellOwner.BlockRowOwner.IsAddingNewItem;
            }

            if (exitEditMode)
            {
                return cellLeftEditingMode && ((editingUnit == BlockGridEditingUnit.Cell) || rowLeftEditingMode);
            }

            if (editingUnit == BlockGridEditingUnit.Cell)
            {
                if (cellContainer != null && cellLeftEditingMode)
                {
                    return TryBeginEdit(null);
                }
                return false;
            }

            if (cellContainer == null || !rowLeftEditingMode)
            {
                return false;
            }

            object rowItem = cellContainer.RowDataItem;

            if (rowItem == null)
            {
                return false;
            }

            cellContainer.BlockGridCellOwner.BlockRowOwner.EditBlockRowItem(rowItem);
            return cellContainer.BlockGridCellOwner.BlockRowOwner.IsEditingRowItem;
        }



        internal void SyncSelectedItems(HashSet<BlockRow> selectedBlockRows)
        {
            List<BlockRow> valuesToAdd = selectedBlockRows.Where(x => !this.SelectedItems.Contains(x)).ToList<BlockRow>();
            List<BlockRow> valuesToDelete = this.SelectedItems.Cast<BlockRow>().Where(x => !selectedBlockRows.Contains(x)).ToList();

            if (valuesToAdd.Any() || valuesToDelete.Any())
            {
                this.BeginUpdateSelectedItems();

                valuesToAdd.ForEach(x => SelectedItems.Add(x));
                valuesToDelete.ForEach(x => SelectedItems.Remove(x));

                this.EndUpdateSelectedItems();
            }
        }

        internal void SelectCell(BlockGridCell cell, RoutedEventArgs e)
        {
            _cellSelectorHandler.SelectCell(cell, e);
            if (((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) ||
                cell == null || cell.Column == null || cell.Column.IsReadOnly ||
                cell.Column.GetType() != typeof(BlockGridCellTemplateColumn))
            {
                return;
            }

            _cellSelectorHandler.SelectCell(cell, e);

            if (!_isSettingFocus && e.GetType() == typeof(MouseButtonEventArgs) && cell.IsEditing && IsScoreEditorColumn(cell.Column) && cell.RowDataItem != null)
            {
                ((IBlockRowViewModel)cell.RowDataItem).SetValueOnStartingEdit();
            }
        }

        internal BlockGridCell CurrentCellContainer
        {
            get { return _cellSelectorHandler.CurrentSelectedCell; }
        }

        internal bool IsEditingCurrentCell
        {
            get
            {
                BlockGridCell cell = CurrentCellContainer;
                return (cell != null) && cell.IsEditing;
            }
        }

        internal bool IsCurrentCellReadOnly
        {
            get
            {
                BlockGridCell cell = CurrentCellContainer;
                return (cell != null) && cell.IsReadOnly;
            }
        }



        protected virtual void OnCanExecuteBeginEdit(CanExecuteRoutedEventArgs e)
        {
            bool canExecute = !IsReadOnly && (CurrentCellContainer != null) && !IsEditingCurrentCell && !IsCurrentCellReadOnly && !HasCellValidationError;

            if (canExecute && HasRowValidationError)
            {
                BlockGridCell cellContainer = CurrentCellContainer;
                if (cellContainer != null)
                {
                    canExecute = true;
                }
                else
                {
                    canExecute = false;
                }
            }

            if (canExecute)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else
            {
                e.ContinueRouting = true;
            }
        }

        protected virtual void OnExecutedBeginEdit(ExecutedRoutedEventArgs e)
        {
            BlockGridCell cell = CurrentCellContainer;
            if ((cell != null) && !cell.IsReadOnly && !cell.IsEditing)
            {
                RoutedEventArgs editingEventArgs = e.Parameter as RoutedEventArgs;
                BlockGridBeginningEditEventArgs beginningEditEventArgs = null;


                beginningEditEventArgs = new BlockGridBeginningEditEventArgs(cell.Column, cell.BlockGridCellOwner, editingEventArgs);
                OnBeginningEdit(beginningEditEventArgs);

                if (!cell.BlockGridCellOwner.BlockRowOwner.IsEditingRowItem)
                {
                    cell.BlockGridCellOwner.BlockRowOwner.EditBlockRowItem(cell.RowDataItem);

                    var bindingGroup = cell.BlockGridCellOwner.BindingGroup;
                    if (bindingGroup != null)
                    {
                        bindingGroup.BeginEdit();
                    }
                }

                cell.BeginEdit(editingEventArgs);
                cell.BlockGridCellOwner.IsEditing = true;
            }

            CommandManager.InvalidateRequerySuggested();

            e.Handled = true;
        }

        public event EventHandler<BlockGridBeginningEditEventArgs> BeginningEdit;

        protected virtual void OnBeginningEdit(BlockGridBeginningEditEventArgs e)
        {
            BeginningEdit?.Invoke(this, e);
        }


        protected virtual void OnCanExecuteCommitEdit(CanExecuteRoutedEventArgs e)
        {
            if (CanEndEdit(e))
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else
            {
                e.ContinueRouting = true;
            }
        }

        protected virtual void OnExecutedCommitEdit(ExecutedRoutedEventArgs e)
        {
            BlockGridCell cell = CurrentCellContainer;
            bool validationPassed = true;
            if (cell == null)
            {
                e.Handled = true;
                return;
            }
            BlockGridEditingUnit editingUnit = GetEditingUnit(e.Parameter);

            bool eventCanceled = false;
            if (cell.IsEditing)
            {
                BlockGridCellEditEndingEventArgs cellEditEndingEventArgs = new BlockGridCellEditEndingEventArgs(cell.Column, cell.BlockGridCellOwner, BlockGridEditAction.Commit);
                OnCellEditEnding(cellEditEndingEventArgs);

                eventCanceled = cellEditEndingEventArgs.Cancel;
                if (!eventCanceled)
                {
                    validationPassed = cell.CommitEdit();
                    HasCellValidationError = !validationPassed;
                }
            }

            if (validationPassed &&
!eventCanceled &&
cell.BlockGridCellOwner.BlockRowOwner.IsAddingOrEditingBlockRowItem(editingUnit, cell.RowDataItem))
            {
                BlockGridRowEditEndingEventArgs rowEditEndingEventArgs = new BlockGridRowEditEndingEventArgs(cell.BlockGridCellOwner, BlockGridEditAction.Commit);
                OnRowEditEnding(rowEditEndingEventArgs);

                if (!rowEditEndingEventArgs.Cancel)
                {
                    var bindingGroup = cell.BlockGridCellOwner.BindingGroup;
                    if (bindingGroup != null)
                    {
                        Dispatcher.Invoke(new DispatcherOperationCallback(DoNothing), DispatcherPriority.DataBind, bindingGroup);
                        validationPassed = bindingGroup.CommitEdit();
                    }

                    HasRowValidationError = !validationPassed;
                    if (validationPassed)
                    {
                        cell.BlockGridCellOwner.BlockRowOwner.CommitBlockRowItem();
                    }
                }
            }

            if (validationPassed)
            {
                UpdateRowEditing(cell);
            }

            CommandManager.InvalidateRequerySuggested();

            e.Handled = true;
        }

        private static object DoNothing(object arg)
        {
            return null;
        }



        private BlockGridEditingUnit GetEditingUnit(object parameter)
        {
            return ((parameter != null) && (parameter is BlockGridEditingUnit)) ? (BlockGridEditingUnit)parameter : IsEditingCurrentCell ? BlockGridEditingUnit.Cell : BlockGridEditingUnit.Row;
        }

        public event EventHandler<BlockGridRowEditEndingEventArgs> RowEditEnding;

        protected virtual void OnRowEditEnding(BlockGridRowEditEndingEventArgs e)
        {
            if (RowEditEnding != null)
            {
                RowEditEnding(this, e);
            }
        }

        public event EventHandler<BlockGridCellEditEndingEventArgs> CellEditEnding;

        protected virtual void OnCellEditEnding(BlockGridCellEditEndingEventArgs e)
        {
            if (CellEditEnding != null)
            {
                CellEditEnding(this, e);
            }
        }


        private BlockGridCell GetEventCellOrCurrentCell(RoutedEventArgs e)
        {
            UIElement source = e.OriginalSource as UIElement;
            return ((source == this) || (source == null)) ? CurrentCellContainer : BlockGridHelper.FindVisualParent<BlockGridCell>(source);
        }


        private bool CanEndEdit(CanExecuteRoutedEventArgs e)
        {
            BlockGridCell cellContainer = GetEventCellOrCurrentCell(e);
            if (cellContainer == null)
            {
                return false;
            }

            BlockGridEditingUnit editingUnit = GetEditingUnit(e.Parameter);
            object rowItem = cellContainer.RowDataItem;

            return cellContainer.IsEditing ||
(!HasCellValidationError &&
cellContainer.BlockGridCellOwner.BlockRowOwner.IsAddingOrEditingBlockRowItem(editingUnit, rowItem));
        }

        protected virtual void OnCanExecuteCancelEdit(CanExecuteRoutedEventArgs e)
        {
            if (CanEndEdit(e))
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else
            {
                e.ContinueRouting = true;
            }
        }

        protected virtual void OnExecutedCancelEdit(ExecutedRoutedEventArgs e)
        {
            BlockGridCell cell = CurrentCellContainer;
            if (cell != null)
            {
                BlockGridEditingUnit editingUnit = GetEditingUnit(e.Parameter);

                bool eventCanceled = false;
                if (cell.IsEditing)
                {
                    BlockGridCellEditEndingEventArgs cellEditEndingEventArgs = new BlockGridCellEditEndingEventArgs(cell.Column, cell.BlockGridCellOwner, BlockGridEditAction.Cancel);
                    OnCellEditEnding(cellEditEndingEventArgs);

                    eventCanceled = cellEditEndingEventArgs.Cancel;
                    if (!eventCanceled)
                    {
                        cell.CancelEdit();
                        HasCellValidationError = false;
                    }
                }

                if (!eventCanceled &&
                    cell.BlockGridCellOwner.BlockRowOwner.IsAddingOrEditingBlockRowItem(editingUnit, cell.RowDataItem))
                {
                    bool cancelAllowed = true;

                    BlockGridRowEditEndingEventArgs rowEditEndingEventArgs = new BlockGridRowEditEndingEventArgs(cell.BlockGridCellOwner, BlockGridEditAction.Cancel);
                    OnRowEditEnding(rowEditEndingEventArgs);
                    cancelAllowed = !rowEditEndingEventArgs.Cancel;

                    if (cancelAllowed)
                    {
                        var bindingGroup = cell.BlockGridCellOwner.BindingGroup;
                        if (bindingGroup != null)
                        {
                            bindingGroup.CancelEdit();
                        }

                        cell.BlockGridCellOwner.BlockRowOwner.CancelBlockRowItem();
                    }
                }

                UpdateRowEditing(cell);

                if (!cell.BlockGridCellOwner.IsEditing)
                {
                    HasRowValidationError = false;
                }

                CommandManager.InvalidateRequerySuggested();
            }

            e.Handled = true;
        }


        private static void UpdateRowEditing(BlockGridCell cell)
        {
            object rowDataItem = cell.RowDataItem;

            if (!cell.BlockGridCellOwner.BlockRowOwner.IsAddingOrEditingBlockRowItem(rowDataItem))
            {
                cell.BlockGridCellOwner.IsEditing = false;
            }
        }

        private static bool IsScoreEditorColumn(BlockGridCellColumn column)
        {
            if (column.GetType() == typeof(BlockGridCellTemplateColumn))
            {
                var col = (BlockGridCellTemplateColumn)column;
                return col.CellEditingTemplateSelector != null && col.CellEditingTemplateSelector.GetType() == typeof(ScoreEditorSelector);
            }
            return false;
        }



        public static readonly RoutedEvent CanInsertBlockGridElementEvent = EventManager.RegisterRoutedEvent("CanInsertBlockGridElement", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BlockGrid));

        public event RoutedEventHandler CanInsertBlockGridElement
        {
            add { AddHandler(CanInsertBlockGridElementEvent, value); }
            remove { RemoveHandler(CanInsertBlockGridElementEvent, value); }
        }

        public static readonly RoutedEvent InsertBlockGridElementEvent = EventManager.RegisterRoutedEvent("InsertBlockGridElement", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BlockGrid));

        public event RoutedEventHandler InsertBlockGridElement
        {
            add { AddHandler(InsertBlockGridElementEvent, value); }
            remove { RemoveHandler(InsertBlockGridElementEvent, value); }
        }

        protected virtual void OnCanInsertElement(BlockGridCanInsertElementEventArgs e)
        {
            this.RaiseEvent(e);
        }

        protected virtual void OnInsertElement(BlockGridInsertElementEventArgs e)
        {
            this.RaiseEvent(e);
        }


        protected virtual void OnCanExecuteInsertBlockGridElement(CanExecuteRoutedEventArgs e)
        {
            BlockGridElement elementToInsert = (BlockGridElement)e.Parameter;

            if (CurrentCellContainer != null && CurrentCellContainer.IsEditing)
            {
                e.CanExecute = CanEndEdit(e);
            }
            else
            {
                e.CanExecute = (CurrentCellContainer != null || (elementToInsert == BlockGridElement.BlockGridBlockRow && this.IsFocused));
            }

            if (e.CanExecute)
            {
                BlockRowDataCoordinates blockRowDataCoords = new BlockRowDataCoordinates(this.ItemsSource, CurrentCellContainer, false);

                BlockGridCanInsertElementEventArgs canInsert = new BlockGridCanInsertElementEventArgs(CanInsertBlockGridElementEvent, blockRowDataCoords, elementToInsert, e);

                OnCanInsertElement(canInsert);
                e.CanExecute = !canInsert.Cancel;
            }

        }

        protected virtual void OnExecutedInsertBlockGridElement(ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                BlockGridElement elementToInsert = (BlockGridElement)e.Parameter;

                BlockRowDataCoordinates blockRowDataCoords = new BlockRowDataCoordinates(this.ItemsSource, CurrentCellContainer, true);

                BlockGridInsertElementEventArgs insertionArgs = new BlockGridInsertElementEventArgs(InsertBlockGridElementEvent, blockRowDataCoords, elementToInsert, e);
                OnInsertElement(insertionArgs);

#if DEBUG
                AssertDataSetIsValidAfterInsertionOrDeletion(insertionArgs.BlockRowDataContext.BlockGridDataItem);
#endif

                MakeInsertedElementCurrent(insertionArgs);
            }
        }


        private BlockGridCell GetCellToActivateAfterInsertOrDelete(object blockGridBlockRowIdentifier,
            object blockInRowIdentifier,
            object blockRowIdentifier,
            bool afterDeletion,
            BlockGridElement affectedElement)
        {
            DependencyObject itemContainerBlockGridBlockRow;
            BlockGridCell bgc = null;

            this.UpdateLayout();

            if (blockGridBlockRowIdentifier is int)
            {
                itemContainerBlockGridBlockRow = this.ItemContainerGenerator.ContainerFromIndex((int)blockGridBlockRowIdentifier);
                if (itemContainerBlockGridBlockRow == null && afterDeletion && (int)blockGridBlockRowIdentifier > 0)
                {
                    itemContainerBlockGridBlockRow = this.ItemContainerGenerator.ContainerFromIndex((int)blockGridBlockRowIdentifier - 1);
                }
            }
            else
            {
                itemContainerBlockGridBlockRow = this.ItemContainerGenerator.ContainerFromItem(blockGridBlockRowIdentifier);
            }

            BlockGridBlockRow bgbr = itemContainerBlockGridBlockRow as BlockGridBlockRow;

            if (bgbr == null)
            {
                return null;
            }

            DependencyObject itemContainerBlockInRow;

            if (affectedElement == BlockGridElement.BlockInRow)
            {
                bgbr.Items.Refresh();
                bgbr.UpdateLayout();
            }

            if (blockInRowIdentifier is int)
            {
                itemContainerBlockInRow = bgbr.ItemContainerGenerator.ContainerFromIndex((int)blockInRowIdentifier);

                if (afterDeletion && itemContainerBlockInRow == null)
                {
                    int intBlockInRowIndex = (int)blockInRowIdentifier - 1;
                    while (itemContainerBlockInRow == null && intBlockInRowIndex > -1)
                    {
                        itemContainerBlockInRow = bgbr.ItemContainerGenerator.ContainerFromIndex(intBlockInRowIndex);

                        intBlockInRowIndex--;
                    }
                }
            }
            else
            {
                itemContainerBlockInRow = bgbr.ItemContainerGenerator.ContainerFromItem(blockInRowIdentifier);
            }

            BlockInRow bir = itemContainerBlockInRow as BlockInRow;

            if (bir == null)
            {
                return null;
            }

            DependencyObject itemContainerBlockRow;

            if (blockRowIdentifier is int)
            {
                itemContainerBlockRow = bir.ContainerFromIndex((int)blockRowIdentifier);
                BlockRow tmpBr = itemContainerBlockRow as BlockRow;

                if (afterDeletion && (tmpBr == null || tmpBr.Items.Count == 0))
                {
                    int intBlockRowIndex = (int)blockRowIdentifier - 1;

                    while ((tmpBr == null || tmpBr.Items.Count == 0) && intBlockRowIndex > -1)
                    {
                        itemContainerBlockRow = bir.ContainerFromIndex(intBlockRowIndex);
                        tmpBr = itemContainerBlockRow as BlockRow;

                        intBlockRowIndex--;
                    }
                }
            }
            else
            {
                itemContainerBlockRow = bir.ContainerFromItem(blockRowIdentifier);
            }

            BlockRow br = itemContainerBlockRow as BlockRow;

            if (br != null)
            {
                DependencyObject itemContainerFirstCell = br.ItemContainerGenerator.ContainerFromIndex(0);
                bgc = itemContainerFirstCell as BlockGridCell;
            }

            return bgc;
        }


        private void MakeInsertedElementCurrent(BlockGridInsertElementEventArgs e)
        {
            if (!e.Cancel && e.InsertedDataItem != null)
            {
                BlockGridCell bgc = null;

                switch (e.TypeOfElementToInsert)
                {
                    case BlockGridElement.BlockGridBlockRow:
                        bgc = GetCellToActivateAfterInsertOrDelete(e.InsertedDataItem, 0, 0, false,
                            e.TypeOfElementToInsert);
                        break;
                    case BlockGridElement.BlockInRow:
                        bgc = GetCellToActivateAfterInsertOrDelete(e.BlockRowDataContext.BlockGridBlockRowIndex,
                            e.InsertedDataItem, 0, false, e.TypeOfElementToInsert);
                        break;
                    case BlockGridElement.BlockRow:
                        bgc = GetCellToActivateAfterInsertOrDelete(e.BlockRowDataContext.BlockGridBlockRowIndex,
                            e.BlockRowDataContext.BlockInRowIndex, e.InsertedDataItem, false,
                            e.TypeOfElementToInsert);
                        break;
                }

                bgc?.Focus();
            }
        }



        public static readonly RoutedEvent CanDeleteBlockGridElementEvent = EventManager.RegisterRoutedEvent("CanDeleteBlockGridElement", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BlockGrid));

        public event RoutedEventHandler CanDeleteBlockGridElement
        {
            add { AddHandler(CanDeleteBlockGridElementEvent, value); }
            remove { RemoveHandler(CanDeleteBlockGridElementEvent, value); }
        }

        public static readonly RoutedEvent DeleteBlockGridElementEvent = EventManager.RegisterRoutedEvent("DeleteBlockGridElement", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BlockGrid));

        public event RoutedEventHandler DeleteBlockGridElement
        {
            add { AddHandler(DeleteBlockGridElementEvent, value); }
            remove { RemoveHandler(DeleteBlockGridElementEvent, value); }
        }

        protected virtual void OnCanDeleteElement(BlockGridCanDeleteElementEventArgs e)
        {
            this.RaiseEvent(e);
        }


        protected virtual void OnDeleteElement(BlockGridDeleteElementEventArgs e)
        {
            this.RaiseEvent(e);
        }

        protected virtual void OnCanExecuteDeleteBlockGridElement(CanExecuteRoutedEventArgs e)
        {
            BlockGridElement elementToDelete = (BlockGridElement)e.Parameter;

            if (CurrentCellContainer != null && CurrentCellContainer.IsEditing)
            {
                e.CanExecute = CanEndEdit(e);
            }
            else
            {
                e.CanExecute = (CurrentCellContainer != null);
            }

            if (e.CanExecute)
            {
                BlockRowDataCoordinates blockRowDataCoords = new BlockRowDataCoordinates(this.ItemsSource, CurrentCellContainer, false);

                BlockGridCanDeleteElementEventArgs canDelete = new BlockGridCanDeleteElementEventArgs(CanDeleteBlockGridElementEvent, blockRowDataCoords, elementToDelete, e);

                OnCanDeleteElement(canDelete);
                e.CanExecute = !canDelete.Cancel;
            }

        }

        protected virtual void OnExecutedDeleteBlockGridElement(ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                BlockGridElement elementToDelete = (BlockGridElement)e.Parameter;

                BlockRowDataCoordinates blockRowDataCoords = new BlockRowDataCoordinates(this.ItemsSource, CurrentCellContainer, true);

                BlockGridDeleteElementEventArgs deletionArgs = new BlockGridDeleteElementEventArgs(DeleteBlockGridElementEvent, blockRowDataCoords, elementToDelete, e);
                OnDeleteElement(deletionArgs);
#if DEBUG
                AssertDataSetIsValidAfterInsertionOrDeletion(deletionArgs.BlockRowDataContext.BlockGridDataItem);
#endif
                SetCurrentElementAfterDeletion(deletionArgs);
            }
        }

#if DEBUG
        private void AssertDataSetIsValidAfterInsertionOrDeletion(object blockGridDataContext)
        {
            IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>> blockgridData = blockGridDataContext as IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>>;

            foreach (IEnumerable<IDictionary<string, IEnumerable>> blockGridRowData in blockgridData)
            {
                Debug.Assert(blockGridRowData.Count<IDictionary<string, IEnumerable>>() > 0, "A BlockGridBlockRow should hold data to create at least 1 child BlockInRow");

                foreach (IDictionary<string, IEnumerable> blockRowData in blockGridRowData)
                {
                    Debug.Assert(blockRowData.Any(), "A BlockInRow should hold data to create at least 1 child BlockRow");
                }
            }
        }
#endif

        private void SetCurrentElementAfterDeletion(BlockGridDeleteElementEventArgs e)
        {
            if (!e.Cancel)
            {
                int blockgridBlockRowIndex = e.BlockRowDataContext.BlockGridBlockRowIndex;
                int blockInRowIndex = e.BlockRowDataContext.BlockInRowIndex;
                int blockRowIndex = e.BlockRowDataContext.BlockRowIndex;

                var bgc = GetCellToActivateAfterInsertOrDelete(blockgridBlockRowIndex, blockInRowIndex, blockRowIndex, true, e.TypeOfElementActuallyDeleted);
                if (bgc != null)
                {
                    bgc.Focus();
                }
                else
                {
                    SelectCell(null, e);
                }
            }
        }



        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Tab:
                    OnTabKeyDown(e);
                    break;

                case Key.Enter:
                    OnEnterKeyDown(e);
                    break;
            }

            if (!e.Handled)
            {
                base.OnKeyDown(e);
            }
        }

        private void OnTabKeyDown(KeyEventArgs e)
        {
            var currentCellContainer = CurrentCellContainer;
            if (currentCellContainer == null)
            {
                return;
            }

            var wasEditing = currentCellContainer.IsEditing;
            var previous = ((e.KeyboardDevice.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift);

            var startElement = Keyboard.FocusedElement as UIElement;
            var startContentElement = (startElement == null) ? Keyboard.FocusedElement as ContentElement : null;

            if (startElement == null && startContentElement == null)
            {
                return;
            }

            e.Handled = true;

            FocusNavigationDirection direction = previous ? FocusNavigationDirection.Previous : FocusNavigationDirection.Next;
            TraversalRequest request = new TraversalRequest(direction);

            if ((startElement != null && startElement.MoveFocus(request) ||
     startContentElement != null && startContentElement.MoveFocus(request)) &&
    wasEditing && previous && Keyboard.FocusedElement == currentCellContainer)
                currentCellContainer.MoveFocus(request);
        }

        private void OnEnterKeyDown(KeyEventArgs e)
        {
            BlockGridCell currentCellContainer = CurrentCellContainer;
            if (currentCellContainer != null && _columns.Count > 0)
            {
                e.Handled = true;
            }
        }



        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty = ScrollViewer.HorizontalScrollBarVisibilityProperty.AddOwner(typeof(BlockGrid), new FrameworkPropertyMetadata(ScrollBarVisibility.Auto));

        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty = ScrollViewer.VerticalScrollBarVisibilityProperty.AddOwner(typeof(BlockGrid), new FrameworkPropertyMetadata(ScrollBarVisibility.Auto));

        public double HorizontalScrollOffset => (double)GetValue(HorizontalScrollOffsetProperty);

        public static readonly DependencyProperty HorizontalScrollOffsetProperty =
    DependencyProperty.Register("HorizontalScrollOffset", typeof(double), typeof(BlockGrid), new FrameworkPropertyMetadata(0d, OnNotifyHorizontalOffsetPropertyChanged));

        private static void OnNotifyHorizontalOffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BlockGrid)d).ColumnHeadersPresenter.InvalidateHeader();
        }

        private void BindHorizontalOffsetToLocalProperty()
        {
            if (_scrollViewer != null)
            {
                return;
            }

            _scrollViewer = GetTemplateChild(part_ScrollViewer) as ScrollViewer;

            Binding horizontalOffsetBinding = new Binding("ContentHorizontalOffset");
            horizontalOffsetBinding.Source = _scrollViewer;
            SetBinding(HorizontalScrollOffsetProperty, horizontalOffsetBinding);
        }



        private static void OnNotifyColumnHeaderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BlockGrid)d).NotifyPropertyChanged(d, string.Empty, e, BlockGridNotificationTarget.ColumnHeaders);
        }

        internal void NotifyPropertyChanged(DependencyObject d, string propertyName, DependencyPropertyChangedEventArgs e, BlockGridNotificationTarget target)
        {
            if ((target & BlockGridNotificationTarget.ColumnHeadersPresenter) == BlockGridNotificationTarget.ColumnHeadersPresenter && propertyName == "Items")
            {
                ColumnHeadersPresenter.ReEvaluateColumnsHeaders();
            }
            if ((BlockGridHelper.ShouldNotifyColumnHeadersPresenter(target) || BlockGridHelper.ShouldNotifyColumnHeaders(target)) && ColumnHeadersPresenter != null)
            {
                ColumnHeadersPresenter.NotifyPropertyChanged(d, propertyName, e, target);
            }
        }



        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null && !(newValue is IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>>))
                Debug.Assert(false,
                    "This control was designed to handle IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>>");
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (_measureNeverInvoked)
            {
                _measureNeverInvoked = false;

                EnsureItemBindingGroup();
            }
            return base.MeasureOverride(constraint);
        }

        private void EnsureItemBindingGroup()
        {
            if (ItemBindingGroup == null)
            {
                _defaultBindingGroup = new BindingGroup();
                _defaultBindingGroup.Name = "bgbg";
                SetCurrentValue(ItemBindingGroupProperty, _defaultBindingGroup);
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            _isSettingFocus = true;
            base.OnGotFocus(e);

            if (this.EnableFirstRowSelect)
            {
                SelectFirstRowAndSetFocusOnSpecifiedCell();
            }
            _isSettingFocus = false;
        }

        private void SelectFirstRowAndSetFocusOnSpecifiedCell()
        {
            if (this.Items.Count < 1)
            {
                return;
            }

            var blockRow = FindFirstBlockRow();

            if (blockRow == null || blockRow.Items.Count < 1)
            {
                return;
            }

            BlockGridCell cell;

            if (FirstRowCellIndexToFocus > -1)
            {
                cell = blockRow.ItemContainerGenerator.ContainerFromIndex(FirstRowCellIndexToFocus) as BlockGridCell;
            }
            else
            {
                cell = blockRow.ItemContainerGenerator.ContainerFromIndex(0) as BlockGridCell;
            }

            cell?.Focus();
        }

        private BlockRow FindFirstBlockRow()
        {
            var blockGridBlockRow = this.ItemContainerGenerator.ContainerFromIndex(0);

            if (blockGridBlockRow == null)
            {
                return null;
            }

            var presenter = FindVisualChild<BlockRowsPresenter>(blockGridBlockRow);

            if (presenter == null || presenter.Items.Count < 1)
            {
                return null;
            }

            var blockRow = presenter.ItemContainerGenerator.ContainerFromIndex(0) as BlockRow;

            return blockRow;
        }

        public T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

    }
}