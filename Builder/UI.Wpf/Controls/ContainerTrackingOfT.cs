using System.Diagnostics;

namespace Questify.Builder.UI.Wpf.Controls
{
    internal class ContainerTracking<T>
    {
        private T _container;
        private ContainerTracking<T> _next;
        private ContainerTracking<T> _previous;

        internal ContainerTracking(T container)
        {
            _container = container;
        }

        internal T Container
        {
            get { return _container; }
        }

        internal ContainerTracking<T> Next
        {
            get { return _next; }
        }

        internal ContainerTracking<T> Previous
        {
            get { return _previous; }
        }

        internal void StartTracking(ref ContainerTracking<T> tail)
        {
            Debug.Assert(this.Next == null && this.Previous == null);

            if (tail != null)
            {
                tail._next = this;
            }

            _previous = tail;
            tail = this;
        }

        internal void StopTracking(ref ContainerTracking<T> tail)
        {
            if (_previous != null)
            {
                _previous._next = _next;
            }

            if (_next != null)
            {
                _next._previous = _previous;
            }

            if (tail == this)
            {
                tail = _previous;
            }

            _previous = null;
            _next = null;
        }


        [Conditional("DEBUG")]
        internal void Debug_AssertIsInList(ContainerTracking<T> tail)
        {
#if DEBUG
            Debug.Assert(IsInList(tail), "This container should be in the tracking list.");
#endif
        }

        [Conditional("DEBUG")]
        internal void Debug_AssertNotInList(ContainerTracking<T> tail)
        {
#if DEBUG
            Debug.Assert(!IsInList(tail), "This container shouldn't be in our tracking list");
#endif
        }

#if DEBUG
        private bool IsInList(ContainerTracking<T> tail)
        {
            ContainerTracking<T> node = tail;

            while (node != null)
            {
                if (node == this)
                {
                    return true;
                }

                node = node._next;
            }

            return false;
        }
#endif



    }
}
