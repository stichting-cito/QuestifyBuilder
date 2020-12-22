using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockGridColumnCollection : ObservableCollection<BlockGridColumn>
    {

        readonly BlockGrid _owner;
        private bool _separatorColumnAdded;

        public BlockGridColumnCollection(BlockGrid owner)
        {
            Debug.Assert(owner != null, "A valid BlockGrid is necessary");
            _owner = owner;
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (_separatorColumnAdded)
                {
                    throw new InvalidOperationException("In a BlockGrid it is not allowed to add columns after the separator column has been added.");
                }

                _separatorColumnAdded = _separatorColumnAdded || (e.NewItems[0] is BlockGridSeparatorColumn);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                if (e.NewItems[0] is BlockGridSeparatorColumn)
                {
                    _separatorColumnAdded = false;
                }
            }

            base.OnCollectionChanged(e);
        }

    }
}
