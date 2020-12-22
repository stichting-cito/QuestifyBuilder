using System.Collections;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockRowEqualizationCollection : IEnumerable
    {
        readonly IEnumerable _originalElements;
        readonly int _numberOfElementsToReturn;

        public BlockRowEqualizationCollection(IEnumerable originalElements, int numberOfElementsToReturn)
        {
            _originalElements = originalElements;
            _numberOfElementsToReturn = numberOfElementsToReturn;
        }

        public IEnumerator GetEnumerator()
        {
            return new SpecialEnumerator(_numberOfElementsToReturn, _originalElements.GetEnumerator());
        }

        private class SpecialEnumerator : IEnumerator
        {
            int position = -1;
            bool originalEnumeratorEnded = false;
            readonly int _numberOfElementsToReturn;
            readonly IEnumerator _originalEnumerator;

            public SpecialEnumerator(int numberOfElementsToReturn, IEnumerator originalEnumerator)
            {
                _numberOfElementsToReturn = numberOfElementsToReturn;
                _originalEnumerator = originalEnumerator;
            }

            public object Current
            {
                get
                {
                    if (originalEnumeratorEnded)
                        return null;
                    else
                        return _originalEnumerator.Current;
                }
            }

            public bool MoveNext()
            {
                originalEnumeratorEnded = !_originalEnumerator.MoveNext();
                position++;
                return (position < _numberOfElementsToReturn);
            }

            public void Reset()
            {
                position = -1;
                originalEnumeratorEnded = false;
                _originalEnumerator.Reset();
            }
        }
    }
}
