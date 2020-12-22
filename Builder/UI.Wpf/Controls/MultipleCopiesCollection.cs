using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace Questify.Builder.UI.Wpf.Controls
{
    internal class MultipleCopiesCollection :
    IList,
    ICollection,
    IEnumerable,
    INotifyCollectionChanged,
    INotifyPropertyChanged
    {

        internal MultipleCopiesCollection(object item, int count)
        {
            Debug.Assert(item != null, "item should not be null.");
            Debug.Assert(count >= 0, "count should not be negative.");

            CopiedItem = item;
            _count = count;
        }



        internal void MirrorCollectionChange(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Debug.Assert(
                        e.NewItems.Count == 1,
                        "We're mirroring the Columns collection which is an ObservableCollection and only supports adding one item at a time");
                    Insert(e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Move:
                    Debug.Assert(
                        e.NewItems.Count == 1,
                        "We're mirroring the Columns collection which is an ObservableCollection and only supports moving one item at a time");
                    Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    Debug.Assert(
                        e.OldItems.Count == 1,
                        "We're mirroring the Columns collection which is an ObservableCollection and only supports removing one item at a time");
                    RemoveAt(e.OldStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    Debug.Assert(
                        e.NewItems.Count == 1,
                        "We're mirroring the Columns collection which is an ObservableCollection and only supports replacing one item at a time");
                    OnReplace(CopiedItem, CopiedItem, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Reset();
                    break;
            }
        }

        internal void SyncToCount(int newCount)
        {
            int oldCount = RepeatCount;

            if (newCount != oldCount)
            {
                if (newCount > oldCount)
                {
                    InsertRange(oldCount, newCount - oldCount);
                }
                else
                {
                    int numToRemove = oldCount - newCount;
                    RemoveRange(oldCount - numToRemove, numToRemove);
                }

                Debug.Assert(RepeatCount == newCount, "We should have properly updated the RepeatCount");
            }
        }

        internal object CopiedItem
        {
            get
            {
                return _item;
            }

            set
            {
                if (_item != value)
                {
                    object oldValue = _item;
                    _item = value;

                    OnPropertyChanged(IndexerName);

                    for (int i = 0; i < _count; i++)
                    {
                        OnReplace(oldValue, _item, i);
                    }
                }
            }
        }

        private int RepeatCount
        {
            get
            {
                return _count;
            }

            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged(CountName);
                    OnPropertyChanged(IndexerName);
                }
            }
        }

        private void Insert(int index)
        {
            RepeatCount++;
            OnCollectionChanged(NotifyCollectionChangedAction.Add, CopiedItem, index);
        }

        private void InsertRange(int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Insert(index);
                index++;
            }
        }

        private void Move(int oldIndex, int newIndex)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, CopiedItem, newIndex, oldIndex));
        }

        private void RemoveAt(int index)
        {
            Debug.Assert((index >= 0) && (index < RepeatCount), "Index out of range");

            RepeatCount--;
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, CopiedItem, index);
        }

        private void RemoveRange(int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveAt(index);
            }
        }

        private void OnReplace(object oldItem, object newItem, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, index));
        }

        private void Reset()
        {
            RepeatCount = 0;
            OnCollectionReset();
        }



        public int Add(object value)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return _item == value;
        }

        public int IndexOf(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (_item == value) ? 0 : -1;
        }

        public void Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public void Remove(object value)
        {
            throw new NotSupportedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public object this[int index]
        {
            get
            {
                if ((index >= 0) && (index < RepeatCount))
                {
                    Debug.Assert(_item != null, "_item should be non-null.");
                    return _item;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
            }

            set
            {
                throw new InvalidOperationException();
            }
        }



        public void CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }
        }



        public IEnumerator GetEnumerator()
        {
            return new MultipleCopiesCollectionEnumerator(this);
        }

        private class MultipleCopiesCollectionEnumerator : IEnumerator
        {
            public MultipleCopiesCollectionEnumerator(MultipleCopiesCollection collection)
            {
                _collection = collection;
                _item = _collection.CopiedItem;
                _count = _collection.RepeatCount;
                _current = -1;
            }


            object IEnumerator.Current
            {
                get
                {
                    if (_current >= 0)
                    {
                        if (_current < _count)
                        {
                            return _item;
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            bool IEnumerator.MoveNext()
            {
                if (IsCollectionUnchanged)
                {
                    int newIndex = _current + 1;
                    if (newIndex < _count)
                    {
                        _current = newIndex;
                        return true;
                    }

                    return false;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            void IEnumerator.Reset()
            {
                if (IsCollectionUnchanged)
                {
                    _current = -1;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            private bool IsCollectionUnchanged
            {
                get
                {
                    return (_collection.RepeatCount == _count) && (_collection.CopiedItem == _item);
                }
            }



            private object _item;
            private int _count;
            private int _current;
            private MultipleCopiesCollection _collection;

        }



        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }



        private object _item; private int _count;

        private const string CountName = "Count";
        private const string IndexerName = "Item[]";

    }
}
