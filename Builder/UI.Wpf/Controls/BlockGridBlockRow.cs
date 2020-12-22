using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Controls
{


    public class BlockGridBlockRow : ItemsControl
    {
        static BlockGridBlockRow()
        {
            var ownerType = typeof(BlockGridBlockRow);
            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
        }

        Dictionary<string, int> _nrMaxOfRowsPerKey;
        private List<double> _blockRowsMaxRowHeight = new List<double>();
        private BlockGrid _owner;
        private ContainerTracking<BlockInRow> _blockRowTrackingTail;

        internal void SetBlockRowMaxRowHeight(int rowIndex, double rowHeight)
        {
            if (rowIndex >= _blockRowsMaxRowHeight.Count)
            {
                for (int i = _blockRowsMaxRowHeight.Count; i <= rowIndex; i++)
                {
                    _blockRowsMaxRowHeight.Add(double.PositiveInfinity);
                }
            }

            if (double.IsPositiveInfinity(_blockRowsMaxRowHeight[rowIndex]) || rowHeight > _blockRowsMaxRowHeight[rowIndex])
            {
                _blockRowsMaxRowHeight[rowIndex] = rowHeight;
            }
        }

        internal double GetBlockRowMaxRowHeight(int rowIndex)
        {
            double rowHeight = double.PositiveInfinity;

            Debug.Assert(rowIndex < _blockRowsMaxRowHeight.Count);

            if (rowIndex < _blockRowsMaxRowHeight.Count)
            {
                rowHeight = _blockRowsMaxRowHeight[rowIndex];
            }

            return rowHeight;
        }



        internal bool BlockInRowHasSuccessor(BlockInRow blockInRow)
        {
            int indexOfBlockInRow = this.ItemContainerGenerator.IndexFromContainer(blockInRow);
            return (indexOfBlockInRow > -1 && (indexOfBlockInRow < (this.ItemContainerGenerator.Items.Count - 1)));
        }



        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is BlockInRow);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BlockInRow();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            BlockInRow blockInRow = element as BlockInRow;


            IDictionary<string, IEnumerable> data = item as IDictionary<string, IEnumerable>;

            System.Collections.Specialized.INotifyCollectionChanged notifyingCollection = item as System.Collections.Specialized.INotifyCollectionChanged;
            if (notifyingCollection != null)
            {
                notifyingCollection.CollectionChanged += BlockInRowData_CollectionChanged;
            }

            IDictionary<string, IEnumerable> equalizedRowsPerKey = AdjustDataForRowsPerKey(data);
            if (blockInRow != null)
            {
                var elements = equalizedRowsPerKey.Keys.SelectMany(k => equalizedRowsPerKey[k].Cast<object>()).ToList();
                blockInRow.PrepareBlock(elements, this);


                blockInRow.Tracker.StartTracking(ref _blockRowTrackingTail);

                BlockGridBlockRowOwner.NotifyPropertyChanged(this, "Items", new DependencyPropertyChangedEventArgs(), BlockGridNotificationTarget.ColumnHeadersPresenter);
            }
        }

        void BlockInRowData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add || e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                DetermineMaxNumberOfElementsPerKey();
                this.Items.Refresh();
            }
        }



        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            BlockInRow blockInRow = element as BlockInRow;

            if (blockInRow != null)
            {
                blockInRow.Tracker.StopTracking(ref _blockRowTrackingTail);

                DetermineMaxNumberOfElementsPerKey();

                System.Collections.Specialized.INotifyCollectionChanged notifyingCollection = item as System.Collections.Specialized.INotifyCollectionChanged;
                if (notifyingCollection != null)
                {
                    notifyingCollection.CollectionChanged -= BlockInRowData_CollectionChanged;
                }


                ContainerTracking<BlockInRow> walk = _blockRowTrackingTail;
                while (walk != null)
                {
                    BlockInRowSeparator separator = BlockGridHelper.FindFirstVisualChild<BlockInRowSeparator>(walk.Container);

                    if (separator != null)
                    {
                        separator.IsInBetweenBlocks = BlockInRowHasSuccessor(walk.Container);
                    }

                    walk = walk.Next;
                }

                BlockGridBlockRowOwner.NotifyPropertyChanged(this, "Items", new DependencyPropertyChangedEventArgs(), BlockGridNotificationTarget.ColumnHeadersPresenter);
            }

            base.ClearContainerForItemOverride(element, item);
        }



        private IDictionary<string, IEnumerable> AdjustDataForRowsPerKey(IDictionary<string, IEnumerable> data)
        {
            var adjustedData = new Dictionary<string, IEnumerable>();
            foreach (var key in data.Keys)
            {
                Debug.Assert(MaxNumberOfElementsPerKey.ContainsKey(key), "Should Not Occur!");

                var nrRowsForKey = MaxNumberOfElementsPerKey[key];
                adjustedData.Add(key, new BlockRowEqualizationCollection(data[key], nrRowsForKey));
            }

            return adjustedData;
        }

        private Dictionary<string, int> MaxNumberOfElementsPerKey
        {
            get
            {
                if (_nrMaxOfRowsPerKey == null)
                {
                    DetermineMaxNumberOfElementsPerKey();
                    Debug.Assert(_nrMaxOfRowsPerKey != null);
                }
                return _nrMaxOfRowsPerKey;
            }
        }

        private void DetermineMaxNumberOfElementsPerKey()
        {
            _nrMaxOfRowsPerKey = new Dictionary<string, int>();
            var blocksData = this.ItemsSource as IEnumerable<IDictionary<string, IEnumerable>>;

            foreach (var blockData in blocksData)
            {
                foreach (var key in blockData.Keys)
                {
                    OfferCountAsMaxForKey(key, blockData[key]);
                }
            }
        }

        private void OfferCountAsMaxForKey(string key, IEnumerable e)
        {
            var numberOfElements = e.Cast<object>().Count();

            if (_nrMaxOfRowsPerKey.ContainsKey(key))
            {
                _nrMaxOfRowsPerKey[key] = Math.Max(_nrMaxOfRowsPerKey[key], numberOfElements);
            }
            else
            {
                _nrMaxOfRowsPerKey.Add(key, numberOfElements);
            }
        }


        internal BlockGrid BlockGridBlockRowOwner
        {
            get { return _owner; }
            set { _owner = value; }
        }



        internal void PrepareBlockGridBlockRow(IEnumerable<IDictionary<string, IEnumerable>> data, BlockGrid blockGrid)
        {
            ItemsSource = data;
            _owner = blockGrid;
        }

    }
}
