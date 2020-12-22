using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Questify.Builder.UI.Wpf.Controls
{

    public class BlockInRow : Control
    {
        static BlockInRow()
        {
            var ownerType = typeof(BlockInRow);
            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));

        }

        private BlockGridBlockRow _owner;
        private ContainerTracking<BlockInRow> _tracker;
        private BlockRowsPresenter _blockrowsPresenter;


        public BlockInRow()
        {
            _tracker = new ContainerTracking<BlockInRow>(this);
        }



        public IEnumerable BlockData
        {
            get { return (IEnumerable)GetValue(BlockDataProperty); }
            set { SetValue(BlockDataProperty, value); }
        }

        public static readonly DependencyProperty BlockDataProperty =
            DependencyProperty.Register("BlockData", typeof(IEnumerable), typeof(BlockInRow), new PropertyMetadata(null));
        public bool HasBlockSeparator
        {
            get { return (bool)GetValue(HasBlockSeparatorProperty); }
            set { SetValue(HasBlockSeparatorProperty, value); }
        }

        public static readonly DependencyProperty HasBlockSeparatorProperty =
            DependencyProperty.Register("HasBlockSeparator", typeof(bool), typeof(BlockInRow), new PropertyMetadata(false));



        protected override Size MeasureOverride(Size constraint)
        {
            var parent = BlockGridHelper.FindVisualParent<BlockGrid>(this);
            if (parent != null)
            {
                double sumHeightChildren = 0;

                for (int i = 0; i < this.VisualChildrenCount; i++)
                {
                    var visualChild = this.GetVisualChild(i);

                    if (visualChild is UIElement)
                    {
                        UIElement uiChild = (UIElement)visualChild;
                        uiChild.Measure(constraint);
                        sumHeightChildren += uiChild.DesiredSize.Height;
                    }
                }

                return new Size(parent.Columns.Sum(column => column.Width), sumHeightChildren);
            }

            return new Size(10, 10);
        }


        internal void PrepareBlock(IEnumerable elements, BlockGridBlockRow blockGridBlockRow)
        {
            BlockData = elements;
            _owner = blockGridBlockRow;

            HasBlockSeparator = (_owner.BlockGridBlockRowOwner.Columns.FirstOrDefault(x => x.GetType() == typeof(BlockGridSeparatorColumn)) != null);
        }


        private BlockRowsPresenter MyBlockRowsPresenter
        {
            get
            {
                if (_blockrowsPresenter == null)
                {
                    _blockrowsPresenter = BlockGridHelper.FindFirstVisualChild<BlockRowsPresenter>(this);
                }

                return _blockrowsPresenter;
            }
        }

        internal int IndexOf(object item)
        {
            int index = -1;

            if (MyBlockRowsPresenter != null)
            {
                index = MyBlockRowsPresenter.Items.IndexOf(item);
            }

            return index;
        }

        internal DependencyObject ContainerFromIndex(int index)
        {
            DependencyObject dobject = null;

            if (MyBlockRowsPresenter != null)
            {
                dobject = MyBlockRowsPresenter.ItemContainerGenerator.ContainerFromIndex(index);
            }

            return dobject;
        }

        internal DependencyObject ContainerFromItem(object item)
        {
            DependencyObject dobject = null;

            if (MyBlockRowsPresenter != null)
            {
                int index = 0;
                int blockRowIndex = -1;

                foreach (object value in BlockData)
                {
                    if (value != null && value.Equals(item))
                    {
                        blockRowIndex = index;
                        break;
                    }
                    index++;
                }

                if (blockRowIndex > -1)
                {
                    dobject = MyBlockRowsPresenter.ItemContainerGenerator.ContainerFromIndex(blockRowIndex);
                }
            }

            return dobject;
        }



        private IEditableCollectionView EditableItems
        {
            get
            {
                if (MyBlockRowsPresenter != null)
                {
                    return (IEditableCollectionView)MyBlockRowsPresenter.Items;
                }

                return null;
            }
        }

        internal bool IsAddingNewItem
        {
            get { return EditableItems.IsAddingNew; }
        }

        internal bool IsEditingRowItem
        {
            get { return EditableItems.IsEditingItem; }
        }

        internal bool IsAddingOrEditingBlockRowItem(object item)
        {
            return IsEditingItem(item) ||
                (IsAddingNewItem && (EditableItems.CurrentAddItem == item));
        }

        internal bool IsAddingOrEditingBlockRowItem(BlockGridEditingUnit editingUnit, object item)
        {
            return (editingUnit == BlockGridEditingUnit.Row) &&
                    IsAddingOrEditingBlockRowItem(item);
        }

        private bool IsEditingItem(object item)
        {
            return IsEditingRowItem && (EditableItems.CurrentEditItem == item);
        }

        internal void EditBlockRowItem(object rowItem)
        {
            EditableItems.EditItem(rowItem);

            CommandManager.InvalidateRequerySuggested();
        }

        internal void CommitBlockRowItem()
        {

            if (IsEditingRowItem)
            {
                EditableItems.CommitEdit();
            }
            else
            {
                EditableItems.CommitNew();

            }
        }

        internal void CancelBlockRowItem()
        {

            if (IsEditingRowItem)
            {
                if (EditableItems.CanCancelEdit)
                {
                    EditableItems.CancelEdit();
                }
                else
                {
                    EditableItems.CommitEdit();
                }
            }







        }



        internal BlockGridBlockRow BlockInRowOwner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public BlockGrid BlockGrid
        {
            get { return _owner.BlockGridBlockRowOwner; }
        }

        internal ContainerTracking<BlockInRow> Tracker
        {
            get { return _tracker; }
        }

    }
}
