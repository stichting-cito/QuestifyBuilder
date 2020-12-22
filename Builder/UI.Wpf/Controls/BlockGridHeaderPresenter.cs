using System;
using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Controls
{

    public class BlockGridHeaderPresenter : ItemsControl
    {


        internal const string part_HeaderCellPanel = "PART_HeaderCellPanel";



        static BlockGridHeaderPresenter()
        {
            Type ownerType = typeof(BlockGridHeaderPresenter);
            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
        }


        BlockGrid _parentBlockGrid;
        private ContainerTracking<BlockGridColumnHeader> _headerTrackingTail;


        internal void NotifyPropertyChanged(DependencyObject d, string propertyName, DependencyPropertyChangedEventArgs e, BlockGridNotificationTarget target)
        {
            BlockGridColumn column = d as BlockGridColumn;

            if (BlockGridHelper.ShouldNotifyColumnHeaders(target))
            {
                if (e.Property != BlockGridColumn.HeaderProperty)
                {
                    ContainerTracking<BlockGridColumnHeader> tracker = _headerTrackingTail;

                    while (tracker != null)
                    {
                        tracker.Container.NotifyPropertyChanged(d, e);
                        tracker = tracker.Next;
                    }
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ParentBlockGrid != null)
            {
                ItemsSource = new RepeatedBlockGridColumnCollection(ParentBlockGrid);

                ParentBlockGrid.ColumnHeadersPresenter = this;
            }
            else
            {
                ItemsSource = null;
            }
        }


        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is BlockGridColumnHeader);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BlockGridColumnHeader();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            BlockGridColumnHeader columnHeader = element as BlockGridColumnHeader;

            if (columnHeader != null)
            {
                if (columnHeader.Column == null)
                {
                    columnHeader.Tracker.StartTracking(ref _headerTrackingTail);
                }
                columnHeader.setColumnData((BlockGridColumn)item);
            }
        }



        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            BlockGridColumnHeader columnHeader = element as BlockGridColumnHeader;

            base.ClearContainerForItemOverride(element, item);

            columnHeader.Tracker.StopTracking(ref _headerTrackingTail);
            columnHeader.ClearHeader();
        }


        internal void ReEvaluateColumnsHeaders()
        {
            RepeatedBlockGridColumnCollection columnCollection = this.ItemsSource as RepeatedBlockGridColumnCollection;

            if (columnCollection != null)
            {
                columnCollection.ReEvaluateColumnCollection();
            }
        }


        internal BlockGrid ParentBlockGrid
        {
            get
            {
                if (_parentBlockGrid == null)
                {
                    _parentBlockGrid = BlockGridHelper.FindParent<BlockGrid>(this);
                }
                return _parentBlockGrid;
            }
        }


        internal void InvalidateHeader()
        {
            this.InvalidateArrange();
            if (InternalItemsHost != null)
            {
                InternalItemsHost.InvalidateMeasure();
                InternalItemsHost.InvalidateArrange();
            }
        }

        internal BlockGridCellsPanel InternalItemsHost { get; set; }
    }
}
