using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridCell : ContentControl
    {

        static BlockGridCell()
        {
            var ownerType = typeof(BlockGridCell);
            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));

            StyleProperty.OverrideMetadata(typeof(BlockGridCell), new FrameworkPropertyMetadata(null, OnNotifyPropertyChanged, OnCoerceStyle));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(BlockGridCell), new FrameworkPropertyMetadata(KeyboardNavigationMode.Local));

            EventManager.RegisterClassHandler(ownerType, MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDownEvent));
            EventManager.RegisterClassHandler(ownerType, GotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(OnGotKeyboardFocus));
            EventManager.RegisterClassHandler(ownerType, LostFocusEvent, new RoutedEventHandler(OnAnyLostFocus), true);
        }

        private static void OnMouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            BlockGridCell cell = sender as BlockGridCell;
            if (cell != null)
            {
                cell.BlockGridOwner.SelectCell(cell, e);
                e.Handled = true;
            }

            e.Handled = true;
        }

        private static void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            BlockGridCell cell = sender as BlockGridCell;
            if (cell != null && cell != cell.BlockGridOwner.CurrentCellContainer)
            {
                cell.BlockGridOwner.SelectCell(cell, e);
                e.Handled = true;
            }
        }

        private static void OnAnyLostFocus(object sender, RoutedEventArgs e)
        {
            BlockGridCell cell = BlockGridHelper.FindVisualParent<BlockGridCell>(e.OriginalSource as UIElement);
            if (cell != null && cell == sender)
            {
                BlockGrid owner = cell.BlockGridOwner;
                if (owner != null && !cell.IsKeyboardFocusWithin && owner.CurrentCellContainer == cell)
                {
                    owner.SelectCell(null, e);
                }
            }

        }

        private ContainerTracking<BlockGridCell> _tracker;
        private BlockRow _cellOwner;


        public BlockGridCell()
        {
            _tracker = new ContainerTracking<BlockGridCell>(this);
        }



        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
    DependencyProperty.Register("IsSelected", typeof(bool), typeof(BlockGridCell), new PropertyMetadata(false));

        public BlockGridCellColumn Column
        {
            get { return (BlockGridCellColumn)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        public static readonly DependencyProperty ColumnProperty =
    DependencyProperty.Register("Column", typeof(BlockGridCellColumn), typeof(BlockGridCell), new PropertyMetadata(null));

        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }

        public static readonly DependencyProperty IsEditingProperty =
            DependencyProperty.Register("IsEditing", typeof(bool), typeof(BlockGridCell), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsEditingChanged)));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
        }

        private static readonly DependencyPropertyKey IsReadOnlyPropertyKey =
            DependencyProperty.RegisterReadOnly("IsReadOnly", typeof(bool), typeof(BlockGridCell), new FrameworkPropertyMetadata(false, OnNotifyIsReadOnlyChanged, OnCoerceIsReadOnly));

        public static readonly DependencyProperty IsReadOnlyProperty = IsReadOnlyPropertyKey.DependencyProperty;


        internal void PrepareCell(object item, BlockRow blockRow, BlockGridCellColumn column)
        {
            Column = column;
            _cellOwner = blockRow;

            if ((Content as FrameworkElement) == null)
            {
                BuildVisualTree();

                if (!NeedsVisualTree)
                {
                    Content = item;
                }
            }
        }

        internal void ClearCell(BlockRow blockRow)
        {
            Debug.Assert(Object.ReferenceEquals(blockRow, _cellOwner), "Owner has changed???");
            _cellOwner = null;
        }



        private static object OnCoerceStyle(DependencyObject d, object baseValue)
        {
            var cell = d as BlockGridCell;
            return BlockGridHelper.GetCoercedTransferPropertyValue(
                cell,
                baseValue,
                StyleProperty,
                cell.Column,
                BlockGridCellColumn.CellStyleProperty,
                cell.BlockGridOwner,
                BlockGrid.CellStyleProperty);
        }

        private static object OnCoerceIsReadOnly(DependencyObject d, object baseValue)
        {
            var cell = d as BlockGridCell;
            var column = cell.Column;
            var dataGrid = cell.BlockGridOwner;

            return BlockGridHelper.GetCoercedTransferPropertyValue(
column,
column.IsReadOnly,
BlockGridCellColumn.IsReadOnlyProperty,
dataGrid,
BlockGrid.IsReadOnlyProperty);
        }



        internal object RowDataItem
        {
            get
            {
                BlockRow row = _cellOwner;
                if (row != null)
                {
                    return row.Item;
                }

                return DataContext;
            }
        }

        internal BlockRow BlockGridCellOwner
        {
            get
            {
                return _cellOwner;
            }
        }

        internal BlockGrid BlockGridOwner
        {
            get
            {
                if (_cellOwner == null)
                {
                    return null;
                }

                BlockGrid blockGridOwner = (_cellOwner.BlockRowOwner == null) ? null : _cellOwner.BlockRowOwner.BlockGrid;

                return blockGridOwner;
            }
        }

        internal ContainerTracking<BlockGridCell> Tracker
        {
            get { return _tracker; }
        }



        internal void BuildVisualTree()
        {
            if (NeedsVisualTree)
            {
                if (Column != null)
                {
                    var bindingGroup = BindingGroup;
                    if (bindingGroup != null && bindingGroup.IsDirty)
                    {
                        bindingGroup.UpdateSources();

                        if (Content != null)
                        {
                            FrameworkElement fwe = Content as FrameworkElement;
                            if (fwe != null && fwe.BindingGroup != null)
                            {
                                fwe.BindingGroup.BindingExpressions.Clear();
                            }
                        }
                    }

                    Content = Column.BuildVisualTree(IsEditing, RowDataItem, this);
                }
            }

            BlockGridHelper.TransferProperty(this, StyleProperty);
            BlockGridHelper.TransferProperty(this, IsReadOnlyProperty);
        }

        private bool NeedsVisualTree
        {
            get
            {
                return (ContentTemplate == null) && (ContentTemplateSelector == null);
            }
        }

        internal void SyncIsSelected(bool isSelected)
        {
            IsSelected = isSelected;
        }

        protected virtual void OnIsEditingChanged(bool isEditing)
        {
            if (IsKeyboardFocusWithin && !IsKeyboardFocused)
            {
                Focus();
            }

            BuildVisualTree();
        }

        internal void BeginEdit(RoutedEventArgs e)
        {
            Debug.Assert(!IsEditing, "Should not call BeginEdit when IsEditing is true.");

            IsEditing = true;

            BlockGridCellColumn column = Column;
            if (column != null)
            {
                column.BeginEdit(Content as FrameworkElement, e);
            }

        }


        internal void CancelEdit()
        {
            Debug.Assert(IsEditing, "Should not call CancelEdit when IsEditing is false.");

            BlockGridCellColumn column = Column;
            if (column != null)
            {
                column.CancelEdit(Content as FrameworkElement);
            }

            IsEditing = false;
        }

        internal bool CommitEdit()
        {
            Debug.Assert(IsEditing, "Should not call CommitEdit when IsEditing is false.");

            bool validationPassed = true;
            BlockGridCellColumn column = Column;
            if (column != null)
            {
                validationPassed = column.CommitEdit(Content as FrameworkElement);
            }

            if (validationPassed)
            {
                IsEditing = false;
            }

            return validationPassed;
        }




        private static void OnIsEditingChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((BlockGridCell)sender).OnIsEditingChanged((bool)e.NewValue);
        }

        private static void OnNotifyIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cell = (BlockGridCell)d;
            var blockGrid = cell.BlockGridOwner;
            if ((bool)e.NewValue && blockGrid != null)
            {
                blockGrid.CancelEdit(cell);
            }

            CommandManager.InvalidateRequerySuggested();

            cell.NotifyPropertyChanged(d, string.Empty, e, BlockGridNotificationTarget.Cells);
        }

        private static void OnNotifyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BlockGridCell)d).NotifyPropertyChanged(d, string.Empty, e, BlockGridNotificationTarget.Cells);
        }

        internal void NotifyPropertyChanged(DependencyObject d, string propertyName, DependencyPropertyChangedEventArgs e, BlockGridNotificationTarget target)
        {
            BlockGridCellColumn column = d as BlockGridCellColumn;
            if ((column != null) && (column != Column))
            {
                return;
            }

            if (BlockGridHelper.ShouldNotifyCells(target) && (e.Property == BlockGrid.CellStyleProperty || e.Property == BlockGridCellColumn.CellStyleProperty || e.Property == StyleProperty))
            {
                BlockGridHelper.TransferProperty(this, StyleProperty);
            }
        }


        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            SendInputToColumn(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            SendInputToColumn(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            SendInputToColumn(e);
        }

        private void SendInputToColumn(InputEventArgs e)
        {
            var column = Column;
            if (column != null)
            {
                column.OnInput(e);
            }
        }


        internal void SimulateSelect()
        {
            Keyboard.Focus(this);
            BlockGridCell.OnMouseLeftButtonDownEvent(this, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left) { RoutedEvent = Button.ClickEvent });
        }
    }
}
