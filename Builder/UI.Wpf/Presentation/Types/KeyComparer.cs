using System;
using System.Collections;
using System.Collections.Generic;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    internal class KeyComparer : IComparer<DictionaryEntry>
    {

        public static KeyComparer ForInvariantStrings()
        {
            return new KeyComparer(StringComparer.InvariantCultureIgnoreCase);
        }

        private readonly IComparer _comp;

        public KeyComparer(IComparer comparer)
        {
            _comp = comparer;
        }

        public int Compare(DictionaryEntry x, DictionaryEntry y)
        {
            return _comp.Compare(x.Key, y.Key);
        }
    }
}
