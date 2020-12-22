

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{



    [Serializable]
    public class ObservableSortedDictionary<TKey, TValue> : ObservableDictionary<TKey, TValue>, ISerializable, IDeserializationCallback
    {


        public ObservableSortedDictionary(IComparer<DictionaryEntry> comparer)
            : base()
        {
            _comparer = comparer;
        }

        public ObservableSortedDictionary(IComparer<DictionaryEntry> comparer, IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
            _comparer = comparer;
        }

        public ObservableSortedDictionary(IComparer<DictionaryEntry> comparer, IEqualityComparer<TKey> equalityComparer)
            : base(equalityComparer)
        {
            _comparer = comparer;
        }

        public ObservableSortedDictionary(IComparer<DictionaryEntry> comparer, IDictionary<TKey, TValue> dictionary,
            IEqualityComparer<TKey> equalityComparer)
            : base(dictionary, equalityComparer)
        {
            _comparer = comparer;
        }

        protected ObservableSortedDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _siInfo = info;
        }





        protected override bool AddEntry(TKey key, TValue value)
        {
            DictionaryEntry entry = new DictionaryEntry(key, value);
            int index = GetInsertionIndexForEntry(entry);
            _keyedEntryCollection.Insert(index, entry);
            return true;
        }

        protected virtual int GetInsertionIndexForEntry(DictionaryEntry newEntry)
        {
            return BinaryFindInsertionIndex(0, Count - 1, newEntry);
        }

        protected override bool SetEntry(TKey key, TValue value)
        {
            bool keyExists = _keyedEntryCollection.Contains(key);

            if (keyExists && value.Equals((TValue)_keyedEntryCollection[key].Value))
                return false;

            if (keyExists)
                _keyedEntryCollection.Remove(key);

            DictionaryEntry entry = new DictionaryEntry(key, value);
            int index = GetInsertionIndexForEntry(entry);
            _keyedEntryCollection.Insert(index, entry);

            return true;
        }



        private int BinaryFindInsertionIndex(int first, int last, DictionaryEntry entry)
        {
            if (last < first)
                return first;
            else
            {
                int mid = first + (int)((last - first) / 2);
                int result = _comparer.Compare(_keyedEntryCollection[mid], entry);
                if (result == 0)
                    return mid;
                else if (result < 0)
                    return BinaryFindInsertionIndex(mid + 1, last, entry);
                else
                    return BinaryFindInsertionIndex(first, mid - 1, entry);
            }
        }





        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!_comparer.GetType().IsSerializable)
            {
                throw new NotSupportedException("The supplied Comparer is not serializable.");
            }

            base.GetObjectData(info, context);
            info.AddValue("_comparer", _comparer);
        }



        public override void OnDeserialization(object sender)
        {
            if (_siInfo != null)
            {
                _comparer = (IComparer<DictionaryEntry>)_siInfo.GetValue("_comparer", typeof(IComparer<DictionaryEntry>));
            }
            base.OnDeserialization(sender);
        }




        private IComparer<DictionaryEntry> _comparer;

        [NonSerialized]
        private SerializationInfo _siInfo = null;

    }
}
