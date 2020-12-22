using System.Windows;

namespace Questify.Builder.UI.Wpf.Presentation.Types
{
    public class RoutedEventArgs<T> : RoutedEventArgs
    {
        private readonly T _parameter;

        public RoutedEventArgs(T arg)
            : base()
        {
            _parameter = arg;
        }

        public RoutedEventArgs(RoutedEvent routedEvent, T arg)
            : base(routedEvent)
        {
            _parameter = arg;
        }

        public RoutedEventArgs(RoutedEvent routedEvent, object sender, T arg)
            : base(routedEvent, sender)
        {
            _parameter = arg;
        }

        public T Parameter
        {
            get { return _parameter; }
        }
    }

    public delegate void RoutedEventHandler<T>(object sender, RoutedEventArgs<T> e);
}
