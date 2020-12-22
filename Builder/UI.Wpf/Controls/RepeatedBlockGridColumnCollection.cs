using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class RepeatedBlockGridColumnCollection : System.Collections.ObjectModel.ObservableCollection<BlockGridColumn>
    {
        private BlockGrid _grid;
        private int _currentMaxBlockInRows;

        public RepeatedBlockGridColumnCollection(BlockGrid grid)
        {
            _grid = grid;

            var itemList = (IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>>)grid.ItemsSource;

            if (itemList != null && itemList.Count<IEnumerable<IDictionary<string, IEnumerable>>>() > 0)
            {
                var max = itemList.Max(sublist => sublist.Count<IDictionary<string, IEnumerable>>());

                for (int i = 0; i < max; i++)
                {
                    AddBlockOfColumns();
                }

                _currentMaxBlockInRows = max;
            }
        }

        internal void ReEvaluateColumnCollection()
        {
            IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>> blockGridItems = (IEnumerable<IEnumerable<IDictionary<string, IEnumerable>>>)_grid.ItemsSource;
            int newMaxBlockInRows = 0;

            if (blockGridItems != null && blockGridItems.Count<IEnumerable<IDictionary<string, IEnumerable>>>() > 0)
            {
                newMaxBlockInRows = blockGridItems.Max(sublist => sublist.Count<IDictionary<string, IEnumerable>>());
            }

            if (newMaxBlockInRows != _currentMaxBlockInRows)
            {
                if (newMaxBlockInRows > _currentMaxBlockInRows)
                {
                    for (int i = _currentMaxBlockInRows; i < newMaxBlockInRows; i++)
                    {
                        AddBlockOfColumns();
                    }
                }
                else if (newMaxBlockInRows < _currentMaxBlockInRows)
                {
                    for (int i = _currentMaxBlockInRows; i > newMaxBlockInRows; i--)
                    {
                        RemoveBlockOfColumns();
                    }
                }
                _currentMaxBlockInRows = newMaxBlockInRows;
            }
        }


        private void AddBlockOfColumns()
        {
            foreach (var column in _grid.Columns)
            {
                this.Add(column);
            }
        }

        private void RemoveBlockOfColumns()
        {
            for (int i = 0; i < _grid.Columns.Count; i++)
            {
                this.RemoveAt(this.Count - 1);
            }
        }
    }
}
