using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridCellsPanel : Panel
    {
        static BlockGridCellsPanel()
        {
        }


        private BlockGrid _parentBlockGrid;
        BlockGridHeaderPresenter _headersPresenter;



        public BlockGridCellsPanel()
        {
        }



        protected override Size MeasureOverride(Size constraint)
        {
            var measureSize = new Size();
            var parentBlockGrid = ParentBlockGrid;

            IList children = InternalChildren;

            if (!DoubleUtil.AreClose(DesiredSize, measureSize))
            {
                ParentPresenter.InvalidateMeasure();
                var parent = VisualTreeHelper.GetParent(this) as UIElement;
                parent?.InvalidateMeasure();
            }

            var beingUsedForColumnHeaderDisplay = ParentPresenter is BlockGridHeaderPresenter;
            var desiredBlockWidth = parentBlockGrid.Columns.Where(x => beingUsedForColumnHeaderDisplay || x.GetType() != typeof(BlockGridSeparatorColumn)).Sum(column => column.Width);
            var cellsPanelHeight = beingUsedForColumnHeaderDisplay ? parentBlockGrid.ColumnHeaderHeight : 25;

            return new Size(desiredBlockWidth * BlockRepeatCount(), cellsPanelHeight);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var parentBlockGrid = ParentBlockGrid;

            if (parentBlockGrid == null)
            {
                return arrangeSize;
            }


            var lastRectangle = new Rect();

            if (ParentPresenter is BlockGridHeaderPresenter)
            {
                lastRectangle.X = -parentBlockGrid.HorizontalScrollOffset;
            }

            for (int i = 0; i < Children.Count; i++)
            {
                lastRectangle = ArrangeChild(Children[i], i, lastRectangle);
            }

            var beingUsedForColumnHeaderDisplay = ParentPresenter is BlockGridHeaderPresenter;
            var desiredBlockWidth = parentBlockGrid.Columns.Where(x => beingUsedForColumnHeaderDisplay || x.GetType() != typeof(BlockGridSeparatorColumn)).Sum(column => column.Width);
            var cellsPanelHeight = beingUsedForColumnHeaderDisplay ? parentBlockGrid.ColumnHeaderHeight : 25;

            return new Size(desiredBlockWidth * BlockRepeatCount(), cellsPanelHeight);
        }

        private Rect ArrangeChild(UIElement uIElement, int i, Rect lastRectangle)
        {
            BlockGrid parentBlockGrid = ParentBlockGrid;
            bool beingUsedForColumnHeaderDisplay = (ParentPresenter is BlockGridHeaderPresenter);
            double cellsPanelHeight = beingUsedForColumnHeaderDisplay ? parentBlockGrid.ColumnHeaderHeight : 25;
            var mod = parentBlockGrid.Columns.Count;
            var rectangle = new Rect(lastRectangle.Right, 0, parentBlockGrid.Columns[i % mod].Width, cellsPanelHeight);

            uIElement.Arrange(rectangle);

            return rectangle;
        }

        private int BlockRepeatCount()
        {
            if (ParentPresenter is BlockGridHeaderPresenter)
            {
                var blockGridItems = ParentBlockGrid.RowsOfBlocks?.ToList();

                if (blockGridItems == null)
                {
                    return 1;
                }

                var repeatedBlocks = !blockGridItems.Any() ? 0 : blockGridItems.Max(blocksOnRow => blocksOnRow.Count());
                return repeatedBlocks;
            }
            return 1;
        }



        private BlockGrid ParentBlockGrid
        {
            get
            {
                if (_parentBlockGrid == null)
                {

                    BlockRow presenter = ParentPresenter as BlockRow;

                    if (presenter != null)
                    {
                        var row = presenter.BlockRowOwner;
                        if (row != null)
                        {
                            _parentBlockGrid = row.BlockGrid;
                        }
                    }
                    else
                    {
                        _headersPresenter = ParentPresenter as BlockGridHeaderPresenter;

                        if (_headersPresenter != null)
                        {
                            _headersPresenter.InternalItemsHost = this;
                            _parentBlockGrid = _headersPresenter.ParentBlockGrid;
                        }
                    }
                }

                return _parentBlockGrid;
            }
        }

        private ItemsControl ParentPresenter
        {
            get
            {
                FrameworkElement itemsPresenter = TemplatedParent as FrameworkElement;
                if (itemsPresenter != null)
                {
                    return itemsPresenter.TemplatedParent as ItemsControl;
                }

                return null;
            }
        }


    }
}