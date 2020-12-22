using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockRow : ItemsControl
    {

        static BlockRow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BlockRow), new FrameworkPropertyMetadata(typeof(BlockRow)));

            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(BlockGridCellsPanel));
            ItemsControl.ItemsPanelProperty.OverrideMetadata(typeof(BlockRow), new FrameworkPropertyMetadata(new ItemsPanelTemplate(frameworkElementFactory)));
        }



        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }

        public static readonly DependencyProperty IsEditingProperty =
    DependencyProperty.Register("IsEditing", typeof(bool), typeof(BlockRow), new PropertyMetadata(null));



        private object _item;
        private BlockInRow _owner;
        private double? _forcedHeight;

        private ContainerTracking<BlockGridCell> _cellTrackingTail;
        private BlockRowDataCoordinates _blockRowDataCoords;




        internal bool IsEmptyRow
        {
            get { return (bool)GetValue(IsEmptyRowProperty); }
            set { SetValue(IsEmptyRowProperty, value); }
        }

        public static readonly DependencyProperty IsEmptyRowProperty =
    DependencyProperty.Register("IsEmptyRow", typeof(bool), typeof(BlockRow), new PropertyMetadata(false));



        public object Item
        {
            get
            {
                return _item;
            }

            internal set
            {
                if (_item != value)
                {
                    object oldItem = _item;
                    _item = value;
                }
            }
        }

        private ObservableCollection<BlockGridColumn> Columns
        {
            get
            {
                BlockInRow owningBlockInRow = BlockRowOwner;
                BlockGrid owningDataGrid = (owningBlockInRow != null) ? owningBlockInRow.BlockGrid : null;
                return (owningDataGrid != null) ? owningDataGrid.Columns : null;
            }
        }


        internal BlockInRow BlockRowOwner
        {
            get { return _owner; }
        }

        internal ContainerTracking<BlockGridCell> CellTrackingTail
        {
            get { return _cellTrackingTail; }
        }

        public BlockRowDataCoordinates BlockRowDataContext
        {
            get
            {
                if (_blockRowDataCoords == null)
                {
                    _blockRowDataCoords = new BlockRowDataCoordinates(this);
                }

                return _blockRowDataCoords;
            }
        }

        internal void SetCellTrakingTail(ContainerTracking<BlockGridCell> tail)
        {
            _cellTrackingTail = tail;
        }



        internal void Prepare(object item, BlockInRow blockRowsPresenter)
        {
            _owner = blockRowsPresenter;
            Item = item;
            IsEmptyRow = item == null;

            if (!IsEmptyRow)
            {
                ItemsSource = new MultipleCopiesCollection(Item, Columns.Count(x => x.GetType() != typeof(BlockGridSeparatorColumn)));
                BindingGroup = new BindingGroup();
            }
        }



        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is BlockGridCell);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BlockGridCell();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            BlockGridCell cell = element as BlockGridCell;

            if (cell != null)
            {
                var index = ItemContainerGenerator.IndexFromContainer(element);
                Debug.Assert(index < Columns.Count, "Out of range exception due to occur");
                cell.PrepareCell(item, this, Columns[index] as BlockGridCellColumn);
                cell.Tracker.StartTracking(ref _cellTrackingTail);
            }
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            BlockGridCell cell = (BlockGridCell)element;

            if (cell != null)
            {
                cell.Tracker.StopTracking(ref _cellTrackingTail);
                cell.ClearCell(this);
            }
        }






        internal double ForcedHeight { set { _forcedHeight = value; } }

        protected override Size MeasureOverride(Size constraint)
        {
            Size sizeBasedOnContent = base.MeasureOverride(constraint);

            if (_forcedHeight.HasValue)
            {
                sizeBasedOnContent.Height = _forcedHeight.Value;
            }
            else
            {
                BlockRowsPresenter parentBlockRowPresenter = BlockGridHelper.FindVisualParent<BlockRowsPresenter>(this) as BlockRowsPresenter;

                if (parentBlockRowPresenter != null)
                {
                    int rowIndex = parentBlockRowPresenter.BlockRowsInBlock.IndexOf(this);

                    if (rowIndex != -1)
                    {
                        BlockGridBlockRow parentGridRow = BlockGridHelper.FindVisualParent<BlockGridBlockRow>(this) as BlockGridBlockRow;
                        if (parentGridRow != null)
                        {
                            parentGridRow.SetBlockRowMaxRowHeight(rowIndex, sizeBasedOnContent.Height);
                        }
                    }
                }
            }

            return sizeBasedOnContent;
        }


    }
}
